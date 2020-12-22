using System.Runtime.Serialization;

namespace Questify.Builder.Logic.Service.Model.Entities
{
    [DataContract(Namespace = "http://questify.eu/Questify.Model")]
    public class TestPackageResourceDto : Logic.Service.Model.Entities.ResourceDto
    {

        public virtual Logic.Service.Model.Entities.ResourceDto Resource { get; set; }
    }
}
