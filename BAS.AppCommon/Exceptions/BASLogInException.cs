using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAS.AppCommon.Exceptions
{
    public class BASLogInException : Exception
    {
        public BASLogInException(string message) : base(message)
        {

        }
    }
}
