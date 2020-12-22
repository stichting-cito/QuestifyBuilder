
Imports System.Xml.Serialization

Namespace Entities

    <Serializable>
    Public MustInherit Class StaticGroupEntry

        Private _resourceIdentifier As String
        Private _title As String



        Public Sub New(resourceIdentifier As String)
            _resourceIdentifier = resourceIdentifier
        End Sub

        Public Sub New()
        End Sub



        <XmlAttribute("resourceIdentifier")>
        Public Property ResourceIdentifier As String
            Get
                Return _resourceIdentifier
            End Get
            Set
                _resourceIdentifier = Value
            End Set
        End Property

        <XmlIgnore>
        Public Property Title As String
            Get
                Return _title
            End Get
            Set
                _title = Value
            End Set
        End Property


    End Class

End Namespace

