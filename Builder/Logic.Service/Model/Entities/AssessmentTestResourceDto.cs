using System.Runtime.Serialization;

namespace Questify.Builder.Logic.Service.Model.Entities
{
    [DataContract(Namespace = "http://questify.eu/Questify.Model")]
    public class AssessmentTestResourceDto : ResourceDto
    {
        [DataMember]
        public bool IsTemplate { get; set; }

        public virtual ResourceDto Resource { get; set; }

        [DataMember]
        public bool CanPropose { get; set; }
    }
}
