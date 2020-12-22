Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.RelationClasses
    Public Class ConceptStructurePartCustomBankPropertyRelations
        Public Sub New()
        End Sub

        Public Overridable Function GetAllRelations() As List(Of IEntityRelation)
            Dim toReturn As List(Of IEntityRelation) = New List(Of IEntityRelation)()
            toReturn.Add(Me.ChildConceptStructurePartCustomBankPropertyEntityUsingChildConceptStructurePartCustomBankPropertyId)
            toReturn.Add(Me.ChildConceptStructurePartCustomBankPropertyEntityUsingConceptStructurePartCustomBankPropertyId)
            toReturn.Add(Me.ConceptStructureCustomBankPropertySelectedPartEntityUsingConceptStructurePartId)
            toReturn.Add(Me.ConceptStructureCustomBankPropertyEntityUsingCustomBankPropertyId)
            toReturn.Add(Me.ConceptTypeEntityUsingConceptTypeId)
            Return toReturn
        End Function


        Public Overridable ReadOnly Property ChildConceptStructurePartCustomBankPropertyEntityUsingChildConceptStructurePartCustomBankPropertyId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "ReferencedConceptStructurePartCustomBankPropertyCollection", True)
                relation.AddEntityFieldPair(ConceptStructurePartCustomBankPropertyFields.ConceptStructurePartCustomBankPropertyId, ChildConceptStructurePartCustomBankPropertyFields.ChildConceptStructurePartCustomBankPropertyId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ConceptStructurePartCustomBankPropertyEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ChildConceptStructurePartCustomBankPropertyEntity", False)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property ChildConceptStructurePartCustomBankPropertyEntityUsingConceptStructurePartCustomBankPropertyId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "ChildConceptStructurePartCustomBankPropertyCollection", True)
                relation.AddEntityFieldPair(ConceptStructurePartCustomBankPropertyFields.ConceptStructurePartCustomBankPropertyId, ChildConceptStructurePartCustomBankPropertyFields.ConceptStructurePartCustomBankPropertyId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ConceptStructurePartCustomBankPropertyEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ChildConceptStructurePartCustomBankPropertyEntity", False)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property ConceptStructureCustomBankPropertySelectedPartEntityUsingConceptStructurePartId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "ConceptStructureCustomBankPropertySelectedPartCollection", True)
                relation.AddEntityFieldPair(ConceptStructurePartCustomBankPropertyFields.ConceptStructurePartCustomBankPropertyId, ConceptStructureCustomBankPropertySelectedPartFields.ConceptStructurePartId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ConceptStructurePartCustomBankPropertyEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ConceptStructureCustomBankPropertySelectedPartEntity", False)
                Return relation
            End Get
        End Property


        Public Overridable ReadOnly Property ConceptStructureCustomBankPropertyEntityUsingCustomBankPropertyId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "ConceptStructureCustomBankProperty", False)
                relation.AddEntityFieldPair(ConceptStructureCustomBankPropertyFields.CustomBankPropertyId, ConceptStructurePartCustomBankPropertyFields.CustomBankPropertyId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ConceptStructureCustomBankPropertyEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ConceptStructurePartCustomBankPropertyEntity", True)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property ConceptTypeEntityUsingConceptTypeId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "ConceptType", False)
                relation.AddEntityFieldPair(ConceptTypeFields.ConceptTypeId, ConceptStructurePartCustomBankPropertyFields.ConceptTypeId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ConceptTypeEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ConceptStructurePartCustomBankPropertyEntity", True)
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

    Friend Class StaticConceptStructurePartCustomBankPropertyRelations
        Friend Shared ReadOnly ChildConceptStructurePartCustomBankPropertyEntityUsingChildConceptStructurePartCustomBankPropertyIdStatic As IEntityRelation = New ConceptStructurePartCustomBankPropertyRelations().ChildConceptStructurePartCustomBankPropertyEntityUsingChildConceptStructurePartCustomBankPropertyId
        Friend Shared ReadOnly ChildConceptStructurePartCustomBankPropertyEntityUsingConceptStructurePartCustomBankPropertyIdStatic As IEntityRelation = New ConceptStructurePartCustomBankPropertyRelations().ChildConceptStructurePartCustomBankPropertyEntityUsingConceptStructurePartCustomBankPropertyId
        Friend Shared ReadOnly ConceptStructureCustomBankPropertySelectedPartEntityUsingConceptStructurePartIdStatic As IEntityRelation = New ConceptStructurePartCustomBankPropertyRelations().ConceptStructureCustomBankPropertySelectedPartEntityUsingConceptStructurePartId
        Friend Shared ReadOnly ConceptStructureCustomBankPropertyEntityUsingCustomBankPropertyIdStatic As IEntityRelation = New ConceptStructurePartCustomBankPropertyRelations().ConceptStructureCustomBankPropertyEntityUsingCustomBankPropertyId
        Friend Shared ReadOnly ConceptTypeEntityUsingConceptTypeIdStatic As IEntityRelation = New ConceptStructurePartCustomBankPropertyRelations().ConceptTypeEntityUsingConceptTypeId

        Shared Sub New()
        End Sub
    End Class
End Namespace
