Imports System.ComponentModel
Imports System.Text
Imports System.Collections.Generic
Imports System.IO

Public Class SelectColumnsOptionsValidator
    Implements IDataErrorInfo

    Private Const FIELD_NUMBER_OF_COLUMNS As String = "SelectedColumns"
    Private Const FIELD_EXPORTPATH As String = "ExportPath"

    Private _validationErrors As New Dictionary(Of String, String)
    Private _overwriteExisting As Boolean = False
    Private _numberOfSelectedColumns As Integer
    Private _exportPath As String = String.Empty
    Private _allowedExtensions() As String = {".xlsx"}
    Private _selectedColumns As New BindingList(Of String)
    Private _availableColumns As New BindingList(Of String)
    Private _filename As String
    Private _fileLocationOnly As Boolean
    Private _includeConceptsWithoutScore As Boolean




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


    Default Public ReadOnly Property Item(columnName As String) As String Implements IDataErrorInfo.Item
        Get
            If _validationErrors.ContainsKey(columnName) Then
                Return _validationErrors(columnName)
            Else
                Return String.Empty
            End If
        End Get
    End Property

    Private Function ValidateThis(field As String, value As String) As Boolean
        Dim valid As Boolean = True
        _validationErrors.Remove(field)

        If String.IsNullOrEmpty(value) Then
            If Not _validationErrors.ContainsKey(field) Then
                _validationErrors.Add(field, String.Format(My.Resources.FieldEmpty, field))
                valid = False
            End If
        End If
        Dim stringValue As String = DirectCast(value, String)
        If field = FIELD_NUMBER_OF_COLUMNS Then
            If CType(value, Integer) = 0 Then
                _validationErrors.Add(field, String.Format(My.Resources.PleaseSelectAtLeastOneColumn, field))
                valid = False
            End If
        End If
        If field = FIELD_EXPORTPATH Then
            If Not _fileLocationOnly Then
                Try
                    Dim fi As New FileInfo(value)

                    If fi.DirectoryName.IndexOfAny(Path.GetInvalidPathChars()) >= 0 OrElse fi.Name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0 Then
                        _validationErrors.Add(field, My.Resources.PathOrFilenameNotValid)
                        valid = False
                    End If

                    If Not Directory.Exists(fi.DirectoryName) Then
                        _validationErrors.Add(field, My.Resources.PathNotValid)
                        valid = False
                    End If
                Catch ex As Exception
                    _validationErrors.Add(field, ex.Message)
                    valid = False
                End Try

                Dim extensionAllowed As Boolean
                For Each allowedExtension As String In _allowedExtensions
                    extensionAllowed = value.EndsWith(allowedExtension, StringComparison.CurrentCultureIgnoreCase)
                    If extensionAllowed Then
                        Exit For
                    End If
                Next
                If Not extensionAllowed Then
                    _validationErrors.Add(field, My.Resources.FilenameNotValid)
                    valid = False
                End If

                If File.Exists(stringValue) AndAlso Not Me.OverwriteExisting Then
                    _validationErrors.Add(field, String.Format(My.Resources.FileExists, field))
                    valid = False
                End If
            Else
                Try
                    Dim di As New DirectoryInfo(value)

                    If Not di.Name.Equals(di.Root.ToString, StringComparison.InvariantCultureIgnoreCase) AndAlso di.Name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0 Then
                        _validationErrors.Add(field, My.Resources.PathOrFilenameNotValid)
                        valid = False
                    End If

                    If Not di.Exists() Then
                        _validationErrors.Add(field, My.Resources.PathNotValid)
                        valid = False
                    End If
                Catch ex As Exception
                    _validationErrors.Add(field, ex.Message)
                    valid = False
                End Try
            End If
        End If
        Return valid
    End Function



    Public Property Filename() As String
        Get
            Return _filename
        End Get
        Set(value As String)
            _filename = value
        End Set
    End Property

    Public Property NumberOfSelectedColumns() As Integer
        Get
            Return _numberOfSelectedColumns
        End Get
        Set(value As Integer)
            _numberOfSelectedColumns = value
            Me.ValidateThis(FIELD_NUMBER_OF_COLUMNS, _numberOfSelectedColumns.ToString)
        End Set
    End Property

    Public ReadOnly Property AvailableColumns() As BindingList(Of String)
        Get
            Return _availableColumns
        End Get
    End Property

    Public ReadOnly Property SelectedColumns() As BindingList(Of String)
        Get
            Return _selectedColumns
        End Get
    End Property

    Public Property AllowedExtensions() As String()
        Get
            Return _allowedExtensions
        End Get
        Set(value As String())
            _allowedExtensions = value
        End Set
    End Property

    Public Property FileLocationOnly() As Boolean
        Get
            Return _fileLocationOnly
        End Get
        Set(value As Boolean)
            _fileLocationOnly = value
        End Set
    End Property

    Public Property ExportPath() As String
        Get
            Return _exportPath
        End Get
        Set(value As String)
            If (_exportPath <> value) Then
                _overwriteExisting = False
            End If
            _exportPath = value
            Me.ValidateThis(FIELD_EXPORTPATH, _exportPath)
        End Set
    End Property

    Public Property OverwriteExisting() As Boolean
        Get
            Return _overwriteExisting
        End Get
        Set(value As Boolean)
            _overwriteExisting = value
            Me.ValidateThis(FIELD_EXPORTPATH, _exportPath)
        End Set
    End Property

    Public Property IncludeConceptsWithoutScore As Boolean
        Get
            Return _includeConceptsWithoutScore
        End Get
        Set(value As Boolean)
            _includeConceptsWithoutScore = value
        End Set
    End Property




    Public Sub ClearError()
        _validationErrors.Clear()
    End Sub


End Class
