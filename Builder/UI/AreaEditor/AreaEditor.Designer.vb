

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AreaEditor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AreaEditor))
        Me.ToolStrip = New System.Windows.Forms.ToolStrip()
        Me.bDelete = New System.Windows.Forms.ToolStripButton()
        Me.bDeleteAll = New System.Windows.Forms.ToolStripButton()
        Me.bCopy = New System.Windows.Forms.ToolStripButton()
        Me.bPaste = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.bNewCircle = New System.Windows.Forms.ToolStripButton()
        Me.bNewEllipse = New System.Windows.Forms.ToolStripButton()
        Me.bNewRect = New System.Windows.Forms.ToolStripButton()
        Me.bNewPointUpTriangle = New System.Windows.Forms.ToolStripButton()
        Me.bNewPointDownTriangle = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripWidthTextBox = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripHeightTextBox = New System.Windows.Forms.ToolStripTextBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Canvas1 = New Cito.Tester.Common.Controls.Canvas.Canvas()
        Me.CmdManager = New Questify.Builder.UI.Commanding.CommandManager(Me.components)
        Me.ToolStrip.SuspendLayout
        Me.TableLayoutPanel1.SuspendLayout
        Me.Panel1.SuspendLayout
        Me.SuspendLayout
        resources.ApplyResources(Me.ToolStrip, "ToolStrip")
        Me.ToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.bDelete, Me.bDeleteAll, Me.bCopy, Me.bPaste, Me.ToolStripSeparator1, Me.bNewCircle, Me.bNewEllipse, Me.bNewRect, Me.bNewPointUpTriangle, Me.bNewPointDownTriangle, Me.ToolStripSeparator2, Me.ToolStripWidthTextBox, Me.ToolStripLabel1, Me.ToolStripHeightTextBox})
        Me.ToolStrip.Name = "ToolStrip"
        Me.bDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.bDelete, "bDelete")
        Me.bDelete.Name = "bDelete"
        Me.bDeleteAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.bDeleteAll, "bDeleteAll")
        Me.bDeleteAll.Name = "bDeleteAll"
        Me.bCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.bCopy, "bCopy")
        Me.bCopy.Name = "bCopy"
        Me.bPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.bPaste, "bPaste")
        Me.bPaste.Name = "bPaste"
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        resources.ApplyResources(Me.ToolStripSeparator1, "ToolStripSeparator1")
        Me.bNewCircle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.bNewCircle, "bNewCircle")
        Me.bNewCircle.Name = "bNewCircle"
        Me.bNewEllipse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.bNewEllipse, "bNewEllipse")
        Me.bNewEllipse.Name = "bNewEllipse"
        Me.bNewRect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.bNewRect, "bNewRect")
        Me.bNewRect.Name = "bNewRect"
        Me.bNewPointUpTriangle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.bNewPointUpTriangle, "bNewPointUpTriangle")
        Me.bNewPointUpTriangle.Name = "bNewPointUpTriangle"
        Me.bNewPointDownTriangle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.bNewPointDownTriangle, "bNewPointDownTriangle")
        Me.bNewPointDownTriangle.Name = "bNewPointDownTriangle"
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        resources.ApplyResources(Me.ToolStripSeparator2, "ToolStripSeparator2")
        Me.ToolStripWidthTextBox.Name = "ToolStripWidthTextBox"
        resources.ApplyResources(Me.ToolStripWidthTextBox, "ToolStripWidthTextBox")
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        resources.ApplyResources(Me.ToolStripLabel1, "ToolStripLabel1")
        Me.ToolStripHeightTextBox.Name = "ToolStripHeightTextBox"
        resources.ApplyResources(Me.ToolStripHeightTextBox, "ToolStripHeightTextBox")
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.ToolStrip, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 1)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        resources.ApplyResources(Me.Panel1, "Panel1")
        Me.Panel1.Controls.Add(Me.Canvas1)
        Me.Panel1.Name = "Panel1"
        Me.Canvas1.BackColor = System.Drawing.Color.White
        Me.Canvas1.BackImageDrawOption = Cito.Tester.Common.Controls.Canvas.CanvasBackGroundImageDrawMethod.Unscaled
        Me.Canvas1.Color = System.Drawing.Color.Black
        resources.ApplyResources(Me.Canvas1, "Canvas1")
        Me.Canvas1.Name = "Canvas1"
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "AreaEditor"
        Me.ToolStrip.ResumeLayout(false)
        Me.ToolStrip.PerformLayout
        Me.TableLayoutPanel1.ResumeLayout(false)
        Me.TableLayoutPanel1.PerformLayout
        Me.Panel1.ResumeLayout(false)
        Me.ResumeLayout(false)
        Me.PerformLayout

    End Sub
    Private WithEvents ToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents bDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents bCopy As System.Windows.Forms.ToolStripButton
    Private WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents bNewCircle As System.Windows.Forms.ToolStripButton
    Friend WithEvents bNewEllipse As System.Windows.Forms.ToolStripButton
    Friend WithEvents bNewRect As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents CmdManager As UI.Commanding.CommandManager
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents bPaste As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Canvas1 As Cito.Tester.Common.Controls.Canvas.Canvas
    Friend WithEvents bDeleteAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripHeightTextBox As ToolStripTextBox
    Friend WithEvents ToolStripLabel1 As ToolStripLabel
    Friend WithEvents ToolStripWidthTextBox As ToolStripTextBox
    Friend WithEvents bNewPointUpTriangle As ToolStripButton
    Friend WithEvents bNewPointDownTriangle As ToolStripButton
End Class
