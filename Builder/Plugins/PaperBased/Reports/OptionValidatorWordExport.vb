Imports System.ComponentModel
Imports Questify.Builder.Logic

Public Class OptionValidatorWordExport
    Inherits OptionValidatorExportBase

    Private Const FIELD_NUMBER_OF_SELECTED_INFORMATION_BLOCKS As String = "NumberOfSelectedInformatieBlocks"
    Private Const FIELD_EXPORTPATH As String = "ExportPath"

    Private _overwriteExisting As Boolean = False
    Private _numberOfSelectedInformationBlocks As Integer
    Private _itemOnNewPage As Boolean = False
    Private _pageOrientationLandscape As Boolean = False
    Private _exportPath As String
    Private ReadOnly _allowedExentions() As String = {".docx"}
    Private _filename As String



    Private Overloads Function ValidateThis(ByVal field As String, ByVal value As String) As Boolean
        _validationErrors.Remove(field)
        Dim valid As Boolean = MyBase.ValidateThis(field, value)
        Dim stringValue As String = value
        If field = FIELD_EXPORTPATH Then
            Try
                Dim fi As New IO.FileInfo(value)

                If fi.DirectoryName.IndexOfAny(IO.Path.GetInvalidPathChars()) >= 0 OrElse fi.Name.IndexOfAny(IO.Path.GetInvalidFileNameChars()) >= 0 Then
                    _validationErrors.Add(field, My.Resources.PathOrFilenameNotValid)
                    valid = False
                End If

                If Not IO.Directory.Exists(fi.DirectoryName) Then
                    _validationErrors.Add(field, My.Resources.PleaseSelectAnExistingPath)
                    valid = False
                End If
            Catch ex As Exception
                _validationErrors.Add(field, ex.Message)
                valid = False
            End Try

            Dim extensionAllowed As Boolean
            For Each allowedExtension As String In _allowedExentions
                extensionAllowed = value.EndsWith(allowedExtension, StringComparison.CurrentCultureIgnoreCase)
                If extensionAllowed Then
                    Exit For
                End If
            Next
            If Not extensionAllowed Then
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
            OnPropertyChanged(New PropertyChangedEventArgs("Filename"))
        End Set
    End Property

    Public Property NumberOfSelectedInformationBlocks() As Integer
        Get
            Return _numberOfSelectedInformationBlocks
        End Get
        Set(ByVal value As Integer)
            _numberOfSelectedInformationBlocks = value
            Me.ValidateThis(FIELD_NUMBER_OF_SELECTED_INFORMATION_BLOCKS, _numberOfSelectedInformationBlocks.ToString)
            OnPropertyChanged(New PropertyChangedEventArgs("NumberOfSelectedInformationBlocks"))
        End Set
    End Property

    Public Property ItemOnNewPage() As Boolean
        Get
            Return _itemOnNewPage
        End Get
        Set(ByVal value As Boolean)
            _itemOnNewPage = value
            OnPropertyChanged(New PropertyChangedEventArgs("ItemOnNewPage"))
        End Set
    End Property

    Public Property PageOrientationLandscape() As Boolean
        Get
            Return _pageOrientationLandscape
        End Get
        Set(ByVal value As Boolean)
            _pageOrientationLandscape = value
            OnPropertyChanged(New PropertyChangedEventArgs("PageOrientationLandscape"))
        End Set
    End Property


    Public Property ShouldItemInformationBeAddedToTheReport As Boolean = False

    Public Property ShouldItemCustomPropertiesBeAddedToTheReport As Boolean = False

    Public Property ShouldItemSolutionBeAddedToTheReport As Boolean = False


    Public Property ShouldReferencesBeAddedToTheReport As Boolean = False


    Public Property ShouldDependenciesBeAddedToTheReport As Boolean = False


    Public Property ShouldAddItemContent As Boolean = False

    Public Property ShouldAddItemAnalyses As Boolean = False

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

    Public Property ShouldShowPreprocessorRules As Boolean = False

    Public Property ShouldAddChoiceAlternatives As Boolean

    Public Property AddChoiceAlternativesOptionVisible As Boolean = False


End Class
