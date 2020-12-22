Imports System.Globalization
Imports System.Linq
Imports System.Xml
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Converters.XhtmlConverter.QTI_Base
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI22

Namespace QTI.Helpers.QTI22

    Public Class AspectScoringHelper

        Public Shared Sub UpdateExtensionDocument(solution As Solution, itemExtensionDocument As XmlDocument, packageCreator As QTI22PackageCreator)
            If solution Is Nothing _
               OrElse solution.AspectReferenceSetCollection Is Nothing _
               OrElse solution.AspectReferenceSetCollection.Count = 0 _
               OrElse solution.AspectReferenceSetCollection(0).Items.Count = 0 Then _
                Return

            Dim depNamespace As String = itemExtensionDocument.DocumentElement.Attributes("xmlns:dep").Value
            Dim xmlNamespaceManager As New XmlNamespaceManager(itemExtensionDocument.NameTable)
            xmlNamespaceManager.AddNamespace("dep", depNamespace)

            Dim depItemNode As XmlNode = itemExtensionDocument.SelectSingleNode("/dep:depItem", xmlNamespaceManager)
            If depItemNode Is Nothing Then Throw New XmlException("AspectScoringHelper.UpdateExtensionDocument: failed to find depItem element")

            Dim scoringInfoElement As XmlElement = itemExtensionDocument.CreateElement("dep", "scoringInfo", depNamespace)
            Dim instructionElement As XmlElement = itemExtensionDocument.CreateElement("dep", "instruction", depNamespace)
            instructionElement.Attributes.Append(itemExtensionDocument.CreateAttribute("qtiRubricBlockIdentifierRef")).Value = "qtiScoringRubricBlock"
            scoringInfoElement.AppendChild(instructionElement)

            If solution.AspectReferenceSetCollection IsNot Nothing AndAlso Not solution.AspectReferenceSetCollection.Count = 0 Then
                For Each aspectReference As AspectReference In solution.AspectReferenceSetCollection(0).Items
                    Dim aspect As Aspect = packageCreator.GetAspectByCode(aspectReference.SourceName)
                    Dim aspectElement As XmlElement = CreateAspectElement(itemExtensionDocument, depNamespace, aspect.Title, aspectReference.SourceName)
                    scoringInfoElement.AppendChild(aspectElement)
                Next
            End If

            itemExtensionDocument.DocumentElement.InsertAfter(scoringInfoElement, depItemNode.LastChild)
        End Sub

        Private Shared Function CreateAspectElement(itemExtensionDocument As XmlDocument, depNamespace As String, title As String, sourceName As String) As XmlElement
            Dim aspectElement As XmlElement = itemExtensionDocument.CreateElement("dep", "aspect", depNamespace)
            aspectElement.Attributes.Append(itemExtensionDocument.CreateAttribute("qtiOutcomeDeclarationIdentifierRef")).Value = String.Format(CultureInfo.InvariantCulture, "qtiAspect{0}OutcomeDeclaration", sourceName)
            aspectElement.Attributes.Append(itemExtensionDocument.CreateAttribute("qtiRubricBlockIdentifierRef")).Value = String.Format(CultureInfo.InvariantCulture, "qtiAspect{0}RubricBlock", sourceName)
            Dim captionElement As XmlElement = itemExtensionDocument.CreateElement("dep", "caption", depNamespace)
            captionElement.InnerText = title
            aspectElement.AppendChild(captionElement)
            Return aspectElement
        End Function

        Public Shared Sub UpdateDocumentBeforeProcessing(solution As Solution, itemDocument As XmlDocument, packageCreator As QTI22PackageCreator)
            If solution Is Nothing _
               OrElse solution.AspectReferenceSetCollection Is Nothing _
               OrElse solution.AspectReferenceSetCollection.Count = 0 _
               OrElse solution.AspectReferenceSetCollection(0).Items.Count = 0 _
                Then
                Return
            End If

            Dim aspectToAdd As XmlElement

            Dim rubricBlockElement As XmlElement = itemDocument.CreateElement("rubricBlock", itemDocument.DocumentElement.NamespaceURI)
            rubricBlockElement.Attributes.Append(itemDocument.CreateAttribute("id")).Value = "qtiScoringRubricBlock"
            rubricBlockElement.Attributes.Append(itemDocument.CreateAttribute("view")).Value = "scorer"
            itemDocument.SelectSingleNode("//itemBody").AppendChild(rubricBlockElement)

            For Each aspectReference As AspectReference In solution.AspectReferenceSetCollection(0).Items
                Dim aspect As Aspect = packageCreator.GetAspectByCode(aspectReference.SourceName)
                Dim description As String = String.Empty

                If Not String.IsNullOrEmpty(aspect.Description) Then
                    Dim xmlDoc As New XmlDocument()
                    xmlDoc.LoadXml(String.Format(CultureInfo.InvariantCulture, "<wrapper>{0}</wrapper>", Trim(aspect.Description)))

                    Dim nodes As XmlNode() = New List(Of XmlNode)(xmlDoc.DocumentElement.OfType(Of XmlNode)).ToArray

                    If Not Cito.Tester.Common.TemplateHelper.IsXHtmlParameterEmpty(nodes) Then
                        description = aspect.Description
                    End If
                End If

                Dim tempDoc As New XmlDocument
                tempDoc.PreserveWhitespace = True

                If Not String.IsNullOrEmpty(aspectReference.Description) Then
                    description = String.Concat(description, "<br />", aspectReference.Description.Replace("<p>", "<div>").Replace("<p ", "<div ").Replace("</p>", "</div>"))
                End If

                tempDoc.LoadXml(String.Format(CultureInfo.InvariantCulture, "<wrapper>{0}</wrapper>", description))

                ConvertInlineElementAnchorsToHtml(tempDoc.DocumentElement, packageCreator.GetAssessmentTestViewType, packageCreator)
                description = tempDoc.DocumentElement.InnerXml.ToString.Trim

                Using converter As New QTIXhtmlConverter
                    converter.Initialise(String.Format(CultureInfo.InvariantCulture, "{0}_{1}", aspect.Identifier, solution.AspectReferenceSetCollection(0).Items.IndexOf(aspectReference)))
                    description = converter.ConvertXhtmlToQti(description, False)
                End Using

                aspectToAdd = itemDocument.CreateElement("rubricBlock", itemDocument.DocumentElement.NamespaceURI)
                aspectToAdd.Attributes.Append(itemDocument.CreateAttribute("id")).Value = String.Format(CultureInfo.InvariantCulture, "qtiAspect{0}RubricBlock", aspectReference.SourceName)
                aspectToAdd.Attributes.Append(itemDocument.CreateAttribute("view")).Value = "scorer"
                aspectToAdd.InnerXml = description

                itemDocument.SelectSingleNode("//itemBody").AppendChild(aspectToAdd)
            Next
        End Sub

        Private Shared Sub ConvertInlineElementAnchorsToHtml(ByVal xml As Xml.XmlNode, ByVal target As String, packageCreator As QTI22PackageCreator)
            Dim nsmgr As Xml.XmlNamespaceManager = New Xml.XmlNamespaceManager(xml.OwnerDocument.NameTable)
            nsmgr.AddNamespace("xhtml", "http://www.w3.org/1999/xhtml")
            nsmgr.AddNamespace("cito", "http://www.cito.nl/citotester")

            For Each node As Xml.XmlNode In xml.SelectNodes("//cito:InlineElement", nsmgr)

                Using reader As New IO.StringReader(node.OuterXml)
                    Dim inlineElement As InlineElement = DirectCast(SerializeHelper.XmlDeserializeFromReader(reader, GetType(InlineElement)), InlineElement)

                    Dim adapter As ItemLayoutAdapter = New ItemLayoutAdapter(inlineElement.LayoutTemplateSourceName, Nothing, AddressOf packageCreator.ResourceNeeded)
                    Dim xHtmlDocument As XHtmlDocument = adapter.ParseTemplate(target, inlineElement.Parameters, False)

                    Dim newNodeList As Xml.XmlNodeList = xHtmlDocument.SelectNodes("html/*")

                    If newNodeList IsNot Nothing AndAlso newNodeList.Count > 0 Then
                        For nodeIndex As Integer = newNodeList.Count - 1 To 0 Step -1
                            node.ParentNode.InsertAfter(xml.OwnerDocument.ImportNode(newNodeList(nodeIndex), True), node)
                        Next

                        node.ParentNode.RemoveChild(node)
                    End If
                End Using
            Next
        End Sub
    End Class
End Namespace