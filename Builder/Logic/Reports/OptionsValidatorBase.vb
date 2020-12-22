Imports System.ComponentModel
Imports System.Text

Public Class OptionsValidatorBase
    Implements IDataErrorInfo

    Protected Const FIELD_EXPORTPATH As String = "ExportPath"

    Protected ReadOnly _validationErrors As New Dictionary(Of String, String)

    Default Public ReadOnly Property Item(ByVal columnName As String) As String Implements IDataErrorInfo.Item
        Get
            If _validationErrors.ContainsKey(columnName) Then
                Return _validationErrors(columnName)
            Else
                Return String.Empty
            End If
        End Get
    End Property


    Public ReadOnly Property [Error]() As String Implements IDataErrorInfo.Error
        Get
            Dim messageBuilder As New StringBuilder

            If _validationErrors.Count > 0 Then
                messageBuilder.AppendFormat(My.Resources.CannotStartExport, Environment.NewLine)
                For Each entry As KeyValuePair(Of String, String) In _validationErrors
                    messageBuilder.AppendFormat(" - {0}{1}", entry.Value, Environment.NewLine)
                Next
            End If

            Return messageBuilder.ToString()
        End Get
    End Property

End Class
