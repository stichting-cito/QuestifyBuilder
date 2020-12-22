Imports Cito.Tester.ContentModel

<TestClass()>
Public Class QTI22ResponseDeclarationInputTests
    Inherits QTI22ResponseDeclarationTests_Base

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub Input_WithAlternatives_Test()
        RunResponseDeclarationTest(_itemBody1, _solution1, GetThreeInputScoringParams(), _result1, 3)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub Input_NoScoringEntered_Test()
        RunResponseDeclarationTest(_itemBody1, _solution2, GetThreeInputScoringParams(), _result2, 3)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub Input_FindingsFactsAndFactSet_Test()
        RunResponseDeclarationTest(_itemBody1, _solution3, GetThreeInputScoringParams(), _result3, 3)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub Input_FindingsFactsAndFactSet_WithAlternatives_Test()
        RunResponseDeclarationTest(_itemBody1, _solution4, GetThreeInputScoringParams(), _result4, 3)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub Input_NoScoringPrms_WithAlternatives_Test()
        RunResponseDeclarationTest(_itemBody2, _solution5, Nothing, _result5, 4)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub Input_NoScoringPrms_GreaterLowerThan_Test()
        RunResponseDeclarationTest(_itemBody2, _solution6, Nothing, _result6, 4)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub Input_NoScoringPrms_Dates_Test()
        RunResponseDeclarationTest(_itemBody3, _solution7, Nothing, _result7, 6)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub Input_NoScoringPrms_Times_Test()
        RunResponseDeclarationTest(_itemBody4, _solution8, Nothing, _result8, 4)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration"), WorkItem(29477)>
    Public Sub Input_NoScoringPrms_IncompleteScoring1_Test()
        RunResponseDeclarationTest(_itemBody5, _solution9, Nothing, _result9, 2)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration"), WorkItem(29477)>
    Public Sub Input_NoScoringPrms_IncompleteScoring2_Test()
        RunResponseDeclarationTest(_itemBody5, _solution10, Nothing, _result10, 2)
    End Sub


    Private Function GetThreeInputScoringParams() As HashSet(Of ScoringParameter)
        Dim scoreParams As New HashSet(Of ScoringParameter)
        Dim scoreParam1 As ScoringParameter = New IntegerScoringParameter() With {.ControllerId = "gapController", .FindingOverride = "gapController", .InlineId = "I812796ae-bcc4-4fcf-803c-85fdae53013d", .MaxLength = 5}.AddSubParameters("1")
        scoreParams.Add(scoreParam1)
        Dim scoreParam2 As ScoringParameter = New DecimalScoringParameter() With {.ControllerId = "gapController", .FindingOverride = "gapController", .InlineId = "I137d2333-4bdf-446a-a4b7-9c97ff566462", .IntegerPartMaxLength = 5, .FractionPartMaxLength = 3}.AddSubParameters("1")
        scoreParams.Add(scoreParam2)
        Dim scoreParam3 As ScoringParameter = New StringScoringParameter() With {.ControllerId = "gapController", .FindingOverride = "gapController", .InlineId = "I53a5a19c-90a1-4d82-979a-08d3b4d6ed6a", .ExpectedLength = 5}.AddSubParameters("1")
        scoreParams.Add(scoreParam3)
        Return scoreParams
    End Function



    Private _itemBody1 As XElement =
       <wrapper>
           <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
           <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
           <itemBody class="defaultBody">
               <div class="content">
                   <div>
                       <div xmlns="http://www.w3.org/1999/xhtml">
                           <div id="questionwithinlinecontrol">
                               <p id="c1-id-11">
                                   <textEntryInteraction patternMask="^-?([0-9]{1,5})?$" responseIdentifier="I812796ae-bcc4-4fcf-803c-85fdae53013d" expectedLength="6"/>
                               </p>
                               <p id="c1-id-12">
                                   <textEntryInteraction patternMask="^.{0,5}$" responseIdentifier="I53a5a19c-90a1-4d82-979a-08d3b4d6ed6a" expectedLength="5"/>
                               </p>
                               <p id="c1-id-13">
                                   <textEntryInteraction patternMask="^-?([0-9]{1,5})?(([\,])([0-9]{0,3}))?$" responseIdentifier="I137d2333-4bdf-446a-a4b7-9c97ff566462" expectedLength="9"/> </p>
                           </div>
                           <div id="answer">

                           </div>
                       </div>
                   </div>
               </div>
           </itemBody>
       </wrapper>

    Private _itemBody2 As XElement =
       <wrapper>
           <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
           <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
           <itemBody class="defaultBody">
               <div class="content">
                   <div xmlns="http://www.w3.org/1999/xhtml">
                       <div id="answer">
                           <table class="interactionTable">
                               <tbody>
                                   <tr>
                                       <td class="td_gap">
                                           <div style="padding-right:6px!important;">
                                               <textEntryInteraction patternMask="^-?([0-9]{1,5})?$" responseIdentifier="gapController" expectedLength="6"/>
                                           </div>
                                       </td>
                                   </tr>
                               </tbody>
                           </table>
                           <table class="interactionTable">
                               <tbody>
                                   <tr>
                                       <td class="td_gap">
                                           <div style="padding-right:6px!important;">
                                               <span>
                                                   <textEntryInteraction patternMask="^(([0-1]?[0-9])|([2][0-3]))$" responseIdentifier="gapController" expectedLength="2" timeSubType="hhmm"/>
						
							:
							
							<textEntryInteraction patternMask="^([0-5]?[0-9])$" responseIdentifier="gapController" expectedLength="2" timeSubType="hhmm"/>
                                               </span>
                                           </div>
                                       </td>
                                   </tr>
                               </tbody>
                           </table>
                           <table class="interactionTable">
                               <tbody>
                                   <tr>
                                       <td class="td_gap">
                                           <div style="padding-right:6px!important;">
                                               <textEntryInteraction patternMask="^.{0,5}$" responseIdentifier="gapController" expectedLength="5"/>
                                           </div>
                                       </td>
                                   </tr>
                               </tbody>
                           </table>
                       </div>
                   </div>
               </div>
           </itemBody>
       </wrapper>

    Private _itemBody3 As XElement =
       <wrapper>
           <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
           <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
           <itemBody class="defaultBody">
               <div class="content">
                   <div xmlns="http://www.w3.org/1999/xhtml">
                       <div id="answer">
                           <table class="interactionTable">
                               <tbody>
                                   <tr>
                                       <td class="td_gap">
                                           <div style="padding-right:6px!important;">
                                               <span>
                                                   <textEntryInteraction patternMask="^(?![0])(([0-2]?[0-9])|([3][0-1]))$" responseIdentifier="gapController" expectedLength="2" dateSubType="dutch"/>
							
							-
							
						
							<textEntryInteraction patternMask="^(?![0])(([0-9])|([1][0-2]))$" responseIdentifier="gapController" expectedLength="2" dateSubType="dutch"/>
							
							-
							
						
							<textEntryInteraction patternMask="^([0-9]{1,4})$" responseIdentifier="gapController" expectedLength="4" dateSubType="dutch"/>
                                               </span>
                                           </div>
                                       </td>
                                   </tr>
                               </tbody>
                           </table>
                           <table class="interactionTable">
                               <tbody>
                                   <tr>
                                       <td class="td_gap">
                                           <div style="padding-right:6px!important;">
                                               <span>
                                                   <textEntryInteraction patternMask="^(?![0])(([0-9])|([1][0-2]))$" responseIdentifier="gapController" expectedLength="2" dateSubType="american"/>
							
							/
							
						
							<textEntryInteraction patternMask="^(?![0])(([0-2]?[0-9])|([3][0-1]))$" responseIdentifier="gapController" expectedLength="2" dateSubType="american"/>
							
							/
							
						
							<textEntryInteraction patternMask="^([0-9]{1,4})$" responseIdentifier="gapController" expectedLength="4" dateSubType="american"/>
                                               </span>
                                           </div>
                                       </td>
                                   </tr>
                               </tbody>
                           </table>
                       </div>
                   </div>
               </div>
           </itemBody>
       </wrapper>

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

    Private _itemBody5 As XElement =
       <wrapper>
           <itemBody class="defaultBody" xml:lang="nl-NL">
               <textEntryInteraction patternMask='^-?([0-9]{1,5})?$' responseIdentifier='gapController' expectedLength='6'/>
               <textEntryInteraction patternMask='^-?([0-9]{1,5})?$' responseIdentifier='gapController' expectedLength='6'/>
           </itemBody>
       </wrapper>


    Private _solution1 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="gapController" scoringMethod="Dichotomous">
                    <keyFact id="1-I812796ae-bcc4-4fcf-803c-85fdae53013d" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="I812796ae-bcc4-4fcf-803c-85fdae53013d" occur="1">
                            <integerValue>
                                <typedValue>1</typedValue>
                            </integerValue>
                            <integerValue>
                                <typedValue>2</typedValue>
                            </integerValue>
                            <integerValue>
                                <typedValue>3</typedValue>
                            </integerValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="1-I53a5a19c-90a1-4d82-979a-08d3b4d6ed6a" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="I53a5a19c-90a1-4d82-979a-08d3b4d6ed6a" occur="1">
                            <stringValue>
                                <typedValue>tekstje</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="1-I137d2333-4bdf-446a-a4b7-9c97ff566462" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="I137d2333-4bdf-446a-a4b7-9c97ff566462" occur="1">
                            <decimalValue>
                                <typedValue>1.1</typedValue>
                            </decimalValue>
                            <decimalValue>
                                <typedValue>1.2</typedValue>
                            </decimalValue>
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

    Private _solution2 As XElement =
        <solution>
            <keyFindings/>
            <aspectReferences/>
            <ItemScoreTranslationTable>
                <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
            </ItemScoreTranslationTable>
        </solution>

    Private _solution3 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="gapController" scoringMethod="Dichotomous">
                    <keyFact id="1-I53a5a19c-90a1-4d82-979a-08d3b4d6ed6a" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="I53a5a19c-90a1-4d82-979a-08d3b4d6ed6a" occur="1">
                            <stringValue>
                                <typedValue>tekstje</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFactSet>
                        <keyFact id="1-I812796ae-bcc4-4fcf-803c-85fdae53013d" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I812796ae-bcc4-4fcf-803c-85fdae53013d" occur="1">
                                <integerValue>
                                    <typedValue>1</typedValue>
                                </integerValue>
                                <integerValue>
                                    <typedValue>2</typedValue>
                                </integerValue>
                                <integerValue>
                                    <typedValue>3</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="1-I137d2333-4bdf-446a-a4b7-9c97ff566462" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I137d2333-4bdf-446a-a4b7-9c97ff566462" occur="1">
                                <decimalValue>
                                    <typedValue>1.1</typedValue>
                                </decimalValue>
                                <decimalValue>
                                    <typedValue>1.2</typedValue>
                                </decimalValue>
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

    Private _solution4 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="gapController" scoringMethod="Dichotomous">
                    <keyFact id="1-I53a5a19c-90a1-4d82-979a-08d3b4d6ed6a" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="I53a5a19c-90a1-4d82-979a-08d3b4d6ed6a" occur="1">
                            <stringValue>
                                <typedValue>tekstje</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFactSet>
                        <keyFact id="1-I812796ae-bcc4-4fcf-803c-85fdae53013d" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I812796ae-bcc4-4fcf-803c-85fdae53013d" occur="1">
                                <integerValue>
                                    <typedValue>1</typedValue>
                                </integerValue>
                                <integerValue>
                                    <typedValue>2</typedValue>
                                </integerValue>
                                <integerValue>
                                    <typedValue>3</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="1-I137d2333-4bdf-446a-a4b7-9c97ff566462" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I137d2333-4bdf-446a-a4b7-9c97ff566462" occur="1">
                                <decimalValue>
                                    <typedValue>1.1</typedValue>
                                </decimalValue>
                                <decimalValue>
                                    <typedValue>1.2</typedValue>
                                </decimalValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="1-I137d2333-4bdf-446a-a4b7-9c97ff566462" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I137d2333-4bdf-446a-a4b7-9c97ff566462" occur="1">
                                <decimalValue>
                                    <typedValue>1.3</typedValue>
                                </decimalValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="1-I812796ae-bcc4-4fcf-803c-85fdae53013d" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I812796ae-bcc4-4fcf-803c-85fdae53013d" occur="1">
                                <integerValue>
                                    <typedValue>4</typedValue>
                                </integerValue>
                                <integerValue>
                                    <typedValue>5</typedValue>
                                </integerValue>
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

    Private _solution5 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="gapController" scoringMethod="Dichotomous">
                    <keyFact id="IntegerInputA" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="IntegerInputA" occur="1">
                            <integerRangeValue rangeEnd="999" rangeStart="100"/>
                            <integerComparisonValue>
                                <typedComparisonValue>1234</typedComparisonValue>
                                <comparisonType>GreaterThan</comparisonType>
                            </integerComparisonValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="TimeInputB" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="TimeInputB" occur="1">
                            <stringValue>
                                <typedValue>12:34</typedValue>
                            </stringValue>
                            <stringValue>
                                <typedValue>21:34</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="StringInputC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="StringInputC" occur="1">
                            <stringValue>
                                <typedValue>tekst</typedValue>
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

    Private _solution6 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="gapController" scoringMethod="Dichotomous">
                    <keyFact id="IntegerInputA" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="IntegerInputA" occur="1">
                            <integerComparisonValue>
                                <typedComparisonValue>100</typedComparisonValue>
                                <comparisonType>GreaterThan</comparisonType>
                            </integerComparisonValue>
                            <integerComparisonValue>
                                <typedComparisonValue>999</typedComparisonValue>
                                <comparisonType>SmallerThan</comparisonType>
                            </integerComparisonValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="TimeInputB" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="TimeInputB" occur="1">
                            <stringValue>
                                <typedValue>12:34</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="StringInputC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="StringInputC" occur="1">
                            <stringValue>
                                <typedValue>tekst</typedValue>
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

    Private _solution7 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="gapController" scoringMethod="Dichotomous">
                    <keyFact id="DateInputA" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="DateInputA" occur="1">
                            <stringValue>
                                <typedValue>03/01/2015</typedValue>
                            </stringValue>
                            <stringValue>
                                <typedValue>05/01/2015</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="DateInputB" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="DateInputB" occur="1">
                            <stringValue>
                                <typedValue>03/01/2015</typedValue>
                            </stringValue>
                            <stringValue>
                                <typedValue>05/01/2015</typedValue>
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

    Private _solution8 As XElement =
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

    Private _solution9 As XElement =
      <solution>
          <keyFindings>
              <keyFinding id="gapController" scoringMethod="Dichotomous">
                  <keyFact id="CurrencyInputA" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                      <keyValue domain="CurrencyInputA" occur="1">
                          <decimalValue>
                              <typedValue>244400.00</typedValue>
                          </decimalValue>
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

    Private _solution10 As XElement =
      <solution>
          <keyFindings>
              <keyFinding id="gapController" scoringMethod="Dichotomous">
                  <keyFact id="CurrencyInputB" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                      <keyValue domain="CurrencyInputB" occur="1">
                          <decimalValue>
                              <typedValue>244400.00</typedValue>
                          </decimalValue>
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


    Private _result1 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="single" baseType="integer">
                <correctResponse interpretation="1#2#3">
                    <value>1</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE2" cardinality="single" baseType="string">
                <correctResponse interpretation="tekstje">
                    <value>tekstje</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE3" cardinality="single" baseType="string">
                <correctResponse interpretation="1.1#1.2">
                    <value>1.1</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result2 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="single" baseType="string"/>
            <responseDeclaration identifier="RESPONSE2" cardinality="single" baseType="string"/>
            <responseDeclaration identifier="RESPONSE3" cardinality="single" baseType="string"/>
        </responseDeclarations>

    Private _result3 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="single" baseType="integer">
                <correctResponse interpretation="(1#2#3&amp;1.1#1.2)">
                    <value>1</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE2" cardinality="single" baseType="string">
                <correctResponse interpretation="tekstje">
                    <value>tekstje</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE3" cardinality="single" baseType="string">
                <correctResponse>
                    <value>1.1</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result4 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="single" baseType="integer">
                <correctResponse interpretation="(1#2#3&amp;1.1#1.2)|(4#5&amp;1.3)">
                    <value>1</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE2" cardinality="single" baseType="string">
                <correctResponse interpretation="tekstje">
                    <value>tekstje</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE3" cardinality="single" baseType="string">
                <correctResponse>
                    <value>1.1</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result5 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="single" baseType="integer">
                <correctResponse interpretation="100-999#&gt;1234">
                    <value>100</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE2" cardinality="single" baseType="integer">
                <correctResponse interpretation="12:34#21:34">
                    <value>12</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE3" cardinality="single" baseType="integer">
                <correctResponse>
                    <value>34</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE4" cardinality="single" baseType="string">
                <correctResponse interpretation="tekst">
                    <value>tekst</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result6 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="single" baseType="integer">
                <correctResponse interpretation="&gt;100#&lt;999">
                    <value>101</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE2" cardinality="single" baseType="integer">
                <correctResponse interpretation="12:34">
                    <value>12</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE3" cardinality="single" baseType="integer">
                <correctResponse>
                    <value>34</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE4" cardinality="single" baseType="string">
                <correctResponse interpretation="tekst">
                    <value>tekst</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result7 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="single" baseType="integer">
                <correctResponse interpretation="03/01/2015#05/01/2015">
                    <value>03</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE2" cardinality="single" baseType="integer">
                <correctResponse>
                    <value>01</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE3" cardinality="single" baseType="integer">
                <correctResponse>
                    <value>2015</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE4" cardinality="single" baseType="integer">
                <correctResponse interpretation="03/01/2015#05/01/2015">
                    <value>03</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE5" cardinality="single" baseType="integer">
                <correctResponse>
                    <value>01</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE6" cardinality="single" baseType="integer">
                <correctResponse>
                    <value>2015</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result8 As XElement =
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

    Private _result9 As XElement =
     <responseDeclarations>
         <responseDeclaration identifier="RESPONSE" cardinality="single" baseType="string">
             <correctResponse interpretation="244400.00">
                 <value>244400.00</value>
             </correctResponse>
         </responseDeclaration>
         <responseDeclaration identifier="RESPONSE2" cardinality="single" baseType="string"/>
     </responseDeclarations>

    Private _result10 As XElement =
 <responseDeclarations>
     <responseDeclaration identifier="RESPONSE" cardinality="single" baseType="string"/>
     <responseDeclaration identifier="RESPONSE2" cardinality="single" baseType="string">
         <correctResponse interpretation="244400.00">
             <value>244400.00</value>
         </correctResponse>
     </responseDeclaration>
 </responseDeclarations>


End Class
