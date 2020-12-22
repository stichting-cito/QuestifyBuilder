Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic

Public Class General_ItemReferencePropertiesEditor
    Implements IItemReferencePropertyEditor


    Private _itemReferenceModel As ItemReference2
    Private _itemReference As GeneralItemReference



    Public Sub New()
        InitializeComponent()

        PopulateDropdownLists()
    End Sub



    Public Property CurrentDataSource() As ItemReference2 Implements IItemReferencePropertyEditor.CurrentDataSource
        Get
            Return _itemReferenceModel
        End Get
        Set(ByVal value As ItemReference2)
            _itemReferenceModel = value

            If value IsNot Nothing Then
                _itemReference = New GeneralItemReference(value)
                ControlBindingSource.DataSource = _itemReference
            Else
                _itemReference = Nothing
                ControlBindingSource.DataSource = Nothing
            End If
        End Set
    End Property

    Public Overrides ReadOnly Property FrameTitle() As String
        Get
            Return My.Resources.General_ItemReferencePropertiesEditor_FrameTitle
        End Get
    End Property

    Public Overrides ReadOnly Property HasFieldsToFillByUser() As Boolean
        Get
            Return True
        End Get
    End Property



    Private Sub ItemReferencePropertiesEditor_DataChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.DataChanged
        ItemFunctionalTypeCombobox.Enabled = _itemReferenceModel.ItemFunctionalType <> ItemFunctionalType.Informational
    End Sub

    Private Sub PopulateDropdownLists()
        ItemFunctionalTypeCombobox.DataSource = Cito.Tester.Common.ResourceEnumConverter.GetValues(GetType(ItemFunctionalType))
        ItemFunctionalTypeCombobox.DisplayMember = "Value"
        ItemFunctionalTypeCombobox.ValueMember = "Key"
    End Sub

    Private Sub ItemFunctionalTypeCombobox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ItemFunctionalTypeCombobox.SelectedIndexChanged
        Dim seedingWeightSetManually = _itemReferenceModel IsNot Nothing AndAlso
            _itemReferenceModel.ItemFunctionalType = ItemFunctionalType.Seeding AndAlso
            _itemReferenceModel.Weight <> 0

        If _itemReferenceModel IsNot Nothing AndAlso
            Not seedingWeightSetManually AndAlso
            ItemFunctionalTypeCombobox.SelectedValue = ItemFunctionalType.Seeding Then
            _itemReferenceModel.Weight = 0
            WeightTextBox.Text = _itemReferenceModel.Weight
        End If
    End Sub




End Class