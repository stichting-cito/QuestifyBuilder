<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TreeStructureUserControl
    Inherits CustomPropertyUserControlBase

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TreeStructureUserControl))
        Me.TableLayoutPanelMain = New System.Windows.Forms.TableLayoutPanel()
        Me.TreeStructureGrid = New Questify.Builder.UI.TreeStructureGrid()
        Me.TreeStructureViewerUserControl = New Questify.Builder.UI.TreeStructureViewerUserControl()
        Me.TableLayoutPanelMiddle = New System.Windows.Forms.TableLayoutPanel()
        Me.VisualizeTreeToggleButton = New System.Windows.Forms.CheckBox()
        Me.ButtonAdd = New System.Windows.Forms.Button()
        Me.StructureTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.StructureErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.TableLayoutPanelMain.SuspendLayout()
        Me.TableLayoutPanelMiddle.SuspendLayout()
        CType(Me.StructureErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        resources.ApplyResources(Me.TableLayoutPanelMain, "TableLayoutPanelMain")
        Me.TableLayoutPanelMain.Controls.Add(Me.TreeStructureGrid, 0, 0)
        Me.TableLayoutPanelMain.Controls.Add(Me.TreeStructureViewerUserControl, 2, 0)
        Me.TableLayoutPanelMain.Controls.Add(Me.TableLayoutPanelMiddle, 1, 0)
        Me.TableLayoutPanelMain.Name = "TableLayoutPanelMain"
        resources.ApplyResources(Me.TreeStructureGrid, "TreeStructureGrid")
        Me.TreeStructureGrid.Name = "TreeStructureGrid"
        resources.ApplyResources(Me.TreeStructureViewerUserControl, "TreeStructureViewerUserControl")
        Me.TreeStructureViewerUserControl.Name = "TreeStructureViewerUserControl"
        resources.ApplyResources(Me.TableLayoutPanelMiddle, "TableLayoutPanelMiddle")
        Me.TableLayoutPanelMiddle.Controls.Add(Me.VisualizeTreeToggleButton, 0, 2)
        Me.TableLayoutPanelMiddle.Controls.Add(Me.ButtonAdd, 0, 1)
        Me.TableLayoutPanelMiddle.Controls.Add(Me.StructureTableLayoutPanel, 0, 0)
        Me.TableLayoutPanelMiddle.Name = "TableLayoutPanelMiddle"
        resources.ApplyResources(Me.VisualizeTreeToggleButton, "VisualizeTreeToggleButton")
        Me.VisualizeTreeToggleButton.Name = "VisualizeTreeToggleButton"
        Me.VisualizeTreeToggleButton.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.ButtonAdd, "ButtonAdd")
        Me.ButtonAdd.Image = Global.Questify.Builder.UI.My.Resources.Resources.add_icon_16
        Me.ButtonAdd.Name = "ButtonAdd"
        Me.ButtonAdd.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.StructureTableLayoutPanel, "StructureTableLayoutPanel")
        Me.StructureTableLayoutPanel.Name = "StructureTableLayoutPanel"
        Me.StructureErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.StructureErrorProvider.ContainerControl = Me
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.Controls.Add(Me.TableLayoutPanelMain)
        Me.Name = "TreeStructureUserControl"
        Me.TableLayoutPanelMain.ResumeLayout(False)
        Me.TableLayoutPanelMiddle.ResumeLayout(False)
        Me.TableLayoutPanelMiddle.PerformLayout()
        CType(Me.StructureErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanelMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents StructureErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents TreeStructureGrid As Questify.Builder.UI.TreeStructureGrid
    Friend WithEvents TreeStructureViewerUserControl As Questify.Builder.UI.TreeStructureViewerUserControl
    Friend WithEvents TableLayoutPanelMiddle As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents VisualizeTreeToggleButton As System.Windows.Forms.CheckBox
    Friend WithEvents ButtonAdd As System.Windows.Forms.Button
    Friend WithEvents StructureTableLayoutPanel As System.Windows.Forms.TableLayoutPanel

End Class
