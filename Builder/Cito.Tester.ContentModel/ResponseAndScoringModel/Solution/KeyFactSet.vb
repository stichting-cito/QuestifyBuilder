Imports System.Diagnostics.CodeAnalysis
Imports System.Xml.Serialization

<Serializable> _
<XmlRoot("keyFactSet")> _
<XmlType([Namespace]:="http://Cito.Tester.Server/xml/serialization")> _
Public Class KeyFactSet

    Private _facts As New List(Of BaseFact)



    <SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")> _
    <XmlElement("responseFact", GetType(ResponseFact))> _
    <XmlElement("keyFact", GetType(KeyFact))> _
    <XmlElement("conceptFact", GetType(ConceptFact))> _
    Public ReadOnly Property Facts As List(Of BaseFact)
        Get
            Return _facts
        End Get
    End Property
End Class
