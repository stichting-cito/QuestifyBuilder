Imports DocumentFormat.OpenXml.Wordprocessing
Imports NotesFor.HtmlToOpenXml

Public Class WidthExtractor
    Public Shared Function Extract(tableCellElement As XElement) As TableCellWidth
        If tableCellElement Is Nothing Then
            Return Nothing
        End If

        Dim widthAttribute = tableCellElement.Attribute("width")
        If widthAttribute Is Nothing Then
            Return Nothing
        End If

        Dim un = Unit.Parse(widthAttribute.Value)
        Dim cellWidth As TableCellWidth = Nothing
        If un.IsValid Then
            Select Case un.Type
                Case UnitMetric.Percent
                    cellWidth = New TableCellWidth() With {.Type = TableWidthUnitValues.Pct, .Width = $"{un.Value * 50}"}

                Case UnitMetric.Point
                    cellWidth = New TableCellWidth() With {.Type = TableWidthUnitValues.Dxa, .Width = $"{un.ValueInDxa * 20}"}

                Case UnitMetric.Pixel
                    cellWidth = New TableCellWidth() With {.Type = TableWidthUnitValues.Dxa, .Width = $"{un.ValueInDxa}"}
            End Select
        End If

        Return cellWidth
    End Function
End Class