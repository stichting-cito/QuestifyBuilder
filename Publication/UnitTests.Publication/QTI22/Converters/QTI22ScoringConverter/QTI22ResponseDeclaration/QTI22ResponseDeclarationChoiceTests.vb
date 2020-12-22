<TestClass()>
Public Class QTI22ResponseDeclarationChoiceTests
    Inherits QTI_Base.ResponseDeclarationChoiceTestsBase

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub IC_Test()
        QTI22PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution1, GetInlineChoiceScoringParams(4), _result1, 4)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub IC_NoFinding_Test()
        QTI22PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution2, GetInlineChoiceScoringParams(4), _result2, 4)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub IC_Factset_Test()
        QTI22PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution3, GetInlineChoiceScoringParams(4), _result3, 4)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub IC_FindingFactsAndFactset_Test()
        QTI22PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution4, GetInlineChoiceScoringParams(4), _result4, 4)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub IC_Factsets_Test()
        QTI22PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution5, GetInlineChoiceScoringParams(4), _result5, 4)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub IC_InCompleteFinding_Test()
        QTI22PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution6, GetInlineChoiceScoringParams(4), _result6, 4)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub IC_Factset_InCompleteFinding_Test()
        QTI22PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution7, GetInlineChoiceScoringParams(4), _result7, 4)
    End Sub


    Private _itemBody1 As XElement =
       <wrapper>
           <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
           <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
           <itemBody class="defaultBody">
               <div class="content">
                   <div xmlns="http://www.w3.org/1999/xhtml">
                       <div id="questionwithinlinecontrol">
                           <p id="c1-id-11">Drop down 1 <inlineChoiceInteraction responseIdentifier="I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" shuffle="false" required="true">
                                   <inlineChoice identifier="A">alt.A</inlineChoice>
                                   <inlineChoice identifier="B">alt.B</inlineChoice>
                                   <inlineChoice identifier="C">alt.C</inlineChoice>
                                   <inlineChoice identifier="D">alt.D</inlineChoice>
                               </inlineChoiceInteraction>
                           </p>
                           <p id="c1-id-12">Drop down 2 <inlineChoiceInteraction responseIdentifier="Ie830b508-32a0-42f4-920f-0e06f0f19313" shuffle="false" required="true">
                                   <inlineChoice identifier="A">opt.1</inlineChoice>
                                   <inlineChoice identifier="B">opt.2</inlineChoice>
                                   <inlineChoice identifier="C">opt.3</inlineChoice>
                                   <inlineChoice identifier="D">opt.4</inlineChoice>
                                   <inlineChoice identifier="E">opt.5</inlineChoice>
                               </inlineChoiceInteraction>
                           </p>
                           <p id="c1-id-13">Drop down 3 <inlineChoiceInteraction responseIdentifier="I22f81e91-aaa4-4528-999e-61b31b8123ec" shuffle="false" required="true">
                                   <inlineChoice identifier="A">keuze 1</inlineChoice>
                                   <inlineChoice identifier="B">keuze 2</inlineChoice>
                               </inlineChoiceInteraction>
                           </p>
                           <p id="c1-id-14">Drop down 4 <inlineChoiceInteraction responseIdentifier="I5196c3f8-ddf3-4423-b777-d1d2c71d6efb" shuffle="false" required="true">
                                   <inlineChoice identifier="A">A</inlineChoice>
                                   <inlineChoice identifier="B">B</inlineChoice>
                                   <inlineChoice identifier="C">C</inlineChoice>
                                   <inlineChoice identifier="D">D</inlineChoice>
                                   <inlineChoice identifier="E">E</inlineChoice>
                                   <inlineChoice identifier="F">F</inlineChoice>
                               </inlineChoiceInteraction>
                           </p>
                       </div>
                       <div id="answer">

                       </div>
                   </div>
               </div>
           </itemBody>
       </wrapper>



    Private _result1 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="single" baseType="identifier">
                <correctResponse interpretation="C">
                    <value>C</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE2" cardinality="single" baseType="identifier">
                <correctResponse interpretation="E">
                    <value>E</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE3" cardinality="single" baseType="identifier">
                <correctResponse interpretation="A">
                    <value>A</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE4" cardinality="single" baseType="identifier">
                <correctResponse interpretation="F">
                    <value>F</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result2 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="single" baseType="string"/>
            <responseDeclaration identifier="RESPONSE2" cardinality="single" baseType="string"/>
            <responseDeclaration identifier="RESPONSE3" cardinality="single" baseType="string"/>
            <responseDeclaration identifier="RESPONSE4" cardinality="single" baseType="string"/>
        </responseDeclarations>

    Private _result3 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="single" baseType="identifier">
                <correctResponse interpretation="(A&amp;A&amp;A&amp;F)|(B&amp;B&amp;B&amp;F)|(D&amp;E&amp;B&amp;F)">
                    <value>A</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE2" cardinality="single" baseType="identifier">
                <correctResponse>
                    <value>A</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE3" cardinality="single" baseType="identifier">
                <correctResponse>
                    <value>A</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE4" cardinality="single" baseType="identifier">
                <correctResponse>
                    <value>F</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result4 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="single" baseType="identifier">
                <correctResponse interpretation="A">
                    <value>A</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE2" cardinality="single" baseType="identifier">
                <correctResponse interpretation="(A&amp;A)|(B&amp;B)|(E&amp;A)">
                    <value>A</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE3" cardinality="single" baseType="identifier">
                <correctResponse>
                    <value>A</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE4" cardinality="single" baseType="identifier">
                <correctResponse interpretation="F">
                    <value>F</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result5 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="single" baseType="identifier">
                <correctResponse interpretation="(C&amp;A)">
                    <value>C</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE2" cardinality="single" baseType="identifier">
                <correctResponse interpretation="(E&amp;F)|(E&amp;D)">
                    <value>E</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE3" cardinality="single" baseType="identifier">
                <correctResponse>
                    <value>A</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE4" cardinality="single" baseType="identifier">
                <correctResponse>
                    <value>F</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result6 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="single" baseType="string"/>
            <responseDeclaration identifier="RESPONSE2" cardinality="single" baseType="identifier">
                <correctResponse interpretation="B">
                    <value>B</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE3" cardinality="single" baseType="string"/>
            <responseDeclaration identifier="RESPONSE4" cardinality="single" baseType="identifier">
                <correctResponse interpretation="F">
                    <value>F</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result7 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="single" baseType="string"/>
            <responseDeclaration identifier="RESPONSE2" cardinality="single" baseType="identifier">
                <correctResponse interpretation="(B&amp;F)">
                    <value>B</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE3" cardinality="single" baseType="string"/>
            <responseDeclaration identifier="RESPONSE4" cardinality="single" baseType="identifier">
                <correctResponse>
                    <value>F</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>


End Class
