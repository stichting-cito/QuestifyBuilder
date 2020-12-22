using System;

namespace Questify.Builder.Logic.Service.Exceptions
{
    [Serializable()]
    public class ServiceException : TestBuilderException
    {
        public ServiceException() : base()
        {
        }

        public ServiceException(string message) : base(message)
        {
        }

        public ServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ServiceException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}
