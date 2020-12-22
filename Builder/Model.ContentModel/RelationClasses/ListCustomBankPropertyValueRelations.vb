Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.RelationClasses
    Public Class ListCustomBankPropertyValueRelations
        Inherits CustomBankPropertyValueRelations
        Public Sub New()
        End Sub

        Public Overrides Function GetAllRelations() As List(Of IEntityRelation)
            Dim toReturn As List(Of IEntityRelation) = MyBase.GetAllRelations()
            toReturn.Add(Me.ListCustomBankPropertySelectedValueEntityUsingCustomBankPropertyIdResourceId)
            toReturn.Add(Me.ListCustomBankPropertyEntityUsingCustomBankPropertyId)
            Return toReturn
        End Function


        Public Overridable ReadOnly Property ListCustomBankPropertySelectedValueEntityUsingCustomBankPropertyIdResourceId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "ListCustomBankPropertySelectedValueCollection", True)
                relation.AddEntityFieldPair(ListCustomBankPropertyValueFields.CustomBankPropertyId, ListCustomBankPropertySelectedValueFields.CustomBankPropertyId)
                relation.AddEntityFieldPair(ListCustomBankPropertyValueFields.ResourceId, ListCustomBankPropertySelectedValueFields.ResourceId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ListCustomBankPropertyValueEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ListCustomBankPropertySelectedValueEntity", False)
                Return relation
            End Get
        End Property


        Public Overrides ReadOnly Property CustomBankPropertyEntityUsingCustomBankPropertyId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "CustomBankProperty", False)
                relation.AddEntityFieldPair(CustomBankPropertyFields.CustomBankPropertyId, ListCustomBankPropertyValueFields.CustomBankPropertyId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("CustomBankPropertyEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ListCustomBankPropertyValueEntity", True)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property ListCustomBankPropertyEntityUsingCustomBankPropertyId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "ListCustomBankProperty", False)
                relation.AddEntityFieldPair(ListCustomBankPropertyFields.CustomBankPropertyId, ListCustomBankPropertyValueFields.CustomBankPropertyId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ListCustomBankPropertyEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ListCustomBankPropertyValueEntity", True)
                Return relation
            End Get
        End Property
        Public Overrides ReadOnly Property ResourceEntityUsingResourceId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Resource", False)
                relation.AddEntityFieldPair(ResourceFields.ResourceId, ListCustomBankPropertyValueFields.ResourceId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ResourceEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ListCustomBankPropertyValueEntity", True)
                Return relation
            End Get
        End Property

        Friend ReadOnly Property RelationToSuperTypeCustomBankPropertyValueEntity As IEntityRelation
            Get
                Dim relation As New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToOne, False)
                relation.AddEntityFieldPair(CustomBankPropertyValueFields.ResourceId, ListCustomBankPropertyValueFields.ResourceId)
                relation.AddEntityFieldPair(CustomBankPropertyValueFields.CustomBankPropertyId, ListCustomBankPropertyValueFields.CustomBankPropertyId)
                relation.IsHierarchyRelation = True
                Return relation
            End Get
        End Property

        Public Overrides Function GetSubTypeRelation(subTypeEntityName As String) As IEntityRelation
            Return Nothing
        End Function

        Public Overrides Function GetSuperTypeRelation() As IEntityRelation
            Return Me.RelationToSuperTypeCustomBankPropertyValueEntity
        End Function



    End Class

    Friend Class StaticListCustomBankPropertyValueRelations
        Friend Shared ReadOnly ListCustomBankPropertySelectedValueEntityUsingCustomBankPropertyIdResourceIdStatic As IEntityRelation = New ListCustomBankPropertyValueRelations().ListCustomBankPropertySelectedValueEntityUsingCustomBankPropertyIdResourceId
        Friend Shared ReadOnly CustomBankPropertyEntityUsingCustomBankPropertyIdStatic As IEntityRelation = New ListCustomBankPropertyValueRelations().CustomBankPropertyEntityUsingCustomBankPropertyId
        Friend Shared ReadOnly ListCustomBankPropertyEntityUsingCustomBankPropertyIdStatic As IEntityRelation = New ListCustomBankPropertyValueRelations().ListCustomBankPropertyEntityUsingCustomBankPropertyId
        Friend Shared ReadOnly ResourceEntityUsingResourceIdStatic As IEntityRelation = New ListCustomBankPropertyValueRelations().ResourceEntityUsingResourceId

        Shared Sub New()
        End Sub
    End Class
End Namespace
