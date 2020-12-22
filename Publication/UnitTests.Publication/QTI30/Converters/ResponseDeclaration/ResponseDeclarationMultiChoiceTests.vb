Namespace QTI30

    <TestClass()>
    Public Class ResponseDeclarationMultiChoiceTests
        Inherits QTI_Base.ResponseDeclarationMultiChoiceTestsBase

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub MC_Test()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution1, GetMcScoringParams(), _result1, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub MC_NoFinding_Test()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution2, GetMcScoringParams(), _result2, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub MR_Test()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody2, _solution3, GetMrScoringParams(), _result3, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub MR_Factset_Test()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody2, _solution4, GetMrScoringParams(), _result4, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub MR_FindingFactsAndFactset_Test()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody2, _solution5, GetMrScoringParams(), _result5, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub MR_Factsets_Test()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody2, _solution6, GetMrScoringParams(), _result6, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub MultipleResponseKeyFactsWithoutTrueValuesTest()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution7, GetMrScoringParams(), _result7, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub MRWithPolytomousScoringWhenCreatingResponseDeclarationShouldCreateCorrectResponsesAndMappings()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody2, _solution8, GetMrScoringParams(), _result8, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub MRWithFactsetAndPolytomousScoringWhenCreatingResponseDeclarationShouldCreateCorrectResponsesButNoMappings()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody2, _solution9, GetMrScoringParams(), _result4, 1)
        End Sub


        Private _itemBody1 As XElement =
           <wrapper>
               <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
               <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
               <itemBody class="defaultBody">
                   <div class="content">
                       <div>
                           <div id="question">
                               <p id="c1-id-11">Welke van de onderstaande stellingen zijn waar?</p>
                           </div>
                           <div id="mc">
                               <qti-choice-interaction id="choiceInteraction1" class="" max-choices="1" shuffle="false" response-identifier="mc">
                                   <qti-simple-choice identifier="A">
                                       <p id="c1-id-11">A</p>
                                   </qti-simple-choice>
                                   <qti-simple-choice identifier="B">
                                       <p id="c1-id-11">B</p>
                                   </qti-simple-choice>
                                   <qti-simple-choice identifier="C">
                                       <p id="c1-id-11">C</p>
                                   </qti-simple-choice>
                                   <qti-simple-choice identifier="D">
                                       <p id="c1-id-11">D</p>
                                   </qti-simple-choice>
                               </qti-choice-interaction>
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
                       <div>
                           <div id="question">
                               <p id="c1-id-11">Welke van de onderstaande stellingen zijn waar?</p>
                           </div>
                           <div id="mc">
                               <qti-choice-interaction id="choiceInteraction1" class="" max-choices="0" shuffle="false" response-identifier="mc">
                                   <qti-simple-choice identifier="A">
                                       <p id="c1-id-11">A</p>
                                   </qti-simple-choice>
                                   <qti-simple-choice identifier="B">
                                       <p id="c1-id-11">B</p>
                                   </qti-simple-choice>
                                   <qti-simple-choice identifier="C">
                                       <p id="c1-id-11">C</p>
                                   </qti-simple-choice>
                                   <qti-simple-choice identifier="D">
                                       <p id="c1-id-11">D</p>
                                   </qti-simple-choice>
                                   <qti-simple-choice identifier="E">
                                       <p id="c1-id-11">E</p>
                                   </qti-simple-choice>
                               </qti-choice-interaction>
                           </div>
                       </div>
                   </div>
               </itemBody>
           </wrapper>



        Private _result1 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="single" base-type="identifier">
                    <qti-correct-response interpretation="A">
                        <qti-value>A</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
            </root>

        Private _result2 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="single" base-type="string"/>
            </root>

        Private _result3 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="multiple" base-type="identifier">
                    <qti-correct-response interpretation="A&amp;C&amp;E">
                        <qti-value>A</qti-value>
                        <qti-value>C</qti-value>
                        <qti-value>E</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
            </root>

        Private _result4 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="multiple" base-type="identifier">
                    <qti-correct-response interpretation="(B&amp;C&amp;E)|(A&amp;B&amp;E)">
                        <qti-value>B</qti-value>
                        <qti-value>C</qti-value>
                        <qti-value>E</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
            </root>

        Private _result5 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="multiple" base-type="identifier">
                    <qti-correct-response interpretation="A&amp;(D&amp;E)|(C&amp;D)">
                        <qti-value>A</qti-value>
                        <qti-value>D</qti-value>
                        <qti-value>E</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
            </root>

        Private _result6 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="multiple" base-type="identifier">
                    <qti-correct-response interpretation="A|B&amp;C&amp;E|D">
                        <qti-value>A</qti-value>
                        <qti-value>C</qti-value>
                        <qti-value>E</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
            </root>

        Private _result7 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="single" base-type="string"/>
            </root>

        Private _result8 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="multiple" base-type="identifier">
                    <qti-correct-response interpretation="A&amp;C&amp;E">
                        <qti-value>A</qti-value>
                        <qti-value>C</qti-value>
                        <qti-value>E</qti-value>
                    </qti-correct-response>
                    <qti-mapping>
                        <qti-map-entry map-key="A" mapped-value="1"/>
                        <qti-map-entry map-key="C" mapped-value="1"/>
                        <qti-map-entry map-key="E" mapped-value="1"/>
                    </qti-mapping>
                </qti-response-declaration>
            </root>


    End Class

End Namespace