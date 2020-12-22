Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI22

<TestClass()>
Public Class QTI22ResponseProcessingMatrixTests
    Inherits QTI22ResponseProcessingTests_Base

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub GetResponseProcessingForCombinationOfFactSetsAndFactsOnFinding_Polytomous_Test()
        GetResponseProcessingTest(_finding1, _responseProcessing1, 0, False)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub GetResponseProcessingForCombinationOfFactSetsAndFactsOnFinding_Dichotomous_Test()
        GetResponseProcessingTest(_finding5, _responseProcessing5, 0, False)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub GetResponseProcessingForMultipleFactSets_Polytomous_Test()
        GetResponseProcessingTest(_finding2, _responseProcessing2, 0, False)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub GetResponseProcessingForMultipleFactSets_Dichotomous_Test()
        GetResponseProcessingTest(_finding6, _responseProcessing6, 0, False)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub GetResponseProcessingForFactSetTest()
        GetResponseProcessingTest(_finding3, _responseProcessing3, 0, False)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub GetResponseProcessingForFactsOnFinding_Polytomous_Test()
        GetResponseProcessingTest(_finding4, _responseProcessing4, 0, False)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub GetResponseProcessingForFactsOnFinding_Dichotomous_Test()
        GetResponseProcessingTest(_finding7, _responseProcessing7, 0, False)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub GetResponseProcessingForFactsOnFinding_Polytomous_FindingIndexEqualsOne_Test()
        GetResponseProcessingTest(_finding4, _responseProcessing8, 1, False)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub GetResponseProcessingForFactsOnFinding_Dichotomous_FindingIndexEqualsOne_Test()
        GetResponseProcessingTest(_finding7, _responseProcessing9, 1, False)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub GetResponseProcessingForFactsOnFinding_Polytomous_ShouldBeTranslated_Test()
        GetResponseProcessingTest(_finding4, _responseProcessing10, 0, True)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub GetResponseProcessingForFactsOnFinding_Dichotomous_ShouldBeTranslated_Test()
        GetResponseProcessingTest(_finding7, _responseProcessing11, 0, True)
    End Sub

    Public Sub GetResponseProcessingTest(findingElement As XElement, responseProcessingElement As XElement, findingIndex As Integer, shouldBeTranslated As Boolean)
        Dim scoringPrms As HashSet(Of ScoringParameter) = GetMatrixScoringParams()
        RunResponseProcessingTest(_itemBody1, findingElement, responseProcessingElement, scoringPrms, New QTI22CombinedScoringConverter(scoringPrms), findingIndex, shouldBeTranslated)
    End Sub

    Private Function GetMatrixScoringParams() As HashSet(Of ScoringParameter)
        Dim scoreParams As New HashSet(Of ScoringParameter)
        Dim matrixColumnsDefinition As MultiChoiceScoringParameter = New MultiChoiceScoringParameter() With {.MinChoices = 1, .MaxChoices = 1, .MultiChoice = MultiChoiceType.Check}.AddSubParameters("A", "B", "C")
        Dim scoreParam As ScoringParameter = New MatrixScoringParameter() With {.ControllerId = "matrix", .FindingOverride = "matrix"}.AddSubParameters("1", "2", "3", "4")
        DirectCast(scoreParam, MatrixScoringParameter).MatrixColumnsDefinition = matrixColumnsDefinition
        scoreParams.Add(scoreParam)
        Return scoreParams
    End Function

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

    Private _finding1 As XElement =
        <keyFinding id="matrix" scoringMethod="Polytomous">
            <keyFact id="3-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="matrix3" occur="1">
                    <stringValue>
                        <typedValue>C</typedValue>
                    </stringValue>
                </keyValue>
            </keyFact>
            <keyFact id="4-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="matrix4" occur="1">
                    <stringValue>
                        <typedValue>A</typedValue>
                    </stringValue>
                </keyValue>
            </keyFact>
            <keyFactSet>
                <keyFact id="2-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="matrix2" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="1-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="matrix1" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="1-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="matrix1" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="2-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="matrix2" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
        </keyFinding>

    Private _finding2 As XElement =
        <keyFinding id="matrix" scoringMethod="Polytomous">
            <keyFactSet>
                <keyFact id="2-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="matrix2" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="1-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="matrix1" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="1-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="matrix1" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="2-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="matrix2" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="3-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="matrix3" occur="1">
                        <stringValue>
                            <typedValue>C</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="4-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="matrix4" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="3-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="matrix3" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="4-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="matrix4" occur="1">
                        <stringValue>
                            <typedValue>C</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
        </keyFinding>

    Private _finding3 As XElement =
        <keyFinding id="matrix" scoringMethod="Polytomous">
            <keyFactSet>
                <keyFact id="2-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="matrix2" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="1-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="matrix1" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="3-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="matrix3" occur="1">
                        <stringValue>
                            <typedValue>C</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="4-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="matrix4" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="1-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="matrix1" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="2-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="matrix2" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="3-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="matrix3" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="4-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="matrix4" occur="1">
                        <stringValue>
                            <typedValue>C</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
        </keyFinding>

    Private _finding4 As XElement =
        <keyFinding id="matrix" scoringMethod="Polytomous">
            <keyFact id="1-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="matrix1" occur="1">
                    <stringValue>
                        <typedValue>A</typedValue>
                    </stringValue>
                </keyValue>
            </keyFact>
            <keyFact id="2-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="matrix2" occur="1">
                    <stringValue>
                        <typedValue>B</typedValue>
                    </stringValue>
                </keyValue>
            </keyFact>
            <keyFact id="3-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="matrix3" occur="1">
                    <stringValue>
                        <typedValue>C</typedValue>
                    </stringValue>
                </keyValue>
            </keyFact>
            <keyFact id="4-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="matrix4" occur="1">
                    <stringValue>
                        <typedValue>A</typedValue>
                    </stringValue>
                </keyValue>
            </keyFact>
        </keyFinding>

    Private _finding5 As XElement =
        <keyFinding id="matrix" scoringMethod="Dichotomous">
            <keyFact id="3-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="matrix3" occur="1">
                    <stringValue>
                        <typedValue>C</typedValue>
                    </stringValue>
                </keyValue>
            </keyFact>
            <keyFact id="4-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="matrix4" occur="1">
                    <stringValue>
                        <typedValue>A</typedValue>
                    </stringValue>
                </keyValue>
            </keyFact>
            <keyFactSet>
                <keyFact id="2-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="matrix2" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="1-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="matrix1" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="1-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="matrix1" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="2-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="matrix2" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
        </keyFinding>

    Private _finding6 As XElement =
        <keyFinding id="matrix" scoringMethod="Dichotomous">
            <keyFactSet>
                <keyFact id="2-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="matrix2" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="1-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="matrix1" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="1-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="matrix1" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="2-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="matrix2" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="3-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="matrix3" occur="1">
                        <stringValue>
                            <typedValue>C</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="4-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="matrix4" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="3-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="matrix3" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="4-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="matrix4" occur="1">
                        <stringValue>
                            <typedValue>C</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
        </keyFinding>

    Private _finding7 As XElement =
        <keyFinding id="matrix" scoringMethod="Dichotomous">
            <keyFact id="1-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="matrix1" occur="1">
                    <stringValue>
                        <typedValue>A</typedValue>
                    </stringValue>
                </keyValue>
            </keyFact>
            <keyFact id="2-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="matrix2" occur="1">
                    <stringValue>
                        <typedValue>B</typedValue>
                    </stringValue>
                </keyValue>
            </keyFact>
            <keyFact id="3-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="matrix3" occur="1">
                    <stringValue>
                        <typedValue>C</typedValue>
                    </stringValue>
                </keyValue>
            </keyFact>
            <keyFact id="4-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="matrix4" occur="1">
                    <stringValue>
                        <typedValue>A</typedValue>
                    </stringValue>
                </keyValue>
            </keyFact>
        </keyFinding>

    Private _responseProcessing1 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <or>
                        <and>
                            <member>
                                <baseValue baseType="identifier">y_A x_1</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="identifier">y_B x_2</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </and>
                        <and>
                            <member>
                                <baseValue baseType="identifier">y_A x_2</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="identifier">y_B x_1</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </and>
                    </or>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <member>
                        <baseValue baseType="identifier">y_C x_3</baseValue>
                        <variable identifier="RESPONSE"/>
                    </member>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <member>
                        <baseValue baseType="identifier">y_D x_1</baseValue>
                        <variable identifier="RESPONSE"/>
                    </member>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
        </responseProcessing>

    Private _responseProcessing2 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <or>
                        <and>
                            <member>
                                <baseValue baseType="identifier">y_A x_1</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="identifier">y_B x_2</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </and>
                        <and>
                            <member>
                                <baseValue baseType="identifier">y_A x_2</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="identifier">y_B x_1</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </and>
                    </or>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <or>
                        <and>
                            <member>
                                <baseValue baseType="identifier">y_C x_3</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="identifier">y_D x_1</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </and>
                        <and>
                            <member>
                                <baseValue baseType="identifier">y_C x_2</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="identifier">y_D x_3</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </and>
                    </or>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
        </responseProcessing>

    Private _responseProcessing3 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <or>
                        <and>
                            <member>
                                <baseValue baseType="identifier">y_A x_1</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="identifier">y_B x_2</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="identifier">y_C x_3</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="identifier">y_D x_1</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </and>
                        <and>
                            <member>
                                <baseValue baseType="identifier">y_A x_2</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="identifier">y_B x_1</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="identifier">y_C x_2</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="identifier">y_D x_3</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </and>
                    </or>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
        </responseProcessing>

    Private _responseProcessing4 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <member>
                        <baseValue baseType="identifier">y_A x_1</baseValue>
                        <variable identifier="RESPONSE"/>
                    </member>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <member>
                        <baseValue baseType="identifier">y_B x_2</baseValue>
                        <variable identifier="RESPONSE"/>
                    </member>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <member>
                        <baseValue baseType="identifier">y_C x_3</baseValue>
                        <variable identifier="RESPONSE"/>
                    </member>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <member>
                        <baseValue baseType="identifier">y_D x_1</baseValue>
                        <variable identifier="RESPONSE"/>
                    </member>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
        </responseProcessing>

    Private _responseProcessing5 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <and>
                        <or>
                            <and>
                                <member>
                                    <baseValue baseType="identifier">y_A x_1</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <member>
                                    <baseValue baseType="identifier">y_B x_2</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </and>
                            <and>
                                <member>
                                    <baseValue baseType="identifier">y_A x_2</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <member>
                                    <baseValue baseType="identifier">y_B x_1</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </and>
                        </or>
                        <member>
                            <baseValue baseType="identifier">y_C x_3</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                        <member>
                            <baseValue baseType="identifier">y_D x_1</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                    </and>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <gte>
                        <variable identifier="SCORE"/>
                        <baseValue baseType="integer">1</baseValue>
                    </gte>
                    <setOutcomeValue identifier="SCORE">
                        <baseValue baseType="integer">1</baseValue>
                    </setOutcomeValue>
                </responseIf>
                <responseElse>
                    <setOutcomeValue identifier="SCORE">
                        <baseValue baseType="integer">0</baseValue>
                    </setOutcomeValue>
                </responseElse>
            </responseCondition>
        </responseProcessing>

    Private _responseProcessing6 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <and>
                        <or>
                            <and>
                                <member>
                                    <baseValue baseType="identifier">y_A x_1</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <member>
                                    <baseValue baseType="identifier">y_B x_2</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </and>
                            <and>
                                <member>
                                    <baseValue baseType="identifier">y_A x_2</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <member>
                                    <baseValue baseType="identifier">y_B x_1</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </and>
                        </or>
                        <or>
                            <and>
                                <member>
                                    <baseValue baseType="identifier">y_C x_3</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <member>
                                    <baseValue baseType="identifier">y_D x_1</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </and>
                            <and>
                                <member>
                                    <baseValue baseType="identifier">y_C x_2</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <member>
                                    <baseValue baseType="identifier">y_D x_3</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </and>
                        </or>
                    </and>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <gte>
                        <variable identifier="SCORE"/>
                        <baseValue baseType="integer">1</baseValue>
                    </gte>
                    <setOutcomeValue identifier="SCORE">
                        <baseValue baseType="integer">1</baseValue>
                    </setOutcomeValue>
                </responseIf>
                <responseElse>
                    <setOutcomeValue identifier="SCORE">
                        <baseValue baseType="integer">0</baseValue>
                    </setOutcomeValue>
                </responseElse>
            </responseCondition>
        </responseProcessing>

    Private _responseProcessing7 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <and>
                        <member>
                            <baseValue baseType="identifier">y_A x_1</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                        <member>
                            <baseValue baseType="identifier">y_B x_2</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                        <member>
                            <baseValue baseType="identifier">y_C x_3</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                        <member>
                            <baseValue baseType="identifier">y_D x_1</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                    </and>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <gte>
                        <variable identifier="SCORE"/>
                        <baseValue baseType="integer">1</baseValue>
                    </gte>
                    <setOutcomeValue identifier="SCORE">
                        <baseValue baseType="integer">1</baseValue>
                    </setOutcomeValue>
                </responseIf>
                <responseElse>
                    <setOutcomeValue identifier="SCORE">
                        <baseValue baseType="integer">0</baseValue>
                    </setOutcomeValue>
                </responseElse>
            </responseCondition>
        </responseProcessing>

    Private _responseProcessing8 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <member>
                        <baseValue baseType="identifier">y_A x_1</baseValue>
                        <variable identifier="RESPONSE"/>
                    </member>
                    <setOutcomeValue identifier="SCOREFINDING">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCOREFINDING"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <member>
                        <baseValue baseType="identifier">y_B x_2</baseValue>
                        <variable identifier="RESPONSE"/>
                    </member>
                    <setOutcomeValue identifier="SCOREFINDING">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCOREFINDING"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <member>
                        <baseValue baseType="identifier">y_C x_3</baseValue>
                        <variable identifier="RESPONSE"/>
                    </member>
                    <setOutcomeValue identifier="SCOREFINDING">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCOREFINDING"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <member>
                        <baseValue baseType="identifier">y_D x_1</baseValue>
                        <variable identifier="RESPONSE"/>
                    </member>
                    <setOutcomeValue identifier="SCOREFINDING">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCOREFINDING"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
        </responseProcessing>

    Private _responseProcessing9 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <and>
                        <member>
                            <baseValue baseType="identifier">y_A x_1</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                        <member>
                            <baseValue baseType="identifier">y_B x_2</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                        <member>
                            <baseValue baseType="identifier">y_C x_3</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                        <member>
                            <baseValue baseType="identifier">y_D x_1</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                    </and>
                    <setOutcomeValue identifier="SCOREFINDING">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCOREFINDING"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <gte>
                        <variable identifier="SCOREFINDING"/>
                        <baseValue baseType="integer">1</baseValue>
                    </gte>
                    <setOutcomeValue identifier="SCOREFINDING">
                        <baseValue baseType="integer">1</baseValue>
                    </setOutcomeValue>
                </responseIf>
                <responseElse>
                    <setOutcomeValue identifier="SCOREFINDING">
                        <baseValue baseType="integer">0</baseValue>
                    </setOutcomeValue>
                </responseElse>
            </responseCondition>
        </responseProcessing>

    Private _responseProcessing10 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <member>
                        <baseValue baseType="identifier">y_A x_1</baseValue>
                        <variable identifier="RESPONSE"/>
                    </member>
                    <setOutcomeValue identifier="RAW_SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="RAW_SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <member>
                        <baseValue baseType="identifier">y_B x_2</baseValue>
                        <variable identifier="RESPONSE"/>
                    </member>
                    <setOutcomeValue identifier="RAW_SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="RAW_SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <member>
                        <baseValue baseType="identifier">y_C x_3</baseValue>
                        <variable identifier="RESPONSE"/>
                    </member>
                    <setOutcomeValue identifier="RAW_SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="RAW_SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <member>
                        <baseValue baseType="identifier">y_D x_1</baseValue>
                        <variable identifier="RESPONSE"/>
                    </member>
                    <setOutcomeValue identifier="RAW_SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="RAW_SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
        </responseProcessing>

    Private _responseProcessing11 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <and>
                        <member>
                            <baseValue baseType="identifier">y_A x_1</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                        <member>
                            <baseValue baseType="identifier">y_B x_2</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                        <member>
                            <baseValue baseType="identifier">y_C x_3</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                        <member>
                            <baseValue baseType="identifier">y_D x_1</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                    </and>
                    <setOutcomeValue identifier="RAW_SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="RAW_SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <gte>
                        <variable identifier="RAW_SCORE"/>
                        <baseValue baseType="integer">1</baseValue>
                    </gte>
                    <setOutcomeValue identifier="RAW_SCORE">
                        <baseValue baseType="integer">1</baseValue>
                    </setOutcomeValue>
                </responseIf>
                <responseElse>
                    <setOutcomeValue identifier="RAW_SCORE">
                        <baseValue baseType="integer">0</baseValue>
                    </setOutcomeValue>
                </responseElse>
            </responseCondition>
        </responseProcessing>

End Class
