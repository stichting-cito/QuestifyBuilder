
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel.ResponseAndScoringModel.Solution.ConcreteScoring

''' <summary>
''' This class tests the behavior of factsets.
''' 
''' A factset is a collection of facts that need to be evaluated in a group.
''' 
''' A concrete example:
''' Question: Given [1 2 3 5 6 7 9] what numbers are missing from [1..9]?
''' Answer: (4 and 8) or (8 and 4)
''' 
''' So the solution has 1 finding with a factset
''' Finding:
'''    factset 1 : 
'''             fact 4
'''             fact 8
'''    factset 2:
'''             fact 8
'''             fact 4
''' 
''' The construction:
''' Finding:
'''       fact 4
'''       fact 8
''' 
''' is incapable of scoring 8 and 4
''' 
''' The construction:
''' Finding:
'''       fact 4 or 8
'''       fact 8 or 4
''' 
''' will allow answer 4 and 4. So this isn't a possibility. So that's why the factset exists.
''' Hope this is clear.
''' </summary>
<TestClass()>
Public Class IntegerValue_FactSet_Test
    Inherits ScoringTestBase

#Region "Dichotomous"

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub MaxScore()
        'Arrange
        Dim solution = toSolution(INT_Dichotomous_43Or131)
       
        'Act
        Dim result = solution.MaxSolutionRawScore

        'Assert
        Assert.AreEqual(1, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub MaxScore_poly()
        'Arrange
        Dim solution = toSolution(INT_Polytomous_43Or131)
      
        'Act
        Dim result = solution.MaxSolutionRawScore

        'Assert
        If ScoringMethod IsNot Nothing AndAlso ScoringMethod.Equals(ScoringFactory.Methods.V23_Scoring) Then
            Assert.AreEqual(2, result)
        Else
            Assert.AreEqual(1, result)
        End If
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub Dichotomous_ScoreWith_A42_B131_Score1()
        'Arrange
        Dim solution = toSolution(INT_Dichotomous_43Or131)
        Dim r = GetResponse(New List(Of Integer) From {42, 131})

        Write("Response", "Arrange", r) 'Write for debugging
       
        'Act
        Dim result = solution.ScoreSolution(r)

        'Assert
        Assert.AreEqual(1, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub Dichotomous_ScoreWith_A131_B42_Score1()
        'Arrange
        Dim solution = toSolution(INT_Dichotomous_43Or131)
        Dim r = GetResponse(New List(Of Integer) From {131, 42})

        Write("Response", "Arrange", r) 'Write for debugging
        
        'Act
        Dim result = solution.ScoreSolution(r)

        'Assert
        Assert.AreEqual(1, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    <Description("Partially incorrect")>
    Public Sub Dichotomous_ScoreWith_A130_B42_Score0()
        'Arrange
        Dim solution = toSolution(INT_Dichotomous_43Or131)
        Dim r = GetResponse(New List(Of Integer) From {130, 42})

        Write("Response", "Arrange", r) 'Write for debugging
       
        'Act
        Dim result = solution.ScoreSolution(r)

        'Assert
        Assert.AreEqual(0, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    <Description("Partially incorrect")>
    Public Sub Dichotomous_ScoreWith_A131_B41_Score0()
        'Arrange
        Dim solution = toSolution(INT_Dichotomous_43Or131)
        Dim r = GetResponse(New List(Of Integer) From {131, 41})

        Write("Response", "Arrange", r) 'Write for debugging
        
        'Act
        Dim result = solution.ScoreSolution(r)

        'Assert
        Assert.AreEqual(0, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    <Description("Fully incorrect")>
    Public Sub Dichotomous_ScoreWith_A200_B800_Score0()
        'Arrange
        Dim solution = toSolution(INT_Dichotomous_43Or131)
        Dim r = GetResponse(New List(Of Integer) From {200, 800})

        Write("Response", "Arrange", r) 'Write for debugging
        
        'Act
        Dim result = solution.ScoreSolution(r)

        'Assert
        Assert.AreEqual(0, result)
    End Sub

#End Region

#Region "Polytomous"

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub Polytomous_ScoreWith_A42_B131_Score2()
        'Arrange
        Dim solution = toSolution(INT_Polytomous_43Or131)
        Dim r = GetResponse(New List(Of Integer) From {42, 131})

        Write("Response", "Arrange", r) 'Write for debugging
        
        'Act
        Dim result = solution.ScoreSolution(r)

        'Assert
        If ScoringMethod IsNot Nothing AndAlso ScoringMethod.Equals(ScoringFactory.Methods.V23_Scoring) Then
            Assert.AreEqual(2, result)
        Else
            Assert.AreEqual(1, result)
        End If
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub Polytomous_ScoreWith_A131_B42_Score2()
        'Arrange
        Dim solution = toSolution(INT_Polytomous_43Or131)
        Dim r = GetResponse(New List(Of Integer) From {131, 42})

        Write("Response", "Arrange", r) 'Write for debugging
      
        'Act
        Dim result = solution.ScoreSolution(r)

        'Assert
        If ScoringMethod IsNot Nothing AndAlso ScoringMethod.Equals(ScoringFactory.Methods.V23_Scoring) Then
            Assert.AreEqual(2, result)
        Else
            Assert.AreEqual(1, result)
        End If
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    <Description("Partially incorrect")>
    Public Sub Polytomouss_ScoreWith_A130_B42_Score1()
        'Arrange
        Dim solution = toSolution(INT_Polytomous_43Or131)
        Dim r = GetResponse(New List(Of Integer) From {130, 42})

        Write("Response", "Arrange", r) 'Write for debugging
        
        'Act
        Dim result = solution.ScoreSolution(r)

        'Assert
        If ScoringMethod IsNot Nothing AndAlso ScoringMethod.Equals(ScoringFactory.Methods.V23_Scoring) Then
            Assert.AreEqual(1, result)
        Else
            Assert.AreEqual(0, result)  'New scoring will only score a keyfactset if all keyfacts are correctly scored.
        End If
    End Sub
    
    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    <Description("Partially incorrect")>
    Public Sub Polytomous_ScoreWith_A131_B41_Score1()
        'Arrange
        Dim solution = toSolution(INT_Polytomous_43Or131)
        Dim r = GetResponse(New List(Of Integer) From {131, 41})

        Write("Response", "Arrange", r) 'Write for debugging
       
        'Act
        Dim result = solution.ScoreSolution(r)

        'Assert
        If ScoringMethod IsNot Nothing AndAlso ScoringMethod.Equals(ScoringFactory.Methods.V23_Scoring) Then
            Assert.AreEqual(1, result)
        Else
            Assert.AreEqual(0, result)  'New scoring will only score a keyfactset if all keyfacts are correctly scored.
        End If
    End Sub
    
    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    <Description("Fully incorrect")>
    Public Sub Polytomous_ScoreWith_A200_B800_Score0()
        'Arrange
        Dim solution = toSolution(INT_Polytomous_43Or131)
        Dim r = GetResponse(New List(Of Integer) From {200, 800})

        Write("Response", "Arrange", r) 'Write for debugging
       
        'Act
        Dim result = solution.ScoreSolution(r)

        'Assert
        Assert.AreEqual(0, result)
    End Sub

#End Region

    'An example of a factSet with where (42 and 131) or (131 and 42) is the answer.
    Private INT_Dichotomous_43Or131 As XElement = <solution>
                                                      <keyFindings>
                                                          <keyFinding id="mc" scoringMethod="Dichotomous">

                                                              <keyFactSet>
                                                                  <keyFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                      <keyValue domain="A" occur="1">
                                                                          <integerValue>
                                                                              <typedValue>42</typedValue>
                                                                          </integerValue>
                                                                      </keyValue>
                                                                  </keyFact>
                                                                  <keyFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                      <keyValue domain="B" occur="1">
                                                                          <integerValue>
                                                                              <typedValue>131</typedValue>
                                                                          </integerValue>
                                                                      </keyValue>
                                                                  </keyFact>
                                                              </keyFactSet>
                                                              <keyFactSet>
                                                                  <keyFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                      <keyValue domain="A" occur="1">
                                                                          <integerValue>
                                                                              <typedValue>131</typedValue>
                                                                          </integerValue>
                                                                      </keyValue>
                                                                  </keyFact>
                                                                  <keyFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                      <keyValue domain="B" occur="1">
                                                                          <integerValue>
                                                                              <typedValue>42</typedValue>
                                                                          </integerValue>
                                                                      </keyValue>
                                                                  </keyFact>
                                                              </keyFactSet>

                                                          </keyFinding>
                                                      </keyFindings>
                                                      <aspectReferences/>
                                                  </solution>

    Private INT_Polytomous_43Or131 As XElement = <solution>
                                                     <keyFindings>
                                                         <keyFinding id="mc" scoringMethod="Polytomous">

                                                             <keyFactSet>
                                                                 <keyFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                     <keyValue domain="A" occur="1">
                                                                         <integerValue>
                                                                             <typedValue>42</typedValue>
                                                                         </integerValue>
                                                                     </keyValue>
                                                                 </keyFact>
                                                                 <keyFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                     <keyValue domain="B" occur="1">
                                                                         <integerValue>
                                                                             <typedValue>131</typedValue>
                                                                         </integerValue>
                                                                     </keyValue>
                                                                 </keyFact>
                                                             </keyFactSet>
                                                             <keyFactSet>
                                                                 <keyFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                     <keyValue domain="A" occur="1">
                                                                         <integerValue>
                                                                             <typedValue>131</typedValue>
                                                                         </integerValue>
                                                                     </keyValue>
                                                                 </keyFact>
                                                                 <keyFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                     <keyValue domain="B" occur="1">
                                                                         <integerValue>
                                                                             <typedValue>42</typedValue>
                                                                         </integerValue>
                                                                     </keyValue>
                                                                 </keyFact>
                                                             </keyFactSet>

                                                         </keyFinding>
                                                     </keyFindings>
                                                     <aspectReferences/>
                                                 </solution>

End Class
