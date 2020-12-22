Imports System
Imports System.Collections.Generic
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Questify.Builder.Model.ContentModel.RelationClasses

Imports SD.LLBLGen.Pro.ORMSupportClasses


Namespace Questify.Builder.Model.ContentModel.FactoryClasses

    <Serializable()> _
    Public Class EntityFactoryBase2(Of TEntity As {EntityBase2, IEntity2})
        Inherits EntityFactoryCore2
        Private _typeOfEntity As Questify.Builder.Model.ContentModel.EntityType
        Private _isInHierarchy As Boolean

        Public Sub New(entityName As String, typeOfEntity As Questify.Builder.Model.ContentModel.EntityType, isInHierarchy As Boolean)
            MyBase.New(entityName)
            _isInHierarchy = isInHierarchy
            _typeOfEntity = typeOfEntity
        End Sub

        Public Overrides Overloads Function CreateEntityFromEntityTypeValue(entityTypeValue As Integer) As IEntity2
            Return GeneralEntityFactory.Create(CType(entityTypeValue, Questify.Builder.Model.ContentModel.EntityType))
        End Function

        Public Overrides Function CreateFields() As IEntityFields2
            Return EntityFieldsFactory.CreateEntityFieldsObject(_typeOfEntity)
        End Function

        Public Overrides Function CreateHierarchyRelations(objectAlias As String) As IRelationCollection
            Return InheritanceInfoProviderSingleton.GetInstance().GetHierarchyRelations(ForEntityName, objectAlias)
        End Function

        Public Overrides Function GetEntityFactory(fieldValues As Object(), entityFieldStartIndexesPerEntity As Dictionary(Of String, Integer)) As IEntityFactory2
            Dim toReturn As IEntityFactory2 = CType(InheritanceInfoProviderSingleton.GetInstance().GetEntityFactory(ForEntityName, fieldValues, entityFieldStartIndexesPerEntity), IEntityFactory2)
            If toReturn Is Nothing Then
                toReturn = Me
            End If
            Return toReturn
        End Function

        Public Overrides Function GetEntityTypeFilter(negate As Boolean, objectAlias As String) As IPredicateExpression
            Return InheritanceInfoProviderSingleton.GetInstance().GetEntityTypeFilter(ForEntityName, objectAlias, negate)
        End Function

        Public Overrides Function CreateEntityCollection() As IEntityCollection2
            Return New EntityCollection(Of TEntity)(Me)
        End Function

        Public Overrides Function CreateHierarchyFields() As IEntityFields2
            If _isInHierarchy Then
                Return New EntityFields2(InheritanceInfoProviderSingleton.GetInstance().GetHierarchyFields(ForEntityName), InheritanceInfoProviderSingleton.GetInstance(), Nothing)
            Else
                Return MyBase.CreateHierarchyFields()
            End If
        End Function
    End Class

    <Serializable()> _
    Public Class ActionEntityFactory
        Inherits EntityFactoryBase2(Of ActionEntity)

        Public Sub New()
            MyBase.New("ActionEntity", Questify.Builder.Model.ContentModel.EntityType.ActionEntity, False)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New ActionEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class AspectResourceEntityFactory
        Inherits EntityFactoryBase2(Of AspectResourceEntity)

        Public Sub New()
            MyBase.New("AspectResourceEntity", Questify.Builder.Model.ContentModel.EntityType.AspectResourceEntity, True)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New AspectResourceEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class AssessmentTestResourceEntityFactory
        Inherits EntityFactoryBase2(Of AssessmentTestResourceEntity)

        Public Sub New()
            MyBase.New("AssessmentTestResourceEntity", Questify.Builder.Model.ContentModel.EntityType.AssessmentTestResourceEntity, True)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New AssessmentTestResourceEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class BankEntityFactory
        Inherits EntityFactoryBase2(Of BankEntity)

        Public Sub New()
            MyBase.New("BankEntity", Questify.Builder.Model.ContentModel.EntityType.BankEntity, False)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New BankEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class ChildConceptStructurePartCustomBankPropertyEntityFactory
        Inherits EntityFactoryBase2(Of ChildConceptStructurePartCustomBankPropertyEntity)

        Public Sub New()
            MyBase.New("ChildConceptStructurePartCustomBankPropertyEntity", Questify.Builder.Model.ContentModel.EntityType.ChildConceptStructurePartCustomBankPropertyEntity, False)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New ChildConceptStructurePartCustomBankPropertyEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class ChildTreeStructurePartCustomBankPropertyEntityFactory
        Inherits EntityFactoryBase2(Of ChildTreeStructurePartCustomBankPropertyEntity)

        Public Sub New()
            MyBase.New("ChildTreeStructurePartCustomBankPropertyEntity", Questify.Builder.Model.ContentModel.EntityType.ChildTreeStructurePartCustomBankPropertyEntity, False)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New ChildTreeStructurePartCustomBankPropertyEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class ConceptStructureCustomBankPropertyEntityFactory
        Inherits EntityFactoryBase2(Of ConceptStructureCustomBankPropertyEntity)

        Public Sub New()
            MyBase.New("ConceptStructureCustomBankPropertyEntity", Questify.Builder.Model.ContentModel.EntityType.ConceptStructureCustomBankPropertyEntity, True)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New ConceptStructureCustomBankPropertyEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class ConceptStructureCustomBankPropertySelectedPartEntityFactory
        Inherits EntityFactoryBase2(Of ConceptStructureCustomBankPropertySelectedPartEntity)

        Public Sub New()
            MyBase.New("ConceptStructureCustomBankPropertySelectedPartEntity", Questify.Builder.Model.ContentModel.EntityType.ConceptStructureCustomBankPropertySelectedPartEntity, False)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New ConceptStructureCustomBankPropertySelectedPartEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class ConceptStructureCustomBankPropertyValueEntityFactory
        Inherits EntityFactoryBase2(Of ConceptStructureCustomBankPropertyValueEntity)

        Public Sub New()
            MyBase.New("ConceptStructureCustomBankPropertyValueEntity", Questify.Builder.Model.ContentModel.EntityType.ConceptStructureCustomBankPropertyValueEntity, True)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New ConceptStructureCustomBankPropertyValueEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class ConceptStructurePartCustomBankPropertyEntityFactory
        Inherits EntityFactoryBase2(Of ConceptStructurePartCustomBankPropertyEntity)

        Public Sub New()
            MyBase.New("ConceptStructurePartCustomBankPropertyEntity", Questify.Builder.Model.ContentModel.EntityType.ConceptStructurePartCustomBankPropertyEntity, False)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New ConceptStructurePartCustomBankPropertyEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class ConceptTypeEntityFactory
        Inherits EntityFactoryBase2(Of ConceptTypeEntity)

        Public Sub New()
            MyBase.New("ConceptTypeEntity", Questify.Builder.Model.ContentModel.EntityType.ConceptTypeEntity, False)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New ConceptTypeEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class ControlTemplateResourceEntityFactory
        Inherits EntityFactoryBase2(Of ControlTemplateResourceEntity)

        Public Sub New()
            MyBase.New("ControlTemplateResourceEntity", Questify.Builder.Model.ContentModel.EntityType.ControlTemplateResourceEntity, True)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New ControlTemplateResourceEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class CustomBankPropertyEntityFactory
        Inherits EntityFactoryBase2(Of CustomBankPropertyEntity)

        Public Sub New()
            MyBase.New("CustomBankPropertyEntity", Questify.Builder.Model.ContentModel.EntityType.CustomBankPropertyEntity, True)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New CustomBankPropertyEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class CustomBankPropertyValueEntityFactory
        Inherits EntityFactoryBase2(Of CustomBankPropertyValueEntity)

        Public Sub New()
            MyBase.New("CustomBankPropertyValueEntity", Questify.Builder.Model.ContentModel.EntityType.CustomBankPropertyValueEntity, True)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New CustomBankPropertyValueEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class DataSourceResourceEntityFactory
        Inherits EntityFactoryBase2(Of DataSourceResourceEntity)

        Public Sub New()
            MyBase.New("DataSourceResourceEntity", Questify.Builder.Model.ContentModel.EntityType.DataSourceResourceEntity, True)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New DataSourceResourceEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class DependentResourceEntityFactory
        Inherits EntityFactoryBase2(Of DependentResourceEntity)

        Public Sub New()
            MyBase.New("DependentResourceEntity", Questify.Builder.Model.ContentModel.EntityType.DependentResourceEntity, False)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New DependentResourceEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class FreeValueCustomBankPropertyEntityFactory
        Inherits EntityFactoryBase2(Of FreeValueCustomBankPropertyEntity)

        Public Sub New()
            MyBase.New("FreeValueCustomBankPropertyEntity", Questify.Builder.Model.ContentModel.EntityType.FreeValueCustomBankPropertyEntity, True)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New FreeValueCustomBankPropertyEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class FreeValueCustomBankPropertyValueEntityFactory
        Inherits EntityFactoryBase2(Of FreeValueCustomBankPropertyValueEntity)

        Public Sub New()
            MyBase.New("FreeValueCustomBankPropertyValueEntity", Questify.Builder.Model.ContentModel.EntityType.FreeValueCustomBankPropertyValueEntity, True)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New FreeValueCustomBankPropertyValueEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class GenericResourceEntityFactory
        Inherits EntityFactoryBase2(Of GenericResourceEntity)

        Public Sub New()
            MyBase.New("GenericResourceEntity", Questify.Builder.Model.ContentModel.EntityType.GenericResourceEntity, True)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New GenericResourceEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class HiddenResourceEntityFactory
        Inherits EntityFactoryBase2(Of HiddenResourceEntity)

        Public Sub New()
            MyBase.New("HiddenResourceEntity", Questify.Builder.Model.ContentModel.EntityType.HiddenResourceEntity, False)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New HiddenResourceEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class ItemLayoutTemplateResourceEntityFactory
        Inherits EntityFactoryBase2(Of ItemLayoutTemplateResourceEntity)

        Public Sub New()
            MyBase.New("ItemLayoutTemplateResourceEntity", Questify.Builder.Model.ContentModel.EntityType.ItemLayoutTemplateResourceEntity, True)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New ItemLayoutTemplateResourceEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class ItemResourceEntityFactory
        Inherits EntityFactoryBase2(Of ItemResourceEntity)

        Public Sub New()
            MyBase.New("ItemResourceEntity", Questify.Builder.Model.ContentModel.EntityType.ItemResourceEntity, True)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New ItemResourceEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class ListCustomBankPropertyEntityFactory
        Inherits EntityFactoryBase2(Of ListCustomBankPropertyEntity)

        Public Sub New()
            MyBase.New("ListCustomBankPropertyEntity", Questify.Builder.Model.ContentModel.EntityType.ListCustomBankPropertyEntity, True)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New ListCustomBankPropertyEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class ListCustomBankPropertySelectedValueEntityFactory
        Inherits EntityFactoryBase2(Of ListCustomBankPropertySelectedValueEntity)

        Public Sub New()
            MyBase.New("ListCustomBankPropertySelectedValueEntity", Questify.Builder.Model.ContentModel.EntityType.ListCustomBankPropertySelectedValueEntity, False)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New ListCustomBankPropertySelectedValueEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class ListCustomBankPropertyValueEntityFactory
        Inherits EntityFactoryBase2(Of ListCustomBankPropertyValueEntity)

        Public Sub New()
            MyBase.New("ListCustomBankPropertyValueEntity", Questify.Builder.Model.ContentModel.EntityType.ListCustomBankPropertyValueEntity, True)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New ListCustomBankPropertyValueEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class ListValueCustomBankPropertyEntityFactory
        Inherits EntityFactoryBase2(Of ListValueCustomBankPropertyEntity)

        Public Sub New()
            MyBase.New("ListValueCustomBankPropertyEntity", Questify.Builder.Model.ContentModel.EntityType.ListValueCustomBankPropertyEntity, False)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New ListValueCustomBankPropertyEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class PackageResourceEntityFactory
        Inherits EntityFactoryBase2(Of PackageResourceEntity)

        Public Sub New()
            MyBase.New("PackageResourceEntity", Questify.Builder.Model.ContentModel.EntityType.PackageResourceEntity, True)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New PackageResourceEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class PermissionEntityFactory
        Inherits EntityFactoryBase2(Of PermissionEntity)

        Public Sub New()
            MyBase.New("PermissionEntity", Questify.Builder.Model.ContentModel.EntityType.PermissionEntity, False)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New PermissionEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class PermissionTargetEntityFactory
        Inherits EntityFactoryBase2(Of PermissionTargetEntity)

        Public Sub New()
            MyBase.New("PermissionTargetEntity", Questify.Builder.Model.ContentModel.EntityType.PermissionTargetEntity, False)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New PermissionTargetEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class ResourceEntityFactory
        Inherits EntityFactoryBase2(Of ResourceEntity)

        Public Sub New()
            MyBase.New("ResourceEntity", Questify.Builder.Model.ContentModel.EntityType.ResourceEntity, True)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New ResourceEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class ResourceDataEntityFactory
        Inherits EntityFactoryBase2(Of ResourceDataEntity)

        Public Sub New()
            MyBase.New("ResourceDataEntity", Questify.Builder.Model.ContentModel.EntityType.ResourceDataEntity, False)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New ResourceDataEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class ResourceHistoryEntityFactory
        Inherits EntityFactoryBase2(Of ResourceHistoryEntity)

        Public Sub New()
            MyBase.New("ResourceHistoryEntity", Questify.Builder.Model.ContentModel.EntityType.ResourceHistoryEntity, False)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New ResourceHistoryEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class RichTextValueCustomBankPropertyEntityFactory
        Inherits EntityFactoryBase2(Of RichTextValueCustomBankPropertyEntity)

        Public Sub New()
            MyBase.New("RichTextValueCustomBankPropertyEntity", Questify.Builder.Model.ContentModel.EntityType.RichTextValueCustomBankPropertyEntity, True)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New RichTextValueCustomBankPropertyEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class RichTextValueCustomBankPropertyValueEntityFactory
        Inherits EntityFactoryBase2(Of RichTextValueCustomBankPropertyValueEntity)

        Public Sub New()
            MyBase.New("RichTextValueCustomBankPropertyValueEntity", Questify.Builder.Model.ContentModel.EntityType.RichTextValueCustomBankPropertyValueEntity, True)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New RichTextValueCustomBankPropertyValueEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class RoleEntityFactory
        Inherits EntityFactoryBase2(Of RoleEntity)

        Public Sub New()
            MyBase.New("RoleEntity", Questify.Builder.Model.ContentModel.EntityType.RoleEntity, False)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New RoleEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class RolePermissionEntityFactory
        Inherits EntityFactoryBase2(Of RolePermissionEntity)

        Public Sub New()
            MyBase.New("RolePermissionEntity", Questify.Builder.Model.ContentModel.EntityType.RolePermissionEntity, False)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New RolePermissionEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class StateEntityFactory
        Inherits EntityFactoryBase2(Of StateEntity)

        Public Sub New()
            MyBase.New("StateEntity", Questify.Builder.Model.ContentModel.EntityType.StateEntity, False)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New StateEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class StateActionEntityFactory
        Inherits EntityFactoryBase2(Of StateActionEntity)

        Public Sub New()
            MyBase.New("StateActionEntity", Questify.Builder.Model.ContentModel.EntityType.StateActionEntity, False)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New StateActionEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class TestPackageResourceEntityFactory
        Inherits EntityFactoryBase2(Of TestPackageResourceEntity)

        Public Sub New()
            MyBase.New("TestPackageResourceEntity", Questify.Builder.Model.ContentModel.EntityType.TestPackageResourceEntity, True)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New TestPackageResourceEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class TreeStructureCustomBankPropertyEntityFactory
        Inherits EntityFactoryBase2(Of TreeStructureCustomBankPropertyEntity)

        Public Sub New()
            MyBase.New("TreeStructureCustomBankPropertyEntity", Questify.Builder.Model.ContentModel.EntityType.TreeStructureCustomBankPropertyEntity, True)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New TreeStructureCustomBankPropertyEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class TreeStructureCustomBankPropertySelectedPartEntityFactory
        Inherits EntityFactoryBase2(Of TreeStructureCustomBankPropertySelectedPartEntity)

        Public Sub New()
            MyBase.New("TreeStructureCustomBankPropertySelectedPartEntity", Questify.Builder.Model.ContentModel.EntityType.TreeStructureCustomBankPropertySelectedPartEntity, False)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New TreeStructureCustomBankPropertySelectedPartEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class TreeStructureCustomBankPropertyValueEntityFactory
        Inherits EntityFactoryBase2(Of TreeStructureCustomBankPropertyValueEntity)

        Public Sub New()
            MyBase.New("TreeStructureCustomBankPropertyValueEntity", Questify.Builder.Model.ContentModel.EntityType.TreeStructureCustomBankPropertyValueEntity, True)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New TreeStructureCustomBankPropertyValueEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class TreeStructurePartCustomBankPropertyEntityFactory
        Inherits EntityFactoryBase2(Of TreeStructurePartCustomBankPropertyEntity)

        Public Sub New()
            MyBase.New("TreeStructurePartCustomBankPropertyEntity", Questify.Builder.Model.ContentModel.EntityType.TreeStructurePartCustomBankPropertyEntity, False)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New TreeStructurePartCustomBankPropertyEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class UserEntityFactory
        Inherits EntityFactoryBase2(Of UserEntity)

        Public Sub New()
            MyBase.New("UserEntity", Questify.Builder.Model.ContentModel.EntityType.UserEntity, False)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New UserEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class UserApplicationRoleEntityFactory
        Inherits EntityFactoryBase2(Of UserApplicationRoleEntity)

        Public Sub New()
            MyBase.New("UserApplicationRoleEntity", Questify.Builder.Model.ContentModel.EntityType.UserApplicationRoleEntity, False)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New UserApplicationRoleEntity(fields)

            Return toReturn
        End Function

    End Class
    <Serializable()> _
    Public Class UserBankRoleEntityFactory
        Inherits EntityFactoryBase2(Of UserBankRoleEntity)

        Public Sub New()
            MyBase.New("UserBankRoleEntity", Questify.Builder.Model.ContentModel.EntityType.UserBankRoleEntity, False)
        End Sub

        Public Overrides Overloads Function Create(fields As IEntityFields2) As IEntity2
            Dim toReturn As IEntity2 = New UserBankRoleEntity(fields)

            Return toReturn
        End Function

    End Class

    <Serializable()> _
    Public Class GeneralEntityFactory
        Public Shared Function Create(entityTypeToCreate As Questify.Builder.Model.ContentModel.EntityType) As IEntity2
            Dim factoryToUse As IEntityFactory2 = Nothing
            Select Case entityTypeToCreate
                Case Questify.Builder.Model.ContentModel.EntityType.ActionEntity
                    factoryToUse = New ActionEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.AspectResourceEntity
                    factoryToUse = New AspectResourceEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.AssessmentTestResourceEntity
                    factoryToUse = New AssessmentTestResourceEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.BankEntity
                    factoryToUse = New BankEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.ChildConceptStructurePartCustomBankPropertyEntity
                    factoryToUse = New ChildConceptStructurePartCustomBankPropertyEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.ChildTreeStructurePartCustomBankPropertyEntity
                    factoryToUse = New ChildTreeStructurePartCustomBankPropertyEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.ConceptStructureCustomBankPropertyEntity
                    factoryToUse = New ConceptStructureCustomBankPropertyEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.ConceptStructureCustomBankPropertySelectedPartEntity
                    factoryToUse = New ConceptStructureCustomBankPropertySelectedPartEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.ConceptStructureCustomBankPropertyValueEntity
                    factoryToUse = New ConceptStructureCustomBankPropertyValueEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.ConceptStructurePartCustomBankPropertyEntity
                    factoryToUse = New ConceptStructurePartCustomBankPropertyEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.ConceptTypeEntity
                    factoryToUse = New ConceptTypeEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.ControlTemplateResourceEntity
                    factoryToUse = New ControlTemplateResourceEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.CustomBankPropertyEntity
                    factoryToUse = New CustomBankPropertyEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.CustomBankPropertyValueEntity
                    factoryToUse = New CustomBankPropertyValueEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.DataSourceResourceEntity
                    factoryToUse = New DataSourceResourceEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.DependentResourceEntity
                    factoryToUse = New DependentResourceEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.FreeValueCustomBankPropertyEntity
                    factoryToUse = New FreeValueCustomBankPropertyEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.FreeValueCustomBankPropertyValueEntity
                    factoryToUse = New FreeValueCustomBankPropertyValueEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.GenericResourceEntity
                    factoryToUse = New GenericResourceEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.HiddenResourceEntity
                    factoryToUse = New HiddenResourceEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.ItemLayoutTemplateResourceEntity
                    factoryToUse = New ItemLayoutTemplateResourceEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.ItemResourceEntity
                    factoryToUse = New ItemResourceEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.ListCustomBankPropertyEntity
                    factoryToUse = New ListCustomBankPropertyEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.ListCustomBankPropertySelectedValueEntity
                    factoryToUse = New ListCustomBankPropertySelectedValueEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.ListCustomBankPropertyValueEntity
                    factoryToUse = New ListCustomBankPropertyValueEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.ListValueCustomBankPropertyEntity
                    factoryToUse = New ListValueCustomBankPropertyEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.PackageResourceEntity
                    factoryToUse = New PackageResourceEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.PermissionEntity
                    factoryToUse = New PermissionEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.PermissionTargetEntity
                    factoryToUse = New PermissionTargetEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.ResourceEntity
                    factoryToUse = New ResourceEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.ResourceDataEntity
                    factoryToUse = New ResourceDataEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.ResourceHistoryEntity
                    factoryToUse = New ResourceHistoryEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.RichTextValueCustomBankPropertyEntity
                    factoryToUse = New RichTextValueCustomBankPropertyEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.RichTextValueCustomBankPropertyValueEntity
                    factoryToUse = New RichTextValueCustomBankPropertyValueEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.RoleEntity
                    factoryToUse = New RoleEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.RolePermissionEntity
                    factoryToUse = New RolePermissionEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.StateEntity
                    factoryToUse = New StateEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.StateActionEntity
                    factoryToUse = New StateActionEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.TestPackageResourceEntity
                    factoryToUse = New TestPackageResourceEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.TreeStructureCustomBankPropertyEntity
                    factoryToUse = New TreeStructureCustomBankPropertyEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.TreeStructureCustomBankPropertySelectedPartEntity
                    factoryToUse = New TreeStructureCustomBankPropertySelectedPartEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.TreeStructureCustomBankPropertyValueEntity
                    factoryToUse = New TreeStructureCustomBankPropertyValueEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.TreeStructurePartCustomBankPropertyEntity
                    factoryToUse = New TreeStructurePartCustomBankPropertyEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.UserEntity
                    factoryToUse = New UserEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.UserApplicationRoleEntity
                    factoryToUse = New UserApplicationRoleEntityFactory()
                Case Questify.Builder.Model.ContentModel.EntityType.UserBankRoleEntity
                    factoryToUse = New UserBankRoleEntityFactory()
            End Select
            Dim toReturn As IEntity2 = Nothing
            If Not factoryToUse Is Nothing Then
                toReturn = factoryToUse.Create()
            End If
            Return toReturn
        End Function
    End Class

    <Serializable()> _
    Public Class EntityFactoryFactory
        Private Shared _factoryPerType As Dictionary(Of Type, IEntityFactory2) = New Dictionary(Of Type, IEntityFactory2)()

        Shared Sub New()
            Dim entityTypeValues As Array = [Enum].GetValues(GetType(Questify.Builder.Model.ContentModel.EntityType))
            For Each entityTypeValue As Integer In entityTypeValues
                Dim dummy As IEntity2 = GeneralEntityFactory.Create(CType(entityTypeValue, Questify.Builder.Model.ContentModel.EntityType))
                _factoryPerType.Add(CType(dummy, Object).GetType(), dummy.GetEntityFactory())
            Next
        End Sub

        Public Shared Function GetFactory(typeOfEntity As Type) As IEntityFactory2
            Dim toReturn As IEntityFactory2 = Nothing
            _factoryPerType.TryGetValue(typeOfEntity, toReturn)
            Return toReturn
        End Function

        Public Shared Function GetFactory(typeOfEntity As Questify.Builder.Model.ContentModel.EntityType) As IEntityFactory2
            Return GetFactory(CType(GeneralEntityFactory.Create(typeOfEntity), Object).GetType())
        End Function
    End Class

    Public Class ElementCreator
        Inherits ElementCreatorBase
        Implements IElementCreator2

        Public Function GetFactory(entityTypeValue As Integer) As IEntityFactory2 Implements IElementCreator2.GetFactory
            Return CType(GetFactoryImpl(entityTypeValue), IEntityFactory2)
        End Function

        Public Function GetFactory(typeOfEntity As Type) As IEntityFactory2 Implements IElementCreator2.GetFactory
            Return CType(GetFactoryImpl(typeOfEntity), IEntityFactory2)
        End Function

        Public Function CreateResultsetFields(numberOfFields As Integer) As IEntityFields2 Implements IElementCreator2.CreateResultsetFields
            Return New ResultsetFields(numberOfFields)
        End Function

        Public Overrides Function ObtainInheritanceInfoProviderInstance() As IInheritanceInfoProvider
            Return InheritanceInfoProviderSingleton.GetInstance()
        End Function


        Public Overrides Overloads Function CreateDynamicRelation(leftOperand As DerivedTableDefinition) As IDynamicRelation
            Return New DynamicRelation(leftOperand)
        End Function

        Public Overrides Overloads Function CreateDynamicRelation(leftOperand As DerivedTableDefinition, joinType As JoinHint, rightOperand As DerivedTableDefinition, onClause As IPredicate) As IDynamicRelation
            Return New DynamicRelation(leftOperand, joinType, rightOperand, onClause)
        End Function

        Public Overrides Overloads Function CreateDynamicRelation(leftOperand As IEntityFieldCore, joinType As JoinHint, rightOperand As DerivedTableDefinition, aliasLeftOperand As String, onClause As IPredicate) As IDynamicRelation
            Return New DynamicRelation(leftOperand, joinType, rightOperand, aliasLeftOperand, onClause)
        End Function


        Public Overrides Overloads Function CreateDynamicRelation(leftOperand As DerivedTableDefinition, joinType As JoinHint, rightOperandEntityName As String, aliasRightOperand As String, onClause As IPredicate) As IDynamicRelation
            Return New DynamicRelation(leftOperand, joinType, CType([Enum].Parse(GetType(Questify.Builder.Model.ContentModel.EntityType), rightOperandEntityName, False), Questify.Builder.Model.ContentModel.EntityType), aliasRightOperand, onClause)
        End Function

        Public Overrides Overloads Function CreateDynamicRelation(leftOperandEntityName As String, joinType As JoinHint, rightOperandEntityName As String, aliasLeftOperand As String, aliasRightOperand As String, onClause As IPredicate) As IDynamicRelation
            Return New DynamicRelation(CType([Enum].Parse(GetType(Questify.Builder.Model.ContentModel.EntityType), leftOperandEntityName, False), Questify.Builder.Model.ContentModel.EntityType), joinType, CType([Enum].Parse(GetType(Questify.Builder.Model.ContentModel.EntityType), rightOperandEntityName, False), Questify.Builder.Model.ContentModel.EntityType), aliasLeftOperand, aliasRightOperand, onClause)
        End Function

        public Overrides Overloads Function CreateDynamicRelation(leftOperand As IEntityFieldCore, joinType As JoinHint, rightOperandEntityName As String, aliasLeftOperand As String, aliasRightOperand As String, onClause As IPredicate) As IDynamicRelation
            Return New DynamicRelation(leftOperand, joinType, CType([Enum].Parse(GetType(Questify.Builder.Model.ContentModel.EntityType), rightOperandEntityName, False), Questify.Builder.Model.ContentModel.EntityType), aliasLeftOperand, aliasRightOperand, onClause)
        End Function

        Protected Overrides Function GetFactoryImpl(entityTypeValue As Integer) As IEntityFactoryCore
            Return EntityFactoryFactory.GetFactory(CType(entityTypeValue, Questify.Builder.Model.ContentModel.EntityType))
        End Function

        Protected Overrides Function GetFactoryImpl(typeOfEntity As Type) As IEntityFactoryCore
            Return EntityFactoryFactory.GetFactory(typeOfEntity)
        End Function

    End Class
End Namespace
