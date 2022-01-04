Imports System.IO
Imports System.Reflection
Imports Xilium.CefGlue

Public Class CefBootstrapper

    Public Shared Sub Initialize()

        Dim sw = New Stopwatch()
        Try
            sw.Start()
            CefRuntime.Load()
            sw.Stop()
            Debugger.Log(0, "", $"Loading the CEF runtime costs {sw.ElapsedMilliseconds}." & vbCrLf)
        Catch dllNotFoundException As DllNotFoundException
            MessageBox.Show(dllNotFoundException.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        Catch cefRuntimeException As CefRuntimeException
            MessageBox.Show(cefRuntimeException.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try
        Dim mainArgs As New CefMainArgs(New String() {})

        Dim app = New PreviewApp()
        Dim exitCode As Integer = CefRuntime.ExecuteProcess(mainArgs, app, IntPtr.Zero)
        If exitCode <> -1 Then
            MessageBox.Show("Failed to execute process", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        Dim settings As New CefSettings()
        settings.MultiThreadedMessageLoop = True
        settings.CommandLineArgsDisabled = False

        Dim localPath As String = Path.GetDirectoryName(New Uri(Assembly.GetEntryAssembly().CodeBase).LocalPath)
        Dim processName As String = Process.GetCurrentProcess().ProcessName

        settings.LogSeverity = CefLogSeverity.Disable

        settings.BrowserSubprocessPath = Path.Combine(localPath, processName.Replace("vshost", "exe"))
        settings.LocalesDirPath = Path.Combine(localPath, "Cef3Browser\locales")
        settings.ResourcesDirPath = Path.Combine(localPath, "Cef3Browser")
        settings.RemoteDebuggingPort = 20480

        sw.Restart()
        CefRuntime.Initialize(mainArgs, settings, app, IntPtr.Zero)
        sw.Stop()
        Debugger.Log(0, "", $"Initializing the CEF runtime costs {sw.ElapsedMilliseconds}." & vbCrLf)

        sw = Nothing
    End Sub

    Public Shared Sub Shutdown()
        CefRuntime.Shutdown()
    End Sub
End Class
