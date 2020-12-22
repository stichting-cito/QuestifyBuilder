Imports System.Xml
Imports System.Globalization

Namespace PluginExtensibility.Html.Handlers.Logic

    Public Class TableItem

        Private ReadOnly _node As XmlNode
        Private ReadOnly _table As Table
        Private ReadOnly _xmlNamespaceManager As XmlNamespaceManager

        Protected Sub New(table As Table, node As Xml.XmlNode)
            _node = node
            _table = table
            If (_node IsNot Nothing) Then
                _xmlNamespaceManager = New XmlNamespaceManager(_node.OwnerDocument.NameTable)
                ns.AddNamespace("def", "http://www.w3.org/1999/xhtml")
            End If
        End Sub


        Protected ReadOnly Property ns As XmlNamespaceManager
            Get
                Return _xmlNamespaceManager
            End Get
        End Property

        Public ReadOnly Property Node As XmlNode
            Get
                Return _node
            End Get
        End Property

        Friend ReadOnly Property Table As Table
            Get
                Return _table
            End Get
        End Property


        Protected Function GetValueFromNode(Of T)(attributteName As String, defaultValue As T) As T
            Return GetValueFromNode(Of T)(attributteName, defaultValue, CultureInfo.InvariantCulture)
        End Function

        Protected Function GetValueFromNode(Of T)(attributteName As String, defaultValue As T, culture As CultureInfo) As T

            Dim att = Node?.Attributes(attributteName)
            If (att Is Nothing OrElse String.IsNullOrEmpty(att.Value)) Then Return defaultValue

            If GetType(T) Is GetType(String) Then
                Return DirectCast(DirectCast(att.Value, Object), T)
            End If

            Dim converter As System.ComponentModel.TypeConverter = System.ComponentModel.TypeDescriptor.GetConverter(GetType(T))
            Dim valid As Boolean = converter.IsValid(att.Value)
            If valid Then
                Try
                    Return DirectCast(converter.ConvertFromString(Nothing, culture, att.Value), T)
                Catch
                    valid = False
                End Try
            End If
            If Not valid Then
                If GetType(T) Is GetType(Boolean) Then
                End If
            End If

        End Function

        Protected Sub SetValueToNode(attributeName As String, value As String)
            Dim e = DirectCast(Node, XmlElement)
            e.SetAttribute(attributeName, value)
        End Sub

        Protected Sub RemoveAttribute(attributeName As String)
            Dim e = DirectCast(Node, XmlElement)
            e.RemoveAttribute(attributeName)
        End Sub

        Protected Function MakeNewXmlNode(tag As String) As XmlNode
            Return Node.OwnerDocument.CreateElement(tag, Node.NamespaceURI)
        End Function

        Protected Function MakeNewXmlTextNode(txt As String) As XmlText
            Return Node.OwnerDocument.CreateTextNode(txt)
        End Function

        Friend Overridable Sub DeleteFromXml()
            Node.ParentNode.RemoveChild(Node)
        End Sub

        Protected Function GetStyle() As CssStyleList
            Return New CssStyleList(GetValueFromNode(Of String)("style", String.Empty))
        End Function

        Protected Sub SetStyle(styleLst As CssStyleList)
            If (styleLst IsNot Nothing AndAlso styleLst.hasStyles) Then
                SetValueToNode("style", styleLst.ToString())
            Else
                RemoveAttribute("style")
            End If
        End Sub

    End Class
End Namespace