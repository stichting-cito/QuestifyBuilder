Imports Cito.Tester.ContentModel
Imports System.Linq
Imports Questify.Builder.Logic.HelperClasses
Imports Versioning

Namespace ItemProcessing
    Friend Class ChoiceCollectionParameterHandler
        Inherits ParameterHandler

        Private ReadOnly _result As New List(Of MetaDataCompareResult)()

        Friend Overrides Sub Merge(newParam As ParameterBase, currentParam As ParameterBase, warnErr As WarningsAndErrors)
            DirectCast(newParam, ChoiceCollectionParameter).Choices.AddRange(DirectCast(currentParam, ChoiceCollectionParameter).Choices)
        End Sub

        Friend Overloads Overrides Function Compare(newParam As ParameterBase, currentParam As ParameterBase) As IEnumerable(Of MetaDataCompareResult)
            _result.AddRange(CompareChoices(currentParam, newParam))

            For Each metaDataCompareResult As MetaDataCompareResult In CompareChoices(newParam, currentParam)
                If _result.FirstOrDefault(Function(i) i.PropertyName = metaDataCompareResult.PropertyName) Is Nothing Then
                    _result.Add(metaDataCompareResult)
                End If
            Next

            Return _result
        End Function

        Private Function CompareChoices(newParam As ParameterBase, currentParam As ParameterBase) As IEnumerable(Of MetaDataCompareResult)
            Dim result As New List(Of MetaDataCompareResult)()

            If newParam Is Nothing Then
                result.Add(New MetaDataCompareResult(currentParam.Name, Nothing, ParameterHelper.GetValue(currentParam), String.Empty, My.Resources.Category_AssessmentItem, Nothing))
            ElseIf currentParam Is Nothing Then
                result.Add(New MetaDataCompareResult(newParam.Name, Nothing, String.Empty, ParameterHelper.GetValue(newParam), My.Resources.Category_AssessmentItem, Nothing))
            Else
                For Each currentChoice As SimpleChoice In CType(currentParam, ChoiceCollectionParameter).Choices
                    Dim newChoice = CType(newParam, ChoiceCollectionParameter).Choices.FirstOrDefault(Function(i) i.Identifier = currentChoice.Identifier AndAlso i.Value = currentChoice.Value)

                    If newChoice Is Nothing Then
                        result.Add(New MetaDataCompareResult(currentChoice.Identifier, Nothing, currentChoice.Value, String.Empty, My.Resources.Category_AssessmentItem, Nothing))
                    End If
                Next
            End If

            Return result
        End Function

    End Class
End Namespace

