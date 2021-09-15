
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports System.Linq
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class ConceptFactsSetLogicTests

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub GetOrphanedConceptFactSets_From_OkSolution_ShouldReturnNotNothing()
        'Arrange
        Dim solution = _okSolutuion.To(Of Solution)()
        Dim integerScoringParameter = New IntegerScoringParameter() With
                                      {.FindingOverride = "sharedIntegerFinding", .ControllerId = "integerScore"}.AddSubParameters("1", "2")
        Dim firstCombined = New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        
        'Act
        Dim result = firstCombined.GetConceptSetIdsWithoutKeySet(solution)
        
        'Assert
        Assert.IsNotNull(result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub GetOrphanedConceptFactSets_From_OkSolution_ShouldEmptyList()
        'Arrange
        Dim solution = _okSolutuion.To(Of Solution)()
        Dim integerScoringParameter = New IntegerScoringParameter() With
                                      {.FindingOverride = "sharedIntegerFinding", .ControllerId = "integerScore"}.AddSubParameters("1", "2")
        Dim firstCombined = New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        
        'Act
        Dim result = firstCombined.GetConceptSetIdsWithoutKeySet(solution)
        
        'Assert
        Assert.AreEqual(0, result.Count())
    End Sub


    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub GetOrphanedConceptFactSets_From_TestSolution_ShouldReturnSingleItem()
        'Arrange
        Dim solution = _solutionWithOrhpanedConceptFactSet.To(Of Solution)()
        Dim integerScoringParameter = New IntegerScoringParameter() With
                                      {.FindingOverride = "sharedIntegerFinding", .ControllerId = "integerScore"}.AddSubParameters("1", "2")
        Dim firstCombined = New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        
        'Act
        Dim result = firstCombined.GetConceptSetIdsWithoutKeySet(solution)
        
        'Assert
        Assert.AreEqual(1, result.Count())
    End Sub

#Region "Data"
    Private _okSolutuion As XElement = <solution>
                                           <keyFindings>
                                               <keyFinding id="sharedIntegerFinding" scoringMethod="Dichotomous">
                                                   <keyFactSet>
                                                       <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                           <keyValue domain="integerScore" occur="1">
                                                               <integerValue>
                                                                   <typedValue>6</typedValue>
                                                               </integerValue>
                                                           </keyValue>
                                                       </keyFact>
                                                       <keyFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                           <keyValue domain="integerScore" occur="1">
                                                               <integerValue>
                                                                   <typedValue>14</typedValue>
                                                               </integerValue>
                                                           </keyValue>
                                                       </keyFact>
                                                   </keyFactSet>
                                                   <keyFactSet>
                                                       <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                           <keyValue domain="integerScore" occur="1">
                                                               <integerValue>
                                                                   <typedValue>14</typedValue>
                                                               </integerValue>
                                                           </keyValue>
                                                       </keyFact>
                                                       <keyFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                           <keyValue domain="integerScore" occur="1">
                                                               <integerValue>
                                                                   <typedValue>6</typedValue>
                                                               </integerValue>
                                                           </keyValue>
                                                       </keyFact>
                                                   </keyFactSet>
                                               </keyFinding>
                                           </keyFindings>
                                           <conceptFindings>
                                               <conceptFinding id="sharedIntegerFinding" scoringMethod="None">
                                                   <conceptFactSet>
                                                       <conceptFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                           <conceptValue domain="integerScore" occur="1">
                                                               <integerValue>
                                                                   <typedValue>6</typedValue>
                                                               </integerValue>
                                                           </conceptValue>
                                                       </conceptFact>
                                                       <conceptFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                           <conceptValue domain="integerScore" occur="1">
                                                               <integerValue>
                                                                   <typedValue>14</typedValue>
                                                               </integerValue>
                                                           </conceptValue>
                                                       </conceptFact>
                                                       <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                                           <concept value="1" code="1"/>
                                                           <concept value="1" code="1.1"/>
                                                       </concepts>
                                                   </conceptFactSet>
                                                   <conceptFactSet>
                                                       <conceptFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                           <conceptValue domain="integerScore" occur="1">
                                                               <integerValue>
                                                                   <typedValue>14</typedValue>
                                                               </integerValue>
                                                           </conceptValue>
                                                       </conceptFact>
                                                       <conceptFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                           <conceptValue domain="integerScore" occur="1">
                                                               <integerValue>
                                                                   <typedValue>6</typedValue>
                                                               </integerValue>
                                                           </conceptValue>
                                                       </conceptFact>
                                                       <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                                           <concept value="2" code="1"/>
                                                           <concept value="2" code="1.1"/>
                                                       </concepts>
                                                   </conceptFactSet>
                                                   <conceptFactSet>
                                                       <conceptFact id="1[*]-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                           <conceptValue domain="integerScore" occur="1">
                                                               <catchAllValue/>
                                                           </conceptValue>
                                                       </conceptFact>
                                                       <conceptFact id="2[*]-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                           <conceptValue domain="integerScore" occur="1">
                                                               <catchAllValue/>
                                                           </conceptValue>
                                                       </conceptFact>
                                                       <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                                           <concept value="3" code="1"/>
                                                           <concept value="3" code="1.1"/>
                                                       </concepts>
                                                   </conceptFactSet>
                                               </conceptFinding>
                                           </conceptFindings>
                                           <aspectReferences/>
                                           <ItemScoreTranslationTable>
                                               <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                                               <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                                           </ItemScoreTranslationTable>
                                       </solution>

    Private _solutionWithOrhpanedConceptFactSet As XElement = <solution>
                                                                  <keyFindings>
                                                                      <keyFinding id="sharedIntegerFinding" scoringMethod="Dichotomous">
                                                                          <keyFactSet>
                                                                              <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                  <keyValue domain="integerScore" occur="1">
                                                                                      <integerValue>
                                                                                          <typedValue>6</typedValue>
                                                                                      </integerValue>
                                                                                  </keyValue>
                                                                              </keyFact>
                                                                              <keyFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                  <keyValue domain="integerScore" occur="1">
                                                                                      <integerValue>
                                                                                          <typedValue>14</typedValue>
                                                                                      </integerValue>
                                                                                  </keyValue>
                                                                              </keyFact>
                                                                          </keyFactSet>
                                                                      </keyFinding>
                                                                  </keyFindings>
                                                                  <conceptFindings>
                                                                      <conceptFinding id="sharedIntegerFinding" scoringMethod="None">
                                                                          <conceptFactSet>
                                                                              <conceptFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                  <conceptValue domain="integerScore" occur="1">
                                                                                      <integerValue>
                                                                                          <typedValue>6</typedValue>
                                                                                      </integerValue>
                                                                                  </conceptValue>
                                                                              </conceptFact>
                                                                              <conceptFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                  <conceptValue domain="integerScore" occur="1">
                                                                                      <integerValue>
                                                                                          <typedValue>14</typedValue>
                                                                                      </integerValue>
                                                                                  </conceptValue>
                                                                              </conceptFact>
                                                                              <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                  <concept value="1" code="1"/>
                                                                                  <concept value="1" code="1.1"/>
                                                                              </concepts>
                                                                          </conceptFactSet>
                                                                          <conceptFactSet>
                                                                              <conceptFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                  <conceptValue domain="integerScore" occur="1">
                                                                                      <integerValue>
                                                                                          <typedValue>14</typedValue>
                                                                                      </integerValue>
                                                                                  </conceptValue>
                                                                              </conceptFact>
                                                                              <conceptFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                  <conceptValue domain="integerScore" occur="1">
                                                                                      <integerValue>
                                                                                          <typedValue>6</typedValue>
                                                                                      </integerValue>
                                                                                  </conceptValue>
                                                                              </conceptFact>
                                                                              <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                  <concept value="2" code="1"/>
                                                                                  <concept value="2" code="1.1"/>
                                                                              </concepts>
                                                                          </conceptFactSet>
                                                                          <conceptFactSet>
                                                                              <conceptFact id="1[*]-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                  <conceptValue domain="integerScore" occur="1">
                                                                                      <catchAllValue/>
                                                                                  </conceptValue>
                                                                              </conceptFact>
                                                                              <conceptFact id="2[*]-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                  <conceptValue domain="integerScore" occur="1">
                                                                                      <catchAllValue/>
                                                                                  </conceptValue>
                                                                              </conceptFact>
                                                                              <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                  <concept value="3" code="1"/>
                                                                                  <concept value="3" code="1.1"/>
                                                                              </concepts>
                                                                          </conceptFactSet>
                                                                      </conceptFinding>
                                                                  </conceptFindings>
                                                                  <aspectReferences/>
                                                                  <ItemScoreTranslationTable>
                                                                      <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                                                                      <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                                                                  </ItemScoreTranslationTable>
                                                              </solution>

#End Region

End Class
