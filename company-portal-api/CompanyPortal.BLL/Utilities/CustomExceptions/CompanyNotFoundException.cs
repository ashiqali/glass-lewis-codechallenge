using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyPortal.BLL.Utilities.CustomExceptions
{
    public class CompanyNotFoundException : Exception
    {
        public CompanyNotFoundException()
        {
        }

        public CompanyNotFoundException(string message)
            : base(message)
        {
        }

        public CompanyNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
