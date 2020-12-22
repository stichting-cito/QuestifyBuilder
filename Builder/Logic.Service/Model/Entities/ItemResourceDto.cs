using System;
using System.Runtime.Serialization;

namespace Questify.Builder.Logic.Service.Model.Entities
{
    [DataContract(Namespace = "http://questify.eu/Questify.Model")]
    public class ItemResourceDto : ResourceDto
    {
        [DataMember]
        public bool IsSystemItem { get; set; }
        [DataMember]
        public int? AlternativesCount { get; set; }
        [DataMember]
        public string KeyValues { get; set; }
        [DataMember]
        public int? ResponseCount { get; set; }
        [DataMember]
        public int? RawScore { get; set; }
        [DataMember]
        public string ILTName { get; set; }
        [DataMember]
        public int? ILTVersion { get; set; }
        [DataMember]
        public decimal? MaxScore { get; set; }
        [DataMember]
        public int ItemAutoId { get; set; }
        [DataMember]
        public string ItemId { get; set; }

        [DataMember]
        public string ItemTypeFromItemLayoutTemplate { get; set; }
        [DataMember]
        public string ItemTypeFromItemLayoutTemplateString { get; set; }
        [DataMember]
        public string ItemLayoutTemplateUsedName { get; set; }
        [DataMember]
        public string InclusionGroupCode { get; set; }
        [DataMember]
        public string ExclusionGroupCode { get; set; }

        public virtual ResourceDto Resource { get; set; }
    }
}
