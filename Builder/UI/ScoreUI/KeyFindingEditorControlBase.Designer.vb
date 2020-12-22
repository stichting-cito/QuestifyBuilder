<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class KeyFindingEditorControlBase
    Inherits SolutionEditorControlBase

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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(KeyFindingEditorControlBase))
        Me.ContentPanel = New System.Windows.Forms.Panel
        Me.CaptionLabel = New System.Windows.Forms.Label
        Me.KeyFindingBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        CType(Me.KeyFindingBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        resources.ApplyResources(Me.ContentPanel, "ContentPanel")
        Me.ContentPanel.Name = "ContentPanel"
        Me.CaptionLabel.BackColor = System.Drawing.SystemColors.ControlDark
        resources.ApplyResources(Me.CaptionLabel, "CaptionLabel")
        Me.CaptionLabel.Name = "CaptionLabel"
        Me.KeyFindingBindingSource.DataSource = GetType(Cito.Tester.ContentModel.KeyFinding)
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ContentPanel)
        Me.Controls.Add(Me.CaptionLabel)
        Me.Name = "KeyFindingEditorControlBase"
        CType(Me.KeyFindingBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Protected WithEvents KeyFindingBindingSource As System.Windows.Forms.BindingSource
    Protected WithEvents ContentPanel As System.Windows.Forms.Panel
    Private WithEvents CaptionLabel As System.Windows.Forms.Label

End Class
