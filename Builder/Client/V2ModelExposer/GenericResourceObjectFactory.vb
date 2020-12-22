Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.ComponentModel.Composition
Imports Questify.Builder.Logic.Service.Factories

<Export(GetType(IGenericResourceEditorObjectFactory))>
Public Class GenericResourceObjectFactory
    Implements IGenericResourceEditorObjectFactory

    Public Function UpdateGenericResourceResource(resource As GenericResourceEntity) As String Implements IGenericResourceEditorObjectFactory.UpdateGenericResource
        Return ResourceFactory.Instance.UpdateGenericResource(resource)
    End Function

End Class
