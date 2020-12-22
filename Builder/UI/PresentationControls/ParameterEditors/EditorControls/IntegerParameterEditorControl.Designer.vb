<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IntegerParameterEditorControl
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IntegerParameterEditorControl))
        Me.InputTextBox = New System.Windows.Forms.TextBox()
        CType(Me.ParameterBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        Me.ParameterBindingSource.DataSource = GetType(Cito.Tester.ContentModel.IntegerParameter)
        Me.InputTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ParameterBindingSource, "Value", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        resources.ApplyResources(Me.InputTextBox, "InputTextBox")
        Me.InputTextBox.Name = "InputTextBox"
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.InputTextBox)
        Me.Name = "IntegerParameterEditorControl"
        CType(Me.ParameterBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents InputTextBox As System.Windows.Forms.TextBox

End Class
