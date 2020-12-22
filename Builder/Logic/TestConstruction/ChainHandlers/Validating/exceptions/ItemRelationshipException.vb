Imports Questify.Builder.Logic.Chain
Imports Cito.Tester.ContentModel
Namespace TestConstruction.ChainHandlers.Validating
    Public Class ItemRelationshipException
        Inherits ChainHandlerException

        Public Sub New(ByVal message As String, ByVal identifierOfSelection As String, ByVal behaviour As Datasources.DataSourceBehaviourEnum, ByVal conflictingResourceRefs As IList(Of Datasources.ResourceRef), ByVal conflictsCausedBygResourceRefs As IList(Of Datasources.ResourceRef))
            MyBase.New(message)

            Me.Data.Add("behaviour", behaviour)
            Me.Data.Add("conflictingResourceRefs", conflictingResourceRefs)
            Me.Data.Add("conflictsCausedBygResourceRefs", conflictsCausedBygResourceRefs)
            Me.Data.Add("identifierOfSelection", identifierOfSelection)
        End Sub


        Public ReadOnly Property Behaviour() As Datasources.DataSourceBehaviourEnum
            Get
                Return CType(Me.Data("behaviour"), Datasources.DataSourceBehaviourEnum)
            End Get
        End Property

        Public ReadOnly Property ConflictingResourceRefs() As IList(Of Datasources.ResourceRef)
            Get
                Return CType(Me.Data("conflictingResourceRefs"), IList(Of Datasources.ResourceRef))
            End Get
        End Property

        Public ReadOnly Property ConflictingCausedByResourceRefs() As IList(Of Datasources.ResourceRef)
            Get
                Return CType(Me.Data("conflictsCausedBygResourceRefs"), IList(Of Datasources.ResourceRef))
            End Get
        End Property

        Public ReadOnly Property IdentifierOfSelection() As String
            Get
                Return CType(Me.Data("identifierOfSelection"), String)
            End Get
        End Property
    End Class
End Namespace