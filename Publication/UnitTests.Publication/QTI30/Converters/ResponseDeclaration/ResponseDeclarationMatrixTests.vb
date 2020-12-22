Namespace QTI30

    <TestClass()>
    Public Class ResponseDeclarationMatrixTests
        Inherits QTI_Base.ResponseDeclarationMatrixTestsBase

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub GetResponseDeclarationForCombinationOfFactSetsAndFactsOnFindingTest()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution1, GetMatrixScoringParams(), _result1, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub GetResponseDeclarationForMultipleFactSetsTest()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution2, GetMatrixScoringParams(), _result2, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub GetResponseDeclarationForFactSetsTest()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution3, GetMatrixScoringParams(), _result3, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub GetEmptyResponseDeclarationTest()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution5, GetMatrixScoringParams(), _result5, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub MatrixWithDichotomousScoringWhenCreatingResponseDeclarationShouldCreateCorrectResponsesButNoMappings()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution6, GetMatrixScoringParams(), _result4, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub MatrixWithPolytomousScoringWhenCreatingResponseDeclarationShouldCreateCorrectResponsesAndMappings()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution4, GetMatrixScoringParams(), _result6, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub MatrixWithFactsetAndPolytomousScoringWhenCreatingResponseDeclarationShouldCreateCorrectResponsesButNoMappings()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution3, GetMatrixScoringParams(), _result3, 1)
        End Sub


        Private _itemBody1 As XElement =
           <wrapper>
               <itemBody class="defaultBody">
                   <div class="content">
                       <div>
                           <div>
                               <qti-match-interaction id="matchInteraction1" class="" max-associations="4" shuffle="false" response-identifier="matrix">
                                   <qti-simple-match-set>
                                       <qti-simple-associable-choice identifier="y_A" match-max="1">
                                           <div>
                                               <p id="c1-id-11">regel 1</p>
                                           </div>
                                       </qti-simple-associable-choice>
                                       <qti-simple-associable-choice identifier="y_B" match-max="1">
                                           <div>
                                               <p id="c1-id-11">regel 2</p>
                                           </div>
                                       </qti-simple-associable-choice>
                                       <qti-simple-associable-choice identifier="y_C" match-max="1">
                                           <div>
                                               <p id="c1-id-11">regel 3</p>
                                           </div>
                                       </qti-simple-associable-choice>
                                       <qti-simple-associable-choice identifier="y_D" match-max="1">
                                           <div>
                                               <p id="c1-id-11">regel 4</p>
                                           </div>
                                       </qti-simple-associable-choice>
                                   </qti-simple-match-set>
                                   <qti-simple-match-set>
                                       <qti-simple-associable-choice identifier="x_1" match-max="2">
                                           <div style="width: 194px;">
                                               <p id="c1-id-11">kolom 1</p>
                                           </div>
                                       </qti-simple-associable-choice>
                                       <qti-simple-associable-choice identifier="x_2" match-max="2">
                                           <div style="width: 194px;">
                                               <p id="c1-id-11">kolom 2</p>
                                           </div>
                                       </qti-simple-associable-choice>
                                       <qti-simple-associable-choice identifier="x_3" match-max="2">
                                           <div style="width: 194px;">
                                               <p id="c1-id-11">kolom 3</p>
                                           </div>
                                       </qti-simple-associable-choice>
                                   </qti-simple-match-set>
                               </qti-match-interaction>
                           </div>
                       </div>
                   </div>
               </itemBody>
           </wrapper>



        Private _result1 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="multiple" base-type="identifier">
                    <qti-correct-response interpretation="(A&amp;B)|(B&amp;A)&amp;C&amp;A">
                        <qti-value>y_A x_1</qti-value>
                        <qti-value>y_B x_2</qti-value>
                        <qti-value>y_C x_3</qti-value>
                        <qti-value>y_D x_1</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
            </root>

        Private _result2 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="multiple" base-type="identifier">
                    <qti-correct-response interpretation="(A&amp;B)|(B&amp;A)&amp;(C&amp;A)|(B&amp;C)">
                        <qti-value>y_A x_1</qti-value>
                        <qti-value>y_B x_2</qti-value>
                        <qti-value>y_C x_3</qti-value>
                        <qti-value>y_D x_1</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
            </root>

        Private _result3 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="multiple" base-type="identifier">
                    <qti-correct-response interpretation="(A&amp;B&amp;C&amp;A)|(B&amp;A&amp;B&amp;C)">
                        <qti-value>y_A x_1</qti-value>
                        <qti-value>y_B x_2</qti-value>
                        <qti-value>y_C x_3</qti-value>
                        <qti-value>y_D x_1</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
            </root>

        Private _result4 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="multiple" base-type="identifier">
                    <qti-correct-response interpretation="A&amp;B&amp;C&amp;A">
                        <qti-value>y_A x_1</qti-value>
                        <qti-value>y_B x_2</qti-value>
                        <qti-value>y_C x_3</qti-value>
                        <qti-value>y_D x_1</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
            </root>

        Private _result5 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="multiple" base-type="string"/>
            </root>

        Private _result6 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="multiple" base-type="identifier">
                    <qti-correct-response interpretation="A&amp;B&amp;C&amp;A">
                        <qti-value>y_A x_1</qti-value>
                        <qti-value>y_B x_2</qti-value>
                        <qti-value>y_C x_3</qti-value>
                        <qti-value>y_D x_1</qti-value>
                    </qti-correct-response>
                    <qti-mapping>
                        <qti-map-entry map-key="y_A x_1" mapped-value="1"/>
                        <qti-map-entry map-key="y_B x_2" mapped-value="1"/>
                        <qti-map-entry map-key="y_C x_3" mapped-value="1"/>
                        <qti-map-entry map-key="y_D x_1" mapped-value="1"/>
                    </qti-mapping>
                </qti-response-declaration>
            </root>


    End Class

End Namespace