Imports Cito.Tester.ContentModel
Imports System.Linq

Namespace ContentModel.Scoring

    Friend MustInherit Class BaseGapScoringManipulator(Of TValue) : Inherits BaseScoringManipulator(Of ScoringParameter) : Implements IGapScoringManipulator(Of TValue)
        Public Sub New(manipulator As IFindingManipulator, param As ScoringParameter)
            MyBase.New(manipulator, param)
            manipulator.SetDomainOverride(Function(id) GetDomainForKey(id))
        End Sub


        Public Sub Clear() Implements IGapScoringManipulator(Of TValue).Clear
            MyBase.RemoveAllManipulatableFacts()
        End Sub

        Public Function GetkeyStatus() As IDictionary(Of String, IEnumerable(Of GapValue(Of TValue))) Implements IGapScoringManipulator(Of TValue).GetKeyStatus
            Dim ret As New SortedDictionary(Of String, IEnumerable(Of GapValue(Of TValue)))(StringComparer.InvariantCultureIgnoreCase)

            For Each id In GetKeysAlreadyManipulated()

                If (Not String.IsNullOrEmpty(ScoreParameter.InlineId) AndAlso Not id.EndsWith(ScoreParameter.InlineId)) AndAlso
                    (Not ScoreParameter.Value.Any(Function(p) p.Id = id)) Then
                    Continue For
                ElseIf (Not String.IsNullOrEmpty(ScoreParameter.CollectionIdx) AndAlso Not id.Equals(ScoreParameter.CollectionIdx)) Then
                    Continue For
                End If
                Dim lst = Manipulator.GetKeys(GetFactIdentfier(id)).ToList()
                Debug.Assert(lst.Count <= 1, "Unable to deal with multiple KeyFacts")
                If (lst.Count = 1) Then
                    ret.Add(id, GetValues(lst(0)).ToList())
                End If
            Next

            If (ScoreParameter.Value IsNot Nothing) Then
                If (Not String.IsNullOrEmpty(ScoreParameter.CollectionIdx) AndAlso ScoreParameter.Value.Count = 1) Then
                    If (Not ret.ContainsKey(ScoreParameter.CollectionIdx)) Then
                        ret.Add(ScoreParameter.CollectionIdx, New GapValue(Of TValue)() {GetDefaultValue()})
                    End If
                Else
                    For Each p In ScoreParameter.Value
                        If (Not ret.ContainsKey(p.Id)) Then
                            ret.Add(p.Id, New GapValue(Of TValue)() {GetDefaultValue()})
                        End If
                    Next
                End If
            End If

            Return ret
        End Function


        Public Function GetValue(key As String, index As Integer) As GapValue(Of TValue) Implements IGapScoringManipulator(Of TValue).GetValue
            Dim result = GetkeyStatus()
            Dim factWithValues = result(key)
            Dim values = factWithValues.ToList()
            Return values(index)
        End Function

        Public Overridable Sub SetKey(key As String, ParamArray values() As TValue) Implements IGapScoringManipulator(Of TValue).SetKey
            Manipulator.SetKeyWithOptionals(GetFactIdentfier(key), values.Select(Function(v) New GapValue(Of TValue)(v)).ToArray())
        End Sub

        Public Overridable Sub SetKey(key As String, ParamArray values() As GapValue(Of TValue)) Implements IGapScoringManipulator(Of TValue).SetKey
            Manipulator.SetKeyWithOptionals(GetFactIdentfier(key), values)
        End Sub

        Public Overridable Sub SetKeys(key As String, values As IEnumerable(Of TValue)) Implements IGapScoringManipulator(Of TValue).SetKeys
            Manipulator.SetKeyWithOptionals(GetFactIdentfier(key), values.Select(Function(v) New GapValue(Of TValue)(v)).ToArray())
        End Sub

        Public Overridable Sub SetKeys(key As String, values As IEnumerable(Of GapValue(Of TValue))) Implements IGapScoringManipulator(Of TValue).SetKeys
            Manipulator.SetKeyWithOptionals(GetFactIdentfier(key), values.ToArray())
        End Sub

        Public Overridable Sub ReplaceKeyValueAt(key As String, value As TValue, index As Integer) Implements IGapScoringManipulator(Of TValue).ReplaceKeyValueAt
            Manipulator.ReplaceKeyWithSpecificOptionals(GetFactIdentfier(key), New GapValue(Of TValue)(value), index)
        End Sub

        Public Overridable Sub ReplaceKeyValueAt(key As String, value As GapValue(Of TValue), index As Integer) Implements IGapScoringManipulator(Of TValue).ReplaceKeyValueAt
            Manipulator.ReplaceKeyWithSpecificOptionals(GetFactIdentfier(key), value, index)
        End Sub

        Public Sub RemoveKey(key As String) Implements IGapScoringManipulator(Of TValue).RemoveKey
            Manipulator.RemoveFact(GetFactIdentfier(key))
        End Sub

        Public Function GetPreProcessingMethods(key As String) As IEnumerable(Of String) Implements IGapScoringManipulator(Of TValue).GetPreProcessingMethods
            Return Manipulator.GetPreProcessingMethods(GetFactIdentfier(key))
        End Function

        Public Sub SetPreProcessingMethods(key As String, preProcessing As IEnumerable(Of String)) Implements IGapScoringManipulator(Of TValue).SetPreProcessingMethods
            Manipulator.SetPreProcessingMethods(GetFactIdentfier(key), preProcessing)
        End Sub

        Public Function GetValuePrefixes(key As String) As IEnumerable(Of String) Implements IGapScoringManipulator(Of TValue).GetValuePrefixes
            Dim result As New List(Of String)
            Dim keys As IEnumerable(Of IEnumerable(Of BaseValue)) = Manipulator.GetKeys(GetFactIdentfier(key)).ToList()
            If (keys.Count = 1) Then
                keys.First().ToList().ForEach(Sub(v)
                                                  If TypeOf v Is StringComparisonValue Then
                                                      Dim icValue = DirectCast(v, StringComparisonValue)
                                                      result.Add(icValue.comparisonPrefix(icValue.TypeOfComparison))
                                                  ElseIf TypeOf v Is IntegerComparisonValue Then
                                                      Dim icValue = DirectCast(v, IntegerComparisonValue)
                                                      result.Add(icValue.comparisonPrefix(icValue.TypeOfComparison))
                                                  ElseIf TypeOf v Is DecimalComparisonValue Then
                                                      Dim icValue = DirectCast(v, DecimalComparisonValue)
                                                      result.Add(icValue.comparisonPrefix(icValue.TypeOfComparison))
                                                  Else
                                                      result.Add(String.Empty)
                                                  End If
                                              End Sub)
            End If
            Return result
        End Function

        Public Sub SetKeyWithDefaultValue(key As String) Implements IGapScoringManipulator(Of TValue).SetKeyWithDefaultValue
            Manipulator.SetKey(GetFactIdentfier(key), GetDefaultValue())
        End Sub


        Protected MustOverride Function GetValues(lst As IEnumerable(Of BaseValue)) As IEnumerable(Of GapValue(Of TValue))

        Protected MustOverride Function GetDefaultValue() As GapValue(Of TValue)

        Public Overrides Function GetDomainForKey(ByVal key As String) As String
            Return FactValueDomainNameGenerators.GetDomainByScoringParam(ScoreParameter)
        End Function

    End Class
End Namespace
