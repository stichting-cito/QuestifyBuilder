Imports Newtonsoft.Json

Namespace CustomInteractions

    <JsonObject("decimal")>
    Public Class DecimalScoring
        Inherits ScoringTypeBase

        Public Sub New()
            MaxIntegerPart = 10
            MaxFactionPart = 2
        End Sub

        <JsonProperty(PropertyName:="maxIntegerPart", Required:=Required.Default)>
        Public Property MaxIntegerPart As Integer

        <JsonProperty(PropertyName:="maxFactionPart", Required:=Required.Default)>
        Public Property MaxFactionPart As Integer

        Protected Overrides Function CreateValidator() As IScoringValidation
            Return New DecimalScoringValidation(Me)
        End Function

    End Class
End Namespace