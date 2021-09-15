Namespace QTI30

    <TestClass()>
    Public Class ResponseDeclarationOrderTests
        Inherits QTI_Base.ResponseDeclarationOrderTestsBase

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub GetResponseDeclarationForFactSetsTest()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution1, GetOrderScoringParams(), _result1, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub GetResponseDeclarationForMixedUpFactsTest()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution3, GetOrderScoringParams(), _result3, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub GetEmptyResponseDeclarationTest()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution4, GetOrderScoringParams(), _result4, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub ItemWithOneOrderInteractionAndDichotomousScoringWhenCreatingResponseDeclarationShouldCreateCorrectResponsesButNoMappings()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution2, GetOrderScoringParams(), _result2, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub ItemWithOneOrderInteractionAndPolytomousScoringWhenCreatingResponseDeclarationShouldCreateCorrectResponsesButNoMappings()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution5, GetOrderScoringParams(), _result5, 1)
        End Sub


        Private _itemBody1 As XElement =
           <wrapper>
               <stylesheet href="resource://package/itemstyle.css" type="text/css"/>
               <stylesheet href="resource://package/userstyle.css" type="text/css"/>
               <itemBody class="defaultBody">
                   <div class="content">
                       <styles xmlns="http://www.w3.org/1999/xhtml">
                           <stylecollection>
                               <style classname=".vertical .orderInteractionList" attributename="width" value="auto"/>
                           </stylecollection>
                       </styles>
                       <div xmlns="http://www.w3.org/1999/xhtml">
                           <div id="question">
                               <p id="c1-id-11">Zet de alternatieven in de juiste volgorde</p>
                           </div>
                           <div id="answer">
                               <qti-order-interaction response-identifier="orderController" shuffle="false" orientation="vertical" min-choices="1">
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
                                   <qti-simple-choice identifier="F">
                                       <p id="c1-id-11">F</p>
                                   </qti-simple-choice>
                               </qti-order-interaction>
                           </div>
                       </div>
                   </div>
               </itemBody>
           </wrapper>



        Private _result1 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="ordered" base-type="identifier">
                    <qti-correct-response interpretation="(C&amp;D&amp;E&amp;F&amp;A&amp;B)|(D&amp;E&amp;F&amp;A&amp;B&amp;C)">
                        <qti-value>C</qti-value>
                        <qti-value>D</qti-value>
                        <qti-value>E</qti-value>
                        <qti-value>F</qti-value>
                        <qti-value>A</qti-value>
                        <qti-value>B</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
            </root>

        Private _result2 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="ordered" base-type="identifier">
                    <qti-correct-response interpretation="C&amp;D&amp;E&amp;F&amp;A&amp;B">
                        <qti-value>C</qti-value>
                        <qti-value>D</qti-value>
                        <qti-value>E</qti-value>
                        <qti-value>F</qti-value>
                        <qti-value>A</qti-value>
                        <qti-value>B</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
            </root>

        Private _result3 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="ordered" base-type="identifier">
                    <qti-correct-response interpretation="(C&amp;D&amp;E&amp;F&amp;A&amp;B)">
                        <qti-value>C</qti-value>
                        <qti-value>D</qti-value>
                        <qti-value>E</qti-value>
                        <qti-value>F</qti-value>
                        <qti-value>A</qti-value>
                        <qti-value>B</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
            </root>

        Private _result4 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="ordered" base-type="string"/>
            </root>

        Private _result5 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="ordered" base-type="identifier">
                    <qti-correct-response interpretation="C&amp;D&amp;E&amp;F&amp;A&amp;B">
                        <qti-value>C</qti-value>
                        <qti-value>D</qti-value>
                        <qti-value>E</qti-value>
                        <qti-value>F</qti-value>
                        <qti-value>A</qti-value>
                        <qti-value>B</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
            </root>


    End Class

End Namespace