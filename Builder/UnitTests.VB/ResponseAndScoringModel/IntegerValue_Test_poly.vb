
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel

''' <summary>
''' Test behavior of two facts (NOT IN A SET)
''' 
''' A needs to be 42
''' B needs to be 131
''' </summary>
<TestClass>
Public Class IntegerValue_Test_poly
    Inherits ScoringTestBase

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub GiveCorrectAnswer_experts_2()
        'Arrange
        Dim solution = ToSolution(INT_Dichotomous_42_AND_131)
        Dim r = GetResponse(New List(Of Integer) From {42, 131})

        Write("Response", "Arrange", r) 'Write for debugging
       
        'Act
        Dim result = solution.ScoreSolution(r)

        'Assert
        Assert.AreEqual(2, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub Answer_ACorrect_NotB_experts_1()
        'Arrange
        Dim solution = ToSolution(INT_Dichotomous_42_AND_131)
        Dim r = GetResponse(New List(Of Integer) From {42, 99999})

        Write("Response", "Arrange", r) 'Write for debugging
       
        'Act
        Dim result = solution.ScoreSolution(r)

        'Assert
        Assert.AreEqual(1, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub Answer_BCorrect_NotA_experts_1()
        'Arrange
        Dim solution = ToSolution(INT_Dichotomous_42_AND_131)
        Dim r = GetResponse(New List(Of Integer) From {999999, 131})

        Write("Response", "Arrange", r) 'Write for debugging
      
        'Act
        Dim result = solution.ScoreSolution(r)

        'Assert
        Assert.AreEqual(1, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub Answer_MissingValue_A_experts_1()
        'Arrange
        Dim solution = ToSolution(INT_Dichotomous_42_AND_131)
        Dim valuesToCreate As New List(Of Tuple(Of Integer, String)) From {New Tuple(Of Integer, String)(131, "B")}
        Dim r = GetResponse(valuesToCreate, "mc")

        Write("Response", "Arrange", r) 'Write for debugging
      
        'Act
        Dim result = solution.ScoreSolution(r)

        'Assert
        Assert.AreEqual(1, result)
    End Sub


    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub Answer_MissingValue_B_experts_1()
        'Arrange
        Dim solution = ToSolution(INT_Dichotomous_42_AND_131)
        Dim r = GetResponse(New List(Of Integer) From {42})

        Write("Response", "Arrange", r) 'Write for debugging
     
        'Act
        Dim result = solution.ScoreSolution(r)

        'Assert
        Assert.AreEqual(1, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub Answer_NoRespose_0()
        'Arrange
        Dim solution = ToSolution(INT_Dichotomous_42_AND_131)
        Dim r As New Response()
        Dim rF As New ResponseFinding(id:="mc") 'Note that the id DOES NOT match the keyfinding id.
        Dim respFact As New ResponseFact()

        'No Response!
        rF.Facts.Add(respFact) : r.Findings.Add(rF)

        Write("Response", "Arrange", r) 'Write for debugging
       
        'Act
        Dim result = solution.ScoreSolution(r)

        'Assert
        Assert.AreEqual(0, result)
    End Sub
    
    Private INT_Dichotomous_42_AND_131 As XElement = <solution>
                                                         <keyFindings>
                                                             <keyFinding id="mc" scoringMethod="Polytomous">

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

                                                             </keyFinding>
                                                         </keyFindings>
                                                         <aspectReferences/>
                                                     </solution>

End Class
