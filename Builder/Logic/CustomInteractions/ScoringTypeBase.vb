Imports Newtonsoft.Json

Namespace CustomInteractions

    <JsonObject()>
    Public MustInherit Class ScoringTypeBase

        <JsonProperty(PropertyName:="label", Required:=Required.Default)>
        Public Property Label As String

        <JsonProperty(PropertyName:="correctResponse", Required:=Required.Default)>
        Public Property CorrectResponse As String

        Friend Function IsValid(ByRef errorMessage As List(Of String)) As Boolean
            If (errorMessage Is Nothing) Then errorMessage = New List(Of String)

            Dim validator = CreateValidator()
            Debug.Assert(validator IsNot Nothing)

            Return validator.IsValid(errorMessage)
        End Function

        Protected MustOverride Function CreateValidator() As IScoringValidation

    End Class

End Namespace