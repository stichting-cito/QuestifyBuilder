
Imports System.Linq
Imports System.Xml.Linq

<TestClass>
Public Class Get_SolutionPropertyValuesFrom_Examples
    Inherits ScoringTestBase

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub Get_MaxFindingScore_FromDichotomousMatrixExample_ShouldBe1()
        'Arrange
        Dim solution = toSolution(example1)
        
        'Act
        Dim result = solution.Findings.Sum(Function(f) f.MaxFindingScore)
      
        'Assert
        Assert.AreEqual(1, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub Get_MaxFindingScore_FromPolytomousMatrixExample_ShouldBe3()
        'Arrange
        Dim solution = toSolution(example2)
      
        'Act
        Dim result = solution.Findings.Sum(Function(f) f.MaxFindingScore)
      
        'Assert
        Assert.AreEqual(3, result)
    End Sub

#Region "DATA"

    Private example1 As XElement = <solution>
                                       <keyFindings>
                                           <keyFinding id="audioControllerverklankingLinks" scoringMethod="None"/>
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
                                                           <typedValue>A</typedValue>
                                                       </stringValue>
                                                   </keyValue>
                                               </keyFact>
                                           </keyFinding>
                                       </keyFindings>
                                       <aspectReferences/>
                                       <ItemScoreTranslationTable>
                                           <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                                           <ItemScoreTranslationTableEntry rawScore="1" translatedScore="0"/>
                                           <ItemScoreTranslationTableEntry rawScore="2" translatedScore="1"/>
                                       </ItemScoreTranslationTable>
                                   </solution>

    Private example2 As XElement = <solution>
                                       <keyFindings>
                                           <keyFinding id="audioControllerverklankingLinks" scoringMethod="None"/>
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
                                                           <typedValue>A</typedValue>
                                                       </stringValue>
                                                   </keyValue>
                                               </keyFact>
                                           </keyFinding>
                                       </keyFindings>
                                       <aspectReferences/>
                                       <ItemScoreTranslationTable>
                                           <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                                           <ItemScoreTranslationTableEntry rawScore="1" translatedScore="0"/>
                                           <ItemScoreTranslationTableEntry rawScore="2" translatedScore="1"/>
                                       </ItemScoreTranslationTable>
                                   </solution>

#End Region

End Class
