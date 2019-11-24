using System;
using System.Collections.Generic;
using System.Text;

namespace olFSoft.Focas
{
    public struct ConnectionHandleAllocationResult
    {
        public FanucControlReturnCode ReturnCode;
        public string Message;
        public ushort ConnectionHandle;
    }
}
