using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGPF.DataController.Exceptions
{
    public class PersonNotFoundException : Exception
    {
        public PersonNotFoundException(string id)
            : base(string.Format(ErrorConstants.PersonNotFoundExceptionFormat, id))
        {
        }
    }
}
