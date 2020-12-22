
Option Strict On
Option Explicit On


Namespace My

    Partial Friend Class MyApplication

        <Global.System.Diagnostics.DebuggerStepThroughAttribute()> _
        Public Sub New()
            MyBase.New(Global.Microsoft.VisualBasic.ApplicationServices.AuthenticationMode.ApplicationDefined)
            Me.IsSingleInstance = false
            Me.EnableVisualStyles = true
            Me.SaveMySettingsOnExit = true
            Me.ShutDownStyle = Global.Microsoft.VisualBasic.ApplicationServices.ShutdownMode.AfterMainFormCloses
        End Sub

        <Global.System.Diagnostics.DebuggerStepThroughAttribute()> _
        Protected Overrides Sub OnCreateMainForm()
            Me.MainForm = Global.Questify.Builder.Client.MainForm
        End Sub

        <Global.System.Diagnostics.DebuggerStepThroughAttribute()> _
        Protected Overrides Sub OnCreateSplashScreen()
            Me.SplashScreen = Global.Questify.Builder.Client.SplashScreen
        End Sub
    End Class
End Namespace
