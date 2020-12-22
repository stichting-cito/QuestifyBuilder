Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.RelationClasses
    Public Class DependentResourceRelations
        Public Sub New()
        End Sub

        Public Overridable Function GetAllRelations() As List(Of IEntityRelation)
            Dim toReturn As List(Of IEntityRelation) = New List(Of IEntityRelation)()
            toReturn.Add(Me.ResourceEntityUsingDependentResourceId)
            toReturn.Add(Me.ResourceEntityUsingResourceId)
            Return toReturn
        End Function




        Public Overridable ReadOnly Property ResourceEntityUsingDependentResourceId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "DependentResource", False)
                relation.AddEntityFieldPair(ResourceFields.ResourceId, DependentResourceFields.DependentResourceId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ResourceEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("DependentResourceEntity", True)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property ResourceEntityUsingResourceId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Resource", False)
                relation.AddEntityFieldPair(ResourceFields.ResourceId, DependentResourceFields.ResourceId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ResourceEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("DependentResourceEntity", True)
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

    Friend Class StaticDependentResourceRelations
        Friend Shared ReadOnly ResourceEntityUsingDependentResourceIdStatic As IEntityRelation = New DependentResourceRelations().ResourceEntityUsingDependentResourceId
        Friend Shared ReadOnly ResourceEntityUsingResourceIdStatic As IEntityRelation = New DependentResourceRelations().ResourceEntityUsingResourceId

        Shared Sub New()
        End Sub
    End Class
End Namespace
