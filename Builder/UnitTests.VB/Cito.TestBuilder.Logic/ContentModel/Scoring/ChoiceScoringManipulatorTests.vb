
Imports System.Linq
Imports System.Diagnostics
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports Cito.Tester.ContentModel
Imports System.Xml.Serialization
Imports System.IO
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class ChoiceScoringManipulatorTests

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub GetKey_A_NoSolution()
        Dim param = New ChoiceScoringParameter() With {.ControllerId = "MC", .MaxChoices = 1}
        param.Value = New ParameterSetCollection
        param.Value.Add(New ParameterCollection() With {.Id = "A"})

        Dim key = MCKeyFinding("MC")
        Dim manipulator As IChoiceScoringManipulator = New ChoiceScoringManipulator(GetKeyManipulator(key), param)

        Dim res = manipulator.GetKeyStatus()

        Write("Assert", key)
        Assert.IsTrue(res.ContainsKey("A"))
        Assert.IsFalse(res("A"))
        Assert.AreEqual(1, res.Keys.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub IdOfFactIsSetToIDOfParam()
        Dim param = New ChoiceScoringParameter() With {.ControllerId = "MC", .MaxChoices = 1}
        param.Value = New ParameterSetCollection
        param.Value.Add(New ParameterCollection() With {.Id = "A"})
        param.Value.Add(New ParameterCollection() With {.Id = "B"})
        Dim key = MCKeyFinding("MC")
        Dim manipulator As IChoiceScoringManipulator = New ChoiceScoringManipulator(GetKeyManipulator(key), param)

        manipulator.SetKey("B")
        Dim result = DirectCast(key.Facts.First(), KeyFact)

        Write("Assert", key)
        Assert.AreEqual("B-MC", result.Id)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub TestMultipleInlineScoringParameters()
        Dim paramA = New ChoiceScoringParameter() With {.ControllerId = "inline", .FindingOverride = "shared_finding", .MaxChoices = 1, .InlineId = "inline-1"}
        paramA.Value = New ParameterSetCollection
        paramA.Value.Add(New ParameterCollection() With {.Id = "A"})
        paramA.Value.Add(New ParameterCollection() With {.Id = "B"})

        Dim paramB = New ChoiceScoringParameter() With {.ControllerId = "inline", .FindingOverride = "shared_finding", .MaxChoices = 1, .InlineId = "inline-2"}
        paramB.Value = New ParameterSetCollection
        paramB.Value.Add(New ParameterCollection() With {.Id = "C"})
        paramB.Value.Add(New ParameterCollection() With {.Id = "D"})

        Dim key = MCKeyFinding("")
        key.Id = "inline"
        Dim manipulatorA As IChoiceScoringManipulator = New ChoiceScoringManipulator(GetKeyManipulator(key), paramA)
        Dim manipulatorB As IChoiceScoringManipulator = New ChoiceScoringManipulator(GetKeyManipulator(key), paramB)

        manipulatorA.SetKey("B")
        manipulatorB.SetKey("C")
        manipulatorB.SetKey("D")
        Dim keyStatusA = manipulatorA.GetKeyStatus()
        Dim keyStatusB = manipulatorB.GetKeyStatus()

        Write("Assert", key)
        Assert.AreEqual(2, keyStatusA.Count)
        Assert.IsFalse(keyStatusA("A"))
        Assert.IsTrue(keyStatusA("B"))
        Assert.AreEqual(2, keyStatusB.Count)
        Assert.IsFalse(keyStatusB("C"))
        Assert.IsTrue(keyStatusB("D"))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    <Description("Tests what would happen with ChoiceScoringPRM in same finding where maxchoice = 1 with overlapping Ids")>
    Public Sub TestMultipleInlineScoringParametersOverlappingIDs()
        Dim paramA = New ChoiceScoringParameter() With {.ControllerId = "inline", .FindingOverride = "shared_finding", .MaxChoices = 1, .InlineId = "inline-1"}
        paramA.Value = New ParameterSetCollection
        paramA.Value.Add(New ParameterCollection() With {.Id = "A"})
        paramA.Value.Add(New ParameterCollection() With {.Id = "B"})
        paramA.Value.Add(New ParameterCollection() With {.Id = "C"})
        paramA.Value.Add(New ParameterCollection() With {.Id = "D"})

        Dim paramB = New ChoiceScoringParameter() With {.ControllerId = "inline", .FindingOverride = "shared_finding", .MaxChoices = 1, .InlineId = "inline-2"}
        paramB.Value = New ParameterSetCollection
        paramB.Value.Add(New ParameterCollection() With {.Id = "A"})
        paramB.Value.Add(New ParameterCollection() With {.Id = "B"})
        paramB.Value.Add(New ParameterCollection() With {.Id = "C"})
        paramB.Value.Add(New ParameterCollection() With {.Id = "D"})

        Dim key = MCKeyFinding("")
        key.Id = "inline"
        Dim manipulatorA As IChoiceScoringManipulator = New ChoiceScoringManipulator(GetKeyManipulator(key), paramA)
        Dim manipulatorB As IChoiceScoringManipulator = New ChoiceScoringManipulator(GetKeyManipulator(key), paramB)

        manipulatorA.SetKey("B")
        manipulatorB.SetKey("C")
        manipulatorB.SetKey("D")
        Dim keyStatusA = manipulatorA.GetKeyStatus()
        Dim keyStatusB = manipulatorB.GetKeyStatus()

        Write("Assert", key)
        Assert.AreEqual(4, keyStatusA.Count)
        Assert.IsFalse(keyStatusA("A"))
        Assert.IsTrue(keyStatusA("B"))
        Assert.IsFalse(keyStatusA("C"))
        Assert.IsFalse(keyStatusA("D"))

        Assert.AreEqual(4, keyStatusB.Count)
        Assert.IsFalse(keyStatusB("A"))
        Assert.IsFalse(keyStatusB("B"))
        Assert.IsFalse(keyStatusB("C"))
        Assert.IsTrue(keyStatusB("D"))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    <Description("Tests a situation with two choiceScoring params in same finding.")>
    Public Sub MultiResponseTestSameFinding()
        Dim paramA = New ChoiceScoringParameter() With {.ControllerId = "inline", .FindingOverride = "shared_finding", .MaxChoices = 2, .InlineId = "inline-1"}
        paramA.Value = New ParameterSetCollection
        paramA.Value.Add(New ParameterCollection() With {.Id = "A"})
        paramA.Value.Add(New ParameterCollection() With {.Id = "B"})
        paramA.Value.Add(New ParameterCollection() With {.Id = "C"})
        paramA.Value.Add(New ParameterCollection() With {.Id = "D"})

        Dim paramB = New ChoiceScoringParameter() With {.ControllerId = "inline", .FindingOverride = "shared_finding", .MaxChoices = 2, .InlineId = "inline-2"}
        paramB.Value = New ParameterSetCollection
        paramB.Value.Add(New ParameterCollection() With {.Id = "A"})
        paramB.Value.Add(New ParameterCollection() With {.Id = "B"})
        paramB.Value.Add(New ParameterCollection() With {.Id = "C"})
        paramB.Value.Add(New ParameterCollection() With {.Id = "D"})

        Dim key = MCKeyFinding("")
        key.Id = "inline"
        Dim manipulatorA As IChoiceScoringManipulator = New MultiResponseScoringManipulator(GetKeyManipulator(key), paramA)
        Dim manipulatorB As IChoiceScoringManipulator = New MultiResponseScoringManipulator(GetKeyManipulator(key), paramB)

        manipulatorA.SetKey("B")
        manipulatorB.SetKey("C")
        manipulatorB.SetKey("D")
        Dim keyStatusA = manipulatorA.GetKeyStatus()
        Dim keyStatusB = manipulatorB.GetKeyStatus()

        Write("Assert", key)
        Assert.AreEqual(4, keyStatusA.Count)
        Assert.IsFalse(keyStatusA("A"))
        Assert.IsTrue(keyStatusA("B"))
        Assert.IsFalse(keyStatusA("C"))
        Assert.IsFalse(keyStatusA("D"))

        Assert.AreEqual(4, keyStatusB.Count)
        Assert.IsFalse(keyStatusB("A"))
        Assert.IsFalse(keyStatusB("B"))
        Assert.IsTrue(keyStatusB("C"))
        Assert.IsTrue(keyStatusB("D"))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub DomainOfFactIsSetToIDOfParam()
        Dim param = New ChoiceScoringParameter() With {.ControllerId = "MC", .MaxChoices = 1}
        param.Value = New ParameterSetCollection
        param.Value.Add(New ParameterCollection() With {.Id = "A"})
        param.Value.Add(New ParameterCollection() With {.Id = "B"})
        Dim key = MCKeyFinding("MC")
        Dim manipulator As IChoiceScoringManipulator = New ChoiceScoringManipulator(GetKeyManipulator(key), param)
        manipulator.SetKey("B")
        Dim result = DirectCast(DirectCast(key.Facts.First(), KeyFact).Values(0), KeyValue)
        Assert.AreEqual("MC", result.Domain)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub DomainOfFactIsSetToCombinationOfCollectionIdxAndIDOfParam()
        Dim param = New ChoiceScoringParameter() With {.ControllerId = "MC", .MaxChoices = 0}
        param.Value = New ParameterSetCollection
        param.Value.Add(New ParameterCollection() With {.Id = "A"})
        param.Value.Add(New ParameterCollection() With {.Id = "B"})
        param.Value.Add(New ParameterCollection() With {.Id = "C"})
        Dim key = MRKeyFinding("MC")
        Dim manipulator As IChoiceScoringManipulator = New MultiResponseScoringManipulator(GetKeyManipulator(key), param)

        manipulator.SetKey("B")
        Dim result = DirectCast(DirectCast(key.Facts.First(), KeyFact).Values(0), KeyValue)

        Assert.AreEqual("B-MC", result.Domain)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub GetDisplayValue_MC()
        Dim solution As New Solution()
        Dim param = New ChoiceScoringParameter() With {.ControllerId = "MC", .MaxChoices = 1}.AddSubParameters("A", "B", "C")
        Dim manipulator = param.GetScoreManipulator(solution)
        manipulator.SetKey("B")

        manipulator = param.GetScoreManipulator(solution)
        Dim result = manipulator.GetDisplayValueForKey("B")

        Assert.IsTrue(param.IsSingleChoice)
        Assert.AreEqual("B", result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub GetDisplayValue_Mr_SetB_GetB()
        Dim solution As New Solution()
        Dim param = New ChoiceScoringParameter() With {.ControllerId = "MC", .MaxChoices = 0}.AddSubParameters("A", "B", "C")
        Dim manipulator = param.GetScoreManipulator(solution)
        manipulator.SetKey("B")

        manipulator = param.GetScoreManipulator(solution)
        Dim result = manipulator.GetDisplayValueForKey("B")

        Assert.IsFalse(param.IsSingleChoice)
        Assert.AreEqual("True", result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub GetDisplayValue_Mr_UnSetA_GetA()
        Dim solution As New Solution()
        Dim param = New ChoiceScoringParameter() With {.ControllerId = "MC", .MaxChoices = 0}.AddSubParameters("A", "B", "C")
        Dim manipulator = param.GetScoreManipulator(solution)
        manipulator.RemoveKey("A")

        manipulator = param.GetScoreManipulator(solution)
        Dim result = manipulator.GetDisplayValueForKey("A")

        Assert.IsFalse(param.IsSingleChoice)
        Assert.AreEqual("False", result)
    End Sub

    Protected Function MRKeyFinding(controller As String, ParamArray keys As String()) As KeyFinding
        Dim ret As New KeyFinding()

        For Each k In keys
            Dim fact = New KeyFact(String.Format("{0}-{1}", k, controller)) With {.Score = 1}
            Dim keyVal = New KeyValue(String.Format("{0}-{1}", k, controller), 1)
            fact.Values.Add(keyVal)
            keyVal.Values.Add(New StringValue(k))

            ret.Facts.Add(fact)
        Next

        Return ret
    End Function

    Protected Function MCKeyFinding(controller As String, ParamArray keys As String()) As KeyFinding
        Dim ret As New KeyFinding()

        For Each k In keys
            Dim fact = New KeyFact(String.Format("{0}-{1}", k, controller)) With {.Score = 1}
            Dim keyVal = New KeyValue("MC", 1)
            fact.Values.Add(keyVal)
            keyVal.Values.Add(New StringValue(k))

            ret.Facts.Add(fact)
        Next

        Return ret
    End Function

    Protected Sub Write(stateName As String, s As KeyFinding)
        Dim a As New XmlSerializer(GetType(KeyFinding))
        Debug.WriteLine(String.Empty)
        Debug.WriteLine(String.Format("Write KeyFinding for State [{0}]", stateName))
        Using stream = New StringWriter()
            a.Serialize(stream, s)

            Debug.WriteLine(stream.ToString())
        End Using
    End Sub

    Protected Sub WriteSolution(stateName As String, sol As Solution)
        Dim a As New XmlSerializer(GetType(Solution))
        Debug.WriteLine(String.Empty)
        Debug.WriteLine(String.Format("Write Solution for State [{0}]", stateName))
        Using stream = New StringWriter()
            a.Serialize(stream, sol)

            Debug.WriteLine(stream.ToString())
        End Using
    End Sub

    Friend Overridable Function GetKeyManipulator(key As KeyFinding) As FindingManipulatorBase
        Return New KeyManipulator(key)
    End Function

End Class
