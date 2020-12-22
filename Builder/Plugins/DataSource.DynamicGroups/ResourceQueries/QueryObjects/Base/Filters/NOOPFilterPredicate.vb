Imports System.Xml.Serialization

<XmlRoot("NOOP")> _
Public Class NOOPFilterPredicate
    Inherits FilterPredicate


    Public Overrides ReadOnly Property Name() As String
        Get
            Return My.Resources.NOOPFilterPredicateName
        End Get
    End Property

    Public Overrides ReadOnly Property NameLocalized() As String
        Get
            Return My.Resources.NOOPFilterPredicateNameLocalized
        End Get
    End Property


End Class