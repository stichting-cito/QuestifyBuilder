
Imports System.Text
Imports System.Linq
Imports System.Xml.Serialization

<Serializable>
<XmlRoot("keyValue")>
Public Class KeyValue
    Inherits BaseFactValue


    Private _values As New KeyValueCollection
    Private _preprocessingRuleCollection As New SelectedPreprocessorCollection



    <XmlElement("stringValue", GetType(StringValue)),
 XmlElement("booleanValue", GetType(BooleanValue)),
 XmlElement("integerValue", GetType(IntegerValue)),
 XmlElement("decimalValue", GetType(DecimalValue)),
 XmlElement("binaryValue", GetType(BinaryValue)),
 XmlElement("integerRangeValue", GetType(IntegerRangeValue)),
 XmlElement("decimalRangeValue", GetType(DecimalRangeValue)),
XmlElement("decimalComparisonValue", GetType(DecimalComparisonValue)),
XmlElement("catchAllValue", GetType(CatchAllValue)),
XmlElement("integerComparisonValue", GetType(IntegerComparisonValue)),
XmlElement("stringComparisonValue", GetType(StringComparisonValue)),
 XmlElement("noValue", GetType(NoValue))>
    Public ReadOnly Property Values As KeyValueCollection
        Get
            Return _values
        End Get
    End Property

    <XmlElement("preprocessingRule", GetType(SelectedPreprocessor))>
    Public Property PreProcessingRules As SelectedPreprocessorCollection
        Get
            Return _preprocessingRuleCollection
        End Get
        Set
            _preprocessingRuleCollection = Value
        End Set
    End Property



    Public Sub New(domain As String, occur As Integer)
        Me.Domain = domain
        Me.Occur = occur
    End Sub

    Public Sub New(domain As String, occur As Integer, value As BaseValue)
        Me.New(domain, occur)
        Me.Values.Add(value)
    End Sub

    Public Sub New()
    End Sub


    Public Overloads Function ToString(valuesOnly As Boolean) As String

        If valuesOnly Then
            Return String.Join("#", Values.Select(Function(v) v.ToString()).ToArray())
        Else
            Return Me.ToString
        End If

    End Function

    Public Overrides Function ToString() As String
        Dim sb = New StringBuilder()
        Dim val = String.Join("#", Values.Select(Function(v) v.ToString()).ToArray())
        sb.AppendFormat("[{0}] =({1})", Domain, val)
        Return sb.ToString()
    End Function

End Class
