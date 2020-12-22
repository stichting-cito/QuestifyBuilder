<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Word_ItemPreviewer
    Inherits ItemPreviewerBase

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Word_ItemPreviewer))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TableLayoutPanelOperatorButtons = New System.Windows.Forms.TableLayoutPanel()
        Me.ItemPreviewPanel = New Questify.Builder.UI.PreviewPanel()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        resources.ApplyResources(Me.SplitContainer1, "SplitContainer1")
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Panel1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.SplitContainer1.Panel1.Controls.Add(Me.TableLayoutPanelOperatorButtons)
        Me.SplitContainer1.Panel2.Controls.Add(Me.ItemPreviewPanel)
        resources.ApplyResources(Me.TableLayoutPanelOperatorButtons, "TableLayoutPanelOperatorButtons")
        Me.TableLayoutPanelOperatorButtons.Name = "TableLayoutPanelOperatorButtons"
        Me.ItemPreviewPanel.BackColor = System.Drawing.SystemColors.ControlLightLight
        resources.ApplyResources(Me.ItemPreviewPanel, "ItemPreviewPanel")
        Me.ItemPreviewPanel.Name = "ItemPreviewPanel"
        Me.ItemPreviewPanel.PreviewSource = Nothing
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "Word_ItemPreviewer"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents ItemPreviewPanel As Questify.Builder.UI.PreviewPanel
    Friend WithEvents TableLayoutPanelOperatorButtons As System.Windows.Forms.TableLayoutPanel

End Class
