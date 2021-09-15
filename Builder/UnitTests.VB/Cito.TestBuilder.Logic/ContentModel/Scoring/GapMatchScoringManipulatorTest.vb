
Imports Cito.Tester.ContentModel
Imports System.Xml.Linq
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports System.Diagnostics
Imports System.Xml.Serialization
Imports System.IO
Imports System.Linq
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.UnitTests.Framework

<TestClass()>
Public Class GapMatchScoringManipulatorTest

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub GapMatch_GetKey_A_NoSolution()
        'Arrange
        Dim param = CreateScoreParameter()

        Dim key = CreateGapMatchKeyFinding(param.Value.First().Id, "A")
        Dim manipulator As IValidatingChoiceArrayScoringManipulator(Of String) = New GapMatchScoringManipulator(GetKeyManipulator(key), param)

        'Act
        Dim res = manipulator.GetKeyStatus()

        'Assert
        Assert.IsTrue(res.ContainsKey(param.Value.First().Id))
        Assert.AreEqual("A", res(param.Value.First().Id).ToString)
        Assert.AreEqual(param.Value.Count, res.Keys.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub GapMatch_SetKey_B_NoSolution()
        'Arrange
        Dim param = CreateScoreParameter()

        Dim key As KeyFinding = CreateGapMatchKeyFinding(param.Value.First().Id, "A")
        WriteKeyFinding("arrange", key)

        Dim manipulator As IValidatingChoiceArrayScoringManipulator(Of String) = New GapMatchScoringManipulator(GetKeyManipulator(key), param)

        'Act
        manipulator.SetKey(param.Value.Last().Id, "B")

        WriteKeyFinding("act", key)

        'Assert
        Dim res = manipulator.GetKeyStatus()

        Assert.IsTrue(res.ContainsKey(param.Value.First().Id))
        Assert.IsTrue(res.ContainsKey(param.Value.Last().Id))
        Assert.AreEqual("A", res(param.Value.First().Id).ToString)
        Assert.AreEqual("B", res(param.Value.Last().Id).ToString)
        Assert.AreEqual(param.Value.Count, res.Keys.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub GapMatch_RemoveKey_A_NoSolution()
        'Arrange
        Dim param = CreateScoreParameter()

        Dim key = CreateGapMatchKeyFinding(param.Value.First().Id, "A")

        Dim manipulator As IChoiceArrayScoringManipulator = New GapMatchScoringManipulator(GetKeyManipulator(key), param)

        'Act
        manipulator.RemoveKey(param.Value.First().Id)

        'Assert
        Dim res = manipulator.GetKeyStatus()
        For Each keyValuePair In res
            Assert.IsTrue(String.IsNullOrEmpty(keyValuePair.Value))
        Next

        Assert.AreEqual(param.Value.Count, res.Keys.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub GapMatch_Clear_All_NoSolution()
        'Arrange
        Dim param = CreateScoreParameter()

        Dim key = CreateGapMatchKeyFinding(param.Value.First().Id, "A")

        Dim manipulator As IChoiceArrayScoringManipulator = New GapMatchScoringManipulator(GetKeyManipulator(key), param)
        manipulator.SetKey(param.Value(0).Id, "B")
        manipulator.SetKey(param.Value(1).Id, "A")
        manipulator.SetKey(param.Value(2).Id, "C")

        'Act
        manipulator.Clear()

        'Assert
        Dim res = manipulator.GetKeyStatus()
        For Each keyValuePair In res
            Assert.IsTrue(String.IsNullOrEmpty(keyValuePair.Value))
        Next

        Assert.AreEqual(param.Value.Count, res.Keys.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub GapMatch_CreateInvalidFinding_Test()
        'Arrange
        Dim param = CreateScoreParameter()

        Dim key = CreateGapMatchKeyFinding(param.Value.First().Id, "A")

        Dim manipulator As IValidatingChoiceArrayScoringManipulator(Of String) = New GapMatchScoringManipulator(GetKeyManipulator(key), param)
        manipulator.SetKey(param.Value(1).Id, "A")

        'Act
        Dim isValidFinding = manipulator.IsValid("A")

        'Assert
        Assert.IsFalse(isValidFinding)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub GapMatch_CreateValidFinding_Test()
        'Arrange
        Dim param = CreateScoreParameter(False)

        'SET MATCH MAX TO 2
        DirectCast(param.Value(0).InnerParameters(0), GapTextParameter).MatchMax = 2

        param = param.Transform()

        Dim key = CreateGapMatchKeyFinding(param.Value.First().Id, "A")

        Dim manipulator As IValidatingChoiceArrayScoringManipulator(Of String) = New GapMatchScoringManipulator(GetKeyManipulator(key), param)
        manipulator.SetKey(param.Value(1).Id, "A")

        'Act
        Dim isValidFinding = manipulator.IsValid("A")

        'Assert
        Assert.IsTrue(isValidFinding)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub GetDisplayValueForExistingItem_ShouldBeSpring()
        'Arrange
        Dim item = ItemFromTestTool.To(Of AssessmentItem)()

        Dim param = DirectCast(item.Parameters.DeepFetchScoringParameters().First(Function(prm) TypeOf prm Is GapMatchScoringParameter), GapMatchScoringParameter)


        Dim sol = item.Solution
        Dim manipulator As IChoiceArrayScoringManipulator = ScoringParameterFactory.GetConceptScoreManipulator(Of IValidatingChoiceArrayScoringManipulator(Of String))(param, sol)

        'Act
        Dim result = manipulator.GetDisplayValueForKey("I68a92575-2ae9-4cfc-8ce2-284844f72fa9")

        'Assert
        Assert.AreEqual("spring", result)
    End Sub

    Friend Overridable Function GetKeyManipulator(key As KeyFinding) As FindingManipulatorBase
        Return New KeyManipulator(key)
    End Function

    Protected Function CreateGapMatchKeyFinding(id As String, ParamArray keys As String()) As KeyFinding
        Dim ret As New KeyFinding()
        id = id
        For Each k In keys
            Dim fact = New KeyFact(id) With {.Score = 1}
            Dim keyVal = New KeyValue(id, 1)
            fact.Values.Add(keyVal)
            keyVal.Values.Add(New StringValue(k))

            ret.Facts.Add(fact)
        Next

        Return ret
    End Function

    Private Function CreateScoreParameter(Optional transform As Boolean = True) As GapMatchScoringParameter
        Dim param = New GapMatchScoringParameter() With {.FindingOverride = "gapMatchController"}
        param.Value = New ParameterSetCollection
        param.Value.Add(New ParameterCollection() With {.Id = "A"})
        param.Value(0).InnerParameters.Add(New GapTextParameter() With {.Name = GapMatchScoringParameter.GapControlName, .MatchMax = 1, .Value = "winter"})
        param.Value.Add(New ParameterCollection() With {.Id = "B"})
        param.Value(1).InnerParameters.Add(New GapTextParameter() With {.Name = GapMatchScoringParameter.GapControlName, .MatchMax = 1, .Value = "spring"})
        param.Value.Add(New ParameterCollection() With {.Id = "C"})
        param.Value(2).InnerParameters.Add(New GapTextParameter() With {.Name = GapMatchScoringParameter.GapControlName, .MatchMax = 1, .Value = "summer"})
        param.Value.Add(New ParameterCollection() With {.Id = "D"})
        param.Value(3).InnerParameters.Add(New GapTextParameter() With {.Name = GapMatchScoringParameter.GapControlName, .MatchMax = 1, .Value = "autumn"})

        Dim xhtmlParam = Deserialize(Of XHtmlParameter)(_gapXhtmlParameter)
        param.GapXhtmlParameter = xhtmlParam

        If transform Then param = param.Transform()

        Return param
    End Function

    Function DoSerialize(Of T)(obj As T) As XElement
        Dim s = New XmlSerializer(GetType(T))
        Dim ret As XElement = Nothing
        Using m As New StringWriter()
            s.Serialize(m, obj)
            ret = XElement.Parse(m.ToString())
        End Using
        Return ret
    End Function

    Protected Function Deserialize(Of T)(input As XElement) As T
        Dim ret As T
        Dim s = New XmlSerializer(GetType(T))

        Using m As New StringReader(input.ToString())
            ret = DirectCast(s.Deserialize(m), T)
        End Using

        Return ret
    End Function

    Sub WriteSolution(stateName As String, s As Solution)
        Dim a As New XmlSerializer(GetType(Solution))
        Debug.WriteLine(String.Empty)
        Debug.WriteLine(String.Format("WriteSolution for State [{0}]", stateName))
        Using stream = New StringWriter()
            a.Serialize(stream, s)
            Debug.WriteLine(stream.ToString())
        End Using
    End Sub

    Sub WriteKeyFinding(stateName As String, s As KeyFinding)
        Dim a As New XmlSerializer(GetType(KeyFinding))
        Debug.WriteLine(String.Empty)
        Debug.WriteLine(String.Format("WriteKeyFinding for State [{0}]", stateName))
        Using stream = New StringWriter()
            a.Serialize(stream, s)
            Debug.WriteLine(stream.ToString())
        End Using
    End Sub

    Private ReadOnly _gapXhtmlParameter As XElement = <XHtmlParameter name="itemInlineInput" xmlns:cito="http://www.cito.nl/citotester">
                                                          <cito:InlineElement id="I23a0653b-b574-4d5e-ad66-e05af1a169da" layoutTemplateSourceName="InlineGapMatchLayoutTemplate">
                                                              <cito:parameters>
                                                                  <cito:parameterSet id="entireItem">
                                                                      <cito:plaintextparameter name="inlineGapMatchId">I23a0653b-b574-4d5e-ad66-e05af1a169da</cito:plaintextparameter>
                                                                      <cito:plaintextparameter name="inlineGapMatchLabel">Gat 1</cito:plaintextparameter>
                                                                  </cito:parameterSet>
                                                              </cito:parameters>
                                                          </cito:InlineElement>
                                                          <cito:InlineElement id="I47a1295a-c729-49d5-9da0-bac0799a019e" layoutTemplateSourceName="InlineGapMatchLayoutTemplate">
                                                              <cito:parameters>
                                                                  <cito:parameterSet id="entireItem">
                                                                      <cito:plaintextparameter name="inlineGapMatchId">I47a1295a-c729-49d5-9da0-bac0799a019e</cito:plaintextparameter>
                                                                      <cito:plaintextparameter name="inlineGapMatchLabel">Gat 2</cito:plaintextparameter>
                                                                  </cito:parameterSet>
                                                              </cito:parameters>
                                                          </cito:InlineElement> maar een <cito:InlineElement id="I27b8eca6-ae70-4f7b-bc23-b904b2e9045f" layoutTemplateSourceName="InlineGapMatchLayoutTemplate">
                                                              <cito:parameters>
                                                                  <cito:parameterSet id="entireItem">
                                                                      <cito:plaintextparameter name="inlineGapMatchId">I27b8eca6-ae70-4f7b-bc23-b904b2e9045f</cito:plaintextparameter>
                                                                      <cito:plaintextparameter name="inlineGapMatchLabel">Gat 3</cito:plaintextparameter>
                                                                  </cito:parameterSet>
                                                              </cito:parameters>
                                                          </cito:InlineElement>
                                                      </XHtmlParameter>


    ReadOnly ItemFromTestTool As XElement = <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="2001" title="2001" layoutTemplateSrc="ilt.gapmatch">
                                                <solution>
                                                    <keyFindings>
                                                        <keyFinding id="gapMatchController" scoringMethod="Dichotomous">
                                                            <keyFact id="I57c72f50-cf3a-499f-9473-3bd398a13282" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                <keyValue domain="I57c72f50-cf3a-499f-9473-3bd398a13282" occur="1">
                                                                    <stringValue>
                                                                        <typedValue>1</typedValue>
                                                                    </stringValue>
                                                                </keyValue>
                                                            </keyFact>
                                                            <keyFact id="I68a92575-2ae9-4cfc-8ce2-284844f72fa9" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                <keyValue domain="I68a92575-2ae9-4cfc-8ce2-284844f72fa9" occur="1">
                                                                    <stringValue>
                                                                        <typedValue>2</typedValue>
                                                                    </stringValue>
                                                                </keyValue>
                                                            </keyFact>
                                                        </keyFinding>
                                                    </keyFindings>
                                                    <conceptFindings>
                                                        <conceptFinding id="gapMatchController" scoringMethod="None">
                                                            <conceptFact id="I57c72f50-cf3a-499f-9473-3bd398a13282" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                <conceptValue domain="I57c72f50-cf3a-499f-9473-3bd398a13282" occur="1">
                                                                    <stringValue>
                                                                        <typedValue>1</typedValue>
                                                                    </stringValue>
                                                                </conceptValue>
                                                                <concepts/>
                                                            </conceptFact>
                                                            <conceptFact id="I68a92575-2ae9-4cfc-8ce2-284844f72fa9" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                <conceptValue domain="I68a92575-2ae9-4cfc-8ce2-284844f72fa9" occur="1">
                                                                    <stringValue>
                                                                        <typedValue>2</typedValue>
                                                                    </stringValue>
                                                                </conceptValue>
                                                                <concepts/>
                                                            </conceptFact>
                                                            <conceptFact id="I68a92575-2ae9-4cfc-8ce2-284844f72fa9[*]" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                <conceptValue domain="I68a92575-2ae9-4cfc-8ce2-284844f72fa9[*]" occur="1">
                                                                    <catchAllValue/>
                                                                </conceptValue>
                                                                <concepts/>
                                                            </conceptFact>
                                                            <conceptFact id="I57c72f50-cf3a-499f-9473-3bd398a13282[*]" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                <conceptValue domain="I57c72f50-cf3a-499f-9473-3bd398a13282[*]" occur="1">
                                                                    <catchAllValue/>
                                                                </conceptValue>
                                                                <concepts/>
                                                            </conceptFact>
                                                            <conceptFact id="I214363b6-f877-4175-95ec-28042ac1b5a9[*]" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                <conceptValue domain="I214363b6-f877-4175-95ec-28042ac1b5a9[*]" occur="1">
                                                                    <catchAllValue/>
                                                                </conceptValue>
                                                                <concepts/>
                                                            </conceptFact>
                                                        </conceptFinding>
                                                    </conceptFindings>
                                                    <aspectReferences/>
                                                    <ItemScoreTranslationTable>
                                                        <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                                                        <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                                                    </ItemScoreTranslationTable>
                                                </solution>
                                                <parameters>
                                                    <parameterSet id="invoer">
                                                        <gapMatchScoringParameter name="domainx" findingOverride="gapMatchController">
                                                            <subparameterset id="1">
                                                                <gapTextParameter name="gapText" matchMax="2">winter</gapTextParameter>
                                                            </subparameterset>
                                                            <subparameterset id="2">
                                                                <gapTextParameter name="gapText" matchMax="2">spring</gapTextParameter>
                                                            </subparameterset>
                                                            <subparameterset id="3">
                                                                <gapTextParameter name="gapText" matchMax="1">summer</gapTextParameter>
                                                            </subparameterset>
                                                            <subparameterset id="4">
                                                                <gapTextParameter name="gapText" matchMax="1">autumn</gapTextParameter>
                                                            </subparameterset>
                                                            <definition id="">
                                                                <gapTextParameter name="gapText" matchMax="1"/>
                                                            </definition>
                                                            <xhtmlParameter name="itemInlineInput">
                                                                <p id="c1-id-9" xmlns="http://www.w3.org/1999/xhtml">
            Now is the <cito:InlineElement id="I68a92575-2ae9-4cfc-8ce2-284844f72fa9" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester"><cito:parameters><cito:parameterSet id="entireItem"><cito:plaintextparameter name="inlineGapMatchId">I68a92575-2ae9-4cfc-8ce2-284844f72fa9</cito:plaintextparameter><cito:plaintextparameter name="inlineGapMatchLabel">Gat 1</cito:plaintextparameter></cito:parameterSet></cito:parameters></cito:InlineElement><br id="c1-id-11"/>of our discontent<br id="c1-id-12"/>Made glorious <cito:InlineElement id="I57c72f50-cf3a-499f-9473-3bd398a13282" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester"><cito:parameters><cito:parameterSet id="entireItem"><cito:plaintextparameter name="inlineGapMatchId">I57c72f50-cf3a-499f-9473-3bd398a13282</cito:plaintextparameter><cito:plaintextparameter name="inlineGapMatchLabel">Gat 2</cito:plaintextparameter></cito:parameterSet></cito:parameters></cito:InlineElement><br id="c1-id-14"/>by this sun of York;<br id="c1-id-15"/>And all the clouds that lour'd upon our house<br id="c1-id-16"/>In the deep bosom of the ocean buried. <cito:InlineElement id="I214363b6-f877-4175-95ec-28042ac1b5a9" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester"><cito:parameters><cito:parameterSet id="entireItem"><cito:plaintextparameter name="inlineGapMatchId">I214363b6-f877-4175-95ec-28042ac1b5a9</cito:plaintextparameter><cito:plaintextparameter name="inlineGapMatchLabel">Gat 3</cito:plaintextparameter></cito:parameterSet></cito:parameters></cito:InlineElement></p>
                                                            </xhtmlParameter>
                                                        </gapMatchScoringParameter>
                                                    </parameterSet>
                                                </parameters>
                                            </assessmentItem>
End Class
