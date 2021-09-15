
Imports System.Xml.Linq
Imports System.Activities
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel.workers.Flow
Imports Questify.Builder.UnitTests.Framework

<TestClass()>
Public Class RemoveEmptyFactsTests

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub RemoveKeyFromAnEmptyFinding_StillEmpty()
        'Arrange
        Dim solution = _testSolution.To(Of Solution)()
        Dim inputs As New Dictionary(Of String, Object) From {{"Facts", solution.GetFindingOrMakeIt("Empty").Facts}}
        
        'Act
        WorkflowInvoker.Invoke(New RemoveEmptyFacts(), inputs)

        'Assert
        solution.WriteToDebug("Assert")
        Assert.AreEqual(0, solution.GetFindingOrMakeIt("Empty").Facts.Count)
        Assert.AreEqual(1, solution.GetFindingOrMakeIt("Has1EmptyFact").Facts.Count)
        Assert.AreEqual(3, solution.GetFindingOrMakeIt("Has2EmptyFactAnd1NotSoEmpty").Facts.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub RemoveKeyFromAFindingThatHas1EmptyFact_IsEmpty()
        'Arrange
        Dim solution = _testSolution.To(Of Solution)()
        Dim inputs As New Dictionary(Of String, Object) From {{"Facts", solution.GetFindingOrMakeIt("Has1EmptyFact").Facts}}
        
        'Act
        WorkflowInvoker.Invoke(New RemoveEmptyFacts(), inputs)

        'Assert
        solution.WriteToDebug("Assert")
        Assert.AreEqual(0, solution.GetFindingOrMakeIt("Empty").Facts.Count)
        Assert.AreEqual(0, solution.GetFindingOrMakeIt("Has1EmptyFact").Facts.Count)
        Assert.AreEqual(3, solution.GetFindingOrMakeIt("Has2EmptyFactAnd1NotSoEmpty").Facts.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub RemoveKeyFromAFindingThatHas2EmptyFactAnd1NotSoEmpty_2FactsRemoved1FactRemains()
        'Arrange
        Dim solution = _testSolution.To(Of Solution)()
        Dim inputs As New Dictionary(Of String, Object) From {{"Facts", solution.GetFindingOrMakeIt("Has2EmptyFactAnd1NotSoEmpty").Facts}}
        
        'Act
        WorkflowInvoker.Invoke(New RemoveEmptyFacts(), inputs)

        'Assert
        solution.WriteToDebug("Assert")
        Assert.AreEqual(0, solution.GetFindingOrMakeIt("Empty").Facts.Count)
        Assert.AreEqual(1, solution.GetFindingOrMakeIt("Has1EmptyFact").Facts.Count)
        Assert.AreEqual(1, solution.GetFindingOrMakeIt("Has2EmptyFactAnd1NotSoEmpty").Facts.Count)
    End Sub

#Region "Data"
    ReadOnly _testSolution As XElement = <solution>
                                             <keyFindings>
                                                 <keyFinding id="Empty" scoringMethod="Dichotomous"/>

                                                 <keyFinding id="Has1EmptyFact" scoringMethod="Dichotomous">
                                                     <keyFact id="Empty" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     </keyFact>
                                                 </keyFinding>

                                                 <keyFinding id="Has2EmptyFactAnd1NotSoEmpty" scoringMethod="Dichotomous">
                                                     <keyFact id="Empty1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     </keyFact>
                                                     <keyFact id="NotEmpty" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                         <keyValue/>
                                                     </keyFact>
                                                     <keyFact id="Empty2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     </keyFact>
                                                 </keyFinding>

                                             </keyFindings>
                                         </solution>
#End Region

End Class
