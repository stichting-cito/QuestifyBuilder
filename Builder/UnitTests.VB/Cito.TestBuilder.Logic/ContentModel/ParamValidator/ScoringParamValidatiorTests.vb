
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.UnitTests.Framework

<TestClass()> Public Class ScoringParamValidatiorTests

    <TestMethod(), TestCategory("Logic")>
    Public Sub Default_MultiChoiceScoringParameter_Test()
        'Arrange
        Dim s = New MultiChoiceScoringParameter
        
        'Act
        Dim result = s.IsValid()
        
        'Assert
        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub Default_MultiChoiceScoringParameter_Test_2()
        'Arrange
        Dim s = New MultiChoiceScoringParameter() With {.ControllerId = "mc"}.AddSubParameters("A", "B")
        
        'Act
        Dim result = s.IsValid()
        
        'Assert
        Assert.IsTrue(result)
    End Sub

End Class