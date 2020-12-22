Imports System.Xml
Imports System.Linq

Namespace PluginExtensibility.Html.Handlers.Logic

    <DebuggerDisplay("TableRow #Cells:{Cells.Count}")>
    Public Class TableRow
        Inherits TableItem

        Private _cells As New List(Of TableCell)
        Private Shared cellInstanceNr As Integer


        Public Sub New(table As Table, node As XmlNode)
            MyBase.New(table, node)
        End Sub


        Public ReadOnly Property Cells As IList(Of TableCell)
            Get
                Return _cells
            End Get
        End Property

        Function CreateNewRow() As TableRow
            Dim n = MyBase.MakeNewXmlNode("tr")
            Dim loc = Table.Rows.IndexOf(Me)
            Dim ret = New TableRow(Table, n)

            Table.Rows.Insert(loc + 1, ret)
            Node.ParentNode.InsertAfter(n, Node)

            Return ret
        End Function

        Public Function CreateNewCell(columnNr As Integer) As TableCell

            Dim ret As TableCell

            Dim n = MyBase.MakeNewXmlNode("td")
            Dim p = MyBase.MakeNewXmlNode("p")



            cellInstanceNr += 1
            Dim t = MyBase.MakeNewXmlTextNode(ChrW(&HA0))

            p.AppendChild(t)
            n.AppendChild(p)

            ret = New TableCell(Table, n)
            Dim cellsOnThisRow = (From c In Cells Where Object.ReferenceEquals(Me, Table.Rows(c.RowNumber)) Select c).ToArray()

            If (cellsOnThisRow.Length = 0) Then
                Node.AppendChild(n)
                Cells.Add(ret)
            Else
                Debug.Assert(columnNr < Me.Cells.Count)
                Dim row = Table.Rows(Cells(columnNr).RowNumber)
                Debug.Assert(Not Object.ReferenceEquals(Me, row), "Should not replace cell on same row.")

                Dim refCell = cellsOnThisRow.Where(Function(c) c.ColNumber > columnNr).FirstOrDefault()
                If (refCell Is Nothing) Then
                    Node.AppendChild(n)
                Else
                    Node.InsertBefore(n, refCell.Node)
                End If
            End If

            Return ret
        End Function

        Public Function OwnsCells() As Boolean
            Dim rowNr = Table.Rows.IndexOf(Me)
            Debug.Assert(rowNr >= 0, "Not in table??!!")
            Return Cells.Any(Function(c) c.RowNumber = rowNr)
        End Function

        Friend Overrides Sub DeleteFromXml()
            MyBase.DeleteFromXml()
            Debug.Assert(Table.Rows.Remove(Me))
        End Sub

    End Class

End Namespace