using System;
using MyImplementation.Exceptions.Interface;

namespace MyImplementation.Exceptions
{
    class DbInvalidOperationException : IExceptionDb
    {
        public void GetException()
        {
            throw new InvalidOperationException();
        }
    }
}
