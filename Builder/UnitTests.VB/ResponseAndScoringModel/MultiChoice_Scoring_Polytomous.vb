
Imports System.Xml.Linq

<TestClass()>
Public Class MultiChoice_Scoring_Polytomous
    Inherits ScoringTestBase

    Private MR_B_and_D As XElement = <solution>
                                         <keyFindings>
                                             <keyFinding id="mc" scoringMethod="Polytomous">
                                                 <keyFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="mc" occur="1">
                                                         <stringValue>
                                                             <typedValue>B</typedValue>
                                                         </stringValue>
                                                     </keyValue>
                                                 </keyFact>
                                                 <keyFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="mc" occur="1">
                                                         <stringValue>
                                                             <typedValue>D</typedValue>
                                                         </stringValue>
                                                     </keyValue>
                                                 </keyFact>
                                             </keyFinding>
                                         </keyFindings>
                                         <aspectReferences/>
                                     </solution>

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub MaxScore()
        Dim solution = ToSolution(MR_B_and_D)

        Dim result = solution.MaxSolutionRawScore

        Assert.AreEqual(2, result)
    End Sub


    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub ScoreSolution_CorrectResponse_Expects2()
        Dim solution = ToSolution(MR_B_and_D)
        Dim valuesToCreate As New List(Of Tuple(Of String, String)) From {New Tuple(Of String, String)("B", "mc"), New Tuple(Of String, String)("D", "mc")}
        Dim r = GetResponse(valuesToCreate, "mc")

        Write("Response", "Arrange", r)

        Dim result = solution.ScoreSolution(r)

        Assert.AreEqual(2, result)
    End Sub


    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub ScoreSolution_CorrectResponseReversed_Expects2()
        Dim solution = ToSolution(MR_B_and_D)
        Dim valuesToCreate As New List(Of Tuple(Of String, String)) From {New Tuple(Of String, String)("D", "mc"), New Tuple(Of String, String)("B", "mc")}
        Dim r = GetResponse(valuesToCreate, "mc")

        Write("Response", "Arrange", r)

        Dim result = solution.ScoreSolution(r)

        Assert.AreEqual(2, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub ScoreSolution_CompletelyWrongAnswer_Expects0()
        Dim solution = ToSolution(MR_B_and_D)
        Dim valuesToCreate As New List(Of Tuple(Of String, String)) From {New Tuple(Of String, String)("A", "mc"), New Tuple(Of String, String)("C", "mc")}
        Dim r = GetResponse(valuesToCreate, "mc")

        Write("Response", "Arrange", r)

        Dim result = solution.ScoreSolution(r)

        Assert.AreEqual(0, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub ScoreSolution_PartialWrongAnswer_Expects1()
        Dim solution = ToSolution(MR_B_and_D)
        Dim valuesToCreate As New List(Of Tuple(Of String, String)) From {New Tuple(Of String, String)("B", "mc"), New Tuple(Of String, String)("C", "mc")}
        Dim r = GetResponse(valuesToCreate, "mc")

        Write("Response", "Arrange", r)

        Dim result = solution.ScoreSolution(r)

        Assert.AreEqual(1, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub ScoreSolution_DoubleSameResponse_Expects1()
        Dim solution = ToSolution(MR_B_and_D)
        Dim valuesToCreate As New List(Of Tuple(Of String, String)) From {New Tuple(Of String, String)("B", "mc"), New Tuple(Of String, String)("B", "mc")}
        Dim r = GetResponse(valuesToCreate, "mc")

        Write("Response", "Arrange", r)

        Dim result = solution.ScoreSolution(r)

        Assert.AreEqual(1, result)
    End Sub


    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub ScoreSolution_MissingValue_Expects1()
        Dim solution = ToSolution(MR_B_and_D)
        Dim valuesToCreate As New List(Of Tuple(Of String, String)) From {New Tuple(Of String, String)("B", "mc")}
        Dim r = GetResponse(valuesToCreate, "mc")

        Write("Response", "Arrange", r)

        Dim result = solution.ScoreSolution(r)

        Assert.AreEqual(1, result)
    End Sub

End Class
