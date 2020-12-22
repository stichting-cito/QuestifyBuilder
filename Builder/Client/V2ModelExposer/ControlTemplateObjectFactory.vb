Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.ComponentModel.Composition
Imports Questify.Builder.Logic.Service.Factories

<Export(GetType(IControlTemplateEditorObjectFactory))>
Public Class ControlTemplateObjectFactory
    Implements IControlTemplateEditorObjectFactory

    Public Function UpdateControlTemplateResource(resource As ControlTemplateResourceEntity) As String Implements IControlTemplateEditorObjectFactory.UpdateControlTemplateResource
        Return ResourceFactory.Instance.UpdateControlTemplateResource(resource)
    End Function

End Class
