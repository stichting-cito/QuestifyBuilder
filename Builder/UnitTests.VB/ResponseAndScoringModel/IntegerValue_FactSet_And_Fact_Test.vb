
Imports System.Xml.Linq

<TestClass>
Public Class IntegerValue_FactSet_And_Fact_Test
    Inherits ScoringTestBase

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub MaxScore()
        Dim solution = toSolution(INT_Dichotomous_8Or4_power)

        Dim result = solution.MaxSolutionRawScore

        Assert.AreEqual(1, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub GiveCorrectAnswer_ScoreShouldBe1()
        Dim solution = toSolution(INT_Dichotomous_8Or4_power)
        Dim r = GetResponse(New List(Of Integer) From {8, 4, 4096})

        Write("Response", "Arrange", r)

        Dim result = solution.ScoreSolution(r)

        Assert.AreEqual(1, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    <Description("This construct was not possible")>
    Public Sub GivePartialIncorrectAnswer_CisWrong_ScoreShouldBe0()
        Dim solution = toSolution(INT_Dichotomous_8Or4_power)
        Dim r = GetResponse(New List(Of Integer) From {8, 4, 2})

        Write("Response", "Arrange", r)

        Dim result = solution.ScoreSolution(r)

        Assert.AreEqual(0, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub GivePartialIncorrectAnswer_AisWrong_ScoreShouldBe0()
        Dim solution = toSolution(INT_Dichotomous_8Or4_power)
        Dim r = GetResponse(New List(Of Integer) From {10, 4, 65536})

        Write("Response", "Arrange", r)

        Dim result = solution.ScoreSolution(r)

        Assert.AreEqual(0, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub GivePartialIncorrectAnswer_BisWrong_ScoreShouldBe0()
        Dim solution = toSolution(INT_Dichotomous_8Or4_power)
        Dim r = GetResponse(New List(Of Integer) From {4, 10, 65536})

        Write("Response", "Arrange", r)

        Dim result = solution.ScoreSolution(r)

        Assert.AreEqual(0, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub GiveFullyIncorrectAnswer_ScoreShouldBe0()
        Dim solution = toSolution(INT_Dichotomous_8Or4_power)
        Dim r = GetResponse(New List(Of Integer) From {100, 1000, 10000})

        Write("Response", "Arrange", r)

        Dim result = solution.ScoreSolution(r)

        Assert.AreEqual(0, result)
    End Sub

    Private INT_Dichotomous_8Or4_power As XElement = <solution>
                                                         <keyFindings>
                                                             <keyFinding id="mc" scoringMethod="Dichotomous">

                                                                 <keyFactSet>
                                                                     <keyFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                         <keyValue domain="A" occur="1">
                                                                             <integerValue>
                                                                                 <typedValue>4</typedValue>
                                                                             </integerValue>
                                                                         </keyValue>
                                                                     </keyFact>
                                                                     <keyFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                         <keyValue domain="B" occur="1">
                                                                             <integerValue>
                                                                                 <typedValue>8</typedValue>
                                                                             </integerValue>
                                                                         </keyValue>
                                                                     </keyFact>
                                                                 </keyFactSet>
                                                                 <keyFactSet>
                                                                     <keyFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                         <keyValue domain="A" occur="1">
                                                                             <integerValue>
                                                                                 <typedValue>8</typedValue>
                                                                             </integerValue>
                                                                         </keyValue>
                                                                     </keyFact>
                                                                     <keyFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                         <keyValue domain="B" occur="1">
                                                                             <integerValue>
                                                                                 <typedValue>4</typedValue>
                                                                             </integerValue>
                                                                         </keyValue>
                                                                     </keyFact>
                                                                 </keyFactSet>

                                                                 <keyFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                     <keyValue domain="C" occur="1">
                                                                         <integerValue>
                                                                             <typedValue>65536</typedValue>
                                                                         </integerValue>
                                                                         <integerValue>
                                                                             <typedValue>4096</typedValue>
                                                                         </integerValue>
                                                                     </keyValue>
                                                                 </keyFact>

                                                             </keyFinding>
                                                         </keyFindings>
                                                         <aspectReferences/>
                                                     </solution>

End Class
