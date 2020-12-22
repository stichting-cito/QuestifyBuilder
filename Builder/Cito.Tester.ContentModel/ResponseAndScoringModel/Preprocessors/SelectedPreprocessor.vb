Imports System.Xml.Serialization

Public Class SelectedPreprocessor

    <XmlElement("ruleName")>
    Public Property Rule As String

    Public Overrides Function ToString() As String
        Return Rule
    End Function

End Class
