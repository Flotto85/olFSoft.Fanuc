using System;
using System.Collections.Generic;
using System.Text;

namespace olFSoft.SDK
{
    public abstract class EnumBase
    {
        private int value;

        protected EnumBase(int value)
        {
            this.value = value;
        }

        public override bool Equals(object obj)
        {
            return obj is EnumBase type &&
                   value == type.value;
        }

        public override int GetHashCode()
        {
            return -1584136870 + value.GetHashCode();
        }

        public static bool operator ==(EnumBase obj1, EnumBase obj2)
        {
            if ((object)obj1 == null && (object)obj2 == null) return true;
            if ((object)obj1 == null || (object)obj2 == null) return false;
            return obj1.value == obj2.value;
        }

        public static bool operator !=(EnumBase obj1, EnumBase obj2)
        {
            return !(obj1 == obj2);
        }
    }
}
