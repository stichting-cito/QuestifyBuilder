Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.RelationClasses
    Public Class ChildTreeStructurePartCustomBankPropertyRelations
        Public Sub New()
        End Sub

        Public Overridable Function GetAllRelations() As List(Of IEntityRelation)
            Dim toReturn As List(Of IEntityRelation) = New List(Of IEntityRelation)()
            toReturn.Add(Me.TreeStructurePartCustomBankPropertyEntityUsingTreeStructurePartCustomBankPropertyId)
            Return toReturn
        End Function




        Public Overridable ReadOnly Property TreeStructurePartCustomBankPropertyEntityUsingTreeStructurePartCustomBankPropertyId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "TreeStructurePartCustomBankProperty", False)
                relation.AddEntityFieldPair(TreeStructurePartCustomBankPropertyFields.TreeStructurePartCustomBankPropertyId, ChildTreeStructurePartCustomBankPropertyFields.TreeStructurePartCustomBankPropertyId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TreeStructurePartCustomBankPropertyEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ChildTreeStructurePartCustomBankPropertyEntity", True)
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

    Friend Class StaticChildTreeStructurePartCustomBankPropertyRelations
        Friend Shared ReadOnly TreeStructurePartCustomBankPropertyEntityUsingTreeStructurePartCustomBankPropertyIdStatic As IEntityRelation = New ChildTreeStructurePartCustomBankPropertyRelations().TreeStructurePartCustomBankPropertyEntityUsingTreeStructurePartCustomBankPropertyId

        Shared Sub New()
        End Sub
    End Class
End Namespace
