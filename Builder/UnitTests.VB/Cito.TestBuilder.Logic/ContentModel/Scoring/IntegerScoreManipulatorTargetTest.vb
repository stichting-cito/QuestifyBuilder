
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports System.Xml.Serialization
Imports System.IO
Imports Questify.Builder.Logic.ContentModel
Imports System.Linq
Imports Questify.Builder.Logic.ContentModel.Scoring

<TestClass>
Public Class IntegerScoreManipulatorTargetTest

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub ScoreManipulator_CallGetID_WithEmptySolution_ShouldNotCreateFinding_NoTargetEvaluation()
        'Arrange
        Dim solution As New Solution
        Dim controllerId = "FieldA"
        Dim scorePrm = CreateIntegerScoreParam(controllerId)
        Dim manipulator = DirectCast(scorePrm.GetScoreManipulator(solution), MultiTypeScoringManipulator) 'Cast back to object. 
        
        'Act
        manipulator.GetKeysAlreadyManipulated().ToList()
        Dim findingManipulator = DirectCast(manipulator.Manipulator, FindingManipulatorBase)
        
        'Assert
        Assert.IsNull(findingManipulator.Target)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub ScoreManipulator_CallGetID_WithOnlyCorrectFinding_DoesTargetEvaluation()
        'Arrange
        Dim solution As New Solution : solution.Findings.Add(New KeyFinding() With {.Id = "finding"})
        Dim controllerId = "FieldA"
        Dim scorePrm = CreateIntegerScoreParam(controllerId)
        Dim manipulator = DirectCast(scorePrm.GetScoreManipulator(solution), MultiTypeScoringManipulator) 'Cast back to object. 
        
        'Act
        manipulator.GetKeysAlreadyManipulated().ToList()
        Dim findingManipulator = DirectCast(manipulator.Manipulator, FindingManipulatorBase)
        
        'Assert
        Assert.IsInstanceOfType(findingManipulator.Target, GetType(FindingManipulatorTarget))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub ScoreManipulator_CallGetID_WithSolutionsInFactSets_TargetIsFactSetManipulator()
        'Arrange
        Dim solution = Deserialize(Of Solution)(WithFactSets)
        Dim controllerId = "FieldA"
        Dim scorePrm = CreateIntegerScoreParam(controllerId)
        Dim manipulator = DirectCast(scorePrm.GetScoreManipulator(solution), MultiTypeScoringManipulator) 'Cast back to object. 
        
        'Act
        manipulator.GetKeysAlreadyManipulated().ToList()
        Dim findingManipulator = DirectCast(manipulator.Manipulator, FindingManipulatorBase)
        
        'Assert
        Assert.IsInstanceOfType(findingManipulator.Target, GetType(FactSetManipulatorTarget(Of KeyFinding, KeyFactSet)))
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
                                                               <typedValue>7</typedValue>
                                                           </integerValue>
                                                       </keyValue>
                                                   </keyFact>
                                                   <keyFact id="A-FieldB" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                       <keyValue domain="FieldB" occur="1">
                                                           <integerValue>
                                                               <typedValue>3</typedValue>
                                                           </integerValue>
                                                       </keyValue>
                                                   </keyFact>
                                               </keyFactSet>
                                           </keyFinding>
                                       </keyFindings>
                                   </solution>
#End Region

End Class
