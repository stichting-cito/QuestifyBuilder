
Imports System.Windows.Forms
Imports C1.Win.C1Editor.UICustomization
Imports System.Xml
Imports Questify.Builder.UI

Public Class EditCellInnerMarginPropertiesDialog

    Private ReadOnly _cntHlp As New HtmlTableContentHelper
    Private ReadOnly _editor As XHtmlEditor
    Private ReadOnly _namespaceManager As XmlNamespaceManager
    Private _item As XHTMLCellItem

#Region "Constructor"

    Public Sub New(ByVal editor As XHtmlEditor, ByVal namespaceManager As XmlNamespaceManager)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _namespaceManager = namespaceManager
        _editor = editor
    End Sub

#End Region

    <Flags()> _
    Public Enum TargetBorder
        None = 0
        Top = 1
        Bottom = 2
        Left = 4
        Right = 8
    End Enum

    Public Enum TargetTableElement
        Cell
        Column
        Row
        Table
    End Enum

    Public ReadOnly Property ApplyInnerMarginTo() As TargetTableElement
        Get
            Return DirectCast(ComboBoxApplyTo.SelectedIndex, TargetTableElement)
        End Get
    End Property

    <CLSCompliant(False)> _
    Public ReadOnly Property MarginTop() As UInteger
        Get
            Dim margin As UInteger = 0
            UInteger.TryParse(MaskedTextBoxMarginTop.Text, margin)
            Return margin
        End Get
    End Property

    <CLSCompliant(False)> _
    Public ReadOnly Property MarginBottom() As UInteger
        Get
            Dim margin As UInteger = 0
            UInteger.TryParse(MaskedTextBoxMarginBottom.Text, margin)
            Return margin
        End Get
    End Property

    <CLSCompliant(False)> _
    Public ReadOnly Property MarginLeft() As UInteger
        Get
            Dim margin As UInteger = 0
            UInteger.TryParse(MaskedTextBoxMarginLeft.Text, margin)
            Return margin
        End Get
    End Property

    <CLSCompliant(False)> _
    Public ReadOnly Property MarginRight() As UInteger
        Get
            Dim margin As UInteger = 0
            UInteger.TryParse(MaskedTextBoxMarginRight.Text, margin)
            Return margin
        End Get
    End Property

    ''' <summary>
    ''' Extracts the style value.
    ''' </summary>
    ''' <param name="elementName">Name of the element.</param>
    ''' <param name="entireStyleString">The entire style string.</param>
    Private Shared Function ExtractStyleValue(ByVal elementName As String, ByRef entireStyleString As String) As String
        Dim styleValue As String = String.Empty

        If entireStyleString Is Nothing Then
            Return String.Empty
        End If

        Dim keyWordIndex As Integer

        keyWordIndex = entireStyleString.IndexOf(elementName)

        If keyWordIndex > -1 Then
            Dim valueOffset As Integer = keyWordIndex + elementName.Length
            Dim semiColonOffset As Integer = entireStyleString.IndexOf(";", valueOffset)

            If semiColonOffset = -1 Then
                semiColonOffset = entireStyleString.Length
            End If

            styleValue = entireStyleString.Substring(valueOffset, semiColonOffset - valueOffset)

            Dim newEntireStyleString As String = String.Empty

            If keyWordIndex > 0 Then
                newEntireStyleString = entireStyleString.Substring(0, keyWordIndex)
            End If

            If semiColonOffset < entireStyleString.Length Then
                newEntireStyleString += entireStyleString.Substring(semiColonOffset + 1)
            End If

            entireStyleString = newEntireStyleString
        End If

        Return styleValue
    End Function

    ''' <summary>
    ''' Constructs the cell style.
    ''' </summary>
    ''' <param name="currentCellStyle">The current cell style.</param>
    ''' <param name="paddingTop">The padding top.</param>
    ''' <param name="paddingBottom">The padding bottom.</param>
    ''' <param name="paddingLeft">The padding left.</param>
    ''' <param name="paddingRight">The padding right.</param>
    <CLSCompliant(False)> _
    Private Function ConstructCellStyle(
            ByVal currentCellStyle As String, 
            ByVal paddingTop As UInteger, 
            ByVal paddingBottom As UInteger, 
            ByVal paddingLeft As UInteger, 
            ByVal paddingRight As UInteger) As String

        Dim newStyle As String = String.Empty

        ' Start by extracting and thus removing the padding related elements from the
        ' current cell style.
        If Not String.IsNullOrEmpty(currentCellStyle) Then
            ExtractStyleValue("PADDING-TOP:", currentCellStyle)
            ExtractStyleValue("PADDING-BOTTOM:", currentCellStyle)
            ExtractStyleValue("PADDING-LEFT:", currentCellStyle)
            ExtractStyleValue("PADDING-RIGHT:", currentCellStyle)

            newStyle = currentCellStyle.Trim()
        End If

        If Not String.IsNullOrEmpty(newStyle) Then
            If Not newStyle.EndsWith(";") Then
                newStyle += ";"
            End If
        End If

        newStyle += String.Format(" PADDING-TOP: {0}px;", paddingTop)
        newStyle += String.Format(" PADDING-BOTTOM: {0}px;", paddingBottom)
        newStyle += String.Format(" PADDING-LEFT: {0}px;", paddingLeft)
        newStyle += String.Format(" PADDING-RIGHT: {0}px;", paddingRight)

        Return newStyle
    End Function

    ''' <summary>
    ''' Parses the border attributes and sets the state of related UI controls accordingly.
    ''' </summary>
    ''' <param name="sourceBorder">The source border.</param>
    ''' <param name="paddingString">The cell padding string.</param>
    Private Sub ParsePaddingAttributes(ByVal sourceBorder As TargetBorder, ByVal paddingString As String)
        Dim currentPaddingAttribs As String()

        currentPaddingAttribs = paddingString.Split(" "c)

        Dim numberOfPixels As Integer = 0
        If currentPaddingAttribs.Length > 1 Then
            Dim numberOfPixelsString As String = currentPaddingAttribs(1).Replace("px", String.Empty)

            Int32.TryParse(numberOfPixelsString, numberOfPixels)
        End If

        ' Set the current margin values 
        Select Case sourceBorder
            Case TargetBorder.Top
                MaskedTextBoxMarginTop.Text = numberOfPixels.ToString()
            Case TargetBorder.Bottom
                MaskedTextBoxMarginBottom.Text = numberOfPixels.ToString()
            Case TargetBorder.Left
                MaskedTextBoxMarginLeft.Text = numberOfPixels.ToString()
            Case TargetBorder.Right
                MaskedTextBoxMarginRight.Text = numberOfPixels.ToString()
        End Select

    End Sub

    Private Sub SetCurrentCellStyle(ByVal cellStyle As String)
        'Sample cell style valie = "BORDER-RIGHT: #000000 1px solid; PADDING-RIGHT: 3px; BORDER-TOP: #000000 1px solid; PADDING-LEFT: 3px; PADDING-BOTTOM: 3px; VERTICAL-ALIGN: middle; BORDER-LEFT: #000000 1px solid; WIDTH: 44px; PADDING-TOP: 3px; BORDER-BOTTOM: #000000 1px solid; HEIGHT: 55px; TEXT-ALIGN: right"
        Dim currentPadding As String

        currentPadding = ExtractStyleValue("PADDING-TOP:", cellStyle)
        ParsePaddingAttributes(TargetBorder.Top, currentPadding)

        currentPadding = ExtractStyleValue("PADDING-BOTTOM:", cellStyle)
        ParsePaddingAttributes(TargetBorder.Bottom, currentPadding)

        currentPadding = ExtractStyleValue("PADDING-RIGHT:", cellStyle)
        ParsePaddingAttributes(TargetBorder.Right, currentPadding)

        currentPadding = ExtractStyleValue("PADDING-LEFT:", cellStyle)
        ParsePaddingAttributes(TargetBorder.Left, currentPadding)
    End Sub

    ''' <summary>
    ''' Sets the table inner margin style.
    ''' </summary>
    ''' <param name="table">The table.</param>
    ''' <param name="fromRow">From row.</param>
    ''' <param name="toRow">To row.</param>
    ''' <param name="fromColumn">From column.</param>
    ''' <param name="toColumn">To column.</param>
    ''' <param name="paddingTop">The padding top.</param>
    ''' <param name="paddingBottom">The padding bottom.</param>
    ''' <param name="paddingLeft">The padding left.</param>
    ''' <param name="paddingRight">The padding right.</param>
    Private Sub SetTableInnerMarginStyle(
        ByVal table As XmlElement, 
        ByVal fromRow As Integer, 
        ByVal toRow As Integer, 
        ByVal fromColumn As Integer, 
        ByVal toColumn As Integer, 
        ByVal paddingTop As UInteger, 
        ByVal paddingBottom As UInteger, 
        ByVal paddingLeft As UInteger, 
        ByVal paddingRight As UInteger)

        For rowIndex As Integer = fromRow To toRow
            If rowIndex < table.SelectNodes("*/def:tr", _namespaceManager).Count Then
                For colIndex As Integer = fromColumn To toColumn
                    Dim row As XmlElement = _cntHlp.GetRow(_item.Node)

                    If colIndex < row.SelectNodes("def:td", _namespaceManager).Count Then
                        Dim cell As XmlElement = DirectCast(table.SelectSingleNode(String.Format("*/def:tr[{0}]/def:td[{1}]", rowIndex + 1, colIndex + 1), _namespaceManager), XmlElement)

                        If cell IsNot Nothing Then 'Cell can be nothing because of row/colspan. 
                            Dim styleToSet As String = ConstructCellStyle(_item.Style, paddingTop, paddingBottom, paddingLeft, paddingRight)

                            ApplyStyle(cell, styleToSet, _cntHlp.GetWidthOfCell(cell), _cntHlp.GetHeightOfCell(cell), _cntHlp.GetVerticalAlignmentFromCell(cell), _cntHlp.GetHorizontalAlignmentFromCell(cell))
                        End If
                    End If
                Next colIndex
            End If
        Next rowIndex
    End Sub

    ''' <summary>
    ''' Applies the style.
    ''' </summary>
    ''' <param name="cell">The cell.</param>
    ''' <param name="cssStyle">The CSS style.</param>
    ''' <param name="width">The width.</param>
    ''' <param name="height">The height.</param>
    ''' <param name="vAlign">The v align.</param>
    ''' <param name="align">The align.</param>
    Private Sub ApplyStyle(
             ByVal cell As XmlElement, 
             ByVal cssStyle As String, 
             ByVal width As Web.UI.WebControls.Unit, 
             ByVal height As Web.UI.WebControls.Unit, 
             ByVal vAlign As Web.UI.WebControls.VerticalAlign, 
             ByVal align As Web.UI.WebControls.HorizontalAlign)

        cell.SetAttribute("style", cssStyle)
        If align <> Web.UI.WebControls.HorizontalAlign.NotSet Then
            cell.SetAttribute("align", [Enum].GetName(GetType(Web.UI.WebControls.HorizontalAlign), align).ToLower())
        End If
        If vAlign <> Web.UI.WebControls.HorizontalAlign.NotSet Then
            cell.SetAttribute("valign", [Enum].GetName(GetType(Web.UI.WebControls.VerticalAlign), vAlign).ToLower())
        End If
        If Not width.IsEmpty Then
            cell.SetAttribute("width", CStr(width.Value))
        End If
        If Not height.IsEmpty Then
            cell.SetAttribute("height", CStr(height.Value))
        End If
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Try
            _editor.BeginTransaction()
            PerformSetTableInnerMarginStyle()
            _editor.CommitTransaction()
        Catch ex As Exception
            MessageBox.Show(String.Format("Something went wrong : {0}", ex.Message))
            _editor.RollbackTransaction()
        End Try

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    ''' <summary>
    ''' Performs the set table inner margin style.
    ''' </summary>
    Private Sub PerformSetTableInnerMarginStyle()
        Dim fromRow As Integer = -1
        Dim toRow As Integer = -1
        Dim fromColumn As Integer = -1
        Dim toColumn As Integer = -1

        Select Case ApplyInnerMarginTo
            Case TargetTableElement.Cell
                fromColumn = _cntHlp.FindColumnIndex(_cntHlp.GetCell(_item.Node), _namespaceManager)
                toColumn = fromColumn
                fromRow = _cntHlp.FindRowIndex(_cntHlp.GetCell(_item.Node), _namespaceManager)
                toRow = fromRow
            Case TargetTableElement.Column
                fromColumn = _cntHlp.FindColumnIndex(_cntHlp.GetCell(_item.Node), _namespaceManager)

                If fromColumn > -1 Then
                    toColumn = fromColumn
                    fromRow = 0
                    toRow = _cntHlp.GetTable(_item.Node).SelectNodes("*/def:tr", _namespaceManager).Count - 1
                End If
            Case TargetTableElement.Row
                fromRow = _cntHlp.FindRowIndex(_cntHlp.GetCell(_item.Node), _namespaceManager)

                If fromRow > -1 Then
                    toRow = fromRow
                    fromColumn = 0
                    toColumn = _cntHlp.GetTable(_item.Node).SelectNodes(String.Format("*/def:tr[{0}]/def:td", fromRow + 1), _namespaceManager).Count - 1
                End If
            Case TargetTableElement.Table
                fromRow = 0
                toRow = _cntHlp.GetTable(_item.Node).SelectNodes("*/def:tr", _namespaceManager).Count - 1
                fromColumn = 0
                toColumn = _cntHlp.GetMaxColumnCount(_cntHlp.GetTable(_item.Node), _namespaceManager) - 1
        End Select

        If fromColumn > -1 AndAlso toColumn > -1 AndAlso fromRow > -1 AndAlso toRow > -1 Then
            SetTableInnerMarginStyle(_cntHlp.GetTable(_item.Node), fromRow, toRow, fromColumn, toColumn, MarginTop, MarginBottom, MarginLeft, MarginRight)
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub EditCellInnerMarginPropertiesDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ComboBoxApplyTo.SelectedIndex = 0
    End Sub

#Region "ICellItemDialog Members"

    ''' <summary>
    ''' Binds data from the item to GUI controls on the form.
    ''' Data can be bound either using the <see cref="Control.DataBindings"/> collection or any other way allowing 
    ''' to read data from the item and write it back.
    ''' </summary>
    ''' <param name="item">The item to be bound to the GUI controls.</param>
    Private Sub ITableItemDialog_BindData(ByVal item As XHTMLCellItem) Implements ICellItemDialog.BindData
        _item = item
        SetCurrentCellStyle(_item.Style)
    End Sub

    ''' <summary>
    ''' Shows the form with the specified owner to the user.
    ''' </summary>
    ''' <param name="owner">Any object that implements <see cref="System.Windows.Forms.IWin32Window"/> and represents 
    ''' the top-level window that will own this form.</param>
    ''' <returns>True if the form was displayed successfully and the item was changed.</returns>
    Private Function ITableItemDialog_Show(ByVal owner As IWin32Window) As Boolean Implements ICellItemDialog.Show
        Return ShowDialog(owner) = DialogResult.OK
    End Function

#End Region
End Class
