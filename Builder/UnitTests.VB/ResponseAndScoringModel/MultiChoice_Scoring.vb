
Imports System.Xml.Linq

<TestClass()>
Public Class MultiChoice_Scoring
    Inherits ScoringTestBase

    'This is a typical multiple choice example. 
    Private MR_A As XElement = <solution>
                                   <keyFindings>
                                       <keyFinding id="mc" scoringMethod="Dichotomous">
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
    Public Sub ScoreSolution_CorrectResponse_Expects1()
        'Arrange
        Dim solution = toSolution(MR_A)
        Dim valuesToCreate As New List(Of Tuple(Of String, String)) From {New Tuple(Of String, String)("B", "mc"), New Tuple(Of String, String)("D", "mc")}
        Dim r = GetResponse(valuesToCreate, "mc")

        Write("Response", "Arrange", r) 'Write for debugging
        
        'Act
        Dim result = solution.ScoreSolution(r)

        'Assert
        Assert.AreEqual(1, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub ScoreSolution_CorrectResponseReversed_Expects1()
        'Arrange
        Dim solution = toSolution(MR_A)
        Dim valuesToCreate As New List(Of Tuple(Of String, String)) From {New Tuple(Of String, String)("D", "mc"), New Tuple(Of String, String)("B", "mc")}
        Dim r = GetResponse(valuesToCreate, "mc")

        Write("Response", "Arrange", r) 'Write for debugging
        
        'Act
        Dim result = solution.ScoreSolution(r)

        'Assert
        Assert.AreEqual(1, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub ScoreSolution_CompletelyWrongAnswer_Expects0()
        'Arrange
        Dim solution = toSolution(MR_A)
        Dim valuesToCreate As New List(Of Tuple(Of String, String)) From {New Tuple(Of String, String)("A", "mc"), New Tuple(Of String, String)("C", "mc")}
        Dim r = GetResponse(valuesToCreate, "mc")

        Write("Response", "Arrange", r) 'Write for debugging
        
        'Act
        Dim result = solution.ScoreSolution(r)

        'Assert
        Assert.AreEqual(0, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub ScoreSolution_PartialWrongAnswer_Expects0()
        'Arrange
        Dim solution = toSolution(MR_A)
        Dim valuesToCreate As New List(Of Tuple(Of String, String)) From {New Tuple(Of String, String)("B", "mc"), New Tuple(Of String, String)("C", "mc")}
        Dim r = GetResponse(valuesToCreate, "mc")

        Write("Response", "Arrange", r) 'Write for debugging
       
        'Act
        Dim result = solution.ScoreSolution(r)

        'Assert
        Assert.AreEqual(0, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub ScoreSolution_DoubleSameResponse_Expects0()
        'Arrange
        Dim solution = toSolution(MR_A)
        Dim valuesToCreate As New List(Of Tuple(Of String, String)) From {New Tuple(Of String, String)("B", "mc"), New Tuple(Of String, String)("B", "mc")}
        Dim r = GetResponse(valuesToCreate, "mc")

        Write("Response", "Arrange", r) 'Write for debugging
      
        'Act
        Dim result = solution.ScoreSolution(r)

        'Assert
        Assert.AreEqual(0, result)
    End Sub

End Class
