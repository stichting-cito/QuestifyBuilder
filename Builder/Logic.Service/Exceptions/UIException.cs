using System;

namespace Questify.Builder.Logic.Service.Exceptions
{
    [Serializable()]
    public class UIException : TestBuilderException
    {
        public UIException() : base()
        {
        }

        public UIException(string message) : base(message)
        {
        }

        public UIException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UIException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}
