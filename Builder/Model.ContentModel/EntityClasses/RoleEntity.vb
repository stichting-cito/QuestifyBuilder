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
    Public Class RoleEntity
        Inherits CommonEntityBase



        Private WithEvents _rolePermissionCollection As EntityCollection(Of RolePermissionEntity)
        Private WithEvents _userApplicationRoleCollection As EntityCollection(Of UserApplicationRoleEntity)
        Private WithEvents _userBankRoleCollection As EntityCollection(Of UserBankRoleEntity)
        Private WithEvents _permissionCollectionViaRolePermission As EntityCollection(Of PermissionEntity)
        Private WithEvents _permissionTargetCollectionViaRolePermission As EntityCollection(Of PermissionTargetEntity)
        Private WithEvents _createdByUser As UserEntity
        Private WithEvents _modifiedByUser As UserEntity



        Private Shared _customProperties As Dictionary(Of String, String)
        Private Shared _fieldsCustomProperties As Dictionary(Of String, Dictionary(Of String, String))

        Public NotInheritable Class MemberNames
            Private Sub New()
            End Sub
            Public Shared ReadOnly [CreatedByUser] As String = "CreatedByUser"
            Public Shared ReadOnly [ModifiedByUser] As String = "ModifiedByUser"
            Public Shared ReadOnly [RolePermissionCollection] As String = "RolePermissionCollection"
            Public Shared ReadOnly [UserApplicationRoleCollection] As String = "UserApplicationRoleCollection"
            Public Shared ReadOnly [UserBankRoleCollection] As String = "UserBankRoleCollection"
            Public Shared ReadOnly [PermissionCollectionViaRolePermission] As String = "PermissionCollectionViaRolePermission"
            Public Shared ReadOnly [PermissionTargetCollectionViaRolePermission] As String = "PermissionTargetCollectionViaRolePermission"
        End Class

        Shared Sub New()
            SetupCustomPropertyHashtables()
        End Sub

        Public Sub New()
            MyBase.New("RoleEntity")
            InitClassEmpty(Nothing, Nothing)
        End Sub

        Public Sub New(fields As IEntityFields2)
            MyBase.New("RoleEntity")
            InitClassEmpty(Nothing, fields)
        End Sub

        Public Sub New(validator As IValidator)
            MyBase.New("RoleEntity")
            InitClassEmpty(validator, Nothing)
        End Sub

        Public Sub New(id As System.Int32)
            MyBase.New("RoleEntity")
            InitClassEmpty(Nothing, Nothing)
            Me.Id = id
        End Sub

        Public Sub New(id As System.Int32, validator As IValidator)
            MyBase.New("RoleEntity")
            InitClassEmpty(validator, Nothing)
            Me.Id = id
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Sub New(info As SerializationInfo, context As StreamingContext)
            MyBase.New(info, context)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                _rolePermissionCollection = CType(info.GetValue("_rolePermissionCollection", GetType(EntityCollection(Of RolePermissionEntity))), EntityCollection(Of RolePermissionEntity))
                _userApplicationRoleCollection = CType(info.GetValue("_userApplicationRoleCollection", GetType(EntityCollection(Of UserApplicationRoleEntity))), EntityCollection(Of UserApplicationRoleEntity))
                _userBankRoleCollection = CType(info.GetValue("_userBankRoleCollection", GetType(EntityCollection(Of UserBankRoleEntity))), EntityCollection(Of UserBankRoleEntity))
                _permissionCollectionViaRolePermission = CType(info.GetValue("_permissionCollectionViaRolePermission", GetType(EntityCollection(Of PermissionEntity))), EntityCollection(Of PermissionEntity))
                _permissionTargetCollectionViaRolePermission = CType(info.GetValue("_permissionTargetCollectionViaRolePermission", GetType(EntityCollection(Of PermissionTargetEntity))), EntityCollection(Of PermissionTargetEntity))
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
            Select Case CType(fieldIndex, RoleFieldIndex)





                Case RoleFieldIndex.CreatedBy
                    DesetupSyncCreatedByUser(True, False)

                Case RoleFieldIndex.ModifiedBy
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
                Case "RolePermissionCollection"
                    Me.RolePermissionCollection.Add(CType(entity, RolePermissionEntity))
                Case "UserApplicationRoleCollection"
                    Me.UserApplicationRoleCollection.Add(CType(entity, UserApplicationRoleEntity))
                Case "UserBankRoleCollection"
                    Me.UserBankRoleCollection.Add(CType(entity, UserBankRoleEntity))
                Case "PermissionCollectionViaRolePermission"
                    Me.PermissionCollectionViaRolePermission.IsReadOnly = False
                    Me.PermissionCollectionViaRolePermission.Add(CType(entity, PermissionEntity))
                    Me.PermissionCollectionViaRolePermission.IsReadOnly = True
                Case "PermissionTargetCollectionViaRolePermission"
                    Me.PermissionTargetCollectionViaRolePermission.IsReadOnly = False
                    Me.PermissionTargetCollectionViaRolePermission.Add(CType(entity, PermissionTargetEntity))
                    Me.PermissionTargetCollectionViaRolePermission.IsReadOnly = True

                Case Else
                    Me.OnSetRelatedEntityProperty(propertyName, entity)
            End Select
        End Sub

        Protected Overrides Function GetRelationsForFieldOfType(fieldName As String) As RelationCollection
            Return RoleEntity.GetRelationsForField(fieldName)
        End Function

        Friend Shared Function GetRelationsForField(fieldName As String) As RelationCollection
            Dim toReturn As New RelationCollection()
            Select Case fieldName
                Case "CreatedByUser"
                    toReturn.Add(RoleEntity.Relations.UserEntityUsingCreatedBy)
                Case "ModifiedByUser"
                    toReturn.Add(RoleEntity.Relations.UserEntityUsingModifiedBy)
                Case "RolePermissionCollection"
                    toReturn.Add(RoleEntity.Relations.RolePermissionEntityUsingRoleId)
                Case "UserApplicationRoleCollection"
                    toReturn.Add(RoleEntity.Relations.UserApplicationRoleEntityUsingApplicationRoleId)
                Case "UserBankRoleCollection"
                    toReturn.Add(RoleEntity.Relations.UserBankRoleEntityUsingBankRoleId)
                Case "PermissionCollectionViaRolePermission"
                    toReturn.Add(RoleEntity.Relations.RolePermissionEntityUsingRoleId, "RoleEntity__", "RolePermission_", JoinHint.None)
                    toReturn.Add(RolePermissionEntity.Relations.PermissionEntityUsingPermissionId, "RolePermission_", String.Empty, JoinHint.None)
                Case "PermissionTargetCollectionViaRolePermission"
                    toReturn.Add(RoleEntity.Relations.RolePermissionEntityUsingRoleId, "RoleEntity__", "RolePermission_", JoinHint.None)
                    toReturn.Add(RolePermissionEntity.Relations.PermissionTargetEntityUsingPermissionTargetId, "RolePermission_", String.Empty, JoinHint.None)
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
                Case "RolePermissionCollection"
                    Me.RolePermissionCollection.Add(CType(relatedEntity, RolePermissionEntity))
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
                Case "RolePermissionCollection"
                    Me.PerformRelatedEntityRemoval(Me.RolePermissionCollection, relatedEntity, signalRelatedEntityManyToOne)
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
            toReturn.Add(Me.RolePermissionCollection)
            toReturn.Add(Me.UserApplicationRoleCollection)
            toReturn.Add(Me.UserBankRoleCollection)
            Return toReturn
        End Function


        Protected Overrides Sub GetObjectData(info As SerializationInfo, context As StreamingContext)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                Dim value As IEntityCollection2 = Nothing
                Dim entityValue As IEntity2 = Nothing
                value = Nothing
                If (Not (_rolePermissionCollection Is Nothing)) AndAlso (_rolePermissionCollection.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _rolePermissionCollection
                End If
                info.AddValue("_rolePermissionCollection", value)
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
                If (Not (_permissionCollectionViaRolePermission Is Nothing)) AndAlso (_permissionCollectionViaRolePermission.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _permissionCollectionViaRolePermission
                End If
                info.AddValue("_permissionCollectionViaRolePermission", value)
                value = Nothing
                If (Not (_permissionTargetCollectionViaRolePermission Is Nothing)) AndAlso (_permissionTargetCollectionViaRolePermission.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _permissionTargetCollectionViaRolePermission
                End If
                info.AddValue("_permissionTargetCollectionViaRolePermission", value)
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
            Return New RoleRelations().GetAllRelations()
        End Function

        Public Overridable Function GetRelationInfoRolePermissionCollection() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(RolePermissionFields.RoleId, Nothing, ComparisonOperator.Equal, Me.Id))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoUserApplicationRoleCollection() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(UserApplicationRoleFields.ApplicationRoleId, Nothing, ComparisonOperator.Equal, Me.Id))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoUserBankRoleCollection() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(UserBankRoleFields.BankRoleId, Nothing, ComparisonOperator.Equal, Me.Id))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoPermissionCollectionViaRolePermission() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.Relations.AddRange(GetRelationsForFieldOfType("PermissionCollectionViaRolePermission"))
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(RoleFields.Id, Nothing, ComparisonOperator.Equal, Me.Id, "RoleEntity__"))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoPermissionTargetCollectionViaRolePermission() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.Relations.AddRange(GetRelationsForFieldOfType("PermissionTargetCollectionViaRolePermission"))
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(RoleFields.Id, Nothing, ComparisonOperator.Equal, Me.Id, "RoleEntity__"))
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
            Return EntityFactoryCache2.GetEntityFactory(GetType(RoleEntityFactory))
        End Function
#If Not CF Then
        Protected Overrides Sub AddToMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2))
            MyBase.AddToMemberEntityCollectionsQueue(collectionsQueue)
            collectionsQueue.Enqueue(_rolePermissionCollection)
            collectionsQueue.Enqueue(_userApplicationRoleCollection)
            collectionsQueue.Enqueue(_userBankRoleCollection)
            collectionsQueue.Enqueue(_permissionCollectionViaRolePermission)
            collectionsQueue.Enqueue(_permissionTargetCollectionViaRolePermission)
        End Sub

        Protected Overrides Sub GetFromMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2))
            MyBase.GetFromMemberEntityCollectionsQueue(collectionsQueue)
            _rolePermissionCollection = CType(collectionsQueue.Dequeue(), EntityCollection(Of RolePermissionEntity))
            _userApplicationRoleCollection = CType(collectionsQueue.Dequeue(), EntityCollection(Of UserApplicationRoleEntity))
            _userBankRoleCollection = CType(collectionsQueue.Dequeue(), EntityCollection(Of UserBankRoleEntity))
            _permissionCollectionViaRolePermission = CType(collectionsQueue.Dequeue(), EntityCollection(Of PermissionEntity))
            _permissionTargetCollectionViaRolePermission = CType(collectionsQueue.Dequeue(), EntityCollection(Of PermissionTargetEntity))
        End Sub

        Protected Overrides Function HasPopulatedMemberEntityCollections() As Boolean
            If (Not _rolePermissionCollection Is Nothing) Then
                Return True
            End If
            If (Not _userApplicationRoleCollection Is Nothing) Then
                Return True
            End If
            If (Not _userBankRoleCollection Is Nothing) Then
                Return True
            End If
            If (Not _permissionCollectionViaRolePermission Is Nothing) Then
                Return True
            End If
            If (Not _permissionTargetCollectionViaRolePermission Is Nothing) Then
                Return True
            End If
            Return MyBase.HasPopulatedMemberEntityCollections()
        End Function

        Protected Overrides Overloads Sub CreateMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2), requiredQueue As Queue(Of Boolean))
            MyBase.CreateMemberEntityCollectionsQueue(collectionsQueue, requiredQueue)
            Dim toAdd As IEntityCollection2 = Nothing
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of RolePermissionEntity)(EntityFactoryCache2.GetEntityFactory(GetType(RolePermissionEntityFactory)))
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
                toAdd = New EntityCollection(Of PermissionEntity)(EntityFactoryCache2.GetEntityFactory(GetType(PermissionEntityFactory)))
            Else
                toAdd = Nothing
            End If
            collectionsQueue.Enqueue(toAdd)
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of PermissionTargetEntity)(EntityFactoryCache2.GetEntityFactory(GetType(PermissionTargetEntityFactory)))
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
            toReturn.Add("RolePermissionCollection", _rolePermissionCollection)
            toReturn.Add("UserApplicationRoleCollection", _userApplicationRoleCollection)
            toReturn.Add("UserBankRoleCollection", _userBankRoleCollection)
            toReturn.Add("PermissionCollectionViaRolePermission", _permissionCollectionViaRolePermission)
            toReturn.Add("PermissionTargetCollectionViaRolePermission", _permissionTargetCollectionViaRolePermission)
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
            _fieldsCustomProperties.Add("Name", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("Description", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("IsApplicationRole", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("CreationDate", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("CreatedBy", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("ModifiedDate", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("ModifiedBy", fieldHashtable)
        End Sub


        Private Sub DesetupSyncCreatedByUser(signalRelatedEntity As Boolean, resetFKFields As Boolean)
            Me.PerformDesetupSyncRelatedEntity(_createdByUser, AddressOf OnCreatedByUserPropertyChanged, "CreatedByUser", Questify.Builder.Model.ContentModel.RelationClasses.StaticRoleRelations.UserEntityUsingCreatedByStatic, True, signalRelatedEntity, "", resetFKFields, New Integer() {CInt(RoleFieldIndex.CreatedBy)})
            _createdByUser = Nothing
        End Sub

        Private Sub SetupSyncCreatedByUser(relatedEntity As IEntityCore)
            If Not _createdByUser Is relatedEntity Then
                DesetupSyncCreatedByUser(True, True)
                _createdByUser = CType(relatedEntity, UserEntity)
                Me.PerformSetupSyncRelatedEntity(_createdByUser, AddressOf OnCreatedByUserPropertyChanged, "CreatedByUser", Questify.Builder.Model.ContentModel.RelationClasses.StaticRoleRelations.UserEntityUsingCreatedByStatic, True, New String() {"CreatedByFullName"})
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
            Me.PerformDesetupSyncRelatedEntity(_modifiedByUser, AddressOf OnModifiedByUserPropertyChanged, "ModifiedByUser", Questify.Builder.Model.ContentModel.RelationClasses.StaticRoleRelations.UserEntityUsingModifiedByStatic, True, signalRelatedEntity, "", resetFKFields, New Integer() {CInt(RoleFieldIndex.ModifiedBy)})
            _modifiedByUser = Nothing
        End Sub

        Private Sub SetupSyncModifiedByUser(relatedEntity As IEntityCore)
            If Not _modifiedByUser Is relatedEntity Then
                DesetupSyncModifiedByUser(True, True)
                _modifiedByUser = CType(relatedEntity, UserEntity)
                Me.PerformSetupSyncRelatedEntity(_modifiedByUser, AddressOf OnModifiedByUserPropertyChanged, "ModifiedByUser", Questify.Builder.Model.ContentModel.RelationClasses.StaticRoleRelations.UserEntityUsingModifiedByStatic, True, New String() {"ModifiedByFullName"})
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

        Public Shared ReadOnly Property Relations() As RoleRelations
            Get
                Return New RoleRelations()
            End Get
        End Property

        Public Shared ReadOnly Property CustomProperties() As Dictionary(Of String, String)
            Get
                Return _customProperties
            End Get
        End Property


        Public Shared ReadOnly Property PrefetchPathRolePermissionCollection() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(Of RolePermissionEntity)(EntityFactoryCache2.GetEntityFactory(GetType(RolePermissionEntityFactory))), _
                    CType(GetRelationsForField("RolePermissionCollection")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.RoleEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.RolePermissionEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "RolePermissionCollection", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathUserApplicationRoleCollection() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(Of UserApplicationRoleEntity)(EntityFactoryCache2.GetEntityFactory(GetType(UserApplicationRoleEntityFactory))), _
                    CType(GetRelationsForField("UserApplicationRoleCollection")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.RoleEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.UserApplicationRoleEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "UserApplicationRoleCollection", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathUserBankRoleCollection() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(Of UserBankRoleEntity)(EntityFactoryCache2.GetEntityFactory(GetType(UserBankRoleEntityFactory))), _
                    CType(GetRelationsForField("UserBankRoleCollection")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.RoleEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.UserBankRoleEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "UserBankRoleCollection", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany)
            End Get
        End Property


        Public Shared ReadOnly Property PrefetchPathPermissionCollectionViaRolePermission() As IPrefetchPathElement2
            Get
                Dim intermediateRelation As IEntityRelation = RoleEntity.Relations.RolePermissionEntityUsingRoleId
                intermediateRelation.SetAliases(String.Empty, "RolePermission_")
                Return New PrefetchPathElement2(New EntityCollection(Of PermissionEntity)(EntityFactoryCache2.GetEntityFactory(GetType(PermissionEntityFactory))), _
                    intermediateRelation, CType(Questify.Builder.Model.ContentModel.EntityType.RoleEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.PermissionEntity, Integer), 0, Nothing, Nothing, GetRelationsForField("PermissionCollectionViaRolePermission"), Nothing, "PermissionCollectionViaRolePermission", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathPermissionTargetCollectionViaRolePermission() As IPrefetchPathElement2
            Get
                Dim intermediateRelation As IEntityRelation = RoleEntity.Relations.RolePermissionEntityUsingRoleId
                intermediateRelation.SetAliases(String.Empty, "RolePermission_")
                Return New PrefetchPathElement2(New EntityCollection(Of PermissionTargetEntity)(EntityFactoryCache2.GetEntityFactory(GetType(PermissionTargetEntityFactory))), _
                    intermediateRelation, CType(Questify.Builder.Model.ContentModel.EntityType.RoleEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.PermissionTargetEntity, Integer), 0, Nothing, Nothing, GetRelationsForField("PermissionTargetCollectionViaRolePermission"), Nothing, "PermissionTargetCollectionViaRolePermission", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToMany)
            End Get
        End Property



        Public Shared ReadOnly Property PrefetchPathCreatedByUser() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(EntityFactoryCache2.GetEntityFactory(GetType(UserEntityFactory))), _
                    CType(GetRelationsForField("CreatedByUser")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.RoleEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.UserEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "CreatedByUser", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathModifiedByUser() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(EntityFactoryCache2.GetEntityFactory(GetType(UserEntityFactory))), _
                    CType(GetRelationsForField("ModifiedByUser")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.RoleEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.UserEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "ModifiedByUser", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne)
            End Get
        End Property


        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property CustomPropertiesOfType() As Dictionary(Of String, String)
            Get
                Return RoleEntity.CustomProperties
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
                Return RoleEntity.FieldsCustomProperties
            End Get
        End Property

        Public Overridable Property [Id]() As System.Int32
            Get
                Return CType(GetValue(CInt(RoleFieldIndex.Id), True), System.Int32)
            End Get
            Set
                SetValue(CInt(RoleFieldIndex.Id), value)
            End Set
        End Property
        Public Overridable Property [Name]() As System.String
            Get
                Return CType(GetValue(CInt(RoleFieldIndex.Name), True), System.String)
            End Get
            Set
                SetValue(CInt(RoleFieldIndex.Name), value)
            End Set
        End Property
        Public Overridable Property [Description]() As System.String
            Get
                Return CType(GetValue(CInt(RoleFieldIndex.Description), True), System.String)
            End Get
            Set
                SetValue(CInt(RoleFieldIndex.Description), value)
            End Set
        End Property
        Public Overridable Property [IsApplicationRole]() As Nullable(Of System.Boolean)
            Get
                Return CType(GetValue(CInt(RoleFieldIndex.IsApplicationRole), False), Nullable(Of System.Boolean))
            End Get
            Set
                SetValue(CInt(RoleFieldIndex.IsApplicationRole), value)
            End Set
        End Property
        Public Overridable Property [CreationDate]() As System.DateTime
            Get
                Return CType(GetValue(CInt(RoleFieldIndex.CreationDate), True), System.DateTime)
            End Get
            Set
                SetValue(CInt(RoleFieldIndex.CreationDate), value)
            End Set
        End Property
        Public Overridable Property [CreatedBy]() As System.Int32
            Get
                Return CType(GetValue(CInt(RoleFieldIndex.CreatedBy), True), System.Int32)
            End Get
            Set
                SetValue(CInt(RoleFieldIndex.CreatedBy), value)
            End Set
        End Property
        Public Overridable Property [ModifiedDate]() As System.DateTime
            Get
                Return CType(GetValue(CInt(RoleFieldIndex.ModifiedDate), True), System.DateTime)
            End Get
            Set
                SetValue(CInt(RoleFieldIndex.ModifiedDate), value)
            End Set
        End Property
        Public Overridable Property [ModifiedBy]() As System.Int32
            Get
                Return CType(GetValue(CInt(RoleFieldIndex.ModifiedBy), True), System.Int32)
            End Get
            Set
                SetValue(CInt(RoleFieldIndex.ModifiedBy), value)
            End Set
        End Property

        <TypeContainedAttribute(GetType(RolePermissionEntity))> _
        Public Overridable ReadOnly Property [RolePermissionCollection]() As EntityCollection(Of RolePermissionEntity)
            Get
                If _rolePermissionCollection Is Nothing Then
                    _rolePermissionCollection = New EntityCollection(Of RolePermissionEntity)(EntityFactoryCache2.GetEntityFactory(GetType(RolePermissionEntityFactory)))
                    _rolePermissionCollection.ActiveContext = Me.ActiveContext
                    _rolePermissionCollection.SetContainingEntityInfo(Me, "Role")
                End If
                Return _rolePermissionCollection
            End Get
        End Property

        <TypeContainedAttribute(GetType(UserApplicationRoleEntity))> _
        Public Overridable ReadOnly Property [UserApplicationRoleCollection]() As EntityCollection(Of UserApplicationRoleEntity)
            Get
                If _userApplicationRoleCollection Is Nothing Then
                    _userApplicationRoleCollection = New EntityCollection(Of UserApplicationRoleEntity)(EntityFactoryCache2.GetEntityFactory(GetType(UserApplicationRoleEntityFactory)))
                    _userApplicationRoleCollection.ActiveContext = Me.ActiveContext
                    _userApplicationRoleCollection.SetContainingEntityInfo(Me, "Role")
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
                    _userBankRoleCollection.SetContainingEntityInfo(Me, "Role")
                End If
                Return _userBankRoleCollection
            End Get
        End Property


        <TypeContainedAttribute(GetType(PermissionEntity))> _
        Public Overridable ReadOnly Property [PermissionCollectionViaRolePermission]() As EntityCollection(Of PermissionEntity)
            Get
                If _permissionCollectionViaRolePermission Is Nothing Then
                    _permissionCollectionViaRolePermission = New EntityCollection(Of PermissionEntity)(EntityFactoryCache2.GetEntityFactory(GetType(PermissionEntityFactory)))
                    _permissionCollectionViaRolePermission.ActiveContext = Me.ActiveContext
                    _permissionCollectionViaRolePermission.IsReadOnly = True
                    CType(_permissionCollectionViaRolePermission, IEntityCollectionCore).IsForMN = True
                End If
                Return _permissionCollectionViaRolePermission
            End Get
        End Property

        <TypeContainedAttribute(GetType(PermissionTargetEntity))> _
        Public Overridable ReadOnly Property [PermissionTargetCollectionViaRolePermission]() As EntityCollection(Of PermissionTargetEntity)
            Get
                If _permissionTargetCollectionViaRolePermission Is Nothing Then
                    _permissionTargetCollectionViaRolePermission = New EntityCollection(Of PermissionTargetEntity)(EntityFactoryCache2.GetEntityFactory(GetType(PermissionTargetEntityFactory)))
                    _permissionTargetCollectionViaRolePermission.ActiveContext = Me.ActiveContext
                    _permissionTargetCollectionViaRolePermission.IsReadOnly = True
                    CType(_permissionTargetCollectionViaRolePermission, IEntityCollectionCore).IsForMN = True
                End If
                Return _permissionTargetCollectionViaRolePermission
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
                Return CInt(Questify.Builder.Model.ContentModel.EntityType.RoleEntity)
            End Get
        End Property






    End Class
End Namespace
