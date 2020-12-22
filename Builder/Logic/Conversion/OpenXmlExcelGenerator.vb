Imports System.Linq
Imports DocumentFormat.OpenXml
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Spreadsheet

Namespace Conversion

    Public Class ExcelExport

        Private _textFormatStyleIndex As UInteger

        Public Sub ExportDataTable(table As DataTable, useDataColumnCaptionAsCellHeader As Boolean, exportFile As String)
            Dim excelDocument As New ExcelDocument()
            excelDocument.CreatePackage(exportFile)

            Using spreadsheet As SpreadsheetDocument = SpreadsheetDocument.Open(exportFile, True)


                Dim workbook As WorkbookPart = spreadsheet.WorkbookPart
                Dim sheet As WorkbookStylesPart = workbook.GetPartsOfType(Of WorkbookStylesPart)().FirstOrDefault()

                _textFormatStyleIndex = sheet.Stylesheet.CellFormats.Count.Value

                Dim textFormat As New CellFormat
                textFormat.NumberFormatId = Convert.ToUInt32(49)
                sheet.Stylesheet.CellFormats.AppendChild(textFormat)
                sheet.Stylesheet.Save()
                Dim worksheet As WorksheetPart = workbook.WorksheetParts.Last()

                Dim data As SheetData = worksheet.Worksheet.GetFirstChild(Of SheetData)()

                Dim header As New Row()
                header.RowIndex = CType(1, UInt32)

                For Each column As DataColumn In table.Columns
                    Dim cellHeader As String = IIf(useDataColumnCaptionAsCellHeader, column.Caption, column.ColumnName).ToString()
                    Dim headerCell As Cell = createTextCell(table.Columns.IndexOf(column) + 1, 1, cellHeader)

                    header.AppendChild(headerCell)
                Next
                data.AppendChild(header)

                Dim contentRow As DataRow
                For i As Integer = 0 To table.Rows.Count - 1
                    contentRow = table.Rows(i)
                    data.AppendChild(createContentRow(contentRow, i + 2))
                Next
            End Using
        End Sub

        Public Sub ExportDataTable(table As DataTable, exportFile As String)
            ExportDataTable(table, False, exportFile)
        End Sub


        Private Function GetColumnName(columnIndex As Integer) As String
            Dim dividend As Integer = columnIndex
            Dim columnName As String = [String].Empty
            Dim modifier As Integer

            While dividend > 0
                modifier = (dividend - 1) Mod 26
                columnName = Convert.ToChar(65 + modifier).ToString() & columnName
                dividend = CInt((dividend - modifier) \ 26)
            End While

            Return columnName
        End Function

        Private Function CreateTextCell(columnIndex As Integer, rowIndex As Integer, cellValue As Object) As Cell
            Dim cell As New Cell()

            cell.DataType = CellValues.InlineString
            cell.CellReference = getColumnName(columnIndex) & rowIndex
            cell.StyleIndex = New UInt32Value(_textFormatStyleIndex)
            Dim inlineString As New InlineString()
            Dim t As New Text()

            t.Text = cellValue.ToString()
            inlineString.AppendChild(t)
            cell.AppendChild(inlineString)

            Return cell
        End Function

        Private Function CreateContentRow(dataRow As DataRow, rowIndex As Integer) As Row

            Dim row As New Row() With { _
                    .RowIndex = CType(rowIndex, UInt32) _
                    }

            For i As Integer = 0 To dataRow.Table.Columns.Count - 1
                Dim dataCell As Cell = createTextCell(i + 1, rowIndex, dataRow(i))
                row.AppendChild(dataCell)
            Next
            Return row
        End Function

    End Class
End NameSpace