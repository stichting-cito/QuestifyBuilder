Imports System.Linq

Namespace ContentModel.Scoring

    Friend Class DefaultFindingToFindingManipulatorLogic : Implements IFindingToFindingManipulatorLogic


        Public Function GetDestinationTarget(from As IFindingManipulator, sourceTarget As Integer, destination As IFindingManipulator) As Integer Implements IFindingToFindingManipulatorLogic.GetDestinationTarget
            If (Not destination.SetNumbers.Contains(sourceTarget)) Then
                Dim result = destination.CreateNewFactSet()
                Debug.Assert(result = sourceTarget, "Should not occur. This code is created so that it will loop through each keyfactset.")
            End If
            Return sourceTarget
        End Function

    End Class
End Namespace