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
    Public Class BankEntity
        Inherits CommonEntityBase



        Private WithEvents _bankCollection As EntityCollection(Of BankEntity)
        Private WithEvents _customBankPropertyCollection As EntityCollection(Of CustomBankPropertyEntity)
        Private WithEvents _roleCollectionViaUserBankRole As EntityCollection(Of RoleEntity)
        Private WithEvents _stateCollectionViaCustomBankProperty As EntityCollection(Of StateEntity)
        Private WithEvents _userCollectionViaCustomBankProperty As EntityCollection(Of UserEntity)
        Private WithEvents _userCollectionViaCustomBankProperty_ As EntityCollection(Of UserEntity)
        Private WithEvents _usersWithPermissionsCollection As EntityCollection(Of UserEntity)
        Private WithEvents _parentBank As BankEntity
        Private WithEvents _createdByUser As UserEntity
        Private WithEvents _modifiedByUser As UserEntity



        Private Shared _customProperties As Dictionary(Of String, String)
        Private Shared _fieldsCustomProperties As Dictionary(Of String, Dictionary(Of String, String))

        Public NotInheritable Class MemberNames
            Private Sub New()
            End Sub
            Public Shared ReadOnly [ParentBank] As String = "ParentBank"
            Public Shared ReadOnly [CreatedByUser] As String = "CreatedByUser"
            Public Shared ReadOnly [ModifiedByUser] As String = "ModifiedByUser"
            Public Shared ReadOnly [BankCollection] As String = "BankCollection"
            Public Shared ReadOnly [CustomBankPropertyCollection] As String = "CustomBankPropertyCollection"
            Public Shared ReadOnly [RoleCollectionViaUserBankRole] As String = "RoleCollectionViaUserBankRole"
            Public Shared ReadOnly [StateCollectionViaCustomBankProperty] As String = "StateCollectionViaCustomBankProperty"
            Public Shared ReadOnly [UserCollectionViaCustomBankProperty] As String = "UserCollectionViaCustomBankProperty"
            Public Shared ReadOnly [UserCollectionViaCustomBankProperty_] As String = "UserCollectionViaCustomBankProperty_"
            Public Shared ReadOnly [UsersWithPermissionsCollection] As String = "UsersWithPermissionsCollection"
        End Class

        Shared Sub New()
            SetupCustomPropertyHashtables()
        End Sub

        Public Sub New()
            MyBase.New("BankEntity")
            InitClassEmpty(Nothing, Nothing)
        End Sub

        Public Sub New(fields As IEntityFields2)
            MyBase.New("BankEntity")
            InitClassEmpty(Nothing, fields)
        End Sub

        Public Sub New(validator As IValidator)
            MyBase.New("BankEntity")
            InitClassEmpty(validator, Nothing)
        End Sub

        Public Sub New(id As System.Int32)
            MyBase.New("BankEntity")
            InitClassEmpty(Nothing, Nothing)
            Me.Id = id
        End Sub

        Public Sub New(id As System.Int32, validator As IValidator)
            MyBase.New("BankEntity")
            InitClassEmpty(validator, Nothing)
            Me.Id = id
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Sub New(info As SerializationInfo, context As StreamingContext)
            MyBase.New(info, context)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                _bankCollection = CType(info.GetValue("_bankCollection", GetType(EntityCollection(Of BankEntity))), EntityCollection(Of BankEntity))
                _customBankPropertyCollection = CType(info.GetValue("_customBankPropertyCollection", GetType(EntityCollection(Of CustomBankPropertyEntity))), EntityCollection(Of CustomBankPropertyEntity))
                _roleCollectionViaUserBankRole = CType(info.GetValue("_roleCollectionViaUserBankRole", GetType(EntityCollection(Of RoleEntity))), EntityCollection(Of RoleEntity))
                _stateCollectionViaCustomBankProperty = CType(info.GetValue("_stateCollectionViaCustomBankProperty", GetType(EntityCollection(Of StateEntity))), EntityCollection(Of StateEntity))
                _userCollectionViaCustomBankProperty = CType(info.GetValue("_userCollectionViaCustomBankProperty", GetType(EntityCollection(Of UserEntity))), EntityCollection(Of UserEntity))
                _userCollectionViaCustomBankProperty_ = CType(info.GetValue("_userCollectionViaCustomBankProperty_", GetType(EntityCollection(Of UserEntity))), EntityCollection(Of UserEntity))
                _usersWithPermissionsCollection = CType(info.GetValue("_usersWithPermissionsCollection", GetType(EntityCollection(Of UserEntity))), EntityCollection(Of UserEntity))
                _parentBank = CType(info.GetValue("_parentBank", GetType(BankEntity)), BankEntity)
                If Not _parentBank Is Nothing Then
                    AddHandler _parentBank.AfterSave, AddressOf OnEntityAfterSave
                End If
                _createdByUser = CType(info.GetValue("_createdByUser", GetType(UserEntity)), UserEntity)
                If Not _createdByUser Is Nothing Then
                    AddHandler _createdByUser.AfterSave, AddressOf OnEntityAfterSave
                End If
                _modifiedByUser = CType(info.GetValue("_modifiedByUser", GetType(UserEntity)), UserEntity)
                If Not _modifiedByUser Is Nothing Then
                    AddHandler _modifiedByUser.AfterSave, AddressOf OnEntityAfterSave
                End If
                Me.FixupDeserialization(FieldInfoProviderSingleton.GetInstance())
            End If

        End Sub


        Protected Overrides Sub PerformDesyncSetupFKFieldChange(fieldIndex As Integer)
            Select Case CType(fieldIndex, BankFieldIndex)

                Case BankFieldIndex.ParentBankId
                    DesetupSyncParentBank(True, False)



                Case BankFieldIndex.CreatedBy
                    DesetupSyncCreatedByUser(True, False)

                Case BankFieldIndex.ModifiedBy
                    DesetupSyncModifiedByUser(True, False)
                Case Else
                    MyBase.PerformDesyncSetupFKFieldChange(fieldIndex)
            End Select
        End Sub


        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub SetRelatedEntityProperty(propertyName As String, entity As IEntityCore)
            Select Case propertyName
                Case "ParentBank"
                    Me.ParentBank = CType(entity, BankEntity)
                Case "CreatedByUser"
                    Me.CreatedByUser = CType(entity, UserEntity)
                Case "ModifiedByUser"
                    Me.ModifiedByUser = CType(entity, UserEntity)
                Case "BankCollection"
                    Me.BankCollection.Add(CType(entity, BankEntity))
                Case "CustomBankPropertyCollection"
                    Me.CustomBankPropertyCollection.Add(CType(entity, CustomBankPropertyEntity))
                Case "RoleCollectionViaUserBankRole"
                    Me.RoleCollectionViaUserBankRole.IsReadOnly = False
                    Me.RoleCollectionViaUserBankRole.Add(CType(entity, RoleEntity))
                    Me.RoleCollectionViaUserBankRole.IsReadOnly = True
                Case "StateCollectionViaCustomBankProperty"
                    Me.StateCollectionViaCustomBankProperty.IsReadOnly = False
                    Me.StateCollectionViaCustomBankProperty.Add(CType(entity, StateEntity))
                    Me.StateCollectionViaCustomBankProperty.IsReadOnly = True
                Case "UserCollectionViaCustomBankProperty"
                    Me.UserCollectionViaCustomBankProperty.IsReadOnly = False
                    Me.UserCollectionViaCustomBankProperty.Add(CType(entity, UserEntity))
                    Me.UserCollectionViaCustomBankProperty.IsReadOnly = True
                Case "UserCollectionViaCustomBankProperty_"
                    Me.UserCollectionViaCustomBankProperty_.IsReadOnly = False
                    Me.UserCollectionViaCustomBankProperty_.Add(CType(entity, UserEntity))
                    Me.UserCollectionViaCustomBankProperty_.IsReadOnly = True
                Case "UsersWithPermissionsCollection"
                    Me.UsersWithPermissionsCollection.IsReadOnly = False
                    Me.UsersWithPermissionsCollection.Add(CType(entity, UserEntity))
                    Me.UsersWithPermissionsCollection.IsReadOnly = True

                Case Else
                    Me.OnSetRelatedEntityProperty(propertyName, entity)
            End Select
        End Sub

        Protected Overrides Function GetRelationsForFieldOfType(fieldName As String) As RelationCollection
            Return BankEntity.GetRelationsForField(fieldName)
        End Function

        Friend Shared Function GetRelationsForField(fieldName As String) As RelationCollection
            Dim toReturn As New RelationCollection()
            Select Case fieldName
                Case "ParentBank"
                    toReturn.Add(BankEntity.Relations.BankEntityUsingIdParentBankId)
                Case "CreatedByUser"
                    toReturn.Add(BankEntity.Relations.UserEntityUsingCreatedBy)
                Case "ModifiedByUser"
                    toReturn.Add(BankEntity.Relations.UserEntityUsingModifiedBy)
                Case "BankCollection"
                    toReturn.Add(BankEntity.Relations.BankEntityUsingParentBankId)
                Case "CustomBankPropertyCollection"
                    toReturn.Add(BankEntity.Relations.CustomBankPropertyEntityUsingBankId)
                Case "RoleCollectionViaUserBankRole"
                    toReturn.Add(BankEntity.Relations.UserBankRoleEntityUsingBankId, "BankEntity__", "UserBankRole_", JoinHint.None)
                    toReturn.Add(UserBankRoleEntity.Relations.RoleEntityUsingBankRoleId, "UserBankRole_", String.Empty, JoinHint.None)
                Case "StateCollectionViaCustomBankProperty"
                    toReturn.Add(BankEntity.Relations.CustomBankPropertyEntityUsingBankId, "BankEntity__", "CustomBankProperty_", JoinHint.None)
                    toReturn.Add(CustomBankPropertyEntity.Relations.StateEntityUsingStateId, "CustomBankProperty_", String.Empty, JoinHint.None)
                Case "UserCollectionViaCustomBankProperty"
                    toReturn.Add(BankEntity.Relations.CustomBankPropertyEntityUsingBankId, "BankEntity__", "CustomBankProperty_", JoinHint.None)
                    toReturn.Add(CustomBankPropertyEntity.Relations.UserEntityUsingModifiedBy, "CustomBankProperty_", String.Empty, JoinHint.None)
                Case "UserCollectionViaCustomBankProperty_"
                    toReturn.Add(BankEntity.Relations.CustomBankPropertyEntityUsingBankId, "BankEntity__", "CustomBankProperty_", JoinHint.None)
                    toReturn.Add(CustomBankPropertyEntity.Relations.UserEntityUsingCreatedBy, "CustomBankProperty_", String.Empty, JoinHint.None)
                Case "UsersWithPermissionsCollection"
                    toReturn.Add(BankEntity.Relations.UserBankRoleEntityUsingBankId, "BankEntity__", "UserBankRole_", JoinHint.None)
                    toReturn.Add(UserBankRoleEntity.Relations.UserEntityUsingUserId, "UserBankRole_", String.Empty, JoinHint.None)
                Case Else
            End Select
            Return toReturn
        End Function
#If Not CF Then
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Function CheckOneWayRelations(propertyName As String) As Boolean
            Dim numberOfOneWayRelations As Integer = 0 + 1 + 1
            Select Case propertyName
                Case Nothing
                    Return ((numberOfOneWayRelations > 0) Or MyBase.CheckOneWayRelations(Nothing))
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
                Case "ParentBank"
                    SetupSyncParentBank(relatedEntity)
                Case "CreatedByUser"
                    SetupSyncCreatedByUser(relatedEntity)
                Case "ModifiedByUser"
                    SetupSyncModifiedByUser(relatedEntity)
                Case "BankCollection"
                    Me.BankCollection.Add(CType(relatedEntity, BankEntity))
                Case "CustomBankPropertyCollection"
                    Me.CustomBankPropertyCollection.Add(CType(relatedEntity, CustomBankPropertyEntity))

                Case Else
            End Select
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub UnsetRelatedEntity(relatedEntity As IEntityCore, fieldName As String, signalRelatedEntityManyToOne As Boolean)
            Select Case fieldName
                Case "ParentBank"
                    DesetupSyncParentBank(False, True)
                Case "CreatedByUser"
                    DesetupSyncCreatedByUser(False, True)
                Case "ModifiedByUser"
                    DesetupSyncModifiedByUser(False, True)
                Case "BankCollection"
                    Me.PerformRelatedEntityRemoval(Me.BankCollection, relatedEntity, signalRelatedEntityManyToOne)
                Case "CustomBankPropertyCollection"
                    Me.PerformRelatedEntityRemoval(Me.CustomBankPropertyCollection, relatedEntity, signalRelatedEntityManyToOne)
                Case Else
            End Select
        End Sub

        Protected Overrides Function GetDependingRelatedEntities() As List(Of IEntity2)
            Dim toReturn As New List(Of IEntity2)()
            Return toReturn
        End Function

        Protected Overrides Function GetDependentRelatedEntities() As List(Of IEntity2)
            Dim toReturn As New List(Of IEntity2)()
            If Not _parentBank Is Nothing Then
                toReturn.Add(_parentBank)
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
            toReturn.Add(Me.BankCollection)
            toReturn.Add(Me.CustomBankPropertyCollection)
            Return toReturn
        End Function


        Protected Overrides Sub GetObjectData(info As SerializationInfo, context As StreamingContext)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                Dim value As IEntityCollection2 = Nothing
                Dim entityValue As IEntity2 = Nothing
                value = Nothing
                If (Not (_bankCollection Is Nothing)) AndAlso (_bankCollection.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _bankCollection
                End If
                info.AddValue("_bankCollection", value)
                value = Nothing
                If (Not (_customBankPropertyCollection Is Nothing)) AndAlso (_customBankPropertyCollection.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _customBankPropertyCollection
                End If
                info.AddValue("_customBankPropertyCollection", value)
                value = Nothing
                If (Not (_roleCollectionViaUserBankRole Is Nothing)) AndAlso (_roleCollectionViaUserBankRole.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _roleCollectionViaUserBankRole
                End If
                info.AddValue("_roleCollectionViaUserBankRole", value)
                value = Nothing
                If (Not (_stateCollectionViaCustomBankProperty Is Nothing)) AndAlso (_stateCollectionViaCustomBankProperty.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _stateCollectionViaCustomBankProperty
                End If
                info.AddValue("_stateCollectionViaCustomBankProperty", value)
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
                value = Nothing
                If (Not (_usersWithPermissionsCollection Is Nothing)) AndAlso (_usersWithPermissionsCollection.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _usersWithPermissionsCollection
                End If
                info.AddValue("_usersWithPermissionsCollection", value)
                entityValue = Nothing
                If Not Me.MarkedForDeletion Then
                    entityValue = _parentBank
                End If
                info.AddValue("_parentBank", entityValue)
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
            End If

            MyBase.GetObjectData(info, context)
        End Sub


        Protected Overrides Overloads Function GetAllRelations() As List(Of IEntityRelation)
            Return New BankRelations().GetAllRelations()
        End Function

        Public Overridable Function GetRelationInfoBankCollection() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(BankFields.ParentBankId, Nothing, ComparisonOperator.Equal, Me.Id))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoCustomBankPropertyCollection() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(CustomBankPropertyFields.BankId, Nothing, ComparisonOperator.Equal, Me.Id))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoRoleCollectionViaUserBankRole() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.Relations.AddRange(GetRelationsForFieldOfType("RoleCollectionViaUserBankRole"))
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(BankFields.Id, Nothing, ComparisonOperator.Equal, Me.Id, "BankEntity__"))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoStateCollectionViaCustomBankProperty() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.Relations.AddRange(GetRelationsForFieldOfType("StateCollectionViaCustomBankProperty"))
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(BankFields.Id, Nothing, ComparisonOperator.Equal, Me.Id, "BankEntity__"))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoUserCollectionViaCustomBankProperty() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.Relations.AddRange(GetRelationsForFieldOfType("UserCollectionViaCustomBankProperty"))
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(BankFields.Id, Nothing, ComparisonOperator.Equal, Me.Id, "BankEntity__"))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoUserCollectionViaCustomBankProperty_() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.Relations.AddRange(GetRelationsForFieldOfType("UserCollectionViaCustomBankProperty_"))
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(BankFields.Id, Nothing, ComparisonOperator.Equal, Me.Id, "BankEntity__"))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoUsersWithPermissionsCollection() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.Relations.AddRange(GetRelationsForFieldOfType("UsersWithPermissionsCollection"))
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(BankFields.Id, Nothing, ComparisonOperator.Equal, Me.Id, "BankEntity__"))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoParentBank() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(BankFields.Id, Nothing, ComparisonOperator.Equal, Me.ParentBankId))
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

        Protected Overrides Function CreateEntityFactory() As IEntityFactory2
            Return EntityFactoryCache2.GetEntityFactory(GetType(BankEntityFactory))
        End Function
#If Not CF Then
        Protected Overrides Sub AddToMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2))
            MyBase.AddToMemberEntityCollectionsQueue(collectionsQueue)
            collectionsQueue.Enqueue(_bankCollection)
            collectionsQueue.Enqueue(_customBankPropertyCollection)
            collectionsQueue.Enqueue(_roleCollectionViaUserBankRole)
            collectionsQueue.Enqueue(_stateCollectionViaCustomBankProperty)
            collectionsQueue.Enqueue(_userCollectionViaCustomBankProperty)
            collectionsQueue.Enqueue(_userCollectionViaCustomBankProperty_)
            collectionsQueue.Enqueue(_usersWithPermissionsCollection)
        End Sub

        Protected Overrides Sub GetFromMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2))
            MyBase.GetFromMemberEntityCollectionsQueue(collectionsQueue)
            _bankCollection = CType(collectionsQueue.Dequeue(), EntityCollection(Of BankEntity))
            _customBankPropertyCollection = CType(collectionsQueue.Dequeue(), EntityCollection(Of CustomBankPropertyEntity))
            _roleCollectionViaUserBankRole = CType(collectionsQueue.Dequeue(), EntityCollection(Of RoleEntity))
            _stateCollectionViaCustomBankProperty = CType(collectionsQueue.Dequeue(), EntityCollection(Of StateEntity))
            _userCollectionViaCustomBankProperty = CType(collectionsQueue.Dequeue(), EntityCollection(Of UserEntity))
            _userCollectionViaCustomBankProperty_ = CType(collectionsQueue.Dequeue(), EntityCollection(Of UserEntity))
            _usersWithPermissionsCollection = CType(collectionsQueue.Dequeue(), EntityCollection(Of UserEntity))
        End Sub

        Protected Overrides Function HasPopulatedMemberEntityCollections() As Boolean
            If (Not _bankCollection Is Nothing) Then
                Return True
            End If
            If (Not _customBankPropertyCollection Is Nothing) Then
                Return True
            End If
            If (Not _roleCollectionViaUserBankRole Is Nothing) Then
                Return True
            End If
            If (Not _stateCollectionViaCustomBankProperty Is Nothing) Then
                Return True
            End If
            If (Not _userCollectionViaCustomBankProperty Is Nothing) Then
                Return True
            End If
            If (Not _userCollectionViaCustomBankProperty_ Is Nothing) Then
                Return True
            End If
            If (Not _usersWithPermissionsCollection Is Nothing) Then
                Return True
            End If
            Return MyBase.HasPopulatedMemberEntityCollections()
        End Function

        Protected Overrides Overloads Sub CreateMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2), requiredQueue As Queue(Of Boolean))
            MyBase.CreateMemberEntityCollectionsQueue(collectionsQueue, requiredQueue)
            Dim toAdd As IEntityCollection2 = Nothing
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of BankEntity)(EntityFactoryCache2.GetEntityFactory(GetType(BankEntityFactory)))
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
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of RoleEntity)(EntityFactoryCache2.GetEntityFactory(GetType(RoleEntityFactory)))
            Else
                toAdd = Nothing
            End If
            collectionsQueue.Enqueue(toAdd)
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of StateEntity)(EntityFactoryCache2.GetEntityFactory(GetType(StateEntityFactory)))
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
            toReturn.Add("ParentBank", _parentBank)
            toReturn.Add("CreatedByUser", _createdByUser)
            toReturn.Add("ModifiedByUser", _modifiedByUser)
            toReturn.Add("BankCollection", _bankCollection)
            toReturn.Add("CustomBankPropertyCollection", _customBankPropertyCollection)
            toReturn.Add("RoleCollectionViaUserBankRole", _roleCollectionViaUserBankRole)
            toReturn.Add("StateCollectionViaCustomBankProperty", _stateCollectionViaCustomBankProperty)
            toReturn.Add("UserCollectionViaCustomBankProperty", _userCollectionViaCustomBankProperty)
            toReturn.Add("UserCollectionViaCustomBankProperty_", _userCollectionViaCustomBankProperty_)
            toReturn.Add("UsersWithPermissionsCollection", _usersWithPermissionsCollection)
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
            _fieldsCustomProperties.Add("Id", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("ParentBankId", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("Name", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("Type", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("CreationDate", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("CreatedBy", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("ModifiedDate", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("ModifiedBy", fieldHashtable)
        End Sub


        Private Sub DesetupSyncParentBank(signalRelatedEntity As Boolean, resetFKFields As Boolean)
            Me.PerformDesetupSyncRelatedEntity(_parentBank, AddressOf OnParentBankPropertyChanged, "ParentBank", Questify.Builder.Model.ContentModel.RelationClasses.StaticBankRelations.BankEntityUsingIdParentBankIdStatic, True, signalRelatedEntity, "BankCollection", resetFKFields, New Integer() {CInt(BankFieldIndex.ParentBankId)})
            _parentBank = Nothing
        End Sub

        Private Sub SetupSyncParentBank(relatedEntity As IEntityCore)
            If Not _parentBank Is relatedEntity Then
                DesetupSyncParentBank(True, True)
                _parentBank = CType(relatedEntity, BankEntity)
                Me.PerformSetupSyncRelatedEntity(_parentBank, AddressOf OnParentBankPropertyChanged, "ParentBank", Questify.Builder.Model.ContentModel.RelationClasses.StaticBankRelations.BankEntityUsingIdParentBankIdStatic, True, New String() {})
            End If
        End Sub

        Private Sub OnParentBankPropertyChanged(sender As Object, e As PropertyChangedEventArgs)
            Select Case e.PropertyName

                Case Else
            End Select
        End Sub

        Private Sub DesetupSyncCreatedByUser(signalRelatedEntity As Boolean, resetFKFields As Boolean)
            Me.PerformDesetupSyncRelatedEntity(_createdByUser, AddressOf OnCreatedByUserPropertyChanged, "CreatedByUser", Questify.Builder.Model.ContentModel.RelationClasses.StaticBankRelations.UserEntityUsingCreatedByStatic, True, signalRelatedEntity, "", resetFKFields, New Integer() {CInt(BankFieldIndex.CreatedBy)})
            _createdByUser = Nothing
        End Sub

        Private Sub SetupSyncCreatedByUser(relatedEntity As IEntityCore)
            If Not _createdByUser Is relatedEntity Then
                DesetupSyncCreatedByUser(True, True)
                _createdByUser = CType(relatedEntity, UserEntity)
                Me.PerformSetupSyncRelatedEntity(_createdByUser, AddressOf OnCreatedByUserPropertyChanged, "CreatedByUser", Questify.Builder.Model.ContentModel.RelationClasses.StaticBankRelations.UserEntityUsingCreatedByStatic, True, New String() {"CreatedByFullName"})
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
            Me.PerformDesetupSyncRelatedEntity(_modifiedByUser, AddressOf OnModifiedByUserPropertyChanged, "ModifiedByUser", Questify.Builder.Model.ContentModel.RelationClasses.StaticBankRelations.UserEntityUsingModifiedByStatic, True, signalRelatedEntity, "", resetFKFields, New Integer() {CInt(BankFieldIndex.ModifiedBy)})
            _modifiedByUser = Nothing
        End Sub

        Private Sub SetupSyncModifiedByUser(relatedEntity As IEntityCore)
            If Not _modifiedByUser Is relatedEntity Then
                DesetupSyncModifiedByUser(True, True)
                _modifiedByUser = CType(relatedEntity, UserEntity)
                Me.PerformSetupSyncRelatedEntity(_modifiedByUser, AddressOf OnModifiedByUserPropertyChanged, "ModifiedByUser", Questify.Builder.Model.ContentModel.RelationClasses.StaticBankRelations.UserEntityUsingModifiedByStatic, True, New String() {"ModifiedByFullName"})
            End If
        End Sub

        Private Sub OnModifiedByUserPropertyChanged(sender As Object, e As PropertyChangedEventArgs)
            Select Case e.PropertyName
                Case "FullName"
                    Me.OnPropertyChanged("ModifiedByFullName")
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

        Public Shared ReadOnly Property Relations() As BankRelations
            Get
                Return New BankRelations()
            End Get
        End Property

        Public Shared ReadOnly Property CustomProperties() As Dictionary(Of String, String)
            Get
                Return _customProperties
            End Get
        End Property


        Public Shared ReadOnly Property PrefetchPathBankCollection() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(Of BankEntity)(EntityFactoryCache2.GetEntityFactory(GetType(BankEntityFactory))), _
                    CType(GetRelationsForField("BankCollection")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.BankEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.BankEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "BankCollection", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathCustomBankPropertyCollection() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(Of CustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(CustomBankPropertyEntityFactory))), _
                    CType(GetRelationsForField("CustomBankPropertyCollection")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.BankEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.CustomBankPropertyEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "CustomBankPropertyCollection", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany)
            End Get
        End Property



        Public Shared ReadOnly Property PrefetchPathRoleCollectionViaUserBankRole() As IPrefetchPathElement2
            Get
                Dim intermediateRelation As IEntityRelation = BankEntity.Relations.UserBankRoleEntityUsingBankId
                intermediateRelation.SetAliases(String.Empty, "UserBankRole_")
                Return New PrefetchPathElement2(New EntityCollection(Of RoleEntity)(EntityFactoryCache2.GetEntityFactory(GetType(RoleEntityFactory))), _
                    intermediateRelation, CType(Questify.Builder.Model.ContentModel.EntityType.BankEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.RoleEntity, Integer), 0, Nothing, Nothing, GetRelationsForField("RoleCollectionViaUserBankRole"), Nothing, "RoleCollectionViaUserBankRole", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToMany)
            End Get
        End Property


        Public Shared ReadOnly Property PrefetchPathStateCollectionViaCustomBankProperty() As IPrefetchPathElement2
            Get
                Dim intermediateRelation As IEntityRelation = BankEntity.Relations.CustomBankPropertyEntityUsingBankId
                intermediateRelation.SetAliases(String.Empty, "CustomBankProperty_")
                Return New PrefetchPathElement2(New EntityCollection(Of StateEntity)(EntityFactoryCache2.GetEntityFactory(GetType(StateEntityFactory))), _
                    intermediateRelation, CType(Questify.Builder.Model.ContentModel.EntityType.BankEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.StateEntity, Integer), 0, Nothing, Nothing, GetRelationsForField("StateCollectionViaCustomBankProperty"), Nothing, "StateCollectionViaCustomBankProperty", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToMany)
            End Get
        End Property





        Public Shared ReadOnly Property PrefetchPathUserCollectionViaCustomBankProperty() As IPrefetchPathElement2
            Get
                Dim intermediateRelation As IEntityRelation = BankEntity.Relations.CustomBankPropertyEntityUsingBankId
                intermediateRelation.SetAliases(String.Empty, "CustomBankProperty_")
                Return New PrefetchPathElement2(New EntityCollection(Of UserEntity)(EntityFactoryCache2.GetEntityFactory(GetType(UserEntityFactory))), _
                    intermediateRelation, CType(Questify.Builder.Model.ContentModel.EntityType.BankEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.UserEntity, Integer), 0, Nothing, Nothing, GetRelationsForField("UserCollectionViaCustomBankProperty"), Nothing, "UserCollectionViaCustomBankProperty", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathUserCollectionViaCustomBankProperty_() As IPrefetchPathElement2
            Get
                Dim intermediateRelation As IEntityRelation = BankEntity.Relations.CustomBankPropertyEntityUsingBankId
                intermediateRelation.SetAliases(String.Empty, "CustomBankProperty_")
                Return New PrefetchPathElement2(New EntityCollection(Of UserEntity)(EntityFactoryCache2.GetEntityFactory(GetType(UserEntityFactory))), _
                    intermediateRelation, CType(Questify.Builder.Model.ContentModel.EntityType.BankEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.UserEntity, Integer), 0, Nothing, Nothing, GetRelationsForField("UserCollectionViaCustomBankProperty_"), Nothing, "UserCollectionViaCustomBankProperty_", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathUsersWithPermissionsCollection() As IPrefetchPathElement2
            Get
                Dim intermediateRelation As IEntityRelation = BankEntity.Relations.UserBankRoleEntityUsingBankId
                intermediateRelation.SetAliases(String.Empty, "UserBankRole_")
                Return New PrefetchPathElement2(New EntityCollection(Of UserEntity)(EntityFactoryCache2.GetEntityFactory(GetType(UserEntityFactory))), _
                    intermediateRelation, CType(Questify.Builder.Model.ContentModel.EntityType.BankEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.UserEntity, Integer), 0, Nothing, Nothing, GetRelationsForField("UsersWithPermissionsCollection"), Nothing, "UsersWithPermissionsCollection", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathParentBank() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(EntityFactoryCache2.GetEntityFactory(GetType(BankEntityFactory))), _
                    CType(GetRelationsForField("ParentBank")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.BankEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.BankEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "ParentBank", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathCreatedByUser() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(EntityFactoryCache2.GetEntityFactory(GetType(UserEntityFactory))), _
                    CType(GetRelationsForField("CreatedByUser")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.BankEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.UserEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "CreatedByUser", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathModifiedByUser() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(EntityFactoryCache2.GetEntityFactory(GetType(UserEntityFactory))), _
                    CType(GetRelationsForField("ModifiedByUser")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.BankEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.UserEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "ModifiedByUser", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne)
            End Get
        End Property


        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property CustomPropertiesOfType() As Dictionary(Of String, String)
            Get
                Return BankEntity.CustomProperties
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
                Return BankEntity.FieldsCustomProperties
            End Get
        End Property

        Public Overridable Property [Id]() As System.Int32
            Get
                Return CType(GetValue(CInt(BankFieldIndex.Id), True), System.Int32)
            End Get
            Set
                SetValue(CInt(BankFieldIndex.Id), value)
            End Set
        End Property
        Public Overridable Property [ParentBankId]() As Nullable(Of System.Int32)
            Get
                Return CType(GetValue(CInt(BankFieldIndex.ParentBankId), False), Nullable(Of System.Int32))
            End Get
            Set
                SetValue(CInt(BankFieldIndex.ParentBankId), value)
            End Set
        End Property
        Public Overridable Property [Name]() As System.String
            Get
                Return CType(GetValue(CInt(BankFieldIndex.Name), True), System.String)
            End Get
            Set
                SetValue(CInt(BankFieldIndex.Name), value)
            End Set
        End Property
        Public Overridable Property [Type]() As System.String
            Get
                Return CType(GetValue(CInt(BankFieldIndex.Type), True), System.String)
            End Get
            Set
                SetValue(CInt(BankFieldIndex.Type), value)
            End Set
        End Property
        Public Overridable Property [CreationDate]() As System.DateTime
            Get
                Return CType(GetValue(CInt(BankFieldIndex.CreationDate), True), System.DateTime)
            End Get
            Set
                SetValue(CInt(BankFieldIndex.CreationDate), value)
            End Set
        End Property
        Public Overridable Property [CreatedBy]() As System.Int32
            Get
                Return CType(GetValue(CInt(BankFieldIndex.CreatedBy), True), System.Int32)
            End Get
            Set
                SetValue(CInt(BankFieldIndex.CreatedBy), value)
            End Set
        End Property
        Public Overridable Property [ModifiedDate]() As System.DateTime
            Get
                Return CType(GetValue(CInt(BankFieldIndex.ModifiedDate), True), System.DateTime)
            End Get
            Set
                SetValue(CInt(BankFieldIndex.ModifiedDate), value)
            End Set
        End Property
        Public Overridable Property [ModifiedBy]() As System.Int32
            Get
                Return CType(GetValue(CInt(BankFieldIndex.ModifiedBy), True), System.Int32)
            End Get
            Set
                SetValue(CInt(BankFieldIndex.ModifiedBy), value)
            End Set
        End Property

        <TypeContainedAttribute(GetType(BankEntity))> _
        Public Overridable ReadOnly Property [BankCollection]() As EntityCollection(Of BankEntity)
            Get
                If _bankCollection Is Nothing Then
                    _bankCollection = New EntityCollection(Of BankEntity)(EntityFactoryCache2.GetEntityFactory(GetType(BankEntityFactory)))
                    _bankCollection.ActiveContext = Me.ActiveContext
                    _bankCollection.SetContainingEntityInfo(Me, "ParentBank")
                End If
                Return _bankCollection
            End Get
        End Property

        <TypeContainedAttribute(GetType(CustomBankPropertyEntity))> _
        Public Overridable ReadOnly Property [CustomBankPropertyCollection]() As EntityCollection(Of CustomBankPropertyEntity)
            Get
                If _customBankPropertyCollection Is Nothing Then
                    _customBankPropertyCollection = New EntityCollection(Of CustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(CustomBankPropertyEntityFactory)))
                    _customBankPropertyCollection.ActiveContext = Me.ActiveContext
                    _customBankPropertyCollection.SetContainingEntityInfo(Me, "Bank")
                End If
                Return _customBankPropertyCollection
            End Get
        End Property



        <TypeContainedAttribute(GetType(RoleEntity))> _
        Public Overridable ReadOnly Property [RoleCollectionViaUserBankRole]() As EntityCollection(Of RoleEntity)
            Get
                If _roleCollectionViaUserBankRole Is Nothing Then
                    _roleCollectionViaUserBankRole = New EntityCollection(Of RoleEntity)(EntityFactoryCache2.GetEntityFactory(GetType(RoleEntityFactory)))
                    _roleCollectionViaUserBankRole.ActiveContext = Me.ActiveContext
                    _roleCollectionViaUserBankRole.IsReadOnly = True
                    CType(_roleCollectionViaUserBankRole, IEntityCollectionCore).IsForMN = True
                End If
                Return _roleCollectionViaUserBankRole
            End Get
        End Property


        <TypeContainedAttribute(GetType(StateEntity))> _
        Public Overridable ReadOnly Property [StateCollectionViaCustomBankProperty]() As EntityCollection(Of StateEntity)
            Get
                If _stateCollectionViaCustomBankProperty Is Nothing Then
                    _stateCollectionViaCustomBankProperty = New EntityCollection(Of StateEntity)(EntityFactoryCache2.GetEntityFactory(GetType(StateEntityFactory)))
                    _stateCollectionViaCustomBankProperty.ActiveContext = Me.ActiveContext
                    _stateCollectionViaCustomBankProperty.IsReadOnly = True
                    CType(_stateCollectionViaCustomBankProperty, IEntityCollectionCore).IsForMN = True
                End If
                Return _stateCollectionViaCustomBankProperty
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

        <TypeContainedAttribute(GetType(UserEntity))> _
        Public Overridable ReadOnly Property [UsersWithPermissionsCollection]() As EntityCollection(Of UserEntity)
            Get
                If _usersWithPermissionsCollection Is Nothing Then
                    _usersWithPermissionsCollection = New EntityCollection(Of UserEntity)(EntityFactoryCache2.GetEntityFactory(GetType(UserEntityFactory)))
                    _usersWithPermissionsCollection.ActiveContext = Me.ActiveContext
                    _usersWithPermissionsCollection.IsReadOnly = True
                    CType(_usersWithPermissionsCollection, IEntityCollectionCore).IsForMN = True
                End If
                Return _usersWithPermissionsCollection
            End Get
        End Property

        <Browsable(True)> _
        Public Overridable Property [ParentBank]() As BankEntity
            Get
                Return _parentBank
            End Get
            Set
                If MyBase.IsDeserializing Then
                    SetupSyncParentBank(value)
                Else
                    SetSingleRelatedEntityNavigator(value, "BankCollection", "ParentBank", _parentBank, True)
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


        Public Overridable Property CreatedByFullName As System.String
            Get
                If Me.CreatedByUser Is Nothing Then
                    Return CType(TypeDefaultValue.GetDefaultValue(GetType(System.String)), System.String)
                Else
                    Return Me.CreatedByUser.FullName
                End If
            End Get
            Set
                Dim relatedEntity As UserEntity = Me.CreatedByUser
                If Not relatedEntity Is Nothing Then
                    relatedEntity.FullName = value
                End If
            End Set
        End Property

        Public Overridable Property ModifiedByFullName As System.String
            Get
                If Me.ModifiedByUser Is Nothing Then
                    Return CType(TypeDefaultValue.GetDefaultValue(GetType(System.String)), System.String)
                Else
                    Return Me.ModifiedByUser.FullName
                End If
            End Get
            Set
                Dim relatedEntity As UserEntity = Me.ModifiedByUser
                If Not relatedEntity Is Nothing Then
                    relatedEntity.FullName = value
                End If
            End Set
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
                Return CInt(Questify.Builder.Model.ContentModel.EntityType.BankEntity)
            End Get
        End Property






    End Class
End Namespace
