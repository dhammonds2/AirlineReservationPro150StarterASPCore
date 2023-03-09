using System;
using System.Runtime.Serialization;

namespace Airline.UnitTest.ExceptionHandling
{
    [Serializable]
    internal class CustomerLoginException : Exception
    {
        public CustomerLoginException()
        {
        }

        public CustomerLoginException(string message) : base(message)
        {
        }

        public CustomerLoginException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CustomerLoginException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}