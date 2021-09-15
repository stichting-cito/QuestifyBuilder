
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports System.Linq
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class ConceptDisplayValueTests

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept"), WorkItem(19693)>
    Public Sub TestThatIDAOfMC_equalsStringEmpty()
        'Arrange
        Dim solution = New Solution()
        Dim choiceScoringParameter = New ChoiceScoringParameter() With {.MaxChoices = 1, .ControllerId = "MC"}.AddSubParameters("A", "B", "C")
        Dim map = New ScoringMap(New ScoringParameter() {choiceScoringParameter}, solution).GetMap()
        Dim conceptManipulator = map.First().GetConceptManipulator(solution)
        solution.WriteToDebug("Arrange")
        
        'Act
        Dim result = conceptManipulator.GetDisplayValueForConceptId("A")
        
        'Assert
        Assert.AreEqual(String.Empty, result)
    End Sub

End Class
