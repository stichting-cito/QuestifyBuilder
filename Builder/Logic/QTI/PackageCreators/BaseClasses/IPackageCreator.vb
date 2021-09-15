Imports Cito.Tester.ContentModel

Public Interface IPackageCreator
    Sub ResourceNeeded(ByVal sender As Object, ByVal e As ResourceNeededEventArgs)
    Function GetAssessmentTestViewType() As String
    Function GetAspectByCode(code As String) As Aspect
End Interface