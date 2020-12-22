Imports Newtonsoft.Json

Namespace CustomInteractions

    <JsonObject("mathml")>
    Public Class MathMlScoring
        Inherits ScoringTypeBase

        Protected Overrides Function CreateValidator() As IScoringValidation
            Return New MathMlScoringValidation(Me)
        End Function
    End Class
End Namespace