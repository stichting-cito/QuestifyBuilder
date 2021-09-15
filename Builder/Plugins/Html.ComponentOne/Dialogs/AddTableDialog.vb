
Imports System.Drawing
Imports System.Windows.Forms
Imports C1.Win.C1Editor.UICustomization

Public Class AddTableDialog

    Private _selectedRow As Integer
    Private _selectedColumn As Integer
    Private _item As XHTMLTableItem
    Private _editor As XHtmlEditor

    Public Sub New(ByVal parentLocation As Point, ByVal editor As XHtmlEditor)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Location = parentLocation
        _editor = editor
    End Sub

   Public ReadOnly Property TableRows() As Integer
        Get
            Return _selectedRow + 1
        End Get
    End Property

    Public ReadOnly Property TableColumns() As Integer
        Get
            Return _selectedColumn + 1
        End Get
    End Property

    Private Sub RenderSelectedCells(ByVal selectedRow As Integer, ByVal selectedColumn As Integer)
        Dim SetCellBackColorTo As Color

        For r As Integer = 0 To 9
            For c As Integer = 0 To 9

                If r <= selectedRow AndAlso c <= selectedColumn Then
                    SetCellBackColorTo = Color.Blue
                Else
                    SetCellBackColorTo = Color.White
                End If
                Dim ControlAtPosition As Control = TableLayoutPanelSelectCells.GetControlFromPosition(c, r)

                If TypeOf (ControlAtPosition) Is Panel Then
                    Dim PanelAtCell As Panel = DirectCast(ControlAtPosition, Panel)

                    PanelAtCell.BackColor = SetCellBackColorTo
                End If
            Next
        Next

        _selectedRow = selectedRow
        _selectedColumn = selectedColumn

        If selectedRow >= 0 AndAlso selectedColumn >= 0 Then
            LabelTableDimensions.Text = String.Format("{0} x {1} table", selectedRow + 1, selectedColumn + 1)
        Else
            LabelTableDimensions.Text = String.Empty
        End If
    End Sub

    Private Sub CellPanel_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim CellPosition As TableLayoutPanelCellPosition

        CellPosition = TableLayoutPanelSelectCells.GetCellPosition(DirectCast(sender, Control))

        RenderSelectedCells(CellPosition.Row, CellPosition.Column)
    End Sub

    Private Sub CellPanel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        _item.RowCount = TableRows
        _item.ColumnCount = TableColumns

        Me.DialogResult = System.Windows.Forms.DialogResult.OK

        Me.Close()
    End Sub

    ''' <summary>
    ''' Upon form load the table with panels for defining the table dimensions is constructed.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub AddTableDialog_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Start of by clearing the layout panel control because the child controls stay alive
        'between subsequent calls.
        TableLayoutPanelSelectCells.Controls.Clear()

        For i As Integer = 0 To 9
            For j As Integer = 0 To 9
                Dim CellPanel As New Panel
                CellPanel.Dock = DockStyle.None
                CellPanel.BackColor = Color.White
                CellPanel.Width = 22
                CellPanel.Height = 16
                CellPanel.Margin = New Padding(1)

                AddHandler CellPanel.MouseEnter, AddressOf CellPanel_MouseEnter
                AddHandler CellPanel.Click, AddressOf CellPanel_Click

                TableLayoutPanelSelectCells.Controls.Add(CellPanel, i, j)
            Next
        Next
    End Sub

    ''' <summary>
    ''' If the user enters the area above or left from the panels that define the table dimensions the 
    ''' panels are rendered to reflect the situation of no selection.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub AddTableDialog_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.MouseEnter
        Dim ClientCoord As Drawing.Point = Me.PointToClient(Control.MousePosition)

        If ClientCoord.Y < TableLayoutPanelSelectCells.Top OrElse ClientCoord.X < TableLayoutPanelSelectCells.Left Then
            RenderSelectedCells(-1, -1)
        End If
    End Sub

#Region "ITableItemDialog Members"

    ''' <summary>
    ''' Binds data from the item to GUI controls on the form.
    ''' Data can be bound either using the <see cref="Control.DataBindings"/> collection or any other way allowing 
    ''' to read data from the item and write it back.
    ''' </summary>
    ''' <param name="item">The item to be bound to the GUI controls.</param>
    Private Sub ITableItemDialog_BindData(ByVal item As XHTMLTableItem) Implements ITableItemDialog.BindData
        _item = item
        _item.Border = 0
        _item.Style = String.Format("BORDER-COLLAPSE: collapse; ")
    End Sub

    ''' <summary>
    ''' Shows the form with the specified owner to the user.
    ''' </summary>
    ''' <param name="owner">Any object that implements <see cref="System.Windows.Forms.IWin32Window"/> and represents 
    ''' the top-level window that will own this form.</param>
    ''' <returns>True if the form was displayed successfully and the item was changed.</returns>
    Private Function ITableItemDialog_Show(ByVal owner As IWin32Window) As Boolean Implements ITableItemDialog.Show
        Return ShowDialog(owner) = DialogResult.OK
    End Function

#End Region

End Class
