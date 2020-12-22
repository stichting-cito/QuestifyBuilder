Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.RelationClasses
    Public Class ResourceRelations
        Implements IRelationFactory
        Public Sub New()
        End Sub

        Public Overridable Function GetAllRelations() As List(Of IEntityRelation)
            Dim toReturn As List(Of IEntityRelation) = New List(Of IEntityRelation)()
            toReturn.Add(Me.CustomBankPropertyValueEntityUsingResourceId)
            toReturn.Add(Me.DependentResourceEntityUsingDependentResourceId)
            toReturn.Add(Me.DependentResourceEntityUsingResourceId)
            toReturn.Add(Me.HiddenResourceEntityUsingResourceId)
            toReturn.Add(Me.ResourceHistoryEntityUsingResourceId)
            toReturn.Add(Me.ResourceDataEntityUsingResourceId)
            toReturn.Add(Me.BankEntityUsingBankId)
            toReturn.Add(Me.StateEntityUsingStateId)
            toReturn.Add(Me.UserEntityUsingCreatedBy)
            toReturn.Add(Me.UserEntityUsingModifiedBy)
            Return toReturn
        End Function


        Public Overridable ReadOnly Property CustomBankPropertyValueEntityUsingResourceId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "CustomBankPropertyValueCollection", True)
                relation.AddEntityFieldPair(ResourceFields.ResourceId, CustomBankPropertyValueFields.ResourceId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ResourceEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("CustomBankPropertyValueEntity", False)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property DependentResourceEntityUsingDependentResourceId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "ReferencedResourceCollection", True)
                relation.AddEntityFieldPair(ResourceFields.ResourceId, DependentResourceFields.DependentResourceId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ResourceEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("DependentResourceEntity", False)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property DependentResourceEntityUsingResourceId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "DependentResourceCollection", True)
                relation.AddEntityFieldPair(ResourceFields.ResourceId, DependentResourceFields.ResourceId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ResourceEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("DependentResourceEntity", False)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property HiddenResourceEntityUsingResourceId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "HiddenResourceCollection", True)
                relation.AddEntityFieldPair(ResourceFields.ResourceId, HiddenResourceFields.ResourceId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ResourceEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("HiddenResourceEntity", False)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property ResourceHistoryEntityUsingResourceId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "ResourceHistoryCollection", True)
                relation.AddEntityFieldPair(ResourceFields.ResourceId, ResourceHistoryFields.ResourceId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ResourceEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ResourceHistoryEntity", False)
                Return relation
            End Get
        End Property

        Public Overridable ReadOnly Property ResourceDataEntityUsingResourceId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToOne, "ResourceData", True)

                relation.AddEntityFieldPair(ResourceFields.ResourceId, ResourceDataFields.ResourceId)



                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ResourceEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ResourceDataEntity", False)
                Return relation
            End Get
        End Property

        Public Overridable ReadOnly Property BankEntityUsingBankId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Bank", False)
                relation.AddEntityFieldPair(BankFields.Id, ResourceFields.BankId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("BankEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ResourceEntity", True)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property StateEntityUsingStateId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "State", False)
                relation.AddEntityFieldPair(StateFields.StateId, ResourceFields.StateId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("StateEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ResourceEntity", True)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property UserEntityUsingCreatedBy() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "CreatedByUser", False)
                relation.AddEntityFieldPair(UserFields.Id, ResourceFields.CreatedBy)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ResourceEntity", True)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property UserEntityUsingModifiedBy() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "ModifiedByUser", False)
                relation.AddEntityFieldPair(UserFields.Id, ResourceFields.ModifiedBy)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ResourceEntity", True)
                Return relation
            End Get
        End Property



        Friend ReadOnly Property RelationToSubTypeAspectResourceEntity As IEntityRelation
            Get
                Dim relation As New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToOne, True)
                relation.AddEntityFieldPair(ResourceFields.ResourceId, AspectResourceFields.ResourceId)
                relation.IsHierarchyRelation = True
                Return relation
            End Get
        End Property
        Friend ReadOnly Property RelationToSubTypeAssessmentTestResourceEntity As IEntityRelation
            Get
                Dim relation As New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToOne, True)
                relation.AddEntityFieldPair(ResourceFields.ResourceId, AssessmentTestResourceFields.ResourceId)
                relation.IsHierarchyRelation = True
                Return relation
            End Get
        End Property
        Friend ReadOnly Property RelationToSubTypeControlTemplateResourceEntity As IEntityRelation
            Get
                Dim relation As New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToOne, True)
                relation.AddEntityFieldPair(ResourceFields.ResourceId, ControlTemplateResourceFields.ResourceId)
                relation.IsHierarchyRelation = True
                Return relation
            End Get
        End Property
        Friend ReadOnly Property RelationToSubTypeDataSourceResourceEntity As IEntityRelation
            Get
                Dim relation As New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToOne, True)
                relation.AddEntityFieldPair(ResourceFields.ResourceId, DataSourceResourceFields.ResourceId)
                relation.IsHierarchyRelation = True
                Return relation
            End Get
        End Property
        Friend ReadOnly Property RelationToSubTypeGenericResourceEntity As IEntityRelation
            Get
                Dim relation As New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToOne, True)
                relation.AddEntityFieldPair(ResourceFields.ResourceId, GenericResourceFields.ResourceId)
                relation.IsHierarchyRelation = True
                Return relation
            End Get
        End Property
        Friend ReadOnly Property RelationToSubTypeItemLayoutTemplateResourceEntity As IEntityRelation
            Get
                Dim relation As New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToOne, True)
                relation.AddEntityFieldPair(ResourceFields.ResourceId, ItemLayoutTemplateResourceFields.ResourceId)
                relation.IsHierarchyRelation = True
                Return relation
            End Get
        End Property
        Friend ReadOnly Property RelationToSubTypeItemResourceEntity As IEntityRelation
            Get
                Dim relation As New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToOne, True)
                relation.AddEntityFieldPair(ResourceFields.ResourceId, ItemResourceFields.ResourceId)
                relation.IsHierarchyRelation = True
                Return relation
            End Get
        End Property
        Friend ReadOnly Property RelationToSubTypePackageResourceEntity As IEntityRelation
            Get
                Dim relation As New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToOne, True)
                relation.AddEntityFieldPair(ResourceFields.ResourceId, PackageResourceFields.ResourceId)
                relation.IsHierarchyRelation = True
                Return relation
            End Get
        End Property
        Friend ReadOnly Property RelationToSubTypeTestPackageResourceEntity As IEntityRelation
            Get
                Dim relation As New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToOne, True)
                relation.AddEntityFieldPair(ResourceFields.ResourceId, TestPackageResourceFields.ResourceId)
                relation.IsHierarchyRelation = True
                Return relation
            End Get
        End Property
        Public Overridable Function GetSubTypeRelation(subTypeEntityName As String) As IEntityRelation Implements IRelationFactory.GetSubTypeRelation
            Select Case subTypeEntityName
                Case "AspectResourceEntity"
                    Return Me.RelationToSubTypeAspectResourceEntity
                Case "AssessmentTestResourceEntity"
                    Return Me.RelationToSubTypeAssessmentTestResourceEntity
                Case "ControlTemplateResourceEntity"
                    Return Me.RelationToSubTypeControlTemplateResourceEntity
                Case "DataSourceResourceEntity"
                    Return Me.RelationToSubTypeDataSourceResourceEntity
                Case "GenericResourceEntity"
                    Return Me.RelationToSubTypeGenericResourceEntity
                Case "ItemLayoutTemplateResourceEntity"
                    Return Me.RelationToSubTypeItemLayoutTemplateResourceEntity
                Case "ItemResourceEntity"
                    Return Me.RelationToSubTypeItemResourceEntity
                Case "PackageResourceEntity"
                    Return Me.RelationToSubTypePackageResourceEntity
                Case "TestPackageResourceEntity"
                    Return Me.RelationToSubTypeTestPackageResourceEntity
                Case Else
                    Return Nothing
            End Select
        End Function

        Public Overridable Function GetSuperTypeRelation() As IEntityRelation Implements IRelationFactory.GetSuperTypeRelation
            Return Nothing
        End Function



    End Class

    Friend Class StaticResourceRelations
        Friend Shared ReadOnly CustomBankPropertyValueEntityUsingResourceIdStatic As IEntityRelation = New ResourceRelations().CustomBankPropertyValueEntityUsingResourceId
        Friend Shared ReadOnly DependentResourceEntityUsingDependentResourceIdStatic As IEntityRelation = New ResourceRelations().DependentResourceEntityUsingDependentResourceId
        Friend Shared ReadOnly DependentResourceEntityUsingResourceIdStatic As IEntityRelation = New ResourceRelations().DependentResourceEntityUsingResourceId
        Friend Shared ReadOnly HiddenResourceEntityUsingResourceIdStatic As IEntityRelation = New ResourceRelations().HiddenResourceEntityUsingResourceId
        Friend Shared ReadOnly ResourceHistoryEntityUsingResourceIdStatic As IEntityRelation = New ResourceRelations().ResourceHistoryEntityUsingResourceId
        Friend Shared ReadOnly ResourceDataEntityUsingResourceIdStatic As IEntityRelation = New ResourceRelations().ResourceDataEntityUsingResourceId
        Friend Shared ReadOnly BankEntityUsingBankIdStatic As IEntityRelation = New ResourceRelations().BankEntityUsingBankId
        Friend Shared ReadOnly StateEntityUsingStateIdStatic As IEntityRelation = New ResourceRelations().StateEntityUsingStateId
        Friend Shared ReadOnly UserEntityUsingCreatedByStatic As IEntityRelation = New ResourceRelations().UserEntityUsingCreatedBy
        Friend Shared ReadOnly UserEntityUsingModifiedByStatic As IEntityRelation = New ResourceRelations().UserEntityUsingModifiedBy

        Shared Sub New()
        End Sub
    End Class
End Namespace
