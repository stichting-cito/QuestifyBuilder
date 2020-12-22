<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ItemSource
    Inherits ItemEditorControlBase

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ItemSource))
        Me.ItemSourceTextBox = New System.Windows.Forms.TextBox()
        CType(Me.AssessmentItemBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        Me.ItemSourceTextBox.BackColor = System.Drawing.Color.White
        Me.ItemSourceTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
        resources.ApplyResources(Me.ItemSourceTextBox, "ItemSourceTextBox")
        Me.ItemSourceTextBox.Name = "ItemSourceTextBox"
        Me.ItemSourceTextBox.ReadOnly = True
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.ItemSourceTextBox)
        Me.Name = "ItemSource"
        CType(Me.AssessmentItemBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ItemSourceTextBox As System.Windows.Forms.TextBox

End Class
