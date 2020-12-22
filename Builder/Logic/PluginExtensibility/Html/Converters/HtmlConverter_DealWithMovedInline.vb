Imports System.Xml.Linq
Imports System.IO
Imports System.Linq
Imports Questify.Builder.Logic.Service.HelperFunctions

Namespace PluginExtensibility.Html.Converters

    Friend MustInherit Class HtmlConverter_DealWithMovedInline
        Inherits HtmlConverterBase

        Private ReadOnly _behavior As IHtmlEditorBehaviour
        Private ReadOnly _attributeName As String
        Private W3 As XNamespace = "http://www.w3.org/1999/xhtml"

        Public Sub New(behavior As IHtmlEditorBehaviour, attributeName As String)
            _behavior = behavior
            _attributeName = attributeName
        End Sub

        Protected Overrides Function DoConvert(html As String) As String
            Dim doc = XDocument.Load(New StringReader("<root>" + html + "</root>"), LoadOptions.PreserveWhitespace)

            Dim inlineHtmlIds = GetIdsFromHtml(doc)
            Dim currentInlineIds = New HashSet(Of String)
            _behavior.InlineElements.ToList.ForEach(Sub(ie)
                                                        If ie.Value IsNot Nothing AndAlso ie.Value.Item2 = False Then currentInlineIds.Add(ie.Key)
                                                    End Sub)

            Dim missing = currentInlineIds.Except(inlineHtmlIds)
            If (missing.Count > 0) Then
                Dim otherside = inlineHtmlIds.Except(currentInlineIds)
                If (otherside.Count > 0) Then
                    Dim newInlineId = otherside.First()
                    Dim currentInlineId = missing.First()

                    Dim toSwitch = _behavior.InlineElements(currentInlineId)

                    _behavior.InlineElements.Remove(currentInlineId)
                    _behavior.InlineElements.Add(newInlineId, toSwitch)

                End If
            End If


            Dim tmp = doc.Descendants("root").First().InnerXml
            Return tmp
        End Function

        Private Function GetIdsFromHtml(ByVal doc As XDocument) As HashSet(Of String)
            Dim result = New HashSet(Of String)((From inline In doc.Descendants(W3 + "img").Where(Function(img) img.Attribute(_attributeName) IsNot Nothing) Select inline.Attribute("id").Value))

            doc.Descendants("img").Where(Function(img) img.Attribute(_attributeName) IsNot Nothing).ToList().ForEach(Sub(descendant)
                                                                                                                         Dim id = descendant.Attribute("id").Value
                                                                                                                         If Not result.Contains(id) Then result.Add(id)
                                                                                                                     End Sub)
            Return result
        End Function

    End Class

End Namespace