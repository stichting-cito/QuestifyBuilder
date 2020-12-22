Imports Questify.Builder.Security
Imports Questify.Builder.Logic.Service.HelperFunctions

Public Class ItemEditor


    Public Event SwitchItemTemplate As EventHandler(Of EventArgs)

    Public Event ChangeItemCode As EventHandler(Of EventArgs)



    Private _bankId As Integer



    Public Property BankId() As Integer
        Get
            Return _bankId
        End Get
        Set(ByVal value As Integer)
            _bankId = value

            If value <> 0 Then
                HideOrShowTemplateSwitchButton(value)
            End If
        End Set
    End Property




    Public Sub ToggleCodeField(ByVal enabled As Boolean)
        CodeTextBox.Enabled = enabled
    End Sub

    Public Sub ToggleChangeCodeButton(enabled As Boolean)
        ChangeCodeButton.Enabled = enabled
    End Sub

    Public Sub SetFocusOnTitleField()
        TitleTextBox.Focus()
    End Sub

    Public Sub SetFocusOnCodeField()
        CodeTextBox.Focus()
    End Sub

    Public Sub HideOrShowItemId()
        ItemIdTextBox.Visible = ItemIdHelper.useItemId
        ItemIdLabel.Visible = ItemIdHelper.useItemId
    End Sub


    Protected Overridable Sub OnSwitchItemTemplate(ByVal e As EventArgs)
        RaiseEvent SwitchItemTemplate(Me, e)
    End Sub

    Protected Overridable Sub OnChangeItemCode(ByVal e As EventArgs)
        RaiseEvent ChangeItemCode(Me, e)
    End Sub



    Private Sub HideOrShowTemplateSwitchButton(ByVal bankId As Integer)
        If Not Me.DesignMode Then
            If Not PermissionFactory.Instance.TryUserIsPermittedToNamedTask(TestBuilderPermissionAccess.Execute, TestBuilderPermissionTarget.NamedTask, TestBuilderPermissionNamedTask.SwitchItemLayoutTemplate, bankId, 0) Then
                SwitchItemTemplateButton.Visible = False
                Dim pad As New Padding(3, 3, 30, 3)
                CodeTextBox.Margin = pad
                TitleTextBox.Margin = pad
                LayoutTemplateSourceNameTextBox.Margin = pad
            End If
        End If
    End Sub


    Private Sub SwitchItemTemplateButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SwitchItemTemplateButton.Click
        OnSwitchItemTemplate(New EventArgs())
    End Sub

    Private Sub ChangeCodeButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangeCodeButton.Click
        OnChangeItemCode(New EventArgs())
    End Sub



End Class
