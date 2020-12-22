Imports System.Text.RegularExpressions

Public NotInheritable Class IdentifierHelper


    Public Const CI_ParameterCollectionName As String = "__CustomInteractions"
    Public Const CI_ParameterCollectionPrefix As String = "__CI_"
    Public Const CI_FindingName As String = "CustomInteractions"


    Public Shared Function MatchesInlineCustomInteractionIdentifier(identifier As String, ByRef guidPart As String) As Boolean
        Dim regex As New Regex("CI_SP_(?<guidPart>.+?)_[0-9]")
        Dim result = regex.Match(identifier)
        If result.Success Then
            If Not String.IsNullOrEmpty(result.Groups("guidPart").Value) AndAlso CheckIdentifierIsGuid(result.Groups("guidPart").Value) Then
                guidPart = result.Groups("guidPart").Value
                Return True
            End If
        End If
        Return False
    End Function

    Public Shared Function MatchesCustomInteractionIdentifier(identifier As String) As Boolean
        Dim regex As New Regex("CI_SP[0-9]")
        Dim result = regex.Match(identifier)
        Return result.Success
    End Function

    Public Shared Function CheckIdentifierIsGuid(identifier As String) As Boolean
        Dim result As Boolean = False

        If IsGuid(identifier) Then
            result = True
        ElseIf IsGuid(identifier.Substring(1)) Then
            result = True
        Else
            Dim underScoreIndex As Integer = identifier.IndexOf("_")
            If underScoreIndex > -1 AndAlso identifier.Length > underScoreIndex + 1 Then
                Dim guidIndex As Integer = underScoreIndex + 1

                If Char.IsUpper(identifier.Substring(guidIndex, 1).ToCharArray()(0)) Then
                    guidIndex += 1
                End If

                If guidIndex < identifier.Length Then
                    result = IsGuid(identifier.Substring(guidIndex))
                End If
            End If
        End If

        Return result
    End Function


    Private Shared Function IsGuid(identifier As String) As Boolean
        Dim guidPattern As String = "[a-fA-F0-9]{8}(\-[a-fA-F0-9]{4}){3}\-[a-fA-F0-9]{12}"
        If String.IsNullOrEmpty(identifier) Then Return False
        Dim regex As New Regex(guidPattern)
        Return regex.IsMatch(identifier)
    End Function


End Class
