Namespace CustomInteractions

    Public Class GeogebraScoringValidation : Implements IScoringValidation

        Private _ggbScoring As GeogebraScoring

        Sub New(ggbScoring As GeogebraScoring)
            _ggbScoring = ggbScoring
        End Sub

        Public Function IsValid(ByRef errorMessage As List(Of String)) As Boolean Implements IScoringValidation.IsValid
            Return True
        End Function

    End Class

End Namespace