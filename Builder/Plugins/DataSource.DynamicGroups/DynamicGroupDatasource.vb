Imports Cito.Tester.Common
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.ResourceManager
Imports Cito.Tester.ContentModel.Datasources
Imports Questify.Builder.Model.ContentModel.HelperClasses

Public Class DynamicGroupDatasource
    Inherits Cito.Tester.ContentModel.Datasources.ItemDataSource


    Private _resourceManager As DataBaseResourceManager



    Public Sub New(ByVal settings As Cito.Tester.ContentModel.Datasources.ItemDataSourceConfig)
        MyBase.New(settings)
    End Sub



    Public Overrides Function [Get](ByVal resourceManager As ResourceManagerBase) As System.Collections.Generic.IEnumerable(Of ResourceRef)
        If TypeOf resourceManager Is DataBaseResourceManager Then
            _resourceManager = DirectCast(resourceManager, DataBaseResourceManager)
        Else
            Throw New NotSupportedException("This editor only works in a TestBuilder environment.")
        End If

        Dim queryVisitor As New LLBLGENItemQueryVisitor(CType(Me.Config, DynamicGroupDatasourceConfig).Query)
        Dim queryResult As EntityCollection = queryVisitor.Execute(_resourceManager.BankId)

        Dim result As New List(Of ResourceRef)

        For Each item As ItemResourceEntity In queryResult
            result.Add(New ResourceRef(item.Name))
        Next

        Return result
    End Function


End Class