Imports Questify.Builder.Logic.Chain
Imports Cito.Tester.ContentModel

Namespace TestPackageConstruction.ChainHandlers.Validating
    Public Class RemoveTestIsUsedAsRetryException
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

    End Class
End Namespace