Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.RelationClasses
    Public Class ConceptTypeRelations
        Public Sub New()
        End Sub

        Public Overridable Function GetAllRelations() As List(Of IEntityRelation)
            Dim toReturn As List(Of IEntityRelation) = New List(Of IEntityRelation)()
            toReturn.Add(Me.ConceptStructurePartCustomBankPropertyEntityUsingConceptTypeId)
            Return toReturn
        End Function


        Public Overridable ReadOnly Property ConceptStructurePartCustomBankPropertyEntityUsingConceptTypeId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "ConceptStructurePartCustomBankPropertyCollection", True)
                relation.AddEntityFieldPair(ConceptTypeFields.ConceptTypeId, ConceptStructurePartCustomBankPropertyFields.ConceptTypeId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ConceptTypeEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ConceptStructurePartCustomBankPropertyEntity", False)
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

    Friend Class StaticConceptTypeRelations
        Friend Shared ReadOnly ConceptStructurePartCustomBankPropertyEntityUsingConceptTypeIdStatic As IEntityRelation = New ConceptTypeRelations().ConceptStructurePartCustomBankPropertyEntityUsingConceptTypeId

        Shared Sub New()
        End Sub
    End Class
End Namespace
