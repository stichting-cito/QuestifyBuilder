Imports Newtonsoft.Json

Namespace CustomInteractions

    <JsonObject("integer")>
    Public Class IntegerScoring
        Inherits ScoringTypeBase

        Public Sub New()
            MaxLength = 10
        End Sub

        <JsonProperty(PropertyName:="maxLength", Required:=Required.Default)>
        Public Property MaxLength As Integer

        Protected Overrides Function CreateValidator() As IScoringValidation
            Return New IntegerScoringValidation(Me)
        End Function
    End Class
End Namespace