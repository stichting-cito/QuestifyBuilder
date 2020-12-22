using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Questify.Builder.Logic.Service.Model.Entities
{
    [DataContract(Namespace = "http://questify.eu/Questify.Model")]
    public class BankDto
    {
        public BankDto()
        {
            this.Bank1 = new HashSet<BankDto>();
            this.CustomBankProperty = new HashSet<CustomBankPropertyDto>();
            this.Resource = new HashSet<ResourceDto>();
        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public Nullable<int> ParentBankId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public string ExternalIdentifier { get; set; }
        [DataMember]
        public System.DateTime CreationDate { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }
        [DataMember]
        public int ModifiedBy { get; set; }

        [DataMember]
        public IList<BankDto> BankCollection { get; set; }

        public virtual ICollection<BankDto> Bank1 { get; set; }
        public virtual BankDto Bank2 { get; set; }
        public virtual ICollection<CustomBankPropertyDto> CustomBankProperty { get; set; }
        public virtual ICollection<ResourceDto> Resource { get; set; }
        public virtual UserDto User { get; set; }
        public virtual UserDto User1 { get; set; }
    }
}
