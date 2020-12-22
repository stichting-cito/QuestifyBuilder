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
    Public Class StateEntity
        Inherits CommonEntityBase



        Private WithEvents _customBankPropertyCollection As EntityCollection(Of CustomBankPropertyEntity)
        Private WithEvents _resourceCollection As EntityCollection(Of ResourceEntity)
        Private WithEvents _stateActionCollection As EntityCollection(Of StateActionEntity)
        Private WithEvents _actionCollectionViaStateAction As EntityCollection(Of ActionEntity)
        Private WithEvents _bankCollectionViaResource As EntityCollection(Of BankEntity)
        Private WithEvents _bankCollectionViaCustomBankProperty As EntityCollection(Of BankEntity)
        Private WithEvents _userCollectionViaResource As EntityCollection(Of UserEntity)
        Private WithEvents _userCollectionViaResource_ As EntityCollection(Of UserEntity)
        Private WithEvents _userCollectionViaCustomBankProperty As EntityCollection(Of UserEntity)
        Private WithEvents _userCollectionViaCustomBankProperty_ As EntityCollection(Of UserEntity)



        Private Shared _customProperties As Dictionary(Of String, String)
        Private Shared _fieldsCustomProperties As Dictionary(Of String, Dictionary(Of String, String))

        Public NotInheritable Class MemberNames
            Private Sub New()
            End Sub
            Public Shared ReadOnly [CustomBankPropertyCollection] As String = "CustomBankPropertyCollection"
            Public Shared ReadOnly [ResourceCollection] As String = "ResourceCollection"
            Public Shared ReadOnly [StateActionCollection] As String = "StateActionCollection"
            Public Shared ReadOnly [ActionCollectionViaStateAction] As String = "ActionCollectionViaStateAction"
            Public Shared ReadOnly [BankCollectionViaResource] As String = "BankCollectionViaResource"
            Public Shared ReadOnly [BankCollectionViaCustomBankProperty] As String = "BankCollectionViaCustomBankProperty"
            Public Shared ReadOnly [UserCollectionViaResource] As String = "UserCollectionViaResource"
            Public Shared ReadOnly [UserCollectionViaResource_] As String = "UserCollectionViaResource_"
            Public Shared ReadOnly [UserCollectionViaCustomBankProperty] As String = "UserCollectionViaCustomBankProperty"
            Public Shared ReadOnly [UserCollectionViaCustomBankProperty_] As String = "UserCollectionViaCustomBankProperty_"
        End Class

        Shared Sub New()
            SetupCustomPropertyHashtables()
        End Sub

        Public Sub New()
            MyBase.New("StateEntity")
            InitClassEmpty(Nothing, Nothing)
        End Sub

        Public Sub New(fields As IEntityFields2)
            MyBase.New("StateEntity")
            InitClassEmpty(Nothing, fields)
        End Sub

        Public Sub New(validator As IValidator)
            MyBase.New("StateEntity")
            InitClassEmpty(validator, Nothing)
        End Sub

        Public Sub New(stateId As System.Int32)
            MyBase.New("StateEntity")
            InitClassEmpty(Nothing, Nothing)
            Me.StateId = stateId
        End Sub

        Public Sub New(stateId As System.Int32, validator As IValidator)
            MyBase.New("StateEntity")
            InitClassEmpty(validator, Nothing)
            Me.StateId = stateId
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Sub New(info As SerializationInfo, context As StreamingContext)
            MyBase.New(info, context)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                _customBankPropertyCollection = CType(info.GetValue("_customBankPropertyCollection", GetType(EntityCollection(Of CustomBankPropertyEntity))), EntityCollection(Of CustomBankPropertyEntity))
                _resourceCollection = CType(info.GetValue("_resourceCollection", GetType(EntityCollection(Of ResourceEntity))), EntityCollection(Of ResourceEntity))
                _stateActionCollection = CType(info.GetValue("_stateActionCollection", GetType(EntityCollection(Of StateActionEntity))), EntityCollection(Of StateActionEntity))
                _actionCollectionViaStateAction = CType(info.GetValue("_actionCollectionViaStateAction", GetType(EntityCollection(Of ActionEntity))), EntityCollection(Of ActionEntity))
                _bankCollectionViaResource = CType(info.GetValue("_bankCollectionViaResource", GetType(EntityCollection(Of BankEntity))), EntityCollection(Of BankEntity))
                _bankCollectionViaCustomBankProperty = CType(info.GetValue("_bankCollectionViaCustomBankProperty", GetType(EntityCollection(Of BankEntity))), EntityCollection(Of BankEntity))
                _userCollectionViaResource = CType(info.GetValue("_userCollectionViaResource", GetType(EntityCollection(Of UserEntity))), EntityCollection(Of UserEntity))
                _userCollectionViaResource_ = CType(info.GetValue("_userCollectionViaResource_", GetType(EntityCollection(Of UserEntity))), EntityCollection(Of UserEntity))
                _userCollectionViaCustomBankProperty = CType(info.GetValue("_userCollectionViaCustomBankProperty", GetType(EntityCollection(Of UserEntity))), EntityCollection(Of UserEntity))
                _userCollectionViaCustomBankProperty_ = CType(info.GetValue("_userCollectionViaCustomBankProperty_", GetType(EntityCollection(Of UserEntity))), EntityCollection(Of UserEntity))
                Me.FixupDeserialization(FieldInfoProviderSingleton.GetInstance())
            End If

        End Sub



        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub SetRelatedEntityProperty(propertyName As String, entity As IEntityCore)
            Select Case propertyName
                Case "CustomBankPropertyCollection"
                    Me.CustomBankPropertyCollection.Add(CType(entity, CustomBankPropertyEntity))
                Case "ResourceCollection"
                    Me.ResourceCollection.Add(CType(entity, ResourceEntity))
                Case "StateActionCollection"
                    Me.StateActionCollection.Add(CType(entity, StateActionEntity))
                Case "ActionCollectionViaStateAction"
                    Me.ActionCollectionViaStateAction.IsReadOnly = False
                    Me.ActionCollectionViaStateAction.Add(CType(entity, ActionEntity))
                    Me.ActionCollectionViaStateAction.IsReadOnly = True
                Case "BankCollectionViaResource"
                    Me.BankCollectionViaResource.IsReadOnly = False
                    Me.BankCollectionViaResource.Add(CType(entity, BankEntity))
                    Me.BankCollectionViaResource.IsReadOnly = True
                Case "BankCollectionViaCustomBankProperty"
                    Me.BankCollectionViaCustomBankProperty.IsReadOnly = False
                    Me.BankCollectionViaCustomBankProperty.Add(CType(entity, BankEntity))
                    Me.BankCollectionViaCustomBankProperty.IsReadOnly = True
                Case "UserCollectionViaResource"
                    Me.UserCollectionViaResource.IsReadOnly = False
                    Me.UserCollectionViaResource.Add(CType(entity, UserEntity))
                    Me.UserCollectionViaResource.IsReadOnly = True
                Case "UserCollectionViaResource_"
                    Me.UserCollectionViaResource_.IsReadOnly = False
                    Me.UserCollectionViaResource_.Add(CType(entity, UserEntity))
                    Me.UserCollectionViaResource_.IsReadOnly = True
                Case "UserCollectionViaCustomBankProperty"
                    Me.UserCollectionViaCustomBankProperty.IsReadOnly = False
                    Me.UserCollectionViaCustomBankProperty.Add(CType(entity, UserEntity))
                    Me.UserCollectionViaCustomBankProperty.IsReadOnly = True
                Case "UserCollectionViaCustomBankProperty_"
                    Me.UserCollectionViaCustomBankProperty_.IsReadOnly = False
                    Me.UserCollectionViaCustomBankProperty_.Add(CType(entity, UserEntity))
                    Me.UserCollectionViaCustomBankProperty_.IsReadOnly = True

                Case Else
                    Me.OnSetRelatedEntityProperty(propertyName, entity)
            End Select
        End Sub

        Protected Overrides Function GetRelationsForFieldOfType(fieldName As String) As RelationCollection
            Return StateEntity.GetRelationsForField(fieldName)
        End Function

        Friend Shared Function GetRelationsForField(fieldName As String) As RelationCollection
            Dim toReturn As New RelationCollection()
            Select Case fieldName
                Case "CustomBankPropertyCollection"
                    toReturn.Add(StateEntity.Relations.CustomBankPropertyEntityUsingStateId)
                Case "ResourceCollection"
                    toReturn.Add(StateEntity.Relations.ResourceEntityUsingStateId)
                Case "StateActionCollection"
                    toReturn.Add(StateEntity.Relations.StateActionEntityUsingStateId)
                Case "ActionCollectionViaStateAction"
                    toReturn.Add(StateEntity.Relations.StateActionEntityUsingStateId, "StateEntity__", "StateAction_", JoinHint.None)
                    toReturn.Add(StateActionEntity.Relations.ActionEntityUsingActionId, "StateAction_", String.Empty, JoinHint.None)
                Case "BankCollectionViaResource"
                    toReturn.Add(StateEntity.Relations.ResourceEntityUsingStateId, "StateEntity__", "Resource_", JoinHint.None)
                    toReturn.Add(ResourceEntity.Relations.BankEntityUsingBankId, "Resource_", String.Empty, JoinHint.None)
                Case "BankCollectionViaCustomBankProperty"
                    toReturn.Add(StateEntity.Relations.CustomBankPropertyEntityUsingStateId, "StateEntity__", "CustomBankProperty_", JoinHint.None)
                    toReturn.Add(CustomBankPropertyEntity.Relations.BankEntityUsingBankId, "CustomBankProperty_", String.Empty, JoinHint.None)
                Case "UserCollectionViaResource"
                    toReturn.Add(StateEntity.Relations.ResourceEntityUsingStateId, "StateEntity__", "Resource_", JoinHint.None)
                    toReturn.Add(ResourceEntity.Relations.UserEntityUsingCreatedBy, "Resource_", String.Empty, JoinHint.None)
                Case "UserCollectionViaResource_"
                    toReturn.Add(StateEntity.Relations.ResourceEntityUsingStateId, "StateEntity__", "Resource_", JoinHint.None)
                    toReturn.Add(ResourceEntity.Relations.UserEntityUsingModifiedBy, "Resource_", String.Empty, JoinHint.None)
                Case "UserCollectionViaCustomBankProperty"
                    toReturn.Add(StateEntity.Relations.CustomBankPropertyEntityUsingStateId, "StateEntity__", "CustomBankProperty_", JoinHint.None)
                    toReturn.Add(CustomBankPropertyEntity.Relations.UserEntityUsingCreatedBy, "CustomBankProperty_", String.Empty, JoinHint.None)
                Case "UserCollectionViaCustomBankProperty_"
                    toReturn.Add(StateEntity.Relations.CustomBankPropertyEntityUsingStateId, "StateEntity__", "CustomBankProperty_", JoinHint.None)
                    toReturn.Add(CustomBankPropertyEntity.Relations.UserEntityUsingModifiedBy, "CustomBankProperty_", String.Empty, JoinHint.None)
                Case Else
            End Select
            Return toReturn
        End Function
#If Not CF Then
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Function CheckOneWayRelations(propertyName As String) As Boolean
            Dim numberOfOneWayRelations As Integer = 0
            Select Case propertyName
                Case Nothing
                    Return ((numberOfOneWayRelations > 0) Or MyBase.CheckOneWayRelations(Nothing))
                Case Else
                    Return MyBase.CheckOneWayRelations(propertyName)
            End Select
        End Function
#End If
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Sub SetRelatedEntity(relatedEntity As IEntityCore, fieldName As String)
            Select Case fieldName
                Case "CustomBankPropertyCollection"
                    Me.CustomBankPropertyCollection.Add(CType(relatedEntity, CustomBankPropertyEntity))
                Case "ResourceCollection"
                    Me.ResourceCollection.Add(CType(relatedEntity, ResourceEntity))
                Case "StateActionCollection"
                    Me.StateActionCollection.Add(CType(relatedEntity, StateActionEntity))

                Case Else
            End Select
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub UnsetRelatedEntity(relatedEntity As IEntityCore, fieldName As String, signalRelatedEntityManyToOne As Boolean)
            Select Case fieldName
                Case "CustomBankPropertyCollection"
                    Me.PerformRelatedEntityRemoval(Me.CustomBankPropertyCollection, relatedEntity, signalRelatedEntityManyToOne)
                Case "ResourceCollection"
                    Me.PerformRelatedEntityRemoval(Me.ResourceCollection, relatedEntity, signalRelatedEntityManyToOne)
                Case "StateActionCollection"
                    Me.PerformRelatedEntityRemoval(Me.StateActionCollection, relatedEntity, signalRelatedEntityManyToOne)
                Case Else
            End Select
        End Sub

        Protected Overrides Function GetDependingRelatedEntities() As List(Of IEntity2)
            Dim toReturn As New List(Of IEntity2)()
            Return toReturn
        End Function

        Protected Overrides Function GetDependentRelatedEntities() As List(Of IEntity2)
            Dim toReturn As New List(Of IEntity2)()
            Return toReturn
        End Function

        Protected Overrides Function GetMemberEntityCollections() As List(Of IEntityCollection2)
            Dim toReturn As New List(Of IEntityCollection2)()
            toReturn.Add(Me.CustomBankPropertyCollection)
            toReturn.Add(Me.ResourceCollection)
            toReturn.Add(Me.StateActionCollection)
            Return toReturn
        End Function


        Protected Overrides Sub GetObjectData(info As SerializationInfo, context As StreamingContext)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                Dim value As IEntityCollection2 = Nothing
                Dim entityValue As IEntity2 = Nothing
                value = Nothing
                If (Not (_customBankPropertyCollection Is Nothing)) AndAlso (_customBankPropertyCollection.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _customBankPropertyCollection
                End If
                info.AddValue("_customBankPropertyCollection", value)
                value = Nothing
                If (Not (_resourceCollection Is Nothing)) AndAlso (_resourceCollection.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _resourceCollection
                End If
                info.AddValue("_resourceCollection", value)
                value = Nothing
                If (Not (_stateActionCollection Is Nothing)) AndAlso (_stateActionCollection.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _stateActionCollection
                End If
                info.AddValue("_stateActionCollection", value)
                value = Nothing
                If (Not (_actionCollectionViaStateAction Is Nothing)) AndAlso (_actionCollectionViaStateAction.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _actionCollectionViaStateAction
                End If
                info.AddValue("_actionCollectionViaStateAction", value)
                value = Nothing
                If (Not (_bankCollectionViaResource Is Nothing)) AndAlso (_bankCollectionViaResource.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _bankCollectionViaResource
                End If
                info.AddValue("_bankCollectionViaResource", value)
                value = Nothing
                If (Not (_bankCollectionViaCustomBankProperty Is Nothing)) AndAlso (_bankCollectionViaCustomBankProperty.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _bankCollectionViaCustomBankProperty
                End If
                info.AddValue("_bankCollectionViaCustomBankProperty", value)
                value = Nothing
                If (Not (_userCollectionViaResource Is Nothing)) AndAlso (_userCollectionViaResource.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _userCollectionViaResource
                End If
                info.AddValue("_userCollectionViaResource", value)
                value = Nothing
                If (Not (_userCollectionViaResource_ Is Nothing)) AndAlso (_userCollectionViaResource_.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _userCollectionViaResource_
                End If
                info.AddValue("_userCollectionViaResource_", value)
                value = Nothing
                If (Not (_userCollectionViaCustomBankProperty Is Nothing)) AndAlso (_userCollectionViaCustomBankProperty.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _userCollectionViaCustomBankProperty
                End If
                info.AddValue("_userCollectionViaCustomBankProperty", value)
                value = Nothing
                If (Not (_userCollectionViaCustomBankProperty_ Is Nothing)) AndAlso (_userCollectionViaCustomBankProperty_.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _userCollectionViaCustomBankProperty_
                End If
                info.AddValue("_userCollectionViaCustomBankProperty_", value)
            End If

            MyBase.GetObjectData(info, context)
        End Sub


        Protected Overrides Overloads Function GetAllRelations() As List(Of IEntityRelation)
            Return New StateRelations().GetAllRelations()
        End Function

        Public Overridable Function GetRelationInfoCustomBankPropertyCollection() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(CustomBankPropertyFields.StateId, Nothing, ComparisonOperator.Equal, Me.StateId))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoResourceCollection() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(ResourceFields.StateId, Nothing, ComparisonOperator.Equal, Me.StateId))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoStateActionCollection() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(StateActionFields.StateId, Nothing, ComparisonOperator.Equal, Me.StateId))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoActionCollectionViaStateAction() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.Relations.AddRange(GetRelationsForFieldOfType("ActionCollectionViaStateAction"))
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(StateFields.StateId, Nothing, ComparisonOperator.Equal, Me.StateId, "StateEntity__"))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoBankCollectionViaResource() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.Relations.AddRange(GetRelationsForFieldOfType("BankCollectionViaResource"))
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(StateFields.StateId, Nothing, ComparisonOperator.Equal, Me.StateId, "StateEntity__"))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoBankCollectionViaCustomBankProperty() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.Relations.AddRange(GetRelationsForFieldOfType("BankCollectionViaCustomBankProperty"))
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(StateFields.StateId, Nothing, ComparisonOperator.Equal, Me.StateId, "StateEntity__"))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoUserCollectionViaResource() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.Relations.AddRange(GetRelationsForFieldOfType("UserCollectionViaResource"))
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(StateFields.StateId, Nothing, ComparisonOperator.Equal, Me.StateId, "StateEntity__"))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoUserCollectionViaResource_() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.Relations.AddRange(GetRelationsForFieldOfType("UserCollectionViaResource_"))
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(StateFields.StateId, Nothing, ComparisonOperator.Equal, Me.StateId, "StateEntity__"))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoUserCollectionViaCustomBankProperty() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.Relations.AddRange(GetRelationsForFieldOfType("UserCollectionViaCustomBankProperty"))
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(StateFields.StateId, Nothing, ComparisonOperator.Equal, Me.StateId, "StateEntity__"))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoUserCollectionViaCustomBankProperty_() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.Relations.AddRange(GetRelationsForFieldOfType("UserCollectionViaCustomBankProperty_"))
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(StateFields.StateId, Nothing, ComparisonOperator.Equal, Me.StateId, "StateEntity__"))
            Return bucket
        End Function

        Protected Overrides Function CreateEntityFactory() As IEntityFactory2
            Return EntityFactoryCache2.GetEntityFactory(GetType(StateEntityFactory))
        End Function
#If Not CF Then
        Protected Overrides Sub AddToMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2))
            MyBase.AddToMemberEntityCollectionsQueue(collectionsQueue)
            collectionsQueue.Enqueue(_customBankPropertyCollection)
            collectionsQueue.Enqueue(_resourceCollection)
            collectionsQueue.Enqueue(_stateActionCollection)
            collectionsQueue.Enqueue(_actionCollectionViaStateAction)
            collectionsQueue.Enqueue(_bankCollectionViaResource)
            collectionsQueue.Enqueue(_bankCollectionViaCustomBankProperty)
            collectionsQueue.Enqueue(_userCollectionViaResource)
            collectionsQueue.Enqueue(_userCollectionViaResource_)
            collectionsQueue.Enqueue(_userCollectionViaCustomBankProperty)
            collectionsQueue.Enqueue(_userCollectionViaCustomBankProperty_)
        End Sub

        Protected Overrides Sub GetFromMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2))
            MyBase.GetFromMemberEntityCollectionsQueue(collectionsQueue)
            _customBankPropertyCollection = CType(collectionsQueue.Dequeue(), EntityCollection(Of CustomBankPropertyEntity))
            _resourceCollection = CType(collectionsQueue.Dequeue(), EntityCollection(Of ResourceEntity))
            _stateActionCollection = CType(collectionsQueue.Dequeue(), EntityCollection(Of StateActionEntity))
            _actionCollectionViaStateAction = CType(collectionsQueue.Dequeue(), EntityCollection(Of ActionEntity))
            _bankCollectionViaResource = CType(collectionsQueue.Dequeue(), EntityCollection(Of BankEntity))
            _bankCollectionViaCustomBankProperty = CType(collectionsQueue.Dequeue(), EntityCollection(Of BankEntity))
            _userCollectionViaResource = CType(collectionsQueue.Dequeue(), EntityCollection(Of UserEntity))
            _userCollectionViaResource_ = CType(collectionsQueue.Dequeue(), EntityCollection(Of UserEntity))
            _userCollectionViaCustomBankProperty = CType(collectionsQueue.Dequeue(), EntityCollection(Of UserEntity))
            _userCollectionViaCustomBankProperty_ = CType(collectionsQueue.Dequeue(), EntityCollection(Of UserEntity))
        End Sub

        Protected Overrides Function HasPopulatedMemberEntityCollections() As Boolean
            If (Not _customBankPropertyCollection Is Nothing) Then
                Return True
            End If
            If (Not _resourceCollection Is Nothing) Then
                Return True
            End If
            If (Not _stateActionCollection Is Nothing) Then
                Return True
            End If
            If (Not _actionCollectionViaStateAction Is Nothing) Then
                Return True
            End If
            If (Not _bankCollectionViaResource Is Nothing) Then
                Return True
            End If
            If (Not _bankCollectionViaCustomBankProperty Is Nothing) Then
                Return True
            End If
            If (Not _userCollectionViaResource Is Nothing) Then
                Return True
            End If
            If (Not _userCollectionViaResource_ Is Nothing) Then
                Return True
            End If
            If (Not _userCollectionViaCustomBankProperty Is Nothing) Then
                Return True
            End If
            If (Not _userCollectionViaCustomBankProperty_ Is Nothing) Then
                Return True
            End If
            Return MyBase.HasPopulatedMemberEntityCollections()
        End Function

        Protected Overrides Overloads Sub CreateMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2), requiredQueue As Queue(Of Boolean))
            MyBase.CreateMemberEntityCollectionsQueue(collectionsQueue, requiredQueue)
            Dim toAdd As IEntityCollection2 = Nothing
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of CustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(CustomBankPropertyEntityFactory)))
            Else
                toAdd = Nothing
            End If
            collectionsQueue.Enqueue(toAdd)
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of ResourceEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ResourceEntityFactory)))
            Else
                toAdd = Nothing
            End If
            collectionsQueue.Enqueue(toAdd)
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of StateActionEntity)(EntityFactoryCache2.GetEntityFactory(GetType(StateActionEntityFactory)))
            Else
                toAdd = Nothing
            End If
            collectionsQueue.Enqueue(toAdd)
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of ActionEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ActionEntityFactory)))
            Else
                toAdd = Nothing
            End If
            collectionsQueue.Enqueue(toAdd)
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of BankEntity)(EntityFactoryCache2.GetEntityFactory(GetType(BankEntityFactory)))
            Else
                toAdd = Nothing
            End If
            collectionsQueue.Enqueue(toAdd)
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of BankEntity)(EntityFactoryCache2.GetEntityFactory(GetType(BankEntityFactory)))
            Else
                toAdd = Nothing
            End If
            collectionsQueue.Enqueue(toAdd)
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of UserEntity)(EntityFactoryCache2.GetEntityFactory(GetType(UserEntityFactory)))
            Else
                toAdd = Nothing
            End If
            collectionsQueue.Enqueue(toAdd)
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of UserEntity)(EntityFactoryCache2.GetEntityFactory(GetType(UserEntityFactory)))
            Else
                toAdd = Nothing
            End If
            collectionsQueue.Enqueue(toAdd)
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of UserEntity)(EntityFactoryCache2.GetEntityFactory(GetType(UserEntityFactory)))
            Else
                toAdd = Nothing
            End If
            collectionsQueue.Enqueue(toAdd)
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of UserEntity)(EntityFactoryCache2.GetEntityFactory(GetType(UserEntityFactory)))
            Else
                toAdd = Nothing
            End If
            collectionsQueue.Enqueue(toAdd)
        End Sub
#End If
        Protected Overrides Overloads Function GetRelatedData() As Dictionary(Of String, Object)
            Dim toReturn As New Dictionary(Of String, Object)()
            toReturn.Add("CustomBankPropertyCollection", _customBankPropertyCollection)
            toReturn.Add("ResourceCollection", _resourceCollection)
            toReturn.Add("StateActionCollection", _stateActionCollection)
            toReturn.Add("ActionCollectionViaStateAction", _actionCollectionViaStateAction)
            toReturn.Add("BankCollectionViaResource", _bankCollectionViaResource)
            toReturn.Add("BankCollectionViaCustomBankProperty", _bankCollectionViaCustomBankProperty)
            toReturn.Add("UserCollectionViaResource", _userCollectionViaResource)
            toReturn.Add("UserCollectionViaResource_", _userCollectionViaResource_)
            toReturn.Add("UserCollectionViaCustomBankProperty", _userCollectionViaCustomBankProperty)
            toReturn.Add("UserCollectionViaCustomBankProperty_", _userCollectionViaCustomBankProperty_)
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
            _fieldsCustomProperties.Add("StateId", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("Name", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("Title", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("Description", fieldHashtable)
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

        Public Shared ReadOnly Property Relations() As StateRelations
            Get
                Return New StateRelations()
            End Get
        End Property

        Public Shared ReadOnly Property CustomProperties() As Dictionary(Of String, String)
            Get
                Return _customProperties
            End Get
        End Property


        Public Shared ReadOnly Property PrefetchPathCustomBankPropertyCollection() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(Of CustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(CustomBankPropertyEntityFactory))), _
                    CType(GetRelationsForField("CustomBankPropertyCollection")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.StateEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.CustomBankPropertyEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "CustomBankPropertyCollection", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathResourceCollection() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(Of ResourceEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ResourceEntityFactory))), _
                    CType(GetRelationsForField("ResourceCollection")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.StateEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.ResourceEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "ResourceCollection", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathStateActionCollection() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(Of StateActionEntity)(EntityFactoryCache2.GetEntityFactory(GetType(StateActionEntityFactory))), _
                    CType(GetRelationsForField("StateActionCollection")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.StateEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.StateActionEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "StateActionCollection", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathActionCollectionViaStateAction() As IPrefetchPathElement2
            Get
                Dim intermediateRelation As IEntityRelation = StateEntity.Relations.StateActionEntityUsingStateId
                intermediateRelation.SetAliases(String.Empty, "StateAction_")
                Return New PrefetchPathElement2(New EntityCollection(Of ActionEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ActionEntityFactory))), _
                    intermediateRelation, CType(Questify.Builder.Model.ContentModel.EntityType.StateEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.ActionEntity, Integer), 0, Nothing, Nothing, GetRelationsForField("ActionCollectionViaStateAction"), Nothing, "ActionCollectionViaStateAction", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathBankCollectionViaResource() As IPrefetchPathElement2
            Get
                Dim intermediateRelation As IEntityRelation = StateEntity.Relations.ResourceEntityUsingStateId
                intermediateRelation.SetAliases(String.Empty, "Resource_")
                Return New PrefetchPathElement2(New EntityCollection(Of BankEntity)(EntityFactoryCache2.GetEntityFactory(GetType(BankEntityFactory))), _
                    intermediateRelation, CType(Questify.Builder.Model.ContentModel.EntityType.StateEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.BankEntity, Integer), 0, Nothing, Nothing, GetRelationsForField("BankCollectionViaResource"), Nothing, "BankCollectionViaResource", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathBankCollectionViaCustomBankProperty() As IPrefetchPathElement2
            Get
                Dim intermediateRelation As IEntityRelation = StateEntity.Relations.CustomBankPropertyEntityUsingStateId
                intermediateRelation.SetAliases(String.Empty, "CustomBankProperty_")
                Return New PrefetchPathElement2(New EntityCollection(Of BankEntity)(EntityFactoryCache2.GetEntityFactory(GetType(BankEntityFactory))), _
                    intermediateRelation, CType(Questify.Builder.Model.ContentModel.EntityType.StateEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.BankEntity, Integer), 0, Nothing, Nothing, GetRelationsForField("BankCollectionViaCustomBankProperty"), Nothing, "BankCollectionViaCustomBankProperty", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathUserCollectionViaResource() As IPrefetchPathElement2
            Get
                Dim intermediateRelation As IEntityRelation = StateEntity.Relations.ResourceEntityUsingStateId
                intermediateRelation.SetAliases(String.Empty, "Resource_")
                Return New PrefetchPathElement2(New EntityCollection(Of UserEntity)(EntityFactoryCache2.GetEntityFactory(GetType(UserEntityFactory))), _
                    intermediateRelation, CType(Questify.Builder.Model.ContentModel.EntityType.StateEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.UserEntity, Integer), 0, Nothing, Nothing, GetRelationsForField("UserCollectionViaResource"), Nothing, "UserCollectionViaResource", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathUserCollectionViaResource_() As IPrefetchPathElement2
            Get
                Dim intermediateRelation As IEntityRelation = StateEntity.Relations.ResourceEntityUsingStateId
                intermediateRelation.SetAliases(String.Empty, "Resource_")
                Return New PrefetchPathElement2(New EntityCollection(Of UserEntity)(EntityFactoryCache2.GetEntityFactory(GetType(UserEntityFactory))), _
                    intermediateRelation, CType(Questify.Builder.Model.ContentModel.EntityType.StateEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.UserEntity, Integer), 0, Nothing, Nothing, GetRelationsForField("UserCollectionViaResource_"), Nothing, "UserCollectionViaResource_", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathUserCollectionViaCustomBankProperty() As IPrefetchPathElement2
            Get
                Dim intermediateRelation As IEntityRelation = StateEntity.Relations.CustomBankPropertyEntityUsingStateId
                intermediateRelation.SetAliases(String.Empty, "CustomBankProperty_")
                Return New PrefetchPathElement2(New EntityCollection(Of UserEntity)(EntityFactoryCache2.GetEntityFactory(GetType(UserEntityFactory))), _
                    intermediateRelation, CType(Questify.Builder.Model.ContentModel.EntityType.StateEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.UserEntity, Integer), 0, Nothing, Nothing, GetRelationsForField("UserCollectionViaCustomBankProperty"), Nothing, "UserCollectionViaCustomBankProperty", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathUserCollectionViaCustomBankProperty_() As IPrefetchPathElement2
            Get
                Dim intermediateRelation As IEntityRelation = StateEntity.Relations.CustomBankPropertyEntityUsingStateId
                intermediateRelation.SetAliases(String.Empty, "CustomBankProperty_")
                Return New PrefetchPathElement2(New EntityCollection(Of UserEntity)(EntityFactoryCache2.GetEntityFactory(GetType(UserEntityFactory))), _
                    intermediateRelation, CType(Questify.Builder.Model.ContentModel.EntityType.StateEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.UserEntity, Integer), 0, Nothing, Nothing, GetRelationsForField("UserCollectionViaCustomBankProperty_"), Nothing, "UserCollectionViaCustomBankProperty_", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToMany)
            End Get
        End Property



        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property CustomPropertiesOfType() As Dictionary(Of String, String)
            Get
                Return StateEntity.CustomProperties
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
                Return StateEntity.FieldsCustomProperties
            End Get
        End Property

        Public Overridable Property [StateId]() As System.Int32
            Get
                Return CType(GetValue(CInt(StateFieldIndex.StateId), True), System.Int32)
            End Get
            Set
                SetValue(CInt(StateFieldIndex.StateId), value)
            End Set
        End Property
        Public Overridable Property [Name]() As System.String
            Get
                Return CType(GetValue(CInt(StateFieldIndex.Name), True), System.String)
            End Get
            Set
                SetValue(CInt(StateFieldIndex.Name), value)
            End Set
        End Property
        Public Overridable Property [Title]() As System.String
            Get
                Return CType(GetValue(CInt(StateFieldIndex.Title), True), System.String)
            End Get
            Set
                SetValue(CInt(StateFieldIndex.Title), value)
            End Set
        End Property
        Public Overridable Property [Description]() As System.String
            Get
                Return CType(GetValue(CInt(StateFieldIndex.Description), True), System.String)
            End Get
            Set
                SetValue(CInt(StateFieldIndex.Description), value)
            End Set
        End Property

        <TypeContainedAttribute(GetType(CustomBankPropertyEntity))> _
        Public Overridable ReadOnly Property [CustomBankPropertyCollection]() As EntityCollection(Of CustomBankPropertyEntity)
            Get
                If _customBankPropertyCollection Is Nothing Then
                    _customBankPropertyCollection = New EntityCollection(Of CustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(CustomBankPropertyEntityFactory)))
                    _customBankPropertyCollection.ActiveContext = Me.ActiveContext
                    _customBankPropertyCollection.SetContainingEntityInfo(Me, "State")
                End If
                Return _customBankPropertyCollection
            End Get
        End Property

        <TypeContainedAttribute(GetType(ResourceEntity))> _
        Public Overridable ReadOnly Property [ResourceCollection]() As EntityCollection(Of ResourceEntity)
            Get
                If _resourceCollection Is Nothing Then
                    _resourceCollection = New EntityCollection(Of ResourceEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ResourceEntityFactory)))
                    _resourceCollection.ActiveContext = Me.ActiveContext
                    _resourceCollection.SetContainingEntityInfo(Me, "State")
                End If
                Return _resourceCollection
            End Get
        End Property

        <TypeContainedAttribute(GetType(StateActionEntity))> _
        Public Overridable ReadOnly Property [StateActionCollection]() As EntityCollection(Of StateActionEntity)
            Get
                If _stateActionCollection Is Nothing Then
                    _stateActionCollection = New EntityCollection(Of StateActionEntity)(EntityFactoryCache2.GetEntityFactory(GetType(StateActionEntityFactory)))
                    _stateActionCollection.ActiveContext = Me.ActiveContext
                    _stateActionCollection.SetContainingEntityInfo(Me, "State")
                End If
                Return _stateActionCollection
            End Get
        End Property

        <TypeContainedAttribute(GetType(ActionEntity))> _
        Public Overridable ReadOnly Property [ActionCollectionViaStateAction]() As EntityCollection(Of ActionEntity)
            Get
                If _actionCollectionViaStateAction Is Nothing Then
                    _actionCollectionViaStateAction = New EntityCollection(Of ActionEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ActionEntityFactory)))
                    _actionCollectionViaStateAction.ActiveContext = Me.ActiveContext
                    _actionCollectionViaStateAction.IsReadOnly = True
                    CType(_actionCollectionViaStateAction, IEntityCollectionCore).IsForMN = True
                End If
                Return _actionCollectionViaStateAction
            End Get
        End Property

        <TypeContainedAttribute(GetType(BankEntity))> _
        Public Overridable ReadOnly Property [BankCollectionViaResource]() As EntityCollection(Of BankEntity)
            Get
                If _bankCollectionViaResource Is Nothing Then
                    _bankCollectionViaResource = New EntityCollection(Of BankEntity)(EntityFactoryCache2.GetEntityFactory(GetType(BankEntityFactory)))
                    _bankCollectionViaResource.ActiveContext = Me.ActiveContext
                    _bankCollectionViaResource.IsReadOnly = True
                    CType(_bankCollectionViaResource, IEntityCollectionCore).IsForMN = True
                End If
                Return _bankCollectionViaResource
            End Get
        End Property

        <TypeContainedAttribute(GetType(BankEntity))> _
        Public Overridable ReadOnly Property [BankCollectionViaCustomBankProperty]() As EntityCollection(Of BankEntity)
            Get
                If _bankCollectionViaCustomBankProperty Is Nothing Then
                    _bankCollectionViaCustomBankProperty = New EntityCollection(Of BankEntity)(EntityFactoryCache2.GetEntityFactory(GetType(BankEntityFactory)))
                    _bankCollectionViaCustomBankProperty.ActiveContext = Me.ActiveContext
                    _bankCollectionViaCustomBankProperty.IsReadOnly = True
                    CType(_bankCollectionViaCustomBankProperty, IEntityCollectionCore).IsForMN = True
                End If
                Return _bankCollectionViaCustomBankProperty
            End Get
        End Property

        <TypeContainedAttribute(GetType(UserEntity))> _
        Public Overridable ReadOnly Property [UserCollectionViaResource]() As EntityCollection(Of UserEntity)
            Get
                If _userCollectionViaResource Is Nothing Then
                    _userCollectionViaResource = New EntityCollection(Of UserEntity)(EntityFactoryCache2.GetEntityFactory(GetType(UserEntityFactory)))
                    _userCollectionViaResource.ActiveContext = Me.ActiveContext
                    _userCollectionViaResource.IsReadOnly = True
                    CType(_userCollectionViaResource, IEntityCollectionCore).IsForMN = True
                End If
                Return _userCollectionViaResource
            End Get
        End Property

        <TypeContainedAttribute(GetType(UserEntity))> _
        Public Overridable ReadOnly Property [UserCollectionViaResource_]() As EntityCollection(Of UserEntity)
            Get
                If _userCollectionViaResource_ Is Nothing Then
                    _userCollectionViaResource_ = New EntityCollection(Of UserEntity)(EntityFactoryCache2.GetEntityFactory(GetType(UserEntityFactory)))
                    _userCollectionViaResource_.ActiveContext = Me.ActiveContext
                    _userCollectionViaResource_.IsReadOnly = True
                    CType(_userCollectionViaResource_, IEntityCollectionCore).IsForMN = True
                End If
                Return _userCollectionViaResource_
            End Get
        End Property

        <TypeContainedAttribute(GetType(UserEntity))> _
        Public Overridable ReadOnly Property [UserCollectionViaCustomBankProperty]() As EntityCollection(Of UserEntity)
            Get
                If _userCollectionViaCustomBankProperty Is Nothing Then
                    _userCollectionViaCustomBankProperty = New EntityCollection(Of UserEntity)(EntityFactoryCache2.GetEntityFactory(GetType(UserEntityFactory)))
                    _userCollectionViaCustomBankProperty.ActiveContext = Me.ActiveContext
                    _userCollectionViaCustomBankProperty.IsReadOnly = True
                    CType(_userCollectionViaCustomBankProperty, IEntityCollectionCore).IsForMN = True
                End If
                Return _userCollectionViaCustomBankProperty
            End Get
        End Property

        <TypeContainedAttribute(GetType(UserEntity))> _
        Public Overridable ReadOnly Property [UserCollectionViaCustomBankProperty_]() As EntityCollection(Of UserEntity)
            Get
                If _userCollectionViaCustomBankProperty_ Is Nothing Then
                    _userCollectionViaCustomBankProperty_ = New EntityCollection(Of UserEntity)(EntityFactoryCache2.GetEntityFactory(GetType(UserEntityFactory)))
                    _userCollectionViaCustomBankProperty_.ActiveContext = Me.ActiveContext
                    _userCollectionViaCustomBankProperty_.IsReadOnly = True
                    CType(_userCollectionViaCustomBankProperty_, IEntityCollectionCore).IsForMN = True
                End If
                Return _userCollectionViaCustomBankProperty_
            End Get
        End Property




        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property LLBLGenProIsInHierarchyOfType() As InheritanceHierarchyType
            Get
                Return InheritanceHierarchyType.None
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
                Return CInt(Questify.Builder.Model.ContentModel.EntityType.StateEntity)
            End Get
        End Property






    End Class
End Namespace
