Imports System.Linq
Imports System.Xml
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI22
Imports Questify.Builder.Logic.QTI.Xsd.QTI22_Final

Namespace QTI.Helpers.QTI22

    Public Class HottextScoringHelper
        Public Shared Sub UpdateDocumentBeforeProcessing(solution As Solution, itemDocument As XmlDocument, packageCreator As QTI22PackageCreator)
            CopyHottextSpanValuesToHottextInteractions(itemDocument)
        End Sub

        Private Shared Sub CopyHottextSpanValuesToHottextInteractions(ByVal itemDocument As XmlDocument)
            Dim xmlNamespaceManager As New XmlNamespaceManager(itemDocument.NameTable)
            xmlNamespaceManager.AddNamespace("qti", itemDocument.DocumentElement.NamespaceURI)

            Dim hottextIdentifierList As XmlNodeList = itemDocument.SelectNodes("//qti:hottext[@identifier]", xmlNamespaceManager)
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
                If DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement.LocalName.ToLower = "hottextinteraction" Then
                    Dim interactionList As XmlNodeList = DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement.SelectNodes("//hottext[@identifier]")
                    For Each interaction As XmlNode In interactionList
                        Dim defaultoutcomeDeclarationType As New OutcomeDeclarationType With {.identifier = $"{QTI22ScoringHelper.GetScoreId(False)}_{interaction.Attributes("identifier").Value}",
        .baseType = OutcomeDeclarationTypeBaseType.boolean, .baseTypeSpecified = True,
        .cardinality = OutcomeDeclarationTypeCardinality.single}
                        defaultoutcomeDeclarationType.defaultValue = New DefaultValueType
                        Dim defaultValue(0) As ValueType
                        defaultValue(0) = New ValueType
                        defaultValue(0).Value = "False"
                        defaultoutcomeDeclarationType.defaultValue.value = defaultValue.ToList()

                        list.Add(defaultoutcomeDeclarationType)
                    Next
                End If
            Next
            Return list
        End Function

    End Class
End Namespace