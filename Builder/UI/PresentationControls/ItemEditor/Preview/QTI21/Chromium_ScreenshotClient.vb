Imports Xilium.CefGlue
Imports System.Drawing.Imaging
Imports System.Threading
Imports System.Threading.Tasks
Imports System.IO

<CLSCompliant(False)>
Public Class Chromium_ScreenshotClient
    Inherits CefClient

    Public Const RENDER_WAIT_TIME As Integer = 3000

    Private _renderHandler As ChromiumScreenshotRenderHandler

    Public Event ScreenshotCompleted()

    Public Sub New(windowWidth As Integer, windowHeight As Integer, filename As String)
        _renderHandler = New ChromiumScreenshotRenderHandler(windowWidth, windowHeight, filename)

        AddHandler _renderHandler.ScreenshotCompleted,
            Sub()
                RaiseEvent ScreenshotCompleted()
            End Sub

    End Sub



    Protected Overrides Function GetRenderHandler() As CefRenderHandler
        Return _renderHandler
    End Function

End Class

<CLSCompliant(False)>
Public Class ChromiumScreenshotRenderHandler
    Inherits CefRenderHandler

    Private _windowHeight As Integer
    Private _windowWidth As Integer
    Private _filename As String
    Private _nrOfPaintActions As Integer = 0
    Private _cancellationTokenSource As CancellationTokenSource

    Public Event ScreenshotCompleted()


    Public Sub New(windowWidth As Integer, windowHeight As Integer, filename As String)
        _windowHeight = windowHeight
        _windowWidth = windowWidth
        _filename = filename
        _nrOfPaintActions = 20
    End Sub

    Protected Overrides Function GetAccessibilityHandler() As CefAccessibilityHandler
        Return Nothing
    End Function

    Protected Overrides Function GetRootScreenRect(browser As CefBrowser, ByRef rect As CefRectangle) As Boolean
        Return GetViewRect(browser, rect)
    End Function

    Protected Overrides Function GetScreenPoint(browser As CefBrowser, viewX As Integer, viewY As Integer, ByRef screenX As Integer, ByRef screenY As Integer) As Boolean
        screenX = viewX
        screenY = viewY
        Return True
    End Function

    Protected Overrides Function GetViewRect(browser As CefBrowser, ByRef rect As CefRectangle) As Boolean
        rect.X = 0
        rect.Y = 0
        rect.Width = _windowWidth
        rect.Height = _windowHeight
        Return True
    End Function
    Protected Overrides Function GetScreenInfo(browser As CefBrowser, screenInfo As CefScreenInfo) As Boolean
        Return False
    End Function

    Protected Overrides Sub OnPaint(browser As CefBrowser, type As CefPaintElementType, dirtyRects() As CefRectangle, buffer As IntPtr, width As Integer, height As Integer)

        If (Not _cancellationTokenSource Is Nothing) Then
            _cancellationTokenSource.Cancel()
        End If
        Using bitmap = New Bitmap(width, height, width * 4, Imaging.PixelFormat.Format32bppRgb, buffer)
            For x As Integer = 0 To 1
                Try
                    bitmap.Save(_filename, ImageFormat.Jpeg)
                    x += 1
                Catch ex As Exception
                    If Not Directory.Exists(Path.GetDirectoryName(_filename)) Then
                        Directory.CreateDirectory(Path.GetDirectoryName(_filename))
                    Else
                        Throw ex
                    End If
                End Try
            Next
            _nrOfPaintActions -= 1
        End Using

        _cancellationTokenSource = New CancellationTokenSource()
        Dim token = _cancellationTokenSource.Token

        Task.Factory.StartNew(Sub()
                                  If _nrOfPaintActions > 0 Then token.WaitHandle.WaitOne(Chromium_ScreenshotClient.RENDER_WAIT_TIME)
                              End Sub).ContinueWith(Sub()
                                                        RaiseEvent ScreenshotCompleted()
                                                        browser.GetHost().CloseBrowser()
                                                        Dispose(True)
                                                    End Sub, TaskContinuationOptions.NotOnCanceled, token)

    End Sub

    Protected Overrides Sub OnCursorChange(ByVal browser As CefBrowser, ByVal cursorHandle As IntPtr, ByVal type As CefCursorType, ByVal customCursorInfo As CefCursorInfo)

    End Sub

    Protected Overrides Sub OnScrollOffsetChanged(ByVal browser As CefBrowser, ByVal x As Double, ByVal y As Double)

    End Sub

    Protected Overrides Sub OnImeCompositionRangeChanged(browser As CefBrowser, selectedRange As CefRange, characterBounds As CefRectangle())

    End Sub

    Protected Overrides Sub OnPopupSize(browser As CefBrowser, rect As CefRectangle)

    End Sub
End Class
