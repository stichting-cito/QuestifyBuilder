
Imports System.Xml.Serialization

<Serializable> _
Public MustInherit Class TypedRangeValue(Of T)
    Inherits BaseValue


    Private _rangeEnd As T
    Private _rangeStart As T



    Protected Sub New()
        MyBase.New()
    End Sub



    Public Overrides ReadOnly Property IsRange As Boolean
        Get
            Return True
        End Get
    End Property

    <XmlAttribute("rangeEnd")> _
    Public Property RangeEnd As T
        Get
            Return _rangeEnd
        End Get
        Set
            _rangeEnd = value
        End Set
    End Property

    <XmlAttribute("rangeStart")> _
    Public Property RangeStart As T
        Get
            Return _rangeStart
        End Get
        Set
            _rangeStart = value
        End Set
    End Property



    Public Overrides Function ToString() As String
        Return $"{_rangeStart.ToString}-{_rangeEnd.ToString}"
    End Function


End Class