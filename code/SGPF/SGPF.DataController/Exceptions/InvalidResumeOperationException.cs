using SGPF.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGPF.DataController.Exceptions
{
    public class InvalidResumeOperationException : Exception
    {
        public InvalidResumeOperationException(Person suspendedBy)
            : base(string.Format(ErrorConstants.InvalidResumeOperationFormat, suspendedBy.Name, suspendedBy.Id))
        {
        }
    }
}
