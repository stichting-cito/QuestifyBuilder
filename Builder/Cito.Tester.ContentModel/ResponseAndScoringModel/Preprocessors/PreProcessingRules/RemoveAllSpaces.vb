﻿Imports Cito.Tester.ContentModel

Public Class RemoveAllSpaces
    Implements IResponseKeyValuePreprocessor

    Public Sub New()
    End Sub

    Public ReadOnly Property DisplayName As String Implements IResponseKeyValuePreprocessor.DisplayName
        Get
            Return My.Resources.RemoveAllSpaces_Description
        End Get
    End Property

    Public ReadOnly Property Id As PreProcessingRuleId Implements IResponseKeyValuePreprocessor.Id
        Get
            Return PreProcessingRuleId.VAS
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
        If Not String.IsNullOrEmpty(value) Then
            Return value.Replace(Chr(32), String.Empty)
        End If

        Return value
    End Function

End Class
