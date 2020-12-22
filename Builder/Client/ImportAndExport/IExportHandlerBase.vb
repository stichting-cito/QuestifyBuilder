Imports Cito.Tester.Common
Public Interface IExportHandlerBase


    ReadOnly Property GetOptionsUserControl() As ExportOptionControlBase

    ReadOnly Property ProgressMessage() As String

    ReadOnly Property UserFriendlyName() As String


    Event Progress(ByVal sender As Object, ByVal e As ProgressEventArgs)
    Event StartProgress(ByVal sender As Object, ByVal e As StartEventArgs)

End Interface
