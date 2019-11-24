using olFSoft.SDK;

namespace olFSoft.Focas
{
    public class FanucControlType : EnumBase
    {
        public static FanucControlType Series_30i_31i_32iModelB = new FanucControlType(0);

        private FanucControlType(int value)
            : base(value)
        {
        }
    }
}
