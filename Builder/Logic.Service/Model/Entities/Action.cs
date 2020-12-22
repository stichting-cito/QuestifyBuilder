
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Questify.Builder.Logic.Service.Model.Entities
{
    [DataContract(Namespace = "http://questify.eu/Questify.Model")]
    public class Action
    {
        public Action()
        {
            this.StateAction = new HashSet<StateAction>();
        }

        [DataMember]
        public int ActionId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Title { get; set; }

        public virtual ICollection<StateAction> StateAction { get; set; }
    }
}
