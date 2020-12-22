
Imports System.Xml
Imports System.Linq
Imports System.Text.RegularExpressions
Imports Questify.Builder.UI.Dialogs
Imports Questify.Builder.UI.Dialogs.BusinessLogic
Imports Cito.Tester.Common.WeakEventHandler
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers

Public Class XHtmlEditorCommands

    Private _contentChangedHandlers As IList(Of IWeakGenericEventHandler(Of EventArgs))
    Private ReadOnly _editor As IXHtmlEditor
    Private ReadOnly _defaultNamespaceManager As XmlNamespaceManager
    Private ReadOnly _cntHlp As New HtmlTableContentHelper

    Public Sub New(editor As IXHtmlEditor, ByVal namespaceManager As XmlNamespaceManager)
        _editor = editor
        _defaultNamespaceManager = namespaceManager
    End Sub

    Public Custom Event ContentChanged As EventHandler(Of EventArgs)
        AddHandler(value As EventHandler(Of EventArgs))
            WeakEventUtils.AddWeakGenericEventHandler(_contentChangedHandlers, value, Sub(e) RemoveHandler ContentChanged, e)
        End AddHandler

        RemoveHandler(value As EventHandler(Of EventArgs))
            WeakEventUtils.RemoveWeakGenericEventHandler(_contentChangedHandlers, value)
        End RemoveHandler

        RaiseEvent(sender As Object, e As System.EventArgs)
            If (_contentChangedHandlers IsNot Nothing) Then
                For Each h As IWeakGenericEventHandler(Of EventArgs) In _contentChangedHandlers
                    h.Handler.Invoke(sender, e)
                Next
            End If
        End RaiseEvent
    End Event


    Public Function IsInTable() As Boolean
        Return IsInTable(_editor.Selection.Node)
    End Function
    Private Function IsInTable(ByVal node As XmlNode) As Boolean
        If node IsNot Nothing AndAlso node.Name.ToLower() <> "body" Then
            If node.Name.ToLower() = "table" Then
                Return True
            ElseIf node.ParentNode IsNot Nothing Then
                Return IsInTable(node.ParentNode)
            End If
        End If
        Return False
    End Function

    Public Sub AddTable()
        Try
            _editor.BeginTransaction()
            _editor.ShowNewTableDialog(Control.MousePosition)

            Dim singleList As IEnumerable = _editor.Document.SelectNodes("//def:td[not(@style)]", _defaultNamespaceManager).Cast(Of XmlNode)().Concat(_editor.Document.SelectNodes("//def:tr[not(@style)]", _defaultNamespaceManager).Cast(Of XmlNode)())
            _cntHlp.SetStyleToTableColumns(singleList)
            _editor.CommitTransaction()

            RaiseContentChanged()
        Catch ex As Exception
            MessageBox.Show(String.Format("Failed to insert Table. {0} {1}", vbNewLine, ex.Message), "Error", MessageBoxButtons.OK)
            _editor.RollbackTransaction()
        End Try
    End Sub

    Public Sub DoInsertRowAbove()
        Try
            _editor.Selection.InsertRowAbove()
            Dim singleList As IEnumerable = _editor.Document.SelectNodes("//def:td[not(@style)]", _defaultNamespaceManager).Cast(Of XmlNode)().Concat(_editor.Document.SelectNodes("//def:tr[not(@style)]", _defaultNamespaceManager).Cast(Of XmlNode)())

            SetStyleToTableColumns(singleList)

            RaiseContentChanged()
        Catch ex As Exception
            MessageBox.Show(String.Format("Failed to insert row (above) in table. {0} {1}", vbNewLine, ex.Message), "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Public Sub DoInsertRowBelow()
        Try
            _editor.Selection.InsertRowBelow()
            Dim singleList As IEnumerable = _editor.Document.SelectNodes("//def:td[not(@style)]", _defaultNamespaceManager).Cast(Of XmlNode)().Concat(_editor.Document.SelectNodes("//def:tr[not(@style)]", _defaultNamespaceManager).Cast(Of XmlNode)())

            SetStyleToTableColumns(singleList)

            RaiseContentChanged()
        Catch ex As Exception
            MessageBox.Show(String.Format("Failed to insert row (below) in table. {0} {1}", vbNewLine, ex.Message), "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Public Sub DoDeleteRow()
        Try
            _editor.Selection.DeleteRows()
            RaiseContentChanged()
        Catch ex As Exception
            MessageBox.Show(String.Format("Failed to delete row in table. {0} {1}", vbNewLine, ex.Message), "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Public Sub DoInsertColumnLeft()
        Try
            _editor.Selection.InsertColumnBefore()
            Dim singleList As IEnumerable = _editor.Document.SelectNodes("//def:td[not(@style)]", _defaultNamespaceManager).Cast(Of XmlNode)().Concat(_editor.Document.SelectNodes("//def:tr[not(@style)]", _defaultNamespaceManager).Cast(Of XmlNode)())

            SetStyleToTableColumns(singleList)
        Catch ex As Exception
            MessageBox.Show(String.Format("Failed to insert column (to the left) in table. {0} {1}", vbNewLine, ex.Message), "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Public Sub DoInsertColumnRight()
        Try
            _editor.Selection.InsertColumnAfter()
            Dim singleList As IEnumerable = _editor.Document.SelectNodes("//def:td[not(@style)]", _defaultNamespaceManager).Cast(Of XmlNode)().Concat(_editor.Document.SelectNodes("//def:tr[not(@style)]", _defaultNamespaceManager).Cast(Of XmlNode)())

            SetStyleToTableColumns(singleList)
        Catch ex As Exception
            MessageBox.Show(String.Format("Failed to insert column (to the right) in table. {0} {1}", vbNewLine, ex.Message), "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Public Sub DoDeleteColumn()
        Try
            _editor.Selection.DeleteColumns()
        Catch ex As Exception
            MessageBox.Show(String.Format("Failed to delete column in table. {0} {1}", vbNewLine, ex.Message), "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Public Sub DoMergeSelectedCells()
        Try
            _editor.Selection.MergeCells()
        Catch ex As Exception
            MessageBox.Show(String.Format("Failed to merge selected cells. {0} {1}", vbNewLine, ex.Message), "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Public Sub DoSplitCell()
        Try
            _editor.Selection.SplitCells()
            Dim singleList As IEnumerable = _editor.Document.SelectNodes("//def:td[not(@style)]", _defaultNamespaceManager).Cast(Of XmlNode)().Concat(_editor.Document.SelectNodes("//def:tr[not(@style)]", _defaultNamespaceManager).Cast(Of XmlNode)())

            SetStyleToTableColumns(singleList)
        Catch ex As Exception
            MessageBox.Show(String.Format("Failed to split cells. {0} {1}", vbNewLine, ex.Message), "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Public Sub DoCellProperties()
        _editor.Selection.ShowCellDialog(_editor, _defaultNamespaceManager)
    End Sub

    Public Sub DoCellBorderProperties(handler As HtmlTableHandler)
        Dim style = handler.GetStyleFromSelectedCells()
        Using dialog = New BordersAndShading
            Dim presenter As New BorderAndShadingPresenter(dialog, style)
            dialog.Presenter = presenter
            If dialog.ShowDialog() = DialogResult.OK Then
                handler.ApplyStyleToTable(presenter.Style)
            End If
        End Using
    End Sub

    Public Sub DoCellInnerMargins()
        _editor.Selection.ShowCellInnerMarginDialog(_editor, _defaultNamespaceManager)
    End Sub

    Public Sub DoTableProperties()
        _editor.Selection.ShowTableDialog(_editor, _defaultNamespaceManager)
    End Sub

    Public Sub DoRemoveTable()
        Try
            Dim table As XmlElement = _cntHlp.GetTable(_editor.Selection.Node)
            _editor.ClearSelection()
            table.ParentNode.RemoveChild(table)
        Catch ex As Exception
            MessageBox.Show(String.Format("Failed to remove table. {0} {1}", vbNewLine, ex.Message), "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub SetStyleToTableColumns(ByVal nodes As IEnumerable)
        Try
            _editor.BeginTransaction()
            _cntHlp.SetStyleToTableColumns(nodes)
            _editor.CommitTransaction()
        Catch ex As Exception
            _editor.RollbackTransaction()
        End Try

    End Sub

    Public Sub ConvertListToRomanNumerals()
        doConvertToRomanOrderedList(_editor.Selection.Node)
    End Sub

    Private Sub doConvertToRomanOrderedList(ByVal node As XmlNode)
        If node IsNot Nothing AndAlso node.Name.ToLower() <> "html" Then
            If String.Equals(node.Name, "ul", StringComparison.OrdinalIgnoreCase) OrElse String.Equals(node.Name, "ol", StringComparison.OrdinalIgnoreCase) Then
                DirectCast(node, XmlElement).SetAttribute("Type", "I")
            Else
                doConvertToRomanOrderedList(node.ParentNode)
            End If
        End If
    End Sub

    Public Sub CreateTextBlock()
        If _editor.Selection IsNot Nothing AndAlso Not _editor.Selection.IsTagApplied("span") Then
            Dim span As XmlElement = _editor.Document.CreateElement("span")
            span.SetAttribute("style", "display:inline-block")
            span.InnerText = _editor.Selection
            _editor.Selection.SetXmlElement(span)
        End If
    End Sub

    Function GetStyles(styleSheets As Dictionary(Of String, String)) As Dictionary(Of String, String)
        Dim ret As New Dictionary(Of String, String)
        Dim reg1 As New Regex("([^{]*){([^}]*)}", RegexOptions.Singleline)
        Dim reg2 As New Regex(".UserSR(?<style>\w+)")

        Dim a = (From e In styleSheets Select reg1.Matches(e.Value)).SelectMany(Function(e) e.AsIEnumerable)
        Dim b = From e2 In a Where reg2.IsMatch(e2.Value) Select New KeyValuePair(Of String, String)(reg2.Match(e2.Value).Groups("style").Value, e2.Value)
        For Each kvp In b
            If (Not ret.ContainsKey(kvp.Key)) Then ret.Add(kvp.Key, kvp.Value)
        Next
        Return ret
    End Function

    Function GetLanguages(languages As Dictionary(Of String, String)) As Dictionary(Of String, String)
        Dim ret As New Dictionary(Of String, String)
        Dim reg1 As New Regex("([^{]*){([^}]*)}", RegexOptions.Singleline)
        Dim reg2 As New Regex(".LangTTS(?<language>\w+)")

        Dim a = (From e In languages Select reg1.Matches(e.Value)).SelectMany(Function(e) e.AsIEnumerable)
        Dim b = From e2 In a Where reg2.IsMatch(e2.Value) Select New KeyValuePair(Of String, String)(reg2.Match(e2.Value).Groups("language").Value, e2.Value)
        For Each kvp In b
            If (Not ret.ContainsKey(kvp.Key)) Then
                ret.Add(kvp.Key, kvp.Value)
            End If
        Next
        Return ret
    End Function

    Private Sub RaiseContentChanged()
        RaiseEvent ContentChanged(Me, EventArgs.Empty)
    End Sub

End Class
