Imports System.IO
Imports System.Linq
Imports System.Runtime.CompilerServices
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Model.Entities
Imports SD.LLBLGen.Pro.ORMSupportClasses

Public Module DataTableConvertHelperExtensions

    <Extension()>
    Public Sub FillWithEntity(row As DataRow, resource As ResourceDto, customProperties As Dictionary(Of Guid, String))
        If (resource Is Nothing) Then
            Throw New ArgumentNullException(NameOf(resource))
        End If

        Dim excludedColumns As New List(Of String)({"StateId", "Id", "Bank id", "ItemAutoId", "ItemTypeFromItemLayoutTemplate",
                                                   "ItemLayoutTemplateUsed", "Template", "createdBy", "modifiedBy", "ReferencedResourceIds", "DependentResourceIds",
                                                  "IsSelectable", "CustomPropertyDisplayValues", "VisibleInPicker", "SetToInvisibleAtBankIds"})
        For Each field In resource.GetType.GetProperties
            If Not excludedColumns.Any(Function(e) e.Equals(field.Name, StringComparison.OrdinalIgnoreCase)) AndAlso field.GetValue(resource) IsNot Nothing Then
                Dim fieldValue = field.GetValue(resource).ToString
                Dim fieldName = field.Name
                Dim rm = My.Resources.ResourceManager
                rm.IgnoreCase = True
                Dim tranlation = rm.GetString(fieldName)
                If Not String.IsNullOrEmpty(tranlation) Then fieldName = fieldValue
                row.AddField(field.Name, fieldValue)
            End If
        Next
        If customProperties IsNot Nothing Then
            For Each cp In customProperties.Values
                row.AddColumnIfNotExist($"[{cp}]")
            Next
            For Each c In resource.CustomPropertyDisplayValues
                If customProperties.ContainsKey(c.CustomPropertyId) Then
                    Dim fieldName As String = $"[{customProperties(c.CustomPropertyId)}]"
                    Debug.Assert(row.Table.Columns.Contains(fieldName), "Custom property should exist!")
                    row.AddField(fieldName, c.DisplayValue)
                End If
            Next
        End If
    End Sub



    <Extension()>
    Public Sub FillWithParameterSet(row As DataRow, ByRef parameters As ParameterCollection, ByRef parameterSets As ParameterSetCollection, ByRef index As Integer)

        Dim parameterSet As ParameterCollection = parameterSets.GetParamCollectionByControlId(parameters.Id)

        For Each parameter As ParameterBase In parameterSet.InnerParameters
            Dim visible As String = parameter.DesignerSettings.GetSettingValueByKey("visible")

            Dim param = parameters.GetParameterByName(parameter.Name, False)
            If param Is Nothing
                Continue For
            End If

            If DirectCast(param, ParameterBase) IsNot Nothing Then
                visible = DirectCast(param, ParameterBase).DesignerSettings.GetSettingValueByKey("visible")
            End If

            If Not String.IsNullOrEmpty(visible) AndAlso String.Equals(visible, Boolean.FalseString, StringComparison.OrdinalIgnoreCase) Then
            ElseIf TypeOf parameter Is CollectionParameter Then
                row.FillWithCollectionParameter(DirectCast(parameter, CollectionParameter), DirectCast(parameters.GetParameterByName(parameter.Name), CollectionParameter), index)
            ElseIf TypeOf parameter Is ChoiceCollectionParameter Then

                For Each choice As SimpleChoice In DirectCast(parameter, ChoiceCollectionParameter).Choices
                    row.AddField(DataTableConvertHelper.GetLocalizedParameterLabel(parameter) & "-" & choice.Identifier, choice.ToString)
                Next
            Else
                If parameters.GetParameterByName(parameter.Name) IsNot Nothing Then
                    Dim columnName As String = DataTableConvertHelper.GetLocalizedParameterLabel(DirectCast(parameters.GetParameterByName(parameter.Name), ParameterBase))
                    If Not String.IsNullOrEmpty(columnName) Then
                        row.AddField(columnName, parameter.ToString())
                    End If
                End If
            End If
        Next
    End Sub


    <Extension()>
    Public Sub FillWithCollectionParameter(row As DataRow, parameterCol As CollectionParameter, designerSettingsCol As CollectionParameter, ByRef index As Integer)
        For Each parameterSet As ParameterCollection In parameterCol.Value

            For Each parameter As ParameterBase In parameterSet.InnerParameters

                If TypeOf parameter Is CollectionParameter Then
                    row.FillWithCollectionParameter(DirectCast(parameter, CollectionParameter), designerSettingsCol, index)
                Else
                    Dim columnName As String
                    Dim columnExtension As String

                    If Not String.Equals(designerSettingsCol.DesignerSettings.GetSettingValueByKey("visible"), Boolean.FalseString, StringComparison.OrdinalIgnoreCase) Then

                        If designerSettingsCol.BluePrint.GetParameterByName(parameter.Name, False) IsNot Nothing Then
                            If String.Equals(designerSettingsCol.DesignerSettings.GetSettingValueByKey("subsetidentifiers"), "alphabetic", StringComparison.OrdinalIgnoreCase) Then
                                columnExtension = " (" & ChrW(64 + index) & ")"
                            Else
                                columnExtension = " (" & index.ToString & ")"
                            End If
                            columnName = DataTableConvertHelper.GetLocalizedParameterLabel(DirectCast(designerSettingsCol.BluePrint.GetParameterByName(parameter.Name), ParameterBase))
                            If Not String.IsNullOrEmpty(columnName) Then
                                row.AddField(columnName & columnExtension, parameter.ToString())
                            End If
                        End If
                    End If
                End If
            Next

            index += 1
        Next
    End Sub

    <Extension()>
    Public Function GetAssessmentItem(entity As ResourceEntity) As AssessmentItem

        Dim data As ResourceDataEntity = ResourceFactory.Instance.GetResourceData(entity)

        Using resourceStream As MemoryStream = New MemoryStream(data.BinData)

            Return DirectCast(SerializeHelper.XmlDeserializeFromStream(resourceStream, GetType(AssessmentItem)), AssessmentItem)
        End Using
    End Function



    <Extension()>
    Public Function GetCustomPropertyValue(entity As ResourceEntity, customBankPropertyId As Guid) As CustomBankPropertyValueEntity

        Dim filter As IPredicate = (CustomBankPropertyValueFields.CustomBankPropertyId = customBankPropertyId)
        Dim indexes As List(Of Integer) = entity.CustomBankPropertyValueCollection.FindMatches(filter)

        Return If(indexes.Count = 1, entity.CustomBankPropertyValueCollection(indexes.First()), Nothing)
    End Function

    <Extension()>
    Public Sub AddField(row As DataRow, fieldName As String, value As Object)
        Dim columnName As String = My.Resources.ResourceManager.GetResourceStringByName(fieldName)

        If row.AddColumnIfNotExist(columnName) IsNot Nothing Then
            row.Table.Columns(columnName).ExtendedProperties("Identifier") = fieldName
        End If

        row.Item(columnName) = value
    End Sub

    <Extension()>
    Public Sub AddField(row As DataRow, fieldName As String, value As Object, index As Integer)
        Dim columnName As String = My.Resources.ResourceManager.GetResourceStringByName(fieldName)

        If row.AddColumnIfNotExist(columnName, index) IsNot Nothing Then
            row.Table.Columns(columnName).ExtendedProperties("Identifier") = fieldName
        End If

        row.Item(columnName) = value
    End Sub

    <Extension()>
    Public Function AddColumnIfNotExist(row As DataRow, columnName As String) As DataColumn
        If columnName Is Nothing Then
            Throw New ArgumentNullException(NameOf(columnName))
        End If
        Return row.AddColumnIfNotExist(columnName, If(columnName.Equals("code", StringComparison.OrdinalIgnoreCase), 0, -1))
    End Function

    <Extension()>
    Public Function AddColumnIfNotExist(row As DataRow, columnName As String, index As Integer) As DataColumn
        If columnName Is Nothing Then
            Throw New ArgumentNullException(NameOf(columnName))
        End If
        If row Is Nothing Then
            Throw New ArgumentNullException(NameOf(row))
        End If
        Dim column As DataColumn = Nothing

        If Not row.Table.Columns.Contains(columnName) Then
            column = row.Table.Columns.Add(columnName)

            If index >= 0 Then
                Try
                    row.Table.Columns(columnName).SetOrdinal(index)
                Catch ex As ArgumentException
                End Try

            End If
        End If

        Return column
    End Function


    <Extension()>
    Public Function GetAspectsCSV(itemSolution As Solution) As String
        Dim csvArray As New List(Of String)

        itemSolution.AspectReferenceSetCollection.ForEach(Sub(x) csvArray.AddRange(x.Items.Select(Function(y) y.SourceName).ToArray()))

        Return String.Join(",", csvArray.ToArray())
    End Function

    <Extension()>
    Public Function GetTest(testEntity As AssessmentTestResourceEntity) As AssessmentTest2
        If testEntity Is Nothing Then
            Throw New ArgumentNullException(NameOf(testEntity))
        End If

        Dim data As ResourceDataEntity = ResourceFactory.Instance.GetResourceData(testEntity)
        Dim result As ReturnedAssessmentTestModelInfo = AssessmentTestv2Factory.ReturnAssessmentTestv2ModelFromByteArray(data.BinData, True)

        testEntity.ResourceData = data

        Return result.AssessmentTestv2
    End Function

    <Extension()>
    Public Sub AddToDictionaries(testSection As TestSection2, ByRef sectionName As String, ByRef testPartName As String, ByRef sectionDictionary As Dictionary(Of String, String),
                             testpartDictionary As Dictionary(Of String, String), ByRef itemCollection As List(Of ItemReference2))
        If Not String.IsNullOrEmpty(sectionName) Then
            sectionName = String.Concat(sectionName, "-->")
        End If

        sectionName = String.Concat(sectionName, testSection.Title)

        For Each testcomponent As TestComponent2 In testSection.Components
            If TypeOf testcomponent Is ItemReference2 Then
                Dim itemRefercence As ItemReference2 = DirectCast(testcomponent, ItemReference2)

                sectionDictionary.Add(itemRefercence.SourceName, sectionName)
                testpartDictionary.Add(itemRefercence.SourceName, testPartName)
                itemCollection.Add(itemRefercence)
            ElseIf TypeOf testcomponent Is TestSection2 Then
                DirectCast(testcomponent, TestSection2).AddToDictionaries(sectionName, testPartName, sectionDictionary, testpartDictionary, itemCollection)
            End If
        Next
    End Sub

    <Extension()>
    Public Function GetResourceStringByName(resourceManager As Resources.ResourceManager, resourceName As String) As String
        Dim returnValue As String = String.Empty

        Try
            returnValue = resourceManager.GetString(resourceName)
        Catch ex As Exception
            returnValue = Nothing
        End Try

        Return If(returnValue Is Nothing, resourceName, returnValue)
    End Function

End Module
