Imports System.Linq
Imports System.Xml
Imports System.Xml.Linq
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base.QTICompliantHelper.Interfaces

Namespace QTI.Helpers.QTI_Base.QTICompliantHelper

    Public Class FixAspects
        Implements IModifyItemDocument

        Public Sub Modify(ByRef xmlDoc As Xml.XmlDocument, docHelper As DocumentHelper) Implements IModifyDocument.Modify
            RemoveCustomInteractionsWrapperInRubikBlock(xmlDoc)
            RemoveMediaInteractionsWrapperInRubikBlock(xmlDoc)
            ReplaceInvalidNestingUnderlineInStrong(xmlDoc)
        End Sub


        Private Sub ReplaceInvalidNestingUnderlineInStrong(xmlDoc As XmlDocument)
            If xmlDoc Is Nothing Then
                Return
            End If
            Dim xDoc As XDocument = XDocument.Parse(xmlDoc.OuterXml, LoadOptions.PreserveWhitespace)
            Dim strongNodes = xDoc.Root.Descendants().Where(Function(e) e.Name.LocalName = "strong")
            Dim strongNodesWithUnderline = strongNodes.Where(Function(n) n.Elements().Any(Function(e) e.Name.LocalName = "u"))

            For Each node as XElement In strongNodesWithUnderline
                Dim oldNodes = node.Elements().Where(Function(x) x.Name.LocalName = "u").ToList()
                For Each oldNode In oldNodes
                    Dim ns As XNamespace = "http://www.w3.org/2001/XMLSchema-instance"
                    Dim newNode = new XElement(ns + "span", oldNode.Value)
                    Dim attribute = new XAttribute("style", "TEXT-DECORATION: underline")
                    newNode.Add(attribute)
                    oldNode.ReplaceWith(newNode)
                Next
            Next

            xmlDoc.LoadXml(xDoc.ToString(SaveOptions.DisableFormatting))
        End Sub


        Private Sub RemoveCustomInteractionsWrapperInRubikBlock(xmlDoc As XmlDocument)
            If xmlDoc IsNot Nothing Then
                GetAllAspects(xmlDoc).Cast(Of XmlNode)().ToList.ForEach(
                    Sub(aspect) GetAllCustomInteractions(aspect).Cast(Of XmlNode).ToList.ForEach(
                        Sub(nodeToRemove) RemoveSurroundingElement(nodeToRemove)))
            End If
        End Sub


        Private Sub RemoveMediaInteractionsWrapperInRubikBlock(xmlDoc As XmlDocument)
            If xmlDoc IsNot Nothing Then
                GetAllAspects(xmlDoc).Cast(Of XmlNode)().ToList.ForEach(
                    Sub(aspect) GetAllMediaInteractions(aspect).Cast(Of XmlNode).ToList.ForEach(
                        Sub(nodeToRemove) RemoveSurroundingElement(nodeToRemove)))
            End If
        End Sub


        Private Sub RemoveSurroundingElement(nodeToRemove As XmlNode)
            If nodeToRemove.ParentNode IsNot Nothing Then
                If nodeToRemove.ChildNodes IsNot Nothing Then
                    For Each nodeToAdd As XmlNode In nodeToRemove.ChildNodes
                        nodeToRemove.ParentNode.InsertBefore(nodeToAdd.CloneNode(True), nodeToRemove)
                    Next
                End If
                nodeToRemove.ParentNode.RemoveChild(nodeToRemove)
            End If
        End Sub

        Private Function GetAllAspects(xmlDoc As XmlDocument) As XmlNodeList
            Dim nodeList As XmlNodeList = Nothing
            If xmlDoc IsNot Nothing AndAlso xmlDoc.DocumentElement IsNot Nothing Then
                nodeList = xmlDoc.DocumentElement.SelectNodes("//rubricBlock[@view='scorer']")
            End If
            Return nodeList
        End Function

        Private Function GetAllCustomInteractions(xmlNode As XmlNode) As XmlNodeList
            Dim nodeList As XmlNodeList = Nothing
            If xmlNode IsNot Nothing Then
                nodeList = xmlNode.SelectNodes("/customInteraction")
            End If
            Return nodeList
        End Function



        Private Function GetAllMediaInteractions(xmlNode As XmlNode) As XmlNodeList
            Dim nodeList As XmlNodeList = Nothing
            If xmlNode IsNot Nothing Then
                nodeList = xmlNode.SelectNodes("/mediaInteraction")
            End If
            Return nodeList
        End Function
    End Class
End NameSpace