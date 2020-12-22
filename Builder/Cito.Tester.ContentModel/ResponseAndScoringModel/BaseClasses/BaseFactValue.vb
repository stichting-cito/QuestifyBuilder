
Imports System.Xml.Serialization

<Serializable> _
<XmlInclude(GetType(ResponseValue))> _
<XmlInclude(GetType(KeyValue))> _
<XmlInclude(GetType(ConceptValue))> _
<XmlRoot("baseFactValue")> _
<XmlType([Namespace]:="http://Cito.Tester.Server/xml/serialization")> _
Public MustInherit Class BaseFactValue

    Protected Sub New()
    End Sub

    <XmlAttribute("domain")> _
    Public Property Domain As String

    <XmlAttribute("occur")> _
    Public Property Occur As Integer

    Public Property Id As String

End Class
