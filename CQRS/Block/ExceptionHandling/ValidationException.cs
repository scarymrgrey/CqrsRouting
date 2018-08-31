using System;

namespace CQRS.Block
{
    public class ValidationException : ApplicationException
    {
        public ValidationException(string message) : base(message)
        {
        }   
    }
}