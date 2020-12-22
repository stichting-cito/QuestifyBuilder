Imports Newtonsoft.Json

Namespace CustomInteractions

    <JsonObject("ggb")>
    Public Class GeogebraScoring
        Inherits ScoringTypeBase

        Protected Overrides Function CreateValidator() As IScoringValidation
            Return New GeogebraScoringValidation(Me)
        End Function

    End Class

End Namespace
