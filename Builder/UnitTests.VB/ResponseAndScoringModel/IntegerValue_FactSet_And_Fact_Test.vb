
Imports System.Xml.Linq

''' <summary>
''' This class will validate the behavior when factsets and facts are mixed:
''' 
''' Question: Given [1 2 3 5 6 7 9] what numbers are missing from [1..9]? And what is the outcome if you take the one to the exponent of the other (i.e. x^y)
''' Answer: ((4 and 8) or (8 and 4)) and (65536 or 4096)
''' 
''' So the solution has 1 finding with a factset
''' Finding:
'''    factset 1 : 
'''             fact 4
'''             fact 8
'''    factset 2:
'''             fact 8
'''             fact 4
'''    fact 65536 or 4096
''' </summary>
<TestClass>
Public Class IntegerValue_FactSet_And_Fact_Test
    Inherits ScoringTestBase

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub MaxScore()
        'Arrange
        Dim solution = toSolution(INT_Dichotomous_8Or4_power)
        
        'Act
        Dim result = solution.MaxSolutionRawScore

        'Assert
        Assert.AreEqual(1, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub GiveCorrectAnswer_ScoreShouldBe1()
        'Arrange
        Dim solution = toSolution(INT_Dichotomous_8Or4_power)
        Dim r = GetResponse(New List(Of Integer) From {8, 4, 4096})
        
        Write("Response", "Arrange", r) 'Write for debugging
       
        'Act
        Dim result = solution.ScoreSolution(r)

        'Assert
        Assert.AreEqual(1, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    <Description("This construct was not possible")>
    Public Sub GivePartialIncorrectAnswer_CisWrong_ScoreShouldBe0()
        'Arrange
        Dim solution = toSolution(INT_Dichotomous_8Or4_power)
        Dim r = GetResponse(New List(Of Integer) From {8, 4, 2})
        
        Write("Response", "Arrange", r) 'Write for debugging
       
        'Act
        Dim result = solution.ScoreSolution(r)

        'Assert
        Assert.AreEqual(0, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub GivePartialIncorrectAnswer_AisWrong_ScoreShouldBe0()
        'Arrange
        Dim solution = toSolution(INT_Dichotomous_8Or4_power)
        Dim r = GetResponse(New List(Of Integer) From {10, 4, 65536})

        Write("Response", "Arrange", r) 'Write for debugging
       
        'Act
        Dim result = solution.ScoreSolution(r)

        'Assert
        Assert.AreEqual(0, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub GivePartialIncorrectAnswer_BisWrong_ScoreShouldBe0()
        'Arrange
        Dim solution = toSolution(INT_Dichotomous_8Or4_power)
        Dim r = GetResponse(New List(Of Integer) From {4, 10, 65536})

        Write("Response", "Arrange", r) 'Write for debugging
    
        'Act
        Dim result = solution.ScoreSolution(r)

        'Assert
        Assert.AreEqual(0, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub GiveFullyIncorrectAnswer_ScoreShouldBe0()
        'Arrange
        Dim solution = toSolution(INT_Dichotomous_8Or4_power)
        Dim r = GetResponse(New List(Of Integer) From {100, 1000, 10000})

        Write("Response", "Arrange", r) 'Write for debugging
      
        'Act
        Dim result = solution.ScoreSolution(r)

        'Assert
        Assert.AreEqual(0, result)
    End Sub

    ''' <summary>    
    ''' Finding:
    '''    factset 1 : 
    '''             fact A= 4
    '''             fact B= 8
    '''    factset 2:
    '''             fact A= 8
    '''             fact B= 4
    '''    fact C = 65536 or 4096
    ''' </summary>
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
