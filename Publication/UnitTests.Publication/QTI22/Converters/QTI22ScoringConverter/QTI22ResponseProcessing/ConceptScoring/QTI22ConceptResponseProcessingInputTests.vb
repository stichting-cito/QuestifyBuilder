
Imports System.Xml
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.HelperClasses
Imports Questify.Builder.UnitTests.Framework
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI22
Imports Questify.Builder.Logic.QTI.Converters.Processing.QTI22

<TestClass()>
Public Class QTI22ConceptResponseProcessingInputTests

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
    Public Sub IntegerOneConceptOneFactTest()

        Dim scoringParameters As New HashSet(Of ScoringParameter)()
        Dim param1 As ScoringParameter = New IntegerScoringParameter() With {.ControllerId = "gapController", .FindingOverride = "gapController", .InlineId = "I3a005164-9453-4e00-ad76-4f5eefd1624c"}.AddSubParameters("1")
        scoringParameters.Add(param1)

        ExecuteConceptResponseProcessingTest(_itemBody1, scoringParameters, _finding1, _responseProcessing1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
    Public Sub IntegerOneConceptTwoFactsTest()

        Dim scoringParameters As New HashSet(Of ScoringParameter)()
        Dim param1 As ScoringParameter = New IntegerScoringParameter() With {.ControllerId = "gapController", .FindingOverride = "gapController", .InlineId = "I3a005164-9453-4e00-ad76-4f5eefd1624c"}.AddSubParameters("1")
        scoringParameters.Add(param1)

        ExecuteConceptResponseProcessingTest(_itemBody2, scoringParameters, _finding2, _responseProcessing2)

    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
    Public Sub IntegerTwoConceptsTwoFactsTest()

        Dim scoringParameters As New HashSet(Of ScoringParameter)()
        Dim param1 As ScoringParameter = New IntegerScoringParameter() With {.ControllerId = "gapController", .FindingOverride = "gapController", .InlineId = "Ib98c50a4-f90f-4172-bb78-5b56aeb4d48a"}.AddSubParameters("1")
        scoringParameters.Add(param1)

        ExecuteConceptResponseProcessingTest(_itemBody3, scoringParameters, _finding3, _responseProcessing3)

    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
    Public Sub TimeOneConceptTest()

        Dim scoringParameters As New HashSet(Of ScoringParameter)()
        Dim param1 As ScoringParameter = New TimeScoringParameter() With {.ControllerId = "gapController", .FindingOverride = "gapController", .InlineId = "I0de6ade6-f82f-4111-b045-a3493f2d1ba6", .TimeFormat = "hh:mm"}.AddSubParameters("1")
        scoringParameters.Add(param1)

        ExecuteConceptResponseProcessingTest(_itemBody4, scoringParameters, _finding4, _responseProcessing4)

    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
    Public Sub DateOneConceptTest()

        Dim scoringParameters As New HashSet(Of ScoringParameter)()
        Dim param1 As ScoringParameter = New DateScoringParameter() With {.ControllerId = "gapController", .FindingOverride = "gapController", .InlineId = "Id8de3e04-8635-463a-bf61-b1e4f4e93d32", .DateFormat = "dd-MM-yyyy"}.AddSubParameters("1")
        scoringParameters.Add(param1)

        ExecuteConceptResponseProcessingTest(_itemBody5, scoringParameters, _finding5, _responseProcessing5)

    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
    Public Sub StringOneConceptTest()

        Dim scoringParameters As New HashSet(Of ScoringParameter)()
        Dim param1 As ScoringParameter = New StringScoringParameter() With {.ControllerId = "gapController", .FindingOverride = "gapController", .InlineId = "I9229bf5f-44ff-4a8f-b5b8-3ddc60746a8f"}.AddSubParameters("1")
        scoringParameters.Add(param1)

        ExecuteConceptResponseProcessingTest(_itemBody6, scoringParameters, _finding6, _responseProcessing6)

    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
    Public Sub StringPreprocessingRulesOneConceptTest()

        Dim scoringParameters As New HashSet(Of ScoringParameter)()
        Dim param1 As ScoringParameter = New StringScoringParameter() With {.ControllerId = "gapController", .FindingOverride = "gapController", .InlineId = "I9229bf5f-44ff-4a8f-b5b8-3ddc60746a8f"}.AddSubParameters("1")
        scoringParameters.Add(param1)

        ExecuteConceptResponseProcessingTest(_itemBody7, scoringParameters, _finding7, _responseProcessing7)

    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
    Public Sub IntegerTwoFactSetsOneConceptTest()

        Dim scoringParameters As New HashSet(Of ScoringParameter)()
        scoringParameters.Add(New IntegerScoringParameter() With {.ControllerId = "gapController", .FindingOverride = "gapController", .InlineId = "Ib98c50a4-f90f-4172-bb78-5b56aeb4d48a"}.AddSubParameters("1"))
        scoringParameters.Add(New IntegerScoringParameter() With {.ControllerId = "gapController", .FindingOverride = "gapController", .InlineId = "Id6a6efe0-8072-46cf-9281-cb10dbeb29fe"}.AddSubParameters("1"))

        ExecuteConceptResponseProcessingTest(_itemBody8, scoringParameters, _finding8, _responseProcessing8)

    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
    Public Sub IntegerFourFactSetsOneConceptTest()

        Dim scoringParameters As New HashSet(Of ScoringParameter)()
        scoringParameters.Add(New IntegerScoringParameter() With {.ControllerId = "gapController", .FindingOverride = "gapController", .InlineId = "Ib015b492-8dde-4cf9-9a3e-f4c80a9a012d"}.AddSubParameters("1"))
        scoringParameters.Add(New IntegerScoringParameter() With {.ControllerId = "gapController", .FindingOverride = "gapController", .InlineId = "I3b39780f-00c6-4b3a-9097-eb9bdf71f6fd"}.AddSubParameters("1"))

        ExecuteConceptResponseProcessingTest(_itemBody9, scoringParameters, _finding9, _responseProcessing9)

    End Sub

    Private Sub ExecuteConceptResponseProcessingTest(itemBody As XElement, scoringParameters As HashSet(Of ScoringParameter), finding As XElement, processing As XElement)

        'Arrange
        Dim responseIdentifierAttributeList As XmlNodeList = QTI22PublicationTestHelper.GetResponseIdentifiers(itemBody)
        Dim conceptFinding As ConceptFinding = finding.Deserialize(Of ConceptFinding)()
        Dim processor = New QTI22ConceptResponseProcessing(responseIdentifierAttributeList, conceptFinding, scoringParameters, New QTI22CombinedScoringConverter())

        'Act
        Dim result = processor.GetProcessing(False)

        'Assert
        Dim expected = New XDocument(processing)
        Dim actual = New XDocument(result)
        Assert.IsTrue(UnitTestHelper.AreSame(expected, actual))
    End Sub

    Private _itemBody1 As XElement =
        <wrapper>
            <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
            <itemBody class="defaultBody">
                <div class="content">
                    <div>
                        <div id="questionwithinlinecontrol">
                            <p id="c1-id-11">
                                <textEntryInteraction patternMask="^-?([0-9]{1,5})?$" responseIdentifier="I3a005164-9453-4e00-ad76-4f5eefd1624c" expectedLength="6"/> </p>
                        </div>
                        <div id="answer">

                        </div>
                    </div>
                </div>
            </itemBody>
        </wrapper>

    Private _finding1 As XElement =
        <conceptFinding id="gapController" scoringMethod="None">
            <conceptFact id="1-I3a005164-9453-4e00-ad76-4f5eefd1624c" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I3a005164-9453-4e00-ad76-4f5eefd1624c" occur="1">
                    <integerValue>
                        <typedValue>1</typedValue>
                    </integerValue>
                </conceptValue>
                <concepts>
                    <concept value="1" code="Concept2.1"/>
                </concepts>
            </conceptFact>
        </conceptFinding>

    Private _responseProcessing1 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <equal toleranceMode="exact">
                        <variable identifier="RESPONSE"/>
                        <baseValue baseType="integer">1</baseValue>
                    </equal>
                    <setOutcomeValue identifier="CONCEPTRESPONSE_Concept2-1">
                        <baseValue baseType="float">1</baseValue>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
        </responseProcessing>

    Private _itemBody2 As XElement =
       <wrapper>
           <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
           <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
           <itemBody class="defaultBody">
               <div class="content">
                   <div>
                       <div id="questionwithinlinecontrol">
                           <p id="c1-id-11">
                               <textEntryInteraction patternMask="^-?([0-9]{1,5})?$" responseIdentifier="I3a005164-9453-4e00-ad76-4f5eefd1624c" expectedLength="6"/> </p>
                       </div>
                       <div id="answer">

                       </div>
                   </div>
               </div>
           </itemBody>
       </wrapper>

    Private _finding2 As XElement =
        <conceptFinding id="gapController" scoringMethod="None">
            <conceptFact id="1-I3a005164-9453-4e00-ad76-4f5eefd1624c" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I3a005164-9453-4e00-ad76-4f5eefd1624c" occur="1">
                    <integerValue>
                        <typedValue>1</typedValue>
                    </integerValue>
                </conceptValue>
                <concepts>
                    <concept value="1" code="Concept2.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="1[1]-I3a005164-9453-4e00-ad76-4f5eefd1624c" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I3a005164-9453-4e00-ad76-4f5eefd1624c" occur="1">
                    <integerValue>
                        <typedValue>2</typedValue>
                    </integerValue>
                </conceptValue>
                <concepts>
                    <concept value="1" code="Concept2.1"/>
                </concepts>
            </conceptFact>
        </conceptFinding>

    Private _responseProcessing2 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <equal toleranceMode="exact">
                        <variable identifier="RESPONSE"/>
                        <baseValue baseType="integer">1</baseValue>
                    </equal>
                    <setOutcomeValue identifier="CONCEPTRESPONSE_Concept2-1">
                        <baseValue baseType="float">1</baseValue>
                    </setOutcomeValue>
                </responseIf>
                <responseElseIf>
                    <equal toleranceMode="exact">
                        <variable identifier="RESPONSE"/>
                        <baseValue baseType="integer">2</baseValue>
                    </equal>
                    <setOutcomeValue identifier="CONCEPTRESPONSE_Concept2-1">
                        <baseValue baseType="float">1</baseValue>
                    </setOutcomeValue>
                </responseElseIf>
            </responseCondition>
        </responseProcessing>


    Private _itemBody3 As XElement =
        <wrapper>
            <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
            <itemBody class="defaultBody">
                <div class="content">
                    <div>
                        <div id="questionwithinlinecontrol">
                            <p id="c1-id-11">
                                <textEntryInteraction patternMask="^-?([0-9]{1,5})?$" responseIdentifier="Ib98c50a4-f90f-4172-bb78-5b56aeb4d48a" expectedLength="6"/> </p>
                        </div>
                        <div id="answer">

                        </div>
                    </div>
                </div>
            </itemBody>
        </wrapper>

    Private _finding3 As XElement =
        <conceptFinding id="gapController" scoringMethod="None">
            <conceptFact id="1-Ib98c50a4-f90f-4172-bb78-5b56aeb4d48a" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="Ib98c50a4-f90f-4172-bb78-5b56aeb4d48a" occur="1">
                    <integerValue>
                        <typedValue>1</typedValue>
                    </integerValue>
                </conceptValue>
                <concepts>
                    <concept value="1" code="Concept1.1"/>
                    <concept value="0" code="Concept1.2"/>
                </concepts>
            </conceptFact>
            <conceptFact id="1[1]-Ib98c50a4-f90f-4172-bb78-5b56aeb4d48a" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="Ib98c50a4-f90f-4172-bb78-5b56aeb4d48a" occur="1">
                    <integerValue>
                        <typedValue>2</typedValue>
                    </integerValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept1.1"/>
                    <concept value="1" code="Concept1.2"/>
                </concepts>
            </conceptFact>
        </conceptFinding>

    Private _responseProcessing3 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <equal toleranceMode="exact">
                        <variable identifier="RESPONSE"/>
                        <baseValue baseType="integer">1</baseValue>
                    </equal>
                    <setOutcomeValue identifier="CONCEPTRESPONSE_Concept1-1">
                        <baseValue baseType="float">1</baseValue>
                    </setOutcomeValue>
                </responseIf>
                <responseElseIf>
                    <equal toleranceMode="exact">
                        <variable identifier="RESPONSE"/>
                        <baseValue baseType="integer">2</baseValue>
                    </equal>
                    <setOutcomeValue identifier="CONCEPTRESPONSE_Concept1-2">
                        <baseValue baseType="float">1</baseValue>
                    </setOutcomeValue>
                </responseElseIf>
            </responseCondition>
        </responseProcessing>

    Private _itemBody4 As XElement =
        <wrapper>
            <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
            <itemBody class="defaultBody">
                <div class="content">
                    <div>
                        <div id="questionwithinlinecontrol">
                            <p id="c1-id-11">
                                <span>
                                    <textEntryInteraction patternMask="^(([0-1]?[0-9])|([2][0-3]))$" responseIdentifier="I0de6ade6-f82f-4111-b045-a3493f2d1ba6" expectedLength="2" timeSubType="hhmm"/>
							  
								    :
								    
								    <textEntryInteraction patternMask="^([0-5]?[0-9])$" responseIdentifier="I0de6ade6-f82f-4111-b045-a3493f2d1ba6" expectedLength="2" timeSubType="hhmm"/>
                                </span> </p>
                        </div>
                        <div id="answer">

                        </div>
                    </div>
                </div>
            </itemBody>
        </wrapper>

    Private _finding4 As XElement =
        <conceptFinding id="gapController" scoringMethod="None">
            <conceptFact id="1-I0de6ade6-f82f-4111-b045-a3493f2d1ba6" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I0de6ade6-f82f-4111-b045-a3493f2d1ba6" occur="1">
                    <stringValue>
                        <typedValue>10:00</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="1" code="Concept2.1"/>
                </concepts>
            </conceptFact>
        </conceptFinding>

    Private _responseProcessing4 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <and>
                        <equal toleranceMode="exact">
                            <variable identifier="RESPONSE"/>
                            <baseValue baseType="integer">10</baseValue>
                        </equal>
                        <equal toleranceMode="exact">
                            <variable identifier="RESPONSE2"/>
                            <baseValue baseType="integer">00</baseValue>
                        </equal>
                    </and>
                    <setOutcomeValue identifier="CONCEPTRESPONSE_Concept2-1">
                        <baseValue baseType="float">1</baseValue>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
        </responseProcessing>

    Private _itemBody5 As XElement =
        <wrapper>
            <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
            <itemBody class="defaultBody">
                <div class="content">
                    <div class="div_left">
                        <!-- div_left_inner is needed to create the 50px margin between the left and right columns in dual column mode -->
                        <div class="div_left_inner">

                        </div>
                    </div>
                    <div class="div_right">
                        <!-- div_right_inner is needed to create the 50px margin between the left and right columns in dual column mode -->
                        <div class="div_right_inner">
                            <div id="questionwithinlinecontrol">
                                <p id="c1-id-11">
                                    <span>
                                        <textEntryInteraction patternMask="^(?![0])(([0-2]?[0-9])|([3][0-1]))$" responseIdentifier="Id8de3e04-8635-463a-bf61-b1e4f4e93d32" expectedLength="2" dateSubType="dutch"/>
								    
								        -
								    
							  
								    <textEntryInteraction patternMask="^(?![0])(([0-9])|([1][0-2]))$" responseIdentifier="Id8de3e04-8635-463a-bf61-b1e4f4e93d32" expectedLength="2" dateSubType="dutch"/>
								    
								        -
								    
							  
								    <textEntryInteraction patternMask="^([0-9]{1,4})$" responseIdentifier="Id8de3e04-8635-463a-bf61-b1e4f4e93d32" expectedLength="4" dateSubType="dutch"/>
                                    </span> </p>
                            </div>
                            <div id="answer">

                            </div>
                        </div>
                    </div>
                </div>
            </itemBody>
        </wrapper>

    Private _finding5 As XElement =
        <conceptFinding id="gapController" scoringMethod="None">
            <conceptFact id="1-Id8de3e04-8635-463a-bf61-b1e4f4e93d32" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="Id8de3e04-8635-463a-bf61-b1e4f4e93d32" occur="1">
                    <stringValue>
                        <typedValue>08/08/2014</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="1" code="Concept2.1"/>
                </concepts>
            </conceptFact>
        </conceptFinding>

    Private _responseProcessing5 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <and>
                        <equal toleranceMode="exact">
                            <variable identifier="RESPONSE"/>
                            <baseValue baseType="integer">08</baseValue>
                        </equal>
                        <equal toleranceMode="exact">
                            <variable identifier="RESPONSE2"/>
                            <baseValue baseType="integer">08</baseValue>
                        </equal>
                        <equal toleranceMode="exact">
                            <variable identifier="RESPONSE3"/>
                            <baseValue baseType="integer">2014</baseValue>
                        </equal>
                    </and>
                    <setOutcomeValue identifier="CONCEPTRESPONSE_Concept2-1">
                        <baseValue baseType="float">1</baseValue>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
        </responseProcessing>

    Private _itemBody6 As XElement =
        <wrapper>
            <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
            <itemBody class="defaultBody">
                <div class="content">
                    <div>
                        <div id="questionwithinlinecontrol">
                            <p id="c1-id-11">
                                <textEntryInteraction patternMask="^.{0,5}$" responseIdentifier="I9229bf5f-44ff-4a8f-b5b8-3ddc60746a8f" expectedLength="5"/> </p>
                        </div>
                        <div id="answer">

                        </div>
                    </div>
                </div>
            </itemBody>
        </wrapper>

    Private _finding6 As XElement =
        <conceptFinding id="gapController" scoringMethod="None">
            <conceptFact id="1-I9229bf5f-44ff-4a8f-b5b8-3ddc60746a8f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I9229bf5f-44ff-4a8f-b5b8-3ddc60746a8f" occur="1">
                    <stringValue>
                        <typedValue>hallo</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="1" code="Concept2-1"/>
                </concepts>
            </conceptFact>
        </conceptFinding>

    Private _responseProcessing6 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <stringMatch caseSensitive="true">
                        <variable identifier="RESPONSE"/>
                        <baseValue baseType="string">hallo</baseValue>
                    </stringMatch>
                    <setOutcomeValue identifier="CONCEPTRESPONSE_Concept2-1">
                        <baseValue baseType="float">1</baseValue>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
        </responseProcessing>

    Private _itemBody7 As XElement =
        <wrapper>
            <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
            <itemBody class="defaultBody">
                <div class="content">
                    <div>
                        <div id="questionwithinlinecontrol">
                            <p id="c1-id-11">
                                <textEntryInteraction patternMask="^.{0,5}$" responseIdentifier="I9229bf5f-44ff-4a8f-b5b8-3ddc60746a8f" expectedLength="5"/> </p>
                        </div>
                        <div id="answer">

                        </div>
                    </div>
                </div>
            </itemBody>
        </wrapper>

    Private _finding7 As XElement =
        <conceptFinding id="gapController" scoringMethod="None">
            <conceptFact id="1-I9229bf5f-44ff-4a8f-b5b8-3ddc60746a8f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I9229bf5f-44ff-4a8f-b5b8-3ddc60746a8f" occur="1">
                    <stringValue>
                        <typedValue>hallo</typedValue>
                    </stringValue>
                    <preprocessingRule>
                        <ruleName>HLKL</ruleName>
                    </preprocessingRule>
                </conceptValue>
                <concepts>
                    <concept value="1" code="Concept2.1"/>
                </concepts>
            </conceptFact>
        </conceptFinding>

    Private _responseProcessing7 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <stringMatch caseSensitive="true">
                        <variable identifier="RESPONSE"/>
                        <baseValue baseType="string">hallo</baseValue>
                    </stringMatch>
                    <setOutcomeValue identifier="CONCEPTRESPONSE_Concept2-1">
                        <baseValue baseType="float">1</baseValue>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
        </responseProcessing>

    Private _itemBody8 As XElement =
        <wrapper>
            <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
            <itemBody class="defaultBody">
                <div class="content">
                    <div>
                        <div id="questionwithinlinecontrol">
                            <p id="c1-id-11">
                                <textEntryInteraction patternMask="^-?([0-9]{1,5})?$" responseIdentifier="Ib98c50a4-f90f-4172-bb78-5b56aeb4d48a" expectedLength="6"/> <textEntryInteraction patternMask="^-?([0-9]{1,5})?$" responseIdentifier="Id6a6efe0-8072-46cf-9281-cb10dbeb29fe" expectedLength="6"/>
                            </p>
                        </div>
                        <div id="answer">

                        </div>
                    </div>
                </div>
            </itemBody>
        </wrapper>

    Private _finding8 As XElement =
        <conceptFinding id="gapController" scoringMethod="None">
            <conceptFactSet>
                <conceptFact id="1-Ib98c50a4-f90f-4172-bb78-5b56aeb4d48a" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ib98c50a4-f90f-4172-bb78-5b56aeb4d48a" occur="1">
                        <integerValue>
                            <typedValue>1</typedValue>
                        </integerValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="1-Id6a6efe0-8072-46cf-9281-cb10dbeb29fe" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Id6a6efe0-8072-46cf-9281-cb10dbeb29fe" occur="1">
                        <integerValue>
                            <typedValue>2</typedValue>
                        </integerValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="1" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
        </conceptFinding>

    Private _responseProcessing8 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <and>
                        <equal toleranceMode="exact">
                            <variable identifier="RESPONSE"/>
                            <baseValue baseType="integer">1</baseValue>
                        </equal>
                        <equal toleranceMode="exact">
                            <variable identifier="RESPONSE2"/>
                            <baseValue baseType="integer">2</baseValue>
                        </equal>
                    </and>
                    <setOutcomeValue identifier="CONCEPTRESPONSE_Concept2-1">
                        <baseValue baseType="float">1</baseValue>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
        </responseProcessing>

    ReadOnly _itemBody9 As XElement =
        <wrapper>
            <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
            <itemBody class="defaultBody">
                <div class="content">
                    <div>
                        <div id="questionwithinlinecontrol">
                            <p id="c1-id-11">
                                <textEntryInteraction patternMask="^-?([0-9]{1,5})?$" responseIdentifier="Ib015b492-8dde-4cf9-9a3e-f4c80a9a012d" expectedLength="6"/> <textEntryInteraction patternMask="^-?([0-9]{1,5})?$" responseIdentifier="I3b39780f-00c6-4b3a-9097-eb9bdf71f6fd" expectedLength="6"/>
                            </p>
                        </div>
                        <div id="answer">

                        </div>
                    </div>
                </div>
            </itemBody>
        </wrapper>

    ReadOnly _finding9 As XElement =
        <conceptFinding id="gapController" scoringMethod="None">
            <conceptFactSet>
                <conceptFact id="1-Ib015b492-8dde-4cf9-9a3e-f4c80a9a012d" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ib015b492-8dde-4cf9-9a3e-f4c80a9a012d" occur="1">
                        <integerValue>
                            <typedValue>1</typedValue>
                        </integerValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="1-I3b39780f-00c6-4b3a-9097-eb9bdf71f6fd" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I3b39780f-00c6-4b3a-9097-eb9bdf71f6fd" occur="1">
                        <integerValue>
                            <typedValue>2</typedValue>
                        </integerValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="1" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="1-Ib015b492-8dde-4cf9-9a3e-f4c80a9a012d" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ib015b492-8dde-4cf9-9a3e-f4c80a9a012d" occur="1">
                        <integerValue>
                            <typedValue>2</typedValue>
                        </integerValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="1-I3b39780f-00c6-4b3a-9097-eb9bdf71f6fd" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I3b39780f-00c6-4b3a-9097-eb9bdf71f6fd" occur="1">
                        <integerValue>
                            <typedValue>1</typedValue>
                        </integerValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="2" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="1[*]-Ib015b492-8dde-4cf9-9a3e-f4c80a9a012d" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ib015b492-8dde-4cf9-9a3e-f4c80a9a012d" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="1[*]-I3b39780f-00c6-4b3a-9097-eb9bdf71f6fd" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I3b39780f-00c6-4b3a-9097-eb9bdf71f6fd" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="0" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="1[3]-Ib015b492-8dde-4cf9-9a3e-f4c80a9a012d" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ib015b492-8dde-4cf9-9a3e-f4c80a9a012d" occur="1">
                        <integerValue>
                            <typedValue>3</typedValue>
                        </integerValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="1[3]-I3b39780f-00c6-4b3a-9097-eb9bdf71f6fd" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I3b39780f-00c6-4b3a-9097-eb9bdf71f6fd" occur="1">
                        <integerValue>
                            <typedValue>0</typedValue>
                        </integerValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="3" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
        </conceptFinding>

    ReadOnly _responseProcessing9 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <and>
                        <equal toleranceMode="exact">
                            <variable identifier="RESPONSE"/>
                            <baseValue baseType="integer">1</baseValue>
                        </equal>
                        <equal toleranceMode="exact">
                            <variable identifier="RESPONSE2"/>
                            <baseValue baseType="integer">2</baseValue>
                        </equal>
                    </and>
                    <setOutcomeValue identifier="CONCEPTRESPONSE_Concept2-1">
                        <baseValue baseType="float">1</baseValue>
                    </setOutcomeValue>
                </responseIf>
                <responseElseIf>
                    <and>
                        <equal toleranceMode="exact">
                            <variable identifier="RESPONSE"/>
                            <baseValue baseType="integer">2</baseValue>
                        </equal>
                        <equal toleranceMode="exact">
                            <variable identifier="RESPONSE2"/>
                            <baseValue baseType="integer">1</baseValue>
                        </equal>
                    </and>
                    <setOutcomeValue identifier="CONCEPTRESPONSE_Concept2-1">
                        <baseValue baseType="float">2</baseValue>
                    </setOutcomeValue>
                </responseElseIf>
                <responseElseIf>
                    <and>
                        <equal toleranceMode="exact">
                            <variable identifier="RESPONSE"/>
                            <baseValue baseType="integer">3</baseValue>
                        </equal>
                        <equal toleranceMode="exact">
                            <variable identifier="RESPONSE2"/>
                            <baseValue baseType="integer">0</baseValue>
                        </equal>
                    </and>
                    <setOutcomeValue identifier="CONCEPTRESPONSE_Concept2-1">
                        <baseValue baseType="float">3</baseValue>
                    </setOutcomeValue>
                </responseElseIf>
            </responseCondition>
        </responseProcessing>


End Class
