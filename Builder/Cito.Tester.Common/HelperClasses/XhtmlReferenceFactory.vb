Imports System.Xml

Public Class XhtmlReferenceFactory

    Public Shared Function ParseXhtmlReference(xhtml As String) As XhtmlReferenceList
        Return ParseXhtmlReference(xhtml, XhtmlReferenceType.None)
    End Function

    Public Shared Function ParseXhtmlReference(xhtml As String, type As XhtmlReferenceType) As XhtmlReferenceList
        Dim references As New XhtmlReferenceList()

        If Not String.IsNullOrEmpty(xhtml) Then
            Dim doc As New XHtmlDocument()
            doc.LoadXml(xhtml)

            Dim nsmgr As XmlNamespaceManager = New XmlNamespaceManager(doc.NameTable)
            nsmgr.AddNamespace("xhtml", "http://www.w3.org/1999/xhtml")
            nsmgr.AddNamespace("cito", "http://www.cito.nl/citotester")

            Dim filter As String
            If type = XhtmlReferenceType.None Then
                filter = String.Empty
            Else
                filter = $" and @cito:reftype='{type.ToString()}'"
            End If

            Dim xhtmlReferences As XmlNodeList = doc.SelectNodes($"//xhtml:span[@cito:type='reference'{filter}]", nsmgr)



            For Each node As XmlNode In xhtmlReferences

                Dim id As String = node.Attributes("id").Value
                Dim refType As XhtmlReferenceType = DirectCast([Enum].Parse(GetType(XhtmlReferenceType), node.Attributes("cito:reftype").Value), XhtmlReferenceType)

                Dim description As String = String.Empty
                If node.Attributes("cito:description") IsNot Nothing Then
                    description = node.Attributes("cito:description").Value
                End If

                Dim value As String = String.Empty
                If node.Attributes("cito:value") IsNot Nothing Then
                    value = node.Attributes("cito:value").Value
                End If
                references.Add(New XhtmlReference(id, refType, description, value))
            Next
        End If

        Return references
    End Function

End Class
