using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using CustomPropertyValueDto = Questify.Builder.Logic.Service.Model.Entities.Custom.CustomPropertyValueDto;

namespace Questify.Builder.Logic.Service.Model.Entities
{
    [DataContract(Namespace = "http://questify.eu/Questify.Model")]
    public class CustomBankPropertyDto
    {
        public CustomBankPropertyDto()
        {
            this.CustomBankPropertyValue = new HashSet<CustomBankPropertyValue>();
        }

        [DataMember]
        public System.Guid CustomBankPropertyId { get; set; }
        [DataMember]
        public int BankId { get; set; }
        [DataMember]
        public Nullable<int> ApplicableToMask { get; set; }
        [DataMember]
        public bool Publishable { get; set; }
        [DataMember]
        public bool Scorable { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public System.DateTime CreationDate { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }
        [DataMember]
        public int ModifiedBy { get; set; }
        [DataMember]
        public System.Guid Code { get; set; }
        [DataMember]
        public Nullable<int> StateId { get; set; }
        [DataMember]
        public string Version { get; set; }

        [DataMember]
        public string BankName { get; set; }
        [DataMember]
        public string CreatedByFullName { get; set; }
        [DataMember]
        public string ModifiedByFullName { get; set; }
        [DataMember]
        public CustomPropertyType CustomPropertyType { get; set; }
        [DataMember]
        public IList<CustomPropertyValueDto> Values { get; set; }

        public virtual Logic.Service.Model.Entities.BankDto Bank { get; set; }
        public virtual ICollection<CustomBankPropertyValue> CustomBankPropertyValue { get; set; }
        public virtual StateDto State { get; set; }
        public virtual UserDto User { get; set; }
        public virtual UserDto User1 { get; set; }

    }
}
