using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Questify.Builder.Logic.Service.Model.Entities
{
    [DataContract(Namespace = "http://questify.eu/Questify.Model")]
    public class StateDto
    {
        public StateDto()
        {
            this.CustomBankProperty = new HashSet<Logic.Service.Model.Entities.CustomBankPropertyDto>();
            this.Resource = new HashSet<Logic.Service.Model.Entities.ResourceDto>();
            this.StateAction = new HashSet<Logic.Service.Model.Entities.StateAction>();
        }

        [DataMember]
        public int StateId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Title { get; set; }
        [NotMapped]
        public string Description { get; set; }

        public virtual ICollection<Logic.Service.Model.Entities.CustomBankPropertyDto> CustomBankProperty { get; set; }
        public virtual ICollection<Logic.Service.Model.Entities.ResourceDto> Resource { get; set; }
        public virtual ICollection<Logic.Service.Model.Entities.StateAction> StateAction { get; set; }
    }
}
