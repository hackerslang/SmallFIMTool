using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallFIMTool.Exceptions
{
    public class IncorrectUsageException : Exception
    {
        public IncorrectUsageException() { }
        public IncorrectUsageException(string message) : base(message) { }
        public IncorrectUsageException(string message, Exception innerException) : base(message, innerException) { }
    }
}
