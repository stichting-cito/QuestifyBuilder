Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.RelationClasses
    Public Class ListCustomBankPropertySelectedValueRelations
        Public Sub New()
        End Sub

        Public Overridable Function GetAllRelations() As List(Of IEntityRelation)
            Dim toReturn As List(Of IEntityRelation) = New List(Of IEntityRelation)()
            toReturn.Add(Me.ListCustomBankPropertyValueEntityUsingCustomBankPropertyIdResourceId)
            toReturn.Add(Me.ListValueCustomBankPropertyEntityUsingListValueBankCustomPropertyId)
            Return toReturn
        End Function




        Public Overridable ReadOnly Property ListCustomBankPropertyValueEntityUsingCustomBankPropertyIdResourceId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "ListCustomBankPropertyValue", False)
                relation.AddEntityFieldPair(ListCustomBankPropertyValueFields.CustomBankPropertyId, ListCustomBankPropertySelectedValueFields.CustomBankPropertyId)
                relation.AddEntityFieldPair(ListCustomBankPropertyValueFields.ResourceId, ListCustomBankPropertySelectedValueFields.ResourceId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ListCustomBankPropertyValueEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ListCustomBankPropertySelectedValueEntity", True)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property ListValueCustomBankPropertyEntityUsingListValueBankCustomPropertyId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "ListValueCustomBankProperty", False)
                relation.AddEntityFieldPair(ListValueCustomBankPropertyFields.ListValueBankCustomPropertyId, ListCustomBankPropertySelectedValueFields.ListValueBankCustomPropertyId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ListValueCustomBankPropertyEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ListCustomBankPropertySelectedValueEntity", True)
                Return relation
            End Get
        End Property
        Public Overridable Function GetSubTypeRelation(subTypeEntityName As String) As IEntityRelation
            Return Nothing
        End Function
        Public Overridable Function GetSuperTypeRelation() As IEntityRelation
            Return Nothing
        End Function


    End Class

    Friend Class StaticListCustomBankPropertySelectedValueRelations
        Friend Shared ReadOnly ListCustomBankPropertyValueEntityUsingCustomBankPropertyIdResourceIdStatic As IEntityRelation = New ListCustomBankPropertySelectedValueRelations().ListCustomBankPropertyValueEntityUsingCustomBankPropertyIdResourceId
        Friend Shared ReadOnly ListValueCustomBankPropertyEntityUsingListValueBankCustomPropertyIdStatic As IEntityRelation = New ListCustomBankPropertySelectedValueRelations().ListValueCustomBankPropertyEntityUsingListValueBankCustomPropertyId

        Shared Sub New()
        End Sub
    End Class
End Namespace
