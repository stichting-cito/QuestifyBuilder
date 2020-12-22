Imports System.Xml.Serialization

<XmlRoot("Or")> _
Public Class OrFilterPredicate
    Inherits DyadicFilterPredicate


    Public Sub New(one As FilterPredicate, other As FilterPredicate)
        MyBase.new()
        Me.One = one
        Me.Other = other
    End Sub

    Public Sub New()
    End Sub



    Public Overrides ReadOnly Property Name() As String
        Get
            Return My.Resources.OrFilterPredicateName
        End Get
    End Property

    Public Overrides ReadOnly Property NameLocalized() As String
        Get
            Return My.Resources.OrFilterPredicateNameLocalized
        End Get
    End Property


End Class