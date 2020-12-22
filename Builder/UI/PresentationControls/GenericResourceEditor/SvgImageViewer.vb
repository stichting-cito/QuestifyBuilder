Imports Cito.Tester.ContentModel

Public Class SvgImageViewer

    Protected Overrides Sub DataBind()
        Dim playerCode As String = "<html><head><meta http-equiv=""X-UA-Compatible"" content=""IE=Edge"" /></head><body><img src=""data:image/svg+xml;base64,{0}""></img></body></html>"

        If Me.IsResourceDataAvailable Then
            Dim resourceData As Byte() = DirectCast(TestSessionContext.GetResourceObject(Me.DataSource.Name, AddressOf Cito.Tester.Common.StreamConverters.ConvertStreamToByteArray), Byte())
            WebBrowser.DocumentText = String.Format(playerCode, Convert.ToBase64String(resourceData))
        End If
    End Sub

End Class
