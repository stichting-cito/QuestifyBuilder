<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DataSourcePreview
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DataSourcePreview))
        Me.PreviewTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.ExecuteButton = New System.Windows.Forms.Button()
        Me.DataSourceSelectionUIPanel = New System.Windows.Forms.Panel()
        Me.DataSourceSettingsEditorInstance = New DataSourceSettingsEditor()
        Me.DataSourceResultUIPanel = New System.Windows.Forms.Panel()
        Me.CommandManager1 = New Commanding.CommandManager(Me.components)
        Me.PreviewTableLayoutPanel.SuspendLayout()
        Me.DataSourceSelectionUIPanel.SuspendLayout()
        Me.SuspendLayout()
        resources.ApplyResources(Me.PreviewTableLayoutPanel, "PreviewTableLayoutPanel")
        Me.PreviewTableLayoutPanel.Controls.Add(Me.ExecuteButton, 0, 1)
        Me.PreviewTableLayoutPanel.Controls.Add(Me.DataSourceSelectionUIPanel, 0, 0)
        Me.PreviewTableLayoutPanel.Name = "PreviewTableLayoutPanel"
        resources.ApplyResources(Me.ExecuteButton, "ExecuteButton")
        Me.ExecuteButton.Name = "ExecuteButton"
        Me.ExecuteButton.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.DataSourceSelectionUIPanel, "DataSourceSelectionUIPanel")
        Me.DataSourceSelectionUIPanel.Controls.Add(Me.DataSourceSettingsEditorInstance)
        Me.DataSourceSelectionUIPanel.Name = "DataSourceSelectionUIPanel"
        resources.ApplyResources(Me.DataSourceSettingsEditorInstance, "DataSourceSettingsEditorInstance")
        Me.DataSourceSettingsEditorInstance.Name = "DataSourceSettingsEditorInstance"
        resources.ApplyResources(Me.DataSourceResultUIPanel, "DataSourceResultUIPanel")
        Me.DataSourceResultUIPanel.Name = "DataSourceResultUIPanel"
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.DataSourceResultUIPanel)
        Me.Controls.Add(Me.PreviewTableLayoutPanel)
        Me.Name = "DataSourcePreview"
        Me.PreviewTableLayoutPanel.ResumeLayout(False)
        Me.PreviewTableLayoutPanel.PerformLayout()
        Me.DataSourceSelectionUIPanel.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ExecuteButton As System.Windows.Forms.Button
    Private WithEvents DataSourceSelectionUIPanel As System.Windows.Forms.Panel
    Private WithEvents PreviewTableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents DataSourceSettingsEditorInstance As DataSourceSettingsEditor
    Protected WithEvents DataSourceResultUIPanel As System.Windows.Forms.Panel
    Friend WithEvents CommandManager1 As Commanding.CommandManager

End Class
