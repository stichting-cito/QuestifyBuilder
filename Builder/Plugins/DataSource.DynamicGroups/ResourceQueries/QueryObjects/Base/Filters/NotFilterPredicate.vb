Imports System.Xml.Serialization

<XmlRoot("Not")> _
Public Class NotFilterPredicate
    Inherits MonadicFilterPredicate


    Public Sub New(wrapped As FilterPredicate)
        MyBase.New()
        Me.Wrapped = wrapped
    End Sub

    Public Sub New()
    End Sub



    Public Overrides ReadOnly Property Name() As String
        Get
            Return My.Resources.NotFilterPredicateName
        End Get
    End Property

    Public Overrides ReadOnly Property NameLocalized() As String
        Get
            Return My.Resources.NotFilterPredicateNameLocalized
        End Get
    End Property


End Class