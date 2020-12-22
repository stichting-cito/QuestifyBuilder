Imports Cito.Tester.ContentModel
Imports System.Xml
Imports System.Text.RegularExpressions

Namespace ContentModel
    Friend Class XHtmlResourceExtractor

        Private ReadOnly _arg As XHtmlParameter


        Public Sub New(arg As XHtmlParameter)
            _arg = arg
        End Sub


        Public Function ExtractResources() As HashSet(Of String)
            Dim ret As New HashSet(Of String)
            Dim dictionaryOfInlineElements = _arg.GetInlineElements()

            For Each inline As InlineElement In dictionaryOfInlineElements.Values
                ret.Add(inline.LayoutTemplateSourceName)
                Dim ParamSetColl As New ParameterSetCollectionResourceExtractor(inline.Parameters)
                ret.UnionWith(ParamSetColl.ExtractResources())
            Next
            ret.UnionWith(GetImageResources(_arg.ToString))
            Return ret
        End Function

        Public Function ReplaceInlineImages(bankId As Integer, bankName As string, itemCode As String) As HashSet(Of String)
            Dim inlineExtractor As New XHtmlInlineElementsManipulator(_arg)

            Return inlineExtractor.ReplaceInlineImages(bankId, bankName, itemCode)
        End Function


        Private Function GetImageResources(ByVal html As String) As HashSet(Of String)
            Dim namespaceManager = New Xml.XmlNamespaceManager(New System.Xml.NameTable())
            namespaceManager.AddNamespace("def", "http://www.w3.org/1999/xhtml")
            namespaceManager.AddNamespace("cito", "http://www.cito.nl/citotester")
            Dim doc As XmlDocument = PrepareXmlDoc(html, namespaceManager)
            Dim RegexToRemovePackagePrefix As String = "resource://package(.*?)/"
            Dim imageNodes As XmlNodeList = doc.SelectNodes("//def:img[not(@isinlineelement=""true"")]", namespaceManager)
            Dim result As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)

            For Each node As XmlElement In imageNodes
                If node.HasAttribute("src") Then
                    Dim srcAttributeValue As String = node.GetAttribute("src")
                    If Regex.IsMatch(srcAttributeValue, RegexToRemovePackagePrefix) Then
                        Dim resourceName As String = Uri.UnescapeDataString(Regex.Replace(srcAttributeValue, RegexToRemovePackagePrefix, String.Empty))
                        If Not result.Contains(resourceName) Then
                            result.Add(resourceName)
                        End If
                    End If
                End If
            Next

            Return result
        End Function
        Private Shared Function PrepareXmlDoc(ByVal html As String, ByVal namespaceManager As XmlNamespaceManager) As XmlDocument
            Dim doc As New XmlDocument()
            Dim root As XmlElement = doc.CreateElement("html", namespaceManager.LookupNamespace("def"))
            root.InnerXml = html
            doc.AppendChild(root)

            Return doc
        End Function

    End Class
End Namespace