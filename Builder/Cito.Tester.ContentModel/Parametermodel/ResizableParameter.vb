Imports System.Xml
Imports System.Xml.Serialization
Imports Cito.Tester.Common

Public Class ResizableParameter
    Inherits PlainTextParameter

    Private _width As Integer?
    Private _height As Integer?
    Private _editSize As Boolean = False

    Public Overridable ReadOnly Property HeightSpecified As Boolean
        Get
            Return _height.HasValue
        End Get
    End Property

    <XmlAttribute("height")>
    Public Property Height As Integer
        Get
            If _height.HasValue Then Return _height.Value
        End Get
        Set(heightValue As Integer)
            If _height Is Nothing OrElse heightValue <> _height Then
                _height = heightValue
                NotifyPropertyChanged("Height")
            End If
        End Set
    End Property

    Public Overridable ReadOnly Property WidthSpecified As Boolean
        Get
            Return _width.HasValue
        End Get
    End Property

    <XmlAttribute("width")>
    Public Property Width As Integer
        Get
            If _width.HasValue Then Return _width.Value
        End Get
        Set(widthValue As Integer)
            If _width Is Nothing OrElse widthValue <> _width Then
                _width = widthValue
                NotifyPropertyChanged("Width")
            End If
        End Set
    End Property
    Public Overridable ReadOnly Property EditSizeSpecified As Boolean
        Get
            Return _editSize
        End Get
    End Property

    <XmlAttribute("editSize")>
    Public Property EditSize As Boolean
        Get
            Return _editSize
        End Get
        Set
            _editSize = value
            NotifyPropertyChanged("_EditSize")
        End Set
    End Property

    <XmlIgnore>
    Public Overrides Property Value As String
        Get
            Return Me.InnerText
        End Get
        Set(newValue As String)
            Dim doc As New XHtmlDocument

            Dim rootElement As XmlElement = doc.CreateElement("root")
            rootElement.InnerXml = newValue
            doc.AppendChild(rootElement)

            Dim nodes As New ArrayList
            For Each node As XmlNode In doc.SelectSingleNode("/root").ChildNodes
                nodes.Add(node.CloneNode(True))
            Next
            Me.Nodes = DirectCast(nodes.ToArray(GetType(XmlNode)), XmlNode())

            NotifyPropertyChanged("value")
        End Set
    End Property

    Public Overrides Function SetValue(value As String) As Boolean
        Me.Value = value
        Return True
    End Function

    Public Overrides Function ToString() As String
        Return Me.Value
    End Function

End Class
