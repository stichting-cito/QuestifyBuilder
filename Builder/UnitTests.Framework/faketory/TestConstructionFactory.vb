Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.TestConstruction.Requests

Namespace Faketory

    Public Module TestConstructionFactory

        Public Function Add(ByVal ParamArray id() As String) As TestConstructionRequest
            Return New TestConstructionRequest(TestConstructionRequest.RequestTypeEnum.Add,
                                               ResourceRefFactory.MakeMultiple(id),
                                               New Datasources.ResourceRef() {})
        End Function


        Public Function Add(ByVal toAddid As IEnumerable(Of String), ByVal alreadyInAssesment As IEnumerable(Of String)) As TestConstructionRequest
            Return New TestConstructionRequest(TestConstructionRequest.RequestTypeEnum.Add,
                                               ResourceRefFactory.MakeMultiple(toAddid.ToArray()),
                                               ResourceRefFactory.MakeMultiple(alreadyInAssesment.ToArray()))
        End Function


        Public Function Remove(ByVal ParamArray id() As String) As TestConstructionRequest
            Return New TestConstructionRequest(TestConstructionRequest.RequestTypeEnum.Add,
                                               ResourceRefFactory.MakeMultiple(id),
                                               New Datasources.ResourceRef() {})
        End Function

    End Module
End NameSpace