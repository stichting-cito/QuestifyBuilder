<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BooleanParameterEditorControl
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BooleanParameterEditorControl))
        Me.CheckBoxInteraction = New FocussedCheckBox()
        CType(Me.ParameterBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        Me.ParameterBindingSource.DataSource = GetType(Cito.Tester.ContentModel.BooleanParameter)
        resources.ApplyResources(Me.CheckBoxInteraction, "CheckBoxInteraction")
        Me.CheckBoxInteraction.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.ParameterBindingSource, "Value", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.CheckBoxInteraction.Name = "CheckBoxInteraction"
        Me.CheckBoxInteraction.UseVisualStyleBackColor = True
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.CheckBoxInteraction)
        Me.Name = "BooleanParameterEditorControl"
        CType(Me.ParameterBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CheckBoxInteraction As FocussedCheckBox

End Class
