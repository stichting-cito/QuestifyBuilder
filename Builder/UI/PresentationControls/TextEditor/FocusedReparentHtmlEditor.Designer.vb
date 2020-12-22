<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FocusedReparentHtmlEditor
    Inherits System.Windows.Forms.UserControl

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
        Me.LayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.editor = New Questify.Builder.UI.ReparentHtmlEditor()
        Me.LayoutPanel.SuspendLayout
        Me.SuspendLayout
        Me.LayoutPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(122, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.LayoutPanel.ColumnCount = 1
        Me.LayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.LayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.LayoutPanel.Controls.Add(Me.editor, 0, 0)
        Me.LayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutPanel.Location = New System.Drawing.Point(0, 0)
        Me.LayoutPanel.Margin = New System.Windows.Forms.Padding(0)
        Me.LayoutPanel.Name = "LayoutPanel"
        Me.LayoutPanel.Padding = New System.Windows.Forms.Padding(1)
        Me.LayoutPanel.RowCount = 1
        Me.LayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.LayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.LayoutPanel.Size = New System.Drawing.Size(150, 32)
        Me.LayoutPanel.TabIndex = 1
        Me.editor.AccessibleName = "DelayedHtmlEditor"
        Me.editor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.editor.BackColor = System.Drawing.SystemColors.Control
        Me.editor.BorderStyle = BorderStyle.None
        Me.editor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.editor.DoHeightUpdate = false
        Me.editor.FormClosing = false
        Me.editor.Location = New System.Drawing.Point(1, 1)
        Me.editor.Margin = New System.Windows.Forms.Padding(0)
        Me.editor.Name = "editor"
        Me.editor.Size = New System.Drawing.Size(148, 30)
        Me.editor.TabIndex = 1
        Me.editor.TabStop = false
        Me.editor.Tag = "1"
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutPanel)
        Me.Name = "FocusedReparentHtmlEditor"
        Me.Size = New System.Drawing.Size(150, 32)
        Me.LayoutPanel.ResumeLayout(false)
        Me.ResumeLayout(false)

    End Sub

    Friend WithEvents LayoutPanel As TableLayoutPanel
    Friend WithEvents editor As ReparentHtmlEditor
End Class
