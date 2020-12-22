using System.Runtime.Serialization;

namespace Questify.Builder.Logic.Service.Model.Entities
{
    [DataContract(Namespace = "http://questify.eu/Questify.Model")]
    public class GenericResourceDto : ResourceDto
    {
        [DataMember]
        public string MediaType { get; set; }
        [DataMember]
        public int Size { get; set; }
        [DataMember]
        public string Dimensions { get; set; }
        [DataMember]
        public bool IsTemplate { get; set; }

        public virtual ResourceDto Resource { get; set; }
    }
}
