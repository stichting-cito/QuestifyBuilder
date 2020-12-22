
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports System.Linq
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class KeyFindingToConceptFinding_SpecificExamples_Tests

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), WorkItem(20802)>
    Public Sub ShouldNotOverwrite_Answer_Category()
        Dim solution = _3KeySets_2_ConceptSetsAnd1CatchAll.To(Of Solution)()

        Dim keyFindingManipulator = New KeyManipulator(solution.Findings.First())
        Dim conceptFindingManipulator = New ConceptManipulator(solution.ConceptFindings.First())
        Dim copier As New FindingToFindingManipulator(keyFindingManipulator, conceptFindingManipulator)
        copier.Logic = New SkipAnswerCategoriesAsTarget(copier.Logic)

        copier.Execute()

        Assert.AreEqual(4, solution.ConceptFindings.First().KeyFactsets.Count, "Should Not Overwrite Answer Catagory")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), WorkItem(20802)>
    Public Sub ShouldNotOverwrite_Answer_Category_2()
        Dim solution = _4KeySets_2_ConceptSetsAnd1CatchAll.To(Of Solution)()

        Dim keyFindingManipulator = New KeyManipulator(solution.Findings.First())
        Dim conceptFindingManipulator = New ConceptManipulator(solution.ConceptFindings.First())
        Dim copier As New FindingToFindingManipulator(keyFindingManipulator, conceptFindingManipulator)
        copier.Logic = New SkipAnswerCategoriesAsTarget(copier.Logic)

        copier.Execute()

        Assert.AreEqual(5, solution.ConceptFindings.First().KeyFactsets.Count, "Should Not Overwrite Answer Catagory")
    End Sub


    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), WorkItem(20802)>
    Public Sub ShouldSkipCatchAll()
        Dim solution = _2KeySets_ConceptSetsWithOnlyCatchAll.To(Of Solution)()

        Dim keyFindingManipulator = New KeyManipulator(solution.Findings.First())
        Dim conceptFindingManipulator = New ConceptManipulator(solution.ConceptFindings.First())
        Dim copier As New FindingToFindingManipulator(keyFindingManipulator, conceptFindingManipulator)
        copier.Logic = New SkipAnswerCategoriesAsTarget(copier.Logic)

        copier.Execute()

        Assert.AreEqual(3, solution.ConceptFindings.First().KeyFactsets.Count, "Should skip catch all.")

    End Sub

    Private _3KeySets_2_ConceptSetsAnd1CatchAll As XElement = <solution>
                                                                  <keyFindings>
                                                                      <keyFinding id="gapMatchController" scoringMethod="Dichotomous">

                                                                          <keyFactSet>
                                                                              <keyFact id="I7156dcf0-f132-4aa3-86d3-02726d7a3ba9" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                  <keyValue domain="I7156dcf0-f132-4aa3-86d3-02726d7a3ba9" occur="1">
                                                                                      <stringValue>
                                                                                          <typedValue>G</typedValue>
                                                                                      </stringValue>
                                                                                  </keyValue>
                                                                              </keyFact>
                                                                              <keyFact id="I53b5dcd1-1f54-443e-a707-ccc9d4920418" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                  <keyValue domain="I53b5dcd1-1f54-443e-a707-ccc9d4920418" occur="1">
                                                                                      <stringValue>
                                                                                          <typedValue>F</typedValue>
                                                                                      </stringValue>
                                                                                  </keyValue>
                                                                              </keyFact>
                                                                          </keyFactSet>

                                                                          <keyFactSet>
                                                                              <keyFact id="I7156dcf0-f132-4aa3-86d3-02726d7a3ba9" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                  <keyValue domain="I7156dcf0-f132-4aa3-86d3-02726d7a3ba9" occur="1">
                                                                                      <stringValue>
                                                                                          <typedValue>B</typedValue>
                                                                                      </stringValue>
                                                                                  </keyValue>
                                                                              </keyFact>
                                                                              <keyFact id="I53b5dcd1-1f54-443e-a707-ccc9d4920418" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                  <keyValue domain="I53b5dcd1-1f54-443e-a707-ccc9d4920418" occur="1">
                                                                                      <stringValue>
                                                                                          <typedValue>E</typedValue>
                                                                                      </stringValue>
                                                                                  </keyValue>
                                                                              </keyFact>
                                                                          </keyFactSet>

                                                                          <keyFactSet>
                                                                              <keyFact id="I7156dcf0-f132-4aa3-86d3-02726d7a3ba9" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                  <keyValue domain="I7156dcf0-f132-4aa3-86d3-02726d7a3ba9" occur="1">
                                                                                      <stringValue>
                                                                                          <typedValue>A</typedValue>
                                                                                      </stringValue>
                                                                                  </keyValue>
                                                                              </keyFact>
                                                                              <keyFact id="I53b5dcd1-1f54-443e-a707-ccc9d4920418" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                  <keyValue domain="I53b5dcd1-1f54-443e-a707-ccc9d4920418" occur="1">
                                                                                      <stringValue>
                                                                                          <typedValue>B</typedValue>
                                                                                      </stringValue>
                                                                                  </keyValue>
                                                                              </keyFact>
                                                                          </keyFactSet>

                                                                      </keyFinding>
                                                                  </keyFindings>
                                                                  <conceptFindings>
                                                                      <conceptFinding id="gapMatchController" scoringMethod="None">

                                                                          <conceptFactSet>
                                                                              <conceptFact id="I7156dcf0-f132-4aa3-86d3-02726d7a3ba9" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                  <conceptValue domain="I7156dcf0-f132-4aa3-86d3-02726d7a3ba9" occur="1">
                                                                                      <stringValue>
                                                                                          <typedValue>G</typedValue>
                                                                                      </stringValue>
                                                                                  </conceptValue>
                                                                              </conceptFact>
                                                                              <conceptFact id="I53b5dcd1-1f54-443e-a707-ccc9d4920418" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                  <conceptValue domain="I53b5dcd1-1f54-443e-a707-ccc9d4920418" occur="1">
                                                                                      <stringValue>
                                                                                          <typedValue>F</typedValue>
                                                                                      </stringValue>
                                                                                  </conceptValue>
                                                                              </conceptFact>
                                                                          </conceptFactSet>

                                                                          <conceptFactSet>
                                                                              <conceptFact id="I7156dcf0-f132-4aa3-86d3-02726d7a3ba9" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                  <conceptValue domain="I7156dcf0-f132-4aa3-86d3-02726d7a3ba9" occur="1">
                                                                                      <stringValue>
                                                                                          <typedValue>B</typedValue>
                                                                                      </stringValue>
                                                                                  </conceptValue>
                                                                              </conceptFact>
                                                                              <conceptFact id="I53b5dcd1-1f54-443e-a707-ccc9d4920418" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                  <conceptValue domain="I53b5dcd1-1f54-443e-a707-ccc9d4920418" occur="1">
                                                                                      <stringValue>
                                                                                          <typedValue>E</typedValue>
                                                                                      </stringValue>
                                                                                  </conceptValue>
                                                                              </conceptFact>
                                                                          </conceptFactSet>

                                                                          <conceptFactSet>
                                                                              <conceptFact id="I7156dcf0-f132-4aa3-86d3-02726d7a3ba9[*]" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                  <conceptValue domain="I7156dcf0-f132-4aa3-86d3-02726d7a3ba9[*]" occur="1">
                                                                                      <catchAllValue/>
                                                                                  </conceptValue>
                                                                              </conceptFact>
                                                                              <conceptFact id="I53b5dcd1-1f54-443e-a707-ccc9d4920418[*]" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                  <conceptValue domain="I53b5dcd1-1f54-443e-a707-ccc9d4920418[*]" occur="1">
                                                                                      <catchAllValue/>
                                                                                  </conceptValue>
                                                                              </conceptFact>
                                                                          </conceptFactSet>

                                                                      </conceptFinding>
                                                                  </conceptFindings>
                                                                  <aspectReferences/>
                                                              </solution>

    Private _4KeySets_2_ConceptSetsAnd1CatchAll As XElement = <solution>
                                                                  <keyFindings>
                                                                      <keyFinding id="gapMatchController" scoringMethod="Dichotomous">

                                                                          <keyFactSet>
                                                                              <keyFact id="I7156dcf0-f132-4aa3-86d3-02726d7a3ba9" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                  <keyValue domain="I7156dcf0-f132-4aa3-86d3-02726d7a3ba9" occur="1">
                                                                                      <stringValue>
                                                                                          <typedValue>G</typedValue>
                                                                                      </stringValue>
                                                                                  </keyValue>
                                                                              </keyFact>
                                                                              <keyFact id="I53b5dcd1-1f54-443e-a707-ccc9d4920418" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                  <keyValue domain="I53b5dcd1-1f54-443e-a707-ccc9d4920418" occur="1">
                                                                                      <stringValue>
                                                                                          <typedValue>F</typedValue>
                                                                                      </stringValue>
                                                                                  </keyValue>
                                                                              </keyFact>
                                                                          </keyFactSet>

                                                                          <keyFactSet>
                                                                              <keyFact id="I7156dcf0-f132-4aa3-86d3-02726d7a3ba9" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                  <keyValue domain="I7156dcf0-f132-4aa3-86d3-02726d7a3ba9" occur="1">
                                                                                      <stringValue>
                                                                                          <typedValue>B</typedValue>
                                                                                      </stringValue>
                                                                                  </keyValue>
                                                                              </keyFact>
                                                                              <keyFact id="I53b5dcd1-1f54-443e-a707-ccc9d4920418" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                  <keyValue domain="I53b5dcd1-1f54-443e-a707-ccc9d4920418" occur="1">
                                                                                      <stringValue>
                                                                                          <typedValue>E</typedValue>
                                                                                      </stringValue>
                                                                                  </keyValue>
                                                                              </keyFact>
                                                                          </keyFactSet>

                                                                          <keyFactSet>
                                                                              <keyFact id="I7156dcf0-f132-4aa3-86d3-02726d7a3ba9" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                  <keyValue domain="I7156dcf0-f132-4aa3-86d3-02726d7a3ba9" occur="1">
                                                                                      <stringValue>
                                                                                          <typedValue>A</typedValue>
                                                                                      </stringValue>
                                                                                  </keyValue>
                                                                              </keyFact>
                                                                              <keyFact id="I53b5dcd1-1f54-443e-a707-ccc9d4920418" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                  <keyValue domain="I53b5dcd1-1f54-443e-a707-ccc9d4920418" occur="1">
                                                                                      <stringValue>
                                                                                          <typedValue>B</typedValue>
                                                                                      </stringValue>
                                                                                  </keyValue>
                                                                              </keyFact>
                                                                          </keyFactSet>


                                                                          <keyFactSet>
                                                                              <keyFact id="I7156dcf0-f132-4aa3-86d3-02726d7a3ba9" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                  <keyValue domain="I7156dcf0-f132-4aa3-86d3-02726d7a3ba9" occur="1">
                                                                                      <stringValue>
                                                                                          <typedValue>B</typedValue>
                                                                                      </stringValue>
                                                                                  </keyValue>
                                                                              </keyFact>
                                                                              <keyFact id="I53b5dcd1-1f54-443e-a707-ccc9d4920418" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                  <keyValue domain="I53b5dcd1-1f54-443e-a707-ccc9d4920418" occur="1">
                                                                                      <stringValue>
                                                                                          <typedValue>A</typedValue>
                                                                                      </stringValue>
                                                                                  </keyValue>
                                                                              </keyFact>
                                                                          </keyFactSet>

                                                                      </keyFinding>
                                                                  </keyFindings>
                                                                  <conceptFindings>
                                                                      <conceptFinding id="gapMatchController" scoringMethod="None">

                                                                          <conceptFactSet>
                                                                              <conceptFact id="I7156dcf0-f132-4aa3-86d3-02726d7a3ba9" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                  <conceptValue domain="I7156dcf0-f132-4aa3-86d3-02726d7a3ba9" occur="1">
                                                                                      <stringValue>
                                                                                          <typedValue>G</typedValue>
                                                                                      </stringValue>
                                                                                  </conceptValue>
                                                                              </conceptFact>
                                                                              <conceptFact id="I53b5dcd1-1f54-443e-a707-ccc9d4920418" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                  <conceptValue domain="I53b5dcd1-1f54-443e-a707-ccc9d4920418" occur="1">
                                                                                      <stringValue>
                                                                                          <typedValue>F</typedValue>
                                                                                      </stringValue>
                                                                                  </conceptValue>
                                                                              </conceptFact>
                                                                          </conceptFactSet>

                                                                          <conceptFactSet>
                                                                              <conceptFact id="I7156dcf0-f132-4aa3-86d3-02726d7a3ba9" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                  <conceptValue domain="I7156dcf0-f132-4aa3-86d3-02726d7a3ba9" occur="1">
                                                                                      <stringValue>
                                                                                          <typedValue>B</typedValue>
                                                                                      </stringValue>
                                                                                  </conceptValue>
                                                                              </conceptFact>
                                                                              <conceptFact id="I53b5dcd1-1f54-443e-a707-ccc9d4920418" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                  <conceptValue domain="I53b5dcd1-1f54-443e-a707-ccc9d4920418" occur="1">
                                                                                      <stringValue>
                                                                                          <typedValue>E</typedValue>
                                                                                      </stringValue>
                                                                                  </conceptValue>
                                                                              </conceptFact>
                                                                          </conceptFactSet>

                                                                          <conceptFactSet>
                                                                              <conceptFact id="I7156dcf0-f132-4aa3-86d3-02726d7a3ba9[*]" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                  <conceptValue domain="I7156dcf0-f132-4aa3-86d3-02726d7a3ba9[*]" occur="1">
                                                                                      <catchAllValue/>
                                                                                  </conceptValue>
                                                                              </conceptFact>
                                                                              <conceptFact id="I53b5dcd1-1f54-443e-a707-ccc9d4920418[*]" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                  <conceptValue domain="I53b5dcd1-1f54-443e-a707-ccc9d4920418[*]" occur="1">
                                                                                      <catchAllValue/>
                                                                                  </conceptValue>
                                                                              </conceptFact>
                                                                          </conceptFactSet>

                                                                      </conceptFinding>
                                                                  </conceptFindings>
                                                                  <aspectReferences/>
                                                              </solution>


    Private _2KeySets_ConceptSetsWithOnlyCatchAll As XElement = <solution>
                                                                    <keyFindings>
                                                                        <keyFinding id="gapMatchController" scoringMethod="Dichotomous">

                                                                            <keyFactSet>
                                                                                <keyFact id="I7156dcf0-f132-4aa3-86d3-02726d7a3ba9" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                    <keyValue domain="I7156dcf0-f132-4aa3-86d3-02726d7a3ba9" occur="1">
                                                                                        <stringValue>
                                                                                            <typedValue>G</typedValue>
                                                                                        </stringValue>
                                                                                    </keyValue>
                                                                                </keyFact>
                                                                                <keyFact id="I53b5dcd1-1f54-443e-a707-ccc9d4920418" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                    <keyValue domain="I53b5dcd1-1f54-443e-a707-ccc9d4920418" occur="1">
                                                                                        <stringValue>
                                                                                            <typedValue>F</typedValue>
                                                                                        </stringValue>
                                                                                    </keyValue>
                                                                                </keyFact>
                                                                            </keyFactSet>

                                                                            <keyFactSet>
                                                                                <keyFact id="I7156dcf0-f132-4aa3-86d3-02726d7a3ba9" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                    <keyValue domain="I7156dcf0-f132-4aa3-86d3-02726d7a3ba9" occur="1">
                                                                                        <stringValue>
                                                                                            <typedValue>B</typedValue>
                                                                                        </stringValue>
                                                                                    </keyValue>
                                                                                </keyFact>
                                                                                <keyFact id="I53b5dcd1-1f54-443e-a707-ccc9d4920418" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                    <keyValue domain="I53b5dcd1-1f54-443e-a707-ccc9d4920418" occur="1">
                                                                                        <stringValue>
                                                                                            <typedValue>E</typedValue>
                                                                                        </stringValue>
                                                                                    </keyValue>
                                                                                </keyFact>
                                                                            </keyFactSet>

                                                                        </keyFinding>
                                                                    </keyFindings>
                                                                    <conceptFindings>
                                                                        <conceptFinding id="gapMatchController" scoringMethod="None">

                                                                            <conceptFactSet>
                                                                                <conceptFact id="I7156dcf0-f132-4aa3-86d3-02726d7a3ba9[*]" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                    <conceptValue domain="I7156dcf0-f132-4aa3-86d3-02726d7a3ba9[*]" occur="1">
                                                                                        <catchAllValue/>
                                                                                    </conceptValue>
                                                                                </conceptFact>
                                                                                <conceptFact id="I53b5dcd1-1f54-443e-a707-ccc9d4920418[*]" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                    <conceptValue domain="I53b5dcd1-1f54-443e-a707-ccc9d4920418[*]" occur="1">
                                                                                        <catchAllValue/>
                                                                                    </conceptValue>
                                                                                </conceptFact>
                                                                            </conceptFactSet>

                                                                        </conceptFinding>
                                                                    </conceptFindings>
                                                                    <aspectReferences/>
                                                                </solution>

End Class
