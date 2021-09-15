
Imports Questify.Builder.Logic.ItemProcessing
Imports Questify.Builder.UnitTests.Framework
Imports Questify.Builder.Logic.ContentModel
Imports System.Linq
Imports Cito.Tester.ContentModel
Imports FakeItEasy
Imports System.Xml.Linq
Imports Questify.Builder.UnitTests.Framework.FakeAppTemplate

<TestClass>
Public Class ItemTemplateSwitchingTest : Inherits baseItemTest

    Private _messageBoxService As Cinch.IMessageBoxService

    <TestInitialize()>
    Public Sub Init()
        FakeDal.Init()
        ItemTestData.AddItemTemplatesAndControlTemplates()

        _messageBoxService = A.Fake(Of Cinch.IMessageBoxService)()
        A.CallTo(Function() _messageBoxService.ShowYesNo(A(Of String).Ignored, A(Of Cinch.CustomDialogIcons).Ignored)).ReturnsLazily(Function()
                                                                                                                                         Return Cinch.CustomDialogResults.Yes
                                                                                                                                     End Function)
        'tmp
        FailOnAssert.Disable = True
    End Sub

    <TestCleanup()>
    Public Sub DeInit()
        FakeDal.Deinit()
    End Sub
    
    <TestMethod(), TestCategory("ItemProcessing")>
    Public Sub Switch_ILT_ThatHasNOMatchingParams_ShouldWork()
        '!! The Can will fail because there are warnings, but switching allows warnings. 
        'Warning should be presented to user.
        'Arrange
        Dim ResourceManager = FakeDal.GetFakeResourceManager()
        Dim itm = MyBase.CreateItem("someItem", ItemTestData.info_AsmntItem, "ilt.html")

        Dim processor = New ItemTemplateSwitching(itm, ResourceManager, _messageBoxService)
        Dim assesmnt As AssessmentItem = Nothing
        
        'Act
        Dim result = processor.SwitchToTemplate("ilt.integer", assesmnt)
        
        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("ItemProcessing")>
    Public Sub Switch_ILT_ThatHasNOMatchingParams_OnlyHasWarnings()
        '!! The Can will fail because there are warnings, but switching allows warnings. 
        'Warning should be presented to user.
        'Arrange
        Dim ResourceManager = FakeDal.GetFakeResourceManager()
        Dim itm = MyBase.CreateItem("someItem", ItemTestData.info_AsmntItem, "ilt.html")
        Dim processor = New ItemTemplateSwitching(itm, ResourceManager, _messageBoxService)
        Dim assesmnt As AssessmentItem = Nothing
        
        'Act
        Dim result = processor.SwitchToTemplate("ilt.integer", assesmnt)
        
        'Assert
        Assert.IsFalse(processor.LastErrorOrWarning.Errors)
        A.CallTo(Function() _messageBoxService.ShowYesNo(A(Of String).Ignored, A(Of Cinch.CustomDialogIcons).Ignored)).MustHaveHappened(Repeated.Exactly.Once)
    End Sub

    <TestMethod(), TestCategory("ItemProcessing")>
    Public Sub Switch_ILT_ThatHasMatchingAndOneMoreParam_ShouldSucceed()
        'Arrange
        Dim ResourceManager = FakeDal.GetFakeResourceManager()
        Dim itm = MyBase.CreateItem("someItem", ItemTestData.info_AsmntItem, "ilt.html")
        Dim processor = New ItemTemplateSwitching(itm, ResourceManager, _messageBoxService)
        Dim assesmnt As AssessmentItem = Nothing
        
        'Act
        Dim result = processor.SwitchToTemplate("ilt.2xhtml", assesmnt)
        
        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("ItemProcessing")>
    Public Sub Switch_ILT_ThatHasMatchingAndOneMoreParam_NoWarningsOrErrors()
        'Arrange
        Dim ResourceManager = FakeDal.GetFakeResourceManager()
        Dim itm = MyBase.CreateItem("someItem", ItemTestData.info_AsmntItem, "ilt.html")
        Dim processor = New ItemTemplateSwitching(itm, ResourceManager, _messageBoxService)
        Dim assesmnt As AssessmentItem = Nothing
        
        'Act
        Dim result = processor.SwitchToTemplate("ilt.2xhtml", assesmnt)
        
        'Assert
        Assert.IsFalse(processor.LastErrorOrWarning.Errors)
        Assert.IsFalse(processor.LastErrorOrWarning.Warnings)
    End Sub


    <TestMethod(), TestCategory("ItemProcessing")>
    Public Sub Switch_ILT_ThatHasMatchingAndOneMoreParam_AssessmentTemplateNameChanged()
        'Arrange
        Dim ResourceManager = FakeDal.GetFakeResourceManager()
        Dim itm = MyBase.CreateItem("someItem", ItemTestData.info_AsmntItem, "ilt.html")
        Dim processor = New ItemTemplateSwitching(itm, ResourceManager, _messageBoxService)
        Dim assesmnt As AssessmentItem = Nothing
        
        'Act
        processor.SwitchToTemplate("ilt.2xhtml", assesmnt)
        Dim ResultingAssessment = itm.GetAssessmentItem()
        
        'Assert
        Assert.AreEqual("ilt.2xhtml", ResultingAssessment.LayoutTemplateSourceName, "Net Template is not present in AssessmentItem ")
    End Sub

    <TestMethod(), TestCategory("ItemProcessing")>
    Public Sub Switch_ILT_ThatHasMatchingAndOneMoreParam_DependencyTo_original_ILT_isGone()
        'Arrange
        Dim ResourceManager = FakeDal.GetFakeResourceManager()
        Dim itm = MyBase.CreateItem("someItem", ItemTestData.info_AsmntItem, "ilt.html")
        Dim ilt = FakeDal.Resources.First(Function(r) r.Name = "ilt.html")
        Dim processor = New ItemTemplateSwitching(itm, ResourceManager, _messageBoxService)
        Dim assesmnt As AssessmentItem = Nothing
        
        'Act
        processor.SwitchToTemplate("ilt.2xhtml", assesmnt)
        
        'Assert
        Assert.IsFalse(itm.DependentResourceCollection.Any(Function(d) d.DependentResourceId = ilt.ResourceId), "dependency to starting ilt has not been removed.")
    End Sub

    <TestMethod(), TestCategory("ItemProcessing")>
    Public Sub Switch_ILT_ThatHasMatchingAndOneMoreParam_DependencyTo_new_ILT_isPresent()
        'Arrange
        Dim ResourceManager = FakeDal.GetFakeResourceManager()
        Dim itm = MyBase.CreateItem("someItem", ItemTestData.info_AsmntItem, "ilt.html")
        Dim ilt = FakeDal.Resources.First(Function(r) r.Name = "ilt.2xhtml")
        Dim processor = New ItemTemplateSwitching(itm, ResourceManager, _messageBoxService)
        Dim assesmnt As AssessmentItem = Nothing
        
        'Act
        processor.SwitchToTemplate("ilt.2xhtml", assesmnt)

        'Assert
        Assert.IsTrue(itm.DependentResourceCollection.Any(Function(d) d.DependentResourceId = ilt.ResourceId), "dependency is missing")
    End Sub

    <TestMethod(), TestCategory("ItemProcessing")>
    Public Sub Switch_ILT_NewAssessment_IsProperlyNamed()
        'Arrange
        Dim ResourceManager = FakeDal.GetFakeResourceManager()
        Dim itm = MyBase.CreateItem("someItem", ItemTestData.info_AsmntItem, "ilt.html")
        Dim processor = New ItemTemplateSwitching(itm, ResourceManager, _messageBoxService)
        Dim assesmnt As AssessmentItem = Nothing
        
        'Act
        processor.SwitchToTemplate("ilt.2xhtml", assesmnt)

        'Assert
        Assert.AreEqual("ilt.2xhtml", assesmnt.LayoutTemplateSourceName)
        Assert.AreEqual("code", assesmnt.Identifier)
        Assert.AreEqual("title", assesmnt.Title)
    End Sub

    <TestMethod(), TestCategory("ItemProcessing")>
    Public Sub Switch_ILT_NewAssessment_AssessmentHasDesignerSettings()
        'Arrange
        Dim ResourceManager = FakeDal.GetFakeResourceManager()
        Dim itm = MyBase.CreateItem("someItem", ItemTestData.info_AsmntItem, "ilt.html")
        Dim processor = New ItemTemplateSwitching(itm, ResourceManager, _messageBoxService)
        Dim assesmnt As AssessmentItem = Nothing
        
        'Act
        processor.SwitchToTemplate("ilt.2xhtml", assesmnt)

        'Assert
        Assert.IsTrue(assesmnt.Parameters(0).InnerParameters(0).DesignerSettings.Count > 0)
    End Sub

    <TestMethod(), TestCategory("ItemProcessing")>
    Public Sub Switch_ILT_MCtoMR_WithScoringParameter_SolutionIsvalid()
        'Arrange
        Dim expectedResult As XElement = <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="code" itemid="testId" title="title" layoutTemplateSrc="ilt.mr.dc">
                                             <solution>
                                                 <keyFindings/>
                                                 <aspectReferences/>
                                             </solution>
                                             <parameters>
                                                 <parameterSet id="fixedid">
                                                     <booleanparameter name="isScoredItem">True</booleanparameter>
                                                     <booleanparameter name="dualColumnLayout">True</booleanparameter>
                                                     <integerparameter name="testInteger">1</integerparameter>
                                                     <xhtmlparameter name="leftBody">
                                                         <p id="fixedid" xmlns="http://www.w3.org/1999/xhtml">Dit is de leftBody</p>
                                                     </xhtmlparameter>
                                                     <xhtmlparameter name="itemQuestion">
                                                         <p id="fixedid" xmlns="http://www.w3.org/1999/xhtml">Dit is de question(zonder inline elementen nog)</p>
                                                     </xhtmlparameter>
                                                     <integerparameter name="multiChoiceType">0</integerparameter>
                                                     <listedparameter name="expectedAnswers">2</listedparameter>
                                                     <multichoicescoringparameter name="multiChoiceScoring" ControllerId="mc" findingOverride="mc" minChoices="2" maxChoices="2" multiChoice="Check">
                                                         <subparameterset id="fixedid">
                                                             <xhtmlparameter name="mcOption">
                                                                 <p id="fixedid" xmlns="http://www.w3.org/1999/xhtml">Antwoord A</p>
                                                             </xhtmlparameter>
                                                             <xhtmlparameter name="mcOption2"/>
                                                         </subparameterset>
                                                         <subparameterset id="fixedid">
                                                             <xhtmlparameter name="mcOption">
                                                                 <p id="fixedid" xmlns="http://www.w3.org/1999/xhtml">Antwoord B</p>
                                                             </xhtmlparameter>
                                                             <xhtmlparameter name="mcOption2"/>
                                                         </subparameterset>
                                                         <subparameterset id="fixedid">
                                                             <xhtmlparameter name="mcOption">
                                                                 <p id="fixedid" xmlns="http://www.w3.org/1999/xhtml">Antwoord B</p>
                                                             </xhtmlparameter>
                                                             <xhtmlparameter name="mcOption2"/>
                                                         </subparameterset>
                                                         <definition id="fixedid">
                                                             <xhtmlparameter name="mcOption"/>
                                                             <xhtmlparameter name="mcOption2"/>
                                                         </definition>
                                                     </multichoicescoringparameter>
                                                 </parameterSet>
                                             </parameters>
                                         </assessmentItem>

        Dim confirmationAsked = False

        A.CallTo(Function() _messageBoxService.ShowYesNo(A(Of String).Ignored, A(Of Cinch.CustomDialogIcons).Ignored)).ReturnsLazily(Function()
                                                                                                                                         confirmationAsked = True
                                                                                                                                         Return Cinch.CustomDialogResults.Yes
                                                                                                                                     End Function)
        Dim resourceManager = FakeDal.GetFakeResourceManager()
        Dim itm = MyBase.CreateItem("mc_dc_item", ItemTestData.mc_dc_item, "ilt.mc.dc")
        Dim processor = New ItemTemplateSwitching(itm, resourceManager, _messageBoxService)
        Dim assessment As AssessmentItem = Nothing

        'Act
        Dim result = processor.SwitchToTemplate("ilt.mr.dc", assessment)

        'Assert
        'Switch should have succeeded
        Assert.IsTrue(result)

        'Should have asked confirmation
        Assert.IsTrue(confirmationAsked)

        'MultiChoice of scoringsparam should have been changed.
        'Value of testInteger should have been changed (defaultvalue changed by new ItemLayoutTemplate)
        Assert.AreEqual(MultiChoiceType.Check, assessment.Parameters(0).InnerParameters.OfType(Of MultiChoiceScoringParameter)().FirstOrDefault().MultiChoice)
        Assert.AreEqual(1, assessment.Parameters(0).InnerParameters.OfType(Of IntegerParameter)().FirstOrDefault(Function(prm) prm.Name = "testInteger").Value)
        Assert.IsTrue(UnitTestHelper.AreSame(itm, expectedResult))
    End Sub

    <TestMethod(), TestCategory("ItemProcessing")>
    Public Sub Switch_ILT_DCtoSC()
        'Arrange
        Dim expected As XElement = <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="code" itemid="testId" title="title" layoutTemplateSrc="ilt.mc.sc">
                                       <solution>
                                           <keyFindings>
                                               <keyFinding id="mc" scoringMethod="Dichotomous">
                                                   <keyFact id="A-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                       <keyValue domain="mc" occur="1">
                                                           <stringValue>
                                                               <typedValue>A</typedValue>
                                                           </stringValue>
                                                       </keyValue>
                                                   </keyFact>
                                               </keyFinding>
                                           </keyFindings>
                                           <aspectReferences/>
                                           <ItemScoreTranslationTable>
                                               <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                                               <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                                           </ItemScoreTranslationTable>
                                       </solution>
                                       <parameters>
                                           <parameterSet id="entireItem">
                                               <booleanparameter name="isScoredItem">True</booleanparameter>
                                               <booleanparameter name="dualColumnLayout">False</booleanparameter>
                                               <integerparameter name="testInteger">15</integerparameter>
                                               <xhtmlparameter name="leftBody"/>
                                               <xhtmlparameter name="itemQuestion">
                                                   <p id="c1-id-10" xmlns="http://www.w3.org/1999/xhtml">Dit is de question(zonder inline elementen nog)</p>
                                               </xhtmlparameter>
                                               <integerparameter name="multiChoiceType">1</integerparameter>
                                               <listedparameter name="expectedAnswers">1</listedparameter>
                                               <multichoicescoringparameter name="multiChoiceScoring" ControllerId="mc" findingOverride="mc" minChoices="1" maxChoices="1" multiChoice="Radio">
                                                   <subparameterset id="A">
                                                       <xhtmlparameter name="mcOption">
                                                           <p id="c1-id-10" xmlns="http://www.w3.org/1999/xhtml">Antwoord A</p>
                                                       </xhtmlparameter>
                                                       <xhtmlparameter name="mcOption2"/>
                                                   </subparameterset>
                                                   <subparameterset id="B">
                                                       <xhtmlparameter name="mcOption">
                                                           <p id="c1-id-10" xmlns="http://www.w3.org/1999/xhtml">Antwoord B</p>
                                                       </xhtmlparameter>
                                                       <xhtmlparameter name="mcOption2"/>
                                                   </subparameterset>
                                                   <subparameterset id="C">
                                                       <xhtmlparameter name="mcOption">
                                                           <p id="c1-id-10" xmlns="http://www.w3.org/1999/xhtml">Antwoord B</p>
                                                       </xhtmlparameter>
                                                       <xhtmlparameter name="mcOption2"/>
                                                   </subparameterset>
                                                   <definition id="">
                                                       <xhtmlparameter name="mcOption"/>
                                                       <xhtmlparameter name="mcOption2"/>
                                                   </definition>
                                               </multichoicescoringparameter>
                                           </parameterSet>
                                       </parameters>
                                   </assessmentItem>

        Dim confirmationAsked = False
        A.CallTo(Function() _messageBoxService.ShowYesNo(A(Of String).Ignored, A(Of Cinch.CustomDialogIcons).Ignored)).ReturnsLazily(Function()
                                                                                                                                         confirmationAsked = True
                                                                                                                                         Return Cinch.CustomDialogResults.Yes
                                                                                                                                     End Function)
        Dim resourceManager = FakeDal.GetFakeResourceManager()
        Dim itm = MyBase.CreateItem("mc_dc_item", ItemTestData.mc_dc_item, "ilt.mc.dc")
        Dim processor = New ItemTemplateSwitching(itm, resourceManager, _messageBoxService)
        Dim assessment As AssessmentItem = Nothing


        'Act
        Dim result = processor.SwitchToTemplate("ilt.mc.sc", assessment)

        'Assert
        'Switch should have succeeded
        Assert.IsTrue(result)

        'Should have asked confirmation: leftBody is cleared
        Assert.IsTrue(confirmationAsked)

        'MultiChoice of scoringsparam should have been changed.
        Assert.IsTrue(UnitTestHelper.AreSame(itm, expected))
    End Sub

    <TestMethod(), TestCategory("ItemProcessing")>
    Public Sub Switch_ILT_ItemWithCollectionParametersToInvisible()
        'Arrange
        Dim expectedResult As XElement = <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="code" itemid="testId" title="title" layoutTemplateSrc="ilt.essay.dc">
                                             <solution>
                                                 <keyFindings/>
                                                 <aspectReferences/>
                                             </solution>
                                             <parameters>
                                                 <parameterSet id="fixedid">
                                                     <booleanparameter name="dualColumnLayout">True</booleanparameter>
                                                     <collectionparameter name="answerFields">
                                                         <subparameterset id="fixedid">
                                                             <booleanparameter name="showAnswerIntro">False</booleanparameter>
                                                             <xhtmlparameter name="answerIntro"/>
                                                         </subparameterset>
                                                         <subparameterset id="fixedid">
                                                             <booleanparameter name="showAnswerIntro">False</booleanparameter>
                                                             <xhtmlparameter name="answerIntro"/>
                                                         </subparameterset>
                                                         <definition id="fixedid">
                                                             <booleanparameter name="showAnswerIntro"/>
                                                             <xhtmlparameter name="answerIntro"/>
                                                         </definition>
                                                     </collectionparameter>
                                                 </parameterSet>
                                             </parameters>
                                         </assessmentItem>

        Dim resourceManager = FakeDal.GetFakeResourceManager()
        Dim itm = MyBase.CreateItem("itm_essay", ItemTestData.itm_essay_sc, "ilt.essay.sc")
        Dim processor = New ItemTemplateSwitching(itm, resourceManager, _messageBoxService)
        Dim assessment As AssessmentItem = Nothing

        'Act
        Dim result = processor.SwitchToTemplate("ilt.essay.dc", assessment)

        'Assert
        Assert.IsTrue(result)

        'Confirmation should have been asked.
        A.CallTo(Function() _messageBoxService.ShowYesNo(A(Of String).Ignored, A(Of Cinch.CustomDialogIcons).Ignored)).MustHaveHappened(Repeated.Exactly.Once)
        Assert.IsFalse(processor.LastErrorOrWarning.Errors)
        Assert.IsFalse(processor.LastErrorOrWarning.Warnings) 'Default, the test confirms the warnings, so the list returned is empty.
        Assert.IsTrue(UnitTestHelper.AreSame(itm, expectedResult))
    End Sub

    <TestMethod, TestCategory("ItemProcessing")>
    Public Sub Switch_ILT_WithInlineElementsVideoShouldNotBeDeleted()
        'Arrange
        ItemTestData.AddInlineItemTemplatesAndControlTemplates()
        Dim expectedResult As XElement = <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="itm_inlinechoice" itemid="testId" title="code" layoutTemplateSrc="Cito.Generic.Gaps.Inline.DC">
                                             <solution>
                                                 <keyFindings/>
                                                 <aspectReferences/>
                                             </solution>
                                             <parameters>
                                                 <parameterSet id="fixedid">
                                                     <booleanparameter name="dualColumnLayout">True</booleanparameter>
                                                     <listedparameter name="inlineType">input</listedparameter>
                                                     <plaintextparameter name="inlineClass"/>
                                                     <integerparameter name="maxChoices">0</integerparameter>
                                                     <xhtmlparameter name="leftBody">
                                                         <p id="fixedid" xmlns="http://www.w3.org/1999/xhtml">
                                                             <strong id="fixedid">Patterns</strong>
                                                         </p>
                                                         <p id="fixedid" xmlns="http://www.w3.org/1999/xhtml">Beantwoord de onderstaande vragen</p>
                                                     </xhtmlparameter>
                                                     <xhtmlparameter name="itemBody"/>
                                                     <xhtmlparameter name="itemQuestion">
                                                         <p id="fixedid" xmlns="http://www.w3.org/1999/xhtml">Hieronder staan een aantal vragen. Beantwoord ze.
                                                                                 <cito:InlineElement id="fixedid" layoutTemplateSourceName="InlineVideoLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester"><cito:parameters><cito:parameterSet id="fixedid"><cito:plaintextparameter name="controlId">VID_4964eaa1-b17f-4c00-aaaf-42d76eb6b42c</cito:plaintextparameter><cito:resourceparameter name="sourceWebm">GenericVideoResource</cito:resourceparameter><cito:resourceparameter name="sourceMp4"/><cito:resourceparameter name="sourceMpg"/><cito:booleanparameter name="autoStart">False</cito:booleanparameter><cito:integerparameter name="width">384</cito:integerparameter><cito:integerparameter name="height">288</cito:integerparameter><cito:booleanparameter name="showToolbar">True</cito:booleanparameter><cito:booleanparameter name="showPlayButton">True</cito:booleanparameter><cito:booleanparameter name="showPauseButton">False</cito:booleanparameter><cito:booleanparameter name="showStopButton">False</cito:booleanparameter><cito:booleanparameter name="showTimeSlider">False</cito:booleanparameter><cito:integerparameter name="maxPlay">0</cito:integerparameter><cito:booleanparameter name="showElapsedTime">False</cito:booleanparameter><cito:booleanparameter name="showTotalTime">False</cito:booleanparameter><cito:booleanparameter name="showFastforwardButton">False</cito:booleanparameter><cito:booleanparameter name="showRewindButton">False</cito:booleanparameter><cito:plaintextparameter name="mediaPlayerDescription"/></cito:parameterSet></cito:parameters></cito:InlineElement></p>
                                                     </xhtmlparameter>
                                                     <xhtmlparameter name="itemInlineInput">
                                                         <p id="fixedid" xmlns="http://www.w3.org/1999/xhtml">Vraag 1:</p>
                                                         <p id="fixedid" xmlns="http://www.w3.org/1999/xhtml"></p>
                                                         <p id="fixedid" xmlns="http://www.w3.org/1999/xhtml">Vraag 2</p>
                                                         <p id="fixedid" xmlns="http://www.w3.org/1999/xhtml"></p>
                                                     </xhtmlparameter>
                                                     <xhtmlparameter name="itemGeneral"/>
                                                 </parameterSet>
                                             </parameters>
                                         </assessmentItem>

        Dim resourceManager = FakeDal.GetFakeResourceManager()
        Dim itm = MyBase.CreateItem("itm_inlinechoice", ItemTestData.item_choice_inline_dc, "choice.inline.dc")
        Dim processor = New ItemTemplateSwitching(itm, resourceManager, _messageBoxService)
        Dim assessment As AssessmentItem = Nothing

        'Act
        Dim result As Boolean = processor.SwitchToTemplate("Cito.Generic.Gaps.Inline.DC", assessment)

        'Assert
        Assert.IsTrue(result)
        A.CallTo(Function() _messageBoxService.ShowYesNo(A(Of String).Ignored, A(Of Cinch.CustomDialogIcons).Ignored)).MustHaveHappened(Repeated.Exactly.Once)
        Assert.IsTrue(UnitTestHelper.AreSame(itm, expectedResult))
    End Sub

    <TestMethod(), TestCategory("ItemProcessing")>
    Public Sub Switch_ILT_GraphicGapMatchDCtoSC()
        'Arrang
        Dim expectedResult As XElement = <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="TestItemIteration12_Original" itemid="testId" title="TestItemIteration12_Original" layoutTemplateSrc="Cito.Generic.GraphicGapMatch.SC">
                                             <solution>
                                                 <keyFindings>
                                                     <keyFinding id="gapMatchController" scoringMethod="Dichotomous">
                                                         <keyFact id="A-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                             <keyValue domain="A-gapMatchController" occur="1">
                                                                 <stringValue>
                                                                     <typedValue>A</typedValue>
                                                                 </stringValue>
                                                             </keyValue>
                                                         </keyFact>
                                                         <keyFact id="B-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                             <keyValue domain="B-gapMatchController" occur="1">
                                                                 <stringValue>
                                                                     <typedValue>B</typedValue>
                                                                 </stringValue>
                                                             </keyValue>
                                                         </keyFact>
                                                     </keyFinding>
                                                 </keyFindings>
                                                 <aspectReferences/>
                                                 <ItemScoreTranslationTable>
                                                     <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                                                     <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                                                 </ItemScoreTranslationTable>
                                             </solution>
                                             <parameters>
                                                 <parameterSet id="entireItem">
                                                     <booleanparameter name="dualColumnLayout">False</booleanparameter>
                                                     <booleanparameter name="isCategorizationItem">False</booleanparameter>
                                                     <graphGapMatchScoringParameter name="graphicGapMatchScoring" ControllerId="gapMatchController" findingOverride="gapMatchController" iscategorizationvariant="false">
                                                         <subparameterset id="A">
                                                             <gapImageParameter name="alternative" matchMax="1" width="0" height="0" contentType="Image" enteredText="">GenericImage</gapImageParameter>
                                                         </subparameterset>
                                                         <subparameterset id="B">
                                                             <gapImageParameter name="alternative" matchMax="1" width="0" height="0" contentType="Text" enteredText="TEXTMATCH"/>
                                                         </subparameterset>
                                                         <subparameterset id="C">
                                                             <gapImageParameter name="alternative" matchMax="1" width="0" height="0" contentType="FormulaImage" enteredText="">GenericImage</gapImageParameter>
                                                         </subparameterset>
                                                         <definition id="">
                                                             <gapImageParameter name="alternative" matchMax="1" width="0" height="0" contentType="Image"/>
                                                         </definition>
                                                         <areaparameter name="itemQuestionArea">
                                                             <subparameterset id="A">
                                                                 <resourceparameter name="clickableImage">GenericImage</resourceparameter>
                                                             </subparameterset>
                                                             <definition id="">
                                                                 <resourceparameter name="clickableImage"/>
                                                             </definition>
                                                             <Shapes>
                                                                 <Rectangle id="A" label="A">
                                                                     <TopLeft>
                                                                         <X>1</X>
                                                                         <Y>204</Y>
                                                                     </TopLeft>
                                                                     <BottomRight>
                                                                         <X>85</X>
                                                                         <Y>284</Y>
                                                                     </BottomRight>
                                                                 </Rectangle>
                                                                 <Rectangle id="B" label="B">
                                                                     <TopLeft>
                                                                         <X>546</X>
                                                                         <Y>201</Y>
                                                                     </TopLeft>
                                                                     <BottomRight>
                                                                         <X>636</X>
                                                                         <Y>277</Y>
                                                                     </BottomRight>
                                                                 </Rectangle>
                                                             </Shapes>
                                                         </areaparameter>
                                                     </graphGapMatchScoringParameter>
                                                 </parameterSet>
                                             </parameters>
                                         </assessmentItem>
        Dim resourceManager = FakeDal.GetFakeResourceManager()
        Dim itm = MyBase.CreateItem("itm_ggm_dc", ItemTestData.itm_ggm_dc, "Cito.Generic.GraphicGapMatch.DC")
        Dim processor = New ItemTemplateSwitching(itm, resourceManager, _messageBoxService)
        Dim assessment As AssessmentItem = Nothing

        'Act
        Dim result As Boolean = processor.SwitchToTemplate("Cito.Generic.GraphicGapMatch.SC", assessment)

        'Assert
        Assert.IsTrue(result)
        Assert.IsTrue(UnitTestHelper.AreSame(itm, expectedResult))
    End Sub

    <TestMethod, TestCategory("ItemProcessing")>
    Public Sub Switch_ILT_Tabular_Dc_To_Mc_Sc()
        'Arrange
        Dim expectedResult As XElement = <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="tabular_dc" itemid="testId" title="tabular_dc" layoutTemplateSrc="Cito.Generic.MC.SC">
                                             <solution>
                                                 <keyFindings/>
                                                 <aspectReferences/>
                                             </solution>
                                             <parameters>
                                                 <parameterSet id="fixedid">
                                                     <booleanparameter name="dualColumnLayout">False</booleanparameter>
                                                     <listedparameter name="expectedAnswers">1</listedparameter>
                                                     <integerparameter name="multiChoiceType">1</integerparameter>
                                                     <xhtmlparameter name="kolomtekst"/>
                                                     <xhtmlparameter name="kolomtekst2"/>
                                                     <multichoicescoringparameter name="multiChoiceScoring" ControllerId="mc" findingOverride="mc" minChoices="1" maxChoices="1" multiChoice="Radio">
                                                         <subparameterset id="fixedid">
                                                             <xhtmlparameter name="mcOption">
                                                                 <p id="fixedid" xmlns="http://www.w3.org/1999/xhtml">A1</p>
                                                             </xhtmlparameter>
                                                             <xhtmlparameter name="mcOption2"/>
                                                         </subparameterset>
                                                         <subparameterset id="fixedid">
                                                             <xhtmlparameter name="mcOption">
                                                                 <p id="fixedid" xmlns="http://www.w3.org/1999/xhtml">B1</p>
                                                             </xhtmlparameter>
                                                             <xhtmlparameter name="mcOption2"/>
                                                         </subparameterset>
                                                         <definition id="fixedid">
                                                             <xhtmlparameter name="mcOption"/>
                                                             <xhtmlparameter name="mcOption2"/>
                                                         </definition>
                                                     </multichoicescoringparameter>
                                                 </parameterSet>
                                             </parameters>
                                         </assessmentItem>

        Dim resourceManager = FakeDal.GetFakeResourceManager()
        Dim itm = CreateItem("tabular_dc", ItemTestData.itm_tabular_dc, "Cito.Generic.Tabular.DC")
        Dim templateSwitcher = New ItemTemplateSwitching(itm, resourceManager, _messageBoxService)
        Dim newAssessmentItem As AssessmentItem = Nothing

        'Act
        Dim result As Boolean = templateSwitcher.SwitchToTemplate("Cito.Generic.MC.SC", newAssessmentItem)

        'Assert
        Assert.AreEqual(True, result, "Expected template to be switched")
        Assert.AreEqual(True, UnitTestHelper.AreSame(itm, expectedResult), "Output is not as expected")
    End Sub

    <TestMethod, TestCategory("ItemProcessing")>
    Public Sub Switch_ILT_WithAttributeReferenceCopiedParameter()
        'Arrange
        Dim expectedResult As XElement = <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="HottextItem" itemid="testId" title="HottextItem" layoutTemplateSrc="Cito.Generic.Hottext.Corrections.DC">
                                             <solution>
                                                 <keyFindings/>
                                                 <aspectReferences/>
                                             </solution>
                                             <parameters>
                                                 <parameterSet id="fixedid">
                                                     <booleanparameter name="dualColumnLayout">True</booleanparameter>
                                                     <booleanparameter name="isCorrectionVariant">True</booleanparameter>
                                                     <xhtmlparameter name="hottextInput">
                                                         <p id="fixedid" xmlns="http://www.w3.org/1999/xhtml">Dit is een <span id="fixedid" style="BACKGROUND-COLOR: #c7b8ce">hottext</span><span id="fixedid" style="BACKGROUND-COLOR: #c7b8ce">invoervak</span></p>
                                                         <p id="fixedid" xmlns="http://www.w3.org/1999/xhtml">
                                                             <span id="fixedid" style="background-color: #C7B8CE;">Dit is een zin.</span>
                                                         </p>
                                                         <p id="fixedid" xmlns="http://www.w3.org/1999/xhtml">
                                                             <span id="fixedid" style="background-color: #C7B8CE;">Dit is een alinea.</span>
                                                         </p>
                                                         <p id="fixedid" xmlns="http://www.w3.org/1999/xhtml">
                                                             <span id="fixedid" style="background-color: #C7B8CE;">Dit is vrij.</span>
                                                         </p>
                                                     </xhtmlparameter>
                                                     <hotTextScoringParameter name="hotTextScoring" ControllerId="hottextController" findingOverride="hottextController" minChoices="1" maxChoices="0" multiChoice="Check" isCorrectionVariant="true">
                                                         <definition id="fixedid">
                                                             <plaintextparameter name="contentLabel"/>
                                                         </definition>
                                                     </hotTextScoringParameter>
                                                     <integerparameter name="maxChoices">0</integerparameter>
                                                 </parameterSet>
                                             </parameters>
                                         </assessmentItem>
        Dim resourceManager = FakeDal.GetFakeResourceManager()
        Dim itm = CreateItem("HottextItem", ItemTestData.item_hottext_dc, "Cito.Generic.Hottext.DC")
        Dim templateSwitcher = New ItemTemplateSwitching(itm, resourceManager, _messageBoxService)
        Dim newAssessmentItem As AssessmentItem = Nothing

        'Act
        Dim result As Boolean = templateSwitcher.SwitchToTemplate("Cito.Generic.Hottext.Corrections.DC", newAssessmentItem)

        'Assert
        Assert.AreEqual(True, result, "Expected template to be switched.")
        Assert.AreEqual(True, UnitTestHelper.AreSame(itm, expectedResult), "Output is not as expected")
    End Sub

End Class
