Namespace PluginExtensibility.Html.Handlers.Logic

    Public Class SetCellBordeStyleHandler_SeperatedStrategy
        Implements ISetTableBorderStyleStrategy


        Private _bounds As TableBounds

        Sub New(bounds As TableBounds)
            _bounds = bounds
        End Sub

        Public Sub SetLeft_BorderStyle(style As CssBorder, forCell As TableCell, borders As Boolean) Implements ISetTableBorderStyleStrategy.SetLeft_BorderStyle
            forCell.Style.BorderLeft = style
        End Sub

        Public Sub SetTop_BorderStyle(style As CssBorder, forCell As TableCell, borders As Boolean) Implements ISetTableBorderStyleStrategy.SetTop_BorderStyle
            forCell.Style.BorderTop = style
        End Sub

        Public Sub SetRight_BorderStyle(style As CssBorder, forCell As TableCell, borders As Boolean) Implements ISetTableBorderStyleStrategy.SetRight_BorderStyle
            forCell.Style.BorderRight = style
        End Sub

        Public Sub SetBottom_BorderStyle(style As CssBorder, forCell As TableCell, borders As Boolean) Implements ISetTableBorderStyleStrategy.SetBottom_BorderStyle
            forCell.Style.BorderBottom = style
        End Sub

        Public Sub SetBackgroundColor(color As System.Drawing.Color?, forCell As TableCell) Implements ISetTableBorderStyleStrategy.SetBackgroundColor
            forCell.Style.Background_color = color
        End Sub


    End Class

End Namespace