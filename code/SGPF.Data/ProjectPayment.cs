using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGPF.Data
{
    public class ProjectPayment : IEquatable<ProjectPayment>
    {
        public DateTime PaymentDate { get; set; }
        public int ProjectId { get; set; }
        public double Amount { get; set; }

        public bool Equals(ProjectPayment other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(ProjectId, other.ProjectId) && PaymentDate.Equals(other.PaymentDate);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ProjectPayment) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((ProjectId != null ? ProjectId.GetHashCode() : 0)*397) ^ PaymentDate.GetHashCode();
            }
        }

        public static bool operator ==(ProjectPayment left, ProjectPayment right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ProjectPayment left, ProjectPayment right)
        {
            return !Equals(left, right);
        }
    }
}
