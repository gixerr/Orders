using System;

namespace Orders.Core.Exceptions
{
    public class OrderException : Exception
    {
        public string ErrorCode { get; }

        public OrderException()
        {
        }

        public OrderException(string errorCode) : this(errorCode, null, null, null)
        {
        }

        public OrderException(string message, params object[] args) 
            : this(null, null, message, args)
        {
        }

        public OrderException(string errorCode, string message, params object[] args) 
            : this(errorCode, null, message, args)
        {
        }

        public OrderException(Exception innerException, string message, params object[] args)
            : this(string.Empty, innerException, message, args)
        {
        }

        public OrderException(string errorCode, Exception innerException, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            ErrorCode = errorCode;
        }        
    }
}