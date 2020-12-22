Imports System.Linq
Imports Questify.Builder.Model.ContentModel.Interfaces
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.ContentModel
Imports System.Text
Imports Cito.Tester.Common
Imports Enums
Imports Questify.Builder.Logic.Service.Factories
Imports Versioning
Imports SD.LLBLGen.Pro.ORMSupportClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses


Public Class VersionReverter

    Private ReadOnly _mostRecentResourceHistoryEntity As ResourceHistoryEntity
    Private ReadOnly _resourceHistoryEntityToRevertTo As ResourceHistoryEntity
    Private ReadOnly _metaDataOfMostRecentResourceHistoryEntity As New Versioning.MetaData()
    Private ReadOnly _metaDataOfResourceHistoryEntityToRevertTo As New Versioning.MetaData()
    Private ReadOnly _cachedParents As New Dictionary(Of TreeStructurePartCustomBankPropertyEntity, List(Of TreeStructurePartCustomBankPropertyEntity))

    Public Sub New(ByVal mostRecentResourceHistoryEntity As ResourceHistoryEntity, ByVal resourceHistoryEntityToRevertTo As ResourceHistoryEntity)
        _mostRecentResourceHistoryEntity = mostRecentResourceHistoryEntity
        _resourceHistoryEntityToRevertTo = resourceHistoryEntityToRevertTo

        If _mostRecentResourceHistoryEntity.MetaData IsNot Nothing AndAlso _mostRecentResourceHistoryEntity.MetaData.Length > 0 Then
            _metaDataOfMostRecentResourceHistoryEntity = New XmlSerializerMetaDataDeserializer(Nothing).DeserializeMetaData(_mostRecentResourceHistoryEntity.MetaData)
        End If

        If _resourceHistoryEntityToRevertTo.MetaData IsNot Nothing AndAlso _resourceHistoryEntityToRevertTo.MetaData.Length > 0 Then
            _metaDataOfResourceHistoryEntityToRevertTo = New XmlSerializerMetaDataDeserializer(Nothing).DeserializeMetaData(_resourceHistoryEntityToRevertTo.MetaData)
        End If
    End Sub

    Public Function CanRevert(ByRef errorMessage As String) As Boolean
        Dim versionableResource = TryCast(_resourceHistoryEntityToRevertTo.Resource, IVersionable)
        If versionableResource IsNot Nothing AndAlso Not versionableResource.SaveObjectAsBinary Then
            Return False
        End If

        If _mostRecentResourceHistoryEntity Is _resourceHistoryEntityToRevertTo Then
            Return False
        End If

        Debug.Assert(_metaDataOfMostRecentResourceHistoryEntity.DependentResourcesMetaData IsNot Nothing, "DependentResourceMetaData collection of the most recent history version cannot be Nothing. Please run QBVerify tool first!")
        Debug.Assert(_metaDataOfResourceHistoryEntityToRevertTo.DependentResourcesMetaData IsNot Nothing, "DependentResourceMetaData collection of the history version to revert to cannot be Nothing. Please run QBVerify tool first!")

        If _metaDataOfResourceHistoryEntityToRevertTo.DependentResourcesMetaData.Count = 0 Then
            If TypeOf _resourceHistoryEntityToRevertTo.Resource Is ItemResourceEntity OrElse TypeOf _resourceHistoryEntityToRevertTo.Resource Is AssessmentTestResourceEntity Then
                AddToErrorMessage(My.Resources.NoDependenciesAvailable, errorMessage)
                Return False
            End If
        End If

        For Each dependentResourceOfMostRecentProperty As DependentResourceMetaData In _metaDataOfMostRecentResourceHistoryEntity.DependentResourcesMetaData
            Dim dependentResourceOfPropertyToRevertTo As DependentResourceMetaData = _metaDataOfResourceHistoryEntityToRevertTo.DependentResourcesMetaData.FirstOrDefault(Function(i) i.Id = dependentResourceOfMostRecentProperty.Id)

            If dependentResourceOfPropertyToRevertTo IsNot Nothing Then
                If dependentResourceOfMostRecentProperty.Version <> dependentResourceOfPropertyToRevertTo.Version Then
                    AddToErrorMessage(dependentResourceOfMostRecentProperty.Name, dependentResourceOfMostRecentProperty.Version, dependentResourceOfPropertyToRevertTo.Version, errorMessage)
                    Return False
                Else
                End If
            Else
            End If
        Next

        Dim dependentResourcesFromDatabase = ResourceFactory.Instance.GetResourcesByIdsWithOption(_metaDataOfResourceHistoryEntityToRevertTo.DependentResourcesMetaData.Select(Function(r) CType(r, DependentResourceMetaData).Id).ToList(), New ResourceRequestDTO())

        For Each dependentResourceOfPropertyToRevertTo As DependentResourceMetaData In _metaDataOfResourceHistoryEntityToRevertTo.DependentResourcesMetaData
            Dim propertyEntityCurrentlyInBank As IPropertyEntity = dependentResourcesFromDatabase.Items.Cast(Of ResourceEntity).FirstOrDefault(Function(i) i.ResourceId = dependentResourceOfPropertyToRevertTo.Id)

            If propertyEntityCurrentlyInBank IsNot Nothing Then
                If dependentResourceOfPropertyToRevertTo.Version <> propertyEntityCurrentlyInBank.Version Then
                    AddToErrorMessage(dependentResourceOfPropertyToRevertTo.Name, propertyEntityCurrentlyInBank.Version, dependentResourceOfPropertyToRevertTo.Version, errorMessage)
                    Return False
                End If
            Else
                AddToErrorMessage(dependentResourceOfPropertyToRevertTo.Name, Nothing, dependentResourceOfPropertyToRevertTo.Version, errorMessage)
                Return False
            End If
        Next

        Return True
    End Function

    Private Sub AddToErrorMessage(ByVal messageToAdd As String, ByRef errorMessage As String)
        Dim sb = New StringBuilder(errorMessage)
        sb.Append(vbNewLine)
        sb.Append(messageToAdd)

        errorMessage = sb.ToString()
    End Sub

    Private Sub AddToErrorMessage(ByVal propertyName As String, ByVal versionOfMostRecent As String, ByVal versionToRevertTo As String, ByRef errorMessage As String)
        Dim result As New StringBuilder(errorMessage)

        result.Append(vbNewLine)
        result.Append(propertyName)
        result.Append(": ")
        result.Append(My.Resources.MostRecentVersion)
        result.Append(" ")

        If Not String.IsNullOrEmpty(versionOfMostRecent) Then
            result.Append(versionOfMostRecent)
        Else
            result.Append(My.Resources.ResourceDoesNotExist)
        End If

        result.Append(", ")
        result.Append(My.Resources.ThisVersion)
        result.Append(" ")
        result.Append(versionToRevertTo)

        errorMessage = result.ToString()
    End Sub

    Public Function Revert() As IPropertyEntity
        If _mostRecentResourceHistoryEntity.Resource.ResourceData Is Nothing Then
            _mostRecentResourceHistoryEntity.Resource.ResourceData = ResourceFactory.Instance.GetResourceData(_mostRecentResourceHistoryEntity.Resource)
        End If

        _mostRecentResourceHistoryEntity.Resource.ResourceData.BinData = _resourceHistoryEntityToRevertTo.BinData

        SetDependentResources()
        SetPropertyEntities()
        SetCustomBankProperties()

        Return _mostRecentResourceHistoryEntity.Resource
    End Function

    Private Sub SetCustomBankProperties()
        Dim cbpMetaData As New List(Of MetaDataBase)
        cbpMetaData.AddRange(_metaDataOfResourceHistoryEntityToRevertTo.CustomPropertiesMetaData)
        cbpMetaData.AddRange(_metaDataOfResourceHistoryEntityToRevertTo.ConceptStructureMetaData)
        cbpMetaData.AddRange(_metaDataOfResourceHistoryEntityToRevertTo.TreeStructureMetaData)
        Dim customBankProperties = BankFactory.Instance.GetCustomBankProperties(cbpMetaData.Select(Function(cbp) cbp.Id).ToList())

        Dim request = New ResourceRequestDTO() With {.WithCustomProperties = True}
        Dim itemFromBank = ResourceFactory.Instance.GetResourceByIdWithOption(_mostRecentResourceHistoryEntity.ResourceId, request)
        If itemFromBank Is Nothing Then Return

        _mostRecentResourceHistoryEntity.Resource.CustomBankPropertyValueCollection.AddRange(itemFromBank.CustomBankPropertyValueCollection.ToList())

        For Each customBankPropertyValue In _mostRecentResourceHistoryEntity.Resource.CustomBankPropertyValueCollection.ToList()
            Dim cbFromMetaData = cbpMetaData.FirstOrDefault(Function(cbp) cbp.Id = customBankPropertyValue.CustomBankPropertyId)
            If cbFromMetaData Is Nothing Then
                If _mostRecentResourceHistoryEntity.Resource.CustomBankPropertyValueCollection.RemovedEntitiesTracker Is Nothing Then
                    _mostRecentResourceHistoryEntity.Resource.CustomBankPropertyValueCollection.RemovedEntitiesTracker = New EntityCollection()
                End If

                _mostRecentResourceHistoryEntity.Resource.CustomBankPropertyValueCollection.Remove(customBankPropertyValue)
                _mostRecentResourceHistoryEntity.Resource.CustomBankPropertyValueCollection.RemovedEntitiesTracker.Add(customBankPropertyValue)
            End If
        Next

        For Each customPropertyMetaData In cbpMetaData
            Dim customPropertyFromBank = customBankProperties.FirstOrDefault(Function(cbp) cbp.CustomBankPropertyId = customPropertyMetaData.Id)
            If customPropertyFromBank IsNot Nothing Then
                Dim customPropteryValueFilter As IPredicate = (CustomBankPropertyValueFields.CustomBankPropertyId = customPropertyFromBank.CustomBankPropertyId)
                Dim matchedPropertyList = _mostRecentResourceHistoryEntity.Resource.CustomBankPropertyValueCollection.FindMatches(customPropteryValueFilter)

                Select Case customPropertyFromBank.GetType.ToString
                    Case GetType(FreeValueCustomBankPropertyEntity).ToString
                        Dim fvProperty As FreeValueCustomBankPropertyValueEntity
                        Dim metaData = CType(customPropertyMetaData, CustomBankPropertyMetaData)
                        If metaData Is Nothing Then Continue For
                        If matchedPropertyList.Count = 0 Then
                            fvProperty = New FreeValueCustomBankPropertyValueEntity(_mostRecentResourceHistoryEntity.ResourceId, customPropertyFromBank.CustomBankPropertyId)
                            fvProperty.CustomBankProperty = customPropertyFromBank
                            _mostRecentResourceHistoryEntity.Resource.CustomBankPropertyValueCollection.Add(fvProperty)
                        Else
                            fvProperty = CType(_mostRecentResourceHistoryEntity.Resource.CustomBankPropertyValueCollection(matchedPropertyList(0)), FreeValueCustomBankPropertyValueEntity)
                        End If
                        fvProperty.Value = metaData.Values.FirstOrDefault()
                        fvProperty.SetCustomPropertyDisplayValue(customPropertyFromBank)

                    Case GetType(ListCustomBankPropertyEntity).ToString
                        Dim lvProperty As ListCustomBankPropertyValueEntity
                        Dim isNew As Boolean = matchedPropertyList.Count = 0
                        Dim metaData = CType(customPropertyMetaData, CustomBankPropertyMetaData)
                        If metaData Is Nothing Then Continue For

                        If isNew Then
                            lvProperty = New ListCustomBankPropertyValueEntity(_mostRecentResourceHistoryEntity.ResourceId, customPropertyFromBank.CustomBankPropertyId)
                            lvProperty.CustomBankProperty = customPropertyFromBank
                        Else
                            lvProperty = CType(_mostRecentResourceHistoryEntity.Resource.CustomBankPropertyValueCollection(matchedPropertyList(0)), ListCustomBankPropertyValueEntity)
                        End If
                        Dim lcp = DirectCast(customPropertyFromBank, ListCustomBankPropertyEntity)
                        AddListValueByValueOrTitle(_mostRecentResourceHistoryEntity.Resource, lcp, lvProperty, metaData.Values, isNew)
                        lvProperty.SetCustomPropertyDisplayValue(customPropertyFromBank)

                    Case GetType(ConceptStructureCustomBankPropertyEntity).ToString
                        Dim csProperty As ConceptStructureCustomBankPropertyValueEntity
                        Dim metaData = CType(customPropertyMetaData, ConceptStructureMetaData)
                        If metaData Is Nothing Then Continue For
                        Dim isNew As Boolean = matchedPropertyList.Count = 0
                        If isNew Then
                            csProperty = New ConceptStructureCustomBankPropertyValueEntity(_mostRecentResourceHistoryEntity.ResourceId, customPropertyFromBank.CustomBankPropertyId)
                            csProperty.CustomBankProperty = customPropertyFromBank
                        Else
                            csProperty = CType(_mostRecentResourceHistoryEntity.Resource.CustomBankPropertyValueCollection(matchedPropertyList(0)), ConceptStructureCustomBankPropertyValueEntity)
                        End If
                        Dim ccp = DirectCast(customPropertyFromBank, ConceptStructureCustomBankPropertyEntity)
                        AddConceptValueByCode(TryCast(_mostRecentResourceHistoryEntity.Resource, ItemResourceEntity), ccp, csProperty, metaData, isNew)
                        csProperty.SetCustomPropertyDisplayValue(customPropertyFromBank)

                    Case GetType(TreeStructureCustomBankPropertyEntity).ToString
                        Dim csProperty As TreeStructureCustomBankPropertyValueEntity = Nothing
                        Dim metaData = CType(customPropertyMetaData, TreeStructureMetaData)
                        If metaData Is Nothing Then Continue For
                        Dim isNew As Boolean = matchedPropertyList.Count = 0
                        If Not isNew Then
                            csProperty = CType(_mostRecentResourceHistoryEntity.Resource.CustomBankPropertyValueCollection(matchedPropertyList(0)), TreeStructureCustomBankPropertyValueEntity)
                        End If
                        Dim tcp = DirectCast(customPropertyFromBank, TreeStructureCustomBankPropertyEntity)
                        AddTreeStructureValueByCode(_mostRecentResourceHistoryEntity.Resource, tcp, csProperty, metaData, isNew)
                        csProperty.SetCustomPropertyDisplayValue(customPropertyFromBank)
                End Select
            End If
        Next
    End Sub

    Private Sub AddListValueByValueOrTitle(itemResource As ResourceEntity, lcp As ListCustomBankPropertyEntity, listValue As ListCustomBankPropertyValueEntity, values As List(Of String), isNew As Boolean)
        Dim matchFound = False
        values.ForEach(Sub(customPropertyValue)
                           If lcp.ListValueCustomBankPropertyCollection IsNot Nothing Then
                               Dim matchedValue = lcp.ListValueCustomBankPropertyCollection.Where(Function(v) v.Name = customPropertyValue).FirstOrDefault
                               If matchedValue IsNot Nothing Then
                                   If Not matchFound Then
                                       Dim lsvCount = listValue.ListCustomBankPropertySelectedValueCollection.Count
                                       For i = 0 To lsvCount - 1
                                           Dim lvToRemove = listValue.ListCustomBankPropertySelectedValueCollection.Item(0)
                                           listValue.ListCustomBankPropertySelectedValueCollection.Remove(lvToRemove)
                                           If itemResource.CustomBankPropertyValueCollection.RemovedEntitiesTracker Is Nothing Then
                                               itemResource.CustomBankPropertyValueCollection.RemovedEntitiesTracker = New EntityCollection
                                           End If
                                           itemResource.CustomBankPropertyValueCollection.RemovedEntitiesTracker.Add(lvToRemove)
                                       Next
                                   End If
                                   matchFound = True
                                   Dim selectedValue = New ListCustomBankPropertySelectedValueEntity
                                   selectedValue.CustomBankPropertyId = lcp.CustomBankPropertyId
                                   selectedValue.ResourceId = itemResource.ResourceId
                                   selectedValue.ListValueBankCustomPropertyId = matchedValue.ListValueBankCustomPropertyId
                                   If listValue.ListCustomBankPropertySelectedValueCollection.Where(Function(c) c.ListValueBankCustomPropertyId = matchedValue.ListValueBankCustomPropertyId).Count = 0 Then
                                       listValue.ListCustomBankPropertySelectedValueCollection.Add(selectedValue)
                                   End If
                               End If
                           End If
                       End Sub)
        If matchFound AndAlso isNew Then
            itemResource.CustomBankPropertyValueCollection.Add(listValue)
        End If
    End Sub

    Private Sub AddConceptValueByCode(resourceEntity As ItemResourceEntity, ccp As ConceptStructureCustomBankPropertyEntity, conceptValue As ConceptStructureCustomBankPropertyValueEntity, metadata As ConceptStructureMetaData, isNew As Boolean)
        Dim matchFound = False
        Dim value = metadata(0)

        Dim assessmentItem = resourceEntity.GetAssessmentItem
        If assessmentItem IsNot Nothing Then
            If ccp.ConceptStructurePartCustomBankPropertyCollection IsNot Nothing Then
                Dim matchedValue = ccp.ConceptStructurePartCustomBankPropertyCollection.Where(Function(v) v.Code = value).FirstOrDefault
                If matchedValue IsNot Nothing Then
                    If matchedValue.ConceptType Is Nothing Then
                        matchedValue.ConceptType = BankFactory.Instance.GetAllConceptTypes().OfType(Of ConceptTypeEntity).FirstOrDefault(Function(conceptType) conceptType.ConceptTypeId = matchedValue.ConceptTypeId)
                    End If

                    If (matchedValue.ConceptType Is Nothing OrElse Not ((matchedValue.ConceptType.ApplicableToMask And ResourceTypeEnum.ItemResource) = ResourceTypeEnum.ItemResource)) Then Return

                    Dim csCount = conceptValue.ConceptStructureCustomBankPropertySelectedPartCollection.Count
                    If Not matchFound Then
                        For i = 0 To csCount - 1
                            Dim lvToRemove = conceptValue.ConceptStructureCustomBankPropertySelectedPartCollection.Item(0)
                            conceptValue.ConceptStructureCustomBankPropertySelectedPartCollection.Remove(lvToRemove)
                            If resourceEntity.CustomBankPropertyValueCollection.RemovedEntitiesTracker Is Nothing Then
                                resourceEntity.CustomBankPropertyValueCollection.RemovedEntitiesTracker = New EntityCollection
                            End If
                            resourceEntity.CustomBankPropertyValueCollection.RemovedEntitiesTracker.Add(lvToRemove)
                        Next
                    End If
                    matchFound = True
                    Dim selectedValue = New ConceptStructureCustomBankPropertySelectedPartEntity
                    selectedValue.CustomBankPropertyId = ccp.CustomBankPropertyId
                    selectedValue.ResourceId = resourceEntity.ResourceId
                    selectedValue.ConceptStructurePartId = matchedValue.ConceptStructurePartCustomBankPropertyId
                    conceptValue.ConceptStructureCustomBankPropertySelectedPartCollection.Add(selectedValue)
                End If
            End If
            If matchFound AndAlso isNew Then
                resourceEntity.CustomBankPropertyValueCollection.Add(conceptValue)
            End If
        End If
    End Sub

    Private Sub AddTreeStructureValueByCode(resourceEntity As ResourceEntity, tcp As TreeStructureCustomBankPropertyEntity, treeStructureValue As TreeStructureCustomBankPropertyValueEntity, metadata As TreeStructureMetaData, isNew As Boolean)
        Dim matchFound = False
        If isNew AndAlso treeStructureValue Is Nothing Then
            treeStructureValue = New TreeStructureCustomBankPropertyValueEntity(resourceEntity.ResourceId, tcp.CustomBankPropertyId)
            treeStructureValue.CustomBankProperty = tcp
        End If

        For valIndex = 0 To metadata.Values.Count - 1
            Dim customPropertyCode As Guid = metadata(valIndex)
            If tcp.TreeStructurePartCustomBankPropertyCollection IsNot Nothing Then
                Dim matchedValue = tcp.TreeStructurePartCustomBankPropertyCollection.Where(Function(v) v.Code = customPropertyCode).FirstOrDefault
                If matchedValue IsNot Nothing Then
                    If Not matchFound Then
                        Dim tsCount = treeStructureValue.TreeStructureCustomBankPropertySelectedPartCollection.Count
                        For i = 0 To tsCount - 1
                            Dim lvToRemove = treeStructureValue.TreeStructureCustomBankPropertySelectedPartCollection.Item(0)
                            treeStructureValue.TreeStructureCustomBankPropertySelectedPartCollection.Remove(lvToRemove)
                            If resourceEntity.CustomBankPropertyValueCollection.RemovedEntitiesTracker Is Nothing Then
                                resourceEntity.CustomBankPropertyValueCollection.RemovedEntitiesTracker = New EntityCollection
                            End If
                            resourceEntity.CustomBankPropertyValueCollection.RemovedEntitiesTracker.Add(lvToRemove)
                        Next
                    End If
                    matchFound = True
                    If treeStructureValue.TreeStructureCustomBankPropertySelectedPartCollection.Where(Function(tv) tv.TreeStructurePartId = matchedValue.TreeStructurePartCustomBankPropertyId).Count = 0 Then
                        Dim selectedValue = New TreeStructureCustomBankPropertySelectedPartEntity
                        selectedValue.CustomBankPropertyId = tcp.CustomBankPropertyId
                        selectedValue.ResourceId = resourceEntity.ResourceId
                        selectedValue.TreeStructurePartId = matchedValue.TreeStructurePartCustomBankPropertyId
                        treeStructureValue.TreeStructureCustomBankPropertySelectedPartCollection.Add(selectedValue)
                        If Not _cachedParents.ContainsKey(matchedValue) Then
                            _cachedParents.Add(matchedValue, matchedValue.GetParents(tcp))
                        End If
                        For Each parent In _cachedParents(matchedValue)
                            If treeStructureValue.TreeStructureCustomBankPropertySelectedPartCollection.Where(Function(tv) tv.TreeStructurePartId = parent.TreeStructurePartCustomBankPropertyId).Count = 0 Then
                                Dim selectedParentValue = New TreeStructureCustomBankPropertySelectedPartEntity
                                selectedParentValue.CustomBankPropertyId = tcp.CustomBankPropertyId
                                selectedParentValue.ResourceId = resourceEntity.ResourceId
                                selectedParentValue.TreeStructurePartId = parent.TreeStructurePartCustomBankPropertyId
                                treeStructureValue.TreeStructureCustomBankPropertySelectedPartCollection.Add(selectedParentValue)
                            End If
                        Next
                    End If
                End If
            End If
        Next
        If matchFound AndAlso isNew Then
            resourceEntity.CustomBankPropertyValueCollection.Add(treeStructureValue)
        End If
    End Sub

    Private Sub SetPropertyEntities()
        Dim title As String = If(_metaDataOfResourceHistoryEntityToRevertTo.PropertyEntityMetaData.Count > 0, _metaDataOfResourceHistoryEntityToRevertTo.PropertyEntityMetaData.First(Function(i) String.Compare(i.Name, "title", True) = 0).Value.ToString(), Nothing)
        Dim description As String = If(_metaDataOfResourceHistoryEntityToRevertTo.PropertyEntityMetaData.Count > 0, _metaDataOfResourceHistoryEntityToRevertTo.PropertyEntityMetaData.First(Function(i) String.Compare(i.Name, "description", True) = 0).Value.ToString(), Nothing)
        Dim stateId As Integer? = If(_metaDataOfResourceHistoryEntityToRevertTo.PropertyEntityMetaData.Count > 0, GetStateIdByStateName(_metaDataOfResourceHistoryEntityToRevertTo.PropertyEntityMetaData.First(Function(i) String.Compare(i.Name, "statename", True) = 0).Value.ToString()), Nothing)

        If title <> _mostRecentResourceHistoryEntity.Resource.Title Then _mostRecentResourceHistoryEntity.Resource.Title = title
        If description <> _mostRecentResourceHistoryEntity.Resource.Description Then _mostRecentResourceHistoryEntity.Resource.Description = description
        If stateId <> _mostRecentResourceHistoryEntity.Resource.StateId Then _mostRecentResourceHistoryEntity.Resource.StateId = stateId
    End Sub

    Private Sub SetDependentResources()
        For i As Integer = _mostRecentResourceHistoryEntity.Resource.DependentResourceCollection.Count - 1 To 0 Step -1
            Dim index As Integer = i

            If _metaDataOfResourceHistoryEntityToRevertTo.DependentResourcesMetaData.FirstOrDefault(Function(x) x.Id = _mostRecentResourceHistoryEntity.Resource.DependentResourceCollection(index).DependentResourceId) Is Nothing Then
                _mostRecentResourceHistoryEntity.Resource.DependentResourceCollection.RemoveAt(index)
            End If
        Next

        For Each dependentResourceMetaData As DependentResourceMetaData In _metaDataOfResourceHistoryEntityToRevertTo.DependentResourcesMetaData
            Dim existingDependentResource As DependentResourceEntity = _mostRecentResourceHistoryEntity.Resource.DependentResourceCollection.FirstOrDefault(Function(i) i.DependentResourceId = dependentResourceMetaData.Id)

            If existingDependentResource Is Nothing Then
                _mostRecentResourceHistoryEntity.Resource.DependentResourceCollection.Add(New DependentResourceEntity(_mostRecentResourceHistoryEntity.ResourceId, dependentResourceMetaData.Id))
            End If
        Next
    End Sub

    Private Function GetStateIdByStateName(ByVal stateName As String) As Integer?
        Dim state As StateEntity = CType(ResourceFactory.Instance.GetAvailableStates().FirstOrDefault(Function(i) CType(i, StateEntity).Name = stateName), StateEntity)

        If state IsNot Nothing Then
            Return state.StateId
        End If

        Return Nothing
    End Function
End Class
