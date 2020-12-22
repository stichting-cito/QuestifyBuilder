<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SelectTestPackageResourceDialog
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SelectTestPackageResourceDialog))
        Me.GridBackgroundWorker = New System.ComponentModel.BackgroundWorker()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.FooterPanel = New System.Windows.Forms.Panel()
        Me.AddButton = New System.Windows.Forms.Button()
        Me.CloseButton = New System.Windows.Forms.Button()
        Me.HeaderPanel = New System.Windows.Forms.Panel()
        Me.InstructionsLabel = New System.Windows.Forms.Label()
        Me.GridPlaceholderPanel = New System.Windows.Forms.Panel()
        Me.TestPackageGrid = New Questify.Builder.UI.TestPackageGrid()
        Me.Panel1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.FooterPanel.SuspendLayout()
        Me.HeaderPanel.SuspendLayout()
        Me.SuspendLayout()
        Me.Panel1.Controls.Add(Me.TableLayoutPanel1)
        Me.Panel1.Controls.Add(Me.GridPlaceholderPanel)
        resources.ApplyResources(Me.Panel1, "Panel1")
        Me.Panel1.Name = "Panel1"
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.FooterPanel, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.HeaderPanel, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TestPackageGrid, 0, 1)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.FooterPanel.Controls.Add(Me.AddButton)
        Me.FooterPanel.Controls.Add(Me.CloseButton)
        resources.ApplyResources(Me.FooterPanel, "FooterPanel")
        Me.FooterPanel.Name = "FooterPanel"
        resources.ApplyResources(Me.AddButton, "AddButton")
        Me.AddButton.Image = Global.Questify.Builder.UI.My.Resources.Resources.add_icon_16
        Me.AddButton.Name = "AddButton"
        Me.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        resources.ApplyResources(Me.CloseButton, "CloseButton")
        Me.CloseButton.Name = "CloseButton"
        Me.HeaderPanel.Controls.Add(Me.InstructionsLabel)
        resources.ApplyResources(Me.HeaderPanel, "HeaderPanel")
        Me.HeaderPanel.Name = "HeaderPanel"
        resources.ApplyResources(Me.InstructionsLabel, "InstructionsLabel")
        Me.InstructionsLabel.Name = "InstructionsLabel"
        resources.ApplyResources(Me.GridPlaceholderPanel, "GridPlaceholderPanel")
        Me.GridPlaceholderPanel.Name = "GridPlaceholderPanel"
        Me.TestPackageGrid.CustomPropertyColumnsVisible = False
        Me.TestPackageGrid.DataMember = ""
        Me.TestPackageGrid.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.AssessmentTestResourceEntity)
        resources.ApplyResources(Me.TestPackageGrid, "TestPackageGrid")
        Me.TestPackageGrid.EnableFiltering = False
        Me.TestPackageGrid.GridContentContextMenuDisabled = False
        Me.TestPackageGrid.Name = "TestPackageGrid"
        Me.TestPackageGrid.SelectedEntity = Nothing
        Me.TestPackageGrid.ShowDisabledRowsAsGray = False
        Me.TestPackageGrid.UseGridAsItemPicker = False
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.Panel1)
        Me.Name = "SelectTestPackageResourceDialog"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.FooterPanel.ResumeLayout(False)
        Me.FooterPanel.PerformLayout()
        Me.HeaderPanel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GridBackgroundWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Protected WithEvents FooterPanel As System.Windows.Forms.Panel
    Protected WithEvents AddButton As System.Windows.Forms.Button
    Protected WithEvents CloseButton As System.Windows.Forms.Button
    Friend WithEvents HeaderPanel As System.Windows.Forms.Panel
    Protected WithEvents InstructionsLabel As System.Windows.Forms.Label
    Friend WithEvents TestPackageGrid As Questify.Builder.UI.TestPackageGrid
    Protected WithEvents GridPlaceholderPanel As System.Windows.Forms.Panel
End Class
