Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Logic.Service.Model.Entities

Friend Class OpenChannelToTestResourceDtoServiceLazy
    Inherits OpenChannelToResourceDtoServiceLazy(Of AssessmentTestResourceDto, ITestResourceDtoRepository)
    Implements ITestResourceDtoRepository

    Public Function GetTestsByCodes(ByVal testCodeList As IEnumerable(Of String), ByVal bankId As Integer) As IEnumerable(Of AssessmentTestResourceDto) Implements ITestResourceDtoRepository.GetTestsByCodes
        Return SpecificDecoree.GetTestsByCodes(testCodeList, bankId)
    End Function
End Class