using olFSoft.SDK;
using System;
using System.Collections.Generic;
using System.Text;

namespace olFSoft.Focas
{
    public class FanucControlReturnCode : EnumBase
    {
        public static FanucControlReturnCode EW_OK = new FanucControlReturnCode(0);

        public FanucControlReturnCode(int value)
            : base(value)
        {
        }
    }
}
