
Imports System.Collections.ObjectModel
Imports System.Configuration
Imports System.Deployment.Application
Imports System.Linq
Imports System.Reflection
Imports System.Security.Permissions
Imports System.Threading
Imports System.Threading.Tasks
Imports Questify.Builder.Logic
Imports Questify.Builder.Configuration
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Security
Imports Questify.Builder.Logic.Service.Direct
Imports Microsoft.VisualBasic.ApplicationServices
Imports Questify.Builder.Logic.Service.Cache
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.InvalidateCache
Imports SD.LLBLGen.Pro.ORMSupportClasses
Imports System.Globalization
Imports System.IO
Imports Microsoft.ApplicationInsights
Imports Microsoft.ApplicationInsights.DataContracts
Imports Microsoft.ApplicationInsights.NLogTarget
Imports NLog
Imports Questify.Builder.UI
Imports Questify.Builder.UI.Wpf.Presentation.GenericDialogs.LoginDialog.ViewModels
Imports LoginDialogView = Questify.Builder.UI.Wpf.Presentation.GenericDialogs.LoginDialog.Views.LoginDialogView
Imports Questify.Builder.IoC
Imports LogHelper = Questify.Builder.Logic.Service.Logging.LogHelper
Imports Questify.Builder.Logic.Service.Logging

Namespace My
    ' The following events are available for MyApplication:
    ' 
    ' Startup :                    Raised when the application starts, before the startup form is created.
    ' Shutdown :                   Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException :         Raised if the application encounters an unhandled exception.
    ' StartupNextInstance :        Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged : Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication
        Private Delegate Sub DisposeDelegate()

        Private Shared _logger As ILogger = LogManager.GetCurrentClassLogger()

        Public Shared Property MainFormLoadingComplete As Boolean

        <SecurityPermission(SecurityAction.Demand, Flags:=SecurityPermissionFlag.ControlAppDomain)>
        Private Sub MyApplication_Startup(ByVal sender As Object, ByVal e As StartupEventArgs) Handles Me.Startup
            ' Thread exceptions
            AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf CurrentDomainOnUnhandledException
            AddHandler System.Windows.Forms.Application.ThreadException, AddressOf MyThreadExceptionHandler

            CheckRunningFromClickOnce()

            'Send event to Application Insights
            LogAppInsightsEvent(EventsToTrack.StartQB)

            ' Dependency injection
            IoCHelper.Init(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins"), True)
            AssessmentTestv2Factory.Plugins = IoCHelper.GetInstances(Of ITestModelPlugin)
            TestPackageFactory.Plugins = IoCHelper.GetInstances(Of ITestPackageModelPlugin)
            PluginHelper.MathMlPlugin = IoCHelper.GetInstances(Of IMathMlEditorPlugin).FirstOrDefault()

            ' Close (after confirmation) already running instances of Questify.Builder.Client.exe
            CheckForRunningProcesses()

            _logger.Log(LogLevel.Info, "Logging initialized")

            ' Initialize manager for resolving dynamically loaded plugings
            TestBuilderAssemblyResolveManager.Initialize()

            ' Set UI-language
            MultiLanguageController.InitializeUILanguage() 'Do this on main thread
            Dim currentLanguage = CultureInfo.DefaultThreadCurrentUICulture.TwoLetterISOLanguageName
            Try
                Wpf.Presentation.Bootstrapper.InitLanguageAndResources()
            Catch ex As Exception
                Throw ex
            End Try

            ' Init services
            Dim initTask = Task.Run(Sub()
                                        Try
                                            Bootstrapper.Init()
                                        Catch ex As Exception
                                            Throw
                                        End Try
                                    End Sub)

            If Not String.IsNullOrEmpty(Resources.Resources.StartUpWarningText) Then
                Application.DoEvents()
                Thread.Sleep(500)

                Dim msgdlg As New MessageDialog(Resources.Resources.StartUpWarningCaption, Resources.Resources.StartUpWarningText)
                msgdlg.ShowDialog()
            End If


            InitAuthorizationService()

            Dim canConnectToDatabase As Boolean = TestConnection()
            If Not canConnectToDatabase Then
                e.Cancel = True
            ElseIf Not e.Cancel Then
                ' Login
                Dim result As Boolean? = Authenticate()
                If result.GetValueOrDefault() Then
                    InitServices()
                Else
                    e.Cancel = True
                End If
            End If

            ' If we're about tot cancel application startup we have to close the SplashScreen explicitly from code.
            ' This is due to a bug in the way MS handles application startup canceling.
            If e.Cancel Then
                If Application.SplashScreen IsNot Nothing Then
                    Dim splashScreenDispose As New DisposeDelegate(AddressOf Application.SplashScreen.Dispose)
                    Application.SplashScreen.Invoke(splashScreenDispose)
                End If
            Else
                ' Try retrieving the usersettings from the database
                Try
                    Dim currentUser = New UserEntity(CType(Thread.CurrentPrincipal.Identity, TestBuilderIdentity).UserId)
                    currentUser = AuthorizationFactory.Instance.GetUserWithRoles(currentUser, True)
                    ' Check if password must be changed
                    If currentUser.ChangePassword Then
                        Dim dlg = New PasswordChangeDialog()
                        dlg.ShowDialog()
                        If (dlg.DialogResult = DialogResult.Cancel) Then
                            e.Cancel = True
                            Return
                        End If

                    End If

                    QbSettingsParser.SetSettings(currentUser.UserSettings)
                    currentUser = Nothing
                Catch ex As Exception
                    ' Too bad, did not work
                End Try

                ' Set UI-language again, because the language setting from the usersettings could differ from the previous language being set
                If Not QbSettingsParser.GetUserSettingLanguage.Equals(currentLanguage) Then
                    MultiLanguageController.InitializeUILanguage() 'Do this on main thread
                    Try
                        Wpf.Presentation.Bootstrapper.InitLanguageAndResources()
                    Catch ex As Exception
                        Throw ex
                    End Try
                End If

                Try
                    initTask.Wait()
                    Bootstrapper.InitializeWpf2WinVisualizerService()
                Catch ex As Exception
                    Throw
                End Try
            End If
        End Sub

        Private Sub CheckRunningFromClickOnce()
#If Not DEBUG Then
            ' When debugging, we don't want to shut down because we aren't running through the ClickOnce launcher...
            If Debugger.IsAttached Then
                Return
            End If

            If Not ApplicationDeployment.IsNetworkDeployed Then
                SplashScreen?.Invoke(Sub()
                                         MessageBox.Show(SplashScreen, String.Format(Resources.Login_LoginFailedBcOldVersion_Text, ConfigurationManager.AppSettings.Get("ClickOnceUrl"), CultureInfo.InvariantCulture), Resources.Login_LoginFailed_Title)
                                     End Sub)
                Process.GetCurrentProcess().Kill()
            End If
            CheckForUpdate()
#End If
        End Sub

        Private Sub LogAppInsightsEvent(eventName As EventsToTrack, Optional isShutDown As Boolean = False)
            LogHelper.TrackEvent(eventName)
            If isShutDown Then
                ' Flush and allow some time for sending the data to Application Insights
                ' Taken from: https://docs.microsoft.com/en-us/azure/azure-monitor/app/windows-desktop
                LogHelper.Client?.Flush()
                Thread.Sleep(1000)
            End If
        End Sub

        Public Sub CheckForUpdate()
            If Not ApplicationDeployment.IsNetworkDeployed Then
                Return
            End If

            If Not IsLatestDeployment() Then
                Dim updateInfo = ApplicationDeployment.CurrentDeployment.CheckForDetailedUpdate()
                _logger.Info($"Application will be updated to version {updateInfo.AvailableVersion}")

                ApplicationDeployment.CurrentDeployment.Update()


                If SplashScreen IsNot Nothing Then
                    SplashScreen.Invoke(Sub()
                                            MessageBox.Show(SplashScreen, Resources.UpdateAvailableWillBeInstalled_Message, $"{Resources.UpdateAvailbleWillBeInstalled_Caption} ({updateInfo.AvailableVersion})")
                                        End Sub)
                Else
                    MessageBox.Show(Application.MainForm, Resources.UpdateAvailableWillBeInstalled_Message, $"{Resources.UpdateAvailbleWillBeInstalled_Caption} ({updateInfo.AvailableVersion})")
                End If
                System.Windows.Forms.Application.Restart()

            End If
        End Sub

        Private Shared Function IsLatestDeployment() As Boolean
            ' CheckForUpdate returns True is there is an update available:
            Return Not ApplicationDeployment.CurrentDeployment.CheckForUpdate()
        End Function

        ''' <summary>
        ''' Determines whether QuestifyBuilder is already running
        ''' </summary>
        Private Function CheckForRunningProcesses() As Boolean
            Dim ownProcess = Process.GetCurrentProcess()
            ' This won't function in Debug Mode, the processname is the vshost's name.
            Dim processes As Process() = Process.GetProcessesByName(ownProcess.ProcessName).Where(Function(p) p.Id <> ownProcess.Id).ToArray()
            If processes IsNot Nothing AndAlso processes.Any() Then
                While Not SplashScreen.IsHandleCreated
                    Thread.Sleep(100)
                End While

                SplashScreen.Invoke(Sub()
                                        If MessageBox.Show(SplashScreen, Resources.OneOrMoreInstancesRunning, Resources.OneOrMoreInstancesRunningTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                                            For Each pr As Process In processes
                                                Try
                                                    pr.Kill()
                                                Catch
                                                End Try
                                            Next
                                        End If
                                    End Sub
                                )
            End If
        End Function

        <SecurityPermission(SecurityAction.Demand, Flags:=SecurityPermissionFlag.ControlAppDomain)>
        Private Sub MyApplication_Shutdown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Shutdown

            LogAppInsightsEvent(EventsToTrack.ShutDownQB, True)

            RemoveHandler AppDomain.CurrentDomain.UnhandledException, AddressOf CurrentDomainOnUnhandledException
            RemoveHandler System.Windows.Forms.Application.ThreadException, AddressOf MyThreadExceptionHandler
        End Sub

        Private Property IsAuthenticated As Boolean = False
        Private Property LoginDialog As LoginDialogView = Nothing

        Private Function LoginPermitted() As Boolean
            Dim loginPermissionEndTime As DateTime? = AuthorizationFactory.Instance.GetMaintenanceWindow()
            Return If(loginPermissionEndTime.HasValue, loginPermissionEndTime.Value, DateTime.MaxValue) > DateTime.Now
        End Function

        Private Sub LoginCloseRequest(s As Object, r As Cinch.CloseRequestEventArgs)
            IsAuthenticated = r.Result.GetValueOrDefault()
            LoginDialog.Close()
        End Sub

        Private Shared Sub CurrentDomainOnUnhandledException(sender As Object, unhandledExceptionEventArgs As System.UnhandledExceptionEventArgs)
            HandleUnhandledException(TryCast(unhandledExceptionEventArgs.ExceptionObject, Exception))
            If unhandledExceptionEventArgs.IsTerminating Then
                LogManager.GetCurrentClassLogger().Log(LogLevel.Info, "Application is terminating due to an unhandled exception in a secondary thread.")
                Process.GetCurrentProcess().Kill()
            End If
        End Sub

        Sub MyThreadExceptionHandler(ByVal sender As Object, ByVal args As ThreadExceptionEventArgs)
            Dim assemblyName As AssemblyName = Assembly.GetExecutingAssembly().GetName()
            _logger.Error(args.Exception, $"Thread exception in {assemblyName.Name} v{assemblyName.Version}")
        End Sub

        Private Shared Sub HandleUnhandledException(exception As Exception)
            Dim message As String = "Unhandled exception"
            Try
                Dim assemblyName As AssemblyName = Assembly.GetExecutingAssembly().GetName()
                message = $"Unhandled exception in {assemblyName.Name} v{assemblyName.Version}"
            Catch exc As Exception
                _logger.Error(exc, "Exception in unhandled exception handler")
            Finally
                _logger.Error(exception, message)
                LogHelper.TrackException(exception, True, New Dictionary(Of String, String) From {{"Message", message}})
                Thread.CurrentThread.Abort()
            End Try
        End Sub

        ''' <summary>
        ''' Tests the connection.
        ''' </summary>
        Private Shared Function TestConnection() As Boolean
            Try
                ' Just execute a low impact query
                AuthorizationFactory.Instance.GetApplicationRoleCollection()
            Catch e As ORMQueryExecutionException
                MessageBox.Show(String.Format("Error occured while accessing database." + vbCrLf + "Verify the connection string in the configuration file!" + vbCrLf + vbCrLf + "{0}", e.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            Catch e As Exception
                MessageBox.Show(String.Format("Unknown error occured while connecting to database." + vbCrLf + vbCrLf + "{0}", e.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End Try
            Return True
        End Function

        Private Function Authenticate() As Boolean
            Using viewModel As New LoginViewModel(False)
                Dim alwaysRequestUserCredentials = True
                If Boolean.TryParse(ConfigurationManager.AppSettings("AlwaysRequestUserCredentials"), alwaysRequestUserCredentials) AndAlso Not alwaysRequestUserCredentials Then
                    viewModel.ValidateCredentials()
                    IsAuthenticated = Not viewModel.InvalidCredentials
                End If

                If Not IsAuthenticated Then
                    viewModel.Reset()

                    ' Show the login dialog
                    LoginDialog = New LoginDialogView()
                    LoginDialog.Topmost = True
                    AddHandler viewModel.CloseRequest, AddressOf LoginCloseRequest
                    LoginDialog.DataContext = viewModel
                    LoginDialog.ShowDialog()
                End If

                IsAuthenticated = IsAuthenticated AndAlso Not LoginNotAllowed()

                RemoveHandler viewModel.CloseRequest, AddressOf LoginCloseRequest
                LoginDialog = Nothing
            End Using

            Return IsAuthenticated
        End Function

        Private Function LoginNotAllowed() As Boolean
            Dim loginIsDisabled As Boolean = Not LoginPermitted()

            If loginIsDisabled Then
                ' If loginIsDisabled then the only users allowed to login are users in the role of application administrators.
                Dim user As New UserEntity(DirectCast(My.User.CurrentPrincipal.Identity(), TestBuilderIdentity).UserId)
                loginIsDisabled = Not AuthorizationFactory.Instance.UserIsApplicationAdministrator(user)

                If loginIsDisabled Then
                    MessageBox.Show(Resources.MainForm_LoginNotPermittedMessage)
                End If
            End If

            Return loginIsDisabled
        End Function

        Private Sub InitServices()
            Dim bankSrvDecorator = New InvalidateCacheBankService(New CacheBankService(New SplitSqlQueryBankServiceDecorator(New BankService)))
            BankFactory.Instantiate(bankSrvDecorator)
            Dim resourceSrvDecorator = New InvalidateCacheResourceService(New CacheResourceService(New SplitSqlQueryDecorator(New ResourceService)))
            ResourceFactory.Instantiate(resourceSrvDecorator)

            DtoFactory.Instantiate(New ItemResourceDtoWcfServiceAdapter,
                                   New DataSourceResourceDtoWcfServiceAdapter,
                                   New TestResourceDtoWcfServiceAdapter,
                                   New TestPackageResourceDtoWcfServiceAdapter,
                                   New AspectResourceDtoWcfServiceAdapter,
                                   New GenericResourceDtoWcfServiceAdapter,
                                   New CustomBankPropertyResourceDtoWcfServiceAdapter,
                                   New CustomBankPropertyDtoWcfServiceAdapter,
                                   New ItemLayoutTemplateResourceDtoWcfServiceAdapter,
                                   New DataSourceTemplateResourceDtoWcfServiceAdapter,
                                   New TestTemplateResourceDtoWcfServiceAdapter,
                                   New ControlTemplateResourceDtoWcfServiceAdapter,
                                   New BankDtoWcfServiceAdapter,
                                   New CacheResourceWcfServiceDecorator)

            DtoFactory.CacheService.FlushAllCachePermissionsForCurrentUser()
        End Sub

        Private Shared Sub InitAuthorizationService()
            Dim authorizationSrvDecorator = New AuthorizationService
            AuthorizationFactory.Instantiate(authorizationSrvDecorator)
        End Sub

        ''' <summary>
        ''' This event handler is invoked when there is already an instance of the application running. The end user is informed about this
        ''' condition via a message.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="Microsoft.VisualBasic.ApplicationServices.StartupNextInstanceEventArgs" /> instance containing the event data.</param>
        Private Sub MyApplication_StartupNextInstance(ByVal sender As Object, ByVal e As StartupNextInstanceEventArgs) Handles Me.StartupNextInstance
            e.BringToForeground = True
            MessageBox.Show(ForegroundWindow.Instance, Resources.InstanceAlreadyOpened, Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Sub

        Protected Overrides Function OnInitialize(ByVal args As ReadOnlyCollection(Of String)) As Boolean
            ' MinimumSplashScreenDisplayTime has to be set before OnInitialize to be effective
            MinimumSplashScreenDisplayTime = 1000
            Return MyBase.OnInitialize(args)
        End Function
    End Class
End Namespace