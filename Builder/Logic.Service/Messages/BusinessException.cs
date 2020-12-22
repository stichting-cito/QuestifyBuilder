using System;

namespace Questify.Builder.Logic.Service.Messages
{
    public class BusinessException
           : ApplicationException
    {
        public BusinessException(BusinessExceptionEnum businessExceptionType, string message)
            : base(message)
        {
            ExceptionType = businessExceptionType;
        }

        public BusinessExceptionEnum ExceptionType { get; set; }
    }
}
