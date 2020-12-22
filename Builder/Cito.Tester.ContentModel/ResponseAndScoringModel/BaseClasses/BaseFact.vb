
Imports System.Linq
Imports System.Text
Imports System.Xml.Serialization

<Serializable> _
<XmlInclude(GetType(ResponseFact))> _
<XmlInclude(GetType(KeyFact))> _
<XmlInclude(GetType(ConceptFact))> _
<XmlRoot("baseFact")> _
<XmlType([Namespace]:="http://Cito.Tester.Server/xml/serialization")> _
Public MustInherit Class BaseFact

    <XmlElement("responseValue", GetType(ResponseValue)), _
 XmlElement("keyValue", GetType(KeyValue)), _
  XmlElement("conceptValue", GetType(ConceptValue))> _
    Public ReadOnly Property Values As New ValueCollection

    <XmlAttribute("id")> _
    Public Property Id As String

    Protected Sub New()
    End Sub

    Public Overrides Function ToString() As String
        Dim sb = New StringBuilder()

        Dim val = String.Join("&", Values.Select(Function(v) v.ToString()).ToArray())
        sb.AppendFormat("({0})", val)
        Return sb.ToString()
    End Function

End Class
