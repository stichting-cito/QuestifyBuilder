Imports System
Imports Cito.Tester.ContentModel

Public Class ResponseKeyValuePreProcessorFactory

    Const Removeallspaces = "RemoveAllSpaces"
    Const Removeapostrophs = "RemoveApostrophs"
    Const Removediacritics = "RemoveDiacritics"
    Const Removehyphens = "RemoveHyphens"
    Const Removeleadingtrailingspaces = "RemoveLeadingTrailingSpaces"
    Const Convertytoij = "ConvertYToIJ"
    Const Converttolower = "ConvertToLower"

    Public Shared Function Create(type As String) As IResponseKeyValuePreprocessor
        Dim ruleId As PreProcessingRuleId
        If [Enum].TryParse(type, ruleId) Then
            Select Case ruleId
                Case PreProcessingRuleId.VAS
                    Return New RemoveAllSpaces
                Case PreProcessingRuleId.VAP
                    Return New RemoveApostrophs
                Case PreProcessingRuleId.VDT
                    Return New RemoveDiacritics
                Case PreProcessingRuleId.VKT
                    Return New RemoveHyphens
                Case PreProcessingRuleId.VSBE
                    Return New RemoveLeadingTrailingSpaces
                Case PreProcessingRuleId.YIJ
                    Return New ConvertYToIJ
                Case PreProcessingRuleId.HLKL
                    Return New ConvertToLower
                Case Else
                    Return Nothing
            End Select
        Else
            Select Case type
                Case Removeallspaces
                    Return New RemoveAllSpaces
                Case Removeapostrophs
                    Return New RemoveApostrophs
                Case Removediacritics
                    Return New RemoveDiacritics
                Case Removehyphens
                    Return New RemoveHyphens
                Case Removeleadingtrailingspaces
                    Return New RemoveLeadingTrailingSpaces
                Case Convertytoij
                    Return New ConvertYToIJ
                Case Converttolower
                    Return New ConvertToLower
                Case Else
                    Return Nothing
            End Select
        End If
    End Function
End Class
