Imports Newtonsoft.Json

Namespace CustomInteractions

    <JsonObject("choice")>
    Public Class ChoiceScoring
        Inherits ScoringTypeBase

        <JsonProperty(PropertyName:="maxChoices", Required:=Required.Default)>
        Public Property MaxChoices As Integer

        <JsonProperty(PropertyName:="minChoices", Required:=Required.Default)>
        Public Property MinChoices As Integer

        Public Property Choices As SubChoice()

        Protected Overrides Function CreateValidator() As IScoringValidation
            Return New ChoiceScoringValidation(Me)
        End Function

    End Class

End Namespace