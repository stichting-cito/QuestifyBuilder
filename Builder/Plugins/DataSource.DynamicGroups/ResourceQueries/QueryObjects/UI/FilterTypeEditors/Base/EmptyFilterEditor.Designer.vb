<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EmptyFilterEditor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EmptyFilterEditor))
        Me.InstructionLabel = New System.Windows.Forms.Label()
        Me.NameLocalizedLabel = New System.Windows.Forms.Label()
        Me.FilterPredicateBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        CType(Me.FilterPredicateBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        resources.ApplyResources(Me.InstructionLabel, "InstructionLabel")
        Me.InstructionLabel.Name = "InstructionLabel"
        resources.ApplyResources(Me.NameLocalizedLabel, "NameLocalizedLabel")
        Me.NameLocalizedLabel.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.FilterPredicateBindingSource, "NameLocalized", True))
        Me.NameLocalizedLabel.Name = "NameLocalizedLabel"
        Me.FilterPredicateBindingSource.DataSource = GetType(FilterPredicate)
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.InstructionLabel)
        Me.Controls.Add(Me.NameLocalizedLabel)
        Me.Name = "EmptyFilterEditor"
        CType(Me.FilterPredicateBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FilterPredicateBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents NameLocalizedLabel As System.Windows.Forms.Label
    Friend WithEvents InstructionLabel As System.Windows.Forms.Label

End Class
