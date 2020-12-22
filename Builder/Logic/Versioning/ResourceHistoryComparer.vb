Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Cito.Tester.ContentModel.Datasources
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Versioning

Public Class ResourceHistoryComparer


    Public Shared Function CompareResourceHistoryEntities(ByVal resourceHistoryEntity1 As ResourceHistoryEntity, ByVal resourceHistoryEntity2 As ResourceHistoryEntity, ByVal type As Type, ByVal resourceManager As ResourceManagerBase) As IEnumerable(Of MetaDataCompareResult)
        Dim comparedMetaData As New List(Of MetaDataCompareResult)

        If resourceHistoryEntity1.MetaData IsNot Nothing AndAlso resourceHistoryEntity2.MetaData IsNot Nothing AndAlso resourceHistoryEntity1.MetaData.Length > 0 AndAlso resourceHistoryEntity2.MetaData.Length > 0 Then
            Dim metaData1 As Versioning.MetaData = DeserializeMetaData(resourceHistoryEntity1.MetaData)
            Dim metaData2 As Versioning.MetaData = DeserializeMetaData(resourceHistoryEntity2.MetaData)

            comparedMetaData.AddRange(CompareMetaData(metaData1, metaData2))
        End If

        If resourceHistoryEntity1.BinData IsNot Nothing AndAlso resourceHistoryEntity2.BinData IsNot Nothing AndAlso resourceHistoryEntity1.BinData.Length > 0 AndAlso resourceHistoryEntity2.BinData.Length > 0 Then
            comparedMetaData.AddRange(CompareBinData(resourceHistoryEntity1.BinData, resourceHistoryEntity2.BinData, type, resourceManager))
        End If

        Return comparedMetaData
    End Function


    Private Shared Function DeserializeMetaData(ByVal metaData As Byte()) As Versioning.MetaData
        Return New XmlSerializerMetaDataDeserializer(Nothing).DeserializeMetaData(metaData)
    End Function


    Private Shared Function CompareMetaData(ByVal metaData1 As Versioning.MetaData, ByVal metaData2 As Versioning.MetaData) As IEnumerable(Of MetaDataCompareResult)
        Dim result As New List(Of MetaDataCompareResult)

        result.AddRange(New PropertyEntityComparer().Compare(metaData1.PropertyEntityMetaData, metaData2.PropertyEntityMetaData))
        result.AddRange(New CustomPropertiesComparer().Compare(metaData1.CustomPropertiesMetaData, metaData2.CustomPropertiesMetaData))
        result.AddRange(New DependentResourceComparer().Compare(metaData1.DependentResourcesMetaData, metaData2.DependentResourcesMetaData))
        result.AddRange(New ConceptStructuresComparer().Compare(metaData1.ConceptStructureMetaData, metaData2.ConceptStructureMetaData))
        result.AddRange(New TreeStructuresComparer().Compare(metaData1.TreeStructureMetaData, metaData2.TreeStructureMetaData))

        Return result
    End Function


    Private Shared Function CompareBinData(ByVal binData1 As Byte(), ByVal binData2 As Byte(), ByVal resourceEntityType As Type, ByVal resourceManager As ResourceManagerBase) As IEnumerable(Of MetaDataCompareResult)
        If resourceEntityType Is GetType(ItemResourceEntity) Then
            Dim deserializedObject1 As AssessmentItem = CType(SerializeHelper.XmlDeserializeFromByteArray(binData1, GetType(AssessmentItem)), AssessmentItem)
            Dim deserializedObject2 As AssessmentItem = CType(SerializeHelper.XmlDeserializeFromByteArray(binData2, GetType(AssessmentItem)), AssessmentItem)

            Return New AssessmentItemComparer(resourceManager).Compare(deserializedObject1, deserializedObject2)
        ElseIf resourceEntityType Is GetType(AssessmentTestResourceEntity) Then
            Dim deserializedObject1 As AssessmentTest2 = CType(SerializeHelper.XmlDeserializeFromByteArray(binData1, GetType(AssessmentTest2)), AssessmentTest2)
            Dim deserializedObject2 As AssessmentTest2 = CType(SerializeHelper.XmlDeserializeFromByteArray(binData2, GetType(AssessmentTest2)), AssessmentTest2)

            Return New AssessmentTest2Comparer().Compare(deserializedObject1, deserializedObject2)
        ElseIf resourceEntityType Is GetType(TestPackageResourceEntity) Then
            Dim deserializedObject1 As TestPackage = CType(SerializeHelper.XmlDeserializeFromByteArray(binData1, GetType(TestPackage)), TestPackage)
            Dim deserializedObject2 As TestPackage = CType(SerializeHelper.XmlDeserializeFromByteArray(binData2, GetType(TestPackage)), TestPackage)

            Return New TestPackageComparer().Compare(deserializedObject1, deserializedObject2)
        ElseIf resourceEntityType Is GetType(AspectResourceEntity) Then
            Dim deserializedObject1 As Aspect = CType(SerializeHelper.XmlDeserializeFromByteArray(binData1, GetType(Aspect)), Aspect)
            Dim deserializedObject2 As Aspect = CType(SerializeHelper.XmlDeserializeFromByteArray(binData2, GetType(Aspect)), Aspect)

            Return New AspectComparer().Compare(deserializedObject1, deserializedObject2)
        ElseIf resourceEntityType Is GetType(DataSourceResourceEntity) Then
            Dim deserializedObject1 As DataSourceSettings = CType(SerializeHelper.XmlDeserializeFromByteArray(binData1, GetType(DataSourceSettings)), DataSourceSettings)
            Dim deserializedObject2 As DataSourceSettings = CType(SerializeHelper.XmlDeserializeFromByteArray(binData2, GetType(DataSourceSettings)), DataSourceSettings)

            Return New DataSourceComparer().Compare(deserializedObject1, deserializedObject2)
        ElseIf resourceEntityType Is GetType(ItemLayoutTemplateResourceEntity) OrElse resourceEntityType Is GetType(ControlTemplateResourceEntity) Then
            Dim deserializedObject1 As String = System.Text.Encoding.Default.GetString(binData1)
            Dim deserializedObject2 As String = System.Text.Encoding.Default.GetString(binData2)

            Return New SourceComparer().Compare(deserializedObject1, deserializedObject2)
        Else
            Return New List(Of MetaDataCompareResult)()
        End If
    End Function


End Class
