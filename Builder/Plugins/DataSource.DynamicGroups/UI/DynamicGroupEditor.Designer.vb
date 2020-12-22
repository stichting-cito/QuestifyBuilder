<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DynamicGroupEditor


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
        Me.QueryEditorControl = New DynamicGroups.QueryEditor()
        Me.SuspendLayout()
        Me.QueryEditorControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.QueryEditorControl.Location = New System.Drawing.Point(0, 0)
        Me.QueryEditorControl.Name = "QueryEditorControl"
        Me.QueryEditorControl.Query = Nothing
        Me.QueryEditorControl.Size = New System.Drawing.Size(718, 523)
        Me.QueryEditorControl.TabIndex = 0
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.QueryEditorControl)
        Me.Name = "DynamicGroupEditor"
        Me.Size = New System.Drawing.Size(718, 523)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents QueryEditorControl As DynamicGroups.QueryEditor

End Class
