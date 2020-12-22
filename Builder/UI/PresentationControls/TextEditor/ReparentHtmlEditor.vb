Imports System.Collections.Specialized
Imports System.ComponentModel
Imports System.Threading
Imports Cito.Tester.ContentModel
Imports NLog
Imports Questify.Builder.IoC
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.PluginExtensibility.Html.EditBehavior


<DebuggerDisplay("ReparentHtmlEditor, Instance: {Tag}")>
Public Class ReparentHtmlEditor

    Private Shared _instanceCount As Integer
    Private _editor As IXHtmlEditor

    Private Shared _isStarting As Boolean = False
    Private _doHeightUpdate As Boolean = False


    Public Sub New()
        Interlocked.Increment(_instanceCount)
        Me.Tag = _instanceCount.ToString()
        InitializeComponent()
        Dim _logger = LogManager.GetCurrentClassLogger()
        _logger.Log(LogLevel.Info, "XHtmlEditor2 initialized.")
        Me.XHtmlViewer1.DoHeightUpdate = False
    End Sub

    Private ReadOnly _inlineElements As New Dictionary(Of String, Tuple(Of InlineElement, Boolean))
    Private _behavior As IHtmlEditorBehaviour
    Private _formClosing As Boolean = False
    Public Event AddedInlineCustomInteraction As EventHandler(Of InlineElementEventArgs)
    Public Event RemovedInlineCustomInteraction As EventHandler(Of InlineElementEventArgs)
    Public Event AddedInlineAspect As EventHandler(Of InlineElementEventArgs)
    Public Event RemovedInlineAspect As EventHandler(Of InlineElementEventArgs)
    Public Event HtmlSizeStored As EventHandler(Of SizeEventArgs)
    Public Event EditorReceivedFocus As EventHandler(Of Boolean)


    Public ReadOnly Property InlineElements As Dictionary(Of String, Tuple(Of InlineElement, Boolean))
        Get
            Return _inlineElements
        End Get
    End Property

    Public Property FormClosing As Boolean
        Get
            Return _formClosing
        End Get
        Set(value As Boolean)
            _formClosing = value
            If _editor IsNot Nothing Then _editor.FormClosing = value
        End Set
    End Property

    <DefaultValue(False)>
    Public Property isReadOnly As Boolean
        Get
            Debug.Assert(Me.XHtmlViewer1 IsNot Nothing)
            Return Me.XHtmlViewer1.IsReadOnly
        End Get
        Set(value As Boolean)
            Debug.Assert(Me.XHtmlViewer1 IsNot Nothing)
            Me.XHtmlViewer1.IsReadOnly = value
        End Set
    End Property

    Public Sub Initialize(behavior As BaseHtmlEditorBehavior)
        _behavior = behavior
        XHtmlViewer1.Initialize(behavior)
        Me.DoHeightUpdate = behavior.DoHeightUpdate
    End Sub



    Sub StartEditor(x As Integer, y As Integer)
        If (Not _isStarting AndAlso Not _formClosing) Then
            _isStarting = True
            CreateEditor()
            AttachEditor()
            _editor.SetHtmlValue(_behavior)
            _editor.InitCaret(x, y, _XHtmlViewer1.MouseFocused)
            _XHtmlViewer1.TabStop = False
            _XHtmlViewer1.EditorHasStarted = True
            _XHtmlViewer1.MouseFocused = False
            _isStarting = False
        End If
    End Sub

    Sub StopEditor()
        If (_editor IsNot Nothing) Then
            _editor.UpdateValue()
            EditedHtmlIsUpdated()
            XHtmlViewer1.StoreHtmlSize()
        End If
    End Sub

    Sub SetFocusVisibility(setFocus As Boolean)
        RaiseEvent EditorReceivedFocus(Me, setFocus)
    End Sub

    Private Sub CreateEditor()
        Debug.Assert(_isStarting)
        If (_editor Is Nothing) Then
            _editor = IoCHelper.GetInstance(Of IXHtmlEditor)
            _editor.FormClosing = _formClosing
            _editor.SetTabStop(False)
        End If
        Debug.Assert(_isStarting)
    End Sub

    Public Sub SetMouseFocused(value As Boolean)
        XHtmlViewer1.MouseFocused = value
        If _editor IsNot Nothing Then
            _editor.MouseFocused = value
        End If
    End Sub

    Public Sub SetFocus()
        Me.Focus()
        Dim x = 0
        Dim y = 0
        If XHtmlViewer1 IsNot Nothing Then
            x = Math.Max(1, XHtmlViewer1.Width - 10)
            y = Math.Max(1, XHtmlViewer1.Height - 10)
        End If
        If _editor Is Nothing Then
            StartEditor(x, y)
        ElseIf Not _editor.MouseFocused Then
            _editor.InitCaret(x, y, _editor.MouseFocused)
        End If
        If _editor IsNot Nothing Then
            _editor.SetFocus()
        End If
    End Sub

    Private Sub AttachEditor()
        Debug.Assert(_isStarting)

        If (_editor.Parent Is Nothing) Then
            Controls.Add(_editor)
            _editor.BringToFront()
            _editor.Size = Me.Size
            _editor.Dock = DockStyle.Fill

            AddHandler _editor.ContentChanged, AddressOf UpdateContent
            AddHandler _editor.InlineElementsCollectionChanged, AddressOf InlineElementCollectionChanged
            AddHandler _editor.AddedInlineCustomInteraction, AddressOf HtmlHandler_AddedInlineCustomInteraction
            AddHandler _editor.RemovedInlineCustomInteraction, AddressOf HtmlHandler_RemovedInlineCustomInteraction
            AddHandler _editor.AddedInlineAspect, AddressOf HtmlHandler_AddedInlineAspect
            AddHandler _editor.RemovedInlineAspect, AddressOf HtmlHandler_RemovedInlineAspect
            Debug.Assert(_isStarting)
        End If
    End Sub

    Private Sub InlineElementCollectionChanged(sender As Object, e As NotifyCollectionChangedEventArgs)
        Select Case e.Action
            Case NotifyCollectionChangedAction.Add
                If e.NewItems IsNot Nothing Then
                    For Each newInlineElement As InlineElement In e.NewItems
                        If _inlineElements.ContainsKey(newInlineElement.Identifier) Then
                            _inlineElements.Remove(newInlineElement.Identifier)
                        End If
                        _inlineElements.Add(newInlineElement.Identifier, New Tuple(Of InlineElement, Boolean)(newInlineElement, ShouldConvertToOldInlineHtml(newInlineElement.LayoutTemplateSourceName)))
                    Next
                End If
            Case NotifyCollectionChangedAction.Remove
                If e.OldItems IsNot Nothing Then

                    For Each oldInlineElement As InlineElement In e.OldItems
                        If _inlineElements.ContainsKey(oldInlineElement.Identifier) Then
                            _inlineElements.Remove(oldInlineElement.Identifier)
                        End If
                    Next

                End If
            Case Else
                Debug.Assert(False, "NotifyCollectionChangedAction Not Handled, Not supported???")
        End Select
    End Sub

    Private Function ShouldConvertToOldInlineHtml(layoutTemplateSourceName As String) As Boolean
        If InlineMediaTemplateHelper.IsEmbeddedResourceInlineMediaTemplate(layoutTemplateSourceName) Then Return True
        If _behavior.ConvertOldInlineToHtml Then
            Dim inlineConverter = _behavior.CreateInlineConverter
            If inlineConverter IsNot Nothing Then
                Return layoutTemplateSourceName.Equals(inlineConverter.GetInlineMediaTemplate("image"), StringComparison.InvariantCultureIgnoreCase)
            End If
        End If
        Return False
    End Function

    Private Sub EditedHtmlIsUpdated()
        Try
            XHtmlViewer1.UpdateHtml()
        Catch ex As Exception
            Dim _logger = LogManager.GetCurrentClassLogger()
            _logger.Log(LogLevel.Error, String.Format("Cannot update the html in the XHtmlEditor2", ex, Me.GetType()))
        End Try
    End Sub

    Private Sub UpdateContent(sender As Object, e As EventArgs)
        _editor.UpdateValue()
        If DoHeightUpdate Then
            EditedHtmlIsUpdated()
        End If
    End Sub

    Private Sub HtmlHandler_AddedInlineCustomInteraction(sender As Object, e As InlineElementEventArgs)
        If e IsNot Nothing AndAlso e.InlineElement IsNot Nothing Then
            RaiseEvent AddedInlineCustomInteraction(sender, e)
        End If
    End Sub

    Private Sub HtmlHandler_RemovedInlineCustomInteraction(sender As Object, e As InlineElementEventArgs)
        If e IsNot Nothing AndAlso e.InlineElement IsNot Nothing Then
            RaiseEvent RemovedInlineCustomInteraction(sender, e)
        End If
    End Sub

    Private Sub HtmlHandler_AddedInlineAspect(sender As Object, e As InlineElementEventArgs)
        If e IsNot Nothing AndAlso e.InlineElement IsNot Nothing Then
            RaiseEvent AddedInlineAspect(sender, e)
        End If
    End Sub

    Private Sub HtmlHandler_RemovedInlineAspect(sender As Object, e As InlineElementEventArgs)
        If e IsNot Nothing AndAlso e.InlineElement IsNot Nothing Then
            RaiseEvent RemovedInlineAspect(sender, e)
        End If
    End Sub



    Protected Overrides Sub OnValidated(e As EventArgs)
        MyBase.OnValidated(e)
        StopEditor()
    End Sub



    <Description("To update or not update the height of the control when the document is done loading")>
    Public Property DoHeightUpdate As Boolean
        Get
            Return _doHeightUpdate
        End Get
        Set(value As Boolean)
            _doHeightUpdate = value
            XHtmlViewer1.DoHeightUpdate = value
        End Set
    End Property

    Private Sub XHtmlViewer_Resize(sender As Object, e As EventArgs) Handles XHtmlViewer1.Resize
        If (DoHeightUpdate AndAlso Not FormClosing) Then
            Height = XHtmlViewer1.Height
        End If
    End Sub

    Private Sub XHtmlViewer_HtmlSizeStored(sender As Object, e As SizeEventArgs) Handles XHtmlViewer1.HtmlSizeStored
        If e IsNot Nothing Then
            RaiseEvent HtmlSizeStored(sender, e)
        End If
    End Sub



    Private Sub DoDispose(disposing As Boolean)

        Interlocked.Decrement(_instanceCount)

        If disposing Then

            If _editor IsNot Nothing Then
                RemoveHandler _editor.ContentChanged, AddressOf UpdateContent
                RemoveHandler _editor.InlineElementsCollectionChanged, AddressOf InlineElementCollectionChanged
                RemoveHandler _editor.AddedInlineCustomInteraction, AddressOf HtmlHandler_AddedInlineCustomInteraction
                RemoveHandler _editor.RemovedInlineCustomInteraction, AddressOf HtmlHandler_RemovedInlineCustomInteraction
                RemoveHandler _editor.AddedInlineAspect, AddressOf HtmlHandler_AddedInlineAspect
                RemoveHandler _editor.RemovedInlineAspect, AddressOf HtmlHandler_RemovedInlineAspect

                _editor.FormClosing = True
                _editor.Dispose()
                _editor = Nothing
            End If

            If XHtmlViewer1 IsNot Nothing Then
                RemoveHandler XHtmlViewer1.Resize, AddressOf XHtmlViewer_Resize
                RemoveHandler XHtmlViewer1.HtmlSizeStored, AddressOf XHtmlViewer_HtmlSizeStored
                XHtmlViewer1.Dispose()
            End If
        End If

    End Sub


End Class
