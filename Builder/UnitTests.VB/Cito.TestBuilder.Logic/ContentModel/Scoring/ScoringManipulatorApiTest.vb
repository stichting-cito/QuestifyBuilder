
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel
Imports System.Linq
Imports System.Xml.Linq
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class ScoringManipulatorApiTest

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub GetKeyStatusTest_NoSolution_GetKeyStatus_ExpectsABC()
        'Arrange
        Dim param = New IntegerScoringParameter() With {.Value = New ParameterSetCollection()}
        param.Value.Add(New ParameterCollection() With {.Id = "A"})
        param.Value.Add(New ParameterCollection() With {.Id = "B"})
        param.Value.Add(New ParameterCollection() With {.Id = "C"})

        Dim manipulator = param.GetScoreManipulator(New Solution())
        
        'Act
        Dim result = manipulator.GetKeyStatus()
        
        'Assert 
        Assert.IsTrue(result.ContainsKey("A"))
        Assert.IsTrue(result.ContainsKey("B"))
        Assert.IsTrue(result.ContainsKey("C"))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub GetKeyStatusTest_NoSolution_ExpectsABC()
        'Arrange
        Dim param = New IntegerScoringParameter() With {.Value = New ParameterSetCollection()}
        param.Value.Add(New ParameterCollection() With {.Id = "A"})
        param.Value.Add(New ParameterCollection() With {.Id = "B"})
        param.Value.Add(New ParameterCollection() With {.Id = "C"})

        Dim manipulator = param.GetScoreManipulator(New Solution())
        
        'Act
        Dim result = manipulator.GetKeysAlreadyManipulated().ToList()
        
        'Assert 
        Assert.AreEqual(0, result.Count, "No solution thus no results here.")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub GetManipulatableKeysTest_NoSolution_ExpectsABC()
        'Arrange
        Dim param = New IntegerScoringParameter() With {.Value = New ParameterSetCollection()}
        param.Value.Add(New ParameterCollection() With {.Id = "A"})
        param.Value.Add(New ParameterCollection() With {.Id = "B"})
        param.Value.Add(New ParameterCollection() With {.Id = "C"})

        Dim manipulator = param.GetScoreManipulator(New Solution())
        
        'Act
        Dim result = manipulator.GetManipulatableKeys().ToList()
        
        'Assert 
        Assert.IsTrue(result.Contains("A"))
        Assert.IsTrue(result.Contains("B"))
        Assert.IsTrue(result.Contains("C"))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub CreatePreProcessingRules()
        'Arrange
        Dim solution As New Solution()
        Dim sp = New StringScoringParameter() With {.ControllerId = "ctrl"}.AddSubParameters("A")
        Dim manipulator = sp.GetScoreManipulator(solution)
        manipulator.SetKey("A", "SomeValue")
        
        'Act                                                                       
        manipulator.SetPreProcessingMethods("A", {"Full.Qualifying.Namespace.Class"})
        
        'Assert  
        Assert.IsTrue(UnitTestHelper.AreSame(referencePreProcessing.ToString(), solution.DoSerialize().ToString()))
    End Sub

#Region "Data"
    ReadOnly referencePreProcessing As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                                      <keyFindings>
                                                          <keyFinding id="ctrl" scoringMethod="None">
                                                              <keyFact id="A-ctrl" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                  <keyValue domain="ctrl" occur="1">
                                                                      <stringValue>
                                                                          <typedValue>SomeValue</typedValue>
                                                                      </stringValue>
                                                                      <preprocessingRule>
                                                                          <ruleName>Full.Qualifying.Namespace.Class</ruleName>
                                                                      </preprocessingRule>
                                                                  </keyValue>
                                                              </keyFact>
                                                          </keyFinding>
                                                      </keyFindings>
                                                      <aspectReferences/>
                                                  </solution>
#End Region

End Class
