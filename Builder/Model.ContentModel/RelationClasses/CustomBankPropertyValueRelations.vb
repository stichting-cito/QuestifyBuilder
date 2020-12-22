Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.RelationClasses
    Public Class CustomBankPropertyValueRelations
        Implements IRelationFactory
        Public Sub New()
        End Sub

        Public Overridable Function GetAllRelations() As List(Of IEntityRelation)
            Dim toReturn As List(Of IEntityRelation) = New List(Of IEntityRelation)()
            toReturn.Add(Me.CustomBankPropertyEntityUsingCustomBankPropertyId)
            toReturn.Add(Me.ResourceEntityUsingResourceId)
            Return toReturn
        End Function




        Public Overridable ReadOnly Property CustomBankPropertyEntityUsingCustomBankPropertyId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "CustomBankProperty", False)
                relation.AddEntityFieldPair(CustomBankPropertyFields.CustomBankPropertyId, CustomBankPropertyValueFields.CustomBankPropertyId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("CustomBankPropertyEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("CustomBankPropertyValueEntity", True)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property ResourceEntityUsingResourceId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Resource", False)
                relation.AddEntityFieldPair(ResourceFields.ResourceId, CustomBankPropertyValueFields.ResourceId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ResourceEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("CustomBankPropertyValueEntity", True)
                Return relation
            End Get
        End Property



        Friend ReadOnly Property RelationToSubTypeConceptStructureCustomBankPropertyValueEntity As IEntityRelation
            Get
                Dim relation As New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToOne, True)
                relation.AddEntityFieldPair(CustomBankPropertyValueFields.ResourceId, ConceptStructureCustomBankPropertyValueFields.ResourceId)
                relation.AddEntityFieldPair(CustomBankPropertyValueFields.CustomBankPropertyId, ConceptStructureCustomBankPropertyValueFields.CustomBankPropertyId)
                relation.IsHierarchyRelation = True
                Return relation
            End Get
        End Property
        Friend ReadOnly Property RelationToSubTypeFreeValueCustomBankPropertyValueEntity As IEntityRelation
            Get
                Dim relation As New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToOne, True)
                relation.AddEntityFieldPair(CustomBankPropertyValueFields.ResourceId, FreeValueCustomBankPropertyValueFields.ResourceId)
                relation.AddEntityFieldPair(CustomBankPropertyValueFields.CustomBankPropertyId, FreeValueCustomBankPropertyValueFields.CustomBankPropertyId)
                relation.IsHierarchyRelation = True
                Return relation
            End Get
        End Property
        Friend ReadOnly Property RelationToSubTypeListCustomBankPropertyValueEntity As IEntityRelation
            Get
                Dim relation As New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToOne, True)
                relation.AddEntityFieldPair(CustomBankPropertyValueFields.ResourceId, ListCustomBankPropertyValueFields.ResourceId)
                relation.AddEntityFieldPair(CustomBankPropertyValueFields.CustomBankPropertyId, ListCustomBankPropertyValueFields.CustomBankPropertyId)
                relation.IsHierarchyRelation = True
                Return relation
            End Get
        End Property
        Friend ReadOnly Property RelationToSubTypeRichTextValueCustomBankPropertyValueEntity As IEntityRelation
            Get
                Dim relation As New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToOne, True)
                relation.AddEntityFieldPair(CustomBankPropertyValueFields.ResourceId, RichTextValueCustomBankPropertyValueFields.ResourceId)
                relation.AddEntityFieldPair(CustomBankPropertyValueFields.CustomBankPropertyId, RichTextValueCustomBankPropertyValueFields.CustomBankPropertyId)
                relation.IsHierarchyRelation = True
                Return relation
            End Get
        End Property
        Friend ReadOnly Property RelationToSubTypeTreeStructureCustomBankPropertyValueEntity As IEntityRelation
            Get
                Dim relation As New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToOne, True)
                relation.AddEntityFieldPair(CustomBankPropertyValueFields.ResourceId, TreeStructureCustomBankPropertyValueFields.ResourceId)
                relation.AddEntityFieldPair(CustomBankPropertyValueFields.CustomBankPropertyId, TreeStructureCustomBankPropertyValueFields.CustomBankPropertyId)
                relation.IsHierarchyRelation = True
                Return relation
            End Get
        End Property
        Public Overridable Function GetSubTypeRelation(subTypeEntityName As String) As IEntityRelation Implements IRelationFactory.GetSubTypeRelation
            Select Case subTypeEntityName
                Case "ConceptStructureCustomBankPropertyValueEntity"
                    Return Me.RelationToSubTypeConceptStructureCustomBankPropertyValueEntity
                Case "FreeValueCustomBankPropertyValueEntity"
                    Return Me.RelationToSubTypeFreeValueCustomBankPropertyValueEntity
                Case "ListCustomBankPropertyValueEntity"
                    Return Me.RelationToSubTypeListCustomBankPropertyValueEntity
                Case "RichTextValueCustomBankPropertyValueEntity"
                    Return Me.RelationToSubTypeRichTextValueCustomBankPropertyValueEntity
                Case "TreeStructureCustomBankPropertyValueEntity"
                    Return Me.RelationToSubTypeTreeStructureCustomBankPropertyValueEntity
                Case Else
                    Return Nothing
            End Select
        End Function

        Public Overridable Function GetSuperTypeRelation() As IEntityRelation Implements IRelationFactory.GetSuperTypeRelation
            Return Nothing
        End Function



    End Class

    Friend Class StaticCustomBankPropertyValueRelations
        Friend Shared ReadOnly CustomBankPropertyEntityUsingCustomBankPropertyIdStatic As IEntityRelation = New CustomBankPropertyValueRelations().CustomBankPropertyEntityUsingCustomBankPropertyId
        Friend Shared ReadOnly ResourceEntityUsingResourceIdStatic As IEntityRelation = New CustomBankPropertyValueRelations().ResourceEntityUsingResourceId

        Shared Sub New()
        End Sub
    End Class
End Namespace
