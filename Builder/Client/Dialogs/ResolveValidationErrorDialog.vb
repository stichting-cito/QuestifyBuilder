Imports Questify.Builder.Logic.Chain
Imports Questify.Builder.Logic.TestConstruction.Helpers
Imports Questify.Builder.Logic.TestConstruction.ChainHandlers.Validating
Imports Cito.Tester.ContentModel.Datasources

Public Class ResolveValidationErrorDialog


    Private ReadOnly _chainHandlerException As ChainHandlerException
    Private ReadOnly _resolutionsAvailable As TestConstructionValidationEventArgs.resolutionEnum



    Sub New(ByVal chainHandlerException As ChainHandlerException, ByVal resolutionsAvailable As TestConstructionValidationEventArgs.ResolutionEnum)
        InitializeComponent()
        _chainHandlerException = chainHandlerException
        _resolutionsAvailable = resolutionsAvailable

        EnableDialogElements()
    End Sub



    Public ReadOnly Property Resolution() As TestConstructionValidationEventArgs.ResolutionEnum
        Get
            Select Case DialogResult
                Case DialogResult.OK
                    If RetryFixRadioButton.Checked Then
                        Return TestConstructionValidationEventArgs.ResolutionEnum.RetryFix
                    End If
                    If RetryIgnoreRadioButton.Checked Then
                        Return TestConstructionValidationEventArgs.ResolutionEnum.RetryIgnore
                    End If

                Case DialogResult.Cancel
                    Return TestConstructionValidationEventArgs.ResolutionEnum.Abort
            End Select
        End Get
    End Property



    Private Sub EnableDialogElements()
        Dim available As TestConstructionValidationEventArgs.ResolutionEnum = _resolutionsAvailable

        Dim numberOfVisibleRadioButtons As Integer = 0

        DialogOkButton.Enabled = False


        If (available And TestConstructionValidationEventArgs.ResolutionEnum.RetryIgnore) = TestConstructionValidationEventArgs.ResolutionEnum.RetryIgnore Then
            RetryIgnoreRadioButton.Enabled = True
            RetryIgnoreRadioButton.Visible = True
            RetryFixRadioButton.Checked = True
            DialogOkButton.Enabled = True
            numberOfVisibleRadioButtons += 1
        Else
            RetryIgnoreRadioButton.Enabled = False
            RetryFixRadioButton.Checked = False
            RetryIgnoreRadioButton.Visible = False
        End If
        If (available And TestConstructionValidationEventArgs.ResolutionEnum.RetryFix) = TestConstructionValidationEventArgs.ResolutionEnum.RetryFix Then
            RetryFixRadioButton.Enabled = True
            RetryFixRadioButton.Visible = True
            RetryFixRadioButton.Checked = True
            DialogOkButton.Enabled = True
            numberOfVisibleRadioButtons += 1
        Else
            RetryFixRadioButton.Enabled = False
            RetryFixRadioButton.Visible = False
            RetryFixRadioButton.Checked = False
        End If

        If TypeOf _chainHandlerException Is ItemRelationshipException Then
            Dim ex As ItemRelationshipException = CType(_chainHandlerException, ItemRelationshipException)
            SetMessageLabel(InclusionMessageLabel, (ex.Behaviour = DataSourceBehaviourEnum.Inclusion), ex)
            SetMessageLabel(ExclusionMessageLabel, (ex.Behaviour = DataSourceBehaviourEnum.Exclusion), ex)
        Else
            GenericMessageLabel.Text = _chainHandlerException.Message
            GenericMessageLabel.Visible = True
        End If
        TableLayoutPanel1.Visible = Not numberOfVisibleRadioButtons = 1
    End Sub

    Private Sub SetMessageLabel(ByVal messageLabel As Label, ByVal visible As Boolean, ByVal ex As ItemRelationshipException)
        With messageLabel
            .Visible = visible
            .Text = String.Format(.Text, ex.Behaviour.ToString, ex.IdentifierOfSelection, ex.ConflictingResourceRefs.Count)
        End With
    End Sub



End Class