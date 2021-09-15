
Imports Cito.Tester.ContentModel
Imports System.Xml.Serialization
Imports Cito.Tester.Common
Imports System.Xml.Linq
Imports Questify.Builder.Logic.ContentModel.Scoring

<TestClass>
Public Class ScoringPropertiesCalculatorTests
    
    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub ResponseCount_Equals_NumberOfKeyFacts()
        'Arrange
        Dim solutionXml = <solution>
                              <keyFindings>
                                  <keyFinding id="mc" scoringMethod="Dichotomous">
                                      <keyFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                      <keyFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                  </keyFinding>
                              </keyFindings>
                          </solution>

        Dim solution = SolutionFromXml(solutionXml)

        'Act
        Dim responseCount = ScoringPropertiesCalculator.GetResponseCount(solution)

        'Assert
        Assert.AreEqual(2, responseCount)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub ResponseCount_IsNull_IfNoFactsAvailable()
        'Arrange
        Dim solutionXml = <solution>
                              <keyFindings>
                                  <keyFinding id="mc" scoringMethod="Dichotomous">
                                  </keyFinding>
                              </keyFindings>
                          </solution>

        Dim solution = SolutionFromXml(solutionXml)

        'Act
        Dim responseCount = ScoringPropertiesCalculator.GetResponseCount(solution)

        'Assert
        Assert.IsNull(responseCount)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub KeyValueString_Joined()
        'Arrange
        Dim solutionXml = <solution>
                              <keyFindings>
                                  <keyFinding id="mc" scoringMethod="Dichotomous">
                                      <keyFact id="24834555-98fa-45ae-b5c0-245085594ade" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                          <keyValue domain="24834555-98fa-45ae-b5c0-245085594ade" occur="1">
                                              <stringValue>
                                                  <typedValue>A</typedValue>
                                              </stringValue>
                                          </keyValue>
                                      </keyFact>
                                      <keyFact id="e4c4fe8a-c27a-41bb-96ae-eb753e505885" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                          <keyValue domain="e4c4fe8a-c27a-41bb-96ae-eb753e505885" occur="1">
                                              <decimalValue>
                                                  <typedValue>123.45</typedValue>
                                              </decimalValue>
                                              <decimalRangeValue rangeEnd="100.00" rangeStart="1.00"/>
                                          </keyValue>
                                      </keyFact>
                                  </keyFinding>
                                  <keyFinding id="gapMatchController" scoringMethod="Dichotomous">
                                      <keyFact id="GAP_018f2965-986f-49ac-8cea-92862e5cdc12" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                          <keyValue domain="GAP_018f2965-986f-49ac-8cea-92862e5cdc12" occur="1">
                                              <stringValue>
                                                  <typedValue>B</typedValue>
                                              </stringValue>
                                          </keyValue>
                                      </keyFact>
                                  </keyFinding>
                              </keyFindings>
                          </solution>

        Dim solution = SolutionFromXml(solutionXml)

        'Act
        Dim keyValueString = ScoringPropertiesCalculator.GetKeyValuesAsString(solution, 0)

        'Assert
        Assert.AreEqual("A&123.45#1.00-100.00|B", keyValueString)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub RawScore_Dichotomous()
        'Arrange
        Dim solutionXml = <solution>
                              <keyFindings>
                                  <keyFinding id="mc" scoringMethod="Dichotomous">
                                      <keyFact id="24834555-98fa-45ae-b5c0-245085594ade" score="1" xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                      <keyFact id="e4c4fe8a-c27a-41bb-96ae-eb753e505885" score="2" xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                  </keyFinding>
                              </keyFindings>
                          </solution>

        Dim solution = SolutionFromXml(solutionXml)

        'Act
        Dim rawScore = ScoringPropertiesCalculator.GetRawScore(solution)

        'Assert
        Assert.AreEqual(1, rawScore)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub RawScore_Dichotomous_WithAspects()
        'Arrange
        Dim solutionXml = <solution>
                              <keyFindings>
                                  <keyFinding id="mc" scoringMethod="Dichotomous">
                                      <keyFact id="24834555-98fa-45ae-b5c0-245085594ade" score="1" xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                      <keyFact id="e4c4fe8a-c27a-41bb-96ae-eb753e505885" score="2" xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                  </keyFinding>
                              </keyFindings>
                              <aspectReferences>
                                  <aspectReferenceSet>
                                      <aspectReference maxScore="2"/>
                                      <aspectReference maxScore="3"/>
                                  </aspectReferenceSet>
                              </aspectReferences>
                          </solution>

        Dim solution = SolutionFromXml(solutionXml)

        'Act
        Dim rawScore = ScoringPropertiesCalculator.GetRawScore(solution)

        'Assert
        Assert.AreEqual(6, rawScore)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub RawScore_Polytomous()
        'Arrange
        Dim solutionXml = <solution>
                              <keyFindings>
                                  <keyFinding id="mc" scoringMethod="Polytomous">
                                      <keyFact id="24834555-98fa-45ae-b5c0-245085594ade" score="1" xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                      <keyFact id="e4c4fe8a-c27a-41bb-96ae-eb753e505885" score="2" xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                  </keyFinding>
                              </keyFindings>
                          </solution>
        Dim solution = SolutionFromXml(solutionXml)

        'Act
        Dim rawScore = ScoringPropertiesCalculator.GetRawScore(solution)

        'Assert
        Assert.AreEqual(3, rawScore)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub RawScore_Polytomous_WithAspect()
        'Arrange
        Dim solutionXml = <solution>
                              <keyFindings>
                                  <keyFinding id="mc" scoringMethod="Polytomous">
                                      <keyFact id="24834555-98fa-45ae-b5c0-245085594ade" score="1" xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                      <keyFact id="e4c4fe8a-c27a-41bb-96ae-eb753e505885" score="2" xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                  </keyFinding>
                              </keyFindings>
                              <aspectReferences>
                                  <aspectReferenceSet>
                                      <aspectReference maxScore="2"/>
                                      <aspectReference maxScore="3"/>
                                  </aspectReferenceSet>
                              </aspectReferences>
                          </solution>
        Dim solution = SolutionFromXml(solutionXml)

        'Act
        Dim rawScore = ScoringPropertiesCalculator.GetRawScore(solution)

        'Assert
        Assert.AreEqual(8, rawScore)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub RawScore_Summed()
        'Arrange
        Dim solutionXml = <solution>
                              <keyFindings>
                                  <keyFinding id="mc" scoringMethod="Polytomous">
                                      <keyFact id="1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                      <keyFact id="2" score="2" xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                  </keyFinding>
                                  <keyFinding id="gapMatchController" scoringMethod="Dichotomous">
                                      <keyFact id="3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                      <keyFact id="4" score="1" xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                  </keyFinding>
                              </keyFindings>
                          </solution>
        Dim solution = SolutionFromXml(solutionXml)

        'Act
        Dim rawScore = ScoringPropertiesCalculator.GetRawScore(solution)

        'Assert
        Assert.AreEqual(4, rawScore)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub MaxScore_DichotomousWithScoreTranslationTable()
        'Arrange
        Dim solutionXml = <solution>
                              <keyFindings>
                                  <keyFinding id="mc" scoringMethod="Dichotomous">
                                      <keyFact id="24834555-98fa-45ae-b5c0-245085594ade" score="1" xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                      <keyFact id="e4c4fe8a-c27a-41bb-96ae-eb753e505885" score="2" xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                  </keyFinding>
                              </keyFindings>
                              <ItemScoreTranslationTable>
                                  <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                                  <ItemScoreTranslationTableEntry rawScore="1" translatedScore="4"/>
                              </ItemScoreTranslationTable>
                          </solution>
        Dim solution = SolutionFromXml(solutionXml)

        'Act
        Dim maxScore = ScoringPropertiesCalculator.GetMaxScore(solution)

        'Assert
        Assert.AreEqual(4D, maxScore)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub MaxScore_PolytomousWithoutScoreTranslationTable()
        'Arrange
        Dim solutionXml = <solution>
                              <keyFindings>
                                  <keyFinding id="mc" scoringMethod="Polytomous">
                                      <keyFact id="24834555-98fa-45ae-b5c0-245085594ade" score="1" xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                      <keyFact id="e4c4fe8a-c27a-41bb-96ae-eb753e505885" score="2" xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                  </keyFinding>
                              </keyFindings>
                          </solution>
        Dim solution = SolutionFromXml(solutionXml)

        'Act
        Dim maxScore = ScoringPropertiesCalculator.GetMaxScore(solution)

        'Assert
        Assert.AreEqual(3D, maxScore)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), Description("Format Matrix KeyValues into a user friendly string")>
    Public Sub KeyValues_FormatMatrixKeyValuesIntoUserFriendlyString()
        'Arrange
        Dim solutionXml = <solution>
                              <keyFindings>
                                  <keyFinding id="mc" scoringMethod="Dichotomous">
                                      <keyFact id="A" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                          <keyValue domain="domainX" occur="1">
                                              <stringValue>
                                                  <typedValue>C</typedValue>
                                              </stringValue>
                                          </keyValue>
                                          <keyValue domain="domainY" occur="1">
                                              <stringValue>
                                                  <typedValue>A</typedValue>
                                              </stringValue>
                                          </keyValue>
                                      </keyFact>
                                      <keyFact id="B" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                          <keyValue domain="domainX" occur="1">
                                              <stringValue>
                                                  <typedValue>B</typedValue>
                                              </stringValue>
                                          </keyValue>
                                          <keyValue domain="domainY" occur="1">
                                              <stringValue>
                                                  <typedValue>C</typedValue>
                                              </stringValue>
                                          </keyValue>
                                      </keyFact>
                                      <keyFact id="C" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                          <keyValue domain="domainX" occur="1">
                                              <stringValue>
                                                  <typedValue>A</typedValue>
                                              </stringValue>
                                          </keyValue>
                                          <keyValue domain="domainY" occur="1">
                                              <stringValue>
                                                  <typedValue>D</typedValue>
                                              </stringValue>
                                          </keyValue>
                                      </keyFact>
                                      <keyFact id="D" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                          <keyValue domain="domainX" occur="1">
                                              <stringValue>
                                                  <typedValue>D</typedValue>
                                              </stringValue>
                                          </keyValue>
                                          <keyValue domain="domainY" occur="1">
                                              <stringValue>
                                                  <typedValue>B</typedValue>
                                              </stringValue>
                                          </keyValue>
                                      </keyFact>
                                  </keyFinding>
                              </keyFindings>
                          </solution>
        Dim solution = SolutionFromXml(solutionXml)

        'Act
        Dim keyValuesString = ScoringPropertiesCalculator.GetKeyValuesAsString(solution, 0)

        'Assert
        Assert.AreEqual("CBAD", keyValuesString)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), Description("Format KeyValues into a user friendly string")>
    Public Sub KeyValues_FormatKeyValuesIntoUserFriendlyString1()
        'Arrange
        Dim solutionXml = <solution>
                              <keyFindings>
                                  <keyFinding id="inlineChoiceController" scoringMethod="Dichotomous">
                                      <keyFact id="fcc726ca-1957-41f5-b526-51c97ecc3926" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                          <keyValue domain="fcc726ca-1957-41f5-b526-51c97ecc3926" occur="1">
                                              <stringValue>
                                                  <typedValue>A</typedValue>
                                              </stringValue>
                                          </keyValue>
                                      </keyFact>
                                  </keyFinding>
                                  <keyFinding id="audioController1" scoringMethod="None"/>
                              </keyFindings>
                              <aspectReferences/>
                          </solution>


        Dim solution = SolutionFromXml(solutionXml)

        'Act
        Dim keyValuesString = ScoringPropertiesCalculator.GetKeyValuesAsString(solution, 0)

        'Assert
        Assert.AreEqual("A", keyValuesString)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), Description("Format KeyValues into a user friendly string")>
    Public Sub KeyValues_FormatKeyValuesIntoUserFriendlyString2()
        'Arrange
        Dim solutionXml = <solution>
                              <keyFindings>
                                  <keyFinding id="mc" scoringMethod="Dichotomous">
                                      <keyFact id="B" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                          <keyValue domain="mc" occur="1">
                                              <stringValue>
                                                  <typedValue>B</typedValue>
                                              </stringValue>
                                          </keyValue>
                                      </keyFact>
                                  </keyFinding>
                                  <keyFinding id="audioControllerverklankingLinks" scoringMethod="None"/>
                                  <keyFinding id="audioControllerverklankingRechts" scoringMethod="None"/>
                              </keyFindings>
                              <aspectReferences/>
                          </solution>

        Dim solution = SolutionFromXml(solutionXml)

        'Act
        Dim keyValuesString = ScoringPropertiesCalculator.GetKeyValuesAsString(solution, 0)

        'Assert
        Assert.AreEqual(keyValuesString, "B")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), Description("Format KeyValues into a user friendly string")>
    Public Sub KeyValues_FormatKeyValuesIntoUserFriendlyString3()
        'Arrange
        Dim solutionXml = <solution>
                              <keyFindings>
                                  <keyFinding id="PoRwItemController" scoringMethod="None"/>
                                  <keyFinding id="PoRwGapController" scoringMethod="Dichotomous">
                                      <keyFact id="VastDeel_A" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                          <keyValue domain="VastDeel_A" occur="1">
                                              <integerValue>
                                                  <typedValue>1</typedValue>
                                              </integerValue>
                                          </keyValue>
                                      </keyFact>
                                      <keyFact id="VastDeel_B" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                          <keyValue domain="VastDeel_B" occur="1">
                                              <integerValue>
                                                  <typedValue>30</typedValue>
                                              </integerValue>
                                          </keyValue>
                                      </keyFact>
                                  </keyFinding>
                              </keyFindings>
                              <aspectReferences/>
                          </solution>

        Dim solution = SolutionFromXml(solutionXml)

        'Act
        Dim keyValuesString = ScoringPropertiesCalculator.GetKeyValuesAsString(solution, 0)

        'Assert
        Assert.AreEqual("1&30", keyValuesString)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), Description("Format KeyValues into a user friendly string")>
    Public Sub KeyValues_FormatKeyValuesIntoUserFriendlyString4()
        'Arrange
        Dim solutionXml =
        <solution>
            <keyFindings>
                <keyFinding id="audioController1" scoringMethod="None"/>
                <keyFinding id="audioController2" scoringMethod="None"/>
            </keyFindings>
            <aspectReferences/>
        </solution>
        
        Dim solution = SolutionFromXml(solutionXml)

        'Act
        Dim keyValuesString = ScoringPropertiesCalculator.GetKeyValuesAsString(solution, 0)

        'Assert
        Assert.IsTrue(String.IsNullOrEmpty(keyValuesString), "The key string should be empty.")
    End Sub

#Region "Helper functions"
    Private Function SolutionFromXml(xml As XElement) As Solution
        Dim serializer As New XmlSerializer(GetType(Solution))
        Return DirectCast(serializer.Deserialize(xml.CreateReader()), Solution)
    End Function

    Private Function IcInfoFromXml(xml As XElement) As InteractionControllerInfoCollection
        Dim xdoc As New XHtmlDocument()
        xdoc.LoadXml(xml.ToString())
        Dim icCollection = New InteractionControllerInfoCollection()
        For Each iControllerNode As Xml.XmlNode In GetListOfInteractionControllers(xdoc)
            Dim controllerId = iControllerNode.Attributes("id").Value
            Dim fakeInteractionControllerInfo = New InteractionControllerInfo(iControllerNode, GetListOfInteractionControls(xdoc, controllerId))
            icCollection.Add(fakeInteractionControllerInfo)
        Next
        Return icCollection
    End Function

    Private Function GetListOfInteractionControllers(ByVal parsedXmlTemplate As XHtmlDocument) As Xml.XmlNodeList
        'Create namespace manager
        Dim nsmgr As Xml.XmlNamespaceManager = New Xml.XmlNamespaceManager(parsedXmlTemplate.NameTable)
        nsmgr.AddNamespace("xhtml", "http://www.w3.org/1999/xhtml")
        nsmgr.AddNamespace("cito", "http://www.cito.nl/citotester")

        'Create XPath for selecting 
        Dim nodes As Xml.XmlNodeList = parsedXmlTemplate.SelectNodes("//cito:interactionController", nsmgr)
        Return nodes
    End Function

    Private Function GetListOfInteractionControls(ByVal parsedXmlTemplate As XHtmlDocument, ByVal controllerId As String) As Xml.XmlNodeList
        'Create namespace manager
        Dim nsmgr As Xml.XmlNamespaceManager = New Xml.XmlNamespaceManager(parsedXmlTemplate.NameTable)
        nsmgr.AddNamespace("xhtml", "http://www.w3.org/1999/xhtml")
        nsmgr.AddNamespace("cito", "http://www.cito.nl/citotester")

        'Create XPath
        Dim nodes As Xml.XmlNodeList = parsedXmlTemplate.SelectNodes(String.Format("//cito:interactionControl[@controller='{0}']", controllerId), nsmgr)
        Return nodes
    End Function

#End Region

End Class
