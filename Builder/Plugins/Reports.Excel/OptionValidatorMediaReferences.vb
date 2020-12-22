Imports System.IO
Imports Questify.Builder.Logic

Public Class OptionValidatorMediaReferences
    inherits OptionsValidatorBase

    Private _filename As String
    Private _overwriteExisting As Boolean
    Private _exportPath As String

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
        Set(ByVal value As Boolean)
            _overwriteExisting = value
            Me.ValidateThis(FIELD_EXPORTPATH, _exportPath)
        End Set
    End Property

    Public Sub ClearError()
        _validationErrors.Clear()
    End Sub

    Private Sub ValidateThis(ByVal field As String, ByVal value As String)
        _validationErrors.Remove(field)

        If String.IsNullOrEmpty(value) Then
            If Not _validationErrors.ContainsKey(field) Then
                _validationErrors.Add(field, String.Format(My.Resources.FieldEmpty, field))
                Return
            End If
        End If

        If field = FIELD_EXPORTPATH Then
            Try
                Dim fi As New FileInfo(value)

                If fi.DirectoryName.IndexOfAny(Path.GetInvalidPathChars()) >= 0 OrElse
                        fi.Name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0 Then
                    _validationErrors.Add(field, My.Resources.PathOrFilenameNotValid)
                End If

                If Not Directory.Exists(fi.DirectoryName) Then
                    _validationErrors.Add(field, My.Resources.PathNotValid)
                End If

                If Not OverwriteExisting AndAlso File.Exists(fi.FullName) Then
                    _validationErrors.Add(field, My.Resources.FileExists)
                End If
            Catch ex As Exception
                _validationErrors.Add(field, ex.Message)
            End Try

            If Not value.EndsWith(".xlsx", StringComparison.CurrentCultureIgnoreCase) Then
                _validationErrors.Add(field, My.Resources.FilenameNotValid)
            End If
        End If
        Return
    End Sub

End Class
