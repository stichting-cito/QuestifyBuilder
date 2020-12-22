
Imports System.IO
Imports System.Linq
Imports System.Runtime.CompilerServices
Imports System.Text
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Enums
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Model.Entities
Imports Questify.Builder.Model.ContentModel.FactoryClasses

Namespace ContentModel

    Public Module ItemResourceExtension


        <Extension>
        Public Function CopyToNew(originalItemResource As ItemResourceEntity, newName As String, oldName As String) As ItemResourceEntity
            Dim worker As New ResourceEntityCopier()
            Return worker.GetCopy(Of ItemResourceEntity)(originalItemResource, newName, oldName)
        End Function


        <Extension>
        Public Function CopyToNew(originalItemResource As ItemResourceEntity, newName As String) As ItemResourceEntity
            Dim worker As New ResourceEntityCopier()
            Return worker.GetCopy(Of ItemResourceEntity)(originalItemResource, newName)
        End Function

        <Extension>
        Public Sub SetAssessmentItem(originalItemResource As ItemResourceEntity, assessmentItem As AssessmentItem)
            If (Not assessmentItem.Identifier.Equals(originalItemResource.Name)) Then
                assessmentItem.Identifier = originalItemResource.Name
            End If

            If (Not assessmentItem.Title.Equals(originalItemResource.Title)) Then
                assessmentItem.Title = originalItemResource.Title
            End If

            If originalItemResource.ItemId IsNot Nothing AndAlso Not String.IsNullOrEmpty(originalItemResource.ItemId) Then
                If (Not assessmentItem.ItemId.Equals(originalItemResource.ItemId)) Then
                    assessmentItem.ItemId = originalItemResource.ItemId
                End If
            End If

            If originalItemResource.IsNew Then
                originalItemResource.ResourceData = New ResourceDataEntity
            ElseIf originalItemResource.ResourceData Is Nothing Then
                originalItemResource.ResourceData = ResourceFactory.Instance.GetResourceData(originalItemResource)
                If originalItemResource.ResourceData Is Nothing Then
                    originalItemResource.ResourceData = New ResourceDataEntity
                End If
            End If

            Using stream = New MemoryStream()
                SerializeHelper.XmlSerializeToStream(stream, assessmentItem)
                originalItemResource.ResourceData.BinData = stream.ToArray()
                originalItemResource.ResourceData.FileExtension = ".xml"
            End Using
        End Sub


        <Extension>
        Public Function GetAssessmentItem(originalItemResource As ItemResourceEntity) As AssessmentItem
            Dim ret As AssessmentItem = Nothing
            If (originalItemResource.ResourceData Is Nothing) Then
                originalItemResource.ResourceData = ResourceFactory.Instance.GetResourceData(originalItemResource)
            End If
            If originalItemResource.ResourceData IsNot Nothing AndAlso originalItemResource.ResourceData.BinData.Length > 0 Then
                ret = DirectCast(SerializeHelper.XmlDeserializeFromByteArray(originalItemResource.ResourceData.BinData, GetType(AssessmentItem), True), AssessmentItem)
            End If
            Return ret
        End Function

        <Extension>
        Public Function GetAssessmentItem(originalItemResource As ItemResourceDto) As AssessmentItem
            Dim ret As AssessmentItem = Nothing
            Dim resourceData = ResourceFactory.Instance.GetResourceDataByResourceId(originalItemResource.ResourceId)
            If resourceData IsNot Nothing AndAlso resourceData.BinData.Length > 0 Then
                ret = DirectCast(SerializeHelper.XmlDeserializeFromByteArray(resourceData.BinData, GetType(AssessmentItem), True), AssessmentItem)
            End If
            Return ret
        End Function

        <Extension>
        Public Sub UpdateDependencies(itemResource As ItemResourceEntity)
            Dim assessment As AssessmentItem = itemResource.GetAssessmentItem()
            Dim resources As New HashSet(Of String)
            If assessment IsNot Nothing Then
                assessment.ReplaceInlineImages(itemResource)
                resources.UnionWith(assessment.GetResources())
            End If

            resources.UnionWith({itemResource.ItemLayoutTemplateUsedName})
            If itemResource.DependentResourceCollection IsNot Nothing Then
                For Each depResource In itemResource.DependentResourceCollection.ToList()
                    If depResource.DependentResource IsNot Nothing AndAlso (depResource.IsNew OrElse depResource.IsDirty) Then
                        depResource.DependentResourceId = depResource.DependentResource.ResourceId
                        depResource.DependentResource = Nothing
                    ElseIf depResource.DependentResource Is Nothing OrElse Not resources.Contains(depResource.DependentResource.Name) Then
                        itemResource.DependentResourceCollection.Remove(depResource)
                    End If
                Next

                itemResource.AddDependencies(resources)
            End If
        End Sub

        <Extension>
        Public Sub Save(itemResource As ItemResourceEntity)
            ResourceFactory.Instance.UpdateItemResource(itemResource)
        End Sub

        <Extension>
        Public Sub Save(itemResource As ItemResourceEntity, refetch As Boolean, recursive As Boolean)
            ResourceFactory.Instance.UpdateItemResource(itemResource, refetch, recursive)
        End Sub

        <Extension>
        Public Sub Save(itemResource As ItemResourceEntity, refetch As Boolean, recursive As Boolean, saveResourceData As Boolean)
            ResourceFactory.Instance.UpdateItemResource(itemResource, refetch, recursive, saveResourceData)
        End Sub

        <Extension>
        Public Sub Save(itemResources As IEnumerable(Of ItemResourceEntity))
            ResourceFactory.Instance.UpdateItemResources(itemResources)
        End Sub

        <Extension>
        Private Sub AddDependency(itemResource As ItemResourceEntity, resourceName As String)
            AddDependencies(itemResource, New List(Of String) From {resourceName})
        End Sub

        <Extension>
        Private Sub AddDependencies(itemResource As ItemResourceEntity, resources As IEnumerable(Of String))
            Dim resourcesToCheck = resources.Where(Function(r) Not String.IsNullOrEmpty(r) AndAlso itemResource.GetDependentResourceByName(r) Is Nothing).ToList()
            If resourcesToCheck IsNot Nothing AndAlso resourcesToCheck.Any() Then
                For Each res As ResourceEntity In ResourceFactory.Instance.GetResourcesByNamesWithOption(itemResource.BankId, resourcesToCheck, New ResourceRequestDTO())
                    If Not itemResource.DependentResourceCollection.Any(Function(dr) dr.DependentResourceId = res.ResourceId AndAlso dr.ResourceId = itemResource.ResourceId) Then
                        Dim depResource As DependentResourceEntity = itemResource.DependentResourceCollection.AddNew()
                        depResource.ResourceId = itemResource.ResourceId

                        depResource.DependentResourceId = res.ResourceId
                    End If
                Next
            End If
        End Sub

        <Extension>
        Public Sub UpdateCustomPropertiesDisplayValues(itemResource As ItemResourceEntity)
            itemResource.UpdateCustomPropertiesDisplayValues(Nothing)
        End Sub

        <Extension>
        Public Sub UpdateCustomPropertiesDisplayValues(itemResource As ItemResourceEntity, customBankProperties As List(Of CustomBankPropertyDto))
            If itemResource.CustomBankPropertyValueCollection.Count > 0 Then
                If customBankProperties Is Nothing Then
                    customBankProperties = DtoFactory.CustomBankProperty.GetCustomBankPropertiesForBranch(itemResource.BankId).ToList()
                End If

                For Each customPropertyValue In itemResource.CustomBankPropertyValueCollection
                    Dim customProperty = BankFactory.Instance.GetCustomBankProperty(customPropertyValue.CustomBankPropertyId)
                    customPropertyValue.SetCustomPropertyDisplayValue(customProperty, customBankProperties)
                Next
            End If
        End Sub

        <Extension>
        Public Function GetStylesFromDependentItemLayoutTemplate(itemResource As ItemResourceEntity, ByRef headerStyleElementContent As String, contextIdentifier As Integer?) As Dictionary(Of String, String)
            Dim ret As New Dictionary(Of String, String)
            Dim sbHeaderStyleElementContent As New StringBuilder()
            For Each dependentStylesheetResource In GetStylesheetResources(itemResource)
                Dim resourceDataEntity As ResourceDataEntity
                resourceDataEntity = ResourceFactory.Instance.GetResourceData(dependentStylesheetResource)

                If Not dependentStylesheetResource.Name.StartsWith("__") Then
                    ret.Add(dependentStylesheetResource.Name, New UTF8Encoding().GetString(resourceDataEntity.BinData))
                End If

                Dim stylesheetEditCounterpart As ResourceEntity = ResourceFactory.Instance.GetResourceByNameWithOption(itemResource.BankId, $"Edit{dependentStylesheetResource.Name}", New ResourceRequestDTO())
                If TypeOf stylesheetEditCounterpart Is GenericResourceEntity AndAlso
                    DirectCast(stylesheetEditCounterpart, GenericResourceEntity).MediaType = "text/css" Then
                    resourceDataEntity = ResourceFactory.Instance.GetResourceData(DirectCast(stylesheetEditCounterpart, GenericResourceEntity))
                    Dim editStylesheetContent = New UTF8Encoding().GetString(resourceDataEntity.BinData)
                    AddContextIdentifierToCssContent(editStylesheetContent, contextIdentifier)
                    sbHeaderStyleElementContent.Append(editStylesheetContent)
                End If
            Next

            headerStyleElementContent = sbHeaderStyleElementContent.ToString()

            Return ret
        End Function

        <Extension>
        Public Function GetStylesheetResources(itemResource As ItemResourceEntity) As IEnumerable(Of GenericResourceEntity)
            Dim sortedDictionary As New SortedDictionary(Of String, GenericResourceEntity)
            If itemResource Is Nothing Then
                Return sortedDictionary.Values
            End If

            Dim dependentResources = ResourceFactory.Instance.
    GetItemLayoutTemplatesFromListOfResourceIds(itemResource.DependentResourceCollection.
    Where(Function(d) d IsNot Nothing AndAlso (TypeOf d.DependentResource Is ItemLayoutTemplateResourceEntity OrElse d.DependentResource Is Nothing)).
    Select(Function(d) d.DependentResourceId), True)

            For Each resourceEntityWithDependencies As ResourceEntity In dependentResources
                Dim itmLayT = TryCast(resourceEntityWithDependencies, ItemLayoutTemplateResourceEntity)
                If itmLayT Is Nothing Then
                    Continue For
                Else
                    Dim currentType As ItemTypeEnum = Nothing
                    If Not String.IsNullOrEmpty(itmLayT.ItemType) Then
                        currentType = DirectCast([Enum].Parse(GetType(ItemTypeEnum), itmLayT.ItemType, True), ItemTypeEnum)
                    End If

                    If currentType = ItemTypeEnum.Inline Then
                        Continue For
                    End If

                    If (resourceEntityWithDependencies.DependentResourceCollection Is Nothing OrElse
                        resourceEntityWithDependencies.DependentResourceCollection.Count = 0 OrElse
                        resourceEntityWithDependencies.DependentResourceCollection.Where(Function(d) d.DependentResource Is Nothing).Any()) Then
                        resourceEntityWithDependencies = ResourceFactory.Instance.GetItemLayoutTemplate(DirectCast(resourceEntityWithDependencies, ItemLayoutTemplateResourceEntity))
                    End If
                End If

                Dim dependentResourcesFromIlt As List(Of ResourceEntity) =
                    resourceEntityWithDependencies.DependentResourceCollection.Select(Function(dr) dr.DependentResource).ToList()
                DetermineStylesheetResources(sortedDictionary, dependentResourcesFromIlt)

                Exit For
            Next

            Return sortedDictionary.Values
        End Function

        Private Sub DetermineStylesheetResources(sortedDictionary As SortedDictionary(Of String, GenericResourceEntity), dependentResources As List(Of ResourceEntity))

            DetermineStylesheetResourcesFromGenericResources(sortedDictionary, dependentResources)

            Dim resourcesFromDb = ResourceFactory.Instance.GetResourcesByIdsWithOption(dependentResources.Select(Function(dr) dr.ResourceId).ToList(), New GenericResourceEntityFactory(), New ResourceRequestDTO()).ToList()
            resourcesFromDb.ForEach(Sub(dr)
                                        If DetermineStylesheetResourceByMediaType(DirectCast(dr, ResourceEntity)) Then
                                            If Not sortedDictionary.ContainsKey(DirectCast(dr, GenericResourceEntity).Name) Then
                                                sortedDictionary.Add(DirectCast(dr, GenericResourceEntity).Name, DirectCast(dr, GenericResourceEntity))
                                            End If
                                        End If
                                    End Sub)
        End Sub

        Private Sub DetermineStylesheetResourcesFromGenericResources(sortedDictionary As SortedDictionary(Of String, GenericResourceEntity), dependentResources As List(Of ResourceEntity))
            Dim dependentResourcesToRemove As New List(Of ResourceEntity)
            dependentResources.ForEach(Sub(dr)
                                           If sortedDictionary.ContainsKey(dr.Name) Then
                                               dependentResourcesToRemove.Add(dr)
                                           ElseIf DetermineStylesheetResourceByMediaType(dr) Then
                                               sortedDictionary.Add(dr.Name, DirectCast(dr, GenericResourceEntity))
                                               dependentResourcesToRemove.Add(dr)
                                           ElseIf Not dr.GetType().ToString().Equals("Questify.Builder.Model.ContentModel.EntityClasses.ResourceEntity", StringComparison.InvariantCultureIgnoreCase) Then
                                               dependentResourcesToRemove.Add(dr)
                                           End If
                                       End Sub)

            dependentResourcesToRemove.ForEach(Sub(drtr)
                                                   dependentResources.Remove(drtr)
                                               End Sub)
        End Sub

        Private Function DetermineStylesheetResourceByMediaType(resource As ResourceEntity) As Boolean
            If TypeOf resource Is GenericResourceEntity AndAlso DirectCast(resource, GenericResourceEntity).MediaType = "text/css" Then
                Return True
            Else
                Return False
            End If
        End Function

        Private Sub AddContextIdentifierToCssContent(ByRef cssContent As String, contextIdentifier As Integer?)
            If contextIdentifier.HasValue Then
                cssContent = cssContent.Replace("resource://package/",
                                                $"resource://package:{contextIdentifier.ToString}/")
            End If
        End Sub

    End Module

End Namespace

