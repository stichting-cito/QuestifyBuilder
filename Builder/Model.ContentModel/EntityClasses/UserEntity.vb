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
    Public Class UserEntity
        Inherits CommonEntityBase



        Private WithEvents _customBankPropertyCollection_ As EntityCollection(Of CustomBankPropertyEntity)
        Private WithEvents _customBankPropertyCollection As EntityCollection(Of CustomBankPropertyEntity)
        Private WithEvents _userApplicationRoleCollection As EntityCollection(Of UserApplicationRoleEntity)
        Private WithEvents _userBankRoleCollection As EntityCollection(Of UserBankRoleEntity)
        Private WithEvents _bankCollectionViaCustomBankProperty As EntityCollection(Of BankEntity)
        Private WithEvents _bankCollectionViaCustomBankProperty_ As EntityCollection(Of BankEntity)
        Private WithEvents _roleCollectionViaUserApplicationRole As EntityCollection(Of RoleEntity)
        Private WithEvents _roleCollectionViaUserBankRole As EntityCollection(Of RoleEntity)
        Private WithEvents _stateCollectionViaCustomBankProperty As EntityCollection(Of StateEntity)
        Private WithEvents _stateCollectionViaCustomBankProperty_ As EntityCollection(Of StateEntity)
        Private WithEvents _createdByUser As UserEntity
        Private WithEvents _modifiedByUser As UserEntity



        Private Shared _customProperties As Dictionary(Of String, String)
        Private Shared _fieldsCustomProperties As Dictionary(Of String, Dictionary(Of String, String))

        Public NotInheritable Class MemberNames
            Private Sub New()
            End Sub
            Public Shared ReadOnly [CreatedByUser] As String = "CreatedByUser"
            Public Shared ReadOnly [ModifiedByUser] As String = "ModifiedByUser"
            Public Shared ReadOnly [CustomBankPropertyCollection_] As String = "CustomBankPropertyCollection_"
            Public Shared ReadOnly [CustomBankPropertyCollection] As String = "CustomBankPropertyCollection"
            Public Shared ReadOnly [UserApplicationRoleCollection] As String = "UserApplicationRoleCollection"
            Public Shared ReadOnly [UserBankRoleCollection] As String = "UserBankRoleCollection"
            Public Shared ReadOnly [BankCollectionViaCustomBankProperty] As String = "BankCollectionViaCustomBankProperty"
            Public Shared ReadOnly [BankCollectionViaCustomBankProperty_] As String = "BankCollectionViaCustomBankProperty_"
            Public Shared ReadOnly [RoleCollectionViaUserApplicationRole] As String = "RoleCollectionViaUserApplicationRole"
            Public Shared ReadOnly [RoleCollectionViaUserBankRole] As String = "RoleCollectionViaUserBankRole"
            Public Shared ReadOnly [StateCollectionViaCustomBankProperty] As String = "StateCollectionViaCustomBankProperty"
            Public Shared ReadOnly [StateCollectionViaCustomBankProperty_] As String = "StateCollectionViaCustomBankProperty_"
        End Class

        Shared Sub New()
            SetupCustomPropertyHashtables()
        End Sub

        Public Sub New()
            MyBase.New("UserEntity")
            InitClassEmpty(Nothing, Nothing)
        End Sub

        Public Sub New(fields As IEntityFields2)
            MyBase.New("UserEntity")
            InitClassEmpty(Nothing, fields)
        End Sub

        Public Sub New(validator As IValidator)
            MyBase.New("UserEntity")
            InitClassEmpty(validator, Nothing)
        End Sub

        Public Sub New(id As System.Int32)
            MyBase.New("UserEntity")
            InitClassEmpty(Nothing, Nothing)
            Me.Id = id
        End Sub

        Public Sub New(id As System.Int32, validator As IValidator)
            MyBase.New("UserEntity")
            InitClassEmpty(validator, Nothing)
            Me.Id = id
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Sub New(info As SerializationInfo, context As StreamingContext)
            MyBase.New(info, context)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                _customBankPropertyCollection_ = CType(info.GetValue("_customBankPropertyCollection_", GetType(EntityCollection(Of CustomBankPropertyEntity))), EntityCollection(Of CustomBankPropertyEntity))
                _customBankPropertyCollection = CType(info.GetValue("_customBankPropertyCollection", GetType(EntityCollection(Of CustomBankPropertyEntity))), EntityCollection(Of CustomBankPropertyEntity))
                _userApplicationRoleCollection = CType(info.GetValue("_userApplicationRoleCollection", GetType(EntityCollection(Of UserApplicationRoleEntity))), EntityCollection(Of UserApplicationRoleEntity))
                _userBankRoleCollection = CType(info.GetValue("_userBankRoleCollection", GetType(EntityCollection(Of UserBankRoleEntity))), EntityCollection(Of UserBankRoleEntity))
                _bankCollectionViaCustomBankProperty = CType(info.GetValue("_bankCollectionViaCustomBankProperty", GetType(EntityCollection(Of BankEntity))), EntityCollection(Of BankEntity))
                _bankCollectionViaCustomBankProperty_ = CType(info.GetValue("_bankCollectionViaCustomBankProperty_", GetType(EntityCollection(Of BankEntity))), EntityCollection(Of BankEntity))
                _roleCollectionViaUserApplicationRole = CType(info.GetValue("_roleCollectionViaUserApplicationRole", GetType(EntityCollection(Of RoleEntity))), EntityCollection(Of RoleEntity))
                _roleCollectionViaUserBankRole = CType(info.GetValue("_roleCollectionViaUserBankRole", GetType(EntityCollection(Of RoleEntity))), EntityCollection(Of RoleEntity))
                _stateCollectionViaCustomBankProperty = CType(info.GetValue("_stateCollectionViaCustomBankProperty", GetType(EntityCollection(Of StateEntity))), EntityCollection(Of StateEntity))
                _stateCollectionViaCustomBankProperty_ = CType(info.GetValue("_stateCollectionViaCustomBankProperty_", GetType(EntityCollection(Of StateEntity))), EntityCollection(Of StateEntity))
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
            Select Case CType(fieldIndex, UserFieldIndex)






                Case UserFieldIndex.CreatedBy
                    DesetupSyncCreatedByUser(True, False)

                Case UserFieldIndex.ModifiedBy
                    DesetupSyncModifiedByUser(True, False)




                Case Else
                    MyBase.PerformDesyncSetupFKFieldChange(fieldIndex)
            End Select
        End Sub


        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub SetRelatedEntityProperty(propertyName As String, entity As IEntityCore)
            Select Case propertyName
                Case "CreatedByUser"
                    Me.CreatedByUser = CType(entity, UserEntity)
                Case "ModifiedByUser"
                    Me.ModifiedByUser = CType(entity, UserEntity)
                Case "CustomBankPropertyCollection_"
                    Me.CustomBankPropertyCollection_.Add(CType(entity, CustomBankPropertyEntity))
                Case "CustomBankPropertyCollection"
                    Me.CustomBankPropertyCollection.Add(CType(entity, CustomBankPropertyEntity))
                Case "UserApplicationRoleCollection"
                    Me.UserApplicationRoleCollection.Add(CType(entity, UserApplicationRoleEntity))
                Case "UserBankRoleCollection"
                    Me.UserBankRoleCollection.Add(CType(entity, UserBankRoleEntity))
                Case "BankCollectionViaCustomBankProperty"
                    Me.BankCollectionViaCustomBankProperty.IsReadOnly = False
                    Me.BankCollectionViaCustomBankProperty.Add(CType(entity, BankEntity))
                    Me.BankCollectionViaCustomBankProperty.IsReadOnly = True
                Case "BankCollectionViaCustomBankProperty_"
                    Me.BankCollectionViaCustomBankProperty_.IsReadOnly = False
                    Me.BankCollectionViaCustomBankProperty_.Add(CType(entity, BankEntity))
                    Me.BankCollectionViaCustomBankProperty_.IsReadOnly = True
                Case "RoleCollectionViaUserApplicationRole"
                    Me.RoleCollectionViaUserApplicationRole.IsReadOnly = False
                    Me.RoleCollectionViaUserApplicationRole.Add(CType(entity, RoleEntity))
                    Me.RoleCollectionViaUserApplicationRole.IsReadOnly = True
                Case "RoleCollectionViaUserBankRole"
                    Me.RoleCollectionViaUserBankRole.IsReadOnly = False
                    Me.RoleCollectionViaUserBankRole.Add(CType(entity, RoleEntity))
                    Me.RoleCollectionViaUserBankRole.IsReadOnly = True
                Case "StateCollectionViaCustomBankProperty"
                    Me.StateCollectionViaCustomBankProperty.IsReadOnly = False
                    Me.StateCollectionViaCustomBankProperty.Add(CType(entity, StateEntity))
                    Me.StateCollectionViaCustomBankProperty.IsReadOnly = True
                Case "StateCollectionViaCustomBankProperty_"
                    Me.StateCollectionViaCustomBankProperty_.IsReadOnly = False
                    Me.StateCollectionViaCustomBankProperty_.Add(CType(entity, StateEntity))
                    Me.StateCollectionViaCustomBankProperty_.IsReadOnly = True

                Case Else
                    Me.OnSetRelatedEntityProperty(propertyName, entity)
            End Select
        End Sub

        Protected Overrides Function GetRelationsForFieldOfType(fieldName As String) As RelationCollection
            Return UserEntity.GetRelationsForField(fieldName)
        End Function

        Friend Shared Function GetRelationsForField(fieldName As String) As RelationCollection
            Dim toReturn As New RelationCollection()
            Select Case fieldName
                Case "CreatedByUser"
                    toReturn.Add(UserEntity.Relations.UserEntityUsingIdCreatedBy)
                Case "ModifiedByUser"
                    toReturn.Add(UserEntity.Relations.UserEntityUsingIdModifiedBy)
                Case "CustomBankPropertyCollection_"
                    toReturn.Add(UserEntity.Relations.CustomBankPropertyEntityUsingCreatedBy)
                Case "CustomBankPropertyCollection"
                    toReturn.Add(UserEntity.Relations.CustomBankPropertyEntityUsingModifiedBy)
                Case "UserApplicationRoleCollection"
                    toReturn.Add(UserEntity.Relations.UserApplicationRoleEntityUsingUserId)
                Case "UserBankRoleCollection"
                    toReturn.Add(UserEntity.Relations.UserBankRoleEntityUsingUserId)
                Case "BankCollectionViaCustomBankProperty"
                    toReturn.Add(UserEntity.Relations.CustomBankPropertyEntityUsingModifiedBy, "UserEntity__", "CustomBankProperty_", JoinHint.None)
                    toReturn.Add(CustomBankPropertyEntity.Relations.BankEntityUsingBankId, "CustomBankProperty_", String.Empty, JoinHint.None)
                Case "BankCollectionViaCustomBankProperty_"
                    toReturn.Add(UserEntity.Relations.CustomBankPropertyEntityUsingCreatedBy, "UserEntity__", "CustomBankProperty_", JoinHint.None)
                    toReturn.Add(CustomBankPropertyEntity.Relations.BankEntityUsingBankId, "CustomBankProperty_", String.Empty, JoinHint.None)
                Case "RoleCollectionViaUserApplicationRole"
                    toReturn.Add(UserEntity.Relations.UserApplicationRoleEntityUsingUserId, "UserEntity__", "UserApplicationRole_", JoinHint.None)
                    toReturn.Add(UserApplicationRoleEntity.Relations.RoleEntityUsingApplicationRoleId, "UserApplicationRole_", String.Empty, JoinHint.None)
                Case "RoleCollectionViaUserBankRole"
                    toReturn.Add(UserEntity.Relations.UserBankRoleEntityUsingUserId, "UserEntity__", "UserBankRole_", JoinHint.None)
                    toReturn.Add(UserBankRoleEntity.Relations.RoleEntityUsingBankRoleId, "UserBankRole_", String.Empty, JoinHint.None)
                Case "StateCollectionViaCustomBankProperty"
                    toReturn.Add(UserEntity.Relations.CustomBankPropertyEntityUsingCreatedBy, "UserEntity__", "CustomBankProperty_", JoinHint.None)
                    toReturn.Add(CustomBankPropertyEntity.Relations.StateEntityUsingStateId, "CustomBankProperty_", String.Empty, JoinHint.None)
                Case "StateCollectionViaCustomBankProperty_"
                    toReturn.Add(UserEntity.Relations.CustomBankPropertyEntityUsingModifiedBy, "UserEntity__", "CustomBankProperty_", JoinHint.None)
                    toReturn.Add(CustomBankPropertyEntity.Relations.StateEntityUsingStateId, "CustomBankProperty_", String.Empty, JoinHint.None)
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
                Case "CreatedByUser"
                    SetupSyncCreatedByUser(relatedEntity)
                Case "ModifiedByUser"
                    SetupSyncModifiedByUser(relatedEntity)
                Case "CustomBankPropertyCollection_"
                    Me.CustomBankPropertyCollection_.Add(CType(relatedEntity, CustomBankPropertyEntity))
                Case "CustomBankPropertyCollection"
                    Me.CustomBankPropertyCollection.Add(CType(relatedEntity, CustomBankPropertyEntity))
                Case "UserApplicationRoleCollection"
                    Me.UserApplicationRoleCollection.Add(CType(relatedEntity, UserApplicationRoleEntity))
                Case "UserBankRoleCollection"
                    Me.UserBankRoleCollection.Add(CType(relatedEntity, UserBankRoleEntity))

                Case Else
            End Select
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub UnsetRelatedEntity(relatedEntity As IEntityCore, fieldName As String, signalRelatedEntityManyToOne As Boolean)
            Select Case fieldName
                Case "CreatedByUser"
                    DesetupSyncCreatedByUser(False, True)
                Case "ModifiedByUser"
                    DesetupSyncModifiedByUser(False, True)
                Case "CustomBankPropertyCollection_"
                    Me.PerformRelatedEntityRemoval(Me.CustomBankPropertyCollection_, relatedEntity, signalRelatedEntityManyToOne)
                Case "CustomBankPropertyCollection"
                    Me.PerformRelatedEntityRemoval(Me.CustomBankPropertyCollection, relatedEntity, signalRelatedEntityManyToOne)
                Case "UserApplicationRoleCollection"
                    Me.PerformRelatedEntityRemoval(Me.UserApplicationRoleCollection, relatedEntity, signalRelatedEntityManyToOne)
                Case "UserBankRoleCollection"
                    Me.PerformRelatedEntityRemoval(Me.UserBankRoleCollection, relatedEntity, signalRelatedEntityManyToOne)
                Case Else
            End Select
        End Sub

        Protected Overrides Function GetDependingRelatedEntities() As List(Of IEntity2)
            Dim toReturn As New List(Of IEntity2)()
            Return toReturn
        End Function

        Protected Overrides Function GetDependentRelatedEntities() As List(Of IEntity2)
            Dim toReturn As New List(Of IEntity2)()
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
            toReturn.Add(Me.CustomBankPropertyCollection_)
            toReturn.Add(Me.CustomBankPropertyCollection)
            toReturn.Add(Me.UserApplicationRoleCollection)
            toReturn.Add(Me.UserBankRoleCollection)
            Return toReturn
        End Function


        Protected Overrides Sub GetObjectData(info As SerializationInfo, context As StreamingContext)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                Dim value As IEntityCollection2 = Nothing
                Dim entityValue As IEntity2 = Nothing
                value = Nothing
                If (Not (_customBankPropertyCollection_ Is Nothing)) AndAlso (_customBankPropertyCollection_.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _customBankPropertyCollection_
                End If
                info.AddValue("_customBankPropertyCollection_", value)
                value = Nothing
                If (Not (_customBankPropertyCollection Is Nothing)) AndAlso (_customBankPropertyCollection.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _customBankPropertyCollection
                End If
                info.AddValue("_customBankPropertyCollection", value)
                value = Nothing
                If (Not (_userApplicationRoleCollection Is Nothing)) AndAlso (_userApplicationRoleCollection.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _userApplicationRoleCollection
                End If
                info.AddValue("_userApplicationRoleCollection", value)
                value = Nothing
                If (Not (_userBankRoleCollection Is Nothing)) AndAlso (_userBankRoleCollection.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _userBankRoleCollection
                End If
                info.AddValue("_userBankRoleCollection", value)
                value = Nothing
                If (Not (_bankCollectionViaCustomBankProperty Is Nothing)) AndAlso (_bankCollectionViaCustomBankProperty.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _bankCollectionViaCustomBankProperty
                End If
                info.AddValue("_bankCollectionViaCustomBankProperty", value)
                value = Nothing
                If (Not (_bankCollectionViaCustomBankProperty_ Is Nothing)) AndAlso (_bankCollectionViaCustomBankProperty_.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _bankCollectionViaCustomBankProperty_
                End If
                info.AddValue("_bankCollectionViaCustomBankProperty_", value)
                value = Nothing
                If (Not (_roleCollectionViaUserApplicationRole Is Nothing)) AndAlso (_roleCollectionViaUserApplicationRole.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _roleCollectionViaUserApplicationRole
                End If
                info.AddValue("_roleCollectionViaUserApplicationRole", value)
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
                If (Not (_stateCollectionViaCustomBankProperty_ Is Nothing)) AndAlso (_stateCollectionViaCustomBankProperty_.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _stateCollectionViaCustomBankProperty_
                End If
                info.AddValue("_stateCollectionViaCustomBankProperty_", value)
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
            Return New UserRelations().GetAllRelations()
        End Function

        Public Overridable Function GetRelationInfoCustomBankPropertyCollection_() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(CustomBankPropertyFields.CreatedBy, Nothing, ComparisonOperator.Equal, Me.Id))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoCustomBankPropertyCollection() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(CustomBankPropertyFields.ModifiedBy, Nothing, ComparisonOperator.Equal, Me.Id))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoUserApplicationRoleCollection() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(UserApplicationRoleFields.UserId, Nothing, ComparisonOperator.Equal, Me.Id))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoUserBankRoleCollection() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(UserBankRoleFields.UserId, Nothing, ComparisonOperator.Equal, Me.Id))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoBankCollectionViaCustomBankProperty() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.Relations.AddRange(GetRelationsForFieldOfType("BankCollectionViaCustomBankProperty"))
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(UserFields.Id, Nothing, ComparisonOperator.Equal, Me.Id, "UserEntity__"))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoBankCollectionViaCustomBankProperty_() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.Relations.AddRange(GetRelationsForFieldOfType("BankCollectionViaCustomBankProperty_"))
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(UserFields.Id, Nothing, ComparisonOperator.Equal, Me.Id, "UserEntity__"))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoRoleCollectionViaUserApplicationRole() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.Relations.AddRange(GetRelationsForFieldOfType("RoleCollectionViaUserApplicationRole"))
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(UserFields.Id, Nothing, ComparisonOperator.Equal, Me.Id, "UserEntity__"))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoRoleCollectionViaUserBankRole() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.Relations.AddRange(GetRelationsForFieldOfType("RoleCollectionViaUserBankRole"))
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(UserFields.Id, Nothing, ComparisonOperator.Equal, Me.Id, "UserEntity__"))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoStateCollectionViaCustomBankProperty() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.Relations.AddRange(GetRelationsForFieldOfType("StateCollectionViaCustomBankProperty"))
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(UserFields.Id, Nothing, ComparisonOperator.Equal, Me.Id, "UserEntity__"))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoStateCollectionViaCustomBankProperty_() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.Relations.AddRange(GetRelationsForFieldOfType("StateCollectionViaCustomBankProperty_"))
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(UserFields.Id, Nothing, ComparisonOperator.Equal, Me.Id, "UserEntity__"))
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
            Return EntityFactoryCache2.GetEntityFactory(GetType(UserEntityFactory))
        End Function
#If Not CF Then
        Protected Overrides Sub AddToMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2))
            MyBase.AddToMemberEntityCollectionsQueue(collectionsQueue)
            collectionsQueue.Enqueue(_customBankPropertyCollection_)
            collectionsQueue.Enqueue(_customBankPropertyCollection)
            collectionsQueue.Enqueue(_userApplicationRoleCollection)
            collectionsQueue.Enqueue(_userBankRoleCollection)
            collectionsQueue.Enqueue(_bankCollectionViaCustomBankProperty)
            collectionsQueue.Enqueue(_bankCollectionViaCustomBankProperty_)
            collectionsQueue.Enqueue(_roleCollectionViaUserApplicationRole)
            collectionsQueue.Enqueue(_roleCollectionViaUserBankRole)
            collectionsQueue.Enqueue(_stateCollectionViaCustomBankProperty)
            collectionsQueue.Enqueue(_stateCollectionViaCustomBankProperty_)
        End Sub

        Protected Overrides Sub GetFromMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2))
            MyBase.GetFromMemberEntityCollectionsQueue(collectionsQueue)
            _customBankPropertyCollection_ = CType(collectionsQueue.Dequeue(), EntityCollection(Of CustomBankPropertyEntity))
            _customBankPropertyCollection = CType(collectionsQueue.Dequeue(), EntityCollection(Of CustomBankPropertyEntity))
            _userApplicationRoleCollection = CType(collectionsQueue.Dequeue(), EntityCollection(Of UserApplicationRoleEntity))
            _userBankRoleCollection = CType(collectionsQueue.Dequeue(), EntityCollection(Of UserBankRoleEntity))
            _bankCollectionViaCustomBankProperty = CType(collectionsQueue.Dequeue(), EntityCollection(Of BankEntity))
            _bankCollectionViaCustomBankProperty_ = CType(collectionsQueue.Dequeue(), EntityCollection(Of BankEntity))
            _roleCollectionViaUserApplicationRole = CType(collectionsQueue.Dequeue(), EntityCollection(Of RoleEntity))
            _roleCollectionViaUserBankRole = CType(collectionsQueue.Dequeue(), EntityCollection(Of RoleEntity))
            _stateCollectionViaCustomBankProperty = CType(collectionsQueue.Dequeue(), EntityCollection(Of StateEntity))
            _stateCollectionViaCustomBankProperty_ = CType(collectionsQueue.Dequeue(), EntityCollection(Of StateEntity))
        End Sub

        Protected Overrides Function HasPopulatedMemberEntityCollections() As Boolean
            If (Not _customBankPropertyCollection_ Is Nothing) Then
                Return True
            End If
            If (Not _customBankPropertyCollection Is Nothing) Then
                Return True
            End If
            If (Not _userApplicationRoleCollection Is Nothing) Then
                Return True
            End If
            If (Not _userBankRoleCollection Is Nothing) Then
                Return True
            End If
            If (Not _bankCollectionViaCustomBankProperty Is Nothing) Then
                Return True
            End If
            If (Not _bankCollectionViaCustomBankProperty_ Is Nothing) Then
                Return True
            End If
            If (Not _roleCollectionViaUserApplicationRole Is Nothing) Then
                Return True
            End If
            If (Not _roleCollectionViaUserBankRole Is Nothing) Then
                Return True
            End If
            If (Not _stateCollectionViaCustomBankProperty Is Nothing) Then
                Return True
            End If
            If (Not _stateCollectionViaCustomBankProperty_ Is Nothing) Then
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
                toAdd = New EntityCollection(Of CustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(CustomBankPropertyEntityFactory)))
            Else
                toAdd = Nothing
            End If
            collectionsQueue.Enqueue(toAdd)
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of UserApplicationRoleEntity)(EntityFactoryCache2.GetEntityFactory(GetType(UserApplicationRoleEntityFactory)))
            Else
                toAdd = Nothing
            End If
            collectionsQueue.Enqueue(toAdd)
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of UserBankRoleEntity)(EntityFactoryCache2.GetEntityFactory(GetType(UserBankRoleEntityFactory)))
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
                toAdd = New EntityCollection(Of RoleEntity)(EntityFactoryCache2.GetEntityFactory(GetType(RoleEntityFactory)))
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
                toAdd = New EntityCollection(Of StateEntity)(EntityFactoryCache2.GetEntityFactory(GetType(StateEntityFactory)))
            Else
                toAdd = Nothing
            End If
            collectionsQueue.Enqueue(toAdd)
        End Sub
#End If
        Protected Overrides Overloads Function GetRelatedData() As Dictionary(Of String, Object)
            Dim toReturn As New Dictionary(Of String, Object)()
            toReturn.Add("CreatedByUser", _createdByUser)
            toReturn.Add("ModifiedByUser", _modifiedByUser)
            toReturn.Add("CustomBankPropertyCollection_", _customBankPropertyCollection_)
            toReturn.Add("CustomBankPropertyCollection", _customBankPropertyCollection)
            toReturn.Add("UserApplicationRoleCollection", _userApplicationRoleCollection)
            toReturn.Add("UserBankRoleCollection", _userBankRoleCollection)
            toReturn.Add("BankCollectionViaCustomBankProperty", _bankCollectionViaCustomBankProperty)
            toReturn.Add("BankCollectionViaCustomBankProperty_", _bankCollectionViaCustomBankProperty_)
            toReturn.Add("RoleCollectionViaUserApplicationRole", _roleCollectionViaUserApplicationRole)
            toReturn.Add("RoleCollectionViaUserBankRole", _roleCollectionViaUserBankRole)
            toReturn.Add("StateCollectionViaCustomBankProperty", _stateCollectionViaCustomBankProperty)
            toReturn.Add("StateCollectionViaCustomBankProperty_", _stateCollectionViaCustomBankProperty_)
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
            _fieldsCustomProperties.Add("UserName", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("Password", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("FullName", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("Active", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("CreationDate", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("CreatedBy", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("ModifiedDate", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("ModifiedBy", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("AuthenticationType", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("UserSettings", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("ChangePassword", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("AllowedFeatures", fieldHashtable)
        End Sub


        Private Sub DesetupSyncCreatedByUser(signalRelatedEntity As Boolean, resetFKFields As Boolean)
            Me.PerformDesetupSyncRelatedEntity(_createdByUser, AddressOf OnCreatedByUserPropertyChanged, "CreatedByUser", Questify.Builder.Model.ContentModel.RelationClasses.StaticUserRelations.UserEntityUsingIdCreatedByStatic, True, signalRelatedEntity, "", resetFKFields, New Integer() {CInt(UserFieldIndex.CreatedBy)})
            _createdByUser = Nothing
        End Sub

        Private Sub SetupSyncCreatedByUser(relatedEntity As IEntityCore)
            If Not _createdByUser Is relatedEntity Then
                DesetupSyncCreatedByUser(True, True)
                _createdByUser = CType(relatedEntity, UserEntity)
                Me.PerformSetupSyncRelatedEntity(_createdByUser, AddressOf OnCreatedByUserPropertyChanged, "CreatedByUser", Questify.Builder.Model.ContentModel.RelationClasses.StaticUserRelations.UserEntityUsingIdCreatedByStatic, True, New String() {"CreatedByFullName"})
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
            Me.PerformDesetupSyncRelatedEntity(_modifiedByUser, AddressOf OnModifiedByUserPropertyChanged, "ModifiedByUser", Questify.Builder.Model.ContentModel.RelationClasses.StaticUserRelations.UserEntityUsingIdModifiedByStatic, True, signalRelatedEntity, "", resetFKFields, New Integer() {CInt(UserFieldIndex.ModifiedBy)})
            _modifiedByUser = Nothing
        End Sub

        Private Sub SetupSyncModifiedByUser(relatedEntity As IEntityCore)
            If Not _modifiedByUser Is relatedEntity Then
                DesetupSyncModifiedByUser(True, True)
                _modifiedByUser = CType(relatedEntity, UserEntity)
                Me.PerformSetupSyncRelatedEntity(_modifiedByUser, AddressOf OnModifiedByUserPropertyChanged, "ModifiedByUser", Questify.Builder.Model.ContentModel.RelationClasses.StaticUserRelations.UserEntityUsingIdModifiedByStatic, True, New String() {"ModifiedByFullName"})
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

        Public Shared ReadOnly Property Relations() As UserRelations
            Get
                Return New UserRelations()
            End Get
        End Property

        Public Shared ReadOnly Property CustomProperties() As Dictionary(Of String, String)
            Get
                Return _customProperties
            End Get
        End Property




        Public Shared ReadOnly Property PrefetchPathCustomBankPropertyCollection_() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(Of CustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(CustomBankPropertyEntityFactory))), _
                    CType(GetRelationsForField("CustomBankPropertyCollection_")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.UserEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.CustomBankPropertyEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "CustomBankPropertyCollection_", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathCustomBankPropertyCollection() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(Of CustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(CustomBankPropertyEntityFactory))), _
                    CType(GetRelationsForField("CustomBankPropertyCollection")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.UserEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.CustomBankPropertyEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "CustomBankPropertyCollection", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany)
            End Get
        End Property







        Public Shared ReadOnly Property PrefetchPathUserApplicationRoleCollection() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(Of UserApplicationRoleEntity)(EntityFactoryCache2.GetEntityFactory(GetType(UserApplicationRoleEntityFactory))), _
                    CType(GetRelationsForField("UserApplicationRoleCollection")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.UserEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.UserApplicationRoleEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "UserApplicationRoleCollection", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathUserBankRoleCollection() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(Of UserBankRoleEntity)(EntityFactoryCache2.GetEntityFactory(GetType(UserBankRoleEntityFactory))), _
                    CType(GetRelationsForField("UserBankRoleCollection")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.UserEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.UserBankRoleEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "UserBankRoleCollection", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany)
            End Get
        End Property





        Public Shared ReadOnly Property PrefetchPathBankCollectionViaCustomBankProperty() As IPrefetchPathElement2
            Get
                Dim intermediateRelation As IEntityRelation = UserEntity.Relations.CustomBankPropertyEntityUsingModifiedBy
                intermediateRelation.SetAliases(String.Empty, "CustomBankProperty_")
                Return New PrefetchPathElement2(New EntityCollection(Of BankEntity)(EntityFactoryCache2.GetEntityFactory(GetType(BankEntityFactory))), _
                    intermediateRelation, CType(Questify.Builder.Model.ContentModel.EntityType.UserEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.BankEntity, Integer), 0, Nothing, Nothing, GetRelationsForField("BankCollectionViaCustomBankProperty"), Nothing, "BankCollectionViaCustomBankProperty", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathBankCollectionViaCustomBankProperty_() As IPrefetchPathElement2
            Get
                Dim intermediateRelation As IEntityRelation = UserEntity.Relations.CustomBankPropertyEntityUsingCreatedBy
                intermediateRelation.SetAliases(String.Empty, "CustomBankProperty_")
                Return New PrefetchPathElement2(New EntityCollection(Of BankEntity)(EntityFactoryCache2.GetEntityFactory(GetType(BankEntityFactory))), _
                    intermediateRelation, CType(Questify.Builder.Model.ContentModel.EntityType.UserEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.BankEntity, Integer), 0, Nothing, Nothing, GetRelationsForField("BankCollectionViaCustomBankProperty_"), Nothing, "BankCollectionViaCustomBankProperty_", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToMany)
            End Get
        End Property


        Public Shared ReadOnly Property PrefetchPathRoleCollectionViaUserApplicationRole() As IPrefetchPathElement2
            Get
                Dim intermediateRelation As IEntityRelation = UserEntity.Relations.UserApplicationRoleEntityUsingUserId
                intermediateRelation.SetAliases(String.Empty, "UserApplicationRole_")
                Return New PrefetchPathElement2(New EntityCollection(Of RoleEntity)(EntityFactoryCache2.GetEntityFactory(GetType(RoleEntityFactory))), _
                    intermediateRelation, CType(Questify.Builder.Model.ContentModel.EntityType.UserEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.RoleEntity, Integer), 0, Nothing, Nothing, GetRelationsForField("RoleCollectionViaUserApplicationRole"), Nothing, "RoleCollectionViaUserApplicationRole", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathRoleCollectionViaUserBankRole() As IPrefetchPathElement2
            Get
                Dim intermediateRelation As IEntityRelation = UserEntity.Relations.UserBankRoleEntityUsingUserId
                intermediateRelation.SetAliases(String.Empty, "UserBankRole_")
                Return New PrefetchPathElement2(New EntityCollection(Of RoleEntity)(EntityFactoryCache2.GetEntityFactory(GetType(RoleEntityFactory))), _
                    intermediateRelation, CType(Questify.Builder.Model.ContentModel.EntityType.UserEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.RoleEntity, Integer), 0, Nothing, Nothing, GetRelationsForField("RoleCollectionViaUserBankRole"), Nothing, "RoleCollectionViaUserBankRole", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToMany)
            End Get
        End Property



        Public Shared ReadOnly Property PrefetchPathStateCollectionViaCustomBankProperty() As IPrefetchPathElement2
            Get
                Dim intermediateRelation As IEntityRelation = UserEntity.Relations.CustomBankPropertyEntityUsingCreatedBy
                intermediateRelation.SetAliases(String.Empty, "CustomBankProperty_")
                Return New PrefetchPathElement2(New EntityCollection(Of StateEntity)(EntityFactoryCache2.GetEntityFactory(GetType(StateEntityFactory))), _
                    intermediateRelation, CType(Questify.Builder.Model.ContentModel.EntityType.UserEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.StateEntity, Integer), 0, Nothing, Nothing, GetRelationsForField("StateCollectionViaCustomBankProperty"), Nothing, "StateCollectionViaCustomBankProperty", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathStateCollectionViaCustomBankProperty_() As IPrefetchPathElement2
            Get
                Dim intermediateRelation As IEntityRelation = UserEntity.Relations.CustomBankPropertyEntityUsingModifiedBy
                intermediateRelation.SetAliases(String.Empty, "CustomBankProperty_")
                Return New PrefetchPathElement2(New EntityCollection(Of StateEntity)(EntityFactoryCache2.GetEntityFactory(GetType(StateEntityFactory))), _
                    intermediateRelation, CType(Questify.Builder.Model.ContentModel.EntityType.UserEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.StateEntity, Integer), 0, Nothing, Nothing, GetRelationsForField("StateCollectionViaCustomBankProperty_"), Nothing, "StateCollectionViaCustomBankProperty_", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathCreatedByUser() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(EntityFactoryCache2.GetEntityFactory(GetType(UserEntityFactory))), _
                    CType(GetRelationsForField("CreatedByUser")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.UserEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.UserEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "CreatedByUser", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathModifiedByUser() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(EntityFactoryCache2.GetEntityFactory(GetType(UserEntityFactory))), _
                    CType(GetRelationsForField("ModifiedByUser")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.UserEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.UserEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "ModifiedByUser", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne)
            End Get
        End Property


        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property CustomPropertiesOfType() As Dictionary(Of String, String)
            Get
                Return UserEntity.CustomProperties
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
                Return UserEntity.FieldsCustomProperties
            End Get
        End Property

        Public Overridable Property [Id]() As System.Int32
            Get
                Return CType(GetValue(CInt(UserFieldIndex.Id), True), System.Int32)
            End Get
            Set
                SetValue(CInt(UserFieldIndex.Id), value)
            End Set
        End Property
        Public Overridable Property [UserName]() As System.String
            Get
                Return CType(GetValue(CInt(UserFieldIndex.UserName), True), System.String)
            End Get
            Set
                SetValue(CInt(UserFieldIndex.UserName), value)
            End Set
        End Property
        Public Overridable Property [Password]() As System.String
            Get
                Return CType(GetValue(CInt(UserFieldIndex.Password), True), System.String)
            End Get
            Set
                SetValue(CInt(UserFieldIndex.Password), value)
            End Set
        End Property
        Public Overridable Property [FullName]() As System.String
            Get
                Return CType(GetValue(CInt(UserFieldIndex.FullName), True), System.String)
            End Get
            Set
                SetValue(CInt(UserFieldIndex.FullName), value)
            End Set
        End Property
        Public Overridable Property [Active]() As System.Boolean
            Get
                Return CType(GetValue(CInt(UserFieldIndex.Active), True), System.Boolean)
            End Get
            Set
                SetValue(CInt(UserFieldIndex.Active), value)
            End Set
        End Property
        Public Overridable Property [CreationDate]() As System.DateTime
            Get
                Return CType(GetValue(CInt(UserFieldIndex.CreationDate), True), System.DateTime)
            End Get
            Set
                SetValue(CInt(UserFieldIndex.CreationDate), value)
            End Set
        End Property
        Public Overridable Property [CreatedBy]() As System.Int32
            Get
                Return CType(GetValue(CInt(UserFieldIndex.CreatedBy), True), System.Int32)
            End Get
            Set
                SetValue(CInt(UserFieldIndex.CreatedBy), value)
            End Set
        End Property
        Public Overridable Property [ModifiedDate]() As System.DateTime
            Get
                Return CType(GetValue(CInt(UserFieldIndex.ModifiedDate), True), System.DateTime)
            End Get
            Set
                SetValue(CInt(UserFieldIndex.ModifiedDate), value)
            End Set
        End Property
        Public Overridable Property [ModifiedBy]() As System.Int32
            Get
                Return CType(GetValue(CInt(UserFieldIndex.ModifiedBy), True), System.Int32)
            End Get
            Set
                SetValue(CInt(UserFieldIndex.ModifiedBy), value)
            End Set
        End Property
        Public Overridable Property [AuthenticationType]() As System.String
            Get
                Return CType(GetValue(CInt(UserFieldIndex.AuthenticationType), True), System.String)
            End Get
            Set
                SetValue(CInt(UserFieldIndex.AuthenticationType), value)
            End Set
        End Property
        Public Overridable Property [UserSettings]() As System.String
            Get
                Return CType(GetValue(CInt(UserFieldIndex.UserSettings), True), System.String)
            End Get
            Set
                SetValue(CInt(UserFieldIndex.UserSettings), value)
            End Set
        End Property
        Public Overridable Property [ChangePassword]() As System.Boolean
            Get
                Return CType(GetValue(CInt(UserFieldIndex.ChangePassword), True), System.Boolean)
            End Get
            Set
                SetValue(CInt(UserFieldIndex.ChangePassword), value)
            End Set
        End Property
        Public Overridable Property [AllowedFeatures]() As System.String
            Get
                Return CType(GetValue(CInt(UserFieldIndex.AllowedFeatures), True), System.String)
            End Get
            Set
                SetValue(CInt(UserFieldIndex.AllowedFeatures), value)
            End Set
        End Property



        <TypeContainedAttribute(GetType(CustomBankPropertyEntity))> _
        Public Overridable ReadOnly Property [CustomBankPropertyCollection_]() As EntityCollection(Of CustomBankPropertyEntity)
            Get
                If _customBankPropertyCollection_ Is Nothing Then
                    _customBankPropertyCollection_ = New EntityCollection(Of CustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(CustomBankPropertyEntityFactory)))
                    _customBankPropertyCollection_.ActiveContext = Me.ActiveContext
                    _customBankPropertyCollection_.SetContainingEntityInfo(Me, "CreatedByUser")
                End If
                Return _customBankPropertyCollection_
            End Get
        End Property

        <TypeContainedAttribute(GetType(CustomBankPropertyEntity))> _
        Public Overridable ReadOnly Property [CustomBankPropertyCollection]() As EntityCollection(Of CustomBankPropertyEntity)
            Get
                If _customBankPropertyCollection Is Nothing Then
                    _customBankPropertyCollection = New EntityCollection(Of CustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(CustomBankPropertyEntityFactory)))
                    _customBankPropertyCollection.ActiveContext = Me.ActiveContext
                    _customBankPropertyCollection.SetContainingEntityInfo(Me, "ModifiedByUser")
                End If
                Return _customBankPropertyCollection
            End Get
        End Property







        <TypeContainedAttribute(GetType(UserApplicationRoleEntity))> _
        Public Overridable ReadOnly Property [UserApplicationRoleCollection]() As EntityCollection(Of UserApplicationRoleEntity)
            Get
                If _userApplicationRoleCollection Is Nothing Then
                    _userApplicationRoleCollection = New EntityCollection(Of UserApplicationRoleEntity)(EntityFactoryCache2.GetEntityFactory(GetType(UserApplicationRoleEntityFactory)))
                    _userApplicationRoleCollection.ActiveContext = Me.ActiveContext
                    _userApplicationRoleCollection.SetContainingEntityInfo(Me, "User")
                End If
                Return _userApplicationRoleCollection
            End Get
        End Property

        <TypeContainedAttribute(GetType(UserBankRoleEntity))> _
        Public Overridable ReadOnly Property [UserBankRoleCollection]() As EntityCollection(Of UserBankRoleEntity)
            Get
                If _userBankRoleCollection Is Nothing Then
                    _userBankRoleCollection = New EntityCollection(Of UserBankRoleEntity)(EntityFactoryCache2.GetEntityFactory(GetType(UserBankRoleEntityFactory)))
                    _userBankRoleCollection.ActiveContext = Me.ActiveContext
                    _userBankRoleCollection.SetContainingEntityInfo(Me, "User")
                End If
                Return _userBankRoleCollection
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

        <TypeContainedAttribute(GetType(BankEntity))> _
        Public Overridable ReadOnly Property [BankCollectionViaCustomBankProperty_]() As EntityCollection(Of BankEntity)
            Get
                If _bankCollectionViaCustomBankProperty_ Is Nothing Then
                    _bankCollectionViaCustomBankProperty_ = New EntityCollection(Of BankEntity)(EntityFactoryCache2.GetEntityFactory(GetType(BankEntityFactory)))
                    _bankCollectionViaCustomBankProperty_.ActiveContext = Me.ActiveContext
                    _bankCollectionViaCustomBankProperty_.IsReadOnly = True
                    CType(_bankCollectionViaCustomBankProperty_, IEntityCollectionCore).IsForMN = True
                End If
                Return _bankCollectionViaCustomBankProperty_
            End Get
        End Property


        <TypeContainedAttribute(GetType(RoleEntity))> _
        Public Overridable ReadOnly Property [RoleCollectionViaUserApplicationRole]() As EntityCollection(Of RoleEntity)
            Get
                If _roleCollectionViaUserApplicationRole Is Nothing Then
                    _roleCollectionViaUserApplicationRole = New EntityCollection(Of RoleEntity)(EntityFactoryCache2.GetEntityFactory(GetType(RoleEntityFactory)))
                    _roleCollectionViaUserApplicationRole.ActiveContext = Me.ActiveContext
                    _roleCollectionViaUserApplicationRole.IsReadOnly = True
                    CType(_roleCollectionViaUserApplicationRole, IEntityCollectionCore).IsForMN = True
                End If
                Return _roleCollectionViaUserApplicationRole
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

        <TypeContainedAttribute(GetType(StateEntity))> _
        Public Overridable ReadOnly Property [StateCollectionViaCustomBankProperty_]() As EntityCollection(Of StateEntity)
            Get
                If _stateCollectionViaCustomBankProperty_ Is Nothing Then
                    _stateCollectionViaCustomBankProperty_ = New EntityCollection(Of StateEntity)(EntityFactoryCache2.GetEntityFactory(GetType(StateEntityFactory)))
                    _stateCollectionViaCustomBankProperty_.ActiveContext = Me.ActiveContext
                    _stateCollectionViaCustomBankProperty_.IsReadOnly = True
                    CType(_stateCollectionViaCustomBankProperty_, IEntityCollectionCore).IsForMN = True
                End If
                Return _stateCollectionViaCustomBankProperty_
            End Get
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
                Return CInt(Questify.Builder.Model.ContentModel.EntityType.UserEntity)
            End Get
        End Property






    End Class
End Namespace
