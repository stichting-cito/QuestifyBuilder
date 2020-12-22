Imports System.Drawing
Imports System.Windows.Forms
Imports System.Linq
Imports Questify.Builder.Configuration
Imports Questify.Builder.Logic

Public Class CBTExtraOptions

    Private ReadOnly _bankId As Integer

    Public Sub New()
        InitializeComponent()

    End Sub

    Public Sub New(dimensions As Dictionary(Of String, Size), bankId As Integer)
        Me.New()
        _bankId = bankId
        PopulateDimensionsComboBox(dimensions)
    End Sub


    Private Sub VisibiltyTheme(makeVisible As Boolean, position As Integer)
        For Each ctl As Control In TableLayoutPanel1.Controls.OfType(Of Control).Where(Function(c) TableLayoutPanel1.GetRow(c) = position)
            ctl.Visible = makeVisible
        Next
    End Sub

    Public Sub PopulateDimensionsComboBox(dimensions As Dictionary(Of String, Size))
        If dimensions IsNot Nothing Then
            Dim dynamicDimension = dimensions.FirstOrDefault(Function(d) d.Value.Height = 0 AndAlso d.Value.Width = 0)
            If dynamicDimension.Key IsNot Nothing Then dimensions.Remove(dynamicDimension.Key)
            VisibiltyTheme(dimensions IsNot Nothing, 0)
            DimensionComboBox.ValueMember = "Value"
            DimensionComboBox.DisplayMember = "Key"
            Dim comboDimensions As New Dictionary(Of String, String)
            For Each dimension In dimensions
                comboDimensions.Add(dimension.Key, String.Format("{0}|{1}", dimension.Value.Width, dimension.Value.Height))
            Next
            DimensionComboBox.DataSource = New BindingSource(comboDimensions, Nothing)

            Dim dimensionSetting = UserSettings.GetUserWizardSettingsForWizard("WordReport").GetSettingsForControl(DimensionComboBox.Name)
            Dim selectIndex = DimensionComboBox.FindStringExact(dimensionSetting)
            If selectIndex > 0 Then
                DimensionComboBox.SelectedIndex = selectIndex
            ElseIf DimensionComboBox.Items.Count > 0 Then
                DimensionComboBox.SelectedIndex = 0
            End If
            CBTExtraOptions_Validating(Me, Nothing)
        End If
    End Sub

    Private Sub CBTExtraOptions_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        Dim datasource As OptionValidatorExportBase = TryCast(Me.OptionValidatorWordExportBindingSource.DataSource, OptionValidatorExportBase)
        If Not datasource Is Nothing AndAlso Not String.IsNullOrEmpty(datasource.Item("Size")) Then
            ErrorProvider.SetError(DimensionComboBox, datasource.Item("Size"))
        End If
    End Sub

    Private Sub Combobox_SaveToUserSettings(sender As Object, e As EventArgs) Handles DimensionComboBox.SelectionChangeCommitted
        Dim combo As ComboBox = DirectCast(sender, ComboBox)
        Dim selectedText = DirectCast(combo.SelectedItem, KeyValuePair(Of String, String)).Key
        UserSettings.GetUserWizardSettingsForWizard("WordReport").AddSettingsForControl(combo.Name, selectedText)
    End Sub
End Class