using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Questify.Builder.Logic.Service.Model.Entities
{
    [DataContract(Namespace = "http://questify.eu/Questify.Model")]
    public class UserDto
    {
        public UserDto()
        {
            this.Bank = new HashSet<Logic.Service.Model.Entities.BankDto>();
            this.Bank1 = new HashSet<Logic.Service.Model.Entities.BankDto>();
            this.CustomBankProperty = new HashSet<Logic.Service.Model.Entities.CustomBankPropertyDto>();
            this.CustomBankProperty1 = new HashSet<Logic.Service.Model.Entities.CustomBankPropertyDto>();
            this.Resource = new HashSet<Logic.Service.Model.Entities.ResourceDto>();
            this.Resource1 = new HashSet<Logic.Service.Model.Entities.ResourceDto>();
            this.User1 = new HashSet<UserDto>();
            this.User11 = new HashSet<UserDto>();
        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string FullName { get; set; }
        [DataMember]
        public bool Active { get; set; }
        [DataMember]
        public System.DateTime CreationDate { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }
        [DataMember]
        public int ModifiedBy { get; set; }
        [DataMember]
        public string AuthenticationType { get; set; }
        [NotMapped]
        public string UserSettings { get; set; }
        [DataMember]
        public bool ChangePassword { get; set; }

        public virtual ICollection<Logic.Service.Model.Entities.BankDto> Bank { get; set; }
        public virtual ICollection<Logic.Service.Model.Entities.BankDto> Bank1 { get; set; }
        public virtual ICollection<Logic.Service.Model.Entities.CustomBankPropertyDto> CustomBankProperty { get; set; }
        public virtual ICollection<Logic.Service.Model.Entities.CustomBankPropertyDto> CustomBankProperty1 { get; set; }
        public virtual ICollection<Logic.Service.Model.Entities.ResourceDto> Resource { get; set; }
        public virtual ICollection<Logic.Service.Model.Entities.ResourceDto> Resource1 { get; set; }
        public virtual ICollection<UserDto> User1 { get; set; }
        public virtual UserDto User2 { get; set; }
        public virtual ICollection<UserDto> User11 { get; set; }
        public virtual UserDto User3 { get; set; }
    }
}
