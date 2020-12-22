Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Xml
Imports Cito.Tester.Common
Imports MediaInfoDotNet

Public Class MediaDimensionsHelper

    Public Function GetDimensions(mediaType As String, ByRef rawBytes As Byte()) As Size

        Dim result = Size.Empty
        Try
            If mediaType.Contains("image/svg") Then
                result = GetSvgDimensions(rawBytes)
            ElseIf mediaType.Contains("image") Then
                result = GetImageSize(rawBytes)
            ElseIf mediaType.Contains("video") Then
                result = GetVideoSize(rawBytes)
            ElseIf mediaType.Equals("application/x-portableCustomInteraction", StringComparison.InvariantCultureIgnoreCase) Then
                result = GetCiSize(rawBytes)
            ElseIf mediaType.Contains("customInteraction") Then
                result = GetCiSize(rawBytes)
            End If
        Catch ex As Exception
            Debug.Assert(False)
        End Try
        Return result
    End Function

    Private Function GetCiSize(rawBytes As Byte()) As Size
        Dim result As Size
        Dim tempfileName = TempStorageHelper.GetTempFilename()
        Dim ciDimensionsHelper = new CustomInterActionDimensionsHelper()

        File.WriteAllBytes(tempfileName, rawBytes)
        result = ciDimensionsHelper.GetCiDimensions(tempfileName)
        File.Delete(tempfileName)

        Return result
    End Function

    Private Function GetImageSize(rawBytes As Byte()) As Size
        Dim result = Size.Empty
        Using mediaStream As New MemoryStream(rawBytes)
            Using resourceImage As Image = Image.FromStream(mediaStream)
                result.Width = resourceImage.Width
                result.Height = resourceImage.Height
            End Using
        End Using
        Return result
    End Function

    Private Function GetSvgDimensions(rawBytes As Byte()) As Size
        Dim result = Size.Empty

        Dim fileContents As String = ASCIIEncoding.ASCII.GetString(rawBytes)
        Dim svg As New XmlDocument()
        Dim namespaceManager As New XmlNamespaceManager(svg.NameTable)
        namespaceManager.AddNamespace("def", "http://www.w3.org/2000/svg")
        svg.LoadXml(fileContents)
        If svg.DocumentElement.Name = "svg" Then
            result = New Size(Convert.ToInt32(svg.SelectSingleNode("//def:svg/@width", namespaceManager).Value.TrimEnd("px".ToCharArray())), Convert.ToInt32(svg.SelectSingleNode("//def:svg/@height", namespaceManager).Value.TrimEnd("px".ToCharArray())))
        End If
        Return result
    End Function

    Private Function GetVideoSize(rawBytes As Byte()) As Size
        Dim result = Size.Empty
        Using stream As New BufferedStream(New MemoryStream(rawBytes))
            Using mediaData As New MediaFile(stream)
                result = GetSize(mediaData)
            End Using
        End Using
        Return result
    End Function

    Public Function GetVideoSize(file As String) As Size
        Dim result = Size.Empty
        Using mediaData As New MediaFile(file)
            result = GetSize(mediaData)
        End Using
        Return result
    End Function

    Private Function GetSize(mediaData As MediaFile) As Size
        If mediaData.Video.Any() Then
            Dim video = mediaData.Video.First().Value
            If video IsNot Nothing Then
                Return New Size(video.width, video.height)
            End If
        End If
        Return Size.Empty
    End Function

End Class
