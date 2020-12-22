Imports System.Xml.Serialization

<Serializable, XmlRoot("designersetting")>
<DebuggerDisplay("key:[{Key}] = '{Value}' :: ref={Ref}")>
Public Class DesignerSetting

    <XmlAttribute("key")> _
    Public Property Key As String

    <XmlText> _
    Public Property Value As String

    <XmlArray("listvalues"), XmlArrayItem(GetType(ListValue), ElementName:="listvalue")> _
    Public Property ListValue As List(Of ListValue)

    <XmlAttribute("ref")>
    Public Property Ref As String

End Class
