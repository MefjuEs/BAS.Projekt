using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAS.AppCommon.Exceptions
{
    public class BASNotFoundException : Exception
    {
        public BASNotFoundException(string message) : base(message)
        {

        }
    }
}
