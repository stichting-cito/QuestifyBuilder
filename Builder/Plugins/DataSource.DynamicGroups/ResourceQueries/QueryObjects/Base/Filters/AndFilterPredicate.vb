Imports System.Xml.Serialization

<XmlRoot("And")> _
Public Class AndFilterPredicate
    Inherits DyadicFilterPredicate


    Public Overrides ReadOnly Property Name() As String
        Get
            Return My.Resources.AndFilterPredicateName
        End Get
    End Property

    Public Overrides ReadOnly Property NameLocalized() As String
        Get
            Return My.Resources.AndFilterPredicateNameLocalized
        End Get
    End Property



    Public Sub New(one As FilterPredicate, other As FilterPredicate)
        MyBase.new()
        Me.One = one
        Me.Other = other
    End Sub

    Public Sub New()
    End Sub


End Class