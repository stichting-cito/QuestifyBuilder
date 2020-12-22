Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.ComponentModel.Composition
Imports Questify.Builder.Logic.Service.Factories
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel

<Export(GetType(IAssessmentTestEditorObjectFactory))>
Public Class AssessmentObjectFactory
    Implements IAssessmentTestEditorObjectFactory

    Public Function UpdateAssessmentTestResource(resource As AssessmentTestResourceEntity) As String Implements IAssessmentTestEditorObjectFactory.UpdateAssessmentTestResource
        Return ResourceFactory.Instance.UpdateAssessmentTestResource(resource)
    End Function

    Public Function GetRequiredObjectsForAssessmentTestWithId(id As Guid) As Tuple(Of AssessmentTestResourceEntity, AssessmentTest2, Boolean, ResourceManagerBase, ActionEntity) Implements IAssessmentTestEditorObjectFactory.GetRequiredObjectsForAssessmentTestWithId
        Throw New NotImplementedException()
    End Function
End Class
