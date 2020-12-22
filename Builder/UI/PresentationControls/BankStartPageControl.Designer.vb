<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BankStartPageControl
    Inherits ActionCommandSupportBaseControl

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BankStartPageControl))
        Dim RecentlyModifiedItemsGrid_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim ItemStateInformationGrid_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.WelcomeLabel = New System.Windows.Forms.Label()
        Me.RecentlyModifiedItemsGrid = New Janus.Windows.GridEX.GridEX()
        Me.ItemStateInformationGrid = New Janus.Windows.GridEX.GridEX()
        Me.TestsCreatedByMeLabel = New System.Windows.Forms.Label()
        Me.ItemsCreatedByMeLabel = New System.Windows.Forms.Label()
        Me.UnusedMediaFilesLabel = New System.Windows.Forms.Label()
        Me.UnusedItemsLabel = New System.Windows.Forms.Label()
        Me.TestTemplatesLabel = New System.Windows.Forms.Label()
        Me.MediaLabel = New System.Windows.Forms.Label()
        Me.ItemsLabel = New System.Windows.Forms.Label()
        Me.TestsLabel = New System.Windows.Forms.Label()
        Me.GroupboxItem = New System.Windows.Forms.GroupBox()
        Me.GroupboxTest = New System.Windows.Forms.GroupBox()
        Me.GroupboxMedia = New System.Windows.Forms.GroupBox()
        Me.GroupboxTestTemplate = New System.Windows.Forms.GroupBox()
        Me.TestTemplatesCreatedByMeLabel = New System.Windows.Forms.Label()
        Me.GroupboxItemTemplates = New System.Windows.Forms.GroupBox()
        Me.ItemTemplatesCreatedByMeLabel = New System.Windows.Forms.Label()
        Me.UnusedItemTemplatesLabel = New System.Windows.Forms.Label()
        Me.ItemTemplatesLabel = New System.Windows.Forms.Label()
        Me.GroupboxControlTemplate = New System.Windows.Forms.GroupBox()
        Me.ControlTemplatesCreatedByMeLabel = New System.Windows.Forms.Label()
        Me.UnusedControlTemplatesLabel = New System.Windows.Forms.Label()
        Me.ControlTemplatesLabel = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        CType(Me.RecentlyModifiedItemsGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ItemStateInformationGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupboxItem.SuspendLayout()
        Me.GroupboxTest.SuspendLayout()
        Me.GroupboxMedia.SuspendLayout()
        Me.GroupboxTestTemplate.SuspendLayout()
        Me.GroupboxItemTemplates.SuspendLayout()
        Me.GroupboxControlTemplate.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.SuspendLayout()
        Me.WelcomeLabel.BackColor = System.Drawing.Color.Gray
        Me.WelcomeLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.WelcomeLabel, "WelcomeLabel")
        Me.WelcomeLabel.ForeColor = System.Drawing.Color.White
        Me.WelcomeLabel.Name = "WelcomeLabel"
        Me.RecentlyModifiedItemsGrid.AllowCardSizing = False
        Me.RecentlyModifiedItemsGrid.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.RecentlyModifiedItemsGrid.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.RecentlyModifiedItemsGrid.CardHeaders = False
        Me.RecentlyModifiedItemsGrid.ColumnAutoResize = True
        resources.ApplyResources(RecentlyModifiedItemsGrid_DesignTimeLayout, "RecentlyModifiedItemsGrid_DesignTimeLayout")
        Me.RecentlyModifiedItemsGrid.DesignTimeLayout = RecentlyModifiedItemsGrid_DesignTimeLayout
        resources.ApplyResources(Me.RecentlyModifiedItemsGrid, "RecentlyModifiedItemsGrid")
        Me.RecentlyModifiedItemsGrid.GroupByBoxVisible = False
        Me.RecentlyModifiedItemsGrid.Name = "RecentlyModifiedItemsGrid"
        Me.RecentlyModifiedItemsGrid.TableHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.ItemStateInformationGrid.AllowCardSizing = False
        Me.ItemStateInformationGrid.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.ItemStateInformationGrid.ColumnAutoResize = True
        resources.ApplyResources(ItemStateInformationGrid_DesignTimeLayout, "ItemStateInformationGrid_DesignTimeLayout")
        Me.ItemStateInformationGrid.DesignTimeLayout = ItemStateInformationGrid_DesignTimeLayout
        resources.ApplyResources(Me.ItemStateInformationGrid, "ItemStateInformationGrid")
        Me.ItemStateInformationGrid.GroupByBoxVisible = False
        Me.ItemStateInformationGrid.Name = "ItemStateInformationGrid"
        Me.ItemStateInformationGrid.TableHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        resources.ApplyResources(Me.TestsCreatedByMeLabel, "TestsCreatedByMeLabel")
        Me.TestsCreatedByMeLabel.BackColor = System.Drawing.Color.White
        Me.TestsCreatedByMeLabel.Name = "TestsCreatedByMeLabel"
        resources.ApplyResources(Me.ItemsCreatedByMeLabel, "ItemsCreatedByMeLabel")
        Me.ItemsCreatedByMeLabel.BackColor = System.Drawing.Color.White
        Me.ItemsCreatedByMeLabel.Name = "ItemsCreatedByMeLabel"
        resources.ApplyResources(Me.UnusedMediaFilesLabel, "UnusedMediaFilesLabel")
        Me.UnusedMediaFilesLabel.BackColor = System.Drawing.Color.White
        Me.UnusedMediaFilesLabel.Name = "UnusedMediaFilesLabel"
        resources.ApplyResources(Me.UnusedItemsLabel, "UnusedItemsLabel")
        Me.UnusedItemsLabel.BackColor = System.Drawing.Color.White
        Me.UnusedItemsLabel.Name = "UnusedItemsLabel"
        resources.ApplyResources(Me.TestTemplatesLabel, "TestTemplatesLabel")
        Me.TestTemplatesLabel.BackColor = System.Drawing.Color.White
        Me.TestTemplatesLabel.Name = "TestTemplatesLabel"
        resources.ApplyResources(Me.MediaLabel, "MediaLabel")
        Me.MediaLabel.BackColor = System.Drawing.Color.White
        Me.MediaLabel.Name = "MediaLabel"
        resources.ApplyResources(Me.ItemsLabel, "ItemsLabel")
        Me.ItemsLabel.BackColor = System.Drawing.Color.White
        Me.ItemsLabel.Name = "ItemsLabel"
        resources.ApplyResources(Me.TestsLabel, "TestsLabel")
        Me.TestsLabel.BackColor = System.Drawing.Color.White
        Me.TestsLabel.Name = "TestsLabel"
        Me.GroupboxItem.BackColor = System.Drawing.SystemColors.Window
        Me.GroupboxItem.Controls.Add(Me.ItemsLabel)
        Me.GroupboxItem.Controls.Add(Me.ItemsCreatedByMeLabel)
        Me.GroupboxItem.Controls.Add(Me.UnusedItemsLabel)
        resources.ApplyResources(Me.GroupboxItem, "GroupboxItem")
        Me.GroupboxItem.Name = "GroupboxItem"
        Me.GroupboxItem.Dock = DockStyle.Fill
        Me.GroupboxItem.TabStop = False
        Me.GroupboxTest.BackColor = System.Drawing.SystemColors.Window
        Me.GroupboxTest.Controls.Add(Me.TestsCreatedByMeLabel)
        Me.GroupboxTest.Controls.Add(Me.TestsLabel)
        resources.ApplyResources(Me.GroupboxTest, "GroupboxTest")
        Me.GroupboxTest.Name = "GroupboxTest"
        Me.GroupboxTest.Dock = DockStyle.Fill
        Me.GroupboxTest.TabStop = False
        Me.GroupboxMedia.BackColor = System.Drawing.SystemColors.Window
        Me.GroupboxMedia.Controls.Add(Me.MediaLabel)
        Me.GroupboxMedia.Controls.Add(Me.UnusedMediaFilesLabel)
        resources.ApplyResources(Me.GroupboxMedia, "GroupboxMedia")
        Me.GroupboxMedia.Name = "GroupboxMedia"
        Me.GroupboxMedia.Dock = DockStyle.Fill
        Me.GroupboxMedia.TabStop = False
        Me.GroupboxTestTemplate.BackColor = System.Drawing.SystemColors.Window
        Me.GroupboxTestTemplate.Controls.Add(Me.TestTemplatesCreatedByMeLabel)
        Me.GroupboxTestTemplate.Controls.Add(Me.TestTemplatesLabel)
        resources.ApplyResources(Me.GroupboxTestTemplate, "GroupboxTestTemplate")
        Me.GroupboxTestTemplate.Name = "GroupboxTestTemplate"
        Me.GroupboxTestTemplate.Dock = DockStyle.Fill
        Me.GroupboxTestTemplate.TabStop = False
        resources.ApplyResources(Me.TestTemplatesCreatedByMeLabel, "TestTemplatesCreatedByMeLabel")
        Me.TestTemplatesCreatedByMeLabel.BackColor = System.Drawing.Color.White
        Me.TestTemplatesCreatedByMeLabel.Name = "TestTemplatesCreatedByMeLabel"
        Me.GroupboxItemTemplates.BackColor = System.Drawing.SystemColors.Window
        Me.GroupboxItemTemplates.Controls.Add(Me.ItemTemplatesCreatedByMeLabel)
        Me.GroupboxItemTemplates.Controls.Add(Me.UnusedItemTemplatesLabel)
        Me.GroupboxItemTemplates.Controls.Add(Me.ItemTemplatesLabel)
        resources.ApplyResources(Me.GroupboxItemTemplates, "GroupboxItemTemplates")
        Me.GroupboxItemTemplates.Name = "GroupboxItemTemplates"
        Me.GroupboxItemTemplates.Dock = DockStyle.Fill
        Me.GroupboxItemTemplates.TabStop = False
        resources.ApplyResources(Me.ItemTemplatesCreatedByMeLabel, "ItemTemplatesCreatedByMeLabel")
        Me.ItemTemplatesCreatedByMeLabel.BackColor = System.Drawing.Color.White
        Me.ItemTemplatesCreatedByMeLabel.Name = "ItemTemplatesCreatedByMeLabel"
        resources.ApplyResources(Me.UnusedItemTemplatesLabel, "UnusedItemTemplatesLabel")
        Me.UnusedItemTemplatesLabel.BackColor = System.Drawing.Color.White
        Me.UnusedItemTemplatesLabel.Name = "UnusedItemTemplatesLabel"
        resources.ApplyResources(Me.ItemTemplatesLabel, "ItemTemplatesLabel")
        Me.ItemTemplatesLabel.BackColor = System.Drawing.Color.White
        Me.ItemTemplatesLabel.Name = "ItemTemplatesLabel"
        Me.GroupboxControlTemplate.BackColor = System.Drawing.SystemColors.Window
        Me.GroupboxControlTemplate.Controls.Add(Me.ControlTemplatesCreatedByMeLabel)
        Me.GroupboxControlTemplate.Controls.Add(Me.UnusedControlTemplatesLabel)
        Me.GroupboxControlTemplate.Controls.Add(Me.ControlTemplatesLabel)
        resources.ApplyResources(Me.GroupboxControlTemplate, "GroupboxControlTemplate")
        Me.GroupboxControlTemplate.Name = "GroupboxControlTemplate"
        Me.GroupboxControlTemplate.Dock = DockStyle.Fill
        Me.GroupboxControlTemplate.TabStop = False
        resources.ApplyResources(Me.ControlTemplatesCreatedByMeLabel, "ControlTemplatesCreatedByMeLabel")
        Me.ControlTemplatesCreatedByMeLabel.BackColor = System.Drawing.Color.White
        Me.ControlTemplatesCreatedByMeLabel.Name = "ControlTemplatesCreatedByMeLabel"
        resources.ApplyResources(Me.UnusedControlTemplatesLabel, "UnusedControlTemplatesLabel")
        Me.UnusedControlTemplatesLabel.BackColor = System.Drawing.Color.White
        Me.UnusedControlTemplatesLabel.Name = "UnusedControlTemplatesLabel"
        resources.ApplyResources(Me.ControlTemplatesLabel, "ControlTemplatesLabel")
        Me.ControlTemplatesLabel.BackColor = System.Drawing.Color.White
        Me.ControlTemplatesLabel.Name = "ControlTemplatesLabel"
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.GroupboxItem, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupboxTest, 3, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupboxControlTemplate, 5, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupboxMedia, 5, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupboxItemTemplates, 3, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupboxTestTemplate, 1, 2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        resources.ApplyResources(Me.TableLayoutPanel2, "TableLayoutPanel2")
        Me.TableLayoutPanel2.Controls.Add(Me.RecentlyModifiedItemsGrid, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.ItemStateInformationGrid, 3, 1)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.WelcomeLabel)
        Me.Name = "BankStartPageControl"
        CType(Me.RecentlyModifiedItemsGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ItemStateInformationGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupboxItem.ResumeLayout(False)
        Me.GroupboxItem.PerformLayout()
        Me.GroupboxTest.ResumeLayout(False)
        Me.GroupboxTest.PerformLayout()
        Me.GroupboxMedia.ResumeLayout(False)
        Me.GroupboxMedia.PerformLayout()
        Me.GroupboxTestTemplate.ResumeLayout(False)
        Me.GroupboxTestTemplate.PerformLayout()
        Me.GroupboxItemTemplates.ResumeLayout(False)
        Me.GroupboxItemTemplates.PerformLayout()
        Me.GroupboxControlTemplate.ResumeLayout(False)
        Me.GroupboxControlTemplate.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents WelcomeLabel As System.Windows.Forms.Label
    Friend WithEvents RecentlyModifiedItemsGrid As Janus.Windows.GridEX.GridEX
    Friend WithEvents ItemStateInformationGrid As Janus.Windows.GridEX.GridEX
    Friend WithEvents UnusedItemsLabel As System.Windows.Forms.Label
    Friend WithEvents TestTemplatesLabel As System.Windows.Forms.Label
    Friend WithEvents MediaLabel As System.Windows.Forms.Label
    Friend WithEvents ItemsLabel As System.Windows.Forms.Label
    Friend WithEvents TestsLabel As System.Windows.Forms.Label
    Friend WithEvents TestsCreatedByMeLabel As System.Windows.Forms.Label
    Friend WithEvents ItemsCreatedByMeLabel As System.Windows.Forms.Label
    Friend WithEvents UnusedMediaFilesLabel As System.Windows.Forms.Label
    Friend WithEvents GroupboxItem As GroupBox
    Friend WithEvents GroupboxTest As GroupBox
    Friend WithEvents GroupboxMedia As GroupBox
    Friend WithEvents GroupboxTestTemplate As GroupBox
    Friend WithEvents GroupboxItemTemplates As GroupBox
    Friend WithEvents UnusedItemTemplatesLabel As System.Windows.Forms.Label
    Friend WithEvents ItemTemplatesLabel As System.Windows.Forms.Label
    Friend WithEvents GroupboxControlTemplate As GroupBox
    Friend WithEvents UnusedControlTemplatesLabel As System.Windows.Forms.Label
    Friend WithEvents ControlTemplatesLabel As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TestTemplatesCreatedByMeLabel As System.Windows.Forms.Label
    Friend WithEvents ItemTemplatesCreatedByMeLabel As System.Windows.Forms.Label
    Friend WithEvents ControlTemplatesCreatedByMeLabel As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
End Class
