using System.Runtime.Serialization;

namespace Questify.Builder.Logic.Service.Messages
{
    [DataContract]
    public abstract class DtoBase
 : IDtoResponseEnvelop
    {

        [DataMember]
        private readonly Response ResponseInstance = new Response();
        public Response Response
        {
            get { return ResponseInstance; }
        }

    }
}

