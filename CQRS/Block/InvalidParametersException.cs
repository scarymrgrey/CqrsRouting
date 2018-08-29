using System;

namespace CQRS.Block
{
    public class InvalidParametersException : ApplicationException
    {
        public InvalidParametersException(string message) : base(message)
        {
            
        }   
    }
}