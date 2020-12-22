<TestClass()>
Public Class QTI22ResponseDeclarationMatrixTests
    Inherits QTI_Base.ResponseDeclarationMatrixTestsBase

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub GetResponseDeclarationForCombinationOfFactSetsAndFactsOnFindingTest()
        QTI22PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution1, GetMatrixScoringParams(), _result1, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub GetResponseDeclarationForMultipleFactSetsTest()
        QTI22PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution2, GetMatrixScoringParams(), _result2, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub GetResponseDeclarationForFactSetsTest()
        QTI22PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution3, GetMatrixScoringParams(), _result3, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub GetResponseDeclarationForFactsOnFindingTest()
        QTI22PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution4, GetMatrixScoringParams(), _result4, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub GetEmptyResponseDeclarationTest()
        QTI22PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution5, GetMatrixScoringParams(), _result5, 1)
    End Sub


    Private _itemBody1 As XElement =
       <wrapper>
           <itemBody class="defaultBody">
               <div class="content">
                   <div>
                       <div>
                           <matchInteraction id="matchInteraction1" class="" maxAssociations="4" shuffle="false" responseIdentifier="matrix">
                               <simpleMatchSet>
                                   <simpleAssociableChoice identifier="y_A" matchMax="1">
                                       <div>
                                           <p id="c1-id-11">regel 1</p>
                                       </div>
                                   </simpleAssociableChoice>
                                   <simpleAssociableChoice identifier="y_B" matchMax="1">
                                       <div>
                                           <p id="c1-id-11">regel 2</p>
                                       </div>
                                   </simpleAssociableChoice>
                                   <simpleAssociableChoice identifier="y_C" matchMax="1">
                                       <div>
                                           <p id="c1-id-11">regel 3</p>
                                       </div>
                                   </simpleAssociableChoice>
                                   <simpleAssociableChoice identifier="y_D" matchMax="1">
                                       <div>
                                           <p id="c1-id-11">regel 4</p>
                                       </div>
                                   </simpleAssociableChoice>
                               </simpleMatchSet>
                               <simpleMatchSet>
                                   <simpleAssociableChoice identifier="x_1" matchMax="2">
                                       <div style="width: 194px;">
                                           <p id="c1-id-11">kolom 1</p>
                                       </div>
                                   </simpleAssociableChoice>
                                   <simpleAssociableChoice identifier="x_2" matchMax="2">
                                       <div style="width: 194px;">
                                           <p id="c1-id-11">kolom 2</p>
                                       </div>
                                   </simpleAssociableChoice>
                                   <simpleAssociableChoice identifier="x_3" matchMax="2">
                                       <div style="width: 194px;">
                                           <p id="c1-id-11">kolom 3</p>
                                       </div>
                                   </simpleAssociableChoice>
                               </simpleMatchSet>
                           </matchInteraction>
                       </div>
                   </div>
               </div>
           </itemBody>
       </wrapper>



    Private _result1 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="identifier">
                <correctResponse interpretation="(A&amp;B)|(B&amp;A)&amp;C&amp;A">
                    <value>y_A x_1</value>
                    <value>y_B x_2</value>
                    <value>y_C x_3</value>
                    <value>y_D x_1</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result2 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="identifier">
                <correctResponse interpretation="(A&amp;B)|(B&amp;A)&amp;(C&amp;A)|(B&amp;C)">
                    <value>y_A x_1</value>
                    <value>y_B x_2</value>
                    <value>y_C x_3</value>
                    <value>y_D x_1</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result3 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="identifier">
                <correctResponse interpretation="(A&amp;B&amp;C&amp;A)|(B&amp;A&amp;B&amp;C)">
                    <value>y_A x_1</value>
                    <value>y_B x_2</value>
                    <value>y_C x_3</value>
                    <value>y_D x_1</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result4 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="identifier">
                <correctResponse interpretation="A&amp;B&amp;C&amp;A">
                    <value>y_A x_1</value>
                    <value>y_B x_2</value>
                    <value>y_C x_3</value>
                    <value>y_D x_1</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result5 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="string"/>
        </responseDeclarations>


End Class
