Imports Cito.Tester.ContentModel

Namespace ContentModel.Scoring

    Public Class ItemScoreTranslationTableCalculator
        Inherits ScoreTranslationTableCalculator(Of ItemScoreTranslationTableEntry)

        Public Sub New(maxRawScore As Integer, currentScoreTranslationTable As ItemScoreTranslationTable)
            MyBase.New(maxRawScore, currentScoreTranslationTable)
        End Sub

        Protected Overrides Sub AddScoreTranslationEntry(valueToAdd As Integer)
            _currentScoreTranslationTable.Add(New ItemScoreTranslationTableEntry(valueToAdd, CDbl(valueToAdd)))
        End Sub

    End Class

End Namespace