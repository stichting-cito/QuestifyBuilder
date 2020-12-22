using System.Runtime.Serialization;

namespace Questify.Builder.Logic.Service.Model.Entities
{
    [DataContract(Namespace = "http://questify.eu/Questify.Model")]
    public class AspectResourceDto : ResourceDto
    {
        [DataMember]
        public int RawScore { get; set; }

        public virtual ResourceDto Resource { get; set; }
    }
}
