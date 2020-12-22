Namespace PluginExtensibility.Html.Handlers.Logic
    Public Class TableBounds

        Private ReadOnly _left As Integer
        Private ReadOnly _right As Integer
        Private ReadOnly _top As Integer
        Private ReadOnly _bottom As Integer


        Public Sub New(cell As TableCell)
            _left = cell.ColNumber
            _top = cell.RowNumber
            _right = _left + cell.ColSpan - 1
            _bottom = _top + cell.RowSpan - 1
        End Sub

        Public Sub New(cell1 As TableCell, cell2 As TableCell)
            _left = Math.Min(cell1.ColNumber, cell2.ColNumber)
            _right = Math.Max(cell1.ColNumber + (cell1.ColSpan - 1), cell2.ColNumber + (cell2.ColSpan - 1))
            _top = Math.Min(cell1.RowNumber, cell2.RowNumber)
            _bottom = Math.Max(cell1.RowNumber + (cell1.RowSpan - 1), cell2.RowNumber + (cell2.RowSpan - 1))
        End Sub

        Public Sub New(column As Integer, row As Integer, width As Integer, height As Integer)
            _left = column
            _right = column + width - 1
            _top = row
            _bottom = row + height - 1
        End Sub

        Public Sub New(column As Integer, row As Integer)
            _left = column
            _right = column
            _top = row
            _bottom = row
        End Sub


        Public ReadOnly Property Top As Integer
            Get
                Return _top
            End Get
        End Property

        Public ReadOnly Property Right As Integer
            Get
                Return _right
            End Get
        End Property

        Public ReadOnly Property Left As Integer
            Get
                Return _left
            End Get
        End Property

        Public ReadOnly Property Bottom As Integer
            Get
                Return _bottom
            End Get
        End Property

        Public ReadOnly Property Rows As Integer
            Get
                Return Bottom - Top + 1
            End Get
        End Property

        Public ReadOnly Property Columns As Integer
            Get
                Return Right - Left + 1
            End Get
        End Property


        Public Function Intersects(bounds As TableBounds) As Boolean
            Return bounds.Left < Me.Left + Me.Columns AndAlso Me.Left < bounds.Left + bounds.Columns AndAlso bounds.Top < Me.Top + Me.Rows AndAlso Me.Top < bounds.Top + bounds.Rows
        End Function

        Public Function Contains(rect As TableBounds) As Boolean
            Return Me.Left <= rect.Left AndAlso rect.Left + rect.Columns <= Me.Left + Me.Columns AndAlso Me.Top <= rect.Top AndAlso rect.Top + rect.Rows <= Me.Top + Me.Rows
        End Function


    End Class

End Namespace