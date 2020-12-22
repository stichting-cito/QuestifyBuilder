
using System.Runtime.Serialization;

namespace Questify.Builder.Logic.Service.Model.Entities
{
    [DataContract(Namespace = "http://questify.eu/Questify.Model")]
    public class CustomBankPropertyValue
    {
        [DataMember]
        public System.Guid ResourceId { get; set; }
        [DataMember]
        public System.Guid CustomBankPropertyId { get; set; }
        [DataMember]
        public string DisplayValue { get; set; }

        public virtual Logic.Service.Model.Entities.CustomBankPropertyDto CustomBankProperty { get; set; }
        public virtual ResourceDto Resource { get; set; }
    }
}
