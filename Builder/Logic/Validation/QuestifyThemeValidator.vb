Imports Questify.Builder.Logic

Public Class QuestifyThemeValidator
    Public Shared Function TryValidate(ByVal path As String) As Boolean
        Dim errorMessages As New List(Of String)()
        Return TryValidate(path, errorMessages)
    End Function

    Public Shared Function TryValidate(ByVal path As String, ByRef errorMessages As List(Of String)) As Boolean
        Dim isQuestifyTheme As Boolean = LogicFileHelper.FileIsQuestifyTheme(path, errorMessages)
        Return errorMessages.Count = 0 AndAlso isQuestifyTheme
    End Function
End Class
