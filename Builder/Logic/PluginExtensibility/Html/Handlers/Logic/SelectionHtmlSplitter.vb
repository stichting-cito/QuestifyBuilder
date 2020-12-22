Imports System.Xml
Imports System.Text.RegularExpressions

Namespace PluginExtensibility.Html.Handlers.Logic

    Public Class SelectionHtmlSplitter : Inherits RegExHtmlSplitter
        Private Shared SelectionDetector As New Regex(".+")

        Public Sub New(selectedNode As XmlNode, startOffset As Integer, endNode As XmlNode, endOffset As Integer)
            MyBase.New(SelectionDetector, selectedNode, startOffset, endNode, endOffset)
        End Sub

    End Class

End Namespace