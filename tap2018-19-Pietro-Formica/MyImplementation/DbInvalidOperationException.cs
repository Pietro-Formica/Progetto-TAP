using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyImplementation
{
    class DbInvalidOperationException : IExceptionDb
    {
        public void GetException()
        {
            throw new InvalidOperationException();
        }
    }
}
