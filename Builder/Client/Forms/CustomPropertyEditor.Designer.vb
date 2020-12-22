<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CustomPropertyEditor
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()

            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CustomPropertyEditor))
        Me.TabControlMain = New System.Windows.Forms.TabControl()
        Me.TabPageGeneral = New System.Windows.Forms.TabPage()
        Me.GeneralUserControl = New Questify.Builder.UI.GeneralUserControl()
        Me.TabPageValues = New System.Windows.Forms.TabPage()
        Me.TabPageStructure = New System.Windows.Forms.TabPage()
        Me.MenuStripMain = New System.Windows.Forms.MenuStrip()
        Me.MenuSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuSaveAndClose = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuAdd = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuDelete = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabControlMain.SuspendLayout
        Me.TabPageGeneral.SuspendLayout
        Me.MenuStripMain.SuspendLayout
        Me.SuspendLayout
        resources.ApplyResources(Me.TabControlMain, "TabControlMain")
        Me.TabControlMain.Controls.Add(Me.TabPageGeneral)
        Me.TabControlMain.Controls.Add(Me.TabPageValues)
        Me.TabControlMain.Controls.Add(Me.TabPageStructure)
        Me.TabControlMain.Name = "TabControlMain"
        Me.TabControlMain.SelectedIndex = 0
        Me.TabPageGeneral.Controls.Add(Me.GeneralUserControl)
        resources.ApplyResources(Me.TabPageGeneral, "TabPageGeneral")
        Me.TabPageGeneral.Name = "TabPageGeneral"
        Me.TabPageGeneral.Tag = "General"
        Me.GeneralUserControl.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange
        resources.ApplyResources(Me.GeneralUserControl, "GeneralUserControl")
        Me.GeneralUserControl.Name = "GeneralUserControl"
        resources.ApplyResources(Me.TabPageValues, "TabPageValues")
        Me.TabPageValues.Name = "TabPageValues"
        Me.TabPageValues.Tag = "Values"
        resources.ApplyResources(Me.TabPageStructure, "TabPageStructure")
        Me.TabPageStructure.Name = "TabPageStructure"
        Me.TabPageStructure.Tag = "Structure"
        Me.MenuStripMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuSave, Me.MenuSaveAndClose, Me.MenuAdd, Me.MenuDelete})
        resources.ApplyResources(Me.MenuStripMain, "MenuStripMain")
        Me.MenuStripMain.Name = "MenuStripMain"
        Me.MenuSave.Image = Global.Questify.Builder.Client.My.Resources.Resources.Save
        Me.MenuSave.Name = "MenuSave"
        resources.ApplyResources(Me.MenuSave, "MenuSave")
        Me.MenuSaveAndClose.Image = Global.Questify.Builder.Client.My.Resources.Resources.Save
        Me.MenuSaveAndClose.Name = "MenuSaveAndClose"
        resources.ApplyResources(Me.MenuSaveAndClose, "MenuSaveAndClose")
        Me.MenuAdd.Image = Global.Questify.Builder.Client.My.Resources.Resources.NewToolStripMenuItem_Image
        Me.MenuAdd.Name = "MenuAdd"
        resources.ApplyResources(Me.MenuAdd, "MenuAdd")
        Me.MenuDelete.Image = Global.Questify.Builder.Client.My.Resources.Resources.DeleteToolStripMenuItem_Image
        Me.MenuDelete.Name = "MenuDelete"
        resources.ApplyResources(Me.MenuDelete, "MenuDelete")
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange
        Me.Controls.Add(Me.TabControlMain)
        Me.Controls.Add(Me.MenuStripMain)
        Me.MainMenuStrip = Me.MenuStripMain
        Me.Name = "CustomPropertyEditor"
        Me.TabControlMain.ResumeLayout(false)
        Me.TabPageGeneral.ResumeLayout(false)
        Me.MenuStripMain.ResumeLayout(false)
        Me.MenuStripMain.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

    End Sub
    Friend WithEvents TabControlMain As TabControl
    Friend WithEvents TabPageGeneral As TabPage
    Friend WithEvents GeneralUserControl As UI.GeneralUserControl
    Friend WithEvents TabPageValues As TabPage
    Friend WithEvents TabPageStructure As TabPage
    Friend WithEvents MenuStripMain As MenuStrip
    Friend WithEvents MenuSave As ToolStripMenuItem
    Friend WithEvents MenuSaveAndClose As ToolStripMenuItem
    Friend WithEvents MenuAdd As ToolStripMenuItem
    Friend WithEvents MenuDelete As ToolStripMenuItem
End Class
