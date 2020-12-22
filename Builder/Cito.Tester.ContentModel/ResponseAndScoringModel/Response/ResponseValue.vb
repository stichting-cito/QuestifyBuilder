
Imports System.Text
Imports System.Xml.Serialization

<Serializable> _
<XmlRoot("responseValue")>
<DebuggerDisplay("{Domain} = {Value}")>
Public Class ResponseValue
    Inherits BaseFactValue


    Private _value As BaseValue



    <XmlElement("stringValue", GetType(StringValue)), _
 XmlElement("booleanValue", GetType(BooleanValue)), _
 XmlElement("integerValue", GetType(IntegerValue)), _
 XmlElement("decimalValue", GetType(DecimalValue)), _
 XmlElement("binaryValue", GetType(BinaryValue)), _
 XmlElement("integerRangeValue", GetType(IntegerRangeValue)), _
 XmlElement("decimalRangeValue", GetType(DecimalRangeValue)), _
 XmlElement("decimalComparisonValue", GetType(DecimalComparisonValue)), _
 XmlElement("integerComparisonValue", GetType(IntegerComparisonValue)), _
 XmlElement("stringComparisonValue", GetType(StringComparisonValue)), _
 XmlElement("noValue", GetType(NoValue))> _
    Public Property Value As BaseValue
        Get
            Return _value
        End Get
        Set
            _value = value
        End Set
    End Property



    Public Sub New(domain As String, value As BaseValue)
        Me.Domain = domain
        Me.Value = value
        Me.Occur = 1
    End Sub

    Public Sub New()
        Me.Occur = 1
    End Sub


    Public Overrides Function ToString() As String
        Dim sb = New StringBuilder()
        Dim val = Value.ToString()
        sb.AppendFormat("[{0}] =({1})", Domain, val)
        Return sb.ToString()
    End Function

End Class
