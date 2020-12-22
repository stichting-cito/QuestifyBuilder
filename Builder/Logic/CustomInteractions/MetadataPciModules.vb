Imports Newtonsoft.Json

Namespace CustomInteractions
    Public Class MetadataPciModule
        <JsonProperty(PropertyName:="id", Required:=Required.Default)>
        Public Property Id As String

        <JsonProperty(PropertyName:="primaryPath", Required:=Required.Default)>
        Public Property PrimaryPath As String
    End Class
End Namespace