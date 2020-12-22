
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports Cito.Tester.ContentModel

<TestClass>
Public Class KeyManipulatorTests : Inherits BaseScoringManipulatorTests(Of KeyFinding, KeyFact, KeyValue)

    Public Overrides Sub AddToFactValueValues(val As BaseValue, toFactValue As KeyValue)
        toFactValue.Values.Add(val)
    End Sub

    Public Overrides Function CreateFindingManipulator(key As KeyFinding) As IFindingManipulator
        Return New KeyManipulator(key)
    End Function

    Public Overrides Function MakeFact(id As String) As KeyFact
        Return New KeyFact(id)
    End Function

    Public Overrides Function MakeFactValue(id As String, occur As Short) As KeyValue
        Return New KeyValue(id, occur)
    End Function

    Public Overrides ReadOnly Property GetPerConcreteManipulator_CanManipulateSets As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides Sub AddFactsToFactSet(finding As KeyFinding, position As Integer, ParamArray facts() As KeyFact)
        While (Not finding.KeyFactsets.Count >= (position + 1))
            finding.KeyFactsets.Add(New KeyFactSet())
        End While

        Dim target = finding.KeyFactsets(position).Facts

        For Each fact In facts
            target.Add(fact)
        Next
    End Sub

End Class
