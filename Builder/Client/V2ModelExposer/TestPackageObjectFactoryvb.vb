Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.ComponentModel.Composition
Imports Questify.Builder.Logic.Service.Factories

<Export(GetType(ITestPackageEditorObjectFactory))>
Public Class TestPackageObjectFactory
    Implements ITestPackageEditorObjectFactory

    Public Function UpdateTestPackageResource(resource As TestPackageResourceEntity) As String Implements ITestPackageEditorObjectFactory.UpdateTestPackageResource
        Return ResourceFactory.Instance.UpdateTestPackageResource(resource)
    End Function

End Class
