Imports System.Linq
Imports System.Runtime.CompilerServices
Imports System.Text.RegularExpressions
Imports System.Xml
Imports System.Xml.Linq

Namespace Publication

    Public Module ExtensionMethod

        Const COPIEDSUFFIX As String = "copied_{0}"

        <Extension()>
        Public Function ToXmlDocument(ByVal element As XContainer) As XmlDocument
            Using xmlReader As XmlReader = element.CreateReader()
                Dim xmlDoc As New XmlDocument()
                xmlDoc.PreserveWhitespace = True
                xmlDoc.Load(xmlReader)
                Return xmlDoc
            End Using
        End Function

        <Extension()>
        Public Function BringElementOutSide(ByVal xmlDocument As XmlDocument, elementToPlaceOutside As String, elementReference As String, changeReferenceElement As String, addBreak As Boolean, addToDiv As Boolean, copyStyles As Boolean) As Boolean
            Return xmlDocument.BringElementOutSide(elementToPlaceOutside, elementReference, changeReferenceElement, addBreak, addToDiv, copyStyles, Guid.NewGuid.ToString)
        End Function

        <Extension()>
        Public Function BringElementOutSide(ByVal xmlDocument As XmlDocument, elementToPlaceOutside As String, elementReference As String, changeReferenceElement As String, addBreak As Boolean, addToDiv As Boolean, copyStyles As Boolean, fixedId As String) As Boolean
            Dim copiedId = String.Format(COPIEDSUFFIX, fixedId)
            Dim fixed = 0
            Dim xmlNamespaceManager As New XmlNamespaceManager(xmlDocument.NameTable)
            Dim nsUri = xmlDocument.DocumentElement.NamespaceURI
            xmlNamespaceManager.AddNamespace("ns", nsUri)
            Dim deepestNodes As XmlNodeList = GetDeepestNodes(xmlDocument, elementReference, elementToPlaceOutside, xmlNamespaceManager, copiedId)

            If deepestNodes.Count > 0 Then
                Return FixNodes(xmlDocument, deepestNodes(0), elementToPlaceOutside, elementReference, changeReferenceElement, addBreak, addToDiv, copyStyles, xmlNamespaceManager, nsUri, copiedId, fixed)
            Else
                Return False
            End If
        End Function

        <Extension()>
        Function IsChildOf(ByVal xmlNode As XmlNode, parentNode As String) As Boolean
            Return xmlNode.IsChildOf(New String() {parentNode}.ToList)
        End Function

        <Extension()>
        Function IsChildOf(ByVal xmlNode As XmlNode, parentNodes As List(Of String)) As Boolean
            While xmlNode.ParentNode IsNot Nothing
                If parentNodes.Any(Function(p) p.Equals(xmlNode.ParentNode.Name, StringComparison.OrdinalIgnoreCase)) Then
                    Return True
                End If
                xmlNode = xmlNode.ParentNode
            End While
            Return False
        End Function

        <Extension()>
        Function BringElementOutSide(ByVal xmlDocument As XmlDocument, elementToPlaceOutside As String, elementReference As String, addBreak As Boolean, addToDiv As Boolean, copyStyles As Boolean) As Boolean
            Return xmlDocument.BringElementOutSide(elementToPlaceOutside, elementReference, String.Empty, addBreak, addToDiv, copyStyles)
        End Function

        <Extension()>
        Function BringElementOutSide(ByVal xmlDocument As XmlDocument, elementToPlaceOutside As String, elementReference As String, addBreak As Boolean, addToDiv As Boolean, copyStyles As Boolean, fixedId As String) As Boolean
            Return xmlDocument.BringElementOutSide(elementToPlaceOutside, elementReference, String.Empty, addBreak, addToDiv, copyStyles, fixedId)
        End Function


        <Extension()>
        Function BringElementOutSide(ByVal xmlDocument As XmlDocument, elementToPlaceOutside As String, elementReference As String) As Boolean
            Return xmlDocument.BringElementOutSide(elementToPlaceOutside, elementReference, String.Empty, False, False, False)
        End Function

        <Extension()>
        Function BringElementOutSide(ByVal xmlDocument As XmlDocument, elementToPlaceOutside As String, elementReference As String, addToDiv As Boolean, copiedId As String) As Boolean
            Return xmlDocument.BringElementOutSide(elementToPlaceOutside, elementReference, String.Empty, False, addToDiv, False, copiedId)
        End Function

        <Extension()>
        Function BringElementOutSide(ByVal xmlDocument As XmlDocument, elementToPlaceOutside As String, elementReference As String, addToDiv As Boolean) As Boolean
            Return xmlDocument.BringElementOutSide(elementToPlaceOutside, elementReference, String.Empty, False, addToDiv, False)
        End Function

        <Extension()>
        Function BringElementOutSide(ByVal xmlDocument As XmlDocument, elementToPlaceOutside As String, elementReference As String, fixedId As String) As Boolean
            Return xmlDocument.BringElementOutSide(elementToPlaceOutside, elementReference, String.Empty, False, False, False, fixedId)
        End Function


        Private Function FixNodes(ByVal xmlDoc As XmlDocument, ByVal nodeToFix As XmlNode, ByVal elementToPlaceOutside As String, ByVal elementReference As String, ByVal changeReferenceElement As String, ByVal addBreak As Boolean, ByVal addToDiv As Boolean, ByVal copyStyles As Boolean, ByVal xmlNamespaceManager As XmlNamespaceManager, ByVal nsUri As String, copiedId As String, ByRef fixedIndex As Integer) As Boolean
            Dim isFixed As Boolean = False

            If FixNode(nodeToFix, elementToPlaceOutside, elementReference, changeReferenceElement, addBreak, addToDiv, copyStyles, xmlNamespaceManager, nsUri, copiedId, fixedIndex) Then isFixed = True

            Dim deepestnodes As XmlNodeList = GetDeepestNodes(xmlDoc, elementReference, elementToPlaceOutside, xmlNamespaceManager, copiedId)
            If deepestnodes.Count > 0 Then
                If FixNodes(xmlDoc, deepestnodes(0), elementToPlaceOutside, elementReference, changeReferenceElement, addBreak, addToDiv, copyStyles, xmlNamespaceManager, nsUri, copiedId, fixedIndex) Then isFixed = True
            End If
            Return isFixed

        End Function

        Private Function FixNode(ByVal nodeToFix As XmlNode, ByVal elementToPlaceOutside As String, ByVal elementReference As String, ByVal changeReferenceElement As String, ByVal addBreak As Boolean, ByVal addToDiv As Boolean, ByVal copyStyles As Boolean, ByVal xmlNamespaceManager As XmlNamespaceManager, ByVal nsUri As String, copiedId As String, ByRef fixedIndex As Integer) As Boolean
            Dim isFixed As Boolean

            Dim nodeString As String = RemoveDefaultNamespace(nodeToFix.OuterXml)
            Dim loopNodeToPlaceOutside As Integer = nodeToFix.SelectNodes($"descendant::ns:{elementToPlaceOutside}", xmlNamespaceManager).Count
            For Each xmlnodeToPlaceOutside As XmlNode In nodeToFix.SelectNodes($"descendant::ns:{elementToPlaceOutside}", xmlNamespaceManager)
                Dim nodeToPlaceOutside = RemoveDefaultNamespace(xmlnodeToPlaceOutside.OuterXml)
                Dim nodes = nodeToFix.CloneNode(True).SelectNodes(
                    $"descendant::ns:{elementToPlaceOutside}[1]/ancestor::*", xmlNamespaceManager)
                For loopIndex = 0 To nodes.Count - 1
                    Dim nodeToClose As XmlNode = nodes(nodes.Count - (loopIndex + 1))
                    If nodeToClose.Attributes("id") Is Nothing OrElse Not nodeToClose.Attributes("id").Value.Contains(copiedId) Then
                        Dim style As String = String.Empty
                        Dim copiedNode = nodeToClose.CloneNode(False)
                        AddCopiedToIdentifierOfCopiedNodes(copiedNode, copiedId, fixedIndex)
                        If copiedNode.Attributes("style") IsNot Nothing Then style = copiedNode.Attributes("style").Value
                        Dim alteredNodeToPlaceOutside = nodeToPlaceOutside
                        alteredNodeToPlaceOutside = DoCopyStyles(xmlnodeToPlaceOutside, nodeToPlaceOutside, copyStyles, style, addToDiv, alteredNodeToPlaceOutside)

                        Dim startTag = GetStartTag(RemoveDefaultNamespace(copiedNode.OuterXml))
                        Dim endTag = GetEndTag(RemoveDefaultNamespace(copiedNode.OuterXml))
                        startTag = ReplaceElementReference(elementReference, changeReferenceElement, startTag, endTag)
                        nodeToPlaceOutside = RemoveMarginFromParagraphInDiv(elementReference, xmlnodeToPlaceOutside, nodeToPlaceOutside, style, alteredNodeToPlaceOutside, changeReferenceElement, loopNodeToPlaceOutside, nodeString, loopIndex)
                        nodeString = SkipLineBreakAfterElement(elementReference, xmlnodeToPlaceOutside, nodeToPlaceOutside, nodeString, loopIndex)

                        Dim replacementNodeString = String.Concat(endTag, alteredNodeToPlaceOutside, startTag)
                        If Not nodeString.Contains(replacementNodeString) Then nodeString = nodeString.Replace(nodeToPlaceOutside, replacementNodeString)
                    End If
                Next
                loopNodeToPlaceOutside -= 1
            Next

            If Not String.IsNullOrEmpty(changeReferenceElement) Then
                nodeString = nodeString.Trim
                Dim startIndex = nodeString.IndexOf(" ") + 1
                If nodeString.IndexOf(">") < startIndex Then startIndex = nodeString.IndexOf(">")
                Dim startString = nodeString.Substring(0, startIndex + 1)
                Dim endString = nodeString.Substring(nodeString.LastIndexOf("<"), nodeString.Length - nodeString.LastIndexOf("<"))
                Dim restString = nodeString.Substring(startString.Length, nodeString.Length - startString.Length - endString.Length)
                nodeString = String.Concat(startString.Replace(elementReference, changeReferenceElement), restString, endString.Replace(elementReference, changeReferenceElement))
                Dim isChildOfGapmatch = nodeToFix.IsChildOf("gapMatchInteraction")
                If addBreak AndAlso Not isChildOfGapmatch Then
                    nodeString = String.Format("{0}{1}{0}", "<br />", nodeString)
                End If
                If isChildOfGapmatch Then
                    nodeString = String.Format("<{0}>{1}</{0}>", "div", nodeString)
                End If
            End If
            Dim docNewNodes = GetFakeElement(nodeString, nsUri)
            For Each newNode As XmlNode In docNewNodes.ChildNodes
                newNode = nodeToFix.OwnerDocument.ImportNode(newNode, True)
                nodeToFix.ParentNode.InsertBefore(newNode, nodeToFix)
            Next
            nodeToFix.ParentNode.RemoveChild(nodeToFix)
            isFixed = True
            Return isFixed
        End Function

        Private Function GetDeepestNodes(ByVal xmlDocument As XmlDocument, ByVal elementReference As String, ByVal elementToPlaceOutside As String, ByVal xmlNamespaceManager As XmlNamespaceManager, copiedId As String) As XmlNodeList
            Dim deepestNodesQuery As String = String.Format("(//ns:{0}[not(contains(@id, '{2}'))]/ns:{1}[not(descendant::ns:{0}//ns:{1})]/ancestor::ns:{0})[1]", elementReference, elementToPlaceOutside, copiedId)
            Return xmlDocument.SelectNodes(deepestNodesQuery, xmlNamespaceManager)
        End Function


        Private Function RemoveMarginFromParagraphInDiv(elementReference As String, xmlnodeToPlaceOutside As XmlNode, nodeToPlaceOutside As String, style As String, ByRef alteredNodeToPlaceOutside As String, changeReferenceElement As String, loopNodeToPlaceOutside As Integer, ByRef nodeString As String, loopindex As Integer) As String

            If loopNodeToPlaceOutside = 1 AndAlso loopindex = 0 AndAlso ((elementReference.Equals("p") AndAlso String.IsNullOrEmpty(changeReferenceElement)) OrElse changeReferenceElement.Equals("p")) AndAlso GetStartTag(xmlnodeToPlaceOutside.ParentNode.OuterXml, True).StartsWith("<p") Then
                nodeToPlaceOutside = RemoveMarginFromParagraphInDiv(nodeToPlaceOutside, style, alteredNodeToPlaceOutside, nodeString)
            End If
            Return nodeToPlaceOutside
        End Function

        Private Function SkipLineBreakAfterElement(elementReference As String, xmlnodeToPlaceOutside As XmlNode, nodeToPlaceOutside As String, nodeString As String, loopindex As Integer) As String

            If loopindex = 0 AndAlso elementReference.Equals("p") AndAlso GetStartTag(xmlnodeToPlaceOutside.ParentNode.OuterXml, True).StartsWith("<p") Then
                nodeString = SkipLineBreakAfterElement(xmlnodeToPlaceOutside, nodeToPlaceOutside, nodeString)
            End If
            Return nodeString
        End Function

        Private Function ReplaceElementReference(elementReference As String, changeReferenceElement As String, startTag As String, ByRef endTag As String) As String

            If Not String.IsNullOrEmpty(changeReferenceElement) Then
                Dim regxStart = New Regex(Regex.Escape("<" & elementReference))
                startTag = regxStart.Replace(startTag, "<" & changeReferenceElement, 1)
                Dim regxEnd = New Regex(Regex.Escape("</" & elementReference))
                endTag = regxEnd.Replace(endTag, "</" & changeReferenceElement, 1)
            End If
            Return startTag
        End Function

        Private Function DoCopyStyles(xmlnodeToPlaceOutside As XmlNode, nodeToPlaceOutside As String, copyStyles As Boolean, style As String, addToDiv As Boolean, alteredNodeToPlaceOutside As String) As String

            If copyStyles AndAlso Not String.IsNullOrEmpty(style) Then
                alteredNodeToPlaceOutside = DoCopyStyles(xmlnodeToPlaceOutside, nodeToPlaceOutside, style, addToDiv, alteredNodeToPlaceOutside)
            End If
            Return alteredNodeToPlaceOutside
        End Function

        Private Sub AddCopiedToIdentifierOfCopiedNodes(copiedNode As XmlNode, copiedId As String, ByRef fixedIndex As Integer)
            If copiedNode.Attributes("id") IsNot Nothing Then
                copiedNode.Attributes("id").Value =
                    $"{copiedNode.Attributes("id").Value}_{copiedId}_{fixedIndex.ToString}"
                fixedIndex += 1
            End If
        End Sub


        Private Function RemoveDefaultNamespace(xml As String) As String
            Dim ret = Regex.Replace(xml, " xmlns=("".*?"")", String.Empty)
            Return Regex.Replace(ret, "xmlns=("".*?"")", String.Empty)
        End Function


        Private Function GetStartTag(xml As String) As String
            Return GetFirstMatch(xml, "<\s*\w.*?>")
        End Function

        Private Function GetStartTag(xml As String, alwaysGetFirst As Boolean) As String
            Return GetFirstMatch(xml, "<\s*\w.*?>", alwaysGetFirst)
        End Function


        Private Function GetEndTag(xml As String) As String
            Return GetFirstMatch(xml, "<\s*\/\s*\w\s*.*?>|<\s*br\s*>")
        End Function


        Private Function GetFirstMatch(xml As String, pattern As String) As String
            Return GetFirstMatch(xml, pattern, False)
        End Function

        Private Function GetFirstMatch(xml As String, pattern As String, alwaysGetFirst As Boolean) As String
            Dim returnValue = String.Empty
            Dim match = Regex.Matches(xml, pattern)
            If alwaysGetFirst Then
                If match.Count >= 1 Then
                    returnValue = match(0).Value
                End If
            Else
                If match.Count = 1 Then
                    returnValue = match(0).Value
                End If
            End If
            Return returnValue
        End Function

        Private Function GetFakeElement(xml As String, defaultNamespace As String) As XmlElement
            Dim doc As New XmlDocument()
            doc.LoadXml($"<fake xmlns=""{defaultNamespace}"">{xml}</fake>")
            Return doc.DocumentElement
        End Function

        Private Function DoCopyStyles(xmlnodeToPlaceOutside As XmlNode, nodeToPlaceOutside As String, style As String, addToDiv As Boolean, alteredNodeToPlaceOutside As String) As String
            Dim firstStartTag As String
            Dim firstStartTagStyle As String

            If addToDiv Then
                alteredNodeToPlaceOutside =
                    $"<{String.Format($"div style=""{style}""", style)}>{alteredNodeToPlaceOutside}</{"div"}>"
            Else
                Dim nodeToPlaceOutsideStyle = String.Empty
                If xmlnodeToPlaceOutside.Attributes("style") IsNot Nothing Then nodeToPlaceOutsideStyle = xmlnodeToPlaceOutside.Attributes("style").Value
                firstStartTag = GetStartTag(nodeToPlaceOutside, True)
                If Not String.IsNullOrEmpty(firstStartTag) Then
                    firstStartTagStyle = String.Empty
                    If Not String.IsNullOrEmpty(nodeToPlaceOutsideStyle) Then
                        firstStartTagStyle = Regex.Replace(firstStartTag, " style=("".*?"")",
                                                           $" style=""{nodeToPlaceOutsideStyle}{style}""")
                    Else
                        firstStartTagStyle = Regex.Replace(firstStartTag, ">", $" style=""{style}"" >")
                    End If
                    alteredNodeToPlaceOutside = nodeToPlaceOutside.Replace(firstStartTag, firstStartTagStyle)
                End If
            End If
            Return alteredNodeToPlaceOutside
        End Function

        Private Function SkipLineBreakAfterElement(xmlnodeToPlaceOutside As XmlNode, nodeToPlaceOutside As String, nodeString As String) As String

            If xmlnodeToPlaceOutside.NextSibling IsNot Nothing AndAlso xmlnodeToPlaceOutside.NextSibling.Name.Equals("br") Then
                nodeString = nodeString.Replace(String.Concat(nodeToPlaceOutside, RemoveDefaultNamespace(xmlnodeToPlaceOutside.NextSibling.OuterXml)), nodeToPlaceOutside)
            End If
            If xmlnodeToPlaceOutside.PreviousSibling IsNot Nothing AndAlso xmlnodeToPlaceOutside.PreviousSibling.Name.Equals("br") Then
                nodeString = nodeString.Replace(String.Concat(RemoveDefaultNamespace(xmlnodeToPlaceOutside.PreviousSibling.OuterXml), nodeToPlaceOutside), nodeToPlaceOutside)
            End If
            Return nodeString
        End Function

        Private Function RemoveMarginFromParagraphInDiv(nodeToPlaceOutside As String, style As String, ByRef alteredNodeToPlaceOutside As String, ByRef nodeString As String) As String
            Dim firstStartTag As String
            Dim firstStartTagStyle As String

            firstStartTag = GetStartTag(nodeString, True)
            If Not String.IsNullOrEmpty(firstStartTag) Then
                firstStartTagStyle = String.Empty
                If Not String.IsNullOrEmpty(style) Then
                    firstStartTagStyle = Regex.Replace(firstStartTag, " style=("".*?"")",
                                                       $" style=""{style}{"margin-bottom: 0px;"}""")
                Else
                    firstStartTagStyle = Regex.Replace(firstStartTag, ">", $" style=""{"margin-bottom: 0px;"}"" >")
                End If
                nodeString = nodeString.Replace(firstStartTag, firstStartTagStyle)
                nodeToPlaceOutside = nodeToPlaceOutside.Replace(firstStartTag, firstStartTagStyle)
                alteredNodeToPlaceOutside = alteredNodeToPlaceOutside.Replace(firstStartTag, firstStartTagStyle)
            End If
            Return nodeToPlaceOutside
        End Function


    End Module
End NameSpace