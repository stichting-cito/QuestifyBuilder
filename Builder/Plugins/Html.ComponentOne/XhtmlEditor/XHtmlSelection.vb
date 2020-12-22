Imports System.Xml
Imports C1.Win.C1Editor
Imports Questify.Builder.Logic

Public Class XHtmlSelection
    Inherits XhtmlTextRange
    Implements ISelection

    Public Sub New(editor As C1Editor)
        MyBase.New(editor)
    End Sub
    Public ReadOnly Property Start As Integer Implements ISelection.StartIndex
        Get
            If (_innerSelection Is Nothing) Then
                Return 0
            End If
            Return _innerSelection.Start.Offset
        End Get
    End Property
    Public ReadOnly Property StartNode As XmlNode Implements ISelection.StartNode
        Get
            If (_innerSelection Is Nothing) Then
                Return Nothing
            End If
            Return _innerSelection.Start.Node
        End Get
    End Property

    Public ReadOnly Property Length As Integer Implements ISelection.Length
        Get
            If _innerSelection Is Nothing Then
                Return 0
            End If
            If _innerSelection.Start Is Nothing Or _innerSelection.End Is Nothing Then
                Return 0
            End If
            Return _innerSelection.End.Offset - _innerSelection.Start.Offset
        End Get
    End Property

    Public ReadOnly Property IsEmpty As Boolean Implements ISelection.IsEmpty
        Get
            If (_innerSelection Is Nothing) Then
                Return True
            End If
            Return String.IsNullOrEmpty(_innerSelection.Text)
        End Get
    End Property

    Public ReadOnly Property IsTable As Boolean Implements ISelection.IsTable
        Get
            If (_innerSelection Is Nothing) Then
                Return False
            End If
            Return _innerSelection.Table IsNot Nothing
        End Get
    End Property

    Public Sub InsertRowAbove() Implements ISelection.InsertRowAbove
        If _innerSelection IsNot Nothing AndAlso _innerSelection.Table.CanExecute(Table.Action.InsertRowAbove) Then
            _innerSelection.Table.Execute(Table.Action.InsertRowAbove)
        End If
    End Sub

    Public Sub InsertRowBelow() Implements ISelection.InsertRowBelow
        If _innerSelection IsNot Nothing AndAlso _innerSelection.Table.CanExecute(Table.Action.InsertRowBelow) Then
            _innerSelection.Table.Execute(Table.Action.InsertRowBelow)
        End If
    End Sub

    Public Sub DeleteRows() Implements ISelection.DeleteRows
        If _innerSelection IsNot Nothing AndAlso _innerSelection.Table.CanExecute(Table.Action.DeleteRows) Then
            _innerSelection.Table.Execute(Table.Action.DeleteRows)
        End If
    End Sub

    Public Sub InsertColumnBefore() Implements ISelection.InsertColumnBefore
        If _innerSelection IsNot Nothing AndAlso _innerSelection.Table.CanExecute(Table.Action.InsertColumnBefore) Then
            _innerSelection.Table.Execute(Table.Action.InsertColumnBefore)
        End If
    End Sub

    Public Sub InsertColumnAfter() Implements ISelection.InsertColumnAfter
        If _innerSelection IsNot Nothing AndAlso _innerSelection.Table.CanExecute(Table.Action.InsertColumnAfter) Then
            _innerSelection.Table.Execute(Table.Action.InsertColumnAfter)
        End If
    End Sub

    Public Sub DeleteColumns() Implements ISelection.DeleteColumns
        If _innerSelection IsNot Nothing AndAlso _innerSelection.Table.CanExecute(Table.Action.DeleteColumns) Then
            _innerSelection.Table.Execute(Table.Action.DeleteColumns)
        End If
    End Sub

    Public Sub MergeCells() Implements ISelection.MergeCells
        If _innerSelection IsNot Nothing AndAlso _innerSelection.Table.CanExecute(Table.Action.MergeCells) Then
            _innerSelection.Table.Execute(Table.Action.MergeCells)
        End If
    End Sub

    Public Sub SplitCells() Implements ISelection.SplitCells
        If _innerSelection IsNot Nothing AndAlso _innerSelection.Table.CanExecute(Table.Action.SplitCells) Then
            _innerSelection.Table.Execute(Table.Action.SplitCells)
        End If
    End Sub

    Public Sub ShowCellDialog(editor As IXHtmlEditor, nmspManager As XmlNamespaceManager) Implements ISelection.ShowCellDialog
        If (_innerEditor IsNot Nothing AndAlso _innerSelection IsNot Nothing) Then
            _innerEditor.CustomDialogs.TableCellDialog = New DialogEditCellProperties(editor, nmspManager)
            _innerSelection.Table.ShowDialog(Table.DialogType.Cell)
        End If
    End Sub

    Public Sub ShowCellInnerMarginDialog(editor As IXHtmlEditor, nmspManager As XmlNamespaceManager) Implements ISelection.ShowCellInnerMarginDialog
        If (_innerEditor IsNot Nothing AndAlso _innerSelection IsNot Nothing) Then
            _innerEditor.CustomDialogs.TableCellDialog = New EditCellInnerMarginPropertiesDialog(editor, nmspManager)
            _innerSelection.Table.ShowDialog(Table.DialogType.Cell)
        End If
    End Sub

    Public Sub ShowTableDialog(editor As IXHtmlEditor, nmspManager As XmlNamespaceManager) Implements ISelection.ShowTableDialog
        If (_innerEditor IsNot Nothing AndAlso _innerSelection IsNot Nothing) Then
            _innerEditor.CustomDialogs.TableDialog = New DialogEditTableProperties(editor, nmspManager)
            _innerSelection.Table.ShowDialog(Table.DialogType.Table)
        End If
    End Sub

    Public Sub ApplyTag(tagName As String) Implements ISelection.ApplyTag
        If (_innerSelection IsNot Nothing) Then
            _innerSelection.ApplyTag(tagName)
        End If
    End Sub
End Class
