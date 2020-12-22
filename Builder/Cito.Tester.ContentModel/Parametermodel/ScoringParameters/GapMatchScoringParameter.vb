Imports System.Xml.Serialization
Imports System.Linq

Public Class GapMatchScoringParameter : Inherits ScoringParameter : Implements ITransformable(Of GapMatchScoringParameter)

    Public Const GapMatchValue As String = "Value"
    Public Const GapMatchMax As String = "MatchMax"
    Public Const GapMatchName As String = "Name"
    Public Const GapMatchLabel As String = "Label"
    Public Const GapControlName As String = "gapText"
    Public Const GapLabelKey As String = "inlineGapMatchLabel"
    Public Const GapIdKey As String = "inlineGapMatchId"

    Private _gapXhtmlParameter As New XHtmlParameter
    Private _gaps As New Dictionary(Of String, Dictionary(Of String, String))
    Protected Transformed As Boolean

    <ParameterControlAttribute>
    <XmlElement("xhtmlParameter")>
    Public Property GapXhtmlParameter As XHtmlParameter
        Get
            Return _gapXhtmlParameter
        End Get
        Set(v As XHtmlParameter)
            _gapXhtmlParameter = v
        End Set
    End Property


    <XmlIgnore> _
    Public Overrides ReadOnly Property IsSingleValue As Boolean
        Get
            Return True
        End Get
    End Property

    Friend Function GetGapIdentifiersFromXhtml() As Dictionary(Of String, Dictionary(Of String, String))

        Dim gapList = New Dictionary(Of String, Dictionary(Of String, String))

        For Each inlineElement As KeyValuePair(Of String, InlineElement) In _gapXhtmlParameter.GetInlineElements()

            For Each parameterCollection As ParameterCollection In inlineElement.Value.Parameters
                GetGapLabelAndValueFromParameterCollection(parameterCollection, gapList)
            Next

        Next

        Return gapList
    End Function

    Private Sub GetGapLabelAndValueFromParameterCollection(parameters As ParameterCollection, gapList As Dictionary(Of String, Dictionary(Of String, String)))

        Dim gapIdParameter = parameters.TryGetParameterByName(Of PlainTextParameter)(GapIdKey)

        If (gapIdParameter IsNot Nothing) Then

            Dim id = gapIdParameter.Value

            Dim gapLabelParameter = parameters.GetParameterByName(GapLabelKey)

            If TypeOf gapLabelParameter Is PlainTextParameter Then
                gapList.Add(id, New Dictionary(Of String, String) From {{GapLabelKey, DirectCast(gapLabelParameter, PlainTextParameter).Value}})
            Else
                gapList.Add(id, New Dictionary(Of String, String) From {{GapLabelKey, id}})
            End If

        End If

    End Sub

    <XmlIgnore>
    Public ReadOnly Property Gaps As Dictionary(Of String, Dictionary(Of String, String))
        Get
            Return _gaps
        End Get
    End Property

    Public Overrides Function GetLabelFor(scoreKey As String) As String

        For Each subParam In Value.Where(Function(v) v.Id = scoreKey)
            Dim gapParam = subParam.InnerParameters.FirstOrDefault(Function(i) i.Name = GapControlName)
            If gapParam IsNot Nothing Then
                Return DirectCast(gapParam, PlainTextParameter).Value
            End If
        Next
        Return Me.Label
    End Function

    Public Overrides ReadOnly Property AlternativesCount As Integer?
        Get
            If Value IsNot Nothing AndAlso Value.Count > 0 Then
                Return Value.Count
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public Overrides Function GetParametersWithDesignerSettings() As IEnumerable(Of ParameterBase)
        Return New ParameterBase() {GapXhtmlParameter}
    End Function

    Public Overridable Function Transform() As GapMatchScoringParameter Implements ITransformable(Of GapMatchScoringParameter).Transform

        Dim transformedParam As New GapMatchScoringParameter() With {.ControllerId = ControllerId, .FindingOverride = FindingOverride}

        For Each parameterCollection As ParameterCollection In Value
            Dim innerParameter As GapTextParameter = parameterCollection.InnerParameters.OfType(Of GapTextParameter).FirstOrDefault()
            If innerParameter IsNot Nothing Then
                Dim labelPrm = parameterCollection.InnerParameters.FirstOrDefault(Function(p) p.Name IsNot Nothing AndAlso p.Name.Equals(ElementLabelParameterName, StringComparison.InvariantCultureIgnoreCase))
                transformedParam.Gaps.Add(parameterCollection.Id, New Dictionary(Of String, String) From {
                                           {GapMatchValue, innerParameter.Value},
                                           {GapMatchMax, innerParameter.MatchMax.ToString()},
                                           {GapMatchName, innerParameter.Name},
                                           {GapMatchLabel, If(labelPrm IsNot Nothing, DirectCast(labelPrm, PlainTextParameter).Value, String.Empty)}
                                       })
            End If
        Next

        transformedParam.Value = New ParameterSetCollection()
        Dim xhtmlGaps = GetGapIdentifiersFromXhtml()
        For Each keyValuePair In xhtmlGaps
            Dim paramSet = New ParameterCollection() With {.Id = keyValuePair.Key}
            paramSet.InnerParameters.Add(New GapTextParameter() With
                                         {.MatchMax = -1, .Id = keyValuePair.Key, .Name = GapControlName,
                                          .Value = keyValuePair.Value.FirstOrDefault(Function(t) t.Key = GapLabelKey).Value})
            transformedParam.Value.Add(paramSet)
        Next

        Transformed = True
        Return transformedParam
    End Function

    Public Overridable ReadOnly Property CanTransform As Boolean Implements ITransformable.CanTransform
        Get
            Return True
        End Get
    End Property

    Public ReadOnly Property IsTransformed As Boolean Implements ITransformable.IsTransformed
        Get
            Return Transformed
        End Get
    End Property

    Public Overrides ReadOnly Property ShouldSuffix As Boolean
        Get
            Return False
        End Get
    End Property
End Class