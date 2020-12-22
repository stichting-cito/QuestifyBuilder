Imports System.Activities
Imports System.Linq
Imports Cito.Tester.ContentModel

Namespace ContentModel.workers.Flow
    Public NotInheritable Class RemoveFactsOfWrongType
        Inherits CodeActivity

        Property BaseFacts As InArgument(Of IList(Of BaseFact))

        Property FactIdsToScoringParameter As InArgument(Of Dictionary(Of String, ScoringParameter))

        Protected Overrides Sub Execute(context As CodeActivityContext)
            Dim baseFacts = context.GetValue(Me.BaseFacts)
            Dim factId2Sp = context.GetValue(Me.FactIdsToScoringParameter)

            For Each baseFact As BaseFact In baseFacts.ToList()
                If Not CheckFactType(baseFact, factId2Sp) Then
                    baseFacts.Remove(baseFact)
                End If
            Next
        End Sub

        Private Function CheckFactType(baseFact As BaseFact, factId2Sp As Dictionary(Of String, ScoringParameter)) As Boolean
            Dim result As Boolean = True
            If Not factId2Sp.ContainsKey(baseFact.Id) Then Return result

            Dim scoringParam As ScoringParameter = factId2Sp(baseFact.Id)
            RemoveWrongTypedValues(baseFact.Values, scoringParam)

            result = baseFact.Values IsNot Nothing AndAlso baseFact.Values.Count > 0
            Return result
        End Function

        Private Sub RemoveWrongTypedValues(valueCollection As ValueCollection, ByVal scoringParam As ScoringParameter)
            If valueCollection Is Nothing Then Return

            For Each value In valueCollection.OfType(Of KeyValue)().ToList()
                If value.Values IsNot Nothing Then
                    If TypeOf scoringParam Is OrderScoringParameter Then
                        value.Values.RemoveAll(Function(iv) Not TypeOf iv Is IntegerValue)

                    ElseIf TypeOf scoringParam Is ChoiceScoringParameter Then
                        If scoringParam.IsSingleChoice Then
                            value.Values.RemoveAll(Function(sv) Not TypeOf sv Is StringValue)
                        Else
                            value.Values.RemoveAll(Function(bv) Not TypeOf bv Is BooleanValue)
                        End If

                    ElseIf TypeOf scoringParam Is IntegerScoringParameter Then
                        value.Values.RemoveAll(Function(iv) Not TypeOf iv Is IntegerValue AndAlso Not TypeOf iv Is IntegerRangeValue AndAlso Not TypeOf iv Is IntegerComparisonValue)

                    ElseIf TypeOf scoringParam Is DecimalScoringParameter Then
                        value.Values.RemoveAll(Function(iv) Not TypeOf iv Is DecimalValue AndAlso Not TypeOf iv Is DecimalRangeValue AndAlso Not TypeOf iv Is DecimalComparisonValue)

                    ElseIf TypeOf scoringParam Is StringScoringParameter OrElse
                           TypeOf scoringParam Is TimeScoringParameter OrElse
                           TypeOf scoringParam Is DateScoringParameter Then
                        value.Values.RemoveAll(Function(iv) Not TypeOf iv Is StringValue AndAlso Not TypeOf iv Is StringComparisonValue)

                    End If
                End If

                If value.Values Is Nothing OrElse value.Values.Count = 0 Then valueCollection.Remove(value)
            Next
        End Sub

    End Class
End Namespace