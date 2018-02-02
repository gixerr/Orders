using System;

namespace Orders.Infrastructure.Exceptions
{
    public class ServiceException : Core.Exceptions.OrdersException
    {
        public ServiceException()
        {
        }

        public ServiceException(string errorCode) 
            : base(errorCode)
        {
        }

        public ServiceException(string message, params object[] args)
            : base(message, args)
        {
        }

        public ServiceException(string errorCode, string message, params object[] args)
            : base(errorCode, null, message, args)
        {
        }

        public ServiceException(Exception innerException, string message, params object[] args)
            : base(innerException, message, args)
        {
        }

        public ServiceException(string errorCode, Exception innerException, string message, params object[] args)
            : base(errorCode, innerException, message, args)
        {
        }
    }
}
