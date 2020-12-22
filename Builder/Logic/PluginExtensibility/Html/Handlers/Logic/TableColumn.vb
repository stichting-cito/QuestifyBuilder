Imports System.Xml
Imports System.Linq

Namespace PluginExtensibility.Html.Handlers.Logic

    Public Class TableColumn
        Inherits TableItem

        Private _cells As New List(Of TableCell)


        Public Sub New(table As Table, node As XmlNode)
            MyBase.New(table, node)
        End Sub



        Public ReadOnly Property Cells As IList(Of TableCell)
            Get
                Return _cells
            End Get
        End Property


        Public Function CreateNewColumn() As TableColumn
            Dim n = MyBase.MakeNewXmlNode("col")
            Dim loc = Table.Columns.IndexOf(Me)
            Dim ret = New TableColumn(Table, n)

            Table.Columns.Insert(loc + 1, ret)
            Node.ParentNode.InsertAfter(n, Node)

            Return ret
        End Function

        Public Function OwnsCells() As Boolean
            Dim colNr = Table.Columns.IndexOf(Me)
            Debug.Assert(colNr >= 0, "Not in table??!!")
            Return Cells.Any(Function(c) c.ColNumber = colNr)
        End Function

        Friend Overrides Sub DeleteFromXml()
            MyBase.DeleteFromXml()
            Debug.Assert(Table.Columns.Remove(Me))
        End Sub


    End Class
End Namespace