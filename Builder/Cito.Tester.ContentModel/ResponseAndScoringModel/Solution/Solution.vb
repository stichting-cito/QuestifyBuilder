Imports System.IO
Imports System.Linq
Imports Cito.Tester.Common.Logging
Imports Cito.Tester.Common.SerializeHelper
Imports System.Xml.Serialization

<Serializable>
<XmlRoot("solution")>
<DebuggerDisplay("{DebuggerDisplay,nq}")>
Public Class Solution

    Private _id As String
    Private _autoScoring As Boolean?
    Private _findings As KeyFindingCollection
    Private _conceptFindings As ConceptFindingCollection
    Private _aspectReferenceSetCollection As New AspectReferenceSetCollection()
    Private _itemScoreTranslationTable As ItemScoreTranslationTable

    <XmlAttribute("id")>
    Public Property Id As String
        Get
            Return _id
        End Get
        Set
            _id = value
        End Set
    End Property

    <XmlArray("keyFindings"), XmlArrayItem("keyFinding", GetType(KeyFinding))>
    Public Overridable ReadOnly Property Findings As KeyFindingCollection
        Get
            Return _findings
        End Get
    End Property

    <XmlIgnore>
    Public ReadOnly Property ConceptFindingsSpecified As Boolean
        Get
            Return _conceptFindings IsNot Nothing AndAlso Not _conceptFindings.Count = 0
        End Get
    End Property

    <XmlArray("conceptFindings"), XmlArrayItem("conceptFinding", GetType(ConceptFinding))>
    Public ReadOnly Property ConceptFindings As ConceptFindingCollection
        Get
            Return _conceptFindings
        End Get
    End Property

    <XmlArray("aspectReferences"), XmlArrayItem("aspectReferenceSet", GetType(AspectReferenceCollection))>
    Public ReadOnly Property AspectReferenceSetCollection As AspectReferenceSetCollection
        Get
            Return _aspectReferenceSetCollection
        End Get
    End Property

    <XmlAttribute("maxSolutionRawScore")>
    Public ReadOnly Property MaxSolutionRawScore As Integer
        Get
            Return Me.GetMaxSolutionRawScore
        End Get
    End Property

    <XmlAttribute("maxSolutionTranslatedScore")>
    Public ReadOnly Property MaxSolutionTranslatedScore As Decimal?
        Get
            Return Me.GetMaxSolutionTranslatedScore()
        End Get
    End Property

    <XmlIgnore>
    Public ReadOnly Property ItemScoreTranslationTableSpecified As Boolean
        Get
            Return _itemScoreTranslationTable IsNot Nothing AndAlso
                _itemScoreTranslationTable.Any()
        End Get
    End Property

    Public ReadOnly Property ItemScoreTranslationTable As ItemScoreTranslationTable
        Get
            Return _itemScoreTranslationTable
        End Get
    End Property

    Private ReadOnly Property DebuggerDisplay As String
        Get
            Return XmlSerializeToString(Me)
        End Get
    End Property

    <XmlAttribute("autoScoring")>
    Public Property AutoScoring() As Boolean
        Get
            If _autoScoring.HasValue Then
                Return _autoScoring.Value
            Else
                Return True
            End If
        End Get
        Set(value As Boolean)
            _autoScoring = value
        End Set
    End Property

    Public ReadOnly Property AutoScoringSpecified() As Boolean
        Get
            Return _autoScoring.HasValue
        End Get
    End Property

    Public Function ScoreSolution(response As Response) As Integer
        Dim cumulativeScore As Integer = 0
        Dim aspectScoring As Boolean = False

        Dim clonedKeyFindingCollection As KeyFindingCollection
        Using memoryStream As New MemoryStream
            XmlSerializeToStream(memoryStream, Me.Findings)
            memoryStream.Position = 0

            clonedKeyFindingCollection = DirectCast(XmlDeserializeFromStream(memoryStream, GetType(KeyFindingCollection)), KeyFindingCollection)
        End Using

        Log.Indent()
        Log.TraceInformation(TraceCategory.ScoreModel, "Scoring Solution")

        For Each responseFinding As ResponseFinding In response.Findings
            If clonedKeyFindingCollection.Contains(responseFinding.Id) Then
                Dim keyFinding As KeyFinding = clonedKeyFindingCollection.Item(responseFinding.Id)
                cumulativeScore += keyFinding.ScoreFinding(responseFinding)
            Else
                If Me.AspectReferenceSetCollection IsNot Nothing Then
                    For Each aspectreference As AspectReferenceCollection In Me.AspectReferenceSetCollection
                        If String.Equals(aspectreference.Id, responseFinding.Id, StringComparison.InvariantCultureIgnoreCase) Then
                            aspectScoring = True
                        End If
                    Next
                End If
                If Not aspectScoring AndAlso Not responseFinding.Facts.Count = 0 Then
                    Log.TraceError(TraceCategory.ScoreModel, "response and solution do not match")
                    Throw New ArgumentException("response and solution do not match")
                End If
            End If
        Next

        Log.TraceInformation(TraceCategory.ScoreModel, $"Solution score = {cumulativeScore}")
        Log.TraceInformation(TraceCategory.ScoreModel, "End Of Scoring Solution")
        Log.Unindent()
        Return cumulativeScore
    End Function

    Private Function GetMaxSolutionRawScore() As Integer
        Dim cummulativeMaxScore As Integer = 0

        For Each keyFinding As KeyFinding In Me.Findings
            cummulativeMaxScore += keyFinding.MaxFindingScore()
        Next

        For Each collection As AspectReferenceCollection In Me.AspectReferenceSetCollection
            cummulativeMaxScore += collection.GetMaxScore()
        Next

        Return cummulativeMaxScore
    End Function

    Public Function GetMaxSolutionTranslatedScore() As Nullable(Of Decimal)
        Dim maxScore As Nullable(Of Integer) = GetMaxSolutionRawScore()
        Dim maxXlatedScore As Nullable(Of Decimal)

        If ItemScoreTranslationTableSpecified AndAlso ItemScoreTranslationTable.Count > 0 Then
            Dim highestValue As Double = -1

            For i As Integer = 0 To ItemScoreTranslationTable.Count - 1
                If ItemScoreTranslationTable(i).TranslatedScore > highestValue Then
                    highestValue = ItemScoreTranslationTable(i).TranslatedScore
                End If
            Next

            maxXlatedScore = CDec(highestValue)
        Else
            maxXlatedScore = maxScore
        End If

        Return If(maxXlatedScore > 0, maxXlatedScore, Nothing)
    End Function

    Public Function GetFindingOrMakeIt(id As String) As KeyFinding
        Dim finding = Findings.FirstOrDefault(Function(f) f.Id = id)
        If finding Is Nothing Then
            finding = New KeyFinding(id)
            Findings.Add(finding)
        End If
        Return finding
    End Function

    Public Sub New()
        _findings = New KeyFindingCollection()
        _conceptFindings = New ConceptFindingCollection()
        _itemScoreTranslationTable = New ItemScoreTranslationTable()
    End Sub

End Class

