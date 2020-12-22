using System;

namespace Questify.Builder.Logic.Service.Exceptions
{
    [Serializable()]
    public class AppLogicException : TestBuilderException
    {
        public AppLogicException() : base()
        {
        }

        public AppLogicException(string message) : base(message)
        {
        }

        public AppLogicException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AppLogicException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}
