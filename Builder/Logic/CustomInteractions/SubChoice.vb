Imports Newtonsoft.Json

Namespace CustomInteractions

    Public Class SubChoice

        <JsonProperty(PropertyName:="id", Required:=Required.Default)>
        Public Property Id As String

        <JsonProperty(PropertyName:="label", Required:=Required.Default)>
        Public Property Label As String

    End Class
End Namespace