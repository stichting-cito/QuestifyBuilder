Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Cito.Tester.ContentModel.Datasources
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Logic.Service.Factories

Public NotInheritable Class Parsers


    Public Shared Function ParseItemDataSourceSettingsFromResourceEntity(ByVal dataSourceResourceEntity As EntityClasses.ResourceEntity) As Datasources.DataSourceSettings
        If dataSourceResourceEntity.ResourceData Is Nothing Then
            dataSourceResourceEntity.ResourceData = ResourceFactory.Instance.GetResourceData(dataSourceResourceEntity)
        End If

        Debug.Assert(dataSourceResourceEntity.ResourceData IsNot Nothing)

        With dataSourceResourceEntity.ResourceData
            Dim serializedData As String = System.Text.Encoding.UTF8.GetString(.BinData)
            If serializedData.StartsWith("<?xml") Then
                Try
                    Dim dataSourceSettingsToReturn As Datasources.DataSourceSettings

                    dataSourceSettingsToReturn = SerializeHelper.XmlDeserializeFromString(Of DataSourceSettings)(serializedData)

                    Debug.Assert(dataSourceSettingsToReturn IsNot Nothing)

                    dataSourceSettingsToReturn.Identifier = dataSourceResourceEntity.Name
                    dataSourceSettingsToReturn.Title = dataSourceResourceEntity.Title

                    Return dataSourceSettingsToReturn
                Catch ex As Exception
                End Try
            End If
        End With

        dataSourceResourceEntity.ResourceData = Nothing
        Return Nothing
    End Function

End Class