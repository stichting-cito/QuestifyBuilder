
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports System.Xml.Serialization
Imports System.IO
Imports Questify.Builder.Logic.ContentModel
Imports System.Diagnostics
Imports System.Linq
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class KeyManipulatorTargetTests

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub GetTargetOnEmptySolution_ShouldBe_FindingManipulatorTarget()
        'Arrange
        Dim solution = New Solution()
        Dim controllerId = "FieldA"

        CreateIntegerScoreParam(controllerId)
        Dim m = New KeyManipulator(solution.GetFindingOrMakeIt("finding")) With {.FactIdPostFix = controllerId}
        
        'Act
        Dim result = m.GetTarget()
        
        'Assert
        Assert.IsInstanceOfType(result, GetType(FindingManipulatorTarget))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub GetTargetOnSolutionWithFactSet_ShouldBe_FactSetManipulatorTarget()
        'Arrange
        Dim solution = Deserialize(Of Solution)(WithFactSets)
        Dim controllerId = "FieldA"

        CreateIntegerScoreParam(controllerId)
        Dim m = New KeyManipulator(solution.GetFindingOrMakeIt("finding")) With {.FactIdPostFix = controllerId}
        
        'Act
        Dim result = m.GetTarget()
        
        'Assert
        Assert.IsInstanceOfType(result, GetType(FactSetManipulatorTarget(Of KeyFinding, KeyFactSet)))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub SetFactSetTargetTo0ShouldReturn3()
        'Arrange
        Dim solution = Deserialize(Of Solution)(WithFactSets)
        Dim controllerId = "FieldA"

        Dim integerScoreParameter = CreateIntegerScoreParam(controllerId)
        Dim m = integerScoreParameter.GetScoreManipulator(solution)
        m.SetFactSetTarget(0)
        
        'Act
        Dim result = m.GetKeyStatus()
        
        'Assert
        Assert.AreEqual(result.Count, 1)
        Assert.AreEqual(result("A").First().Value.IntegerValue, 3)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    <ExpectedException(GetType(IndexOutOfRangeException))>
    Public Sub SetFactSetTargetToNonExistingFactSetShouldThrowException()
        'Arrange
        Dim solution = Deserialize(Of Solution)(WithFactSets)
        Dim controllerId = "FieldA"

        Dim integerScoreParameter = CreateIntegerScoreParam(controllerId)
        Dim m = integerScoreParameter.GetScoreManipulator(solution)
        
        'Act
        m.SetFactSetTarget(999)

        'Assert
        Assert.Fail()
    End Sub
    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub SetFactSetTargetTo1ShouldReturn77()
        'Arrange
        Dim solution = Deserialize(Of Solution)(WithFactSets)
        Dim controllerId = "FieldA"

        Dim integerScoreParameter = CreateIntegerScoreParam(controllerId)
        Dim m = integerScoreParameter.GetScoreManipulator(solution)
        
        'Act
        m.SetFactSetTarget(1)
        Dim result = m.GetKeyStatus()
        
        'Assert
        Assert.AreEqual(result.Count, 1)
        Assert.AreEqual(result("A").First().Value.IntegerValue, 77)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub GetValueFromFirstFact()
        'Arrange
        Dim solution = Deserialize(Of Solution)(WithFactSets)
        Dim controllerId = "FieldA"

        Dim integerScoreParameter = CreateIntegerScoreParam(controllerId)
        Dim m = New KeyManipulator(solution.GetFindingOrMakeIt("finding")) With {.FactIdPostFix = controllerId}
        m.GetTarget()
        
        'Act
        Dim result = integerScoreParameter.GetScoreManipulator(solution).GetKeyStatus()
        
        'Assert
        WriteSolution("assert", solution)
        Assert.AreEqual(result.Count, 1)
        Assert.AreEqual(result("A").First().Value.IntegerValue, 3)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub ClearsTargetFirstFactSet_GetMovesToFactSet()
        'Arrange
        Dim solution = Deserialize(Of Solution)(WithFactSets)
        WriteSolution("arrange 1", solution)
        Dim controllerId = "FieldA"

        Dim integerScoreParameter = CreateIntegerScoreParam(controllerId)
        Dim m = New KeyManipulator(solution.GetFindingOrMakeIt("finding")) With {.FactIdPostFix = controllerId}
        Dim target = m.GetTarget()

        target.Clear()
        WriteSolution("arrange 2", solution)

        'Act
        Dim result = integerScoreParameter.GetScoreManipulator(solution).GetKeyStatus() 'Re-determines target.

        'Assert
        WriteSolution("assert", solution)
        Assert.AreEqual(result.Count, 1)
        Assert.AreEqual(result("A").First().Value.IntegerValue, 77)
    End Sub


    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub GettingIDS_FactsetTarget_IsInitializedToFirstFactSet_Expects0()
        'Arrange
        Dim solution = Deserialize(Of Solution)(WithFactSets)
        WriteSolution("arrange 1", solution)
        Dim controllerId = "FieldA"

        CreateIntegerScoreParam(controllerId)
        Dim manipulator = New KeyManipulator(solution.GetFindingOrMakeIt("finding")) With {.FactIdPostFix = controllerId}
        Dim oldValue = manipulator.FactsetTarget
        
        'Act
        Dim target = manipulator.GetIds().ToList()

        'Assert
        WriteSolution("assert", solution)
        Assert.IsNull(oldValue)
        Assert.AreEqual(0, manipulator.FactsetTarget)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub GettingIDS_FactsetTarget_IsInitializedToFirstFactSet_Expects1()
        'Arrange
        Dim solution = Deserialize(Of Solution)(WithFactSets_FieldAIs_2ndSet)
        WriteSolution("arrange 1", solution)
        Dim controllerId = "FieldA"

        CreateIntegerScoreParam(controllerId)
        Dim manipulator = New KeyManipulator(solution.GetFindingOrMakeIt("finding")) With {.FactIdPostFix = controllerId}
        Dim oldValue = manipulator.FactsetTarget
        
        'Act
        manipulator.GetIds().ToList()

        'Assert
        WriteSolution("assert", solution)
        Assert.IsNull(oldValue)
        Assert.AreEqual(1, manipulator.FactsetTarget)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub SetFactSetTarget_GetScore_FactSetTarget_NotChanged()
        'Arrange
        Dim solution = Deserialize(Of Solution)(solutionWith1FactSet2Integer)

        Dim integerScoreParameter = New IntegerScoringParameter() With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1", "2", "3")
        Dim manipulator = integerScoreParameter.GetScoreManipulator(solution)

        manipulator.SetFactSetTarget(Nothing)
        
        'Act
        manipulator.GetKeyStatus()

        'Assert
        Assert.AreEqual(Nothing, manipulator.FactSetTarget)
    End Sub

#Region "Helpers"

    Private Function CreateIntegerScoreParam(controllerId As String, Optional findingName As String = "finding") As IntegerScoringParameter
        Dim fieldA As New IntegerScoringParameter With {.ControllerId = controllerId, .FindingOverride = findingName} : fieldA.Value = New ParameterSetCollection()
        Return fieldA
    End Function

    Protected Function Deserialize(Of T)(input As XElement) As T
        Dim ret As T
        Dim s = New XmlSerializer(GetType(T))

        Using m As New StringReader(input.ToString())
            ret = DirectCast(s.Deserialize(m), T)
        End Using

        Return ret
    End Function

    Sub WriteSolution(stateName As String, s As Solution)
        Dim a As New XmlSerializer(GetType(Solution))
        Debug.WriteLine(String.Empty)
        Debug.WriteLine(String.Format("WriteSolution for State [{0}]", stateName))
        Using stream = New StringWriter()
            a.Serialize(stream, s)

            Debug.WriteLine(stream.ToString())
        End Using
    End Sub

#End Region

#Region "Data"
    Dim WithFactSets As XElement = <solution>
                                       <keyFindings>
                                           <keyFinding id="finding">
                                               <keyFactSet>
                                                   <keyFact id="A-FieldA" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                       <keyValue domain="FieldA" occur="1">
                                                           <integerValue>
                                                               <typedValue>3</typedValue>
                                                           </integerValue>
                                                       </keyValue>
                                                   </keyFact>
                                                   <keyFact id="A-FieldB" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                       <keyValue domain="FieldB" occur="1">
                                                           <integerValue>
                                                               <typedValue>7</typedValue>
                                                           </integerValue>
                                                       </keyValue>
                                                   </keyFact>
                                               </keyFactSet>
                                               <keyFactSet>
                                                   <keyFact id="A-FieldA" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                       <keyValue domain="FieldA" occur="1">
                                                           <integerValue>
                                                               <typedValue>77</typedValue>
                                                           </integerValue>
                                                       </keyValue>
                                                   </keyFact>
                                                   <keyFact id="A-FieldB" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                       <keyValue domain="FieldB" occur="1">
                                                           <integerValue>
                                                               <typedValue>33</typedValue>
                                                           </integerValue>
                                                       </keyValue>
                                                   </keyFact>
                                               </keyFactSet>
                                           </keyFinding>
                                       </keyFindings>
                                   </solution>

    'Mangled example. Please do not re-use
    Dim WithFactSets_FieldAIs_2ndSet As XElement = <solution>
                                                       <keyFindings>
                                                           <keyFinding id="finding">
                                                               <keyFactSet>
                                                                   <keyFact id="A-FieldB" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                       <keyValue domain="FieldB" occur="1">
                                                                           <integerValue>
                                                                               <typedValue>7</typedValue>
                                                                           </integerValue>
                                                                       </keyValue>
                                                                   </keyFact>
                                                               </keyFactSet>
                                                               <keyFactSet>
                                                                   <keyFact id="A-FieldA" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                       <keyValue domain="FieldA" occur="1">
                                                                           <integerValue>
                                                                               <typedValue>77</typedValue>
                                                                           </integerValue>
                                                                       </keyValue>
                                                                   </keyFact>
                                                                   <keyFact id="A-FieldB" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                       <keyValue domain="FieldB" occur="1">
                                                                           <integerValue>
                                                                               <typedValue>33</typedValue>
                                                                           </integerValue>
                                                                       </keyValue>
                                                                   </keyFact>
                                                               </keyFactSet>
                                                           </keyFinding>
                                                       </keyFindings>
                                                   </solution>

    ReadOnly solutionWith1FactSet2Integer As XElement = <solution>
                                                            <keyFindings>
                                                                <keyFinding id="sharedIntegerFinding" scoringMethod="Dichotomous">
                                                                    <keyFactSet>
                                                                        <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                            <keyValue domain="integerScore" occur="1">
                                                                                <integerValue>
                                                                                    <typedValue>6</typedValue>
                                                                                </integerValue>
                                                                            </keyValue>
                                                                        </keyFact>
                                                                        <keyFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                            <keyValue domain="integerScore" occur="1">
                                                                                <integerValue>
                                                                                    <typedValue>14</typedValue>
                                                                                </integerValue>
                                                                            </keyValue>
                                                                        </keyFact>
                                                                    </keyFactSet>
                                                                </keyFinding>
                                                            </keyFindings>
                                                        </solution>
#End Region

End Class
