Imports Cito.Tester.ContentModel
Imports Cito.Tester.Common

Public Class MpegVideoViewer

    Protected Overrides Sub DataBind()

        Dim playerCode As String = "<html><body><div><object data=""{0}"" type=""video/x-msvideo"">" & _
                                                "<param name=""src"" value=""{0}""></param>" & _
                                                "<param name=""showcontrols"" value=""true""/>" & _
                                                "<param name=""autostart"" value=""false""/>" & _
                                            "</object></div></body></html>"


        If Me.IsResourceDataAvailable Then
            Dim resourceData As Byte() = DirectCast(TestSessionContext.GetResourceObject(Me.DataSource.Name, AddressOf Cito.Tester.Common.StreamConverters.ConvertStreamToByteArray), Byte())
            Dim fullPath As String = TempStorageHelper.CopyResourceToTempFolder(Me.DataSource.Name, resourceData, Me.DataSource.ResourceId.ToString())

            WebBrowser.Navigate("about:blank")
            WebBrowser.Document.Write(String.Format(playerCode, fullPath))
        End If
    End Sub

End Class
