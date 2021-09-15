
Imports System.ComponentModel
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.PluginExtensibility.Html.EditBehavior

''' <summary>
''' This control assists the Re-parent control. This control is not intended to be used stand alone.
''' </summary>
''' <remarks>
''' This control exists to speedup ParameterSetEditor's XHtmlParameter. This control aims to only present a viewer. 
''' When this control receives focus, we re-parent an existing XHtmlEditor for editor logic.
''' </remarks>''' 
<ToolboxBitmap(GetType(System.Windows.Forms.WebBrowser))>
Public Class XHtmlViewer
    Inherits WebBrowser

    Private Const MinimumHeightInPixels = 30

    ' Flag: Has Dispose already been called?
    Private _disposed As Boolean = False
    Private _behaviour As IHtmlEditorBehaviour
    '------Internal State variables
    Private _handlesAreSet As Boolean = False
    Private _lastClick As Integer = Environment.TickCount

    '----Designer Vars
    Private _doHeightUpdate As Boolean = False
    Private _determiningSize As Boolean = False

    ' Flag: Has html content changed?
    Private _updatedOnceOnOpen As Boolean = False
    Private _editorHasStarted As Boolean = False
    Private _mouseFocused As Boolean = False

    Public Event HtmlSizeStored As EventHandler(Of SizeEventArgs)


    ''' <summary>
    ''' Gets or sets a value indicating whether this instance is read only.
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if this instance is read only; otherwise, <c>false</c>.
    ''' </value>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Friend Property IsReadOnly As Boolean
    Private _isLoaded As Boolean = False


    Public Sub Initialize(ByVal behaviour As BaseHtmlEditorBehavior)
        _behaviour = behaviour
        Debug.Assert(_behaviour IsNot Nothing)
        UpdateHtml()
        SetHandlers()

        Me.Focus()
    End Sub

    ''' <summary>
    ''' Sets the edited HTML. It's intended to be called when the editor is dismounted.
    ''' </summary>
    Friend Sub UpdateHtml()
        If Not _determiningSize Then
            Dim html As String = String.Empty
            Try
                html = _behaviour.GetHtml()

                'In classes GenericResourceBehaviour, AspectEditorBehaviour and AspectReferenceEditorBehaviour these placeholders are added to be able to adjust the font here.
                html = html.Replace("fontFamilyPlaceholderKey", "font-family")
                html = html.Replace("fontFamilyPlaceholderValue", DefaultFont.FontFamily.Name)
            Catch ex As Exception

                html = "<html><head><title></title></head><body><h3>" + My.Resources.XhtmlViewerErrorReadingHtml + "</h3><p>" + ex.Message + "</p></body></html>"
            End Try
            If Not _disposed AndAlso Not Disposing Then
                DocumentText = html
            End If
        Else
            CallHtmlSizeStored()
        End If
    End Sub

    Private Sub SetHandlers()
        If (Not _handlesAreSet) Then
            Debug.Assert(Document IsNot Nothing) 'Assert that the document is not nothing, ensured by initially setting the document's text.

            '-----=====[Set handlers]=====-----
            AddHandler Document.ContextMenuShowing, New HtmlElementEventHandler(AddressOf contextMenuShowingHandler)
            AddHandler Document.MouseDown, New HtmlElementEventHandler(AddressOf mouseDownHandler)
            AddHandler DocumentCompleted, New WebBrowserDocumentCompletedEventHandler(AddressOf documentCompletedHandler)

            _handlesAreSet = True
        End If
    End Sub

    Sub contextMenuShowingHandler(sender As Object, e As HtmlElementEventArgs)
        e.ReturnValue = False
    End Sub

    Sub mouseDownHandler(sender As Object, e As HtmlElementEventArgs)
        If Not IsReadOnly Then
            _mouseFocused = True
            _editorHasStarted = True
        End If
    End Sub

    ''' <summary>
    ''' 'using WndProc because this occurs earlier in the call chain then the standard MouseDown handler
    ''' </summary>
    ''' <param name="m"></param>
    Protected Overrides Sub WndProc(ByRef m As Message)
        ' catch WM_LBUTTONDOWN
        If m.Msg = &H210 AndAlso m.WParam.ToInt32() = &H201 Then
            _mouseFocused = True
        Else
            MyBase.WndProc(m)
        End If
    End Sub

    Sub documentCompletedHandler(sender As Object, e As WebBrowserDocumentCompletedEventArgs)
        Me.ScrollBarsEnabled = Not DoHeightUpdate
        Me.AllowWebBrowserDrop = False
        UpdateHeight()
        CallHtmlSizeStored()
        _isLoaded = True
    End Sub

    Friend Sub UpdateHeight()
        If Not (_doHeightUpdate) Then
            Return
        End If

        If (Document IsNot Nothing AndAlso Document.Body IsNot Nothing AndAlso (Not _updatedOnceOnOpen OrElse _editorHasStarted)) Then 'Prevent NullReference exception by checking if Document is not nothing.
            ' _updatedOnce prevents multiple rendering iterations, possibly caused by:
            ' http://stackoverflow.com/questions/6771258/what-does-meta-http-equiv-x-ua-compatible-content-ie-edge-do/6771584#6771584
            _updatedOnceOnOpen = True
            Dim body = CType(Document.Body.DomElement, mshtml.IHTMLElement2)
            Dim desiredHeight As Integer = Math.Max(body.scrollHeight, Document.Body.ScrollRectangle.Height)
            Dim minHeightInPixels = MinimumHeightInPixels
            If _behaviour.IsToolstripVisible Then
                desiredHeight += 35     'extra height of 35 to compensate the toolbar.
                minHeightInPixels += 35
            End If
            MinimumSize = New Size(MinimumSize.Width, Math.Max(minHeightInPixels, desiredHeight))
            Height = MinimumSize.Height
        End If
    End Sub

    Public Sub StoreHtmlSize()
        If Not (_behaviour.StoreSizeOfHtml) Then
            Return
        End If

        _determiningSize = True
        DocumentText = _behaviour.ConvertForCalculationOfHtmlSize()
    End Sub

    Friend Sub CallHtmlSizeStored()
        If Not (_behaviour.StoreSizeOfHtml) Then
            Return
        End If

        Dim calculateSizeDiv = Document.GetElementById("calculateSizeDiv")
        If (Document IsNot Nothing AndAlso Document.Body IsNot Nothing) AndAlso calculateSizeDiv IsNot Nothing Then
            RaiseEvent HtmlSizeStored(Me, New SizeEventArgs(calculateSizeDiv.ScrollRectangle.Size))
        End If

        _determiningSize = False
    End Sub

    ''' <summary>
    ''' Gets or sets a value indicating whether to alter the height when the document is done loading.
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [do height update]; otherwise, <c>false</c>.
    ''' </value>
    <Description("To update or not update the height of the control when the document is done loading")>
    <DefaultValue(False)>
    Public Property DoHeightUpdate As Boolean
        Get
            Return _doHeightUpdate
        End Get
        Set(value As Boolean)
            _doHeightUpdate = value
            If (_isLoaded) Then Me.ScrollBarsEnabled = Not value
        End Set
    End Property

    Public Property MouseFocused As Boolean
        Get
            Return _mouseFocused
        End Get
        Set(value As Boolean)
            _mouseFocused = value
        End Set
    End Property

    Public Property EditorHasStarted As Boolean
        Get
            Return _editorHasStarted
        End Get
        Set(value As Boolean)
            _editorHasStarted = value
        End Set
    End Property

    Protected Overrides Sub Dispose(disposing As Boolean)
        If _disposed Then
            Exit Sub
        End If
        If (disposing) Then
            If Document IsNot Nothing Then
                RemoveHandler Document.ContextMenuShowing, AddressOf contextMenuShowingHandler
                RemoveHandler Document.MouseDown, AddressOf mouseDownHandler
                RemoveHandler DocumentCompleted, AddressOf documentCompletedHandler
            End If
            _handlesAreSet = False

            If _behaviour IsNot Nothing Then
                _behaviour.Dispose()
                _behaviour = Nothing
            End If

        End If
        _disposed = True
        MyBase.Dispose(disposing)
    End Sub

End Class
