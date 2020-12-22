using System.Runtime.Serialization;

namespace Questify.Builder.Logic.Service.Model.Entities
{
    [DataContract(Namespace = "http://questify.eu/Questify.Model")]
    public class StateAction
    {
        [DataMember]
        public string Target { get; set; }
        [DataMember]
        public int StateId { get; set; }
        [DataMember]
        public int ActionId { get; set; }

        public virtual Logic.Service.Model.Entities.Action Action { get; set; }
        public virtual StateDto State { get; set; }
    }
}
