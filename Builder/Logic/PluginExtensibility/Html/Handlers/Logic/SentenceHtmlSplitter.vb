Imports System.Xml
Imports System.Text.RegularExpressions

Namespace PluginExtensibility.Html.Handlers.Logic

    Public Class SentenceHtmlSplitter : Inherits RegExHtmlSplitter

        Private Shared sentenceDetector As New Regex("(.*?)[.?!]")

        Public Sub New(selectedNode As XmlNode, startOffset As Integer, endNode As XmlNode, endOffset As Integer)
            MyBase.New(sentenceDetector, selectedNode, startOffset, endNode, endOffset)
        End Sub

    End Class

End Namespace
