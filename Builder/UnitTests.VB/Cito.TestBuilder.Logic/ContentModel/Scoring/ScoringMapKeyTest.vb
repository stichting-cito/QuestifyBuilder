
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class ScoringMapKeyTest

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub LabesIsMoreImportantThanName()
        'Label is specific to scoring parameters and not to parameters, thus is more important.

        'Arrange                                                                                            
        Dim sp = New IntegerScoringParameter() With {.Name = "nameProperty", .Label = "labelProperty"}.AddSubParameters("A")
        
        'Act                                                                                                 
        Dim result = New ScoringMapKey(sp, "A")
        
        'Assert                         
        Assert.AreEqual("labelProperty", result.Name)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub ScoringMapKeyWillTakeNamePropertyAsName()
        'Arrange                                                                                            
        Dim sp = New IntegerScoringParameter() With {.Name = "nameProperty"}.AddSubParameters("A")
        
        'Act                                                                                                 
        Dim result = New ScoringMapKey(sp, "A")
        
        'Assert                         
        Assert.AreEqual("nameProperty", result.Name)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub LabesIsMoreImportantThanName_MoreThanOneScoringPrmExpectsSuffix()
        'Label is specific to scoring parameters and not to parameters, thus is more important.

        'Arrange                                                                                            
        Dim sp = New IntegerScoringParameter() With {.Name = "nameProperty", .Label = "labelProperty"}.AddSubParameters("A", "B")
        
        'Act                                                                                                 
        Dim result = New ScoringMapKey(sp, "A")
        
        'Assert                         
        Assert.AreEqual("labelProperty.A", result.Name)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub ScoringMapKey_ShouldSuffixDeterminedByScoringParameter()
        'Arrange                                                                                            
        Dim sp = New GapMatchScoringParameter() With {.Name = "Name"}.AddSubParameters("A", "B")
        
        'Act                                                                                                 
        Dim result = New ScoringMapKey(sp, "A")
        
        'Assert                         
        Assert.AreEqual("Name", result.Name)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub ScoringMapKeyWillTakeNamePropertyAsName_MoreThanOneScoringPrmExpectsSuffix()
        'Arrange                                                                                            
        Dim sp = New IntegerScoringParameter() With {.Name = "nameProperty"}.AddSubParameters("A", "B")
        
        'Act                                                                                                 
        Dim result = New ScoringMapKey(sp, "B")
        
        'Assert                         
        Assert.AreEqual("nameProperty.B", result.Name)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub ScoringMapKeyWillTakeNamePropertyAsName_MoreThanOneScoringPrmExpectsSuffix_NoValidationThatKeyExists()
        'Arrange                                                                                            
        Dim sp = New IntegerScoringParameter() With {.Name = "nameProperty"}.AddSubParameters("A", "B")
        
        'Act                                                                                                 
        Dim result = New ScoringMapKey(sp, "X")
        
        'Assert                         
        Assert.AreEqual("nameProperty.X", result.Name)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub ScoringMapKeyHandlesScoringParameterWihoutNameOrLabel_NameIs_EmptyString()
        'Arrange                                                                                            
        Dim sp = New IntegerScoringParameter().AddSubParameters("A")
        
        'Act                                                                                                 
        Dim result = New ScoringMapKey(sp, "A")
        
        'Assert                         
        Assert.AreEqual(String.Empty, result.Name)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub ScoringMapKeyHandlesScoringParameterWihoutNameOrLabel_WithMultipleSubParameters_NameIs_Key()
        'Arrange                                                                                            
        Dim sp = New IntegerScoringParameter().AddSubParameters("A", "B")
        
        'Act                                                                                                 
        Dim result = New ScoringMapKey(sp, "A")
        
        'Assert                         
        Assert.AreEqual("A", result.Name)
    End Sub

End Class
