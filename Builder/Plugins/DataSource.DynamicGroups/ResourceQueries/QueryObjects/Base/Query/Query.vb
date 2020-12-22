Imports System.Xml.Serialization

<XmlRoot("Query")> _
<XmlInclude(GetType(ItemQuery))> _
Public MustInherit Class Query


    Private _description As String
    Private _filter As FilterPredicate



    Public Sub New(filter As FilterPredicate)
        If filter Is Nothing Then
            Throw New ArgumentNullException("filter")
        End If

        Me.Filter = filter
    End Sub

    Public Sub New()
        Me.Filter = New NOOPFilterPredicate
    End Sub



    <XmlElement("Description")> _
    Public Property Description() As String
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
        End Set
    End Property

    <XmlElement("Filter")> _
    Public Property Filter() As FilterPredicate
        Get
            Return _filter
        End Get
        Set(ByVal value As FilterPredicate)
            _filter = value
        End Set
    End Property


End Class