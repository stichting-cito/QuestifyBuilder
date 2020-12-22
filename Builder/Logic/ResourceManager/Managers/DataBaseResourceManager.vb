Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Security.Cryptography
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Enums
Imports Questify.Builder.Logic.Service.Factories
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace ResourceManager

    Public Class DataBaseResourceManager
        Inherits ResourceManagerBase
        Implements IDisposable

        Protected CachedResourceEntity As ResourceEntity

        Private _bankId As Integer
        Private _bankCustomProperties As EntityCollection
        Private _disposedValue As Boolean
        Private ReadOnly _retrieveCustomBankProperties As Boolean = False

        Private _includeMetaData As MetaDataType = MetaDataType.None

        Private Shared _overrides As Dictionary(Of String, String)



        Public Property BankId As Integer
            Get
                Return _bankId
            End Get
            Set(value As Integer)
                Dim shouldRetrieveCustomBankProperties As Boolean = (_retrieveCustomBankProperties AndAlso (value <> _bankId))

                _bankId = value

                If shouldRetrieveCustomBankProperties Then
                    _bankCustomProperties = BankFactory.Instance.GetCustomBankPropertiesForBranchById(_bankId, ResourceTypeEnum.AllResources)
                End If
            End Set
        End Property


        Public ReadOnly Property BankCustomProperties As EntityCollection
            Get
                Return _bankCustomProperties
            End Get
        End Property

        Public Property IncludeMetaData As MetaDataType
            Get
                Return _includeMetaData
            End Get
            Set(value As MetaDataType)
                _includeMetaData = value
            End Set
        End Property



        Public Event CopyToLocalCacheProgress As EventHandler(Of EventArgs)

        Protected Overridable Sub OnCopyToLocalCacheProgress(e As EventArgs)
            RaiseEvent CopyToLocalCacheProgress(Me, e)
        End Sub




        Public Overrides Function GetResource(name As String, processingMethod As ResourceProcessingFunction, request As ResourceRequestDTO) As BinaryResource
            Return GetResourceAndProcessStream(name, processingMethod, Nothing, request)
        End Function

        <Obsolete>
        Public Overrides Function GetResource(name As String) As StreamResource
            Dim request As ResourceRequestDTO = New ResourceRequestDTO() With {.WithCustomProperties = True, .WithDependencies = True}
            Return GetResource(name, request)
        End Function


        Public Overrides Function GetResource(name As String, request As ResourceRequestDTO) As StreamResource
            Dim resource As ResourceEntity

            If CachedResourceEntity IsNot Nothing AndAlso CachedResourceEntity.Name = name Then
                resource = CachedResourceEntity
            Else
                resource = ResourceFactory.Instance.GetResourceByNameWithOption(_bankId, name, request)
                CachedResourceEntity = resource
            End If

            If resource IsNot Nothing Then
                Dim dependentResources As New DependentResourceCollection
                If request.WithDependencies Then
                    For Each dResource As DependentResourceEntity In resource.DependentResourceCollection
                        dependentResources.Add(New DependentResource(dResource.DependentResource.Name))
                    Next
                End If

                Dim resourceData As ResourceDataEntity
                resourceData = ResourceFactory.Instance.GetResourceData(resource)

                Dim sResource As StreamResource = resourceData.GetStream()
                Dim sResourceToReturn As StreamResource
                If Not String.IsNullOrEmpty(resource.OriginalVersion) Then
                    sResourceToReturn = New StreamResource(name, ResourceVersionConverter.ConvertVersion(resource.Version), resource.GetType().Name, False, sResource.ResourceObject, dependentResources, resource.OriginalName,
                                                           ResourceVersionConverter.ConvertVersion(resource.OriginalVersion), resource.StateId.GetValueOrDefault())
                Else
                    sResourceToReturn = New StreamResource(name, ResourceVersionConverter.ConvertVersion(resource.Version), resource.GetType().Name, False, sResource.ResourceObject, dependentResources, resource.StateId.GetValueOrDefault())
                End If
                sResourceToReturn.MetaData.Clear()

                resource.AddMetaDataTo(sResourceToReturn)

                If request.WithCustomProperties AndAlso (IncludeMetaData = MetaDataType.All OrElse IncludeMetaData = MetaDataType.Publishable) Then
                    AddCustomProperties(resource, sResourceToReturn, IncludeMetaData)
                End If

                Return sResourceToReturn
            Else
                Return Nothing
            End If
        End Function

        Public Overrides Function GetGenericResourceMimeType(name As String) As String
            Dim genericResource = ResourceFactory.Instance.GetResourceByNameWithOption(BankId, name, New ResourceRequestDTO())
            If genericResource IsNot Nothing AndAlso TypeOf genericResource Is GenericResourceEntity Then
                Dim mediaType = DirectCast(genericResource, GenericResourceEntity).MediaType.ToLower
                If mediaType.Contains("image/") Then
                    Return "image"
                ElseIf mediaType.Contains("video/") Then
                    Return "video"
                ElseIf mediaType.Contains("audio/") Then
                    Return "audio"
                End If
            End If
            Return String.Empty
        End Function

        Public Overrides Function GetResourceMetaData(name As String) As MetaDataCollection
            Dim returnValue As New MetaDataCollection()
            Dim request = New ResourceRequestDTO() With {.WithCustomProperties = (IncludeMetaData = MetaDataType.All OrElse IncludeMetaData = MetaDataType.Publishable)}
            Using sResource As StreamResource = GetResource(name, request)

                If Not sResource Is Nothing Then
                    returnValue.AddRange(sResource.MetaData)

                End If

            End Using
            Return returnValue
        End Function

        Public Overrides Function GetResourcesOfType(type As String) As ResourceEntryCollection
            Throw New NotImplementedException()
        End Function

        Public Overrides Function GetDependentResourcesForResource(name As String) As DependentResourceCollection
            Return GetDependentResourcesForResource(name, False)
        End Function

        Public Overrides Function GetDependentResourcesForResource(name As String, getCopies As Boolean) As DependentResourceCollection
            Dim request = New ResourceRequestDTO With {.WithDependencies = True, .WithCustomProperties = False}
            Dim resource As ResourceEntity = ResourceFactory.Instance.GetResourceByNameWithOption(_bankId, name, request)

            Dim dependentResources As New DependentResourceCollection
            If resource IsNot Nothing Then
                For Each dResource As DependentResourceEntity In resource.DependentResourceCollection
                    dependentResources.Add(New DependentResource(dResource.DependentResource.Name))
                Next
            End If
            Return dependentResources
        End Function

        Public Overrides Function GetResourceEntry(name As String) As ResourceEntry
            Dim returnValue As ResourceEntry = Nothing

            If name Is Nothing Then
                Throw New ArgumentNullException("name")
            End If
            Dim request = New ResourceRequestDTO With {.WithDependencies = True, .WithCustomProperties = False}
            Dim resource As StreamResource = GetResource(name, request)
            If (resource IsNot Nothing) Then
                returnValue = New ResourceEntry(name, resource.Version, GetResourceUri(name).ToString(), resource.Type, False, resource.DependentResources, resource.OriginalName, resource.OriginalVersion, resource.State)

                resource.Dispose()
            End If

            Return returnValue
        End Function

        Public Overrides Function GetTypedResource(name As String, usingType As Type, request As ResourceRequestDTO) As BinaryResource
            Return GetResourceAndProcessStream(name, New ResourceProcessingFunction(AddressOf StreamConverters.ConvertStreamToTypedInstance), usingType, request)
        End Function

        Public Sub HandleResourceNeeded(ByRef e As ResourceNeededEventArgs, request As ResourceRequestDTO)
            If TypeOf e Is ResourceNeededEventArgs Then
                If (e.Command And ResourceNeededCommand.Resource) = ResourceNeededCommand.Resource Then
                    If e.TypedResourceType IsNot Nothing Then
                        e.BinaryResource = GetTypedResource(e.ResourceName, e.TypedResourceType, request)
                    Else
                        e.BinaryResource = GetResource(e.ResourceName, e.StreamProcessingDelegate, request)
                    End If
                Else
                    e.BinaryResource = New BinaryResource(New Object)
                End If

                If (e.Command And ResourceNeededCommand.MetaData) = ResourceNeededCommand.MetaData Then
                    IncludeMetaData = MetaDataType.Publishable
                    Dim fetchedMetaData As MetaDataCollection = GetResourceMetaData(e.ResourceName)

                    e.Metadata.Clear()
                    e.Metadata.AddRange(fetchedMetaData)
                End If
            End If
        End Sub



        Private ReadOnly _resourceMappingCollection As New Dictionary(Of String, Guid)(StringComparer.InvariantCultureIgnoreCase)

        Public Overrides Sub UpdateResource(resource As StreamResource)
            UpdateResource(resource, True)
        End Sub

        Public Overrides Sub UpdateResource(resource As StreamResource, refetch As Boolean)
            PutStream(BankId, True, resource, _resourceMappingCollection, refetch)
        End Sub

        Public Overrides Sub PutResource(ByVal resource As StreamResource)
            PutResource(resource, True)
        End Sub

        Public Overrides Sub PutResource(ByVal resource As StreamResource, refetch As Boolean)
            If ResourceFactory.Instance.ResourceExists(BankId, resource.Name, True) Then
                Dim resourceEntity = ResourceFactory.Instance.GetResourceByNameWithOption(BankId, resource.Name, New ResourceRequestDTO())
                Dim resourceBankId As Integer
                If resourceEntity Is Nothing Then
                    resourceBankId = 0
                Else
                    resourceBankId = resourceEntity.BankId
                End If
                Throw New DuplicateResourceException("Failed to import Resource, probably due to duplicate resource names", resourceBankId, BankId)
            End If

            Me.PutStream(BankId, False, resource, _resourceMappingCollection, refetch)
        End Sub




        Private Function GetResourceUri(name As String) As Uri
            Return New Uri(
    $"db://{ _
                  name.Replace(" ", "").Replace(".....", "_").Replace("....", "_").Replace("...", "_").
                  Replace("..", "_")}")
        End Function


        Private Function GetResourceAndProcessStream(
                                                    name As String,
                                                    processingMethod As ResourceProcessingFunction,
                                                    usingType As Type,
                                                    request As ResourceRequestDTO) As BinaryResource
            Dim resource As ResourceEntity

            If CachedResourceEntity IsNot Nothing AndAlso CachedResourceEntity.Name = name Then
                resource = CachedResourceEntity
            Else
                resource = ResourceFactory.Instance.GetResourceByNameWithOption(_bankId, name, request)
                CachedResourceEntity = resource
            End If

            If resource IsNot Nothing Then
                Dim resourceData As ResourceDataEntity
                resourceData = ResourceFactory.Instance.GetResourceData(resource)

                Dim resourceObject As Object
                Dim resourceStream As StreamResource = Nothing

                Try
                    resourceStream = resourceData.GetStream()
                    resourceObject = processingMethod?.Invoke(name, resourceStream, usingType)

                Catch ex As Exception
                    Throw

                Finally
                    If resourceStream IsNot Nothing Then
                        resourceStream.CloseStream()
                    End If
                End Try

                Return New BinaryResource(name, Nothing, resourceObject, Nothing)
            Else
                Return Nothing
            End If

        End Function



        Private Function PutStream(bankIdParam As Integer, isUpdate As Boolean, stream As StreamResource, dependancyMapping As Dictionary(Of String, Guid), refetch As Boolean) As Boolean
            Dim currentResourceEntity As ResourceEntity = Nothing
            Dim request = New ResourceRequestDTO With {.WithCustomProperties = True, .WithDependencies = True}
            If isUpdate Then
                currentResourceEntity = ResourceFactory.Instance.GetResourceByNameWithOption(bankIdParam, stream.Name, request)
                currentResourceEntity.IsDirty = True
                If currentResourceEntity.BankId <> bankIdParam Then
                    bankIdParam = currentResourceEntity.BankId
                End If
            End If

            Select Case stream.Type
                Case GetType(TestPackageResourceEntity).Name
                    Dim testPackageResourceEntity = SetResourceEntityForPutStream(Of TestPackageResourceEntity)(bankIdParam, stream, dependancyMapping, currentResourceEntity)
                    Return String.IsNullOrEmpty(ResourceFactory.Instance.UpdateTestPackageResource(testPackageResourceEntity, refetch, True))

                Case GetType(AssessmentTestResourceEntity).Name
                    Dim assessmentTestResource = SetResourceEntityForPutStream(Of AssessmentTestResourceEntity)(bankIdParam, stream, dependancyMapping, currentResourceEntity)
                    Return String.IsNullOrEmpty(ResourceFactory.Instance.UpdateAssessmentTestResource(assessmentTestResource, refetch, True))

                Case GetType(ControlTemplateResourceEntity).Name
                    Dim controlTemplateResource = SetResourceEntityForPutStream(Of ControlTemplateResourceEntity)(bankIdParam, stream, dependancyMapping, currentResourceEntity)
                    Return String.IsNullOrEmpty(ResourceFactory.Instance.UpdateControlTemplateResource(controlTemplateResource, refetch, True))

                Case GetType(ItemLayoutTemplateResourceEntity).Name
                    Dim itemLayoutTemplateResource = SetResourceEntityForPutStream(Of ItemLayoutTemplateResourceEntity)(bankIdParam, stream, dependancyMapping, currentResourceEntity)
                    Return String.IsNullOrEmpty(ResourceFactory.Instance.UpdateItemLayoutTemplateResource(itemLayoutTemplateResource, refetch, True))

                Case GetType(ItemResourceEntity).Name
                    Dim itemResource = SetResourceEntityForPutStream(Of ItemResourceEntity)(bankIdParam, stream, dependancyMapping, currentResourceEntity)
                    Return String.IsNullOrEmpty(ResourceFactory.Instance.UpdateItemResource(itemResource, refetch, True, True))

                Case GetType(AspectResourceEntity).Name
                    Dim aspectResource = SetResourceEntityForPutStream(Of AspectResourceEntity)(bankIdParam, stream, dependancyMapping, currentResourceEntity)
                    Return String.IsNullOrEmpty(ResourceFactory.Instance.UpdateAspectResource(aspectResource, refetch, True))

                Case GetType(DataSourceResourceEntity).Name
                    Dim datasourceResource = SetResourceEntityForPutStream(Of DataSourceResourceEntity)(bankIdParam, stream, dependancyMapping, currentResourceEntity)
                    Return String.IsNullOrEmpty(ResourceFactory.Instance.UpdateDataSourceResource(datasourceResource, refetch, True))

                Case GetType(PackageResourceEntity).Name
                    Dim packageResource = SetResourceEntityForPutStream(Of PackageResourceEntity)(bankIdParam, stream, dependancyMapping, currentResourceEntity)
                    Return String.IsNullOrEmpty(ResourceFactory.Instance.UpdatePackageResource(packageResource))

                Case Else
                    Dim genericResource = SetResourceEntityForPutStream(Of GenericResourceEntity)(bankIdParam, stream, dependancyMapping, currentResourceEntity)
                    Return String.IsNullOrEmpty(ResourceFactory.Instance.UpdateGenericResource(genericResource, refetch, True))
            End Select

        End Function

        Private Function SetResourceEntityForPutStream(Of T As {New, ResourceEntity})(bankIdParam As Integer, stream As StreamResource, dependancyMapping As Dictionary(Of String, Guid), currentResource As ResourceEntity) As T
            Dim resource As T

            If currentResource IsNot Nothing Then
                resource = DirectCast(currentResource, T)
            Else
                resource = New T
            End If

            SetBankMetaData(stream, resource)
            SetSharedResourceProperties(bankIdParam, stream, resource, dependancyMapping)
            SetCustomBankPropertyValues(stream, resource)

            Return resource
        End Function

        Private Shared Sub SetBankMetaData(stream As StreamResource, resource As ResourceEntity)
            For Each metaData As MetaData In stream.MetaData.Where(Function(m) m.MetaDatatype = MetaData.enumMetaDataType.BankMetaData)
                Dim fld As IEntityField2 = resource.Fields(metaData.Name)

                If fld IsNot Nothing AndAlso Not fld.IsReadOnly AndAlso String.Compare(fld.ContainingObjectName, "ResourceEntity") <> 0 Then
                    Dim metaDataValue As String = metaData.Value

                    Select Case fld.DataType.ToString
                        Case GetType(String).ToString
                            fld.CurrentValue = metaDataValue
                        Case GetType(Boolean).ToString
                            fld.CurrentValue = Boolean.Parse(metaDataValue)
                        Case GetType(Integer).ToString
                            fld.CurrentValue = Integer.Parse(metaDataValue)
                        Case GetType(Nullable(Of Integer)).ToString
                            If CType(metaDataValue, Nullable(Of Integer)).HasValue Then
                                fld.CurrentValue = Integer.Parse(metaDataValue)
                            Else
                                fld.CurrentValue = 0
                            End If
                        Case GetType(Decimal).ToString
                            fld.CurrentValue = Decimal.Parse(metaDataValue, CultureInfo.InvariantCulture)
                        Case GetType(Nullable(Of Decimal)).ToString
                            Dim decimalValue As Decimal = 0
                            Decimal.TryParse(metaDataValue, NumberStyles.Any, CultureInfo.InvariantCulture, decimalValue)
                            fld.CurrentValue = decimalValue
                        Case Else
                            Throw New NotSupportedException(
                                $"DataBaseResourceManager while setting bank meta data, type '{fld.DataType.ToString _
                                                               }' not supported")
                    End Select
                End If
            Next
        End Sub

        Private Shared Sub SetSharedResourceProperties(bankId As Integer, stream As StreamResource, resource As ResourceEntity, dependencyMapping As Dictionary(Of String, Guid))
            With resource
                If .IsNew Then
                    .ResourceId = Guid.NewGuid
                End If
                .Version = ResourceVersionConverter.ConvertBackVersion(stream.Version)
                .BankId = bankId
                .Name = stream.Name

                .OriginalVersion = ResourceVersionConverter.ConvertBackVersion(stream.OriginalVersion)
                .OriginalName = stream.OriginalName
                .StateId = CType(IIf(stream.State = 0, Nothing, stream.State), Integer?)

                .Title = GetMetaData("Title", stream.MetaData, Path.GetFileNameWithoutExtension(stream.Name))
                .Description = GetMetaData("Description", stream.MetaData, "")

                If .IsNew Then
                    .ResourceData = New ResourceDataEntity()
                Else
                    .ResourceData = ResourceFactory.Instance.GetResourceData(resource)
                    If .ResourceData Is Nothing Then
                        .ResourceData = New ResourceDataEntity()
                    End If

                    .DependentResourceCollection.ToList().ForEach(Sub(d)
                                                                      .RemovedDependentEntities.Add(d)
                                                                  End Sub)
                    .DependentResourceCollection.Clear()
                End If

                If stream.Type = GetType(ItemResourceEntity).Name Then
                    .ResourceData.FileExtension = ".xml"
                ElseIf stream.Type = GetType(GenericResourceEntity).Name AndAlso DirectCast(resource, GenericResourceEntity).MediaType = "text/plain" Then
                    .ResourceData.FileExtension = ".xml"
                End If

                Dim resData(CType(stream.Length, Integer) - 1) As Byte
                Using resDataStream As New MemoryStream(resData, True)
                    stream.ResourceObject.CopyTo(resDataStream)

                    If stream.ResourceObject.GetType() Is GetType(CryptoStream) Then
                        If resDataStream.Position <> resData.Length Then
                            ReDim Preserve resData(CInt(resDataStream.Position - 1))
                        End If
                    End If

                    .ResourceData.BinData = resData
                    resData = Nothing
                End Using

                For Each depResource As ResourceEntity In ResourceFactory.Instance.GetResourcesByNamesWithOption(bankId, stream.DependentResources.Where(Function(dr) Not dependencyMapping.ContainsKey(CType(dr, DependentResource).Name.Trim)).Select(Function(dr) CType(dr, DependentResource).Name.Trim).ToList(), New ResourceRequestDTO())
                    If depResource IsNot Nothing Then
                        dependencyMapping.Add(depResource.Name.Trim, depResource.ResourceId)
                    Else
                        Throw New InvalidOperationException(String.Format(My.Resources.Error_DependentResourceNotFound, depResource.Name))
                    End If
                Next

                For Each dres As DependentResource In stream.DependentResources
                    Dim dresEntity As DependentResourceEntity = .DependentResourceCollection.AddNew()

                    If dependencyMapping.ContainsKey(dres.Name.Trim) Then
                        dresEntity.DependentResourceId = dependencyMapping.Item(dres.Name.Trim)
                    End If
                Next

                If Not dependencyMapping.ContainsKey(.Name) Then
                    dependencyMapping.Add(.Name, .ResourceId)
                End If
            End With
        End Sub

        Private Sub SetCustomBankPropertyValues(stream As StreamResource, resource As ResourceEntity)

            If Not resource.IsNew Then
                resource.CustomBankPropertyValueCollection.RemovedEntitiesTracker = New EntityCollection()
                resource.CustomBankPropertyValueCollection.ToList().ForEach(Sub(c)
                                                                                resource.CustomBankPropertyValueCollection.RemovedEntitiesTracker.Add(c)
                                                                            End Sub)
                resource.CustomBankPropertyValueCollection.Clear()
            End If

            For Each md As MetaData In stream.MetaData.Where(Function(m) m.MetaDatatype = MetaData.enumMetaDataType.BankCustomProperty AndAlso BankCustomProperties.FindMatches(CustomBankPropertyFields.Name = m.Name).Count = 1)
                Dim customBankProperty = DirectCast(BankCustomProperties(BankCustomProperties.FindMatches(CustomBankPropertyFields.Name = md.Name)(0)), CustomBankPropertyEntity)

                If TypeOf md Is MetaDataRichText AndAlso TypeOf customBankProperty Is RichTextValueCustomBankPropertyEntity Then
                    Dim fvValue As New RichTextValueCustomBankPropertyValueEntity(resource.ResourceId, customBankProperty.CustomBankPropertyId)
                    With fvValue
                        .Value = md.Value.ToString()
                    End With

                    resource.CustomBankPropertyValueCollection.Add(fvValue)

                ElseIf TypeOf md Is MetaData AndAlso TypeOf customBankProperty Is FreeValueCustomBankPropertyEntity Then
                    Dim fvValue As New FreeValueCustomBankPropertyValueEntity(resource.ResourceId, customBankProperty.CustomBankPropertyId)
                    With fvValue
                        .Value = md.Value.ToString()
                    End With

                    resource.CustomBankPropertyValueCollection.Add(fvValue)

                ElseIf TypeOf md Is MetaDataMultiValue AndAlso TypeOf customBankProperty Is ListCustomBankPropertyEntity Then
                    Dim mv = DirectCast(customBankProperty, ListCustomBankPropertyEntity)

                    Dim mvValue As New ListCustomBankPropertyValueEntity(resource.ResourceId, mv.CustomBankPropertyId)

                    For Each limd As MetaData In DirectCast(md, MetaDataMultiValue).ListValues.Where(Function(lv) lv.IsSelected)
                        Dim selectedValueResult As List(Of Integer)
                        selectedValueResult = mv.ListValueCustomBankPropertyCollection.FindMatches(ListValueCustomBankPropertyFields.Name = limd.Name)

                        If selectedValueResult.Count = 1 Then
                            Dim selectedValue As New ListCustomBankPropertySelectedValueEntity(resource.ResourceId, mv.CustomBankPropertyId, mv.ListValueCustomBankPropertyCollection(selectedValueResult(0)).ListValueBankCustomPropertyId)
                            mvValue.ListCustomBankPropertySelectedValueCollection.Add(selectedValue)
                        End If
                    Next

                    resource.CustomBankPropertyValueCollection.Add(mvValue)

                ElseIf TypeOf md Is MetaDataConceptStructure AndAlso TypeOf customBankProperty Is ConceptStructureCustomBankPropertyEntity Then
                    Dim cs = DirectCast(customBankProperty, ConceptStructureCustomBankPropertyEntity)
                    Dim csValue As New ConceptStructureCustomBankPropertyValueEntity With {.ConceptStructureCustomBankProperty = cs}

                    For Each selectedConceptStructurePart In DirectCast(md, MetaDataConceptStructure).StructureParts.Where(Function(sp) cs.ConceptStructurePartCustomBankPropertyCollection.FindMatches(ConceptStructurePartCustomBankPropertyFields.Name = sp.Name).Count = 1)
                        Dim referencedConceptStructurePart As New ConceptStructureCustomBankPropertySelectedPartEntity With {.ConceptStructurePartCustomBankProperty = cs.ConceptStructurePartCustomBankPropertyCollection(cs.ConceptStructurePartCustomBankPropertyCollection.FindMatches(ConceptStructurePartCustomBankPropertyFields.Name = selectedConceptStructurePart.Name)(0))}
                        csValue.ConceptStructureCustomBankPropertySelectedPartCollection.Add(referencedConceptStructurePart)
                    Next

                    resource.CustomBankPropertyValueCollection.Add(csValue)

                ElseIf TypeOf md Is MetaDataTreeStructure AndAlso TypeOf customBankProperty Is TreeStructureCustomBankPropertyEntity Then
                    Dim cs = DirectCast(customBankProperty, TreeStructureCustomBankPropertyEntity)
                    Dim csValue As New TreeStructureCustomBankPropertyValueEntity With {.TreeStructureCustomBankProperty = cs}

                    For Each selectedTreeStructurePart In DirectCast(md, MetaDataTreeStructure).StructureParts.Where(Function(sp) cs.TreeStructurePartCustomBankPropertyCollection.FindMatches(TreeStructurePartCustomBankPropertyFields.Name = sp.Name).Count() = 1)
                        Dim referencedTreeStructurePart As New TreeStructureCustomBankPropertySelectedPartEntity With {.TreeStructurePartCustomBankProperty = cs.TreeStructurePartCustomBankPropertyCollection(cs.TreeStructurePartCustomBankPropertyCollection.FindMatches(TreeStructurePartCustomBankPropertyFields.Name = selectedTreeStructurePart.Name)(0))}
                        csValue.TreeStructureCustomBankPropertySelectedPartCollection.Add(referencedTreeStructurePart)
                    Next

                    resource.CustomBankPropertyValueCollection.Add(csValue)
                End If
            Next
        End Sub

        Private Shared Function GetMetaData(metaName As String, metaDataCollection As MetaDataCollection, whenNullReturn As String) As String
            Dim metaData As MetaData = metaDataCollection.Item(metaName, MetaData.enumMetaDataType.BankMetaData)

            If metaData IsNot Nothing Then
                Return metaData.Value
            Else
                Return whenNullReturn
            End If
        End Function

        Protected Sub AddCustomProperties(resource As ResourceEntity, sResourceToReturn As StreamResource, IncludeMetaData As MetaDataType)
            If resource.CustomBankPropertyValueCollection IsNot Nothing AndAlso resource.CustomBankPropertyValueCollection.Count > 0 Then
                If (IncludeMetaData = MetaDataType.All) OrElse (IncludeMetaData = MetaDataType.Publishable AndAlso resource.CustomBankPropertyValueCollection.Any(Function(c) c.CustomBankProperty.Publishable)) Then
                    With resource.CustomBankPropertyValueCollection
                        Dim toCheck = .Items
                        If IncludeMetaData = MetaDataType.Publishable Then
                            toCheck = .Items.Where(Function(c) c.CustomBankProperty.Publishable).ToList()
                        End If
                        For Each propValue As CustomBankPropertyValueEntity In toCheck
                            Select Case propValue.GetType.ToString
                                Case GetType(FreeValueCustomBankPropertyValueEntity).ToString
                                    Dim fv = CType(propValue, FreeValueCustomBankPropertyValueEntity)
                                    Dim freeValueCustomProperty = CType(fv.CustomBankProperty, FreeValueCustomBankPropertyEntity)
                                    Dim metadata As MetaData = freeValueCustomProperty.ToResourceManagerSharedMetaData()

                                    metadata.Value = fv.Value

                                    sResourceToReturn.MetaData.Add(metadata)

                                Case GetType(ListCustomBankPropertyValueEntity).ToString
                                    Dim listValue = CType(propValue, ListCustomBankPropertyValueEntity)
                                    Dim listCustomProperty As ListCustomBankPropertyEntity = listValue.ListCustomBankProperty
                                    Dim metaData = DirectCast(listCustomProperty.ToResourceManagerSharedMetaData(), MetaDataMultiValue)

                                    For Each value As ListValueCustomBankPropertyEntity In listValue.ListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue
                                        Dim mdSelectedValue As MetaData = metaData.ListValues.Item(value.Name, metaData.enumMetaDataType.BankCustomProperty)
                                        If mdSelectedValue IsNot Nothing Then
                                            mdSelectedValue.IsSelected = True
                                        Else
                                            Throw New Exception(
                                                $"Inconsistent data during conversion of CustomBankProperty [{ _
                                                                   listCustomProperty.Name}]")
                                        End If
                                    Next

                                    sResourceToReturn.MetaData.Add(metaData)

                                Case GetType(ConceptStructureCustomBankPropertyValueEntity).ToString()
                                    Dim conceptStructureValue = CType(propValue, ConceptStructureCustomBankPropertyValueEntity)
                                    Dim conceptStructureCustomProperty As ConceptStructureCustomBankPropertyEntity = conceptStructureValue.ConceptStructureCustomBankProperty
                                    Dim metaData = DirectCast(conceptStructureCustomProperty.ToResourceManagerSharedMetaData(), MetaDataConceptStructure)

                                    metaData.StructureParts.Clear()
                                    For Each value As ConceptStructurePartCustomBankPropertyEntity In conceptStructureValue.ConceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart
                                        metaData.StructureParts.Add(New MetaDataConceptStructurePart() With {.Name = value.Name, .MetaDatatype = metaData.enumMetaDataType.BankCustomProperty})
                                    Next

                                    sResourceToReturn.MetaData.Add(metaData)

                                Case GetType(TreeStructureCustomBankPropertyValueEntity).ToString()
                                    Dim treeStructureValue = CType(propValue, TreeStructureCustomBankPropertyValueEntity)
                                    Dim treeStructureCustomProperty As TreeStructureCustomBankPropertyEntity = treeStructureValue.TreeStructureCustomBankProperty
                                    Dim metaData = DirectCast(treeStructureCustomProperty.ToResourceManagerSharedMetaData(), MetaDataTreeStructure)

                                    metaData.StructureParts.Clear()
                                    For Each value As TreeStructurePartCustomBankPropertyEntity In treeStructureValue.TreeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart
                                        metaData.StructureParts.Add(New MetaDataTreeStructurePart() With {.Name = value.Name, .MetaDatatype = metaData.enumMetaDataType.BankCustomProperty})
                                    Next

                                    sResourceToReturn.MetaData.Add(metaData)

                                Case GetType(RichTextValueCustomBankPropertyValueEntity).ToString()
                                    Dim richTextValue = CType(propValue, RichTextValueCustomBankPropertyValueEntity)
                                    Dim richTextCustomProperty As RichTextValueCustomBankPropertyEntity = CType(richTextValue.CustomBankProperty, RichTextValueCustomBankPropertyEntity)
                                    Dim metaData = DirectCast(richTextCustomProperty.ToResourceManagerSharedMetaData(), MetaDataRichText)

                                    metaData.Value = richTextValue.Value

                                    sResourceToReturn.MetaData.Add(metaData)

                                Case Else
                                    Throw New NotImplementedException(
                                        $"Unable to add custom property to StreamResource, type [{ _
                                                                         propValue.GetType.ToString}] is not known")
                            End Select
                        Next
                    End With
                End If
            End If
        End Sub



        Public Sub New(bankId As Integer, Optional retrieveCustomBankProperties As Boolean = False)
            _retrieveCustomBankProperties = retrieveCustomBankProperties
            Me.BankId = bankId
        End Sub



        Protected Overrides Sub Finalize()
            Dispose(False)
        End Sub



        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not _disposedValue Then
                If disposing Then
                    If _bankCustomProperties IsNot Nothing Then
                        _bankCustomProperties.Clear()
                    End If

                    If _overrides IsNot Nothing Then
                        _overrides.Clear()
                    End If

                    CachedResourceEntity = Nothing
                End If

            End If
            _disposedValue = True
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub


    End Class

End Namespace