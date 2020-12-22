Imports Cito.Tester.ContentModel

Namespace ItemProcessing
    Friend Class GapImageParameterHandler
        Inherits GapChoiceParameterHandler

        Friend Overrides Sub Merge(ByVal newParam As ParameterBase, ByVal currentParam As ParameterBase, ByVal warnErr As WarningsAndErrors)
            MyBase.Merge(newParam, currentParam, warnErr)
            Dim newGIP As GapImageParameter = DirectCast(newParam, GapImageParameter)
            Dim curGIP As GapImageParameter = DirectCast(currentParam, GapImageParameter)

            newGIP.ContentType = curGIP.ContentType
            newGIP.EnteredText = curGIP.EnteredText
            newGIP.Width = curGIP.Width
            newGIP.Height = curGIP.Height
        End Sub


    End Class
End Namespace