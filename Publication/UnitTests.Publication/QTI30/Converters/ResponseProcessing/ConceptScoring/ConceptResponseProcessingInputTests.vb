
Imports System.Xml
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.HelperClasses
Imports Questify.Builder.UnitTests.Framework
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI30
Imports Questify.Builder.Logic.QTI.Converters.Processing.QTI30
Imports Questify.Builder.UnitTests.Publication.QTI30

Namespace QTI30

    <TestClass()>
    Public Class ConceptResponseProcessingInputTests

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
            Dim responseIdentifierAttributeList As XmlNodeList = PublicationTestHelper.GetResponseIdentifiers(itemBody)
            Dim conceptFinding As ConceptFinding = finding.Deserialize(Of ConceptFinding)()
            Dim processor = New ConceptResponseProcessing(responseIdentifierAttributeList, conceptFinding, scoringParameters, New CombinedScoringConverter())

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
                <qti-item-body class="defaultBody">
                    <div class="content">
                        <div>
                            <div id="questionwithinlinecontrol">
                                <p id="c1-id-11">
                                    <qti-text-entry-interaction pattern-mask="^-?([0-9]{1,5})?$" response-identifier="I3a005164-9453-4e00-ad76-4f5eefd1624c" expected-length="6"/> </p>
                            </div>
                            <div id="answer">

                            </div>
                        </div>
                    </div>
                </qti-item-body>
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
    <qti-response-processing>
        <qti-response-condition>
            <qti-response-if>
                <qti-equal tolerance-mode="exact">
                    <qti-variable identifier="RESPONSE"/>
                    <qti-base-value base-type="integer">1</qti-base-value>
                </qti-equal>
                <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                    <qti-base-value base-type="float">1</qti-base-value>
                </qti-set-outcome-value>
            </qti-response-if>
        </qti-response-condition>
    </qti-response-processing>

        Private _itemBody2 As XElement =
           <wrapper>
               <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
               <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
               <qti-item-body class="defaultBody">
                   <div class="content">
                       <div>
                           <div id="questionwithinlinecontrol">
                               <p id="c1-id-11">
                                   <qti-text-entry-interaction pattern-mask="^-?([0-9]{1,5})?$" response-identifier="I3a005164-9453-4e00-ad76-4f5eefd1624c" expected-length="6"/> </p>
                           </div>
                           <div id="answer">

                           </div>
                       </div>
                   </div>
               </qti-item-body>
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
    <qti-response-processing>
        <qti-response-condition>
            <qti-response-if>
                <qti-equal tolerance-mode="exact">
                    <qti-variable identifier="RESPONSE"/>
                    <qti-base-value base-type="integer">1</qti-base-value>
                </qti-equal>
                <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                    <qti-base-value base-type="float">1</qti-base-value>
                </qti-set-outcome-value>
            </qti-response-if>
            <qti-response-else-if>
                <qti-equal tolerance-mode="exact">
                    <qti-variable identifier="RESPONSE"/>
                    <qti-base-value base-type="integer">2</qti-base-value>
                </qti-equal>
                <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                    <qti-base-value base-type="float">1</qti-base-value>
                </qti-set-outcome-value>
            </qti-response-else-if>
        </qti-response-condition>
    </qti-response-processing>


        Private _itemBody3 As XElement =
            <wrapper>
                <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
                <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
                <qti-item-body class="defaultBody">
                    <div class="content">
                        <div>
                            <div id="questionwithinlinecontrol">
                                <p id="c1-id-11">
                                    <qti-text-entry-interaction pattern-mask="^-?([0-9]{1,5})?$" response-identifier="Ib98c50a4-f90f-4172-bb78-5b56aeb4d48a" expected-length="6"/> </p>
                            </div>
                            <div id="answer">

                            </div>
                        </div>
                    </div>
                </qti-item-body>
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
    <qti-response-processing>
        <qti-response-condition>
            <qti-response-if>
                <qti-equal tolerance-mode="exact">
                    <qti-variable identifier="RESPONSE"/>
                    <qti-base-value base-type="integer">1</qti-base-value>
                </qti-equal>
                <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                    <qti-base-value base-type="float">1</qti-base-value>
                </qti-set-outcome-value>
            </qti-response-if>
            <qti-response-else-if>
                <qti-equal tolerance-mode="exact">
                    <qti-variable identifier="RESPONSE"/>
                    <qti-base-value base-type="integer">2</qti-base-value>
                </qti-equal>
                <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                    <qti-base-value base-type="float">1</qti-base-value>
                </qti-set-outcome-value>
            </qti-response-else-if>
        </qti-response-condition>
    </qti-response-processing>

        Private _itemBody4 As XElement =
            <wrapper>
                <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
                <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
                <qti-item-body class="defaultBody">
                    <div class="content">
                        <div>
                            <div id="questionwithinlinecontrol">
                                <p id="c1-id-11">
                                    <span>
                                        <qti-text-entry-interaction pattern-mask="^(([0-1]?[0-9])|([2][0-3]))$" response-identifier="I0de6ade6-f82f-4111-b045-a3493f2d1ba6" expected-length="2" timeSubType="hhmm"/>
							  
								    :
								    
								    <qti-text-entry-interaction pattern-mask="^([0-5]?[0-9])$" response-identifier="I0de6ade6-f82f-4111-b045-a3493f2d1ba6" expected-length="2" timeSubType="hhmm"/>
                                    </span> </p>
                            </div>
                            <div id="answer">

                            </div>
                        </div>
                    </div>
                </qti-item-body>
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
    <qti-response-processing>
        <qti-response-condition>
            <qti-response-if>
                <qti-and>
                    <qti-equal toleranceMode="exact">
                        <qti-variable identifier="RESPONSE"/>
                        <qti-base-value base-type="integer">10</qti-base-value>
                    </qti-equal>
                    <qti-equal toleranceMode="exact">
                        <qti-variable identifier="RESPONSE2"/>
                        <qti-base-value base-type="integer">00</qti-base-value>
                    </qti-equal>
                </qti-and>
                <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                    <qti-base-value base-type="float">1</qti-base-value>
                </qti-set-outcome-value>
            </qti-response-if>
        </qti-response-condition>
    </qti-response-processing>

        Private _itemBody5 As XElement =
            <wrapper>
                <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
                <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
                <qti-item-body class="defaultBody">
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
                                            <qti-text-entry-interaction pattern-mask="^(?![0])(([0-2]?[0-9])|([3][0-1]))$" response-identifier="Id8de3e04-8635-463a-bf61-b1e4f4e93d32" expected-length="2" dateSubType="dutch"/>
								    
								        -
								    
							  
								    <qti-text-entry-interaction pattern-mask="^(?![0])(([0-9])|([1][0-2]))$" response-identifier="Id8de3e04-8635-463a-bf61-b1e4f4e93d32" expected-length="2" dateSubType="dutch"/>
								    
								        -
								    
							  
								    <qti-text-entry-interaction pattern-mask="^([0-9]{1,4})$" response-identifier="Id8de3e04-8635-463a-bf61-b1e4f4e93d32" expected-length="4" dateSubType="dutch"/>
                                        </span> </p>
                                </div>
                                <div id="answer">

                                </div>
                            </div>
                        </div>
                    </div>
                </qti-item-body>
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
    <qti-response-processing>
        <qti-response-condition>
            <qti-response-if>
                <qti-and>
                    <qti-equal tolerance-mode="exact">
                        <qti-variable identifier="RESPONSE"/>
                        <qti-base-value base-type="integer">08</qti-base-value>
                    </qti-equal>
                    <qti-equal tolerance-mode="exact">
                        <qti-variable identifier="RESPONSE2"/>
                        <qti-base-value base-type="integer">08</qti-base-value>
                    </qti-equal>
                    <qti-equal tolerance-mode="exact">
                        <qti-variable identifier="RESPONSE3"/>
                        <qti-base-value base-type="integer">2014</qti-base-value>
                    </qti-equal>
                </qti-and>
                <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                    <qti-base-value base-type="float">1</qti-base-value>
                </qti-set-outcome-value>
            </qti-response-if>
        </qti-response-condition>
    </qti-response-processing>

        Private _itemBody6 As XElement =
            <wrapper>
                <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
                <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
                <qti-item-body class="defaultBody">
                    <div class="content">
                        <div>
                            <div id="questionwithinlinecontrol">
                                <p id="c1-id-11">
                                    <qti-text-entry-interaction pattern-mask="^.{0,5}$" response-identifier="I9229bf5f-44ff-4a8f-b5b8-3ddc60746a8f" expected-length="5"/> </p>
                            </div>
                            <div id="answer">

                            </div>
                        </div>
                    </div>
                </qti-item-body>
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
    <qti-response-processing>
        <qti-response-condition>
            <qti-response-if>
                <qti-string-match case-sensitive="true">
                    <qti-variable identifier="RESPONSE"/>
                    <qti-base-value base-type="string">hallo</qti-base-value>
                </qti-string-match>
                <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                    <qti-base-value base-type="float">1</qti-base-value>
                </qti-set-outcome-value>
            </qti-response-if>
        </qti-response-condition>
    </qti-response-processing>

        Private _itemBody7 As XElement =
            <wrapper>
                <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
                <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
                <qti-item-body class="defaultBody">
                    <div class="content">
                        <div>
                            <div id="questionwithinlinecontrol">
                                <p id="c1-id-11">
                                    <qti-text-entry-interaction pattern-mask="^.{0,5}$" response-identifier="I9229bf5f-44ff-4a8f-b5b8-3ddc60746a8f" expected-length="5"/> </p>
                            </div>
                            <div id="answer">

                            </div>
                        </div>
                    </div>
                </qti-item-body>
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
    <qti-response-processing>
        <qti-response-condition>
            <qti-response-if>
                <qti-string-match case-sensitive="true">
                    <qti-variable identifier="RESPONSE"/>
                    <qti-base-value base-type="string">hallo</qti-base-value>
                </qti-string-match>
                <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                    <qti-base-value base-type="float">1</qti-base-value>
                </qti-set-outcome-value>
            </qti-response-if>
        </qti-response-condition>
    </qti-response-processing>

        Private _itemBody8 As XElement =
            <wrapper>
                <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
                <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
                <qti-item-body class="defaultBody">
                    <div class="content">
                        <div>
                            <div id="questionwithinlinecontrol">
                                <p id="c1-id-11">
                                    <qti-text-entry-interaction pattern-mask="^-?([0-9]{1,5})?$" response-identifier="Ib98c50a4-f90f-4172-bb78-5b56aeb4d48a" expected-length="6"/> <qti-text-entry-interaction pattern-mask="^-?([0-9]{1,5})?$" response-identifier="Id6a6efe0-8072-46cf-9281-cb10dbeb29fe" expected-length="6"/>
                                </p>
                            </div>
                            <div id="answer">

                            </div>
                        </div>
                    </div>
                </qti-item-body>
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
    <qti-response-processing>
        <qti-response-condition>
            <qti-response-if>
                <qti-and>
                    <qti-equal tolerance-mode="exact">
                        <qti-variable identifier="RESPONSE"/>
                        <qti-base-value base-type="integer">1</qti-base-value>
                    </qti-equal>
                    <qti-equal tolerance-mode="exact">
                        <qti-variable identifier="RESPONSE2"/>
                        <qti-base-value base-type="integer">2</qti-base-value>
                    </qti-equal>
                </qti-and>
                <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                    <qti-base-value base-type="float">1</qti-base-value>
                </qti-set-outcome-value>
            </qti-response-if>
        </qti-response-condition>
    </qti-response-processing>

        ReadOnly _itemBody9 As XElement =
            <wrapper>
                <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
                <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
                <qti-item-body class="defaultBody">
                    <div class="content">
                        <div>
                            <div id="questionwithinlinecontrol">
                                <p id="c1-id-11">
                                    <qti-text-entry-interaction pattern-mask="^-?([0-9]{1,5})?$" response-identifier="Ib015b492-8dde-4cf9-9a3e-f4c80a9a012d" expected-length="6"/> <qti-text-entry-interaction pattern-mask="^-?([0-9]{1,5})?$" response-identifier="I3b39780f-00c6-4b3a-9097-eb9bdf71f6fd" expected-length="6"/>
                                </p>
                            </div>
                            <div id="answer">

                            </div>
                        </div>
                    </div>
                </qti-item-body>
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
    <qti-response-processing>
        <qti-response-condition>
            <qti-response-if>
                <qti-and>
                    <qti-equal tolerance-mode="exact">
                        <qti-variable identifier="RESPONSE"/>
                        <qti-base-value base-type="integer">1</qti-base-value>
                    </qti-equal>
                    <qti-equal tolerance-mode="exact">
                        <qti-variable identifier="RESPONSE2"/>
                        <qti-base-value base-type="integer">2</qti-base-value>
                    </qti-equal>
                </qti-and>
                <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                    <qti-base-value base-type="float">1</qti-base-value>
                </qti-set-outcome-value>
            </qti-response-if>
            <qti-response-else-if>
                <qti-and>
                    <qti-equal tolerance-mode="exact">
                        <qti-variable identifier="RESPONSE"/>
                        <qti-base-value base-type="integer">2</qti-base-value>
                    </qti-equal>
                    <qti-equal tolerance-mode="exact">
                        <qti-variable identifier="RESPONSE2"/>
                        <qti-base-value base-type="integer">1</qti-base-value>
                    </qti-equal>
                </qti-and>
                <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                    <qti-base-value base-type="float">2</qti-base-value>
                </qti-set-outcome-value>
            </qti-response-else-if>
            <qti-response-else-if>
                <qti-and>
                    <qti-equal tolerance-mode="exact">
                        <qti-variable identifier="RESPONSE"/>
                        <qti-base-value base-type="integer">3</qti-base-value>
                    </qti-equal>
                    <qti-equal tolerance-mode="exact">
                        <qti-variable identifier="RESPONSE2"/>
                        <qti-base-value base-type="integer">0</qti-base-value>
                    </qti-equal>
                </qti-and>
                <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                    <qti-base-value base-type="float">3</qti-base-value>
                </qti-set-outcome-value>
            </qti-response-else-if>
        </qti-response-condition>
    </qti-response-processing>

    End Class

End Namespace
