Imports Cito.Tester.Common
Imports System.Reflection

Public NotInheritable Class SplashScreen

    Private Sub SplashScreen_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim thisAssembly As Assembly = Assembly.GetExecutingAssembly()
        Version.Text = String.Format(Version.Text, New Version(VersionHelper.GetFileVersion(thisAssembly)).ToString(3), VersionHelper.GetCopyrightInfo(thisAssembly))
    End Sub

End Class
