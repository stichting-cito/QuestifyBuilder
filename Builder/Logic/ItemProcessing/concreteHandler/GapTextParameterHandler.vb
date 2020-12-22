Imports Cito.Tester.ContentModel
Imports Versioning

Namespace ItemProcessing
    Friend Class GapChoiceParameterHandler
        Inherits DefaultParameterHandler

        Friend Overrides Sub Merge(ByVal newParam As ParameterBase, ByVal currentParam As ParameterBase, ByVal warnErr As WarningsAndErrors)
            MyBase.Merge(newParam, currentParam, warnErr)

            Dim newGtPrm As IGapChoice = DirectCast(newParam, IGapChoice)
            Dim currentGtPrm As IGapChoice = DirectCast(currentParam, IGapChoice)

            newGtPrm.MatchMax = currentGtPrm.MatchMax
        End Sub


        Friend Overrides Function Compare(ByVal newParam As ParameterBase, ByVal currentParam As ParameterBase) As IEnumerable(Of MetaDataCompareResult)
            Dim result = DirectCast(MyBase.Compare(newParam, currentParam), List(Of MetaDataCompareResult))

            Dim currentName As String = currentParam.Name
            Dim newGtPrm As IGapChoice = DirectCast(newParam, IGapChoice)
            Dim currentGtPrm As IGapChoice = DirectCast(currentParam, IGapChoice)

            Dim addResult = New MetaDataCompareResult(currentName, Nothing, currentGtPrm.MatchMax.ToString(), newGtPrm.MatchMax.ToString(), My.Resources.Category_AssessmentItem, Nothing)
            result.Add(addResult)

            Return result
        End Function
    End Class
End Namespace