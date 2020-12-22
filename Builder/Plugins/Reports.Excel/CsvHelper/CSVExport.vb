Imports System.Linq
Imports System.IO

Public Class CSVExport

    Public Shared Sub ExportDataTableToCSV(dataTable As DataTable, outputFile As String, seperatorChar As Char, includeColumnHeader As Boolean)
        Using fCsvOut As New StreamWriter(outputFile)

            If seperatorChar = Nothing Then
                seperatorChar = ","c
            End If

            If includeColumnHeader Then
                Dim columnHeaders As String() = (From c In dataTable.Columns Select new String(c.ColumnName)).ToArray()
                fCsvOut.WriteLine(String.Join(seperatorChar, columnHeaders))
            End If

            For i As Integer = 0 To dataTable.Rows.Count - 1

                For j As Integer = 0 To dataTable.Columns.Count - 1
                    Dim colValue As String = dataTable.Rows(i)(j).ToString()

                    colValue = colValue.Replace("""", """""")

                    If colValue.Contains(seperatorChar) Then
                        colValue = String.Format("""{0}""", colValue)
                    End If

                    fCsvOut.Write(colValue)

                    If j < dataTable.Columns.Count - 1 Then
                        fCsvOut.Write(seperatorChar)
                    End If
                Next

                If i < dataTable.Rows.Count - 1 Then
                    fCsvOut.WriteLine()
                End If
            Next

            fCsvOut.Close()
        End Using
    End Sub

End Class
