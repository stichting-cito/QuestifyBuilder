Imports System.ComponentModel
Imports System.IO
Imports System.Text
Imports Questify.Builder.Plugins.Reports.Excel.My.Resources

Public Class OptionValidatorResourceExposureLog
    Implements IDataErrorInfo

    Private _filename As String
    Private _overwriteExisting As Boolean
    Private _exportPath As String
    Private _fromDate As DateTime
    Private _toDate As DateTime
    Private ReadOnly _validationErrors As New Dictionary(Of String, String)

    Private Const FIELD_EXPORTPATH As String = "ExportPath"
    Private Const FIELD_FILENAME As String = "FileName"
    Private Const FIELD_DATES As String = "Dates"

    Public Sub New()
        Me.FromDate = DateTime.Now.Date
        Me.ToDate = DateTime.Now.Date
    End Sub


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

    Default Public ReadOnly Property Item(ByVal field As String) As String Implements IDataErrorInfo.Item
        Get
            If _validationErrors.ContainsKey(field) Then
                Return _validationErrors(field)
            Else
                Return String.Empty
            End If
        End Get
    End Property


    Public Property Filename() As String
        Get
            Return _filename
        End Get
        Set(ByVal value As String)
            _filename = value
            Me.ValidateThis(FIELD_FILENAME, _filename)
        End Set
    End Property

    Public Property ExportPath() As String
        Get
            Return _exportPath
        End Get
        Set(ByVal value As String)
            If (_exportPath <> value) Then
                _overwriteExisting = False
            End If

            _exportPath = value
            Filename = value
            Me.ValidateThis(FIELD_EXPORTPATH, _exportPath)
        End Set
    End Property


    Public Property OverwriteExisting() As Boolean
        Get
            Return _overwriteExisting
        End Get
        Set(ByVal value As Boolean)
            _overwriteExisting = value
            Me.ValidateThis(FIELD_EXPORTPATH, _exportPath)
        End Set
    End Property

    Public Sub ClearError()
        _validationErrors.Clear()
    End Sub

    Public Property FromDate As DateTime
        Get
            Return _fromDate.Date
        End Get
        Set(value As DateTime)
            _fromDate = value
            Me.ValidateDates()
        End Set
    End Property

    Public Property ToDate As DateTime
        Get
            Return _toDate.Date.AddDays(1).AddSeconds(-1)
        End Get
        Set(value As DateTime)
            _toDate = value
            Me.ValidateDates()
        End Set
    End Property


    Private Sub ValidateThis(ByVal field As String, ByVal value As String)
        _validationErrors.Remove(field)

        If String.IsNullOrEmpty(value) Then
            If Not _validationErrors.ContainsKey(field) Then
                _validationErrors.Add(field, String.Format(My.Resources.FieldEmpty, field))
            End If
        End If
        If field = FIELD_EXPORTPATH Then
            Try
                If Not Directory.Exists(Path.GetDirectoryName(value)) Then
                    _validationErrors.Add(field, My.Resources.PathNotValid)
                End If

            Catch ex As Exception
                _validationErrors.Add(field, ex.Message)
            End Try
        End If
        If field = FIELD_FILENAME Then
            If Not value.EndsWith(".csv", StringComparison.CurrentCultureIgnoreCase) Then
                _validationErrors.Add(field, My.Resources.FilenameNotValid)
            End If
            If String.IsNullOrEmpty(IO.Path.GetFileNameWithoutExtension(value)) Then
                _validationErrors.Add(field, My.Resources.FilenameNotValid)
            End If

        End If
        Return
    End Sub

    Private Sub ValidateDates()
        _validationErrors.Remove(FIELD_DATES)
        If Not _toDate >= _fromDate Then
            _validationErrors.Add(FIELD_DATES, OptionValidatorResourceExposureLog_The__date_from__is_not_earlier_than__date_to_)
        End If
    End Sub

End Class