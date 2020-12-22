Imports System.Linq
Imports System.Xml

Namespace PluginExtensibility.Html.Handlers.Logic

    Public Class Table
        Inherits TableItem


        Private _rows As List(Of TableRow)
        Private _columns As List(Of TableColumn)
        Private _style As CssStyleList


        Public Sub New(node As Xml.XmlNode)
            MyBase.New(Nothing, node)
            WalkTable()
        End Sub

        Public Sub New()
            MyBase.New(Nothing, Nothing)
        End Sub


        Public Shared Function GetTableFromNode(node As XmlNode) As Table
            If (node Is Nothing) Then Return Nothing
            If (node.Name <> "table") Then Return GetTableFromNode(node.ParentNode)
            Return New Table(node)
        End Function


        Public Overridable Function GetRowCount() As Integer
            Return _rows.Count
        End Function

        Public Overridable Function GetColumnCount() As Integer
            Return _rows.First.Cells.Count
        End Function

        Public Function TableIsBalanced() As Boolean
            Return _rows.All(Function(r) r.Cells.Count = _rows.First.Cells.Count)
        End Function

        Public Overridable Function GetColumnCountForRow(rowNumber As Integer) As Integer
            Return _rows(rowNumber).Cells.Count
        End Function

        Public Function GetCellByNode(node As XmlNode) As TableCell
            Dim cellNode As XmlNode = TableCell.GetTableCellNode(node)
            For Each row In Rows
                For Each cell In row.Cells
                    If Object.ReferenceEquals(cell.Node, cellNode) Then
                        Return cell
                    End If
                Next
            Next
            Return Nothing
        End Function

        Public Function GetCellCoordinates(cell As TableCell, ByRef columnNr As Integer, ByRef rowNr As Integer) As Boolean
            Dim i, j As Integer
            columnNr = -1 : rowNr = -1

            Dim row As TableRow = Nothing
            For Each r In Rows
                If r.Cells.Contains(cell) Then
                    row = r
                    For Each c In r.Cells
                        If (Object.ReferenceEquals(cell, c)) Then
                            columnNr = i
                            rowNr = j
                            Return True
                        End If
                        i += 1
                    Next

                    Return False
                End If
                j += 1
            Next

            Return False
        End Function

        Public Function CanMerge(topLeftCell As TableCell, bottomRightCell As TableCell) As Boolean
            Dim bounds = New TableBounds(topLeftCell, bottomRightCell)

            Dim ref_intersects As List(Of TableCell) = Nothing
            Dim ref_contains As List(Of TableCell) = Nothing
            GetInvolvedCells(bounds, ref_contains, ref_intersects)

            Return ref_intersects Is Nothing OrElse ref_intersects.Count = 0
        End Function

        Public Sub MergeCells(topLeftCell As TableCell, bottomRightCell As TableCell)
            MergeCells(topLeftCell, bottomRightCell, New TableCellMerger(Me))
        End Sub

        Public Sub SetStyleTo(topLeftCell As TableCell, bottomRightCell As TableCell, style As TableStyleDto)
            Me.doSetStyleTo(topLeftCell, bottomRightCell, style)
        End Sub

        Public Overridable Function GetCellByCoords(x As Integer, y As Integer) As TableCell
            Return _rows(y).Cells(x)
        End Function

        Function GetTableStyleDto(bounds As TableBounds) As TableStyleDto
            Return TableStyleDeterminator.GetStyle(Me, bounds)
        End Function



        Public ReadOnly Property Rows As IList(Of TableRow)
            Get
                Return _rows
            End Get
        End Property

        Public ReadOnly Property Columns As IList(Of TableColumn)
            Get
                Return _columns
            End Get
        End Property

        Public ReadOnly Property Style As CssStyleList
            Get
                If (_style Is Nothing) Then _style = MyBase.GetStyle()
                Return _style
            End Get
        End Property





        Friend Sub WalkTable()
            Dim rowNr As Integer = 0
            _rows = New List(Of TableRow)
            _columns = New List(Of TableColumn)
            For Each row As XmlNode In Node.SelectNodes(".//def:tr", ns)
                _rows.Add(New TableRow(Me, row))

                For Each e As XmlNode In row.SelectNodes("def:td", ns)
                    Dim cell = New TableCell(Me, e) With {.RowNumber = rowNr}
                    For i = 1 To cell.ColSpan
                        _rows(rowNr).Cells.Add(cell)
                    Next
                Next
                rowNr = rowNr + 1
            Next
            Dim handled As New HashSet(Of TableCell)
            Dim Nr, rws As Integer : rws = _rows.Count
            Dim nxtCell As TableCell = Nothing
            If rws > 0 Then
                nxtCell = _rows(Nr Mod rws).Cells(Nr \ rws)
            End If
            While nxtCell IsNot Nothing
                If (nxtCell.RowSpan > 1 AndAlso Not handled.Contains(nxtCell)) Then

                    For rowIns = 1 To nxtCell.RowSpan - 1
                        For colspan = 1 To nxtCell.ColSpan
                            _rows(rowIns + (Nr Mod rws)).Cells.Insert((Nr \ rws), nxtCell)
                        Next
                    Next
                    handled.Add(nxtCell)
                End If
                Nr += 1
                nxtCell = If(Rows(Nr Mod rws).Cells.Count = Nr \ rws, Nothing, _rows(Nr Mod rws).Cells(Nr \ rws))
            End While
            handled.Clear()

            Dim colNode = Node.SelectNodes(".//def:colgroup", ns)
            Debug.Assert(colNode IsNot Nothing)
            Debug.Assert(colNode.Count = 1)
            For Each r In _rows
                Dim i As Integer = 0
                For Each c In r.Cells
                    If (_columns.Count <= i) Then
                        _columns.Add(New TableColumn(Me, colNode(0).ChildNodes(i)))
                    End If
                    _columns(i).Cells.Add(c)

                    If (Not handled.Contains(c)) Then
                        c.ColNumber = i
                        handled.Add(c)
                    End If

                    i += 1
                Next
            Next
        End Sub

        Friend Sub GetInvolvedCells(bounds As TableBounds, ByRef ref_contains As List(Of TableCell), ByRef ref_intersects As List(Of TableCell))
            ref_contains = New List(Of TableCell) : ref_intersects = New List(Of TableCell)
            Dim handled As New HashSet(Of TableCell)

            For r = bounds.Top To bounds.Bottom
                For c = bounds.Left To bounds.Right
                    Dim cell = _rows(r).Cells(c)

                    If (Not handled.Contains(cell)) Then
                        Dim cellBounds As New TableBounds(cell)
                        If (bounds.Contains(cellBounds)) Then
                            ref_contains.Add(cell) : handled.Add(cell)
                        Else
                            If (bounds.Intersects(cellBounds)) Then
                                ref_intersects.Add(cell) : handled.Add(cell)
                            Else
                                Debug.Assert(False, "Should NOT occur!")
                            End If
                        End If
                    End If

                Next
            Next
        End Sub

        Friend Sub MergeCells(topLeftCell As TableCell, bottomRightCell As TableCell, mergeStrategy As ITableMergeStrategy)
            Dim bounds = New TableBounds(topLeftCell, bottomRightCell)

            If (CanMerge(topLeftCell, bottomRightCell)) Then
                mergeStrategy.MergeCells(bounds)
            End If

        End Sub

        Friend Sub doSetStyleTo(topLeftCell As TableCell, bottomRightCell As TableCell,
                              style As TableStyleDto)
            Dim bounds = New TableBounds(topLeftCell, bottomRightCell)
            Dim setSyleStrategy As ISetTableBorderStyleStrategy
            If (IsCollapsedStyle()) Then
                setSyleStrategy = New SetCellBorderStyleHandler_CollapsedStrategy(bounds)
            Else
                setSyleStrategy = New SetCellBordeStyleHandler_SeperatedStrategy(bounds)
            End If
            Dim worker As New TableBorderStyleWorker(Me)

            worker.setStyleFor(bounds, style, setSyleStrategy)
        End Sub

        Friend Overridable Function IsCollapsedStyle() As Boolean
            Return ((Style.BorderCollapse IsNot Nothing) AndAlso (Style.BorderCollapse.isCollapsed.HasValue) AndAlso (Style.BorderCollapse.isCollapsed.Value))
        End Function

    End Class
End Namespace