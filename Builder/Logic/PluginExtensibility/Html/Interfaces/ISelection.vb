Imports System.Xml

Public Interface ISelection
    Inherits ITextRange
    ReadOnly Property StartIndex As Integer

    ReadOnly Property StartNode As XmlNode
    ReadOnly Property Length As Integer
    ReadOnly Property IsEmpty As Boolean
    ReadOnly Property IsTable As Boolean
    Sub InsertRowAbove()
    Sub InsertRowBelow()
    Sub DeleteRows()
    Sub InsertColumnBefore()
    Sub InsertColumnAfter()
    Sub DeleteColumns()
    Sub MergeCells()
    Sub SplitCells()
    Sub ShowCellDialog(editor As IXHtmlEditor, nmspManager As XmlNamespaceManager)

    Sub ShowCellInnerMarginDialog(editor As IXHtmlEditor, nmspManager As XmlNamespaceManager)
    Sub ShowTableDialog(editor As IXHtmlEditor, nmspManager As XmlNamespaceManager)

    Sub ApplyTag(tagName As String)

End Interface
