
using System.Runtime.Serialization;

namespace Questify.Builder.Logic.Service.Model.Entities
{
    [DataContract(Namespace = "http://questify.eu/Questify.Model")]
    public class ItemLayoutTemplateResourceDto : ResourceDto
    {
        [DataMember]
        public string ItemType { get; set; }

        [DataMember]
        public string ItemTypeString { get; set; }

        public virtual ResourceDto Resource { get; set; }
    }
}
