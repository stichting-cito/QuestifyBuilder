Imports System.IO
Imports Newtonsoft.Json
Public NotInheritable Class StoreSettings(Of T)

    Shared Sub New()

    End Sub

    Public Shared Function GetEntities() As List(Of T)
        Dim entitiesToReturn As New List(Of T)
        If Not String.IsNullOrEmpty(My.Settings.GridColumnOrderSettings) Then
            Dim serializer As New JsonSerializer()
            Using sr As JsonTextReader = New JsonTextReader(New StringReader(My.Settings.Item(GetType(T).Name).ToString()))
                entitiesToReturn = serializer.Deserialize(Of List(Of T))(sr)
            End Using
        End If
        Return entitiesToReturn
    End Function

    Public Shared Sub StoreEntities(entitiesToStore As List(Of T))
        If (entitiesToStore.Count = 0) Then
            My.Settings.Item(GetType(T).Name) = String.Empty
        Else
            My.Settings.Item(entitiesToStore(0).GetType.Name) = JsonConvert.SerializeObject(entitiesToStore)
        End If
    End Sub
End Class
