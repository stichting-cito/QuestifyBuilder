<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ConceptStructureUserControl
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ConceptStructureUserControl))
        Me.TableLayoutPanelMain = New System.Windows.Forms.TableLayoutPanel()
        Me.ConceptStructureGrid = New Questify.Builder.UI.ConceptStructureGrid()
        Me.StructureTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.ButtonAdd = New System.Windows.Forms.Button()
        Me.StructureErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.TableLayoutPanelMain.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.StructureErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        resources.ApplyResources(Me.TableLayoutPanelMain, "TableLayoutPanelMain")
        Me.TableLayoutPanelMain.Controls.Add(Me.ConceptStructureGrid, 0, 0)
        Me.TableLayoutPanelMain.Controls.Add(Me.StructureTableLayoutPanel, 1, 0)
        Me.TableLayoutPanelMain.Controls.Add(Me.TableLayoutPanel1, 1, 1)
        Me.TableLayoutPanelMain.Name = "TableLayoutPanelMain"
        resources.ApplyResources(Me.ConceptStructureGrid, "ConceptStructureGrid")
        Me.ConceptStructureGrid.Name = "ConceptStructureGrid"
        Me.TableLayoutPanelMain.SetRowSpan(Me.ConceptStructureGrid, 2)
        resources.ApplyResources(Me.StructureTableLayoutPanel, "StructureTableLayoutPanel")
        Me.StructureTableLayoutPanel.Name = "StructureTableLayoutPanel"
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonAdd, 1, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        resources.ApplyResources(Me.ButtonAdd, "ButtonAdd")
        Me.ButtonAdd.Image = Global.Questify.Builder.UI.My.Resources.Resources.add_icon_16
        Me.ButtonAdd.Name = "ButtonAdd"
        Me.ButtonAdd.UseVisualStyleBackColor = True
        Me.StructureErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.StructureErrorProvider.ContainerControl = Me
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.Controls.Add(Me.TableLayoutPanelMain)
        Me.Name = "ConceptStructureUserControl"
        Me.TableLayoutPanelMain.ResumeLayout(False)
        Me.TableLayoutPanelMain.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.StructureErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanelMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ConceptStructureGrid As Questify.Builder.UI.ConceptStructureGrid
    Friend WithEvents StructureTableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ButtonAdd As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents StructureErrorProvider As System.Windows.Forms.ErrorProvider

End Class
