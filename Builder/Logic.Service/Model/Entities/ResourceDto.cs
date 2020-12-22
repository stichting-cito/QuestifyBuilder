using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using CustomPropertyDisplayValueDto = Questify.Builder.Logic.Service.Model.Entities.Custom.CustomPropertyDisplayValueDto;

namespace Questify.Builder.Logic.Service.Model.Entities
{
    [KnownType(typeof(Logic.Service.Model.Entities.AspectResourceDto))]
    [KnownType(typeof(Logic.Service.Model.Entities.AssessmentTestResourceDto))]
    [KnownType(typeof(Logic.Service.Model.Entities.ControlTemplateResourceDto))]
    [KnownType(typeof(Logic.Service.Model.Entities.DataSourceResourceDto))]
    [KnownType(typeof(Logic.Service.Model.Entities.GenericResourceDto))]
    [KnownType(typeof(Logic.Service.Model.Entities.ItemLayoutTemplateResourceDto))]
    [KnownType(typeof(Logic.Service.Model.Entities.ItemResourceDto))]
    [KnownType(typeof(Logic.Service.Model.Entities.PackageResourceDto))]
    [KnownType(typeof(TestPackageResourceDto))]
    [DataContract(Namespace = "http://questify.eu/Questify.Model")]
    public class ResourceDto
    {
        public ResourceDto()
        {
            this.CustomBankPropertyValue = new HashSet<Logic.Service.Model.Entities.CustomBankPropertyValue>();
            this.Dependencies = new HashSet<ResourceDto>();
            this.References = new HashSet<ResourceDto>();
        }

        [DataMember]
        public System.Guid ResourceId { get; set; }
        [DataMember]
        public string Version { get; set; }
        [DataMember]
        public int BankId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Title { get; set; }
        [NotMapped]
        public string Description { get; set; }
        [DataMember]
        public Nullable<int> StateId { get; set; }
        [DataMember]
        public System.DateTime CreationDate { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }
        [DataMember]
        public int ModifiedBy { get; set; }
        [DataMember]
        public string OriginalVersion { get; set; }
        [DataMember]
        public string OriginalName { get; set; }

        public virtual Logic.Service.Model.Entities.AspectResourceDto AspectResource { get; set; }
        public virtual Logic.Service.Model.Entities.AssessmentTestResourceDto AssessmentTestResource { get; set; }
        public virtual Logic.Service.Model.Entities.ControlTemplateResourceDto ControlTemplateResource { get; set; }
        public virtual Logic.Service.Model.Entities.DataSourceResourceDto DataSourceResource { get; set; }
        public virtual Logic.Service.Model.Entities.GenericResourceDto GenericResource { get; set; }
        public virtual Logic.Service.Model.Entities.ItemLayoutTemplateResourceDto ItemLayoutTemplateResource { get; set; }
        public virtual Logic.Service.Model.Entities.PackageResourceDto PackageResource { get; set; }
        public virtual TestPackageResourceDto TestPackageResource { get; set; }
        public virtual Logic.Service.Model.Entities.ItemResourceDto ItemResource { get; set; }
        public virtual Logic.Service.Model.Entities.BankDto Bank { get; set; }
        public virtual ICollection<Logic.Service.Model.Entities.CustomBankPropertyValue> CustomBankPropertyValue { get; set; }
        public virtual ICollection<ResourceDto> Dependencies { get; set; }
        public virtual ICollection<ResourceDto> References { get; set; }
        public virtual StateDto State { get; set; }
        public virtual UserDto UserCreated { get; set; }
        public virtual UserDto UserModified { get; set; }

        private bool _visibleInPicker = true;

        [DataMember]
        public string BankName { get; set; }
        [DataMember]
        public string StateName { get; set; }
        [DataMember]
        public string CreatedByFullName { get; set; }
        [DataMember]
        public string ModifiedByFullName { get; set; }
        [DataMember]
        public IEnumerable<Guid> ReferencedResourceIds { get; set; }
        [DataMember]
        public int ReferenceCount { get; set; }
        [DataMember]
        public IEnumerable<Guid> DependentResourceIds { get; set; }
        [DataMember]
        public bool IsSelectable { get; set; }
        [DataMember]
        public IEnumerable<CustomPropertyDisplayValueDto> CustomPropertyDisplayValues { get; set; }
        [DataMember]
        public bool VisibleInPicker { get { return _visibleInPicker; } set { _visibleInPicker = value; } }
        [DataMember]
        public IList<int> SetToInvisibleAtBankIds { get; set; }
    }
}
