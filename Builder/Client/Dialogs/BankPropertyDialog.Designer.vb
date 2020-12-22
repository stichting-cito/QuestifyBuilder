

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BankPropertyDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BankPropertyDialog))
        Me.BankPropertiesTabs = New System.Windows.Forms.TabControl()
        Me.GeneralTabPage = New System.Windows.Forms.TabPage()
        Me.BankMetaData = New Questify.Builder.UI.BankMetaData()
        Me.SecurityTabPage = New System.Windows.Forms.TabPage()
        Me.BankSecurity = New Questify.Builder.UI.BankSecurityControl()
        Me.ContentPanel.SuspendLayout()
        Me.BankPropertiesTabs.SuspendLayout()
        Me.GeneralTabPage.SuspendLayout()
        Me.SecurityTabPage.SuspendLayout()
        Me.SuspendLayout()
        Me.ContentPanel.Controls.Add(Me.BankPropertiesTabs)
        resources.ApplyResources(Me.ContentPanel, "ContentPanel")
        Me.BankPropertiesTabs.Controls.Add(Me.GeneralTabPage)
        Me.BankPropertiesTabs.Controls.Add(Me.SecurityTabPage)
        resources.ApplyResources(Me.BankPropertiesTabs, "BankPropertiesTabs")
        Me.BankPropertiesTabs.Name = "BankPropertiesTabs"
        Me.BankPropertiesTabs.SelectedIndex = 0
        Me.GeneralTabPage.Controls.Add(Me.BankMetaData)
        resources.ApplyResources(Me.GeneralTabPage, "GeneralTabPage")
        Me.GeneralTabPage.Name = "GeneralTabPage"
        Me.GeneralTabPage.UseVisualStyleBackColor = True
        Me.BankMetaData.Datasource = Nothing
        resources.ApplyResources(Me.BankMetaData, "BankMetaData")
        Me.BankMetaData.Name = "BankMetaData"
        Me.SecurityTabPage.Controls.Add(Me.BankSecurity)
        resources.ApplyResources(Me.SecurityTabPage, "SecurityTabPage")
        Me.SecurityTabPage.Name = "SecurityTabPage"
        Me.SecurityTabPage.UseVisualStyleBackColor = True
        Me.BankSecurity.BankContext = Nothing
        resources.ApplyResources(Me.BankSecurity, "BankSecurity")
        Me.BankSecurity.Name = "BankSecurity"
        Me.ApplyEnabled = True
        resources.ApplyResources(Me, "$this")
        Me.Name = "BankPropertyDialog"
        Me.ContentPanel.ResumeLayout(False)
        Me.BankPropertiesTabs.ResumeLayout(False)
        Me.GeneralTabPage.ResumeLayout(False)
        Me.SecurityTabPage.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BankPropertiesTabs As System.Windows.Forms.TabControl
    Friend WithEvents GeneralTabPage As System.Windows.Forms.TabPage
    Friend WithEvents BankMetaData As Questify.Builder.UI.BankMetaData
    Friend WithEvents SecurityTabPage As System.Windows.Forms.TabPage
    Friend WithEvents BankSecurity As Questify.Builder.UI.BankSecurityControl

End Class
