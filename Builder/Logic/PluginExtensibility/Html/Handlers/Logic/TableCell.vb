Imports System.Drawing
Imports System.Xml

Namespace PluginExtensibility.Html.Handlers.Logic

    <DebuggerDisplay("TableCell '{InnerText}' Col-/RowSpan ({ColSpan}/{RowSpan})")>
    Public Class TableCell
        Inherits TableItem

        Shared instanceNr As Integer = 0
        Private _rowNumber As Integer
        Private _colNumber As Integer
        Private _style As CssStyleList = Nothing


        Public Sub New(table As Table, node As XmlNode)
            MyBase.New(table, node)
        End Sub

        Public Sub New(t As Table, styleForUnitTests As CssStyleList)
            MyBase.New(t, Nothing)
            _style = styleForUnitTests
        End Sub

        Shared Function GetTableCellNode(node As XmlNode) As XmlNode
            If (node Is Nothing) Then Return Nothing
            If (node.Name.Equals("td", StringComparison.InvariantCultureIgnoreCase) OrElse
                node.Name.Equals("th", StringComparison.InvariantCultureIgnoreCase)) Then
                Return node
            End If
            Return GetTableCellNode(node.ParentNode)
        End Function


        Public Property RowNumber As Integer
            Get
                Return _rowNumber
            End Get
            Friend Set(ByVal value As Integer)
                _rowNumber = value
            End Set
        End Property


        Public Property ColNumber As Integer
            Get
                Return _colNumber
            End Get
            Friend Set(ByVal value As Integer)
                _colNumber = value
            End Set
        End Property


        Public ReadOnly Property InnerText As String
            Get
                Return Node.InnerText
            End Get
        End Property

        Public Overridable Property RowSpan As Integer
            Get
                Return MyBase.GetValueFromNode(Of Integer)("rowspan", 1)
            End Get
            Set(value As Integer)
                If (value = 1) Then
                    MyBase.RemoveAttribute("rowspan")
                Else
                    MyBase.SetValueToNode("rowspan", value.ToString())
                End If
            End Set
        End Property

        Public Overridable Property ColSpan As Integer
            Get
                Return MyBase.GetValueFromNode(Of Integer)("colspan", 1)
            End Get
            Set(value As Integer)
                If (value = 1) Then
                    MyBase.RemoveAttribute("colspan")
                Else
                    MyBase.SetValueToNode("colspan", value.ToString())
                End If
            End Set
        End Property

        Public ReadOnly Property Style As CssStyleList
            Get
                If (_style Is Nothing) Then _style = MyBase.GetStyle()
                Return _style
            End Get
        End Property


        Public Sub SplitHorizontal()

            If (RowSpan = 1) Then
                Dim row = Table.Rows(_rowNumber)
                Dim newRow As TableRow = row.CreateNewRow()
                Dim lastCell As TableCell = Nothing
                For Each c In row.Cells
                    If (Object.ReferenceEquals(c, lastCell)) Then Continue For

                    If (Not Object.ReferenceEquals(Me, c)) Then
                        newRow.Cells.Add(c)
                        c.RowSpan += 1
                    Else
                        Dim newCell = newRow.CreateNewCell(_colNumber)
                        newCell.RowNumber = _rowNumber + 1
                        newCell.ColSpan = c.ColSpan
                        newCell.SetStyle(Me.Style)
                    End If
                    lastCell = c
                Next
            Else
                Dim nextRow = Table.Rows(_rowNumber + (Me.RowSpan - (Me.RowSpan \ 2)))
                Dim newCell = nextRow.CreateNewCell(_colNumber)
                newCell.RowSpan = Me.RowSpan \ 2
                Me.RowSpan = Me.RowSpan - (Me.RowSpan \ 2)
                newCell.ColSpan = ColSpan
                newCell.SetStyle(Me.Style)
            End If

        End Sub

        Public Sub SplitVertical()

            If (ColSpan = 1) Then

                Dim col = Table.Columns(_colNumber)
                Dim newCol = col.CreateNewColumn()
                Dim lastCell As TableCell = Nothing
                For Each c In col.Cells
                    If (Object.ReferenceEquals(lastCell, c)) Then Continue For

                    If (Not Object.ReferenceEquals(c, Me)) Then
                        c.ColSpan += 1
                        newCol.Cells.Add(c)
                    Else
                        Dim newCell = Me.CreateNewCell()
                        With newCell
                            .RowNumber = _rowNumber
                            .ColNumber = _colNumber + 1
                            .RowSpan = Me.RowSpan
                        End With
                        newCell.SetStyle(Me.Style)
                        newCol.Cells.Add(newCell)
                    End If
                    lastCell = c
                Next
            Else
                With Me.CreateNewCell
                    .ColSpan = Me.ColSpan \ 2
                    .RowSpan = Me.RowSpan
                    .SetStyle(Me.Style)
                End With

                Me.ColSpan = Me.ColSpan - (Me.ColSpan \ 2)
            End If

        End Sub

        Public Function CreateNewCell() As TableCell

            Dim ret As TableCell

            Dim n = MyBase.MakeNewXmlNode("td")
            Dim p = MyBase.MakeNewXmlNode("p")
            instanceNr += 1
            Dim t = MyBase.MakeNewXmlTextNode(ChrW(&HA0))

            p.AppendChild(t)
            n.AppendChild(p)

            ret = New TableCell(Table, n) With {.RowNumber = _rowNumber}
            Node.ParentNode.InsertAfter(n, Node)

            Dim locMe = Table.Rows(_rowNumber).Cells.IndexOf(Me)
            Table.Rows(_rowNumber).Cells.Insert(locMe + 1, ret)
            Return ret
        End Function

        Sub SetBGColor(color As Color)
            Dim style = MyBase.GetStyle()
            style.Background_color = color
            MyBase.SetStyle(style)
        End Sub


        Overridable Sub ApplyStyles()
            MyBase.SetStyle(_style)
        End Sub






    End Class

End Namespace