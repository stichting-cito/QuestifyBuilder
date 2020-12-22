<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UserPropertyDialog
    Inherits Questify.Builder.UI.PropertyDialogBase

    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UserPropertyDialog))
        Me.UserTabControl = New System.Windows.Forms.TabControl()
        Me.GeneralTabPage = New System.Windows.Forms.TabPage()
        Me.MetaData = New Questify.Builder.UI.UserMetaData()
        Me.AppRoleTabPage = New System.Windows.Forms.TabPage()
        Me.UserApplicationRoleGrid = New Questify.Builder.UI.ApplicationRoleGrid()
        Me.BankRoleTabPage = New System.Windows.Forms.TabPage()
        Me.BankRoleViewer = New Questify.Builder.UI.UserBankRoleViewer()
        Me.DeleteBankRoleButton = New System.Windows.Forms.Button()
        Me.AddBankRoleButton = New System.Windows.Forms.Button()
        Me.UserTabControl.SuspendLayout()
        Me.GeneralTabPage.SuspendLayout()
        Me.AppRoleTabPage.SuspendLayout()
        Me.BankRoleTabPage.SuspendLayout()
        Me.SuspendLayout()
        resources.ApplyResources(Me.ContentPanel, "ContentPanel")
        Me.UserTabControl.Controls.Add(Me.GeneralTabPage)
        Me.UserTabControl.Controls.Add(Me.AppRoleTabPage)
        Me.UserTabControl.Controls.Add(Me.BankRoleTabPage)
        resources.ApplyResources(Me.UserTabControl, "UserTabControl")
        Me.UserTabControl.Name = "UserTabControl"
        Me.UserTabControl.SelectedIndex = 0
        Me.GeneralTabPage.Controls.Add(Me.MetaData)
        resources.ApplyResources(Me.GeneralTabPage, "GeneralTabPage")
        Me.GeneralTabPage.Name = "GeneralTabPage"
        Me.GeneralTabPage.UseVisualStyleBackColor = True
        Me.MetaData.Datasource = Nothing
        Me.MetaData.IsEditable = False
        resources.ApplyResources(Me.MetaData, "MetaData")
        Me.MetaData.Name = "MetaData"
        Me.AppRoleTabPage.Controls.Add(Me.UserApplicationRoleGrid)
        resources.ApplyResources(Me.AppRoleTabPage, "AppRoleTabPage")
        Me.AppRoleTabPage.Name = "AppRoleTabPage"
        Me.AppRoleTabPage.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.UserApplicationRoleGrid, "UserApplicationRoleGrid")
        Me.UserApplicationRoleGrid.Name = "UserApplicationRoleGrid"
        Me.BankRoleTabPage.Controls.Add(Me.DeleteBankRoleButton)
        Me.BankRoleTabPage.Controls.Add(Me.BankRoleViewer)
        Me.BankRoleTabPage.Controls.Add(Me.AddBankRoleButton)
        resources.ApplyResources(Me.BankRoleTabPage, "BankRoleTabPage")
        Me.BankRoleTabPage.Name = "BankRoleTabPage"
        Me.BankRoleTabPage.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.BankRoleViewer, "BankRoleViewer")
        Me.BankRoleViewer.Name = "BankRoleViewer"
        resources.ApplyResources(Me.DeleteBankRoleButton, "DeleteBankRoleButton")
        Me.DeleteBankRoleButton.Name = "DeleteBankRoleButton"
        Me.DeleteBankRoleButton.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.AddBankRoleButton, "AddBankRoleButton")
        Me.AddBankRoleButton.Name = "AddBankRoleButton"
        Me.AddBankRoleButton.UseVisualStyleBackColor = True
        Me.ApplyEnabled = True
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.UserTabControl)
        Me.Name = "UserPropertyDialog"
        Me.Controls.SetChildIndex(Me.ContentPanel, 0)
        Me.Controls.SetChildIndex(Me.UserTabControl, 0)
        Me.UserTabControl.ResumeLayout(False)
        Me.GeneralTabPage.ResumeLayout(False)
        Me.AppRoleTabPage.ResumeLayout(False)
        Me.BankRoleTabPage.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UserTabControl As System.Windows.Forms.TabControl
    Friend WithEvents GeneralTabPage As System.Windows.Forms.TabPage
    Friend WithEvents AppRoleTabPage As System.Windows.Forms.TabPage
    Friend WithEvents BankRoleTabPage As System.Windows.Forms.TabPage
    Friend WithEvents MetaData As Questify.Builder.UI.UserMetaData
    Friend WithEvents UserApplicationRoleGrid As Questify.Builder.UI.ApplicationRoleGrid
    Friend WithEvents DeleteBankRoleButton As System.Windows.Forms.Button
    Friend WithEvents AddBankRoleButton As System.Windows.Forms.Button
    Friend WithEvents BankRoleViewer As Questify.Builder.UI.UserBankRoleViewer

End Class
