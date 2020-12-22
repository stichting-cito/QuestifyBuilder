Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic

Public Class Word_ItemReferencePropertiesEditor
    Implements IItemReferencePropertyEditor


    Private _itemReference As WordItemReference
    Private _itemRefModel As ItemReference2



    Public Sub New()
        InitializeComponent()

    End Sub



    Public Property CurrentDataSource() As ItemReference2 Implements IItemReferencePropertyEditor.CurrentDataSource
        Get
            Return _itemRefModel
        End Get
        Set(ByVal value As ItemReference2)
            If value IsNot Nothing Then
                _itemRefModel = value
                If _itemRefModel IsNot Nothing Then
                    _itemReference = New WordItemReference(value)
                    ControlBindingSource.DataSource = _itemReference

                Else
                    _itemReference = Nothing
                    ControlBindingSource.DataSource = Nothing
                End If
            End If
        End Set
    End Property

    Public Overrides ReadOnly Property FrameTitle() As String
        Get
            Return My.Resources.Word_ItemReferencePropertiesEditor_FrameTitle
        End Get
    End Property

    Public Overrides ReadOnly Property HasFieldsToFillByUser() As Boolean
        Get
            Return False
        End Get
    End Property



    Private Sub ItemReferencePropertiesEditor_DataChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.DataChanged
    End Sub

End Class