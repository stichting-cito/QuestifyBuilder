Imports Enums
Imports Questify.Builder.Model.ContentModel.EntityClasses

Public Class UserMetaData


    Private _user As UserEntity
    Private _isEditable As Boolean



    Public Property Datasource() As UserEntity
        Get
            Return _user
        End Get
        Set(ByVal value As UserEntity)
            If value IsNot Nothing Then
                _user = value
                Init()
            End If
        End Set
    End Property

    Public Property IsEditable() As Boolean
        Get
            Return _isEditable
        End Get
        Set(ByVal value As Boolean)
            _isEditable = value
            Me.SetEditable(_isEditable)
        End Set
    End Property



    Private Sub Init()
        UserEntityBindingSource.DataSource = _user
        If _user.AuthenticationType <> AuthenticationType.Default.ToString() Then
            Me.PasswordLabel.Visible = False
            Me.PasswordTextBox.Visible = False
        End If
    End Sub

    Private Sub SetEditable(ByVal isEditable As Boolean)
        Me.UserNameLabel.Visible = Not isEditable
        Me.UserNameTextBox.Visible = isEditable
        Me.FullNameLabel.Visible = Not isEditable
        Me.FullNameTextBox.Visible = isEditable
        Me.IsActiveCheckBox.Enabled = isEditable

        If _user IsNot Nothing AndAlso _user.AuthenticationType = AuthenticationType.Default.ToString() Then
            Me.PasswordLabel.Visible = isEditable
            Me.PasswordTextBox.Visible = isEditable
        End If
    End Sub


    Public Sub New()

        InitializeComponent()


    End Sub
End Class
