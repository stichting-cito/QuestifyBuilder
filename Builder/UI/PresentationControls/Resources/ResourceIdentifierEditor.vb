Imports Questify.Builder.Model.ContentModel.EntityClasses

Public Class ResourceIdentifierEditor


    Private _resourceEntity As ResourceEntity

    Public Event ChangeCode As EventHandler(Of EventArgs)



    Public Sub ToggleCodeField(ByVal enabled As Boolean)
        CodeTextBox.Enabled = enabled
        ChangeCodeButton.Enabled = Not enabled
    End Sub

    Public Property ResourceEntity() As ResourceEntity
        Get
            Return _resourceEntity
        End Get
        Set(ByVal value As ResourceEntity)
            _resourceEntity = value

            If value IsNot Nothing Then
                Me.ResourceEntityBindingSource.DataSource = _resourceEntity
            Else
                Me.ResourceEntityBindingSource.DataSource = GetType(ResourceEntity)
            End If
        End Set
    End Property



    Protected Overridable Sub OnChangeItemCode(ByVal e As EventArgs)
        RaiseEvent ChangeCode(Me, e)
    End Sub

    Private Sub ResourceEntityBindingSource_DataSourceChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResourceEntityBindingSource.DataSourceChanged
        If TypeOf ResourceEntityBindingSource.DataSource Is ResourceEntity Then
            Me.Enabled = True
            SetUIState()
        Else
            Me.Enabled = False
        End If
    End Sub

    Private Sub SetUIState()
        If _resourceEntity.IsNew Then
            CodeTextBox.Enabled = True
            TitleTextBox.Enabled = True
        Else
            CodeTextBox.Enabled = False
            TitleTextBox.Enabled = True
        End If
    End Sub

    Private Sub ChangeCodeButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangeCodeButton.Click
        OnChangeItemCode(New EventArgs())
    End Sub


End Class
