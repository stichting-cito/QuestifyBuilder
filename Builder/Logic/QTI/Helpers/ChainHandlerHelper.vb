Imports System.Collections.Concurrent
Imports System.IO
Imports System.IO.Compression
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Xml
Imports System.Xml.Linq
Imports System.Xml.Serialization
Imports System.Xml.XPath
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI_Base

Namespace QTI.Helpers

    Public Class ChainHandlerHelper


        Private Const ITEMPREFIX As String = "ITM"
        Private Const RESOURCEPREFIX As String = "RES"
        Private Const TESTPREFIX As String = "TST"
        Private Const PREFIXSEPARATOR As String = "-"



        Private Const RegexSrcBase64 As String = "(?<src>src="")(?<resourceName>{0})"""
        Private Const RegexPackageUrl As String = "resource://package([a-zA-Z0-9\~\!\@\#\$\%\^\&amp;\*\(\)_\ \-\=\+\\\/\?\.\:\;\'\,]*)?"
        Private Const RegexPackageUrlToReplace As String = "resource://package([a-zA-Z0-9\~\!\@\#\$\%\^\&amp;\*\(\)_\ \-\=\+\\\/\?\.\:\;\'\,]*)?/{0}"
        Private Const RegexBetweenTags As String = "<{0}\b[^>]*>(.*?)</{0}>"
        Private Shared ReadOnly FilesWriting As New ConcurrentDictionary(Of String, Boolean)



        Public Shared Function RemoveAllNamespaces(ByVal element As String) As String
            Return Regex.Replace(element, "(xmlns:?[^=]*=[""][^""]*[""])", "", RegexOptions.IgnoreCase Or RegexOptions.Multiline)
        End Function

        Public Shared Function RemoveAttribute(ByVal element As String, attributeName As String, matchPart As Boolean) As String
            Dim wildCard As String = String.Empty
            If matchPart Then
                wildCard = ".*?"
            End If
            Return Regex.Replace(element, $"\s*({attributeName}:{wildCard})=\s*""[^""]*""\s*", "", RegexOptions.IgnoreCase Or RegexOptions.Multiline)
        End Function

        Public Shared Function RemoveNamespaces(ByVal element As String, ByVal namespacePrefixes As List(Of String), ByVal removeDefaultNamespaces As Boolean) As String
            Dim result As String = element
            If namespacePrefixes IsNot Nothing Then
                For Each namespacePrefix As String In namespacePrefixes
                    result = Regex.Replace(result, $"(xmlns:{namespacePrefix}=[""][^""]*[""])", "", RegexOptions.IgnoreCase Or RegexOptions.Multiline)
                Next

            End If
            If (removeDefaultNamespaces) Then
                Return Regex.Replace(result, "(xmlns=[""][^""]*[""])", "", RegexOptions.IgnoreCase Or RegexOptions.Multiline)
            Else
                Return result
            End If
        End Function

        Public Shared Function RemoveHtmlAttribute(input As String, attributeName As String) As String
            Dim validAttributeOrTagNameRegEx As New Regex("^\w+$", RegexOptions.Compiled And RegexOptions.IgnoreCase)
            If validAttributeOrTagNameRegEx.IsMatch(attributeName) Then
                Dim reg As New Regex($"(<[^>]*){attributeName}\s*=\s*('|"")[^\2]*?\2([^>]*>)", RegexOptions.IgnoreCase And RegexOptions.Multiline)
                Return reg.Replace(input, Function(item) item.Groups(1).Value + item.Groups(3).Value)
            End If
            Return input
        End Function



        Public Shared Sub AddNewChild(
                              ByVal navigator As XPathNavigator,
                              ByVal namespacePrefix As String,
                              ByVal namespaceUri As String,
                              ByVal elementName As String,
                              ByVal val As String,
                              setNavigatorToNewElement As Boolean,
                              skipIfNothing As Boolean)
            Dim value = val
            If value = String.Empty Then
                value = Nothing
            End If

            If (String.IsNullOrEmpty(value) AndAlso skipIfNothing) Then
                Return
            End If

            navigator.AppendChildElement(namespacePrefix, elementName, namespaceUri, value)
            If (value Is Nothing) AndAlso Not setNavigatorToNewElement Then
                Return
            End If

            If Not navigator.MoveToChild(elementName, namespaceUri) Then
                Throw New XmlException($"Failed to move to child '{elementName}'. Navigator is at Node: {DirectCast(navigator.UnderlyingObject, XmlElement).Name}")
            End If
            MoveNavigatorToLastElement(navigator)

            If (value IsNot Nothing) Then
                navigator.SetTypedValue(value)
            End If
            If Not (setNavigatorToNewElement) Then
                navigator.MoveToParent()
            End If
        End Sub

        Public Shared Sub AddNewChild(
                              ByVal navigator As XPathNavigator,
                              ByVal namespacePrefix As String,
                              ByVal namespaceUri As String,
                              ByVal elementName As String,
                              ByVal value As String,
                              setNavigatorToNewElement As Boolean)

            AddNewChild(navigator, namespacePrefix, namespaceUri, elementName, value, setNavigatorToNewElement, False)

        End Sub

        Public Shared Sub AddAttribute(
                               ByRef navigator As XPathNavigator,
                               attributeName As String,
                               value As String,
                               prefix As String,
                               nameSpaceUri As String,
                               skipIfEmpty As Boolean)

            If Not (String.IsNullOrEmpty(value) AndAlso skipIfEmpty) Then
                navigator.CreateAttribute(prefix, attributeName, nameSpaceUri, value)
            End If
        End Sub

        Public Shared Sub AddAttribute(ByRef navigator As XPathNavigator, attributeName As String, value As String)
            AddAttribute(navigator, attributeName, value, String.Empty, String.Empty, False)
        End Sub

        Public Shared Sub AppendChild(ByRef navigator As XPathNavigator, value As String, ByVal setNavigatorToNewElement As Boolean)
            Dim stringToAppend As String = $"<{value}/>"
            navigator.AppendChild(stringToAppend)
            If setNavigatorToNewElement Then
                MoveNavigatorToLastElement(navigator, True)
            End If
        End Sub

        Public Shared Sub MoveNavigatorToLastElement(ByVal navigator As XPathNavigator, navToElementFirst As Boolean)
            If navToElementFirst Then
                navigator.MoveToChild(XPathNodeType.Element)
            End If
            While (navigator.MoveToNext(XPathNodeType.Element))
            End While
        End Sub

        Public Shared Sub MoveNavigatorToLastElement(ByVal navigator As XPathNavigator)
            MoveNavigatorToLastElement(navigator, False)
        End Sub




        Public Shared Sub ExtractZipToDirectory(zipReader As StreamReader, ByVal path As String)
            Dim archive = New ZipArchive(zipReader.BaseStream)
            archive.ExtractToDirectory(path)
        End Sub

        Public Shared Sub AddFilesToZip(ByVal zipFilename As String, ByVal sourceFolder As String)
            If (sourceFolder.Length = 0) Then
                Throw New ArgumentException("You must specify a source folder from which files will be added to the zip file.", "sourceFolder")
            End If

            If File.Exists(zipFilename) Then
                Return
            End If

            Using archive = ZipFile.Open(zipFilename, ZipArchiveMode.Create)
                For Each f In Directory.GetFiles(sourceFolder, "*", SearchOption.AllDirectories)
                    archive.CreateEntryFromFile(f, f.Replace(sourceFolder, "").TrimStart("\"c).Replace("\"c, "/"c))
                Next
            End Using
        End Sub



        Public Shared Function GetAllResourcesFromTemplate(parsedTemplate As String) As MatchCollection
            Dim urlRegex As New Regex(RegexPackageUrl, RegexOptions.IgnoreCase And RegexOptions.Singleline)
            Return urlRegex.Matches(parsedTemplate.Trim)
        End Function

        Public Shared Sub UpdateReferenceInTemplate(ByRef parsedTemplate As String, resourceName As String, newReference As String)
            Dim urlRegex As New Regex(String.Format(RegexPackageUrlToReplace, resourceName), RegexOptions.IgnoreCase)
            parsedTemplate = urlRegex.Replace(parsedTemplate, newReference)
        End Sub

        Public Shared Sub UpdateBase64ToFileReference(ByRef parsedTemplate As String, base64String As String, newReference As String)
            Dim escapedString = Regex.Escape(base64String)
            Dim urlRegex As New Regex(String.Format(RegexSrcBase64, escapedString), RegexOptions.IgnoreCase)
            parsedTemplate = urlRegex.Replace(parsedTemplate, New MatchEvaluator(Function(m As Match) As String
                                                                                     Return $"{m.Groups("src").Value}../{newReference}"""
                                                                                 End Function))
        End Sub

        Public Shared Function GetEveryThingBetweenTags(parsedTemplate As String, tags As List(Of String)) As String
            Dim sb As New StringBuilder(String.Empty)
            tags.ForEach(Sub(t)
                             sb.Append(GetEveryThingBetweenTag(parsedTemplate, t))
                         End Sub)
            Return sb.ToString
        End Function

        Private Shared Function GetEveryThingBetweenTag(parsedTemplate As String, tag As String) As String
            Dim rx As New Regex(String.Format(RegexBetweenTags, tag), RegexOptions.Singleline)
            Dim matchCollection As MatchCollection = rx.Matches(parsedTemplate)
            Dim sb As New StringBuilder(String.Empty)

            For Each m As Match In matchCollection
                sb.Append(m.Groups(m.Groups.Count - 1).ToString().Trim)
            Next

            Return sb.ToString
        End Function

        Public Shared Function RemoveTags(parsedTemplate As String, tags As List(Of String), Optional removeTagOnly As Boolean = False) As String
            Dim template As String = parsedTemplate
            tags.ForEach(Sub(t)
                             template = RemoveTag(template, t, removeTagOnly)
                         End Sub)

            Return template
        End Function

        Public Shared Function RemoveTag(parsedTemplate As String, tag As String, Optional removeTagOnly As Boolean = False) As String
            Dim template As String = parsedTemplate
            Dim rx As New Regex(String.Format(RegexBetweenTags, tag), RegexOptions.Singleline)
            Dim matchCollection As MatchCollection = rx.Matches(template)
            Dim shouldDoubleCheck As Boolean

            For Each m As Match In matchCollection
                If m.Groups(1).Value.Contains($"<{tag}") AndAlso Not removeTagOnly Then
                    shouldDoubleCheck = True
                    Continue For
                End If

                If removeTagOnly Then
                    template = template.Replace(m.Value, m.Groups(m.Groups.Count - 1).ToString().Trim)
                Else
                    template = template.Replace(m.Value, String.Empty)
                End If
            Next

            If shouldDoubleCheck OrElse rx.Matches(template).Count > 0 Then
                template = RemoveTag(template, tag, removeTagOnly)
            End If

            Return template
        End Function

        Public Shared Function ReplaceTags(parsedTemplate As String, tags As Dictionary(Of String, String)) As String
            Dim template As String = parsedTemplate

            For Each tag As KeyValuePair(Of String, String) In tags
                template = ReplaceTag(template, tag.Key, tag.Value, "<{0}")
                template = ReplaceTag(template, tag.Key, tag.Value, "</{0}>")
            Next

            Return template
        End Function

        Private Shared Function ReplaceTag(parsedTemplate As String, tagToReplace As String, replacingTag As String, tagFormat As String) As String
            Dim template As String = parsedTemplate
            Dim rx As New Regex(String.Format(tagFormat, tagToReplace))
            Dim matchCollection As MatchCollection = rx.Matches(template)

            For Each m As Match In matchCollection
                template = template.Replace(m.Value, String.Format(tagFormat, replacingTag))
            Next

            Return template
        End Function

        Public Shared Function GetFilenameFromPath(ByVal path As String) As String
            Return path.Substring((path.LastIndexOf(CChar("/")) + 1), path.Length - (path.LastIndexOf(CChar("/")) + 1)).Trim
        End Function

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

        Public Shared Function GetCategoryFolderFromFileName(filename As String) As PackageCreatorConstants.FileDirectoryType
            Dim returnValue As PackageCreatorConstants.FileDirectoryType = PackageCreatorConstants.FileDirectoryType.other
            Dim extension As String = Path.GetExtension(filename)

            Select Case extension.ToLower
                Case ".bmp", ".emf", ".gif", ".icon", ".jpeg", ".jpg", ".png", ".wmf", ".tiff", ".exif", ".raw", ".webp"
                    returnValue = PackageCreatorConstants.FileDirectoryType.img
                Case ".264", ".3g2", ".3gp", ".3gp2", ".3gpp", ".3gpp2", ".aaf", ".ale", ".amv", ".amx", ".asf", ".asx", ".avi", ".avp", ".avs", ".bdm", ".bik", ".bin", ".mp4", ".mov", ".mpg", ".3gp", ".rm", ".mkv", ".divx", ".vob"
                    returnValue = PackageCreatorConstants.FileDirectoryType.video
                Case ".mp3", ".aac"
                    returnValue = PackageCreatorConstants.FileDirectoryType.audio
                Case ".css"
                    returnValue = PackageCreatorConstants.FileDirectoryType.css
                Case ".xsd"
                    returnValue = PackageCreatorConstants.FileDirectoryType.controlxsds
                Case ".pdf"
                    returnValue = PackageCreatorConstants.FileDirectoryType.attachments
            End Select

            Return returnValue
        End Function

        Public Shared Function ConvertMimeType(mimeType As String, fileName As String) As String
            Dim returnValue As String = mimeType
            Dim extension As String = Path.GetExtension(fileName)

            Select Case extension.ToLower
                Case ".webm"
                    If mimeType.Split("/"c)(0).ToLower = "application" Then
                        returnValue = "video/webm"
                    End If
                Case ".ogv", ".opus"
                    If mimeType = "video/x-ogg" OrElse mimeType.Split("/"c)(0).ToLower = "application" Then
                        returnValue = "video/ogg"
                    End If
                Case ".ogg"
                    returnValue = "audio/ogg"
                Case ".mp4"
                    If mimeType.Split("/"c)(0).ToLower = "application" Then
                        returnValue = "video/mp4"
                    End If
                Case ".mp3"
                    returnValue = "audio/mpeg"
                Case ".aac"
                    returnValue = "audio/aac"
                Case ".png"
                    returnValue = "image/png"
                Case ".pdf"
                    returnValue = "application/pdf"
            End Select

            Return returnValue
        End Function

        Public Shared Function GetCategoryFolderFromFile(mimeType As String, fileName As String) As PackageCreatorConstants.FileDirectoryType
            Dim returnValue As PackageCreatorConstants.FileDirectoryType = PackageCreatorConstants.FileDirectoryType.other

            Select Case mimeType
                Case "application/x-pointplus"
                    returnValue = PackageCreatorConstants.FileDirectoryType.css
                Case "application/xhtml+xml", "application/x-customInteraction", "application/vnd.geogebra.file"
                    returnValue = PackageCreatorConstants.FileDirectoryType.ref
                Case "application/x-portableCustomInteraction"
                    returnValue = PackageCreatorConstants.FileDirectoryType.pci
                Case "application/pdf"
                    returnValue = PackageCreatorConstants.FileDirectoryType.attachments
                Case LogicFileHelper.QuestifyThemeMimeType
                    returnValue = PackageCreatorConstants.FileDirectoryType.template
            End Select

            If returnValue.ToString = PackageCreatorConstants.FileDirectoryType.other.ToString Then
                Select Case mimeType.Split("/"c)(0).ToLower
                    Case "image"
                        returnValue = PackageCreatorConstants.FileDirectoryType.img
                    Case "video"
                        returnValue = PackageCreatorConstants.FileDirectoryType.video
                    Case "text"
                        returnValue = PackageCreatorConstants.FileDirectoryType.ref
                    Case "audio"
                        returnValue = PackageCreatorConstants.FileDirectoryType.audio
                End Select
            End If

            Return returnValue
        End Function

        Public Shared Function GetMimeTypeFromFile(byteArray() As Byte, fileName As String) As String
            Dim mimeType As String = FileHelper.GetMimeFromByteArray(fileName, byteArray)

            If mimeType.Equals("application/x-zip-compressed") AndAlso byteArray IsNot Nothing Then
                Dim resourcePath As String = Path.Combine(TempStorageHelper.GetTempStoragePath(), fileName)
                SaveFile(byteArray, resourcePath)

                If LogicFileHelper.FileIsQuestifyTheme(resourcePath) Then
                    mimeType = LogicFileHelper.QuestifyThemeMimeType
                End If

                If File.Exists(resourcePath) Then
                    File.Delete(resourcePath)
                End If
            End If

            Return mimeType
        End Function

        Public Shared Function IsSourceTextFile(resourceFile() As Byte, resourceName As String) As Boolean
            Dim mimeType As String = GetMimeTypeFromFile(resourceFile, resourceName)

            If mimeType.Contains("application/xhtml+xml") _
               OrElse mimeType.Contains("text/plain") _
               OrElse mimeType.Contains("text/html") _
                Then
                Return True
            End If

            Return False
        End Function

        Public Shared Sub SaveDocument(objectToSave As Object, filePath As String, ByVal entityName As String, ByRef filesPerEntity As ConcurrentDictionary(Of String, List(Of String)), ns As XmlSerializerNamespaces)
            Dim xmlDocument = ObjectToXmlDocument(objectToSave, ns)
            SaveDocument(xmlDocument, filePath, entityName, filesPerEntity)
        End Sub

        Public Shared Function ObjectToXmlDocument(objectToSerialize As Object, ns As XmlSerializerNamespaces, encoding As String) As XmlDocument
            Dim tempDocument As New XmlDocument
            tempDocument.PreserveWhitespace = True
            Dim x As New XmlSerializer(objectToSerialize.GetType)
            Dim sb As New StringBuilder()

            Using writer = XmlWriter.Create(sb, New XmlWriterSettings() With {.Indent = False})
                x.Serialize(writer, objectToSerialize, ns)
                tempDocument.LoadXml(sb.ToString)
            End Using

            If encoding IsNot Nothing Then
                Dim xmlDeclaration = tempDocument.CreateXmlDeclaration("1.0", encoding, Nothing)

                If tempDocument.FirstChild.OuterXml.StartsWith("<?xml") Then
                    tempDocument.ReplaceChild(xmlDeclaration, tempDocument.FirstChild)
                Else
                    tempDocument.InsertBefore(xmlDeclaration, tempDocument.FirstChild)
                End If
            End If

            Return tempDocument
        End Function

        Public Shared Function ObjectToXmlDocument(objectToSerialize As Object, ns As XmlSerializerNamespaces) As XmlDocument
            Return ObjectToXmlDocument(objectToSerialize, ns, Nothing)
        End Function

        Public Shared Function ObjectToString(objectToSerialize As Object, ns As XmlSerializerNamespaces, ommitxmlDeclaration As Boolean) As String
            Dim returnValue As String
            Dim x As New XmlSerializer(objectToSerialize.GetType)

            Using writer As New StringWriter()
                x.Serialize(writer, objectToSerialize, ns)
                returnValue = writer.ToString

                If ommitxmlDeclaration AndAlso returnValue.StartsWith("<?xml") Then
                    Dim xTemp = XElement.Parse(returnValue)
                    returnValue = xTemp.ToString
                End If
            End Using

            Return returnValue
        End Function

        Public Shared Function ObjectToAny(objectToConvert As Object, ns As XmlSerializerNamespaces) As XmlElement
            Return ObjectToXmlDocument(objectToConvert, ns).DocumentElement
        End Function

        Public Shared Function StringToObject(stringValue As String, type As Type) As Object
            Dim returnValue As Object
            Dim x As New XmlSerializer(type)

            Using sr As New StringReader(stringValue)
                Try
                    Dim xmlReader As XmlTextReader = New XmlTextReader(sr)
                    xmlReader.WhitespaceHandling = WhitespaceHandling.All
                    xmlReader.Normalization = True

                    returnValue = x.Deserialize(xmlReader)
                Catch ex As Exception
                    Throw New Exception($"Error while deserialize to {type.ToString}: {ex.Message}")
                End Try
            End Using

            Return returnValue
        End Function

        Public Shared Sub SaveDocument(xmlDocument As XmlDocument, filePath As String, ByVal entityName As String, ByRef filesPerEntity As ConcurrentDictionary(Of String, List(Of String)))
            If xmlDocument Is Nothing Then
                Return
            End If

            SaveDocument(xmlDocument, filePath)
            Dim list As List(Of String)

            If filesPerEntity Is Nothing Then
                Return
            End If

            If Not filesPerEntity.ContainsKey(entityName) Then
                list = New List(Of String)
                filesPerEntity.TryAdd(entityName, list)
            Else
                list = filesPerEntity.Item(entityName)
            End If

            list.Add(filePath)
        End Sub

        Public Shared Sub SaveDocument(xmlDocument As XmlDocument, filePath As String)
            If xmlDocument Is Nothing Then
                Return
            End If

            If Not Directory.Exists(Path.GetDirectoryName(filePath)) Then
                Directory.CreateDirectory(Path.GetDirectoryName(filePath))
            End If

            Using writer As XmlTextWriter = New XmlTextWriter(filePath, New UTF8Encoding(False))
                writer.Formatting = Formatting.None
                xmlDocument.Save(writer)
            End Using
        End Sub

        Public Shared Sub SaveFile(file() As Byte, filePath As String)
            If Not FilesWriting.TryAdd(filePath, True) Then
                Return
            End If

            If Not Directory.Exists(Path.GetDirectoryName(filePath)) Then
                Directory.CreateDirectory(Path.GetDirectoryName(filePath))
            End If

            Using stream As Stream = New MemoryStream(file)
                Using fileStream As New FileStream(filePath, FileMode.Create, FileAccess.Write)
                    ReadWriteStream(stream, fileStream)
                End Using
            End Using

            Select Case Path.GetExtension(filePath)
                Case ".js", ".css"
                    ScriptHelper.MinifyFile(filePath)
            End Select

            FilesWriting.TryRemove(filePath, True)
        End Sub

        Public Shared Sub SaveFile(inputString As String, filePath As String)
            Dim input As String = inputString
            If Not FilesWriting.TryAdd(filePath, True) Then
                Return
            End If


            If Not Directory.Exists(Path.GetDirectoryName(filePath)) Then
                Directory.CreateDirectory(Path.GetDirectoryName(filePath))
            End If

            Select Case Path.GetExtension(filePath)
                Case ".js"
                    input = ScriptHelper.MinifyJsString(input)
                Case ".css"
                    input = ScriptHelper.MinifyCssString(input)
            End Select

            File.WriteAllText(filePath, input)
            FilesWriting.TryRemove(filePath, True)
        End Sub



        Public Shared Function GetIdentifierFromResourceId(resourceName As String, type As PackageCreatorConstants.TypeOfResource) As String
            Dim prefix As String = String.Empty

            Select Case type
                Case PackageCreatorConstants.TypeOfResource.item
                    prefix = ITEMPREFIX
                Case PackageCreatorConstants.TypeOfResource.resource
                    prefix = RESOURCEPREFIX
                Case PackageCreatorConstants.TypeOfResource.test
                    prefix = TESTPREFIX
            End Select

            Debug.Assert(Not resourceName.Contains($"{prefix}-"), "Prefix Already added!!")

            If Not resourceName.Contains(String.Format("{0}" + PREFIXSEPARATOR, prefix)) Then
                Return String.Format("{0}" + PREFIXSEPARATOR + "{1}", prefix, resourceName.Replace(Chr(32), "_"c).Replace(".", "_"))
            Else
                Return resourceName.Replace(Chr(32), "_"c).Replace(".", "_")
            End If
        End Function

        Public Shared Function GetPossibleIdentifiersForResourceName(resourceName As String, stylesheetHelper As StyleSheetHelper) As String()
            Dim name As String = resourceName
            Dim result As New List(Of String)
            If GetCategoryFolderFromFileName(name) = PackageCreatorConstants.FileDirectoryType.css Then
                name = stylesheetHelper.PrefixStylesheet(name)
            End If
            result.Add(GetIdentifierFromResourceId(name, PackageCreatorConstants.TypeOfResource.item))
            result.Add(GetIdentifierFromResourceId(name, PackageCreatorConstants.TypeOfResource.resource))
            result.Add(GetIdentifierFromResourceId(name, PackageCreatorConstants.TypeOfResource.test))
            Return result.ToArray()
        End Function

        Public Shared Function RemovePrefixFromResourceIdentifier(identifier As String, type As PackageCreatorConstants.TypeOfResource) As String
            Dim result As String = identifier
            Select Case type
                Case PackageCreatorConstants.TypeOfResource.item
                    If result.StartsWith(ITEMPREFIX + PREFIXSEPARATOR) Then result = result.Remove(0, ITEMPREFIX.Length + PREFIXSEPARATOR.Length)
                Case PackageCreatorConstants.TypeOfResource.resource
                    If result.StartsWith(RESOURCEPREFIX + PREFIXSEPARATOR) Then result = result.Remove(0, RESOURCEPREFIX.Length + PREFIXSEPARATOR.Length)
                Case PackageCreatorConstants.TypeOfResource.test
                    If result.StartsWith(TESTPREFIX + PREFIXSEPARATOR) Then result = result.Remove(0, TESTPREFIX.Length + PREFIXSEPARATOR.Length)
            End Select
            Return result
        End Function

        Public Shared Function TimeLimitsIsEmpty(timeLimits As TimeLimits) As Boolean
            Dim returnValue As Boolean = True

            If timeLimits IsNot Nothing AndAlso Not (timeLimits.MinTime = 0 AndAlso timeLimits.MaxTime = 0) Then
                returnValue = False
            End If

            Return returnValue
        End Function



        Public Shared Function GetBase64Images(content As String) As Dictionary(Of String, String)
            Dim result As New Dictionary(Of String, String)
            Dim xmlDoc = XDocument.Parse("<root>" + content + "</root>")
            Dim imgElements = xmlDoc.XPathSelectElements("//img[starts-with(@src, 'data:image/png;base64,')]")

            For Each element In imgElements
                Try
                    Dim tmpFilename = GetTemporaryFileName("png")
                    Dim base64String = element.Attribute("src").Value
                    result(tmpFilename) = base64String
                Catch
                End Try
            Next

            Return result
        End Function

        Private Shared Function GetTemporaryFileName(ByVal extension As String) As String
            Dim tmpFilename = Path.GetTempFileName()

            If Not String.IsNullOrEmpty(extension) Then
                tmpFilename = Path.ChangeExtension(tmpFilename, extension)
            End If

            Return Path.GetFileName(tmpFilename)
        End Function
    End Class
End Namespace