Imports System.IO
Imports System.Text
Imports System.Xml

Namespace QTI.Helpers

    Public Class CustomXmlTextWriter
        Inherits XmlTextWriter

        Public Sub New(textWriter As TextWriter)
            MyBase.New(textWriter)
        End Sub

        Public Sub New(stream As Stream, encoding As Encoding)
            MyBase.New(stream, encoding)
        End Sub

        Public Sub New(path As String, encoding As Encoding)
            MyBase.New(path, encoding)
        End Sub

        Private Sub WriteNewLine()
            MyBase.WriteWhitespace(vbCrLf)
        End Sub

        Public Overrides Sub WriteEndElement()
            MyBase.WriteEndElement()
            WriteNewLine()
        End Sub

        Public Overrides Sub WriteFullEndElement()
            MyBase.WriteFullEndElement()
            WriteNewLine()
        End Sub
    End Class
End NameSpace