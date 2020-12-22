Imports Cito.Tester.ContentModel
Imports Questify.Builder.Configuration
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.Common
Imports System.Windows.Forms
Imports System.Linq
Imports System.IO
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.Service.Classes
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.UI

Public Class SelectInformationBlocks


    Private _shouldShowTargetSpecificOptionWhenContentShouldPublished As Boolean = False
    Private _selectedHandler As IItemPreviewHandler
    Private _cbtExtraOptions As CBTExtraOptions
    Private ReadOnly _bankId As Integer
    Private ReadOnly _publicationProperties As List(Of PublicationProperty)
    Private _selectedItemCodes As List(Of String)
    Private _handlers As List(Of IItemPreviewHandler)



    Public Sub New(ByVal datasource As OptionValidatorWordExport, ByVal bankId As Integer, ByVal itemcodes As List(Of String), handlers As List(Of IItemPreviewHandler), publicationProperties As List(Of PublicationProperty))
        InitializeComponent()

        Me.OptionValidatorWordExportBindingSource.DataSource = datasource
        _bankId = bankId
        _selectedItemCodes = itemcodes
        _handlers = handlers
        _publicationProperties = publicationProperties
    End Sub



    Private Sub SelectInformationBlocks_Load()
        If TargetSpecificOptionsPanel.Controls.Count = 0 AndAlso _selectedHandler IsNot Nothing Then
            _cbtExtraOptions = New CBTExtraOptions(_selectedHandler.Dimensions, _bankId)
            _cbtExtraOptions.OptionValidatorWordExportBindingSource.DataSource = OptionValidatorWordExportBindingSource.DataSource

            TargetSpecificOptionsPanel.BringToFront()
            TargetSpecificOptionsPanel.Controls.Add(_cbtExtraOptions)
        End If
    End Sub

    Private Sub InitialiseTargetCombobox()
        SelectTargetComboBox.ValueMember = "Value"
        SelectTargetComboBox.DisplayMember = "Key"

        Dim optionValidator As OptionValidatorWordExport = DirectCast(OptionValidatorWordExportBindingSource.Current, OptionValidatorWordExport)
        Dim currentHandler As IItemPreviewHandler = optionValidator.SelectedHandler

        Dim handlers = ReportHelperClass.GetTargetDictionaryByHandlers(optionValidator.Handlers)
        SelectTargetComboBox.DataSource = New BindingSource(handlers, Nothing)

        If currentHandler IsNot Nothing Then
            If handlers.Keys.Contains(currentHandler.UserFriendlyName) Then
                SelectTargetComboBox.SelectedValue = handlers(currentHandler.UserFriendlyName)
            End If
        End If
    End Sub

    Private Function ChooseTarget() As Boolean
        Return Not (DirectCast(OptionValidatorWordExportBindingSource.DataSource, OptionValidatorWordExport).Handlers IsNot Nothing AndAlso
            DirectCast(OptionValidatorWordExportBindingSource.DataSource, OptionValidatorWordExport).Handlers.Count <= 1)
    End Function

    Private Function ShowAddInlineChoiceAlternativesCheckbox() As Boolean
        Dim itemCollection = ResourceFactory.Instance.GetItemsByCodes(_selectedItemCodes, _bankId, New ItemResourceRequestDTO())
        For Each itemCode As String In _selectedItemCodes
            Dim itemEntity As ItemResourceEntity = itemCollection(_selectedItemCodes.IndexOf(itemCode))

            itemEntity = ResourceFactory.Instance.GetItem(itemEntity, New ResourceRequestDTO())
            If itemEntity IsNot Nothing Then
                Dim itm = GetAssessmentItem(itemEntity)
                If itm IsNot Nothing AndAlso itm.Parameters.DeepFetchInlineScoringParameters.Any(Function(sp) TypeOf sp Is InlineChoiceScoringParameter) Then
                    Return True
                End If
            End If
        Next
        Return False
    End Function

    Private Function GetAssessmentItem(ByVal entity As ResourceEntity) As AssessmentItem
        Dim data As ResourceDataEntity = ResourceFactory.Instance.GetResourceData(entity)

        Dim item As AssessmentItem
        Dim resourceStream As MemoryStream = New MemoryStream(data.BinData)
        item = DirectCast(SerializeHelper.XmlDeserializeFromStream(resourceStream, GetType(AssessmentItem)), AssessmentItem)

        Return item
    End Function

    Private Function GetNumberOfSelectedInformationBlocks() As Integer
        Dim returnValue As Integer = 0
        For Each c As Windows.Forms.Control In GroupBox.Controls
            GetCheckBoxValue(returnValue, c)
        Next
        Return returnValue
    End Function

    Private Sub GetCheckBoxValue(ByRef numberOfSelectedInformationBlocks As Integer, ByVal control As Windows.Forms.Control)
        If control.GetType Is GetType(Windows.Forms.CheckBox) Then
            If DirectCast(control, Windows.Forms.CheckBox).Checked Then
                numberOfSelectedInformationBlocks += 1
            End If
        End If
        If control.HasChildren Then
            For Each childControl As Windows.Forms.Control In control.Controls
                GetCheckBoxValue(numberOfSelectedInformationBlocks, childControl)
            Next
        End If
    End Sub

    Private Sub LoadUserSettings()
        LoadSelectedCheckBoxesFromUserSettings()
        Dim target As String = UserSettings.GetUserWizardSettingsForWizard("WordReport").GetSettingsForControl(SelectTargetComboBox.Name)
        Dim selectIndex = SelectTargetComboBox.FindStringExact(target)
        If selectIndex > 0 Then
            SelectTargetComboBox.SelectedIndex = selectIndex
        ElseIf SelectTargetComboBox.Items.Count > 0 Then
            SelectTargetComboBox.SelectedIndex = 0
        End If
    End Sub

    Private Sub LoadSelectedCheckBoxesFromUserSettings()
        Dim selectedInformationBlocks As List(Of String) = UserSettings.GetUserWizardSettingsForWizard("WordReport").GetTabSettingsForTab("SelectInformationBlock")

        For Each c As Windows.Forms.Control In GroupBox.Controls
            SetSelectedInformationBlocks(selectedInformationBlocks, c)
        Next

        If selectedInformationBlocks.Contains(ItemOnNewPageCheckBox.Name) Then
            ItemOnNewPageCheckBox.Checked = True
        End If
        If selectedInformationBlocks.Contains(PageOrientationLandscapeCheckbox.Name) Then
            PageOrientationLandscapeCheckbox.Checked = True
        End If
    End Sub

    Private Sub SetSelectedInformationBlocks(ByVal selectedInformationBlocks As List(Of String), ByVal control As Windows.Forms.Control)
        If control.GetType Is GetType(Windows.Forms.CheckBox) Then
            If selectedInformationBlocks.Contains(control.Name) Then
                DirectCast(control, Windows.Forms.CheckBox).Checked = True
            End If
        End If
        If control.HasChildren Then
            For Each childControl As Windows.Forms.Control In control.Controls
                SetSelectedInformationBlocks(selectedInformationBlocks, childControl)
            Next
        End If
    End Sub

    Private Sub StoreCheckboxSettingsInUserSettings()
        Dim selectedInformationBlocks As New List(Of String)()

        For Each c As Windows.Forms.Control In GroupBox.Controls
            GetSelectedInformationBlocks(selectedInformationBlocks, c)
        Next

        If ItemOnNewPageCheckBox.Checked Then
            selectedInformationBlocks.Add(ItemOnNewPageCheckBox.Name)
        End If
        If PageOrientationLandscapeCheckbox.Checked Then
            selectedInformationBlocks.Add(PageOrientationLandscapeCheckbox.Name)
        End If

        UserSettings.GetUserWizardSettingsForWizard("WordReport").SetTabSettingsForTab("SelectInformationBlock", selectedInformationBlocks)
        UserSettings.StoreUserWizardSettings()
    End Sub

    Private Sub GetSelectedInformationBlocks(ByRef selectedInformationBlocks As List(Of String), ByVal control As Windows.Forms.Control)
        If control.GetType Is GetType(Windows.Forms.CheckBox) Then
            If DirectCast(control, Windows.Forms.CheckBox).Checked Then
                selectedInformationBlocks.Add(control.Name)
            End If
        End If
        If control.HasChildren Then
            For Each childControl As Windows.Forms.Control In control.Controls
                GetSelectedInformationBlocks(selectedInformationBlocks, childControl)
            Next
        End If
    End Sub



    Private Sub SelectInformationBlocks_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim optionValidator As OptionValidatorWordExport = DirectCast(OptionValidatorWordExportBindingSource.DataSource, OptionValidatorWordExport)
        If (optionValidator.Handlers Is Nothing OrElse optionValidator.Handlers.Count = 0) Then
            ItemContentCheckBox.Visible = False
            ItemContentCheckBox.Checked = False
        End If

        LoadUserSettings()

        SelectInformationBlocks_Load()
    End Sub

    Private Sub SelectInformationBlocks_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        Dim datasource As OptionValidatorWordExport = DirectCast(OptionValidatorWordExportBindingSource.Current, OptionValidatorWordExport)
        datasource.NumberOfSelectedInformationBlocks = GetNumberOfSelectedInformationBlocks()

        Dim errorMessage As String = datasource.Item("NumberOfSelectedInformatieBlocks")
        If Not String.IsNullOrEmpty(errorMessage) Then
            ErrorProvider.SetError(ItemInformationCheckBox, errorMessage)
        End If

        datasource.SelectedHandler = _selectedHandler
        If _selectedHandler IsNot Nothing AndAlso (_selectedHandler.Dimensions Is Nothing OrElse _selectedHandler.Dimensions.Count = 0) Then
            datasource.Size = String.Empty
        End If

        Me.ValidateChildren()

        StoreCheckboxSettingsInUserSettings()
    End Sub

    Private Sub ItemContentCheckBox_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ItemContentCheckBox.CheckStateChanged
        TargetSpecificOptionsPanel.Visible = DirectCast(sender, Windows.Forms.CheckBox).Checked AndAlso _shouldShowTargetSpecificOptionWhenContentShouldPublished
        SelectTargetLabel.Visible = DirectCast(sender, Windows.Forms.CheckBox).Checked AndAlso ChooseTarget()
        SelectTargetComboBox.Visible = DirectCast(sender, Windows.Forms.CheckBox).Checked AndAlso ChooseTarget()
        Dim datasource As OptionValidatorWordExport = DirectCast(OptionValidatorWordExportBindingSource.Current, OptionValidatorWordExport)
        datasource.AddChoiceAlternativesOptionVisible = ShowAddInlineChoiceAlternativesCheckbox()
        AddChoiceAlternativesCheckbox.Visible = DirectCast(sender, Windows.Forms.CheckBox).Checked AndAlso datasource.AddChoiceAlternativesOptionVisible
        _selectedHandler = Nothing
        InitialiseTargetCombobox()
        If TargetSpecificOptionsPanel.Visible Then
            SelectInformationBlocks_Load()
        End If
    End Sub

    Private Sub SelectTargetComboBox_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles SelectTargetComboBox.SelectedValueChanged
        If SelectTargetComboBox.SelectedValue IsNot Nothing AndAlso Me.ItemContentCheckBox.Checked Then
            SelectTargetComboBox.DataBindings("SelectedValue").WriteValue()

            _selectedHandler = DirectCast(SelectTargetComboBox.SelectedValue, IItemPreviewHandler)

            SelectInformationBlocks_Load()
            If _selectedHandler.Dimensions IsNot Nothing Then
                _shouldShowTargetSpecificOptionWhenContentShouldPublished = True
            Else
                _shouldShowTargetSpecificOptionWhenContentShouldPublished = False
            End If
            TargetSpecificOptionsPanel.Visible = _shouldShowTargetSpecificOptionWhenContentShouldPublished
            If _cbtExtraOptions IsNot Nothing Then
                _cbtExtraOptions.PopulateDimensionsComboBox(_selectedHandler.Dimensions)
            End If
        End If
    End Sub

    Private Sub SelectTargetComboBox_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles SelectTargetComboBox.SelectionChangeCommitted
        Dim setting As String = DirectCast(SelectTargetComboBox.SelectedItem, KeyValuePair(Of String, IItemPreviewHandler)).Key
        UserSettings.GetUserWizardSettingsForWizard("WordReport").AddSettingsForControl(SelectTargetComboBox.Name, setting)
    End Sub


End Class

