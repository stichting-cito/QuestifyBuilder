Imports System.Linq

Public Class GapMatchRichTextScoringParameter : Inherits GapMatchScoringParameter
    Public Shadows Const GapControlName = "gapTextRichText"

    Public Overrides Function GetLabelFor(scoreKey As String) As String

        For Each subParam In Value.Where(Function(v) v.Id = scoreKey)
            Dim gapParam = subParam.InnerParameters.FirstOrDefault(Function(i) i.Name = GapControlName)
            If gapParam IsNot Nothing Then
                Return DirectCast(gapParam, XHtmlParameter).Value
            End If
        Next

        Return Me.Label
    End Function

    Public Overrides Function Transform() As GapMatchScoringParameter

        Dim transformedParam As New GapMatchRichTextScoringParameter() With {.ControllerId = ControllerId, .FindingOverride = FindingOverride}

        For Each parameterCollection As ParameterCollection In Value
            Dim innerParameter As GapTextRichTextParameter = parameterCollection.InnerParameters.OfType(Of GapTextRichTextParameter).FirstOrDefault()
            If innerParameter IsNot Nothing Then
                Dim labelPrm = parameterCollection.InnerParameters.OfType(Of PlainTextParameter).FirstOrDefault(Function(p) p.Name IsNot Nothing AndAlso p.Name.Equals(ElementLabelParameterName, StringComparison.InvariantCultureIgnoreCase))

                transformedParam.Gaps.Add(parameterCollection.Id, New Dictionary(Of String, String) From {
                                       {GapMatchValue, innerParameter.Value},
                                       {GapMatchMax, innerParameter.MatchMax.ToString()},
                                       {GapMatchName, innerParameter.Name},
                                       {GapMatchLabel, If(labelPrm IsNot Nothing AndAlso Not String.IsNullOrEmpty(labelPrm.Value), labelPrm.Value, parameterCollection.Id)}})
            End If
        Next

        transformedParam.Value = New ParameterSetCollection()
        Dim xhtmlGaps = GetGapIdentifiersFromXhtml()

        For Each keyValuePair In xhtmlGaps
            Dim paramSet = New ParameterCollection() With {.Id = keyValuePair.Key}
            paramSet.InnerParameters.Add(New GapTextRichTextParameter() With
                                         {.MatchMax = -1, .Id = keyValuePair.Key, .Name = GapControlName,
                                          .Value = keyValuePair.Value.FirstOrDefault(Function(t) t.Key = GapLabelKey).Value})
            transformedParam.Value.Add(paramSet)
        Next

        Transformed = True

        Return transformedParam
    End Function

End Class
