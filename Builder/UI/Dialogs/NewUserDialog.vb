
Imports System.Text
Imports Enums
Imports Questify.Builder.Model.ContentModel

Public Class NewUserDialog

    Private _user As EntityClasses.UserEntity

    Public Property User() As EntityClasses.UserEntity
        Get
            Return _user
        End Get
        Private Set(ByVal value As EntityClasses.UserEntity)
            _user = value
        End Set
    End Property

    Public Sub New()
        InitializeComponent()

        Me.Text = My.Resources.Create_User

        Dim newUser As New EntityClasses.UserEntity()
        newUser.Active = True
        newUser.AuthenticationType = AuthenticationType.Default.ToString()

        Me.User = newUser

        DataBind()
    End Sub

    Private Sub DataBind()
        UserEntityBindingSource.DataSource = Me.User
    End Sub

    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If (IsFormValid()) Then
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        End If
    End Sub

    Private Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.User = Nothing
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Public Function IsFormValid() As Boolean
        Dim errors = GetValidationErrors()
        If errors.Length > 0 Then
            Return False
        End If
        Return True
    End Function

    Private Function GetValidationErrors() As StringBuilder
        Dim errors As New System.Text.StringBuilder()
        If String.IsNullOrEmpty(FullNameTextBox.Text) Then
            SetControlError(FullNameLabel.Text, FullNameTextBox, My.Resources.FieldIsRequired)
            errors.Append(UserErrorProvider.GetError(FullNameTextBox) + Environment.NewLine)
        End If
        If String.IsNullOrEmpty(UserNameTextBox.Text) Then
            SetControlError(UserNameLabel.Text, UserNameTextBox, My.Resources.FieldIsRequired)
            errors.Append(UserErrorProvider.GetError(UserNameTextBox) + Environment.NewLine)
        End If
        If String.IsNullOrEmpty(PasswordTextBox.Text) Then
            SetControlError(PasswordLabel.Text, PasswordTextBox, My.Resources.FieldIsRequired)
            errors.Append(UserErrorProvider.GetError(PasswordTextBox) + Environment.NewLine)
        End If

        Dim dataErrorInfo As System.ComponentModel.IDataErrorInfo = DirectCast(_user, System.ComponentModel.IDataErrorInfo)
        If Not String.IsNullOrEmpty(dataErrorInfo.Error) Then
            errors.Append(dataErrorInfo.Error)
        End If
        Return errors
    End Function

    Private Sub SetControlError(ByVal controlName As String, ByVal control As Control, ByVal errorMessage As String)
        Dim err As String = String.Format(errorMessage, controlName)
        UserErrorProvider.SetError(control, err)
    End Sub

End Class
