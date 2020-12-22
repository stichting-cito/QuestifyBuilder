Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.RelationClasses
    Public Class ListValueCustomBankPropertyRelations
        Public Sub New()
        End Sub

        Public Overridable Function GetAllRelations() As List(Of IEntityRelation)
            Dim toReturn As List(Of IEntityRelation) = New List(Of IEntityRelation)()
            toReturn.Add(Me.ListCustomBankPropertySelectedValueEntityUsingListValueBankCustomPropertyId)
            toReturn.Add(Me.ListCustomBankPropertyEntityUsingCustomBankPropertyId)
            Return toReturn
        End Function


        Public Overridable ReadOnly Property ListCustomBankPropertySelectedValueEntityUsingListValueBankCustomPropertyId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "ListCustomBankPropertySelectedValueCollection", True)
                relation.AddEntityFieldPair(ListValueCustomBankPropertyFields.ListValueBankCustomPropertyId, ListCustomBankPropertySelectedValueFields.ListValueBankCustomPropertyId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ListValueCustomBankPropertyEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ListCustomBankPropertySelectedValueEntity", False)
                Return relation
            End Get
        End Property


        Public Overridable ReadOnly Property ListCustomBankPropertyEntityUsingCustomBankPropertyId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "ListCustomBankProperty", False)
                relation.AddEntityFieldPair(ListCustomBankPropertyFields.CustomBankPropertyId, ListValueCustomBankPropertyFields.CustomBankPropertyId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ListCustomBankPropertyEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ListValueCustomBankPropertyEntity", True)
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

    Friend Class StaticListValueCustomBankPropertyRelations
        Friend Shared ReadOnly ListCustomBankPropertySelectedValueEntityUsingListValueBankCustomPropertyIdStatic As IEntityRelation = New ListValueCustomBankPropertyRelations().ListCustomBankPropertySelectedValueEntityUsingListValueBankCustomPropertyId
        Friend Shared ReadOnly ListCustomBankPropertyEntityUsingCustomBankPropertyIdStatic As IEntityRelation = New ListValueCustomBankPropertyRelations().ListCustomBankPropertyEntityUsingCustomBankPropertyId

        Shared Sub New()
        End Sub
    End Class
End Namespace
