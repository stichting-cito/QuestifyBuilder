Imports Cito.Tester.ContentModel

Namespace ContentModel.Scoring

    Public MustInherit Class ScoreTranslationTableCalculator(Of T As ScoreTranslationTableEntry)

        Private _maxRawScore As Integer
        Protected _currentScoreTranslationTable As ScoreTranslationTable(Of T)

        Public Sub New(maxRawScore As Integer, currentScoreTranslationTable As ScoreTranslationTable(Of T))
            _maxRawScore = maxRawScore
            _currentScoreTranslationTable = currentScoreTranslationTable
        End Sub

        Public Function SynchronizeScoreTranslationTableWithMaxRawScore() As ScoreTranslationTable(Of T)
            If ShouldAddScoringTranslationRows() Then
                AddMissingScoringTranslationRows()
            ElseIf ShouldRemoveScoringTranslationRows() Then
                RemoveExcessScoringTranslationRows()
            End If
            ClearUnneededScoringTranslationRows()
            Return _currentScoreTranslationTable
        End Function

        Private Function ShouldAddScoringTranslationRows() As Boolean
            Dim currentNrOfScoringTranslationRows As Integer = _currentScoreTranslationTable.Count - 1
            Return _maxRawScore > currentNrOfScoringTranslationRows AndAlso _maxRawScore > 0
        End Function

        Private Function ShouldRemoveScoringTranslationRows() As Boolean
            Dim currentNrOfScoringTranslationRows As Integer = _currentScoreTranslationTable.Count - 1
            Return _maxRawScore < currentNrOfScoringTranslationRows
        End Function

        Private Sub AddMissingScoringTranslationRows()
            Dim idx As Integer = _currentScoreTranslationTable.Count
            While idx <= _maxRawScore
                AddScoreTranslationEntry(idx)
                idx += 1
            End While
        End Sub

        Protected MustOverride Sub AddScoreTranslationEntry(valueToAdd As Integer)

        Private Sub RemoveExcessScoringTranslationRows()
            Dim idx As Integer = _currentScoreTranslationTable.Count - 1
            While idx > _maxRawScore
                Dim toRemove = _currentScoreTranslationTable(idx)
                _currentScoreTranslationTable.Remove(toRemove)
                idx -= 1
            End While
        End Sub

        Private Sub ClearUnneededScoringTranslationRows()
            If _maxRawScore = 0 AndAlso _currentScoreTranslationTable.Count = 1 Then
                Dim toRemove = _currentScoreTranslationTable(0)
                _currentScoreTranslationTable.Remove(toRemove)
            End If
        End Sub
    End Class

End Namespace