Imports System.Threading
Imports System.Windows.Forms
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Security

Public Class PasswordChangeDialog
    Dim User as UserEntity
    Public Sub New()

        InitializeComponent()

        IntroLabel.Text = My.Resources.NewPasswordIntro
        NewPasswordLabel.Text = My.Resources.NewPassword
        RepeatPasswordLabel.Text = My.Resources.RepeatPassword

        Me.Text = My.Resources.ChangePasswordDialogTitle

        Dim currentUser = New UserEntity(CType(Thread.CurrentPrincipal.Identity, TestBuilderIdentity).UserId)
        User = AuthorizationFactory.Instance.GetUserWithRoles(currentUser, True)

    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If (String.IsNullOrEmpty(PasswordTextBox.Text))
            PolicyLabel.Text = My.Resources.PasswordCannotBeEmpty
            Return
        End If
        If Not PasswordTextBox.Text.Equals(RepeatPasswordTextBox.Text) Then
            PolicyLabel.Text = My.Resources.PasswordsDoNotMatch
            Return
        End If
        Dim policy As New PasswordPolicy
        If Not policy.IsValid(PasswordTextBox.Text, User.UserName) Then
            PolicyLabel.Text = My.Resources.Passwordvalidate
            Return
        End If
        User.Password = PasswordTextBox.Text
        User.ChangePassword = False
        AuthorizationFactory.Instance.UpdateUser(User)
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        DialogResult = DialogResult.Cancel
        Close()
        Application.Exit()
    End Sub

End Class
