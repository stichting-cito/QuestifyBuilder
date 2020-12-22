Imports System.Threading
Imports System.Windows.Forms
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class XHtmlEditor
    Inherits System.Windows.Forms.UserControl

    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                Interlocked.Decrement(InstanceCount)
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(XHtmlEditor))
        Me.C1Editor1 = New C1.Win.C1Editor.C1Editor()
        Me.mainContextStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmInline = New System.Windows.Forms.ToolStripMenuItem()
        Me.InlineToolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmRemoveReference = New System.Windows.Forms.ToolStripMenuItem()
        Me.RemoveReferenceToolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmTable = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmInsertRowAbove = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmInsertRowBelow = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmDeleteRow = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmInsertColumnLeft = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmInsertColumnRight = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmDeleteColumn = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmMergeSelectedCells = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmSplitCell = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmSplitCellVert = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator12 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmCellBorders = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmCellProperties = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmCellInnerMargins = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator13 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmTableProperties = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmRemoveTable = New System.Windows.Forms.ToolStripMenuItem()
        Me.TableToolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmEditFormula = New System.Windows.Forms.ToolStripMenuItem()
        Me.InsertTableC1EditorToolStripButton = New C1.Win.C1Editor.ToolStrips.C1EditorToolStripButton()
        Me.C1EditorToolStripButton1 = New C1.Win.C1Editor.ToolStrips.C1EditorToolStripButton()
        Me.bPasteFromWord = New System.Windows.Forms.ToolStripButton()
        Me.bPasteAsText = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.bImage = New System.Windows.Forms.ToolStripButton()
        Me.bMovie = New System.Windows.Forms.ToolStripButton()
        Me.bAudio = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.bTable = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.bLockEditImg = New System.Windows.Forms.ToolStripButton()
        Me.cbStyle = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.bConvertToRoman = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStrip = New System.Windows.Forms.ToolStrip()
        Me.bCut = New C1.Win.C1Editor.ToolStrips.C1EditorToolStripButton()
        Me.bCopy = New C1.Win.C1Editor.ToolStrips.C1EditorToolStripButton()
        Me.bUndo = New C1.Win.C1Editor.ToolStrips.C1EditorToolStripButton()
        Me.bRedo = New C1.Win.C1Editor.ToolStrips.C1EditorToolStripButton()
        Me.bInsertControl = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.bCustomInteraction = New System.Windows.Forms.ToolStripButton()
        Me.bUnderLine = New C1.Win.C1Editor.ToolStrips.C1EditorToolStripButton()
        Me.bBold = New C1.Win.C1Editor.ToolStrips.C1EditorToolStripButton()
        Me.bItalic = New C1.Win.C1Editor.ToolStrips.C1EditorToolStripButton()
        Me.bSuperScript = New C1.Win.C1Editor.ToolStrips.C1EditorToolStripButton()
        Me.bSubscript = New C1.Win.C1Editor.ToolStrips.C1EditorToolStripButton()
        Me.bStrikeThrough = New C1.Win.C1Editor.ToolStrips.C1EditorToolStripButton()
        Me.bClearFormatting = New C1.Win.C1Editor.ToolStrips.C1EditorToolStripButton()
        Me.cbLanguage = New System.Windows.Forms.ToolStripComboBox()
        Me.bAlignLeft = New C1.Win.C1Editor.ToolStrips.C1EditorToolStripButton()
        Me.bAlignCenter = New C1.Win.C1Editor.ToolStrips.C1EditorToolStripButton()
        Me.bAlignRight = New C1.Win.C1Editor.ToolStrips.C1EditorToolStripButton()
        Me.bBulletList = New C1.Win.C1Editor.ToolStrips.C1EditorToolStripButton()
        Me.bNumberedList = New C1.Win.C1Editor.ToolStrips.C1EditorToolStripButton()
        Me.bIndent = New C1.Win.C1Editor.ToolStrips.C1EditorToolStripButton()
        Me.bDecreaseIndent = New C1.Win.C1Editor.ToolStrips.C1EditorToolStripButton()
        Me.bInsertSymbol = New System.Windows.Forms.ToolStripButton()
        Me.bInsertFormula = New System.Windows.Forms.ToolStripButton()
        Me.bFitToContents = New System.Windows.Forms.ToolStripButton()
        Me.ddbReferences = New System.Windows.Forms.ToolStripDropDownButton()
        Me.tsmiElementReference = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiSymbolReference = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiHighlightReference = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiRemoveReference = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiOverviewReferences = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsbReferTo = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
        Me.bTTSMute = New System.Windows.Forms.ToolStripButton()
        Me.bTTSAlternative = New System.Windows.Forms.ToolStripButton()
        Me.bTTSPause = New System.Windows.Forms.ToolStripSplitButton()
        Me.bTTSDelete = New System.Windows.Forms.ToolStripButton()
        Me.bPopup = New C1.Win.C1Editor.ToolStrips.C1EditorToolStripButton()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.CmdManager = New Questify.Builder.UI.Commanding.CommandManager(Me.components)
        CType(Me.C1Editor1, System.ComponentModel.ISupportInitialize).BeginInit
        Me.mainContextStrip.SuspendLayout
        Me.ToolStrip.SuspendLayout
        Me.TableLayoutPanel1.SuspendLayout
        Me.SuspendLayout
        Me.C1Editor1.AcceptsTab = false
        Me.C1Editor1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.C1Editor1.ContextMenuStrip = Me.mainContextStrip
        resources.ApplyResources(Me.C1Editor1, "C1Editor1")
        Me.C1Editor1.Name = "C1Editor1"
        Me.C1Editor1.Xml = resources.GetString("C1Editor1.Xml")
        Me.C1Editor1.XmlExtensions = resources.GetString("C1Editor1.XmlExtensions")
        Me.mainContextStrip.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.mainContextStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmInline, Me.InlineToolStripSeparator, Me.tsmRemoveReference, Me.RemoveReferenceToolStripSeparator, Me.tsmTable, Me.TableToolStripSeparator, Me.tsmEditFormula})
        Me.mainContextStrip.Name = "ContextMenuStrip1"
        Me.mainContextStrip.ShowImageMargin = false
        resources.ApplyResources(Me.mainContextStrip, "mainContextStrip")
        Me.tsmInline.Name = "tsmInline"
        resources.ApplyResources(Me.tsmInline, "tsmInline")
        Me.InlineToolStripSeparator.Name = "InlineToolStripSeparator"
        resources.ApplyResources(Me.InlineToolStripSeparator, "InlineToolStripSeparator")
        Me.tsmRemoveReference.Name = "tsmRemoveReference"
        resources.ApplyResources(Me.tsmRemoveReference, "tsmRemoveReference")
        Me.RemoveReferenceToolStripSeparator.Name = "RemoveReferenceToolStripSeparator"
        resources.ApplyResources(Me.RemoveReferenceToolStripSeparator, "RemoveReferenceToolStripSeparator")
        Me.tsmTable.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmInsertRowAbove, Me.tsmInsertRowBelow, Me.tsmDeleteRow, Me.ToolStripSeparator10, Me.tsmInsertColumnLeft, Me.tsmInsertColumnRight, Me.tsmDeleteColumn, Me.ToolStripSeparator11, Me.tsmMergeSelectedCells, Me.tsmSplitCell, Me.tsmSplitCellVert, Me.ToolStripSeparator12, Me.tsmCellBorders, Me.tsmCellProperties, Me.tsmCellInnerMargins, Me.ToolStripSeparator13, Me.tsmTableProperties, Me.tsmRemoveTable})
        Me.tsmTable.Name = "tsmTable"
        resources.ApplyResources(Me.tsmTable, "tsmTable")
        Me.tsmInsertRowAbove.Name = "tsmInsertRowAbove"
        resources.ApplyResources(Me.tsmInsertRowAbove, "tsmInsertRowAbove")
        Me.tsmInsertRowBelow.Name = "tsmInsertRowBelow"
        resources.ApplyResources(Me.tsmInsertRowBelow, "tsmInsertRowBelow")
        Me.tsmDeleteRow.Name = "tsmDeleteRow"
        resources.ApplyResources(Me.tsmDeleteRow, "tsmDeleteRow")
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        resources.ApplyResources(Me.ToolStripSeparator10, "ToolStripSeparator10")
        Me.tsmInsertColumnLeft.Name = "tsmInsertColumnLeft"
        resources.ApplyResources(Me.tsmInsertColumnLeft, "tsmInsertColumnLeft")
        Me.tsmInsertColumnRight.Name = "tsmInsertColumnRight"
        resources.ApplyResources(Me.tsmInsertColumnRight, "tsmInsertColumnRight")
        Me.tsmDeleteColumn.Name = "tsmDeleteColumn"
        resources.ApplyResources(Me.tsmDeleteColumn, "tsmDeleteColumn")
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        resources.ApplyResources(Me.ToolStripSeparator11, "ToolStripSeparator11")
        Me.tsmMergeSelectedCells.Name = "tsmMergeSelectedCells"
        resources.ApplyResources(Me.tsmMergeSelectedCells, "tsmMergeSelectedCells")
        Me.tsmSplitCell.Name = "tsmSplitCell"
        resources.ApplyResources(Me.tsmSplitCell, "tsmSplitCell")
        Me.tsmSplitCellVert.Name = "tsmSplitCellVert"
        resources.ApplyResources(Me.tsmSplitCellVert, "tsmSplitCellVert")
        Me.ToolStripSeparator12.Name = "ToolStripSeparator12"
        resources.ApplyResources(Me.ToolStripSeparator12, "ToolStripSeparator12")
        Me.tsmCellBorders.Name = "tsmCellBorders"
        resources.ApplyResources(Me.tsmCellBorders, "tsmCellBorders")
        Me.tsmCellProperties.Name = "tsmCellProperties"
        resources.ApplyResources(Me.tsmCellProperties, "tsmCellProperties")
        Me.tsmCellInnerMargins.Name = "tsmCellInnerMargins"
        resources.ApplyResources(Me.tsmCellInnerMargins, "tsmCellInnerMargins")
        Me.ToolStripSeparator13.Name = "ToolStripSeparator13"
        resources.ApplyResources(Me.ToolStripSeparator13, "ToolStripSeparator13")
        Me.tsmTableProperties.Name = "tsmTableProperties"
        resources.ApplyResources(Me.tsmTableProperties, "tsmTableProperties")
        Me.tsmRemoveTable.Name = "tsmRemoveTable"
        resources.ApplyResources(Me.tsmRemoveTable, "tsmRemoveTable")
        Me.TableToolStripSeparator.Name = "TableToolStripSeparator"
        resources.ApplyResources(Me.TableToolStripSeparator, "TableToolStripSeparator")
        Me.tsmEditFormula.Name = "tsmEditFormula"
        resources.ApplyResources(Me.tsmEditFormula, "tsmEditFormula")
        Me.InsertTableC1EditorToolStripButton.Command = C1.Win.C1Editor.ToolStrips.CommandButton.None
        Me.InsertTableC1EditorToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.InsertTableC1EditorToolStripButton.Editor = Nothing
        Me.InsertTableC1EditorToolStripButton.Image = My.Resources.Resources.InsertTable_small
        resources.ApplyResources(Me.InsertTableC1EditorToolStripButton, "InsertTableC1EditorToolStripButton")
        Me.InsertTableC1EditorToolStripButton.Name = "InsertTableC1EditorToolStripButton"
        Me.InsertTableC1EditorToolStripButton.Tag = "InsertTableC1EditorToolStripButton"
        Me.C1EditorToolStripButton1.Command = C1.Win.C1Editor.ToolStrips.CommandButton.None
        Me.C1EditorToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.C1EditorToolStripButton1.Editor = Nothing
        Me.C1EditorToolStripButton1.Image = My.Resources.Resources.InsertTable_small
        resources.ApplyResources(Me.C1EditorToolStripButton1, "C1EditorToolStripButton1")
        Me.C1EditorToolStripButton1.Name = "C1EditorToolStripButton1"
        Me.C1EditorToolStripButton1.Tag = "InsertTableC1EditorToolStripButton"
        Me.bPasteFromWord.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bPasteFromWord.Image = My.Resources.Resources.PastFromWord_small
        resources.ApplyResources(Me.bPasteFromWord, "bPasteFromWord")
        Me.bPasteFromWord.Name = "bPasteFromWord"
        Me.bPasteAsText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.bPasteAsText, "bPasteAsText")
        Me.bPasteAsText.Name = "bPasteAsText"
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        resources.ApplyResources(Me.ToolStripSeparator1, "ToolStripSeparator1")
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        resources.ApplyResources(Me.ToolStripSeparator2, "ToolStripSeparator2")
        Me.bImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bImage.Image = My.Resources.Resources.InsertImage_small
        resources.ApplyResources(Me.bImage, "bImage")
        Me.bImage.Name = "bImage"
        Me.bMovie.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bMovie.Image = My.Resources.Resources.insert_video
        resources.ApplyResources(Me.bMovie, "bMovie")
        Me.bMovie.Name = "bMovie"
        Me.bAudio.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bAudio.Image = My.Resources.Resources.insert_audio
        resources.ApplyResources(Me.bAudio, "bAudio")
        Me.bAudio.Name = "bAudio"
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        resources.ApplyResources(Me.ToolStripSeparator3, "ToolStripSeparator3")
        Me.bTable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bTable.Image = My.Resources.Resources.InsertTable_small
        resources.ApplyResources(Me.bTable, "bTable")
        Me.bTable.Name = "bTable"
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        resources.ApplyResources(Me.ToolStripSeparator4, "ToolStripSeparator4")
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        resources.ApplyResources(Me.ToolStripSeparator5, "ToolStripSeparator5")
        Me.bLockEditImg.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bLockEditImg.Image = My.Resources.Resources.LockedForEditImage
        resources.ApplyResources(Me.bLockEditImg, "bLockEditImg")
        Me.bLockEditImg.Name = "bLockEditImg"
        Me.cbStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbStyle.Name = "cbStyle"
        resources.ApplyResources(Me.cbStyle, "cbStyle")
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        resources.ApplyResources(Me.ToolStripSeparator6, "ToolStripSeparator6")
        Me.bConvertToRoman.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bConvertToRoman.Image = My.Resources.Resources.ConvertToRoman_small
        resources.ApplyResources(Me.bConvertToRoman, "bConvertToRoman")
        Me.bConvertToRoman.Name = "bConvertToRoman"
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        resources.ApplyResources(Me.ToolStripSeparator7, "ToolStripSeparator7")
        Me.ToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.bCut, Me.bCopy, Me.bPasteAsText, Me.bPasteFromWord, Me.ToolStripSeparator1, Me.bUndo, Me.bRedo, Me.ToolStripSeparator2, Me.bInsertControl, Me.ToolStripSeparator8, Me.bImage, Me.bMovie, Me.bAudio, Me.bCustomInteraction, Me.ToolStripSeparator3, Me.bTable, Me.ToolStripSeparator4, Me.bUnderLine, Me.bBold, Me.bItalic, Me.bSuperScript, Me.bSubscript, Me.bStrikeThrough, Me.ToolStripSeparator5, Me.bLockEditImg, Me.bClearFormatting, Me.cbStyle, Me.cbLanguage, Me.ToolStripSeparator6, Me.bAlignLeft, Me.bAlignCenter, Me.bAlignRight, Me.bBulletList, Me.bNumberedList, Me.bIndent, Me.bDecreaseIndent, Me.bConvertToRoman, Me.ToolStripSeparator7, Me.bInsertSymbol, Me.bInsertFormula, Me.bFitToContents, Me.ddbReferences, Me.tsbReferTo, Me.ToolStripSeparator9, Me.bTTSMute, Me.bTTSAlternative, Me.bTTSPause, Me.bTTSDelete})
        resources.ApplyResources(Me.ToolStrip, "ToolStrip")
        Me.ToolStrip.Name = "ToolStrip"
        Me.bCut.Command = C1.Win.C1Editor.ToolStrips.CommandButton.None
        Me.bCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bCut.Editor = Me.C1Editor1
        resources.ApplyResources(Me.bCut, "bCut")
        Me.bCut.Name = "bCut"
        Me.bCopy.Command = C1.Win.C1Editor.ToolStrips.CommandButton.None
        Me.bCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bCopy.Editor = Me.C1Editor1
        resources.ApplyResources(Me.bCopy, "bCopy")
        Me.bCopy.Name = "bCopy"
        Me.bUndo.Command = C1.Win.C1Editor.ToolStrips.CommandButton.Undo
        Me.bUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bUndo.Editor = Me.C1Editor1
        resources.ApplyResources(Me.bUndo, "bUndo")
        Me.bUndo.Name = "bUndo"
        Me.bRedo.Command = C1.Win.C1Editor.ToolStrips.CommandButton.Redo
        Me.bRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bRedo.Editor = Me.C1Editor1
        resources.ApplyResources(Me.bRedo, "bRedo")
        Me.bRedo.Name = "bRedo"
        Me.bInsertControl.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.bInsertControl, "bInsertControl")
        Me.bInsertControl.Image = My.Resources.Resources.add_icon_16
        Me.bInsertControl.Name = "bInsertControl"
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        resources.ApplyResources(Me.ToolStripSeparator8, "ToolStripSeparator8")
        Me.bCustomInteraction.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bCustomInteraction.Image = My.Resources.Resources.customInteraction_16
        resources.ApplyResources(Me.bCustomInteraction, "bCustomInteraction")
        Me.bCustomInteraction.Name = "bCustomInteraction"
        Me.bUnderLine.Command = C1.Win.C1Editor.ToolStrips.CommandButton.Underline
        Me.bUnderLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bUnderLine.Editor = Me.C1Editor1
        resources.ApplyResources(Me.bUnderLine, "bUnderLine")
        Me.bUnderLine.Name = "bUnderLine"
        Me.bBold.Command = C1.Win.C1Editor.ToolStrips.CommandButton.Bold
        Me.bBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bBold.Editor = Me.C1Editor1
        resources.ApplyResources(Me.bBold, "bBold")
        Me.bBold.Name = "bBold"
        Me.bItalic.Command = C1.Win.C1Editor.ToolStrips.CommandButton.Italic
        Me.bItalic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bItalic.Editor = Me.C1Editor1
        resources.ApplyResources(Me.bItalic, "bItalic")
        Me.bItalic.Name = "bItalic"
        Me.bSuperScript.Command = C1.Win.C1Editor.ToolStrips.CommandButton.Superscript
        Me.bSuperScript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bSuperScript.Editor = Me.C1Editor1
        resources.ApplyResources(Me.bSuperScript, "bSuperScript")
        Me.bSuperScript.Name = "bSuperScript"
        Me.bSubscript.Command = C1.Win.C1Editor.ToolStrips.CommandButton.Subscript
        Me.bSubscript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bSubscript.Editor = Me.C1Editor1
        resources.ApplyResources(Me.bSubscript, "bSubscript")
        Me.bSubscript.Name = "bSubscript"
        Me.bStrikeThrough.Command = C1.Win.C1Editor.ToolStrips.CommandButton.Strikethrough
        Me.bStrikeThrough.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bStrikeThrough.Editor = Me.C1Editor1
        resources.ApplyResources(Me.bStrikeThrough, "bStrikeThrough")
        Me.bStrikeThrough.Name = "bStrikeThrough"
        Me.bClearFormatting.Command = C1.Win.C1Editor.ToolStrips.CommandButton.ClearFormatting
        Me.bClearFormatting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bClearFormatting.Editor = Me.C1Editor1
        resources.ApplyResources(Me.bClearFormatting, "bClearFormatting")
        Me.bClearFormatting.Name = "bClearFormatting"
        Me.cbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbLanguage.Name = "cbLanguage"
        resources.ApplyResources(Me.cbLanguage, "cbLanguage")
        Me.bAlignLeft.Command = C1.Win.C1Editor.ToolStrips.CommandButton.Left
        Me.bAlignLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bAlignLeft.Editor = Me.C1Editor1
        resources.ApplyResources(Me.bAlignLeft, "bAlignLeft")
        Me.bAlignLeft.Name = "bAlignLeft"
        Me.bAlignCenter.Command = C1.Win.C1Editor.ToolStrips.CommandButton.Center
        Me.bAlignCenter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bAlignCenter.Editor = Me.C1Editor1
        resources.ApplyResources(Me.bAlignCenter, "bAlignCenter")
        Me.bAlignCenter.Name = "bAlignCenter"
        Me.bAlignRight.Command = C1.Win.C1Editor.ToolStrips.CommandButton.Right
        Me.bAlignRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bAlignRight.Editor = Me.C1Editor1
        resources.ApplyResources(Me.bAlignRight, "bAlignRight")
        Me.bAlignRight.Name = "bAlignRight"
        Me.bBulletList.Command = C1.Win.C1Editor.ToolStrips.CommandButton.BulletedList
        Me.bBulletList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bBulletList.Editor = Me.C1Editor1
        resources.ApplyResources(Me.bBulletList, "bBulletList")
        Me.bBulletList.Name = "bBulletList"
        Me.bNumberedList.Command = C1.Win.C1Editor.ToolStrips.CommandButton.NumberedList
        Me.bNumberedList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bNumberedList.Editor = Me.C1Editor1
        resources.ApplyResources(Me.bNumberedList, "bNumberedList")
        Me.bNumberedList.Name = "bNumberedList"
        Me.bIndent.Command = C1.Win.C1Editor.ToolStrips.CommandButton.IncreaseIndent
        Me.bIndent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bIndent.Editor = Me.C1Editor1
        resources.ApplyResources(Me.bIndent, "bIndent")
        Me.bIndent.Name = "bIndent"
        Me.bDecreaseIndent.Command = C1.Win.C1Editor.ToolStrips.CommandButton.DecreaseIndent
        Me.bDecreaseIndent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bDecreaseIndent.Editor = Me.C1Editor1
        resources.ApplyResources(Me.bDecreaseIndent, "bDecreaseIndent")
        Me.bDecreaseIndent.Name = "bDecreaseIndent"
        Me.bInsertSymbol.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bInsertSymbol.Image = My.Resources.Resources.InsertSymbol_small
        resources.ApplyResources(Me.bInsertSymbol, "bInsertSymbol")
        Me.bInsertSymbol.Name = "bInsertSymbol"
        Me.bInsertFormula.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bInsertFormula.Image = My.Resources.Resources.InsertFormula_small
        resources.ApplyResources(Me.bInsertFormula, "bInsertFormula")
        Me.bInsertFormula.Name = "bInsertFormula"
        Me.bFitToContents.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bFitToContents.Image = My.Resources.Resources.FitToContent
        resources.ApplyResources(Me.bFitToContents, "bFitToContents")
        Me.bFitToContents.Name = "bFitToContents"
        Me.ddbReferences.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ddbReferences.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiElementReference, Me.tsmiSymbolReference, Me.tsmiHighlightReference, Me.tsmiRemoveReference, Me.tsmiOverviewReferences})
        resources.ApplyResources(Me.ddbReferences, "ddbReferences")
        Me.ddbReferences.Name = "ddbReferences"
        Me.tsmiElementReference.Name = "tsmiElementReference"
        resources.ApplyResources(Me.tsmiElementReference, "tsmiElementReference")
        Me.tsmiSymbolReference.Name = "tsmiSymbolReference"
        resources.ApplyResources(Me.tsmiSymbolReference, "tsmiSymbolReference")
        Me.tsmiHighlightReference.Name = "tsmiHighlightReference"
        resources.ApplyResources(Me.tsmiHighlightReference, "tsmiHighlightReference")
        Me.tsmiRemoveReference.Name = "tsmiRemoveReference"
        resources.ApplyResources(Me.tsmiRemoveReference, "tsmiRemoveReference")
        Me.tsmiOverviewReferences.Name = "tsmiOverviewReferences"
        resources.ApplyResources(Me.tsmiOverviewReferences, "tsmiOverviewReferences")
        Me.tsbReferTo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        resources.ApplyResources(Me.tsbReferTo, "tsbReferTo")
        Me.tsbReferTo.Name = "tsbReferTo"
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        resources.ApplyResources(Me.ToolStripSeparator9, "ToolStripSeparator9")
        Me.bTTSMute.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bTTSMute.Image = My.Resources.Resources.Mute_16
        resources.ApplyResources(Me.bTTSMute, "bTTSMute")
        Me.bTTSMute.Name = "bTTSMute"
        Me.bTTSAlternative.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bTTSAlternative.Image = My.Resources.Resources.TTSAlternative_16
        resources.ApplyResources(Me.bTTSAlternative, "bTTSAlternative")
        Me.bTTSAlternative.Name = "bTTSAlternative"
        Me.bTTSPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bTTSPause.Image = My.Resources.Resources.pause_16
        resources.ApplyResources(Me.bTTSPause, "bTTSPause")
        Me.bTTSPause.Name = "bTTSPause"
        Me.bTTSDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bTTSDelete.Image = My.Resources.Resources.TTSRemove_16
        resources.ApplyResources(Me.bTTSDelete, "bTTSDelete")
        Me.bTTSDelete.Name = "bTTSDelete"
        Me.bPopup.Command = C1.Win.C1Editor.ToolStrips.CommandButton.None
        Me.bPopup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bPopup.Editor = Nothing
        Me.bPopup.Image = My.Resources.Resources.add_icon_16
        resources.ApplyResources(Me.bPopup, "bPopup")
        Me.bPopup.Name = "bPopup"
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.C1Editor1, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.ToolStrip, 0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "XHtmlEditor"
        CType(Me.C1Editor1, System.ComponentModel.ISupportInitialize).EndInit
        Me.mainContextStrip.ResumeLayout(false)
        Me.ToolStrip.ResumeLayout(false)
        Me.ToolStrip.PerformLayout
        Me.TableLayoutPanel1.ResumeLayout(false)
        Me.TableLayoutPanel1.PerformLayout
        Me.ResumeLayout(false)

    End Sub
    Private WithEvents InsertTableC1EditorToolStripButton As C1.Win.C1Editor.ToolStrips.C1EditorToolStripButton
    Private WithEvents C1EditorToolStripButton1 As C1.Win.C1Editor.ToolStrips.C1EditorToolStripButton
    Private WithEvents CmdManager As Questify.Builder.UI.Commanding.CommandManager
    Private WithEvents bPasteFromWord As System.Windows.Forms.ToolStripButton
    Private WithEvents bPasteAsText As System.Windows.Forms.ToolStripButton
    Private WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents bImage As System.Windows.Forms.ToolStripButton
    Private WithEvents bMovie As System.Windows.Forms.ToolStripButton
    Private WithEvents bAudio As System.Windows.Forms.ToolStripButton
    Private WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents bTable As System.Windows.Forms.ToolStripButton
    Private WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents bLockEditImg As System.Windows.Forms.ToolStripButton
    Private WithEvents cbStyle As System.Windows.Forms.ToolStripComboBox
    Private WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents bConvertToRoman As System.Windows.Forms.ToolStripButton
    Private WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents ToolStrip As System.Windows.Forms.ToolStrip
    Private WithEvents bClearFormatting As C1.Win.C1Editor.ToolStrips.C1EditorToolStripButton
    Private WithEvents bAlignLeft As C1.Win.C1Editor.ToolStrips.C1EditorToolStripButton
    Private WithEvents bAlignCenter As C1.Win.C1Editor.ToolStrips.C1EditorToolStripButton
    Private WithEvents bAlignRight As C1.Win.C1Editor.ToolStrips.C1EditorToolStripButton
    Private WithEvents bBulletList As C1.Win.C1Editor.ToolStrips.C1EditorToolStripButton
    Private WithEvents bNumberedList As C1.Win.C1Editor.ToolStrips.C1EditorToolStripButton
    Private WithEvents bIndent As C1.Win.C1Editor.ToolStrips.C1EditorToolStripButton
    Private WithEvents bDecreaseIndent As C1.Win.C1Editor.ToolStrips.C1EditorToolStripButton
    Friend WithEvents bUnderLine As C1.Win.C1Editor.ToolStrips.C1EditorToolStripButton
    Friend WithEvents bBold As C1.Win.C1Editor.ToolStrips.C1EditorToolStripButton
    Friend WithEvents bItalic As C1.Win.C1Editor.ToolStrips.C1EditorToolStripButton
    Friend WithEvents bSuperScript As C1.Win.C1Editor.ToolStrips.C1EditorToolStripButton
    Friend WithEvents bSubscript As C1.Win.C1Editor.ToolStrips.C1EditorToolStripButton
    Friend WithEvents bStrikeThrough As C1.Win.C1Editor.ToolStrips.C1EditorToolStripButton
    Friend WithEvents bCut As C1.Win.C1Editor.ToolStrips.C1EditorToolStripButton
    Friend WithEvents bCopy As C1.Win.C1Editor.ToolStrips.C1EditorToolStripButton
    Friend WithEvents bUndo As C1.Win.C1Editor.ToolStrips.C1EditorToolStripButton
    Friend WithEvents bRedo As C1.Win.C1Editor.ToolStrips.C1EditorToolStripButton
    Friend WithEvents mainContextStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tsmInline As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents bInsertSymbol As System.Windows.Forms.ToolStripButton
    Private WithEvents bInsertFormula As System.Windows.Forms.ToolStripButton
    Private WithEvents bFitToContents As System.Windows.Forms.ToolStripButton
    Private WithEvents ddbReferences As System.Windows.Forms.ToolStripDropDownButton
    Private WithEvents tsmiElementReference As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents tsmiSymbolReference As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiHighlightReference As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents tsmiRemoveReference As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents tsmiOverviewReferences As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents tsbReferTo As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsmRemoveReference As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InlineToolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents RemoveReferenceToolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsmTable As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmTableProperties As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmInsertRowAbove As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmInsertRowBelow As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator10 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsmDeleteRow As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmInsertColumnLeft As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmInsertColumnRight As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator11 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsmMergeSelectedCells As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmSplitCell As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator12 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsmCellProperties As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmCellBorders As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmCellInnerMargins As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator13 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsmDeleteColumn As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents C1Editor1 As C1.Win.C1Editor.C1Editor
    Friend WithEvents TableToolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Private WithEvents bInsertControl As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsmEditFormula As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmRemoveTable As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmSplitCellVert As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents cbLanguage As System.Windows.Forms.ToolStripComboBox
    Private WithEvents bCustomInteraction As System.Windows.Forms.ToolStripButton
    Private WithEvents bPopup As C1.Win.C1Editor.ToolStrips.C1EditorToolStripButton
    Friend WithEvents ToolStripSeparator9 As ToolStripSeparator
    Friend WithEvents bTTSMute As ToolStripButton
    Friend WithEvents bTTSAlternative As ToolStripButton
    Friend WithEvents bTTSDelete As ToolStripButton
    Friend WithEvents bTTSPause As ToolStripSplitButton
End Class
