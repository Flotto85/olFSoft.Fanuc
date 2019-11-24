using FluentAssertions;
using Moq;
using olFSoft.Focas;
using System;
using System.Threading;
using Xunit;

namespace olFSoft.Focas.Tests
{
    public class FanucControlConnectionLoopTests
    {
        private FanucControlConnectionLoop connectionLoop;

        public FanucControlConnectionLoopTests()
        {
            Func<ConnectionHandleAllocationResult> allocateHandle = () => new ConnectionHandleAllocationResult() { ConnectionHandle = 1, Message = "", ReturnCode = FanucControlReturnCode.EW_OK };
            var readWriteActionProviderMock = new Mock<IFanucControlReadWriteActionsProvider>();
            readWriteActionProviderMock.Setup((x) => x.GetReadWriteActions()).Returns(new FanucControlReadWriteActionsProvider.ReadWriteAction((ushort handle) => { }));
            var freeHandle = new Action<ushort>((handle) => { });
            connectionLoop = new FanucControlConnectionLoop(allocateHandle, readWriteActionProviderMock.Object, freeHandle);
        }

        [Fact]
        public void StopAndDisconnect_DoesNotBlockIfNotConnected()
        {
            ManualResetEvent waitForFinished = new ManualResetEvent(false);
            Thread t = new Thread(() =>
            {
                connectionLoop.StopAndDisconnect();
                waitForFinished.Set();
            });
            waitForFinished.Reset();
            t.Start();
            bool noTimeout = waitForFinished.WaitOne(1000);
            noTimeout.Should().BeTrue();
        }

        [Fact]
        public void ConnectAndRun_DoesNotBlock()
        {
            ManualResetEvent waitForFinished = new ManualResetEvent(false);
            Thread t = new Thread(() =>
            {
                connectionLoop.ConnectAndRun();
                waitForFinished.Set();
            });
            waitForFinished.Reset();
            t.Start();
            bool noTimeout = waitForFinished.WaitOne(1000);
            noTimeout.Should().BeTrue();
        }
    }
}
