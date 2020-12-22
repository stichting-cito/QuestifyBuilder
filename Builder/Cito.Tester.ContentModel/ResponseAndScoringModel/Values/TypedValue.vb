
Imports System.Xml.Serialization

<Serializable> _
Public MustInherit Class TypedValue(Of T)
    Inherits BaseValue


    Private _value As T



    Protected Sub New()
        MyBase.New()
    End Sub



    Public Overrides ReadOnly Property IsRange As Boolean
        Get
            Return False
        End Get
    End Property

    <XmlElement("typedValue")> _
    Public Property Value As T
        Get
            Return _value
        End Get
        Set
            _value = value
        End Set
    End Property



    Public Overrides Function ToString() As String
        Return _value.ToString
    End Function


End Class