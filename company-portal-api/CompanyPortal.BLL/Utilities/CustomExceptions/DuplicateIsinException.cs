using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyPortal.BLL.Utilities.CustomExceptions
{
    public class DuplicateIsinException : Exception
    {
        public DuplicateIsinException()
        {
        }

        public DuplicateIsinException(string message)
            : base(message)
        {
        }

        public DuplicateIsinException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
