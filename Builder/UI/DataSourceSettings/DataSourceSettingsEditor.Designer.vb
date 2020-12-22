<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DataSourceSettingsEditor
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DataSourceSettingsEditor))
        Me.DataSourceConfigBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.NoConfigLabel = New System.Windows.Forms.Label()
        CType(Me.DataSourceConfigBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        resources.ApplyResources(Me.NoConfigLabel, "NoConfigLabel")
        Me.NoConfigLabel.Name = "NoConfigLabel"
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.NoConfigLabel)
        Me.Name = "DataSourceSettingsEditor"
        CType(Me.DataSourceConfigBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents NoConfigLabel As System.Windows.Forms.Label
    Protected WithEvents DataSourceConfigBindingSource As System.Windows.Forms.BindingSource

End Class
