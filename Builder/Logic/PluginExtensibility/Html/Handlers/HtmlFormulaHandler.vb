Imports System.Globalization
Imports System.IO
Imports System.Net
Imports System.Xml
Imports Cito.Tester.Common
Imports HtmlAgilityPack
Imports Questify.Builder.Logic.PluginExtensibility.Html.Converters
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Model.ContentModel.EntityClasses

Namespace PluginExtensibility.Html.Handlers

    Public Class HtmlFormulaHandler
        Inherits HtmlHandlerBase

        Public Sub New(ByVal editor As IXHtmlEditor, ByVal resourceManager As ResourceManagerBase, ByVal namespaceManager As XmlNamespaceManager, ByVal bankId As Integer)
            MyBase.New(editor, bankId, resourceManager, Nothing)
        End Sub

        Public ReadOnly Property IsMathMLImage As Boolean
            Get
                Return editor.Selection.Node IsNot Nothing AndAlso editor.Selection.Node.Name = "img" AndAlso editor.Selection.Node.Attributes("ismathmlimage") IsNot Nothing
            End Get
        End Property

        Friend Function GetSelectedMathMLImage() As XmlNode
            If TypeOf editor.Selection.Node Is XmlElement AndAlso editor.Selection.Node.Name.ToLower() = "img" Then
                Return editor.Selection.Node
            End If

            Return Nothing
        End Function

        Friend Function ConvertPastedImagesToMathMLFormulas(ByVal pastedTags As HtmlNodeCollection) As Boolean
            If pastedTags IsNot Nothing Then
                For Each pastedImageTag As HtmlNode In pastedTags
                    Dim mathMlValue As String = pastedImageTag.Attributes("mathml_value").Value
                    If Not String.IsNullOrEmpty(mathMlValue) Then
                        Dim converter As New HtmlConverter_MathMLToMathImage(PluginHelper.MathMlPlugin)
                        Dim convertedMathMlImage = converter.ConvertHtml(mathMlValue)
                        Dim newNode As HtmlNode = HtmlNode.CreateNode(convertedMathMlImage)
                        pastedImageTag.ParentNode.ReplaceChild(newNode, pastedImageTag)
                    Else
                        Return False
                    End If
                Next
            End If
            Return True
        End Function

        Public Sub EditMathMlFormula(ByVal image As Byte(), ByVal imageName As String, ByVal verticalAlignValue As Double, ByVal mathMlValue As String)
            Dim existingImage As XmlNode = GetSelectedMathMLImage()

            If existingImage IsNot Nothing Then
                Dim srcAttribute = existingImage.Attributes("src")

                Dim mathMlAttribute = editor.Document.CreateAttribute("mathml_value")
                mathMlAttribute.InnerXml = WebUtility.HtmlEncode(mathMlValue)
                existingImage.Attributes.Append(mathMlAttribute)

                srcAttribute.Value = String.Format("data:image/png;base64,{0}", Convert.ToBase64String(image))

                Dim styleAttribute As XmlAttribute = Nothing
                If existingImage.Attributes("style") Is Nothing Then
                    styleAttribute = existingImage.OwnerDocument.CreateAttribute("style")
                    existingImage.Attributes.Append(styleAttribute)
                Else
                    styleAttribute = existingImage.Attributes("style")
                End If
                styleAttribute.InnerXml = String.Format("vertical-align:{0}px;", (verticalAlignValue - 1) * -1).ToString(CultureInfo.InvariantCulture)
                If existingImage.Attributes("class") IsNot Nothing Then
                    existingImage.Attributes("class").InnerXml = ""
                End If
            Else
                Dim newImageElement As XmlElement = editor.Document.CreateElement("img", namespaceManager.LookupNamespace("def"))

                Dim newAttribute = editor.Document.CreateAttribute("src")
                newAttribute.InnerXml = String.Format("data:image/png;base64,{0}", Convert.ToBase64String(image))
                newImageElement.Attributes.Append(newAttribute)

                newAttribute = editor.Document.CreateAttribute("id")
                newAttribute.InnerXml = Guid.NewGuid().ToString()
                newImageElement.Attributes.Append(newAttribute)

                newAttribute = editor.Document.CreateAttribute("ismathmlimage")
                newAttribute.InnerXml = "true"
                newImageElement.Attributes.Append(newAttribute)

                newAttribute = editor.Document.CreateAttribute("mathml_value")
                newAttribute.InnerXml = WebUtility.HtmlEncode(mathMlValue)
                newImageElement.Attributes.Append(newAttribute)

                newAttribute = editor.Document.CreateAttribute("alt")
                newAttribute.InnerXml = String.Empty

                newAttribute = editor.Document.CreateAttribute("style")
                newAttribute.InnerXml = String.Format("vertical-align:{0}px;", (verticalAlignValue - 1) * -1).ToString(CultureInfo.InvariantCulture)
                newImageElement.Attributes.Append(newAttribute)

                Dim range As ITextRange = editor.CreateRangeFromSelection()
                range.SetXmlElement(newImageElement)
            End If

        End Sub

        Public Function ReadMathMlAttribute() As String
            Dim result As String = String.Empty
            Dim node = GetSelectedMathMLImage()
            If (node Is Nothing) Then
                Return result
            End If

            Dim attr = node.Attributes("mathml_value")
            If (attr Is Nothing OrElse attr.Value Is Nothing) Then
                Return result
            End If
            Return WebUtility.HtmlDecode(attr.Value)
        End Function

        Public Function ReadMathMLImage() As Byte()
            Dim mathMLNode As XmlNode = GetSelectedMathMLImage()

            If mathMLNode IsNot Nothing Then
                Dim imageResourceUri As String = editor.Selection.Node.Attributes("src").Value
                Dim imageUri As Uri = New Uri(imageResourceUri)
                If imageUri.Host = "package" Then
                    Dim ImageGenericResource As GenericResourceEntity = DirectCast(ResourceFactory.Instance.GetResourceByNameWithOption(bankId, imageUri.Segments(1), New ResourceRequestDTO()), GenericResourceEntity)
                    If ImageGenericResource IsNot Nothing Then
                        ImageGenericResource.ResourceData = ResourceFactory.Instance.GetResourceData(ImageGenericResource)

                        Using memStream As New MemoryStream(DirectCast(ImageGenericResource.ResourceData.BinData, Byte()))
                            Return memStream.ToArray()
                        End Using
                    End If
                Else
                    If imageUri.Scheme = "file" Then
                        Using fileStream As FileStream = New FileStream(imageUri.LocalPath, IO.FileMode.Open, IO.FileAccess.Read)
                            Using memStream As New MemoryStream()
                                memStream.SetLength(fileStream.Length)
                                fileStream.Read(memStream.GetBuffer(), 0, CInt(fileStream.Length))
                                Return memStream.ToArray()
                            End Using
                        End Using

                    ElseIf imageUri.Scheme = "data" Then
                        Return Convert.FromBase64String(imageResourceUri.Replace("data:image/png;base64,", ""))
                    End If
                End If
            End If

            Return Nothing
        End Function

    End Class
End Namespace