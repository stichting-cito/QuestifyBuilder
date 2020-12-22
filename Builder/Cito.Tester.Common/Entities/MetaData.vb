Imports System.Xml.Serialization

<DebuggerDisplay("Name:[{Name}] Value:[{Value}]")>
Public Class MetaData

    Public Enum enumMetaDataType
        unknown = 0
        BankMetaData = 1
        BankCustomProperty = 2
    End Enum



    <XmlAttribute("title")>
    Public Property Title() As String

    <XmlAttribute("name")>
    Public Property Name() As String

    <XmlAttribute("value")>
    Public Overridable Property Value() As String

    <XmlAttribute("isSelected")>
    Public Property IsSelected() As Boolean = False

    <XmlAttribute("metatDatatype")>
    Public Property MetaDatatype() As enumMetaDataType

    <XmlAttribute("reference")>
    Public Property Reference() As String

    <XmlAttribute("applicableTo")>
    Public Property ApplicableTo() As Integer = 0

    <XmlAttribute("publishable")>
    Public Property Publishable() As Boolean = False

    <XmlAttribute("scorable")>
    Public Property Scorable() As Boolean = False


    Public Sub New()
        Me.MetaDatatype = enumMetaDataType.unknown
    End Sub

    Public Sub New(name As String, value As String, type As enumMetaDataType)
        Me.New()
        If String.IsNullOrEmpty(name) Then
            Throw New ArgumentException("name of value is null or empty")
        End If

        Me.Name = name
        Me.Value = value
        Me.MetaDatatype = type
    End Sub

End Class
