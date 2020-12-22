<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MathMLParameterEditorControl
    Inherits ParameterEditorControlBase

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
        Me.mathMLPictureBox = New System.Windows.Forms.PictureBox()
        CType(Me.ParameterBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.mathMLPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        Me.mathMLPictureBox.BackColor = System.Drawing.SystemColors.Window
        Me.mathMLPictureBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.mathMLPictureBox.Location = New System.Drawing.Point(0, 0)
        Me.mathMLPictureBox.Name = "mathMLPictureBox"
        Me.mathMLPictureBox.Size = New System.Drawing.Size(723, 113)
        Me.mathMLPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.mathMLPictureBox.TabIndex = 0
        Me.mathMLPictureBox.TabStop = False
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.mathMLPictureBox)
        Me.Name = "MathMLParameterEditorControl"
        Me.Size = New System.Drawing.Size(723, 113)
        CType(Me.ParameterBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.mathMLPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(false)

    End Sub
    Friend WithEvents mathMLPictureBox As System.Windows.Forms.PictureBox

End Class
