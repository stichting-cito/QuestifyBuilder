Imports Cito.Tester.ContentModel
Imports System.Text
Imports System.Globalization

Public Class RemoveDiacritics
    Implements IResponseKeyValuePreprocessor

    Public Sub New()
    End Sub

    Public ReadOnly Property DisplayName As String Implements IResponseKeyValuePreprocessor.DisplayName
        Get
            Return My.Resources.RemoveDiacritics_Description
        End Get
    End Property

    Public ReadOnly Property Id As PreProcessingRuleId Implements IResponseKeyValuePreprocessor.Id
        Get
            Return PreProcessingRuleId.VDT
        End Get
    End Property

    Public Function PreprocessValue(keyValue As BaseValue) As BaseValue Implements IResponseKeyValuePreprocessor.PreprocessValue
        Dim returnValue As New StringValue

        If TypeOf keyValue Is StringValue Then
            returnValue.Value = ConvertValue(DirectCast(keyValue, StringValue).Value)
        End If

        Return returnValue
    End Function

    Private Function ConvertValue(value As String) As String
        Dim result As String = value

        If Not String.IsNullOrEmpty(result) Then
            Dim stFormD As String = result.Normalize(NormalizationForm.FormD)
            Dim sb As New StringBuilder()
            For ich As Integer = 0 To stFormD.Length - 1

                Dim uc As UnicodeCategory = CharUnicodeInfo.GetUnicodeCategory(stFormD(ich))
                If uc <> UnicodeCategory.NonSpacingMark Then
                    sb.Append(stFormD(ich))
                End If
            Next
            result = (sb.ToString().Normalize(NormalizationForm.FormC))
        End If

        Return result
    End Function

End Class
