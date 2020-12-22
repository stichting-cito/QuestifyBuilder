Imports System.ComponentModel
Imports System.IO
Imports System.Text

Public Class PackageExportOptionsDataEntity
    Implements IDataErrorInfo


    Private Const FIELD_EXPORTSUBBANKS As String = "ExportSubBanks"
    Private Const FIELD_PACKAGEURL As String = "PackageUrl"



    Private _packageUrl As String = String.Empty
    Private ReadOnly _validationErrors As New Dictionary(Of String, String)



    Public Property ExportSubBanks As Boolean = False


    Default Public ReadOnly Property Item(ByVal columnName As String) As String Implements IDataErrorInfo.Item
        Get
            If _validationErrors.ContainsKey(columnName) Then
                Return _validationErrors(columnName)
            Else
                Return String.Empty
            End If
        End Get
    End Property

    Public Property OverwriteExisting As Boolean = False

    Public Property PackageUrl() As String
        Get
            Return _packageUrl
        End Get
        Set(ByVal value As String)
            _packageUrl = value
            Me.ValidateThis(FIELD_PACKAGEURL, _packageUrl)
        End Set
    End Property


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




    Private Function ValidateThis(ByVal field As String, ByVal value As Object) As Boolean
        Dim valid As Boolean = True

        _validationErrors.Remove(field)

        Select Case field
            Case FIELD_PACKAGEURL

                If String.IsNullOrEmpty(DirectCast(value, String)) Then
                    _validationErrors.Add(field, String.Format(My.Resources.FieldMustBeFilled, My.Resources.FieldExportSource))
                    valid = False
                Else
                    Dim stringValue As String = DirectCast(value, String)

                    Try
                        Dim fi As New FileInfo(stringValue)
                        If Not fi.Directory.Exists Then
                            _validationErrors.Add(field, My.Resources.ExportLocationDoesNotExist)
                            valid = False
                        End If
                    Catch ex As Exception
                        _validationErrors.Add(field, ex.Message)
                        valid = False
                    End Try


                    If Not stringValue.EndsWith(".export", StringComparison.CurrentCultureIgnoreCase) Then
                        _validationErrors.Add(field, String.Format(My.Resources.PleaseEnterAValidExportFilename, My.Resources.FieldExportSource))
                        valid = False
                    End If

                    If File.Exists(stringValue) AndAlso Not Me.OverwriteExisting Then
                        _validationErrors.Add(field, String.Format(My.Resources.ExportAlreadyExistsSelectOtherExportLocation, My.Resources.FieldExportSource))
                        valid = False
                    End If
                End If
            Case Else
                _validationErrors.Add(field, String.Format(My.Resources.NoFieldValidation, field))
                valid = False
        End Select

        Return valid
    End Function


End Class