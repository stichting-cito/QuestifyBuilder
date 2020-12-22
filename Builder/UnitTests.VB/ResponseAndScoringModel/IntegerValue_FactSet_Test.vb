
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel.ResponseAndScoringModel.Solution.ConcreteScoring

<TestClass()>
Public Class IntegerValue_FactSet_Test
    Inherits ScoringTestBase


    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub MaxScore()
        Dim solution = toSolution(INT_Dichotomous_43Or131)

        Dim result = solution.MaxSolutionRawScore

        Assert.AreEqual(1, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub MaxScore_poly()
        Dim solution = toSolution(INT_Polytomous_43Or131)

        Dim result = solution.MaxSolutionRawScore

        If ScoringMethod IsNot Nothing AndAlso ScoringMethod.Equals(ScoringFactory.Methods.V23_Scoring) Then
            Assert.AreEqual(2, result)
        Else
            Assert.AreEqual(1, result)
        End If
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub Dichotomous_ScoreWith_A42_B131_Score1()
        Dim solution = toSolution(INT_Dichotomous_43Or131)
        Dim r = GetResponse(New List(Of Integer) From {42, 131})

        Write("Response", "Arrange", r)

        Dim result = solution.ScoreSolution(r)

        Assert.AreEqual(1, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub Dichotomous_ScoreWith_A131_B42_Score1()
        Dim solution = toSolution(INT_Dichotomous_43Or131)
        Dim r = GetResponse(New List(Of Integer) From {131, 42})

        Write("Response", "Arrange", r)

        Dim result = solution.ScoreSolution(r)

        Assert.AreEqual(1, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    <Description("Partially incorrect")>
    Public Sub Dichotomous_ScoreWith_A130_B42_Score0()
        Dim solution = toSolution(INT_Dichotomous_43Or131)
        Dim r = GetResponse(New List(Of Integer) From {130, 42})

        Write("Response", "Arrange", r)

        Dim result = solution.ScoreSolution(r)

        Assert.AreEqual(0, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    <Description("Partially incorrect")>
    Public Sub Dichotomous_ScoreWith_A131_B41_Score0()
        Dim solution = toSolution(INT_Dichotomous_43Or131)
        Dim r = GetResponse(New List(Of Integer) From {131, 41})

        Write("Response", "Arrange", r)

        Dim result = solution.ScoreSolution(r)

        Assert.AreEqual(0, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    <Description("Fully incorrect")>
    Public Sub Dichotomous_ScoreWith_A200_B800_Score0()
        Dim solution = toSolution(INT_Dichotomous_43Or131)
        Dim r = GetResponse(New List(Of Integer) From {200, 800})

        Write("Response", "Arrange", r)

        Dim result = solution.ScoreSolution(r)

        Assert.AreEqual(0, result)
    End Sub



    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub Polytomous_ScoreWith_A42_B131_Score2()
        Dim solution = toSolution(INT_Polytomous_43Or131)
        Dim r = GetResponse(New List(Of Integer) From {42, 131})

        Write("Response", "Arrange", r)

        Dim result = solution.ScoreSolution(r)

        If ScoringMethod IsNot Nothing AndAlso ScoringMethod.Equals(ScoringFactory.Methods.V23_Scoring) Then
            Assert.AreEqual(2, result)
        Else
            Assert.AreEqual(1, result)
        End If
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub Polytomous_ScoreWith_A131_B42_Score2()
        Dim solution = toSolution(INT_Polytomous_43Or131)
        Dim r = GetResponse(New List(Of Integer) From {131, 42})

        Write("Response", "Arrange", r)

        Dim result = solution.ScoreSolution(r)

        If ScoringMethod IsNot Nothing AndAlso ScoringMethod.Equals(ScoringFactory.Methods.V23_Scoring) Then
            Assert.AreEqual(2, result)
        Else
            Assert.AreEqual(1, result)
        End If
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    <Description("Partially incorrect")>
    Public Sub Polytomouss_ScoreWith_A130_B42_Score1()
        Dim solution = toSolution(INT_Polytomous_43Or131)
        Dim r = GetResponse(New List(Of Integer) From {130, 42})

        Write("Response", "Arrange", r)

        Dim result = solution.ScoreSolution(r)

        If ScoringMethod IsNot Nothing AndAlso ScoringMethod.Equals(ScoringFactory.Methods.V23_Scoring) Then
            Assert.AreEqual(1, result)
        Else
            Assert.AreEqual(0, result)
        End If
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    <Description("Partially incorrect")>
    Public Sub Polytomous_ScoreWith_A131_B41_Score1()
        Dim solution = toSolution(INT_Polytomous_43Or131)
        Dim r = GetResponse(New List(Of Integer) From {131, 41})

        Write("Response", "Arrange", r)

        Dim result = solution.ScoreSolution(r)

        If ScoringMethod IsNot Nothing AndAlso ScoringMethod.Equals(ScoringFactory.Methods.V23_Scoring) Then
            Assert.AreEqual(1, result)
        Else
            Assert.AreEqual(0, result)
        End If
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    <Description("Fully incorrect")>
    Public Sub Polytomous_ScoreWith_A200_B800_Score0()
        Dim solution = toSolution(INT_Polytomous_43Or131)
        Dim r = GetResponse(New List(Of Integer) From {200, 800})

        Write("Response", "Arrange", r)

        Dim result = solution.ScoreSolution(r)

        Assert.AreEqual(0, result)
    End Sub


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
