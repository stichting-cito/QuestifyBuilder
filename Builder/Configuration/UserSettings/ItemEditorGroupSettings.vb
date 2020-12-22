Imports System.IO
Imports Newtonsoft.Json

Public Class ItemEditorGroupSettings

    Private Shared _collapsedGroups As New List(Of String)
    Private Shared _isLoaded As Boolean = False

    Public Shared Function IsExpanded(groupName As String) As Boolean
        If Not _isLoaded Then
            If Not String.IsNullOrEmpty(My.Settings.ItemEditorGroups) Then
                Using sr As JsonTextReader = New JsonTextReader(New StringReader(My.Settings.ItemEditorGroups))
                    Dim serializer As New JsonSerializer()
                    _collapsedGroups = serializer.Deserialize(Of List(Of String))(sr)
                End Using
            End If
            _isLoaded = True
        End If
        Return Not _collapsedGroups.Contains(groupName)
    End Function
    Public Shared Sub ChangeCollapseState(groupName As String, collapse As Boolean)
        If collapse AndAlso Not _collapsedGroups.Contains(groupName) Then
            _collapsedGroups.Add(groupName)
        ElseIf Not collapse AndAlso _collapsedGroups.Contains(groupName) Then
            _collapsedGroups.Remove(groupName)
        End If
        My.Settings.ItemEditorGroups = JsonConvert.SerializeObject(_collapsedGroups)
    End Sub

End Class
