
Imports System.Xml.Serialization

<Serializable> _
<XmlInclude(GetType(DecimalValue))> _
<XmlInclude(GetType(IntegerValue))> _
<XmlInclude(GetType(StringValue))> _
<XmlInclude(GetType(BooleanValue))> _
<XmlInclude(GetType(BinaryValue))> _
<XmlInclude(GetType(DecimalRangeValue))> _
<XmlInclude(GetType(IntegerRangeValue))> _
<XmlInclude(GetType(DecimalComparisonValue))> _
<XmlInclude(GetType(IntegerComparisonValue))> _
<XmlInclude(GetType(StringComparisonValue))> _
<XmlInclude(GetType(CatchAllValue))> _
<XmlInclude(GetType(NoValue))> _
<XmlRoot("baseValue")> _
<XmlType([Namespace]:="http://Cito.Tester.Server/xml/serialization")> _
Public MustInherit Class BaseValue

    Public MustOverride ReadOnly Property IsRange As Boolean

    Public MustOverride Function IsMatch(value As BaseValue) As Boolean

    Protected Sub New()
    End Sub

End Class
