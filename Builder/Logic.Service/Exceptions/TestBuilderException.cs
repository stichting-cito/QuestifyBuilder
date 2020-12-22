using System;

namespace Questify.Builder.Logic.Service.Exceptions
{
    [Serializable()]
    public abstract class TestBuilderException : Exception
    {
        protected TestBuilderException() : base()
        {
        }

        protected TestBuilderException(string message) : base(message)
        {
        }

        protected TestBuilderException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TestBuilderException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}
