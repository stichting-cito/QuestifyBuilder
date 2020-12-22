Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.ContentModel
Imports System.ComponentModel
Imports System.Linq
Imports Questify.Builder.Logic.Service.Factories
Imports System.IO
Imports Questify.Builder.Logic
Imports System.Windows.Forms
Imports Questify.Builder.UI

Public Class ChoosePrintFormControl

    <Description("This event will be raised when a new template is chosen."), _
Category("ChoosePrintFormControl Events")> _
    Public Event PrintFormAdded As EventHandler(Of PrintFormEventArgs)
    <Description("This event will be raised when a template is removed."), _
Category("ChoosePrintFormControl Events")> _
    Public Event PrintFormRemoved As EventHandler(Of PrintFormEventArgs)

    <Description("This event will be raised when the control needs a resource from the bank."), _
Category("AssessmentTestPropertiesEditor events")> _
    Public Event ResourceNeeded As EventHandler(Of ResourceNeededEventArgs)

    Private _testEntity As AssessmentTestResourceEntity
    Private _printForm As PrintForm

    Private Const _filter As String = "application/x-zip-compressed|application/vnd.openxmlformats-officedocument.wordprocessingml.document|application/vnd.openxmlformats-officedocument.wordprocessingml.template"



    Public Sub New()
        InitializeComponent()
    End Sub


    Public Sub InitializeEditorAndSetDataSource(ByVal testEntity As AssessmentTestResourceEntity, ByVal printForm As PrintForm)
        _testEntity = testEntity
        _printForm = printForm
        PrintFormBindingSource.DataSource = printForm
    End Sub



    Private Sub PrintFormLabelTextBox_Validating(sender As Object, e As CancelEventArgs) Handles PrintFormLabelTextBox.Validating
        Dim printFormLabel = PrintFormLabelTextBox.Text
        Dim errorString As String = Nothing

        If String.IsNullOrEmpty(printFormLabel) Then
            errorString = My.Resources.ChoosePrintFormControl_LabelIsRequired
        ElseIf printFormLabel.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0 Then
            Dim InvalidCharsAsString As New String(Path.GetInvalidFileNameChars().Where(Function(x) Not Char.IsControl(x)).ToArray())

            errorString = String.Format(My.Resources.ChoosePrintFormControl_LabelMayNotContainFollowingCharacters, InvalidCharsAsString)
        ElseIf printFormLabel.Length > 30 Then
            errorString = String.Format(My.Resources.ChoosePrintFormControl_LabelMaxLength, 30)
        End If

        ErrorProvider1.SetIconAlignment(TableLayoutPanel1, ErrorIconAlignment.MiddleLeft)
        ErrorProvider1.SetError(TableLayoutPanel1, errorString)
    End Sub

    Private Sub ResourceParameterTextBox_Validating(sender As Object, e As CancelEventArgs) Handles ResourceParameterTextBox.Validating
        If _printForm.Type = PrintFormType.UserDefinedBooklet AndAlso Not String.IsNullOrWhiteSpace(PrintFormLabelTextBox.Text) Then
            Dim errorString As String = Nothing

            If String.IsNullOrEmpty(ResourceParameterTextBox.Text) Then
                errorString = My.Resources.ChoosePrintFormControl_TemplateIsRequired

                ErrorProvider1.SetIconAlignment(ResourceParameterTextBox, ErrorIconAlignment.MiddleRight)
                ErrorProvider1.SetError(ResourceParameterTextBox, errorString)
            Else
                ErrorProvider1.SetError(ResourceParameterTextBox, Nothing)
            End If
        End If
    End Sub

    Private Sub SelectResourceButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectResourceButton.Click
        Using dialog As New SelectMediaResourceDialog(_testEntity.BankId)
            AddHandler dialog.ResourcesLoaded, AddressOf SelectMediaResourceDialog_DialogResourcesLoaded
            AddHandler dialog.ResourceNeeded, AddressOf SelectMediaResourceDialog_ResourceNeeded
            dialog.Filter = _filter
            dialog.CanPickFiles = True

            If dialog.ShowDialog() = DialogResult.OK AndAlso dialog.SelectedEntity.name <> _printForm.ResourceName Then
                If Not dialog.EntitiesProhibitedToSelect.Contains(dialog.SelectedEntity.resourceId) Then
                    OnRemovingResource(_printForm.ResourceName)
                    _printForm.ResourceName = dialog.SelectedEntity.name
                    OnAddingResource(_printForm.ResourceName)
                    Me.ValidateChildren()
                Else
                    MessageBox.Show(My.Resources.SelectResourceDialog_CannotSelectBecauseOfStatus, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            End If
        End Using
    End Sub

    Private Sub DeleteResourceButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteResourceButton.Click
        OnRemovingResource(_printForm.ResourceName)
        _printForm.ResourceName = Nothing
        ResourceParameterTextBox.Text = String.Empty
        Me.ValidateChildren()
    End Sub

    Private Sub OnAddingResource(ByVal resourceToAdd As String)
        If Not String.IsNullOrEmpty(resourceToAdd) Then
            Dim e As New PrintFormEventArgs(_printForm)
            RaiseEvent PrintFormAdded(Nothing, e)
        End If
    End Sub

    Private Sub OnRemovingResource(ByVal resourceToRemove As String)
        If Not String.IsNullOrEmpty(resourceToRemove) Then
            Dim e As New PrintFormEventArgs(_printForm)
            RaiseEvent PrintFormRemoved(Nothing, e)
        End If
    End Sub

    Private Sub SelectMediaResourceDialog_DialogResourcesLoaded(ByVal sender As Object, ByVal e As ResourceEventArgs)
        If Not String.IsNullOrEmpty(_printForm.ResourceName) Then
            If Not (_testEntity.GetDependentResourceByName(_printForm.ResourceName) Is Nothing) Then
                e.Resource = DtoFactory.Generic.Get(_testEntity.GetDependentResourceByName(_printForm.ResourceName).DependentResource.ResourceId)
            End If
        End If
    End Sub


    Private Sub SelectMediaResourceDialog_ResourceNeeded(ByVal sender As Object, ByVal e As ResourceNeededEventArgs)
        OnResourceNeeded(e)
    End Sub

    Protected Overridable Sub OnResourceNeeded(ByVal e As ResourceNeededEventArgs)
        RaiseEvent ResourceNeeded(Me, e)
    End Sub


End Class
