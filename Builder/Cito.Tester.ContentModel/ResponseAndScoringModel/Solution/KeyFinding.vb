Imports System.Diagnostics.CodeAnalysis
Imports System.Xml.Serialization
Imports System.Text
Imports Cito.Tester.ContentModel.ResponseAndScoringModel.Solution.ConcreteScoring

<Serializable> _
<XmlRoot("keyFinding")> _
Public Class KeyFinding
    Inherits BaseFinding


    Private _method As EnumScoringMethod
    Private _keyFactsets As New List(Of KeyFactSet)

    Private _scoreMethod As IScoringFindingStrategy



    Public Sub New(id As String)
        MyBase.Id = id
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub



    <SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")> _
    <XmlElement("keyFactSet", GetType(KeyFactSet))> _
    <XmlElement("conceptFactSet", GetType(ConceptFactsSet))> _
    Public Overridable ReadOnly Property KeyFactsets As List(Of KeyFactSet)
        Get
            Return _keyFactsets
        End Get
    End Property

    <XmlAttribute("scoringMethod")> _
    Public Property Method As EnumScoringMethod
        Get
            Return _method
        End Get
        Set
            _method = value
        End Set
    End Property

    <XmlIgnore> _
    Public ReadOnly Property MaxFindingScore As Integer
        Get
            Return ScoreMethod.GetMaxScoreForFinding()
        End Get
    End Property

    <XmlIgnore> _
    Friend Property ScoreMethod As IScoringFindingStrategy
        Get
            If (_scoreMethod Is Nothing) Then
                _scoreMethod = ScoringFactory.ConstructDefaultScoringMethod(Me)
            End If
            Return _scoreMethod
        End Get
        Set
            _scoreMethod = value
        End Set
    End Property



    Function ScoreFinding(ResponseFinding As ResponseFinding) As Integer
        Return ScoreMethod.ScoreFinding(ResponseFinding)
    End Function

    Public Overrides Function ToString() As String
        If Me.KeyFactsets.Count = 0 Then
            Return ConvertFactsToString(Me.Facts)
        Else
            Dim returnValueSb As New StringBuilder()

            For Each factset As KeyFactSet In Me.KeyFactsets
                If returnValueSb.Length > 0 Then
                    returnValueSb.Append("|")
                End If
                returnValueSb.Append($"({ConvertFactsToString(factset.Facts)})")
            Next

            If Me.Facts.Count > 0 Then
                If returnValueSb.Length > 0 Then
                    returnValueSb.Append("&")
                End If
                returnValueSb.Append($"{ConvertFactsToString(Me.Facts)}")
            End If

            Return returnValueSb.ToString
        End If
    End Function

    Private Function ConvertFactsToString(facts As List(Of BaseFact)) As String
        Dim returnValueSb As New StringBuilder()

        For Each fact As KeyFact In facts
            If returnValueSb.Length > 0 Then returnValueSb.Append("&")
            Dim valueCounter As Integer = 0
            For Each keyValue As KeyValue In fact.Values
                For Each baseValue As BaseValue In keyValue.Values
                    If valueCounter > 0 Then returnValueSb.Append("#")
                    If baseValue IsNot Nothing Then
                        returnValueSb.Append(baseValue.ToString)
                        valueCounter += 1
                    End If
                Next
            Next
        Next
        Return returnValueSb.ToString()
    End Function


End Class
