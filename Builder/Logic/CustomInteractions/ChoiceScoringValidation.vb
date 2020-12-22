Imports System.Linq

Namespace CustomInteractions

    Class ChoiceScoringValidation : Implements IScoringValidation

        Private ReadOnly _choiceScoring As ChoiceScoring

        Public Sub New(choiceScoring As ChoiceScoring)
            _choiceScoring = choiceScoring
        End Sub




        Public Function IsValid(ByRef errorMessage As List(Of String)) As Boolean Implements IScoringValidation.IsValid

            Dim choiceIds = _choiceScoring.Choices.Select(Function(choice) choice.Id).ToList()

            MaxChoicesIsNotGreaterThanChoices(_choiceScoring, errorMessage)
            MinChoicesCanNotBeGreaterThanChoices(_choiceScoring, errorMessage)
            MinChoicesCanNotBeGreaterThanMaxChoices(_choiceScoring, errorMessage)
            HasNoDuplicates(errorMessage, choiceIds)
            CorrectResponseShouldBePresentInChoices(_choiceScoring, errorMessage, choiceIds)

            Return errorMessage.Count = 0
        End Function

        Private Sub CorrectResponseShouldBePresentInChoices(choiceScoring As ChoiceScoring, errorMessage As List(Of String), choiceIds As List(Of String))
            If Not String.IsNullOrEmpty(choiceScoring.CorrectResponse) Then
                For Each correctResponse In choiceScoring.CorrectResponse.ToCharArray.Where(Function(c) Not "#|()& ".ToCharArray.Contains(c))
                    If Not choiceIds.Contains(correctResponse) Then
                        errorMessage.Add(String.Format(My.Resources.CorrectResponseNotInChoiceCollection, correctResponse))
                    End If
                Next
            End If
        End Sub

        Private Sub HasNoDuplicates(errorMessage As List(Of String), choiceIds As List(Of String))
            Dim duplicates = choiceIds.GroupBy(Function(s) s).SelectMany(Function(grp) grp.Skip(1))
            If duplicates IsNot Nothing AndAlso duplicates.Count > 0 Then
                errorMessage.Add(String.Format(My.Resources.DuplicateKey, String.Join(",", duplicates.ToArray)))
            End If
        End Sub

        Private Sub MinChoicesCanNotBeGreaterThanMaxChoices(choiceScoring As ChoiceScoring, errorMessage As List(Of String))
            If choiceScoring.MinChoices > choiceScoring.MaxChoices Then
                errorMessage.Add(My.Resources.MinchoicesGreaterThanMaxChoices)
            End If
        End Sub

        Private Sub MinChoicesCanNotBeGreaterThanChoices(choiceScoring As ChoiceScoring, errorMessage As List(Of String))
            If choiceScoring.MinChoices > choiceScoring.Choices.Count Then
                errorMessage.Add(My.Resources.MinchoicesGreaterThanChoices)
            End If
        End Sub

        Private Sub MaxChoicesIsNotGreaterThanChoices(choiceScoring As ChoiceScoring, errorMessage As List(Of String))
            If choiceScoring.MaxChoices > choiceScoring.Choices.Count Then
                errorMessage.Add(My.Resources.MaxChoicesGreaterThanChoices)
            End If
        End Sub
    End Class
End Namespace