Imports Questify.Builder.Logic.ResourceManager

Public Class FilterEditorBase


    Private _filter As FilterPredicate
    Private _resourceManager As DataBaseResourceManager



    Public Sub New()
        InitializeComponent()

    End Sub



    Public Overridable Property Filter() As FilterPredicate
        Get
            Return _filter
        End Get
        Set(ByVal value As FilterPredicate)
            _filter = value
        End Set
    End Property

    Public Overridable Property ResourceManager() As DataBaseResourceManager
        Get
            Return _resourceManager
        End Get
        Set(ByVal value As DataBaseResourceManager)
            _resourceManager = value
        End Set
    End Property


End Class