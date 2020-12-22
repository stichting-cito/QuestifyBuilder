Imports System
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.Model.ContentModel.RelationClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses



Namespace Questify.Builder.Model.ContentModel.HelperClasses
    Public NotInheritable Class InheritanceInfoProviderSingleton
        Private Shared ReadOnly _providerInstance As IInheritanceInfoProvider = New InheritanceInfoProviderCore()

        Shared Sub New()
        End Sub

        Public Shared Function GetInstance() As IInheritanceInfoProvider
            Return _providerInstance
        End Function



    End Class


    Friend Class InheritanceInfoProviderCore
        Inherits InheritanceInfoProviderBase

        Friend Sub New()
            Init()
        End Sub

        Private Sub Init()

            Me.AddEntityInfo("AspectResourceEntity", "ResourceEntity", New AspectResourceRelations(), New AspectResourceEntityFactory(), 1 - 1)
            Me.AddEntityInfo("AssessmentTestResourceEntity", "ResourceEntity", New AssessmentTestResourceRelations(), New AssessmentTestResourceEntityFactory(), 1 - 1)
            Me.AddEntityInfo("ConceptStructureCustomBankPropertyEntity", "CustomBankPropertyEntity", New ConceptStructureCustomBankPropertyRelations(), New ConceptStructureCustomBankPropertyEntityFactory(), 1 - 1)
            Me.AddEntityInfo("ConceptStructureCustomBankPropertyValueEntity", "CustomBankPropertyValueEntity", New ConceptStructureCustomBankPropertyValueRelations(), New ConceptStructureCustomBankPropertyValueEntityFactory(), 1 - 1, 2 - 1)
            Me.AddEntityInfo("ControlTemplateResourceEntity", "ResourceEntity", New ControlTemplateResourceRelations(), New ControlTemplateResourceEntityFactory(), 1 - 1)
            Me.AddEntityInfo("CustomBankPropertyEntity", String.Empty, New CustomBankPropertyRelations(), New CustomBankPropertyEntityFactory())
            Me.AddEntityInfo("CustomBankPropertyValueEntity", String.Empty, New CustomBankPropertyValueRelations(), New CustomBankPropertyValueEntityFactory())
            Me.AddEntityInfo("DataSourceResourceEntity", "ResourceEntity", New DataSourceResourceRelations(), New DataSourceResourceEntityFactory(), 1 - 1)
            Me.AddEntityInfo("FreeValueCustomBankPropertyEntity", "CustomBankPropertyEntity", New FreeValueCustomBankPropertyRelations(), New FreeValueCustomBankPropertyEntityFactory(), 1 - 1)
            Me.AddEntityInfo("FreeValueCustomBankPropertyValueEntity", "CustomBankPropertyValueEntity", New FreeValueCustomBankPropertyValueRelations(), New FreeValueCustomBankPropertyValueEntityFactory(), 1 - 1, 2 - 1)
            Me.AddEntityInfo("GenericResourceEntity", "ResourceEntity", New GenericResourceRelations(), New GenericResourceEntityFactory(), 1 - 1)
            Me.AddEntityInfo("ItemLayoutTemplateResourceEntity", "ResourceEntity", New ItemLayoutTemplateResourceRelations(), New ItemLayoutTemplateResourceEntityFactory(), 1 - 1)
            Me.AddEntityInfo("ItemResourceEntity", "ResourceEntity", New ItemResourceRelations(), New ItemResourceEntityFactory(), 1 - 1)
            Me.AddEntityInfo("ListCustomBankPropertyEntity", "CustomBankPropertyEntity", New ListCustomBankPropertyRelations(), New ListCustomBankPropertyEntityFactory(), 1 - 1)
            Me.AddEntityInfo("ListCustomBankPropertyValueEntity", "CustomBankPropertyValueEntity", New ListCustomBankPropertyValueRelations(), New ListCustomBankPropertyValueEntityFactory(), 1 - 1, 2 - 1)
            Me.AddEntityInfo("PackageResourceEntity", "ResourceEntity", New PackageResourceRelations(), New PackageResourceEntityFactory(), 1 - 1)
            Me.AddEntityInfo("ResourceEntity", String.Empty, New ResourceRelations(), New ResourceEntityFactory())
            Me.AddEntityInfo("RichTextValueCustomBankPropertyEntity", "CustomBankPropertyEntity", New RichTextValueCustomBankPropertyRelations(), New RichTextValueCustomBankPropertyEntityFactory(), 1 - 1)
            Me.AddEntityInfo("RichTextValueCustomBankPropertyValueEntity", "CustomBankPropertyValueEntity", New RichTextValueCustomBankPropertyValueRelations(), New RichTextValueCustomBankPropertyValueEntityFactory(), 1 - 1, 2 - 1)
            Me.AddEntityInfo("TestPackageResourceEntity", "ResourceEntity", New TestPackageResourceRelations(), New TestPackageResourceEntityFactory(), 1 - 1)
            Me.AddEntityInfo("TreeStructureCustomBankPropertyEntity", "CustomBankPropertyEntity", New TreeStructureCustomBankPropertyRelations(), New TreeStructureCustomBankPropertyEntityFactory(), 1 - 1)
            Me.AddEntityInfo("TreeStructureCustomBankPropertyValueEntity", "CustomBankPropertyValueEntity", New TreeStructureCustomBankPropertyValueRelations(), New TreeStructureCustomBankPropertyValueEntityFactory(), 1 - 1, 2 - 1)
            MyBase.BuildHierarchyInfoStore()
        End Sub

        Public Overrides Function GetEntityFields(entityName As String) As IEntityFieldCore()
            Return EntityFieldsFactory.CreateFields(entityName)
        End Function
    End Class
End Namespace




