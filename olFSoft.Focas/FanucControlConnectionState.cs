using olFSoft.SDK;

namespace olFSoft.Focas
{
    public class FanucControlConnectionState : EnumBase
    {
        public static FanucControlConnectionState Disconnected = new FanucControlConnectionState(0);
        public static FanucControlConnectionState Connecting = new FanucControlConnectionState(1);
        public static FanucControlConnectionState Connected = new FanucControlConnectionState(2);
        public static FanucControlConnectionState Disconnecting = new FanucControlConnectionState(3);
        public static FanucControlConnectionState Faulted = new FanucControlConnectionState(666);

        private FanucControlConnectionState(int value)
            : base(value)
        {
        }
    }
}
