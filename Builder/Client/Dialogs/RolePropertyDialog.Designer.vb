<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RolePropertyDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RolePropertyDialog))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.MetaData = New Questify.Builder.UI.RoleMetaData()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.ContentPanel.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.SuspendLayout()
        Me.ContentPanel.Controls.Add(Me.TabControl1)
        resources.ApplyResources(Me.ContentPanel, "ContentPanel")
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        resources.ApplyResources(Me.TabControl1, "TabControl1")
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabPage1.Controls.Add(Me.MetaData)
        resources.ApplyResources(Me.TabPage1, "TabPage1")
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.UseVisualStyleBackColor = True
        Me.MetaData.Datasource = Nothing
        resources.ApplyResources(Me.MetaData, "MetaData")
        Me.MetaData.IsEditable = False
        Me.MetaData.Name = "MetaData"
        resources.ApplyResources(Me.TabPage2, "TabPage2")
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        Me.ApplyEnabled = True
        resources.ApplyResources(Me, "$this")
        Me.Name = "RolePropertyDialog"
        Me.ContentPanel.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents MetaData As Questify.Builder.UI.RoleMetaData

End Class
