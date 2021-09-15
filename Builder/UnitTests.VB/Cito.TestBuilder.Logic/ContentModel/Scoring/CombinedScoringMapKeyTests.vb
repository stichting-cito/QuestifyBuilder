
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports Questify.Builder.UnitTests.Framework
Imports System.Linq

<TestClass>
Public Class CombinedScoringMapKeyTests

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub CreateTestByParamArray()
        'Arrange
        Dim sp = New IntegerScoringParameter() With {.Name = "nameProperty", .Label = "labelProperty"}.AddSubParameters("A")
        Dim scoringMapKey = New ScoringMapKey(sp, "A")
        
        'Act
        Dim result = CombinedScoringMapKey.Create(scoringMapKey)

        'Assert
        Assert.AreEqual("labelProperty", result.Name)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub CreateTestByEnumerable()
        'Arrange
        Dim sp = New IntegerScoringParameter() With {.Name = "nameProperty", .Label = "labelProperty"}.AddSubParameters("A")
        Dim lst As New List(Of ScoringMapKey)
        Dim scoringMapKey = New ScoringMapKey(sp, "A")
        lst.Add(scoringMapKey)
        
        'Act
        Dim result = CombinedScoringMapKey.Create(lst)

        'Assert
        Assert.AreEqual("labelProperty", result.Name)
    End Sub


    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub CreateTestMultipleScoreMapKeys()
        'Arrange
        Dim sp = New IntegerScoringParameter() With {.Label = "Getal"}.AddSubParameters("A", "B", "C")
        Dim lst As New List(Of ScoringMapKey)
        lst.Add(New ScoringMapKey(sp, "A"))
        lst.Add(New ScoringMapKey(sp, "C"))
        
        'Act
        Dim result = CombinedScoringMapKey.Create(lst)

        'Assert
        Assert.AreEqual("Getal.A & Getal.C", result.Name)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub UsageAsEnumerator()
        'Arrange
        Dim sp = New IntegerScoringParameter() With {.Label = "Getal"}.AddSubParameters("A", "B", "C")
        Dim lst As New List(Of ScoringMapKey)
        lst.Add(New ScoringMapKey(sp, "A"))
        lst.Add(New ScoringMapKey(sp, "C"))

        Dim combined = CombinedScoringMapKey.Create(lst)
        
        'Act
        Dim result As New List(Of String)
        For Each e In combined
            result.Add(e.ScoreKey)

        Next

        'Assert 
        Assert.AreEqual("AC", String.Concat(result.ToArray()))
    End Sub


    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub UsageLinqCount()
        'Arrange
        Dim sp = New IntegerScoringParameter() With {.Label = "Getal"}.AddSubParameters("A", "B", "C")
        Dim sp2 = New StringScoringParameter() With {.Label = "TXT"}.AddSubParameters("A")
        Dim lst As New List(Of ScoringMapKey)
        lst.Add(New ScoringMapKey(sp, "A"))
        lst.Add(New ScoringMapKey(sp, "C"))
        lst.Add(New ScoringMapKey(sp2, "A"))

        Dim combined = CombinedScoringMapKey.Create(lst)
        
        'Act
        Dim result = combined.Count()

        'Assert 
        Assert.AreEqual(3, result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub TwoSP_VerifyName()
        'Arrange
        Dim spA = New IntegerScoringParameter() With {.Label = "GetalA"}.AddSubParameters("1")
        Dim spB = New IntegerScoringParameter() With {.Label = "GetalB"}.AddSubParameters("1")
        Dim lst As New List(Of ScoringMapKey)
        lst.Add(New ScoringMapKey(spA, "1"))
        lst.Add(New ScoringMapKey(spB, "1"))
        
        'Act
        Dim result = CombinedScoringMapKey.Create(lst)

        'Assert
        Assert.AreEqual("GetalA & GetalB", result.Name)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub TwoSP_VerifyIsGroup()
        'Arrange
        Dim spA = New IntegerScoringParameter() With {.Label = "GetalA"}.AddSubParameters("1")
        Dim spB = New IntegerScoringParameter() With {.Label = "GetalB"}.AddSubParameters("1")
        Dim lst As New List(Of ScoringMapKey)
        lst.Add(New ScoringMapKey(spA, "1"))
        lst.Add(New ScoringMapKey(spB, "1"))
        
        'Act
        Dim result = CombinedScoringMapKey.Create(lst)

        'Assert
        Assert.IsTrue(result.IsGroup)
    End Sub


    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub CreateMCTest()
        'Arrange
        Dim sp = New MultiChoiceScoringParameter() With {.Label = "MC", .MaxChoices = 1}.AddSubParameters("A", "B", "C")
        Dim lst As New List(Of ScoringMapKey)
        lst.Add(New ScoringMapKey(sp, "A"))
        lst.Add(New ScoringMapKey(sp, "B"))
        lst.Add(New ScoringMapKey(sp, "C"))
        
        'Act
        Dim result = CombinedScoringMapKey.Create(lst)

        'Assert
        Assert.AreEqual("MC", result.Name)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub CreateMRTest()
        'Arrange
        Dim sp = New MultiChoiceScoringParameter() With {.Label = "MC", .MaxChoices = 4}.AddSubParameters("A", "B", "C")
        Dim lst As New List(Of ScoringMapKey)
        lst.Add(New ScoringMapKey(sp, "A"))
        lst.Add(New ScoringMapKey(sp, "B"))
        lst.Add(New ScoringMapKey(sp, "C"))
        
        'Act
        Dim result = CombinedScoringMapKey.Create(lst)

        'Assert
        Assert.AreEqual("MC.A & MC.B & MC.C", result.Name)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub IsGroupShouldReturnTrueWithMultipleScoreKeys()
        'Arrange
        Dim sp = New IntegerScoringParameter() With {.Label = "Getal"}.AddSubParameters("A", "B", "C")
        Dim lst As New List(Of ScoringMapKey)
        lst.Add(New ScoringMapKey(sp, "A"))
        lst.Add(New ScoringMapKey(sp, "C"))
        
        'Act
        Dim result = CombinedScoringMapKey.Create(lst)

        'Assert
        Assert.IsTrue(result.IsGroup)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub IsGroupShouldReturnFalseWithSingleScoreKeys()
        'Arrange
        Dim sp = New IntegerScoringParameter() With {.Label = "Getal"}.AddSubParameters("A", "B", "C")
        Dim lst As New List(Of ScoringMapKey)
        lst.Add(New ScoringMapKey(sp, "A"))
        
        'Act
        Dim result = CombinedScoringMapKey.Create(lst)

        'Assert
        Assert.IsFalse(result.IsGroup)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub IsGroupShouldReturnFalseWithMultiChoiceAndMultipleKeys()
        'Arrange
        Dim sp = New ChoiceScoringParameter() With {.Label = "MC", .MaxChoices = 1}.AddSubParameters("A", "B", "C")
        Dim lst As New List(Of ScoringMapKey)
        lst.Add(New ScoringMapKey(sp, "A"))
        lst.Add(New ScoringMapKey(sp, "B"))
        lst.Add(New ScoringMapKey(sp, "C"))

        'Act
        Dim result = CombinedScoringMapKey.Create(lst)

        'Assert
        Assert.IsFalse(result.IsGroup)
    End Sub

End Class
