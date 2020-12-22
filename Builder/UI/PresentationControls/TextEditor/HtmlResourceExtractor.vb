Option Strict Off
Imports System.Xml
Imports System.Linq
Imports System.Text.RegularExpressions
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel

Friend Class HtmlResourceExtractor



    Private Shared RegexToRemovePackagePrefix As String = "resource://package(.*?)/"



    Private Sub New(ByVal namespaceManager As XmlNamespaceManager)

    End Sub



    Friend Shared Function GetAllResourcesInHtml(inlineElements As IEnumerable(Of InlineElement), ByVal html As String) As IEnumerable(Of String)
        Return GetAllResources(inlineElements, html)
    End Function

    Friend Shared Function GetImageResources(ByVal html As String) As IEnumerable(Of String)
        Dim namespaceManager = XHtmlParameterExtensions.GetNamespaceManager()
        Dim doc As XmlDocument = PrepareXmlDoc(html, namespaceManager)

        Dim imageNodes As XmlNodeList = doc.SelectNodes("//def:img[not(@isinlineelement=""true"")]", namespaceManager)
        Dim result As New List(Of String)
        Dim regexResult As Match = Nothing

        For Each node As XmlElement In imageNodes
            If node.HasAttribute("src") Then
                Dim srcAttributeValue As String = node.GetAttribute("src")

                If Regex.IsMatch(srcAttributeValue, RegexToRemovePackagePrefix) Then
                    Dim resourceName As String = Regex.Replace(srcAttributeValue, RegexToRemovePackagePrefix, String.Empty)
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

    Private Shared Function GetAllResources(inlineElements As IEnumerable(Of InlineElement), ByVal html As String) As IEnumerable(Of String)
        Dim returnValue As New List(Of String)

        For Each inline As InlineElement In inlineElements
            returnValue.AddRange(inline.GetResourcesFromResourceParameter().Concat(New String() {inline.LayoutTemplateSourceName}.ToList))
        Next

        returnValue.AddRange(GetImageResources(html))

        Return returnValue
    End Function


End Class
