Imports Cito.Tester.ContentModel.Datasources
Imports Questify.Builder.Logic.Common.Filtering

Public NotInheritable Class ModifyDataSourceRequestHandler
    Inherits ModifyResourceRefRequestHandler(Of DataSourceConstructionRequest)


    Public Sub New(ByVal requestType As FilterRequestTypeEnum, ByVal items As IEnumerable(Of ResourceRef))
        MyBase.New(requesttype, items)
    End Sub

    Public Sub New(ByVal requestType As FilterRequestTypeEnum, ByVal items As IEnumerable(Of String))
        MyBase.New(requestType, items)
    End Sub

End Class