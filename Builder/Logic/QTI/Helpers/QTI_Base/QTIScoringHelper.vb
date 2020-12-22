Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.Web
Imports System.Xml
Imports System.Xml.Linq
Imports System.Xml.XPath
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI_Base

Namespace QTI.Helpers.QTI_Base

    Public Class QTIScoringHelper

        Friend Shared Function GetNewHottextIdentifier(hottextIndex As Integer) As String
            Return String.Concat("HT_", AlphabeticIdentifierHelper.GetAlphabeticIdentifierForHottext(hottextIndex))
        End Function

        Friend Shared Function HasResponseIdentifiersWithDateTimeSubType(responseIdentifierAttribute As XmlNode, subType As String) As Boolean
            Return (Not DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement.Attributes Is Nothing _
                    AndAlso Not DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement.Attributes(subType) Is Nothing)
        End Function

        Friend Shared Function IsTimeFact(responseIdentifierAttribute As XmlNode, fact As KeyFact) As Boolean
            Return (HasResponseIdentifiersWithDateTimeSubType(responseIdentifierAttribute, "timeSubType") AndAlso GapTimeScoringHelper.FactContainsTimeValues(fact))
        End Function

        Friend Shared Function IsTimeValue(responseIdentifierAttribute As XmlNode, keyvalue As KeyValue) As Boolean
            Return (HasResponseIdentifiersWithDateTimeSubType(responseIdentifierAttribute, "timeSubType") AndAlso GapTimeScoringHelper.ValueContainsTimeValues(keyvalue.Values))
        End Function

        Friend Shared Function IsDateFact(responseIdentifierAttribute As XmlNode, fact As KeyFact) As Boolean
            Return (HasResponseIdentifiersWithDateTimeSubType(responseIdentifierAttribute, "dateSubType") AndAlso GapDateScoringHelper.FactContainsDateValues(fact))
        End Function

        Friend Shared Function IsDateValue(responseIdentifierAttribute As XmlNode, keyvalue As KeyValue) As Boolean
            Return (HasResponseIdentifiersWithDateTimeSubType(responseIdentifierAttribute, "dateSubType") AndAlso GapDateScoringHelper.ValueContainsDateValues(keyvalue.Values))
        End Function

        Friend Shared Sub RemoveAttributesFromInteractions(ByRef itemDocument As XmlDocument, interactionType As String, attrName As String)
            Dim xmlNamespaceManager As New XmlNamespaceManager(itemDocument.NameTable)
            xmlNamespaceManager.AddNamespace("qti", itemDocument.DocumentElement.NamespaceURI)

            Dim interactionList As XmlNodeList = itemDocument.SelectNodes($"//qti:{interactionType}[@{attrName}]", xmlNamespaceManager)

            For Each interaction As XmlNode In interactionList
                interaction.Attributes.RemoveNamedItem(attrName)
            Next
        End Sub

        Public Shared Function GetResponseId(responseIndex As Integer) As String
            Dim responseId As String

            If responseIndex > 1 Then
                responseId = String.Concat(PackageCreatorConstants.RESPONSE, responseIndex.ToString)
            Else
                responseId = PackageCreatorConstants.RESPONSE
            End If

            Return responseId
        End Function

        Public Shared Function GetScoreResponseId(responseIndex As Integer) As String
            Dim responseId As String

            If (responseIndex > 1) Then
                responseId = String.Concat(PackageCreatorConstants.SCORE_RESPONSE, responseIndex.ToString)
            Else
                responseId = PackageCreatorConstants.SCORE_RESPONSE
            End If

            Return responseId
        End Function

        Public Shared Function GetScoreFindingId(findingIndex As Integer) As String
            Dim findingId As String

            If findingIndex > 1 Then
                findingId = String.Concat(PackageCreatorConstants.SCORE_FINDING, findingIndex.ToString)
            Else
                findingId = PackageCreatorConstants.SCORE_FINDING
            End If

            Return findingId
        End Function

        Public Shared Function GetDecimalResponseId(responseIndex As Integer) As String
            Dim responseId As String

            If (responseIndex > 1) Then
                responseId = String.Concat(PackageCreatorConstants.DECIMAL_RESPONSE, responseIndex.ToString)
            Else
                responseId = PackageCreatorConstants.DECIMAL_RESPONSE
            End If

            Return responseId
        End Function

        Public Shared Function GetScoreId(shouldScoreBeTranslated As Boolean) As String
            Return If(shouldScoreBeTranslated, PackageCreatorConstants.RAW_SCORE, PackageCreatorConstants.SCORE)
        End Function

        Public Shared Function GetResponseId(responseIdentifier As String) As Integer
            Dim responseId As Integer = 1
            Dim identifier As String = responseIdentifier
            If Not String.IsNullOrEmpty(identifier) Then
                identifier = identifier.Replace(PackageCreatorConstants.RESPONSE, String.Empty)

                If Not String.IsNullOrEmpty(identifier) Then
                    responseId = Convert.ToInt32(identifier)
                End If
            End If

            Return responseId
        End Function

        Public Shared Function GetConceptScoringId(conceptScoringIndex As Integer, conceptName As String) As String
            Dim conceptId As String

            If (conceptScoringIndex > 1) Then
                conceptId = $"{PackageCreatorConstants.CONCEPT_RESPONSE}{conceptScoringIndex.ToString()}_{conceptName}"
            Else
                conceptId = $"{PackageCreatorConstants.CONCEPT_RESPONSE}_{conceptName}"
            End If

            Return conceptId
        End Function

        Friend Shared Function ShouldScoreBeTranslated(translationTable As ItemScoreTranslationTable) As Boolean
            Dim returnValue As Boolean = False

            If translationTable IsNot Nothing Then
                returnValue = translationTable.Where(Function(element) element.RawScore <> element.TranslatedScore).Count > 0
            End If

            Return returnValue
        End Function

        Public Shared Function GetItemMaxScore(translatedScore As Nullable(Of Decimal), itemWeightInTest As Double) As Double
            Dim returnValue As Double = 0

            If translatedScore.HasValue Then
                returnValue = translatedScore.Value * itemWeightInTest
            End If

            Return returnValue
        End Function

        Friend Shared Function GetNumberOfResponses(ByVal solution As Solution) As Integer
            Dim baseValueIndex As Integer = 0

            For Each finding As KeyFinding In solution.Findings
                If finding IsNot Nothing AndAlso Not finding.Facts.Count = 0 Then
                    baseValueIndex = GetNumberOfResponses(finding)

                    Exit For
                End If
            Next

            Return baseValueIndex
        End Function

        Friend Shared Function GetNumberOfResponses(ByVal finding As KeyFinding) As Integer
            Dim baseValueIndex As Integer = 0

            If finding IsNot Nothing Then
                For Each keyFact As KeyFact In finding.Facts
                    For Each keyValue As KeyValue In keyFact.Values
                        For Each baseValue As BaseValue In keyValue.Values
                            baseValueIndex += 1
                        Next
                    Next
                Next
            End If

            Return baseValueIndex
        End Function

        Friend Shared Function GetScoringMethod(ByVal solution As Solution) As EnumScoringMethod
            Dim scoringMethod As EnumScoringMethod = EnumScoringMethod.None

            If GetNrOfKeyFindingsWithScoringMethod(solution) > 0 Then
                scoringMethod = solution.Findings.Where(Function(x) Not x.Method = EnumScoringMethod.None).FirstOrDefault.Method
            End If

            Return scoringMethod
        End Function

        Friend Shared Function GetNrOfKeyFindingsWithScoringMethod(ByVal solution As Solution) As Integer
            Dim returnValue As Integer = 0

            If solution IsNot Nothing AndAlso solution.Findings IsNot Nothing AndAlso Not solution.Findings.Count = 0 Then
                returnValue = solution.Findings.Where(Function(x) Not x.Method = EnumScoringMethod.None).Count
            End If

            Return returnValue
        End Function

        Public Shared Function KeyValueContainsDecimal(ByVal keyValueCollection As KeyValueCollection) As Boolean
            Dim returnValue As Boolean = False

            For Each baseValue As BaseValue In keyValueCollection
                Select Case baseValue.GetType
                    Case GetType(DecimalRangeValue)
                        returnValue = True
                        Exit For
                    Case GetType(DecimalValue)
                        returnValue = True
                        Exit For
                    Case GetType(DecimalComparisonValue)
                        returnValue = True
                        Exit For
                End Select
            Next

            Return returnValue
        End Function

        Public Shared Function KeyValueContainsInteger(ByVal keyValueCollection As KeyValueCollection) As Boolean
            Dim returnValue As Boolean = False

            For Each baseValue As BaseValue In keyValueCollection
                Select Case baseValue.GetType
                    Case GetType(IntegerRangeValue)
                        returnValue = True
                        Exit For
                    Case GetType(IntegerValue)
                        returnValue = True
                        Exit For
                    Case GetType(IntegerComparisonValue)
                        returnValue = True
                        Exit For
                End Select
            Next

            Return returnValue
        End Function

        Public Shared Function KeyValueContainsString(ByVal keyValueCollection As KeyValueCollection) As Boolean
            Dim returnValue As Boolean = False

            For Each baseValue As BaseValue In keyValueCollection
                Select Case baseValue.GetType
                    Case GetType(StringValue)
                        returnValue = True
                        Exit For
                    Case GetType(StringComparisonValue)
                        returnValue = True
                        Exit For
                End Select
            Next

            Return returnValue
        End Function


        Public Shared Function BaseValueIsOfTypeNumericComparison(ByVal baseValue As BaseValue) As Boolean
            Dim returnValue As Boolean = False

            Select Case baseValue.GetType
                Case GetType(DecimalComparisonValue)
                    returnValue = True
                Case GetType(IntegerComparisonValue)
                    returnValue = True
            End Select

            Return returnValue
        End Function


        Public Shared Function KeyValueContainsMathML(ByVal keyValueCollection As KeyValueCollection) As Boolean
            Dim returnValue As Boolean = False

            For Each baseValue As BaseValue In keyValueCollection
                If BaseValueContainsMathML(baseValue) Then Return True
            Next

            Return returnValue
        End Function

        Friend Shared Function BaseValueContainsMathML(ByVal baseValue As BaseValue) As Boolean
            If Not baseValue Is Nothing Then
                If baseValue.ToString.StartsWith("<math") Then
                    Return True
                ElseIf TypeOf baseValue Is StringComparisonValue Then
                    Dim prefix As String = DirectCast(baseValue, StringComparisonValue).ComparisonPrefix(DirectCast(baseValue, StringComparisonValue).TypeOfComparison)

                    If baseValue.ToString.StartsWith(String.Concat(prefix, "<math")) Then Return True
                End If
            End If

            Return False
        End Function

        Friend Shared Function BaseValueContainsMathML(ByVal baseValue As String) As Boolean
            If Not String.IsNullOrEmpty(baseValue) Then
                If (baseValue.StartsWith("<math") OrElse HttpUtility.HtmlDecode(baseValue).StartsWith("<math")) Then
                    Return True
                Else
                    Dim prefixValue As New StringComparisonValue()
                    Dim prefix As String = prefixValue.ComparisonPrefix(TypedComparisonValue(Of String).ComparisonType.Equivalent)

                    If baseValue.ToString.StartsWith(String.Concat(prefix, "<math")) OrElse HttpUtility.HtmlDecode(baseValue).StartsWith(String.Concat(prefix, "<math")) Then Return True

                    prefix = prefixValue.ComparisonPrefix(TypedComparisonValue(Of String).ComparisonType.NotEquals)

                    If baseValue.ToString.StartsWith(String.Concat(prefix, "<math")) OrElse HttpUtility.HtmlDecode(baseValue).StartsWith(String.Concat(prefix, "<math")) Then Return True
                End If
            End If

            Return False
        End Function


        Public Shared Function ValueContainsPreProcessingRules(ByVal keyValue As KeyValue) As Boolean
            Return (keyValue.PreProcessingRules.Count > 0)
        End Function

        Friend Shared Function HasManualScoring(solution As Solution) As Boolean
            Dim returnValue = False

            If solution IsNot Nothing _
               AndAlso solution.AspectReferenceSetCollection IsNot Nothing _
               AndAlso Not solution.AspectReferenceSetCollection.Count = 0 _
               AndAlso Not solution.AspectReferenceSetCollection(0).Items.Count = 0 _
                Then
                returnValue = True
            End If

            Return returnValue
        End Function

        Friend Shared Function HasAutomaticScoring(solution As Solution) As Boolean
            Dim returnValue = False

            If solution IsNot Nothing _
               AndAlso solution.Findings IsNot Nothing _
               AndAlso Not solution.Findings.Count = 0 _
               AndAlso Not (solution.Findings.Where(Function(f) KeyFactsInFindingsAreEmpty(f) = True OrElse FindingHasFacts(f)).Count = 0) _
                Then
                returnValue = True
            End If

            Return returnValue
        End Function

        Friend Shared Function KeyFactsInFindingsAreEmpty(finding As KeyFinding) As Boolean
            Return finding.KeyFactsets IsNot Nothing _
                   AndAlso Not finding.KeyFactsets.Count = 0 _
                   AndAlso Not finding.KeyFactsets.Where(Function(ks) KeyFactHasFacts(ks) = True).Count = 0
        End Function

        Friend Shared Function KeyFactHasFacts(keyFactSet As KeyFactSet) As Boolean
            Return keyFactSet.Facts IsNot Nothing AndAlso Not keyFactSet.Facts.Count = 0
        End Function

        Friend Shared Function FindingHasFacts(finding As KeyFinding) As Boolean
            Return finding IsNot Nothing _
                   AndAlso finding.Facts IsNot Nothing _
                   AndAlso Not finding.Facts.Count = 0
        End Function


        Public Shared Function EncodeAsQTIIdentifier(identifier As String, replacement As String) As String
            Dim id = identifier.Trim()
            Dim encodedIdentifier = ReplaceForbiddenCharacters(id, replacement)

            If encodedIdentifier.StartsWith("-") Then
                encodedIdentifier = "_" + encodedIdentifier.Substring(1)
            End If
            If IsFirstCharacterNumeric(encodedIdentifier) Then
                encodedIdentifier = "_" + encodedIdentifier
            End If

            Return encodedIdentifier
        End Function

        Public Shared Function EncodeAsQTIIdentifier(identifier As String) As String
            Return EncodeAsQTIIdentifier(identifier, String.Empty)
        End Function

        Private Shared Function ReplaceForbiddenCharacters(text As String, replacement As String) As String
            Dim regEx As New Regex("[\s!""#$%&'()*\+/:.,;<=>?@\[\\\]\^`{|}~]+")
            Dim replacementString As String = "_"

            If Not String.IsNullOrEmpty(replacement) Then
                replacementString = replacement
            End If

            Return regEx.Replace(text, replacementString)
        End Function

        Private Shared Function IsFirstCharacterNumeric(text As String) As Boolean
            Dim regEx As New Regex("^[0-9]")

            Return regEx.IsMatch(text)
        End Function

    End Class
End Namespace