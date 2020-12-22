Imports System.Linq
Imports System.Net
Imports System.Xml
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ItemProcessing
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic

Namespace PresentationControls.TextEditor.Handlers

    Public Class HtmlInlineSplicer

        Private ReadOnly _handler As HtmlInlineDialogHandler

        Public Sub New(handler As HtmlInlineDialogHandler)
            _handler = handler
        End Sub

        Public Function Execute(strategyName As String, startNode As XmlNode, startPos As Integer, endNode As XmlNode, endPos As Integer) As IEnumerable(Of KeyValuePair(Of InlineElement, XmlNode))
            Dim ret As New List(Of KeyValuePair(Of InlineElement, XmlNode))
            Dim axe As IInlineTextSplitter = GetByStrategyName(strategyName, startNode, startPos, endNode, endPos)
            Dim inline As InlineElement = Nothing
            Dim cachingStrategy As IITemSetupCacheHelper = New InMemoryParameterSetCacheByBank(0)

            If axe IsNot Nothing Then
                For Each slice As XmlNode In axe.Split()

                    inline = Nothing
                    If slice.LocalName = "span" Then
                        Dim spanElement As XmlElement = DirectCast(slice, XmlElement)
                        Dim result As KeyValuePair(Of InlineElement, XmlNode) = _handler.ExecuteNoDialog(cachingStrategy)

                        If spanElement.ParentNode IsNot Nothing Then
                            Dim replaceSpan = spanElement.OwnerDocument.CreateElement("span", "http://www.w3.org/1999/xhtml")
                            For Each n As XmlNode In spanElement.ChildNodes
                                replaceSpan.AppendChild(n)
                            Next

                            Dim labelText = replaceSpan.InnerText
                            labelText = If(labelText.Length > 50, labelText.Substring(0, 50) + "...", labelText)
                            Dim prm = result.Key.Parameters(0)
                            Dim controlLabelPrm = prm.InnerParameters.FirstOrDefault(Function(p) p.Name = "controlLabel")
                            If controlLabelPrm Is Nothing Then
                                controlLabelPrm = New PlainTextParameter() With {.Name = "controlLabel"}
                            End If
                            controlLabelPrm.SetValue(WebUtility.HtmlEncode(labelText))

                            replaceSpan.SetAttribute("id", String.Concat("S", result.Key.Identifier))
                            replaceSpan.SetAttribute("style", "background-color: #C7B8CE;")

                            Dim imported = replaceSpan.OwnerDocument.ImportNode(result.Value, True)

                            spanElement.ParentNode.InsertBefore(imported, spanElement)

                            spanElement.ParentNode.ReplaceChild(replaceSpan, spanElement)
                        End If

                        ret.Add(result)
                    End If

                    ret.Add(New KeyValuePair(Of InlineElement, XmlNode)(inline, slice))
                Next
            End If

            Return ret
        End Function

        Private Function GetByStrategyName(method As String, startNode As XmlNode, startPos As Integer, endNode As XmlNode, endPos As Integer) As IInlineTextSplitter
            Try
                Select Case method
                    Case "paragraph"
                        Return New ParagraphHtmlSplitter(startNode, startPos, endNode, endPos)
                    Case "word"
                        Return New WordHtmlSplitter(startNode, startPos, endNode, endPos)
                    Case "sentence"
                        Return New SentenceHtmlSplitter(startNode, startPos, endNode, endPos)
                    Case "free"
                        Return New SelectionHtmlSplitter(startNode, startPos, endNode, endPos)
                    Case Else
                        Debug.Assert(False, "Unknown method!")
                End Select
            Catch ex As ArgumentException
            End Try
            Return Nothing
        End Function

    End Class

End Namespace