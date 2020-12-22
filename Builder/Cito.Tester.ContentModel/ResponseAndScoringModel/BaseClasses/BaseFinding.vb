Imports System.Diagnostics.CodeAnalysis
Imports System.Xml.Serialization

<Serializable> _
<XmlInclude(GetType(ResponseFinding))> _
<XmlInclude(GetType(KeyFinding))> _
<XmlInclude(GetType(ConceptFinding))> _
<XmlRoot("baseFinding")> _
<XmlType([Namespace]:="http://Cito.Tester.Server/xml/serialization")> _
Public MustInherit Class BaseFinding

    Protected Sub New(id As String)
        Me.Id = id
    End Sub

    Protected Sub New()
    End Sub


    <SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")> _
    <XmlElement("responseFact", GetType(ResponseFact))> _
    <XmlElement("keyFact", GetType(KeyFact))> _
    <XmlElement("conceptFact", GetType(ConceptFact))> _
    Public ReadOnly Property Facts As New List(Of BaseFact)

    <XmlAttribute("id")> _
    Public Property Id As String

End Class
