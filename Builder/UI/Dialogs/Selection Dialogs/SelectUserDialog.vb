Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Logic.Service.Factories

Public Class SelectUserDialog

    Private _selectedUser As EntityClasses.UserEntity

    Public Sub New()
        InitializeComponent()
    End Sub

    Public ReadOnly Property SelectedUser() As EntityClasses.UserEntity
        Get
            Return _selectedUser
        End Get
    End Property

    Private Sub AddUserButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        _selectedUser = TheGrid.SelectedUser
        If _selectedUser Is Nothing Then
            MessageBox.Show(My.Resources.SelectUserDialog_PlzSelectUser, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End If
    End Sub

    Private Sub NoAddButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NoAddButton.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub SelectUserDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TheGrid.DataSource = AuthorizationFactory.Instance.GetUsers()
    End Sub

End Class