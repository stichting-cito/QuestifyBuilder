Imports System.IO
Imports System.Text

Namespace QTI.Helpers.QTI_Base

    Public Class StyleSheetHelper


        Protected Const SourceTextStyleElement As String = "dep-referenceInfo"
        Protected Const CanvasStyleElement As String = "custom-qti-style"


        Public Overridable Sub AddStyleToCss(ByRef css As String, styles As List(Of String))
            Dim cssStringBuilder As New StringBuilder(css)
            For Each style As String In styles
                cssStringBuilder.AppendLine(style)
            Next
            css = cssStringBuilder.ToString
        End Sub

        Public Function PrefixStylesheet(file As String) As String
            Dim returnValue As String = file
            If Not file.Contains("/") AndAlso Not file.Contains("\") AndAlso Not Path.GetExtension(file) = ".js" Then
                If Not file.StartsWith("cito_") Then
                    returnValue = String.Concat("cito_", file)
                End If
            Else
                Debug.Assert(True, "Input file should be as filename, not a path or url")
            End If
            Return returnValue
        End Function

        Public Function GetSourceTextStylesheetName(cssName As String) As String
            Dim result As String = cssName.Replace(Chr(32), "_"c)
            result = PrefixStylesheet(result)
            Return String.Concat(Path.GetFileNameWithoutExtension(result), "_source.css")
        End Function

        Public Overridable Function PrefixSourceTextStyles(cssContent As String) As String
            Return cssContent
        End Function

        Public Overridable Function PrefixGeneratedStyles(ByRef cssContent As String) As Boolean
            Return False
        End Function

        Public Overridable Function StripUnwantedPrefixesFromStylesheet(ByRef cssContent As String) As Boolean
            Return False
        End Function

        Public Overridable Function StripUnwantedPrefixesFromSourcetextStylesheet(ByRef cssContent As String) As Boolean
            Return False
        End Function

    End Class
End NameSpace