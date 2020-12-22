Imports System.Xml
Imports System.ComponentModel
Imports System.Xml.Serialization
Imports Cito.Tester.Common

Public Class SimpleChoice
    Implements INotifyPropertyChanged


    Private _identifier As String
    Private _fixed As Boolean
    Private _nodes() As XmlNode



    <XmlAttribute("identifier")> _
    Public Property Identifier As String
        Get
            Return _identifier
        End Get
        Set
            If value <> Me.Identifier Then
                _identifier = value
                NotifyPropertyChanged("Identifier")
            End If
        End Set
    End Property

    <XmlAttribute("fixed")> _
    Public Property Fixed As Boolean
        Get
            Return _fixed
        End Get
        Set
            If value <> Me.Fixed Then
                _fixed = value
                NotifyPropertyChanged("Fixed")
            End If
        End Set
    End Property

    <XmlAnyElement, XmlText> _
    Public Property Nodes As XmlNode()
        Get
            Return _nodes
        End Get
        Set
            _nodes = value
        End Set
    End Property

    Protected ReadOnly Property InnerText As String
        Get
            Return ParameterHelper.CreateInnerText(Me.Nodes)
        End Get
    End Property

    <XmlIgnore> _
    Public Property Value As String
        Get
            Return Me.InnerText
        End Get
        Set
            Dim doc As New XHtmlDocument

            Dim rootElement As XmlElement = doc.CreateElement("root")
            rootElement.InnerXml = value
            doc.AppendChild(rootElement)

            Dim nodes As New ArrayList
            For Each node As XmlNode In doc.SelectSingleNode("/root").ChildNodes
                nodes.Add(node.CloneNode(True))
            Next
            Me.Nodes = DirectCast(nodes.ToArray(GetType(XmlNode)), XmlNode())

            NotifyPropertyChanged("Nodes")
        End Set
    End Property



    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged

    Protected Sub NotifyPropertyChanged(info As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
    End Sub


    Public Overrides Function ToString() As String
        Return Me.Value.ToString
    End Function

End Class