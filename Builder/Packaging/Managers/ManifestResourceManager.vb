Imports System.Diagnostics.CodeAnalysis
Imports System.IO
Imports Cito.Tester.Common
Imports Cito.Tester.Common.Logging
Imports Cito.Tester.ContentModel
Imports Microsoft.VisualBasic.FileIO

Public Class ManifestResourceManager
    Inherits ResourceManagerBase
    Implements IDisposable


    Private _packager As PackageManager
    Private _manifest As ResourceManifest
    Private _manifestMetaData As ResourceMetaDataManifest

    Private _baseUri As Uri

    Private _sessionId As String

    Private _resourceEntriesCache As New ResourceEntriesInCacheCollection
    Private _cacheLocation As String = TempStorageHelper.GetTempStoragePath
    Private _cacheTestLocation As String

    Private _processStreamLock As New Object



    Public ReadOnly Property Manifest() As ResourceManifest
        Get
            Return _manifest
        End Get
    End Property

    Public Property ManifestMetaData() As ResourceMetaDataManifest
        Get
            Return _manifestMetaData
        End Get
        Set(value As ResourceMetaDataManifest)
            _manifestMetaData = value
        End Set
    End Property


    Public Property SessionId() As String
        Get
            Return _sessionId
        End Get
        Set(value As String)
            _sessionId = value
        End Set
    End Property

    Public ReadOnly Property CacheTestLocation() As String
        Get
            Return _cacheTestLocation
        End Get
    End Property

    Public ReadOnly Property CacheLocation() As String
        Get
            Return _cacheLocation
        End Get
    End Property


    Public Event CopyToLocalCacheProgress As EventHandler(Of EventArgs)

    Protected Overridable Sub OnCopyToLocalCacheProgress(e As EventArgs)
        RaiseEvent CopyToLocalCacheProgress(Me, e)
    End Sub



    Private disposedValue As Boolean

    Protected Overridable Overloads Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ClearCache()

                If _resourceEntriesCache IsNot Nothing Then
                    _resourceEntriesCache.Clear()
                    _resourceEntriesCache = Nothing
                End If

                If _packager IsNot Nothing Then
                    _packager.Dispose()
                    _packager = Nothing
                End If

            End If
            Me.disposedValue = True
        End If
    End Sub

    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub



    Public Sub New(manifest As ResourceManifest, baseUri As Uri, sessionId As String)
        Me.New(manifest, New ResourceMetaDataManifest, Nothing, baseUri, sessionId)
    End Sub

    Public Sub New(manifest As ResourceManifest, customBankPropertyMetaDataCollection As MetaDataCollection, baseUri As Uri, sessionId As String)
        Me.New(manifest, New ResourceMetaDataManifest, customBankPropertyMetaDataCollection, baseUri, sessionId)
    End Sub

    Public Sub New(manifest As ResourceManifest, manifestMetadata As ResourceMetaDataManifest, customBankPropertyMetaDataCollection As MetaDataCollection, baseUri As Uri, sessionId As String)
        _manifest = manifest
        _manifestMetaData = manifestMetadata
        _baseUri = baseUri
        _sessionId = sessionId

        If _manifestMetaData IsNot Nothing Then
            _manifestMetaData.CustomPropertyDefinitions.AddSharedMetaDataCollection(customBankPropertyMetaDataCollection)
        End If

        If Not String.IsNullOrEmpty(_sessionId) Then
            _cacheTestLocation = Path.Combine(_cacheLocation, sessionId.ToString)
        End If
        If _manifest IsNot Nothing Then _manifest.Validate(baseUri)
    End Sub



    Public Overrides Function GetResource(name As String, processingMethod As ResourceProcessingFunction, request as ResourceRequestDTO) As BinaryResource
        Dim resource As BinaryResource = Nothing
        Log.TraceInformation(TraceCategory.ResourceManager, My.Resources.Trace_ManifestResourceManager_GetResource_GetResource, name)
        Log.Indent()
        Dim resourceEntry As ResourceEntry = _resourceEntriesCache.Item(name)

        If resourceEntry IsNot Nothing Then
            Log.TraceInformation(TraceCategory.ResourceManager, My.Resources.Trace_ManifestResourceManager_GetResource_FoundInCache, name)
            resource = GetFromCache(resourceEntry, processingMethod, GetType(Object))
        Else
            Log.TraceInformation(TraceCategory.ResourceManager, My.Resources.Trace_ManifestResourceManager_GetResource_NotFoundInCache, name)
            resource = GetResourceAndProcessStream(name, processingMethod, GetType(Object))
        End If

        Log.Unindent()

        Return resource
    End Function

    <Obsolete("Please use GetResource(name, withDependencies, withCustomProperties")>
    Public Overrides Function GetResource(name As String) As StreamResource
        Dim request = new ResourceRequestDTO With {.WithDependencies = True, .WithCustomProperties = True}
        Return GetResource(name, request)
    End Function

    Public Overrides Function GetResource(name As String, request As ResourceRequestDTO) As StreamResource
        Dim entry As ResourceEntry = Manifest.Resources.Item(name)
        Dim resourceUri As Uri
        Dim returnedResource As StreamResource = Nothing

        If entry IsNot Nothing Then
            Dim streamResource As StreamResource = Nothing
            resourceUri = MakeAbsoluteResourceUri(entry)
            streamResource = GetStream(resourceUri)

            If streamResource Is Nothing Then
                Throw New ResourceException(String.Format(My.Resources.Error_ManifestResourceManager_EmptyStream, name))
            End If

            returnedResource = New StreamResource(name, entry.Version, entry.Type, entry.CacheLocal, streamResource.ResourceObject, entry.DependentResources, entry.OriginalName, entry.OriginalVersion, entry.State)
            With returnedResource
                .Length = streamResource.Length
                .MetaData.AddRange(Me.GetResourceMetaData(name))
            End With

        End If
        Return returnedResource
    End Function

    Public Overrides Function GetResourceMetaData(name As String) As MetaDataCollection
        Dim metaDataCollectionToReturn As New MetaDataCollection

        If Me.ManifestMetaData IsNot Nothing AndAlso Me.ManifestMetaData.ResourceReferences.Item(name) IsNot Nothing Then

            For Each resourceElement As ResourceManifestMetadataElement In Me.ManifestMetaData.ResourceReferences.Item(name).MetaData
                Select Case resourceElement.MetaDataType
                    Case ResourceManifestMetadataDefinitionBase.enumMetaDataType.BankMetaData
                        Dim newBankMetaData As New MetaData

                        With newBankMetaData
                            .Name = resourceElement.Name
                            .Title = resourceElement.Title
                            .MetaDatatype = MetaData.enumMetaDataType.BankMetaData

                            If resourceElement.Values.Count = 1 Then
                                .Value = resourceElement.Values(0).Value
                            Else
                                .Value = String.Empty
                            End If
                        End With

                        metaDataCollectionToReturn.Add(newBankMetaData)

                    Case ResourceManifestMetadataDefinitionBase.enumMetaDataType.BankCustomProperty
                        Dim customPropertyDefinition As ResourceManifestMetadataDefinitionBase

                        customPropertyDefinition = Me.ManifestMetaData.CustomPropertyDefinitions.Item(resourceElement.Name, ResourceManifestMetadataDefinitionBase.enumMetaDataType.BankCustomProperty)

                        If customPropertyDefinition IsNot Nothing Then
                            Select Case customPropertyDefinition.GetType.ToString
                                Case GetType(ResourceManifestMetadataSingleValueDefinition).ToString

                                    If Not CType(customPropertyDefinition, ResourceManifestMetadataSingleValueDefinition).RichText Then
                                        Dim newBankCustomProperty As New MetaData

                                        With newBankCustomProperty
                                            .Name = resourceElement.Name
                                            .Title = resourceElement.Title
                                            .MetaDatatype = MetaData.enumMetaDataType.BankCustomProperty

                                            If resourceElement.Values.Count = 1 Then
                                                .Value = resourceElement.Values(0).Value
                                            Else
                                                .Value = String.Empty
                                            End If
                                        End With

                                        metaDataCollectionToReturn.Add(newBankCustomProperty)
                                    Else
                                        Dim newBankCustomProperty As New MetaDataRichText

                                        With newBankCustomProperty
                                            .Name = resourceElement.Name
                                            .Title = resourceElement.Title
                                            .MetaDatatype = MetaData.enumMetaDataType.BankCustomProperty

                                            If resourceElement.Values.Count = 1 Then
                                                .Value = resourceElement.Values(0).Value
                                            Else
                                                .Value = String.Empty
                                            End If
                                        End With

                                        metaDataCollectionToReturn.Add(newBankCustomProperty)
                                    End If

                                Case GetType(ResourceManifestMetadataMultiValueDefinition).ToString
                                    Dim newBankCustomProperty As New MetaDataMultiValue

                                    With newBankCustomProperty
                                        .Name = resourceElement.Name
                                        .Title = resourceElement.Title
                                        .MetaDatatype = MetaData.enumMetaDataType.BankCustomProperty

                                        Dim selected As New Dictionary(Of String, ResourceManifestMetadataValue)
                                        For Each value As ResourceManifestMetadataValue In resourceElement.Values
                                            selected.Add(value.Name, value)
                                        Next

                                        For Each mdlv As ResourceManifestMetadataListValue In CType(customPropertyDefinition, ResourceManifestMetadataMultiValueDefinition).ListValues
                                            Dim lv As New MetaDataCode
                                            With lv
                                                .Name = mdlv.Name
                                                .Title = mdlv.Title
                                                .IsSelected = selected.ContainsKey(mdlv.Name)
                                                .MetaDatatype = MetaData.enumMetaDataType.BankCustomProperty
                                                .Value = String.Empty
                                                .Code = mdlv.Code
                                            End With
                                            .ListValues.Add(lv)
                                        Next
                                    End With

                                    metaDataCollectionToReturn.Add(newBankCustomProperty)

                                Case GetType(ResourceManifestMetaDataConceptStructureDefinition).ToString()
                                    Dim newBankCustomProperty As New MetaDataConceptStructure

                                    With newBankCustomProperty
                                        .Name = resourceElement.Name
                                        .Title = resourceElement.Title
                                        .MetaDatatype = MetaData.enumMetaDataType.BankCustomProperty

                                        For Each value As ResourceManifestMetadataValue In resourceElement.Values
                                            Dim csPartMetaData As New MetaDataConceptStructurePart() With {.Name = value.Name}
                                            .StructureParts.Add(csPartMetaData)
                                        Next
                                    End With

                                    metaDataCollectionToReturn.Add(newBankCustomProperty)

                                Case GetType(ResourceManifestMetaDataTreeStructureDefinition).ToString()
                                    Dim newBankCustomProperty As New MetaDataTreeStructure

                                    With newBankCustomProperty
                                        .Name = resourceElement.Name
                                        .Title = resourceElement.Title
                                        .MetaDatatype = MetaData.enumMetaDataType.BankCustomProperty

                                        For Each value As ResourceManifestMetadataValue In resourceElement.Values
                                            Dim csPartMetaData As New MetaDataTreeStructurePart() With {.Name = value.Name}
                                            .StructureParts.Add(csPartMetaData)
                                        Next
                                    End With

                                    metaDataCollectionToReturn.Add(newBankCustomProperty)
                                Case Else
                                    Throw New NotImplementedException(
                                        $"{customPropertyDefinition.GetType.ToString _
                                                                         } is not implemented in ManifestResourceManager")
                            End Select
                        Else
                            Throw New Exception(
                                $"No Custom Property Definition for '{resourceElement.Name}' in MetaDataManifest")
                        End If
                    Case Else
                        Throw New Exception("unknown metatdata type in GetResource for ManifestResourceManager")
                End Select
            Next
        End If

        Return metaDataCollectionToReturn
    End Function

    Public Overrides Function GetResourceEntry(name As String) As ResourceEntry
        Return Manifest.Resources.Item(name)
    End Function

    Public Overrides Function GetDependentResourcesForResource(name As String) As DependentResourceCollection
        Dim entry As ResourceEntry = Manifest.Resources.Item(name)
        Return entry.DependentResources
    End Function

    <SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
    <SuppressMessage("Microsoft.Usage", "CA2234:PassSystemUriObjectsInsteadOfStrings")> _
    Public Overrides Sub PutResource(resource As StreamResource)
        ReflectionHelper.CheckIsNotNothing(resource, "StreamResource")

        Dim resourceUri As Uri = New Uri(_baseUri, resource.Name)
        Dim relativeResourceUri As Uri = New Uri(resource.Name, UriKind.Relative)

        If Manifest IsNot Nothing Then
            If Not resource.Type().Equals("manifest") Then
                Dim entry As ResourceEntry = Manifest.Resources.Item(resource.Name)
                If entry Is Nothing Then
                    Dim depResourceCopy = New DependentResourceCollection() : depResourceCopy.AddRange(resource.DependentResources)
                    entry = New ResourceEntry(resource.Name, resource.Version, relativeResourceUri.ToString, resource.Type, resource.CacheLocal,
                                              depResourceCopy, resource.OriginalName, resource.OriginalVersion, resource.State)

                    Me.Manifest.Resources.Add(entry)
                    If resource.MetaData IsNot Nothing AndAlso resource.MetaData.Count > 0 Then
                        Dim entryRef As New ResourceManifestMetaDataEntryReference
                        entryRef.Name = entry.Name

                        For Each md As MetaData In resource.MetaData

                            Select Case md.MetaDatatype
                                Case MetaData.enumMetaDataType.BankMetaData
                                    Dim newBankMetaData As New ResourceManifestMetadataElement
                                    With newBankMetaData
                                        .Name = md.Name
                                        .Title = md.Title

                                        .MetaDataType = ResourceManifestMetadataDefinitionBase.enumMetaDataType.BankMetaData
                                        .Values.Add(New ResourceManifestMetadataValue(md.Name, md.Value))
                                    End With
                                    entryRef.MetaData.Add(newBankMetaData)

                                Case MetaData.enumMetaDataType.BankCustomProperty
                                    Select Case md.GetType.ToString
                                        Case GetType(MetaDataMultiValue).ToString
                                            Dim multiValue As MetaDataMultiValue = CType(md, MetaDataMultiValue)
                                            Dim multiValueDefinition As ResourceManifestMetadataMultiValueDefinition = DirectCast(Me.ManifestMetaData.CustomPropertyDefinitions.Item(multiValue.Name, ResourceManifestMetadataDefinitionBase.enumMetaDataType.BankCustomProperty), ResourceManifestMetadataMultiValueDefinition)
                                            Dim newCustomPropertyValue As New ResourceManifestMetadataElement

                                            Debug.Assert(multiValueDefinition IsNot Nothing,
                                                         $"The multi value definition with name {multiValue.Name _
                                                            } should exist in the ManifestMetaData.CustomPropertyDefinitions collections.")

                                            With newCustomPropertyValue
                                                .Name = multiValue.Name
                                                .Title = multiValue.Title
                                                .MetaDataType = ResourceManifestMetadataDefinitionBase.enumMetaDataType.BankCustomProperty
                                            End With

                                            For Each mditem As MetaData In multiValue.ListValues
                                                Debug.Assert(TypeOf mditem Is MetaDataCode, "This should have been a MetaDataCode")
                                                Dim metacode As MetaDataCode = DirectCast(mditem, MetaDataCode)

                                                If mditem.IsSelected Then
                                                    newCustomPropertyValue.Values.Add(New ResourceManifestMetadataValue(metacode.Name, ""))
                                                End If
                                            Next

                                            entryRef.MetaData.Add(newCustomPropertyValue)

                                        Case GetType(MetaDataConceptStructure).ToString()
                                            Dim conceptStructureMetaData As MetaDataConceptStructure = CType(md, MetaDataConceptStructure)
                                            Dim conceptStructureDefinition As ResourceManifestMetaDataConceptStructureDefinition = DirectCast(Me.ManifestMetaData.CustomPropertyDefinitions.Item(conceptStructureMetaData.Name, ResourceManifestMetadataDefinitionBase.enumMetaDataType.BankCustomProperty), ResourceManifestMetaDataConceptStructureDefinition)
                                            Debug.Assert(conceptStructureDefinition IsNot Nothing,
                                                         $"The concept structure definition with name {conceptStructureMetaData.Name} should exist in the ManifestMetaData.CustomPropertyDefinitions collection.")

                                            Dim newCustomPropertyValue As New ResourceManifestMetadataElement With {
                                                .Name = conceptStructureMetaData.Name,
                                                .Title = conceptStructureMetaData.Title,
                                                .MetaDataType = ResourceManifestMetadataDefinitionBase.enumMetaDataType.BankCustomProperty}

                                            For Each conceptStructurePartMetaData As MetaDataConceptStructurePart In conceptStructureMetaData.StructureParts
                                                newCustomPropertyValue.Values.Add(New ResourceManifestMetadataValue(conceptStructurePartMetaData.Name, String.Empty))
                                            Next

                                            entryRef.MetaData.Add(newCustomPropertyValue)

                                        Case GetType(MetaDataTreeStructure).ToString()
                                            Dim treeStructureMetaData As MetaDataTreeStructure = CType(md, MetaDataTreeStructure)
                                            Dim treeStructureDefinition As ResourceManifestMetaDataTreeStructureDefinition = DirectCast(Me.ManifestMetaData.CustomPropertyDefinitions.Item(treeStructureMetaData.Name, ResourceManifestMetadataDefinitionBase.enumMetaDataType.BankCustomProperty), ResourceManifestMetaDataTreeStructureDefinition)
                                            Debug.Assert(treeStructureDefinition IsNot Nothing,
                                                         $"The tree structure definition with name {treeStructureMetaData.Name} should exist in the ManifestMetaData.CustomPropertyDefinitions collection.")

                                            Dim newCustomPropertyValue As New ResourceManifestMetadataElement With {
                                                .Name = treeStructureMetaData.Name,
                                                .Title = treeStructureMetaData.Title,
                                                .MetaDataType = ResourceManifestMetadataDefinitionBase.enumMetaDataType.BankCustomProperty}

                                            For Each treeStructurePartMetaData As MetaDataTreeStructurePart In treeStructureMetaData.StructureParts
                                                newCustomPropertyValue.Values.Add(New ResourceManifestMetadataValue(treeStructurePartMetaData.Name, String.Empty))
                                            Next

                                            entryRef.MetaData.Add(newCustomPropertyValue)

                                        Case GetType(MetaDataRichText).ToString, GetType(MetaData).ToString
                                            Dim singleValue As MetaData = md
                                            Dim singleValueDefinition As ResourceManifestMetadataSingleValueDefinition = DirectCast(Me.ManifestMetaData.CustomPropertyDefinitions.Item(singleValue.Name, ResourceManifestMetadataDefinitionBase.enumMetaDataType.BankCustomProperty), ResourceManifestMetadataSingleValueDefinition)
                                            Dim newCustomPropertyValue As New ResourceManifestMetadataElement

                                            Debug.Assert(singleValueDefinition IsNot Nothing,
                                                         $"The single value definition with name {singleValue.Name} should exist in the ManifestMetaData.CustomPropertyDefinitions collections.")

                                            With newCustomPropertyValue
                                                .Name = singleValue.Name
                                                .Title = singleValue.Title
                                                .MetaDataType = ResourceManifestMetadataDefinitionBase.enumMetaDataType.BankCustomProperty
                                                If String.IsNullOrEmpty(singleValue.Value) Then
                                                    .Values.Add(New ResourceManifestMetadataValue(.Name, String.Empty))
                                                Else
                                                    .Values.Add(New ResourceManifestMetadataValue(.Name, singleValue.Value))
                                                End If
                                            End With

                                            entryRef.MetaData.Add(newCustomPropertyValue)
                                        Case Else
                                            Throw New NotImplementedException($"metadata of type {md.GetType.ToString} not implemented")
                                    End Select
                                Case Else
                                    Throw New Exception(String.Empty)
                            End Select
                        Next

                        Me.ManifestMetaData.ResourceReferences.Add(entryRef)
                    End If
                End If
            End If
            PutStream(resourceUri, resource)
        End If
    End Sub


    <SuppressMessage("Microsoft.Usage", "CA2234:PassSystemUriObjectsInsteadOfStrings")>
    Public Sub UpdateManifest()
        ResourceManifest.Save(New Uri(_baseUri, "manifest.xml"), Me.Manifest, Me)
        ResourceMetaDataManifest.Save(New Uri(_baseUri, "manifestMetaData.xml"), Me.ManifestMetaData, Me)
    End Sub

    <SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")>
    Public Overrides Function GetTypedResource(name As String, usingType As Type, request As ResourceRequestDTO) As BinaryResource
        Log.TraceInformation(TraceCategory.ResourceManager, My.Resources.Trace_ManifestResourceManager_GetTypedResource_GetResource, name, usingType.FullName)
        Return GetResourceAndProcessStream(name, New ResourceProcessingFunction(AddressOf StreamConverters.ConvertStreamToTypedInstance), usingType)
    End Function

    Public Sub CopyMediaToCache()
        Try
            My.Computer.FileSystem.CreateDirectory(_cacheTestLocation)

            Dim resourceEntries As ResourceEntryCollection = GetAllCacheableResourcesInManifest()
            Log.TraceInformation(TraceCategory.ResourceManager, String.Format(My.Resources.Trace_ManifestResourceManager_CopyMediaToCache_CopyResources, resourceEntries.Count))
            Log.Indent()

            For Each resource As ResourceEntry In resourceEntries
                Dim data As BinaryResource = GetResourceAndProcessStream(resource.Name, AddressOf StreamConverters.ConvertStreamToByteArray, GetType(Object))
                Dim filename As String = Path.Combine(_cacheTestLocation, Path.GetFileName(resource.Uri.ToString))

                FileHelper.MakeFileFromByteArray(filename, CType(data.ResourceObject, Byte()))

                Log.TraceInformation(TraceCategory.ResourceManager, My.Resources.Trace_ManifestResourceManager_CopyMediaToCache_AddedToCache, resource.Name)

                _resourceEntriesCache.Add(resource)

                OnCopyToLocalCacheProgress(New EventArgs)
            Next
        Catch e As Exception
            Throw New ResourceException(My.Resources.Error_ManifestResourceManager_CopyMediaToCache_ErrorWhileWritingToCache, e)
        End Try

        Log.Unindent()
    End Sub

    <SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> _
    Public Sub ClearCache()
        Try
            If (My.Computer.FileSystem.DirectoryExists(_cacheTestLocation)) Then
                My.Computer.FileSystem.DeleteDirectory(_cacheTestLocation, DeleteDirectoryOption.DeleteAllContents)
                If _resourceEntriesCache IsNot Nothing Then
                    _resourceEntriesCache.Clear()
                End If
                Log.TraceInformation(TraceCategory.ResourceManager, My.Resources.Trace_ManifestResourceManager_ClearCache_CacheCleared)
            Else
                Log.TraceInformation(TraceCategory.ResourceManager, My.Resources.Trace_ManifestResourceManager_ClearCache_CachedirNotFoundOrNoEntriesInCache)
            End If
        Catch Ex As Exception
            Console.WriteLine($"Error: disposing of the resources {Ex.ToString()}")
        End Try
    End Sub

    Public Function GetAbsoluteResourceFilename(name As String) As String
        Dim resourceEntry As ResourceEntry = _resourceEntriesCache.Item(name)

        If resourceEntry IsNot Nothing Then
            Return Path.Combine(_cacheTestLocation, Path.GetFileName(resourceEntry.Uri.ToString))
        Else
            Return String.Empty
        End If

    End Function

    Public Overrides Function GetResourcesOfType(type As String) As ResourceEntryCollection
        Dim BinaryResourceEntries As ResourceEntryCollection = New ResourceEntryCollection

        For Each entry As ResourceEntry In Manifest.Resources
            If entry.Type = type Then
                BinaryResourceEntries.Add(entry)
            End If
        Next
        Return BinaryResourceEntries
    End Function

    Public Function GetCachableResourcesCount() As Integer
        Return GetAllCacheableResourcesInManifest().Count
    End Function



    Private Function GetFromCache(resourceEntry As ResourceEntry, processingMethod As ResourceProcessingFunction, usingType As Type) As BinaryResource
        Dim filename As String = Path.Combine(_cacheTestLocation, Path.GetFileName(resourceEntry.Uri.ToString))
        Dim resourceUri As Uri

        If resourceEntry IsNot Nothing Then
            Log.Indent()
            resourceUri = New Uri(filename)
            Dim resourceObject As Object = Nothing
            Dim resourceStream As StreamResource = Nothing

            Try
                Log.TraceInformation(TraceCategory.ResourceManager, My.Resources.Trace_ManifestResourceManager_GetFromCache_GetResourceStream)
                resourceStream = GetStream(resourceUri)
                If resourceStream Is Nothing Then
                    Throw New ResourceException(String.Format(My.Resources.Error_ManifestResourceManager_EmptyStream, resourceEntry.Name))
                End If
                Log.TraceInformation(TraceCategory.ResourceManager, My.Resources.Trace_ManifestResourceManager_GetFromCache_ProcessingStream, resourceStream.Length)
                resourceObject = processingMethod.Invoke(resourceEntry.Name, resourceStream, usingType)
            Catch ex As Exception
                Throw New ResourceException(String.Format(My.Resources.Error_ManifestResourceManager_CannotGetResource, resourceEntry.Name, ex.Message))
            Finally
                If resourceStream IsNot Nothing Then resourceStream.CloseStream()
            End Try

            Log.Unindent()

            Return New BinaryResource(resourceEntry.Name, resourceEntry.Type, resourceObject, resourceEntry.DependentResources)
        Else
            Return Nothing
        End If

    End Function

    Private Function MakeAbsoluteResourceUri(entry As ResourceEntry) As Uri
        Dim resourceUri As New Uri(entry.Uri, UriKind.RelativeOrAbsolute)

        If resourceUri.IsAbsoluteUri Then
            Return resourceUri
        Else
            Return New Uri(_baseUri, resourceUri)
        End If

    End Function

    Public Function GetStream(uri As Uri, Optional getReadOnly As Boolean = False) As StreamResource
        Dim resourceReader As IResourceReader
        Dim packageUri As Uri = PackageManager.GetPackageUri(uri)

        If packageUri Is Nothing Then
            resourceReader = ProtocolHandlerFactory.GetReaderHandler(uri.Scheme)

            If getReadOnly Then
                Return resourceReader.GetReadonlyStream(uri)
            Else
                Return resourceReader.GetStream(uri)
            End If
        Else
            Dim streamRelativeUri As Uri = PackageManager.GetStreamRelativeUri(uri)

            If _packager Is Nothing OrElse Not _packager.Packagekey.Equals(packageUri.ToString, StringComparison.CurrentCultureIgnoreCase) Then
                _packager = New PackageManager(packageUri, False, getReadOnly)
            End If

            Return _packager.GetStream(streamRelativeUri)
        End If
    End Function


    Private Sub PutStream(uri As Uri, streamResource As StreamResource)
        Dim resourceWriter As IResourceWriter

        Dim packageUri As Uri = PackageManager.GetPackageUri(uri)

        If packageUri Is Nothing Then
            resourceWriter = ProtocolHandlerFactory.GetWriterHandler(uri.Scheme)
            resourceWriter.PutStream(uri, streamResource)
        Else
            Dim streamRelativeUri As Uri = PackageManager.GetStreamRelativeUri(uri)
            If _packager Is Nothing Then
                _packager = New PackageManager(packageUri, True)
            End If

            _packager.PutStream(streamRelativeUri, streamResource)
        End If
    End Sub

    Private Function GetResourceAndProcessStream(name As String, processingMethod As ResourceProcessingFunction, usingType As Type) As BinaryResource
        Dim entry As ResourceEntry = Manifest.Resources.Item(name)

        If entry IsNot Nothing Then
            Dim resourceUri As Uri = MakeAbsoluteResourceUri(entry)
            Dim resourceObject As Object = Nothing
            Dim resourceStream As StreamResource = Nothing

            Log.Indent()

            SyncLock _processStreamLock
                Try
                    Log.TraceInformation(TraceCategory.ResourceManager, My.Resources.Trace_ManifestResourceManager_GetResourceAndProcessStream_GetResourceStream)
                    resourceStream = GetStream(resourceUri)

                    If resourceStream Is Nothing Then
                        Throw New ResourceException(String.Format(My.Resources.Error_ManifestResourceManager_EmptyStream, name))
                    End If
                    Log.TraceInformation(TraceCategory.ResourceManager, My.Resources.Trace_ManifestResourceManager_GetResourceAndProcessStream_ProcessingStream, resourceStream.Length)
                    resourceObject = processingMethod.Invoke(name, resourceStream, usingType)
                Catch ex As Exception
                    Throw New ResourceException(String.Format(My.Resources.Error_ManifestResourceManager_CannotGetResource, name, ex.Message), ex)
                Finally
                    If resourceStream IsNot Nothing Then resourceStream.CloseStream()
                End Try
            End SyncLock

            Log.Unindent()
            Return New BinaryResource(entry.Name, entry.Type, resourceObject, entry.DependentResources)
        Else
            Return Nothing
        End If
    End Function

    Private Function GetAllCacheableResourcesInManifest() As ResourceEntryCollection
        Dim BinaryResourceEntries As ResourceEntryCollection = New ResourceEntryCollection

        For Each entry As ResourceEntry In Manifest.Resources
            If entry.CacheLocal Then
                BinaryResourceEntries.Add(entry)
            End If
        Next
        Return BinaryResourceEntries
    End Function



    <SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")> _
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        Me.Dispose(False)
    End Sub

End Class
