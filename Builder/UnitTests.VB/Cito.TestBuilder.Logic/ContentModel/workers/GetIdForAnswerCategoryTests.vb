
Imports Cito.Tester.ContentModel
Imports System.Xml.Linq
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports System.Linq
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.UnitTests.Framework

<TestClass()>
Public Class GetIdForAnswerCategoryTests

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub GetIdFromEmptySolution()
        Dim solution = solutionMC.To(Of Solution)()
        Dim sp = New ChoiceScoringParameter() With {.FindingOverride = "Integratie", .ControllerId = "Controller", .MaxChoices = 1}.AddSubParameters("A", "B", "C")
        Dim combinedScoringMapKey = New ScoringMap(New ScoringParameter() {sp}, solution).GetMap().First()

        Dim result = combinedScoringMapKey.GetIdForAnswerCategory(solution)

        Assert.AreEqual(1, result)
    End Sub


    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub GetIdForFilled_maxIdIs1_Expexts2()
        Dim solution = solutionMCWithAnswerCategory_1.To(Of Solution)()
        Dim sp = New ChoiceScoringParameter() With {.FindingOverride = "Integratie", .ControllerId = "Controller", .MaxChoices = 1}.AddSubParameters("A", "B", "C")
        Dim combinedScoringMapKey = New ScoringMap(New ScoringParameter() {sp}, solution).GetMap().First()

        Dim result = combinedScoringMapKey.GetIdForAnswerCategory(solution)

        Assert.AreEqual(2, result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub GetIdForFilled_maxIdIs500_Expexts501()
        Dim solution = solutionMCWithAnswerCategory_500.To(Of Solution)()
        Dim sp = New ChoiceScoringParameter() With {.FindingOverride = "Integratie", .ControllerId = "Controller", .MaxChoices = 1}.AddSubParameters("A", "B", "C")
        Dim combinedScoringMapKey = New ScoringMap(New ScoringParameter() {sp}, solution).GetMap().First()

        Dim result = combinedScoringMapKey.GetIdForAnswerCategory(solution)

        Assert.AreEqual(501, result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub GetIdForGroupedMC_SolutionHasNoAnswerCategory_ButHasCatchAll_ExpectsId_1()
        Dim solution = solutionTwoMC_ConceptAndCatchAllButNoAnswerCategory.To(Of Solution)()
        Dim choiceSps = New ChoiceScoringParameter() {
            New ChoiceScoringParameter() With {.FindingOverride = "Opgave", .ControllerId = "een", .MaxChoices = 1}.AddSubParameters("A", "B", "C"),
            New ChoiceScoringParameter() With {.FindingOverride = "Opgave", .ControllerId = "twee", .MaxChoices = 1}.AddSubParameters("A", "B", "C")
        }
        Dim combinedScoringMapKey = New ScoringMap(choiceSps, solution).GetMap().First()

        Dim result = combinedScoringMapKey.GetIdForAnswerCategory(solution)

        Assert.AreEqual(1, result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub GetIdForGroupedMC_SolutionHasAnswerCategory_ButHasCatchAll_ExpectsId_2()
        Dim solution = solutionTwoMC_ConceptAndCatchAllButSingleAnswerCategory.To(Of Solution)()
        Dim choiceSps = New ChoiceScoringParameter() {
            New ChoiceScoringParameter() With {.FindingOverride = "Opgave", .ControllerId = "een", .MaxChoices = 1}.AddSubParameters("A", "B", "C"),
            New ChoiceScoringParameter() With {.FindingOverride = "Opgave", .ControllerId = "twee", .MaxChoices = 1}.AddSubParameters("A", "B", "C")
        }
        Dim combinedScoringMapKey = New ScoringMap(choiceSps, solution).GetMap().First()

        Dim result = combinedScoringMapKey.GetIdForAnswerCategory(solution)

        Assert.AreEqual(2, result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub GetIdForGroupedMC_SolutionHasNoAnswerCategory500_ButHasCatchAll_ExpectsId_501()
        Dim solution = solutionTwoMC_ConceptAndCatchAllButSingleAnswerCategory500.To(Of Solution)()
        Dim choiceSps = New ChoiceScoringParameter() {
            New ChoiceScoringParameter() With {.FindingOverride = "Opgave", .ControllerId = "een", .MaxChoices = 1}.AddSubParameters("A", "B", "C"),
            New ChoiceScoringParameter() With {.FindingOverride = "Opgave", .ControllerId = "twee", .MaxChoices = 1}.AddSubParameters("A", "B", "C")
        }
        Dim combinedScoringMapKey = New ScoringMap(choiceSps, solution).GetMap().First()

        Dim result = combinedScoringMapKey.GetIdForAnswerCategory(solution)

        Assert.AreEqual(501, result)
    End Sub


    ReadOnly solutionMC As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                          <keyFindings>
                                              <keyFinding id="Integratie" scoringMethod="None">
                                                  <keyFact id="A-Controller" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                      <keyValue domain="Controller" occur="1">
                                                          <stringValue>
                                                              <typedValue>A</typedValue>
                                                          </stringValue>
                                                      </keyValue>
                                                  </keyFact>
                                              </keyFinding>
                                          </keyFindings>
                                          <conceptFindings>
                                              <conceptFinding id="Integratie" scoringMethod="None">
                                                  <conceptFact id="A-Controller" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                      <conceptValue domain="A-Controller" occur="1">
                                                          <stringValue>
                                                              <typedValue>A</typedValue>
                                                          </stringValue>
                                                      </conceptValue>
                                                  </conceptFact>
                                              </conceptFinding>
                                          </conceptFindings>
                                          <aspectReferences/>
                                      </solution>

    ReadOnly solutionMCWithAnswerCategory_1 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                                              <keyFindings>
                                                                  <keyFinding id="Integratie" scoringMethod="None">
                                                                      <keyFact id="A-Controller" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                          <keyValue domain="Controller" occur="1">
                                                                              <stringValue>
                                                                                  <typedValue>A</typedValue>
                                                                              </stringValue>
                                                                          </keyValue>
                                                                      </keyFact>
                                                                  </keyFinding>
                                                              </keyFindings>
                                                              <conceptFindings>
                                                                  <conceptFinding id="Integratie" scoringMethod="None">

                                                                      <conceptFact id="A-Controller" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                          <conceptValue domain="A-Controller" occur="1">
                                                                              <stringValue>
                                                                                  <typedValue>A</typedValue>
                                                                              </stringValue>
                                                                          </conceptValue>
                                                                      </conceptFact>

                                                                      <conceptFact id="B[1]-Controller" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                          <conceptValue domain="Controller" occur="1">
                                                                              <stringValue>
                                                                                  <typedValue>B</typedValue>
                                                                              </stringValue>
                                                                          </conceptValue>
                                                                      </conceptFact>

                                                                      <conceptFact id="C[1]-Controller" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                          <conceptValue domain="Controller" occur="1">
                                                                              <stringValue>
                                                                                  <typedValue>C</typedValue>
                                                                              </stringValue>
                                                                          </conceptValue>
                                                                      </conceptFact>

                                                                  </conceptFinding>
                                                              </conceptFindings>
                                                              <aspectReferences/>
                                                          </solution>

    ReadOnly solutionMCWithAnswerCategory_500 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                                                <keyFindings>
                                                                    <keyFinding id="Integratie" scoringMethod="None">
                                                                        <keyFact id="A-Controller" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                            <keyValue domain="Controller" occur="1">
                                                                                <stringValue>
                                                                                    <typedValue>A</typedValue>
                                                                                </stringValue>
                                                                            </keyValue>
                                                                        </keyFact>
                                                                    </keyFinding>
                                                                </keyFindings>
                                                                <conceptFindings>
                                                                    <conceptFinding id="Integratie" scoringMethod="None">

                                                                        <conceptFact id="A-Controller" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                            <conceptValue domain="A-Controller" occur="1">
                                                                                <stringValue>
                                                                                    <typedValue>A</typedValue>
                                                                                </stringValue>
                                                                            </conceptValue>
                                                                        </conceptFact>

                                                                        <conceptFact id="B[1]-Controller" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                            <conceptValue domain="Controller" occur="1">
                                                                                <stringValue>
                                                                                    <typedValue>B</typedValue>
                                                                                </stringValue>
                                                                            </conceptValue>
                                                                        </conceptFact>

                                                                        <conceptFact id="C[500]-Controller" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                            <conceptValue domain="Controller" occur="1">
                                                                                <stringValue>
                                                                                    <typedValue>C</typedValue>
                                                                                </stringValue>
                                                                            </conceptValue>
                                                                        </conceptFact>

                                                                    </conceptFinding>
                                                                </conceptFindings>
                                                                <aspectReferences/>
                                                            </solution>

    ReadOnly solutionTwoMC_ConceptAndCatchAllButNoAnswerCategory As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                                                                   <keyFindings>
                                                                                       <keyFinding id="Opgave" scoringMethod="Dichotomous">
                                                                                           <keyFactSet>
                                                                                               <keyFact id="C-een" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                   <keyValue domain="een" occur="1">
                                                                                                       <stringValue>
                                                                                                           <typedValue>C</typedValue>
                                                                                                       </stringValue>
                                                                                                   </keyValue>
                                                                                               </keyFact>
                                                                                               <keyFact id="A-twee" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                   <keyValue domain="twee" occur="1">
                                                                                                       <stringValue>
                                                                                                           <typedValue>A</typedValue>
                                                                                                       </stringValue>
                                                                                                   </keyValue>
                                                                                               </keyFact>
                                                                                           </keyFactSet>
                                                                                       </keyFinding>
                                                                                   </keyFindings>
                                                                                   <conceptFindings>
                                                                                       <conceptFinding id="Opgave" scoringMethod="None">
                                                                                           <conceptFactSet>
                                                                                               <conceptFact id="C-een" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                   <conceptValue domain="C-een" occur="1">
                                                                                                       <stringValue>
                                                                                                           <typedValue>C</typedValue>
                                                                                                       </stringValue>
                                                                                                   </conceptValue>
                                                                                               </conceptFact>
                                                                                               <conceptFact id="A-twee" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                   <conceptValue domain="A-twee" occur="1">
                                                                                                       <stringValue>
                                                                                                           <typedValue>A</typedValue>
                                                                                                       </stringValue>
                                                                                                   </conceptValue>
                                                                                               </conceptFact>
                                                                                               <concepts xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                                                           </conceptFactSet>
                                                                                           <conceptFactSet>
                                                                                               <conceptFact id="A[*]-twee" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                   <conceptValue domain="A[*]-twee" occur="1">
                                                                                                       <catchAllValue/>
                                                                                                   </conceptValue>
                                                                                               </conceptFact>
                                                                                               <conceptFact id="B[*]-twee" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                   <conceptValue domain="B[*]-twee" occur="1">
                                                                                                       <catchAllValue/>
                                                                                                   </conceptValue>
                                                                                               </conceptFact>
                                                                                               <conceptFact id="C[*]-twee" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                   <conceptValue domain="C[*]-twee" occur="1">
                                                                                                       <catchAllValue/>
                                                                                                   </conceptValue>
                                                                                               </conceptFact>
                                                                                               <conceptFact id="A[*]-een" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                   <conceptValue domain="A[*]-een" occur="1">
                                                                                                       <catchAllValue/>
                                                                                                   </conceptValue>
                                                                                               </conceptFact>
                                                                                               <conceptFact id="B[*]-een" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                   <conceptValue domain="B[*]-een" occur="1">
                                                                                                       <catchAllValue/>
                                                                                                   </conceptValue>
                                                                                               </conceptFact>
                                                                                               <conceptFact id="C[*]-een" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                   <conceptValue domain="C[*]-een" occur="1">
                                                                                                       <catchAllValue/>
                                                                                                   </conceptValue>
                                                                                               </conceptFact>
                                                                                               <concepts xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                                                           </conceptFactSet>
                                                                                       </conceptFinding>
                                                                                   </conceptFindings>
                                                                                   <aspectReferences/>
                                                                                   <ItemScoreTranslationTable>
                                                                                       <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                                                                                       <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                                                                                   </ItemScoreTranslationTable>
                                                                               </solution>

    ReadOnly solutionTwoMC_ConceptAndCatchAllButSingleAnswerCategory As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                                                                       <keyFindings>
                                                                                           <keyFinding id="Opgave" scoringMethod="Dichotomous">
                                                                                               <keyFactSet>
                                                                                                   <keyFact id="C-een" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                       <keyValue domain="een" occur="1">
                                                                                                           <stringValue>
                                                                                                               <typedValue>C</typedValue>
                                                                                                           </stringValue>
                                                                                                       </keyValue>
                                                                                                   </keyFact>
                                                                                                   <keyFact id="A-twee" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                       <keyValue domain="twee" occur="1">
                                                                                                           <stringValue>
                                                                                                               <typedValue>A</typedValue>
                                                                                                           </stringValue>
                                                                                                       </keyValue>
                                                                                                   </keyFact>
                                                                                               </keyFactSet>
                                                                                           </keyFinding>
                                                                                       </keyFindings>
                                                                                       <conceptFindings>
                                                                                           <conceptFinding id="Opgave" scoringMethod="None">
                                                                                               <conceptFactSet>
                                                                                                   <conceptFact id="C-een" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                       <conceptValue domain="C-een" occur="1">
                                                                                                           <stringValue>
                                                                                                               <typedValue>C</typedValue>
                                                                                                           </stringValue>
                                                                                                       </conceptValue>
                                                                                                   </conceptFact>
                                                                                                   <conceptFact id="A-twee" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                       <conceptValue domain="A-twee" occur="1">
                                                                                                           <stringValue>
                                                                                                               <typedValue>A</typedValue>
                                                                                                           </stringValue>
                                                                                                       </conceptValue>
                                                                                                   </conceptFact>
                                                                                                   <concepts xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                                                               </conceptFactSet>

                                                                                               <conceptFactSet>
                                                                                                   <conceptFact id="B[1]-een" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                       <conceptValue domain="een" occur="1">
                                                                                                           <stringValue>
                                                                                                               <typedValue>B</typedValue>
                                                                                                           </stringValue>
                                                                                                       </conceptValue>
                                                                                                   </conceptFact>
                                                                                                   <conceptFact id="B[1]-twee" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                       <conceptValue domain="twee" occur="1">
                                                                                                           <stringValue>
                                                                                                               <typedValue>B</typedValue>
                                                                                                           </stringValue>
                                                                                                       </conceptValue>
                                                                                                   </conceptFact>
                                                                                                   <concepts xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                                                               </conceptFactSet>

                                                                                               <conceptFactSet>
                                                                                                   <conceptFact id="A[*]-twee" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                       <conceptValue domain="A[*]-twee" occur="1">
                                                                                                           <catchAllValue/>
                                                                                                       </conceptValue>
                                                                                                   </conceptFact>
                                                                                                   <conceptFact id="B[*]-twee" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                       <conceptValue domain="B[*]-twee" occur="1">
                                                                                                           <catchAllValue/>
                                                                                                       </conceptValue>
                                                                                                   </conceptFact>
                                                                                                   <conceptFact id="C[*]-twee" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                       <conceptValue domain="C[*]-twee" occur="1">
                                                                                                           <catchAllValue/>
                                                                                                       </conceptValue>
                                                                                                   </conceptFact>
                                                                                                   <conceptFact id="A[*]-een" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                       <conceptValue domain="A[*]-een" occur="1">
                                                                                                           <catchAllValue/>
                                                                                                       </conceptValue>
                                                                                                   </conceptFact>
                                                                                                   <conceptFact id="B[*]-een" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                       <conceptValue domain="B[*]-een" occur="1">
                                                                                                           <catchAllValue/>
                                                                                                       </conceptValue>
                                                                                                   </conceptFact>
                                                                                                   <conceptFact id="C[*]-een" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                       <conceptValue domain="C[*]-een" occur="1">
                                                                                                           <catchAllValue/>
                                                                                                       </conceptValue>
                                                                                                   </conceptFact>
                                                                                                   <concepts xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                                                               </conceptFactSet>
                                                                                           </conceptFinding>
                                                                                       </conceptFindings>
                                                                                       <aspectReferences/>
                                                                                       <ItemScoreTranslationTable>
                                                                                           <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                                                                                           <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                                                                                       </ItemScoreTranslationTable>
                                                                                   </solution>

    ReadOnly solutionTwoMC_ConceptAndCatchAllButSingleAnswerCategory500 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                                                                          <keyFindings>
                                                                                              <keyFinding id="Opgave" scoringMethod="Dichotomous">
                                                                                                  <keyFactSet>
                                                                                                      <keyFact id="C-een" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                          <keyValue domain="een" occur="1">
                                                                                                              <stringValue>
                                                                                                                  <typedValue>C</typedValue>
                                                                                                              </stringValue>
                                                                                                          </keyValue>
                                                                                                      </keyFact>
                                                                                                      <keyFact id="A-twee" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                          <keyValue domain="twee" occur="1">
                                                                                                              <stringValue>
                                                                                                                  <typedValue>A</typedValue>
                                                                                                              </stringValue>
                                                                                                          </keyValue>
                                                                                                      </keyFact>
                                                                                                  </keyFactSet>
                                                                                              </keyFinding>
                                                                                          </keyFindings>
                                                                                          <conceptFindings>
                                                                                              <conceptFinding id="Opgave" scoringMethod="None">
                                                                                                  <conceptFactSet>
                                                                                                      <conceptFact id="C-een" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                          <conceptValue domain="C-een" occur="1">
                                                                                                              <stringValue>
                                                                                                                  <typedValue>C</typedValue>
                                                                                                              </stringValue>
                                                                                                          </conceptValue>
                                                                                                      </conceptFact>
                                                                                                      <conceptFact id="A-twee" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                          <conceptValue domain="A-twee" occur="1">
                                                                                                              <stringValue>
                                                                                                                  <typedValue>A</typedValue>
                                                                                                              </stringValue>
                                                                                                          </conceptValue>
                                                                                                      </conceptFact>
                                                                                                      <concepts xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                                                                  </conceptFactSet>

                                                                                                  <conceptFactSet>
                                                                                                      <conceptFact id="B[500]-een" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                          <conceptValue domain="een" occur="1">
                                                                                                              <stringValue>
                                                                                                                  <typedValue>B</typedValue>
                                                                                                              </stringValue>
                                                                                                          </conceptValue>
                                                                                                      </conceptFact>
                                                                                                      <conceptFact id="B[500]-twee" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                          <conceptValue domain="twee" occur="1">
                                                                                                              <stringValue>
                                                                                                                  <typedValue>B</typedValue>
                                                                                                              </stringValue>
                                                                                                          </conceptValue>
                                                                                                      </conceptFact>
                                                                                                      <concepts xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                                                                  </conceptFactSet>

                                                                                                  <conceptFactSet>
                                                                                                      <conceptFact id="A[*]-twee" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                          <conceptValue domain="A[*]-twee" occur="1">
                                                                                                              <catchAllValue/>
                                                                                                          </conceptValue>
                                                                                                      </conceptFact>
                                                                                                      <conceptFact id="B[*]-twee" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                          <conceptValue domain="B[*]-twee" occur="1">
                                                                                                              <catchAllValue/>
                                                                                                          </conceptValue>
                                                                                                      </conceptFact>
                                                                                                      <conceptFact id="C[*]-twee" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                          <conceptValue domain="C[*]-twee" occur="1">
                                                                                                              <catchAllValue/>
                                                                                                          </conceptValue>
                                                                                                      </conceptFact>
                                                                                                      <conceptFact id="A[*]-een" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                          <conceptValue domain="A[*]-een" occur="1">
                                                                                                              <catchAllValue/>
                                                                                                          </conceptValue>
                                                                                                      </conceptFact>
                                                                                                      <conceptFact id="B[*]-een" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                          <conceptValue domain="B[*]-een" occur="1">
                                                                                                              <catchAllValue/>
                                                                                                          </conceptValue>
                                                                                                      </conceptFact>
                                                                                                      <conceptFact id="C[*]-een" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                          <conceptValue domain="C[*]-een" occur="1">
                                                                                                              <catchAllValue/>
                                                                                                          </conceptValue>
                                                                                                      </conceptFact>
                                                                                                      <concepts xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                                                                  </conceptFactSet>
                                                                                              </conceptFinding>
                                                                                          </conceptFindings>
                                                                                          <aspectReferences/>
                                                                                          <ItemScoreTranslationTable>
                                                                                              <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                                                                                              <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                                                                                          </ItemScoreTranslationTable>
                                                                                      </solution>


End Class
