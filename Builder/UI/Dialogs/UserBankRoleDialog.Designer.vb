<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UserBankRoleDialog
    Inherits DialogBase

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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UserBankRoleDialog))
        Me.BankSelector = New Questify.Builder.UI.BankBrowser
        Me.RoleListBox = New System.Windows.Forms.ListBox
        Me.RoleEntityBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.ContentPanel.SuspendLayout()
        CType(Me.RoleEntityBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        Me.ContentPanel.Controls.Add(Me.Label2)
        Me.ContentPanel.Controls.Add(Me.Label1)
        Me.ContentPanel.Controls.Add(Me.RoleListBox)
        Me.ContentPanel.Controls.Add(Me.BankSelector)
        resources.ApplyResources(Me.ContentPanel, "ContentPanel")
        Me.BankSelector.DataMember = ""
        Me.BankSelector.DataSource = Nothing
        Me.BankSelector.EnableFiltering = False
        resources.ApplyResources(Me.BankSelector, "BankSelector")
        Me.BankSelector.Name = "BankSelector"
        Me.BankSelector.SelectedBank = Nothing
        Me.RoleListBox.DataSource = Me.RoleEntityBindingSource
        Me.RoleListBox.DisplayMember = "Name"
        Me.RoleListBox.FormattingEnabled = True
        resources.ApplyResources(Me.RoleListBox, "RoleListBox")
        Me.RoleListBox.Name = "RoleListBox"
        Me.RoleListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
        Me.RoleEntityBindingSource.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.RoleEntity)
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        Me.AcceptButton = Nothing
        resources.ApplyResources(Me, "$this")
        Me.Name = "UserBankRoleDialog"
        Me.ContentPanel.ResumeLayout(False)
        Me.ContentPanel.PerformLayout()
        CType(Me.RoleEntityBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BankSelector As Questify.Builder.UI.BankBrowser
    Friend WithEvents RoleListBox As System.Windows.Forms.ListBox
    Friend WithEvents RoleEntityBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label

End Class
