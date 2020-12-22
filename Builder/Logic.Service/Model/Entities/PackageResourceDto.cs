using System.Runtime.Serialization;

namespace Questify.Builder.Logic.Service.Model.Entities
{
    [DataContract(Namespace = "http://questify.eu/Questify.Model")]
    public class PackageResourceDto : ResourceDto
    {

        public virtual ResourceDto Resource { get; set; }
    }
}
