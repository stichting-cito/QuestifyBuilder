Imports System.IO
Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.Xml.Linq
Imports Questify.Builder.Logic.Service.Classes
Imports Questify.Builder.Logic.Service.HelperFunctions

Namespace PluginExtensibility.Html.Converters

    Friend Class HtmlConverter_HtmlToTextToSpeech
        Inherits HtmlConverterBase

        Protected Overrides Function DoConvert(html As String) As String
            Dim xDoc As XDocument
            Using strReader As New StringReader("<root>" + html + "</root>")
                xDoc = XDocument.Load(strReader, LoadOptions.PreserveWhitespace)
            End Using

            ConvertTTSPause(xDoc)
            ConvertTTSAlternative(xDoc)

            Return xDoc.Descendants("root").First().InnerXml()
        End Function

        Private Sub ConvertTTSAlternative(xDoc As XDocument)
            Dim alternativeTTSTags As IEnumerable(Of XElement) = xDoc.Descendants().Where(Function(d) d.Attributes.Any(Function(a) a.Name.LocalName.Equals("class", StringComparison.InvariantCultureIgnoreCase) AndAlso a.Value.Equals("TTSAlias", StringComparison.InvariantCultureIgnoreCase)))
            For Each tag In alternativeTTSTags
                Dim value As String = tag.Attribute("data-alias")?.Value
                If Not String.IsNullOrEmpty(value) Then
                    tag.Value = value
                    tag.SetAttributeValue("data-alias", Nothing)
                Else
                    tag.Value = "    "
                End If
            Next
        End Sub

        Private Sub ConvertTTSPause(xDoc As XDocument)
            Dim ttsPauseTags As IEnumerable(Of XElement) = xDoc.Descendants().Where(Function(d) d.Attributes.Any(Function(attr As XAttribute) attr.Name.LocalName.Equals("class", StringComparison.InvariantCultureIgnoreCase) AndAlso attr.Value.Contains("TTSPause")))
            For Each ttsPauseTag As XElement In ttsPauseTags
                Dim duration As Integer = GetDuration(ttsPauseTag)
                Dim durationText As String = GetDurationText(duration)

                ttsPauseTag.Value = durationText
            Next
        End Sub

        Private Function GetDuration(ttsPauseTag As XElement) As Integer
            Dim attr = ttsPauseTag.Attributes().FirstOrDefault(Function(a As XAttribute) a.Name.LocalName.Equals("class"))
            Dim pauseMatch = Regex.Match(attr.Value, "^([A-Za-z ]*)(PauseDuration_){1}([0-9]+)$")
            If pauseMatch.Success AndAlso pauseMatch.Groups.Count = 4 Then
                Dim duration As Integer
                If (Integer.TryParse(pauseMatch.Groups(3).Value, duration)) Then
                    Return duration
                End If

            End If

            Return 1000
        End Function

        Private Function GetDurationText(duration As Integer) As String
            Return CType(duration, PauseDuration).ToString()
        End Function
    End Class

End Namespace
