

<TestClass()>
Public Class QTI22ResponseDeclarationHotspotTests
    Inherits QTI_Base.ResponseDeclarationHotspotTestsBase


    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub GetResponseDeclarationForCombinationOfFactSetsAndFactsOnFindingTest()
        QTI22PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution1, GetHotspotScoringParams(), _result1, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub GetResponseDeclarationForMultipleFactSetsTest()
        QTI22PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution2, GetHotspotScoringParams(), _result2, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub GetResponseDeclarationForFactSetsTest()
        QTI22PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution3, GetHotspotScoringParams(), _result3, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub GetResponseDeclarationForFactsOnFindingTest()
        QTI22PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution4, GetHotspotScoringParams(), _result4, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub GetEmptyResponseDeclarationTest()
        QTI22PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution5, GetHotspotScoringParams(), _result5, 1)
    End Sub


    Private _itemBody1 As XElement =
       <wrapper>
           <itemBody class="defaultBody">
               <div class="content">
                   <div>
                       <div>
                           <hotspotInteraction responseIdentifier="areaInteractionController" minChoices="1" maxChoices="5">
                               <object type="image/jpeg" data="resource://package/UK.jpg" width="197" height="256"/>
                               <hotspotChoice identifier="A" coords="44,45,40" shape="circle"/>
                               <hotspotChoice identifier="B" coords="148,45,40" shape="circle"/>
                               <hotspotChoice identifier="C" coords="45,133,40" shape="circle"/>
                               <hotspotChoice identifier="D" coords="149,129,40" shape="circle"/>
                               <hotspotChoice identifier="E" coords="150,214,40" shape="circle"/>
                           </hotspotInteraction>
                       </div>
                   </div>
               </div>
           </itemBody>
       </wrapper>



    Private _result1 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="identifier">
                <correctResponse interpretation="(A&amp;B)|(B&amp;C)&amp;E">
                    <value>A</value>
                    <value>B</value>
                    <value>E</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result2 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="identifier">
                <correctResponse interpretation="(A&amp;B)|(B&amp;C)&amp;E|D">
                    <value>A</value>
                    <value>B</value>
                    <value>E</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result3 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="identifier">
                <correctResponse interpretation="(A&amp;B&amp;E)|(B&amp;C)">
                    <value>A</value>
                    <value>B</value>
                    <value>E</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result4 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="identifier">
                <correctResponse interpretation="A&amp;B&amp;E">
                    <value>A</value>
                    <value>B</value>
                    <value>E</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result5 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="string"/>
        </responseDeclarations>


End Class
