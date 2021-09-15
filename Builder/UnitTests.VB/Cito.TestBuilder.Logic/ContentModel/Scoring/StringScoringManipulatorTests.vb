
Imports System.Linq
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports Cito.Tester.ContentModel
Imports System.Xml.Linq
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class StringScoringManipulatorTests

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub FindKeysInSolution()
        'Arrange
        Dim param = New StringScoringParameter() With {.ControllerId = "Gap"}
        param.Value = New ParameterSetCollection
        param.Value.Add(New ParameterCollection() With {.Id = "A"})

        'Act
        Dim key = StringKeyFinding(CreateKeyValuePair("A", "ABC"))
        Dim manipulator As IGapScoringManipulator(Of String) = New StringScoringManipulator(New KeyManipulator(key), param)

        'Assert
        Dim res = manipulator.GetKeyStatus()
        Assert.IsTrue(res.ContainsKey("A"))
        Assert.AreEqual(1, res("A").Count())
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub SetKey_A_ShouldContainA()
        'Arrange
        Dim param = New StringScoringParameter() With {.ControllerId = "Gap"}
        param.Value = New ParameterSetCollection
        param.Value.Add(New ParameterCollection() With {.Id = "A"})

        'Act
        Dim key = StringKeyFinding(CreateKeyValuePair("A", "ABC"))
        Dim manipulator As IGapScoringManipulator(Of String) = New StringScoringManipulator(New KeyManipulator(key), param)
        
        'Assert
        Dim res = manipulator.GetKeyStatus()
        Assert.IsTrue(res.ContainsKey("A"))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub SetKeyWithInterface_A_ShouldContainA()
        'Arrange
        Dim param = New StringScoringParameter() With {.ControllerId = "Gap"}
        param.Value = New ParameterSetCollection
        param.Value.Add(New ParameterCollection() With {.Id = "A"})

        Dim key = StringKeyFinding()
        Dim manipulator As IGapScoringManipulator(Of String) = New StringScoringManipulator(New KeyManipulator(key), param)
        
        'Act
        manipulator.SetKey("A", "Abc")
        
        'Assert
        Dim res = manipulator.GetKeyStatus()
        Assert.IsTrue(res.ContainsKey("A"))
    End Sub


    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub SetKey_A_ShouldContainA_AndValue()
        'Arrange
        Dim param = New StringScoringParameter() With {.ControllerId = "Gap"}
        param.Value = New ParameterSetCollection
        param.Value.Add(New ParameterCollection() With {.Id = "A"})

        Dim key = StringKeyFinding()
        Dim manipulator As IGapScoringManipulator(Of String) = New StringScoringManipulator(New KeyManipulator(key), param)
        
        'Act
        manipulator.SetKey("A", "Abc")
        
        'Assert
        Dim res = manipulator.GetKeyStatus()
        Assert.IsTrue(res.ContainsKey("A"))
        Assert.AreEqual(1, res("A").Count())
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub SetKey_A_ShouldContainA_AndValues()
        'Arrange
        Dim param = New StringScoringParameter() With {.ControllerId = "Gap"}
        param.Value = New ParameterSetCollection
        param.Value.Add(New ParameterCollection() With {.Id = "A"})

        Dim key = StringKeyFinding()
        Dim manipulator As IGapScoringManipulator(Of String) = New StringScoringManipulator(New KeyManipulator(key), param)
        
        'Act
        manipulator.SetKey("A", "Abc", "123")
        
        'Assert
        Dim res = manipulator.GetKeyStatus()
        Assert.IsTrue(res.ContainsKey("A"))
        Assert.AreEqual(2, res("A").Count())
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub TakeKeysFromParameter()
        'Arrange
        Dim param = New StringScoringParameter() With {.ControllerId = "Gap"}
        param.Value = New ParameterSetCollection
        param.Value.Add(New ParameterCollection() With {.Id = "A"})
        param.Value.Add(New ParameterCollection() With {.Id = "C"})
        param.Value.Add(New ParameterCollection() With {.Id = "D"})
        param.Value.Add(New ParameterCollection() With {.Id = "G"})

        Dim key = StringKeyFinding()
        
        'Act
        Dim manipulator As IGapScoringManipulator(Of String) = New StringScoringManipulator(New KeyManipulator(key), param)
        
        'Assert
        Dim res = manipulator.GetKeyStatus()
        Assert.IsTrue(res.ContainsKey("A"))
        Assert.IsTrue(res.ContainsKey("C"))
        Assert.IsTrue(res.ContainsKey("D"))
        Assert.IsTrue(res.ContainsKey("G"))

    End Sub


    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub ClearKeys_Should_thus_have_Keys_noValue()
        'Arrange
        Dim param = New StringScoringParameter() With {.ControllerId = "Gap"}
        param.Value = New ParameterSetCollection
        param.Value.Add(New ParameterCollection() With {.Id = "A"})

        Dim key = StringKeyFinding(CreateKeyValuePair("A-Gap", "ABC")) 'Note that ControllerID has been set, thus finding id is A-Gap
        Dim manipulator As IGapScoringManipulator(Of String) = New StringScoringManipulator(New KeyManipulator(key), param)
        
        'Act
        manipulator.RemoveKey("A") ' This will not remove finding but its values.
        
        'Assert
        Dim res = manipulator.GetKeyStatus()
        Assert.IsTrue(res.ContainsKey("A"))
        Assert.AreEqual(0, key.Facts.Count)
    End Sub


    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub UnsetKey_A_Expects_JustKeysOnParam()
        'Arrange
        Dim param = New StringScoringParameter() With {.ControllerId = "Gap"}
        param.Value = New ParameterSetCollection
        param.Value.Add(New ParameterCollection() With {.Id = "A"})

        Dim key = StringKeyFinding(CreateKeyValuePair("A", "ABC", "Def"), CreateKeyValuePair("B", "123", "456"))

        Dim manipulator As IGapScoringManipulator(Of String) = New StringScoringManipulator(New KeyManipulator(key), param)
        
        'Act
        manipulator.Clear()
        
        'Assert
        Dim res = manipulator.GetKeyStatus()
        Assert.AreEqual(1, res.Count)
    End Sub


    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub IdOfFactIsParameterId()
        'Arrange
        Dim param = New StringScoringParameter() With {.ControllerId = "Gap"}
        param.Value = New ParameterSetCollection
        param.Value.Add(New ParameterCollection() With {.Id = "A"})
        param.Value.Add(New ParameterCollection() With {.Id = "B"})

        Dim key = StringKeyFinding()

        Dim manipulator As IGapScoringManipulator(Of String) = New StringScoringManipulator(New KeyManipulator(key), param)
        
        'Act
        manipulator.SetKey("A", "123", "456")
        Dim result = DirectCast(key.Facts.First(), KeyFact)
        
        'Assert
        Assert.AreEqual("A-Gap", result.Id)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub DomainOfFactIsControllerID()
        'Arrange
        Dim param = New StringScoringParameter() With {.ControllerId = "Gap"} '<= CONTROLLER ID MATCHES DOMAIN
        param.Value = New ParameterSetCollection
        param.Value.Add(New ParameterCollection() With {.Id = "A"})
        param.Value.Add(New ParameterCollection() With {.Id = "B"})

        Dim key = StringKeyFinding()

        Dim manipulator As IGapScoringManipulator(Of String) = New StringScoringManipulator(New KeyManipulator(key), param)
        
        'Act
        manipulator.SetKey("B", "123", "456")
        Dim result = DirectCast(DirectCast(key.Facts.First(), KeyFact).Values(0), KeyValue)
        
        'Assert
        Assert.AreEqual("Gap", result.Domain, "Domain does not have expected value") '<= CONTROLLER ID MATCHES DOMAIN
    End Sub


    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub GetDisplayValueForKey()
        'Arrange
        Dim solution = sampleData.To(Of Solution)()
        Dim stringParam = New StringScoringParameter() With {.ControllerId = "stringScore"}.AddSubParameters("A")
        Dim manipulator = stringParam.GetScoreManipulator(solution)
        
        'Act
        Dim result = manipulator.GetDisplayValueForKey("A")
        
        'Assert
        Dim expected As String = "Value1#Value2#Value3"
        Assert.AreEqual(expected, result)
    End Sub

    Private Function CreateKeyValuePair(key As String, ParamArray values As String()) As KeyValuePair(Of String, IEnumerable(Of String))
        Dim ret = New KeyValuePair(Of String, IEnumerable(Of String))(key, values)

        Return ret
    End Function

    Private Function StringKeyFinding(ParamArray kvp As KeyValuePair(Of String, IEnumerable(Of String))()) As KeyFinding
        Dim ret As New KeyFinding()

        For Each k In kvp
            Dim fact = New KeyFact(k.Key)
            Dim keyVal = New KeyValue(k.Key, 1)
            fact.Values.Add(keyVal)

            For Each v In k.Value
                keyVal.Values.Add(New StringValue(v))
            Next
            ret.Facts.Add(fact)
        Next

        Return ret
    End Function

#Region "Data"
    ReadOnly sampleData As XElement = <solution>
                                          <keyFindings>
                                              <keyFinding id="stringScore" scoringMethod="Dichotomous">
                                                  <keyFact id="A-stringScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                      <keyValue domain="stringScore" occur="1">
                                                          <stringValue>
                                                              <typedValue>Value1</typedValue>
                                                          </stringValue>
                                                          <stringValue>
                                                              <typedValue>Value2</typedValue>
                                                          </stringValue>
                                                          <stringValue>
                                                              <typedValue>Value3</typedValue>
                                                          </stringValue>
                                                      </keyValue>
                                                  </keyFact>
                                              </keyFinding>
                                          </keyFindings>
                                      </solution>
#End Region

End Class
