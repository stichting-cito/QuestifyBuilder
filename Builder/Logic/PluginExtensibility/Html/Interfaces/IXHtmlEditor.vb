Imports System.Collections.Specialized
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Xml
Imports System.Drawing
Imports Cito.Tester.Common
Imports Questify.Builder.Logic.Service.Interfaces.UI

Public Interface IXHtmlEditor
    Inherits ICutPaste
    Inherits IMedia
    Inherits IRichText
    Inherits IInline

    Event AddedInlineCustomInteraction As EventHandler(Of InlineElementEventArgs)
    Event RemovedInlineCustomInteraction As EventHandler(Of InlineElementEventArgs)
    Event AddedInlineAspect As EventHandler(Of InlineElementEventArgs)
    Event RemovedInlineAspect As EventHandler(Of InlineElementEventArgs)
    Event ContentChanged As EventHandler(Of EventArgs)
    Event InlineElementsCollectionChanged As EventHandler(Of NotifyCollectionChangedEventArgs)

    Property FormClosing As Boolean
    Property MouseFocused As Boolean
    Property ActiveReference As XhtmlReference
    Property Parent As Control
    Property Dock As DockStyle
    Property Size As Size
    Property Document As XmlDocument
    ReadOnly Property Selection() As ISelection
    <Browsable(False)>
    ReadOnly Property DefaultNamespaceManager As XmlNamespaceManager

    ReadOnly Property IsInline As Boolean

    Sub InitCaret(x As Integer, y As Integer, isMouseFocused As Boolean)

    Sub SetHtmlValue(behavior As IHtmlEditorBehaviour)
    Sub UpdateValue()
    Sub OpenSymbolDialog()
    Sub AddNodeAfterCurrentNode(newNode As XmlNode, isNew As Boolean)
    Sub BringToFront()
    Sub Dispose()
    Sub SetFocus()
    Sub LoadXml(outerXml As String)

    Sub BeginTransaction()
    Sub CommitTransaction()
    Sub RollbackTransaction()
    sub DoPasteAsText()

    Sub ClearSelection()
    Sub [Select](xmlNode As XmlNode)
    Sub [Select](start As Integer, length As Integer)
    Sub ShowNewTableDialog(drawingPoint As Point)
    Function SelectedText() As String
    Function CreateRangeFromSelection() As ITextRange
    Function CreateRange(start As Integer, length As Integer) As ITextRange
    Sub SetTabStop(value As Boolean)

End Interface