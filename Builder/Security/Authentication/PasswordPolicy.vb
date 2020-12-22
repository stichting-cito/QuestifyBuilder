Imports System.Text.RegularExpressions

Public Class PasswordPolicy
    Private MinimalLength As Integer = 8

    Public Function IsValid(ByVal password As String, ByVal userName As String) As Boolean
        If password.Length < MinimalLength Then
            Return False
        End If
        Dim categories As Integer = 0
        if ContainsLowerCase(password) Then
            categories += 1
        End If
        if ContainsUpperCase(password) Then
            categories += 1
        End If
        if ContainsNumber(password) Then
            categories += 1
        End If
        if ContainsSpecial(password) Then
            categories += 1
        End If
        if (categories < 3) Then
            Return False
        End If
        if (password.Contains(username))
            Return False
        End If
        Return True
    End Function

    Private Function ContainsUpperCase(ByVal password As String) As Boolean
        Return Regex.Matches(password, "[A-Z]").Count > 0
    End Function

    Private Function ContainsLowerCase(ByVal password As String) As Boolean
        Return Regex.Matches(password, "[a-z]").Count > 0
    End Function

    Private Function ContainsNumber(ByVal password As String) As Boolean
        Return Regex.Matches(password, "[0-9]").Count > 0
    End Function

    Private Function ContainsSpecial(ByVal password As String) As Boolean
        Return Regex.Matches(password, "[^0-9a-zA-Z\._]").Count > 0
    End Function
End Class
