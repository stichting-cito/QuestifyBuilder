
Imports System.Collections.Specialized
Imports System.ComponentModel
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Windows.Forms
Imports System.Xml
Imports C1.Win.C1Editor
Imports Questify.Builder.Logic.ContentModel
Imports Cito.Tester.Common
Imports Cito.Tester.Common.WeakEventHandler
Imports Cito.Tester.ContentModel
Imports NLog
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.PluginExtensibility.Html
Imports Questify.Builder.Logic.Service.Classes
Imports Questify.Builder.Logic.Service.Interfaces.UI
Imports Questify.Builder.UI
Imports Questify.Builder.UI.Commanding
Imports Questify.Builder.Logic.PluginExtensibility.Html.Converters
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers
Imports Questify.Builder.UI.PresentationControls.TextEditor.Handlers

Public Class XHtmlEditor
    Implements IDisposable
    Implements IXHtmlEditor

    Const DEFAULTHTML As String = "<html><body></body></html>"
    Const CLASS_ATTRIBUTE As String = "class"

    Private _suppressRaiseContentChangedOnDocumentChanged As Boolean = False
    Private _raiseContentChangedOnFirstKeyDown As Nullable(Of Boolean)
    Friend Shared InstanceCount As Integer = 0

    Private _currentHtmlValue As String
    Private _currentStyle As String
    Private _currentLanguage As String
    Private _currentSelection As ISelection
    Private _formClosing As Boolean = False
    Private _lastCopiedInlineElements As List(Of InlineElement) = Nothing
    Private _mouseFocused As Boolean = False

    Private _stylesheets As Dictionary(Of String, String)
    Private _userStyles As Dictionary(Of String, String)
    Private _editorCss As String = String.Empty

    Private _behavior As IInlineHtmlEditBehavior
    Private _htmlEditorBehaviour As IHtmlEditorBehaviour

    Private _xhtmlEditorCommands As XHtmlEditorCommands
    Private _htmlReferencesHandler As HtmlReferencesHandler
    Private _htmlFormulaHandler As HtmlFormulaHandler
    Private ReadOnly _htmlTableHandler As HtmlTableHandler

    Private _insPic As DelegateCommand(Of Boolean)
    Private _insMov As DelegateCommand(Of Boolean)
    Private _insAud As DelegateCommand(Of Boolean)
    Private _insCi As DelegateCommand(Of Boolean)
    Private _insPop As DelegateCommand(Of Boolean)

    Private _htmlInlineHandler As HtmlInlineHandler
    Private _imageHtmlInlineHandler As HtmlInlineHandler
    Private _audioHtmlInlineHandler As HtmlInlineHandler
    Private _videoHtmlInlineHandler As HtmlInlineHandler

    Private _customInteractionHtmlInlineHandler As HtmlInlineHandler
    Private _popupHtmlInlineHandler As HtmlInlineHandler

    Private ReadOnly _cntHlp As New HtmlContentHelper

    Private _contentChangedHandlers As IList(Of IWeakGenericEventHandler(Of EventArgs))
    Private _inlineChangedHandlers As IList(Of IWeakGenericEventHandler(Of NotifyCollectionChangedEventArgs))

    Public Event AddedInlineCustomInteraction As EventHandler(Of InlineElementEventArgs) Implements IXHtmlEditor.AddedInlineCustomInteraction
    Public Event RemovedInlineCustomInteraction As EventHandler(Of InlineElementEventArgs) Implements IXHtmlEditor.RemovedInlineCustomInteraction
    Public Event AddedInlineAspect As EventHandler(Of InlineElementEventArgs) Implements IXHtmlEditor.AddedInlineAspect
    Public Event RemovedInlineAspect As EventHandler(Of InlineElementEventArgs) Implements IXHtmlEditor.RemovedInlineAspect

    Public Sub New()
        Interlocked.Increment(InstanceCount)

        InitializeComponent()
        C1Editor1.KeyboardShortcutsEnabled = False

        _htmlTableHandler = New HtmlTableHandler(Me)

        DefaultNamespaceManager = GetNamespaceManager()
        AddHandler DirectCast(cbStyle.Control, ComboBox).SelectionChangeCommitted, AddressOf HandleCssCombo
        AddHandler DirectCast(cbLanguage.Control, ComboBox).SelectionChangeCommitted, AddressOf HandleLanguageCombo

        AddHandler bBold.CheckedChanged, Sub(s, o) RaiseEvent IsButtonCheckedChanged(Me, New ButtonCheckChangedEventArgs(Questify.Builder.Logic.Service.Interfaces.UI.Button.BOLD, bBold.Checked))
        AddHandler bItalic.CheckedChanged, Sub(s, o) RaiseEvent IsButtonCheckedChanged(Me, New ButtonCheckChangedEventArgs(Questify.Builder.Logic.Service.Interfaces.UI.Button.ITALIC, bItalic.Checked))
        AddHandler bUnderLine.CheckedChanged, Sub(s, o) RaiseEvent IsButtonCheckedChanged(Me, New ButtonCheckChangedEventArgs(Questify.Builder.Logic.Service.Interfaces.UI.Button.UNDERLINE, bUnderLine.Checked))
        AddHandler bSuperScript.CheckedChanged, Sub(s, o) RaiseEvent IsButtonCheckedChanged(Me, New ButtonCheckChangedEventArgs(Questify.Builder.Logic.Service.Interfaces.UI.Button.SUPERSCRIPT, bSuperScript.Checked))
        AddHandler bSubscript.CheckedChanged, Sub(s, o) RaiseEvent IsButtonCheckedChanged(Me, New ButtonCheckChangedEventArgs(Questify.Builder.Logic.Service.Interfaces.UI.Button.SUBSCRIPT, bSubscript.Checked))
        AddHandler bStrikeThrough.CheckedChanged, Sub(s, o) RaiseEvent IsButtonCheckedChanged(Me, New ButtonCheckChangedEventArgs(Questify.Builder.Logic.Service.Interfaces.UI.Button.STRIKETHROUGH, bStrikeThrough.Checked))
        AddHandler bAlignLeft.CheckedChanged, Sub(s, o) RaiseEvent IsButtonCheckedChanged(Me, New ButtonCheckChangedEventArgs(Questify.Builder.Logic.Service.Interfaces.UI.Button.ALIGNLEFT, bAlignLeft.Checked))
        AddHandler bAlignCenter.CheckedChanged, Sub(s, o) RaiseEvent IsButtonCheckedChanged(Me, New ButtonCheckChangedEventArgs(Questify.Builder.Logic.Service.Interfaces.UI.Button.ALIGNMIDDLE, bAlignCenter.Checked))
        AddHandler bAlignRight.CheckedChanged, Sub(s, o) RaiseEvent IsButtonCheckedChanged(Me, New ButtonCheckChangedEventArgs(Questify.Builder.Logic.Service.Interfaces.UI.Button.ALIGNRIGHT, bAlignRight.Checked))
        AddHandler bBulletList.CheckedChanged, Sub(s, o) RaiseEvent IsButtonCheckedChanged(Me, New ButtonCheckChangedEventArgs(Questify.Builder.Logic.Service.Interfaces.UI.Button.MAKEBULLETED, bBulletList.Checked))
        AddHandler C1Editor1.SelectionChanged, AddressOf SelectionChanged

        AddHandler bNumberedList.CheckedChanged, AddressOf CheckListButtonStates
        AddHandler C1Editor1.SelectionChanged, AddressOf CheckListButtonStates
        AddHandler C1Editor1.DoubleClick, AddressOf C1Editor1_DoubleClick
        AddHandler C1Editor1.MouseUp, AddressOf C1Editor1_MouseUp

        C1Editor1.Margin = New Padding(0)

    End Sub

    <DllImport("user32.dll")>
    Public Shared Sub mouse_event(dwFlags As Integer, dx As Integer, dy As Integer, cButtons As Integer, dwExtraInfo As Integer)
    End Sub

    <DllImport("user32.dll")>
    Public Shared Function SetCursorPos(x As Integer, y As Integer) As Long
    End Function

    <DllImport("user32.dll")>
    Private Shared Function GetCursorPos(ByRef lpPoint As Point) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    Public Const MOUSEEVENTF_LEFTDOWN As Integer = &H2
    Public Const MOUSEEVENTF_LEFTUP As Integer = &H4

    Public Sub InitCaret(x As Integer, y As Integer, isMouseFocused As Boolean) Implements IXHtmlEditor.InitCaret
        If _formClosing Then
            Return
        End If

        Dim t = New Thread(Sub()
                               If _formClosing OrElse C1Editor1 Is Nothing Then
                                   Return
                               End If
                               Try
                                   C1Editor1.Invoke(Sub()
                                                        Dim currentPoint = New Point()
                                                        Dim toolstripVisible As Boolean = (_htmlEditorBehaviour IsNot Nothing AndAlso _htmlEditorBehaviour.IsToolstripVisible)
                                                        Dim compensateY As Integer = 0
                                                        GetCursorPos(currentPoint)
                                                        If toolstripVisible Then
                                                            compensateY = 35
                                                        End If
                                                        Dim caretPoint = IIf(isMouseFocused, New Point(currentPoint.X, currentPoint.Y + compensateY), New Point(x, y - compensateY))
                                                        Dim screenPoint = C1Editor1.PointToScreen(caretPoint)
                                                        If Not isMouseFocused Then
                                                            SetCursorPos(screenPoint.X, screenPoint.Y)
                                                        ElseIf toolstripVisible Then
                                                            SetCursorPos(caretPoint.X, caretPoint.Y)
                                                        End If
                                                        mouse_event(MOUSEEVENTF_LEFTDOWN, caretPoint.X, caretPoint.Y, 0, 0)
                                                        mouse_event(MOUSEEVENTF_LEFTUP, caretPoint.X, caretPoint.Y, 0, 0)
                                                        SetCursorPos(currentPoint.X, currentPoint.Y)
                                                    End Sub)
                               Catch ex As Exception

                               End Try
                           End Sub)
        t.Start()
    End Sub

    Public Sub SetFocus() Implements IXHtmlEditor.SetFocus
        If C1Editor1 IsNot Nothing Then
            C1Editor1.Focus()
        End If
    End Sub

    Public Function CreateRange(start As Integer, length As Integer) As ITextRange Implements IXHtmlEditor.CreateRange
        If C1Editor1 Is Nothing Then
            Return Nothing
        End If
        Dim fromRange = C1Editor1.CreateRange(start, length)
        Dim range As XhtmlTextRange = New XhtmlTextRange(C1Editor1)
        Return range
    End Function

    Public Custom Event ContentChanged As EventHandler(Of EventArgs) Implements IXHtmlEditor.ContentChanged
        AddHandler(value As EventHandler(Of EventArgs))
            AddWeakGenericEventHandler(_contentChangedHandlers, value, Sub(e) RemoveHandler ContentChanged, e)
        End AddHandler

        RemoveHandler(value As EventHandler(Of EventArgs))
            RemoveWeakGenericEventHandler(_contentChangedHandlers, value)
        End RemoveHandler

        RaiseEvent(sender As Object, e As EventArgs)
            If (_contentChangedHandlers IsNot Nothing) Then
                For Each h As IWeakGenericEventHandler(Of EventArgs) In _contentChangedHandlers
                    h.Handler.Invoke(sender, e)
                Next
            End If
        End RaiseEvent
    End Event

    Friend Sub RaiseContentChanged(sender As Object, e As EventArgs)
        RaiseEvent ContentChanged(sender, e)
    End Sub

    Public Custom Event InlineElementsCollectionChanged As EventHandler(Of NotifyCollectionChangedEventArgs) Implements IXHtmlEditor.InlineElementsCollectionChanged
        AddHandler(value As EventHandler(Of NotifyCollectionChangedEventArgs))
            AddWeakGenericEventHandler(_inlineChangedHandlers, value, Sub(e) RemoveHandler InlineElementsCollectionChanged, e)
        End AddHandler

        RemoveHandler(value As EventHandler(Of NotifyCollectionChangedEventArgs))
            RemoveWeakGenericEventHandler(_inlineChangedHandlers, value)
        End RemoveHandler

        RaiseEvent(sender As Object, e As NotifyCollectionChangedEventArgs)
            If (_inlineChangedHandlers IsNot Nothing) Then
                For Each h As IWeakGenericEventHandler(Of NotifyCollectionChangedEventArgs) In _inlineChangedHandlers
                    h.Handler.Invoke(sender, e)
                Next
            End If
        End RaiseEvent
    End Event

    Public Sub SetHtmlValue(behavior As IHtmlEditorBehaviour) Implements IXHtmlEditor.SetHtmlValue
        If _formClosing Then
            Return
        End If
        Dim behaviorWasNull = _behavior Is Nothing
        If (TypeOf behavior Is IInlineHtmlEditBehavior) Then
            _behavior = DirectCast(behavior, IInlineHtmlEditBehavior)
        Else
            _behavior = New EmptyInlineHtmlEditBehavior(behavior)
        End If
        If (TypeOf behavior Is IHtmlEditorBehaviour) Then
            _htmlEditorBehaviour = behavior
        End If
        UpdateControlsToReflectAvailableBehavior()

        If (behaviorWasNull AndAlso _behavior IsNot Nothing) Then
            BindCommands()
        End If

        _stylesheets = _behavior.GetStyle()

        Dim html As String = behavior.GetHtml()
        _xhtmlEditorCommands = New XHtmlEditorCommands(Me, DefaultNamespaceManager)
        _htmlReferencesHandler = _behavior.CreateHtmlReferencesHandler(Me)
        _htmlFormulaHandler = behavior.CreateFormulaHandler(Me)
        InitCss(_stylesheets)
        InitLanguages(_stylesheets)

        AddHandler _xhtmlEditorCommands.ContentChanged, Sub(s, e) RaiseContentChanged(s, e)
        If _htmlReferencesHandler IsNot Nothing Then
            AddHandler _htmlReferencesHandler.ActiveReferenceChanged, AddressOf HtmlreferencesHandler_ActiveReferenceChanged

            _currentHtmlValue = If(String.IsNullOrEmpty(html), DEFAULTHTML, html)

            If _formClosing Then
                Return
            End If
            If C1Editor1 IsNot Nothing AndAlso Not C1Editor1.IsDisposed AndAlso Not Disposing Then
                C1Editor1.Font = DefaultFont
                Try
                    C1Editor1.LoadXml(_currentHtmlValue, Nothing)
                Catch ex As Exception
                End Try
            End If
            _htmlReferencesHandler.LoadReferences()
        End If
        Me.SetFocus()
    End Sub

    Friend ReadOnly Property IsInline As Boolean Implements IXHtmlEditor.IsInline
        Get
            If Me.Selection.Node IsNot Nothing AndAlso
               Me.Selection.Node.Attributes IsNot Nothing AndAlso
               Me.Selection.Node.Attributes.Count > 0 Then
                Dim id As String = Me.Selection.Node.Attributes("id").Value
                Return _behavior.InlineElements.ContainsKey(id)
            End If
            Return False
        End Get

    End Property

    Private Function InlineElementIsCustomInteraction(inlineElement As InlineElement) As Boolean
        If Not String.IsNullOrEmpty(inlineElement.LayoutTemplateSourceName) AndAlso inlineElement.LayoutTemplateSourceName.Equals(_behavior.InlineCustomInteractionTemplate, StringComparison.InvariantCultureIgnoreCase) Then
            Return True
        End If
        Return False
    End Function

    Private Function InlineElementIsAspect(inlineElement As InlineElement) As Boolean
        Return inlineElement.Parameters.Any(Function(ps) ps.InnerParameters.Any(Function(p) TypeOf p Is AspectScoringParameter))
    End Function

    Private Function SelectedInlineElements() As List(Of InlineElement)
        Dim result As New List(Of InlineElement)
        If Me.Selection.Node IsNot Nothing Then
            Dim selectedInlines = Me.Selection.Node.SelectNodes("//img[@isinlineelement='true' or @isinlinecontrol='true'] | //def:img[@isinlineelement='true' or @isinlinecontrol='true']", DefaultNamespaceManager)
            Dim allInlineElements = _behavior.InlineElements

            For Each selected As XmlNode In selectedInlines
                Dim id = selected.Attributes("id").Value
                If allInlineElements.ContainsKey(id) Then
                    result.Add(allInlineElements(id).Item1)
                End If
            Next
        End If

        Return result
    End Function

    Public Sub UpdateValue() Implements IXHtmlEditor.UpdateValue
        If _formClosing OrElse Me.Document Is Nothing Then
            Return
        End If
        Dim node As XmlNode = Me.Document.SelectSingleNode("//def:html", DefaultNamespaceManager)
        If node IsNot Nothing Then
            _behavior.SetHtml(node.OuterXml)
        End If
    End Sub

    Private Sub BindCommands()
        If _formClosing Then
            Return
        End If
        Dim pasteFromWord As New DelegateCommand(Of Boolean)(bPasteFromWord.Text, Sub() DoPasteOperation(False), Function() C1Editor1.CanPaste)
        Dim pasteAsText As New DelegateCommand(Of Boolean)(bPasteFromWord.Text, Sub() DoPasteOperation(True), Function() C1Editor1.CanPasteAsText)
        Dim copyCmd As New DelegateCommand(Of Boolean)(bCopy.Text, Sub() Copy(), Function() C1Editor1.CanCopy)
        Dim cutCmd As New DelegateCommand(Of Boolean)(bCut.Text, Sub() Cut(), Function() C1Editor1.CanCut)

        Dim insControl As New DelegateCommand(Of Boolean)(bInsertControl.Text, Sub() ExecuteInline(InlineHandler), Function() _behavior.CanInsertControls AndAlso InlineHandler.CanExecute())

        _insPic = New DelegateCommand(Of Boolean)(bImage.Text, Sub() ExecuteInline(ImageHtmlInlineHandler), Function() _behavior.CanInsertImages AndAlso ImageHtmlInlineHandler.CanExecute())
        _insMov = New DelegateCommand(Of Boolean)(bMovie.Text, Sub() ExecuteInline(VideoHtmlInlineHandler), Function() _behavior.CanInsertMovies AndAlso VideoHtmlInlineHandler.CanExecute())
        _insAud = New DelegateCommand(Of Boolean)(bAudio.Text, Sub() ExecuteInline(AudioHtmlInlineHandler), Function() _behavior.CanInsertAudio AndAlso AudioHtmlInlineHandler.CanExecute())
        _insCi = New DelegateCommand(Of Boolean)(bCustomInteraction.Text, Sub() ExecuteInline(CustomInteractionHtmlInlineHandler), Function() _behavior.CanInsertCI AndAlso CustomInteractionHtmlInlineHandler.CanExecute())
        If PopupHtmlInlineHandler IsNot Nothing Then
            _insPop = New DelegateCommand(Of Boolean)(bPopup.Text, Sub() ExecuteInline(PopupHtmlInlineHandler), Function() _behavior.CanInsertPopup AndAlso PopupHtmlInlineHandler.CanExecute())
        End If

        Dim insTbl As New DelegateCommand(Of Boolean)(bTable.Text, Sub() _xhtmlEditorCommands.AddTable(), Function() C1Editor1.CanSelect)
        Dim lckImg As New DelegateCommand(Of Boolean)(bLockEditImg.Text, Sub() _xhtmlEditorCommands.CreateTextBlock(), Function() True)
        Dim convertToRomanImg As New DelegateCommand(Of Boolean)(bConvertToRoman.Text, Sub() _xhtmlEditorCommands.ConvertListToRomanNumerals(), Function() True)
        Dim insertSymbolImg As New DelegateCommand(Of Boolean)(bInsertSymbol.Text, Sub() OpenSymbolDialog(), Function() True)
        Dim insertFormulaImg As New DelegateCommand(Of Boolean)(bInsertFormula.Text, Sub() EditMathFormula(), Function() _behavior.CanInsertFormula)
        Dim fitToContentsImg As New DelegateCommand(Of Boolean)(bFitToContents.Text, Sub() FitToContents(), Function() True)

        Dim overviewReferencesImg As New DelegateCommand(Of Boolean)(tsmiOverviewReferences.Text, Sub() OverviewReferences(), Function() True)
        Dim insertElementReferenceImg As New DelegateCommand(Of Boolean)(tsmiElementReference.Text, Sub() _htmlReferencesHandler.InsertElementReference(), Function() True)
        Dim insertSymbolReferenceImg As New DelegateCommand(Of Boolean)(tsmiSymbolReference.Text, Sub() _htmlReferencesHandler.InsertSymbolOrHighlightReference(XhtmlReferenceType.Symbol), Function() True)
        Dim insertHighlightReferenceImg As New DelegateCommand(Of Boolean)(tsmiHighlightReference.Text, Sub() _htmlReferencesHandler.InsertSymbolOrHighlightReference(XhtmlReferenceType.Highlight), Function() True)
        Dim removeReferenceImg As New DelegateCommand(Of Boolean)(tsmiRemoveReference.Text, Sub() _htmlReferencesHandler.RemoveReference(), Function() True)
        Dim insertReferenceImg As New DelegateCommand(Of Boolean)(tsbReferTo.Text, Sub() _htmlReferencesHandler.DoReferToToolStripButton(), Function() ActiveReference IsNot Nothing)

        Dim muteTTS As New DelegateCommand(Of Boolean)(bTTSMute.Text, Sub() MuteTextToSpeech(), Function() _behavior.CanSetTextToSpeechOptions)
        Dim alternativeTTS As New DelegateCommand(Of Boolean)(bTTSAlternative.Text, Sub() AlternativeTextToSpeech(), Function() _behavior.CanSetTextToSpeechOptions)
        Dim deleteTTS As New DelegateCommand(Of Boolean)(bTTSDelete.Text, Sub() RemoveTextToSpeech(), Function() _behavior.CanSetTextToSpeechOptions)

        CmdManager.Bind(pasteFromWord, bPasteFromWord)
        CmdManager.Bind(pasteAsText, bPasteAsText)
        CmdManager.Bind(copyCmd, bCopy)
        CmdManager.Bind(cutCmd, bCut)

        CmdManager.Bind(insControl, bInsertControl)
        CmdManager.Bind(_insPic, bImage)
        CmdManager.Bind(_insMov, bMovie)
        CmdManager.Bind(_insAud, bAudio)
        CmdManager.Bind(_insCi, bCustomInteraction)
        CmdManager.Bind(insTbl, bTable)
        CmdManager.Bind(lckImg, bLockEditImg)
        CmdManager.Bind(convertToRomanImg, bConvertToRoman)
        CmdManager.Bind(insertFormulaImg, bInsertFormula)
        CmdManager.Bind(fitToContentsImg, bFitToContents)
        CmdManager.Bind(overviewReferencesImg, tsmiOverviewReferences)
        CmdManager.Bind(insertElementReferenceImg, tsmiElementReference)
        CmdManager.Bind(insertSymbolReferenceImg, tsmiSymbolReference)
        CmdManager.Bind(insertSymbolImg, bInsertSymbol)
        CmdManager.Bind(insertHighlightReferenceImg, tsmiHighlightReference)
        CmdManager.Bind(removeReferenceImg, tsmiRemoveReference)
        CmdManager.Bind(insertReferenceImg, tsbReferTo)

        CmdManager.Bind(muteTTS, bTTSMute)
        CmdManager.Bind(alternativeTTS, bTTSAlternative)
        CmdManager.Bind(deleteTTS, bTTSDelete)

        For Each duration In PauseDuration.FromConfig
            bTTSPause.DropDownItems.Add(duration.Name, Nothing, Sub() PauseTextToSpeech(duration.Duration))
        Next

        Dim inline As New DelegateCommand(Of Boolean)(tsmInline.Text, Sub() DoInlineProperties(), Function() IsInline)
        If _formClosing Then
            Return
        End If
        Dim editFormula As New DelegateCommand(Of Boolean)(tsmEditFormula.Text, Sub() EditMathFormula(), Function() _htmlFormulaHandler.IsMathMLImage)

        Dim removeRef As New DelegateCommand(Of Boolean)(tsmRemoveReference.Text, Sub() _htmlReferencesHandler.RemoveReference(), Function() _htmlReferencesHandler.IsReference())
        Dim insertRowAbove As New DelegateCommand(Of Boolean)(tsmInsertRowAbove.Text, Sub() _xhtmlEditorCommands.DoInsertRowAbove(), Function() _xhtmlEditorCommands.IsInTable())
        Dim insertRowBelow As New DelegateCommand(Of Boolean)(tsmInsertRowBelow.Text, Sub() _xhtmlEditorCommands.DoInsertRowBelow(), Function() _xhtmlEditorCommands.IsInTable())
        Dim deleteRow As New DelegateCommand(Of Boolean)(tsmDeleteRow.Text, Sub() _xhtmlEditorCommands.DoDeleteRow(), Function() _xhtmlEditorCommands.IsInTable())
        Dim insertColumnLeft As New DelegateCommand(Of Boolean)(tsmInsertColumnLeft.Text, Sub() _xhtmlEditorCommands.DoInsertColumnLeft(), Function() _xhtmlEditorCommands.IsInTable())
        Dim insertColumnRight As New DelegateCommand(Of Boolean)(tsmInsertColumnRight.Text, Sub() _xhtmlEditorCommands.DoInsertColumnRight(), Function() _xhtmlEditorCommands.IsInTable())
        Dim deleteColumn As New DelegateCommand(Of Boolean)(tsmDeleteColumn.Text, Sub() _xhtmlEditorCommands.DoDeleteColumn(), Function() _xhtmlEditorCommands.IsInTable())
        Dim mergeSelectedCells As New DelegateCommand(Of Boolean)(tsmMergeSelectedCells.Text, Sub() If (Not Me._htmlTableHandler.MergeCells()) Then MessageBox.Show(My.Resources.Resources.XhtmlEditor_MergeCellError), Function() _xhtmlEditorCommands.IsInTable())
        Dim splitCellh As New DelegateCommand(Of Boolean)(tsmSplitCell.Text, Sub() _htmlTableHandler.SplitCellHorizontal(), Function() _htmlTableHandler.IsSingleCellSelected)
        Dim splitCellv As New DelegateCommand(Of Boolean)(tsmSplitCellVert.Text, Sub() _htmlTableHandler.SplitCellVertical(), Function() _htmlTableHandler.IsSingleCellSelected)
        Dim cellProperties As New DelegateCommand(Of Boolean)(tsmCellProperties.Text, Sub() _xhtmlEditorCommands.DoCellProperties(), Function() _xhtmlEditorCommands.IsInTable())
        Dim cellBorderProperties As New DelegateCommand(Of Boolean)(tsmCellBorders.Text, Sub() _xhtmlEditorCommands.DoCellBorderProperties(_htmlTableHandler), Function() _xhtmlEditorCommands.IsInTable())
        Dim cellInnerMargins As New DelegateCommand(Of Boolean)(tsmCellInnerMargins.Text, Sub() _xhtmlEditorCommands.DoCellInnerMargins(), Function() _xhtmlEditorCommands.IsInTable())
        Dim tableProperties As New DelegateCommand(Of Boolean)(tsmTableProperties.Text, Sub() _xhtmlEditorCommands.DoTableProperties(), Function() _xhtmlEditorCommands.IsInTable())
        Dim removeTable As New DelegateCommand(Of Boolean)(tsmRemoveTable.Text, Sub() _xhtmlEditorCommands.DoRemoveTable(), Function() _xhtmlEditorCommands.IsInTable())

        CmdManager.Bind(inline, tsmInline)
        CmdManager.Bind(editFormula, tsmEditFormula)

        CmdManager.Bind(removeRef, tsmRemoveReference)
        CmdManager.Bind(insertRowAbove, tsmInsertRowAbove)
        CmdManager.Bind(insertRowBelow, tsmInsertRowBelow)
        CmdManager.Bind(deleteRow, tsmDeleteRow)
        CmdManager.Bind(insertColumnLeft, tsmInsertColumnLeft)
        CmdManager.Bind(insertColumnRight, tsmInsertColumnRight)
        CmdManager.Bind(deleteColumn, tsmDeleteColumn)
        CmdManager.Bind(mergeSelectedCells, tsmMergeSelectedCells)
        CmdManager.Bind(splitCellh, tsmSplitCell)
        CmdManager.Bind(splitCellv, tsmSplitCellVert)
        CmdManager.Bind(cellProperties, tsmCellProperties)
        CmdManager.Bind(cellBorderProperties, tsmCellBorders)
        CmdManager.Bind(cellInnerMargins, tsmCellInnerMargins)
        CmdManager.Bind(tableProperties, tsmTableProperties)
        CmdManager.Bind(removeTable, tsmRemoveTable)
    End Sub

    Private Sub FitToContents()
        RaiseEvent ContentChanged(Me, EventArgs.Empty)
    End Sub

    Private Sub EditMathFormula()
        If _formClosing Then
            Return
        End If
        XhtmlDialogs.OpenMathFormulaDialog(Me, _htmlFormulaHandler, GetSrcOfSelectedElement(), Font, Me.Location, ParentForm)
        SetFocus()
    End Sub

    Public Sub OpenSymbolDialog() Implements IXHtmlEditor.OpenSymbolDialog
        If _formClosing Then
            Return
        End If
        XhtmlDialogs.OpenSymbolDialog(Me, Me.Selection, Me.Location)
    End Sub

    Private Function GetSrcOfSelectedElement() As String
        If Me.Selection.Length <> 0 Then
            Dim n As XmlNode = Me.Selection.Node
            Dim srcAttribute As XmlAttribute = If(n.Attributes Is Nothing, Nothing, n.Attributes("src"))

            If srcAttribute IsNot Nothing Then
                Return srcAttribute.Value
            End If
        End If

        Return String.Empty
    End Function

    Public Sub AddNodeAfterCurrentNode(newNode As XmlNode, isNew As Boolean) Implements IXHtmlEditor.AddNodeAfterCurrentNode
        XhtmlText.AddNodeAfterCurrentNode(Me, newNode, isNew)
    End Sub

    Private Function ShouldAddParagraphBeforeTable() As Boolean
        If Me.Selection IsNot Nothing AndAlso C1Editor1.Selection.Table IsNot Nothing AndAlso Me.Selection.Length = 0 AndAlso C1Editor1.Selection.Start.Offset = 0 Then
            Dim node = Me.Selection.Node
            Dim parentNode = GetParentNodeOfType(node, "p")
            If parentNode Is Nothing Then Return False

            node = parentNode
            If Not GetParentAndCheckFirstChild(node, "td", parentNode) Then
                Return False
            End If

            node = parentNode
            If Not GetParentAndCheckFirstChild(node, "tr", parentNode) Then
                Return False
            End If

            node = parentNode
            If Not GetParentAndCheckFirstChild(node, "tbody", parentNode) Then
                Return False
            End If

            node = parentNode
            parentNode = GetParentNodeOfType(node, "table")
            If parentNode Is Nothing Then
                Return False
            End If

            Dim bodyNode = Me.Document.SelectSingleNode("//def:body", DefaultNamespaceManager)
            If bodyNode Is Nothing OrElse Not bodyNode.FirstChild.Equals(parentNode) Then
                Return False
            End If

            Return True
        End If
    End Function

    Private Function ShouldRemoveParagraphBeforeTable() As XmlNode
        If Me.Selection IsNot Nothing Then
            If NodeIsEmptyAndNextNodeIsTable(Me.Selection.Node) Then
                Return Me.Selection.Node
            End If
            Dim paragraph = GetParentNodeOfType(Me.Selection.Node, "p")
            If paragraph IsNot Nothing AndAlso NodeIsEmptyAndNextNodeIsTable(paragraph) Then
                Return paragraph
            End If
        End If
        Return Nothing
    End Function

    Private Function GetParentAndCheckFirstChild(node As XmlNode, parentType As String, ByRef parentNode As XmlNode) As Boolean
        parentNode = GetParentNodeOfType(node, parentType)
        If parentNode Is Nothing Then
            Return False
        End If
        If Not parentNode.FirstChild.Equals(node) Then
            Return False
        End If
        Return True
    End Function

    Private Function GetParentNodeOfType(node As XmlNode, parentType As String) As XmlNode
        If node.ParentNode Is Nothing Then
            Return Nothing
        End If

        If node.ParentNode.Name.Equals(parentType, StringComparison.InvariantCultureIgnoreCase) Then
            Return node.ParentNode
        Else
            Return GetParentNodeOfType(node.ParentNode, parentType)
        End If
    End Function

    Private Function NodeIsEmptyAndNextNodeIsTable(node As XmlNode) As Boolean
        Return node.Name.Equals("p", StringComparison.InvariantCultureIgnoreCase) AndAlso String.IsNullOrWhiteSpace(node.InnerText) AndAlso
               node.NextSibling IsNot Nothing AndAlso node.NextSibling.Name.Equals("table", StringComparison.InvariantCultureIgnoreCase)
    End Function

    Private Sub AddParagraphBeforeTable()
        Try
            Me.BeginTransaction()
            Dim body = Me.Document.SelectSingleNode("//def:body", DefaultNamespaceManager)
            Dim newParagraph = body.OwnerDocument.CreateElement("p", body.NamespaceURI)
            body.InsertBefore(newParagraph, body.FirstChild)
            Me.CommitTransaction()
        Catch ex As NullReferenceException
            Dim _logger = LogManager.GetCurrentClassLogger()
            _logger.Log(LogLevel.Error, String.Format("IxHtmlEditor - AddParagraphBeforeTable", ex, Me.GetType()))
        End Try
    End Sub

    Private Sub RemoveParagraphBeforeTable(nodeToDelete As XmlNode)
        Try
            Me.BeginTransaction()
            nodeToDelete.ParentNode.RemoveChild(nodeToDelete)
            Me.CommitTransaction()
        Catch ex As NullReferenceException
            Dim _logger = LogManager.GetCurrentClassLogger()
            _logger.Log(LogLevel.Error, String.Format("IxHtmlEditor - RemoveParagraphBeforeTable", ex, Me.GetType()))
        End Try
    End Sub

    Private Sub InitCss(stylesheets As Dictionary(Of String, String))
        cbStyle.Items.Clear() : cbStyle.Items.Add(String.Empty) : cbStyle.SelectedIndex = 0
        _userStyles = _xhtmlEditorCommands.GetStyles(stylesheets) : cbStyle.Items.AddRange(_userStyles.Keys.ToArray())
        _editorCss = String.Join(Environment.NewLine, stylesheets.Keys.Select(Function(e) stylesheets(e)).ToArray())
        If String.IsNullOrEmpty(_editorCss) Then Return
        Using stream As Stream = New MemoryStream(Encoding.UTF8.GetBytes(_editorCss))
            C1Editor1.LoadDesignCSS(stream)
        End Using
    End Sub

    Private Sub InitLanguages(stylesheets As Dictionary(Of String, String))
        cbLanguage.Items.Clear() : cbLanguage.Items.Add(String.Empty) : cbLanguage.SelectedIndex = 0
        Dim languages As Dictionary(Of String, String) = _xhtmlEditorCommands.GetLanguages(stylesheets) : cbLanguage.Items.AddRange(languages.Keys.ToArray())
    End Sub

    Private Function DoDropOperation(ByVal html As String) As Dictionary(Of String, String)
        Dim droppedInlineElements As New Dictionary(Of String, String)

        Dim htmlHandler = _behavior.CreateForPasting(Me)
        AddHandler htmlHandler.InlineElementAdded, AddressOf HtmlHandler_InlineElementAdded
        AddHandler htmlHandler.ResourceAdded, AddressOf HtmlHandler_ResourceAdded

        htmlHandler.PerformDropOperation(html, droppedInlineElements)

        RemoveHandler htmlHandler.InlineElementAdded, AddressOf HtmlHandler_InlineElementAdded
        RemoveHandler htmlHandler.ResourceAdded, AddressOf HtmlHandler_ResourceAdded

        Return droppedInlineElements
    End Function

    Friend Sub DoPasteOperation(ByVal forcePlainTextPaste As Boolean)
        Dim caretPosition = C1Editor1.SelectionStart
        Dim pasteLength = Clipboard.GetText.Length

        Dim htmlHandler As HtmlHandlerBase
        htmlHandler = _behavior.CreateForPasting(Me)

        If Not forcePlainTextPaste Then
            AddHandler htmlHandler.InlineElementAdded, AddressOf HtmlHandler_InlineElementAdded
            AddHandler htmlHandler.ResourceAdded, AddressOf HtmlHandler_ResourceAdded
        End If

        htmlHandler.PerformPasteOperation(forcePlainTextPaste, Me._lastCopiedInlineElements)
        Thread.Sleep(500)

        If Not forcePlainTextPaste Then
            RemoveHandler htmlHandler.InlineElementAdded, AddressOf HtmlHandler_InlineElementAdded
            RemoveHandler htmlHandler.ResourceAdded, AddressOf HtmlHandler_ResourceAdded
        End If

        RaiseContentChanged(Me, EventArgs.Empty)

        SetHtmlValue(_behavior)
        Const NOTHING_SELECTED_LENGTH = 0
        C1Editor1.Select(caretPosition + pasteLength, NOTHING_SELECTED_LENGTH)
    End Sub

    Private Sub HtmlHandler_InlineElementAdded(sender As Object, e As InlineElementEventArgs)
        DoAddInline(e.InlineElement)
    End Sub

    Private Sub HtmlHandler_InlineElementAddedAndFocused(sender As Object, e As InlineElementEventArgs)
        DoAddInline(e.InlineElement)
        SetFocus()
    End Sub

    Private Sub HtmlHandler_ResourceAdded(sender As Object, e As ResourceNameEventArgs)
        If (TypeOf Me.Parent Is XHtmlParameterEditorControl2) Then
            Dim p = DirectCast(Me.Parent, XHtmlParameterEditorControl2)
            p.AddDependentResource(e.ResourceName)
        Else
            Dim parent = GetReparentHtmlEditor(Me)
            If parent IsNot Nothing Then
                _behavior.AddDependency(e.ResourceName, False)
            End If
        End If
    End Sub

    Private Sub HtmlHandler_AddedInlineCustomInteraction(sender As Object, e As InlineElementEventArgs)
        If e IsNot Nothing AndAlso e.InlineElement IsNot Nothing Then
            RaiseEvent AddedInlineCustomInteraction(sender, e)
        End If
    End Sub

    Private Sub HtmlHandler_AddedPopup(sender As Object, e As InlineElementEventArgs)
        If e IsNot Nothing AndAlso e.InlineElement IsNot Nothing Then
            RaiseEvent AddedInlineCustomInteraction(sender, e)
        End If
    End Sub

    Private Sub HtmlHandler_AddedInlineAspect(sender As Object, e As InlineElementEventArgs)
        If e IsNot Nothing AndAlso e.InlineElement IsNot Nothing Then
            RaiseEvent AddedInlineAspect(sender, e)
        End If
    End Sub

    Private Sub HtmlHandler_RemovedInlineCustomInteraction(sender As Object, e As InlineElementEventArgs)
        If e IsNot Nothing AndAlso e.InlineElement IsNot Nothing Then
            RaiseEvent RemovedInlineCustomInteraction(sender, e)
        End If
    End Sub

    Private Sub HtmlHandler_RemovedInlineAspect(sender As Object, e As InlineElementEventArgs)
        If e IsNot Nothing AndAlso e.InlineElement IsNot Nothing Then
            RaiseEvent RemovedInlineAspect(sender, e)
        End If
    End Sub

    Private Sub bTTSPause_ButtonClick(sender As Object, e As EventArgs) Handles bTTSPause.ButtonClick
        PauseTextToSpeech(PauseDuration.FromConfig.Skip(1).First().Duration)
    End Sub

    Private Sub ExecuteInline(inl As HtmlInlineHandler, isNew As Boolean)
        If (inl.CanExecute()) Then
            Dim inlDialogHandler = New HtmlInlineDialogHandler(inl)
            Dim result As KeyValuePair(Of InlineElement, XmlNode) = inlDialogHandler.Execute()
            If (result.Key IsNot Nothing) Then
                Dim node = If(_behavior.ContextIdentifier.HasValue,
                              _cntHlp.GiveResourceElementsContextNumber(result.Value.OuterXml.ToString(), _behavior.ContextIdentifier).ToXmlElement(),
                              result.Value)
                AddNodeAfterCurrentNode(node, isNew)
                DoAddInlineElementPlaceholder(result.Key, result.Value)
                DoAddInline(result.Key)
                RaiseContentChanged(Me, EventArgs.Empty)
            End If
            SetFocus()
        Else
            Dim _logger = LogManager.GetCurrentClassLogger()
            _logger.Log(LogLevel.Warn, String.Format("IxHtmlEditor - While executing inline functionality it is missing a resource: " & My.Resources.ItemLayoutTemplateInlineMultimediaMissing, inl.RequiredResource))
            MessageBox.Show(String.Format(My.Resources.ItemLayoutTemplateInlineMultimediaMissing, inl.RequiredResource))
        End If
    End Sub

    Private Sub ExecuteInline(inl As HtmlInlineHandler)
        If inl IsNot Nothing Then
            ExecuteInline(inl, True)
        End If
    End Sub

    Private Sub DoInlineProperties()
        Dim inlineElement As InlineElement = Nothing
        Dim id As String = Me.Selection.Node.Attributes("id").Value
        If _behavior.InlineElements.ContainsKey(id) Then
            inlineElement = _behavior.InlineElements(id).Item1
        End If

        If inlineElement IsNot Nothing Then
            Dim toEdit As HtmlInlineHandler = _behavior.InlineToEdit(Me, inlineElement)
            Dim customInteractionInlineElement = InlineElementIsCustomInteraction(inlineElement)
            Dim aspectInlineElement = InlineElementIsAspect(inlineElement)

            AddHandler toEdit.InlineElementAdded, AddressOf HtmlHandler_InlineElementAddedAndFocused

            If customInteractionInlineElement Then
                AddHandler toEdit.AddingInlineCustomInteraction, AddressOf HtmlHandler_AddedInlineCustomInteraction
            ElseIf aspectInlineElement Then
                AddHandler toEdit.AddingInlineAspect, AddressOf HtmlHandler_AddedInlineAspect
            End If

            ExecuteInline(toEdit, False)

            If customInteractionInlineElement Then
                RemoveHandler toEdit.AddingInlineCustomInteraction, AddressOf HtmlHandler_AddedInlineCustomInteraction
            ElseIf aspectInlineElement Then
                RemoveHandler toEdit.AddingInlineAspect, AddressOf HtmlHandler_AddedInlineAspect
            End If

            RemoveHandler toEdit.InlineElementAdded, AddressOf HtmlHandler_InlineElementAddedAndFocused
        End If
    End Sub

    Private Sub DoAddInline(inlineElement As InlineElement)
        If (_behavior.InlineElements.ContainsKey(inlineElement.Identifier)) Then
            _behavior.InlineElements.Remove(inlineElement.Identifier)
        End If
        _behavior.AddDependency(inlineElement)
        _behavior.InlineElements.Add(inlineElement.Identifier, New Tuple(Of InlineElement, Boolean)(inlineElement, ShouldConvertToOldInlineHtml(inlineElement.LayoutTemplateSourceName)))

        RaiseEvent InlineElementsCollectionChanged(Me, New NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, inlineElement))
    End Sub

    Private Sub DoAddInlineElementPlaceholder(inlineElement As InlineElement, placeHolder As XmlNode)
        _behavior.AddInlineElementPlaceholder(inlineElement.Identifier, placeHolder)
    End Sub

    Private Function ShouldConvertToOldInlineHtml(layoutTemplateSourceName As String) As Boolean
        If InlineMediaTemplateHelper.IsEmbeddedResourceInlineMediaTemplate(layoutTemplateSourceName) Then
            Return True
        End If
        If ShouldConvertToOldInlineHtml(layoutTemplateSourceName, _behavior) Then
            Return True
        End If
        Return ShouldConvertToOldInlineHtml(layoutTemplateSourceName, _htmlEditorBehaviour)
    End Function

    Private Function ShouldConvertToOldInlineHtml(layoutTemplateSourceName As String, behavior As IHtmlEditorBehaviour) As Boolean
        If behavior.ConvertOldInlineToHtml Then
            Dim inlineConverter = behavior.CreateInlineConverter
            If inlineConverter IsNot Nothing Then
                Return layoutTemplateSourceName.Equals(inlineConverter.GetInlineMediaTemplate("image"), StringComparison.InvariantCultureIgnoreCase)
            End If
        End If
        Return False
    End Function

    Private Sub XHtmlEditor2_Leave(sender As Object, e As EventArgs) Handles Me.Leave
        StopEditor()
    End Sub

    Private Sub HtmlreferencesHandler_ActiveReferenceChanged(sender As Object, e As EventArgs)
    End Sub

    Private Sub DetermineVisibleStrips(sender As Object, e As CancelEventArgs) Handles mainContextStrip.Opening
        tsmInline.Visible = IsInline
        tsmTable.Visible = _xhtmlEditorCommands.IsInTable()
        tsmRemoveReference.Visible = _htmlReferencesHandler.IsReference()
        tsmEditFormula.Visible = _htmlFormulaHandler.IsMathMLImage
    End Sub

    Private Sub Opened(sender As Object, e As EventArgs) Handles mainContextStrip.Opened
        mainContextStrip.Focus()

        InlineToolStripSeparator.Visible = tsmInline.Visible AndAlso (tsmTable.Visible OrElse tsmRemoveReference.Visible OrElse tsmEditFormula.Visible)
        TableToolStripSeparator.Visible = tsmTable.Visible AndAlso (tsmRemoveReference.Visible OrElse tsmEditFormula.Visible)
        RemoveReferenceToolStripSeparator.Visible = tsmRemoveReference.Visible AndAlso tsmEditFormula.Visible
    End Sub

    Private Sub XHtmlEditor2_Load(sender As Object, e As EventArgs) Handles Me.Load
        BackColor = Color.White
    End Sub

    Private Sub HandleCssCombo(sender As Object, e As EventArgs)
        Dim userStyleRuleVariablePart As String = cbStyle.SelectedItem.ToString()
        ApplyStyle(userStyleRuleVariablePart)
    End Sub

    Private Sub HandleLanguageCombo(sender As Object, e As EventArgs)
        Dim userStyleRuleVariablePart As String = cbLanguage.SelectedItem.ToString()
        ApplyLanguage(userStyleRuleVariablePart)
    End Sub

    Private Sub HandleKeyDown(sender As Object, keyEventArgs As KeyEventArgs) Handles C1Editor1.KeyDown
        _suppressRaiseContentChangedOnDocumentChanged = False

        HandleCtrlV(keyEventArgs)
        HandleCtrlX(keyEventArgs)
        HandleCtrlC(keyEventArgs)
        HandleCtrlSpace(keyEventArgs)

        If (keyEventArgs.Shift AndAlso keyEventArgs.KeyCode = Keys.F12) Then
            ShowSourceCode()
            keyEventArgs.Handled = True
        End If

        If (keyEventArgs.KeyCode = Keys.Delete OrElse keyEventArgs.KeyCode = Keys.Back) AndAlso IsInline Then
            RemoveResourcesForSelectedInlineElement()
        ElseIf keyEventArgs.KeyCode = Keys.Delete Then
            Dim nodeToDelete = ShouldRemoveParagraphBeforeTable()
            If nodeToDelete IsNot Nothing Then
                RemoveParagraphBeforeTable(nodeToDelete)
                keyEventArgs.Handled = True
            End If
        End If

        HandleCtrlT(keyEventArgs)
        HandleCtrlB(keyEventArgs)
        HandleCtrlI(keyEventArgs)
        HandleCtrlU(keyEventArgs)

        If keyEventArgs.KeyCode = Keys.Enter AndAlso ShouldAddParagraphBeforeTable() Then
            AddParagraphBeforeTable()
            keyEventArgs.Handled = True
        End If

        If Not keyEventArgs.Handled Then
            _suppressRaiseContentChangedOnDocumentChanged = True

            CheckIfFirstKeyDown(keyEventArgs)

            If _raiseContentChangedOnFirstKeyDown OrElse
                ((keyEventArgs.Modifiers = Keys.None OrElse keyEventArgs.Modifiers = Keys.Shift) AndAlso keyEventArgs.KeyCode = Keys.Enter) OrElse
                (keyEventArgs.KeyCode = Keys.Delete OrElse keyEventArgs.KeyCode = Keys.Back) Then
                _suppressRaiseContentChangedOnDocumentChanged = False
            End If
        End If
    End Sub

    Private Sub HandleCtrlV(keyEventArgs As KeyEventArgs)
        If keyEventArgs.Control AndAlso keyEventArgs.KeyCode = Keys.V Then
            If Me.CanPaste Then
                If keyEventArgs.Shift Then
                    DoPasteOperation(True)
                Else
                    DoPasteOperation(False)
                End If
            End If

            keyEventArgs.Handled = True
        End If
    End Sub

    Private Sub HandleCtrlX(keyEventArgs As KeyEventArgs)
        If keyEventArgs.Control AndAlso keyEventArgs.KeyCode = Keys.X Then
            If Me.CanCut Then
                Cut()
            End If

            keyEventArgs.Handled = True
        End If
    End Sub

    Private Sub HandleCtrlC(keyEventArgs As KeyEventArgs)
        If keyEventArgs.Control AndAlso keyEventArgs.KeyCode = Keys.C Then
            If Me.CanCopy Then
                Copy()
            End If

            keyEventArgs.Handled = True
        End If
    End Sub

    Private Sub HandleCtrlSpace(keyEventArgs As KeyEventArgs)
        If keyEventArgs.Control AndAlso keyEventArgs.KeyCode = Keys.Space Then
            If keyEventArgs.Shift Then
                Me.SelectionText = Encoding.Unicode.GetString(BitConverter.GetBytes(160))(0)
            Else
                Me.SelectionText = Encoding.Unicode.GetString(BitConverter.GetBytes(8239))(0)
            End If
            keyEventArgs.Handled = True
        End If
    End Sub

    Private Sub HandleCtrlT(keyEventArgs As KeyEventArgs)
        If (keyEventArgs.Control AndAlso keyEventArgs.KeyCode = Keys.T) Then
            InsertSpaces()
            keyEventArgs.Handled = True
        End If
    End Sub

    Private Sub HandleCtrlB(keyEventArgs As KeyEventArgs)
        If (keyEventArgs.Control AndAlso keyEventArgs.KeyCode = Keys.B) Then
            keyEventArgs.Handled = Not CanSetFormatting
        End If
    End Sub

    Private Sub HandleCtrlI(keyEventArgs As KeyEventArgs)
        If (keyEventArgs.Control AndAlso keyEventArgs.KeyCode = Keys.I) Then
            keyEventArgs.Handled = Not CanSetFormatting
        End If
    End Sub

    Private Sub HandleCtrlU(keyEventArgs As KeyEventArgs)
        If (keyEventArgs.Control AndAlso keyEventArgs.KeyCode = Keys.U) Then
            keyEventArgs.Handled = Not CanSetFormatting
        End If
    End Sub

    Private Sub CheckIfFirstKeyDown(keyEventArgs As KeyEventArgs)
        If Not _raiseContentChangedOnFirstKeyDown.HasValue Then
            Dim modifyingKey = Not (keyEventArgs.Modifiers Or
                                    keyEventArgs.KeyCode = (Keys.Up OrElse Keys.Down OrElse Keys.Left OrElse Keys.Right))

            If modifyingKey Then
                _raiseContentChangedOnFirstKeyDown = True
            End If
        End If
    End Sub

    Private Sub DetectAndRemoveDeletedInlineElements()
        Dim result = New List(Of InlineElement)
        For Each inlineElement In _behavior.InlineElements

            Dim nodes = Me.Document.SelectNodes(String.Format("//def:img[@id='{0}' and @isinlineelement='true'] | //img[@id='{0}' and @isinlineelement='true']", inlineElement.Key), DefaultNamespaceManager)
            If nodes.Count = 0 Then
                result.Add(inlineElement.Value.Item1)
            End If
        Next

        If result.Count > 0 Then
            RemoveResources(result.ToArray())
        End If
    End Sub

    Private Sub RemoveResourcesForSelectedInlineElement()
        _suppressRaiseContentChangedOnDocumentChanged = True
        Dim inlineElement As InlineElement = Nothing

        Dim node = Me.Selection.Node
        Dim id As String = node.Attributes("id").Value
        If _behavior.InlineElements.ContainsKey(id) Then
            inlineElement = _behavior.InlineElements.First(Function(ie) ie.Key.Equals(id, StringComparison.InvariantCultureIgnoreCase)).Value.Item1
            RemoveResources(New InlineElement() {inlineElement})
            If (node.NextSibling IsNot Nothing AndAlso node.NextSibling.LocalName = "span") Then
                Dim span = DirectCast(node.NextSibling, XmlElement)
                If (span.HasAttribute("id") AndAlso
    span.Attributes("id").Value.Contains(id)) Then
                    Dim prefNode As XmlNode = span
                    For Each n As XmlNode In span.ChildNodes
                        span.ParentNode.InsertAfter(n, prefNode)
                        prefNode = n
                    Next
                    span.ParentNode.RemoveChild(span)
                End If
            End If
        End If
        _suppressRaiseContentChangedOnDocumentChanged = False
    End Sub

    Private Sub RemoveResources(inlineElements As InlineElement())
        For Each inlineElement In inlineElements
            Dim usedResourceNames = inlineElement.GetResourcesFromResourceParameter()

            If Not _behavior.InlineElements.Any(Function(el)
                                                    Return el.Value.Item1.LayoutTemplateSourceName = inlineElement.LayoutTemplateSourceName AndAlso el.Key <> inlineElement.Identifier
                                                End Function) Then
                _behavior.RemoveDependency(inlineElement.LayoutTemplateSourceName)
            End If

            For Each resourceName In usedResourceNames
                If Not _behavior.InlineElements.Any(Function(el)
                                                        Return el.Key <> inlineElement.Identifier AndAlso el.Value.Item1.GetResourcesFromResourceParameter().Contains(resourceName)
                                                    End Function) Then
                    _behavior.RemoveDependency(resourceName)
                End If
            Next

            _behavior.InlineElements.Remove(inlineElement.Identifier)

            If InlineElementIsCustomInteraction(inlineElement) Then
                HtmlHandler_RemovedInlineCustomInteraction(Me, New InlineElementEventArgs(inlineElement))
            ElseIf InlineElementIsAspect(inlineElement) Then
                HtmlHandler_RemovedInlineAspect(Me, New InlineElementEventArgs(inlineElement))
            End If
        Next
    End Sub

    Private Sub C1Editor1_KeyUp(sender As Object, keyEventArgs As KeyEventArgs) Handles C1Editor1.KeyUp
        If (keyEventArgs.KeyCode = Keys.Enter OrElse keyEventArgs.KeyCode = Keys.Delete OrElse keyEventArgs.KeyCode = Keys.Back) Then
            DetectAndRemoveDeletedInlineElements()
        End If
    End Sub

    Private Sub C1Editor1_DocumentChanged(sender As Object, e As EventArgs) Handles C1Editor1.DocumentChanged
        If Not _suppressRaiseContentChangedOnDocumentChanged Then
            RaiseContentChanged(Me, e)
            If _raiseContentChangedOnFirstKeyDown.HasValue AndAlso _raiseContentChangedOnFirstKeyDown Then
                _raiseContentChangedOnFirstKeyDown = False
            End If
        End If
    End Sub

    Private Sub ShowSourceCode()
        Dim xHtmlSourceCodeForm = New XHtmlSourceCodeViewer(C1Editor1.Xml)
        If (Me.ParentForm IsNot Nothing) Then Me.ParentForm.AddOwnedForm(xHtmlSourceCodeForm)
        xHtmlSourceCodeForm.Location = Me.Location
        AddHandler xHtmlSourceCodeForm.FormClosed, Sub(sender, e)
                                                       If xHtmlSourceCodeForm.DialogResult = DialogResult.OK Then
                                                           C1Editor1.LoadXml(xHtmlSourceCodeForm.Xhtml, Nothing)
                                                       End If
                                                   End Sub
        xHtmlSourceCodeForm.ShowDialog(Me)
        SetFocus()
    End Sub

    Private Sub InsertSpaces()
        Dim range As C1TextRange = Me.C1Editor1.Selection
        range.Select()
        range.XmlText = "&nbsp;&nbsp;&nbsp;&nbsp;"
    End Sub

    Private Sub StopEditor()
        Dim parent = GetReparentHtmlEditor(Me)
        If parent IsNot Nothing Then
            parent.StopEditor()
        End If
    End Sub

    Private Property SelectionText As String
        Get
            Return Me.Selection.Text
        End Get
        Set(value As String)
            Dim range As C1TextRange = C1Editor1.Selection.Clone()
            range.Text = value
            range.Normalize()
            range.Start.MoveTo(range.[End])
            range.[Select]()
        End Set
    End Property

    Public Property FormClosing As Boolean Implements IXHtmlEditor.FormClosing
        Get
            Return _formClosing
        End Get
        Set(value As Boolean)
            _formClosing = value
        End Set
    End Property

    Public Property ActiveReference As XhtmlReference Implements IXHtmlEditor.ActiveReference
        Get
            Return If(_htmlReferencesHandler Is Nothing, Nothing, _htmlReferencesHandler.ActiveReference)
        End Get
        Set(value As XhtmlReference)
            _htmlReferencesHandler.ActiveReference = value
        End Set
    End Property

    <Browsable(False)>
    Public ReadOnly Property DefaultNamespaceManager As XmlNamespaceManager Implements IXHtmlEditor.DefaultNamespaceManager

    Private Sub UpdateControlsToReflectAvailableBehavior()
        bInsertControl.Enabled = _behavior.CanInsertControls AndAlso InlineHandler.CanExecute()
        bInsertControl.Visible = _behavior.CanInsertControls

        bImage.Enabled = _behavior.CanInsertImages AndAlso ImageHtmlInlineHandler.CanExecute()
        bImage.Visible = _behavior.CanInsertImages

        bMovie.Enabled = _behavior.CanInsertMovies AndAlso VideoHtmlInlineHandler.CanExecute()
        bMovie.Visible = _behavior.CanInsertMovies

        bAudio.Enabled = _behavior.CanInsertAudio AndAlso AudioHtmlInlineHandler.CanExecute()
        bAudio.Visible = _behavior.CanInsertAudio

        bCustomInteraction.Enabled = _behavior.CanInsertCI AndAlso CustomInteractionHtmlInlineHandler.CanExecute()
        bCustomInteraction.Visible = _behavior.CanInsertCI

        bInsertFormula.Enabled = _behavior.CanInsertFormula
        bInsertFormula.Visible = _behavior.CanInsertFormula

        cbLanguage.Enabled = _behavior.CanSelectTextToSpeech
        cbLanguage.Visible = _behavior.CanSelectTextToSpeech

        ToolStrip.Visible = _behavior.IsToolstripVisible

        ddbReferences.Enabled = _behavior.CanCreateReferences
        ddbReferences.Visible = _behavior.CanCreateReferences

        tsbReferTo.Enabled = _behavior.CanInsertReferences
        tsbReferTo.Visible = _behavior.CanInsertReferences
    End Sub

    Private ReadOnly Property CanCopy As Boolean Implements IXHtmlEditor.CanCopy
        Get
            Return C1Editor1.CanCopy
        End Get
    End Property

    Private ReadOnly Property CanCut As Boolean Implements IXHtmlEditor.CanCut
        Get
            Return C1Editor1.CanCut
        End Get
    End Property

    Private ReadOnly Property CanPaste As Boolean Implements IXHtmlEditor.CanPaste
        Get
            Return C1Editor1.CanPaste
        End Get
    End Property

    Private Sub Copy() Implements IXHtmlEditor.Copy
        StoreInlineElements()
        C1Editor1.Copy()
    End Sub

    Private Sub Cut() Implements IXHtmlEditor.Cut
        StoreInlineElements()
        C1Editor1.Cut()
    End Sub

    Private Sub StoreInlineElements()
        Dim inlineElements = SelectedInlineElements()
        If inlineElements.Any() Then
            _lastCopiedInlineElements = inlineElements
        Else
            _lastCopiedInlineElements = Nothing
        End If
    End Sub

    Private Sub PasteAsText() Implements IXHtmlEditor.PasteAsText
        DoPasteOperation(True)
    End Sub

    Public Property Document As XmlDocument Implements IXHtmlEditor.Document
        Get
            Return C1Editor1.Document
        End Get
        Set(value As XmlDocument)
            C1Editor1.Document = value
        End Set
    End Property

    Public Sub LoadXml(outerXml As String) Implements IXHtmlEditor.LoadXml
        C1Editor1.LoadXml(outerXml, Nothing)
    End Sub

    Private Sub PasteSpecial() Implements IXHtmlEditor.PasteSpecial
        DoPasteOperation(False)
    End Sub

    Private ReadOnly Property InlineHandler As HtmlInlineHandler
        Get
            If _htmlInlineHandler Is Nothing Then
                If _behavior IsNot Nothing Then
                    _htmlInlineHandler = _behavior.CreateForInlineControl(Me)
                    AddHandler _htmlInlineHandler.AddingInlineAspect, AddressOf HtmlHandler_AddedInlineAspect
                End If
            End If
            Return _htmlInlineHandler
        End Get
    End Property

    Private ReadOnly Property ImageHtmlInlineHandler As HtmlInlineHandler
        Get
            If _imageHtmlInlineHandler Is Nothing Then
                If _behavior IsNot Nothing Then
                    _imageHtmlInlineHandler = _behavior.CreateForImage(Me)
                End If
            End If
            Return _imageHtmlInlineHandler
        End Get
    End Property

    Private ReadOnly Property AudioHtmlInlineHandler As HtmlInlineHandler
        Get
            If _audioHtmlInlineHandler Is Nothing Then
                If _behavior IsNot Nothing Then
                    _audioHtmlInlineHandler = _behavior.CreateForAudio(Me)
                End If
            End If
            Return _audioHtmlInlineHandler
        End Get
    End Property

    Private ReadOnly Property VideoHtmlInlineHandler As HtmlInlineHandler
        Get
            If _videoHtmlInlineHandler Is Nothing Then
                If _behavior IsNot Nothing Then
                    _videoHtmlInlineHandler = _behavior.CreateForVideo(Me)
                End If
            End If
            Return _videoHtmlInlineHandler
        End Get
    End Property

    Private ReadOnly Property CustomInteractionHtmlInlineHandler As HtmlInlineHandler
        Get
            If _customInteractionHtmlInlineHandler Is Nothing Then
                If _behavior IsNot Nothing Then
                    _customInteractionHtmlInlineHandler = _behavior.CreateForCustomInteraction(Me)
                    AddHandler _customInteractionHtmlInlineHandler.AddingInlineCustomInteraction, AddressOf HtmlHandler_AddedInlineCustomInteraction
                End If
            End If

            Return _customInteractionHtmlInlineHandler
        End Get
    End Property

    Private ReadOnly Property PopupHtmlInlineHandler As HtmlInlineHandler
        Get
            If _popupHtmlInlineHandler Is Nothing Then
                If _behavior IsNot Nothing Then
                    _popupHtmlInlineHandler = _behavior.CreateForPopup(Me)
                    If _popupHtmlInlineHandler IsNot Nothing Then AddHandler _popupHtmlInlineHandler.AddingPopup, AddressOf HtmlHandler_AddedPopup
                End If
            End If

            Return _popupHtmlInlineHandler
        End Get
    End Property

    Private Sub AddAudio() Implements IXHtmlEditor.AddAudio
        _insAud.Execute(Nothing)
    End Sub

    Private Sub AddImage() Implements IXHtmlEditor.AddImage
        _insPic.Execute(Nothing)
    End Sub

    Private Sub AddVideo() Implements IXHtmlEditor.AddVideo
        _insMov.Execute(Nothing)
    End Sub

    Private ReadOnly Property CanAddAudio As Boolean Implements IXHtmlEditor.CanAddAudio
        Get
            Return _insAud.CanExecute(Nothing) AndAlso AudioHtmlInlineHandler.CanExecute() AndAlso CanSetFormatting
        End Get
    End Property

    Private ReadOnly Property CanAddImage As Boolean Implements IXHtmlEditor.CanAddImage
        Get
            Return _insPic.CanExecute(Nothing) AndAlso ImageHtmlInlineHandler.CanExecute() AndAlso CanSetFormatting
        End Get
    End Property

    Private ReadOnly Property CanAddVideo As Boolean Implements IXHtmlEditor.CanAddVideo
        Get
            Return _insMov.CanExecute(Nothing) AndAlso VideoHtmlInlineHandler.CanExecute() AndAlso CanSetFormatting
        End Get
    End Property


    Public ReadOnly Property CurrentStyle As String Implements IXHtmlEditor.CurrentStyle
        Get
            Return _currentStyle
        End Get
    End Property

    Public ReadOnly Property CurrentLanguage As String Implements IXHtmlEditor.CurrentLanguage
        Get
            Return _currentLanguage
        End Get
    End Property

    Private Sub SelectionChanged(o As Object, e As EventArgs)
        Dim sel = Me.Selection
        If sel Is Nothing Then Return

        Dim selectedStyle As String = Nothing
        Dim selectedLanguage As String = Nothing

        Dim currentStylesProperty = sel.GetType().GetProperty("CurrentStyle", BindingFlags.NonPublic Or BindingFlags.Instance)
        If currentStylesProperty IsNot Nothing Then
            Dim currentStylesValue = currentStylesProperty.GetValue(sel, Nothing)
            Dim styleNamesField = currentStylesValue.GetType().GetField("n", BindingFlags.Instance Or BindingFlags.NonPublic)
            If styleNamesField IsNot Nothing Then
                Dim styleNamesValue = DirectCast(styleNamesField.GetValue(currentStylesValue), IEnumerable(Of String))
                selectedStyle = styleNamesValue.FirstOrDefault(Function(sm) sm.StartsWith(".UserSR"))
                selectedStyle = If(String.IsNullOrEmpty(selectedStyle), Nothing, selectedStyle.TrimStart.Substring(7))

                selectedLanguage = styleNamesValue.FirstOrDefault(Function(sm) sm.StartsWith(".LangTTS"))
                selectedLanguage = If(String.IsNullOrEmpty(selectedLanguage), Nothing, selectedLanguage.TrimStart.Substring(8))
            End If
        End If

        SetCurrentStyle(selectedStyle)
        SetCurrentLanguage(selectedLanguage)

        If _currentSelection Is Nothing OrElse sel IsNot _currentSelection Then
            _currentSelection = sel
            RaiseEvent EditorSelectionChanged(o, e)
        End If
    End Sub

    Public Event CurrentStyleChanged(sender As Object, args As EventArgs) Implements IXHtmlEditor.CurrentStyleChanged
    Public Event CurrentLanguageChanged(sender As Object, args As EventArgs) Implements IXHtmlEditor.CurrentLanguageChanged
    Public Event EditorSelectionChanged(sender As Object, args As EventArgs) Implements IXHtmlEditor.SelectionChanged

    Public Function IsButtonChecked(btn As Questify.Builder.Logic.Service.Interfaces.UI.Button) As Boolean Implements IXHtmlEditor.IsButtonChecked
        Select Case btn
            Case Questify.Builder.Logic.Service.Interfaces.UI.Button.BOLD
                Return bBold.Checked
            Case Questify.Builder.Logic.Service.Interfaces.UI.Button.ITALIC
                Return bItalic.Checked
            Case Else
                Return False
        End Select
    End Function

    Public Event IsButtonCheckedChanged(sender As Object, args As ButtonCheckChangedEventArgs) Implements IXHtmlEditor.IsButtonCheckedChanged

    Public ReadOnly Property CanSetFormatting As Boolean Implements IXHtmlEditor.CanSetFormatting
        Get
            Dim startNodeParent As XmlNode = Me.Selection?.Start?.ParentNode
            Dim endNodeParent As XmlNode = Me.Selection?.End?.ParentNode
            If startNodeParent Is Nothing Or endNodeParent Is Nothing Then
                Return False
            End If

            Dim hasNotAllowedClassName As Boolean? =
                startNodeParent?.Attributes?.Cast(Of XmlAttribute).Any(Function(a) ContainsNotAllowedTTSClassName(a)) OrElse
                endNodeParent?.Attributes?.Cast(Of XmlAttribute).Any(Function(a) ContainsNotAllowedTTSClassName(a))

            Return Not hasNotAllowedClassName.HasValue OrElse Not hasNotAllowedClassName.Value
        End Get
    End Property

    Private ReadOnly Property CanAlignLeft As Boolean Implements IXHtmlEditor.CanAlignLeft
        Get
            Return CanSetFormatting
        End Get
    End Property

    Private ReadOnly Property CanAlignMiddle As Boolean Implements IXHtmlEditor.CanAlignMiddle
        Get
            Return CanSetFormatting
        End Get
    End Property

    Private ReadOnly Property CanAlignRight As Boolean Implements IXHtmlEditor.CanAlignRight
        Get
            Return CanSetFormatting
        End Get
    End Property

    Private ReadOnly Property CanBold As Boolean Implements IXHtmlEditor.CanBold
        Get
            Return CanSetFormatting
        End Get
    End Property

    Private ReadOnly Property CanClearStyling As Boolean Implements IXHtmlEditor.CanClearStyling
        Get
            Return True
        End Get
    End Property

    Private ReadOnly Property CanIndent As Boolean Implements IXHtmlEditor.CanIndent
        Get
            Return CanSetFormatting
        End Get
    End Property

    Private ReadOnly Property CanItalic As Boolean Implements IXHtmlEditor.CanItalic
        Get
            Return CanSetFormatting
        End Get
    End Property

    Private ReadOnly Property CanLock As Boolean Implements IXHtmlEditor.CanLock
        Get
            Return CanSetFormatting
        End Get
    End Property

    Private ReadOnly Property CanMakeBulleted As Boolean Implements IXHtmlEditor.CanMakeBulleted
        Get
            Return CanSetFormatting
        End Get
    End Property

    Private ReadOnly Property CanMakeNumbered As Boolean Implements IXHtmlEditor.CanMakeNumbered
        Get
            Return CanSetFormatting
        End Get
    End Property

    Private ReadOnly Property CanMakeRomanNumbered As Boolean Implements IXHtmlEditor.CanMakeRomanNumbered
        Get
            Return CanSetFormatting
        End Get
    End Property

    Private ReadOnly Property CanStrikethrough As Boolean Implements IXHtmlEditor.CanStrikethrough
        Get
            Return CanSetFormatting
        End Get
    End Property

    Private ReadOnly Property CanSubScript As Boolean Implements IXHtmlEditor.CanSubScript
        Get
            Return CanSetFormatting
        End Get
    End Property

    Private ReadOnly Property CanSuperScript As Boolean Implements IXHtmlEditor.CanSuperScript
        Get
            Return CanSetFormatting
        End Get
    End Property

    Private ReadOnly Property CanUnderlined As Boolean Implements IXHtmlEditor.CanUnderlined
        Get
            Return CanSetFormatting
        End Get
    End Property

    Private ReadOnly Property CanUnIndent As Boolean Implements IXHtmlEditor.CanUnIndent
        Get
            Return CanSetFormatting
        End Get
    End Property

    Private ReadOnly Property CanAddReference As Boolean Implements IXHtmlEditor.CanAddReference
        Get
            Return CanSetFormatting
        End Get
    End Property

    Private ReadOnly Property CanAddSpecialSymbol As Boolean Implements IXHtmlEditor.CanAddSpecialSymbol
        Get
            Return CanSetFormatting
        End Get
    End Property

    Private ReadOnly Property CanAddTableByRowsColums As Boolean Implements IXHtmlEditor.CanAddTableByRowsColums
        Get
            Return False
        End Get
    End Property

    Private ReadOnly Property CanAddTable As Boolean Implements IXHtmlEditor.CanAddTable
        Get
            Return CanSetFormatting
        End Get
    End Property

    ReadOnly Property CanAddFormula As Boolean Implements IXHtmlEditor.CanAddFormula
        Get
            Return CanSetFormatting
        End Get
    End Property

    ReadOnly Property CanAddInlineControl As Boolean Implements IXHtmlEditor.CanAddInlineControl
        Get
            Return _behavior.CanInsertControls AndAlso CanSetFormatting
        End Get
    End Property

    Private ReadOnly Property CanAddCustomInteraction As Boolean Implements IXHtmlEditor.CanAddCustomInteraction
        Get
            Return _insCi.CanExecute(Nothing) AndAlso CustomInteractionHtmlInlineHandler.CanExecute() AndAlso CanSetFormatting
        End Get
    End Property

    Public ReadOnly Property CanSetTextToSpeechOptions As Boolean Implements IXHtmlEditor.CanSetTextToSpeechOptions
        Get
            Return _behavior.CanSetTextToSpeechOptions AndAlso CanSetFormatting
        End Get
    End Property

    Public ReadOnly Property CanRemoveTTS As Boolean Implements IXHtmlEditor.CanRemoveTTS
        Get
            Return _behavior.CanSetTextToSpeechOptions AndAlso TextToSpeech.CanRemove(Me.Selection)
        End Get
    End Property

    Private ReadOnly Property CanAddPupop As Boolean Implements IXHtmlEditor.CanAddPopup
        Get
            If Not _insPop Is Nothing Then
                Return _insPop.CanExecute(Nothing) AndAlso CanSetFormatting
            Else
                Return False
            End If
        End Get
    End Property

    Private ReadOnly Property ShowAddCustomInteraction As Boolean Implements IXHtmlEditor.ShowAddCustomInteraction
        Get
            Return _behavior.CanInsertCI AndAlso CanSetFormatting
        End Get
    End Property

    ReadOnly Property UserStyles As IList(Of String) Implements IXHtmlEditor.UserStyles
        Get
            Return New List(Of String)(_xhtmlEditorCommands.GetStyles(_stylesheets).Keys)
        End Get
    End Property

    ReadOnly Property Languages As IList(Of String) Implements IXHtmlEditor.Languages
        Get
            Return New List(Of String)(_xhtmlEditorCommands.GetLanguages(_stylesheets).Keys)
        End Get
    End Property

    Private Sub AlignLeft() Implements IXHtmlEditor.AlignLeft
        bAlignLeft.PerformClick()
        SetFocus()
    End Sub

    Private Sub AlignMiddle() Implements IXHtmlEditor.AlignMiddle
        bAlignCenter.PerformClick()
        SetFocus()
    End Sub

    Private Sub AlignRight() Implements IXHtmlEditor.AlignRight
        bAlignRight.PerformClick()
        SetFocus()
    End Sub

    Private Sub ClearStyling() Implements IXHtmlEditor.ClearStyling
        bClearFormatting.PerformClick()
        SelectionChanged(Me, New EventArgs())
        SetFocus()
    End Sub

    Private Sub DoIndent() Implements IXHtmlEditor.DoIndent
        Dim listType As String = Nothing

        Dim orderedlist = GetSelectedListElements().FirstOrDefault(Function(e) String.Equals(e.Name, "ol", StringComparison.OrdinalIgnoreCase))

        If Not IsNothing(orderedlist) Then
            listType = orderedlist.GetAttribute("type")
        End If

        bIndent.PerformClick()

        If listType IsNot Nothing Then
            For Each element In GetSelectedListElements().Where(Function(e) String.Equals(e.Name, "ol", StringComparison.OrdinalIgnoreCase))
                element.SetAttribute("type", listType)
            Next
        End If

        SetFocus()
    End Sub

    Private Sub DoUnIndent() Implements IXHtmlEditor.DoUnIndent
        bDecreaseIndent.PerformClick()
        SetFocus()
    End Sub

    Private Sub Lock() Implements IXHtmlEditor.Lock
        bLockEditImg.PerformClick()
        SetFocus()
    End Sub

    Private Sub MakeBold() Implements IXHtmlEditor.MakeBold
        bBold.PerformClick()
        SetFocus()
    End Sub

    Private Sub MakeBulleted() Implements IXHtmlEditor.MakeBulleted
        bBulletList.PerformClick()
        SetFocus()
    End Sub

    Private Sub MakeItalic() Implements IXHtmlEditor.MakeItalic
        bItalic.PerformClick()
        SetFocus()
    End Sub

    Private Sub MakeNumbered() Implements IXHtmlEditor.MakeNumbered
        Dim orderedlists = GetSelectedListElements().Where(Function(e) String.Equals(e.Name, "ol", StringComparison.OrdinalIgnoreCase))

        If Not orderedlists.Any() OrElse orderedlists.Any(Function(e) e.GetAttribute("type") = "1") Then
            bNumberedList.PerformClick()

            RaiseContentChanged(Me, New EventArgs())
            SelectionChanged(Me, New EventArgs())

            orderedlists = GetSelectedListElements().Where(Function(e) String.Equals(e.Name, "ol", StringComparison.OrdinalIgnoreCase))
        End If

        For Each element In orderedlists
            element.SetAttribute("type", "1")
        Next

        RaiseContentChanged(Me, New EventArgs())
        SelectionChanged(Me, New EventArgs())

        CheckListButtonStates(Me, Nothing)

        SetFocus()
    End Sub

    Private Sub MakeRomanNumbered() Implements IXHtmlEditor.MakeRomanNumbered
        Dim orderedlists = GetSelectedListElements().Where(Function(e) String.Equals(e.Name, "ol", StringComparison.OrdinalIgnoreCase))

        If Not orderedlists.Any() OrElse orderedlists.Any(Function(e) e.GetAttribute("type") = "I") Then
            bNumberedList.PerformClick()

            RaiseContentChanged(Me, New EventArgs())
            SelectionChanged(Me, New EventArgs())

            orderedlists = GetSelectedListElements().Where(Function(e) String.Equals(e.Name, "ol", StringComparison.OrdinalIgnoreCase))
        End If

        For Each element In orderedlists
            element.SetAttribute("type", "I")
        Next

        RaiseContentChanged(Me, New EventArgs())
        SelectionChanged(Me, New EventArgs())

        CheckListButtonStates(Me, Nothing)

        SetFocus()
    End Sub

    Private Sub CheckListButtonStates(sender As Object, args As EventArgs)
        RaiseEvent IsButtonCheckedChanged(Me, New ButtonCheckChangedEventArgs(Questify.Builder.Logic.Service.Interfaces.UI.Button.MAKEROMANNUMBERED, GetSelectedListElements().Any(Function(e) (String.Equals(e.Name, "ol", StringComparison.OrdinalIgnoreCase) AndAlso e.GetAttribute("type") = "I"))))
        RaiseEvent IsButtonCheckedChanged(Me, New ButtonCheckChangedEventArgs(Questify.Builder.Logic.Service.Interfaces.UI.Button.MAKENUMBERED, GetSelectedListElements().Any(Function(e) (String.Equals(e.Name, "ol", StringComparison.OrdinalIgnoreCase) AndAlso e.GetAttribute("type") = "1"))))
        RaiseEvent IsButtonCheckedChanged(Me, New ButtonCheckChangedEventArgs(Questify.Builder.Logic.Service.Interfaces.UI.Button.MAKEBULLETED, GetSelectedListElements().Any(Function(e) String.Equals(e.Name, "ul", StringComparison.OrdinalIgnoreCase))))
    End Sub

    Private Function GetSelectedListElements() As IList(Of XmlElement)
        Dim listElements As New List(Of XmlElement)
        listElements.AddRange(C1Editor1.Selection.GetTags(C1StyleType.List, False).Where(Function(n) (String.Equals(n.Name, "ol", StringComparison.OrdinalIgnoreCase) OrElse String.Equals(n.Name, "ul", StringComparison.OrdinalIgnoreCase))).Cast(Of XmlElement))

        If Not listElements.Any() Then
            Dim parentNode = Me.Selection.Node
            While parentNode IsNot Nothing
                If String.Equals(parentNode.Name, "ol", StringComparison.OrdinalIgnoreCase) OrElse String.Equals(parentNode.Name, "ul", StringComparison.OrdinalIgnoreCase) Then
                    listElements.Add(DirectCast(parentNode, XmlElement))

                    Exit While
                End If

                parentNode = parentNode.ParentNode
            End While
        End If

        Return listElements
    End Function

    Private Sub C1Editor1_DoubleClick(sender As Object, args As EventArgs)
        If Me.IsInline Then
            Me.DoInlineProperties()
        ElseIf _htmlReferencesHandler.IsReference Then
            Me._htmlReferencesHandler.InsertElementReference()
        ElseIf _htmlFormulaHandler.IsMathMLImage Then
            Me.EditMathFormula()
        End If
    End Sub

    Private Sub MakeStrikethrough() Implements IXHtmlEditor.MakeStrikethrough
        bStrikeThrough.PerformClick()
        SetFocus()
    End Sub

    Private Sub MakeSubScript() Implements IXHtmlEditor.MakeSubScript
        bSubscript.PerformClick()
        SetFocus()
    End Sub

    Private Sub MakeSuperScript() Implements IXHtmlEditor.MakeSuperScript
        bSuperScript.PerformClick()
        SetFocus()
    End Sub

    Private Sub MakeUnderlined() Implements IXHtmlEditor.MakeUnderlined
        bUnderLine.PerformClick()
        SetFocus()
    End Sub

    Private Sub AddReference(referenceId As String) Implements IXHtmlEditor.AddReference

        Dim parameters As List(Of ParameterBase) = _htmlEditorBehaviour.Parameters
        For Each parameterBase As ParameterBase In parameters
            If TypeOf parameterBase Is XhtmlResourceParameter Then
                Dim xhtmlResourceParameter = DirectCast(parameterBase, XhtmlResourceParameter)
                _htmlReferencesHandler.ActiveReference = xhtmlResourceParameter.References.GetReferenceById(referenceId)
            End If
        Next

        _htmlReferencesHandler.DoReferToToolStripButton()

        SetFocus()
    End Sub

    Private Sub AddFormula() Implements IXHtmlEditor.AddFormula
        EditMathFormula()
        SetFocus()
    End Sub

    Private Sub AddSpecialSymbol(symbol As Char) Implements IXHtmlEditor.AddSpecialSymbol
        Me.SelectionText = symbol
        SetFocus()
    End Sub

    Private Sub AddTable(columns As Integer, row As Integer) Implements IXHtmlEditor.AddTableByRowsColums
        Throw New NotImplementedException()
    End Sub

    Private Sub AddTable() Implements IXHtmlEditor.AddTable
        _xhtmlEditorCommands.AddTable()
    End Sub

    Private Sub AddInlineControl() Implements IXHtmlEditor.AddInlineControl
        ExecuteInline(InlineHandler)

        SetFocus()
    End Sub

    Private Sub SetCurrentStyle(styleName As String)
        _currentStyle = styleName
        RaiseEvent CurrentStyleChanged(Me, New EventArgs())
    End Sub

    Private Sub SetCurrentLanguage(languageName As String)
        _currentLanguage = languageName
        RaiseEvent CurrentLanguageChanged(Me, New EventArgs())
    End Sub

    Private Sub ApplyStyle(styleName As String) Implements IXHtmlEditor.ApplyStyle
        SetCurrentStyle(styleName)
        Dim classToSet As String = String.Format("{0}{1}", "UserSR", styleName)
        If Not String.IsNullOrEmpty(styleName) Then
            Try
                If C1Editor1.Selection.Start.Offset <> C1Editor1.Selection.End.Offset Then
                    Dim existingClass As String = String.Empty
                    If C1Editor1.Selection.GetTags(C1StyleType.Any, False).Count > 0 Then
                        If C1Editor1.Selection.GetTags(C1StyleType.Any, False)(0).Attributes IsNot Nothing AndAlso C1Editor1.Selection.GetTags(C1StyleType.Any, False)(0).Attributes(CLASS_ATTRIBUTE) IsNot Nothing Then
                            existingClass = C1Editor1.Selection.GetTags(C1StyleType.Any, False)(0).Attributes(CLASS_ATTRIBUTE).Value
                        End If
                    End If
                    If Not String.IsNullOrEmpty(existingClass) Then
                        Dim regex As New Regex("\bUserSR\w+\b")
                        existingClass = regex.Replace(existingClass, String.Empty)
                        If Not String.IsNullOrEmpty(existingClass) Then classToSet = String.Concat(Trim(existingClass), " ", classToSet)
                    End If
                    C1Editor1.Selection.ApplyClass(classToSet)
                    If _cntHlp.RemoveColGroupFromTables(C1Editor1.Selection.Node, DefaultNamespaceManager) Then
                        C1Editor1.LoadXml(C1Editor1.Document.OuterXml, Nothing)
                    End If
                End If
            Catch ex As Exception
                MessageBox.Show(String.Format("The selected style could not be applied to this selection. Reason: {0}", ex.Message))
            End Try
        End If

        SetFocus()
    End Sub

    Private Sub ApplyLanguage(languageName As String) Implements IXHtmlEditor.ApplyLanguage
        SetCurrentLanguage(languageName)
        Dim classToSet As String = String.Format("{0}{1}", "LangTTS", languageName)
        If Not String.IsNullOrEmpty(languageName) Then
            Try
                If C1Editor1.Selection.Start.Offset <> C1Editor1.Selection.End.Offset Then
                    Dim existingClass As String = GetExistingClassName()

                    If Not String.IsNullOrEmpty(existingClass) Then
                        Dim regex As New Regex("\bLangTTS\w+\b")
                        existingClass = regex.Replace(existingClass, String.Empty)
                        If Not String.IsNullOrEmpty(existingClass) Then classToSet = String.Concat(Trim(existingClass), " ", classToSet)
                    End If
                    C1Editor1.Selection.ApplyClass(classToSet)
                    If _cntHlp.RemoveColGroupFromTables(C1Editor1.Selection.Node, DefaultNamespaceManager) Then
                        C1Editor1.LoadXml(C1Editor1.Document.OuterXml, Nothing)
                    End If
                End If
            Catch ex As Exception
                MessageBox.Show(String.Format("The selected language could not be applied to this selection. Reason: {0}", ex.Message))
            End Try
        End If

        SetFocus()
    End Sub

    Private Function GetExistingClassName() As String
        Dim existingClass As String = String.Empty
        If C1Editor1.Selection.GetTags(C1StyleType.Any, False).Any() Then
            If C1Editor1.Selection.GetTags(C1StyleType.Any, False)(0).Attributes IsNot Nothing AndAlso C1Editor1.Selection.GetTags(C1StyleType.Any, False)(0).Attributes(CLASS_ATTRIBUTE) IsNot Nothing Then
                existingClass = C1Editor1.Selection.GetTags(C1StyleType.Any, False)(0).Attributes(CLASS_ATTRIBUTE).Value
            End If
        End If

        Return existingClass.Trim()
    End Function

    Sub MuteTextToSpeech() Implements IXHtmlEditor.MuteTextToSpeech
        TextToSpeech.Mute(Me)
    End Sub

    Sub AlternativeTextToSpeech() Implements IXHtmlEditor.AlternativeTextToSpeech
        TextToSpeech.Alternative(Me)
        SetFocus()
    End Sub

    Sub PauseTextToSpeech(duration As Integer?) Implements IXHtmlEditor.PauseTextToSpeech
        TextToSpeech.Pause(Me, duration)
        SetFocus()
    End Sub

    Sub RemoveTextToSpeech() Implements IXHtmlEditor.RemoveTextToSpeech
        TextToSpeech.Remove(Me.Selection, Me.Document)
        SetFocus()
    End Sub

    Sub InsertElementReference() Implements IXHtmlEditor.InsertElementReference
        _htmlReferencesHandler.InsertElementReference()
        SetFocus()
    End Sub

    Sub InsertSymbolReference() Implements IXHtmlEditor.InsertSymbolReference
        _htmlReferencesHandler.InsertSymbolOrHighlightReference(XhtmlReferenceType.Symbol)
        SetFocus()
    End Sub

    Sub InsertHighlightReference() Implements IXHtmlEditor.InsertHighlightReference
        _htmlReferencesHandler.InsertSymbolOrHighlightReference(XhtmlReferenceType.Highlight)
        SetFocus()
    End Sub

    Sub RemoveReference() Implements IXHtmlEditor.RemoveReference
        _htmlReferencesHandler.RemoveReference()

        SetFocus()
    End Sub

    Sub OverviewReferences() Implements IXHtmlEditor.OverviewReferences
        Dim conf As New C1HtmlConverter
        Dim html As String = conf.ToCitoFormatForReferenceReadOut().ConvertHtml(Me.Document.DocumentElement.OuterXml)
        Dim dlg As New XhtmlReferenceDialog(XhtmlReferenceFactory.ParseXhtmlReference(html))
        dlg.ReadOnly = True
        dlg.ShowDialog()
        SetFocus()
    End Sub

    Private Sub AddCI() Implements IXHtmlEditor.AddCI
        _insCi.Execute(Nothing)
    End Sub

    Private Sub AddPopup() Implements IXHtmlEditor.AddPopup
        If Not _insPop Is Nothing Then _insPop.Execute(Nothing)
    End Sub

    Public ReadOnly Property CanAddInline As Boolean Implements IXHtmlEditor.CanAddInline
        Get
            Return _behavior.CanInsertControls() AndAlso CanSetFormatting
        End Get
    End Property

    Public Sub CreateInline(text As String) Implements IXHtmlEditor.CreateInline
        Dim properties = _behavior.GetPropertiesForTemplate(text)
        Dim template = _behavior.GetInlineTemplate(text)
        Dim inlineHandler = _behavior.CreateForInlineControl(Me, template)
        Dim inlineDialogHandler = New HtmlInlineDialogHandler(inlineHandler)

        Debug.Assert(inlineHandler IsNot Nothing)

        If (properties.ContainsKey("divideStrategy")) Then
            Dim splicer As New HtmlInlineSplicer(inlineDialogHandler)

            Dim selection = C1Editor1.Selection

            Me.BeginTransaction()

            Dim result = splicer.Execute(properties("divideStrategy"), selection.Start.Node, selection.Start.Offset, selection.End.Node, selection.End.Offset)

            For Each part As KeyValuePair(Of InlineElement, XmlNode) In result

                If (part.Key IsNot Nothing) Then
                    DoAddInline(part.Key)
                End If

            Next

            Me.CommitTransaction()

            C1Editor1.LoadXml(Me.Document.OuterXml, Nothing)

            RaiseContentChanged(Me, EventArgs.Empty)
        Else
            AddHandler inlineHandler.AddingInlineAspect, AddressOf HtmlHandler_AddedInlineAspect

            ExecuteInline(inlineHandler)

            RemoveHandler inlineHandler.AddingInlineAspect, AddressOf HtmlHandler_AddedInlineAspect
        End If
    End Sub

    Public ReadOnly Property InlineControlCount As Integer Implements IXHtmlEditor.InlineControlCount
        Get
            Return _behavior.InlineTemplates.Count
        End Get
    End Property

    Public ReadOnly Property InlineControls As IEnumerable(Of String) Implements IXHtmlEditor.InlineControls
        Get
            Return _behavior.InlineTemplates
        End Get
    End Property

    Private Property IXHtmlEditor2_Parent As Control Implements IXHtmlEditor.Parent
        Get
            Return Me.Parent
        End Get
        Set
            Me.Parent = Value
        End Set
    End Property

    Private Property IXHtmlEditor2_Dock As DockStyle Implements IXHtmlEditor.Dock
        Get
            Return Me.Dock
        End Get
        Set
            Me.Dock = Value
        End Set
    End Property

    Private Property IXHtmlEditor2_Size As Size Implements IXHtmlEditor.Size
        Get
            Return Me.Size
        End Get
        Set
            Me.Size = Value
        End Set
    End Property

    Public Function InlineIcon(inlineTemplate As String) As String Implements IXHtmlEditor.InlineIcon
        Return _behavior.GetIconNameForTemplate(inlineTemplate)
    End Function

    Public Function GetInlineTemplate(name As String) As String Implements IXHtmlEditor.GetInlineTemplate
        Return _behavior.GetInlineTemplate(name)
    End Function

    Public Sub RemoveCursor() Implements IXHtmlEditor.RemoveCursor
        If Me.C1Editor1 IsNot Nothing Then
            Me.C1Editor1.Mode = EditorMode.Preview
            If Not String.IsNullOrEmpty(_editorCss) Then
                Using stream As Stream = New MemoryStream(Encoding.UTF8.GetBytes(_editorCss))
                    Me.C1Editor1.LoadPreviewCSS(stream)
                End Using
            End If
        End If
    End Sub

    Public Sub SetCursor() Implements IXHtmlEditor.SetCursor
        If Me.C1Editor1 IsNot Nothing Then Me.C1Editor1.Mode = EditorMode.Design
    End Sub

    Public Sub ResetCurrentSelection() Implements IXHtmlEditor.ResetCurrentSelection
        _currentSelection = Nothing
        _raiseContentChangedOnFirstKeyDown = Nothing
    End Sub

    Public Sub SetFocusVisibility(setFocus As Boolean) Implements IXHtmlEditor.SetFocusVisibility
        Dim parent = GetReparentHtmlEditor(Me)
        If parent IsNot Nothing Then
            parent.SetFocusVisibility(setFocus)
        End If
    End Sub

    Private Sub C1Editor1_Enter(sender As Object, e As EventArgs) Handles C1Editor1.Enter
        SetCursor()
    End Sub

    Private Sub C1Editor1_MouseUp(sender As Object, e As MouseEventArgs) Handles C1Editor1.MouseUp
        RaiseEvent EditorSelectionChanged(sender, e)
    End Sub

    Private Sub C1Editor1_DragDrop(sender As Object, e As DragEventArgs) Handles C1Editor1.DragDrop
        If Not e.Data.GetDataPresent(DataFormats.Html) Then
            Return
        End If

        Dim html = e.Data.GetData(DataFormats.Html).ToString()
        Dim droppedInlineElements = DoDropOperation(html)

        For Each kvp As KeyValuePair(Of String, String) In droppedInlineElements
            html = html.Replace(kvp.Key, kvp.Value)
        Next
        e.Data.SetData(DataFormats.Html, html)
    End Sub

    Private Sub C1Editor1_DragEnter(sender As Object, e As DragEventArgs) Handles C1Editor1.DragEnter
        If (e.Data.GetDataPresent(DataFormats.Html)) Then
            e.Effect = DragDropEffects.Move
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Function ContainsNotAllowedTTSClassName(a As XmlAttribute) As Boolean
        Dim notAllowed As New List(Of String) From {"TTSMute", "TTSAlternative", "TTSAlias"}
        Return a.Name.Equals("class", StringComparison.InvariantCultureIgnoreCase) AndAlso notAllowed.Any(Function(na) a.Value.ToLowerInvariant().Contains(na.ToLowerInvariant()))
    End Function

    Private Function GetReparentHtmlEditor(control As Control) As ReparentHtmlEditor
        If control.Parent Is Nothing Then
            Return Nothing
        End If

        If TypeOf control.Parent Is ReparentHtmlEditor Then
            Return CType(control.Parent, ReparentHtmlEditor)
        Else
            Return GetReparentHtmlEditor(control.Parent)
        End If
    End Function

    Private _disposedValue As Boolean = False

    Private Sub DisposeEditor(ByVal disposing As Boolean)
        If Me._disposedValue Then
            Return
        End If

        If disposing Then
            RemoveHandler DirectCast(cbStyle.Control, ComboBox).SelectionChangeCommitted, AddressOf HandleCssCombo
            RemoveHandler DirectCast(cbLanguage.Control, ComboBox).SelectionChangeCommitted, AddressOf HandleLanguageCombo

            RemoveHandler C1Editor1.SelectionChanged, AddressOf SelectionChanged

            RemoveHandler bNumberedList.CheckedChanged, AddressOf CheckListButtonStates
            RemoveHandler C1Editor1.SelectionChanged, AddressOf CheckListButtonStates
            RemoveHandler C1Editor1.DoubleClick, AddressOf C1Editor1_DoubleClick

            If _xhtmlEditorCommands IsNot Nothing Then
                RemoveHandler _xhtmlEditorCommands.ContentChanged, AddressOf RaiseContentChanged
            End If

            If _htmlReferencesHandler IsNot Nothing Then
                RemoveHandler _htmlReferencesHandler.ActiveReferenceChanged, AddressOf HtmlreferencesHandler_ActiveReferenceChanged
            End If

            If _htmlInlineHandler IsNot Nothing Then
                RemoveHandler _htmlInlineHandler.AddingInlineAspect, AddressOf HtmlHandler_AddedInlineAspect
            End If
            If _popupHtmlInlineHandler IsNot Nothing Then
                RemoveHandler _popupHtmlInlineHandler.AddingPopup, AddressOf HtmlHandler_AddedPopup
            End If
            If _customInteractionHtmlInlineHandler IsNot Nothing Then
                RemoveHandler _customInteractionHtmlInlineHandler.AddingInlineCustomInteraction, AddressOf HtmlHandler_AddedInlineCustomInteraction
            End If
            C1Editor1.Dispose()
        End If

        Me._disposedValue = True
    End Sub

    Public Overloads Sub Dispose() Implements IDisposable.Dispose, IXHtmlEditor.Dispose
        DisposeEditor(True)
        GC.SuppressFinalize(Me)
        MyBase.Dispose()
    End Sub

    Public Sub IXHtmlEditor2_BringToFront() Implements IXHtmlEditor.BringToFront
        BringToFront()
    End Sub

    Public Function CreateRangeFromSelection() As ITextRange Implements IXHtmlEditor.CreateRangeFromSelection
        Return New XhtmlTextRange(C1Editor1)
    End Function

    Public Sub BeginTransaction() Implements IXHtmlEditor.BeginTransaction
        C1Editor1.BeginTransaction("")
    End Sub

    Public Sub CommitTransaction() Implements IXHtmlEditor.CommitTransaction
        C1Editor1.CommitTransaction()
    End Sub

    Public Sub RollbackTransaction() Implements IXHtmlEditor.RollbackTransaction
        C1Editor1.RollbackTransaction()
    End Sub

    Public ReadOnly Property Selection As ISelection Implements IXHtmlEditor.Selection
        Get
            Return New XHtmlSelection(C1Editor1)
        End Get
    End Property

    Public Property MouseFocused As Boolean Implements IXHtmlEditor.MouseFocused
        Get
            Return _mouseFocused
        End Get
        Set(value As Boolean)
            _mouseFocused = value
        End Set
    End Property

    Public Sub ClearSelection() Implements IXHtmlEditor.ClearSelection
        C1Editor1.CreateRange().Select()
    End Sub

    Public Sub [Select](xmlNode As XmlNode) Implements IXHtmlEditor.[Select]
        C1Editor1.CreateRange(xmlNode).Select()
    End Sub

    Public Sub [Select](start As Integer, length As Integer) Implements IXHtmlEditor.[Select]
        C1Editor1.Select(start, length)
    End Sub

    Public Sub ShowNewTableDialog(drawingPoint As Point) Implements IXHtmlEditor.ShowNewTableDialog
        C1Editor1.CustomDialogs.TableDialog = New AddTableDialog(drawingPoint, Me)
        C1Editor1.ShowDialog(C1.Win.C1Editor.DialogType.NewTable)
    End Sub

    Public Function SelectedText() As String Implements IXHtmlEditor.SelectedText
        Return C1Editor1.SelectedText
    End Function

    Public Sub SetTabStop(value As Boolean) Implements IXHtmlEditor.SetTabStop
        Me.TabStop = value
    End Sub

    Private Sub C1Editor1_MouseDown(sender As Object, e As MouseEventArgs) Handles C1Editor1.MouseDown
        _mouseFocused = True
    End Sub

    Public Sub DoPasteAsText() Implements IXHtmlEditor.DoPasteAsText
        C1Editor1.PasteAsText()
    End Sub
End Class

