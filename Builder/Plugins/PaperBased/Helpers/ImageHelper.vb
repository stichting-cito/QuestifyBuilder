Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Xml.XPath
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ImageGenerator

Public Class ImageHelper

    Private _resourceManager As ResourceManagerBase
    Private _regexPackageUrl As String = "(resource://package|file:///)([a-zA-Z0-9\~\!\@\#\$\%\^\&amp;\*\(\)_\ \-\=\+\\\/\?\.\:\;\'\,]*)?"
    Private _regexImage As String = "<img.+?src=[\"" ""](.+?)[\""""].*?>"
    Private _tempFolder As String = TempStorageHelper.GetTempStoragePath

    Public Sub New(resourceManager As ResourceManagerBase)
        _resourceManager = resourceManager
    End Sub

    Public Sub DownloadImages(ByRef xhtmlString As String)
        Dim fileName As String
        Dim tempPathRoot As String = Path.Combine(_tempFolder, Path.GetRandomFileName)
        Dim tempPathRootInfo As New DirectoryInfo(tempPathRoot)
        tempPathRootInfo.Create()

        Dim imageResources As List(Of String) = GetImageSrcFromString(xhtmlString.ToString(), _regexImage)

        For Each resource As String In imageResources
            If IsImageFromMemory(resource) Then
                fileName = CheckFileForWrongChars(Path.Combine(tempPathRoot, TempStorageHelper.GetTemporaryFileName("png")))
                SaveResourceToDisk(resource, fileName)
                UpdateMemoryReferenceInTemplate(xhtmlString, resource, fileName)
            Else
                Dim fileNameWithoutExtension As String = Path.GetFileNameWithoutExtension(resource)
                Dim fileExtension As String = Path.GetExtension(resource).ToLower()

                If Not String.IsNullOrEmpty(fileExtension) Then
                    fileName = String.Format("{0}{1}{2}", GetDirFromPath(tempPathRoot), fileNameWithoutExtension, fileExtension)
                Else
                    fileName = String.Format("{0}{1}", GetDirFromPath(tempPathRoot), fileNameWithoutExtension)
                End If
                fileName = CheckFileForWrongChars(fileName)
                resource = CheckFileForWrongChars(resource)

                SaveResourceToDisk(resource, fileName)
                UpdateReferenceInTemplate(xhtmlString, resource, fileName)
            End If
        Next
    End Sub

    Public Function StoreTempImage(bytes As Byte()) As String
        Dim fileName As String = Path.Combine(_tempFolder, Path.GetRandomFileName)
        File.WriteAllBytes(fileName, bytes)
        Return fileName
    End Function

    Private Function GetImageSrcFromString(xmlHtmlString As String, regexExpression As String) As List(Of String)
        Dim imageResources As New List(Of String)
        Dim matches As MatchCollection = ExecuteRegexFromString(xmlHtmlString, regexExpression, RegexOptions.IgnoreCase)
        For Each match As Match In matches
            imageResources.Add(GetImageSrc(match.Value).ToString())
        Next
        Return imageResources
    End Function

    Public Function GetImageSrc(imgXmlString As String) As String
        Dim imgSrc As String = String.Empty
        Dim xmlDoc As XDocument = XDocument.Parse("<root>" + Regex.Replace(imgXmlString, "(xmlns:?[^=]*=[""][^""]*[""])", String.Empty, RegexOptions.IgnoreCase) + "</root>")
        Dim imgElements As IEnumerable(Of XElement) = xmlDoc.XPathSelectElements(".//img")
        For Each element As XElement In imgElements
            Try
                imgSrc = element.Attribute("src").Value
            Catch
            End Try
        Next
        Return imgSrc
    End Function

    Private Function ExecuteRegexFromString(xmlHtmlString As String, regexExpression As String, regexOptions As RegexOptions) As MatchCollection
        Dim regex As New Regex(regexExpression, regexOptions)
        Return regex.Matches(xmlHtmlString.Trim())
    End Function

    Private Function IsImageFromMemory(imgValue As String) As Boolean
        Return imgValue.StartsWith("data:image/png;base64,")
    End Function

    Private Function CheckFileForWrongChars(filename As String) As String
        filename = Replace(filename, "%20", " ")
        If Right(filename, 1) = "'" Then filename = filename.Remove(Len(filename) - 1)
        Return filename
    End Function

    Private Function GetDirFromPath(path As String) As String
        Return (path.Substring(0, path.LastIndexOf("\") + 1))
    End Function

    Private Sub SaveResourceToDisk(resource As String, ByRef filename As String)
        Dim uri As New Uri(resource)

        If uri.Scheme = "file" Then
            If Not FileHelper.HasMatchingFileExtensionAndMimeType(filename) Then
                filename = Path.Combine(Path.GetDirectoryName(filename), String.Format("{0}.{1}", Path.GetFileNameWithoutExtension(filename), FileHelper.GetFileExtensionByMimeType(FileHelper.GetMimeFromFile(uri.LocalPath))))
            End If
            SaveFile(File.ReadAllBytes(uri.LocalPath), filename)
        ElseIf uri.Scheme = "resource" Then
            Dim eventArgs As New ResourceNeededEventArgs(Path.GetFileName(resource), AddressOf StreamConverters.ConvertStreamToByteArray)
            ResourceNeeded(Me, eventArgs)

            If (eventArgs.BinaryResource IsNot Nothing) Then
                Dim file As Byte() = CType(eventArgs.BinaryResource.ResourceObject, Byte())
                If Not FileHelper.HasMatchingFileExtensionAndMimeType(filename) Then
                    filename = Path.Combine(Path.GetDirectoryName(filename), String.Format("{0}.{1}", Path.GetFileNameWithoutExtension(filename), FileHelper.GetFileExtensionByMimeType(FileHelper.GetMimeFromByteArray(filename, file))))
                End If
                SaveFile(CType(eventArgs.BinaryResource.ResourceObject, Byte()), filename)
            Else
                Dim isNotSupportedPlaceholder As Boolean = False
                Dim notSupportedPlaceholder As Byte() = Nothing

                Dim regexResult As Match = Nothing
                isNotSupportedPlaceholder = RegexpHelper.TryMatchNotSupportedPlaceholder(resource, regexResult)

                If isNotSupportedPlaceholder Then
                    Dim width As Integer = CType(regexResult.Groups(RegexpHelper.WIDTH).Value, Integer)
                    Dim height As Integer = CType(regexResult.Groups(RegexpHelper.HEIGHT).Value, Integer)
                    Dim text As String = regexResult.Groups(RegexpHelper.TEXT).Value
                    Dim notSupportedPlaceholderGenerator As New NotSupportedPlaceholderGenerator
                    notSupportedPlaceholder = notSupportedPlaceholderGenerator.CreateImage(width, height, text)

                    SaveFile(notSupportedPlaceholder, filename)
                End If
            End If
        ElseIf uri.Scheme = "data" Then
            Dim bytes As Byte() = Convert.FromBase64String(resource.Replace("data:image/png;base64,", ""))
            SaveFile(bytes, filename)
        End If

    End Sub

    Private Sub ResourceNeeded(sender As Object, e As ResourceNeededEventArgs)
        Dim _resource As BinaryResource = Nothing

        Dim request = New ResourceRequestDTO()
        If (e.Command And ResourceNeededCommand.Resource) = ResourceNeededCommand.Resource Then
            If e.TypedResourceType IsNot Nothing Then
                _resource = _resourceManager.GetTypedResource(e.ResourceName, e.TypedResourceType, request)
            Else
                _resource = _resourceManager.GetResource(e.ResourceName, e.StreamProcessingDelegate, request)
            End If
            e.BinaryResource = _resource
        End If
    End Sub

    Private Sub SaveFile(file() As Byte, filePath As String)
        Using stream As Stream = New MemoryStream(file)
            Using fileStream As New FileStream(filePath, FileMode.Create, FileAccess.Write)
                ReadWriteStream(stream, fileStream)
            End Using
        End Using
    End Sub

    Public Shared Sub ReadWriteStream(readStream As Stream, writeStream As Stream)
        Dim Length As Integer = 256
        Dim buffer As [Byte]() = New [Byte](Length - 1) {}
        Dim bytesRead As Integer = readStream.Read(buffer, 0, Length)
        While bytesRead > 0
            writeStream.Write(buffer, 0, bytesRead)
            bytesRead = readStream.Read(buffer, 0, Length)
        End While
        readStream.Close()
        writeStream.Close()
    End Sub

    Private Sub UpdateMemoryReferenceInTemplate(ByRef parsedTemplate As String, resource As String, newReference As String)
        parsedTemplate = parsedTemplate.Replace(resource.ToString(), newReference)
    End Sub

    Private Sub UpdateReferenceInTemplate(ByRef parsedTemplate As String, resourceName As String, newReference As String)
        Dim urlRegex As New Regex(_regexPackageUrl, RegexOptions.IgnoreCase)
        parsedTemplate = Replace(parsedTemplate, "%20", " ")
        For Each match As Match In urlRegex.Matches(resourceName)
            parsedTemplate = parsedTemplate.Replace(match.Value.ToString(), newReference)
        Next
    End Sub

End Class