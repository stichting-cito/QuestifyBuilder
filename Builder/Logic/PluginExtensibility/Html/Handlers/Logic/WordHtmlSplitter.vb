Imports System.Xml
Imports System.Text.RegularExpressions

Namespace PluginExtensibility.Html.Handlers.Logic

    Public Class WordHtmlSplitter : Inherits RegExHtmlSplitter

        Private Shared wordDetector As New Regex("\w+")

        Public Sub New(selectedNode As XmlNode, startOffset As Integer, endNode As XmlNode, endOffset As Integer)
            MyBase.New(wordDetector, selectedNode, startOffset, endNode, endOffset)
        End Sub

    End Class

End Namespace
