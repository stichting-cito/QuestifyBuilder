Imports Questify.Builder.Model.ContentModel.EntityClasses

Public Class RoleMetaData


    Private _role As RoleEntity
    Private _isEditable As Boolean



    Public Property Datasource() As RoleEntity
        Get
            Return _role
        End Get
        Set(ByVal value As RoleEntity)
            If value IsNot Nothing Then
                _role = value
                RoleEntityBindingSource.DataSource = _role
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



    Private Sub SetEditable(ByVal isEditable As Boolean)
        Me.NameLabel.Visible = Not isEditable
        Me.NameTextBox.Visible = isEditable
        Me.DescriptionLabel.Visible = Not isEditable
        Me.DescriptionTextBox.Visible = isEditable
    End Sub


End Class
