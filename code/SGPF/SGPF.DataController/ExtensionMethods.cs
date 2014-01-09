using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGPF.DataController
{
    public static class ExtensionMethods
    {
        public static T Random<T>(this IEnumerable<T> elements)
        {
            return elements.Skip(new Random((int)System.DateTime.Now.Ticks).Next(elements.Count())).FirstOrDefault();
        }
    }
}
