
Imports System.Xml.Linq

''' <summary>
''' This class will validate the behavior when fact sets have distinct domain values.
'''
''' So the solution has 1 finding with a factset
''' Finding:
'''    factset 1 : 
'''             fact A= 4
'''             fact B= 8
'''    factset 2:
'''             fact A= 8
'''             fact B= 4
'''    factset 3 : 
'''             fact C= 24
'''             fact D= 28
'''    factset 4:
'''             fact C= 28
'''             fact D= 24
''' 
''' Fact sets with domain (A and B) should be handled separately from (C and D)
''' </summary>
<TestClass>
Public Class DistinctFactGroupsTests_poly
    Inherits ScoringTestBase

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub MaxScore()
        'Arrange
        Dim solution = toSolution(INT_4and8_or_8and4__24and28_or_28and24)
      
        'Act
        Dim result = solution.MaxSolutionRawScore

        'Assert
        Assert.AreEqual(2, result, "Polytomous scoring max should is dependent on facts and factsets")
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub GiveCorrectAnswer_ScoreShouldBe1()
        'Arrange
        Dim solution = toSolution(INT_4and8_or_8and4__24and28_or_28and24)
        Dim r = GetResponse(New List(Of Integer) From {8, 4, 24, 28})

        Write("Response", "Arrange", r) 'Write for debugging
       
        'Act
        Dim result = solution.ScoreSolution(r)

        'Assert
        Assert.AreEqual(2, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub GivePartialCorrectAnswerFOR_AB_NOT4_CD_ScoreShouldBe0()
        'Arrange
        Dim solution = toSolution(INT_4and8_or_8and4__24and28_or_28and24)
        Dim r = GetResponse(New List(Of Integer) From {8, 4, 2222, 3333})

        Write("Response", "Arrange", r) 'Write for debugging
       
        'Act
        Dim result = solution.ScoreSolution(r)

        'Assert
        Assert.AreEqual(1, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub GivePartialCorrectAnswerFOR_A_NOT4_BCD_ScoreShouldBe0()
        'Arrange
        Dim solution = toSolution(INT_4and8_or_8and4__24and28_or_28and24)
        Dim r = GetResponse(New List(Of Integer) From {8, 4444, 2222, 3333})

        Write("Response", "Arrange", r) 'Write for debugging
        
        'Act
        Dim result = solution.ScoreSolution(r)

        'Assert
        Assert.AreEqual(0, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub GivePartialCorrectAnswerFOR_B_NOT4_ACD_ScoreShouldBe0()
        'Arrange
        Dim solution = toSolution(INT_4and8_or_8and4__24and28_or_28and24)
        Dim r = GetResponse(New List(Of Integer) From {8888, 8, 2222, 3333})

        Write("Response", "Arrange", r) 'Write for debugging
       
        'Act
        Dim result = solution.ScoreSolution(r)

        'Assert
        Assert.AreEqual(0, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub GivePartialCorrectAnswerFOR_CD_NOT4_AB_ScoreShouldBe0()
        'Arrange
        Dim solution = toSolution(INT_4and8_or_8and4__24and28_or_28and24)
        Dim r = GetResponse(New List(Of Integer) From {8888, 4444, 28, 24})

        Write("Response", "Arrange", r) 'Write for debugging
       
        'Act
        Dim result = solution.ScoreSolution(r)

        'Assert
        Assert.AreEqual(1, result)
    End Sub
    
    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub GiveAnswerFOR_CD_NOT4_AB_ScoreShouldBe0()
        'Arrange
        Dim solution = toSolution(INT_4and8_or_8and4__24and28_or_28and24)
        Dim r = GetResponse(New List(Of Integer) From {8888, 4444, 28, 24})

        Write("Response", "Arrange", r) 'Write for debugging
      
        'Act
        Dim result = solution.ScoreSolution(r)

        'Assert
        Assert.AreEqual(1, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub GiveAnswerFOR_D_NOT4_ABC_ScoreShouldBe0()
        'Arrange
        Dim solution = toSolution(INT_4and8_or_8and4__24and28_or_28and24)
        Dim r = GetResponse(New List(Of Integer) From {8888, 4444, 8888, 24})

        Write("Response", "Arrange", r) 'Write for debugging
      
        'Act
        Dim result = solution.ScoreSolution(r)

        'Assert
        Assert.AreEqual(0, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub GiveAnswerFOR_C_NOT4_ABD_ScoreShouldBe0()
        'Arrange
        Dim solution = toSolution(INT_4and8_or_8and4__24and28_or_28and24)
        Dim r = GetResponse(New List(Of Integer) From {8888, 4444, 24, 9999})

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
    '''    factset 3 : 
    '''             fact C= 24
    '''             fact D= 28
    '''    factset 4:
    '''             fact C= 28
    '''             fact D= 24
    ''' </summary>
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
