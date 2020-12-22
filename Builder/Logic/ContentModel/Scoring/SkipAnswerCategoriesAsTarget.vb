Imports System.Linq

Namespace ContentModel.Scoring
    Friend Class SkipAnswerCategoriesAsTarget : Inherits FindingToFindingManipulatorLogicDecorator

        Public Sub New(decoree As IFindingToFindingManipulatorLogic)
            MyBase.New(decoree)
        End Sub

        Protected Overrides Function GetDestinationTarget(ByVal from As IFindingManipulator, ByVal sourceTarget As Integer, ByVal destination As IFindingManipulator) As Integer

            Dim newTarget = sourceTarget

            For Each target In destination.SetNumbers.ToList()

                If (target > newTarget) Then Exit For

                If (isAnswerCategorySet(target, destination)) Then newTarget += 1

            Next

            Return MyBase.GetDestinationTarget(from, newTarget, destination)
        End Function

        Private Function isAnswerCategorySet(target As Integer, destination As IFindingManipulator) As Boolean
            destination.SetFactSetTarget(target)
            Dim sampleId = destination.GetIds().FirstOrDefault()

            Return DefaultStringOperations.IsCatchAllOrAnswerCategoryFactId(sampleId)

        End Function

    End Class
End Namespace