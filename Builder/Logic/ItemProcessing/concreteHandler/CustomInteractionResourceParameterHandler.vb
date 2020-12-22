Imports Cito.Tester.ContentModel

Namespace ItemProcessing
    Friend Class CustomInteractionResourceParameterHandler
        Inherits ResourceParameterHandler

        Friend Overrides Sub Merge(ByVal newParam As ParameterBase, ByVal currentParam As ParameterBase, ByVal warnErr As WarningsAndErrors)
            MyBase.Merge(newParam, currentParam, warnErr)
            Dim newPrm As CustomInteractionResourceParameter = DirectCast(newParam, CustomInteractionResourceParameter)
            Dim curPrm As CustomInteractionResourceParameter = DirectCast(currentParam, CustomInteractionResourceParameter)

            If String.IsNullOrEmpty(curPrm.CommunicationType.ToString()) Then newPrm.CommunicationType = curPrm.CommunicationType
            If curPrm.Scorable Then newPrm.Scorable = curPrm.Scorable
        End Sub

    End Class

End Namespace