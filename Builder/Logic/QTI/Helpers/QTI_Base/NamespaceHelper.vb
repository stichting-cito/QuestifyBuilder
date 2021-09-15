Imports System.Linq
Imports System.Xml
Imports System.Xml.Linq
Imports System.Xml.Serialization
Imports Questify.Builder.Logic.HelperClasses
Imports Questify.Builder.Logic.Service.HelperFunctions

Namespace QTI.Helpers.QTI_Base

    Public Class NamespaceHelper

        Protected Overridable Function GetImsQtiNamespaces() As Dictionary(Of String, String)
            Return New Dictionary(Of String, String) From {
            {"imsqti", "http://www.imsglobal.org/xsd/imsqtiasi_v3p0"},
            {"", "http://www.w3.org/2001/XMLSchema"}
            }
        End Function

        Public Overridable Function GetNamespaceManager() As XmlSerializerNamespaces
            Return Nothing
        End Function

        Public Overridable Function GetBaseItemExtensionDocument() As XDocument
            Return Nothing
        End Function

        Public Overridable Function GetImsQtiNamespace() As XNamespace
            Return Nothing
        End Function

        Public Overridable Function GetImsMetadataNamespace() As XNamespace
            Return Nothing
        End Function

        Public Overridable Function GetSSMLNamespace() As XNamespace
            Return Nothing
        End Function

        Public Overridable Function GetImsManifestXmlSerializerNamespaces() As XmlSerializerNamespaces
            Return Nothing
        End Function

        Public Overridable Function GetDepXmlSerializerNamespaces() As XmlSerializerNamespaces
            Return Nothing
        End Function

        Public Overridable Function GetDepXmlNamespaceManager(nameTable As XmlNameTable) As XmlNamespaceManager
            Return Nothing
        End Function

        Public Overridable Sub UpdateNameSpaces(ByRef doc As XmlDocument, Optional updateQtiNamespaces As Boolean = False)
            UpdateMathMlNamespaces(doc)
            UpdateImsMetadataNamespaces(doc)
            If updateQtiNamespaces Then
                UpdateImsQtiNamespaces(doc)
            End If
        End Sub

        Private Sub UpdateMathMlNamespaces(ByRef doc As XmlDocument)
            If doc IsNot Nothing AndAlso MathMLHelper.HtmlContainsMathML(doc.DocumentElement.OuterXml) Then
                doc.DocumentElement.SetAttribute("xmlns:m", "http://www.w3.org/1998/Math/MathML")
                Dim xDoc = XDocument.Parse(doc.DocumentElement.OuterXml, LoadOptions.PreserveWhitespace)
                MathMLHelper.RemoveMathMlNamespace(xDoc, False)
                doc.LoadXml(xDoc.Root.OuterXml())
            End If
        End Sub

        Private Sub UpdateImsMetadataNamespaces(ByRef doc As XmlDocument)
            Dim imsmetadata = GetImsMetadataNamespace()
            If doc IsNot Nothing AndAlso imsmetadata IsNot Nothing Then
                Dim xDoc As XDocument = XDocument.Parse(doc.DocumentElement.OuterXml, LoadOptions.PreserveWhitespace)
                If xDoc.Descendants().Any(Function(d) d.Name.Namespace = imsmetadata) Then
                    doc.DocumentElement.SetAttribute("xmlns:meta", imsmetadata.ToString())
                    xDoc = XDocument.Parse(doc.DocumentElement.OuterXml, LoadOptions.PreserveWhitespace)
                    For Each xElem As XElement In xDoc.Descendants().Where(Function(d) d.Name.Namespace = imsmetadata)
                        Dim attr As XAttribute = xElem.Attributes().FirstOrDefault(Function(x) x.IsNamespaceDeclaration AndAlso x.Value.Equals(imsmetadata.ToString(), StringComparison.InvariantCultureIgnoreCase))
                        If attr IsNot Nothing Then
                            attr.Remove()
                        End If
                    Next
                End If

                doc.LoadXml(xDoc.Root.OuterXml())
            End If
        End Sub

        Private Sub UpdateImsQtiNamespaces(ByRef doc As XmlDocument)
            If doc Is Nothing Then
                Return
            End If

            Dim xDoc As XDocument = XDocument.Parse(doc.DocumentElement.OuterXml, LoadOptions.PreserveWhitespace)
            Dim unusedNamespaces As New List(Of XAttribute)
            For Each namespaceToUpdate As KeyValuePair(Of String, String) In GetImsQtiNamespaces()
                If xDoc.Descendants().Any(Function(d) d.Name.Namespace = namespaceToUpdate.Value) Then
                    RemoveNamespaceFromTag(namespaceToUpdate, doc, xDoc)
                Else
                    AddToUnusedNamespaces(namespaceToUpdate, unusedNamespaces, xDoc)
                End If
            Next

            RemoveUnusedNamespaces(unusedNamespaces, doc, xDoc)
        End Sub

        Private Sub RemoveNamespaceFromTag(namespaceToUpdate As KeyValuePair(Of String, String),
                                          ByRef doc As XmlDocument,
                                          xDoc As XDocument)
            doc.DocumentElement.SetAttribute($"xmlns:{namespaceToUpdate.Key}", namespaceToUpdate.Value)
            xDoc = XDocument.Parse(doc.DocumentElement.OuterXml, LoadOptions.PreserveWhitespace)
            For Each xElem As XElement In xDoc.Descendants().Where(Function(d) d.Name.Namespace = namespaceToUpdate.Value)
                Dim attrs = xElem.Attributes().Where(Function(x) x.IsNamespaceDeclaration AndAlso GetImsQtiNamespaces().ContainsValue(x.Value))
                attrs.ToList().ForEach(Sub(attr)
                                           attr.Remove()
                                       End Sub)
            Next

            doc.LoadXml(xDoc.Root.OuterXml())
        End Sub

        Private Sub AddToUnusedNamespaces(namespaceToUpdate As KeyValuePair(Of String, String),
                                          ByRef unusedNamespaces As List(Of XAttribute),
                                          xDoc As XDocument)
            Dim unusedNamespace = xDoc.Root.Attributes().
                FirstOrDefault(Function(a) a.IsNamespaceDeclaration AndAlso a.Name.LocalName.Equals(namespaceToUpdate.Key, StringComparison.InvariantCultureIgnoreCase))
            If unusedNamespace IsNot Nothing Then
                unusedNamespaces.Add(unusedNamespace)
            End If
        End Sub

        Private Sub RemoveUnusedNamespaces(unusedNamespaces As List(Of XAttribute), ByRef doc As XmlDocument, xDoc As XDocument)
            If unusedNamespaces.Any() Then
                unusedNamespaces.ForEach(Sub(a)
                                             xDoc.Root.Attribute(a.Name).Remove()
                                         End Sub)

                doc.LoadXml(xDoc.Root.OuterXml())
            End If
        End Sub
    End Class
End Namespace