using SGPF.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGPF.DataController.Exceptions
{
    public class InvalidStateException : Exception
    {
        public InvalidStateException(ProjectState state)
            : base(string.Format(ErrorConstants.InvalidStateException, state.ToString()))
        { 
        
        }
    }
}
