Imports Cito.Tester.ContentModel

Namespace ItemProcessing
    Friend Class ResourceParameterHandler
        Inherits DefaultParameterHandler

        Friend Overrides Sub Merge(ByVal newParam As ParameterBase, ByVal currentParam As ParameterBase, ByVal warnErr As WarningsAndErrors)
            MyBase.Merge(newParam, currentParam, warnErr)
            Dim newGIP As ResourceParameter = DirectCast(newParam, ResourceParameter)
            Dim curGIP As ResourceParameter = DirectCast(currentParam, ResourceParameter)

            If curGIP.WidthSpecified Then newGIP.Width = curGIP.Width
            If curGIP.HeightSpecified Then newGIP.Height = curGIP.Height
            If curGIP.EditSize OrElse newGIP.EditSize Then newGIP.EditSize = curGIP.EditSize
        End Sub

    End Class
End Namespace