
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel

<TestClass()>
Public Class SingleChoice_Scoring
    Inherits ScoringTestBase

    'This is a typical multiple choice example. 
    Private SC_A As XElement = <solution>
                                   <keyFindings>
                                       <keyFinding id="mc" scoringMethod="Dichotomous">
                                           <keyFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                               <keyValue domain="mc" occur="1">
                                                   <stringValue>
                                                       <typedValue>B</typedValue>
                                                   </stringValue>
                                               </keyValue>
                                           </keyFact>
                                       </keyFinding>
                                   </keyFindings>
                               </solution>

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub MaxScore()
        'Arrange
        Dim solution = ToSolution(SC_A)
       
        'Act
        Dim result = solution.MaxSolutionRawScore

        'Assert
        Assert.AreEqual(1, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    <ExpectedException(GetType(ArgumentException))>
    Public Sub ScoreSolution_ResponseFindingDoesNotMatch_WithStringValue_ExpectException()
        'Arrange
        Dim solution = ToSolution(SC_A)

        Dim r As New Response()
        Dim rF As New ResponseFinding(id:="findingId") 'Note that the id DOES NOT match the keyfinding id.
        Dim respFact As New ResponseFact()
        r.Findings.Add(rF)
        rF.Facts.Add(respFact)
        respFact.Values.Add(CreateResponseValueNoDomain("B"))

        Write("Response", "Arrange", r) 'Write for debugging
       
        'Act
        Dim result = solution.ScoreSolution(r)

        'Assert
        'Expects Exception (ArgumentException("response and solution do not match"))
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub ScoreSolution_ResponseFindingDoesMatch_DomainDoesNotMatch_WithStringValue_Expects0()
        'Arrange
        Dim solution = ToSolution(SC_A)
        Dim r As New Response()
        Dim rF As New ResponseFinding(id:="mc") 'Note that the id DOES NOT match the keyfinding id.
        Dim respFact As New ResponseFact()
        respFact.Values.Add(CreateResponseValueNoDomain("B"))
        rF.Facts.Add(respFact) : r.Findings.Add(rF)

        Write("Response", "Arrange", r) 'Write for debugging
       
        'Act
        Dim result = solution.ScoreSolution(r)

        'Assert
        Assert.AreEqual(0, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub ScoreSolution_ResponseFindingDoesMatch_DomainNotDoesMatch_WithStringValue_Expects0()
        'Arrange
        Dim solution = ToSolution(SC_A)
        Dim r As New Response()
        Dim rF As New ResponseFinding(id:="mc") 'Note that the id DOES NOT match the keyfinding id.
        Dim respFact As New ResponseFact()
        respFact.Values.Add(CreateResponseValueNoDomain("B"))
        rF.Facts.Add(respFact) : r.Findings.Add(rF)


        Write("Response", "Arrange", r) 'Write for debugging
        'Act
        Dim result = solution.ScoreSolution(r)

        'Assert
        Assert.AreEqual(0, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub ScoreSolution_ResponseFindingDoesMatch_DomainDoesMatchButIdOfResponseFactDoes_WithStringValue_Expects1()
        'Arrange
        Dim solution = ToSolution(SC_A)
        Dim valuesToCreate As New List(Of Tuple(Of String, String)) From {New Tuple(Of String, String)("B", "mc")}
        Dim r = GetResponse(valuesToCreate, "mc")

        Write("Response", "Arrange", r) 'Write for debugging
       
        'Act
        Dim result = solution.ScoreSolution(r)

        'Assert
        Assert.AreEqual(1, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub ScoreSolution_DoubleCorrectResponse_Expects1()
        'Arrange
        Dim solution = ToSolution(SC_A)
        Dim valuesToCreate As New List(Of Tuple(Of String, String)) From {New Tuple(Of String, String)("B", "mc"), New Tuple(Of String, String)("B", "mc")}
        Dim r = GetResponse(valuesToCreate, "mc")

        Write("Response", "Arrange", r) 'Write for debugging
        
        'Act
        Dim result = solution.ScoreSolution(r)

        'Assert
        Assert.AreEqual(1, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub ScoreSolution_DoubleResponse_Wrong_Correct__Expects1()
        'Arrange
        Dim solution = ToSolution(SC_A)
        Dim valuesToCreate As New List(Of Tuple(Of String, String)) From {New Tuple(Of String, String)("A", "mc"), New Tuple(Of String, String)("B", "mc")}
        Dim r = GetResponse(valuesToCreate, "mc")

        Write("Response", "Arrange", r) 'Write for debugging
        
        'Act
        Dim result = solution.ScoreSolution(r)

        'Assert
        Assert.AreEqual(1, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub ScoreSolution_DoubleResponse_Correct_Wrong__Expects1()
        'Arrange
        Dim solution = ToSolution(SC_A)
        Dim valuesToCreate As New List(Of Tuple(Of String, String)) From {New Tuple(Of String, String)("B", "mc"), New Tuple(Of String, String)("A", "mc")}
        Dim r = GetResponse(valuesToCreate, "mc")

        Write("Response", "Arrange", r) 'Write for debugging
       
        'Act
        Dim result = solution.ScoreSolution(r)

        'Assert
        Assert.AreEqual(1, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub ScoreSolution_ResponseFormatCorrect_WrongAnswer_Expects0()
        'Arrange
        Dim solution = ToSolution(SC_A)
        Dim valuesToCreate As New List(Of Tuple(Of String, String)) From {New Tuple(Of String, String)("A", "mc")}
        Dim r = GetResponse(valuesToCreate, "mc")

        Write("Response", "Arrange", r) 'Write for debugging
      
        'Act
        Dim result = solution.ScoreSolution(r)

        'Assert
        Assert.AreEqual(0, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    <ExpectedException(GetType(NotSupportedException))>
    Public Sub ScoreSolution_ResponseWrongType_ExpectsException()
        'Arrange
        Dim solution = ToSolution(SC_A)
        Dim valuesToCreate As New List(Of Tuple(Of Integer, String)) From {New Tuple(Of Integer, String)(1, "mc")}
        Dim r = GetResponse(valuesToCreate, "mc")

        Write("Response", "Arrange", r) 'Write for debugging
      
        'Act
        Dim result = solution.ScoreSolution(r)

        'Assert
        Assert.AreEqual(0, result)
    End Sub

End Class
