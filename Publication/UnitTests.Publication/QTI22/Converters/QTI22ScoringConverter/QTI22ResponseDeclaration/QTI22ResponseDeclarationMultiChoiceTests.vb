<TestClass()>
Public Class QTI22ResponseDeclarationMultiChoiceTests
    Inherits QTI_Base.ResponseDeclarationMultiChoiceTestsBase

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub MC_Test()
        QTI22PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution1, GetMcScoringParams(), _result1, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub MC_NoFinding_Test()
        QTI22PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution2, GetMcScoringParams(), _result2, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub MR_Test()
        QTI22PublicationTestHelper.RunResponseDeclarationTest(_itemBody2, _solution3, GetMrScoringParams(), _result3, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub MR_Factset_Test()
        QTI22PublicationTestHelper.RunResponseDeclarationTest(_itemBody2, _solution4, GetMrScoringParams(), _result4, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub MR_FindingFactsAndFactset_Test()
        QTI22PublicationTestHelper.RunResponseDeclarationTest(_itemBody2, _solution5, GetMrScoringParams(), _result5, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub MR_Factsets_Test()
        QTI22PublicationTestHelper.RunResponseDeclarationTest(_itemBody2, _solution6, GetMrScoringParams(), _result6, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub MultipleResponseKeyFactsWithoutTrueValuesTest()
        QTI22PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution7, GetMrScoringParams(), _result7, 1)
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
                           <choiceInteraction id="choiceInteraction1" class="" maxChoices="1" shuffle="false" responseIdentifier="mc">
                               <simpleChoice identifier="A">
                                   <p id="c1-id-11">A</p>
                               </simpleChoice>
                               <simpleChoice identifier="B">
                                   <p id="c1-id-11">B</p>
                               </simpleChoice>
                               <simpleChoice identifier="C">
                                   <p id="c1-id-11">C</p>
                               </simpleChoice>
                               <simpleChoice identifier="D">
                                   <p id="c1-id-11">D</p>
                               </simpleChoice>
                           </choiceInteraction>
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
                           <choiceInteraction id="choiceInteraction1" class="" maxChoices="0" shuffle="false" responseIdentifier="mc">
                               <simpleChoice identifier="A">
                                   <p id="c1-id-11">A</p>
                               </simpleChoice>
                               <simpleChoice identifier="B">
                                   <p id="c1-id-11">B</p>
                               </simpleChoice>
                               <simpleChoice identifier="C">
                                   <p id="c1-id-11">C</p>
                               </simpleChoice>
                               <simpleChoice identifier="D">
                                   <p id="c1-id-11">D</p>
                               </simpleChoice>
                               <simpleChoice identifier="E">
                                   <p id="c1-id-11">E</p>
                               </simpleChoice>
                           </choiceInteraction>
                       </div>
                   </div>
               </div>
           </itemBody>
       </wrapper>



    Private _result1 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="single" baseType="identifier">
                <correctResponse interpretation="A">
                    <value>A</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result2 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="single" baseType="string"/>
        </responseDeclarations>

    Private _result3 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="identifier">
                <correctResponse interpretation="A&amp;C&amp;E">
                    <value>A</value>
                    <value>C</value>
                    <value>E</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result4 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="identifier">
                <correctResponse interpretation="(B&amp;C&amp;E)|(A&amp;B&amp;E)">
                    <value>B</value>
                    <value>C</value>
                    <value>E</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result5 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="identifier">
                <correctResponse interpretation="A&amp;(D&amp;E)|(C&amp;D)">
                    <value>A</value>
                    <value>D</value>
                    <value>E</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result6 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="identifier">
                <correctResponse interpretation="A|B&amp;C&amp;E|D">
                    <value>A</value>
                    <value>C</value>
                    <value>E</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result7 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="single" baseType="string"/>
        </responseDeclarations>


End Class
