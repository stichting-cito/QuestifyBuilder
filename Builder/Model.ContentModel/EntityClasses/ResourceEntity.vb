Imports System
Imports System.ComponentModel
Imports System.Collections.Generic
Imports System.Runtime.Serialization
Imports System.Xml.Serialization
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.Model.ContentModel.RelationClasses

Imports SD.LLBLGen.Pro.ORMSupportClasses


Namespace Questify.Builder.Model.ContentModel.EntityClasses

    <Serializable()> _
    Public Class ResourceEntity
        Inherits CommonEntityBase



        Private WithEvents _customBankPropertyValueCollection As EntityCollection(Of CustomBankPropertyValueEntity)
        Private WithEvents _referencedResourceCollection As EntityCollection(Of DependentResourceEntity)
        Private WithEvents _dependentResourceCollection As EntityCollection(Of DependentResourceEntity)
        Private WithEvents _hiddenResourceCollection As EntityCollection(Of HiddenResourceEntity)
        Private WithEvents _resourceHistoryCollection As EntityCollection(Of ResourceHistoryEntity)
        Private WithEvents _customBankPropertyCollectionViaCustomBankPropertyValue As EntityCollection(Of CustomBankPropertyEntity)
        Private WithEvents _bank As BankEntity
        Private WithEvents _state As StateEntity
        Private WithEvents _createdByUser As UserEntity
        Private WithEvents _modifiedByUser As UserEntity
        Private WithEvents _resourceData As ResourceDataEntity



        Private Shared _customProperties As Dictionary(Of String, String)
        Private Shared _fieldsCustomProperties As Dictionary(Of String, Dictionary(Of String, String))

        Public NotInheritable Class MemberNames
            Private Sub New()
            End Sub
            Public Shared ReadOnly [Bank] As String = "Bank"
            Public Shared ReadOnly [State] As String = "State"
            Public Shared ReadOnly [CreatedByUser] As String = "CreatedByUser"
            Public Shared ReadOnly [ModifiedByUser] As String = "ModifiedByUser"
            Public Shared ReadOnly [CustomBankPropertyValueCollection] As String = "CustomBankPropertyValueCollection"
            Public Shared ReadOnly [ReferencedResourceCollection] As String = "ReferencedResourceCollection"
            Public Shared ReadOnly [DependentResourceCollection] As String = "DependentResourceCollection"
            Public Shared ReadOnly [HiddenResourceCollection] As String = "HiddenResourceCollection"
            Public Shared ReadOnly [ResourceHistoryCollection] As String = "ResourceHistoryCollection"
            Public Shared ReadOnly [CustomBankPropertyCollectionViaCustomBankPropertyValue] As String = "CustomBankPropertyCollectionViaCustomBankPropertyValue"
            Public Shared ReadOnly [ResourceData] As String = "ResourceData"
        End Class

        Shared Sub New()
            SetupCustomPropertyHashtables()
        End Sub

        Public Sub New()
            MyBase.New("ResourceEntity")
            InitClassEmpty(Nothing, Nothing)
        End Sub

        Public Sub New(fields As IEntityFields2)
            MyBase.New("ResourceEntity")
            InitClassEmpty(Nothing, fields)
        End Sub

        Public Sub New(validator As IValidator)
            MyBase.New("ResourceEntity")
            InitClassEmpty(validator, Nothing)
        End Sub

        Public Sub New(resourceId As System.Guid)
            MyBase.New("ResourceEntity")
            InitClassEmpty(Nothing, Nothing)
            Me.ResourceId = resourceId
        End Sub

        Public Sub New(resourceId As System.Guid, validator As IValidator)
            MyBase.New("ResourceEntity")
            InitClassEmpty(validator, Nothing)
            Me.ResourceId = resourceId
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Sub New(info As SerializationInfo, context As StreamingContext)
            MyBase.New(info, context)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                _customBankPropertyValueCollection = CType(info.GetValue("_customBankPropertyValueCollection", GetType(EntityCollection(Of CustomBankPropertyValueEntity))), EntityCollection(Of CustomBankPropertyValueEntity))
                _referencedResourceCollection = CType(info.GetValue("_referencedResourceCollection", GetType(EntityCollection(Of DependentResourceEntity))), EntityCollection(Of DependentResourceEntity))
                _dependentResourceCollection = CType(info.GetValue("_dependentResourceCollection", GetType(EntityCollection(Of DependentResourceEntity))), EntityCollection(Of DependentResourceEntity))
                _hiddenResourceCollection = CType(info.GetValue("_hiddenResourceCollection", GetType(EntityCollection(Of HiddenResourceEntity))), EntityCollection(Of HiddenResourceEntity))
                _resourceHistoryCollection = CType(info.GetValue("_resourceHistoryCollection", GetType(EntityCollection(Of ResourceHistoryEntity))), EntityCollection(Of ResourceHistoryEntity))
                _customBankPropertyCollectionViaCustomBankPropertyValue = CType(info.GetValue("_customBankPropertyCollectionViaCustomBankPropertyValue", GetType(EntityCollection(Of CustomBankPropertyEntity))), EntityCollection(Of CustomBankPropertyEntity))
                _bank = CType(info.GetValue("_bank", GetType(BankEntity)), BankEntity)
                If Not _bank Is Nothing Then
                    AddHandler _bank.AfterSave, AddressOf OnEntityAfterSave
                End If
                _state = CType(info.GetValue("_state", GetType(StateEntity)), StateEntity)
                If Not _state Is Nothing Then
                    AddHandler _state.AfterSave, AddressOf OnEntityAfterSave
                End If
                _createdByUser = CType(info.GetValue("_createdByUser", GetType(UserEntity)), UserEntity)
                If Not _createdByUser Is Nothing Then
                    AddHandler _createdByUser.AfterSave, AddressOf OnEntityAfterSave
                End If
                _modifiedByUser = CType(info.GetValue("_modifiedByUser", GetType(UserEntity)), UserEntity)
                If Not _modifiedByUser Is Nothing Then
                    AddHandler _modifiedByUser.AfterSave, AddressOf OnEntityAfterSave
                End If
                _resourceData = CType(info.GetValue("_resourceData", GetType(ResourceDataEntity)), ResourceDataEntity)
                If Not _resourceData Is Nothing Then
                    AddHandler _resourceData.AfterSave, AddressOf OnEntityAfterSave
                End If
                Me.FixupDeserialization(FieldInfoProviderSingleton.GetInstance())
            End If

        End Sub


        Protected Overrides Sub PerformDesyncSetupFKFieldChange(fieldIndex As Integer)
            Select Case CType(fieldIndex, ResourceFieldIndex)


                Case ResourceFieldIndex.BankId
                    DesetupSyncBank(True, False)



                Case ResourceFieldIndex.StateId
                    DesetupSyncState(True, False)

                Case ResourceFieldIndex.CreatedBy
                    DesetupSyncCreatedByUser(True, False)

                Case ResourceFieldIndex.ModifiedBy
                    DesetupSyncModifiedByUser(True, False)


                Case Else
                    MyBase.PerformDesyncSetupFKFieldChange(fieldIndex)
            End Select
        End Sub


        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub SetRelatedEntityProperty(propertyName As String, entity As IEntityCore)
            Select Case propertyName
                Case "Bank"
                    Me.Bank = CType(entity, BankEntity)
                Case "State"
                    Me.State = CType(entity, StateEntity)
                Case "CreatedByUser"
                    Me.CreatedByUser = CType(entity, UserEntity)
                Case "ModifiedByUser"
                    Me.ModifiedByUser = CType(entity, UserEntity)
                Case "CustomBankPropertyValueCollection"
                    Me.CustomBankPropertyValueCollection.Add(CType(entity, CustomBankPropertyValueEntity))
                Case "ReferencedResourceCollection"
                    Me.ReferencedResourceCollection.Add(CType(entity, DependentResourceEntity))
                Case "DependentResourceCollection"
                    Me.DependentResourceCollection.Add(CType(entity, DependentResourceEntity))
                Case "HiddenResourceCollection"
                    Me.HiddenResourceCollection.Add(CType(entity, HiddenResourceEntity))
                Case "ResourceHistoryCollection"
                    Me.ResourceHistoryCollection.Add(CType(entity, ResourceHistoryEntity))
                Case "CustomBankPropertyCollectionViaCustomBankPropertyValue"
                    Me.CustomBankPropertyCollectionViaCustomBankPropertyValue.IsReadOnly = False
                    Me.CustomBankPropertyCollectionViaCustomBankPropertyValue.Add(CType(entity, CustomBankPropertyEntity))
                    Me.CustomBankPropertyCollectionViaCustomBankPropertyValue.IsReadOnly = True
                Case "ResourceData"
                    Me.ResourceData = CType(entity, ResourceDataEntity)

                Case Else
                    Me.OnSetRelatedEntityProperty(propertyName, entity)
            End Select
        End Sub

        Protected Overrides Function GetRelationsForFieldOfType(fieldName As String) As RelationCollection
            Return ResourceEntity.GetRelationsForField(fieldName)
        End Function

        Friend Shared Function GetRelationsForField(fieldName As String) As RelationCollection
            Dim toReturn As New RelationCollection()
            Select Case fieldName
                Case "Bank"
                    toReturn.Add(ResourceEntity.Relations.BankEntityUsingBankId)
                Case "State"
                    toReturn.Add(ResourceEntity.Relations.StateEntityUsingStateId)
                Case "CreatedByUser"
                    toReturn.Add(ResourceEntity.Relations.UserEntityUsingCreatedBy)
                Case "ModifiedByUser"
                    toReturn.Add(ResourceEntity.Relations.UserEntityUsingModifiedBy)
                Case "CustomBankPropertyValueCollection"
                    toReturn.Add(ResourceEntity.Relations.CustomBankPropertyValueEntityUsingResourceId)
                Case "ReferencedResourceCollection"
                    toReturn.Add(ResourceEntity.Relations.DependentResourceEntityUsingDependentResourceId)
                Case "DependentResourceCollection"
                    toReturn.Add(ResourceEntity.Relations.DependentResourceEntityUsingResourceId)
                Case "HiddenResourceCollection"
                    toReturn.Add(ResourceEntity.Relations.HiddenResourceEntityUsingResourceId)
                Case "ResourceHistoryCollection"
                    toReturn.Add(ResourceEntity.Relations.ResourceHistoryEntityUsingResourceId)
                Case "CustomBankPropertyCollectionViaCustomBankPropertyValue"
                    toReturn.Add(ResourceEntity.Relations.CustomBankPropertyValueEntityUsingResourceId, "ResourceEntity__", "CustomBankPropertyValue_", JoinHint.None)
                    toReturn.Add(CustomBankPropertyValueEntity.Relations.CustomBankPropertyEntityUsingCustomBankPropertyId, "CustomBankPropertyValue_", String.Empty, JoinHint.None)
                Case "ResourceData"
                    toReturn.Add(ResourceEntity.Relations.ResourceDataEntityUsingResourceId)
                Case Else
            End Select
            Return toReturn
        End Function
#If Not CF Then
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Function CheckOneWayRelations(propertyName As String) As Boolean
            Dim numberOfOneWayRelations As Integer = 0 + 1 + 1 + 1
            Select Case propertyName
                Case Nothing
                    Return ((numberOfOneWayRelations > 0) Or MyBase.CheckOneWayRelations(Nothing))
                Case "Bank"
                    Return True
                Case "CreatedByUser"
                    Return True
                Case "ModifiedByUser"
                    Return True
                Case Else
                    Return MyBase.CheckOneWayRelations(propertyName)
            End Select
        End Function
#End If
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Sub SetRelatedEntity(relatedEntity As IEntityCore, fieldName As String)
            Select Case fieldName
                Case "Bank"
                    SetupSyncBank(relatedEntity)
                Case "State"
                    SetupSyncState(relatedEntity)
                Case "CreatedByUser"
                    SetupSyncCreatedByUser(relatedEntity)
                Case "ModifiedByUser"
                    SetupSyncModifiedByUser(relatedEntity)
                Case "CustomBankPropertyValueCollection"
                    Me.CustomBankPropertyValueCollection.Add(CType(relatedEntity, CustomBankPropertyValueEntity))
                Case "ReferencedResourceCollection"
                    Me.ReferencedResourceCollection.Add(CType(relatedEntity, DependentResourceEntity))
                Case "DependentResourceCollection"
                    Me.DependentResourceCollection.Add(CType(relatedEntity, DependentResourceEntity))
                Case "HiddenResourceCollection"
                    Me.HiddenResourceCollection.Add(CType(relatedEntity, HiddenResourceEntity))
                Case "ResourceHistoryCollection"
                    Me.ResourceHistoryCollection.Add(CType(relatedEntity, ResourceHistoryEntity))
                Case "ResourceData"
                    SetupSyncResourceData(relatedEntity)

                Case Else
            End Select
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub UnsetRelatedEntity(relatedEntity As IEntityCore, fieldName As String, signalRelatedEntityManyToOne As Boolean)
            Select Case fieldName
                Case "Bank"
                    DesetupSyncBank(False, True)
                Case "State"
                    DesetupSyncState(False, True)
                Case "CreatedByUser"
                    DesetupSyncCreatedByUser(False, True)
                Case "ModifiedByUser"
                    DesetupSyncModifiedByUser(False, True)
                Case "CustomBankPropertyValueCollection"
                    Me.PerformRelatedEntityRemoval(Me.CustomBankPropertyValueCollection, relatedEntity, signalRelatedEntityManyToOne)
                Case "ReferencedResourceCollection"
                    Me.PerformRelatedEntityRemoval(Me.ReferencedResourceCollection, relatedEntity, signalRelatedEntityManyToOne)
                Case "DependentResourceCollection"
                    Me.PerformRelatedEntityRemoval(Me.DependentResourceCollection, relatedEntity, signalRelatedEntityManyToOne)
                Case "HiddenResourceCollection"
                    Me.PerformRelatedEntityRemoval(Me.HiddenResourceCollection, relatedEntity, signalRelatedEntityManyToOne)
                Case "ResourceHistoryCollection"
                    Me.PerformRelatedEntityRemoval(Me.ResourceHistoryCollection, relatedEntity, signalRelatedEntityManyToOne)
                Case "ResourceData"
                    DesetupSyncResourceData(False, True)
                Case Else
            End Select
        End Sub

        Protected Overrides Function GetDependingRelatedEntities() As List(Of IEntity2)
            Dim toReturn As New List(Of IEntity2)()
            If Not _resourceData Is Nothing Then
                toReturn.Add(_resourceData)
            End If

            Return toReturn
        End Function

        Protected Overrides Function GetDependentRelatedEntities() As List(Of IEntity2)
            Dim toReturn As New List(Of IEntity2)()
            If Not _bank Is Nothing Then
                toReturn.Add(_bank)
            End If
            If Not _state Is Nothing Then
                toReturn.Add(_state)
            End If
            If Not _createdByUser Is Nothing Then
                toReturn.Add(_createdByUser)
            End If
            If Not _modifiedByUser Is Nothing Then
                toReturn.Add(_modifiedByUser)
            End If



            Return toReturn
        End Function

        Protected Overrides Function GetMemberEntityCollections() As List(Of IEntityCollection2)
            Dim toReturn As New List(Of IEntityCollection2)()
            toReturn.Add(Me.CustomBankPropertyValueCollection)
            toReturn.Add(Me.ReferencedResourceCollection)
            toReturn.Add(Me.DependentResourceCollection)
            toReturn.Add(Me.HiddenResourceCollection)
            toReturn.Add(Me.ResourceHistoryCollection)
            Return toReturn
        End Function

        Public Shared Function GetEntityTypeFilter() As IPredicateExpression
            Return InheritanceInfoProviderSingleton.GetInstance().GetEntityTypeFilter("ResourceEntity", False)
        End Function

        Public Shared Function GetEntityTypeFilter(negate As Boolean) As IPredicateExpression
            Return InheritanceInfoProviderSingleton.GetInstance().GetEntityTypeFilter("ResourceEntity", negate)
        End Function

        Protected Overrides Sub GetObjectData(info As SerializationInfo, context As StreamingContext)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                Dim value As IEntityCollection2 = Nothing
                Dim entityValue As IEntity2 = Nothing
                value = Nothing
                If (Not (_customBankPropertyValueCollection Is Nothing)) AndAlso (_customBankPropertyValueCollection.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _customBankPropertyValueCollection
                End If
                info.AddValue("_customBankPropertyValueCollection", value)
                value = Nothing
                If (Not (_referencedResourceCollection Is Nothing)) AndAlso (_referencedResourceCollection.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _referencedResourceCollection
                End If
                info.AddValue("_referencedResourceCollection", value)
                value = Nothing
                If (Not (_dependentResourceCollection Is Nothing)) AndAlso (_dependentResourceCollection.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _dependentResourceCollection
                End If
                info.AddValue("_dependentResourceCollection", value)
                value = Nothing
                If (Not (_hiddenResourceCollection Is Nothing)) AndAlso (_hiddenResourceCollection.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _hiddenResourceCollection
                End If
                info.AddValue("_hiddenResourceCollection", value)
                value = Nothing
                If (Not (_resourceHistoryCollection Is Nothing)) AndAlso (_resourceHistoryCollection.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _resourceHistoryCollection
                End If
                info.AddValue("_resourceHistoryCollection", value)
                value = Nothing
                If (Not (_customBankPropertyCollectionViaCustomBankPropertyValue Is Nothing)) AndAlso (_customBankPropertyCollectionViaCustomBankPropertyValue.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _customBankPropertyCollectionViaCustomBankPropertyValue
                End If
                info.AddValue("_customBankPropertyCollectionViaCustomBankPropertyValue", value)
                entityValue = Nothing
                If Not Me.MarkedForDeletion Then
                    entityValue = _bank
                End If
                info.AddValue("_bank", entityValue)
                entityValue = Nothing
                If Not Me.MarkedForDeletion Then
                    entityValue = _state
                End If
                info.AddValue("_state", entityValue)
                entityValue = Nothing
                If Not Me.MarkedForDeletion Then
                    entityValue = _createdByUser
                End If
                info.AddValue("_createdByUser", entityValue)
                entityValue = Nothing
                If Not Me.MarkedForDeletion Then
                    entityValue = _modifiedByUser
                End If
                info.AddValue("_modifiedByUser", entityValue)
                entityValue = Nothing
                If Not Me.MarkedForDeletion Then
                    entityValue = _resourceData
                End If
                info.AddValue("_resourceData", entityValue)
            End If

            MyBase.GetObjectData(info, context)
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Function CheckIfIsSubTypeOf(typeOfEntity As Integer) As Boolean
            Return InheritanceInfoProviderSingleton.GetInstance().CheckIfIsSubTypeOf("ResourceEntity", CType(typeOfEntity, Questify.Builder.Model.ContentModel.EntityType).ToString())
        End Function

        Protected Overrides Overloads Function GetAllRelations() As List(Of IEntityRelation)
            Return New ResourceRelations().GetAllRelations()
        End Function

        Public Overridable Function GetRelationInfoCustomBankPropertyValueCollection() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(CustomBankPropertyValueFields.ResourceId, Nothing, ComparisonOperator.Equal, Me.ResourceId))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoReferencedResourceCollection() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(DependentResourceFields.DependentResourceId, Nothing, ComparisonOperator.Equal, Me.ResourceId))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoDependentResourceCollection() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(DependentResourceFields.ResourceId, Nothing, ComparisonOperator.Equal, Me.ResourceId))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoHiddenResourceCollection() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(HiddenResourceFields.ResourceId, Nothing, ComparisonOperator.Equal, Me.ResourceId))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoResourceHistoryCollection() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(ResourceHistoryFields.ResourceId, Nothing, ComparisonOperator.Equal, Me.ResourceId))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoCustomBankPropertyCollectionViaCustomBankPropertyValue() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.Relations.AddRange(GetRelationsForFieldOfType("CustomBankPropertyCollectionViaCustomBankPropertyValue"))
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(ResourceFields.ResourceId, Nothing, ComparisonOperator.Equal, Me.ResourceId, "ResourceEntity__"))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoBank() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(BankFields.Id, Nothing, ComparisonOperator.Equal, Me.BankId))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoState() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(StateFields.StateId, Nothing, ComparisonOperator.Equal, Me.StateId))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoCreatedByUser() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(UserFields.Id, Nothing, ComparisonOperator.Equal, Me.CreatedBy))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoModifiedByUser() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(UserFields.Id, Nothing, ComparisonOperator.Equal, Me.ModifiedBy))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoResourceData() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(ResourceDataFields.ResourceId, Nothing, ComparisonOperator.Equal, Me.ResourceId))
            Return bucket
        End Function

        Protected Overrides Function CreateEntityFactory() As IEntityFactory2
            Return EntityFactoryCache2.GetEntityFactory(GetType(ResourceEntityFactory))
        End Function
#If Not CF Then
        Protected Overrides Sub AddToMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2))
            MyBase.AddToMemberEntityCollectionsQueue(collectionsQueue)
            collectionsQueue.Enqueue(_customBankPropertyValueCollection)
            collectionsQueue.Enqueue(_referencedResourceCollection)
            collectionsQueue.Enqueue(_dependentResourceCollection)
            collectionsQueue.Enqueue(_hiddenResourceCollection)
            collectionsQueue.Enqueue(_resourceHistoryCollection)
            collectionsQueue.Enqueue(_customBankPropertyCollectionViaCustomBankPropertyValue)
        End Sub

        Protected Overrides Sub GetFromMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2))
            MyBase.GetFromMemberEntityCollectionsQueue(collectionsQueue)
            _customBankPropertyValueCollection = CType(collectionsQueue.Dequeue(), EntityCollection(Of CustomBankPropertyValueEntity))
            _referencedResourceCollection = CType(collectionsQueue.Dequeue(), EntityCollection(Of DependentResourceEntity))
            _dependentResourceCollection = CType(collectionsQueue.Dequeue(), EntityCollection(Of DependentResourceEntity))
            _hiddenResourceCollection = CType(collectionsQueue.Dequeue(), EntityCollection(Of HiddenResourceEntity))
            _resourceHistoryCollection = CType(collectionsQueue.Dequeue(), EntityCollection(Of ResourceHistoryEntity))
            _customBankPropertyCollectionViaCustomBankPropertyValue = CType(collectionsQueue.Dequeue(), EntityCollection(Of CustomBankPropertyEntity))
        End Sub

        Protected Overrides Function HasPopulatedMemberEntityCollections() As Boolean
            If (Not _customBankPropertyValueCollection Is Nothing) Then
                Return True
            End If
            If (Not _referencedResourceCollection Is Nothing) Then
                Return True
            End If
            If (Not _dependentResourceCollection Is Nothing) Then
                Return True
            End If
            If (Not _hiddenResourceCollection Is Nothing) Then
                Return True
            End If
            If (Not _resourceHistoryCollection Is Nothing) Then
                Return True
            End If
            If (Not _customBankPropertyCollectionViaCustomBankPropertyValue Is Nothing) Then
                Return True
            End If
            Return MyBase.HasPopulatedMemberEntityCollections()
        End Function

        Protected Overrides Overloads Sub CreateMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2), requiredQueue As Queue(Of Boolean))
            MyBase.CreateMemberEntityCollectionsQueue(collectionsQueue, requiredQueue)
            Dim toAdd As IEntityCollection2 = Nothing
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of CustomBankPropertyValueEntity)(EntityFactoryCache2.GetEntityFactory(GetType(CustomBankPropertyValueEntityFactory)))
            Else
                toAdd = Nothing
            End If
            collectionsQueue.Enqueue(toAdd)
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of DependentResourceEntity)(EntityFactoryCache2.GetEntityFactory(GetType(DependentResourceEntityFactory)))
            Else
                toAdd = Nothing
            End If
            collectionsQueue.Enqueue(toAdd)
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of DependentResourceEntity)(EntityFactoryCache2.GetEntityFactory(GetType(DependentResourceEntityFactory)))
            Else
                toAdd = Nothing
            End If
            collectionsQueue.Enqueue(toAdd)
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of HiddenResourceEntity)(EntityFactoryCache2.GetEntityFactory(GetType(HiddenResourceEntityFactory)))
            Else
                toAdd = Nothing
            End If
            collectionsQueue.Enqueue(toAdd)
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of ResourceHistoryEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ResourceHistoryEntityFactory)))
            Else
                toAdd = Nothing
            End If
            collectionsQueue.Enqueue(toAdd)
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of CustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(CustomBankPropertyEntityFactory)))
            Else
                toAdd = Nothing
            End If
            collectionsQueue.Enqueue(toAdd)
        End Sub
#End If
        Protected Overrides Overloads Function GetRelatedData() As Dictionary(Of String, Object)
            Dim toReturn As New Dictionary(Of String, Object)()
            toReturn.Add("Bank", _bank)
            toReturn.Add("State", _state)
            toReturn.Add("CreatedByUser", _createdByUser)
            toReturn.Add("ModifiedByUser", _modifiedByUser)
            toReturn.Add("CustomBankPropertyValueCollection", _customBankPropertyValueCollection)
            toReturn.Add("ReferencedResourceCollection", _referencedResourceCollection)
            toReturn.Add("DependentResourceCollection", _dependentResourceCollection)
            toReturn.Add("HiddenResourceCollection", _hiddenResourceCollection)
            toReturn.Add("ResourceHistoryCollection", _resourceHistoryCollection)
            toReturn.Add("CustomBankPropertyCollectionViaCustomBankPropertyValue", _customBankPropertyCollectionViaCustomBankPropertyValue)
            toReturn.Add("ResourceData", _resourceData)
            Return toReturn
        End Function

        Private Sub InitClassMembers()
            PerformDependencyInjection()


            OnInitClassMembersComplete()
        End Sub

        Private Shared Sub SetupCustomPropertyHashtables()
            _customProperties = New Dictionary(Of String, String)()
            _fieldsCustomProperties = New Dictionary(Of String, Dictionary(Of String, String))()
            Dim fieldHashtable As Dictionary(Of String, String)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("ResourceId", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("Version", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("BankId", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("Name", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("Title", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("Description", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("StateId", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("CreationDate", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("CreatedBy", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("ModifiedDate", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("ModifiedBy", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("OriginalVersion", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("OriginalName", fieldHashtable)
        End Sub


        Private Sub DesetupSyncBank(signalRelatedEntity As Boolean, resetFKFields As Boolean)
            Me.PerformDesetupSyncRelatedEntity(_bank, AddressOf OnBankPropertyChanged, "Bank", Questify.Builder.Model.ContentModel.RelationClasses.StaticResourceRelations.BankEntityUsingBankIdStatic, True, signalRelatedEntity, "", resetFKFields, New Integer() {CInt(ResourceFieldIndex.BankId)})
            _bank = Nothing
        End Sub

        Private Sub SetupSyncBank(relatedEntity As IEntityCore)
            If Not _bank Is relatedEntity Then
                DesetupSyncBank(True, True)
                _bank = CType(relatedEntity, BankEntity)
                Me.PerformSetupSyncRelatedEntity(_bank, AddressOf OnBankPropertyChanged, "Bank", Questify.Builder.Model.ContentModel.RelationClasses.StaticResourceRelations.BankEntityUsingBankIdStatic, True, New String() {"BankName"})
            End If
        End Sub

        Private Sub OnBankPropertyChanged(sender As Object, e As PropertyChangedEventArgs)
            Select Case e.PropertyName
                Case "Name"
                    Me.OnPropertyChanged("BankName")
                Case Else
            End Select
        End Sub

        Private Sub DesetupSyncState(signalRelatedEntity As Boolean, resetFKFields As Boolean)
            Me.PerformDesetupSyncRelatedEntity(_state, AddressOf OnStatePropertyChanged, "State", Questify.Builder.Model.ContentModel.RelationClasses.StaticResourceRelations.StateEntityUsingStateIdStatic, True, signalRelatedEntity, "ResourceCollection", resetFKFields, New Integer() {CInt(ResourceFieldIndex.StateId)})
            _state = Nothing
        End Sub

        Private Sub SetupSyncState(relatedEntity As IEntityCore)
            If Not _state Is relatedEntity Then
                DesetupSyncState(True, True)
                _state = CType(relatedEntity, StateEntity)
                Me.PerformSetupSyncRelatedEntity(_state, AddressOf OnStatePropertyChanged, "State", Questify.Builder.Model.ContentModel.RelationClasses.StaticResourceRelations.StateEntityUsingStateIdStatic, True, New String() {"StateName"})
            End If
        End Sub

        Private Sub OnStatePropertyChanged(sender As Object, e As PropertyChangedEventArgs)
            Select Case e.PropertyName
                Case "Name"
                    Me.OnPropertyChanged("StateName")
                Case Else
            End Select
        End Sub

        Private Sub DesetupSyncCreatedByUser(signalRelatedEntity As Boolean, resetFKFields As Boolean)
            Me.PerformDesetupSyncRelatedEntity(_createdByUser, AddressOf OnCreatedByUserPropertyChanged, "CreatedByUser", Questify.Builder.Model.ContentModel.RelationClasses.StaticResourceRelations.UserEntityUsingCreatedByStatic, True, signalRelatedEntity, "", resetFKFields, New Integer() {CInt(ResourceFieldIndex.CreatedBy)})
            _createdByUser = Nothing
        End Sub

        Private Sub SetupSyncCreatedByUser(relatedEntity As IEntityCore)
            If Not _createdByUser Is relatedEntity Then
                DesetupSyncCreatedByUser(True, True)
                _createdByUser = CType(relatedEntity, UserEntity)
                Me.PerformSetupSyncRelatedEntity(_createdByUser, AddressOf OnCreatedByUserPropertyChanged, "CreatedByUser", Questify.Builder.Model.ContentModel.RelationClasses.StaticResourceRelations.UserEntityUsingCreatedByStatic, True, New String() {"CreatedByFullName"})
            End If
        End Sub

        Private Sub OnCreatedByUserPropertyChanged(sender As Object, e As PropertyChangedEventArgs)
            Select Case e.PropertyName
                Case "FullName"
                    Me.OnPropertyChanged("CreatedByFullName")
                Case Else
            End Select
        End Sub

        Private Sub DesetupSyncModifiedByUser(signalRelatedEntity As Boolean, resetFKFields As Boolean)
            Me.PerformDesetupSyncRelatedEntity(_modifiedByUser, AddressOf OnModifiedByUserPropertyChanged, "ModifiedByUser", Questify.Builder.Model.ContentModel.RelationClasses.StaticResourceRelations.UserEntityUsingModifiedByStatic, True, signalRelatedEntity, "", resetFKFields, New Integer() {CInt(ResourceFieldIndex.ModifiedBy)})
            _modifiedByUser = Nothing
        End Sub

        Private Sub SetupSyncModifiedByUser(relatedEntity As IEntityCore)
            If Not _modifiedByUser Is relatedEntity Then
                DesetupSyncModifiedByUser(True, True)
                _modifiedByUser = CType(relatedEntity, UserEntity)
                Me.PerformSetupSyncRelatedEntity(_modifiedByUser, AddressOf OnModifiedByUserPropertyChanged, "ModifiedByUser", Questify.Builder.Model.ContentModel.RelationClasses.StaticResourceRelations.UserEntityUsingModifiedByStatic, True, New String() {"ModifiedByFullName"})
            End If
        End Sub

        Private Sub OnModifiedByUserPropertyChanged(sender As Object, e As PropertyChangedEventArgs)
            Select Case e.PropertyName
                Case "FullName"
                    Me.OnPropertyChanged("ModifiedByFullName")
                Case Else
            End Select
        End Sub

        Private Sub DesetupSyncResourceData(signalRelatedEntity As Boolean, resetFKFields As Boolean)
            Me.PerformDesetupSyncRelatedEntity(_resourceData, AddressOf OnResourceDataPropertyChanged, "ResourceData", Questify.Builder.Model.ContentModel.RelationClasses.StaticResourceRelations.ResourceDataEntityUsingResourceIdStatic, False, signalRelatedEntity, "Resource", False, New Integer() {CInt(ResourceFieldIndex.ResourceId)})
            _resourceData = Nothing
        End Sub

        Private Sub SetupSyncResourceData(relatedEntity As IEntityCore)
            If Not _resourceData Is relatedEntity Then
                DesetupSyncResourceData(True, True)
                _resourceData = CType(relatedEntity, ResourceDataEntity)
                Me.PerformSetupSyncRelatedEntity(_resourceData, AddressOf OnResourceDataPropertyChanged, "ResourceData", Questify.Builder.Model.ContentModel.RelationClasses.StaticResourceRelations.ResourceDataEntityUsingResourceIdStatic, False, New String() {})
            End If
        End Sub

        Private Sub OnResourceDataPropertyChanged(sender As Object, e As PropertyChangedEventArgs)
            Select Case e.PropertyName

                Case Else
            End Select
        End Sub


        Private Sub InitClassEmpty(validator As IValidator, fields As IEntityFields2)
            OnInitializing()
            If fields Is Nothing Then
                Me.Fields = CreateFields()
            Else
                Me.Fields = fields
            End If
            Me.Validator = validator
            InitClassMembers()



            OnInitialized()
        End Sub

        Public Shared ReadOnly Property Relations() As ResourceRelations
            Get
                Return New ResourceRelations()
            End Get
        End Property

        Public Shared ReadOnly Property CustomProperties() As Dictionary(Of String, String)
            Get
                Return _customProperties
            End Get
        End Property


        Public Shared ReadOnly Property PrefetchPathCustomBankPropertyValueCollection() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(Of CustomBankPropertyValueEntity)(EntityFactoryCache2.GetEntityFactory(GetType(CustomBankPropertyValueEntityFactory))), _
                    CType(GetRelationsForField("CustomBankPropertyValueCollection")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.ResourceEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.CustomBankPropertyValueEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "CustomBankPropertyValueCollection", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathReferencedResourceCollection() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(Of DependentResourceEntity)(EntityFactoryCache2.GetEntityFactory(GetType(DependentResourceEntityFactory))), _
                    CType(GetRelationsForField("ReferencedResourceCollection")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.ResourceEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.DependentResourceEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "ReferencedResourceCollection", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathDependentResourceCollection() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(Of DependentResourceEntity)(EntityFactoryCache2.GetEntityFactory(GetType(DependentResourceEntityFactory))), _
                    CType(GetRelationsForField("DependentResourceCollection")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.ResourceEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.DependentResourceEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "DependentResourceCollection", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathHiddenResourceCollection() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(Of HiddenResourceEntity)(EntityFactoryCache2.GetEntityFactory(GetType(HiddenResourceEntityFactory))), _
                    CType(GetRelationsForField("HiddenResourceCollection")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.ResourceEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.HiddenResourceEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "HiddenResourceCollection", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathResourceHistoryCollection() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(Of ResourceHistoryEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ResourceHistoryEntityFactory))), _
                    CType(GetRelationsForField("ResourceHistoryCollection")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.ResourceEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.ResourceHistoryEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "ResourceHistoryCollection", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathCustomBankPropertyCollectionViaCustomBankPropertyValue() As IPrefetchPathElement2
            Get
                Dim intermediateRelation As IEntityRelation = ResourceEntity.Relations.CustomBankPropertyValueEntityUsingResourceId
                intermediateRelation.SetAliases(String.Empty, "CustomBankPropertyValue_")
                Return New PrefetchPathElement2(New EntityCollection(Of CustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(CustomBankPropertyEntityFactory))), _
                    intermediateRelation, CType(Questify.Builder.Model.ContentModel.EntityType.ResourceEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.CustomBankPropertyEntity, Integer), 0, Nothing, Nothing, GetRelationsForField("CustomBankPropertyCollectionViaCustomBankPropertyValue"), Nothing, "CustomBankPropertyCollectionViaCustomBankPropertyValue", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathBank() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(EntityFactoryCache2.GetEntityFactory(GetType(BankEntityFactory))), _
                    CType(GetRelationsForField("Bank")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.ResourceEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.BankEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "Bank", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathState() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(EntityFactoryCache2.GetEntityFactory(GetType(StateEntityFactory))), _
                    CType(GetRelationsForField("State")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.ResourceEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.StateEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "State", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathCreatedByUser() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(EntityFactoryCache2.GetEntityFactory(GetType(UserEntityFactory))), _
                    CType(GetRelationsForField("CreatedByUser")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.ResourceEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.UserEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "CreatedByUser", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathModifiedByUser() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(EntityFactoryCache2.GetEntityFactory(GetType(UserEntityFactory))), _
                    CType(GetRelationsForField("ModifiedByUser")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.ResourceEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.UserEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "ModifiedByUser", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathResourceData() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(EntityFactoryCache2.GetEntityFactory(GetType(ResourceDataEntityFactory))), _
                    CType(GetRelationsForField("ResourceData")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.ResourceEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.ResourceDataEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "ResourceData", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToOne)
            End Get
        End Property

        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property CustomPropertiesOfType() As Dictionary(Of String, String)
            Get
                Return ResourceEntity.CustomProperties
            End Get
        End Property

        Public Shared ReadOnly Property FieldsCustomProperties() As Dictionary(Of String, Dictionary(Of String, String))
            Get
                Return _fieldsCustomProperties
            End Get
        End Property

        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property FieldsCustomPropertiesOfType() As Dictionary(Of String, Dictionary(Of String, String))
            Get
                Return ResourceEntity.FieldsCustomProperties
            End Get
        End Property

        Public Overridable Property [ResourceId]() As System.Guid
            Get
                Return CType(GetValue(CInt(ResourceFieldIndex.ResourceId), True), System.Guid)
            End Get
            Set
                SetValue(CInt(ResourceFieldIndex.ResourceId), value)
            End Set
        End Property
        Public Overridable Property [Version]() As System.String
            Get
                Return CType(GetValue(CInt(ResourceFieldIndex.Version), True), System.String)
            End Get
            Set
                SetValue(CInt(ResourceFieldIndex.Version), value)
            End Set
        End Property
        Public Overridable Property [BankId]() As System.Int32
            Get
                Return CType(GetValue(CInt(ResourceFieldIndex.BankId), True), System.Int32)
            End Get
            Set
                SetValue(CInt(ResourceFieldIndex.BankId), value)
            End Set
        End Property
        Public Overridable Property [Name]() As System.String
            Get
                Return CType(GetValue(CInt(ResourceFieldIndex.Name), True), System.String)
            End Get
            Set
                SetValue(CInt(ResourceFieldIndex.Name), value)
            End Set
        End Property
        Public Overridable Property [Title]() As System.String
            Get
                Return CType(GetValue(CInt(ResourceFieldIndex.Title), True), System.String)
            End Get
            Set
                SetValue(CInt(ResourceFieldIndex.Title), value)
            End Set
        End Property
        Public Overridable Property [Description]() As System.String
            Get
                Return CType(GetValue(CInt(ResourceFieldIndex.Description), True), System.String)
            End Get
            Set
                SetValue(CInt(ResourceFieldIndex.Description), value)
            End Set
        End Property
        Public Overridable Property [StateId]() As Nullable(Of System.Int32)
            Get
                Return CType(GetValue(CInt(ResourceFieldIndex.StateId), False), Nullable(Of System.Int32))
            End Get
            Set
                SetValue(CInt(ResourceFieldIndex.StateId), value)
            End Set
        End Property
        Public Overridable Property [CreationDate]() As System.DateTime
            Get
                Return CType(GetValue(CInt(ResourceFieldIndex.CreationDate), True), System.DateTime)
            End Get
            Set
                SetValue(CInt(ResourceFieldIndex.CreationDate), value)
            End Set
        End Property
        Public Overridable Property [CreatedBy]() As System.Int32
            Get
                Return CType(GetValue(CInt(ResourceFieldIndex.CreatedBy), True), System.Int32)
            End Get
            Set
                SetValue(CInt(ResourceFieldIndex.CreatedBy), value)
            End Set
        End Property
        Public Overridable Property [ModifiedDate]() As System.DateTime
            Get
                Return CType(GetValue(CInt(ResourceFieldIndex.ModifiedDate), True), System.DateTime)
            End Get
            Set
                SetValue(CInt(ResourceFieldIndex.ModifiedDate), value)
            End Set
        End Property
        Public Overridable Property [ModifiedBy]() As System.Int32
            Get
                Return CType(GetValue(CInt(ResourceFieldIndex.ModifiedBy), True), System.Int32)
            End Get
            Set
                SetValue(CInt(ResourceFieldIndex.ModifiedBy), value)
            End Set
        End Property
        Public Overridable Property [OriginalVersion]() As System.String
            Get
                Return CType(GetValue(CInt(ResourceFieldIndex.OriginalVersion), True), System.String)
            End Get
            Set
                SetValue(CInt(ResourceFieldIndex.OriginalVersion), value)
            End Set
        End Property
        Public Overridable Property [OriginalName]() As System.String
            Get
                Return CType(GetValue(CInt(ResourceFieldIndex.OriginalName), True), System.String)
            End Get
            Set
                SetValue(CInt(ResourceFieldIndex.OriginalName), value)
            End Set
        End Property

        <TypeContainedAttribute(GetType(CustomBankPropertyValueEntity))> _
        Public Overridable ReadOnly Property [CustomBankPropertyValueCollection]() As EntityCollection(Of CustomBankPropertyValueEntity)
            Get
                If _customBankPropertyValueCollection Is Nothing Then
                    _customBankPropertyValueCollection = New EntityCollection(Of CustomBankPropertyValueEntity)(EntityFactoryCache2.GetEntityFactory(GetType(CustomBankPropertyValueEntityFactory)))
                    _customBankPropertyValueCollection.ActiveContext = Me.ActiveContext
                    _customBankPropertyValueCollection.SetContainingEntityInfo(Me, "Resource")
                End If
                Return _customBankPropertyValueCollection
            End Get
        End Property

        <TypeContainedAttribute(GetType(DependentResourceEntity))> _
        Public Overridable ReadOnly Property [ReferencedResourceCollection]() As EntityCollection(Of DependentResourceEntity)
            Get
                If _referencedResourceCollection Is Nothing Then
                    _referencedResourceCollection = New EntityCollection(Of DependentResourceEntity)(EntityFactoryCache2.GetEntityFactory(GetType(DependentResourceEntityFactory)))
                    _referencedResourceCollection.ActiveContext = Me.ActiveContext
                    _referencedResourceCollection.SetContainingEntityInfo(Me, "DependentResource")
                End If
                Return _referencedResourceCollection
            End Get
        End Property

        <TypeContainedAttribute(GetType(DependentResourceEntity))> _
        Public Overridable ReadOnly Property [DependentResourceCollection]() As EntityCollection(Of DependentResourceEntity)
            Get
                If _dependentResourceCollection Is Nothing Then
                    _dependentResourceCollection = New EntityCollection(Of DependentResourceEntity)(EntityFactoryCache2.GetEntityFactory(GetType(DependentResourceEntityFactory)))
                    _dependentResourceCollection.ActiveContext = Me.ActiveContext
                    _dependentResourceCollection.SetContainingEntityInfo(Me, "Resource")
                End If
                Return _dependentResourceCollection
            End Get
        End Property

        <TypeContainedAttribute(GetType(HiddenResourceEntity))> _
        Public Overridable ReadOnly Property [HiddenResourceCollection]() As EntityCollection(Of HiddenResourceEntity)
            Get
                If _hiddenResourceCollection Is Nothing Then
                    _hiddenResourceCollection = New EntityCollection(Of HiddenResourceEntity)(EntityFactoryCache2.GetEntityFactory(GetType(HiddenResourceEntityFactory)))
                    _hiddenResourceCollection.ActiveContext = Me.ActiveContext
                    _hiddenResourceCollection.SetContainingEntityInfo(Me, "Resource")
                End If
                Return _hiddenResourceCollection
            End Get
        End Property

        <TypeContainedAttribute(GetType(ResourceHistoryEntity))> _
        Public Overridable ReadOnly Property [ResourceHistoryCollection]() As EntityCollection(Of ResourceHistoryEntity)
            Get
                If _resourceHistoryCollection Is Nothing Then
                    _resourceHistoryCollection = New EntityCollection(Of ResourceHistoryEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ResourceHistoryEntityFactory)))
                    _resourceHistoryCollection.ActiveContext = Me.ActiveContext
                    _resourceHistoryCollection.SetContainingEntityInfo(Me, "Resource")
                End If
                Return _resourceHistoryCollection
            End Get
        End Property

        <TypeContainedAttribute(GetType(CustomBankPropertyEntity))> _
        Public Overridable ReadOnly Property [CustomBankPropertyCollectionViaCustomBankPropertyValue]() As EntityCollection(Of CustomBankPropertyEntity)
            Get
                If _customBankPropertyCollectionViaCustomBankPropertyValue Is Nothing Then
                    _customBankPropertyCollectionViaCustomBankPropertyValue = New EntityCollection(Of CustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(CustomBankPropertyEntityFactory)))
                    _customBankPropertyCollectionViaCustomBankPropertyValue.ActiveContext = Me.ActiveContext
                    _customBankPropertyCollectionViaCustomBankPropertyValue.IsReadOnly = True
                    CType(_customBankPropertyCollectionViaCustomBankPropertyValue, IEntityCollectionCore).IsForMN = True
                End If
                Return _customBankPropertyCollectionViaCustomBankPropertyValue
            End Get
        End Property

        <Browsable(True)> _
        Public Overridable Property [Bank]() As BankEntity
            Get
                Return _bank
            End Get
            Set
                If MyBase.IsDeserializing Then
                    SetupSyncBank(value)
                Else
                    SetSingleRelatedEntityNavigator(value, "", "Bank", _bank, False)
                End If
            End Set
        End Property

        <Browsable(True)> _
        Public Overridable Property [State]() As StateEntity
            Get
                Return _state
            End Get
            Set
                If MyBase.IsDeserializing Then
                    SetupSyncState(value)
                Else
                    SetSingleRelatedEntityNavigator(value, "ResourceCollection", "State", _state, True)
                End If
            End Set
        End Property

        <Browsable(True)> _
        Public Overridable Property [CreatedByUser]() As UserEntity
            Get
                Return _createdByUser
            End Get
            Set
                If MyBase.IsDeserializing Then
                    SetupSyncCreatedByUser(value)
                Else
                    SetSingleRelatedEntityNavigator(value, "", "CreatedByUser", _createdByUser, False)
                End If
            End Set
        End Property

        <Browsable(True)> _
        Public Overridable Property [ModifiedByUser]() As UserEntity
            Get
                Return _modifiedByUser
            End Get
            Set
                If MyBase.IsDeserializing Then
                    SetupSyncModifiedByUser(value)
                Else
                    SetSingleRelatedEntityNavigator(value, "", "ModifiedByUser", _modifiedByUser, False)
                End If
            End Set
        End Property

        <Browsable(True)> _
        Public Overridable Property [ResourceData]() As ResourceDataEntity
            Get
                Return _resourceData
            End Get
            Set
                If MyBase.IsDeserializing Then
                    SetupSyncResourceData(value)
                    CallSetRelatedEntityDuringDeserialization(value, "Resource")
                Else
                    If value Is Nothing Then
                        Dim raisePropertyChanged As Boolean = Not (_resourceData Is Nothing)
                        DesetupSyncResourceData(True, True)
                        If raisePropertyChanged Then
                            OnPropertyChanged("ResourceData")
                        End If
                    Else
                        If Not _resourceData Is value Then
                            CType(value, IEntity2).SetRelatedEntity(Me, "Resource")
                            SetupSyncResourceData(value)
                        End If
                    End If
                End If
            End Set
        End Property

        Public Overridable ReadOnly Property BankName As System.String
            Get
                If Me.Bank Is Nothing Then
                    Return CType(TypeDefaultValue.GetDefaultValue(GetType(System.String)), System.String)
                Else
                    Return Me.Bank.Name
                End If
            End Get

        End Property

        Public Overridable ReadOnly Property CreatedByFullName As System.String
            Get
                If Me.CreatedByUser Is Nothing Then
                    Return CType(TypeDefaultValue.GetDefaultValue(GetType(System.String)), System.String)
                Else
                    Return Me.CreatedByUser.FullName
                End If
            End Get

        End Property

        Public Overridable ReadOnly Property ModifiedByFullName As System.String
            Get
                If Me.ModifiedByUser Is Nothing Then
                    Return CType(TypeDefaultValue.GetDefaultValue(GetType(System.String)), System.String)
                Else
                    Return Me.ModifiedByUser.FullName
                End If
            End Get

        End Property

        Public Overridable ReadOnly Property StateName As System.String
            Get
                If Me.State Is Nothing Then
                    Return CType(TypeDefaultValue.GetDefaultValue(GetType(System.String)), System.String)
                Else
                    Return Me.State.Name
                End If
            End Get

        End Property

        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property LLBLGenProIsInHierarchyOfType() As InheritanceHierarchyType
            Get
                Return InheritanceHierarchyType.TargetPerEntity
            End Get
        End Property

        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property LLBLGenProIsSubType As Boolean
            Get
                Return False
            End Get
        End Property

        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property LLBLGenProEntityTypeValue As Integer
            Get
                Return CInt(Questify.Builder.Model.ContentModel.EntityType.ResourceEntity)
            End Get
        End Property






    End Class
End Namespace
