
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports Cito.Tester.ContentModel

<TestClass>
Public Class ConceptManipulatorTests : Inherits BaseScoringManipulatorTests(Of ConceptFinding, ConceptFact, ConceptValue)

    Public Overrides Sub AddToFactValueValues(val As BaseValue, toFactValue As ConceptValue)
        toFactValue.Values.Add(val)
    End Sub

    Public Overrides Function CreateFindingManipulator(key As ConceptFinding) As IFindingManipulator
        Return New ConceptManipulator(key)
    End Function

    Public Overrides Function MakeFact(id As String) As ConceptFact
        Return New ConceptFact(id)
    End Function

    Public Overrides Function MakeFactValue(id As String, occur As Short) As ConceptValue
        Return New ConceptValue(id, occur)
    End Function

    Public Overrides Sub AddFactsToFactSet(finding As ConceptFinding, position As Integer, ParamArray facts() As ConceptFact)
        While (Not finding.KeyFactsets.Count >= (position + 1))
            finding.KeyFactsets.Add(New ConceptFactsSet())
        End While

        Dim target = finding.KeyFactsets(position).Facts

        For Each fact In facts
            target.Add(fact)
        Next
    End Sub

    Public Overrides ReadOnly Property GetPerConcreteManipulator_CanManipulateSets As Boolean
        Get
            Return True
        End Get
    End Property
End Class
