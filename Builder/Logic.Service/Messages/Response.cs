using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;

namespace Questify.Builder.Logic.Service.Messages
{
    [DataContract]
    public class Response
    {

        public Response() : this(0) { }

        public Response(long entityId)
        {
            EntityIdInstance = entityId;
        }


        [DataMember]
        private BusinessExceptionDto BusinessExceptionInstance;

        [DataMember]
        private readonly IList<BusinessWarning> BusinessWarningList = new List<BusinessWarning>();

        [DataMember]
        private readonly long EntityIdInstance;


        public void AddBusinessException(BusinessException exception)
        {
            BusinessExceptionInstance = new BusinessExceptionDto(exception.ExceptionType, exception.Message, exception.StackTrace);
        }

        public void AddBusinessWarnings(IEnumerable<BusinessWarning> warnings)
        {
            warnings.ToList().ForEach(w => BusinessWarningList.Add(w));
        }


        public bool HasWarning
        {
            get { return BusinessWarningList.Count > 0; }
        }

        public IEnumerable<BusinessWarning> BusinessWarnings
        {
            get { return new ReadOnlyCollection<BusinessWarning>(BusinessWarningList); }
        }

        public long EntityId
        {
            get { return EntityIdInstance; }
        }

        public bool HasException
        {
            get { return BusinessExceptionInstance != null; }
        }

        public BusinessExceptionDto BusinessException
        {
            get { return BusinessExceptionInstance; }
        }

    }
}
