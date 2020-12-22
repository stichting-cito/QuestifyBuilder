Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.ComponentModel.Composition
Imports Questify.Builder.Logic.Service.Factories

<Export(GetType(IDataSourceEditorObjectFactory))>
Public Class DataSourceObjectFactory
    Implements IDataSourceEditorObjectFactory

    Public Function UpdateDataSourceResource(resource As DataSourceResourceEntity) As String Implements IDataSourceEditorObjectFactory.UpdateDataSourceResource
        Return ResourceFactory.Instance.UpdateDataSourceResource(resource)
    End Function

End Class
