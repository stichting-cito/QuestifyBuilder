Namespace QTI30

    <TestClass()>
    Public Class ResponseDeclarationGapMatchTests
        Inherits QTI_Base.ResponseDeclarationGapMatchTestsBase

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub GetResponseDeclarationForCombinationOfFactSetsAndFactsOnFindingTest()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution1, GetGapMatchScoringParams(), _result1, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub GetResponseDeclarationForMultipleFactSetsTest()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution2, GetGapMatchScoringParams(), _result2, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub GetResponseDeclarationForFactSetsTest()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution3, GetGapMatchScoringParams(), _result3, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub GetEmptyResponseDeclarationTest()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution5, GetGapMatchScoringParams(), _result5, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub GetResponseDeclarationWithNoValueTest()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution6, GetGapMatchScoringParams(), _result6, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub ItemWithOneGapMatchInteractionAndDichotomousScoringWhenCreatingResponseDeclarationShouldCreateCorrectResponsesButNoMappings()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution4, GetGapMatchScoringParams(), _result4, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub ItemWithOneGapMatchInteractionAndPolytomousScoringWhenCreatingResponseDeclarationShouldCreateCorrectResponsesAndMappings()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution7, GetGapMatchScoringParams(), _result7, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub ItemWithOneGapMatchInteractionAndPolytomousScoringWithTranslatedScoresWhenCreatingResponseDeclarationShouldCreateCorrectResponsesButNoMappings()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution8, GetGapMatchScoringParams(), _result4, 1)
        End Sub


        Private _itemBody1 As XElement =
           <wrapper>
               <itemBody class="defaultBody">
                   <div class="content">
                       <div>
                           <div>
                               <qti-gap-match-interaction response-identifier="gapMatchController" shuffle="false">
                                   <qti-gap-text identifier="A" match-max="1">A</qti-gap-text>
                                   <qti-gap-text identifier="B" match-max="1">B</qti-gap-text>
                                   <qti-gap-text identifier="C" match-max="1">C</qti-gap-text>
                                   <qti-gap-text identifier="D" match-max="1">D</qti-gap-text>
                                   <qti-gap-text identifier="E" match-max="1">E</qti-gap-text>
                                   <qti-gap-text identifier="F" match-max="1">F</qti-gap-text>
                                   <p id="c1-id-11">Tekst met <span>
                                           <qti-gap identifier="I51faf178-ca03-41eb-8276-385ef2a185b3" required="true"/>
                                       </span> een aantal <span>
                                           <qti-gap identifier="I989f6f3c-9d38-492f-80fe-4b71cfee574f" required="true"/>
                                       </span> gaten waarin teksten <span>
                                           <qti-gap identifier="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" required="true"/>
                                       </span> kunnen worden gesleept <span>
                                           <qti-gap identifier="I723529e7-8893-455e-b785-595592528040" required="true"/>
                                       </span> door de kandidaat.</p>
                               </qti-gap-match-interaction>
                           </div>
                       </div>
                   </div>
               </itemBody>
           </wrapper>



        Private _result1 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="multiple" base-type="directedPair">
                    <qti-correct-response interpretation="A I51faf178-ca03-41eb-8276-385ef2a185b3&amp;(B I989f6f3c-9d38-492f-80fe-4b71cfee574f&amp;C Ie1f57945-a74c-4f6c-948b-5133fb9778e8)|(B I989f6f3c-9d38-492f-80fe-4b71cfee574f&amp;A Ie1f57945-a74c-4f6c-948b-5133fb9778e8)|(E I989f6f3c-9d38-492f-80fe-4b71cfee574f&amp;D Ie1f57945-a74c-4f6c-948b-5133fb9778e8)&amp;D I723529e7-8893-455e-b785-595592528040">
                        <qti-value>A I51faf178-ca03-41eb-8276-385ef2a185b3</qti-value>
                        <qti-value>B I989f6f3c-9d38-492f-80fe-4b71cfee574f</qti-value>
                        <qti-value>C Ie1f57945-a74c-4f6c-948b-5133fb9778e8</qti-value>
                        <qti-value>D I723529e7-8893-455e-b785-595592528040</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
            </root>

        Private _result2 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="multiple" base-type="directedPair">
                    <qti-correct-response interpretation="(A I51faf178-ca03-41eb-8276-385ef2a185b3&amp;C Ie1f57945-a74c-4f6c-948b-5133fb9778e8)|(C I51faf178-ca03-41eb-8276-385ef2a185b3&amp;A Ie1f57945-a74c-4f6c-948b-5133fb9778e8)|(F I51faf178-ca03-41eb-8276-385ef2a185b3&amp;D Ie1f57945-a74c-4f6c-948b-5133fb9778e8)&amp;(B I989f6f3c-9d38-492f-80fe-4b71cfee574f&amp;D I723529e7-8893-455e-b785-595592528040)|(E I989f6f3c-9d38-492f-80fe-4b71cfee574f&amp;F I723529e7-8893-455e-b785-595592528040)">
                        <qti-value>A I51faf178-ca03-41eb-8276-385ef2a185b3</qti-value>
                        <qti-value>B I989f6f3c-9d38-492f-80fe-4b71cfee574f</qti-value>
                        <qti-value>C Ie1f57945-a74c-4f6c-948b-5133fb9778e8</qti-value>
                        <qti-value>D I723529e7-8893-455e-b785-595592528040</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
            </root>

        Private _result3 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="multiple" base-type="directedPair">
                    <qti-correct-response interpretation="(A I51faf178-ca03-41eb-8276-385ef2a185b3&amp;B I989f6f3c-9d38-492f-80fe-4b71cfee574f&amp;C Ie1f57945-a74c-4f6c-948b-5133fb9778e8&amp;D I723529e7-8893-455e-b785-595592528040)|(D I51faf178-ca03-41eb-8276-385ef2a185b3&amp;C I989f6f3c-9d38-492f-80fe-4b71cfee574f&amp;B Ie1f57945-a74c-4f6c-948b-5133fb9778e8&amp;A I723529e7-8893-455e-b785-595592528040)|(F I51faf178-ca03-41eb-8276-385ef2a185b3&amp;E I989f6f3c-9d38-492f-80fe-4b71cfee574f&amp;B Ie1f57945-a74c-4f6c-948b-5133fb9778e8&amp;A I723529e7-8893-455e-b785-595592528040)">
                        <qti-value>A I51faf178-ca03-41eb-8276-385ef2a185b3</qti-value>
                        <qti-value>B I989f6f3c-9d38-492f-80fe-4b71cfee574f</qti-value>
                        <qti-value>C Ie1f57945-a74c-4f6c-948b-5133fb9778e8</qti-value>
                        <qti-value>D I723529e7-8893-455e-b785-595592528040</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
            </root>

        Private _result4 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="multiple" base-type="directedPair">
                    <qti-correct-response interpretation="A I51faf178-ca03-41eb-8276-385ef2a185b3&amp;B I989f6f3c-9d38-492f-80fe-4b71cfee574f&amp;C Ie1f57945-a74c-4f6c-948b-5133fb9778e8&amp;D I723529e7-8893-455e-b785-595592528040">
                        <qti-value>A I51faf178-ca03-41eb-8276-385ef2a185b3</qti-value>
                        <qti-value>B I989f6f3c-9d38-492f-80fe-4b71cfee574f</qti-value>
                        <qti-value>C Ie1f57945-a74c-4f6c-948b-5133fb9778e8</qti-value>
                        <qti-value>D I723529e7-8893-455e-b785-595592528040</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
            </root>

        Private _result5 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="multiple" base-type="string"/>
            </root>

        Private _result6 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="multiple" base-type="directedPair">
                    <qti-correct-response interpretation="Ø I51faf178-ca03-41eb-8276-385ef2a185b3&amp;(B I989f6f3c-9d38-492f-80fe-4b71cfee574f&amp;Ø Ie1f57945-a74c-4f6c-948b-5133fb9778e8)|(Ø I989f6f3c-9d38-492f-80fe-4b71cfee574f&amp;A Ie1f57945-a74c-4f6c-948b-5133fb9778e8)|(E I989f6f3c-9d38-492f-80fe-4b71cfee574f&amp;D Ie1f57945-a74c-4f6c-948b-5133fb9778e8)&amp;D I723529e7-8893-455e-b785-595592528040">
                        <qti-value>B I989f6f3c-9d38-492f-80fe-4b71cfee574f</qti-value>
                        <qti-value>D I723529e7-8893-455e-b785-595592528040</qti-value>
                    </qti-correct-response>
                </qti-response-declaration>
            </root>

        Private _result7 As XElement =
            <root>
                <qti-response-declaration identifier="RESPONSE" cardinality="multiple" base-type="directedPair">
                    <qti-correct-response interpretation="A I51faf178-ca03-41eb-8276-385ef2a185b3&amp;B I989f6f3c-9d38-492f-80fe-4b71cfee574f&amp;C Ie1f57945-a74c-4f6c-948b-5133fb9778e8&amp;D I723529e7-8893-455e-b785-595592528040">
                        <qti-value>A I51faf178-ca03-41eb-8276-385ef2a185b3</qti-value>
                        <qti-value>B I989f6f3c-9d38-492f-80fe-4b71cfee574f</qti-value>
                        <qti-value>C Ie1f57945-a74c-4f6c-948b-5133fb9778e8</qti-value>
                        <qti-value>D I723529e7-8893-455e-b785-595592528040</qti-value>
                    </qti-correct-response>
                    <qti-mapping>
                        <qti-map-entry map-key="A I51faf178-ca03-41eb-8276-385ef2a185b3" mapped-value="1"/>
                        <qti-map-entry map-key="B I989f6f3c-9d38-492f-80fe-4b71cfee574f" mapped-value="1"/>
                        <qti-map-entry map-key="C Ie1f57945-a74c-4f6c-948b-5133fb9778e8" mapped-value="1"/>
                        <qti-map-entry map-key="D I723529e7-8893-455e-b785-595592528040" mapped-value="1"/>
                    </qti-mapping>
                </qti-response-declaration>
            </root>


    End Class

End Namespace