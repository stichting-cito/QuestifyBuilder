<TestClass()>
Public Class QTI22ResponseDeclarationGapMatchTests
    Inherits QTI_Base.ResponseDeclarationGapMatchTestsBase

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub GetResponseDeclarationForCombinationOfFactSetsAndFactsOnFindingTest()
        QTI22PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution1, GetGapMatchScoringParams(), _result1, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub GetResponseDeclarationForMultipleFactSetsTest()
        QTI22PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution2, GetGapMatchScoringParams(), _result2, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub GetResponseDeclarationForFactSetsTest()
        QTI22PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution3, GetGapMatchScoringParams(), _result3, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub GetResponseDeclarationForFactsOnFindingTest()
        QTI22PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution4, GetGapMatchScoringParams(), _result4, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub GetEmptyResponseDeclarationTest()
        QTI22PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution5, GetGapMatchScoringParams(), _result5, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub GetResponseDeclarationWithNoValueTest()
        QTI22PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution6, GetGapMatchScoringParams(), _result6, 1)
    End Sub


    Private _itemBody1 As XElement =
       <wrapper>
           <itemBody class="defaultBody">
               <div class="content">
                   <div>
                       <div>
                           <gapMatchInteraction responseIdentifier="gapMatchController" shuffle="false">
                               <gapText identifier="A" matchMax="1">A</gapText>
                               <gapText identifier="B" matchMax="1">B</gapText>
                               <gapText identifier="C" matchMax="1">C</gapText>
                               <gapText identifier="D" matchMax="1">D</gapText>
                               <gapText identifier="E" matchMax="1">E</gapText>
                               <gapText identifier="F" matchMax="1">F</gapText>
                               <p id="c1-id-11">Tekst met <span>
                                       <gap identifier="I51faf178-ca03-41eb-8276-385ef2a185b3" required="true"/>
                                   </span> een aantal <span>
                                       <gap identifier="I989f6f3c-9d38-492f-80fe-4b71cfee574f" required="true"/>
                                   </span> gaten waarin teksten <span>
                                       <gap identifier="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" required="true"/>
                                   </span> kunnen worden gesleept <span>
                                       <gap identifier="I723529e7-8893-455e-b785-595592528040" required="true"/>
                                   </span> door de kandidaat.</p>
                           </gapMatchInteraction>
                       </div>
                   </div>
               </div>
           </itemBody>
       </wrapper>



    Private _result1 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="directedPair">
                <correctResponse interpretation="A I51faf178-ca03-41eb-8276-385ef2a185b3&amp;(B I989f6f3c-9d38-492f-80fe-4b71cfee574f&amp;C Ie1f57945-a74c-4f6c-948b-5133fb9778e8)|(B I989f6f3c-9d38-492f-80fe-4b71cfee574f&amp;A Ie1f57945-a74c-4f6c-948b-5133fb9778e8)|(E I989f6f3c-9d38-492f-80fe-4b71cfee574f&amp;D Ie1f57945-a74c-4f6c-948b-5133fb9778e8)&amp;D I723529e7-8893-455e-b785-595592528040">
                    <value>A I51faf178-ca03-41eb-8276-385ef2a185b3</value>
                    <value>B I989f6f3c-9d38-492f-80fe-4b71cfee574f</value>
                    <value>C Ie1f57945-a74c-4f6c-948b-5133fb9778e8</value>
                    <value>D I723529e7-8893-455e-b785-595592528040</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result2 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="directedPair">
                <correctResponse interpretation="(A I51faf178-ca03-41eb-8276-385ef2a185b3&amp;C Ie1f57945-a74c-4f6c-948b-5133fb9778e8)|(C I51faf178-ca03-41eb-8276-385ef2a185b3&amp;A Ie1f57945-a74c-4f6c-948b-5133fb9778e8)|(F I51faf178-ca03-41eb-8276-385ef2a185b3&amp;D Ie1f57945-a74c-4f6c-948b-5133fb9778e8)&amp;(B I989f6f3c-9d38-492f-80fe-4b71cfee574f&amp;D I723529e7-8893-455e-b785-595592528040)|(E I989f6f3c-9d38-492f-80fe-4b71cfee574f&amp;F I723529e7-8893-455e-b785-595592528040)">
                    <value>A I51faf178-ca03-41eb-8276-385ef2a185b3</value>
                    <value>B I989f6f3c-9d38-492f-80fe-4b71cfee574f</value>
                    <value>C Ie1f57945-a74c-4f6c-948b-5133fb9778e8</value>
                    <value>D I723529e7-8893-455e-b785-595592528040</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result3 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="directedPair">
                <correctResponse interpretation="(A I51faf178-ca03-41eb-8276-385ef2a185b3&amp;B I989f6f3c-9d38-492f-80fe-4b71cfee574f&amp;C Ie1f57945-a74c-4f6c-948b-5133fb9778e8&amp;D I723529e7-8893-455e-b785-595592528040)|(D I51faf178-ca03-41eb-8276-385ef2a185b3&amp;C I989f6f3c-9d38-492f-80fe-4b71cfee574f&amp;B Ie1f57945-a74c-4f6c-948b-5133fb9778e8&amp;A I723529e7-8893-455e-b785-595592528040)|(F I51faf178-ca03-41eb-8276-385ef2a185b3&amp;E I989f6f3c-9d38-492f-80fe-4b71cfee574f&amp;B Ie1f57945-a74c-4f6c-948b-5133fb9778e8&amp;A I723529e7-8893-455e-b785-595592528040)">
                    <value>A I51faf178-ca03-41eb-8276-385ef2a185b3</value>
                    <value>B I989f6f3c-9d38-492f-80fe-4b71cfee574f</value>
                    <value>C Ie1f57945-a74c-4f6c-948b-5133fb9778e8</value>
                    <value>D I723529e7-8893-455e-b785-595592528040</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result4 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="directedPair">
                <correctResponse interpretation="A I51faf178-ca03-41eb-8276-385ef2a185b3&amp;B I989f6f3c-9d38-492f-80fe-4b71cfee574f&amp;C Ie1f57945-a74c-4f6c-948b-5133fb9778e8&amp;D I723529e7-8893-455e-b785-595592528040">
                    <value>A I51faf178-ca03-41eb-8276-385ef2a185b3</value>
                    <value>B I989f6f3c-9d38-492f-80fe-4b71cfee574f</value>
                    <value>C Ie1f57945-a74c-4f6c-948b-5133fb9778e8</value>
                    <value>D I723529e7-8893-455e-b785-595592528040</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result5 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="string"/>
        </responseDeclarations>

    Private _result6 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="directedPair">
                <correctResponse interpretation="Ø I51faf178-ca03-41eb-8276-385ef2a185b3&amp;(B I989f6f3c-9d38-492f-80fe-4b71cfee574f&amp;Ø Ie1f57945-a74c-4f6c-948b-5133fb9778e8)|(Ø I989f6f3c-9d38-492f-80fe-4b71cfee574f&amp;A Ie1f57945-a74c-4f6c-948b-5133fb9778e8)|(E I989f6f3c-9d38-492f-80fe-4b71cfee574f&amp;D Ie1f57945-a74c-4f6c-948b-5133fb9778e8)&amp;D I723529e7-8893-455e-b785-595592528040">
                    <value>B I989f6f3c-9d38-492f-80fe-4b71cfee574f</value>
                    <value>D I723529e7-8893-455e-b785-595592528040</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>


End Class
