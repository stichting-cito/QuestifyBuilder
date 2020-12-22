<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BankBrowser
    Inherits System.Windows.Forms.UserControl

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BankBrowser))
        Dim BankTreeControl_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.BankTreeControl = New Janus.Windows.GridEX.GridEX()
        Me.BankContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AddBankToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddRootbankTODOToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClearToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PropertiesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BankBrowserImages = New System.Windows.Forms.ImageList(Me.components)
        Me.BankBrowserToolStrip = New System.Windows.Forms.ToolStrip()
        Me.BankSearchTextBox = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripButtonRefresh = New System.Windows.Forms.ToolStripButton()
        Me.ClearAndDeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        CType(Me.BankTreeControl, System.ComponentModel.ISupportInitialize).BeginInit
        Me.BankContextMenu.SuspendLayout
        Me.BankBrowserToolStrip.SuspendLayout
        Me.SuspendLayout
        resources.ApplyResources(Me.BankTreeControl, "BankTreeControl")
        Me.BankTreeControl.AllowCardSizing = false
        Me.BankTreeControl.AllowColumnDrag = false
        Me.BankTreeControl.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.BankTreeControl.ColumnAutoResize = true
        Me.BankTreeControl.ColumnHeaders = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.BankTreeControl.ContextMenuStrip = Me.BankContextMenu
        resources.ApplyResources(BankTreeControl_DesignTimeLayout, "BankTreeControl_DesignTimeLayout")
        Me.BankTreeControl.DesignTimeLayout = BankTreeControl_DesignTimeLayout
        Me.BankTreeControl.ExpandableGroups = Janus.Windows.GridEX.InheritableBoolean.[Default]
        Me.BankTreeControl.FilterRowFormatStyle.Appearance = Janus.Windows.GridEX.Appearance.Sunken
        Me.BankTreeControl.FilterRowFormatStyle.BackColor = System.Drawing.Color.Transparent
        Me.BankTreeControl.FilterRowFormatStyle.BackColorGradient = System.Drawing.Color.LightSkyBlue
        Me.BankTreeControl.FilterRowFormatStyle.BackgroundGradientMode = Janus.Windows.GridEX.BackgroundGradientMode.Vertical
        Me.BankTreeControl.FocusCellFormatStyle.BackColor = System.Drawing.SystemColors.Highlight
        Me.BankTreeControl.FocusCellFormatStyle.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.BankTreeControl.FocusStyle = Janus.Windows.GridEX.FocusStyle.Solid
        Me.BankTreeControl.GridLines = Janus.Windows.GridEX.GridLines.None
        Me.BankTreeControl.GroupByBoxVisible = false
        Me.BankTreeControl.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight
        Me.BankTreeControl.Hierarchical = true
        Me.BankTreeControl.ImageList = Me.BankBrowserImages
        Me.BankTreeControl.Name = "BankTreeControl"
        Me.BankTreeControl.SelectedFormatStyle.BackColorGradient = System.Drawing.Color.Transparent
        Me.BankTreeControl.TreeLineColor = System.Drawing.SystemColors.ActiveCaption
        resources.ApplyResources(Me.BankContextMenu, "BankContextMenu")
        Me.BankContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddBankToolStripMenuItem, Me.AddRootbankTODOToolStripMenuItem, Me.ToolStripSeparator1, Me.DeleteToolStripMenuItem, Me.ClearToolStripMenuItem, Me.ClearAndDeleteToolStripMenuItem, Me.ToolStripSeparator2, Me.PropertiesToolStripMenuItem})
        Me.BankContextMenu.Name = "BankContextMenu"
        resources.ApplyResources(Me.AddBankToolStripMenuItem, "AddBankToolStripMenuItem")
        Me.AddBankToolStripMenuItem.Name = "AddBankToolStripMenuItem"
        resources.ApplyResources(Me.AddRootbankTODOToolStripMenuItem, "AddRootbankTODOToolStripMenuItem")
        Me.AddRootbankTODOToolStripMenuItem.Name = "AddRootbankTODOToolStripMenuItem"
        resources.ApplyResources(Me.DeleteToolStripMenuItem, "DeleteToolStripMenuItem")
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        resources.ApplyResources(Me.ClearToolStripMenuItem, "ClearToolStripMenuItem")
        Me.ClearToolStripMenuItem.Name = "ClearToolStripMenuItem"
        resources.ApplyResources(Me.PropertiesToolStripMenuItem, "PropertiesToolStripMenuItem")
        Me.PropertiesToolStripMenuItem.Name = "PropertiesToolStripMenuItem"
        Me.BankBrowserImages.ImageStream = CType(resources.GetObject("BankBrowserImages.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.BankBrowserImages.TransparentColor = System.Drawing.Color.Transparent
        Me.BankBrowserImages.Images.SetKeyName(0, "ComponentBank")
        resources.ApplyResources(Me.BankBrowserToolStrip, "BankBrowserToolStrip")
        Me.BankBrowserToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BankSearchTextBox, Me.ToolStripButtonRefresh})
        Me.BankBrowserToolStrip.Name = "BankBrowserToolStrip"
        resources.ApplyResources(Me.BankSearchTextBox, "BankSearchTextBox")
        Me.BankSearchTextBox.AutoToolTip = true
        Me.BankSearchTextBox.HideSelection = false
        Me.BankSearchTextBox.Name = "BankSearchTextBox"
        resources.ApplyResources(Me.ToolStripButtonRefresh, "ToolStripButtonRefresh")
        Me.ToolStripButtonRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonRefresh.Margin = New System.Windows.Forms.Padding(3, 1, 0, 2)
        Me.ToolStripButtonRefresh.Name = "ToolStripButtonRefresh"
        resources.ApplyResources(Me.ClearAndDeleteToolStripMenuItem, "ClearAndDeleteToolStripMenuItem")
        Me.ClearAndDeleteToolStripMenuItem.Name = "ClearAndDeleteToolStripMenuItem"
        resources.ApplyResources(Me.ToolStripSeparator1, "ToolStripSeparator1")
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        resources.ApplyResources(Me.ToolStripSeparator2, "ToolStripSeparator2")
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.BankTreeControl)
        Me.Controls.Add(Me.BankBrowserToolStrip)
        Me.Name = "BankBrowser"
        CType(Me.BankTreeControl, System.ComponentModel.ISupportInitialize).EndInit
        Me.BankContextMenu.ResumeLayout(false)
        Me.BankBrowserToolStrip.ResumeLayout(false)
        Me.BankBrowserToolStrip.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

    End Sub
    Friend WithEvents BankTreeControl As Janus.Windows.GridEX.GridEX
    Friend WithEvents BankBrowserImages As System.Windows.Forms.ImageList
    Friend WithEvents BankContextMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AddBankToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddRootbankTODOToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PropertiesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BankBrowserToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents BankSearchTextBox As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents ToolStripButtonRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents ClearToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ClearAndDeleteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
End Class
