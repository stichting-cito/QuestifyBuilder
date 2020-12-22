
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Net
Imports System.Text.RegularExpressions
Imports System.Windows.Forms
Imports System.Windows.Media.Imaging
Imports System.Xml
Imports System.Xml.Linq
Imports Cito.Tester.Common
Imports NLog
Imports Questify.Builder.Configuration
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.HelperFunctions
Imports Questify.Builder.Logic.Service.Logging
Imports Questify.Builder.Model.ContentModel.EntityClasses

Namespace HelperClasses

    Public Class MathMLHelper

        Private Const PngMathMLPath As String = "/tEXt/MathML"
        Private Const MathMlNamespace As String = "http://www.w3.org/1998/Math/MathML"
        Private Shared ReadOnly RegexMathMl As New Regex("<math[>\s](?s).*?</math>")
        Private Shared ReadOnly RegexMathImage As New Regex("^MFI_[0-9]{6,8}_.*?.png$")
        Public Shared ReadOnly DEFAULT_FONT As New Font("Times New Roman", 14)

        Public Shared Function IsValidMathMlExpression(ByRef mathMl As String) As Boolean
            Try
                Dim xDoc As New XDocument()
                xDoc = XDocument.Parse(mathMl)
                RemoveMathMlNamespace(xDoc, True)
                AddDefaultMathMlNamespace(xDoc)
                mathMl = xDoc.Root.OuterXml()
                Return RegexMathMl.IsMatch(mathMl)
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Shared Function HtmlContainsMathML(html As String) As Boolean
            Dim xDoc As XDocument
            Using strReader As New StringReader("<root>" + html + "</root>")
                Try
                    xDoc = XDocument.Load(strReader, LoadOptions.PreserveWhitespace)
                Catch
                    Return False
                End Try
            End Using
            Return xDoc.Descendants().Any(Function(d) d.Name.LocalName.Equals("math", StringComparison.InvariantCultureIgnoreCase))
        End Function

        Public Shared Function HtmlContainsMathMLImages(html As String) As Boolean
            Dim xDoc As XDocument
            Using strReader As New StringReader("<root>" + html + "</root>")
                xDoc = XDocument.Load(strReader, LoadOptions.PreserveWhitespace)
            End Using
            Return xDoc.Descendants().Any(Function(d) d.Name.LocalName.Equals("img", StringComparison.InvariantCultureIgnoreCase) AndAlso d.Attributes().Any(Function(attr As XAttribute) attr.Name.LocalName.Equals("ismathmlimage", StringComparison.InvariantCultureIgnoreCase)))
        End Function

        Public Shared Function GetMetaDataFromPngImage(ByVal mathMLImage As Byte()) As String
            Using memStream As New MemoryStream(mathMLImage)
                Dim pngDecoder As New PngBitmapDecoder(memStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default)
                Dim pngFrame As BitmapFrame = pngDecoder.Frames(0)
                Dim pngInplace As InPlaceBitmapMetadataWriter = pngFrame.CreateInPlaceBitmapMetadataWriter()
                Dim metaData As Object = pngInplace.GetQuery(PngMathMLPath)

                If metaData IsNot Nothing Then
                    Return metaData.ToString()
                End If
            End Using

            Return String.Empty
        End Function

        Public Shared Function SetMathMLMetaDataInImage(ByVal mathMLImage As Byte(), ByVal mathML As String) As Byte()
            Using inputMemStream As New MemoryStream(mathMLImage)
                Dim pngDecoder As New PngBitmapDecoder(inputMemStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default)
                Dim pngFrame As BitmapFrame = pngDecoder.Frames(0)
                Dim pngInplace As InPlaceBitmapMetadataWriter = pngFrame.CreateInPlaceBitmapMetadataWriter()
                Dim clonedBitmapMetaData As BitmapMetadata = DirectCast(pngFrame.Metadata, BitmapMetadata).Clone()
                Dim bitmapEncoder As New PngBitmapEncoder()

                If pngInplace.TrySave() Then
                    clonedBitmapMetaData.SetQuery(PngMathMLPath, mathML)
                    bitmapEncoder.Frames.Add(BitmapFrame.Create(pngFrame, pngFrame.Thumbnail, clonedBitmapMetaData, pngFrame.ColorContexts))
                End If

                Using outputMemStream As MemoryStream = New MemoryStream()
                    bitmapEncoder.Save(outputMemStream)
                    Return outputMemStream.ToArray()
                End Using
            End Using
        End Function

        Public Shared Sub SetMathMlNamespace(ByRef html As String)
            Dim xDoc As XDocument
            Using strReader As New StringReader("<root>" + html + "</root>")
                xDoc = XDocument.Load(strReader, LoadOptions.PreserveWhitespace)
            End Using
            SetMathMlNamespace(xDoc)
            html = xDoc.Root.InnerXml()
        End Sub

        Public Shared Sub AddDefaultMathMlNamespace(xDoc As XDocument)
            Dim mathMlTags As IEnumerable(Of XElement) = xDoc.Descendants("math")
            For Each mathMlTag As XElement In mathMlTags
                If mathMlTag.Attribute("xmlns") Is Nothing Then
                    mathMlTag.Add(New XAttribute("xmlns", MathMlNamespace))
                End If
            Next
        End Sub

        Public Shared Sub SetMathMlNamespace(xDoc As XDocument)
            Dim m As XNamespace = MathMlNamespace

            Dim mathMlTags As IEnumerable(Of XElement) = xDoc.Descendants(m + "math")
            SetMathMlNamespace(mathMlTags)
        End Sub

        Public Shared Sub SetMathMlNamespace(xElements As IEnumerable(Of XElement))
            Dim m As XNamespace = MathMlNamespace
            For Each mathMlTag As XElement In xElements
                SetMathMlNamespace(mathMlTag, m)
            Next
        End Sub

        Private Shared Sub SetMathMlNamespace(xElem As XElement, m As XNamespace)
            For Each mathMlTag As XElement In xElem.DescendantsAndSelf()
                mathMlTag.Name = m.GetName(mathMlTag.Name.LocalName)
                If mathMlTag.Attribute("xmlns") IsNot Nothing Then mathMlTag.Attribute("xmlns").Remove()
            Next
            xElem.Add(New XAttribute(XNamespace.Xmlns + "m", MathMlNamespace))
        End Sub

        Public Shared Sub RemoveMathMlNamespace(xDoc As XDocument, Optional convertElementNameToLocalName As Boolean = True)
            Dim m As XNamespace = MathMlNamespace
            Dim mathMlTags As IEnumerable(Of XElement) = xDoc.Descendants(m + "math")
            RemoveMathMlNamespace(mathMlTags, convertElementNameToLocalName)
        End Sub

        Public Shared Sub RemoveMathMlNamespace(xElements As IEnumerable(Of XElement), Optional convertElementNameToLocalName As Boolean = True)
            For Each mathMlTag As XElement In xElements
                RemoveMathMlNamespace(mathMlTag, convertElementNameToLocalName)
            Next
        End Sub

        Private Shared Sub RemoveMathMlNamespace(xElem As XElement, Optional convertElementNameToLocalName As Boolean = True)
            Dim attr As XAttribute = xElem.Attributes().FirstOrDefault(Function(x) x.IsNamespaceDeclaration)
            If attr IsNot Nothing Then
                attr.Remove()
            End If
            If convertElementNameToLocalName Then
                For Each el As XElement In xElem.DescendantsAndSelf()
                    el.Name = el.Name.LocalName
                Next
            End If
        End Sub

        Private Shared Function GetFontFromMathMl(ByVal mathMl As String, ByVal baseFont As Font) As Font
            Dim fontFamilyName = baseFont.FontFamily.Name
            Dim fontSize = CInt(baseFont.Size)
            Dim xmlDoc As XmlDocument = New XmlDocument()
            Dim ns As New XmlNamespaceManager(xmlDoc.NameTable)

            xmlDoc.LoadXml(mathMl)
            ns.AddNamespace("mml", MathMlNamespace)

            Dim styleNode = xmlDoc.SelectSingleNode("//mml:mi[@style]", ns)
            If styleNode IsNot Nothing Then
                fontFamilyName = styleNode.Attributes("style").Value.Split(":"c)(1).Trim("'"c)
            End If

            Dim sizeNode = xmlDoc.SelectSingleNode("//mml:mi[@mathsize]", ns)
            If sizeNode IsNot Nothing Then
                Dim sizeString = sizeNode.Attributes("mathsize").Value.Replace("px", "")
                Dim size = 0
                If Integer.TryParse(sizeString, size) Then
                    fontSize = size
                End If
            End If

            Return New Font(fontFamilyName, fontSize)
        End Function

        Public Shared Function GetBaseFont() As Font
            Return If(UISettings.FormulaEditorFont IsNot Nothing, UISettings.FormulaEditorFont, DEFAULT_FONT)
        End Function

        Public Shared Function CreateImageOptions(mathMl As String, baseFont As Font) As Dictionary(Of String, String)
            Dim font = GetFontFromMathMl(mathMl, baseFont)
            Return CreateImageOptions(font)
        End Function

        Public Shared Function CreateImageOptions(font As Font) As Dictionary(Of String, String)
            Dim options As New Dictionary(Of String, String)

            options.Add("fontSize", $"{CInt(font.Size)}px")
            options.Add("fontFamily", font.FontFamily.Name)
            options.Add("centerbaseline", "false")

            Return options
        End Function

        Public Shared Function StoreMathMLImageInTempStore(ByVal mathMLImage As Byte(), ByVal imageResourceUri As Uri) As Uri
            Dim imageFileName As Uri

            If imageResourceUri Is Nothing Then
                Dim tmpFileName = Path.ChangeExtension(TempStorageHelper.GetTempFilename(), ".png")
                imageFileName = New Uri(tmpFileName)
            Else
                imageFileName = New Uri(Path.Combine(TempStorageHelper.GetTempStoragePath(), Path.GetFileName(imageResourceUri.AbsoluteUri)))
            End If

            Try
                File.WriteAllBytes(imageFileName.LocalPath, mathMLImage)
            Catch pathTooLongEx As PathTooLongException
                Dim _logger = LogManager.GetCurrentClassLogger()
                _logger.Log(LogLevel.Error, String.Format("Error user: '{0}' while saving file: '{1}' in [StoreMathMLImageInTempStore]: '{2}'", LogHelper.GetUser(), imageFileName.LocalPath, LogHelper.GetErrorMessage(pathTooLongEx)))
                Throw
            Catch ex As Exception
                Throw ex
            End Try

            Return imageFileName
        End Function

        Public Shared Function StoreMathMLImageAsGenericResource(ByVal tempImageUri As Uri, ByVal bankId As Integer) As String
            Dim imageNameUsedInBank As String = String.Empty
            Dim imageGenericResource As GenericResourceEntity = Nothing
            Dim tempFilename As String = IO.Path.GetFileName(tempImageUri.LocalPath)

            If tempImageUri.Scheme = "file" Then
                Dim fileName As String = Path.GetFileName(tempImageUri.ToString())
                Dim existingGenericResource As GenericResourceEntity = CType(ResourceFactory.Instance.GetResourceByNameWithOption(bankId, fileName, New ResourceRequestDTO()), GenericResourceEntity)
                imageNameUsedInBank = $"MFI_{Date.Today.Year:####}{Date.Today.Month:##}{Date.Today.Day:##}_{Date.Now.Hour:##}_{Date.Now.Minute:##}_{Date.Now.Second:##}_{Date.Now.Millisecond:###}.png"
                If existingGenericResource Is Nothing Then
                    imageGenericResource = CreateNewPngImageGenericResource(imageNameUsedInBank, bankId)
                Else
                    If existingGenericResource.ResourceData Is Nothing Then
                        existingGenericResource.ResourceData = ResourceFactory.Instance.GetResourceData(existingGenericResource)
                    End If
                    imageGenericResource = existingGenericResource
                    imageGenericResource.Name = imageNameUsedInBank
                End If
                imageGenericResource.ResourceData.BinData = FileHelper.MakeByteArrayFromFile(tempImageUri.LocalPath)
            Else
                imageNameUsedInBank = tempFilename
            End If

            If imageGenericResource IsNot Nothing Then
                Dim helper As New MediaDimensionsHelper()
                Dim size = helper.GetDimensions(imageGenericResource.MediaType, imageGenericResource.ResourceData.BinData)
                If Not size.IsEmpty Then
                    imageGenericResource.Dimensions = String.Format("{0} x {1}", size.Width, size.Height)
                End If
                Dim result As String = ResourceFactory.Instance.UpdateGenericResource(imageGenericResource)

                If Not String.IsNullOrEmpty(result) Then
                    MessageBox.Show(result, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            End If

            Return imageNameUsedInBank
        End Function

        Private Shared Function CreateNewPngImageGenericResource(ByVal name As String, ByVal bankId As Integer) As GenericResourceEntity
            Dim genericResource As New GenericResourceEntity()

            With genericResource
                .ResourceId = Guid.NewGuid()
                .BankId = bankId
                .Version = "0.1"
                .ResourceData = New ResourceDataEntity()
                .Name = name
                .Title = My.Resources.MathFormulaImage
                .Description = My.Resources.MathFormulaImageOfResource
                .MediaType = "image/png"
            End With

            Return genericResource
        End Function

        Public Shared Function ResourceIsMathImage(ByVal resource As ResourceEntity) As Boolean
            Return TypeOf resource Is GenericResourceEntity AndAlso DirectCast(resource, GenericResourceEntity).MediaType = "image/png" _
                   AndAlso (resource.Title = "Wiskundige formule afbeelding" OrElse resource.Title = "Math formula image") _
                   AndAlso RegexMathImage.IsMatch(resource.Name)
        End Function

        Public Shared Function ConvertMathMLImagesToMathML(html As String, Optional bankId As Integer = 0) As String
            Dim xDoc As XDocument
            Using strReader As New StringReader("<root>" + html + "</root>")
                xDoc = XDocument.Load(strReader, LoadOptions.PreserveWhitespace)
            End Using

            ProcessMathFormulaImages(xDoc, bankId)

            Return xDoc.Root.InnerXml()
        End Function

        Private Shared Sub ProcessMathFormulaImages(xDoc As XDocument, Optional bankId As Integer = 0)
            Dim elementsToRemove As New List(Of XElement)
            Dim mathImageTags As IEnumerable(Of XElement) = xDoc.Descendants().Where(Function(d) d.Attributes.Any(Function(attr As XAttribute) attr.Name.LocalName.Equals("ismathmlimage", StringComparison.InvariantCultureIgnoreCase)))
            For Each mathImageTag As XElement In mathImageTags
                Dim srcAttr = mathImageTag.Attributes.FirstOrDefault(Function(a) a.Name.LocalName.Equals("src", StringComparison.InvariantCultureIgnoreCase))
                If srcAttr IsNot Nothing AndAlso Not String.IsNullOrEmpty(srcAttr.Value) Then
                    If srcAttr.Value.StartsWith("data:image/png;base64,", StringComparison.InvariantCultureIgnoreCase) Then
                        Dim mathMlAttr = mathImageTag.Attributes.FirstOrDefault(Function(a) a.Name.LocalName.Equals("mathml_value", StringComparison.InvariantCultureIgnoreCase))
                        If mathMlAttr IsNot Nothing AndAlso Not String.IsNullOrEmpty(mathMlAttr.Value) Then
                            mathImageTag.AddAfterSelf(XElement.Parse(WebUtility.HtmlDecode(mathMlAttr.Value)))
                        End If
                        elementsToRemove.Add(mathImageTag)
                    ElseIf srcAttr.Value.StartsWith("resource://package", StringComparison.InvariantCultureIgnoreCase) AndAlso bankId > 0 Then
                        Dim image = ResourceFactory.Instance.GetResourceByNameWithOption(bankId, Path.GetFileName(srcAttr.Value), New ResourceRequestDTO())
                        If image IsNot Nothing Then
                            image.ResourceData = ResourceFactory.Instance.GetResourceData(image)
                            If image?.ResourceData?.BinData IsNot Nothing AndAlso ResourceIsMathImage(image) Then
                                Dim mathMl = GetMetaDataFromPngImage(image.ResourceData.BinData)
                                If Not String.IsNullOrEmpty(mathMl) Then
                                    If Not String.IsNullOrEmpty(mathMl) Then
                                        mathImageTag.AddAfterSelf(XElement.Parse(mathMl))
                                    End If
                                    elementsToRemove.Add(mathImageTag)
                                End If
                            End If
                        End If
                    End If
                End If
            Next

            elementsToRemove.ForEach(Sub(e)
                                         e.Remove()
                                     End Sub)
        End Sub

    End Class
End Namespace