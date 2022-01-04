Imports System.IO
Imports System.Runtime.CompilerServices
Imports Cito.Tester.Common
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.ContentModel.Datasources

Namespace ContentModel

    Public Module DataSourceExtension
        <Extension>
        Public Sub SetDataSource(dataSourceResource As DataSourceResourceEntity, dataSource As DataSourceSettings)
            If (Not dataSource.Identifier.Equals(dataSourceResource.Name)) Then dataSource.Identifier = dataSourceResource.Name
            If (Not dataSource.Title.Equals(dataSourceResource.Title)) Then dataSource.Title = dataSourceResource.Title
            If dataSourceResource.ResourceData Is Nothing Then
                dataSourceResource.ResourceData = New ResourceDataEntity
            End If
            Using stream = New MemoryStream()
                SerializeHelper.XmlSerializeToStream(stream, dataSource)
                dataSourceResource.ResourceData.BinData = stream.ToArray()
                dataSourceResource.ResourceData.FileExtension = ".xml"
            End Using
        End Sub


        <Extension>
        Public Function GetDatasource(datasourceResource As DataSourceResourceEntity) As DataSourceSettings
            Dim ret As DataSourceSettings = Nothing
            datasourceResource.EnsureResourceData
            If datasourceResource.ResourceData IsNot Nothing AndAlso datasourceResource.ResourceData.BinData.Length > 0 Then
                ret = DirectCast(Cito.Tester.Common.SerializeHelper.XmlDeserializeFromByteArray(datasourceResource.ResourceData.BinData, GetType(DataSourceSettings), True), DataSourceSettings)
            End If
            Return ret
        End Function

        <Extension>
        Public Function RenameItemCode(datasourceResource As DataSourceResourceEntity, currentItemCode As String, newItemCode As String) As Boolean
            Dim wrk As New ItemManipulationInDatasource(datasourceResource)
            Return wrk.Rename(currentItemCode, newItemCode)
        End Function

    End Module

End Namespace
