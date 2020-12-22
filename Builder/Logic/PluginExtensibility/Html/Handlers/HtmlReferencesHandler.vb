
Imports System.ComponentModel
Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.Windows.Forms
Imports System.Xml
Imports Cito.Tester.Common
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.Logic.PluginExtensibility.Html.Converters

Namespace PluginExtensibility.Html.Handlers

    Public Class HtmlReferencesHandler
        Inherits HtmlHandlerBase

        Private Const ElementRefTemplate As String = "&#160;&#160;&#160;&#160;{0}&#160;&#160;&#160;&#160;"
        Private Const MaxSymbolReferenceCount As Integer = 5

        Private ReadOnly _xhtmlElementReferences As New List(Of XmlElement)
        Private ReadOnly _xhtmlSymbolReferences As New List(Of XmlElement)
        Private ReadOnly _xhtmlHighlightReferences As New List(Of XmlElement)
        Private _activeReference As XhtmlReference
        Private ReadOnly _canCreateReferences As Boolean
        Private ReadOnly _contextIdentifier As Nullable(Of Integer)

        Public Event ActiveReferenceChanged As EventHandler(Of EventArgs)

        Public Sub New(editor As IXHtmlEditor,
               resourceManager As ResourceManagerBase,
               defaultNameSpaceManager As XmlNamespaceManager,
               contextIdentifier As Nullable(Of Integer),
               canCreateReferences As Boolean)
            MyBase.New(editor, Nothing, resourceManager, Nothing)

            _contextIdentifier = contextIdentifier
            _canCreateReferences = canCreateReferences
        End Sub

        Public Function IsReference() As Boolean
            Dim currentNode As XmlNode = editor.Selection.Node

            If currentNode IsNot Nothing Then
                While Not currentNode.Name.ToLower() = "body"
                    If (currentNode.Attributes IsNot Nothing AndAlso IsReferenceNode(currentNode)) Then
                        Return True
                    Else
                        If currentNode.ParentNode IsNot Nothing Then
                            currentNode = currentNode.ParentNode
                        Else
                            Return False
                        End If
                    End If
                End While
            End If
            Return False
        End Function

        <Browsable(False)>
        Public Property ActiveReference() As XhtmlReference
            Get
                Return _activeReference
            End Get
            Set(ByVal value As XhtmlReference)
                _activeReference = value

                If _activeReference IsNot Nothing Then
                    ChangeActiveReferenceInHtml()
                    RaiseEvent ActiveReferenceChanged(Me, EventArgs.Empty)
                End If
            End Set
        End Property

        Public Sub LoadReferences()
            If _canCreateReferences Then
                Dim conf As New C1HtmlConverter
                Dim html As String = conf.ToCitoFormatForReferenceReadOut().ConvertHtml(editor.Document.DocumentElement.OuterXml)
                For Each reference As XhtmlReference In XhtmlReferenceFactory.ParseXhtmlReference(html)
                    If reference.Type = XhtmlReferenceType.Element Then
                        _xhtmlElementReferences.Add(editor.Document.GetElementById(reference.ID))
                    ElseIf reference.Type = XhtmlReferenceType.Symbol Then
                        _xhtmlSymbolReferences.Add(editor.Document.GetElementById(reference.ID))
                    ElseIf reference.Type = XhtmlReferenceType.Highlight Then
                        _xhtmlHighlightReferences.Add(editor.Document.GetElementById(reference.ID))
                    End If
                Next
            End If

            ChangeActiveReferenceInHtml()
        End Sub

        Public Sub DoReferToToolStripButton()
            If ActiveReference IsNot Nothing Then
                Dim newReference As XmlElement = editor.Document.CreateElement("span", "http://www.w3.org/1999/xhtml")

                If ActiveReference.Type = XhtmlReferenceType.Element Then
                    newReference.InnerXml = ActiveReference.Value
                ElseIf ActiveReference.Type = XhtmlReferenceType.Symbol Then
                    Dim img As XmlElement = DirectCast(newReference.AppendChild(editor.Document.CreateElement("img")), XmlElement)

                    Dim newUri As New UriBuilder(ActiveReference.Value)
                    newUri.Port = _contextIdentifier.Value
                    img.SetAttribute("src", newUri.ToString())
                End If

                Dim id As String = $"ref{Guid.NewGuid()}"
                newReference.SetAttribute("id", id)
                CreateReferenceAtt(newReference)
                CreateRefTypeAtt(newReference, XhtmlReferenceType.ReferTo)
                newReference.SetAttribute("contenteditable", "true")

                editor.Selection.Node.ParentNode.InsertBefore(newReference, editor.Selection.Node)
            End If
        End Sub

        Private Sub ChangeActiveReferenceInHtml()
            If _activeReference IsNot Nothing Then
                For Each node As XmlNode In GetReferToElements()
                    Select Case _activeReference.Type
                        Case XhtmlReferenceType.Element
                            node.InnerXml = _activeReference.Value
                        Case XhtmlReferenceType.Symbol
                            Dim img As XmlNode = node.FirstChild

                            If img Is Nothing OrElse Not img.Name.ToLower() = "img" Then
                                node.InnerXml = String.Empty
                                img = node.AppendChild(editor.Document.CreateElement("img"))
                                img.Attributes.Append(editor.Document.CreateAttribute("src"))
                            End If

                            Dim newUri As New UriBuilder(_activeReference.Value)

                            newUri.Port = _contextIdentifier.Value
                            img.Attributes("src").Value = newUri.ToString()
                    End Select
                Next
            End If
        End Sub

        Private Function GetReferToElements() As List(Of XmlNode)
            Dim nodes As New List(Of XmlNode)

            For Each node As XmlNode In editor.Document.SelectNodes(
                $"//def:span[@cito_type='reference' and @cito_reftype='{XhtmlReferenceType.ReferTo}']", namespaceManager)
                nodes.Add(editor.Document.GetElementById(node.Attributes("id").Value))
            Next

            Return nodes
        End Function

        Public Sub RemoveReference()
            Dim currentNode As XmlNode = editor.Selection.Node

            While currentNode.Name.ToLower() <> "body" AndAlso Not (currentNode.Attributes IsNot Nothing AndAlso IsReferenceNode(currentNode))
                currentNode = currentNode.ParentNode
            End While

            If currentNode IsNot Nothing AndAlso currentNode.Name.ToLower() <> "body" Then
                Dim referenceTypeToDelete As XhtmlReferenceType = DirectCast([Enum].Parse(GetType(XhtmlReferenceType), currentNode.Attributes("cito_reftype").Value), XhtmlReferenceType)
                Dim keepInnerHtml As Boolean = referenceTypeToDelete = XhtmlReferenceType.Symbol OrElse referenceTypeToDelete = XhtmlReferenceType.Highlight

                If keepInnerHtml Then
                    editor.Selection.MoveTo(currentNode)

                    If (editor.Selection.IsTagApplied("span")) Then
                        editor.Selection.RemoveTag("span")
                        _xhtmlSymbolReferences.Remove(_xhtmlSymbolReferences.FirstOrDefault(Function(n) n.Attributes("id").Value = currentNode.Attributes("id").Value))
                    End If
                Else
                    Dim spanNode As XmlNode = editor.Document.SelectSingleNode(
                        $"//def:*[@id=""{currentNode.Attributes("id").Value}""]", GetNamespaceManager())
                    spanNode.ParentNode.RemoveChild(spanNode)
                    _xhtmlElementReferences.Remove(_xhtmlElementReferences.FirstOrDefault(Function(n) n.Attributes("id").Value = spanNode.Attributes("id").Value))
                End If
            End If
        End Sub

        Public Sub InsertElementReference()
            Dim newReference As XmlElement = editor.Document.CreateElement("span", "http://www.w3.org/1999/xhtml")

            editor.Document.PreserveWhitespace = True
            Dim id As String = $"ref{Guid.NewGuid()}"
            newReference.SetAttribute("id", id)
            Dim underlineElement As XmlElement = editor.Document.CreateElement("u", "http://www.w3.org/1999/xhtml")
            underlineElement.AppendChild(newReference)

            editor.Selection.SetXmlElement(underlineElement)
            newReference = DirectCast(editor.Document.SelectSingleNode($"//def:*[@id=""{id}""]", GetNamespaceManager()), XmlElement)
            newReference.SetAttribute("contenteditable", "false")

            CreateReferenceAtt(newReference)
            CreateRefTypeAtt(newReference, XhtmlReferenceType.Element)

            Dim references As XhtmlReferenceList = GetCurrentReferences(XhtmlReferenceType.Element)
            Dim index As Integer = references.GetIndexById(id)

            _xhtmlElementReferences.Insert(index, newReference)
            CreateDescriptionAtt(newReference, $"Element {(index + 1)}")
            CreateValueAtt(newReference, (index + 1).ToString())
            newReference.InnerXml = String.Format(ElementRefTemplate, index + 1)

            For i As Integer = index + 1 To references.Count - 1
                Dim reference As XmlElement = editor.Document.GetElementById(references(i).ID)
                reference.SetAttribute("description", $"Element {(i + 1)}")
                reference.SetAttribute("value", (i + 1).ToString())
                reference.InnerXml = String.Format(ElementRefTemplate, i + 1)
            Next
        End Sub

        Public Sub InsertSymbolOrHighlightReference(ByVal referenceTypeToInsert As XhtmlReferenceType)
            If referenceTypeToInsert = XhtmlReferenceType.Symbol AndAlso _xhtmlSymbolReferences.Count >= MaxSymbolReferenceCount Then
                MessageBox.Show(My.Resources.MaximumNumberSymbolreferencesReached, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                Dim id As String = $"ref{Guid.NewGuid()}"

                Dim description As String
                Dim reg As New Regex("\w+")
                Dim matches As MatchCollection = reg.Matches(editor.Selection.Text)
                If matches.Count <= 4 Then
                    description = editor.Selection.Text.Replace("""", String.Empty)
                Else
                    description =
                        $"{matches(0).Value} {matches(1).Value} ... {matches(matches.Count - 2).Value} { _
                            matches(matches.Count - 1).Value}"
                End If

                Dim newReference As XmlElement = editor.Document.CreateElement("span", "http://www.w3.org/1999/xhtml")

                newReference.InnerText = editor.Selection.Text
                newReference.SetAttribute("id", id)

                editor.Selection.SetXmlElement(newReference)
                newReference = DirectCast(editor.Document.SelectSingleNode($"//def:*[@id=""{id}""]", GetNamespaceManager()), XmlElement)
                newReference.SetAttribute("contenteditable", "false")

                CreateReferenceAtt(newReference)
                CreateRefTypeAtt(newReference, referenceTypeToInsert)
                CreateDescriptionAtt(newReference, description)

                Dim references As XhtmlReferenceList = GetCurrentReferences(referenceTypeToInsert)
                Dim index As Integer = references.GetIndexById(id)

                If index = -1 Then index = 0


                If referenceTypeToInsert = XhtmlReferenceType.Symbol Then
                    CreateValueAtt(newReference,
                                   $"{Constants.ResourceProtocolPrefix}referencesymbol{(index + 1)}")
                Else
                    CreateValueAtt(newReference, (index + 1).ToString())
                End If

                If referenceTypeToInsert = XhtmlReferenceType.Symbol Then
                    _xhtmlSymbolReferences.Insert(index, newReference)
                Else
                    _xhtmlHighlightReferences.Insert(index, newReference)
                End If

                For i As Integer = index + 1 To references.Count - 1
                    Dim reference As XmlElement = editor.Document.GetElementById(references(i).ID)
                    If referenceTypeToInsert = XhtmlReferenceType.Symbol Then
                        reference.SetAttribute("value",
                                               $"{Constants.ResourceProtocolPrefix}referencesymbol{ _
                                                  (i + 1)}")
                    Else
                        reference.SetAttribute("value", (index + 1).ToString())
                    End If
                Next

            End If
        End Sub

        Private Function GetCurrentReferences(type As XhtmlReferenceType) As XhtmlReferenceList
            Dim conf As New C1HtmlConverter
            Dim html As String = conf.ToCitoFormatForReferenceReadOut().ConvertHtml(editor.Document.DocumentElement.OuterXml)
            Return XhtmlReferenceFactory.ParseXhtmlReference(html, type)
        End Function

        Private Sub CreateReferenceAtt(newReference As XmlElement)
            Dim attribute As XmlAttribute = editor.Document.CreateAttribute("cito_type")
            attribute.Value = "reference"
            newReference.Attributes.Append(attribute)
        End Sub

        Private Sub CreateRefTypeAtt(newReference As XmlElement, xhtmlReferenceType As XhtmlReferenceType)
            Dim attribute As XmlAttribute = editor.Document.CreateAttribute("cito_reftype")
            attribute.Value = xhtmlReferenceType.ToString()
            newReference.Attributes.Append(attribute)
        End Sub

        Private Sub CreateDescriptionAtt(newReference As XmlElement, desc As String)
            Dim attribute As XmlAttribute = editor.Document.CreateAttribute("cito_description")
            attribute.Value = desc
            newReference.Attributes.Append(attribute)
        End Sub

        Private Sub CreateValueAtt(newReference As XmlElement, val As String)
            Dim attribute As XmlAttribute = editor.Document.CreateAttribute("cito_value")
            attribute.Value = val
            newReference.Attributes.Append(attribute)
        End Sub

        Private Function IsReferenceNode(currentNode As XmlNode) As Boolean
            Return currentNode.Attributes("cito_type") IsNot Nothing AndAlso currentNode.Attributes("cito_type").Value = "reference"
        End Function

    End Class

End Namespace