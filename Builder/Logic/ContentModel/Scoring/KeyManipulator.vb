Imports Cito.Tester.ContentModel
Imports System.Linq
Imports Questify.Builder.Logic.Service.HelperFunctions
Imports Questify.Builder.Logic.Service.Logging

Namespace ContentModel.Scoring
    Friend Class KeyManipulator
        Inherits FindingManipulatorBase


        Private ReadOnly _justInTimeFinding As CreateObjectJIT(Of KeyFinding)



        Sub New(justInTimeFinding As CreateObjectJIT(Of KeyFinding))
            MyBase.New(New CreateObjectJIT(Of BaseFinding)(justInTimeFinding.CurrentValue,
                                                           Function() justInTimeFinding.GetEnsuredValue))
            _justInTimeFinding = justInTimeFinding
        End Sub

        Sub New(finding As KeyFinding)
            Me.New(New CreateObjectJIT(Of KeyFinding)(finding, Function() finding))
        End Sub


        Protected Overrides Sub AddExtraValues(factValue As BaseFactValue, baseValue As BaseValue)
            Dim kValue As KeyValue = DirectCast(factValue, KeyValue)
            kValue.Values.Add(baseValue)
        End Sub

        Protected Overrides Function CreateBaseFactValue(id As String, baseValue As BaseValue) As BaseFactValue
            Return New KeyValue(GetDomain(id), 1, baseValue)
        End Function

        Friend Overrides Function CreateFact(id As String) As BaseFact
            Return New KeyFact(id)
        End Function

        Protected Overrides Iterator Function GetFactValues(factValue As BaseFactValue) As IEnumerable(Of BaseValue)
            Dim kv As KeyValue = DirectCast(factValue, KeyValue)
            For Each v In kv.Values
                Yield v
            Next
        End Function

        Protected Overrides Sub RemoveValueFromFactValues(factValue As BaseFactValue, compareValue As BaseValue)
            Dim kv As KeyValue = DirectCast(factValue, KeyValue)
            For Each v In kv.Values
                If (v.IsMatch(compareValue)) Then
                    kv.Values.Remove(v)
                    Exit For
                End If
            Next
        End Sub

        Protected Overrides Sub ReplaceValue(factValue As BaseFactValue, baseValue As BaseValue, index As Integer)
            Dim kValue As KeyValue = DirectCast(factValue, KeyValue)
            kValue.Values(index) = baseValue
        End Sub

        Protected Overrides Sub SetScore(fact As BaseFact, scoreToSet As Integer)
            Dim f As KeyFact = DirectCast(fact, KeyFact)
            f.Score = scoreToSet
        End Sub


        Public Overrides Function GetScoringMethod() As EnumScoringMethod
            Return _justInTimeFinding.GetEnsuredValue.Method
        End Function

        Public Overrides Sub SetScoringMethod(method As EnumScoringMethod)
            _justInTimeFinding.GetEnsuredValue.Method = method
        End Sub

        Protected Overloads Overrides Function GetPreProcessingMethods(baseFactValue As BaseFactValue) As IEnumerable(Of String)
            Dim ret As New List(Of String)
            If (baseFactValue IsNot Nothing) Then
                Dim kv As KeyValue = DirectCast(baseFactValue, KeyValue)

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
                Dim kv As KeyValue = DirectCast(baseFactValue, KeyValue)

                kv.PreProcessingRules = New SelectedPreprocessorCollection()

                Dim rulesToAdd = From p In preProcessing
                                 Select
                                 New SelectedPreprocessor() _
                                 With {.Rule = p}

                For Each ruleToAdd In rulesToAdd
                    If Not kv.PreProcessingRules.Any(Function(r) Not String.IsNullOrEmpty(r.Rule) AndAlso r.Rule = ruleToAdd.Rule) Then
                        kv.PreProcessingRules.Add(ruleToAdd)
                    End If
                Next

                Dim props = New Dictionary(Of String, String)
                props.Add("Selected preprocessing rules", String.Join(",", preProcessing))
                LogHelper.TrackEvent(EventsToTrack.PreprocessorUsage, props)
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

        Friend Overrides Function TryFindTargetForId(postFix As String) As IFindingManipulatorTarget

            Dim finding = _justInTimeFinding.CurrentValue

            If (finding Is Nothing) Then
                Return New FindingManipulatorTarget(JustInTimeFinding, Me, postFix)
            Else
                If (finding.KeyFactsets.Count = 0 OrElse (finding.Facts.Count > 0 AndAlso Not FactsetTarget.HasValue)) _
    Then
                    Return New FindingManipulatorTarget(JustInTimeFinding, Me, postFix)
                Else
                    Return New FactSetManipulatorTarget(Of KeyFinding, KeyFactSet)(_justInTimeFinding, Me, postFix)
                End If
            End If
        End Function

        Friend Overrides Function GetTargetForSetNumber(setNumber As Integer?, factIdPostFix As String) _
            As IFindingManipulatorTarget

            Dim finding = _justInTimeFinding.CurrentValue

            If (setNumber.HasValue) Then
                If (finding IsNot Nothing AndAlso finding.KeyFactsets.Any) AndAlso (Not (setNumber.Value < finding.KeyFactsets.Count)) Then _
                    Throw New IndexOutOfRangeException()
                Return _
                    New FactSetManipulatorTarget(Of KeyFinding, KeyFactSet)(_justInTimeFinding, Me, FactIdPostFix) _
                        With {.FactSetNumber = setNumber.Value}
            Else
                Return New FindingManipulatorTarget(JustInTimeFinding, Me, FactIdPostFix)
            End If
        End Function

        Public Overrides Function CreateNewFactSet() As Integer

            Dim factSetManipulator = New FactSetManipulatorTarget(Of KeyFinding, KeyFactSet)(_justInTimeFinding, Me,
                                                                                             FactIdPostFix)
            Return factSetManipulator.CreateFactSet()
        End Function

        Public Overrides Function GetFactSetNumbers(scoreKey As String) As IEnumerable(Of Integer)

            If _justInTimeFinding.CurrentValue.KeyFactsets.Any() Then

                Return _
                    New FactSetManipulatorTarget(Of KeyFinding, KeyFactSet)(_justInTimeFinding, Me, FactIdPostFix).
                        GetFactSetNumbers(scoreKey)
            End If

            Return GetTarget().GetFactSetNumbers(scoreKey)
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
