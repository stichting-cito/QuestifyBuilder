Imports System.Linq

Namespace PluginExtensibility.Html.Handlers.Logic
    Public MustInherit Class BaseMerger
        Implements ITableMergeStrategy


        Private ReadOnly _table As Table

        Public Sub New(table As Table)
            _table = table
        End Sub

        Public ReadOnly Property Table As Table
            Get
                Return _table
            End Get
        End Property


        Private Sub MergeCells(bounds As TableBounds) Implements ITableMergeStrategy.MergeCells
            DoMergeCells(bounds)
            DensifyTable()
        End Sub

        Public MustOverride Sub Merge(mergeTo As TableCell, other As TableCell)

        Private Sub DoMergeCells(bounds As TableBounds)

            Dim ref_intersects As List(Of TableCell) = Nothing
            Dim ref_contains As List(Of TableCell) = Nothing
            Table.GetInvolvedCells(bounds, ref_contains, ref_intersects)

            Dim mergeTo = ref_contains.First()

            For i = 1 To ref_contains.Count - 1
                Merge(mergeTo, ref_contains(i))
                Replace(mergeTo, ref_contains(i))
            Next

            mergeTo.ColSpan = bounds.Columns
            mergeTo.RowSpan = bounds.Rows

        End Sub

        Private Sub DensifyTable()
            Dim _CellRemoveColSpan As New Dictionary(Of TableCell, Integer)
            Dim _CellRemoveRowSpan As New Dictionary(Of TableCell, Integer)
            Dim _RowsToRemove As New HashSet(Of TableRow)
            Dim _ColsToRemove As New HashSet(Of TableColumn)
            Dim _handled As New HashSet(Of TableCell)

            For r = 0 To Table.Rows.Count - 1
                Dim row = Table.Rows(r) : _handled.Clear()
                If (Not row.OwnsCells()) Then
                    _RowsToRemove.Add(row)
                    For Each c In row.Cells
                        If (_CellRemoveRowSpan.ContainsKey(c)) Then
                            If (Not _handled.Contains(c)) Then
                                _CellRemoveRowSpan(c) = _CellRemoveRowSpan(c) + 1
                                _handled.Add(c)
                            End If
                        Else
                            _CellRemoveRowSpan.Add(c, 1)
                            _handled.Add(c)
                        End If
                    Next
                End If
            Next

            For _c = 0 To Table.Columns.Count - 1
                Dim column = Table.Columns(_c) : _handled.Clear()
                If (Not column.OwnsCells()) Then
                    _ColsToRemove.Add(column)
                    For Each c In column.Cells
                        If (_CellRemoveColSpan.ContainsKey(c)) Then
                            If (Not _handled.Contains(c)) Then
                                _CellRemoveColSpan(c) = _CellRemoveColSpan(c) + 1
                                _handled.Add(c)
                            End If

                        Else
                            _CellRemoveColSpan.Add(c, 1)
                            _handled.Add(c)
                        End If
                    Next
                End If
            Next

            AlterRowspans(_CellRemoveRowSpan)
            AlterColspans(_CellRemoveColSpan)

            For Each row In _RowsToRemove
                row.DeleteFromXml()
            Next

            For Each col In _ColsToRemove
                col.DeleteFromXml()
            Next

        End Sub


        Private Sub AlterRowspans(CellRemoveRowSpan As Dictionary(Of TableCell, Integer))
            If (CellRemoveRowSpan.Count > 0) Then
                For Each kvp In CellRemoveRowSpan
                    Dim b4 = kvp.Key.RowSpan
                    kvp.Key.RowSpan -= kvp.Value
                    Debug.Assert(kvp.Key.RowSpan > 0, "This should remain at least 1")
                Next
            End If
        End Sub

        Private Sub AlterColspans(CellRemoveColSpan As Dictionary(Of TableCell, Integer))
            If (CellRemoveColSpan.Count > 0) Then
                For Each kvp In CellRemoveColSpan
                    Dim b4 = kvp.Key.ColSpan
                    kvp.Key.ColSpan -= kvp.Value
                    Debug.Assert(kvp.Key.ColSpan > 0, "This should remain at least 1")
                Next
            End If
        End Sub

        Private Sub Replace(mergeTo As TableCell, cell As TableCell)
            For r = cell.RowNumber To (cell.RowNumber + cell.RowSpan - 1)
                For c = cell.ColNumber To (cell.ColNumber + cell.ColSpan - 1)
                    Table.Rows(r).Cells.Remove(cell)
                    Table.Rows(r).Cells.Insert(cell.ColNumber, mergeTo)
                    Table.Columns(c).Cells.Remove(cell)
                    Table.Columns(c).Cells.Insert(cell.RowNumber, mergeTo)
                Next
            Next
            cell.DeleteFromXml()
        End Sub
    End Class
End Namespace