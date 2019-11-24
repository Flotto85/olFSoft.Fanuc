using System;

namespace olFSoft.Focas
{
    public interface IFanucControlConnection : IDisposable
    {
        IFanucControl Open(bool autoReconnect = true, int autoReconnectTimeout = 10000);

        void Close();

        IObservable<FanucControlConnectionState> ConnectionState { get; }
    }
}