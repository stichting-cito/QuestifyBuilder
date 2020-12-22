
Imports System.Xml.Linq

<TestClass>
Public Class DistinctFactGroupsTests_poly
    Inherits ScoringTestBase

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub MaxScore()
        Dim solution = toSolution(INT_4and8_or_8and4__24and28_or_28and24)

        Dim result = solution.MaxSolutionRawScore

        Assert.AreEqual(2, result, "Polytomous scoring max should is dependent on facts and factsets")
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub GiveCorrectAnswer_ScoreShouldBe1()
        Dim solution = toSolution(INT_4and8_or_8and4__24and28_or_28and24)
        Dim r = GetResponse(New List(Of Integer) From {8, 4, 24, 28})

        Write("Response", "Arrange", r)

        Dim result = solution.ScoreSolution(r)

        Assert.AreEqual(2, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub GivePartialCorrectAnswerFOR_AB_NOT4_CD_ScoreShouldBe0()
        Dim solution = toSolution(INT_4and8_or_8and4__24and28_or_28and24)
        Dim r = GetResponse(New List(Of Integer) From {8, 4, 2222, 3333})

        Write("Response", "Arrange", r)

        Dim result = solution.ScoreSolution(r)

        Assert.AreEqual(1, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub GivePartialCorrectAnswerFOR_A_NOT4_BCD_ScoreShouldBe0()
        Dim solution = toSolution(INT_4and8_or_8and4__24and28_or_28and24)
        Dim r = GetResponse(New List(Of Integer) From {8, 4444, 2222, 3333})

        Write("Response", "Arrange", r)

        Dim result = solution.ScoreSolution(r)

        Assert.AreEqual(0, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub GivePartialCorrectAnswerFOR_B_NOT4_ACD_ScoreShouldBe0()
        Dim solution = toSolution(INT_4and8_or_8and4__24and28_or_28and24)
        Dim r = GetResponse(New List(Of Integer) From {8888, 8, 2222, 3333})

        Write("Response", "Arrange", r)

        Dim result = solution.ScoreSolution(r)

        Assert.AreEqual(0, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub GivePartialCorrectAnswerFOR_CD_NOT4_AB_ScoreShouldBe0()
        Dim solution = toSolution(INT_4and8_or_8and4__24and28_or_28and24)
        Dim r = GetResponse(New List(Of Integer) From {8888, 4444, 28, 24})

        Write("Response", "Arrange", r)

        Dim result = solution.ScoreSolution(r)

        Assert.AreEqual(1, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub GiveAnswerFOR_CD_NOT4_AB_ScoreShouldBe0()
        Dim solution = toSolution(INT_4and8_or_8and4__24and28_or_28and24)
        Dim r = GetResponse(New List(Of Integer) From {8888, 4444, 28, 24})

        Write("Response", "Arrange", r)

        Dim result = solution.ScoreSolution(r)

        Assert.AreEqual(1, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub GiveAnswerFOR_D_NOT4_ABC_ScoreShouldBe0()
        Dim solution = toSolution(INT_4and8_or_8and4__24and28_or_28and24)
        Dim r = GetResponse(New List(Of Integer) From {8888, 4444, 8888, 24})

        Write("Response", "Arrange", r)

        Dim result = solution.ScoreSolution(r)

        Assert.AreEqual(0, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub GiveAnswerFOR_C_NOT4_ABD_ScoreShouldBe0()
        Dim solution = toSolution(INT_4and8_or_8and4__24and28_or_28and24)
        Dim r = GetResponse(New List(Of Integer) From {8888, 4444, 24, 9999})

        Write("Response", "Arrange", r)

        Dim result = solution.ScoreSolution(r)

        Assert.AreEqual(0, result)
    End Sub

    Private INT_4and8_or_8and4__24and28_or_28and24 As XElement = <solution>
                                                                     <keyFindings>
                                                                         <keyFinding id="mc" scoringMethod="Polytomous">

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

                                                                             <keyFactSet>
                                                                                 <keyFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                     <keyValue domain="C" occur="1">
                                                                                         <integerValue>
                                                                                             <typedValue>24</typedValue>
                                                                                         </integerValue>
                                                                                     </keyValue>
                                                                                 </keyFact>
                                                                                 <keyFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                     <keyValue domain="D" occur="1">
                                                                                         <integerValue>
                                                                                             <typedValue>28</typedValue>
                                                                                         </integerValue>
                                                                                     </keyValue>
                                                                                 </keyFact>
                                                                             </keyFactSet>
                                                                             <keyFactSet>
                                                                                 <keyFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                     <keyValue domain="C" occur="1">
                                                                                         <integerValue>
                                                                                             <typedValue>28</typedValue>
                                                                                         </integerValue>
                                                                                     </keyValue>
                                                                                 </keyFact>
                                                                                 <keyFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                     <keyValue domain="D" occur="1">
                                                                                         <integerValue>
                                                                                             <typedValue>24</typedValue>
                                                                                         </integerValue>
                                                                                     </keyValue>
                                                                                 </keyFact>
                                                                             </keyFactSet>

                                                                         </keyFinding>
                                                                     </keyFindings>
                                                                     <aspectReferences/>
                                                                 </solution>
End Class
