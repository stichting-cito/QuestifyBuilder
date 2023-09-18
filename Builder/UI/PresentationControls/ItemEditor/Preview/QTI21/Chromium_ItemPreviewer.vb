Imports Cito.Tester.ContentModel
Imports System.IO
Imports Cito.Tester.Common
Imports System.Net
Imports Questify.Builder.Logic.HelperClasses
Imports System.Linq
Imports Xilium.CefGlue
Imports System.Threading
Imports Questify.Builder.Configuration
Imports Questify.Builder.Logic.Service.Classes
Imports Questify.Builder.Logic.Service.Interfaces
Imports System.IO.Compression
Imports System.Threading.Tasks
Imports Questify.Builder.UI.PreviewService

Public Class Chromium_ItemPreviewer

    Private Shared _isCefRuntimeInitialized As Boolean = False
    Private _handler As IItemPreviewHandler
    Private _bankId As Integer
    Private _item As AssessmentItem
    Private _itemHash As Byte()
    Private _disposing As Boolean = False

    Private ReadOnly _browserClaim As Guid = Guid.NewGuid
    Private _lastPreview As New KeyValuePair(Of String, Byte())
    Private _lastHandlerName As String = String.Empty


    Public Sub New()
        MyBase.New()
        InitializeComponent()

        If Not _isCefRuntimeInitialized Then
            CefBootstrapper.Initialize()
            _isCefRuntimeInitialized = True
        End If
    End Sub


    Public Overrides Function CreateScreenshot(handler As IItemPreviewHandler, bankId As Integer, assessmentItem As AssessmentItem, spath As String, dimSize As Size, sequenceNumber As Integer, publicationProperties As List(Of PublicationProperty)) As String

        Me.ScreenshotPath = spath
        Me.DimensionsForScreenshot = dimSize

        Dim publicationResult As PublicationResult = GetPreview(handler, bankId, assessmentItem, publicationProperties, False)
        Me.PublicationUrl = GetPublicationUrl(publicationResult)
        Dim filename As String = Path.Combine(Me.ScreenshotPath, String.Format("{2}_{0}_{1}.jpg", handler.UserFriendlyName.Replace(Chr(32), String.Empty), assessmentItem.Identifier, sequenceNumber))

        Dim cefClient As New Chromium_ScreenshotClient(Me.DimensionsForScreenshot.Width + 2, Me.DimensionsForScreenshot.Height + 2, filename)
        AddHandler cefClient.ScreenshotCompleted, Sub()
                                                      OnItemRenderingCompleted(Me, New EventArgs)
                                                  End Sub
        Dim cefWindowInfo As CefWindowInfo = CefWindowInfo.Create()
        cefWindowInfo.SetAsWindowless(IntPtr.Zero, False)

        Dim cefBrowserSettings As New CefBrowserSettings
        Dim browser = CefBrowserManager.GetBrowser(_browserClaim)

        CefBrowserHost.CreateBrowser(cefWindowInfo, cefClient, cefBrowserSettings, Me.PublicationUrl)

        Return filename
    End Function

    Public Overrides Function PreviewItem(handler As IItemPreviewHandler, bankId As Integer, assessmentItem As AssessmentItem, force As Boolean) As String
        PopulateDimensionsComboBox(New Dictionary(Of String, Size)(handler.Dimensions))
        TestMonitorButton.Visible = (handler.ShowTestMonitor)
        Dim md5Hash As Byte() = assessmentItem.GetMD5Hash()
        If force OrElse _lastPreview.Value Is Nothing OrElse Not md5Hash.SequenceEqual(_lastPreview.Value) OrElse Not handler.UserFriendlyName = _lastHandlerName Then
            _handler = handler
            _bankId = bankId
            _item = assessmentItem
            _itemHash = md5Hash

            InitCef()
            PublicationUrl = _lastPreview.Key
        Else
            LoadUrl()
        End If
        _lastHandlerName = handler.UserFriendlyName
        TextBoxUrl.Text = PublicationUrl
        Return PublicationUrl
    End Function

    Public Overrides Sub StopItemPreview(ByVal handler As IItemPreviewHandler)
        Dim saveSettings As Boolean = False
        If UISettings.ItemPreviewResolution <> Me.ComboBoxDimensions.SelectedIndex Then
            UISettings.ItemPreviewResolution = Me.ComboBoxDimensions.SelectedIndex
            saveSettings = True
        End If

        _item = Nothing
        _handler = Nothing
        _lastPreview = Nothing
    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = &H210 AndAlso m.WParam.ToInt32() = &H201 AndAlso Not borderPanel.Enabled Then
            borderPanel.Enabled = True
        Else
            MyBase.WndProc(m)
        End If
    End Sub



    Private Sub LoadUrl()
        Dim browser = CefBrowserManager.GetBrowser(_browserClaim)
        If browser IsNot Nothing AndAlso
            browser.Browser IsNot Nothing Then

            Dim cefFrame = browser.Browser.GetMainFrame()
            If cefFrame IsNot Nothing Then
                browser.Browser.GetMainFrame().LoadUrl(Me.PublicationUrl)
            End If
        End If
    End Sub

    Private Sub InitializeCefWebBrowser()
        Dim browser = CefBrowserManager.GetBrowser(_browserClaim)
        browser.Dock = DockStyle.Fill
        browser.BringToFront()
    End Sub

    Private Sub EnableFacetPreviewerButton(isFacet As Boolean)
        PreviewerButton.Enabled = isFacet
        PreviewerButton.Visible = isFacet
    End Sub

    Private Async Sub InitCef()
        InitializeCefWebBrowser()
        Try
            Dim task = Await GetPreviewAsync(_handler, _bankId, _item, New List(Of PublicationProperty), False)
            PublicationUrl = GetPublicationUrl(task)

            If _disposing Then
                Return
            End If

            Dim browser = CefBrowserManager.GetBrowser(_browserClaim)
            If browser IsNot Nothing AndAlso Not browser.IsDisposed AndAlso Not _disposing Then
                If Not borderPanel.Contains(browser) AndAlso Not _disposing Then
                    borderPanel.Controls.Add(browser)
                End If
                If Uri.IsWellFormedUriString(Me.PublicationUrl, UriKind.RelativeOrAbsolute) Then
                    SetCanvasDimensions()
                    LoadUrl()
                    _lastPreview = New KeyValuePair(Of String, Byte())(Me.PublicationUrl, _itemHash)
                    _lastHandlerName = _handler.UserFriendlyName
                    TextBoxUrl.Text = Me.PublicationUrl

                    browser = CefBrowserManager.GetBrowser(_browserClaim)
                    If browser.Browser Is Nothing AndAlso (browser.Address Is Nothing OrElse browser.Address.Equals("about:blank", StringComparison.InvariantCultureIgnoreCase)) Then
                        InitCef()
                    End If
                Else
                    LoadUrl()
                End If
                pleaseWaitLabel.Visible = False
            End If
        Catch ex As AggregateException
            Me.PublicationUrl = "about:blank"
            LoadUrl()
        End Try
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxDimensions.SelectionChangeCommitted
        SetCanvasDimensions()
    End Sub

    Private Sub SetCanvasDimensions()
        Dim browser = CefBrowserManager.GetBrowser(_browserClaim)
        If browser IsNot Nothing AndAlso Not ComboBoxDimensions.SelectedItem Is Nothing Then
            Dim dimensionItem = DirectCast(ComboBoxDimensions.SelectedItem, KeyValuePair(Of String, Dimension))
            Dim dimension = dimensionItem.Value

            If (dimension.Width = 0 AndAlso dimension.Height = 0) Then
                dimension = New Dimension(SplitContainer1.Panel2.Size.Width - 3, SplitContainer1.Panel2.Size.Height - 3)
            End If

            browser.MaximumSize = New Size(dimension.Width + 2, dimension.Height + 2)
            browser.MinimumSize = New Size(dimension.Width + 2, dimension.Height + 2)
            borderPanel.MaximumSize = New Size(dimension.Width + 2, dimension.Height + 2)
            borderPanel.MinimumSize = New Size(dimension.Width + 2, dimension.Height + 2)
            SplitContainer1.Panel2.AutoScrollMinSize = New Size(dimension.Width + 3, dimension.Height + 3)
            If browser.Browser IsNot Nothing Then
                ResizeWindow(browser.Browser.GetHost().GetWindowHandle(), dimension.Width, dimension.Height)
            End If
        End If
    End Sub

    Private Function GetPublicationUrl(publicationResult As PublicationResult) As String
        Dim url As String = String.Empty
        If publicationResult.ErrorMessage IsNot Nothing AndAlso Not String.IsNullOrEmpty(publicationResult.ErrorMessage) Then
            ErrorPage = $"<html><body><p>{publicationResult.ErrorMessage}</p></body></html>"
            url = Path.Combine(TempStorageHelper.GetTempStoragePath, $"error_{DateTime.Now.ToString("yyyy-dd-M-HH-mm-ss")}.html")
            PublicationUrl = url
            File.WriteAllText(PublicationUrl, ErrorPage)
        Else
            url = publicationResult.PublicationLocation
        End If
        Return url
    End Function

    Private Sub PopulateDimensionsComboBox(dimensions As Dictionary(Of String, Size))
        ComboBoxDimensions.ValueMember = "Key"
        ComboBoxDimensions.DisplayMember = "Value"

        ComboBoxDimensions.Items.Clear()
        For Each dimension As KeyValuePair(Of String, Size) In dimensions
            ComboBoxDimensions.Items.Add(New KeyValuePair(Of String, Dimension)(dimension.Key, New Dimension(dimension.Value.Width, dimension.Value.Height)))
        Next
        If Not Me.DesignMode Then
            If Me.ComboBoxDimensions.Items.Count > UISettings.ItemPreviewResolution Then
                If UISettings.ItemPreviewResolution < 0 Then
                    Me.ComboBoxDimensions.SelectedIndex = 0
                Else
                    Me.ComboBoxDimensions.SelectedIndex = UISettings.ItemPreviewResolution
                End If
            Else
                Me.ComboBoxDimensions.SelectedIndex = 0
            End If
        End If
    End Sub

    Private Sub ResizeWindow(handle As IntPtr, width As Integer, height As Integer)
        If handle <> IntPtr.Zero Then
            NativeMethods.SetWindowPos(handle, IntPtr.Zero, 0, 0, width, height,
                NativeMethods.SetWindowPosFlags.IgnoreMove Or NativeMethods.SetWindowPosFlags.IgnoreZOrder)
        End If
    End Sub

    Public Overrides Sub DisposeItemPreviewer(disposing As Boolean)
        Me.Dispose(disposing)
    End Sub

    Protected Overrides Sub Dispose(disposing As Boolean)
        Dim browser = CefBrowserManager.GetBrowser(_browserClaim)
        Me.PublicationUrl = "about:blank"
        LoadUrl()
        _disposing = True
        borderPanel.Controls.Remove(browser)
        CefBrowserManager.ReleaseBrowser(_browserClaim)
        MyBase.Dispose(disposing)
    End Sub


    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.DoubleClick
        Me.OpenPackageButton.Enabled = True
        Me.OpenPackageButton.Visible = True
        TextBoxUrl.Visible = True
    End Sub

    Private Sub OpenPackage_Click(sender As Object, e As EventArgs) Handles OpenPackageButton.Click
        Try
            If _handler IsNot Nothing Then
                Dim result = GetPreview(_handler, _bankId, _item, New List(Of PublicationProperty), True)
                Dim newPath = Directory.CreateDirectory(Path.Combine(TempStorageHelper.GetTempStoragePath, Guid.NewGuid.ToString))
                Dim webClient As WebClient = New WebClient()
                Dim fileName = Path.Combine(newPath.FullName, Path.GetFileName(result.DebugFileLocation))
                webClient.DownloadFile(result.DebugFileLocation, fileName)
                ZipFile.ExtractToDirectory(fileName, Path.Combine(newPath.FullName, _item.Identifier))
                Rename(fileName, Path.Combine(Path.GetDirectoryName(fileName), String.Format("{0}.zip", _item.Identifier)))
                Process.Start("explorer.exe", newPath.FullName)

            Else
                MessageBox.Show("handler not set", String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub TestMonitorButton_Click(sender As Object, e As EventArgs) Handles TestMonitorButton.Click
        Dim browser = CefBrowserManager.GetBrowser(_browserClaim)
        If browser.Browser IsNot Nothing AndAlso browser.Browser.GetHost() IsNot Nothing Then
            browser.Browser.GetHost().SetFocus(True)
            Thread.Sleep(500)
            SendKeys.SendWait("^{`}")
            Thread.Sleep(500)
        End If
    End Sub

    Private Sub PreviewerButton_Click(sender As Object, e As EventArgs) Handles PreviewerButton.Click
        If Not String.IsNullOrEmpty(PublicationUrl) Then
            Process.Start(PublicationUrl.ToString())
        End If
    End Sub

    Private Sub Chromium_ItemPreviewer_Load(sender As Object, e As EventArgs) Handles Me.Load
#If DEBUG Then
        Me.OpenPackageButton.Visible = True
        Me.OpenPackageButton.Enabled = True
        TextBoxUrl.Visible = True
#Else
        Me.OpenPackageButton.Visible = False
        Me.OpenPackageButton.Enabled = False
        TextBoxUrl.Visible = False
#End If
        EnableFacetPreviewerButton(_handler.UserFriendlyName.ToUpper.Contains("FACET"))
    End Sub


    Private Shared Function GetPreview(handler As IItemPreviewHandler, bankId As Integer, assessmentItem As AssessmentItem, publicationProperties As List(Of PublicationProperty), isDebug As Boolean) As PublicationResult
        Dim client = New ItemPreviewServiceClient()
        Return client.PreviewItemByAssessmentItem(handler.ServiceName, handler.PreviewTarget, bankId, SerializeHelper.XmlSerializeToByteArray(assessmentItem), isDebug, publicationProperties)
    End Function

    Private Shared Async Function GetPreviewAsync(handler As IItemPreviewHandler, bankId As Integer, assessmentItem As AssessmentItem, publicationProperties As List(Of PublicationProperty), isDebug As Boolean) As Task(Of PublicationResult)
        Dim taskRet As New Task(Of PublicationResult)(Function()
                                                          Dim client = New ItemPreviewServiceClient()
                                                          Dim r = client.PreviewItemByAssessmentItemAsync(handler.ServiceName, handler.PreviewTarget, bankId, SerializeHelper.XmlSerializeToByteArray(assessmentItem), isDebug, publicationProperties)
                                                          Return r.Result
                                                      End Function)

        taskRet.Start()
        Return Await taskRet
    End Function

    Private Class Dimension
        Public Property Width As Integer
        Public Property Height As Integer

        Public Sub New(width As Integer, height As Integer)
            Me.Width = width
            Me.Height = height
        End Sub

        Public Overrides Function ToString() As String
            Return $"{Width} x {Height}"
        End Function
    End Class

End Class
