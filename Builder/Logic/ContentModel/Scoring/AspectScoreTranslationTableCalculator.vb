Imports Cito.Tester.ContentModel

Namespace ContentModel.Scoring

    Public Class AspectScoreTranslationTableCalculator
        Inherits ScoreTranslationTableCalculator(Of AspectScoreTranslationTableEntry)

        Public Sub New(maxRawScore As Integer, currentScoreTranslationTable As AspectScoreTranslationTable)
            MyBase.New(maxRawScore, currentScoreTranslationTable)
        End Sub

        Protected Overrides Sub AddScoreTranslationEntry(valueToAdd As Integer)
            _currentScoreTranslationTable.Add(New AspectScoreTranslationTableEntry(valueToAdd, CDbl(valueToAdd)))
        End Sub

    End Class

End Namespace