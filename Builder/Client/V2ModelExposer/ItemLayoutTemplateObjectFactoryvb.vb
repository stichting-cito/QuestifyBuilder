Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.ComponentModel.Composition
Imports Questify.Builder.Logic.Service.Factories

<Export(GetType(IItemLayoutTemplateEditorObjectFactory))>
Public Class ItemLayoutTemplateObjectFactory
    Implements IItemLayoutTemplateEditorObjectFactory

    Public Function UpdateItemLayoutTemplateResource(resource As ItemLayoutTemplateResourceEntity) As String Implements IItemLayoutTemplateEditorObjectFactory.UpdateItemLayoutTemplateResource
        Return ResourceFactory.Instance.UpdateItemLayoutTemplateResource(resource)
    End Function

End Class
