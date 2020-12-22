Imports System.Activities
Imports Cito.Tester.ContentModel
Imports System.Linq

Namespace ContentModel.workers.Flow

    Public NotInheritable Class RemoveOutOfBoundsValues
        Inherits CodeActivity

        Property BaseFact As InArgument(Of BaseFact)

        Property FactIdsToScoringParameter As InArgument(Of Dictionary(Of String, ScoringParameter))

        Protected Overrides Sub Execute(ByVal context As CodeActivityContext)

            Dim theBaseFact = context.GetValue(Me.BaseFact)
            Dim factId2Sp = context.GetValue(Me.FactIdsToScoringParameter)

            If factId2Sp.ContainsKey(theBaseFact.Id) Then
                Dim scoringParam As ScoringParameter = factId2Sp(theBaseFact.Id)
                Dim iTransformableInterface As ITransformable = TryCast(scoringParam, ITransformable)

                If iTransformableInterface IsNot Nothing Then
                    Dim ggmScoringParameter As GraphGapMatchScoringParameter = TryCast(scoringParam, GraphGapMatchScoringParameter)

                    If ggmScoringParameter IsNot Nothing Then
                        If iTransformableInterface.IsTransformed Then
                            RemoveValuesNotDefinedInGapKeyCollection(theBaseFact, ggmScoringParameter.Gaps.Keys)
                        End If
                    Else
                        Dim gapmatchScoringParameter As GapMatchScoringParameter = TryCast(scoringParam, GapMatchScoringParameter)

                        If gapmatchScoringParameter IsNot Nothing Then
                            If Not iTransformableInterface.IsTransformed Then
                                RemoveValuesNotDefinedInGapKeyCollection(theBaseFact, gapmatchScoringParameter.Gaps.Keys)
                            End If
                        End If
                    End If

                End If
            End If
        End Sub


        Private Shared Sub RemoveValuesNotDefinedInGapKeyCollection(ByVal baseFact As BaseFact, ByVal gapKeys As Dictionary(Of String, Dictionary(Of String, String)).KeyCollection)
            For Each factValue As KeyValue In baseFact.Values
                Dim valuesToRemove As New List(Of BaseValue)

                For Each baseValue As BaseValue In factValue.Values
                    Dim sv As StringValue = TryCast(baseValue, StringValue)
                    If sv IsNot Nothing Then
                        If Not gapKeys.Contains(sv.Value) Then
                            valuesToRemove.Add(baseValue)
                        End If
                    End If
                Next

                valuesToRemove.ForEach(Sub(x) factValue.Values.Remove(x))
            Next
        End Sub

    End Class

End Namespace