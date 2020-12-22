
Namespace QTI30

    <TestClass()>
    Public Class ResponseDeclarationHotspotTests
        Inherits QTI_Base.ResponseDeclarationHotspotTestsBase


        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub GetResponseDeclarationForCombinationOfFactSetsAndFactsOnFindingTest()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution1, GetHotspotScoringParams(), _result1, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub GetResponseDeclarationForMultipleFactSetsTest()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution2, GetHotspotScoringParams(), _result2, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub GetResponseDeclarationForFactSetsTest()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution3, GetHotspotScoringParams(), _result3, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub ItemWithOneHotspotInteractionAndDichotomousScoringWhenCreatingResponseDeclarationShouldCreateCorrectResponsesButNoMappings()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution4, GetHotspotScoringParams(), _result4, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub ItemWithOneHotspotInteractionAndPolytomousScoringWhenCreatingResponseDeclarationShouldCreateCorrectResponsesButNoMappings()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution6, GetHotspotScoringParams(), _result6, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub GetEmptyResponseDeclarationTest()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution5, GetHotspotScoringParams(), _result5, 1)
        End Sub


        Private _itemBody1 As XElement =
           <wrapper>
               <itemBody class="defaultBody">
                   <div class="content">
                       <div>
                           <div>
                               <qti-hotspot-interaction response-identifier="areaInteractionController" min-choices="1" max-choices="5">
                                   <object type="image/jpeg" data="resource://package/UK.jpg" width="197" height="256"/>
                                   <qti-hotspot-choice identifier="A" coords="44,45,40" shape="circle"/>
                                   <qti-hotspot-choice identifier="B" coords="148,45,40" shape="circle"/>
                                   <qti-hotspot-choice identifier="C" coords="45,133,40" shape="circle"/>
                                   <qti-hotspot-choice identifier="D" coords="149,129,40" shape="circle"/>
                                   <qti-hotspot-choice identifier="E" coords="150,214,40" shape="circle"/>
                               </qti-hotspot-interaction>
                           </div>
                       </div>
                   </div>
               </itemBody>
           </wrapper>



        Private _result1 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="multiple" base-type="identifier">
                    <qti-correct-response interpretation="(A&amp;B)|(B&amp;C)&amp;E">
                        <qti-value>A</qti-value>
                        <qti-value>B</qti-value>
                        <qti-value>E</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
            </root>

        Private _result2 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="multiple" base-type="identifier">
                    <qti-correct-response interpretation="(A&amp;B)|(B&amp;C)&amp;E|D">
                        <qti-value>A</qti-value>
                        <qti-value>B</qti-value>
                        <qti-value>E</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
            </root>

        Private _result3 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="multiple" base-type="identifier">
                    <qti-correct-response interpretation="(A&amp;B&amp;E)|(B&amp;C)">
                        <qti-value>A</qti-value>
                        <qti-value>B</qti-value>
                        <qti-value>E</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
            </root>

        Private _result4 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="multiple" base-type="identifier">
                    <qti-correct-response interpretation="A&amp;B&amp;E">
                        <qti-value>A</qti-value>
                        <qti-value>B</qti-value>
                        <qti-value>E</qti-value>
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
                    <qti-correct-response interpretation="A&amp;B&amp;E">
                        <qti-value>A</qti-value>
                        <qti-value>B</qti-value>
                        <qti-value>E</qti-value>
                    </qti-correct-response>
                    <qti-mapping>
                        <qti-map-entry map-key="A" mapped-value="1"/>
                        <qti-map-entry map-key="B" mapped-value="1"/>
                        <qti-map-entry map-key="E" mapped-value="1"/>
                    </qti-mapping>
                </qti-response-declaration>
            </root>


    End Class

End Namespace