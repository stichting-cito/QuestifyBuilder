Imports System.IO
Imports System.Linq
Imports System.Xml.Linq
Imports Questify.Builder.Logic.Service.HelperFunctions

Namespace PluginExtensibility.Html.Converters

    Friend Class HtmlConverter_TextToSpeechToHtml
        Inherits HtmlConverterBase

        Protected Overrides Function DoConvert(html As String) As String
            Dim xDoc As XDocument
            Using strReader As New StringReader("<root>" + html + "</root>")
                xDoc = XDocument.Load(strReader, LoadOptions.PreserveWhitespace)
            End Using

            ConvertPauseTags(xDoc)
            ConvertAlternativeTTS(xDoc)

            Return xDoc.Descendants("root").First().InnerXml()
        End Function

        Private Sub ConvertPauseTags(xDoc As XDocument)
            Dim ttsPauseTags As IEnumerable(Of XElement) = xDoc.Descendants().Where(Function(d) d.Attributes.Any(Function(attr As XAttribute) attr.Name.LocalName.Equals("class", StringComparison.InvariantCultureIgnoreCase) AndAlso attr.Value.Contains("TTSPause")))
            For Each ttsPauseTag As XElement In ttsPauseTags
                ttsPauseTag.Value = String.Empty
            Next
        End Sub

        Private Sub ConvertAlternativeTTS(xDoc As XDocument)
            Dim alternativeTTSTags As IEnumerable(Of XElement) = xDoc.Descendants().Where(Function(d) d.Attributes.Any(Function(a) a.Name.LocalName.Equals("class", StringComparison.InvariantCultureIgnoreCase) AndAlso a.Value.Equals("TTSAlias", StringComparison.InvariantCultureIgnoreCase)))
            For Each tag In alternativeTTSTags
                If Not String.IsNullOrEmpty(tag.InnerXml) Then
                    Dim dataAliasAttribute = tag.Attributes.FirstOrDefault(Function(attr) attr.Name.LocalName.Equals("data-alias", StringComparison.InvariantCultureIgnoreCase))
                    If (dataAliasAttribute Is Nothing) Then
                        dataAliasAttribute = New XAttribute("data-alias", tag.InnerXml)
                        tag.Add(dataAliasAttribute)
                    Else
                        dataAliasAttribute.Value = tag.InnerXml()
                    End If

                    tag.Value = String.Empty
                End If
            Next
        End Sub
    End Class

End Namespace