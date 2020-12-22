Imports Questify.Builder.Security
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic

Public Class General_AssessmentTestPropertiesEditor
    Implements IAssessmentTestPropertyEditor

    Private _assessmentTestModel As AssessmentTest2
    Private _assessmentTest As GeneralAssessmentTest

    Public Sub New()
        InitializeComponent()

    End Sub

    Public Property CurrentDataSource As AssessmentTest2 Implements IAssessmentTestPropertyEditor.CurrentDataSource
        Get
            Return _assessmentTestModel
        End Get
        Set
            If Value IsNot Nothing Then
                _assessmentTestModel = Value

                If Value IsNot Nothing Then
                    _assessmentTest = New GeneralAssessmentTest(Value)
                Else
                    _assessmentTest = Nothing
                End If

                ControlBindingSource.DataSource = _assessmentTest
            End If
        End Set
    End Property

    Public Overrides ReadOnly Property FrameTitle As String
        Get
            Return My.Resources.General_AssessmentTestPropertiesEditor_FrameTitle
        End Get
    End Property

    Public Overrides ReadOnly Property HasFieldsToFillByUser As Boolean
        Get
            Return True
        End Get
    End Property

    Private Sub ChangeCodeButton_Click(sender As Object, e As EventArgs) Handles ChangeCodeButton.Click
        OnCommandExecuteRequest(New CommandExecuteRequestEventArgs(TestEditorCommands.ChangeTestCode))
    End Sub

    Private Sub General_AssessmentTestPropertiesEditor_Load(sender As Object, e As EventArgs) Handles Me.Load
        If CodeTextBox.Enabled Then
            CodeTextBox.SelectionStart = 0
        End If
    End Sub

    Public Overrides Sub HandleTestDesignPermissionChange(permission As TestDesignPermission)
        For Each ctrl As Control In DetailTableLayoutPanel.Controls
            If permission = TestDesignPermission.Minimal Then
                ctrl.Enabled = ctrl Is TitleLabel OrElse ctrl Is TitleTextBox
            End If
        Next
    End Sub

    Public Sub ToggleCodeField(enabled As Boolean)
        CodeTextBox.Enabled = enabled
        ChangeCodeButton.Visible = Not enabled
    End Sub

End Class