Imports Newtonsoft.Json

Namespace CustomInteractions

    <JsonObject("coordinate")>
    Public Class CoordinateScoring
        Inherits ScoringTypeBase

        Protected Overrides Function CreateValidator() As IScoringValidation
            Return New CoordinateScoringValidation(Me)
        End Function

    End Class
End Namespace