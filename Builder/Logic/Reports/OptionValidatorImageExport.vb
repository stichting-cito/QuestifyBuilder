Imports System.ComponentModel

Public Class OptionValidatorImageExport
    Inherits OptionValidatorExportBase

    Private Const FIELD_FOLDERTOEXPORT As String = "FolderToExport"

    Private _exportPath As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
    Private _folderToExport As String

    Private Overloads Function ValidateThis(ByVal field As String, ByVal value As String) As Boolean
        Dim valid As Boolean = MyBase.ValidateThis(field, value)
        _validationErrors.Remove(field)
        If field = FIELD_EXPORTPATH Then
            Try
                If Not IO.Directory.Exists(value) Then
                    _validationErrors.Add(field, My.Resources.PleaseSelectAnExistingPath)
                    valid = False
                End If

            Catch ex As Exception
                _validationErrors.Add(field, ex.Message)
                valid = False
            End Try
        End If
        If field = FIELD_FOLDERTOEXPORT Then
            For Each c As Char In IO.Path.GetInvalidPathChars()
                If value.Contains(c) Then
                    _validationErrors.Add(field, String.Format(My.Resources.FolderContainsInvalidCharacters, c))
                    valid = False
                End If
            Next
        End If

        Return valid
    End Function

    Public Property ExportPath() As String
        Get
            Return _exportPath
        End Get
        Set(ByVal value As String)
            _exportPath = value
            Me.ValidateThis(FIELD_EXPORTPATH, _exportPath)
            OnPropertyChanged(New PropertyChangedEventArgs(FIELD_EXPORTPATH))
        End Set
    End Property

    Public Property FolderToExport() As String
        Get
            Return _folderToExport
        End Get
        Set(ByVal value As String)
            _folderToExport = value
            Me.ValidateThis(FIELD_FOLDERTOEXPORT, _folderToExport)
            OnPropertyChanged(New PropertyChangedEventArgs(FIELD_FOLDERTOEXPORT))
        End Set
    End Property
End Class
