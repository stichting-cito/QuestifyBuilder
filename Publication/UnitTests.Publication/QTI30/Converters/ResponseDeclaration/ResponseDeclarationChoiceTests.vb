Namespace QTI30

    <TestClass()>
    Public Class ResponseDeclarationChoiceTests
        Inherits QTI_Base.ResponseDeclarationChoiceTestsBase

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub IC_Test()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution1, GetInlineChoiceScoringParams(4), _result1, 4)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub IC_NoFinding_Test()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution2, GetInlineChoiceScoringParams(4), _result2, 4)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub IC_Factset_Test()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution3, GetInlineChoiceScoringParams(4), _result3, 4)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub IC_FindingFactsAndFactset_Test()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution4, GetInlineChoiceScoringParams(4), _result4, 4)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub IC_Factsets_Test()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution5, GetInlineChoiceScoringParams(4), _result5, 4)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub IC_InCompleteFinding_Test()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution6, GetInlineChoiceScoringParams(4), _result6, 4)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub IC_Factset_InCompleteFinding_Test()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution7, GetInlineChoiceScoringParams(4), _result7, 4)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub ItemWithOneInlineChoiceInteractionAndDichotomousScoringWhenCreatingResponseDeclarationShouldCreateCorrectResponseButNoMappings()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody2, _solution8, GetInlineChoiceScoringParams(1), _result8, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub ItemWithOneInlineChoiceInteractionAndPolytomousScoringWhenCreatingResponseDeclarationShouldCreateCorrectResponseAndMappings()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody2, _solution9, GetInlineChoiceScoringParams(1), _result9, 1)
        End Sub


        Private _itemBody1 As XElement =
           <wrapper>
               <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
               <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
               <itemBody class="defaultBody">
                   <div class="content">
                       <div xmlns="http://www.w3.org/1999/xhtml">
                           <div id="questionwithinlinecontrol">
                               <p id="c1-id-11">Drop down 1 <qti-inline-choice-interaction response-identifier="I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" shuffle="false" required="true">
                                       <qti-inline-choice identifier="A">alt.A</qti-inline-choice>
                                       <qti-inline-choice identifier="B">alt.B</qti-inline-choice>
                                       <qti-inline-choice identifier="C">alt.C</qti-inline-choice>
                                       <qti-inline-choice identifier="D">alt.D</qti-inline-choice>
                                   </qti-inline-choice-interaction>
                               </p>
                               <p id="c1-id-12">Drop down 2 <qti-inline-choice-interaction response-identifier="Ie830b508-32a0-42f4-920f-0e06f0f19313" shuffle="false" required="true">
                                       <qti-inline-choice identifier="A">opt.1</qti-inline-choice>
                                       <qti-inline-choice identifier="B">opt.2</qti-inline-choice>
                                       <qti-inline-choice identifier="C">opt.3</qti-inline-choice>
                                       <qti-inline-choice identifier="D">opt.4</qti-inline-choice>
                                       <qti-inline-choice identifier="E">opt.5</qti-inline-choice>
                                   </qti-inline-choice-interaction>
                               </p>
                               <p id="c1-id-13">Drop down 3 <qti-inline-choice-interaction response-identifier="I22f81e91-aaa4-4528-999e-61b31b8123ec" shuffle="false" required="true">
                                       <qti-inline-choice identifier="A">keuze 1</qti-inline-choice>
                                       <qti-inline-choice identifier="B">keuze 2</qti-inline-choice>
                                   </qti-inline-choice-interaction>
                               </p>
                               <p id="c1-id-14">Drop down 4 <qti-inline-choice-interaction response-identifier="I5196c3f8-ddf3-4423-b777-d1d2c71d6efb" shuffle="false" required="true">
                                       <qti-inline-choice identifier="A">A</qti-inline-choice>
                                       <qti-inline-choice identifier="B">B</qti-inline-choice>
                                       <qti-inline-choice identifier="C">C</qti-inline-choice>
                                       <qti-inline-choice identifier="D">D</qti-inline-choice>
                                       <qti-inline-choice identifier="E">E</qti-inline-choice>
                                       <qti-inline-choice identifier="F">F</qti-inline-choice>
                                   </qti-inline-choice-interaction>
                               </p>
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
                           <div id="questionwithinlinecontrol">
                               <p id="c1-id-11">Drop down 1 <qti-inline-choice-interaction response-identifier="I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" shuffle="false" required="true">
                                       <qti-inline-choice identifier="A">alt.A</qti-inline-choice>
                                       <qti-inline-choice identifier="B">alt.B</qti-inline-choice>
                                       <qti-inline-choice identifier="C">alt.C</qti-inline-choice>
                                       <qti-inline-choice identifier="D">alt.D</qti-inline-choice>
                                   </qti-inline-choice-interaction>
                               </p>
                           </div>
                       </div>
                   </div>
               </itemBody>
           </wrapper>



        Private _result1 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="single" base-type="identifier">
                    <qti-correct-response interpretation="C">
                        <qti-value>C</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
                <qti-response-declaration identifier="RESPONSE2" cardinality="single" base-type="identifier">
                    <qti-correct-response interpretation="E">
                        <qti-value>E</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
                <qti-response-declaration identifier="RESPONSE3" cardinality="single" base-type="identifier">
                    <qti-correct-response interpretation="A">
                        <qti-value>A</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
                <qti-response-declaration identifier="RESPONSE4" cardinality="single" base-type="identifier">
                    <qti-correct-response interpretation="F">
                        <qti-value>F</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
            </root>

        Private _result2 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="single" base-type="string"/>
                <qti-response-declaration identifier="RESPONSE2" cardinality="single" base-type="string"/>
                <qti-response-declaration identifier="RESPONSE3" cardinality="single" base-type="string"/>
                <qti-response-declaration identifier="RESPONSE4" cardinality="single" base-type="string"/>
            </root>

        Private _result3 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="single" base-type="identifier">
                    <qti-correct-response interpretation="(A&amp;A&amp;A&amp;F)|(B&amp;B&amp;B&amp;F)|(D&amp;E&amp;B&amp;F)">
                        <qti-value>A</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
                <qti-response-declaration identifier="RESPONSE2" cardinality="single" base-type="identifier">
                    <qti-correct-response>
                        <qti-value>A</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
                <qti-response-declaration identifier="RESPONSE3" cardinality="single" base-type="identifier">
                    <qti-correct-response>
                        <qti-value>A</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
                <qti-response-declaration identifier="RESPONSE4" cardinality="single" base-type="identifier">
                    <qti-correct-response>
                        <qti-value>F</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
            </root>

        Private _result4 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="single" base-type="identifier">
                    <qti-correct-response interpretation="A">
                        <qti-value>A</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
                <qti-response-declaration identifier="RESPONSE2" cardinality="single" base-type="identifier">
                    <qti-correct-response interpretation="(A&amp;A)|(B&amp;B)|(E&amp;A)">
                        <qti-value>A</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
                <qti-response-declaration identifier="RESPONSE3" cardinality="single" base-type="identifier">
                    <qti-correct-response>
                        <qti-value>A</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
                <qti-response-declaration identifier="RESPONSE4" cardinality="single" base-type="identifier">
                    <qti-correct-response interpretation="F">
                        <qti-value>F</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
            </root>

        Private _result5 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="single" base-type="identifier">
                    <qti-correct-response interpretation="(C&amp;A)">
                        <qti-value>C</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
                <qti-response-declaration identifier="RESPONSE2" cardinality="single" base-type="identifier">
                    <qti-correct-response interpretation="(E&amp;F)|(E&amp;D)">
                        <qti-value>E</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
                <qti-response-declaration identifier="RESPONSE3" cardinality="single" base-type="identifier">
                    <qti-correct-response>
                        <qti-value>A</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
                <qti-response-declaration identifier="RESPONSE4" cardinality="single" base-type="identifier">
                    <qti-correct-response>
                        <qti-value>F</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
            </root>

        Private _result6 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="single" base-type="string"/>
                <qti-response-declaration identifier="RESPONSE2" cardinality="single" base-type="identifier">
                    <qti-correct-response interpretation="B">
                        <qti-value>B</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
                <qti-response-declaration identifier="RESPONSE3" cardinality="single" base-type="string"/>
                <qti-response-declaration identifier="RESPONSE4" cardinality="single" base-type="identifier">
                    <qti-correct-response interpretation="F">
                        <qti-value>F</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
            </root>

        Private _result7 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="single" base-type="string"/>
                <qti-response-declaration identifier="RESPONSE2" cardinality="single" base-type="identifier">
                    <qti-correct-response interpretation="(B&amp;F)">
                        <qti-value>B</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
                <qti-response-declaration identifier="RESPONSE3" cardinality="single" base-type="string"/>
                <qti-response-declaration identifier="RESPONSE4" cardinality="single" base-type="identifier">
                    <qti-correct-response>
                        <qti-value>F</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
            </root>

        Private _result8 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="single" base-type="identifier">
                    <qti-correct-response interpretation="C">
                        <qti-value>C</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
            </root>

        Private _result9 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="single" base-type="identifier">
                    <qti-correct-response interpretation="C">
                        <qti-value>C</qti-value>
                    </qti-correct-response>
                    <qti-mapping>
                        <qti-map-entry map-key="C" mapped-value="1"/>
                    </qti-mapping>
                </qti-response-declaration>
            </root>


    End Class

End Namespace