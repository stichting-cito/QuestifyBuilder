using System.Runtime.Serialization;

namespace Questify.Builder.Logic.Service.Model.Entities.Custom
{
    public class CustomBankPropertyResourceDto : ResourceDto
    {
        [DataMember]
        public string ApplicableToString { get; set; }

        [DataMember]
        public string CustomPropertyTypeString { get; set; }

        [DataMember]
        public int? ApplicableToMask { get; set; }

        [DataMember]
        public CustomPropertyType CustomPropertyType { get; set; }
    }
}
