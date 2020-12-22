<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MonadicFilterEditor
    Inherits FilterEditorBase

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MonadicFilterEditor))
        Me.NameLocalizedLabel1 = New System.Windows.Forms.Label()
        Me.MonadicFilterPredicateBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.InstructionLabel = New System.Windows.Forms.Label()
        CType(Me.MonadicFilterPredicateBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        Me.NameLocalizedLabel1.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MonadicFilterPredicateBindingSource, "NameLocalized", True))
        resources.ApplyResources(Me.NameLocalizedLabel1, "NameLocalizedLabel1")
        Me.NameLocalizedLabel1.Name = "NameLocalizedLabel1"
        Me.MonadicFilterPredicateBindingSource.DataSource = GetType(MonadicFilterPredicate)
        resources.ApplyResources(Me.InstructionLabel, "InstructionLabel")
        Me.InstructionLabel.Name = "InstructionLabel"
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.InstructionLabel)
        Me.Controls.Add(Me.NameLocalizedLabel1)
        Me.Name = "MonadicFilterEditor"
        CType(Me.MonadicFilterPredicateBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MonadicFilterPredicateBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents NameLocalizedLabel1 As System.Windows.Forms.Label
    Friend WithEvents InstructionLabel As System.Windows.Forms.Label

End Class
