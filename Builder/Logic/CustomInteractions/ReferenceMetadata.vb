Imports System.Linq
Imports Newtonsoft.Json

Namespace CustomInteractions

    <JsonObject>
    Public Class ReferenceMetadata
        <JsonProperty("script")>
        Public Property Scripts As String()

        <JsonProperty("style")>
        Public Property Styles As String()

        <JsonProperty("media")>
        Public Property Medias As String()

        Public Function GetAllReferencedFiles() As List(Of String)
            Return Scripts.
                Union(Medias).
                Union(Styles).ToList()
        End Function
    End Class
End Namespace