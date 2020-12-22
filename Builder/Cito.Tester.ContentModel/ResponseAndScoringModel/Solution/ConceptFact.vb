
Imports System.Linq
Imports System.Xml.Serialization

<Serializable> _
<XmlRoot("conceptFact")> _
Public Class ConceptFact
    Inherits KeyFact

    Private _concepts As ConceptCollection

    Public Sub New()
        _concepts = New ConceptCollection()
    End Sub

    Public Sub New(id As String)
        Me.Id = id
    End Sub

    <XmlArray("concepts"), XmlArrayItem("concept", GetType(Concept))> _
    Public Property Concepts As ConceptCollection
        Get
            Return _concepts
        End Get
        Set
            _concepts = value
        End Set
    End Property

    Public Function ShouldSerializeConcepts() As Boolean
        Return (_concepts IsNot Nothing) AndAlso _concepts.Any
    End Function


End Class
