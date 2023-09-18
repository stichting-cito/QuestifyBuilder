Imports System.Linq
Imports System.Xml
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI30
Imports Questify.Builder.Logic.QTI.Xsd.QTI30

Namespace QTI.Helpers.QTI30

    Public Class HottextScoringHelper
        Public Shared Sub UpdateDocumentBeforeProcessing(solution As Solution, itemDocument As XmlDocument, packageCreator As PackageCreator)
            CopyHottextSpanValuesToHottextInteractions(itemDocument)
        End Sub

        Private Shared Sub CopyHottextSpanValuesToHottextInteractions(ByVal itemDocument As XmlDocument)
            Dim xmlNamespaceManager As New XmlNamespaceManager(itemDocument.NameTable)
            xmlNamespaceManager.AddNamespace("qti", itemDocument.DocumentElement.NamespaceURI)

            Dim hottextIdentifierList As XmlNodeList = itemDocument.SelectNodes("//qti:qti-hottext[@identifier]", xmlNamespaceManager)
            Dim hottextIndex As Integer = 1

            For Each hottextIdentifier As XmlNode In hottextIdentifierList
                Dim attribute As XmlAttribute = DirectCast(hottextIdentifier.Attributes("identifier"), XmlAttribute)
                Dim spanNode As XmlNode = itemDocument.SelectSingleNode($"//qti:span[@id=""{String.Concat("S", attribute.Value)}""]", xmlNamespaceManager)
                If spanNode IsNot Nothing Then
                    If Not String.IsNullOrEmpty(spanNode.InnerXml) Then hottextIdentifier.InnerXml = spanNode.InnerXml
                    spanNode.ParentNode.RemoveChild(spanNode)
                End If
                hottextIndex += 1
            Next
        End Sub

        Public Shared Function GetHottextValueForRelatedInputField(ByVal itemDocument As XmlDocument, ByVal identifier As String) As String
            Dim result As String = String.Empty
            Dim xmlNamespaceManager As New XmlNamespaceManager(itemDocument.NameTable)
            xmlNamespaceManager.AddNamespace("qti", itemDocument.DocumentElement.NamespaceURI)

            Dim hottextNode As XmlNode = itemDocument.SelectSingleNode($"//qti:span[@id=""{String.Concat("S", identifier)}""]", xmlNamespaceManager)
            If hottextNode IsNot Nothing AndAlso Not String.IsNullOrEmpty(hottextNode.InnerText) Then
                result = hottextNode.InnerText
            End If
            Return result
        End Function

        Public Shared Function GetOutcomeDeclarationsForHottextInteractions(ByVal responseIdentifierAttributeList As XmlNodeList) As List(Of OutcomeDeclarationType)
            Dim list As List(Of OutcomeDeclarationType) = New List(Of OutcomeDeclarationType)
            For Each responseIdentifierAttribute As XmlNode In responseIdentifierAttributeList
                If DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement.LocalName.ToLower = "qti-hottext-interaction" Then
                    Dim interactionList As XmlNodeList = DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement.SelectNodes("//qti-hottext[@identifier]")
                    For Each interaction As XmlNode In interactionList
                        Dim defaultoutcomeDeclarationType As New OutcomeDeclarationType With {.identifier = $"{QTIScoringHelper.GetScoreId(False)}_{interaction.Attributes("identifier").Value}",
        .basetype = OutcomeDeclarationTypeBasetype.boolean, .basetypeSpecified = True,
        .cardinality = OutcomeDeclarationTypeCardinality.single}
                        defaultoutcomeDeclarationType.qtidefaultvalue = New DefaultValueType
                        Dim defaultValue = New ValueType With {.Value = "False"}
                        defaultoutcomeDeclarationType.qtidefaultvalue.qtivalue = {defaultValue}

                        list.Add(defaultoutcomeDeclarationType)
                    Next
                End If
            Next
            Return list
        End Function

    End Class
End Namespace