Imports Questify.Builder.Logic.Chain
Imports Cito.Tester.ContentModel.Datasources

Public Class DataSourceConstructionRequest
    Inherits ChainHandlerRequest(Of ResourceRef)


    Public Sub New()
        MyBase.new()
    End Sub

    Public Sub New(ByVal items As IEnumerable(Of ResourceRef))
        MyBase.New(items)
    End Sub



End Class