﻿Imports Cito.Tester.ContentModel
Imports Questify.Builder.UnitTests.Publication.QTI30

Namespace QTI30

    <TestClass()>
    Public Class ResponseDeclarationCasTests

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub CasEvaluateTest()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody2, _solution2, GetCasEvaluateScoringParameters("Ica1d45e1-87cc-4a1c-932b-6f5bdc23bf95"), _result2, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub CasEvaluateTest2()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody3, _solution3, GetCasEvaluateScoringParameter(), _result3, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub CasEvaluateWithDegreeTest()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody3, _solution4, GetCasEvaluateScoringParameter(), _result4, 1)
        End Sub


        Private Function GetCasEvaluateScoringParameter() As HashSet(Of ScoringParameter)
            Dim scoringParameters = New HashSet(Of ScoringParameter)
            Dim scoringParam = New MathScoringParameter With {.Name = "mathMLScoring", .InlineId = "I5db47936-a93f-4184-8c0d-7aecba570a1e", .ControllerId = "gapController", .Value = New ParameterSetCollection(), .BluePrint = New ParameterCollection()}
            scoringParam.Value.Add(New ParameterCollection With {.Id = "1"})
            scoringParam.Value(0).InnerParameters.Add(New BooleanParameter With {.Name = "fictiveMathML", .Value = True})
            scoringParam.BluePrint.InnerParameters.Add(New BooleanParameter With {.Name = "fictiveMathML"})

            scoringParameters.Add(scoringParam)

            Return scoringParameters
        End Function

        Private Function GetCasEvaluateScoringParameters(ByVal inlineId As String) As HashSet(Of ScoringParameter)
            Dim scoringParameters = New HashSet(Of ScoringParameter)()

            Dim firstMathCasEvaluateScoringParameter As MathCasEvaluateScoringParameter = New MathCasEvaluateScoringParameter() With {.Name = "mathCasEvaluateScoringFirst", .InlineId = inlineId}.AddSubParameters("First")
            scoringParameters.Add(firstMathCasEvaluateScoringParameter)

            Dim lastMathCasEvaluateScoringParameter As MathCasEvaluateScoringParameter = New MathCasEvaluateScoringParameter() With {.Name = "mathCasEvaluateScoringLast", .InlineId = inlineId}.AddSubParameters("Second")
            scoringParameters.Add(lastMathCasEvaluateScoringParameter)

            Return scoringParameters
        End Function




        ReadOnly _itemBody2 As XElement =
            <wrapper>
                <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
                <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
                <qti-item-body class="defaultBody">
                    <div class="content">
                        <div>
                            <div id="itemBody">
                                <p id="c1-id-11">Hier is de body van de vraag.</p>
                            </div>
                            <div id="itemquestion">
                                <p id="c1-id-11">
                                    <qti-text-entry-interaction id="Ica1d45e1-87cc-4a1c-932b-6f5bdc23bf95" response-identifier="Ica1d45e1-87cc-4a1c-932b-6f5bdc23bf95"/> </p>
                            </div>
                            <div id="answer">
                            </div>
                        </div>
                    </div>
                </qti-item-body>
            </wrapper>

        ReadOnly _itemBody3 As XElement =
        <wrapper>
            <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
            <qti-item-body class="defaultBody">
                <div class="content">
                    <div>
                        <div id="itemBody">
                            <p id="c1-id-11">Hier is de body van de vraag.</p>
                        </div>
                        <div id="itemquestion">
                            <p id="c1-id-11">
                                <qti-text-entry-interaction id="I5db47936-a93f-4184-8c0d-7aecba570a1e" response-identifier="I5db47936-a93f-4184-8c0d-7aecba570a1e"/> </p>
                        </div>
                        <div id="answer">
                        </div>
                    </div>
                </div>
            </qti-item-body>
        </wrapper>




        ReadOnly _solution2 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="gapController" scoringMethod="Dichotomous">
                        <keyFact id="First-Ica1d45e1-87cc-4a1c-932b-6f5bdc23bf95" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Ica1d45e1-87cc-4a1c-932b-6f5bdc23bf95" occur="1">
                                <stringValue>
                                    <typedValue>{x:-1;y:1}</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="Second-Ica1d45e1-87cc-4a1c-932b-6f5bdc23bf95" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Ica1d45e1-87cc-4a1c-932b-6f5bdc23bf95" occur="1">
                                <stringValue>
                                    <typedValue>{x:3;y:8}</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                    </keyFinding>
                </keyFindings>
                <aspectReferences/>
                <ItemScoreTranslationTable>
                    <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                    <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                </ItemScoreTranslationTable>
            </solution>

        ReadOnly _solution3 As XElement =
              <solution>
                  <keyFindings>
                      <keyFinding id="gapController" scoringMethod="Dichotomous">
                          <keyFact id="1-I5db47936-a93f-4184-8c0d-7aecba570a1e" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                              <keyValue domain="I5db47936-a93f-4184-8c0d-7aecba570a1e" occur="1">
                                  <stringComparisonValue>
                                      <typedComparisonValue>{x:-3.2,y:8}{x:-4.8,y:0.44}</typedComparisonValue>
                                      <comparisonType>Evaluate</comparisonType>
                                  </stringComparisonValue>
                              </keyValue>
                          </keyFact>
                      </keyFinding>
                  </keyFindings>
                  <conceptFindings>
                      <conceptFinding id="gapController" scoringMethod="None">
                          <conceptFact id="1[*]-I5db47936-a93f-4184-8c0d-7aecba570a1e" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                              <conceptValue domain="I5db47936-a93f-4184-8c0d-7aecba570a1e" occur="1">
                                  <catchAllValue/>
                              </conceptValue>
                          </conceptFact>
                      </conceptFinding>
                  </conceptFindings>
                  <aspectReferences/>
                  <ItemScoreTranslationTable>
                      <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                      <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                  </ItemScoreTranslationTable>
              </solution>

        ReadOnly _solution4 As XElement =
              <solution>
                  <keyFindings>
                      <keyFinding id="gapController" scoringMethod="Dichotomous">
                          <keyFact id="1-I5db47936-a93f-4184-8c0d-7aecba570a1e" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                              <keyValue domain="I5db47936-a93f-4184-8c0d-7aecba570a1e" occur="1">
                                  <stringComparisonValue>
                                      <typedComparisonValue>{x:-3.2,y:8}{x:-4.8,y:0.44}[deg:1]</typedComparisonValue>
                                      <comparisonType>Evaluate</comparisonType>
                                  </stringComparisonValue>
                              </keyValue>
                          </keyFact>
                      </keyFinding>
                  </keyFindings>
              </solution>



        ReadOnly _result2 As XElement =
    <root>
        <qti-response-declaration identifier="RESPONSE" cardinality="single" base-type="string">
            <qti-correct-response interpretation="{x:-1;y:1}&amp;{x:3;y:8}">
                <qti-value>{x:-1;y:1}</qti-value>
                <qti-value>{x:3;y:8}</qti-value>
            </qti-correct-response>
        </qti-response-declaration>
    </root>

        ReadOnly _result3 As XElement =
    <root>
        <qti-response-declaration identifier="RESPONSE" cardinality="single" base-type="string">
            <qti-correct-response interpretation="{x:-3.2;y:8}&amp;{x:-4.8;y:0.44}">
                <qti-value/>
            </qti-correct-response>
        </qti-response-declaration>
    </root>

        ReadOnly _result4 As XElement =
    <root>
        <qti-response-declaration identifier="RESPONSE" cardinality="single" base-type="string">
            <qti-correct-response interpretation="{x:-3.2;y:8}&amp;{x:-4.8;y:0.44}[deg:1]">
                <qti-value/>
            </qti-correct-response>
        </qti-response-declaration>
    </root>


    End Class

End Namespace
