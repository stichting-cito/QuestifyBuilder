Imports Questify.Builder.Logic


Public Class OptionValidatorGridExport
    Inherits OptionsValidatorBase

    Private _overwriteExisting As Boolean = False
    Private _exportPath As String = String.Empty
    Private _filename As String


    Private Function ValidateThis(ByVal field As String, ByVal value As String) As Boolean
        Dim valid As Boolean = True
        _validationErrors.Remove(field)

        If String.IsNullOrEmpty(value) Then
            If Not _validationErrors.ContainsKey(field) Then
                _validationErrors.Add(field, String.Format(My.Resources.FieldEmpty, field))
                valid = False
            End If
        End If
        Dim stringValue As String = value
        If field = FIELD_EXPORTPATH Then
            Try
                Dim fi As New IO.FileInfo(value)

                If fi.DirectoryName.IndexOfAny(IO.Path.GetInvalidPathChars()) >= 0 OrElse fi.Name.IndexOfAny(IO.Path.GetInvalidFileNameChars()) >= 0 Then
                    _validationErrors.Add(field, My.Resources.PathOrFilenameNotValid)
                    valid = False
                End If

                If Not IO.Directory.Exists(fi.DirectoryName) Then
                    _validationErrors.Add(field, My.Resources.PathNotValid)
                    valid = False
                End If
            Catch ex As Exception
                _validationErrors.Add(field, ex.Message)
                valid = False
            End Try

            If Not value.EndsWith(".xls", StringComparison.CurrentCultureIgnoreCase) AndAlso
                Not value.EndsWith(".xlsx", StringComparison.CurrentCultureIgnoreCase) Then
                _validationErrors.Add(field, My.Resources.FilenameNotValid)
                valid = False
            End If

            If IO.File.Exists(stringValue) AndAlso Not Me.OverwriteExisting Then
                _validationErrors.Add(field, String.Format(My.Resources.FileExists, field))
                valid = False
            End If
        End If
        Return valid
    End Function



    Public Property Filename() As String
        Get
            Return _filename
        End Get
        Set(ByVal value As String)
            _filename = value
        End Set
    End Property


    Public Property ExportPath() As String
        Get
            Return _exportPath
        End Get
        Set(ByVal value As String)
            _exportPath = value
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


End Class

