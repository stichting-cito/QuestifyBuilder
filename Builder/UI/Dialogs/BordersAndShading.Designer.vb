Namespace Dialogs

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class BordersAndShading
        Inherits DialogBase

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

        <System.Diagnostics.DebuggerStepThrough(), DebuggerNonUserCode()> _
        Private Sub InitializeComponent()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BordersAndShading))
            Me.tabControl = New System.Windows.Forms.TabControl()
            Me.tabBorders = New System.Windows.Forms.TabPage()
            Me.pBorderBorderChoice = New System.Windows.Forms.Panel()
            Me.LineColorButton = New Questify.Builder.UI.Controls.ColorButton()
            Me.LineColorLabel = New System.Windows.Forms.Label()
            Me.ListBoxLineStyle = New System.Windows.Forms.ListBox()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.ComboBoxLineWeight = New System.Windows.Forms.ComboBox()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.Panel2 = New System.Windows.Forms.Panel()
            Me.lblStyle = New System.Windows.Forms.Label()
            Me.pBorderSetting = New System.Windows.Forms.Panel()
            Me.lblSetting = New System.Windows.Forms.Label()
            Me.lblBorderCustom = New System.Windows.Forms.Label()
            Me.lblBorderGrid = New System.Windows.Forms.Label()
            Me.lblBorderAll = New System.Windows.Forms.Label()
            Me.lblBorderBox = New System.Windows.Forms.Label()
            Me.lblBorderNone = New System.Windows.Forms.Label()
            Me.tbpNone = New Questify.Builder.UI.Controls.TableCellPreview()
            Me.tbpBox = New Questify.Builder.UI.Controls.TableCellPreview()
            Me.tbpAll = New Questify.Builder.UI.Controls.TableCellPreview()
            Me.tbpGrid = New Questify.Builder.UI.Controls.TableCellPreview()
            Me.tbpCustom = New Questify.Builder.UI.Controls.TableCellPreview()
            Me.pBorderPreview = New System.Windows.Forms.Panel()
            Me.Panel3 = New System.Windows.Forms.Panel()
            Me.lblPreview = New System.Windows.Forms.Label()
            Me.ToggleRightVertical = New Questify.Builder.UI.Controls.TableToggleButton()
            Me.ToggleMidVertical = New Questify.Builder.UI.Controls.TableToggleButton()
            Me.ToggleLeftVertical = New Questify.Builder.UI.Controls.TableToggleButton()
            Me.ToggleBottomHorizontal = New Questify.Builder.UI.Controls.TableToggleButton()
            Me.ToggleMidHorizontal = New Questify.Builder.UI.Controls.TableToggleButton()
            Me.ToggleTopHorizontal = New Questify.Builder.UI.Controls.TableToggleButton()
            Me.TableCellPreview1 = New Questify.Builder.UI.Controls.TableCellPreview()
            Me.tabShading = New System.Windows.Forms.TabPage()
            Me.Panel6 = New System.Windows.Forms.Panel()
            Me.Label5 = New System.Windows.Forms.Label()
            Me.ClearColor = New System.Windows.Forms.Button()
            Me.ColorButton1 = New Questify.Builder.UI.Controls.ColorButton()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.Panel4 = New System.Windows.Forms.Panel()
            Me.Panel5 = New System.Windows.Forms.Panel()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.TableCellPreview2 = New Questify.Builder.UI.Controls.TableCellPreview()
            Me.ContentPanel.SuspendLayout()
            Me.tabControl.SuspendLayout()
            Me.tabBorders.SuspendLayout()
            Me.pBorderBorderChoice.SuspendLayout()
            Me.pBorderSetting.SuspendLayout()
            Me.pBorderPreview.SuspendLayout()
            Me.tabShading.SuspendLayout()
            Me.Panel6.SuspendLayout()
            Me.Panel4.SuspendLayout()
            Me.SuspendLayout()
            Me.ContentPanel.Controls.Add(Me.tabControl)
            resources.ApplyResources(Me.ContentPanel, "ContentPanel")
            resources.ApplyResources(Me.tabControl, "tabControl")
            Me.tabControl.Controls.Add(Me.tabBorders)
            Me.tabControl.Controls.Add(Me.tabShading)
            Me.tabControl.Name = "tabControl"
            Me.tabControl.SelectedIndex = 0
            Me.tabBorders.Controls.Add(Me.pBorderBorderChoice)
            Me.tabBorders.Controls.Add(Me.pBorderSetting)
            Me.tabBorders.Controls.Add(Me.pBorderPreview)
            resources.ApplyResources(Me.tabBorders, "tabBorders")
            Me.tabBorders.Name = "tabBorders"
            Me.tabBorders.UseVisualStyleBackColor = True
            resources.ApplyResources(Me.pBorderBorderChoice, "pBorderBorderChoice")
            Me.pBorderBorderChoice.Controls.Add(Me.LineColorButton)
            Me.pBorderBorderChoice.Controls.Add(Me.LineColorLabel)
            Me.pBorderBorderChoice.Controls.Add(Me.ListBoxLineStyle)
            Me.pBorderBorderChoice.Controls.Add(Me.Label3)
            Me.pBorderBorderChoice.Controls.Add(Me.ComboBoxLineWeight)
            Me.pBorderBorderChoice.Controls.Add(Me.Label1)
            Me.pBorderBorderChoice.Controls.Add(Me.Panel2)
            Me.pBorderBorderChoice.Controls.Add(Me.lblStyle)
            Me.pBorderBorderChoice.Name = "pBorderBorderChoice"
            resources.ApplyResources(Me.LineColorButton, "LineColorButton")
            Me.LineColorButton.Name = "LineColorButton"
            Me.LineColorButton.UseVisualStyleBackColor = True
            resources.ApplyResources(Me.LineColorLabel, "LineColorLabel")
            Me.LineColorLabel.Name = "LineColorLabel"
            Me.ListBoxLineStyle.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
            Me.ListBoxLineStyle.FormattingEnabled = True
            resources.ApplyResources(Me.ListBoxLineStyle, "ListBoxLineStyle")
            Me.ListBoxLineStyle.Items.AddRange(New Object() {resources.GetString("ListBoxLineStyle.Items"), resources.GetString("ListBoxLineStyle.Items1"), resources.GetString("ListBoxLineStyle.Items2"), resources.GetString("ListBoxLineStyle.Items3")})
            Me.ListBoxLineStyle.Name = "ListBoxLineStyle"
            resources.ApplyResources(Me.Label3, "Label3")
            Me.Label3.Name = "Label3"
            Me.ComboBoxLineWeight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ComboBoxLineWeight.FormattingEnabled = True
            Me.ComboBoxLineWeight.Items.AddRange(New Object() {resources.GetString("ComboBoxLineWeight.Items"), resources.GetString("ComboBoxLineWeight.Items1"), resources.GetString("ComboBoxLineWeight.Items2"), resources.GetString("ComboBoxLineWeight.Items3"), resources.GetString("ComboBoxLineWeight.Items4")})
            resources.ApplyResources(Me.ComboBoxLineWeight, "ComboBoxLineWeight")
            Me.ComboBoxLineWeight.Name = "ComboBoxLineWeight"
            resources.ApplyResources(Me.Label1, "Label1")
            Me.Label1.Name = "Label1"
            Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            resources.ApplyResources(Me.Panel2, "Panel2")
            Me.Panel2.Name = "Panel2"
            resources.ApplyResources(Me.lblStyle, "lblStyle")
            Me.lblStyle.Name = "lblStyle"
            resources.ApplyResources(Me.pBorderSetting, "pBorderSetting")
            Me.pBorderSetting.Controls.Add(Me.lblSetting)
            Me.pBorderSetting.Controls.Add(Me.lblBorderCustom)
            Me.pBorderSetting.Controls.Add(Me.lblBorderGrid)
            Me.pBorderSetting.Controls.Add(Me.lblBorderAll)
            Me.pBorderSetting.Controls.Add(Me.lblBorderBox)
            Me.pBorderSetting.Controls.Add(Me.lblBorderNone)
            Me.pBorderSetting.Controls.Add(Me.tbpNone)
            Me.pBorderSetting.Controls.Add(Me.tbpBox)
            Me.pBorderSetting.Controls.Add(Me.tbpAll)
            Me.pBorderSetting.Controls.Add(Me.tbpGrid)
            Me.pBorderSetting.Controls.Add(Me.tbpCustom)
            Me.pBorderSetting.Name = "pBorderSetting"
            resources.ApplyResources(Me.lblSetting, "lblSetting")
            Me.lblSetting.Name = "lblSetting"
            resources.ApplyResources(Me.lblBorderCustom, "lblBorderCustom")
            Me.lblBorderCustom.Name = "lblBorderCustom"
            resources.ApplyResources(Me.lblBorderGrid, "lblBorderGrid")
            Me.lblBorderGrid.Name = "lblBorderGrid"
            resources.ApplyResources(Me.lblBorderAll, "lblBorderAll")
            Me.lblBorderAll.Name = "lblBorderAll"
            resources.ApplyResources(Me.lblBorderBox, "lblBorderBox")
            Me.lblBorderBox.Name = "lblBorderBox"
            resources.ApplyResources(Me.lblBorderNone, "lblBorderNone")
            Me.lblBorderNone.Name = "lblBorderNone"
            Me.tbpNone.IsSelected = False
            resources.ApplyResources(Me.tbpNone, "tbpNone")
            Me.tbpNone.Name = "tbpNone"
            Me.tbpBox.IsSelected = False
            resources.ApplyResources(Me.tbpBox, "tbpBox")
            Me.tbpBox.Name = "tbpBox"
            Me.tbpAll.IsSelected = False
            resources.ApplyResources(Me.tbpAll, "tbpAll")
            Me.tbpAll.Name = "tbpAll"
            Me.tbpGrid.IsSelected = False
            resources.ApplyResources(Me.tbpGrid, "tbpGrid")
            Me.tbpGrid.Name = "tbpGrid"
            Me.tbpCustom.IsSelected = False
            resources.ApplyResources(Me.tbpCustom, "tbpCustom")
            Me.tbpCustom.Name = "tbpCustom"
            resources.ApplyResources(Me.pBorderPreview, "pBorderPreview")
            Me.pBorderPreview.Controls.Add(Me.Panel3)
            Me.pBorderPreview.Controls.Add(Me.lblPreview)
            Me.pBorderPreview.Controls.Add(Me.ToggleRightVertical)
            Me.pBorderPreview.Controls.Add(Me.ToggleMidVertical)
            Me.pBorderPreview.Controls.Add(Me.ToggleLeftVertical)
            Me.pBorderPreview.Controls.Add(Me.ToggleBottomHorizontal)
            Me.pBorderPreview.Controls.Add(Me.ToggleMidHorizontal)
            Me.pBorderPreview.Controls.Add(Me.ToggleTopHorizontal)
            Me.pBorderPreview.Controls.Add(Me.TableCellPreview1)
            Me.pBorderPreview.Name = "pBorderPreview"
            Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            resources.ApplyResources(Me.Panel3, "Panel3")
            Me.Panel3.Name = "Panel3"
            resources.ApplyResources(Me.lblPreview, "lblPreview")
            Me.lblPreview.Name = "lblPreview"
            resources.ApplyResources(Me.ToggleRightVertical, "ToggleRightVertical")
            Me.ToggleRightVertical.Name = "ToggleRightVertical"
            Me.ToggleRightVertical.PointFrom = System.Drawing.ContentAlignment.TopRight
            Me.ToggleRightVertical.PointTo = System.Drawing.ContentAlignment.BottomRight
            Me.ToggleRightVertical.UseVisualStyleBackColor = True
            resources.ApplyResources(Me.ToggleMidVertical, "ToggleMidVertical")
            Me.ToggleMidVertical.Name = "ToggleMidVertical"
            Me.ToggleMidVertical.PointFrom = System.Drawing.ContentAlignment.BottomCenter
            Me.ToggleMidVertical.PointTo = System.Drawing.ContentAlignment.TopCenter
            Me.ToggleMidVertical.UseVisualStyleBackColor = True
            resources.ApplyResources(Me.ToggleLeftVertical, "ToggleLeftVertical")
            Me.ToggleLeftVertical.Name = "ToggleLeftVertical"
            Me.ToggleLeftVertical.PointFrom = System.Drawing.ContentAlignment.TopLeft
            Me.ToggleLeftVertical.PointTo = System.Drawing.ContentAlignment.BottomLeft
            Me.ToggleLeftVertical.UseVisualStyleBackColor = True
            resources.ApplyResources(Me.ToggleBottomHorizontal, "ToggleBottomHorizontal")
            Me.ToggleBottomHorizontal.Name = "ToggleBottomHorizontal"
            Me.ToggleBottomHorizontal.PointFrom = System.Drawing.ContentAlignment.BottomLeft
            Me.ToggleBottomHorizontal.PointTo = System.Drawing.ContentAlignment.BottomRight
            Me.ToggleBottomHorizontal.UseVisualStyleBackColor = True
            resources.ApplyResources(Me.ToggleMidHorizontal, "ToggleMidHorizontal")
            Me.ToggleMidHorizontal.Name = "ToggleMidHorizontal"
            Me.ToggleMidHorizontal.PointFrom = System.Drawing.ContentAlignment.MiddleLeft
            Me.ToggleMidHorizontal.PointTo = System.Drawing.ContentAlignment.MiddleRight
            Me.ToggleMidHorizontal.UseVisualStyleBackColor = True
            resources.ApplyResources(Me.ToggleTopHorizontal, "ToggleTopHorizontal")
            Me.ToggleTopHorizontal.Name = "ToggleTopHorizontal"
            Me.ToggleTopHorizontal.PointFrom = System.Drawing.ContentAlignment.TopLeft
            Me.ToggleTopHorizontal.PointTo = System.Drawing.ContentAlignment.TopRight
            Me.ToggleTopHorizontal.UseVisualStyleBackColor = True
            Me.TableCellPreview1.IsSelected = False
            resources.ApplyResources(Me.TableCellPreview1, "TableCellPreview1")
            Me.TableCellPreview1.Name = "TableCellPreview1"
            Me.tabShading.Controls.Add(Me.Panel6)
            Me.tabShading.Controls.Add(Me.Panel4)
            resources.ApplyResources(Me.tabShading, "tabShading")
            Me.tabShading.Name = "tabShading"
            Me.tabShading.UseVisualStyleBackColor = True
            resources.ApplyResources(Me.Panel6, "Panel6")
            Me.Panel6.Controls.Add(Me.Label5)
            Me.Panel6.Controls.Add(Me.ClearColor)
            Me.Panel6.Controls.Add(Me.ColorButton1)
            Me.Panel6.Controls.Add(Me.Label4)
            Me.Panel6.Name = "Panel6"
            resources.ApplyResources(Me.Label5, "Label5")
            Me.Label5.Name = "Label5"
            resources.ApplyResources(Me.ClearColor, "ClearColor")
            Me.ClearColor.Name = "ClearColor"
            Me.ClearColor.UseVisualStyleBackColor = True
            resources.ApplyResources(Me.ColorButton1, "ColorButton1")
            Me.ColorButton1.Name = "ColorButton1"
            Me.ColorButton1.UseVisualStyleBackColor = True
            resources.ApplyResources(Me.Label4, "Label4")
            Me.Label4.Name = "Label4"
            resources.ApplyResources(Me.Panel4, "Panel4")
            Me.Panel4.Controls.Add(Me.Panel5)
            Me.Panel4.Controls.Add(Me.Label2)
            Me.Panel4.Controls.Add(Me.TableCellPreview2)
            Me.Panel4.Name = "Panel4"
            Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            resources.ApplyResources(Me.Panel5, "Panel5")
            Me.Panel5.Name = "Panel5"
            resources.ApplyResources(Me.Label2, "Label2")
            Me.Label2.Name = "Label2"
            Me.TableCellPreview2.IsSelected = False
            resources.ApplyResources(Me.TableCellPreview2, "TableCellPreview2")
            Me.TableCellPreview2.Name = "TableCellPreview2"
            resources.ApplyResources(Me, "$this")
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Name = "BordersAndShading"
            Me.ContentPanel.ResumeLayout(False)
            Me.tabControl.ResumeLayout(False)
            Me.tabBorders.ResumeLayout(False)
            Me.pBorderBorderChoice.ResumeLayout(False)
            Me.pBorderBorderChoice.PerformLayout()
            Me.pBorderSetting.ResumeLayout(False)
            Me.pBorderSetting.PerformLayout()
            Me.pBorderPreview.ResumeLayout(False)
            Me.pBorderPreview.PerformLayout()
            Me.tabShading.ResumeLayout(False)
            Me.Panel6.ResumeLayout(False)
            Me.Panel6.PerformLayout()
            Me.Panel4.ResumeLayout(False)
            Me.Panel4.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents tabControl As System.Windows.Forms.TabControl
        Friend WithEvents tabBorders As System.Windows.Forms.TabPage
        Friend WithEvents pBorderBorderChoice As System.Windows.Forms.Panel
        Friend WithEvents lblStyle As System.Windows.Forms.Label
        Friend WithEvents pBorderSetting As System.Windows.Forms.Panel
        Friend WithEvents lblSetting As System.Windows.Forms.Label
        Friend WithEvents lblBorderCustom As System.Windows.Forms.Label
        Friend WithEvents lblBorderGrid As System.Windows.Forms.Label
        Friend WithEvents lblBorderAll As System.Windows.Forms.Label
        Friend WithEvents lblBorderBox As System.Windows.Forms.Label
        Friend WithEvents lblBorderNone As System.Windows.Forms.Label
        Friend WithEvents tbpNone As Questify.Builder.UI.Controls.TableCellPreview
        Friend WithEvents tbpBox As Questify.Builder.UI.Controls.TableCellPreview
        Friend WithEvents tbpAll As Questify.Builder.UI.Controls.TableCellPreview
        Friend WithEvents tbpGrid As Questify.Builder.UI.Controls.TableCellPreview
        Friend WithEvents tbpCustom As Questify.Builder.UI.Controls.TableCellPreview
        Friend WithEvents pBorderPreview As System.Windows.Forms.Panel
        Friend WithEvents lblPreview As System.Windows.Forms.Label
        Friend WithEvents ToggleRightVertical As Questify.Builder.UI.Controls.TableToggleButton
        Friend WithEvents ToggleMidVertical As Questify.Builder.UI.Controls.TableToggleButton
        Friend WithEvents ToggleLeftVertical As Questify.Builder.UI.Controls.TableToggleButton
        Friend WithEvents ToggleBottomHorizontal As Questify.Builder.UI.Controls.TableToggleButton
        Friend WithEvents ToggleMidHorizontal As Questify.Builder.UI.Controls.TableToggleButton
        Friend WithEvents ToggleTopHorizontal As Questify.Builder.UI.Controls.TableToggleButton
        Friend WithEvents TableCellPreview1 As Questify.Builder.UI.Controls.TableCellPreview
        Friend WithEvents tabShading As System.Windows.Forms.TabPage
        Friend WithEvents Panel2 As System.Windows.Forms.Panel
        Friend WithEvents Panel3 As System.Windows.Forms.Panel
        Friend WithEvents ListBoxLineStyle As System.Windows.Forms.ListBox
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents ComboBoxLineWeight As System.Windows.Forms.ComboBox
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents Panel6 As System.Windows.Forms.Panel
        Friend WithEvents Label5 As System.Windows.Forms.Label
        Friend WithEvents ClearColor As System.Windows.Forms.Button
        Friend WithEvents ColorButton1 As Questify.Builder.UI.Controls.ColorButton
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents Panel4 As System.Windows.Forms.Panel
        Friend WithEvents Panel5 As System.Windows.Forms.Panel
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents TableCellPreview2 As Questify.Builder.UI.Controls.TableCellPreview
        Friend WithEvents LineColorLabel As Label
        Friend WithEvents LineColorButton As Controls.ColorButton
    End Class

End Namespace
