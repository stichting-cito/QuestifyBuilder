Imports System.Collections.Concurrent
Imports System.Globalization
Imports System.Linq
Imports System.Xml
Imports System.Xml.Linq
Imports System.Xml.XPath
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.Logic.QTI.Converters.XhtmlConverter.QTI30
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI_Base
Imports Questify.Builder.Logic.QTI.Xsd.QTI30

Namespace QTI.Helpers.QTI30

    Public Class AspectScoringHelper

        Private _solution As Solution
        Private _packageCreator As IPackageCreator
        Private _aspects As New ConcurrentDictionary(Of String, Aspect)
        Private _scoringParameters As HashSet(Of ScoringParameter)

        Public Sub New(solution As Solution, packageCreator As IPackageCreator, scoringParameters As HashSet(Of ScoringParameter))
            _solution = solution
            _packageCreator = packageCreator
            _scoringParameters = scoringParameters
        End Sub

        Friend Sub UpdateDocumentBeforeProcessing(itemDocument As XmlDocument)
            If _solution Is Nothing _
               OrElse _solution.AspectReferenceSetCollection Is Nothing _
               OrElse _solution.AspectReferenceSetCollection.Count = 0 _
               OrElse _solution.AspectReferenceSetCollection(0).Items.Count = 0 _
                Then
                Return
            End If

            Dim aspectToAdd As XmlElement

            Dim rubricBlockElement As XmlElement = itemDocument.CreateElement("qti-rubric-block", itemDocument.DocumentElement.NamespaceURI)
            rubricBlockElement.Attributes.Append(itemDocument.CreateAttribute("id")).Value = "qtiScoringRubricBlock"
            rubricBlockElement.Attributes.Append(itemDocument.CreateAttribute("use")).Value = "instructions"
            rubricBlockElement.Attributes.Append(itemDocument.CreateAttribute("view")).Value = "scorer"
            rubricBlockElement.AppendChild(itemDocument.CreateElement("qti-content-body", itemDocument.DocumentElement.NamespaceURI))

            itemDocument.SelectSingleNode("//qti-item-body").AppendChild(rubricBlockElement)

            For Each aspectReference As AspectReference In _solution.AspectReferenceSetCollection(0).Items
                Dim aspect As Aspect = GetAspectByCode(aspectReference.SourceName)
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

                ConvertInlineElementAnchorsToHtml(tempDoc.DocumentElement, _packageCreator.GetAssessmentTestViewType)
                description = tempDoc.DocumentElement.InnerXml.ToString.Trim

                Using converter As New QTI30XhtmlConverter
                    converter.Initialise(String.Format(CultureInfo.InvariantCulture, "{0}_{1}", aspect.Identifier, _solution.AspectReferenceSetCollection(0).Items.IndexOf(aspectReference)))
                    description = converter.ConvertXhtmlToQti(description, False)
                End Using

                aspectToAdd = itemDocument.CreateElement("qti-rubric-block", itemDocument.DocumentElement.NamespaceURI)
                aspectToAdd.Attributes.Append(itemDocument.CreateAttribute("id")).Value = String.Format(CultureInfo.InvariantCulture, "qtiAspect{0}RubricBlock", GetCorrectAspectIdentifier(aspectReference.SourceName))
                aspectToAdd.Attributes.Append(itemDocument.CreateAttribute("use")).Value = "scoring"
                aspectToAdd.Attributes.Append(itemDocument.CreateAttribute("view")).Value = "scorer"
                aspectToAdd.Attributes.Append(itemDocument.CreateAttribute("data-dep-caption")).Value = aspect.Title

                If AspectScoreIsTranslated(aspectReference) Then
                    aspectToAdd.Attributes.Append(itemDocument.CreateAttribute("data-outcome-idref")).Value = GetAspectOutcomeIdentifierForTranslatedScoring(aspectReference)
                Else
                    aspectToAdd.Attributes.Append(itemDocument.CreateAttribute("data-outcome-idref")).Value = GetAspectOutcomeIdentifier(aspectReference)
                End If

                Dim contentElement As XmlElement = itemDocument.CreateElement("qti-content-body", itemDocument.DocumentElement.NamespaceURI)
                contentElement.InnerXml = description

                aspectToAdd.AppendChild(contentElement)

                itemDocument.SelectSingleNode("//qti-item-body").AppendChild(aspectToAdd)
            Next
        End Sub

        Private Function GetCorrectAspectIdentifier(aspectIdentifier As String) As String
            Return aspectIdentifier.Replace(" ", "_").Replace(".", "_")
        End Function

        Friend Function GetOutcomeDeclarations() As IEnumerable(Of OutcomeDeclarationType)
            Dim result = New List(Of OutcomeDeclarationType)
            For Each aspectReference As AspectReference In _solution.AspectReferenceSetCollection(0).Items
                Dim rawScoreOutcomeDeclarationType = GetRawScoreOutcomeDeclarationForAspect(aspectReference)
                If rawScoreOutcomeDeclarationType IsNot Nothing Then
                    result.Add(rawScoreOutcomeDeclarationType)
                End If

                Dim outcomeDeclarationType = GetOutcomeDeclarationForAspect(aspectReference)
                result.Add(outcomeDeclarationType)
            Next
            Return result
        End Function

        Friend Sub AddLookUpOutComeValues(navigator As XPathNavigator)
            For Each aspectReference As AspectReference In _solution.AspectReferenceSetCollection(0).Items
                If AspectScoreIsTranslated(aspectReference) Then
                    AddLookUpOutComeValue(aspectReference, navigator)
                End If
            Next
        End Sub

        Friend Sub AddOverallSumOfAspectOutcomes(identifier As String, navigator As XPathNavigator)
            If Not ShouldAddAspectResponseProcessing() Then
                Return
            End If

            Dim outcomeVariable As XElement = <qti-variable identifier="{0}"/>
            Dim variableProcessing As XElement = <root></root>

            For Each aspectReference As AspectReference In _solution.AspectReferenceSetCollection(0).Items
                variableProcessing.Add(XElement.Parse(String.Format(outcomeVariable.ToString, GetAspectOutcomeIdentifier(aspectReference))))
            Next

            If variableProcessing.HasElements() Then
                Dim outcome As XElement = <qti-set-outcome-value identifier=<%= identifier %>>
                                              <qti-sum>
                                                  <%= variableProcessing.Elements %>
                                              </qti-sum>
                                          </qti-set-outcome-value>

                ResponseProcessingHelper.AddResponseProcessingIfNeeded(navigator)

                navigator.AppendChild(outcome.ToString)
                navigator.MoveToRoot()
                navigator.MoveToFirstChild()
            End If
        End Sub

        Private Function ShouldAddAspectResponseProcessing() As Boolean
            If _scoringParameters IsNot Nothing Then
                Dim aspectScoringPrm = _scoringParameters.OfType(Of AspectScoringParameter).FirstOrDefault()
                Return aspectScoringPrm IsNot Nothing AndAlso Not aspectScoringPrm.SingleAspectScoringEditor
            End If
            Return False
        End Function

        Private Function GetOutcomeDeclarationForAspect(aspectReference As AspectReference) As OutcomeDeclarationType
            Dim interpolationTableForAspect = GetInterpolationTableIfAspectIsTranslated(aspectReference)

            Dim outcomeDeclarationType As OutcomeDeclarationType = New OutcomeDeclarationType With {
                .identifier = GetAspectOutcomeIdentifier(aspectReference),
                .basetype = OutcomeDeclarationTypeBasetype.integer,
                .basetypeSpecified = True,
                .cardinality = OutcomeDeclarationTypeCardinality.single,
                .view = {ViewEnumType.scorer},
                .normalminimum = GetMinScoreFromInterpolationTable(interpolationTableForAspect),
                .normalminimumSpecified = True,
                .normalmaximum = GetMaxScoreForAspectOutcomeDeclaration(interpolationTableForAspect, aspectReference),
                .normalmaximumSpecified = True
            }

            outcomeDeclarationType.Item = interpolationTableForAspect

            Return outcomeDeclarationType
        End Function

        Private Function GetRawScoreOutcomeDeclarationForAspect(aspectReference As AspectReference) As OutcomeDeclarationType
            If Not AspectScoreIsTranslated(aspectReference) Then
                Return Nothing
            End If

            Dim outcomeDeclarationType As OutcomeDeclarationType = New OutcomeDeclarationType With {
                .identifier = GetAspectOutcomeIdentifierForTranslatedScoring(aspectReference),
                .basetype = OutcomeDeclarationTypeBasetype.integer,
                .basetypeSpecified = True,
                .cardinality = OutcomeDeclarationTypeCardinality.single,
                .view = {ViewEnumType.scorer},
                .normalminimum = 0,
                .normalminimumSpecified = True,
                .normalmaximum = aspectReference.MaxScore,
                .normalmaximumSpecified = True
            }

            Return outcomeDeclarationType
        End Function

        Private Function GetInterpolationTableIfAspectIsTranslated(aspectReference As AspectReference) As InterpolationTableType
            If Not AspectScoreIsTranslated(aspectReference) Then
                Return Nothing
            End If

            Dim interpolationTableEntries = New List(Of InterpolationTableEntryType)

            Dim aspect = GetAspectByCode(aspectReference.SourceName)
            For Each st In aspect.AspectScoreTranslationTable
                Dim interpolationTableEntry = New InterpolationTableEntryType With {.sourcevalue = st.RawScore, .targetvalue = st.TranslatedScore.ToString()}
                interpolationTableEntries.Add(interpolationTableEntry)
            Next

            Return New InterpolationTableType() With {
                    .qtiinterpolationtableentry = interpolationTableEntries.ToArray()
                }
        End Function

        Private Function GetAspectByCode(aspectCode As String) As Aspect
            Dim aspect As Aspect
            If Not _aspects.TryGetValue(aspectCode, aspect) Then
                aspect = _packageCreator.GetAspectByCode(aspectCode)
                _aspects.TryAdd(aspectCode, aspect)
            End If
            Return aspect
        End Function

        Private Function GetMaxScoreForAspectOutcomeDeclaration(interpolationTable As InterpolationTableType, aspectRef As AspectReference) As Double
            If interpolationTable IsNot Nothing Then
                Return GetMaxScoreFromInterpolationTable(interpolationTable)
            Else
                Return aspectRef.MaxScore
            End If
        End Function

        Private Function GetMaxScoreFromInterpolationTable(interpolationTable As InterpolationTableType) As Double
            If interpolationTable Is Nothing Then
                Return 0
            End If
            Dim maxTargetValue = interpolationTable.qtiinterpolationtableentry.Max(Function(entry) GetInterpolationTargetValue(entry.targetvalue))
            Return maxTargetValue
        End Function

        Private Function GetMinScoreFromInterpolationTable(interpolationTable As InterpolationTableType) As Double
            If interpolationTable Is Nothing Then
                Return 0
            End If
            Dim minTargetValue = interpolationTable.qtiinterpolationtableentry.Min(Function(entry) GetInterpolationTargetValue(entry.targetvalue))
            Return minTargetValue
        End Function

        Private Function GetInterpolationTargetValue(stringValue As String) As Double
            Dim result As Double
            If Double.TryParse(stringValue, result) Then
                Return result
            End If
            Return 0
        End Function

        Private Function AspectScoreIsTranslated(aspectReference As AspectReference) As Boolean
            Dim aspect = GetAspectByCode(aspectReference.SourceName)
            Return aspect.AspectScoreIsTranslated()
        End Function

        Private Function GetAspectOutcomeIdentifier(aspectReference As AspectReference) As String
            Dim aspectIdentifier = GetCorrectAspectIdentifier(aspectReference.SourceName)
            Return $"qtiAspect{aspectIdentifier}OutcomeDeclaration"
        End Function

        Private Function GetAspectOutcomeIdentifierForTranslatedScoring(aspectReference As AspectReference) As String
            Return $"{GetAspectOutcomeIdentifier(aspectReference)}_{PackageCreatorConstants.RAW_SCORE}"
        End Function

        Private Sub AddLookUpOutComeValue(aspectReference As AspectReference, navigator As XPathNavigator)
            ResponseProcessingHelper.AddResponseProcessingIfNeeded(navigator)

            ChainHandlerHelper.AppendChild(navigator, "qti-lookup-outcome-value", True)
            ChainHandlerHelper.AddAttribute(navigator, "identifier", GetAspectOutcomeIdentifier(aspectReference))
            ChainHandlerHelper.AppendChild(navigator, "qti-variable", True)
            ChainHandlerHelper.AddAttribute(navigator, "identifier", GetAspectOutcomeIdentifierForTranslatedScoring(aspectReference))
        End Sub

        Private Sub ConvertInlineElementAnchorsToHtml(ByVal xml As Xml.XmlNode, ByVal target As String)
            Dim nsmgr As Xml.XmlNamespaceManager = New Xml.XmlNamespaceManager(xml.OwnerDocument.NameTable)
            nsmgr.AddNamespace("xhtml", "http://www.w3.org/1999/xhtml")
            nsmgr.AddNamespace("cito", "http://www.cito.nl/citotester")

            For Each node As Xml.XmlNode In xml.SelectNodes("//cito:InlineElement", nsmgr)

                Using reader As New IO.StringReader(node.OuterXml)
                    Dim inlineElement As InlineElement = DirectCast(SerializeHelper.XmlDeserializeFromReader(reader, GetType(InlineElement)), InlineElement)

                    Dim adapter As ItemLayoutAdapter = New ItemLayoutAdapter(inlineElement.LayoutTemplateSourceName, Nothing, AddressOf _packageCreator.ResourceNeeded)
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