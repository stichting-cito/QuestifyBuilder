Imports Questify.Builder.Security
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic

Public Class General_TestPackagePropertiesEditor
    Implements ITestPackageEditorPropertyEditor

    Private _testPackageModel As TestPackage
    Private _testPackage As GeneralTestPackage


    Public Sub New()
        InitializeComponent()

    End Sub

    Public Property CurrentDataSource() As TestPackage Implements ITestPackageEditorPropertyEditor.CurrentDataSource
        Get
            Return _testPackageModel
        End Get
        Set(ByVal value As TestPackage)
            If value IsNot Nothing Then
                _testPackageModel = value

                If value IsNot Nothing Then
                    _testPackage = New GeneralTestPackage(value)
                Else
                    _testPackage = Nothing
                End If

                ControlBindingSource.DataSource = _testPackage
            End If
        End Set
    End Property

    Public Overrides ReadOnly Property FrameTitle() As String
        Get
            Return My.Resources.General_TestPackagePropertiesEditor_FrameTitle
        End Get
    End Property

    Public Overrides ReadOnly Property HasFieldsToFillByUser() As Boolean
        Get
            Return True
        End Get
    End Property

    Private Sub ChangeCodeButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangeCodeButton.Click
        OnCommandExecuteRequest(New CommandExecuteRequestEventArgs(TestEditorCommands.ChangeTestCode))
    End Sub

    Public Overrides Sub HandleTestPackageDesignPermissionChange(ByVal permission As TestDesignPermission)
        For Each ctrl As System.Windows.Forms.Control In DetailTableLayoutPanel.Controls
            If permission = TestDesignPermission.Minimal Then
                ctrl.Enabled = ctrl Is TitleLabel OrElse ctrl Is TitleTextBox
            End If
        Next
    End Sub

    Public Sub ToggleCodeField(ByVal enabled As Boolean)
        CodeTextBox.Enabled = enabled
    End Sub



End Class