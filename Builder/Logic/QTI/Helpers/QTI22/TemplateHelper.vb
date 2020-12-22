Imports System.Xml
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Interfaces
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI_Base
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI22
Imports Questify.Builder.Logic.QTI.Xsd.QTI22_Final

Namespace QTI.Helpers.QTI22

    Public Class TemplateHelper
        Public Shared Function GetInterationTypeFromTemplate(itemDocument As XmlDocument, itemExtension As XmlDocument) As QTIMetadataTypeInteractionType()
            Dim returnList As New List(Of QTIMetadataTypeInteractionType)
            Dim xmlDocumentList As New List(Of XmlDocument)
            If itemDocument IsNot Nothing AndAlso itemDocument.DocumentElement IsNot Nothing Then
                xmlDocumentList.Add(itemDocument)
            End If
            If itemExtension IsNot Nothing AndAlso itemExtension.DocumentElement IsNot Nothing Then
                xmlDocumentList.Add(itemExtension)
            End If
            For Each xmlDocument As XmlDocument In xmlDocumentList
                For Each enumValueString As String In [Enum].GetNames(GetType(QTIMetadataTypeInteractionType))

                    Dim toolNodes As XmlNodeList = xmlDocument.DocumentElement.SelectNodes($"//{enumValueString}")
                    If toolNodes IsNot Nothing Then
                        For i As Integer = 1 To toolNodes.Count
                            returnList.Add(CType([Enum].Parse(GetType(QTIMetadataTypeInteractionType), enumValueString), QTIMetadataTypeInteractionType))
                        Next
                    End If
                Next
            Next
            Return returnList.ToArray
        End Function

        Public Function GetParsedTemplate(item As AssessmentItem, ByRef itemMetaDataCollection As MetaDataCollection, itemcode As String, packageCreator As QTI22PackageCreator, assessmentTestViewType As String) As String

            Dim parsedTemplate As String = packageCreator.ParseTemplate(itemcode, assessmentTestViewType, item)
            If Not packageCreator.TypeOfPackage = PackageCreatorConstants.PackageType.ItemPreview Then
                itemMetaDataCollection = packageCreator.GetMetadata(itemcode)
            End If
            parsedTemplate = ChainHandlerHelper.RemoveHtmlAttribute(parsedTemplate, "isinlineelement")
            parsedTemplate = ChainHandlerHelper.GetEveryThingBetweenTags(parsedTemplate, New List(Of String) From {"root"})
            Return parsedTemplate
        End Function

        Public Overridable Function GetQtiTemplate(parsedTemplate As String) As String
            Return GetQtiTemplate(parsedTemplate, New List(Of String)(New String() {"qtiextension", "qtiWithExtension", "qtiWithoutExtension", "qti3"}))
        End Function

        Protected Function GetQtiTemplate(parsedTemplate As String, tagsToRemove As List(Of String)) As String
            tagsToRemove.Add("styles")
            Return ChainHandlerHelper.RemoveAttribute(
                ChainHandlerHelper.RemoveAttribute(
                ChainHandlerHelper.RemoveNamespaces(
                ChainHandlerHelper.RemoveTags(
                ChainHandlerHelper.RemoveTags(parsedTemplate, tagsToRemove),
                New List(Of String)(New String() {"qtiWithExtension", "qtiWithoutExtension", "qti2"}), True),
                Nothing, True),
                "cito", True),
                "contenteditable", False)
        End Function

        Public Overridable Function GetQtiExtensions(parsedTemplate As String) As String
            Return GetQtiExtensions(parsedTemplate, New List(Of String) From {"qtiextension"})
        End Function

        Protected Function GetQtiExtensions(parsedTemplate As String, extensionTags As List(Of String), Optional tagsToRemove As List(Of String) = Nothing) As String
            Dim newListOfPrefixToDelete As New List(Of String)
            newListOfPrefixToDelete.Add("cito")
            Dim tagsToBeRemovedFirst = New List(Of String)
            If tagsToRemove IsNot Nothing Then
                tagsToBeRemovedFirst = tagsToRemove
            End If
            Return ChainHandlerHelper.RemoveAttribute(ChainHandlerHelper.RemoveAttribute(ChainHandlerHelper.RemoveNamespaces(ChainHandlerHelper.GetEveryThingBetweenTags(ChainHandlerHelper.RemoveTags(parsedTemplate, tagsToBeRemovedFirst), extensionTags), newListOfPrefixToDelete, False), "cito", True), "contenteditable", False)
        End Function

        Public Shared Function GetStyles(parsedTemplate As String) As List(Of String)
            Dim styles As New Dictionary(Of String, String)
            Dim xml As String = ChainHandlerHelper.GetEveryThingBetweenTags(parsedTemplate, New List(Of String) From {"styles"})
            If Not String.IsNullOrEmpty(xml) Then
                Dim tempXml As New XmlDocument
                tempXml.LoadXml($"<wrapper>{xml}</wrapper>")
                If tempXml IsNot Nothing AndAlso tempXml.DocumentElement IsNot Nothing Then
                    For Each XmlNode As XmlNode In tempXml.SelectNodes("//style")
                        If XmlNode.Attributes("classname") IsNot Nothing AndAlso
                           XmlNode.Attributes("attributename") IsNot Nothing AndAlso
                           XmlNode.Attributes("value") IsNot Nothing Then
                            Dim className As String = XmlNode.Attributes("classname").Value
                            Dim style As String = $"{vbTab}{XmlNode.Attributes("attributename").Value}: {XmlNode.Attributes("value").Value};{vbNewLine}"
                            If styles.ContainsKey(className) Then
                                styles(className) += style
                            Else
                                styles.Add(className, style)
                            End If
                        End If
                    Next
                End If
            End If
            Return GetClasses(styles)
        End Function

        Private Shared Function GetClasses(styles As Dictionary(Of String, String)) As List(Of String)
            Dim l As New List(Of String)
            For Each styleKey As String In styles.Keys
                l.Add(String.Format("{0}{{{1}{2}{3}{1}}}", styleKey, vbNewLine, vbTab, styles(styleKey)))
            Next
            Return l
        End Function

        Public Shared Sub ConvertInlineStylesToCss(xmlDoc As XmlDocument, ByRef css As String, converter As IXhtmlConverter)
            converter.ConvertStylesToCss(xmlDoc, css)
        End Sub

    End Class
End Namespace