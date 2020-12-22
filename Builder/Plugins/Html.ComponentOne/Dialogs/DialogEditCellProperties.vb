
Imports System.Web.UI.WebControls
Imports System.Text
Imports System.Windows.Forms
Imports C1.Win.C1Editor.UICustomization
Imports System.Xml
Imports Questify.Builder.Logic
Imports Questify.Builder.UI

Public Class DialogEditCellProperties
    Implements ICellItemDialog

    Private _cellHorizontalAlign As HorizontalAlign
    Private _cellStyle As String
    Private _cellVerticalAlign As VerticalAlign
    Private _item As XHTMLCellItem
    Private ReadOnly _editor As XHtmlEditor
    Private ReadOnly _namespaceManager As XmlNamespaceManager
    Private ReadOnly _cntHlp As New HtmlTableContentHelper

    Public Sub New(ByVal editor As XHtmlEditor, ByVal namespaceManager As XmlNamespaceManager)

        InitializeComponent()

        _editor = editor
        _namespaceManager = namespaceManager
    End Sub


    Public ReadOnly Property ApplyCellPropertiesTo() As String
        Get
            Dim myResMan As New System.ComponentModel.ComponentResourceManager(GetType(DialogEditCellProperties))
            Dim origCulture As New System.Globalization.CultureInfo("en")
            Dim itemToGet As String

            If ComboBoxApplyTo.SelectedIndex = 0 Then
                itemToGet = String.Format("{0}.Items", ComboBoxApplyTo.Name)
            Else
                itemToGet = String.Format("{0}.Items{1}", ComboBoxApplyTo.Name, ComboBoxApplyTo.SelectedIndex)
            End If

            Return myResMan.GetString(itemToGet, origCulture)
        End Get
    End Property

    Public Property CellHeight() As System.Web.UI.WebControls.Unit
        Get
            If RadioButtonFixedHeight.Checked Then
                Dim TryInt As Integer

                If Integer.TryParse(TextBoxHeight.Text, TryInt) Then
                    Return System.Web.UI.WebControls.Unit.Pixel(TryInt)
                End If
            End If

            Return System.Web.UI.WebControls.Unit.Empty
        End Get

        Set(ByVal value As System.Web.UI.WebControls.Unit)
            If value.IsEmpty Then
                RadioButtonAutoAdjustHeight.Checked = True
            Else
                If value.Type = Web.UI.WebControls.UnitType.Pixel Then
                    RadioButtonFixedHeight.Checked = True
                    TextBoxHeight.Text = value.Value.ToString()
                End If
            End If
        End Set
    End Property

    Public Property CellHorizontalAlignment() As System.Web.UI.WebControls.HorizontalAlign
        Get
            Return GetHorizontalAlignmentFromString(ComboBoxAlignmentHorizontal.SelectedItem.ToString())
        End Get

        Set(ByVal value As System.Web.UI.WebControls.HorizontalAlign)
            _cellHorizontalAlign = value
        End Set
    End Property

    Public Property CellStyle() As String
        Get
            Return ConstructCellStyle()
        End Get

        Set(ByVal value As String)
            _cellStyle = value
        End Set
    End Property

    Public Property CellVerticalAlignment() As System.Web.UI.WebControls.VerticalAlign
        Get
            Return GetVerticalAlignmentFromString(ComboBoxAlignmentVertical.SelectedItem.ToString())
        End Get

        Set(ByVal value As System.Web.UI.WebControls.VerticalAlign)
            _cellVerticalAlign = value
        End Set
    End Property

    Public Property CellWidth() As System.Web.UI.WebControls.Unit
        Get
            If RadioButtonFixedWidth.Checked Then
                Dim TryInt As Integer

                If Integer.TryParse(TextBoxWidth.Text, TryInt) Then
                    If ComboBoxWidthUnit.SelectedIndex = 1 Then
                        Return System.Web.UI.WebControls.Unit.Pixel(TryInt)
                    Else
                        Return System.Web.UI.WebControls.Unit.Percentage(TryInt)
                    End If
                End If
            End If

            Return System.Web.UI.WebControls.Unit.Empty
        End Get

        Set(ByVal value As System.Web.UI.WebControls.Unit)
            If value.IsEmpty Then
                RadioButtonAutoAdjustWidth.Checked = True
                ComboBoxWidthUnit.SelectedIndex = 0
            Else
                RadioButtonFixedWidth.Checked = True

                If value.Type = Web.UI.WebControls.UnitType.Percentage Then
                    ComboBoxWidthUnit.SelectedIndex = 0
                Else
                    ComboBoxWidthUnit.SelectedIndex = 1
                End If

                TextBoxWidth.Text = value.Value.ToString()
            End If
        End Set
    End Property



    Private Sub SetCellStyleAndProperties(ByVal cellStyle As String, ByVal horizontalAlignment As HorizontalAlign, ByVal verticalAlignment As VerticalAlign, ByVal cellWidth As Unit, ByVal cellHeight As Unit)
        Me.CellHorizontalAlignment = horizontalAlignment
        Me.CellVerticalAlignment = verticalAlignment
        Me.CellWidth = cellWidth
        Me.CellHeight = cellHeight

        Dim alignFromCellStyle As String = String.Empty
        Dim valignFromCellStyle As String = String.Empty
        Dim widthFromCellStyle As String = String.Empty
        Dim heightFromCellStyle As String = String.Empty

        Dim styleParts() As String = cellStyle.Split(";".ToCharArray())
        For index As Integer = 0 To styleParts.GetUpperBound(0)
            Dim stylePart As String = styleParts(index)
            If stylePart.Trim().ToLower().StartsWith("text-align") Then
                alignFromCellStyle = stylePart.Substring(stylePart.IndexOf(":") + 1).Trim()
                styleParts(index) = vbNullString
            ElseIf stylePart.Trim().ToLower().StartsWith("vertical-align") Then
                valignFromCellStyle = stylePart.Substring(stylePart.IndexOf(":") + 1).Trim()
                styleParts(index) = vbNullString
            ElseIf stylePart.Trim().ToLower().StartsWith("width") Then
                widthFromCellStyle = stylePart.Substring(stylePart.IndexOf(":") + 1).Trim()
                styleParts(index) = vbNullString
            ElseIf stylePart.Trim().ToLower().StartsWith("height") Then
                heightFromCellStyle = stylePart.Substring(stylePart.IndexOf(":") + 1).Trim()
                styleParts(index) = vbNullString
            End If
            If Not (String.IsNullOrEmpty(alignFromCellStyle) OrElse String.IsNullOrEmpty(valignFromCellStyle) OrElse
             String.IsNullOrEmpty(widthFromCellStyle) OrElse String.IsNullOrEmpty(heightFromCellStyle)) Then
                Exit For
            End If
        Next
        Dim styleBuilder As StringBuilder = New StringBuilder()
        For Each stylePart As String In styleParts
            If Not String.IsNullOrEmpty(stylePart) Then
                styleBuilder.Append(stylePart)
                styleBuilder.Append(";")
            End If
        Next
        Me.CellStyle = styleBuilder.ToString()
        If Not String.IsNullOrEmpty(alignFromCellStyle) Then
            CellHorizontalAlignment = GetHorizontalAlignmentFromString(alignFromCellStyle)
        End If
        If Not String.IsNullOrEmpty(valignFromCellStyle) Then
            CellVerticalAlignment = GetVerticalAlignmentFromString(valignFromCellStyle)
        End If
        If Not String.IsNullOrEmpty(widthFromCellStyle) Then
            Dim cellWidthUnit As Unit = GetUnitFromString(widthFromCellStyle)
            If cellWidthUnit <> Unit.Empty Then
                Me.CellWidth = cellWidthUnit
            End If
        End If
        If Not String.IsNullOrEmpty(heightFromCellStyle) Then
            Dim cellHeightUnit As Unit = GetUnitFromString(heightFromCellStyle)
            If cellHeightUnit <> Unit.Empty Then
                Me.CellHeight = cellHeightUnit
            End If
        End If
    End Sub

    Private Function GetHorizontalAlignmentFromString(ByVal horizontalAlignment As String) As HorizontalAlign
        Dim tconverter As System.ComponentModel.TypeConverter = System.ComponentModel.TypeDescriptor.GetConverter(GetType(System.Web.UI.WebControls.HorizontalAlign))
        Return DirectCast(tconverter.ConvertFromString(Nothing, System.Threading.Thread.CurrentThread.CurrentUICulture, horizontalAlignment), System.Web.UI.WebControls.HorizontalAlign)
    End Function

    Private Function GetVerticalAlignmentFromString(ByVal verticalAlignment As String) As VerticalAlign
        Dim tconverter As System.ComponentModel.TypeConverter = System.ComponentModel.TypeDescriptor.GetConverter(GetType(System.Web.UI.WebControls.VerticalAlign))
        Return DirectCast(tconverter.ConvertFromString(Nothing, System.Threading.Thread.CurrentThread.CurrentUICulture, verticalAlignment), System.Web.UI.WebControls.VerticalAlign)
    End Function

    Private Function GetUnitFromString(ByVal unitString As String) As Unit
        If Not String.IsNullOrEmpty(unitString) Then
            Dim value As Integer
            If unitString.Trim().EndsWith("%") Then
                If Integer.TryParse(unitString.Replace("%"c, vbNullChar), value) Then
                    Return Unit.Percentage(value)
                End If
            ElseIf unitString.Trim().ToLower().EndsWith("px") Then
                If Integer.TryParse(unitString.ToLower().Replace("px", vbNullString), value) Then
                    Return Unit.Pixel(value)
                End If
            End If
        End If
        Return Unit.Empty
    End Function

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Function ConstructCellStyle() As String
        Dim Style As String

        Style = _cellStyle

        Return Style
    End Function

    Private Sub DialogEditTableProperties_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim element As XmlElement = DirectCast(_item.Node, XmlElement)
        SetCellStyleAndProperties(_item.Style, _cntHlp.GetHorizontalAlignmentFromCell(element), _cntHlp.GetVerticalAlignmentFromCell(element), _cntHlp.GetWidthOfCell(element), _cntHlp.GetHeightOfCell(element))

        ComboBoxApplyTo.SelectedIndex = 0

        InitComboboxWithEnumValues(GetType(System.Web.UI.WebControls.HorizontalAlign), _cellHorizontalAlign, ComboBoxAlignmentHorizontal)
        InitComboboxWithEnumValues(GetType(System.Web.UI.WebControls.VerticalAlign), _cellVerticalAlign, ComboBoxAlignmentVertical)
    End Sub

    Private Sub InitComboboxWithEnumValues(ByVal etype As System.Type, ByVal evalue As Object, ByVal combo As ComboBox)
        System.ComponentModel.TypeDescriptor.AddAttributes(etype, New System.ComponentModel.TypeConverterAttribute(GetType(LocalEnumLocalizer)))

        For Each kv As KeyValuePair(Of System.Enum, String) In LocalEnumLocalizer.GetValues(etype)
            If Not kv.Value.Equals("Justify") Then
                combo.Items.Add(kv.Value)
            End If
        Next

        Dim tconverter As System.ComponentModel.TypeConverter = System.ComponentModel.TypeDescriptor.GetConverter(etype)
        Dim SelectedText As String = tconverter.ConvertToString(Nothing, Threading.Thread.CurrentThread.CurrentUICulture, evalue)
        Dim SelectedIndex As Integer = 0

        For Each cbItem As Object In combo.Items
            Dim stringCBItem As String = DirectCast(cbItem, String)

            If stringCBItem.Equals(SelectedText) Then
                combo.SelectedIndex = SelectedIndex
                combo.SelectedItem = cbItem
                Exit For
            End If

            SelectedIndex += 1
        Next
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Try
            _editor.BeginTransaction()

            PerformApplyCellPropertiesTo()
            _editor.CommitTransaction()

            Dim htmlHelper As New HtmlContentHelper
            If htmlHelper.RemoveColGroupFromTables(_item.Node, _namespaceManager) Then
                _editor.LoadXml(_editor.Document.OuterXml)
            End If
        Catch ex As Exception
            _editor.RollbackTransaction()
        End Try

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub PerformApplyCellPropertiesTo()
        Dim element As XmlElement = DirectCast(_item.Node, XmlElement)

        Select Case ApplyCellPropertiesTo
            Case "Row"
                ApplyStyleToRow(element, CellHorizontalAlignment, CellVerticalAlignment, CellWidth, CellHeight)
            Case "Column"
                ApplyStyleToColumn(element, CellHorizontalAlignment, CellVerticalAlignment, CellWidth, CellHeight)
            Case "Table"
                ApplyStyleToTable(element, CellHorizontalAlignment, CellVerticalAlignment, CellWidth, CellHeight)
            Case Else
                ApplyStyle(element, CellHorizontalAlignment, CellVerticalAlignment, CellWidth, CellHeight)
        End Select

        If Not CellWidth.IsEmpty Then
            Dim table As XmlElement = _cntHlp.GetTable(element)
            If table Is Nothing Then Return

            Dim rows As XmlNodeList = table.SelectNodes("*/def:tr", _namespaceManager)
            Dim nrOfColumns = 0
            For Each row As XmlElement In rows
                Dim columns As XmlNodeList = row.SelectNodes("def:td", _namespaceManager)
                nrOfColumns = Math.Max(nrOfColumns, columns.Count)
            Next

            If nrOfColumns = 1 Then
                ModifyTableWidth(table, CellWidth)
            End If
        End If
    End Sub

    Private Sub ModifyTableWidth(ByVal table As XmlElement, ByVal width As Unit)
        Dim styleAttribute = table.Attributes.GetNamedItem("style")
        If styleAttribute Is Nothing Then
            table.SetAttribute("style", "width: 0px")
            styleAttribute = table.Attributes.GetNamedItem("style")
        End If

        Dim styleParts = styleAttribute.Value.Split(New String() {";"}, StringSplitOptions.RemoveEmptyEntries)
        Dim widthPartFound = False
        For i As Integer = 0 To styleParts.Length - 1 Step 1
            Dim part = styleParts(i)
            If part.Trim.ToLower.StartsWith("width") Then
                styleParts(i) = String.Format("width: {0}{1}", width.Value, If(ComboBoxWidthUnit.SelectedIndex = 0, "%", "px"))
                widthPartFound = True
                Exit For
            End If
        Next

        Dim styleString = String.Join("; ", styleParts)
        styleString += ";"

        If Not widthPartFound Then
            styleString += String.Format(" width: {0}{1}", width.Value, If(ComboBoxWidthUnit.SelectedIndex = 0, "%", "px"))
        End If

        table.SetAttribute("style", styleString)
        table.SetAttribute("width", CStr(width.Value))
    End Sub

    Private Sub ApplyStyleToTable(ByVal cell As XmlElement, ByVal align As HorizontalAlign, ByVal vAlign As VerticalAlign, ByVal width As Unit, ByVal height As Unit)
        Dim table As XmlElement = _cntHlp.GetTable(cell)
        Dim rows As XmlNodeList = table.SelectNodes("*/def:tr", _namespaceManager)

        For Each row As XmlElement In rows
            ApplyStyle(row, align, vAlign, width, height)

            Dim columns As XmlNodeList = row.SelectNodes("def:td", _namespaceManager)

            For Each column As XmlElement In columns
                ApplyStyle(column, align, vAlign, width, height)
            Next
        Next
    End Sub

    Private Sub ApplyStyleToRow(ByVal cell As XmlElement, ByVal align As HorizontalAlign, ByVal vAlign As VerticalAlign, ByVal width As Unit, ByVal height As Unit)
        Dim row As XmlElement = _cntHlp.GetRow(cell)
        ApplyStyle(row, align, vAlign, width, height)
    End Sub

    Private Sub ApplyStyleToColumn(ByVal cell As XmlElement, ByVal align As HorizontalAlign, ByVal vAlign As VerticalAlign, ByVal width As Unit, ByVal height As Unit)
        Dim row As XmlElement = _cntHlp.GetRow(cell)
        Dim columns As XmlNodeList = row.SelectNodes("def:td", _namespaceManager)
        Dim columnIndex As Integer = 0

        For i As Integer = 0 To columns.Count - 1
            If columns(i).Equals(cell) Then
                columnIndex = i
                Exit For
            End If
        Next

        Dim table As XmlElement = _cntHlp.GetTable(cell)
        Dim otherCellsOfColumn As XmlNodeList = table.SelectNodes(String.Format("def:*/def:tr/def:td[{0}]", columnIndex + 1), _namespaceManager)

        For Each column As XmlElement In otherCellsOfColumn
            ApplyStyle(column, align, vAlign, width, height)
        Next
    End Sub

    Private Sub ApplyStyle(ByVal element As XmlElement, ByVal align As HorizontalAlign, ByVal vAlign As VerticalAlign, ByVal width As Unit, ByVal height As Unit)
        _item.Style = CellStyle
        If align = HorizontalAlign.NotSet Then element.Attributes.RemoveNamedItem("align") Else element.SetAttribute("align", [Enum].GetName(GetType(HorizontalAlign), align).ToLower())
        If vAlign = HorizontalAlign.NotSet Then element.Attributes.RemoveNamedItem("valign") Else element.SetAttribute("valign", [Enum].GetName(GetType(VerticalAlign), vAlign).ToLower())
        If width.IsEmpty Then element.Attributes.RemoveNamedItem("width") Else element.SetAttribute("width", CStr(width.Value))
        If height.IsEmpty Then element.Attributes.RemoveNamedItem("height") Else element.SetAttribute("height", CStr(height.Value))
    End Sub

    Private Sub RadioButtonFixedHeight_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonFixedHeight.CheckedChanged
        TextBoxHeight.Enabled = RadioButtonFixedHeight.Checked
    End Sub

    Private Sub RadioButtonFixedWidth_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonFixedWidth.CheckedChanged
        TextBoxWidth.Enabled = RadioButtonFixedWidth.Checked
        ComboBoxWidthUnit.Enabled = RadioButtonFixedWidth.Checked
    End Sub



    Private Class LocalEnumLocalizer
        Inherits Cito.Tester.Common.ResourceEnumConverter

        Public Sub New(ByVal sType As System.Type)
            MyBase.New(sType, New System.ComponentModel.ComponentResourceManager(GetType(DialogEditCellProperties)))
        End Sub

    End Class



    Private Sub ITableItemDialog_BindData(ByVal item As XHTMLCellItem) Implements ICellItemDialog.BindData
        _item = item
    End Sub

    Private Function ITableItemDialog_Show(ByVal owner As IWin32Window) As Boolean Implements ICellItemDialog.Show
        Return ShowDialog(owner) = DialogResult.OK
    End Function


End Class