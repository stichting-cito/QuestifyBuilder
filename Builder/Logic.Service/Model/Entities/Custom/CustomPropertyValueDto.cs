using System;
using System.Runtime.Serialization;

namespace Questify.Builder.Logic.Service.Model.Entities.Custom
{
    [DataContract(Namespace = "http://questify.eu/Questify.Model")]
    public class CustomPropertyValueDto
    {

        [DataMember]
        public Guid CustomPropertyValueId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string DisplayValue { get; set; }

    }
}
