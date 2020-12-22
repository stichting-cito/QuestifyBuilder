Imports System.Net
Imports System.Xml
Imports System.Text.RegularExpressions

Namespace PluginExtensibility.Html.Handlers.Logic

    Public Class RegExHtmlSplitter : Inherits BaseHtmlSplitter

        Private Shared _detector As Regex
        Private nodesToScan As New List(Of XmlNode)

        Public Sub New(regex As Regex, selectedNode As XmlNode, startOffset As Integer, endNode As XmlNode, endOffset As Integer)
            MyBase.New(selectedNode, startOffset, endNode, endOffset)
            _detector = regex
            Debug.Assert(TypeOf selectedNode Is System.Xml.XmlText)
            Debug.Assert(TypeOf endNode Is System.Xml.XmlText)
        End Sub

        Public Overrides Function Split() As IEnumerable(Of System.Xml.XmlNode)
            Dim ret As New List(Of System.Xml.XmlNode)

            nodesToScan = FlattenNodes()

            Dim result = evalSelection(nodesToScan)
            ret.AddRange(result)
            Return ret
        End Function

        Friend Function FlattenNodes() As List(Of XmlNode)
            Dim ret As New List(Of XmlNode)

            ret.Add(SelectedNode)
            Dim done = AddSiblings(ret, SelectedNode, EndNode)

            If (Not done) Then
                done = AddParent(ret, SelectedNode.ParentNode, EndNode)
            End If

            Debug.Assert(done)


            Return ret
        End Function

        Private Function AddSiblings(ret As List(Of XmlNode), xmlNode As XmlNode, endNode As XmlNode) As Boolean
            Dim found = (Object.ReferenceEquals(xmlNode, endNode))

            If (found) Then Return found

            Dim nxt As XmlNode = xmlNode.NextSibling()

            While (nxt IsNot Nothing)
                ret.Add(nxt)

                If (Object.ReferenceEquals(nxt, endNode)) Then
                    Return True
                End If

                found = AddChildren(ret, nxt, endNode)
                If (found) Then Return found
                nxt = nxt.NextSibling()
            End While
            Return found
        End Function

        Private Function AddChildren(ret As List(Of XmlNode), nxt As XmlNode, endNode As XmlNode) As Boolean
            Dim found = (Object.ReferenceEquals(nxt, endNode))

            If (found) Then Return found

            If (nxt.HasChildNodes) Then

                For Each c As XmlNode In nxt.ChildNodes

                    ret.Add(c)

                    found = AddChildren(ret, c, endNode)
                    If (found) Then Return found

                    found = AddSiblings(ret, c, endNode)
                    If (found) Then Return found
                Next

            End If

            Return found
        End Function

        Private Function AddParent(ret As List(Of XmlNode), xmlNode As XmlNode, endNode As XmlNode) As Boolean
            Dim found = False
            ret.Add(xmlNode)

            found = AddSiblings(ret, xmlNode, endNode)

            If (found) Then Return found

            found = AddParent(ret, xmlNode.ParentNode, endNode)

            Return found
        End Function

        Friend Function evalSelection(nodesToScan As List(Of XmlNode)) As List(Of XmlNode)
            Dim ret As New List(Of XmlNode)


            Dim strForMatching As String = String.Empty

            For Each node As XmlNode In nodesToScan
                Dim tmpLst = New List(Of XmlNode)()

                Select Case node.LocalName
                    Case "#text"
                        strForMatching = GetTextFrom(node)
                    Case Else
                        strForMatching = String.Empty
                End Select

                If (String.IsNullOrEmpty(strForMatching)) Then

                Else

                    If _detector.IsMatch(strForMatching) Then
                        Dim matches = _detector.Matches(strForMatching)
                        Dim lastPosition As Integer = 0

                        tmpLst.Add(DeliverText(GetTXTbeforeNode(node, StartOffset)))

                        For Each match As Match In matches

                            Dim b4 = strForMatching.Substring(lastPosition, match.Index - lastPosition)
                            Dim notMatchedStr = DeliverText(b4)
                            Dim matchedPart = DeliverSpan(match.Value)
                            lastPosition = match.Index + match.Length

                            tmpLst.Add(notMatchedStr)
                            tmpLst.Add(matchedPart)
                            ret.Add(matchedPart)
                        Next

                        tmpLst.Add(DeliverText(GetTXTAfterNode(node, lastPosition)))

                        ReplaceChilds(node, tmpLst)

                    End If
                End If
            Next


            Return ret
        End Function

        Private Function GetTextFrom(node As XmlNode) As String

            Dim renderedText = WebUtility.HtmlDecode(node.OuterXml)

            If (Object.ReferenceEquals(node, SelectedNode)) Then

                If (Object.ReferenceEquals(node, EndNode)) Then
                    Dim tmp = renderedText.Remove(0, StartOffset)
                    Return tmp.Substring(0, Math.Min(tmp.Length, EndOffset - StartOffset))
                Else
                    Return renderedText.Remove(0, StartOffset)
                End If

            Else


                If (Object.ReferenceEquals(node, EndNode)) Then
                    Return renderedText.Substring(0, Math.Min(renderedText.Length, EndOffset))
                Else
                    Return renderedText
                End If
            End If
        End Function

        Private Function GetTXTbeforeNode(node As XmlNode, startOffset As Integer) As String

            If (Object.ReferenceEquals(node, SelectedNode)) Then
                If (startOffset > 0) Then
                    Dim s = node.OuterXml
                    Return s.Substring(0, Math.Min(s.Length, startOffset))
                End If

            End If

            Return String.Empty
        End Function

        Private Function GetTXTAfterNode(node As XmlNode, endpos As Integer) As String
            Dim extraOffset = If(Object.ReferenceEquals(node, SelectedNode), StartOffset, 0)
            Dim s = WebUtility.HtmlDecode(node.OuterXml)
            Return s.Substring((endpos + extraOffset), s.Length - (endpos + extraOffset))
        End Function

        Private Function DeliverSpan(data As String) As XmlNode
            Dim ret = SelectedNode.OwnerDocument.CreateElement("span")
            ret.InnerText = data
            Return ret
        End Function

        Private Function DeliverText(txt As String) As XmlNode
            Dim ret = SelectedNode.OwnerDocument.CreateTextNode(txt)
            Return ret
        End Function

        Private Sub ReplaceChilds(node As XmlNode, childNodes As List(Of XmlNode))
            Dim parent = node.ParentNode
            Dim i As Integer = 0
            If parent IsNot Nothing Then
                For i = 0 To childNodes.Count - 1
                    If (i = 0) Then
                        parent.ReplaceChild(childNodes(i), node)
                    Else
                        parent.InsertAfter(childNodes(i), childNodes(i - 1))
                    End If
                Next
            End If
        End Sub


        Friend Function GetStringMap() As List(Of Tuple(Of String, Integer, Integer, XmlNode))
            Dim ret As New List(Of Tuple(Of String, Integer, Integer, XmlNode))
            Dim nodes = FlattenNodes()
            Dim pos = 0

            For Each n As XmlNode In nodes
                If (TypeOf n Is XmlText) Then
                    Dim s = n.OuterXml
                    Dim l = s.Length
                    ret.Add(New Tuple(Of String, Integer, Integer, XmlNode)(s, pos, l, n))
                    pos += l
                End If
            Next

            Return ret
        End Function
    End Class
End Namespace
