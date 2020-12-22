Imports Cito.Tester.ContentModel
Imports System.Linq
Imports Questify.Builder.Logic.Service.HelperFunctions

Namespace ContentModel.Scoring

    Friend Class ConceptManipulator
        Inherits FindingManipulatorBase


        Private ReadOnly _justInTimeFinding As CreateObjectJIT(Of ConceptFinding)



        Sub New(justInTimeFinding As CreateObjectJIT(Of ConceptFinding))

            MyBase.New(New CreateObjectJIT(Of BaseFinding)(justInTimeFinding.CurrentValue,
                                                           Function() justInTimeFinding.GetEnsuredValue))
            _justInTimeFinding = justInTimeFinding
        End Sub

        Sub New(finding As ConceptFinding)
            Me.New(New CreateObjectJIT(Of ConceptFinding)(finding, Function() finding))
        End Sub


        Public Overrides ReadOnly Property IsConceptManipulator As Boolean
            Get
                Return True
            End Get
        End Property

        Protected Overrides Sub AddExtraValues(factValue As BaseFactValue,
                                               baseValue As BaseValue)
            Dim kValue As ConceptValue = DirectCast(factValue, ConceptValue)
            kValue.Values.Add(baseValue)
        End Sub

        Protected Overrides Function CreateBaseFactValue(id As String, baseValue As BaseValue) _
            As BaseFactValue
            Return New ConceptValue(id, 1, baseValue) With {.Domain = GetDomain(id)}
        End Function

        Friend Overrides Function CreateFact(id As String) As BaseFact
            Return New ConceptFact(id)
        End Function

        Protected Overrides Iterator Function GetFactValues(factValue As BaseFactValue) _
            As IEnumerable(Of BaseValue)
            Dim kv As ConceptValue = DirectCast(factValue, ConceptValue)
            For Each v In kv.Values
                Yield v
            Next
        End Function

        Protected Overrides Sub RemoveValueFromFactValues(factValue As BaseFactValue,
                                                          compareValue As BaseValue)
            Dim kv As ConceptValue = DirectCast(factValue, ConceptValue)
            For Each v In kv.Values
                If (v.IsMatch(compareValue)) Then
                    kv.Values.Remove(v)
                    Exit For
                End If
            Next
        End Sub

        Protected Overrides Sub ReplaceValue(factValue As BaseFactValue,
                                             baseValue As BaseValue, index As Integer)
            Dim kValue As ConceptValue = DirectCast(factValue, ConceptValue)
            kValue.Values(index) = baseValue
        End Sub

        Protected Overrides Sub SetScore(fact As BaseFact, scoreToSet As Integer)
            Dim f As ConceptFact = DirectCast(fact, ConceptFact)
            f.Score = scoreToSet
        End Sub

        Public Overloads Overrides Function GetScoringMethod() As EnumScoringMethod
            Return _justInTimeFinding.GetEnsuredValue.Method
        End Function

        Public Overloads Overrides Sub SetScoringMethod(method As EnumScoringMethod)
            _justInTimeFinding.GetEnsuredValue.Method = method
        End Sub

        Protected Overloads Overrides Function GetPreProcessingMethods(baseFactValue As BaseFactValue) As IEnumerable(Of String)
            Dim ret As New List(Of String)
            If (baseFactValue IsNot Nothing) Then
                Dim kv As ConceptValue = DirectCast(baseFactValue, ConceptValue)

                If (kv.PreProcessingRules IsNot Nothing) Then
                    ret.AddRange(
                        From p In kv.PreProcessingRules
                        Select p.Rule)
                End If

            End If
            Return ret
        End Function

        Protected Overloads Overrides Sub SetPreProcessingMethods(baseFactValue As BaseFactValue, preProcessing As IEnumerable(Of String))
            If (baseFactValue IsNot Nothing) Then
                Dim kv As ConceptValue = DirectCast(baseFactValue, ConceptValue)

                kv.PreProcessingRules = New SelectedPreprocessorCollection()
                kv.PreProcessingRules.AddRange(
                    From p In preProcessing
                    Select New SelectedPreprocessor() With {.rule = p})
            End If
        End Sub

        Public Overrides ReadOnly Property CanManipulateSets As Boolean
            Get
                Return True
            End Get
        End Property

        Public Overrides ReadOnly Property HasSets As Boolean
            Get
                If (_justInTimeFinding.CurrentValue IsNot Nothing) Then
                    Return _justInTimeFinding.CurrentValue.KeyFactsets.Count > 0
                End If
                Return False
            End Get
        End Property

        Public Overrides Function GetFactSetNumbers(scoreKey As String) As IEnumerable(Of Integer)

            If _justInTimeFinding.CurrentValue.KeyFactsets.Any() Then

                Return _
                    New FactSetManipulatorTarget(Of ConceptFinding, ConceptFactsSet)(_justInTimeFinding, Me,
                                                                                     FactIdPostFix).GetFactSetNumbers(
                                                                                         scoreKey)
            End If

            Return GetTarget().GetFactSetNumbers(scoreKey)
        End Function

        Friend Overrides Function TryFindTargetForId(postFix As String) As IFindingManipulatorTarget
            Dim finding = _justInTimeFinding.CurrentValue

            If (finding Is Nothing) Then
                Return New FindingManipulatorTarget(JustInTimeFinding, Me, postFix)
            Else
                If (finding.KeyFactsets.Count = 0) Then
                    Return New FindingManipulatorTarget(JustInTimeFinding, Me, postFix)
                Else
                    Dim factSetManipulator =
        New FactSetManipulatorTarget(Of ConceptFinding, ConceptFactsSet)(_justInTimeFinding, Me,
                                                                         postFix)
                    Dim isFactIdPresent = factSetManipulator.ContainsFactWithPostfix(postFix)

                    If (isFactIdPresent) Then
                        Return factSetManipulator
                    End If

                    Return New FindingManipulatorTarget(JustInTimeFinding, Me, postFix)

                End If
            End If
        End Function

        Friend Overrides Function GetTargetForSetNumber(setNumber As Integer?, factIdPostFix As String) _
            As IFindingManipulatorTarget
            Dim finding = _justInTimeFinding.CurrentValue

            If (finding Is Nothing) Then
                Debug.Assert(False, "Not Handled")
            Else

                If (setNumber.HasValue) Then
                    Debug.Assert(setNumber.Value < finding.KeyFactsets.Count, "Whoa")
                    Return _
                        New FactSetManipulatorTarget(Of ConceptFinding, ConceptFactsSet)(_justInTimeFinding, Me,
                                                                                         factIdPostFix) _
                            With {.FactSetNumber = setNumber.Value}
                Else
                    Return New FindingManipulatorTarget(JustInTimeFinding, Me, factIdPostFix)
                End If

            End If

            Throw New NotSupportedException()
        End Function

        Public Overrides Function CreateNewFactSet() As Integer
            Dim factSetManipulator = New FactSetManipulatorTarget(Of ConceptFinding, ConceptFactsSet)(_justInTimeFinding,
                                                                                                      Me, FactIdPostFix)
            Return factSetManipulator.CreateFactSet()
        End Function

        Protected Overrides Function GetSetNumbers() As IEnumerable(Of Integer)
            Dim ret As New List(Of Integer)
            Dim teller As Integer = 0

            For Each x In _justInTimeFinding.GetEnsuredValue().KeyFactsets
                ret.Add(teller)
                teller += 1
            Next
            Return ret
        End Function
    End Class
End Namespace