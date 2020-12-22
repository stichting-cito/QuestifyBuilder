

Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel.Datasources
Imports Enums
Imports Questify.Builder.Logic.Service.Exceptions

Public Class AddTemplateDialog


    Dim Shared ReadOnly specificHandlers As New Dictionary(Of Type, IProcessor)()

    Shared Sub New()
        specificHandlers.Add(GetType(DataSourceResourceEntity), New DataSourceProcessor())
    End Sub



    Private ReadOnly _dialogInitializations As New Dictionary(Of Type, dialogInitializationDelegate)
    Private _resourceCode As String
    Private _resourceDescription As String
    Private _resourceTitle As String
    Private _resourceType As String
    Private ReadOnly _type As Type



    Public Sub New(ByVal type As Type)
        InitializeComponent()

        _type = type

        InitDefaults()

        If _dialogInitializations.ContainsKey(_type) Then
            Dim initDelegate As dialogInitializationDelegate = _dialogInitializations(_type)
            initDelegate()
        Else
            Throw New UIException("Type not supported")
        End If
    End Sub



    Private Delegate Function DialogInitializationDelegate() As Boolean



    Public Property ResourceCode() As String
        Get
            Return _resourceCode
        End Get
        Set(ByVal value As String)
            _resourceCode = value
        End Set
    End Property

    Public Property ResourceDescription() As String
        Get
            Return _resourceDescription
        End Get
        Set(ByVal value As String)
            _resourceDescription = value
        End Set
    End Property

    Public Property ResourceTitle() As String
        Get
            Return _resourceTitle
        End Get
        Set(ByVal value As String)
            _resourceTitle = value
        End Set
    End Property

    Public Property ResourceType() As String
        Get
            Return _resourceType
        End Get
        Set(ByVal value As String)
            _resourceType = value
        End Set
    End Property



    Private Sub Cancel_Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Cancel_Button.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub CheckCode()
        Dim value As String = CodeTextBox.Text
        Dim illegalChars As String = "`~!@#$%^&*()=+[]{}'"";:<>,/?\|"
        If value.IndexOfAny(illegalChars.ToCharArray()) > -1 Then
            MessageBox.Show(My.Resources.WizardForm_CodeFieldCannotContainIllegalChars, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CodeTextBox.Focus()
        End If
    End Sub

    Private Sub CodeTextBox_Leave(ByVal sender As Object, ByVal e As EventArgs) Handles CodeTextBox.Leave
        CheckCode()
    End Sub

    Private Sub InitDefaults()
        _dialogInitializations.Add(GetType(ItemLayoutTemplateResourceEntity), AddressOf InitForTypeItemLayoutTemplateResourceEntity)
        _dialogInitializations.Add(GetType(ControlTemplateResourceEntity), AddressOf InitForTypeControlTemplateResourceEntity)
        _dialogInitializations.Add(GetType(DataSourceResourceEntity), AddressOf InitForTypeDataSourceResourceEntity)
    End Sub

    Private Function InitForTypeControlTemplateResourceEntity() As Boolean
        ItemTypeComboBox.Visible = False
        ItemTypeLabel.Visible = False
        Text = My.Resources.AddAControlTemplate
    End Function

    Private Function InitForTypeDataSourceResourceEntity() As Boolean
        ItemTypeComboBox.Visible = True
        ItemTypeLabel.Visible = True
        Text = My.Resources.AddASelectionTemplate

        ItemTypeComboBox.DataSource = ResourceEnumConverter.GetValues(GetType(DataSourceBehaviourEnum))
        ItemTypeComboBox.DisplayMember = "Value"
        ItemTypeComboBox.ValueMember = "Key"
    End Function

    Private Function InitForTypeItemLayoutTemplateResourceEntity() As Boolean
        ItemTypeComboBox.Visible = True
        ItemTypeLabel.Visible = True
        Text = My.Resources.AddAItemLayoutTemplate

        ItemTypeComboBox.DataSource = ResourceEnumConverter.GetValues(GetType(ItemTypeEnum))
        ItemTypeComboBox.DisplayMember = "Value"
        ItemTypeComboBox.ValueMember = "Key"
    End Function

    Private Sub OK_Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles OK_Button.Click
        Dim allRequiredFieldsFilled As Boolean = True
        DataErrorProvider.Clear()

        Dim codeError As String = ValidationHelper.IsValidResourceCode(CodeTextBox.Text)
        If Not String.IsNullOrEmpty(codeError) Then
            DataErrorProvider.SetError(CodeTextBox, codeError)
            allRequiredFieldsFilled = False
        Else
            ResourceCode = CodeTextBox.Text
        End If

        If String.IsNullOrEmpty(TitleTextBox.Text) Then
            DataErrorProvider.SetError(TitleTextBox, My.Resources.TitleIsARequiredField)
            allRequiredFieldsFilled = False
        Else
            ResourceTitle = TitleTextBox.Text
        End If

        If ItemTypeComboBox.Visible Then
            If String.IsNullOrEmpty(ItemTypeComboBox.SelectedValue.ToString) Then
                DataErrorProvider.SetError(ItemTypeComboBox, My.Resources.TypeIsARequiredField)
                allRequiredFieldsFilled = False
            Else
                Dim additionalLogic As IProcessor = Nothing
                If (specificHandlers.TryGetValue(_type, additionalLogic)) Then
                    additionalLogic.DoPostProcessing(Me)
                Else
                    ResourceType = ItemTypeComboBox.SelectedValue.ToString
                End If
            End If
        End If

        Me.ResourceDescription = DescriptionTextBox.Text
        If allRequiredFieldsFilled Then
            DialogResult = DialogResult.OK
            Close()
        Else
            MessageBox.Show(My.Resources.NotAllRequiredTemplateFieldsAreFilled, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub


End Class