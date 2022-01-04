Imports Xilium.CefGlue

Friend Class PreviewApp
    Inherits CefApp

    Protected Overrides Sub OnBeforeCommandLineProcessing(ByVal processType As String, ByVal commandLine As CefCommandLine)
        commandLine.AppendSwitch("--allow-file-access-from-files")
        commandLine.AppendSwitch("--no-proxy-server")


        commandLine.AppendSwitch("--single-process")
        commandLine.AppendSwitch("--ignore-certificate-errors")
    End Sub

End Class
