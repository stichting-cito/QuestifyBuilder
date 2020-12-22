Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.ComponentModel.Composition
Imports Questify.Builder.Logic.Service.Factories

<Export(GetType(IAspectEditorObjectFactory))>
Public Class AspectObjectFactory
    Implements IAspectEditorObjectFactory

    Public Function UpdateAspectResource(resource As AspectResourceEntity) As String Implements IAspectEditorObjectFactory.UpdateAspectResource
        Return ResourceFactory.Instance.UpdateAspectResource(resource)
    End Function

End Class
