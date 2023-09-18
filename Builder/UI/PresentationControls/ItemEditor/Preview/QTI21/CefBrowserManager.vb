Imports Xilium.CefGlue.WindowsForms
Imports System.Linq
Imports Xilium.CefGlue

Public Class CefBrowserManager
    Private Shared _lock As New Object
    Private Shared BrowserPool As List(Of CachedBrowser) = New List(Of CachedBrowser)

    Public Shared Function ReleaseBrowser(claimId As Guid) As Boolean
        SyncLock _lock
            Dim b = BrowserPool.FirstOrDefault(Function(cb) cb.ClaimId = claimId)
            If b IsNot Nothing Then
                ReleaseBrowsers()
                b.ClaimId = Guid.Empty
                Return true
            Else
                Return False
            End If
        End SyncLock
    End Function

    Public Shared Function GetBrowser(claimId As Guid) As CefWebBrowser
        Dim claimed = BrowserPool.FirstOrDefault(Function(b) b.ClaimId = claimId)
        If claimed IsNot Nothing Then Return claimed.Browser
        SyncLock _lock
            Dim browser = BrowserPool.FirstOrDefault(Function(b) b.ClaimId = Guid.Empty)
            If browser Is Nothing Then
                Dim newBrowser = New CefWebBrowser()
                BrowserPool.Add(New CachedBrowser(newBrowser, claimId))
                Return newBrowser
            Else
                browser.ClaimId = claimId
                ReleaseBrowsers()
                Return browser.Browser
            End If
        End SyncLock
    End Function

    Public Shared Sub DisposeBrowser(browser As CefWebBrowser)
        If browser IsNot Nothing AndAlso browser.Browser IsNot Nothing Then
            Dim host As CefBrowserHost = browser.Browser.GetHost()
            host.CloseBrowser(True)
            host.Dispose()
        End If
    End Sub

    Private Shared Function ReleaseBrowsers() As Boolean
        Dim listOfDisposedBrowserIndex As New List(Of Integer)
        Dim freeBrowsers = BrowserPool.Where(Function(fb) fb.ClaimId = Guid.Empty).ToList
        freeBrowsers.ForEach(Sub(fb)
                                 listOfDisposedBrowserIndex.Add(BrowserPool.IndexOf(fb))
                                 DisposeBrowser(fb.Browser)
                             End Sub)
        listOfDisposedBrowserIndex.ForEach(Sub(index) BrowserPool.RemoveAt(index))
    End Function

End Class

Public Class CachedBrowser
    Public Sub New(browser As CefWebBrowser, claimId As Guid)
        Me.Browser = browser
        Me.ClaimId = claimId
    End Sub

    Public Property Browser As CefWebBrowser

    Public Property ClaimId As Guid
End Class
