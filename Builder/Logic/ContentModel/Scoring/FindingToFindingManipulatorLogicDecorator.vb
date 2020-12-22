Namespace ContentModel.Scoring
    Friend MustInherit Class FindingToFindingManipulatorLogicDecorator : Implements IFindingToFindingManipulatorLogic

        Private ReadOnly _decoree As IFindingToFindingManipulatorLogic

        Public Sub New(decoree As IFindingToFindingManipulatorLogic)
            If (decoree Is Nothing) Then
                Throw New ArgumentException(NameOf(decoree))
            End If

            _decoree = decoree
        End Sub

        Protected Overridable Function GetDestinationTarget(from As IFindingManipulator, sourceTarget As Integer, destination As IFindingManipulator) As Integer Implements IFindingToFindingManipulatorLogic.GetDestinationTarget
            Return _decoree.GetDestinationTarget(from, sourceTarget, destination)
        End Function

    End Class
End Namespace