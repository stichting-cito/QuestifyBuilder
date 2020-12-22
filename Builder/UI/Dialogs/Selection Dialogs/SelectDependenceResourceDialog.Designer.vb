<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SelectDependenceResourceDialog
    Inherits System.Windows.Forms.Form

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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SelectDependenceResourceDialog))
        Me.ResourceTabControl = New System.Windows.Forms.TabControl()
        Me.ItemTabPage = New System.Windows.Forms.TabPage()
        Me.ItemGrid = New Questify.Builder.UI.ItemGrid()
        Me.ItemLayoutTemplateTabPage = New System.Windows.Forms.TabPage()
        Me.ItemLayoutTemplateGrid = New Questify.Builder.UI.ItemLayoutTemplateGrid()
        Me.ControlLayoutTemplateTabPage = New System.Windows.Forms.TabPage()
        Me.ControlTemplateGrid = New Questify.Builder.UI.ControlTemplateGrid()
        Me.GenericTabPage = New System.Windows.Forms.TabPage()
        Me.MediaGrid = New Questify.Builder.UI.MediaGrid()
        Me.AspectTabPage = New System.Windows.Forms.TabPage()
        Me.AspectGrid = New Questify.Builder.UI.AspectGrid()
        Me.ButtonPanel = New System.Windows.Forms.Panel()
        Me.CustomButton = New System.Windows.Forms.Button()
        Me.OkButton = New System.Windows.Forms.Button()
        Me.CloseButton = New System.Windows.Forms.Button()
        Me.InstructionsLabel = New System.Windows.Forms.Label()
        Me.GridBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.BackGroundDataWorkerPool = New Questify.Builder.UI.BackGroundWorkerPool()
        Me.ResourceTabControl.SuspendLayout()
        Me.ItemTabPage.SuspendLayout()
        Me.ItemLayoutTemplateTabPage.SuspendLayout()
        Me.ControlLayoutTemplateTabPage.SuspendLayout()
        Me.GenericTabPage.SuspendLayout()
        Me.AspectTabPage.SuspendLayout()
        Me.ButtonPanel.SuspendLayout()
        CType(Me.GridBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        resources.ApplyResources(Me.ResourceTabControl, "ResourceTabControl")
        Me.ResourceTabControl.Controls.Add(Me.ItemTabPage)
        Me.ResourceTabControl.Controls.Add(Me.ItemLayoutTemplateTabPage)
        Me.ResourceTabControl.Controls.Add(Me.ControlLayoutTemplateTabPage)
        Me.ResourceTabControl.Controls.Add(Me.GenericTabPage)
        Me.ResourceTabControl.Controls.Add(Me.AspectTabPage)
        Me.ResourceTabControl.Multiline = True
        Me.ResourceTabControl.Name = "ResourceTabControl"
        Me.ResourceTabControl.SelectedIndex = 0
        resources.ApplyResources(Me.ItemTabPage, "ItemTabPage")
        Me.ItemTabPage.Controls.Add(Me.ItemGrid)
        Me.ItemTabPage.Name = "ItemTabPage"
        Me.ItemTabPage.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.ItemGrid, "ItemGrid")
        Me.ItemGrid.CustomPropertyColumnsVisible = False
        Me.ItemGrid.DataMember = ""
        Me.ItemGrid.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.ItemResourceEntity)
        Me.ItemGrid.EnableFiltering = False
        Me.ItemGrid.GridContentContextMenuDisabled = False
        Me.ItemGrid.MultiSelect = True
        Me.ItemGrid.Name = "ItemGrid"
        Me.ItemGrid.SearchToolbarVisibility = False
        Me.ItemGrid.SelectedEntity = Nothing
        Me.ItemGrid.ShowDisabledRowsAsGray = False
        Me.ItemGrid.UseGridAsItemPicker = False
        resources.ApplyResources(Me.ItemLayoutTemplateTabPage, "ItemLayoutTemplateTabPage")
        Me.ItemLayoutTemplateTabPage.Controls.Add(Me.ItemLayoutTemplateGrid)
        Me.ItemLayoutTemplateTabPage.Name = "ItemLayoutTemplateTabPage"
        Me.ItemLayoutTemplateTabPage.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.ItemLayoutTemplateGrid, "ItemLayoutTemplateGrid")
        Me.ItemLayoutTemplateGrid.CustomPropertyColumnsVisible = False
        Me.ItemLayoutTemplateGrid.DataMember = ""
        Me.ItemLayoutTemplateGrid.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.ItemLayoutTemplateResourceEntity)
        Me.ItemLayoutTemplateGrid.EnableFiltering = False
        Me.ItemLayoutTemplateGrid.GridContentContextMenuDisabled = False
        Me.ItemLayoutTemplateGrid.Name = "ItemLayoutTemplateGrid"
        Me.ItemLayoutTemplateGrid.SelectedEntity = Nothing
        Me.ItemLayoutTemplateGrid.ShowDisabledRowsAsGray = False
        Me.ItemLayoutTemplateGrid.UseGridAsItemPicker = False
        resources.ApplyResources(Me.ControlLayoutTemplateTabPage, "ControlLayoutTemplateTabPage")
        Me.ControlLayoutTemplateTabPage.Controls.Add(Me.ControlTemplateGrid)
        Me.ControlLayoutTemplateTabPage.Name = "ControlLayoutTemplateTabPage"
        Me.ControlLayoutTemplateTabPage.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.ControlTemplateGrid, "ControlTemplateGrid")
        Me.ControlTemplateGrid.CustomPropertyColumnsVisible = False
        Me.ControlTemplateGrid.DataMember = ""
        Me.ControlTemplateGrid.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.ControlTemplateResourceEntity)
        Me.ControlTemplateGrid.EnableFiltering = False
        Me.ControlTemplateGrid.GridContentContextMenuDisabled = False
        Me.ControlTemplateGrid.MultiSelect = True
        Me.ControlTemplateGrid.Name = "ControlTemplateGrid"
        Me.ControlTemplateGrid.SelectedEntity = Nothing
        Me.ControlTemplateGrid.ShowDisabledRowsAsGray = False
        Me.ControlTemplateGrid.UseGridAsItemPicker = False
        resources.ApplyResources(Me.GenericTabPage, "GenericTabPage")
        Me.GenericTabPage.Controls.Add(Me.MediaGrid)
        Me.GenericTabPage.Name = "GenericTabPage"
        Me.GenericTabPage.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.MediaGrid, "MediaGrid")
        Me.MediaGrid.CustomPropertyColumnsVisible = False
        Me.MediaGrid.DataMember = ""
        Me.MediaGrid.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.GenericResourceEntity)
        Me.MediaGrid.EnableFiltering = False
        Me.MediaGrid.GridContentContextMenuDisabled = False
        Me.MediaGrid.MultiSelect = True
        Me.MediaGrid.Name = "MediaGrid"
        Me.MediaGrid.SelectedEntity = Nothing
        Me.MediaGrid.ShowDisabledRowsAsGray = False
        Me.MediaGrid.UseGridAsItemPicker = False
        resources.ApplyResources(Me.AspectTabPage, "AspectTabPage")
        Me.AspectTabPage.Controls.Add(Me.AspectGrid)
        Me.AspectTabPage.Name = "AspectTabPage"
        Me.AspectTabPage.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.AspectGrid, "AspectGrid")
        Me.AspectGrid.CustomPropertyColumnsVisible = False
        Me.AspectGrid.DataMember = ""
        Me.AspectGrid.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.AspectResourceEntity)
        Me.AspectGrid.EnableFiltering = False
        Me.AspectGrid.GridContentContextMenuDisabled = False
        Me.AspectGrid.Name = "AspectGrid"
        Me.AspectGrid.SelectedEntity = Nothing
        Me.AspectGrid.ShowDisabledRowsAsGray = False
        Me.AspectGrid.UseGridAsItemPicker = False
        resources.ApplyResources(Me.ButtonPanel, "ButtonPanel")
        Me.ButtonPanel.Controls.Add(Me.CustomButton)
        Me.ButtonPanel.Controls.Add(Me.OkButton)
        Me.ButtonPanel.Controls.Add(Me.CloseButton)
        Me.ButtonPanel.Name = "ButtonPanel"
        resources.ApplyResources(Me.CustomButton, "CustomButton")
        Me.CustomButton.Name = "CustomButton"
        resources.ApplyResources(Me.OkButton, "OkButton")
        Me.OkButton.Name = "OkButton"
        resources.ApplyResources(Me.CloseButton, "CloseButton")
        Me.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.CloseButton.Name = "CloseButton"
        resources.ApplyResources(Me.InstructionsLabel, "InstructionsLabel")
        Me.InstructionsLabel.Name = "InstructionsLabel"
        Me.GridBindingSource.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.ResourceEntity)
        Me.BackGroundDataWorkerPool.MaxWorkers = 3
        Me.BackGroundDataWorkerPool.StatusbarListener = Nothing
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ResourceTabControl)
        Me.Controls.Add(Me.InstructionsLabel)
        Me.Controls.Add(Me.ButtonPanel)
        Me.MinimizeBox = False
        Me.Name = "SelectDependenceResourceDialog"
        Me.ResourceTabControl.ResumeLayout(False)
        Me.ItemTabPage.ResumeLayout(False)
        Me.ItemLayoutTemplateTabPage.ResumeLayout(False)
        Me.ControlLayoutTemplateTabPage.ResumeLayout(False)
        Me.GenericTabPage.ResumeLayout(False)
        Me.AspectTabPage.ResumeLayout(False)
        Me.ButtonPanel.ResumeLayout(False)
        CType(Me.GridBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Protected WithEvents InstructionsLabel As System.Windows.Forms.Label
    Friend WithEvents ButtonPanel As System.Windows.Forms.Panel
    Protected WithEvents CustomButton As System.Windows.Forms.Button
    Protected WithEvents CloseButton As System.Windows.Forms.Button
    Protected WithEvents OkButton As System.Windows.Forms.Button
    Friend WithEvents GridBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ResourceTabControl As System.Windows.Forms.TabControl
    Friend WithEvents ItemTabPage As System.Windows.Forms.TabPage
    Friend WithEvents ItemLayoutTemplateTabPage As System.Windows.Forms.TabPage
    Friend WithEvents ControlLayoutTemplateTabPage As System.Windows.Forms.TabPage
    Friend WithEvents GenericTabPage As System.Windows.Forms.TabPage
    Friend WithEvents ItemLayoutTemplateGrid As Questify.Builder.UI.ItemLayoutTemplateGrid
    Friend WithEvents ControlTemplateGrid As Questify.Builder.UI.ControlTemplateGrid
    Friend WithEvents MediaGrid As Questify.Builder.UI.MediaGrid
    Friend WithEvents BackGroundDataWorkerPool As Questify.Builder.UI.BackGroundWorkerPool
    Friend WithEvents AspectTabPage As System.Windows.Forms.TabPage
    Friend WithEvents AspectGrid As Questify.Builder.UI.AspectGrid
    Friend WithEvents ItemGrid As Questify.Builder.UI.ItemGrid
End Class
