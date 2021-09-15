<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AspectEditor
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim FormStatusStrip As System.Windows.Forms.StatusStrip
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AspectEditor))
        Me.StatusTextLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.AspectEditorToolStrip = New System.Windows.Forms.ToolStrip()
        Me.SaveToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.SaveAsToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.SaveCloseToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.AspectEditorMenuStrip = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveCloseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.AspectGeneral = New Questify.Builder.UI.AspectGeneralProperties()
        Me.AspectResourceMetaData = New Questify.Builder.UI.ResourceMetaData()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.AspectPropertyEditor = New Questify.Builder.UI.AspectEditor()
        Me.AspectScoreTranslationTableEditor = New Questify.Builder.UI.AspectScoreTranslationTableControl()
        FormStatusStrip = New System.Windows.Forms.StatusStrip()
        FormStatusStrip.SuspendLayout()
        Me.AspectEditorToolStrip.SuspendLayout()
        Me.AspectEditorMenuStrip.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        resources.ApplyResources(FormStatusStrip, "FormStatusStrip")
        FormStatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StatusTextLabel})
        FormStatusStrip.Name = "FormStatusStrip"
        Me.StatusTextLabel.Name = "StatusTextLabel"
        resources.ApplyResources(Me.StatusTextLabel, "StatusTextLabel")
        resources.ApplyResources(Me.AspectEditorToolStrip, "AspectEditorToolStrip")
        Me.AspectEditorToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveToolStripButton, Me.SaveAsToolStripButton, Me.SaveCloseToolStripButton})
        Me.AspectEditorToolStrip.Name = "AspectEditorToolStrip"
        Me.SaveToolStripButton.Image = Global.Questify.Builder.Client.My.Resources.Resources.Save16
        resources.ApplyResources(Me.SaveToolStripButton, "SaveToolStripButton")
        Me.SaveToolStripButton.Name = "SaveToolStripButton"
        Me.SaveAsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.SaveAsToolStripButton.Image = Global.Questify.Builder.Client.My.Resources.Resources.SaveAs16
        resources.ApplyResources(Me.SaveAsToolStripButton, "SaveAsToolStripButton")
        Me.SaveAsToolStripButton.Name = "SaveAsToolStripButton"
        Me.SaveCloseToolStripButton.Image = Global.Questify.Builder.Client.My.Resources.Resources.SaveClose16
        resources.ApplyResources(Me.SaveCloseToolStripButton, "SaveCloseToolStripButton")
        Me.SaveCloseToolStripButton.Name = "SaveCloseToolStripButton"
        resources.ApplyResources(Me.AspectEditorMenuStrip, "AspectEditorMenuStrip")
        Me.AspectEditorMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem})
        Me.AspectEditorMenuStrip.Name = "AspectEditorMenuStrip"
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.toolStripSeparator, Me.SaveToolStripMenuItem, Me.SaveAsToolStripMenuItem, Me.SaveCloseToolStripMenuItem, Me.toolStripSeparator1, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        resources.ApplyResources(Me.FileToolStripMenuItem, "FileToolStripMenuItem")
        Me.toolStripSeparator.Name = "toolStripSeparator"
        resources.ApplyResources(Me.toolStripSeparator, "toolStripSeparator")
        Me.SaveToolStripMenuItem.Image = Global.Questify.Builder.Client.My.Resources.Resources.Save16
        resources.ApplyResources(Me.SaveToolStripMenuItem, "SaveToolStripMenuItem")
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveAsToolStripMenuItem.Image = Global.Questify.Builder.Client.My.Resources.Resources.SaveAs16
        Me.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem"
        resources.ApplyResources(Me.SaveAsToolStripMenuItem, "SaveAsToolStripMenuItem")
        Me.SaveCloseToolStripMenuItem.Image = Global.Questify.Builder.Client.My.Resources.Resources.SaveClose16
        resources.ApplyResources(Me.SaveCloseToolStripMenuItem, "SaveCloseToolStripMenuItem")
        Me.SaveCloseToolStripMenuItem.Name = "SaveCloseToolStripMenuItem"
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        resources.ApplyResources(Me.toolStripSeparator1, "toolStripSeparator1")
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        resources.ApplyResources(Me.ExitToolStripMenuItem, "ExitToolStripMenuItem")
        resources.ApplyResources(Me.SplitContainer1, "SplitContainer1")
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.TableLayoutPanel1)
        resources.ApplyResources(Me.SplitContainer2, "SplitContainer2")
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Panel1.Controls.Add(Me.AspectGeneral)
        Me.SplitContainer2.Panel2.Controls.Add(Me.AspectResourceMetaData)
        Me.AspectGeneral.Aspect = Nothing
        resources.ApplyResources(Me.AspectGeneral, "AspectGeneral")
        Me.AspectGeneral.Name = "AspectGeneral"
        resources.ApplyResources(Me.AspectResourceMetaData, "AspectResourceMetaData")
        Me.AspectResourceMetaData.IsNameChangable = False
        Me.AspectResourceMetaData.Name = "AspectResourceMetaData"
        Me.AspectResourceMetaData.ResourceEntity = Nothing
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.AspectPropertyEditor, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.AspectScoreTranslationTableEditor, 0, 1)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        resources.ApplyResources(Me.AspectPropertyEditor, "AspectPropertyEditor")
        Me.AspectPropertyEditor.ContextIdentifier = Nothing
        Me.AspectPropertyEditor.Name = "AspectPropertyEditor"
        resources.ApplyResources(Me.AspectScoreTranslationTableEditor, "AspectScoreTranslationTableEditor")
        Me.AspectScoreTranslationTableEditor.DataSource = Nothing
        Me.AspectScoreTranslationTableEditor.Name = "AspectScoreTranslationTableEditor"
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.AspectEditorToolStrip)
        Me.Controls.Add(Me.AspectEditorMenuStrip)
        Me.Controls.Add(FormStatusStrip)
        Me.Name = "AspectEditor"
        FormStatusStrip.ResumeLayout(False)
        FormStatusStrip.PerformLayout()
        Me.AspectEditorToolStrip.ResumeLayout(False)
        Me.AspectEditorToolStrip.PerformLayout()
        Me.AspectEditorMenuStrip.ResumeLayout(False)
        Me.AspectEditorMenuStrip.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents AspectEditorToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents SaveToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents SaveAsToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents SaveCloseToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents AspectEditorMenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SaveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveAsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveCloseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AspectResourceMetaData As Questify.Builder.UI.ResourceMetaData
    Friend WithEvents AspectGeneral As Questify.Builder.UI.AspectGeneralProperties
    Friend WithEvents StatusTextLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents AspectPropertyEditor As UI.AspectEditor
    Friend WithEvents AspectScoreTranslationTableEditor As UI.AspectScoreTranslationTableControl
End Class
