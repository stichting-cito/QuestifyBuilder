using System;
using System.Runtime.Serialization;

namespace Questify.Builder.Logic.Service.Model.Entities.Custom
{
    [DataContract(Namespace = "http://questify.eu/Questify.Model")]
    public class CustomPropertyDisplayValueDto
    {
        [DataMember]
        public Guid CustomPropertyId { get; set; }
        [DataMember]
        public int BankId { get; set; }
        [DataMember]
        public string DisplayValue { get; set; }
    }
}