Imports System.Linq
Imports System.Xml
Imports System.Xml.Linq
Imports System.Xml.Serialization
Imports Questify.Builder.Logic.HelperClasses
Imports Questify.Builder.Logic.Service.HelperFunctions

Namespace QTI.Helpers.QTI_Base

    Public Class NamespaceHelper

        Public Overridable Function GetNamespaceManager() As Serialization.XmlSerializerNamespaces
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

        Public Overridable Function GetDepXmlSerializerNamespaces() As XmlSerializerNamespaces
            Return Nothing
        End Function

        Public Overridable Function GetDepXmlNamespaceManager(nameTable As XmlNameTable) As XmlNamespaceManager
            Return Nothing
        End Function

        Public Overridable Sub UpdateNameSpaces(ByRef doc As XmlDocument, Optional updateQtiNamespaces As Boolean = False)
            UpdateMathMlNamespaces(doc)
            UpdateImsMetadataNamespaces(doc)
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
    End Class
End Namespace