Imports System.ComponentModel
Imports Questify.Builder.Logic.Service.Interfaces

Public MustInherit Class OptionValidatorExportBase
    Inherits OptionsValidatorBase
    Implements INotifyPropertyChanged

    Private Const FIELD_SELECTED_TARGET As String = "SelectedTarget"
    Private Const FIELD_SELECTED_SIZE As String = "Size"

    Private _size As String
    Private _selectedHandler As IItemPreviewHandler

    Protected Sub OnPropertyChanged(ByVal e As PropertyChangedEventArgs)
        RaiseEvent PropertyChanged(Me, e)
    End Sub

    Public Event PropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged

    Protected Function ValidateThis(ByVal field As String, ByVal value As String) As Boolean
        Dim valid As Boolean = True
        _validationErrors.Remove(field)

        If field = FIELD_SELECTED_SIZE AndAlso _selectedHandler IsNot Nothing AndAlso _selectedHandler.PreviewTarget = "Word" Then
        ElseIf String.IsNullOrEmpty(value) Then
            If Not _validationErrors.ContainsKey(field) Then
                _validationErrors.Add(field, String.Format(My.Resources.FieldEmpty, field))
                valid = False
            End If
        End If

        Return valid
    End Function

    Public Sub ClearErrors()
        _validationErrors.Clear()
    End Sub

    Public Property Size() As String
        Get
            Return _size
        End Get
        Set(ByVal value As String)
            _size = value
            Me.ValidateThis(FIELD_SELECTED_SIZE, _size)
        End Set
    End Property

    Public Property Handlers() As List(Of IItemPreviewHandler)


    Public Property SelectedHandler() As IItemPreviewHandler
        Get
            Return _selectedHandler
        End Get
        Set(ByVal value As IItemPreviewHandler)
            _selectedHandler = value
            If _selectedHandler IsNot Nothing Then
                Me.ValidateThis(FIELD_SELECTED_TARGET, _selectedHandler.PreviewTarget)
            End If
        End Set
    End Property

    Public Overridable Sub ClearError()
        _validationErrors.Clear()
    End Sub

End Class
