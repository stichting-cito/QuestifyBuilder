Imports Cito.Tester.ContentModel

Namespace QTI30

    <TestClass()>
    Public Class ResponseDeclarationInputTests
        Inherits QTI_Base.ResponseDeclarationInputTestsBase

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub Input_WithAlternatives_Test()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution1, GetThreeInputScoringParams(), _result1, 3)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub Input_NoScoringEntered_Test()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution2, GetThreeInputScoringParams(), _result2, 3)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub Input_FindingsFactsAndFactSet_Test()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution3, GetThreeInputScoringParams(), _result3, 3)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub Input_FindingsFactsAndFactSet_WithAlternatives_Test()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution4, GetThreeInputScoringParams(), _result4, 3)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub Input_NoScoringPrms_WithAlternatives_Test()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody2, _solution5, Nothing, _result5, 4)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub Input_NoScoringPrms_GreaterLowerThan_Test()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody2, _solution6, Nothing, _result6, 4)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub Input_NoScoringPrms_Dates_Test()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody3, _solution7, Nothing, _result7, 6)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub Input_NoScoringPrms_Times_Test()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody4, _solution8, Nothing, _result8, 4)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration"), WorkItem(29477)>
        Public Sub Input_NoScoringPrms_IncompleteScoring1_Test()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody5, _solution9, Nothing, _result9, 2)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration"), WorkItem(29477)>
        Public Sub Input_NoScoringPrms_IncompleteScoring2_Test()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody5, _solution10, Nothing, _result10, 2)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub ItemWithSingleInteractionAndDichotomousScoringWhenCreatingResponseDeclarationShouldCreateCorrectResponseButNoMappings()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody6, _solution11, GetSingleInputScoringParam(), _result11, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub ItemWithSingleInteractionAndPolytomousScoringWhenCreatingResponseDeclarationShouldCreateCorrectResponseAndMappings()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody6, _solution12, GetSingleInputScoringParam(), _result12, 1)
        End Sub


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
                                        <qti-text-entry-interaction pattern-mask="^-?([0-9]{1,5})?$" response-identifier="I812796ae-bcc4-4fcf-803c-85fdae53013d" expected-length="6"/>
                                    </p>
                                    <p id="c1-id-12">
                                        <qti-text-entry-interaction pattern-mask="^.{0,5}$" response-identifier="I53a5a19c-90a1-4d82-979a-08d3b4d6ed6a" expected-length="5"/>
                                    </p>
                                    <p id="c1-id-13">
                                        <qti-text-entry-interaction pattern-mask="^-?([0-9]{1,5})?(([\,])([0-9]{0,3}))?$" response-identifier="I137d2333-4bdf-446a-a4b7-9c97ff566462" expected-length="9"/> </p>
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
                                                   <qti-text-entry-interaction pattern-mask="^-?([0-9]{1,5})?$" response-identifier="gapController" expected-length="6"/>
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
                                                       <qti-text-entry-interaction pattern-mask="^(([0-1]?[0-9])|([2][0-3]))$" response-identifier="gapController" expected-length="2" timeSubType="hhmm"/>
						
							:
							
							<qti-text-entry-interaction pattern-mask="^([0-5]?[0-9])$" response-identifier="gapController" expected-length="2" timeSubType="hhmm"/>
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
                                                   <qti-text-entry-interaction pattern-mask="^.{0,5}$" response-identifier="gapController" expected-length="5"/>
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
                                                       <qti-text-entry-interaction pattern-mask="^(?![0])(([0-2]?[0-9])|([3][0-1]))$" response-identifier="gapController" expected-length="2" dateSubType="dutch"/>
							
							-
							
						
							<qti-text-entry-interaction pattern-mask="^(?![0])(([0-9])|([1][0-2]))$" response-identifier="gapController" expected-length="2" dateSubType="dutch"/>
							
							-
							
						
							<qti-text-entry-interaction pattern-mask="^([0-9]{1,4})$" response-identifier="gapController" expected-length="4" dateSubType="dutch"/>
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
                                                       <qti-text-entry-interaction pattern-mask="^(?![0])(([0-9])|([1][0-2]))$" response-identifier="gapController" expected-length="2" dateSubType="american"/>
							
							/
							
						
							<qti-text-entry-interaction pattern-mask="^(?![0])(([0-2]?[0-9])|([3][0-1]))$" response-identifier="gapController" expected-length="2" dateSubType="american"/>
							
							/
							
						
							<qti-text-entry-interaction pattern-mask="^([0-9]{1,4})$" response-identifier="gapController" expected-length="4" dateSubType="american"/>
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
                                        <qti-text-entry-interaction pattern-mask="^(([0-1]?[0-9])|([2][0-3]))$" response-identifier="Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" expected-length="2" timeSubType="hhmm"/>
							  
								    :
								    
								    <qti-text-entry-interaction pattern-mask="^([0-5]?[0-9])$" response-identifier="Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" expected-length="2" timeSubType="hhmm"/>
                                    </span> <span>
                                        <qti-text-entry-interaction pattern-mask="^(([0-1]?[0-9])|([2][0-3]))$" response-identifier="I0b525f2d-804e-42a1-b8da-9abbd6d0f6ae" expected-length="2" timeSubType="hhmm"/>
							  
								    :
								    
								    <qti-text-entry-interaction pattern-mask="^([0-5]?[0-9])$" response-identifier="I0b525f2d-804e-42a1-b8da-9abbd6d0f6ae" expected-length="2" timeSubType="hhmm"/>
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
                   <qti-text-entry-interaction pattern-mask='^-?([0-9]{1,5})?$' response-identifier='gapController' expected-length='6'/>
                   <qti-text-entry-interaction pattern-mask='^-?([0-9]{1,5})?$' response-identifier='gapController' expected-length='6'/>
               </itemBody>
           </wrapper>

        Private _itemBody6 As XElement =
            <wrapper>
                <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
                <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
                <itemBody class="defaultBody">
                    <div class="content">
                        <div>
                            <div xmlns="http://www.w3.org/1999/xhtml">
                                <div id="questionwithinlinecontrol">
                                    <p id="c1-id-11">
                                        <qti-text-entry-interaction pattern-mask="^-?([0-9]{1,5})?$" response-identifier="I812796ae-bcc4-4fcf-803c-85fdae53013d" expected-length="6"/>
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                </itemBody>
            </wrapper>



        Private _result1 As XElement =
        <root>
            <qti-response-declaration identifier="RESPONSE" cardinality="single" base-type="integer">
                <qti-correct-response interpretation="1#2#3">
                    <qti-value>1</qti-value>
                </qti-correct-response>
            </qti-response-declaration>
            <qti-response-declaration identifier="RESPONSE2" cardinality="single" base-type="string">
                <qti-correct-response interpretation="tekstje">
                    <qti-value>tekstje</qti-value>
                </qti-correct-response>
            </qti-response-declaration>
            <qti-response-declaration identifier="RESPONSE3" cardinality="single" base-type="string">
                <qti-correct-response interpretation="1.1#1.2">
                    <qti-value>1.1</qti-value>
                </qti-correct-response>
            </qti-response-declaration>
        </root>

        Private _result2 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="single" base-type="string"/>
                <qti-response-declaration identifier="RESPONSE2" cardinality="single" base-type="string"/>
                <qti-response-declaration identifier="RESPONSE3" cardinality="single" base-type="string"/>
            </root>

        Private _result3 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="single" base-type="integer">
                    <qti-correct-response interpretation="(1#2#3&amp;1.1#1.2)">
                        <qti-value>1</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
                <qti-response-declaration identifier="RESPONSE2" cardinality="single" base-type="string">
                    <qti-correct-response interpretation="tekstje">
                        <qti-value>tekstje</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
                <qti-response-declaration identifier="RESPONSE3" cardinality="single" base-type="string">
                    <qti-correct-response>
                        <qti-value>1.1</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
            </root>

        Private _result4 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="single" base-type="integer">
                    <qti-correct-response interpretation="(1#2#3&amp;1.1#1.2)|(4#5&amp;1.3)">
                        <qti-value>1</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
                <qti-response-declaration identifier="RESPONSE2" cardinality="single" base-type="string">
                    <qti-correct-response interpretation="tekstje">
                        <qti-value>tekstje</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
                <qti-response-declaration identifier="RESPONSE3" cardinality="single" base-type="string">
                    <qti-correct-response>
                        <qti-value>1.1</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
            </root>

        Private _result5 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="single" base-type="integer">
                    <qti-correct-response interpretation="100-999#&gt;1234">
                        <qti-value>100</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
                <qti-response-declaration identifier="RESPONSE2" cardinality="single" base-type="integer">
                    <qti-correct-response interpretation="12:34#21:34">
                        <qti-value>12</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
                <qti-response-declaration identifier="RESPONSE3" cardinality="single" base-type="integer">
                    <qti-correct-response>
                        <qti-value>34</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
                <qti-response-declaration identifier="RESPONSE4" cardinality="single" base-type="string">
                    <qti-correct-response interpretation="tekst">
                        <qti-value>tekst</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
            </root>

        Private _result6 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="single" base-type="integer">
                    <qti-correct-response interpretation="&gt;100#&lt;999">
                        <qti-value>101</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
                <qti-response-declaration identifier="RESPONSE2" cardinality="single" base-type="integer">
                    <qti-correct-response interpretation="12:34">
                        <qti-value>12</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
                <qti-response-declaration identifier="RESPONSE3" cardinality="single" base-type="integer">
                    <qti-correct-response>
                        <qti-value>34</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
                <qti-response-declaration identifier="RESPONSE4" cardinality="single" base-type="string">
                    <qti-correct-response interpretation="tekst">
                        <qti-value>tekst</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
            </root>

        Private _result7 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="single" base-type="integer">
                    <qti-correct-response interpretation="03/01/2015#05/01/2015">
                        <qti-value>03</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
                <qti-response-declaration identifier="RESPONSE2" cardinality="single" base-type="integer">
                    <qti-correct-response>
                        <qti-value>01</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
                <qti-response-declaration identifier="RESPONSE3" cardinality="single" base-type="integer">
                    <qti-correct-response>
                        <qti-value>2015</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
                <qti-response-declaration identifier="RESPONSE4" cardinality="single" base-type="integer">
                    <qti-correct-response interpretation="03/01/2015#05/01/2015">
                        <qti-value>03</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
                <qti-response-declaration identifier="RESPONSE5" cardinality="single" base-type="integer">
                    <qti-correct-response>
                        <qti-value>01</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
                <qti-response-declaration identifier="RESPONSE6" cardinality="single" base-type="integer">
                    <qti-correct-response>
                        <qti-value>2015</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
            </root>

        Private _result8 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="single" base-type="integer">
                    <qti-correct-response interpretation="10:00#11:00">
                        <qti-value>10</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
                <qti-response-declaration identifier="RESPONSE2" cardinality="single" base-type="integer">
                    <qti-correct-response>
                        <qti-value>00</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
                <qti-response-declaration identifier="RESPONSE3" cardinality="single" base-type="integer">
                    <qti-correct-response interpretation="12:00">
                        <qti-value>12</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
                <qti-response-declaration identifier="RESPONSE4" cardinality="single" base-type="integer">
                    <qti-correct-response>
                        <qti-value>00</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
            </root>

        Private _result9 As XElement =
         <root>
             <qti-response-declaration identifier="RESPONSE" cardinality="single" base-type="string">
                 <qti-correct-response interpretation="244400.00">
                     <qti-value>244400.00</qti-value>
                 </qti-correct-response>
             </qti-response-declaration>
             <qti-response-declaration identifier="RESPONSE2" cardinality="single" base-type="string"/>
         </root>

        Private _result10 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="single" base-type="string"/>
                <qti-response-declaration identifier="RESPONSE2" cardinality="single" base-type="string">
                    <qti-correct-response interpretation="244400.00">
                        <qti-value>244400.00</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
            </root>

        Private _result11 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="single" base-type="integer">
                    <qti-correct-response interpretation="1#2#3">
                        <qti-value>1</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
            </root>

        Private _result12 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="single" base-type="integer">
                    <qti-correct-response interpretation="1#2#3">
                        <qti-value>1</qti-value>
                    </qti-correct-response>
                    <qti-mapping>
                        <qti-map-entry map-key="1" mapped-value="1"/>
                        <qti-map-entry map-key="2" mapped-value="1"/>
                        <qti-map-entry map-key="3" mapped-value="1"/>
                    </qti-mapping>
                </qti-response-declaration>
            </root>


    End Class

End Namespace