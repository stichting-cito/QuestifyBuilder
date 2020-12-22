Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.RelationClasses
    Public Class TreeStructurePartCustomBankPropertyRelations
        Public Sub New()
        End Sub

        Public Overridable Function GetAllRelations() As List(Of IEntityRelation)
            Dim toReturn As List(Of IEntityRelation) = New List(Of IEntityRelation)()
            toReturn.Add(Me.ChildTreeStructurePartCustomBankPropertyEntityUsingTreeStructurePartCustomBankPropertyId)
            toReturn.Add(Me.TreeStructureCustomBankPropertySelectedPartEntityUsingTreeStructurePartId)
            toReturn.Add(Me.TreeStructureCustomBankPropertyEntityUsingCustomBankPropertyId)
            Return toReturn
        End Function


        Public Overridable ReadOnly Property ChildTreeStructurePartCustomBankPropertyEntityUsingTreeStructurePartCustomBankPropertyId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "ChildTreeStructurePartCustomBankPropertyCollection", True)
                relation.AddEntityFieldPair(TreeStructurePartCustomBankPropertyFields.TreeStructurePartCustomBankPropertyId, ChildTreeStructurePartCustomBankPropertyFields.TreeStructurePartCustomBankPropertyId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TreeStructurePartCustomBankPropertyEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ChildTreeStructurePartCustomBankPropertyEntity", False)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property TreeStructureCustomBankPropertySelectedPartEntityUsingTreeStructurePartId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "TreeStructureCustomBankPropertySelectedPartCollection", True)
                relation.AddEntityFieldPair(TreeStructurePartCustomBankPropertyFields.TreeStructurePartCustomBankPropertyId, TreeStructureCustomBankPropertySelectedPartFields.TreeStructurePartId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TreeStructurePartCustomBankPropertyEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TreeStructureCustomBankPropertySelectedPartEntity", False)
                Return relation
            End Get
        End Property


        Public Overridable ReadOnly Property TreeStructureCustomBankPropertyEntityUsingCustomBankPropertyId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "TreeStructureCustomBankProperty", False)
                relation.AddEntityFieldPair(TreeStructureCustomBankPropertyFields.CustomBankPropertyId, TreeStructurePartCustomBankPropertyFields.CustomBankPropertyId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TreeStructureCustomBankPropertyEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TreeStructurePartCustomBankPropertyEntity", True)
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

    Friend Class StaticTreeStructurePartCustomBankPropertyRelations
        Friend Shared ReadOnly ChildTreeStructurePartCustomBankPropertyEntityUsingTreeStructurePartCustomBankPropertyIdStatic As IEntityRelation = New TreeStructurePartCustomBankPropertyRelations().ChildTreeStructurePartCustomBankPropertyEntityUsingTreeStructurePartCustomBankPropertyId
        Friend Shared ReadOnly TreeStructureCustomBankPropertySelectedPartEntityUsingTreeStructurePartIdStatic As IEntityRelation = New TreeStructurePartCustomBankPropertyRelations().TreeStructureCustomBankPropertySelectedPartEntityUsingTreeStructurePartId
        Friend Shared ReadOnly TreeStructureCustomBankPropertyEntityUsingCustomBankPropertyIdStatic As IEntityRelation = New TreeStructurePartCustomBankPropertyRelations().TreeStructureCustomBankPropertyEntityUsingCustomBankPropertyId

        Shared Sub New()
        End Sub
    End Class
End Namespace
