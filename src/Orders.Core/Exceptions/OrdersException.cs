using System;

namespace Orders.Core.Exceptions
{
    public class OrdersException : Exception
    {
        public string ErrorCode { get; }

        public OrdersException()
        {
        }

        public OrdersException(string errorCode) : this(errorCode, null, null, null)
        {
        }

        public OrdersException(string message, params object[] args) 
            : this(null, null, message, args)
        {
        }

        public OrdersException(string errorCode, string message, params object[] args) 
            : this(errorCode, null, message, args)
        {
        }

        public OrdersException(Exception innerException, string message, params object[] args)
            : this(string.Empty, innerException, message, args)
        {
        }

        public OrdersException(string errorCode, Exception innerException, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            ErrorCode = errorCode;
        }        
    }
}