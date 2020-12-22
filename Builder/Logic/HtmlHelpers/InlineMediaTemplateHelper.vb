Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.HtmlHelpers

Public Class InlineMediaTemplateHelper

    Public Const EMBEDDED_IMAGE_PARAMETERSET_NAME As String = "InlineImageParameterSet"

    Public Shared Function GetInlineMediaTemplate(inlineMediaTemplates As Dictionary(Of String, String), mediaType As String, htmlInlineTemplateNames As IHtmlInlineTemplateNames) As String
        Dim result As String = String.Empty
        If inlineMediaTemplates IsNot Nothing AndAlso inlineMediaTemplates.ContainsKey(mediaType) Then
            result = inlineMediaTemplates(mediaType)
        End If

        Select Case mediaType.ToLower
            Case "image"
                If htmlInlineTemplateNames.nrOneOptionForImage OrElse String.IsNullOrEmpty(result) Then result = htmlInlineTemplateNames.forImage
            Case "audio"
                If htmlInlineTemplateNames.nrOneOptionForAudio OrElse String.IsNullOrEmpty(result) Then result = htmlInlineTemplateNames.forAudio
            Case "video"
                If htmlInlineTemplateNames.nrOneOptionForVideo OrElse String.IsNullOrEmpty(result) Then result = htmlInlineTemplateNames.forVideo
        End Select

        Return result
    End Function

    Public Shared Function IsEmbeddedResourceInlineMediaTemplate(inlineMediaTemplate As String) As Boolean
        Return inlineMediaTemplate.Equals(EMBEDDED_IMAGE_PARAMETERSET_NAME, StringComparison.InvariantCultureIgnoreCase)
    End Function

    Public Shared Function GetParameterSetFromEmbeddedResource(inlineMediaTemplate As String) As ParameterSetCollection
        Dim result As ParameterSetCollection
        result = DirectCast(SerializeHelper.XmlDeserializeFromString(My.Resources.ResourceManager.GetObject(inlineMediaTemplate).ToString(), GetType(ParameterSetCollection)), ParameterSetCollection)
        For Each prmColl As ParameterCollection In result
            ExtractDesignerSettingsAndAttributeReferences(prmColl)
        Next
        Return result
    End Function

    Private Shared Sub ExtractDesignerSettingsAndAttributeReferences(ByVal parameterColl As ParameterCollection)
        If parameterColl IsNot Nothing Then
            For Each parameter As ParameterBase In parameterColl.InnerParameters

                Dim collectionParameter = TryCast(parameter, ICollectionParameter)
                If (collectionParameter IsNot Nothing) Then
                    ExtractDesignerSettingsAndAttributeReferences(collectionParameter.BluePrint)
                End If

                Dim scoringParameter = TryCast(parameter, ScoringParameter)
                If (scoringParameter IsNot Nothing) Then
                    Dim parameters = scoringParameter.GetParametersWithDesignerSettings()
                    If (parameters IsNot Nothing) Then
                        For Each subParameter As ParameterBase In parameters
                            ExtractAndAddFromNodes(subParameter)
                        Next
                    End If
                End If

                ExtractAndAddFromNodes(parameter)
            Next
        End If
    End Sub

    Private Shared Sub ExtractAndAddFromNodes(ByVal parameter As ParameterBase)
        Dim designerSettingSerializer As New Xml.Serialization.XmlSerializer(GetType(DesignerSetting))
        Dim attributeReferenceSerializer As New Xml.Serialization.XmlSerializer(GetType(AttributeReference))

        If parameter.Nodes IsNot Nothing Then
            For Each node As Xml.XmlNode In parameter.Nodes
                If node.Name = "designersetting" Then
                    Dim reader As New Xml.XmlNodeReader(node)
                    Dim setting As DesignerSetting = DirectCast(designerSettingSerializer.Deserialize(reader), DesignerSetting)
                    parameter.DesignerSettings.Add(setting)
                    reader.Close()
                ElseIf node.Name = "attributereference" Then
                    Dim reader As New Xml.XmlNodeReader(node)
                    Dim reference As AttributeReference = DirectCast(attributeReferenceSerializer.Deserialize(reader), AttributeReference)
                    parameter.AttributeReferences.Add(reference)
                    reader.Close()
                End If
            Next

            parameter.Nodes = Nothing
        End If
    End Sub

End Class
