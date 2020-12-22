Imports System.Windows.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SelectTargetAndColumns
    Inherits UserControl

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
        Me.components = New System.ComponentModel.Container()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SelectColumns = New SelectColumns()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        Me.SplitContainer1.CausesValidation = False
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.SplitContainer1.IsSplitterFixed = True
        If Me.SelectTargetControl IsNot Nothing Then
            Me.SplitContainer1.Panel1.Controls.Add(Me.SelectTargetControl)
        End If
        Me.SplitContainer1.Panel2.Controls.Add(Me.SelectColumns)
        Me.SplitContainer1.Size = New System.Drawing.Size(650, 1009)
        Me.SplitContainer1.SplitterDistance = 130
        Me.SplitContainer1.TabIndex = 0
        If Me.SelectTargetControl IsNot Nothing Then
            Me.SelectTargetControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.SelectTargetControl.Dock = System.Windows.Forms.DockStyle.Fill
            Me.SelectTargetControl.Location = New System.Drawing.Point(0, 0)
            Me.SelectTargetControl.Name = "SelectTargetControl"
            Me.SelectTargetControl.Padding = New System.Windows.Forms.Padding(12)
            Me.SelectTargetControl.Size = New System.Drawing.Size(650, 130)
            Me.SelectTargetControl.TabIndex = 0
        End If
        Me.SelectColumns.AutoSize = True
        Me.SelectColumns.CausesValidation = False
        Me.SelectColumns.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.SelectColumns.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SelectColumns.Location = New System.Drawing.Point(0, 0)
        Me.SelectColumns.Name = "SelectColumns"
        Me.SelectColumns.Padding = New System.Windows.Forms.Padding(12)
        Me.SelectColumns.Size = New System.Drawing.Size(650, 875)
        Me.SelectColumns.TabIndex = 0
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "SelectTargetAndColumns"
        Me.Size = New System.Drawing.Size(650, 1009)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Private WithEvents SelectTargetControl As SelectTarget
    Public WithEvents SelectColumns As SelectColumns

End Class
