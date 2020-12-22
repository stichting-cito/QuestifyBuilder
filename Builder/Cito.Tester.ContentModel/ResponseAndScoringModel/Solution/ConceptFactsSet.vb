
Imports System.Linq
Imports System.Xml.Serialization

<Serializable> _
<XmlRoot("conceptFactsSet")> _
<XmlType([Namespace]:="http://Cito.Tester.Server/xml/serialization")>
Public Class ConceptFactsSet
    Inherits KeyFactSet

    Private _concepts As New ConceptCollection


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
