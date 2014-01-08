using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGPF.Data
{
    public class ProjectHistory : IEquatable<ProjectHistory>
    {
        public int ProjectId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public bool Equals(ProjectHistory other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(ProjectId, other.ProjectId) && Date.Equals(other.Date);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ProjectHistory) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((ProjectId != null ? ProjectId.GetHashCode() : 0)*397) ^ Date.GetHashCode();
            }
        }

        public static bool operator ==(ProjectHistory left, ProjectHistory right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ProjectHistory left, ProjectHistory right)
        {
            return !Equals(left, right);
        }
    }
}
