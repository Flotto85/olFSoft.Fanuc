using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Text;

namespace olFSoft.Focas
{
    internal abstract class FanucControlConnectionBase<TConnectionSettings> 
        : IFanucControlConnection
    {
        private FanucControlConnectionState currentConnectionState;
        private TConnectionSettings connectionSettings;

        protected FanucControlConnectionBase(TConnectionSettings connectionSettings)
        {
            this.connectionSettings = connectionSettings;
            currentConnectionState = FanucControlConnectionState.Disconnected;

            ConnectionState = new BehaviorSubject<FanucControlConnectionState>(currentConnectionState);
        }

        public IObservable<FanucControlConnectionState> ConnectionState { get; }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IFanucControl Open(
            bool autoReconnect = true,
            int autoReconnectTimeout = 10000)
        {
            throw new NotImplementedException();
        }

        protected abstract ConnectionHandleAllocationResult AllocateConnectionHandle(TConnectionSettings connectionSettings);
    }
}
