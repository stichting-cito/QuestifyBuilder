Imports System
Imports SD.LLBLGen.Pro.ORMSupportClasses
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.Model.ContentModel

Namespace Questify.Builder.Model.ContentModel.HelperClasses

    Public Class ActionFields

        Public Shared ReadOnly Property [ActionId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ActionFieldIndex.ActionId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Name] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ActionFieldIndex.Name), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Title] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ActionFieldIndex.Title), EntityField2)
            End Get
        End Property
    End Class
    Public Class AspectResourceFields

        Public Shared ReadOnly Property [ResourceId_Resource] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(AspectResourceFieldIndex.ResourceId_Resource), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Version] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(AspectResourceFieldIndex.Version), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [BankId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(AspectResourceFieldIndex.BankId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Name] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(AspectResourceFieldIndex.Name), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Title] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(AspectResourceFieldIndex.Title), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Description] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(AspectResourceFieldIndex.Description), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [StateId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(AspectResourceFieldIndex.StateId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreationDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(AspectResourceFieldIndex.CreationDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreatedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(AspectResourceFieldIndex.CreatedBy), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(AspectResourceFieldIndex.ModifiedDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(AspectResourceFieldIndex.ModifiedBy), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [OriginalVersion] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(AspectResourceFieldIndex.OriginalVersion), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [OriginalName] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(AspectResourceFieldIndex.OriginalName), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ResourceId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(AspectResourceFieldIndex.ResourceId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [RawScore] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(AspectResourceFieldIndex.RawScore), EntityField2)
            End Get
        End Property
    End Class
    Public Class AssessmentTestResourceFields

        Public Shared ReadOnly Property [ResourceId_Resource] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(AssessmentTestResourceFieldIndex.ResourceId_Resource), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Version] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(AssessmentTestResourceFieldIndex.Version), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [BankId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(AssessmentTestResourceFieldIndex.BankId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Name] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(AssessmentTestResourceFieldIndex.Name), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Title] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(AssessmentTestResourceFieldIndex.Title), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Description] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(AssessmentTestResourceFieldIndex.Description), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [StateId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(AssessmentTestResourceFieldIndex.StateId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreationDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(AssessmentTestResourceFieldIndex.CreationDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreatedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(AssessmentTestResourceFieldIndex.CreatedBy), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(AssessmentTestResourceFieldIndex.ModifiedDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(AssessmentTestResourceFieldIndex.ModifiedBy), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [OriginalVersion] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(AssessmentTestResourceFieldIndex.OriginalVersion), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [OriginalName] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(AssessmentTestResourceFieldIndex.OriginalName), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ResourceId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(AssessmentTestResourceFieldIndex.ResourceId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [IsTemplate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(AssessmentTestResourceFieldIndex.IsTemplate), EntityField2)
            End Get
        End Property
    End Class
    Public Class BankFields

        Public Shared ReadOnly Property [Id] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(BankFieldIndex.Id), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ParentBankId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(BankFieldIndex.ParentBankId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Name] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(BankFieldIndex.Name), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Type] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(BankFieldIndex.Type), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreationDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(BankFieldIndex.CreationDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreatedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(BankFieldIndex.CreatedBy), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(BankFieldIndex.ModifiedDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(BankFieldIndex.ModifiedBy), EntityField2)
            End Get
        End Property
    End Class
    Public Class ChildConceptStructurePartCustomBankPropertyFields

        Public Shared ReadOnly Property [Id] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ChildConceptStructurePartCustomBankPropertyFieldIndex.Id), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ConceptStructurePartCustomBankPropertyId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ChildConceptStructurePartCustomBankPropertyFieldIndex.ConceptStructurePartCustomBankPropertyId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ChildConceptStructurePartCustomBankPropertyId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ChildConceptStructurePartCustomBankPropertyFieldIndex.ChildConceptStructurePartCustomBankPropertyId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [VisualOrder] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ChildConceptStructurePartCustomBankPropertyFieldIndex.VisualOrder), EntityField2)
            End Get
        End Property
    End Class
    Public Class ChildTreeStructurePartCustomBankPropertyFields

        Public Shared ReadOnly Property [Id] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ChildTreeStructurePartCustomBankPropertyFieldIndex.Id), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ChildTreeStructurePartCustomBankPropertyId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ChildTreeStructurePartCustomBankPropertyFieldIndex.ChildTreeStructurePartCustomBankPropertyId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [TreeStructurePartCustomBankPropertyId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ChildTreeStructurePartCustomBankPropertyFieldIndex.TreeStructurePartCustomBankPropertyId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [VisualOrder] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ChildTreeStructurePartCustomBankPropertyFieldIndex.VisualOrder), EntityField2)
            End Get
        End Property
    End Class
    Public Class ConceptStructureCustomBankPropertyFields

        Public Shared ReadOnly Property [CustomBankPropertyId_CustomBankProperty] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ConceptStructureCustomBankPropertyFieldIndex.CustomBankPropertyId_CustomBankProperty), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [BankId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ConceptStructureCustomBankPropertyFieldIndex.BankId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ApplicableToMask] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ConceptStructureCustomBankPropertyFieldIndex.ApplicableToMask), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Publishable] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ConceptStructureCustomBankPropertyFieldIndex.Publishable), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Scorable] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ConceptStructureCustomBankPropertyFieldIndex.Scorable), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Name] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ConceptStructureCustomBankPropertyFieldIndex.Name), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Title] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ConceptStructureCustomBankPropertyFieldIndex.Title), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Description] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ConceptStructureCustomBankPropertyFieldIndex.Description), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreationDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ConceptStructureCustomBankPropertyFieldIndex.CreationDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreatedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ConceptStructureCustomBankPropertyFieldIndex.CreatedBy), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ConceptStructureCustomBankPropertyFieldIndex.ModifiedDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ConceptStructureCustomBankPropertyFieldIndex.ModifiedBy), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Code] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ConceptStructureCustomBankPropertyFieldIndex.Code), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [StateId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ConceptStructureCustomBankPropertyFieldIndex.StateId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Version] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ConceptStructureCustomBankPropertyFieldIndex.Version), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CustomBankPropertyId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ConceptStructureCustomBankPropertyFieldIndex.CustomBankPropertyId), EntityField2)
            End Get
        End Property
    End Class
    Public Class ConceptStructureCustomBankPropertySelectedPartFields

        Public Shared ReadOnly Property [ConceptStructurePartId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ConceptStructureCustomBankPropertySelectedPartFieldIndex.ConceptStructurePartId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ResourceId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ConceptStructureCustomBankPropertySelectedPartFieldIndex.ResourceId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CustomBankPropertyId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ConceptStructureCustomBankPropertySelectedPartFieldIndex.CustomBankPropertyId), EntityField2)
            End Get
        End Property
    End Class
    Public Class ConceptStructureCustomBankPropertyValueFields

        Public Shared ReadOnly Property [ResourceId_CustomBankPropertyValue] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ConceptStructureCustomBankPropertyValueFieldIndex.ResourceId_CustomBankPropertyValue), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CustomBankPropertyId_CustomBankPropertyValue] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ConceptStructureCustomBankPropertyValueFieldIndex.CustomBankPropertyId_CustomBankPropertyValue), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [DisplayValue] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ConceptStructureCustomBankPropertyValueFieldIndex.DisplayValue), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ResourceId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ConceptStructureCustomBankPropertyValueFieldIndex.ResourceId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CustomBankPropertyId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ConceptStructureCustomBankPropertyValueFieldIndex.CustomBankPropertyId), EntityField2)
            End Get
        End Property
    End Class
    Public Class ConceptStructurePartCustomBankPropertyFields

        Public Shared ReadOnly Property [ConceptStructurePartCustomBankPropertyId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ConceptStructurePartCustomBankPropertyFieldIndex.ConceptStructurePartCustomBankPropertyId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CustomBankPropertyId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ConceptStructurePartCustomBankPropertyFieldIndex.CustomBankPropertyId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Name] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ConceptStructurePartCustomBankPropertyFieldIndex.Name), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Title] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ConceptStructurePartCustomBankPropertyFieldIndex.Title), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Code] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ConceptStructurePartCustomBankPropertyFieldIndex.Code), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ConceptTypeId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ConceptStructurePartCustomBankPropertyFieldIndex.ConceptTypeId), EntityField2)
            End Get
        End Property
    End Class
    Public Class ConceptTypeFields

        Public Shared ReadOnly Property [ConceptTypeId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ConceptTypeFieldIndex.ConceptTypeId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Name] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ConceptTypeFieldIndex.Name), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ApplicableToMask] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ConceptTypeFieldIndex.ApplicableToMask), EntityField2)
            End Get
        End Property
    End Class
    Public Class ControlTemplateResourceFields

        Public Shared ReadOnly Property [ResourceId_Resource] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ControlTemplateResourceFieldIndex.ResourceId_Resource), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Version] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ControlTemplateResourceFieldIndex.Version), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [BankId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ControlTemplateResourceFieldIndex.BankId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Name] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ControlTemplateResourceFieldIndex.Name), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Title] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ControlTemplateResourceFieldIndex.Title), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Description] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ControlTemplateResourceFieldIndex.Description), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [StateId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ControlTemplateResourceFieldIndex.StateId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreationDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ControlTemplateResourceFieldIndex.CreationDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreatedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ControlTemplateResourceFieldIndex.CreatedBy), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ControlTemplateResourceFieldIndex.ModifiedDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ControlTemplateResourceFieldIndex.ModifiedBy), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [OriginalVersion] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ControlTemplateResourceFieldIndex.OriginalVersion), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [OriginalName] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ControlTemplateResourceFieldIndex.OriginalName), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ResourceId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ControlTemplateResourceFieldIndex.ResourceId), EntityField2)
            End Get
        End Property
    End Class
    Public Class CustomBankPropertyFields

        Public Shared ReadOnly Property [CustomBankPropertyId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(CustomBankPropertyFieldIndex.CustomBankPropertyId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [BankId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(CustomBankPropertyFieldIndex.BankId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ApplicableToMask] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(CustomBankPropertyFieldIndex.ApplicableToMask), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Publishable] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(CustomBankPropertyFieldIndex.Publishable), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Scorable] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(CustomBankPropertyFieldIndex.Scorable), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Name] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(CustomBankPropertyFieldIndex.Name), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Title] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(CustomBankPropertyFieldIndex.Title), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Description] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(CustomBankPropertyFieldIndex.Description), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreationDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(CustomBankPropertyFieldIndex.CreationDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreatedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(CustomBankPropertyFieldIndex.CreatedBy), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(CustomBankPropertyFieldIndex.ModifiedDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(CustomBankPropertyFieldIndex.ModifiedBy), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Code] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(CustomBankPropertyFieldIndex.Code), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [StateId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(CustomBankPropertyFieldIndex.StateId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Version] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(CustomBankPropertyFieldIndex.Version), EntityField2)
            End Get
        End Property
    End Class
    Public Class CustomBankPropertyValueFields

        Public Shared ReadOnly Property [ResourceId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(CustomBankPropertyValueFieldIndex.ResourceId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CustomBankPropertyId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(CustomBankPropertyValueFieldIndex.CustomBankPropertyId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [DisplayValue] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(CustomBankPropertyValueFieldIndex.DisplayValue), EntityField2)
            End Get
        End Property
    End Class
    Public Class DataSourceResourceFields

        Public Shared ReadOnly Property [ResourceId_Resource] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(DataSourceResourceFieldIndex.ResourceId_Resource), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Version] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(DataSourceResourceFieldIndex.Version), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [BankId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(DataSourceResourceFieldIndex.BankId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Name] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(DataSourceResourceFieldIndex.Name), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Title] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(DataSourceResourceFieldIndex.Title), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Description] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(DataSourceResourceFieldIndex.Description), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [StateId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(DataSourceResourceFieldIndex.StateId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreationDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(DataSourceResourceFieldIndex.CreationDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreatedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(DataSourceResourceFieldIndex.CreatedBy), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(DataSourceResourceFieldIndex.ModifiedDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(DataSourceResourceFieldIndex.ModifiedBy), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [OriginalVersion] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(DataSourceResourceFieldIndex.OriginalVersion), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [OriginalName] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(DataSourceResourceFieldIndex.OriginalName), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ResourceId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(DataSourceResourceFieldIndex.ResourceId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [DataSourceType] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(DataSourceResourceFieldIndex.DataSourceType), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [IsTemplate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(DataSourceResourceFieldIndex.IsTemplate), EntityField2)
            End Get
        End Property
    End Class
    Public Class DependentResourceFields

        Public Shared ReadOnly Property [ResourceId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(DependentResourceFieldIndex.ResourceId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [DependentResourceId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(DependentResourceFieldIndex.DependentResourceId), EntityField2)
            End Get
        End Property
    End Class
    Public Class FreeValueCustomBankPropertyFields

        Public Shared ReadOnly Property [CustomBankPropertyId_CustomBankProperty] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(FreeValueCustomBankPropertyFieldIndex.CustomBankPropertyId_CustomBankProperty), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [BankId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(FreeValueCustomBankPropertyFieldIndex.BankId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ApplicableToMask] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(FreeValueCustomBankPropertyFieldIndex.ApplicableToMask), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Publishable] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(FreeValueCustomBankPropertyFieldIndex.Publishable), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Scorable] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(FreeValueCustomBankPropertyFieldIndex.Scorable), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Name] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(FreeValueCustomBankPropertyFieldIndex.Name), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Title] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(FreeValueCustomBankPropertyFieldIndex.Title), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Description] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(FreeValueCustomBankPropertyFieldIndex.Description), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreationDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(FreeValueCustomBankPropertyFieldIndex.CreationDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreatedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(FreeValueCustomBankPropertyFieldIndex.CreatedBy), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(FreeValueCustomBankPropertyFieldIndex.ModifiedDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(FreeValueCustomBankPropertyFieldIndex.ModifiedBy), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Code] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(FreeValueCustomBankPropertyFieldIndex.Code), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [StateId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(FreeValueCustomBankPropertyFieldIndex.StateId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Version] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(FreeValueCustomBankPropertyFieldIndex.Version), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CustomBankPropertyId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(FreeValueCustomBankPropertyFieldIndex.CustomBankPropertyId), EntityField2)
            End Get
        End Property
    End Class
    Public Class FreeValueCustomBankPropertyValueFields

        Public Shared ReadOnly Property [ResourceId_CustomBankPropertyValue] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(FreeValueCustomBankPropertyValueFieldIndex.ResourceId_CustomBankPropertyValue), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CustomBankPropertyId_CustomBankPropertyValue] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(FreeValueCustomBankPropertyValueFieldIndex.CustomBankPropertyId_CustomBankPropertyValue), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [DisplayValue] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(FreeValueCustomBankPropertyValueFieldIndex.DisplayValue), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ResourceId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(FreeValueCustomBankPropertyValueFieldIndex.ResourceId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CustomBankPropertyId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(FreeValueCustomBankPropertyValueFieldIndex.CustomBankPropertyId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Value] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(FreeValueCustomBankPropertyValueFieldIndex.Value), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [FreeValueCustomBankPropertyValueId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(FreeValueCustomBankPropertyValueFieldIndex.FreeValueCustomBankPropertyValueId), EntityField2)
            End Get
        End Property
    End Class
    Public Class GenericResourceFields

        Public Shared ReadOnly Property [ResourceId_Resource] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(GenericResourceFieldIndex.ResourceId_Resource), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Version] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(GenericResourceFieldIndex.Version), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [BankId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(GenericResourceFieldIndex.BankId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Name] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(GenericResourceFieldIndex.Name), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Title] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(GenericResourceFieldIndex.Title), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Description] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(GenericResourceFieldIndex.Description), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [StateId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(GenericResourceFieldIndex.StateId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreationDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(GenericResourceFieldIndex.CreationDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreatedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(GenericResourceFieldIndex.CreatedBy), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(GenericResourceFieldIndex.ModifiedDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(GenericResourceFieldIndex.ModifiedBy), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [OriginalVersion] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(GenericResourceFieldIndex.OriginalVersion), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [OriginalName] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(GenericResourceFieldIndex.OriginalName), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ResourceId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(GenericResourceFieldIndex.ResourceId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [MediaType] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(GenericResourceFieldIndex.MediaType), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Size] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(GenericResourceFieldIndex.Size), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Dimensions] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(GenericResourceFieldIndex.Dimensions), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [IsTemplate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(GenericResourceFieldIndex.IsTemplate), EntityField2)
            End Get
        End Property
    End Class
    Public Class HiddenResourceFields

        Public Shared ReadOnly Property [ResourceId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(HiddenResourceFieldIndex.ResourceId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [BankId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(HiddenResourceFieldIndex.BankId), EntityField2)
            End Get
        End Property
    End Class
    Public Class ItemLayoutTemplateResourceFields

        Public Shared ReadOnly Property [ResourceId_Resource] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ItemLayoutTemplateResourceFieldIndex.ResourceId_Resource), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Version] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ItemLayoutTemplateResourceFieldIndex.Version), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [BankId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ItemLayoutTemplateResourceFieldIndex.BankId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Name] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ItemLayoutTemplateResourceFieldIndex.Name), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Title] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ItemLayoutTemplateResourceFieldIndex.Title), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Description] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ItemLayoutTemplateResourceFieldIndex.Description), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [StateId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ItemLayoutTemplateResourceFieldIndex.StateId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreationDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ItemLayoutTemplateResourceFieldIndex.CreationDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreatedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ItemLayoutTemplateResourceFieldIndex.CreatedBy), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ItemLayoutTemplateResourceFieldIndex.ModifiedDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ItemLayoutTemplateResourceFieldIndex.ModifiedBy), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [OriginalVersion] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ItemLayoutTemplateResourceFieldIndex.OriginalVersion), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [OriginalName] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ItemLayoutTemplateResourceFieldIndex.OriginalName), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ResourceId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ItemLayoutTemplateResourceFieldIndex.ResourceId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ItemType] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ItemLayoutTemplateResourceFieldIndex.ItemType), EntityField2)
            End Get
        End Property
    End Class
    Public Class ItemResourceFields

        Public Shared ReadOnly Property [ResourceId_Resource] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ItemResourceFieldIndex.ResourceId_Resource), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Version] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ItemResourceFieldIndex.Version), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [BankId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ItemResourceFieldIndex.BankId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Name] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ItemResourceFieldIndex.Name), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Title] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ItemResourceFieldIndex.Title), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Description] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ItemResourceFieldIndex.Description), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [StateId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ItemResourceFieldIndex.StateId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreationDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ItemResourceFieldIndex.CreationDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreatedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ItemResourceFieldIndex.CreatedBy), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ItemResourceFieldIndex.ModifiedDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ItemResourceFieldIndex.ModifiedBy), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [OriginalVersion] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ItemResourceFieldIndex.OriginalVersion), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [OriginalName] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ItemResourceFieldIndex.OriginalName), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ResourceId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ItemResourceFieldIndex.ResourceId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [IsSystemItem] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ItemResourceFieldIndex.IsSystemItem), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [AlternativesCount] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ItemResourceFieldIndex.AlternativesCount), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [KeyValues] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ItemResourceFieldIndex.KeyValues), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ResponseCount] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ItemResourceFieldIndex.ResponseCount), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [RawScore] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ItemResourceFieldIndex.RawScore), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [TesterSchemaVersion] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ItemResourceFieldIndex.TesterSchemaVersion), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Iltname] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ItemResourceFieldIndex.Iltname), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Iltversion] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ItemResourceFieldIndex.Iltversion), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [MaxScore] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ItemResourceFieldIndex.MaxScore), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ItemAutoId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ItemResourceFieldIndex.ItemAutoId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ItemId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ItemResourceFieldIndex.ItemId), EntityField2)
            End Get
        End Property
    End Class
    Public Class ListCustomBankPropertyFields

        Public Shared ReadOnly Property [CustomBankPropertyId_CustomBankProperty] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ListCustomBankPropertyFieldIndex.CustomBankPropertyId_CustomBankProperty), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [BankId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ListCustomBankPropertyFieldIndex.BankId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ApplicableToMask] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ListCustomBankPropertyFieldIndex.ApplicableToMask), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Publishable] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ListCustomBankPropertyFieldIndex.Publishable), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Scorable] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ListCustomBankPropertyFieldIndex.Scorable), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Name] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ListCustomBankPropertyFieldIndex.Name), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Title] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ListCustomBankPropertyFieldIndex.Title), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Description] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ListCustomBankPropertyFieldIndex.Description), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreationDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ListCustomBankPropertyFieldIndex.CreationDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreatedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ListCustomBankPropertyFieldIndex.CreatedBy), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ListCustomBankPropertyFieldIndex.ModifiedDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ListCustomBankPropertyFieldIndex.ModifiedBy), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Code] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ListCustomBankPropertyFieldIndex.Code), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [StateId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ListCustomBankPropertyFieldIndex.StateId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Version] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ListCustomBankPropertyFieldIndex.Version), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CustomBankPropertyId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ListCustomBankPropertyFieldIndex.CustomBankPropertyId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [MultipleSelect] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ListCustomBankPropertyFieldIndex.MultipleSelect), EntityField2)
            End Get
        End Property
    End Class
    Public Class ListCustomBankPropertySelectedValueFields

        Public Shared ReadOnly Property [ResourceId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ListCustomBankPropertySelectedValueFieldIndex.ResourceId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CustomBankPropertyId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ListCustomBankPropertySelectedValueFieldIndex.CustomBankPropertyId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ListValueBankCustomPropertyId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ListCustomBankPropertySelectedValueFieldIndex.ListValueBankCustomPropertyId), EntityField2)
            End Get
        End Property
    End Class
    Public Class ListCustomBankPropertyValueFields

        Public Shared ReadOnly Property [ResourceId_CustomBankPropertyValue] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ListCustomBankPropertyValueFieldIndex.ResourceId_CustomBankPropertyValue), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CustomBankPropertyId_CustomBankPropertyValue] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ListCustomBankPropertyValueFieldIndex.CustomBankPropertyId_CustomBankPropertyValue), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [DisplayValue] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ListCustomBankPropertyValueFieldIndex.DisplayValue), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ResourceId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ListCustomBankPropertyValueFieldIndex.ResourceId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CustomBankPropertyId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ListCustomBankPropertyValueFieldIndex.CustomBankPropertyId), EntityField2)
            End Get
        End Property
    End Class
    Public Class ListValueCustomBankPropertyFields

        Public Shared ReadOnly Property [ListValueBankCustomPropertyId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ListValueCustomBankPropertyFieldIndex.ListValueBankCustomPropertyId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CustomBankPropertyId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ListValueCustomBankPropertyFieldIndex.CustomBankPropertyId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Name] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ListValueCustomBankPropertyFieldIndex.Name), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Title] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ListValueCustomBankPropertyFieldIndex.Title), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Code] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ListValueCustomBankPropertyFieldIndex.Code), EntityField2)
            End Get
        End Property
    End Class
    Public Class PackageResourceFields

        Public Shared ReadOnly Property [ResourceId_Resource] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(PackageResourceFieldIndex.ResourceId_Resource), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Version] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(PackageResourceFieldIndex.Version), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [BankId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(PackageResourceFieldIndex.BankId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Name] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(PackageResourceFieldIndex.Name), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Title] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(PackageResourceFieldIndex.Title), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Description] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(PackageResourceFieldIndex.Description), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [StateId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(PackageResourceFieldIndex.StateId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreationDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(PackageResourceFieldIndex.CreationDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreatedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(PackageResourceFieldIndex.CreatedBy), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(PackageResourceFieldIndex.ModifiedDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(PackageResourceFieldIndex.ModifiedBy), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [OriginalVersion] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(PackageResourceFieldIndex.OriginalVersion), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [OriginalName] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(PackageResourceFieldIndex.OriginalName), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ResourceId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(PackageResourceFieldIndex.ResourceId), EntityField2)
            End Get
        End Property
    End Class
    Public Class PermissionFields

        Public Shared ReadOnly Property [Id] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(PermissionFieldIndex.Id), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Name] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(PermissionFieldIndex.Name), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Description] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(PermissionFieldIndex.Description), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [WhenOwnerCondition] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(PermissionFieldIndex.WhenOwnerCondition), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreationDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(PermissionFieldIndex.CreationDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreatedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(PermissionFieldIndex.CreatedBy), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(PermissionFieldIndex.ModifiedDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(PermissionFieldIndex.ModifiedBy), EntityField2)
            End Get
        End Property
    End Class
    Public Class PermissionTargetFields

        Public Shared ReadOnly Property [Id] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(PermissionTargetFieldIndex.Id), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Name] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(PermissionTargetFieldIndex.Name), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [TargettedNamedTask] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(PermissionTargetFieldIndex.TargettedNamedTask), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [IsApplicationTarget] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(PermissionTargetFieldIndex.IsApplicationTarget), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreationDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(PermissionTargetFieldIndex.CreationDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreatedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(PermissionTargetFieldIndex.CreatedBy), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(PermissionTargetFieldIndex.ModifiedDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(PermissionTargetFieldIndex.ModifiedBy), EntityField2)
            End Get
        End Property
    End Class
    Public Class ResourceFields

        Public Shared ReadOnly Property [ResourceId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ResourceFieldIndex.ResourceId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Version] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ResourceFieldIndex.Version), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [BankId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ResourceFieldIndex.BankId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Name] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ResourceFieldIndex.Name), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Title] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ResourceFieldIndex.Title), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Description] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ResourceFieldIndex.Description), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [StateId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ResourceFieldIndex.StateId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreationDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ResourceFieldIndex.CreationDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreatedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ResourceFieldIndex.CreatedBy), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ResourceFieldIndex.ModifiedDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ResourceFieldIndex.ModifiedBy), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [OriginalVersion] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ResourceFieldIndex.OriginalVersion), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [OriginalName] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ResourceFieldIndex.OriginalName), EntityField2)
            End Get
        End Property
    End Class
    Public Class ResourceDataFields

        Public Shared ReadOnly Property [ResourceId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ResourceDataFieldIndex.ResourceId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [BinData] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ResourceDataFieldIndex.BinData), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Url] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ResourceDataFieldIndex.Url), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [FileExtension] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ResourceDataFieldIndex.FileExtension), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Ident] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ResourceDataFieldIndex.Ident), EntityField2)
            End Get
        End Property
    End Class
    Public Class ResourceHistoryFields

        Public Shared ReadOnly Property [Id] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ResourceHistoryFieldIndex.Id), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ResourceId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ResourceHistoryFieldIndex.ResourceId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [MajorVersion] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ResourceHistoryFieldIndex.MajorVersion), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [MinorVersion] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ResourceHistoryFieldIndex.MinorVersion), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ResourceHistoryFieldIndex.ModifiedBy), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ResourceHistoryFieldIndex.ModifiedDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Label] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ResourceHistoryFieldIndex.Label), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [BinData] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ResourceHistoryFieldIndex.BinData), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [MetaData] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(ResourceHistoryFieldIndex.MetaData), EntityField2)
            End Get
        End Property
    End Class
    Public Class RichTextValueCustomBankPropertyFields

        Public Shared ReadOnly Property [CustomBankPropertyId_CustomBankProperty] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(RichTextValueCustomBankPropertyFieldIndex.CustomBankPropertyId_CustomBankProperty), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [BankId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(RichTextValueCustomBankPropertyFieldIndex.BankId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ApplicableToMask] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(RichTextValueCustomBankPropertyFieldIndex.ApplicableToMask), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Publishable] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(RichTextValueCustomBankPropertyFieldIndex.Publishable), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Scorable] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(RichTextValueCustomBankPropertyFieldIndex.Scorable), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Name] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(RichTextValueCustomBankPropertyFieldIndex.Name), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Title] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(RichTextValueCustomBankPropertyFieldIndex.Title), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Description] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(RichTextValueCustomBankPropertyFieldIndex.Description), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreationDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(RichTextValueCustomBankPropertyFieldIndex.CreationDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreatedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(RichTextValueCustomBankPropertyFieldIndex.CreatedBy), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(RichTextValueCustomBankPropertyFieldIndex.ModifiedDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(RichTextValueCustomBankPropertyFieldIndex.ModifiedBy), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Code] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(RichTextValueCustomBankPropertyFieldIndex.Code), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [StateId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(RichTextValueCustomBankPropertyFieldIndex.StateId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Version] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(RichTextValueCustomBankPropertyFieldIndex.Version), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CustomBankPropertyId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(RichTextValueCustomBankPropertyFieldIndex.CustomBankPropertyId), EntityField2)
            End Get
        End Property
    End Class
    Public Class RichTextValueCustomBankPropertyValueFields

        Public Shared ReadOnly Property [ResourceId_CustomBankPropertyValue] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(RichTextValueCustomBankPropertyValueFieldIndex.ResourceId_CustomBankPropertyValue), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CustomBankPropertyId_CustomBankPropertyValue] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(RichTextValueCustomBankPropertyValueFieldIndex.CustomBankPropertyId_CustomBankPropertyValue), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [DisplayValue] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(RichTextValueCustomBankPropertyValueFieldIndex.DisplayValue), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ResourceId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(RichTextValueCustomBankPropertyValueFieldIndex.ResourceId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CustomBankPropertyId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(RichTextValueCustomBankPropertyValueFieldIndex.CustomBankPropertyId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Value] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(RichTextValueCustomBankPropertyValueFieldIndex.Value), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [RichTextValueCustomBankPropertyValueId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(RichTextValueCustomBankPropertyValueFieldIndex.RichTextValueCustomBankPropertyValueId), EntityField2)
            End Get
        End Property
    End Class
    Public Class RoleFields

        Public Shared ReadOnly Property [Id] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(RoleFieldIndex.Id), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Name] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(RoleFieldIndex.Name), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Description] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(RoleFieldIndex.Description), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [IsApplicationRole] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(RoleFieldIndex.IsApplicationRole), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreationDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(RoleFieldIndex.CreationDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreatedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(RoleFieldIndex.CreatedBy), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(RoleFieldIndex.ModifiedDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(RoleFieldIndex.ModifiedBy), EntityField2)
            End Get
        End Property
    End Class
    Public Class RolePermissionFields

        Public Shared ReadOnly Property [RoleId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(RolePermissionFieldIndex.RoleId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [PermissionTargetId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(RolePermissionFieldIndex.PermissionTargetId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [PermissionId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(RolePermissionFieldIndex.PermissionId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreationDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(RolePermissionFieldIndex.CreationDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreatedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(RolePermissionFieldIndex.CreatedBy), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(RolePermissionFieldIndex.ModifiedDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(RolePermissionFieldIndex.ModifiedBy), EntityField2)
            End Get
        End Property
    End Class
    Public Class StateFields

        Public Shared ReadOnly Property [StateId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(StateFieldIndex.StateId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Name] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(StateFieldIndex.Name), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Title] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(StateFieldIndex.Title), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Description] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(StateFieldIndex.Description), EntityField2)
            End Get
        End Property
    End Class
    Public Class StateActionFields

        Public Shared ReadOnly Property [Target] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(StateActionFieldIndex.Target), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [StateId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(StateActionFieldIndex.StateId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ActionId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(StateActionFieldIndex.ActionId), EntityField2)
            End Get
        End Property
    End Class
    Public Class TestPackageResourceFields

        Public Shared ReadOnly Property [ResourceId_Resource] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TestPackageResourceFieldIndex.ResourceId_Resource), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Version] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TestPackageResourceFieldIndex.Version), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [BankId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TestPackageResourceFieldIndex.BankId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Name] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TestPackageResourceFieldIndex.Name), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Title] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TestPackageResourceFieldIndex.Title), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Description] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TestPackageResourceFieldIndex.Description), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [StateId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TestPackageResourceFieldIndex.StateId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreationDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TestPackageResourceFieldIndex.CreationDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreatedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TestPackageResourceFieldIndex.CreatedBy), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TestPackageResourceFieldIndex.ModifiedDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TestPackageResourceFieldIndex.ModifiedBy), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [OriginalVersion] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TestPackageResourceFieldIndex.OriginalVersion), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [OriginalName] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TestPackageResourceFieldIndex.OriginalName), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ResourceId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TestPackageResourceFieldIndex.ResourceId), EntityField2)
            End Get
        End Property
    End Class
    Public Class TreeStructureCustomBankPropertyFields

        Public Shared ReadOnly Property [CustomBankPropertyId_CustomBankProperty] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TreeStructureCustomBankPropertyFieldIndex.CustomBankPropertyId_CustomBankProperty), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [BankId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TreeStructureCustomBankPropertyFieldIndex.BankId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ApplicableToMask] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TreeStructureCustomBankPropertyFieldIndex.ApplicableToMask), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Publishable] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TreeStructureCustomBankPropertyFieldIndex.Publishable), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Scorable] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TreeStructureCustomBankPropertyFieldIndex.Scorable), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Name] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TreeStructureCustomBankPropertyFieldIndex.Name), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Title] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TreeStructureCustomBankPropertyFieldIndex.Title), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Description] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TreeStructureCustomBankPropertyFieldIndex.Description), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreationDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TreeStructureCustomBankPropertyFieldIndex.CreationDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreatedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TreeStructureCustomBankPropertyFieldIndex.CreatedBy), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TreeStructureCustomBankPropertyFieldIndex.ModifiedDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TreeStructureCustomBankPropertyFieldIndex.ModifiedBy), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Code] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TreeStructureCustomBankPropertyFieldIndex.Code), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [StateId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TreeStructureCustomBankPropertyFieldIndex.StateId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Version] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TreeStructureCustomBankPropertyFieldIndex.Version), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CustomBankPropertyId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TreeStructureCustomBankPropertyFieldIndex.CustomBankPropertyId), EntityField2)
            End Get
        End Property
    End Class
    Public Class TreeStructureCustomBankPropertySelectedPartFields

        Public Shared ReadOnly Property [TreeStructurePartId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TreeStructureCustomBankPropertySelectedPartFieldIndex.TreeStructurePartId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ResourceId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TreeStructureCustomBankPropertySelectedPartFieldIndex.ResourceId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CustomBankPropertyId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TreeStructureCustomBankPropertySelectedPartFieldIndex.CustomBankPropertyId), EntityField2)
            End Get
        End Property
    End Class
    Public Class TreeStructureCustomBankPropertyValueFields

        Public Shared ReadOnly Property [ResourceId_CustomBankPropertyValue] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TreeStructureCustomBankPropertyValueFieldIndex.ResourceId_CustomBankPropertyValue), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CustomBankPropertyId_CustomBankPropertyValue] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TreeStructureCustomBankPropertyValueFieldIndex.CustomBankPropertyId_CustomBankPropertyValue), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [DisplayValue] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TreeStructureCustomBankPropertyValueFieldIndex.DisplayValue), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ResourceId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TreeStructureCustomBankPropertyValueFieldIndex.ResourceId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CustomBankPropertyId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TreeStructureCustomBankPropertyValueFieldIndex.CustomBankPropertyId), EntityField2)
            End Get
        End Property
    End Class
    Public Class TreeStructurePartCustomBankPropertyFields

        Public Shared ReadOnly Property [TreeStructurePartCustomBankPropertyId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TreeStructurePartCustomBankPropertyFieldIndex.TreeStructurePartCustomBankPropertyId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CustomBankPropertyId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TreeStructurePartCustomBankPropertyFieldIndex.CustomBankPropertyId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Name] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TreeStructurePartCustomBankPropertyFieldIndex.Name), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Title] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TreeStructurePartCustomBankPropertyFieldIndex.Title), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Code] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(TreeStructurePartCustomBankPropertyFieldIndex.Code), EntityField2)
            End Get
        End Property
    End Class
    Public Class UserFields

        Public Shared ReadOnly Property [Id] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(UserFieldIndex.Id), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [UserName] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(UserFieldIndex.UserName), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Password] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(UserFieldIndex.Password), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [FullName] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(UserFieldIndex.FullName), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [Active] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(UserFieldIndex.Active), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreationDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(UserFieldIndex.CreationDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreatedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(UserFieldIndex.CreatedBy), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(UserFieldIndex.ModifiedDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(UserFieldIndex.ModifiedBy), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [AuthenticationType] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(UserFieldIndex.AuthenticationType), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [UserSettings] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(UserFieldIndex.UserSettings), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ChangePassword] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(UserFieldIndex.ChangePassword), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [AllowedFeatures] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(UserFieldIndex.AllowedFeatures), EntityField2)
            End Get
        End Property
    End Class
    Public Class UserApplicationRoleFields

        Public Shared ReadOnly Property [UserId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(UserApplicationRoleFieldIndex.UserId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ApplicationRoleId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(UserApplicationRoleFieldIndex.ApplicationRoleId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreationDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(UserApplicationRoleFieldIndex.CreationDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreatedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(UserApplicationRoleFieldIndex.CreatedBy), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(UserApplicationRoleFieldIndex.ModifiedDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(UserApplicationRoleFieldIndex.ModifiedBy), EntityField2)
            End Get
        End Property
    End Class
    Public Class UserBankRoleFields

        Public Shared ReadOnly Property [UserId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(UserBankRoleFieldIndex.UserId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [BankId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(UserBankRoleFieldIndex.BankId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [BankRoleId] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(UserBankRoleFieldIndex.BankRoleId), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreationDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(UserBankRoleFieldIndex.CreationDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [CreatedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(UserBankRoleFieldIndex.CreatedBy), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedDate] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(UserBankRoleFieldIndex.ModifiedDate), EntityField2)
            End Get
        End Property
        Public Shared ReadOnly Property [ModifiedBy] As EntityField2
            Get
                Return CType(EntityFieldFactory.Create(UserBankRoleFieldIndex.ModifiedBy), EntityField2)
            End Get
        End Property
    End Class


End Namespace