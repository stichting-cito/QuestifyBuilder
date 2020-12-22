Imports System.Diagnostics.CodeAnalysis
Imports System.Linq
Imports System.Xml.Serialization
Imports Cito.Tester.ContentModel.ResponseAndScoringModel.Solution.ConcreteScoring

<Serializable> _
<XmlRoot("keyFact")> _
Public Class KeyFact
    Inherits BaseFact


    Private _score As Integer = 1



    Public Sub New(id As String)
        MyBase.New()
        Me.Id = id
    End Sub


    Public Sub New()
        MyBase.New()
    End Sub



    <XmlAttribute("score")> _
    Public Property Score As Integer
        Get
            Return _score
        End Get
        Set
            _score = value
        End Set
    End Property



    Friend Function ScorableDomains() As String
        Dim ret = New List(Of String)(Values.Select(Function(v) v.Domain))
        ret.Sort()
        Return String.Join("&", ret.ToArray())
    End Function

    Public Function ContainsValueForProcessing() As Boolean
        Dim keyValue As KeyValue = DirectCast(Values.First(), KeyValue)
        If keyValue.Values.Count >= 1 Then
            Dim value = keyValue.Values.First()
            If TypeOf value Is BooleanValue Then
                If DirectCast(value, BooleanValue).Value Then Return True
            Else
                Return True
            End If
        End If
        Return False
    End Function


End Class
