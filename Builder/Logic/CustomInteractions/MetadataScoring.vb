Imports Newtonsoft.Json

Namespace CustomInteractions

    Public Class MetadataScoring

        <JsonProperty("coordinate")>
        Public Property CoordinateScoring As CoordinateScoring

        <JsonProperty("integer")>
        Public Property IntegerScoring As IntegerScoring

        <JsonProperty("decimal")>
        Public Property DecimalScoring As DecimalScoring

        <JsonProperty("mathml")>
        Public Property MathmlScoring As MathMlScoring

        <JsonProperty("choice")>
        Public Property ChoiceScoring As ChoiceScoring

        <JsonProperty("ggb")>
        Public Property GeogebraScoring As GeogebraScoring

    End Class
End Namespace