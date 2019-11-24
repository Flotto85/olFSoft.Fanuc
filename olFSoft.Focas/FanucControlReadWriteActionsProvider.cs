using System;
using System.Collections.Generic;
using System.Text;

namespace olFSoft.Focas
{
    public class FanucControlReadWriteActionsProvider : IFanucControlReadWriteActionsProvider
    {
        public delegate void ReadWriteAction(ushort connectionHandle);

        public ReadWriteAction GetReadWriteActions()
        {
            throw new NotImplementedException();
        }
    }
}
