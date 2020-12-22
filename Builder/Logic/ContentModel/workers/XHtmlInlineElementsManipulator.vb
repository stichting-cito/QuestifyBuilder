Imports Cito.Tester.ContentModel
Imports System.Xml.Linq
Imports System.Linq
Imports System.IO
Imports Cito.Tester.Common
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Text
Imports Questify.Builder.Logic.HtmlHelpers
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Interfaces

Namespace ContentModel

    Friend Class XHtmlInlineElementsManipulator

        Private ReadOnly _cito As XNamespace = "http://www.cito.nl/citotester"
        Private ReadOnly _cachedParameterSet As New Dictionary(Of Integer, ParameterSetCollection)
        Private ReadOnly _arg As XHtmlParameter
        Private _bankId As Integer
        Private _itemCode As String = String.Empty
        Private _bankName As String = String.Empty

        Public Sub New(arg As XHtmlParameter)
            _arg = arg
        End Sub

        Public Function ReplaceInlineImages(bankId As Integer, bankName As String, itemCode As String) As HashSet(Of String)
            _itemCode = itemCode
            _bankId = bankId
            _bankName = bankName

            Dim ret As New HashSet(Of String)
            Dim doc As New XHtmlDocument()
            doc.LoadXml($"<body xmlns=""http://www.w3.org/1999/xhtml"">{_arg.Value}</body>")
            Dim namespaceManager = New XmlNamespaceManager(doc.NameTable)
            namespaceManager.AddNamespace("def", "http://www.w3.org/1999/xhtml")
            namespaceManager.AddNamespace("cito", "http://www.cito.nl/citotester")

            Dim inlineMediaTemplates As New Dictionary(Of String, String)
            Dim designerSettingsNodes As XmlNodeList = doc.SelectNodes("/Template/Settings/DesignerSetting[@key='inlineImageTemplate']")
            If designerSettingsNodes.Count = 1 Then
                inlineMediaTemplates.Add("image", designerSettingsNodes.Item(0).InnerText)
            End If
            Dim inlineImageLayoutTemplateName As String = InlineMediaTemplateHelper.GetInlineMediaTemplate(inlineMediaTemplates, "image", New DefaultHtmlInlineTemplateNames())

            Dim imageBasedOnOldItemLayoutNodes As XmlNodeList = doc.SelectNodes("//def:img[(@isinlineelement=""true"") and not(ancestor::def:a) and not(@ismathmlimage=""true"")]", namespaceManager)
            Dim isChanged As Boolean = ReplaceImageToInline(imageBasedOnOldItemLayoutNodes, namespaceManager, inlineImageLayoutTemplateName)
            If isChanged Then
                ret.Add(inlineImageLayoutTemplateName)
                For Each image As XmlElement In imageBasedOnOldItemLayoutNodes
                    If image.Attributes("src") IsNot Nothing Then
                        ret.Add(Path.GetFileName(image.GetAttribute("src")))
                    End If
                Next
            End If
            _arg.Value = doc.DocumentElement.InnerXml
            Return ret
        End Function

        Public Function ReplaceInlineElement(id As String, toReplace As InlineElement) As Boolean
            Dim doc = XDocument.Load(New StringReader("<root xmlns=""http://www.w3.org/1999/xhtml"">" + _arg.Value + "</root>"), LoadOptions.PreserveWhitespace)
            For Each e In doc.Descendants(_cito + "InlineElement")
                Dim str = OuterXmltoStr(e).Trim()
                Dim inline = DirectCast(SerializeHelper.XmlDeserializeFromString(str, GetType(InlineElement)), InlineElement)
                If (inline.Identifier = id) Then
                    Dim ns As New XmlSerializerNamespaces()
                    ns.Add("cito", "http://www.cito.nl/citotester")
                    Dim strTmp As String = SerializeHelper.XmlSerializeToString(toReplace, False, ns, False, Encoding.UTF8)
                    Dim doc2 = XDocument.Load(New StringReader(strTmp))

                    e.ReplaceWith(doc2.Elements().First)
                    _arg.Value = InnerXmltoStr(doc.Elements().First())
                    Return True
                End If
            Next

            Return False
        End Function

        Public Function RemoveInlineElement(ByVal id As String) As Boolean
            Dim doc = XDocument.Load(New StringReader("<root>" + _arg.Value + "</root>"), LoadOptions.PreserveWhitespace)
            For Each e In doc.Descendants(_cito + "InlineElement")
                Dim str = OuterXmltoStr(e).Trim()
                Dim inline = DirectCast(SerializeHelper.XmlDeserializeFromString(str, GetType(InlineElement)), InlineElement)
                If (inline.Identifier = id) Then
                    e.Remove()
                    _arg.Value = InnerXmltoStr(doc.Elements().First())
                    Return True
                End If
            Next

            Return False
        End Function


        Private Function ReplaceImageToInline(images As XmlNodeList, ns As XmlNamespaceManager, inlineImageLayoutTemplateName As String) As Boolean
            Dim returnValue As Boolean = False
            If images.Count > 0 Then
                Dim adapter As ItemLayoutAdapter = Nothing
                For Each childElement As XmlElement In images
                    If childElement.Attributes("src") IsNot Nothing AndAlso Not childElement.Attributes("src").Value.Contains("placeholderimage") Then
                        Dim inlineElement As InlineElement = New InlineElement()
                        inlineElement.Identifier = Guid.NewGuid().ToString()
                        inlineElement.LayoutTemplateSourceName = inlineImageLayoutTemplateName

                        Dim parameterSet As ParameterSetCollection
                        If _cachedParameterSet.ContainsKey(_bankId) Then
                            parameterSet = _cachedParameterSet(_bankId)
                        Else
                            If Not InlineMediaTemplateHelper.IsEmbeddedResourceInlineMediaTemplate(inlineImageLayoutTemplateName) Then
                                If adapter Is Nothing Then
                                    adapter = New ItemLayoutAdapter(inlineImageLayoutTemplateName, Nothing, AddressOf GenericHandler_ResourceNeeded)
                                End If
                                parameterSet = adapter.CreateParameterSetsFromItemTemplate()
                            Else
                                parameterSet = InlineMediaTemplateHelper.GetParameterSetFromEmbeddedResource(inlineImageLayoutTemplateName)
                            End If

                            _cachedParameterSet.Add(_bankId, parameterSet)
                        End If
                        inlineElement.Parameters.AddRange(parameterSet)
                        Dim inlineImageConverter As New InlineElement.InlineElementImageConverter(ns, "1", False)
                        inlineImageConverter.ConvertHtmlBasedOnOldItemLayoutToInlineElementLayout(childElement, inlineElement)

                        Dim newElement As XmlElement = childElement.OwnerDocument.CreateElement("img", childElement.NamespaceURI)
                        Dim xmlSerializerNamespaces As New XmlSerializerNamespaces()
                        xmlSerializerNamespaces.Add("cito", "http://www.cito.nl/citotester")
                        newElement.InnerXml = SerializeHelper.XmlSerializeToString(inlineElement, False, xmlSerializerNamespaces, True, Encoding.UTF8).Trim()
                        childElement.ParentNode.ReplaceChild(newElement.GetElementsByTagName("InlineElement", "http://www.cito.nl/citotester")(0), childElement)
                        returnValue = True
                    ElseIf childElement.Attributes("src") IsNot Nothing AndAlso childElement.Attributes("src").Value.Contains("placeholderimage") Then
                        Trace.WriteLine(
                            $"PlaceHolder image saved instead of resource in inline element: placeholder [{ _
                                           childElement.Attributes("src").Value}] item [{_itemCode}] bank [{_bankName}]")
                    End If

                Next

            End If

            Return returnValue
        End Function


        Private Function OuterXmltoStr(x As XElement) As String
            Dim reader = x.CreateReader()
            reader.MoveToContent()
            Return reader.ReadOuterXml()
        End Function

        Private Function InnerXmltoStr(x As XElement) As String
            Dim reader = x.CreateReader()
            reader.MoveToContent()
            Return reader.ReadInnerXml()
        End Function


        Private Sub GenericHandler_ResourceNeeded(ByVal sender As Object, ByVal e As ResourceNeededEventArgs)
            Dim resourceManager As IResourceService = ResourceFactory.Instance

            If (e.Command And ResourceNeededCommand.Resource) = ResourceNeededCommand.Resource Then
                Dim resource = resourceManager.GetResourceByNameWithOption(_bankId, e.ResourceName, New ResourceRequestDTO())
                resource.ResourceData = resourceManager.GetResourceData(resource)
                If resource.ResourceData IsNot Nothing AndAlso resource.ResourceData.BinData IsNot Nothing Then
                    Dim resourceString As String = String.Empty
                    Try
                        Dim xDoc As New XHtmlDocument
                        Dim stream = resource.ResourceData.GetStream()
                        Dim obj = StreamConverters.ConvertStreamToString(e.ResourceName, stream, Nothing)
                        If TypeOf obj Is String Then
                            xDoc.LoadXml(CType(obj, String))
                            resourceString = xDoc.OuterXml
                        End If
                    Catch ex As Exception
                        Trace.WriteLine($"Template: {e.ResourceName} in bank {_bankName} could not be opened {_itemCode}")
                    End Try
                    e.BinaryResource = New BinaryResource(resourceString)
                Else
                    Trace.WriteLine($"Template: {e.ResourceName} not found in bank {_bankName} while fixing item {_itemCode}")
                End If

            Else
                e.BinaryResource = New BinaryResource(New Object)
            End If
        End Sub
    End Class
End Namespace

