using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGPF.Data
{
    public class UserSession : IEquatable<UserSession>
    {
        public BasePerson UserDetails { get; set; }

        public bool Equals(UserSession other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(UserDetails, other.UserDetails);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((UserSession) obj);
        }

        public override int GetHashCode()
        {
            return (UserDetails != null ? UserDetails.GetHashCode() : 0);
        }

        public static bool operator ==(UserSession left, UserSession right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(UserSession left, UserSession right)
        {
            return !Equals(left, right);
        }
    }
}
