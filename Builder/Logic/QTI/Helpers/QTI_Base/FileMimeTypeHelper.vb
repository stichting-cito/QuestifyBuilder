Imports System.Collections.Concurrent
Imports System.IO
Imports System.Xml

Namespace QTI.Helpers.QTI_Base

    Public Class FileMimeTypeHelper
        Public Shared Sub ModifyMimeType(ByRef content As String, resourceMimeTypeDictionary As ConcurrentDictionary(Of String, String))
            Dim tempDoc As New XmlDocument
            tempDoc.PreserveWhitespace = True
            tempDoc.LoadXml($"<wrapper>{content}</wrapper>")
            Dim xmlnode = tempDoc.DocumentElement
            If xmlNode IsNot Nothing Then
                Dim ns As New XmlNamespaceManager(xmlNode.OwnerDocument.NameTable)
                For Each objectNode As XmlNode In xmlnode.SelectNodes("//object", ns)
                    If objectNode.Attributes("data") Is Nothing Then Continue For

                    Dim resourceName As String = objectNode.Attributes("data").InnerText
                    resourceName = Path.GetFileName(resourceName)
                    If resourceMimeTypeDictionary.ContainsKey(resourceName) Then
                        objectNode.Attributes("type").InnerText = resourceMimeTypeDictionary(resourceName)
                    End If
                Next
            End If
            content = xmlnode.InnerXml
        End Sub
    End Class
End NameSpace