using System;
using System.Collections.Generic;
using System.Text;

namespace olFSoft.Focas
{
    internal class FanucControlEthernetConnection
        : IFanucControlConnection
    {
        public IObservable<FanucControlConnectionState> ConnectionState => throw new NotImplementedException();

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IFanucControl Open(bool autoReconnect = true, int autoReconnectTimeout = 10000)
        {
            throw new NotImplementedException();
        }
    }
}
