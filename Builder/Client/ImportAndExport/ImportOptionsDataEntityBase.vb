Imports System.ComponentModel
Imports System.IO
Imports System.Text

Public Class ImportOptionsDataEntityBase
    Implements IDataErrorInfo

    Private Const FieldUrl As String = "Url"
    Private _url As String = " "
    Private ReadOnly _validationErrors As New Dictionary(Of String, String)

    Public Property Url() As String
        Get
            Return _url
        End Get
        Set(ByVal value As String)
            _url = value
            ValidateThis(FieldUrl, _url)
        End Set
    End Property


    Private Function ValidateThis(ByVal field As String, ByVal value As Object) As Boolean
        Dim valid As Boolean = True

        _validationErrors.Remove(field)

        Select Case field
            Case FieldUrl

                If String.IsNullOrEmpty(DirectCast(value, String)) OrElse DirectCast(value, String).Trim.Length = 0 Then
                    _validationErrors.Add(field, String.Format(My.Resources.FieldMustBeFilled, field))
                    valid = False
                Else
                    If Not File.Exists(DirectCast(value, String)) Then
                        _validationErrors.Add(field, String.Format(My.Resources.FileDoesNotExist, field))
                        valid = False
                    End If
                End If
            Case Else
                _validationErrors.Add(field, String.Format(My.Resources.NoFieldValidation, field))
                valid = False
        End Select

        Return valid
    End Function




    Public ReadOnly Property [Error]() As String Implements IDataErrorInfo.Error
        Get
            Dim messageBuilder As New StringBuilder

            If _validationErrors.Count > 0 Then
                messageBuilder.AppendFormat(My.Resources.CannotStartPublication, Environment.NewLine)
                For Each entry As KeyValuePair(Of String, String) In _validationErrors
                    messageBuilder.AppendFormat(" - {0}{1}", entry.Value, Environment.NewLine)
                Next
            End If

            Return messageBuilder.ToString()
        End Get
    End Property


    Default Public ReadOnly Property Item(ByVal columnName As String) As String Implements IDataErrorInfo.Item
        Get
            If _validationErrors.ContainsKey(columnName) Then
                Return _validationErrors(columnName)
            Else
                Return String.Empty
            End If
        End Get
    End Property


End Class
