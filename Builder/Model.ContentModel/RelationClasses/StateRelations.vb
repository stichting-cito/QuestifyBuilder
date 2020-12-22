Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.RelationClasses
    Public Class StateRelations
        Public Sub New()
        End Sub

        Public Overridable Function GetAllRelations() As List(Of IEntityRelation)
            Dim toReturn As List(Of IEntityRelation) = New List(Of IEntityRelation)()
            toReturn.Add(Me.CustomBankPropertyEntityUsingStateId)
            toReturn.Add(Me.ResourceEntityUsingStateId)
            toReturn.Add(Me.StateActionEntityUsingStateId)
            Return toReturn
        End Function


        Public Overridable ReadOnly Property CustomBankPropertyEntityUsingStateId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "CustomBankPropertyCollection", True)
                relation.AddEntityFieldPair(StateFields.StateId, CustomBankPropertyFields.StateId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("StateEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("CustomBankPropertyEntity", False)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property ResourceEntityUsingStateId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "ResourceCollection", True)
                relation.AddEntityFieldPair(StateFields.StateId, ResourceFields.StateId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("StateEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ResourceEntity", False)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property StateActionEntityUsingStateId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "StateActionCollection", True)
                relation.AddEntityFieldPair(StateFields.StateId, StateActionFields.StateId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("StateEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("StateActionEntity", False)
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

    Friend Class StaticStateRelations
        Friend Shared ReadOnly CustomBankPropertyEntityUsingStateIdStatic As IEntityRelation = New StateRelations().CustomBankPropertyEntityUsingStateId
        Friend Shared ReadOnly ResourceEntityUsingStateIdStatic As IEntityRelation = New StateRelations().ResourceEntityUsingStateId
        Friend Shared ReadOnly StateActionEntityUsingStateIdStatic As IEntityRelation = New StateRelations().StateActionEntityUsingStateId

        Shared Sub New()
        End Sub
    End Class
End Namespace
