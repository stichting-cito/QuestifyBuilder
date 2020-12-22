﻿
Imports System.Collections.Concurrent
Imports Cito.Tester.ContentModel
Imports Cito.Tester.Common
Imports Questify.Builder.Configuration
Imports FakeItEasy
Imports System.Xml
Imports Questify.Builder.Logic.QTI.Helpers
Imports Questify.Builder.Logic.QTI.Helpers.QTI22
Imports Questify.Builder.Logic.QTI.Helpers.QTI22.QtiModelHelpers
Imports Questify.Builder.UnitTests.Framework
Imports ResourceType = Questify.Builder.Logic.QTI.Xsd.QTI22_Final.ResourceType
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI22
Imports Questify.Builder.Logic.QTI.Interfaces.QTI22
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI22

<TestClass()>
Public Class QTI22ScoringConvertTest


    <TestMethod(), TestCategory("QTIScoring"), Description("Test responseDeclarations of gap/input items - with multiple correct answers")>
    Public Sub GapInline_AlternativeAnswers_ResponseDeclaration_MatchesExpectedResult()
        Assert.IsTrue(BasicResponseDeclarationTest(My.Resources.GapInlineSC, My.Resources.GapInlineSC_ItemBody, My.Resources.GapInlineSC_ResponseDeclarations, New QTI22CombinedScoringConverter))
    End Sub

    <TestMethod(), TestCategory("QTIScoring"), Description("Test responseDeclarations of gap/input items - with multiple correct answers")>
    Public Sub GapInline2_AlternativeAnswers_ResponseDeclaration_MatchesExpectedResult()
        Assert.IsTrue(BasicResponseDeclarationTest(My.Resources.GapInlineSC2, My.Resources.GapInlineSC2_Itembody, My.Resources.GapInlineSC2_ResponseDeclarations, New QTI22CombinedScoringConverter))
    End Sub

    <TestMethod(), TestCategory("QTIScoring"), Description("Test responseDeclarations of graphic gapmatch items - with multiple correct answers")>
    Public Sub GraphicGapMatch_AlternativeAnswers_ResponseDeclaration_MatchesExpectedResult()
        Dim itemSource As XElement = XElement.Parse(My.Resources.GraphicGapMatch)
        Dim item As AssessmentItem = CreateAssessmentItem(itemSource)
        Assert.IsTrue(BasicResponseDeclarationTest(My.Resources.GraphicGapMatch, My.Resources.GraphicGapMatch_Itembody, My.Resources.GraphicGapMatch_ResponseDeclarations, New QTI22CombinedScoringConverter(item.Parameters.DeepFetchScoringParameters)))
    End Sub

    <TestMethod(), TestCategory("QTIScoring"), Description("Test responseDeclarations of inline choice items - with multiple correct answers")>
    Public Sub ChoiceInline_AlternativeAnswers_ResponseDeclaration_MatchesExpectedResult()
        Dim itemSource As XElement = XElement.Parse(My.Resources.ChoiceInline)
        Dim item As AssessmentItem = CreateAssessmentItem(itemSource)
        Assert.IsTrue(BasicResponseDeclarationTest(My.Resources.ChoiceInline, My.Resources.ChoiceInline_Itembody, My.Resources.ChoiceInline_ResponseDeclarations, New QTI22CombinedScoringConverter(item.Parameters.DeepFetchScoringParameters)))
    End Sub



    <TestMethod(), TestCategory("QTIScoring"), Description("Test scoring of gap/input items - date")>
    Public Sub GapDateScoringTest()
        Assert.IsTrue(BasicScoringConverterTest(My.Resources.GapDateSC, My.Resources.GapDateSC_Itembody, My.Resources.GapDateSC_ResponseProcessing_QTI22, New QTI22CombinedScoringConverter))
    End Sub

    <TestMethod(), TestCategory("QTIScoring"), Description("Test scoring of MC items - with multiple concepts")>
    Public Sub McWithConceptsScoringTest()
        Dim itemSource As XElement = XElement.Parse(My.Resources.McWithConcepts)
        Dim item As AssessmentItem = CreateAssessmentItem(itemSource)
        Assert.IsTrue(BasicScoringConverterTest(My.Resources.McWithConcepts, My.Resources.McWithConcepts_Itembody, My.Resources.McWithConcepts_ResponseProcessing_QTI22, New QTI22CombinedScoringConverter(item.Parameters.DeepFetchScoringParameters())))
    End Sub

    <TestMethod(), TestCategory("QTIScoring"), Description("Test scoring of inline alternative answers")>
    Public Sub GapInline_AlternativeAnswers_ResponseProcessing_MatchesExpectedResult()
        Assert.IsTrue(BasicScoringConverterTest(My.Resources.GapInlineSC, My.Resources.GapInlineSC_ItemBody, My.Resources.GapInlineSC_ResponseProcessing_QTI22, New QTI22CombinedScoringConverter()))
    End Sub

    <TestMethod(), TestCategory("QTIScoring"), Description("Test scoring of inline alternative answers")>
    Public Sub GapInline2_AlternativeAnswers_ResponseProcessing_MatchesExpectedResult()
        Assert.IsTrue(BasicScoringConverterTest(My.Resources.GapInlineSC2, My.Resources.GapInlineSC2_Itembody, My.Resources.GapInlineSC2_ResponseProcessing_QTI22, New QTI22CombinedScoringConverter()))
    End Sub

    <TestMethod(), TestCategory("QTIScoring"), Description("Test scoring of graphic alternative answers")>
    Public Sub GraphicGapMatch_AlternativeAnswers_ResponseProcessing_MatchesExpectedResult()
        Dim itemSource As XElement = XElement.Parse(My.Resources.GraphicGapMatch)
        Dim item As AssessmentItem = CreateAssessmentItem(itemSource)
        Assert.IsTrue(BasicScoringConverterTest(My.Resources.GraphicGapMatch, My.Resources.GraphicGapMatch_Itembody, My.Resources.GraphicGapMatch_ResponseProcessing_QTI22, New QTI22CombinedScoringConverter(item.Parameters.DeepFetchScoringParameters())))
    End Sub

    <TestMethod(), TestCategory("QTIScoring"), Description("Test scoring of inline choice alternative answers")>
    Public Sub ChoiceInline_AlternativeAnswers_ResponseProcessing_MatchesExpectedResult()
        Dim itemSource As XElement = XElement.Parse(My.Resources.ChoiceInline)
        Dim item As AssessmentItem = CreateAssessmentItem(itemSource)
        Assert.IsTrue(BasicScoringConverterTest(My.Resources.ChoiceInline, My.Resources.ChoiceInline_Itembody, My.Resources.ChoiceInline_ResponseProcessing_QTI22, New QTI22CombinedScoringConverter(item.Parameters.DeepFetchScoringParameters())))
    End Sub



    <TestMethod(), TestCategory("QTIScoring"), Description("Test outcomeDeclarations of MC items - with multiple concepts")>
    Public Sub McWithMultipleConceptsOutcomeDeclarationTest()
        Dim itemSource As XElement = XElement.Parse(My.Resources.McWithConcepts)
        Dim item As AssessmentItem = CreateAssessmentItem(itemSource)
        Assert.IsTrue(BasicOutcomeDeclarationTest(My.Resources.McWithConcepts, My.Resources.McWithConcepts_Itembody, My.Resources.McWithConcepts_OutcomeDeclarations, New QTI22CombinedScoringConverter(item.Parameters.DeepFetchScoringParameters())))
    End Sub

    <TestMethod(), TestCategory("QTIScoring"), Description("Test outcomeDeclarations of textual gap/input items - with preprocessing rules and concepts")>
    Public Sub PreprocessorWithConceptsOutcomeDeclarationTest()
        Dim itemSource As XElement = XElement.Parse(My.Resources.PreprocessorWithConcepts)
        Dim item As AssessmentItem = CreateAssessmentItem(itemSource)
        Assert.IsTrue(BasicOutcomeDeclarationTest(My.Resources.PreprocessorWithConcepts, My.Resources.PreprocessorWithConcepts_Itembody, My.Resources.PreprocessorWithConcepts_OutcomeDeclarations, New QTI22CombinedScoringConverter(item.Parameters.DeepFetchScoringParameters())))
    End Sub



    <TestMethod(), TestCategory("QTIScoring")>
    Public Sub VideoResponseTest()
        Dim item As AssessmentItem = CreateAssessmentItem(<assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="biKB-2012-B3 vraag 13" title="Bloed 3 van 4" layoutTemplateSrc="Cito.Generic.MC.DC">
                                                              <solution>
                                                                  <keyFindings>
                                                                      <keyFinding id="mc" scoringMethod="Dichotomous">
                                                                          <keyFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                              <keyValue domain="mc" occur="1">
                                                                                  <stringValue>
                                                                                      <typedValue>B</typedValue>
                                                                                  </stringValue>
                                                                              </keyValue>
                                                                          </keyFact>
                                                                      </keyFinding>
                                                                  </keyFindings>
                                                                  <aspectReferences/>
                                                              </solution>
                                                              <parameters>
                                                                  <parameterSet id="entireItem">
                                                                      <booleanparameter name="isScoredItem">True</booleanparameter>
                                                                      <booleanparameter name="dualColumnLayout">True</booleanparameter>
                                                                      <booleanparameter name="showCalculatorButton"/>
                                                                      <booleanparameter name="displayVerklankingOnTheRight">False</booleanparameter>
                                                                      <collectionparameter name="numberOfAudioContentItems">
                                                                          <definition id="">
                                                                              <resourceparameter name="audiocontent"/>
                                                                              <xhtmlparameter name="description"/>
                                                                          </definition>
                                                                      </collectionparameter>
                                                                      <xhtmlparameter name="leftBody">
                                                                          <p id="c1-id-12" xmlns="http://www.w3.org/1999/xhtml">
                                                                              <strong id="c1-id-13">Bloed</strong>
                                                                              <br id="c1-id-14"/>
                                                                              <br id="c1-id-15"/>Wraak. <br id="c1-id-16"/><br id="c1-id-17"/><cito:InlineElement id="ac32316e-3d0f-4f02-96db-e8bb6c907fc9" layoutTemplateSourceName="InlineVideoLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester"><cito:parameters><cito:parameterSet id="entireItem"><cito:resourceparameter name="sourceWebm">bloedgeven_VP8.webm</cito:resourceparameter><cito:resourceparameter name="sourceMp4"/><cito:resourceparameter name="sourceMpg"/><cito:booleanparameter name="autoStart">False</cito:booleanparameter><cito:integerparameter name="width">384</cito:integerparameter><cito:integerparameter name="height">288</cito:integerparameter><cito:booleanparameter name="showToolbar">True</cito:booleanparameter><cito:booleanparameter name="showPlayButton">True</cito:booleanparameter><cito:booleanparameter name="showPauseButton">False</cito:booleanparameter><cito:booleanparameter name="showStopButton">False</cito:booleanparameter><cito:booleanparameter name="showTimeSlider">False</cito:booleanparameter><cito:booleanparameter name="showElapsedTime">False</cito:booleanparameter><cito:booleanparameter name="showTotalTime">False</cito:booleanparameter><cito:booleanparameter name="showFastforwardButton">False</cito:booleanparameter><cito:booleanparameter name="showRewindButton">False</cito:booleanparameter><cito:integerparameter name="maxPlay">1</cito:integerparameter><cito:plaintextparameter name="mediaPlayerDescription"/></cito:parameterSet></cito:parameters></cito:InlineElement></p>
                                                                          <p id="c1-id-18" xmlns="http://www.w3.org/1999/xhtml">Costa<br id="c1-id-19"/>'Nostre'<br id="c1-id-20"/><br id="c1-id-21"/>Bram.</p>
                                                                      </xhtmlparameter>
                                                                      <xhtmlresourceparameter name="leftSource"/>
                                                                      <integerparameter name="sourceHeight">200</integerparameter>
                                                                      <integerparameter name="sourcePositionTop">0</integerparameter>
                                                                      <xhtmlparameter name="itemBody">
                                                                          <p id="c1-id-12" xmlns="http://www.w3.org/1999/xhtml">Stoker.</p>
                                                                      </xhtmlparameter>
                                                                      <xhtmlparameter name="itemQuestion">
                                                                          <p id="c1-id-12" xmlns="http://www.w3.org/1999/xhtml">
                                                                              <strong id="c1-id-13">Foo?</strong>
                                                                          </p>
                                                                      </xhtmlparameter>
                                                                      <listedparameter name="expectedAnswers">1</listedparameter>
                                                                      <booleanparameter name="showGroup">False</booleanparameter>
                                                                      <integerparameter name="fixedHeightAlternatives">0</integerparameter>
                                                                      <listedparameter name="nrAlternativesPerLine">1</listedparameter>
                                                                      <booleanparameter name="horizontallyCenteredAlternatives">False</booleanparameter>
                                                                      <xhtmlparameter name="kolomtekst"/>
                                                                      <xhtmlparameter name="kolomtekst2"/>
                                                                      <collectionparameter name="multiChoice">
                                                                          <subparameterset id="A">
                                                                              <xhtmlparameter name="choice">
                                                                                  <p id="c1-id-12" xmlns="http://www.w3.org/1999/xhtml">ongeveer 6%</p>
                                                                              </xhtmlparameter>
                                                                              <xhtmlparameter name="choice2"/>
                                                                          </subparameterset>
                                                                          <subparameterset id="B">
                                                                              <xhtmlparameter name="choice">
                                                                                  <p id="c1-id-12" xmlns="http://www.w3.org/1999/xhtml">ongeveer 12%</p>
                                                                              </xhtmlparameter>
                                                                              <xhtmlparameter name="choice2"/>
                                                                          </subparameterset>
                                                                          <subparameterset id="C">
                                                                              <xhtmlparameter name="choice">
                                                                                  <p id="c1-id-12" xmlns="http://www.w3.org/1999/xhtml">ongeveer 18%</p>
                                                                              </xhtmlparameter>
                                                                              <xhtmlparameter name="choice2"/>
                                                                          </subparameterset>
                                                                          <subparameterset id="D">
                                                                              <xhtmlparameter name="choice">
                                                                                  <p id="c1-id-12" xmlns="http://www.w3.org/1999/xhtml">ongeveer 24%</p>
                                                                              </xhtmlparameter>
                                                                              <xhtmlparameter name="choice2"/>
                                                                          </subparameterset>
                                                                          <definition id="">
                                                                              <xhtmlparameter name="choice"/>
                                                                              <xhtmlparameter name="choice2"/>
                                                                          </definition>
                                                                      </collectionparameter>
                                                                      <xhtmlparameter name="itemGeneral"/>
                                                                      <booleanparameter name="boldedMcLettersForWord">True</booleanparameter>
                                                                  </parameterSet>
                                                                  <parameterSet id="kenmerken">
                                                                      <booleanparameter name="showCalculatorButton">False</booleanparameter>
                                                                      <integerparameter name="hightOfScrollText">200</integerparameter>
                                                                      <booleanparameter name="showGroup">False</booleanparameter>
                                                                      <plaintextparameter name="calculatorDescription"/>
                                                                      <listedparameter name="calculatorMode">basic</listedparameter>
                                                                      <booleanparameter name="showNotepad"/>
                                                                      <plaintextparameter name="notepadDescription"/>
                                                                      <booleanparameter name="showSymbolPicker"/>
                                                                      <plaintextparameter name="symbolPickerDescription"/>
                                                                      <plaintextparameter name="symbols"/>
                                                                      <booleanparameter name="showRuler"/>
                                                                      <plaintextparameter name="rulerDescription"/>
                                                                      <integerparameter name="rulerStart"/>
                                                                      <integerparameter name="rulerEnd"/>
                                                                      <integerparameter name="rulerStep"/>
                                                                      <integerparameter name="rulerStepSize"/>
                                                                      <listedparameter name="rulerLengthUnit">centimeter</listedparameter>
                                                                      <booleanparameter name="showTriangle"/>
                                                                      <plaintextparameter name="trianglePickerDescription"/>
                                                                      <integerparameter name="triangleMinDegrees"/>
                                                                      <integerparameter name="triangleMaxDegrees"/>
                                                                      <booleanparameter name="showSpellCheck">False</booleanparameter>
                                                                      <listedparameter name="spellCheckCulture">nl-NL</listedparameter>
                                                                      <booleanparameter name="showFormulaEditor">False</booleanparameter>
                                                                      <plaintextparameter name="formulaEditorDescription"/>
                                                                      <listedparameter name="formulaEditorType"/>
                                                                      <booleanparameter name="showTextMarker">False</booleanparameter>
                                                                      <plaintextparameter name="textMarkerDescription"/>
                                                                      <integerparameter name="fixedHeightAlternatives">0</integerparameter>
                                                                      <listedparameter name="foreignLanguage">0</listedparameter>
                                                                      <listedparameter name="specialItemLayout">0</listedparameter>
                                                                      <integerparameter name="fixedWidthAlternatives">100</integerparameter>
                                                                      <listedparameter name="inputFilter"/>
                                                                      <integerparameter name="fixedWidthMatrixColumn">100</integerparameter>
                                                                  </parameterSet>
                                                              </parameters>
                                                          </assessmentItem>)
        Dim template = <itemBody class="defaultBody">
                           <div class="content">
                               <div class="div_left">
                                   <!-- div_left_inner is needed to create the 50px margin between the left and right columns in dual column mode -->
                                   <div class="div_left_inner">
                                       <div>
                                           <strong>Bloed</strong>
                                           <br/>
                                           <br/>Nog een keer! <br/>
                                           <br/>
                                           <div id="VID_b64d7875-a2ae-42c6-b721-04fd25dd42d9">
                                               <mediaInteraction responseIdentifier="VIDEORESPONSE" autostart="false" maxPlays="1" minPlays="0" loop="false" id="qtiVideo">
                                                   <object type="video/webm" data="video/bloedgeven_VP8.webm" height="288" width="384"/>
                                               </mediaInteraction>
                                           </div>
                                       </div>
                                       <p>Did it again <br/>'Hit me baby'<br/>
                                           <br/>oops.</p>
                                   </div>
                               </div>
                               <div class="div_right">
                                   <!-- div_right_inner is needed to create the 50px margin between the left and right columns in dual column mode -->
                                   <div class="div_right_inner">
                                       <div id="itembody">
                                           <p>Done.</p>
                                       </div>
                                       <div id="question">
                                           <p>
                                               <strong>Deal?</strong>
                                           </p>
                                       </div>
                                       <div>
                                           <choiceInteraction id="choiceInteraction1" class="" maxChoices="1" shuffle="false" responseIdentifier="RESPONSE">
                                               <simpleChoice identifier="A">
                                                   <p>ongeveer 1%</p>
                                               </simpleChoice>
                                               <simpleChoice identifier="B">
                                                   <p>ongeveer 2%</p>
                                               </simpleChoice>
                                               <simpleChoice identifier="C">
                                                   <p>ongeveer 3%</p>
                                               </simpleChoice>
                                               <simpleChoice identifier="D">
                                                   <p>ongeveer 50%</p>
                                               </simpleChoice>
                                           </choiceInteraction>
                                       </div>
                                   </div>
                               </div>
                           </div>
                       </itemBody>

        Dim fakeResourceHelper As ResourceHelper = GetFakeResourceHelper()
        Dim packageCreator As QTI22PackageCreator = New QTI22PackageCreator(New PluginHandlerConfigCollection())
        Dim itemHelper As New ItemHelper(item, Nothing, Nothing, fakeResourceHelper, template.ToString, New QTI22CombinedScoringConverter, packageCreator)

        Dim itmDoc = itemHelper.CreateItemDocument()
        Dim nsmgr As New XmlNamespaceManager(itmDoc.NameTable)
        nsmgr.AddNamespace("default", "http://www.imsglobal.org/xsd/imsqti_v2p2")
        Assert.IsTrue(itmDoc.SelectNodes("//default:responseDeclaration", nsmgr).Count = 2)
    End Sub


    Private Function CreateAssessmentItem(xItem As XElement) As AssessmentItem
        Dim itemObject = SerializeHelper.XmlDeserializeFromString(xItem.ToString, GetType(AssessmentItem))
        Return DirectCast(itemObject, AssessmentItem)
    End Function

    Private Function GetFakeResourceHelper() As ResourceHelper
        Dim fakeResourceHelper = A.Fake(Of ResourceHelper)()
        Dim RedirectedCall = A.CallTo(Function() fakeResourceHelper.ProcessResources(String.Empty,
                                                               New ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String))),
                                                               New ConcurrentDictionary(Of String, String),
                                                               False, String.Empty))
        RedirectedCall.ReturnsLazily(Function(args)
                                         Dim ret As New List(Of String)
                                         Return ret
                                     End Function)
        Return fakeResourceHelper
    End Function

    Private Function BasicScoringConverterTest(resourceItemSource As String, resourceItemBody As String, resourceExpectedResult As String, scoringConverter As IScoringConverterQTI22) As Boolean
        Dim itemSource As XElement = XElement.Parse(resourceItemSource)
        Dim item As AssessmentItem = CreateAssessmentItem(itemSource)
        Dim template As XElement = XElement.Parse(resourceItemBody)
        Dim expectedResult As XDocument = XDocument.Parse(resourceExpectedResult)
        Dim fakeResourceHelper As ResourceHelper = GetFakeResourceHelper()
        Dim packageCreator As QTI22PackageCreator = New QTI22PackageCreator(New PluginHandlerConfigCollection())
        Dim itemHelper As New ItemHelper(item, Nothing, Nothing, fakeResourceHelper, template.ToString, scoringConverter, packageCreator)

        Dim itmDoc = itemHelper.CreateItemDocument()

        Dim nsmgr As New XmlNamespaceManager(itmDoc.NameTable)
        nsmgr.AddNamespace("default", "http://www.imsglobal.org/xsd/imsqti_v2p2")
        Dim resultOutput = XDocument.Parse(itmDoc.SelectSingleNode("//default:responseProcessing", nsmgr).OuterXml)
        Return UnitTestHelper.AreSame(expectedResult, resultOutput)
    End Function

    Private Function BasicOutcomeDeclarationTest(resourceItemSource As String, resourceItemBody As String, resourceExpectedResult As String, scoringConverter As IScoringConverterQTI22) As Boolean
        Dim itemSource As XElement = XElement.Parse(resourceItemSource)
        Dim item As AssessmentItem = CreateAssessmentItem(itemSource)
        Dim template As XElement = XElement.Parse(resourceItemBody)
        Dim expectedResult As XDocument = XDocument.Parse(resourceExpectedResult)

        Dim fakeResourceHelper As ResourceHelper = GetFakeResourceHelper()
        Dim packageCreator As QTI22PackageCreator = New QTI22PackageCreator(New PluginHandlerConfigCollection())
        Dim itemHelper As New ItemHelper(item, Nothing, Nothing, fakeResourceHelper, template.ToString, scoringConverter, packageCreator)

        Dim itmDoc = itemHelper.CreateItemDocument()

        Dim nsmgr As New XmlNamespaceManager(itmDoc.NameTable)
        nsmgr.AddNamespace("default", "http://www.imsglobal.org/xsd/imsqti_v2p2")
        Dim resultOutput As XDocument = New XDocument
        Dim root As XElement = <outcomeDeclarations></outcomeDeclarations>
        resultOutput.Add(root)
        For Each node As XmlNode In itmDoc.SelectNodes("//default:outcomeDeclaration", nsmgr)
            Dim importedNode As XElement = XElement.Parse(ChainHandlerHelper.RemoveNamespaces(node.OuterXml.ToString, New List(Of String), True))
            If resultOutput.Root.FirstNode IsNot Nothing Then
                resultOutput.Root.LastNode.AddAfterSelf(importedNode)
            Else
                resultOutput.Root.AddFirst(importedNode)
            End If
        Next

        Return UnitTestHelper.AreSame(expectedResult, resultOutput)
    End Function

    Private Function BasicResponseDeclarationTest(resourceItemSource As String, resourceItemBody As String, resourceExpectedResult As String, scoringConverter As IScoringConverterQTI22) As Boolean
        Dim itemSource As XElement = XElement.Parse(resourceItemSource)
        Dim item As AssessmentItem = CreateAssessmentItem(itemSource)
        Dim template As XElement = XElement.Parse(resourceItemBody)
        Dim expectedResult As XDocument = XDocument.Parse(resourceExpectedResult)

        Dim fakeResourceHelper As ResourceHelper = GetFakeResourceHelper()
        Dim packageCreator As QTI22PackageCreator = New QTI22PackageCreator(New PluginHandlerConfigCollection())
        Dim itemHelper As New ItemHelper(item, Nothing, Nothing, fakeResourceHelper, template.ToString, scoringConverter, packageCreator)

        Dim itmDoc = itemHelper.CreateItemDocument()

        Dim nsmgr As New XmlNamespaceManager(itmDoc.NameTable)
        nsmgr.AddNamespace("default", "http://www.imsglobal.org/xsd/imsqti_v2p2")
        Dim resultOutput As XDocument = New XDocument
        Dim root As XElement = <responseDeclarations></responseDeclarations>
        resultOutput.Add(root)
        For Each node As XmlNode In itmDoc.SelectNodes("//default:responseDeclaration", nsmgr)
            Dim importedNode As XElement = XElement.Parse(ChainHandlerHelper.RemoveNamespaces(node.OuterXml.ToString, New List(Of String), True))
            If resultOutput.Root.FirstNode IsNot Nothing Then
                resultOutput.Root.LastNode.AddAfterSelf(importedNode)
            Else
                resultOutput.Root.AddFirst(importedNode)
            End If
        Next

        Return UnitTestHelper.AreSame(expectedResult, resultOutput)
    End Function


End Class
