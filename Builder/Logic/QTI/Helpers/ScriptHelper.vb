
Imports System.IO
Imports Microsoft.Ajax.Utilities

Namespace QTI.Helpers

    Public Class ScriptHelper
        Public Shared Sub MinifyFile(fileName As String)
            Dim source = File.ReadAllText(fileName)
            Dim newSource As String = source
            If Not fileName.Contains(".min.") Then
                Select Case Path.GetExtension(fileName)
                    Case ".js"
                        newSource = MinifyJsString(source)
                    Case ".css"
                        newSource = MinifyCssString(source)
                End Select
                File.WriteAllText(fileName, newSource)
            End If
        End Sub

        Public Shared Function MinifyJsString(source As String) As String
            Dim minifier As New Minifier
            Return minifier.MinifyJavaScript(source)
        End Function

        Public Shared Function MinifyCssString(source As String) As String
            Dim minifier As New Minifier
            Return minifier.MinifyStyleSheet(source)
        End Function


    End Class
End NameSpace