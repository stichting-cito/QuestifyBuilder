Imports Enums

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.MainBankBrowser = New Questify.Builder.UI.BankBrowser()
        Me.ExportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MainStatusStrip = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusBarMessage = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripEmptySpace = New System.Windows.Forms.ToolStripStatusLabel()
        Me.BreadcrumbBankPartToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.BreadcrumbStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripEmptySpace2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.UsernameStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.MainToolStrip = New System.Windows.Forms.ToolStrip()
        Me.NewToolStripSplitButton = New System.Windows.Forms.ToolStripSplitButton()
        Me.ItemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SelectionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TestToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TestPackagesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AspectsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MediaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CustomPropertyMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TestTemplateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeliveryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SourceTextTemplateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefreshToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.EditToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.DeleteToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.PreviewToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.PublishToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ImportToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ExportToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ReportsToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.SearchToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.PropertyToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.HarmonizeDependentItemsToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ExportToExcelToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.MainMenu = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewItemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewSelectionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewTestToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewTestPackageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewAspectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewMediaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewTestTemplateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewSourceTextTemplateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparatorFile1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ImportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PublishToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PreviewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparatorFile2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ProperiesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.SelectAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SearchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.RefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HarmonizeDependentItemsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.MoveResourceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UserAccountToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.ReportsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.MultiEditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.OptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.AnnouncementControlToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.FolderBrowserDialog = New System.Windows.Forms.FolderBrowserDialog()
        Me.BackGroundDataWorkerPool = New Questify.Builder.UI.BackGroundWorkerPool()
        Me.MainTabControl = New System.Windows.Forms.TabControl()
        Me.TabPageStart = New System.Windows.Forms.TabPage()
        Me.BankStartPage = New Questify.Builder.UI.BankStartPageControl()
        Me.TabPageItems = New System.Windows.Forms.TabPage()
        Me.ItemGrid = New Questify.Builder.UI.ItemGrid()
        Me.TabPageSelections = New System.Windows.Forms.TabPage()
        Me.DataSourceGrid = New Questify.Builder.UI.DataSourceGrid()
        Me.TabPageTests = New System.Windows.Forms.TabPage()
        Me.TestGrid = New Questify.Builder.UI.TestGrid()
        Me.TabPagePackages = New System.Windows.Forms.TabPage()
        Me.TestPackageGrid = New Questify.Builder.UI.TestPackageGrid()
        Me.TabPageAspects = New System.Windows.Forms.TabPage()
        Me.AspectGrid = New Questify.Builder.UI.AspectGrid()
        Me.TabPageMedia = New System.Windows.Forms.TabPage()
        Me.MediaGrid = New Questify.Builder.UI.MediaGrid()
        Me.TabPageCustomProperties = New System.Windows.Forms.TabPage()
        Me.CustomPropertyGrid = New Questify.Builder.UI.CustomPropertyGrid()
        Me.TabPageItemLayoutTemplates = New System.Windows.Forms.TabPage()
        Me.ItemLayoutTemplateGrid = New Questify.Builder.UI.ItemLayoutTemplateGrid()
        Me.TabPageSelectionTemplates = New System.Windows.Forms.TabPage()
        Me.DataSourceTemplateGrid = New Questify.Builder.UI.DataSourceTemplateGrid()
        Me.TabPageTestTemplates = New System.Windows.Forms.TabPage()
        Me.TestTemplateGrid = New Questify.Builder.UI.TestTemplateGrid()
        Me.TabPageControlTemplates = New System.Windows.Forms.TabPage()
        Me.ControlTemplateGrid = New Questify.Builder.UI.ControlTemplateGrid()
        Me.ImageListTabs = New System.Windows.Forms.ImageList(Me.components)
        Me.SplitContainerMain = New System.Windows.Forms.SplitContainer()
        Me.MainStatusStrip.SuspendLayout()
        Me.MainToolStrip.SuspendLayout()
        Me.MainMenu.SuspendLayout()
        Me.MainTabControl.SuspendLayout()
        Me.TabPageStart.SuspendLayout()
        Me.TabPageItems.SuspendLayout()
        Me.TabPageSelections.SuspendLayout()
        Me.TabPageTests.SuspendLayout()
        Me.TabPagePackages.SuspendLayout()
        Me.TabPageAspects.SuspendLayout()
        Me.TabPageMedia.SuspendLayout()
        Me.TabPageCustomProperties.SuspendLayout()
        Me.TabPageItemLayoutTemplates.SuspendLayout()
        Me.TabPageSelectionTemplates.SuspendLayout()
        Me.TabPageTestTemplates.SuspendLayout()
        Me.TabPageControlTemplates.SuspendLayout()
        CType(Me.SplitContainerMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerMain.Panel1.SuspendLayout()
        Me.SplitContainerMain.Panel2.SuspendLayout()
        Me.SplitContainerMain.SuspendLayout()
        Me.SuspendLayout()
        Me.MainBankBrowser.DataMember = ""
        Me.MainBankBrowser.DataSource = Nothing
        resources.ApplyResources(Me.MainBankBrowser, "MainBankBrowser")
        Me.MainBankBrowser.EnableFiltering = False
        Me.MainBankBrowser.Name = "MainBankBrowser"
        Me.MainBankBrowser.SelectedBank = Nothing
        resources.ApplyResources(Me.ExportToolStripMenuItem, "ExportToolStripMenuItem")
        Me.ExportToolStripMenuItem.Name = "ExportToolStripMenuItem"
        resources.ApplyResources(Me.MainStatusStrip, "MainStatusStrip")
        Me.MainStatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusBarMessage, Me.ToolStripEmptySpace, Me.BreadcrumbBankPartToolStripStatusLabel, Me.BreadcrumbStripStatusLabel, Me.ToolStripEmptySpace2, Me.UsernameStatusLabel})
        Me.MainStatusStrip.Name = "MainStatusStrip"
        Me.MainStatusStrip.ShowItemToolTips = True
        resources.ApplyResources(Me.ToolStripStatusBarMessage, "ToolStripStatusBarMessage")
        Me.ToolStripStatusBarMessage.Name = "ToolStripStatusBarMessage"
        resources.ApplyResources(Me.ToolStripEmptySpace, "ToolStripEmptySpace")
        Me.ToolStripEmptySpace.Name = "ToolStripEmptySpace"
        Me.ToolStripEmptySpace.Spring = True
        resources.ApplyResources(Me.BreadcrumbBankPartToolStripStatusLabel, "BreadcrumbBankPartToolStripStatusLabel")
        Me.BreadcrumbBankPartToolStripStatusLabel.Name = "BreadcrumbBankPartToolStripStatusLabel"
        resources.ApplyResources(Me.BreadcrumbStripStatusLabel, "BreadcrumbStripStatusLabel")
        Me.BreadcrumbStripStatusLabel.Name = "BreadcrumbStripStatusLabel"
        Me.ToolStripEmptySpace2.Name = "ToolStripEmptySpace2"
        resources.ApplyResources(Me.ToolStripEmptySpace2, "ToolStripEmptySpace2")
        Me.ToolStripEmptySpace2.Spring = True
        Me.UsernameStatusLabel.ActiveLinkColor = System.Drawing.Color.Black
        resources.ApplyResources(Me.UsernameStatusLabel, "UsernameStatusLabel")
        Me.UsernameStatusLabel.Image = Global.Questify.Builder.Client.My.Resources.Resources.user__16x16
        Me.UsernameStatusLabel.IsLink = True
        Me.UsernameStatusLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.UsernameStatusLabel.LinkColor = System.Drawing.Color.Black
        Me.UsernameStatusLabel.Name = "UsernameStatusLabel"
        Me.UsernameStatusLabel.Padding = New System.Windows.Forms.Padding(2, 0, 5, 0)
        resources.ApplyResources(Me.MainToolStrip, "MainToolStrip")
        Me.MainToolStrip.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.MainToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripSplitButton, Me.RefreshToolStripButton, Me.EditToolStripButton, Me.DeleteToolStripButton, Me.PreviewToolStripButton, Me.PublishToolStripButton, Me.ImportToolStripButton, Me.ExportToolStripButton, Me.ReportsToolStripButton, Me.SearchToolStripButton, Me.PropertyToolStripButton, Me.HarmonizeDependentItemsToolStripButton})
        Me.MainToolStrip.Name = "MainToolStrip"
        Me.MainToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.NewToolStripSplitButton.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ItemToolStripMenuItem, Me.SelectionToolStripMenuItem, Me.TestToolStripMenuItem, Me.TestPackagesToolStripMenuItem, Me.AspectsToolStripMenuItem, Me.MediaToolStripMenuItem, Me.CustomPropertyMenuItem, Me.TestTemplateToolStripMenuItem, Me.DeliveryToolStripMenuItem, Me.SourceTextTemplateToolStripMenuItem})
        resources.ApplyResources(Me.NewToolStripSplitButton, "NewToolStripSplitButton")
        Me.NewToolStripSplitButton.Name = "NewToolStripSplitButton"
        resources.ApplyResources(Me.ItemToolStripMenuItem, "ItemToolStripMenuItem")
        Me.ItemToolStripMenuItem.Name = "ItemToolStripMenuItem"
        Me.ItemToolStripMenuItem.Tag = "Items"
        Me.SelectionToolStripMenuItem.Image = Global.Questify.Builder.Client.My.Resources.Resources.Appearance5_Image
        Me.SelectionToolStripMenuItem.Name = "SelectionToolStripMenuItem"
        resources.ApplyResources(Me.SelectionToolStripMenuItem, "SelectionToolStripMenuItem")
        Me.SelectionToolStripMenuItem.Tag = "DataSources"
        resources.ApplyResources(Me.TestToolStripMenuItem, "TestToolStripMenuItem")
        Me.TestToolStripMenuItem.Name = "TestToolStripMenuItem"
        Me.TestToolStripMenuItem.Tag = "Tests"
        Me.TestPackagesToolStripMenuItem.Image = Global.Questify.Builder.Client.My.Resources.Resources.TestPackageToolStripMenuItem_Image
        Me.TestPackagesToolStripMenuItem.Name = "TestPackagesToolStripMenuItem"
        resources.ApplyResources(Me.TestPackagesToolStripMenuItem, "TestPackagesToolStripMenuItem")
        Me.TestPackagesToolStripMenuItem.Tag = "TestPackages"
        resources.ApplyResources(Me.AspectsToolStripMenuItem, "AspectsToolStripMenuItem")
        Me.AspectsToolStripMenuItem.Name = "AspectsToolStripMenuItem"
        Me.AspectsToolStripMenuItem.Tag = "Aspects"
        resources.ApplyResources(Me.MediaToolStripMenuItem, "MediaToolStripMenuItem")
        Me.MediaToolStripMenuItem.Name = "MediaToolStripMenuItem"
        Me.MediaToolStripMenuItem.Tag = "Media"
        Me.CustomPropertyMenuItem.Image = Global.Questify.Builder.Client.My.Resources.Resources.options_16x16
        Me.CustomPropertyMenuItem.Name = "CustomPropertyMenuItem"
        resources.ApplyResources(Me.CustomPropertyMenuItem, "CustomPropertyMenuItem")
        Me.CustomPropertyMenuItem.Tag = "CustomProperties"
        resources.ApplyResources(Me.TestTemplateToolStripMenuItem, "TestTemplateToolStripMenuItem")
        Me.TestTemplateToolStripMenuItem.Name = "TestTemplateToolStripMenuItem"
        Me.TestTemplateToolStripMenuItem.Tag = "TestTemplates"
        Me.DeliveryToolStripMenuItem.Image = Global.Questify.Builder.Client.My.Resources.Resources.options_16x16
        Me.DeliveryToolStripMenuItem.Name = "DeliveryToolStripMenuItem"
        resources.ApplyResources(Me.DeliveryToolStripMenuItem, "DeliveryToolStripMenuItem")
        Me.DeliveryToolStripMenuItem.Tag = "Deliveries"
        Me.SourceTextTemplateToolStripMenuItem.Image = Global.Questify.Builder.Client.My.Resources.Resources.NewTestTemplateToolStripMenuItem_Image
        Me.SourceTextTemplateToolStripMenuItem.Name = "SourceTextTemplateToolStripMenuItem"
        resources.ApplyResources(Me.SourceTextTemplateToolStripMenuItem, "SourceTextTemplateToolStripMenuItem")
        Me.SourceTextTemplateToolStripMenuItem.Tag = "Media"
        resources.ApplyResources(Me.RefreshToolStripButton, "RefreshToolStripButton")
        Me.RefreshToolStripButton.Name = "RefreshToolStripButton"
        resources.ApplyResources(Me.EditToolStripButton, "EditToolStripButton")
        Me.EditToolStripButton.Name = "EditToolStripButton"
        resources.ApplyResources(Me.DeleteToolStripButton, "DeleteToolStripButton")
        Me.DeleteToolStripButton.Name = "DeleteToolStripButton"
        resources.ApplyResources(Me.PreviewToolStripButton, "PreviewToolStripButton")
        Me.PreviewToolStripButton.Name = "PreviewToolStripButton"
        resources.ApplyResources(Me.PublishToolStripButton, "PublishToolStripButton")
        Me.PublishToolStripButton.Name = "PublishToolStripButton"
        resources.ApplyResources(Me.ImportToolStripButton, "ImportToolStripButton")
        Me.ImportToolStripButton.Name = "ImportToolStripButton"
        resources.ApplyResources(Me.ExportToolStripButton, "ExportToolStripButton")
        Me.ExportToolStripButton.Name = "ExportToolStripButton"
        resources.ApplyResources(Me.ReportsToolStripButton, "ReportsToolStripButton")
        Me.ReportsToolStripButton.Name = "ReportsToolStripButton"
        Me.SearchToolStripButton.CheckOnClick = True
        resources.ApplyResources(Me.SearchToolStripButton, "SearchToolStripButton")
        Me.SearchToolStripButton.Name = "SearchToolStripButton"
        resources.ApplyResources(Me.PropertyToolStripButton, "PropertyToolStripButton")
        Me.PropertyToolStripButton.Name = "PropertyToolStripButton"
        resources.ApplyResources(Me.HarmonizeDependentItemsToolStripButton, "HarmonizeDependentItemsToolStripButton")
        Me.HarmonizeDependentItemsToolStripButton.Image = Global.Questify.Builder.Client.My.Resources.Resources.RefreshToolStripButton_Image
        Me.HarmonizeDependentItemsToolStripButton.Name = "HarmonizeDependentItemsToolStripButton"
        Me.ExportToExcelToolStripButton.Name = "ExportToExcelToolStripButton"
        resources.ApplyResources(Me.ExportToExcelToolStripButton, "ExportToExcelToolStripButton")
        resources.ApplyResources(Me.MainMenu, "MainMenu")
        Me.MainMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.EditToolStripMenuItem, Me.ToolsToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MainMenu.Name = "MainMenu"
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripMenuItem, Me.EditFileToolStripMenuItem, Me.ToolStripSeparatorFile1, Me.ImportToolStripMenuItem, Me.ExportToolStripMenuItem, Me.PublishToolStripMenuItem, Me.PreviewToolStripMenuItem, Me.ToolStripSeparatorFile2, Me.ProperiesToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        resources.ApplyResources(Me.FileToolStripMenuItem, "FileToolStripMenuItem")
        Me.NewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewItemToolStripMenuItem, Me.NewSelectionToolStripMenuItem, Me.NewTestToolStripMenuItem, Me.NewTestPackageToolStripMenuItem, Me.NewAspectToolStripMenuItem, Me.NewMediaToolStripMenuItem, Me.NewTestTemplateToolStripMenuItem, Me.NewSourceTextTemplateToolStripMenuItem})
        resources.ApplyResources(Me.NewToolStripMenuItem, "NewToolStripMenuItem")
        Me.NewToolStripMenuItem.Name = "NewToolStripMenuItem"
        resources.ApplyResources(Me.NewItemToolStripMenuItem, "NewItemToolStripMenuItem")
        Me.NewItemToolStripMenuItem.Name = "NewItemToolStripMenuItem"
        Me.NewItemToolStripMenuItem.Tag = "Items"
        Me.NewSelectionToolStripMenuItem.Image = Global.Questify.Builder.Client.My.Resources.Resources.Appearance5_Image
        Me.NewSelectionToolStripMenuItem.Name = "NewSelectionToolStripMenuItem"
        resources.ApplyResources(Me.NewSelectionToolStripMenuItem, "NewSelectionToolStripMenuItem")
        Me.NewSelectionToolStripMenuItem.Tag = "DataSources"
        resources.ApplyResources(Me.NewTestToolStripMenuItem, "NewTestToolStripMenuItem")
        Me.NewTestToolStripMenuItem.Name = "NewTestToolStripMenuItem"
        Me.NewTestToolStripMenuItem.Tag = "Tests"
        Me.NewTestPackageToolStripMenuItem.Image = Global.Questify.Builder.Client.My.Resources.Resources.NewTestPackageToolStripMenuItem_Image
        Me.NewTestPackageToolStripMenuItem.Name = "NewTestPackageToolStripMenuItem"
        resources.ApplyResources(Me.NewTestPackageToolStripMenuItem, "NewTestPackageToolStripMenuItem")
        Me.NewTestPackageToolStripMenuItem.Tag = "TestPackages"
        resources.ApplyResources(Me.NewAspectToolStripMenuItem, "NewAspectToolStripMenuItem")
        Me.NewAspectToolStripMenuItem.Name = "NewAspectToolStripMenuItem"
        Me.NewAspectToolStripMenuItem.Tag = "Aspects"
        resources.ApplyResources(Me.NewMediaToolStripMenuItem, "NewMediaToolStripMenuItem")
        Me.NewMediaToolStripMenuItem.Name = "NewMediaToolStripMenuItem"
        Me.NewMediaToolStripMenuItem.Tag = "Media"
        resources.ApplyResources(Me.NewTestTemplateToolStripMenuItem, "NewTestTemplateToolStripMenuItem")
        Me.NewTestTemplateToolStripMenuItem.Name = "NewTestTemplateToolStripMenuItem"
        Me.NewTestTemplateToolStripMenuItem.Tag = "TestTemplates"
        Me.NewSourceTextTemplateToolStripMenuItem.Image = Global.Questify.Builder.Client.My.Resources.Resources.NewTestTemplateToolStripMenuItem_Image
        Me.NewSourceTextTemplateToolStripMenuItem.Name = "NewSourceTextTemplateToolStripMenuItem"
        resources.ApplyResources(Me.NewSourceTextTemplateToolStripMenuItem, "NewSourceTextTemplateToolStripMenuItem")
        Me.NewSourceTextTemplateToolStripMenuItem.Tag = "Media"
        resources.ApplyResources(Me.EditFileToolStripMenuItem, "EditFileToolStripMenuItem")
        Me.EditFileToolStripMenuItem.Name = "EditFileToolStripMenuItem"
        Me.ToolStripSeparatorFile1.Name = "ToolStripSeparatorFile1"
        resources.ApplyResources(Me.ToolStripSeparatorFile1, "ToolStripSeparatorFile1")
        resources.ApplyResources(Me.ImportToolStripMenuItem, "ImportToolStripMenuItem")
        Me.ImportToolStripMenuItem.Name = "ImportToolStripMenuItem"
        resources.ApplyResources(Me.PublishToolStripMenuItem, "PublishToolStripMenuItem")
        Me.PublishToolStripMenuItem.Image = Global.Questify.Builder.Client.My.Resources.Resources.online_16x16
        Me.PublishToolStripMenuItem.Name = "PublishToolStripMenuItem"
        resources.ApplyResources(Me.PreviewToolStripMenuItem, "PreviewToolStripMenuItem")
        Me.PreviewToolStripMenuItem.Name = "PreviewToolStripMenuItem"
        Me.ToolStripSeparatorFile2.Name = "ToolStripSeparatorFile2"
        resources.ApplyResources(Me.ToolStripSeparatorFile2, "ToolStripSeparatorFile2")
        resources.ApplyResources(Me.ProperiesToolStripMenuItem, "ProperiesToolStripMenuItem")
        Me.ProperiesToolStripMenuItem.Name = "ProperiesToolStripMenuItem"
        Me.ExitToolStripMenuItem.Image = Global.Questify.Builder.Client.My.Resources.Resources.close_16x16
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        resources.ApplyResources(Me.ExitToolStripMenuItem, "ExitToolStripMenuItem")
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DeleteToolStripMenuItem, Me.ToolStripSeparator2, Me.SelectAllToolStripMenuItem, Me.SearchToolStripMenuItem, Me.ToolStripSeparator3, Me.RefreshToolStripMenuItem, Me.HarmonizeDependentItemsToolStripMenuItem, Me.ToolStripSeparator6, Me.MoveResourceToolStripMenuItem})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        resources.ApplyResources(Me.EditToolStripMenuItem, "EditToolStripMenuItem")
        resources.ApplyResources(Me.DeleteToolStripMenuItem, "DeleteToolStripMenuItem")
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        resources.ApplyResources(Me.ToolStripSeparator2, "ToolStripSeparator2")
        Me.SelectAllToolStripMenuItem.Name = "SelectAllToolStripMenuItem"
        resources.ApplyResources(Me.SelectAllToolStripMenuItem, "SelectAllToolStripMenuItem")
        Me.SearchToolStripMenuItem.CheckOnClick = True
        resources.ApplyResources(Me.SearchToolStripMenuItem, "SearchToolStripMenuItem")
        Me.SearchToolStripMenuItem.Name = "SearchToolStripMenuItem"
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        resources.ApplyResources(Me.ToolStripSeparator3, "ToolStripSeparator3")
        resources.ApplyResources(Me.RefreshToolStripMenuItem, "RefreshToolStripMenuItem")
        Me.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem"
        resources.ApplyResources(Me.HarmonizeDependentItemsToolStripMenuItem, "HarmonizeDependentItemsToolStripMenuItem")
        Me.HarmonizeDependentItemsToolStripMenuItem.Image = Global.Questify.Builder.Client.My.Resources.Resources.RefreshToolStripMenuItem_Image
        Me.HarmonizeDependentItemsToolStripMenuItem.Name = "HarmonizeDependentItemsToolStripMenuItem"
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        resources.ApplyResources(Me.ToolStripSeparator6, "ToolStripSeparator6")
        Me.MoveResourceToolStripMenuItem.Name = "MoveResourceToolStripMenuItem"
        resources.ApplyResources(Me.MoveResourceToolStripMenuItem, "MoveResourceToolStripMenuItem")
        Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UserAccountToolStripMenuItem, Me.ToolStripSeparator4, Me.ReportsToolStripMenuItem, Me.ToolStripSeparator5, Me.MultiEditToolStripMenuItem, Me.ToolStripSeparator1, Me.OptionsToolStripMenuItem, Me.ToolStripSeparator7, Me.AnnouncementControlToolStripMenuItem})
        Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        resources.ApplyResources(Me.ToolsToolStripMenuItem, "ToolsToolStripMenuItem")
        resources.ApplyResources(Me.UserAccountToolStripMenuItem, "UserAccountToolStripMenuItem")
        Me.UserAccountToolStripMenuItem.Name = "UserAccountToolStripMenuItem"
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        resources.ApplyResources(Me.ToolStripSeparator4, "ToolStripSeparator4")
        resources.ApplyResources(Me.ReportsToolStripMenuItem, "ReportsToolStripMenuItem")
        Me.ReportsToolStripMenuItem.Name = "ReportsToolStripMenuItem"
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        resources.ApplyResources(Me.ToolStripSeparator5, "ToolStripSeparator5")
        Me.MultiEditToolStripMenuItem.Name = "MultiEditToolStripMenuItem"
        resources.ApplyResources(Me.MultiEditToolStripMenuItem, "MultiEditToolStripMenuItem")
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        resources.ApplyResources(Me.ToolStripSeparator1, "ToolStripSeparator1")
        Me.OptionsToolStripMenuItem.Image = Global.Questify.Builder.Client.My.Resources.Resources.options_16x16
        Me.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem"
        resources.ApplyResources(Me.OptionsToolStripMenuItem, "OptionsToolStripMenuItem")
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        resources.ApplyResources(Me.ToolStripSeparator7, "ToolStripSeparator7")
        Me.AnnouncementControlToolStripMenuItem.Name = "AnnouncementControlToolStripMenuItem"
        resources.ApplyResources(Me.AnnouncementControlToolStripMenuItem, "AnnouncementControlToolStripMenuItem")
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        resources.ApplyResources(Me.HelpToolStripMenuItem, "HelpToolStripMenuItem")
        resources.ApplyResources(Me.AboutToolStripMenuItem, "AboutToolStripMenuItem")
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.OpenFileDialog.Multiselect = True
        Me.BackGroundDataWorkerPool.MaxWorkers = 3
        Me.BackGroundDataWorkerPool.StatusbarListener = Nothing
        Me.MainTabControl.Controls.Add(Me.TabPageStart)
        Me.MainTabControl.Controls.Add(Me.TabPageItems)
        Me.MainTabControl.Controls.Add(Me.TabPageSelections)
        Me.MainTabControl.Controls.Add(Me.TabPageTests)
        Me.MainTabControl.Controls.Add(Me.TabPagePackages)
        Me.MainTabControl.Controls.Add(Me.TabPageAspects)
        Me.MainTabControl.Controls.Add(Me.TabPageMedia)
        Me.MainTabControl.Controls.Add(Me.TabPageCustomProperties)
        Me.MainTabControl.Controls.Add(Me.TabPageItemLayoutTemplates)
        Me.MainTabControl.Controls.Add(Me.TabPageSelectionTemplates)
        Me.MainTabControl.Controls.Add(Me.TabPageTestTemplates)
        Me.MainTabControl.Controls.Add(Me.TabPageControlTemplates)
        resources.ApplyResources(Me.MainTabControl, "MainTabControl")
        Me.MainTabControl.ImageList = Me.ImageListTabs
        Me.MainTabControl.Name = "MainTabControl"
        Me.MainTabControl.SelectedIndex = 0
        Me.TabPageStart.Controls.Add(Me.BankStartPage)
        resources.ApplyResources(Me.TabPageStart, "TabPageStart")
        Me.TabPageStart.Name = "TabPageStart"
        Me.TabPageStart.Tag = "Start"
        Me.TabPageStart.UseVisualStyleBackColor = True
        Me.BankStartPage.BankId = 0
        Me.BankStartPage.BankInfo = Nothing
        Me.BankStartPage.DataSource = Nothing
        resources.ApplyResources(Me.BankStartPage, "BankStartPage")
        Me.BankStartPage.Name = "BankStartPage"
        Me.TabPageItems.Controls.Add(Me.ItemGrid)
        resources.ApplyResources(Me.TabPageItems, "TabPageItems")
        Me.TabPageItems.Name = "TabPageItems"
        Me.TabPageItems.Tag = "Items"
        Me.TabPageItems.UseVisualStyleBackColor = True
        Me.ItemGrid.CustomPropertyColumnsVisible = True
        Me.ItemGrid.CustomPropertyFilter = Enums.ResourceTypeEnum.ItemResource
        Me.ItemGrid.DataMember = Nothing
        Me.ItemGrid.DataSource = Nothing
        resources.ApplyResources(Me.ItemGrid, "ItemGrid")
        Me.ItemGrid.EnableFiltering = False
        Me.ItemGrid.GridContentContextMenuDisabled = False
        Me.ItemGrid.MultiSelect = True
        Me.ItemGrid.Name = "ItemGrid"
        Me.ItemGrid.SearchToolbarVisibility = False
        Me.ItemGrid.SelectedEntity = Nothing
        Me.ItemGrid.ShouldDoSelectionCheck = False
        Me.ItemGrid.ShowDisabledRowsAsGray = False
        Me.ItemGrid.UseGridAsItemPicker = False
        Me.TabPageSelections.Controls.Add(Me.DataSourceGrid)
        resources.ApplyResources(Me.TabPageSelections, "TabPageSelections")
        Me.TabPageSelections.Name = "TabPageSelections"
        Me.TabPageSelections.Tag = "DataSources"
        Me.TabPageSelections.UseVisualStyleBackColor = True
        Me.DataSourceGrid.CustomPropertyColumnsVisible = False
        Me.DataSourceGrid.DataMember = ""
        Me.DataSourceGrid.DataSource = Nothing
        resources.ApplyResources(Me.DataSourceGrid, "DataSourceGrid")
        Me.DataSourceGrid.EnableFiltering = False
        Me.DataSourceGrid.GridContentContextMenuDisabled = False
        Me.DataSourceGrid.MultiSelect = True
        Me.DataSourceGrid.Name = "DataSourceGrid"
        Me.DataSourceGrid.SelectedEntity = Nothing
        Me.DataSourceGrid.ShouldDoSelectionCheck = False
        Me.DataSourceGrid.ShowDisabledRowsAsGray = False
        Me.DataSourceGrid.UseGridAsItemPicker = False
        Me.TabPageTests.Controls.Add(Me.TestGrid)
        resources.ApplyResources(Me.TabPageTests, "TabPageTests")
        Me.TabPageTests.Name = "TabPageTests"
        Me.TabPageTests.Tag = "Tests"
        Me.TabPageTests.UseVisualStyleBackColor = True
        Me.TestGrid.CustomPropertyColumnsVisible = True
        Me.TestGrid.CustomPropertyFilter = Enums.ResourceTypeEnum.AssessmentTestResource
        Me.TestGrid.DataMember = Nothing
        Me.TestGrid.DataSource = Nothing
        resources.ApplyResources(Me.TestGrid, "TestGrid")
        Me.TestGrid.EnableFiltering = False
        Me.TestGrid.GridContentContextMenuDisabled = False
        Me.TestGrid.MultiSelect = True
        Me.TestGrid.Name = "TestGrid"
        Me.TestGrid.SelectedEntity = Nothing
        Me.TestGrid.ShouldDoSelectionCheck = False
        Me.TestGrid.ShowDisabledRowsAsGray = False
        Me.TestGrid.UseGridAsItemPicker = False
        Me.TabPagePackages.Controls.Add(Me.TestPackageGrid)
        resources.ApplyResources(Me.TabPagePackages, "TabPagePackages")
        Me.TabPagePackages.Name = "TabPagePackages"
        Me.TabPagePackages.Tag = "TestPackages"
        Me.TabPagePackages.UseVisualStyleBackColor = True
        Me.TestPackageGrid.CustomPropertyColumnsVisible = False
        Me.TestPackageGrid.DataMember = ""
        Me.TestPackageGrid.DataSource = Nothing
        resources.ApplyResources(Me.TestPackageGrid, "TestPackageGrid")
        Me.TestPackageGrid.EnableFiltering = False
        Me.TestPackageGrid.GridContentContextMenuDisabled = False
        Me.TestPackageGrid.MultiSelect = True
        Me.TestPackageGrid.Name = "TestPackageGrid"
        Me.TestPackageGrid.SelectedEntity = Nothing
        Me.TestPackageGrid.ShouldDoSelectionCheck = False
        Me.TestPackageGrid.ShowDisabledRowsAsGray = False
        Me.TestPackageGrid.UseGridAsItemPicker = False
        Me.TabPageAspects.Controls.Add(Me.AspectGrid)
        resources.ApplyResources(Me.TabPageAspects, "TabPageAspects")
        Me.TabPageAspects.Name = "TabPageAspects"
        Me.TabPageAspects.Tag = "Aspects"
        Me.TabPageAspects.UseVisualStyleBackColor = True
        Me.AspectGrid.CustomPropertyColumnsVisible = False
        Me.AspectGrid.DataMember = ""
        Me.AspectGrid.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.AspectResourceEntity)
        resources.ApplyResources(Me.AspectGrid, "AspectGrid")
        Me.AspectGrid.EnableFiltering = False
        Me.AspectGrid.GridContentContextMenuDisabled = False
        Me.AspectGrid.MultiSelect = True
        Me.AspectGrid.Name = "AspectGrid"
        Me.AspectGrid.SelectedEntity = Nothing
        Me.AspectGrid.ShouldDoSelectionCheck = False
        Me.AspectGrid.ShowDisabledRowsAsGray = False
        Me.AspectGrid.UseGridAsItemPicker = False
        Me.TabPageMedia.Controls.Add(Me.MediaGrid)
        resources.ApplyResources(Me.TabPageMedia, "TabPageMedia")
        Me.TabPageMedia.Name = "TabPageMedia"
        Me.TabPageMedia.Tag = "Media"
        Me.TabPageMedia.UseVisualStyleBackColor = True
        Me.MediaGrid.CustomPropertyColumnsVisible = True
        Me.MediaGrid.CustomPropertyFilter = Enums.ResourceTypeEnum.GenericResource
        Me.MediaGrid.DataMember = ""
        Me.MediaGrid.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.GenericResourceEntity)
        resources.ApplyResources(Me.MediaGrid, "MediaGrid")
        Me.MediaGrid.EnableFiltering = False
        Me.MediaGrid.GridContentContextMenuDisabled = False
        Me.MediaGrid.MultiSelect = True
        Me.MediaGrid.Name = "MediaGrid"
        Me.MediaGrid.SelectedEntity = Nothing
        Me.MediaGrid.ShouldDoSelectionCheck = False
        Me.MediaGrid.ShowDisabledRowsAsGray = False
        Me.MediaGrid.UseGridAsItemPicker = False
        Me.TabPageCustomProperties.Controls.Add(Me.CustomPropertyGrid)
        resources.ApplyResources(Me.TabPageCustomProperties, "TabPageCustomProperties")
        Me.TabPageCustomProperties.Name = "TabPageCustomProperties"
        Me.TabPageCustomProperties.Tag = "CustomProperties"
        Me.TabPageCustomProperties.UseVisualStyleBackColor = True
        Me.CustomPropertyGrid.CustomPropertyColumnsVisible = False
        Me.CustomPropertyGrid.DataMember = ""
        Me.CustomPropertyGrid.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.CustomBankPropertyEntity)
        resources.ApplyResources(Me.CustomPropertyGrid, "CustomPropertyGrid")
        Me.CustomPropertyGrid.EnableFiltering = False
        Me.CustomPropertyGrid.GridContentContextMenuDisabled = False
        Me.CustomPropertyGrid.MultiSelect = True
        Me.CustomPropertyGrid.Name = "CustomPropertyGrid"
        Me.CustomPropertyGrid.SelectedEntity = Nothing
        Me.CustomPropertyGrid.ShouldDoSelectionCheck = False
        Me.CustomPropertyGrid.ShowDisabledRowsAsGray = False
        Me.CustomPropertyGrid.UseGridAsItemPicker = False
        Me.TabPageItemLayoutTemplates.Controls.Add(Me.ItemLayoutTemplateGrid)
        resources.ApplyResources(Me.TabPageItemLayoutTemplates, "TabPageItemLayoutTemplates")
        Me.TabPageItemLayoutTemplates.Name = "TabPageItemLayoutTemplates"
        Me.TabPageItemLayoutTemplates.Tag = "ItemLayoutTemplates"
        Me.TabPageItemLayoutTemplates.UseVisualStyleBackColor = True
        Me.ItemLayoutTemplateGrid.CustomPropertyColumnsVisible = False
        Me.ItemLayoutTemplateGrid.DataMember = ""
        Me.ItemLayoutTemplateGrid.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.ItemLayoutTemplateResourceEntity)
        resources.ApplyResources(Me.ItemLayoutTemplateGrid, "ItemLayoutTemplateGrid")
        Me.ItemLayoutTemplateGrid.EnableFiltering = False
        Me.ItemLayoutTemplateGrid.GridContentContextMenuDisabled = False
        Me.ItemLayoutTemplateGrid.MultiSelect = True
        Me.ItemLayoutTemplateGrid.Name = "ItemLayoutTemplateGrid"
        Me.ItemLayoutTemplateGrid.SelectedEntity = Nothing
        Me.ItemLayoutTemplateGrid.ShouldDoSelectionCheck = False
        Me.ItemLayoutTemplateGrid.ShowDisabledRowsAsGray = False
        Me.ItemLayoutTemplateGrid.UseGridAsItemPicker = False
        Me.TabPageSelectionTemplates.Controls.Add(Me.DataSourceTemplateGrid)
        resources.ApplyResources(Me.TabPageSelectionTemplates, "TabPageSelectionTemplates")
        Me.TabPageSelectionTemplates.Name = "TabPageSelectionTemplates"
        Me.TabPageSelectionTemplates.Tag = "DataSourceTemplates"
        Me.TabPageSelectionTemplates.UseVisualStyleBackColor = True
        Me.DataSourceTemplateGrid.CustomPropertyColumnsVisible = False
        Me.DataSourceTemplateGrid.DataMember = ""
        Me.DataSourceTemplateGrid.DataSource = Nothing
        resources.ApplyResources(Me.DataSourceTemplateGrid, "DataSourceTemplateGrid")
        Me.DataSourceTemplateGrid.EnableFiltering = False
        Me.DataSourceTemplateGrid.GridContentContextMenuDisabled = False
        Me.DataSourceTemplateGrid.MultiSelect = True
        Me.DataSourceTemplateGrid.Name = "DataSourceTemplateGrid"
        Me.DataSourceTemplateGrid.SelectedEntity = Nothing
        Me.DataSourceTemplateGrid.ShouldDoSelectionCheck = False
        Me.DataSourceTemplateGrid.ShowDisabledRowsAsGray = False
        Me.DataSourceTemplateGrid.UseGridAsItemPicker = False
        Me.TabPageTestTemplates.Controls.Add(Me.TestTemplateGrid)
        resources.ApplyResources(Me.TabPageTestTemplates, "TabPageTestTemplates")
        Me.TabPageTestTemplates.Name = "TabPageTestTemplates"
        Me.TabPageTestTemplates.Tag = "TestTemplates"
        Me.TabPageTestTemplates.UseVisualStyleBackColor = True
        Me.TestTemplateGrid.CustomPropertyColumnsVisible = False
        Me.TestTemplateGrid.DataMember = Nothing
        Me.TestTemplateGrid.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.AssessmentTestResourceEntity)
        resources.ApplyResources(Me.TestTemplateGrid, "TestTemplateGrid")
        Me.TestTemplateGrid.EnableFiltering = False
        Me.TestTemplateGrid.GridContentContextMenuDisabled = False
        Me.TestTemplateGrid.MultiSelect = True
        Me.TestTemplateGrid.Name = "TestTemplateGrid"
        Me.TestTemplateGrid.SelectedEntity = Nothing
        Me.TestTemplateGrid.ShouldDoSelectionCheck = False
        Me.TestTemplateGrid.ShowDisabledRowsAsGray = False
        Me.TestTemplateGrid.UseGridAsItemPicker = False
        Me.TabPageControlTemplates.Controls.Add(Me.ControlTemplateGrid)
        resources.ApplyResources(Me.TabPageControlTemplates, "TabPageControlTemplates")
        Me.TabPageControlTemplates.Name = "TabPageControlTemplates"
        Me.TabPageControlTemplates.Tag = "ControlTemplates"
        Me.TabPageControlTemplates.UseVisualStyleBackColor = True
        Me.ControlTemplateGrid.CustomPropertyColumnsVisible = False
        Me.ControlTemplateGrid.DataMember = ""
        Me.ControlTemplateGrid.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.ControlTemplateResourceEntity)
        resources.ApplyResources(Me.ControlTemplateGrid, "ControlTemplateGrid")
        Me.ControlTemplateGrid.EnableFiltering = False
        Me.ControlTemplateGrid.GridContentContextMenuDisabled = False
        Me.ControlTemplateGrid.MultiSelect = True
        Me.ControlTemplateGrid.Name = "ControlTemplateGrid"
        Me.ControlTemplateGrid.SelectedEntity = Nothing
        Me.ControlTemplateGrid.ShouldDoSelectionCheck = False
        Me.ControlTemplateGrid.ShowDisabledRowsAsGray = False
        Me.ControlTemplateGrid.UseGridAsItemPicker = False
        Me.ImageListTabs.ImageStream = CType(resources.GetObject("ImageListTabs.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageListTabs.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageListTabs.Images.SetKeyName(0, "Appearance1.Image.png")
        Me.ImageListTabs.Images.SetKeyName(1, "Appearance2.Image.png")
        Me.ImageListTabs.Images.SetKeyName(2, "Appearance3.Image.png")
        Me.ImageListTabs.Images.SetKeyName(3, "Appearance4.Image.png")
        Me.ImageListTabs.Images.SetKeyName(4, "Appearance5.Image.png")
        Me.ImageListTabs.Images.SetKeyName(5, "Appearance7.Image.png")
        Me.ImageListTabs.Images.SetKeyName(6, "Appearance8.Image.png")
        Me.ImageListTabs.Images.SetKeyName(7, "Appearance9.Image.png")
        Me.ImageListTabs.Images.SetKeyName(8, "Appearance10.Image.png")
        Me.ImageListTabs.Images.SetKeyName(9, "Appearance11.Image.png")
        Me.ImageListTabs.Images.SetKeyName(10, "options_16x16.png")
        Me.ImageListTabs.Images.SetKeyName(11, "documents2_16x16.png")
        resources.ApplyResources(Me.SplitContainerMain, "SplitContainerMain")
        Me.SplitContainerMain.Name = "SplitContainerMain"
        Me.SplitContainerMain.Panel1.Controls.Add(Me.MainBankBrowser)
        Me.SplitContainerMain.Panel2.Controls.Add(Me.MainTabControl)
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.SplitContainerMain)
        Me.Controls.Add(Me.MainToolStrip)
        Me.Controls.Add(Me.MainMenu)
        Me.Controls.Add(Me.MainStatusStrip)
        Me.KeyPreview = True
        Me.Name = "MainForm"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MainStatusStrip.ResumeLayout(False)
        Me.MainStatusStrip.PerformLayout()
        Me.MainToolStrip.ResumeLayout(False)
        Me.MainToolStrip.PerformLayout()
        Me.MainMenu.ResumeLayout(False)
        Me.MainMenu.PerformLayout()
        Me.MainTabControl.ResumeLayout(False)
        Me.TabPageStart.ResumeLayout(False)
        Me.TabPageItems.ResumeLayout(False)
        Me.TabPageSelections.ResumeLayout(False)
        Me.TabPageTests.ResumeLayout(False)
        Me.TabPagePackages.ResumeLayout(False)
        Me.TabPageAspects.ResumeLayout(False)
        Me.TabPageMedia.ResumeLayout(False)
        Me.TabPageCustomProperties.ResumeLayout(False)
        Me.TabPageItemLayoutTemplates.ResumeLayout(False)
        Me.TabPageSelectionTemplates.ResumeLayout(False)
        Me.TabPageTestTemplates.ResumeLayout(False)
        Me.TabPageControlTemplates.ResumeLayout(False)
        Me.SplitContainerMain.Panel1.ResumeLayout(False)
        Me.SplitContainerMain.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerMain.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents HarmonizeDependentItemsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HarmonizeDependentItemsToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents MainStatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents MainToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents MainMenu As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OptionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents PublishToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents MainBankBrowser As Questify.Builder.UI.BankBrowser
    Friend WithEvents BackGroundDataWorkerPool As Questify.Builder.UI.BackGroundWorkerPool
    Friend WithEvents OpenFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents FolderBrowserDialog As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents PropertyToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripEmptySpace As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusBarMessage As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents UsernameStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents UserAccountToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparatorFile2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ProperiesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparatorFile1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents PublishToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SelectAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RefreshToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents PreviewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReportsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents NewToolStripSplitButton As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents ItemToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TestToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TestTemplateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AspectsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MediaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExportToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ExportToExcelToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents PreviewToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents EditToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ExportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExportResourcesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImportResourcesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SearchToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents NewItemToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewTestTemplateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewMediaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewTestToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewAspectToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImportToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents RefreshToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents SearchToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MultiEditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ReportsToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents SelectionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewSelectionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewTestPackageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TestPackagesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeliveryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CustomPropertyMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BreadcrumbBankPartToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripEmptySpace2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents BreadcrumbStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents HarmonizeDependantItemsToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents HarmonizeDependantItemsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SourceTextTemplateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewSourceTextTemplateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MoveResourceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As ToolStripSeparator
    Friend WithEvents AnnouncementControlToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MainTabControl As TabControl
    Friend WithEvents TabPageStart As TabPage
    Friend WithEvents TabPageItems As TabPage
    Friend WithEvents TabPageSelections As TabPage
    Friend WithEvents TabPageTests As TabPage
    Friend WithEvents TabPagePackages As TabPage
    Friend WithEvents TabPageAspects As TabPage
    Friend WithEvents TabPageMedia As TabPage
    Friend WithEvents TabPageCustomProperties As TabPage
    Friend WithEvents TabPageItemLayoutTemplates As TabPage
    Friend WithEvents TabPageSelectionTemplates As TabPage
    Friend WithEvents TabPageTestTemplates As TabPage
    Friend WithEvents TabPageControlTemplates As TabPage
    Friend WithEvents BankStartPage As UI.BankStartPageControl
    Friend WithEvents ItemGrid As UI.ItemGrid
    Friend WithEvents DataSourceGrid As UI.DataSourceGrid
    Friend WithEvents TestGrid As UI.TestGrid
    Friend WithEvents TestPackageGrid As UI.TestPackageGrid
    Friend WithEvents AspectGrid As UI.AspectGrid
    Friend WithEvents MediaGrid As UI.MediaGrid
    Friend WithEvents CustomPropertyGrid As UI.CustomPropertyGrid
    Friend WithEvents ItemLayoutTemplateGrid As UI.ItemLayoutTemplateGrid
    Friend WithEvents DataSourceTemplateGrid As UI.DataSourceTemplateGrid
    Friend WithEvents TestTemplateGrid As UI.TestTemplateGrid
    Friend WithEvents ControlTemplateGrid As UI.ControlTemplateGrid
    Friend WithEvents ImageListTabs As ImageList
    Friend WithEvents SplitContainerMain As SplitContainer
End Class
