
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Security
Imports Questify.Builder.UI

Public Class ChangeCreatorModifierDialog

    Private _newUser As UserEntity

    Public ReadOnly Property SelectedUser() As UserEntity
        Get
            Return _newUser
        End Get
    End Property

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonCancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub ButtonOK_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonOK.Click
        If _newUser Is Nothing Then
            MessageBox.Show(My.Resources.SelectAnUserBeforeContinuing, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            DialogResult = DialogResult.OK
            Close()
        End If
    End Sub

    Public Sub New(ByVal currentUser As UserEntity)
        InitializeComponent()

        CurrentNameTextBox.Text = currentUser.UserName
    End Sub

    Private Sub SelectUserButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SelectUserButton.Click
        Dim TBIdentity As TestBuilderIdentity = DirectCast(My.User.CurrentPrincipal.Identity, TestBuilderIdentity)

        Dim selectUserDialog As New SelectUserDialog()
        If selectUserDialog.ShowDialog(Me) = DialogResult.OK Then
            UserNameTextBox.Text = selectUserDialog.SelectedUser.UserName
            _newUser = selectUserDialog.SelectedUser
        End If
    End Sub

    Private Sub ClearUserSelectionButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ClearUserSelectionButton.Click
        UserNameTextBox.Text = String.Empty
        _newUser = Nothing
    End Sub

End Class