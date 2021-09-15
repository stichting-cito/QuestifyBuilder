
Imports System.Deployment.Application
Imports System.IO
Imports System.Text
Imports Cito.Tester.Common
Imports System.Reflection

''' <summary>
''' Dialog for showing productinformation of this application
''' </summary>
Public NotInheritable Class AboutDialog

    Private Sub AboutBox_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        ' Set the title of the form.
        Dim applicationTitle As String
        If My.Application.Info.Title <> String.Empty Then
            applicationTitle = My.Application.Info.Title
        Else
            applicationTitle = Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If
        Text = $"{My.Resources.AboutDialog_About} {applicationTitle}"

        ' Initialize all of the text displayed on the About Box.
        Dim thisAssembly As Assembly = Assembly.GetExecutingAssembly()
        LabelProductName.Text = My.Application.Info.ProductName

        LabelVersion.Text =
            $"{My.Resources.AboutDialog_Version} {VersionHelper.GetInformationalVersion(thisAssembly)} ({ _
                VersionHelper.GetFileVersion(thisAssembly)})"
        LabelCopyright.Text = VersionHelper.GetCopyrightInfo(thisAssembly)
        TextBoxDescription.Text = CreateInfoString()
    End Sub

    Private Function CreateInfoString() As String

        Dim sb As New StringBuilder()

        sb.AppendFormat("Configuration:{0}{0}", Environment.NewLine)

        If ApplicationDeployment.IsNetworkDeployed AndAlso ApplicationDeployment.CurrentDeployment.ActivationUri IsNot Nothing Then
            sb.AppendFormat("Activation URI: " + ApplicationDeployment.CurrentDeployment.ActivationUri.ToString() + "{0}", vbNewLine)
        End If

        sb.Append(vbNewLine + vbNewLine + vbNewLine)
        sb.Append(Encoding.UTF8.GetString(My.Resources.Components))

        Return sb.ToString()
    End Function


    Private Sub OKButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles OKButton.Click
        Close()
    End Sub

End Class
