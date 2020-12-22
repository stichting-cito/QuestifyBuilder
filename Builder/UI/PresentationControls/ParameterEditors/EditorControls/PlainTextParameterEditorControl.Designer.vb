<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PlainTextParameterEditorControl
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PlainTextParameterEditorControl))
        Me.PlainTextParameterTextBox = New System.Windows.Forms.TextBox()
        CType(Me.ParameterBindingSource, System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        Me.ParameterBindingSource.DataSource = GetType(Cito.Tester.ContentModel.PlainTextParameter)
        Me.PlainTextParameterTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ParameterBindingSource, "Value", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        resources.ApplyResources(Me.PlainTextParameterTextBox, "PlainTextParameterTextBox")
        Me.PlainTextParameterTextBox.Name = "PlainTextParameterTextBox"
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.PlainTextParameterTextBox)
        Me.Name = "PlainTextParameterEditorControl"
        CType(Me.ParameterBindingSource, System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

    End Sub
    Friend WithEvents PlainTextParameterTextBox As System.Windows.Forms.TextBox

End Class
