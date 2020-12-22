Imports System
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.RelationClasses

    <Serializable()> _
    Public Class DynamicRelation
        Inherits DynamicRelationBase

        Public Sub New(leftOperand As DerivedTableDefinition)
            Me.InitClass(JoinHint.None, String.Empty, String.Empty, Nothing, leftOperand, Nothing)
        End Sub

        Public Sub New(leftOperand As DerivedTableDefinition, joinType As JoinHint, rightOperand As DerivedTableDefinition, onClause As IPredicate)
            Me.InitClass(joinType, String.Empty, String.Empty, onClause, leftOperand, rightOperand)
        End Sub

        Public Sub New(leftOperand As IEntityFieldCore, joinType As JoinHint, rightOperand As DerivedTableDefinition, aliasLeftOperand As String, onClause As IPredicate)
            Me.InitClass(joinType, aliasLeftOperand, string.Empty, onClause, leftOperand, rightOperand)
        End Sub

        Public Sub New(leftOperand As DerivedTableDefinition, joinType As JoinHint, rightOperand As Questify.Builder.Model.ContentModel.EntityType, aliasRightOperand As String, onClause As IPredicate)
            Me.InitClass(joinType, String.Empty, aliasRightOperand, onClause, leftOperand, GeneralEntityFactory.Create(rightOperand))
        End Sub


        Public Sub New(leftOperand As Questify.Builder.Model.ContentModel.EntityType, joinType As JoinHint, rightOperand As Questify.Builder.Model.ContentModel.EntityType, aliasLeftOperand As String, aliasRightOperand As String, onClause As IPredicate)
            Me.InitClass(joinType, aliasLeftOperand, aliasRightOperand, onClause, GeneralEntityFactory.Create(leftOperand), GeneralEntityFactory.Create(rightOperand))
        End Sub

        Public Sub New(leftOperand As IEntityFieldCore, joinType As JoinHint, rightOperand As Questify.Builder.Model.ContentModel.EntityType, aliasLeftOperand As String, aliasRightOperand As String, onClause As IPredicate)
            Me.InitClass(joinType, aliasLeftOperand, aliasRightOperand, onClause, leftOperand, GeneralEntityFactory.Create(rightOperand))
        End Sub


        Protected Overrides Function GetInheritanceProvider() As IInheritanceInfoProvider
            Return InheritanceInfoProviderSingleton.GetInstance()
        End Function
    End Class
End Namespace