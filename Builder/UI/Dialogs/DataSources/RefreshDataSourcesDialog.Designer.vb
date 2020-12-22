<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RefreshDataSourcesDialog
    Inherits DialogBase2

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RefreshDataSourcesDialog))
        Me.DialogSplitContainer = New System.Windows.Forms.SplitContainer()
        Me.MultiRefreshOptionsPanel = New System.Windows.Forms.Panel()
        Me.MaxLabel = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.NumberOfProposalsUpDown = New System.Windows.Forms.NumericUpDown()
        Me.DataSourceSettingsEditorInstance = New DataSourceSettingsEditor()
        Me.ContentPlaceHolderPanel.SuspendLayout()
        CType(Me.DialogSplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DialogSplitContainer.Panel1.SuspendLayout()
        Me.DialogSplitContainer.Panel2.SuspendLayout()
        Me.DialogSplitContainer.SuspendLayout()
        Me.MultiRefreshOptionsPanel.SuspendLayout()
        CType(Me.NumberOfProposalsUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        resources.ApplyResources(Me.ContentPlaceHolderPanel, "ContentPlaceHolderPanel")
        Me.ContentPlaceHolderPanel.Controls.Add(Me.DialogSplitContainer)
        Me.ContentPlaceHolderPanel.Controls.SetChildIndex(Me.DialogSplitContainer, 0)
        resources.ApplyResources(Me.InstructionsLabel, "InstructionsLabel")
        resources.ApplyResources(Me.DialogSplitContainer, "DialogSplitContainer")
        Me.DialogSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.DialogSplitContainer.Name = "DialogSplitContainer"
        Me.DialogSplitContainer.Panel1.Controls.Add(Me.MultiRefreshOptionsPanel)
        Me.DialogSplitContainer.Panel2.Controls.Add(Me.DataSourceSettingsEditorInstance)
        Me.MultiRefreshOptionsPanel.Controls.Add(Me.MaxLabel)
        Me.MultiRefreshOptionsPanel.Controls.Add(Me.Label1)
        Me.MultiRefreshOptionsPanel.Controls.Add(Me.NumberOfProposalsUpDown)
        resources.ApplyResources(Me.MultiRefreshOptionsPanel, "MultiRefreshOptionsPanel")
        Me.MultiRefreshOptionsPanel.Name = "MultiRefreshOptionsPanel"
        resources.ApplyResources(Me.MaxLabel, "MaxLabel")
        Me.MaxLabel.Name = "MaxLabel"
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        resources.ApplyResources(Me.NumberOfProposalsUpDown, "NumberOfProposalsUpDown")
        Me.NumberOfProposalsUpDown.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.NumberOfProposalsUpDown.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumberOfProposalsUpDown.Name = "NumberOfProposalsUpDown"
        Me.NumberOfProposalsUpDown.Value = New Decimal(New Integer() {1, 0, 0, 0})
        resources.ApplyResources(Me.DataSourceSettingsEditorInstance, "DataSourceSettingsEditorInstance")
        Me.DataSourceSettingsEditorInstance.Name = "DataSourceSettingsEditorInstance"
        resources.ApplyResources(Me, "$this")
        Me.Name = "RefreshDataSourcesDialog"
        Me.ContentPlaceHolderPanel.ResumeLayout(False)
        Me.DialogSplitContainer.Panel1.ResumeLayout(False)
        Me.DialogSplitContainer.Panel2.ResumeLayout(False)
        CType(Me.DialogSplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DialogSplitContainer.ResumeLayout(False)
        Me.MultiRefreshOptionsPanel.ResumeLayout(False)
        Me.MultiRefreshOptionsPanel.PerformLayout()
        CType(Me.NumberOfProposalsUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DialogSplitContainer As System.Windows.Forms.SplitContainer
    Friend WithEvents MultiRefreshOptionsPanel As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents NumberOfProposalsUpDown As System.Windows.Forms.NumericUpDown
    Friend WithEvents DataSourceSettingsEditorInstance As DataSourceSettingsEditor
    Friend WithEvents MaxLabel As System.Windows.Forms.Label

End Class
