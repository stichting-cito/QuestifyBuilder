
<TestClass()>
Public Class QTI22ResponseDeclarationTests
    Inherits QTI22ResponseDeclarationTests_Base

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub InlineChoiceTest()
        RunResponseDeclarationTest(_itemBody1, _solution1, Nothing, _result1, 2)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub InlineGapTimeAlternativesTest()
        RunResponseDeclarationTest(_itemBody2, _solution2, Nothing, _result2, 4)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub InlineGapTimeAlternativesFactSetsTest()
        RunResponseDeclarationTest(_itemBody3, _solution3, Nothing, _result3, 4)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub InlineGapIntegerAlternativesRangeFactSetTest()
        RunResponseDeclarationTest(_itemBody4, _solution4, Nothing, _result4, 3)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub InlineGapDateAlternativesTimeFactSetTest()
        RunResponseDeclarationTest(_itemBody5, _solution5, Nothing, _result5, 5)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub MultipleResponseTest()
        RunResponseDeclarationTest(_itemBody6, _solution6, Nothing, _result6, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub MultipleResponseTest_II()
        RunResponseDeclarationTest(_itemBody7, _solution7, Nothing, _result7, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub CombinedInlineWithMultipleChoiceWithoutScoringParameterTest()
        RunResponseDeclarationTest(_itemBody8, _solution8, Nothing, _result8, 3)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub MultipleGapsWithoutScoringParameterTest()
        RunResponseDeclarationTest(_itemBody9, _solution9, Nothing, _result9, 6)
    End Sub


    ReadOnly _itemBody1 As XElement =
        <wrapper>
            <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
            <itemBody class="defaultBody">
                <div class="content">
                    <div>
                        <div id="itemquestion">
                            <p id="c1-id-11">Welke ?</p>
                        </div>
                        <div id="questionwithinlinecontrol">
                            <p id="c1-id-11">
                                <inlineChoiceInteraction responseIdentifier="I67632965-d307-4fb3-a4b0-1107e5af0085" shuffle="false" required="true">
                                    <inlineChoice identifier="A">100</inlineChoice>
                                    <inlineChoice identifier="B">200</inlineChoice>
                                </inlineChoiceInteraction> <inlineChoiceInteraction responseIdentifier="I440d2525-0615-4521-ab71-17f9473e15d5" shuffle="false" required="true">
                                    <inlineChoice identifier="A">C</inlineChoice>
                                    <inlineChoice identifier="B">CC</inlineChoice>
                                </inlineChoiceInteraction>
                            </p>
                        </div>
                        <div id="answer">

                        </div>
                    </div>
                </div>
            </itemBody>
        </wrapper>

    ReadOnly _itemBody2 As XElement =
        <wrapper>
            <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
            <itemBody class="defaultBody">
                <div class="content">
                    <div>
                        <div id="questionwithinlinecontrol">
                            <p id="c1-id-11">
                                <span>
                                    <textEntryInteraction patternMask="^(([0-1]?[0-9])|([2][0-3]))$" responseIdentifier="Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" expectedLength="2" timeSubType="hhmm"/>
							  
								    :
								    
								    <textEntryInteraction patternMask="^([0-5]?[0-9])$" responseIdentifier="Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" expectedLength="2" timeSubType="hhmm"/>
                                </span> <span>
                                    <textEntryInteraction patternMask="^(([0-1]?[0-9])|([2][0-3]))$" responseIdentifier="I0b525f2d-804e-42a1-b8da-9abbd6d0f6ae" expectedLength="2" timeSubType="hhmm"/>
							  
								    :
								    
								    <textEntryInteraction patternMask="^([0-5]?[0-9])$" responseIdentifier="I0b525f2d-804e-42a1-b8da-9abbd6d0f6ae" expectedLength="2" timeSubType="hhmm"/>
                                </span> </p>
                        </div>
                        <div id="answer">

                        </div>
                    </div>
                </div>
            </itemBody>
        </wrapper>

    ReadOnly _itemBody3 As XElement =
        <wrapper>
            <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
            <itemBody class="defaultBody">
                <div class="content">
                    <div>
                        <div id="questionwithinlinecontrol">
                            <p id="c1-id-11">
                                <span>
                                    <textEntryInteraction patternMask="^(([0-1]?[0-9])|([2][0-3]))$" responseIdentifier="Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" expectedLength="2" timeSubType="hhmm"/>
							  
								    :
								    
								    <textEntryInteraction patternMask="^([0-5]?[0-9])$" responseIdentifier="Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" expectedLength="2" timeSubType="hhmm"/>
                                </span> <span>
                                    <textEntryInteraction patternMask="^(([0-1]?[0-9])|([2][0-3]))$" responseIdentifier="I0b525f2d-804e-42a1-b8da-9abbd6d0f6ae" expectedLength="2" timeSubType="hhmm"/>
							  
								    :
								    
								    <textEntryInteraction patternMask="^([0-5]?[0-9])$" responseIdentifier="I0b525f2d-804e-42a1-b8da-9abbd6d0f6ae" expectedLength="2" timeSubType="hhmm"/>
                                </span> </p>
                        </div>
                        <div id="answer">

                        </div>
                    </div>
                </div>
            </itemBody>
        </wrapper>

    ReadOnly _itemBody4 As XElement =
        <wrapper>
            <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
            <itemBody class="defaultBody">
                <div class="content">
                    <div>
                        <div id="itemquestion">
                            <p id="c1-id-11">Welke 3?</p>
                        </div>
                        <div id="questionwithinlinecontrol">
                            <p id="c1-id-11">
                                <textEntryInteraction patternMask="^-?([0-9]{1,5})?$" responseIdentifier="I0dec2572-468c-49d9-bdff-c2482ec461c1" expectedLength="6"/> <textEntryInteraction patternMask="^-?([0-9]{1,5})?$" responseIdentifier="Ia896c73f-84fa-4ead-b3fc-210882efb8b9" expectedLength="6"/> <textEntryInteraction patternMask="^-?([0-9]{1,5})?$" responseIdentifier="I499a63a9-fdf8-43eb-bdce-e1ecabfa2bed" expectedLength="6"/>
                            </p>
                        </div>
                        <div id="answer">

                        </div>
                    </div>
                </div>
            </itemBody>
        </wrapper>

    ReadOnly _itemBody5 As XElement =
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
                                        <textEntryInteraction patternMask="^(?![0])(([0-2]?[0-9])|([3][0-1]))$" responseIdentifier="I294446c7-6f39-4fd2-ad12-ad523209ac86" expectedLength="2" dateSubType="dutch"/>
								    
								        -
								    
							  
								    <textEntryInteraction patternMask="^(?![0])(([0-9])|([1][0-2]))$" responseIdentifier="I294446c7-6f39-4fd2-ad12-ad523209ac86" expectedLength="2" dateSubType="dutch"/>
								    
								        -
								    
							  
								    <textEntryInteraction patternMask="^([0-9]{1,4})$" responseIdentifier="I294446c7-6f39-4fd2-ad12-ad523209ac86" expectedLength="4" dateSubType="dutch"/>
                                    </span> <span>
                                        <textEntryInteraction patternMask="^(([0-1]?[0-9])|([2][0-3]))$" responseIdentifier="I6642e452-d8a6-47db-a70b-04d38953600a" expectedLength="2" timeSubType="hhmm"/>
							  
								    :
								    
								    <textEntryInteraction patternMask="^([0-5]?[0-9])$" responseIdentifier="I6642e452-d8a6-47db-a70b-04d38953600a" expectedLength="2" timeSubType="hhmm"/>
                                    </span>
                                </p>
                            </div>
                            <div id="answer">

                            </div>
                        </div>
                    </div>
                </div>
            </itemBody>
        </wrapper>

    ReadOnly _itemBody6 As XElement =
        <wrapper>
            <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
            <itemBody class="defaultBody">
                <div class="content">
                    <div>
                        <div id="question">
                            <p id="c1-id-11">Welke ?</p>
                        </div>
                        <div id="mc">
                            <choiceInteraction id="choiceInteraction1" class="" maxChoices="0" shuffle="false" responseIdentifier="mc">
                                <simpleChoice identifier="A">
                                    <p id="c1-id-11">A</p>
                                </simpleChoice>
                                <simpleChoice identifier="B">
                                    <p id="c1-id-11">B</p>
                                </simpleChoice>
                            </choiceInteraction>
                        </div>
                    </div>
                </div>
            </itemBody>
        </wrapper>

    ReadOnly _itemBody7 As XElement =
        <wrapper>
            <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
            <itemBody class="defaultBody">
                <div class="content">
                    <div>
                        <div id="question">
                            <p id="c1-id-11">Welke ?</p>
                        </div>
                        <div id="mc">
                            <choiceInteraction id="choiceInteraction1" class="" maxChoices="0" shuffle="false" responseIdentifier="mc">
                                <simpleChoice identifier="A">
                                    <p id="c1-id-11">A</p>
                                </simpleChoice>
                                <simpleChoice identifier="B">
                                    <p id="c1-id-11">B</p>
                                </simpleChoice>
                                <simpleChoice identifier="C">
                                    <p id="c1-id-11">C</p>
                                </simpleChoice>
                                <simpleChoice identifier="D">
                                    <p id="c1-id-11">D</p>
                                </simpleChoice>
                            </choiceInteraction>
                        </div>
                    </div>
                </div>
            </itemBody>
        </wrapper>

    ReadOnly _itemBody8 As XElement =
        <div class="div_right" xmlns="http://www.w3.org/1999/xhtml">
            <!-- div_right_inner is needed to create the 50px margin between the left and right columns in dual column mode -->
            <div class="div_right_inner">
                <div id="itemquestion">
                    <p id="c1-id-11">In deze .. </p>
                    <p id="c1-id-12">
                        <strong id="c1-id-13">Zijn deze .. ? </strong>
                    </p>
                    <p id="c1-id-14">
                        <em id="c1-id-15">Licht je antwoord toe.</em>
                    </p>
                    <p id="c1-id-16">
                        <extendedTextInteraction responseIdentifier="Ie4f1899e-162d-41bf-878c-bd6e1a149788" expectedLines="8" expectedLength="0"/>
                    </p>
                    <p id="c1-id-17">
                        <strong id="c1-id-18"> </strong>
                    </p>
                    <p id="c1-id-19">
                        <strong id="c1-id-20">Hoe lang ...?</strong>
                    </p>
                    <p id="c1-id-21">
                        <choiceInteraction id="Ia9ec64c9-1360-479a-8b2a-9f5458eb9973" class="" shuffle="false" responseIdentifier="Ia9ec64c9-1360-479a-8b2a-9f5458eb9973" maxChoices="1">
                            <simpleChoice identifier="Ia9ec64c9-1360-479a-8b2a-9f5458eb9973A">
                                <p>1</p>
                            </simpleChoice>
                            <simpleChoice identifier="Ia9ec64c9-1360-479a-8b2a-9f5458eb9973B">
                                <p>2</p>
                            </simpleChoice>
                            <simpleChoice identifier="Ia9ec64c9-1360-479a-8b2a-9f5458eb9973C">
                                <p>3</p>
                            </simpleChoice>
                        </choiceInteraction> </p>
                    <p id="c1-id-22"> </p>
                    <p id="c1-id-23">
                        <em id="c1-id-24">Inlinecontrol-item: type open vraag, meerkeuze en multiple response, voorbeeld van een brontekst met brief, twee kolommen.</em>
                    </p>
                    <p id="c1-id-25">
                        <choiceInteraction id="Ie238b8b8-ff68-4b76-9762-2f1065c65160" class="" shuffle="false" responseIdentifier="Ie238b8b8-ff68-4b76-9762-2f1065c65160" maxChoices="0">
                            <simpleChoice identifier="Ie238b8b8-ff68-4b76-9762-2f1065c65160A">
                                <p>fsfs</p>
                            </simpleChoice>
                            <simpleChoice identifier="Ie238b8b8-ff68-4b76-9762-2f1065c65160B">
                                <p>sfsfs</p>
                            </simpleChoice>
                            <simpleChoice identifier="Ie238b8b8-ff68-4b76-9762-2f1065c65160C">
                                <p>fsf</p>
                            </simpleChoice>
                            <simpleChoice identifier="Ie238b8b8-ff68-4b76-9762-2f1065c65160D">
                                <p>sfsfs</p>
                            </simpleChoice>
                        </choiceInteraction>
                    </p>
                </div>
            </div>
        </div>

    ReadOnly _itemBody9 As XElement =
       <wrapper>
           <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
           <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
           <itemBody class="defaultBody">
               <div class="content">
                   <div>
                       <p id="c1-id-11">Vul in, tekst:  nee</p>
                       <p id="c1-id-12">
                           <textEntryInteraction patternMask="^[^A-Z]{0,5}$" responseIdentifier="Ifaf38ff9-00f5-448c-b86b-252db6377abf" expectedLength="5"/> </p>
                       <p id="c1-id-13">Vul in, geheel getal:  10</p>
                       <p id="c1-id-14">
                           <textEntryInteraction patternMask="^-?([0-9]{1,5})?$" responseIdentifier="Iebfe9a06-1bd8-4430-a1b8-412e268266f7" expectedLength="6"/>
                       </p>
                       <p id="c1-id-15">Vul in, decimaal getal: 15,50</p>
                       <p id="c1-id-16">
                           <textEntryInteraction patternMask="^-?([0-9]{1,15})?(([\,])([0-9]{0,2}))?$" responseIdentifier="I65bd20fb-80f1-4e34-95f8-b70ccf11cb9f" expectedLength="18"/> </p>
                       <p id="c1-id-17">Vul in, valuta:  10,50</p>
                       <p id="c1-id-18">
                           <textEntryInteraction patternMask="^-?([0-9]{1,5})?(([\,])([0-9]{0,2}))?$" responseIdentifier="Ia7b2ea59-2a1d-43d1-a09f-fafff847191b" expectedLength="8"/> </p>
                       <p id="c1-id-19">Vul in, tijd:  12:15</p>
                       <p id="c1-id-20">
                           <span>
                               <textEntryInteraction patternMask="^(([0-1]?[0-9])|([2][0-3]))$" responseIdentifier="I59205989-12f5-4dbf-a5d0-e83e47d6c87f" expectedLength="2" timeSubType="hhmm"/>

								    :

								    <textEntryInteraction patternMask="^([0-5]?[0-9])$" responseIdentifier="I59205989-12f5-4dbf-a5d0-e83e47d6c87f" expectedLength="2" timeSubType="hhmm"/>
                           </span>
                       </p>
                   </div>
               </div>
           </itemBody>
       </wrapper>




    ReadOnly _solution1 As XElement =
         <solution>
             <keyFindings>
                 <keyFinding id="inlineChoiceController" scoringMethod="Dichotomous">
                     <keyFact id="A-I67632965-d307-4fb3-a4b0-1107e5af0085" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                         <keyValue domain="I67632965-d307-4fb3-a4b0-1107e5af0085" occur="1">
                             <stringValue>
                                 <typedValue>A</typedValue>
                             </stringValue>
                         </keyValue>
                     </keyFact>
                     <keyFact id="A-I440d2525-0615-4521-ab71-17f9473e15d5" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                         <keyValue domain="I440d2525-0615-4521-ab71-17f9473e15d5" occur="1">
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

    ReadOnly _solution2 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="gapController" scoringMethod="Dichotomous">
                    <keyFact id="1-Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" occur="1">
                            <stringValue>
                                <typedValue>10:00</typedValue>
                            </stringValue>
                            <stringValue>
                                <typedValue>11:00</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="1-I0b525f2d-804e-42a1-b8da-9abbd6d0f6ae" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="I0b525f2d-804e-42a1-b8da-9abbd6d0f6ae" occur="1">
                            <stringValue>
                                <typedValue>12:00</typedValue>
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

    ReadOnly _solution3 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="gapController" scoringMethod="Dichotomous">
                    <keyFactSet>
                        <keyFact id="1-Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" occur="1">
                                <stringValue>
                                    <typedValue>10:00</typedValue>
                                </stringValue>
                                <stringValue>
                                    <typedValue>11:00</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="1-I0b525f2d-804e-42a1-b8da-9abbd6d0f6ae" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I0b525f2d-804e-42a1-b8da-9abbd6d0f6ae" occur="1">
                                <stringValue>
                                    <typedValue>12:00</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="1-Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" occur="1">
                                <stringValue>
                                    <typedValue>01:00</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="1-I0b525f2d-804e-42a1-b8da-9abbd6d0f6ae" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I0b525f2d-804e-42a1-b8da-9abbd6d0f6ae" occur="1">
                                <stringValue>
                                    <typedValue>02:00</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                </keyFinding>
            </keyFindings>
            <aspectReferences/>
            <ItemScoreTranslationTable>
                <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
            </ItemScoreTranslationTable>
        </solution>

    ReadOnly _solution4 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="gapController" scoringMethod="Dichotomous">
                    <keyFactSet>
                        <keyFact id="1-Ia896c73f-84fa-4ead-b3fc-210882efb8b9" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Ia896c73f-84fa-4ead-b3fc-210882efb8b9" occur="1">
                                <integerValue>
                                    <typedValue>3</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="1-I0dec2572-468c-49d9-bdff-c2482ec461c1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I0dec2572-468c-49d9-bdff-c2482ec461c1" occur="1">
                                <integerValue>
                                    <typedValue>1</typedValue>
                                </integerValue>
                                <integerValue>
                                    <typedValue>2</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="1-I499a63a9-fdf8-43eb-bdce-e1ecabfa2bed" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I499a63a9-fdf8-43eb-bdce-e1ecabfa2bed" occur="1">
                                <integerRangeValue rangeEnd="5" rangeStart="4"/>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                </keyFinding>
            </keyFindings>
            <aspectReferences/>
            <ItemScoreTranslationTable>
                <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
            </ItemScoreTranslationTable>
        </solution>

    ReadOnly _solution5 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="gapController" scoringMethod="Dichotomous">
                    <keyFactSet>
                        <keyFact id="1-I294446c7-6f39-4fd2-ad12-ad523209ac86" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I294446c7-6f39-4fd2-ad12-ad523209ac86" occur="1">
                                <stringValue>
                                    <typedValue>07/11/2014</typedValue>
                                </stringValue>
                                <stringValue>
                                    <typedValue>07/21/2014</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="1-I6642e452-d8a6-47db-a70b-04d38953600a" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I6642e452-d8a6-47db-a70b-04d38953600a" occur="1">
                                <stringValue>
                                    <typedValue>01:00</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                </keyFinding>
            </keyFindings>
            <aspectReferences/>
            <ItemScoreTranslationTable>
                <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
            </ItemScoreTranslationTable>
        </solution>

    ReadOnly _solution6 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="mc" scoringMethod="Polytomous">
                    <keyFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
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

    ReadOnly _solution7 As XElement =
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
                    <keyFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="mc" occur="1">
                            <stringValue>
                                <typedValue>D</typedValue>
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

    ReadOnly _solution8 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="Ia9ec64c9-1360-479a-8b2a-9f5458eb9973" scoringMethod="Dichotomous">
                    <keyFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="Ia9ec64c9-1360-479a-8b2a-9f5458eb9973" occur="1">
                            <stringValue>
                                <typedValue>B</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                </keyFinding>
                <keyFinding id="Ie238b8b8-ff68-4b76-9762-2f1065c65160" scoringMethod="Dichotomous">
                    <keyFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="Ie238b8b8-ff68-4b76-9762-2f1065c65160" occur="1">
                            <stringValue>
                                <typedValue>B</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="Ie238b8b8-ff68-4b76-9762-2f1065c65160" occur="1">
                            <stringValue>
                                <typedValue>C</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                </keyFinding>
            </keyFindings>
        </solution>

    ReadOnly _solution9 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="gapController" scoringMethod="Dichotomous">
                    <keyFact id="1-Ifaf38ff9-00f5-448c-b86b-252db6377abf" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="Ifaf38ff9-00f5-448c-b86b-252db6377abf" occur="1">
                            <stringValue>
                                <typedValue>nee</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFactSet>
                        <keyFact id="1-Iebfe9a06-1bd8-4430-a1b8-412e268266f7" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Iebfe9a06-1bd8-4430-a1b8-412e268266f7" occur="1">
                                <integerValue>
                                    <typedValue>10</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="1-I65bd20fb-80f1-4e34-95f8-b70ccf11cb9f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I65bd20fb-80f1-4e34-95f8-b70ccf11cb9f" occur="1">
                                <decimalValue>
                                    <typedValue>15.50</typedValue>
                                </decimalValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="1-Ia7b2ea59-2a1d-43d1-a09f-fafff847191b" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Ia7b2ea59-2a1d-43d1-a09f-fafff847191b" occur="1">
                                <decimalValue>
                                    <typedValue>10.50</typedValue>
                                </decimalValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="1-I59205989-12f5-4dbf-a5d0-e83e47d6c87f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I59205989-12f5-4dbf-a5d0-e83e47d6c87f" occur="1">
                                <stringValue>
                                    <typedValue>12:15</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                </keyFinding>
            </keyFindings>
        </solution>




    ReadOnly _result1 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="single" baseType="identifier">
                <correctResponse interpretation="A">
                    <value>A</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE2" cardinality="single" baseType="identifier">
                <correctResponse interpretation="A">
                    <value>A</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    ReadOnly _result2 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="single" baseType="integer">
                <correctResponse interpretation="10:00#11:00">
                    <value>10</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE2" cardinality="single" baseType="integer">
                <correctResponse>
                    <value>00</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE3" cardinality="single" baseType="integer">
                <correctResponse interpretation="12:00">
                    <value>12</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE4" cardinality="single" baseType="integer">
                <correctResponse>
                    <value>00</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    ReadOnly _result3 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="single" baseType="integer">
                <correctResponse interpretation="(10:00#11:00&amp;12:00)|(01:00&amp;02:00)">
                    <value>10</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE2" cardinality="single" baseType="integer">
                <correctResponse>
                    <value>00</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE3" cardinality="single" baseType="integer">
                <correctResponse>
                    <value>12</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE4" cardinality="single" baseType="integer">
                <correctResponse>
                    <value>00</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    ReadOnly _result4 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="single" baseType="integer">
                <correctResponse interpretation="(1#2&amp;3&amp;4-5)">
                    <value>1</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE2" cardinality="single" baseType="integer">
                <correctResponse>
                    <value>3</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE3" cardinality="single" baseType="integer">
                <correctResponse>
                    <value>4</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    ReadOnly _result5 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="single" baseType="integer">
                <correctResponse interpretation="(07/11/2014#07/21/2014&amp;01:00)">
                    <value>07</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE2" cardinality="single" baseType="integer">
                <correctResponse>
                    <value>11</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE3" cardinality="single" baseType="integer">
                <correctResponse>
                    <value>2014</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE4" cardinality="single" baseType="integer">
                <correctResponse>
                    <value>01</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE5" cardinality="single" baseType="integer">
                <correctResponse>
                    <value>00</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    ReadOnly _result6 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="identifier">
                <correctResponse interpretation="A">
                    <value>A</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    ReadOnly _result7 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="identifier">
                <correctResponse interpretation="B&amp;D">
                    <value>B</value>
                    <value>D</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    ReadOnly _result8 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="single" baseType="string"/>
            <responseDeclaration identifier="RESPONSE2" cardinality="single" baseType="identifier">
                <correctResponse interpretation="Ia9ec64c9-1360-479a-8b2a-9f5458eb9973B">
                    <value>Ia9ec64c9-1360-479a-8b2a-9f5458eb9973B</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE3" cardinality="multiple" baseType="identifier">
                <correctResponse interpretation="Ie238b8b8-ff68-4b76-9762-2f1065c65160B&amp;Ie238b8b8-ff68-4b76-9762-2f1065c65160C">
                    <value>Ie238b8b8-ff68-4b76-9762-2f1065c65160B</value>
                    <value>Ie238b8b8-ff68-4b76-9762-2f1065c65160C</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    ReadOnly _result9 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="single" baseType="string">
                <correctResponse interpretation="nee">
                    <value>nee</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE2" cardinality="single" baseType="integer">
                <correctResponse interpretation="(10&amp;15.50)">
                    <value>10</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE3" cardinality="single" baseType="string">
                <correctResponse>
                    <value>15.50</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE4" cardinality="single" baseType="string">
                <correctResponse interpretation="(10.50&amp;12:15)">
                    <value>10.50</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE5" cardinality="single" baseType="integer">
                <correctResponse>
                    <value>12</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE6" cardinality="single" baseType="integer">
                <correctResponse>
                    <value>15</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>


End Class
