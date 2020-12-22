<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ImportantMessageControl
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ImportantMessageControl))
        Me.ButtonClose = New System.Windows.Forms.Button()
        Me.TestEditorMessage = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        Me.ButtonClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.ButtonClose.Image = CType(resources.GetObject("ButtonClose.Image"), System.Drawing.Image)
        Me.ButtonClose.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.ButtonClose.Location = New System.Drawing.Point(405, 0)
        Me.ButtonClose.Name = "ButtonClose"
        Me.ButtonClose.Size = New System.Drawing.Size(20, 21)
        Me.ButtonClose.TabIndex = 13
        Me.ButtonClose.UseVisualStyleBackColor = True
        Me.TestEditorMessage.BackColor = System.Drawing.Color.Transparent
        Me.TestEditorMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TestEditorMessage.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TestEditorMessage.Location = New System.Drawing.Point(0, 0)
        Me.TestEditorMessage.Margin = New System.Windows.Forms.Padding(3)
        Me.TestEditorMessage.Name = "TestEditorMessage"
        Me.TestEditorMessage.Padding = New System.Windows.Forms.Padding(3)
        Me.TestEditorMessage.Size = New System.Drawing.Size(425, 21)
        Me.TestEditorMessage.TabIndex = 14
        Me.TestEditorMessage.Text = "Placeholder for test editor message"
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Controls.Add(Me.ButtonClose)
        Me.Controls.Add(Me.TestEditorMessage)
        Me.Name = "ImportantMessageControl"
        Me.Size = New System.Drawing.Size(425, 21)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ButtonClose As System.Windows.Forms.Button
    Friend WithEvents TestEditorMessage As System.Windows.Forms.Label

End Class
