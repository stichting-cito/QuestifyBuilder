Imports System.Collections.Generic
Imports Cito.Tester.Common
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.EntityClasses.Workers

    Friend Class MetaDataGeneratorForEntity

        ReadOnly _resourceEntity As ResourceEntity
        ReadOnly _entityName As String

        Public Sub New(ByVal resourceEntity As ResourceEntity)
            _resourceEntity = resourceEntity
            Dim entityCore = CType(_resourceEntity, IEntityCore)
            _entityName = entityCore.LLBLGenProEntityName
        End Sub

        Function GetEntitySpecificMetadata() As IEnumerable(Of MetaData)
            Dim ret = New List(Of MetaData)()


            For Each field As IEntityField2 In _resourceEntity.Fields

                If Not IsDirectlyDefinedOnThisType(field) Then Continue For
                If Not HasValue(field) Then Continue For
                If isResourceId(field) Then Continue For

                Dim metaDataName As String = field.Name
                Dim metaDataValue As String = String.Empty

                Select Case field.DataType.ToString
                    Case GetType(String).ToString
                        metaDataValue = CType(field.CurrentValue, String)
                    Case GetType(Boolean).ToString, GetType(Integer).ToString, GetType(Decimal).ToString, GetType(Nullable(Of System.Int32)).ToString(), GetType(Nullable(Of System.Decimal)).ToString()
                        metaDataValue = CType(field.CurrentValue, IConvertible).ToString(System.Globalization.CultureInfo.InvariantCulture)
                    Case Else
                        Throw New NotSupportedException($"while adding meta data, type '{field.DataType.ToString}' not supported")
                End Select

                ret.Add(New MetaData(metaDataName, metaDataValue, MetaData.enumMetaDataType.BankMetaData))

            Next

            Return ret
        End Function

        Private Function isResourceId(ByVal entityField2 As IEntityField2) As Boolean
            Return String.Equals(entityField2.Name, "resourceId", StringComparison.CurrentCultureIgnoreCase)
        End Function

        Private Function IsDirectlyDefinedOnThisType(ByVal entityField2 As IEntityField2) As Boolean
            Return entityField2.ContainingObjectName.Equals(_entityName)
        End Function

        Private Function HasValue(field As IEntityField2) As Boolean
            Return field.CurrentValue IsNot Nothing
        End Function

        Public Function DefaultMetadata() As IEnumerable(Of MetaData)
            Dim ret = New List(Of MetaData)()
            ret.Add(New MetaData("OriginalResourceId", _resourceEntity.ResourceId.ToString, MetaData.enumMetaDataType.BankMetaData))

            If Not String.IsNullOrEmpty(_resourceEntity.Title) Then
                ret.Add(New MetaData("Title", _resourceEntity.Title, MetaData.enumMetaDataType.BankMetaData))
            End If

            If Not String.IsNullOrEmpty(_resourceEntity.Description) Then
                ret.Add(New MetaData("Description", _resourceEntity.Description, MetaData.enumMetaDataType.BankMetaData))
            End If

            If Not IsDirectlyDefinedOnThisType(_resourceEntity.Fields("Version")) AndAlso Not String.IsNullOrEmpty(_resourceEntity.Version) Then
                ret.Add(New MetaData("Version", _resourceEntity.Version.ToString, MetaData.enumMetaDataType.BankMetaData))
            End If
            Return ret
        End Function

    End Class

End Namespace