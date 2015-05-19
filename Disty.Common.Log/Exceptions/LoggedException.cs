using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disty.Common.Log.Exceptions
{
    public class LoggedException : Exception
    {
        public LoggedException(string msg, Exception exception) : base(msg, exception) { }

    }
}
