Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic

Namespace PluginExtensibility.Html.Handlers
    Public Class HtmlTableHandler
        Inherits HtmlHandlerBase



        Public Sub New(editor As IXHtmlEditor)
            MyBase.New(editor, Nothing, Nothing, Nothing)
        End Sub


        Public ReadOnly Property IsSingleCellSelected As Boolean
            Get
                Return MyBase.editor.Selection.IsTable AndAlso
                    Object.ReferenceEquals(editor.Selection.Start, editor.Selection.End)
            End Get
        End Property

        Public Sub SplitCellHorizontal()
            SplitCell(False)
        End Sub

        Public Sub SplitCellVertical()
            SplitCell(True)
        End Sub

        Public Function MergeCells() As Boolean

            Dim t As Table = Table.GetTableFromNode(editor.Selection.Start)
            Dim n = editor.Selection.Start
            Dim n2 = editor.Selection.End
            Dim cell As TableCell = t.GetCellByNode(n)
            Dim cell2 As TableCell = t.GetCellByNode(n2)

            Debug.Assert(cell IsNot Nothing)
            Debug.Assert(cell2 IsNot Nothing)

            Dim ret = t.CanMerge(cell, cell2)
            If (ret) Then
                editor.BeginTransaction()
                t.MergeCells(cell, cell2)
                editor.CommitTransaction()

                editor.ClearSelection()

                editor.LoadXml(editor.Document.OuterXml)

                editor.Select(n)
            End If
            Return ret
        End Function

        Public Function ApplyStyleToTable(style As TableStyleDto) As Boolean
            Dim t As Table = Table.GetTableFromNode(editor.Selection.Start)
            Dim n = editor.Selection.Start
            Dim n2 = editor.Selection.End
            Dim cell As TableCell = t.GetCellByNode(n)
            Dim cell2 As TableCell = t.GetCellByNode(n2)

            Debug.Assert(cell IsNot Nothing)
            Debug.Assert(cell2 IsNot Nothing)

            Dim ret = t.CanMerge(cell, cell2)
            If (ret) Then
                editor.BeginTransaction()
                t.SetStyleTo(cell, cell2, style)
                editor.CommitTransaction()
            End If
            Return ret
        End Function



        Public Function GetStyleFromSelectedCells() As TableStyleDto

            Dim t As Table = Table.GetTableFromNode(editor.Selection.Start)
            Dim n = editor.Selection.Start
            Dim n2 = editor.Selection.End
            Dim cell As TableCell = t.GetCellByNode(n)
            Dim cell2 As TableCell = t.GetCellByNode(n2)

            Debug.Assert(cell IsNot Nothing)
            Debug.Assert(cell2 IsNot Nothing)

            Dim bounds As New TableBounds(t.GetCellByNode(cell.Node),
                                                  t.GetCellByNode(cell2.Node))

            Return t.GetTableStyleDto(bounds)
        End Function


        Private Sub SplitCell(doVerticaly As Boolean)

            editor.BeginTransaction()
            Dim t As Table = Table.GetTableFromNode(editor.Selection.Start)
            Dim n = editor.Selection.Start
            Dim cell As TableCell = t.GetCellByNode(n)
            Debug.Assert(cell IsNot Nothing)
            If (doVerticaly) Then
                cell.SplitVertical()
            Else
                cell.SplitHorizontal()
            End If

            Dim selectNode = cell.Node
            editor.CommitTransaction()
            editor.Select(selectNode)
        End Sub


    End Class
End Namespace

