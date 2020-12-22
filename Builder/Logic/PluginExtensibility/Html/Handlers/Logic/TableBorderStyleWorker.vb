Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic

Namespace PluginExtensibility.Html.Handlers.Logic

    Friend Class TableBorderStyleWorker

        Private _table As Table

        Public Sub New(table As Table)
            _table = table
        End Sub

        Public Sub setStyleFor(bounds As TableBounds, styleDto As TableStyleDto, setStyleMethod As ISetTableBorderStyleStrategy)
            Debug.Assert(Not (styleDto.HasMidVertical Xor (bounds.Columns > 1)), "style Marker for midsection false")
            Debug.Assert(Not (styleDto.HasMidHorizontal Xor (bounds.Rows > 1)), "style Marker for midsection false")

            For x = bounds.Left To bounds.Right
                For y = bounds.Top To bounds.Bottom
                    Dim cellToModify As TableCell = _table.GetCellByCoords(x, y)

                    Dim bLeft = cellToModify.ColNumber = bounds.Left
                    Dim bTop = cellToModify.RowNumber = bounds.Top
                    Dim bRight = cellToModify.ColNumber + (cellToModify.ColSpan - 1) = bounds.Right
                    Dim bBottom = cellToModify.RowNumber + (cellToModify.RowSpan - 1) = bounds.Bottom

                    setStyleFor(cellToModify, styleDto, setStyleMethod, bLeft, bTop, bRight, bBottom)
                    cellToModify.ApplyStyles()
                Next
            Next
        End Sub

        Protected Sub setStyleFor(cellToModify As TableCell, styleDto As TableStyleDto, setStyleMethod As ISetTableBorderStyleStrategy,
                                  bordersLeft As Boolean, bordersTop As Boolean, bordersRight As Boolean, bordersBottom As Boolean)
            setStyleMethod.SetLeft_BorderStyle(styleDto.GetLeftVert(bordersLeft, styleDto.LineColor), cellToModify, bordersLeft)
            setStyleMethod.SetTop_BorderStyle(styleDto.GetTopHorizontal(bordersTop, styleDto.LineColor), cellToModify, bordersTop)
            setStyleMethod.SetRight_BorderStyle(styleDto.GetRightVert(bordersRight, styleDto.LineColor), cellToModify, bordersRight)
            setStyleMethod.SetBottom_BorderStyle(styleDto.GetBottomHorizontal(bordersBottom, styleDto.LineColor), cellToModify, bordersBottom)
            setStyleMethod.SetBackgroundColor(styleDto.BackColor, cellToModify)
        End Sub

    End Class
End Namespace