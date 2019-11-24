using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;

namespace olFSoft.Focas
{
    public class FanucControlConnectionLoop
    {
        private Func<ConnectionHandleAllocationResult> allocateHandle;
        private IFanucControlReadWriteActionsProvider readWriteActionsProvider;
        private Action<ushort> freeHandle;
        private FanucControlConnectionState currentConnectionState;
        private BehaviorSubject<FanucControlConnectionState> connectionStateSubject;
        private Thread connectionThread;
        private ManualResetEvent waitForConnectedOrFailed;
        private ManualResetEvent waitForStopped;

        public IObservable<FanucControlConnectionState> ConnectionState => connectionStateSubject;

        public FanucControlConnectionLoop(
            Func<ConnectionHandleAllocationResult> allocateHandle,
            IFanucControlReadWriteActionsProvider readWriteActionsProvider,
            Action<ushort> freeHandle)
        {
            currentConnectionState = FanucControlConnectionState.Disconnected;
            connectionStateSubject = new BehaviorSubject<FanucControlConnectionState>(currentConnectionState);

            this.allocateHandle = allocateHandle;
            this.readWriteActionsProvider = readWriteActionsProvider;
            this.freeHandle = freeHandle;

            waitForConnectedOrFailed = new ManualResetEvent(false);
            waitForStopped = new ManualResetEvent(false);

            connectionThread = new Thread(() => ConnectionLoop());
        }

        public void ConnectAndRun()
        {
            connectionThread.Start();
            waitForConnectedOrFailed.Reset();
            waitForConnectedOrFailed.WaitOne();
        }

        public void StopAndDisconnect()
        {
            if (!runLoop)
                return;
            stopLoop = true;
            waitForStopped.Reset();
            waitForStopped.WaitOne();
        }

        private bool runLoop = false;
        private bool stopLoop;
        private void ConnectionLoop()
        {
            SetConnectionState(FanucControlConnectionState.Connecting);

            var allocationResult = allocateHandle();

            if (allocationResult.ReturnCode != FanucControlReturnCode.EW_OK)
            {
                SetConnectionState(FanucControlConnectionState.Faulted);
                runLoop = false;
            }
            else
            {
                SetConnectionState(FanucControlConnectionState.Connected);
                runLoop = true;
            }

            stopLoop = false;
            waitForConnectedOrFailed.Set();
           
            while (runLoop)
            {
                readWriteActionsProvider.GetReadWriteActions()(allocationResult.ConnectionHandle);
                Thread.Sleep(10);
                if (stopLoop)
                    runLoop = false;
            }

            SetConnectionState(FanucControlConnectionState.Disconnecting);
            freeHandle(allocationResult.ConnectionHandle);
            SetConnectionState(FanucControlConnectionState.Disconnected);
            waitForStopped.Set();
        }

        private void SetConnectionState(FanucControlConnectionState connectionState)
        {
            if (connectionState != currentConnectionState)
            {
                currentConnectionState = connectionState;
                connectionStateSubject.OnNext(currentConnectionState);
            }
        }
    }
}
