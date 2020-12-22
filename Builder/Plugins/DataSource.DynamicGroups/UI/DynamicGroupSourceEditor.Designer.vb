<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DynamicGroupSourceEditor


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
        Me.QuerySourceTextBox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        Me.QuerySourceTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.QuerySourceTextBox.Location = New System.Drawing.Point(0, 13)
        Me.QuerySourceTextBox.Multiline = True
        Me.QuerySourceTextBox.Name = "QuerySourceTextBox"
        Me.QuerySourceTextBox.Size = New System.Drawing.Size(541, 293)
        Me.QuerySourceTextBox.TabIndex = 0
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Item Filter Editor"
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.QuerySourceTextBox)
        Me.Controls.Add(Me.Label1)
        Me.Name = "DynamicGroupSourceEditor"
        Me.Size = New System.Drawing.Size(541, 306)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents QuerySourceTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label

End Class
