Imports Questify.Builder.Logic.Chain
Imports Cito.Tester.ContentModel

Namespace TestConstruction.ChainHandlers.Validating
    Public Class NoItemsInRequestException
        Inherits ChainHandlerException


        Public Sub New(ByVal message As String, ByVal conflictingResourceRefs As IList(Of Datasources.ResourceRef))
            MyBase.New(message)
            Me.Data.Add("conflictingResourceRefs", conflictingResourceRefs)
        End Sub

        Public Sub New(ByVal message As String, ByVal conflictingResourceRefs As IList(Of Datasources.ResourceRef), ByVal innerException As Exception)
            MyBase.New(message, innerException)
            Me.Data.Add("conflictingResourceRefs", conflictingResourceRefs)
        End Sub

        Public Sub New()
        End Sub



        Public ReadOnly Property conflictingResourceRefs() As IList(Of Datasources.ResourceRef)
            Get
                Return CType(Me.Data("conflictingResourceRefs"), IList(Of Datasources.ResourceRef))
            End Get
        End Property


    End Class
End Namespace