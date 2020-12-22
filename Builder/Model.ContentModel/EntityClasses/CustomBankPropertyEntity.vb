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
    Public Class CustomBankPropertyEntity
        Inherits CommonEntityBase



        Private WithEvents _customBankPropertyValueCollection As EntityCollection(Of CustomBankPropertyValueEntity)
        Private WithEvents _bank As BankEntity
        Private WithEvents _state As StateEntity
        Private WithEvents _createdByUser As UserEntity
        Private WithEvents _modifiedByUser As UserEntity



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
        End Class

        Shared Sub New()
            SetupCustomPropertyHashtables()
        End Sub

        Public Sub New()
            MyBase.New("CustomBankPropertyEntity")
            InitClassEmpty(Nothing, Nothing)
        End Sub

        Public Sub New(fields As IEntityFields2)
            MyBase.New("CustomBankPropertyEntity")
            InitClassEmpty(Nothing, fields)
        End Sub

        Public Sub New(validator As IValidator)
            MyBase.New("CustomBankPropertyEntity")
            InitClassEmpty(validator, Nothing)
        End Sub

        Public Sub New(customBankPropertyId As System.Guid)
            MyBase.New("CustomBankPropertyEntity")
            InitClassEmpty(Nothing, Nothing)
            Me.CustomBankPropertyId = customBankPropertyId
        End Sub

        Public Sub New(customBankPropertyId As System.Guid, validator As IValidator)
            MyBase.New("CustomBankPropertyEntity")
            InitClassEmpty(validator, Nothing)
            Me.CustomBankPropertyId = customBankPropertyId
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Sub New(info As SerializationInfo, context As StreamingContext)
            MyBase.New(info, context)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                _customBankPropertyValueCollection = CType(info.GetValue("_customBankPropertyValueCollection", GetType(EntityCollection(Of CustomBankPropertyValueEntity))), EntityCollection(Of CustomBankPropertyValueEntity))
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
                Me.FixupDeserialization(FieldInfoProviderSingleton.GetInstance())
            End If

        End Sub


        Protected Overrides Sub PerformDesyncSetupFKFieldChange(fieldIndex As Integer)
            Select Case CType(fieldIndex, CustomBankPropertyFieldIndex)

                Case CustomBankPropertyFieldIndex.BankId
                    DesetupSyncBank(True, False)






                Case CustomBankPropertyFieldIndex.CreatedBy
                    DesetupSyncCreatedByUser(True, False)

                Case CustomBankPropertyFieldIndex.ModifiedBy
                    DesetupSyncModifiedByUser(True, False)

                Case CustomBankPropertyFieldIndex.StateId
                    DesetupSyncState(True, False)

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

                Case Else
                    Me.OnSetRelatedEntityProperty(propertyName, entity)
            End Select
        End Sub

        Protected Overrides Function GetRelationsForFieldOfType(fieldName As String) As RelationCollection
            Return CustomBankPropertyEntity.GetRelationsForField(fieldName)
        End Function

        Friend Shared Function GetRelationsForField(fieldName As String) As RelationCollection
            Dim toReturn As New RelationCollection()
            Select Case fieldName
                Case "Bank"
                    toReturn.Add(CustomBankPropertyEntity.Relations.BankEntityUsingBankId)
                Case "State"
                    toReturn.Add(CustomBankPropertyEntity.Relations.StateEntityUsingStateId)
                Case "CreatedByUser"
                    toReturn.Add(CustomBankPropertyEntity.Relations.UserEntityUsingCreatedBy)
                Case "ModifiedByUser"
                    toReturn.Add(CustomBankPropertyEntity.Relations.UserEntityUsingModifiedBy)
                Case "CustomBankPropertyValueCollection"
                    toReturn.Add(CustomBankPropertyEntity.Relations.CustomBankPropertyValueEntityUsingCustomBankPropertyId)
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
                Case Else
            End Select
        End Sub

        Protected Overrides Function GetDependingRelatedEntities() As List(Of IEntity2)
            Dim toReturn As New List(Of IEntity2)()
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
            Return toReturn
        End Function

        Public Shared Function GetEntityTypeFilter() As IPredicateExpression
            Return InheritanceInfoProviderSingleton.GetInstance().GetEntityTypeFilter("CustomBankPropertyEntity", False)
        End Function

        Public Shared Function GetEntityTypeFilter(negate As Boolean) As IPredicateExpression
            Return InheritanceInfoProviderSingleton.GetInstance().GetEntityTypeFilter("CustomBankPropertyEntity", negate)
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
            End If

            MyBase.GetObjectData(info, context)
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Function CheckIfIsSubTypeOf(typeOfEntity As Integer) As Boolean
            Return InheritanceInfoProviderSingleton.GetInstance().CheckIfIsSubTypeOf("CustomBankPropertyEntity", CType(typeOfEntity, Questify.Builder.Model.ContentModel.EntityType).ToString())
        End Function

        Protected Overrides Overloads Function GetAllRelations() As List(Of IEntityRelation)
            Return New CustomBankPropertyRelations().GetAllRelations()
        End Function

        Public Overridable Function GetRelationInfoCustomBankPropertyValueCollection() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(CustomBankPropertyValueFields.CustomBankPropertyId, Nothing, ComparisonOperator.Equal, Me.CustomBankPropertyId))
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

        Protected Overrides Function CreateEntityFactory() As IEntityFactory2
            Return EntityFactoryCache2.GetEntityFactory(GetType(CustomBankPropertyEntityFactory))
        End Function
#If Not CF Then
        Protected Overrides Sub AddToMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2))
            MyBase.AddToMemberEntityCollectionsQueue(collectionsQueue)
            collectionsQueue.Enqueue(_customBankPropertyValueCollection)
        End Sub

        Protected Overrides Sub GetFromMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2))
            MyBase.GetFromMemberEntityCollectionsQueue(collectionsQueue)
            _customBankPropertyValueCollection = CType(collectionsQueue.Dequeue(), EntityCollection(Of CustomBankPropertyValueEntity))
        End Sub

        Protected Overrides Function HasPopulatedMemberEntityCollections() As Boolean
            If (Not _customBankPropertyValueCollection Is Nothing) Then
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
        End Sub
#End If
        Protected Overrides Overloads Function GetRelatedData() As Dictionary(Of String, Object)
            Dim toReturn As New Dictionary(Of String, Object)()
            toReturn.Add("Bank", _bank)
            toReturn.Add("State", _state)
            toReturn.Add("CreatedByUser", _createdByUser)
            toReturn.Add("ModifiedByUser", _modifiedByUser)
            toReturn.Add("CustomBankPropertyValueCollection", _customBankPropertyValueCollection)
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
            _fieldsCustomProperties.Add("CustomBankPropertyId", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("BankId", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("ApplicableToMask", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("Publishable", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("Scorable", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("Name", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("Title", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("Description", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("CreationDate", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("CreatedBy", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("ModifiedDate", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("ModifiedBy", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("Code", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("StateId", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("Version", fieldHashtable)
        End Sub


        Private Sub DesetupSyncBank(signalRelatedEntity As Boolean, resetFKFields As Boolean)
            Me.PerformDesetupSyncRelatedEntity(_bank, AddressOf OnBankPropertyChanged, "Bank", Questify.Builder.Model.ContentModel.RelationClasses.StaticCustomBankPropertyRelations.BankEntityUsingBankIdStatic, True, signalRelatedEntity, "CustomBankPropertyCollection", resetFKFields, New Integer() {CInt(CustomBankPropertyFieldIndex.BankId)})
            _bank = Nothing
        End Sub

        Private Sub SetupSyncBank(relatedEntity As IEntityCore)
            If Not _bank Is relatedEntity Then
                DesetupSyncBank(True, True)
                _bank = CType(relatedEntity, BankEntity)
                Me.PerformSetupSyncRelatedEntity(_bank, AddressOf OnBankPropertyChanged, "Bank", Questify.Builder.Model.ContentModel.RelationClasses.StaticCustomBankPropertyRelations.BankEntityUsingBankIdStatic, True, New String() {"BankName"})
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
            Me.PerformDesetupSyncRelatedEntity(_state, AddressOf OnStatePropertyChanged, "State", Questify.Builder.Model.ContentModel.RelationClasses.StaticCustomBankPropertyRelations.StateEntityUsingStateIdStatic, True, signalRelatedEntity, "CustomBankPropertyCollection", resetFKFields, New Integer() {CInt(CustomBankPropertyFieldIndex.StateId)})
            _state = Nothing
        End Sub

        Private Sub SetupSyncState(relatedEntity As IEntityCore)
            If Not _state Is relatedEntity Then
                DesetupSyncState(True, True)
                _state = CType(relatedEntity, StateEntity)
                Me.PerformSetupSyncRelatedEntity(_state, AddressOf OnStatePropertyChanged, "State", Questify.Builder.Model.ContentModel.RelationClasses.StaticCustomBankPropertyRelations.StateEntityUsingStateIdStatic, True, New String() {"StateName"})
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
            Me.PerformDesetupSyncRelatedEntity(_createdByUser, AddressOf OnCreatedByUserPropertyChanged, "CreatedByUser", Questify.Builder.Model.ContentModel.RelationClasses.StaticCustomBankPropertyRelations.UserEntityUsingCreatedByStatic, True, signalRelatedEntity, "CustomBankPropertyCollection_", resetFKFields, New Integer() {CInt(CustomBankPropertyFieldIndex.CreatedBy)})
            _createdByUser = Nothing
        End Sub

        Private Sub SetupSyncCreatedByUser(relatedEntity As IEntityCore)
            If Not _createdByUser Is relatedEntity Then
                DesetupSyncCreatedByUser(True, True)
                _createdByUser = CType(relatedEntity, UserEntity)
                Me.PerformSetupSyncRelatedEntity(_createdByUser, AddressOf OnCreatedByUserPropertyChanged, "CreatedByUser", Questify.Builder.Model.ContentModel.RelationClasses.StaticCustomBankPropertyRelations.UserEntityUsingCreatedByStatic, True, New String() {"CreatedByFullName"})
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
            Me.PerformDesetupSyncRelatedEntity(_modifiedByUser, AddressOf OnModifiedByUserPropertyChanged, "ModifiedByUser", Questify.Builder.Model.ContentModel.RelationClasses.StaticCustomBankPropertyRelations.UserEntityUsingModifiedByStatic, True, signalRelatedEntity, "CustomBankPropertyCollection", resetFKFields, New Integer() {CInt(CustomBankPropertyFieldIndex.ModifiedBy)})
            _modifiedByUser = Nothing
        End Sub

        Private Sub SetupSyncModifiedByUser(relatedEntity As IEntityCore)
            If Not _modifiedByUser Is relatedEntity Then
                DesetupSyncModifiedByUser(True, True)
                _modifiedByUser = CType(relatedEntity, UserEntity)
                Me.PerformSetupSyncRelatedEntity(_modifiedByUser, AddressOf OnModifiedByUserPropertyChanged, "ModifiedByUser", Questify.Builder.Model.ContentModel.RelationClasses.StaticCustomBankPropertyRelations.UserEntityUsingModifiedByStatic, True, New String() {"ModifiedByFullName"})
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

        Public Shared ReadOnly Property Relations() As CustomBankPropertyRelations
            Get
                Return New CustomBankPropertyRelations()
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
                    CType(GetRelationsForField("CustomBankPropertyValueCollection")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.CustomBankPropertyEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.CustomBankPropertyValueEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "CustomBankPropertyValueCollection", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany)
            End Get
        End Property


        Public Shared ReadOnly Property PrefetchPathBank() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(EntityFactoryCache2.GetEntityFactory(GetType(BankEntityFactory))), _
                    CType(GetRelationsForField("Bank")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.CustomBankPropertyEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.BankEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "Bank", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathState() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(EntityFactoryCache2.GetEntityFactory(GetType(StateEntityFactory))), _
                    CType(GetRelationsForField("State")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.CustomBankPropertyEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.StateEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "State", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathCreatedByUser() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(EntityFactoryCache2.GetEntityFactory(GetType(UserEntityFactory))), _
                    CType(GetRelationsForField("CreatedByUser")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.CustomBankPropertyEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.UserEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "CreatedByUser", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathModifiedByUser() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(EntityFactoryCache2.GetEntityFactory(GetType(UserEntityFactory))), _
                    CType(GetRelationsForField("ModifiedByUser")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.CustomBankPropertyEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.UserEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "ModifiedByUser", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne)
            End Get
        End Property


        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property CustomPropertiesOfType() As Dictionary(Of String, String)
            Get
                Return CustomBankPropertyEntity.CustomProperties
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
                Return CustomBankPropertyEntity.FieldsCustomProperties
            End Get
        End Property

        Public Overridable Property [CustomBankPropertyId]() As System.Guid
            Get
                Return CType(GetValue(CInt(CustomBankPropertyFieldIndex.CustomBankPropertyId), True), System.Guid)
            End Get
            Set
                SetValue(CInt(CustomBankPropertyFieldIndex.CustomBankPropertyId), value)
            End Set
        End Property
        Public Overridable Property [BankId]() As System.Int32
            Get
                Return CType(GetValue(CInt(CustomBankPropertyFieldIndex.BankId), True), System.Int32)
            End Get
            Set
                SetValue(CInt(CustomBankPropertyFieldIndex.BankId), value)
            End Set
        End Property
        Public Overridable Property [ApplicableToMask]() As Nullable(Of System.Int32)
            Get
                Return CType(GetValue(CInt(CustomBankPropertyFieldIndex.ApplicableToMask), False), Nullable(Of System.Int32))
            End Get
            Set
                SetValue(CInt(CustomBankPropertyFieldIndex.ApplicableToMask), value)
            End Set
        End Property
        Public Overridable Property [Publishable]() As System.Boolean
            Get
                Return CType(GetValue(CInt(CustomBankPropertyFieldIndex.Publishable), True), System.Boolean)
            End Get
            Set
                SetValue(CInt(CustomBankPropertyFieldIndex.Publishable), value)
            End Set
        End Property

        Public Overridable Property [Scorable]() As System.Boolean
            Get
                Return CType(GetValue(CInt(CustomBankPropertyFieldIndex.Scorable), True), System.Boolean)
            End Get
            Set
                SetValue(CInt(CustomBankPropertyFieldIndex.Scorable), Value)
            End Set
        End Property

        Public Overridable Property [Name]() As System.String
            Get
                Return CType(GetValue(CInt(CustomBankPropertyFieldIndex.Name), True), System.String)
            End Get
            Set
                SetValue(CInt(CustomBankPropertyFieldIndex.Name), value)
            End Set
        End Property
        Public Overridable Property [Title]() As System.String
            Get
                Return CType(GetValue(CInt(CustomBankPropertyFieldIndex.Title), True), System.String)
            End Get
            Set
                SetValue(CInt(CustomBankPropertyFieldIndex.Title), value)
            End Set
        End Property
        Public Overridable Property [Description]() As System.String
            Get
                Return CType(GetValue(CInt(CustomBankPropertyFieldIndex.Description), True), System.String)
            End Get
            Set
                SetValue(CInt(CustomBankPropertyFieldIndex.Description), value)
            End Set
        End Property
        Public Overridable Property [CreationDate]() As System.DateTime
            Get
                Return CType(GetValue(CInt(CustomBankPropertyFieldIndex.CreationDate), True), System.DateTime)
            End Get
            Set
                SetValue(CInt(CustomBankPropertyFieldIndex.CreationDate), value)
            End Set
        End Property
        Public Overridable Property [CreatedBy]() As System.Int32
            Get
                Return CType(GetValue(CInt(CustomBankPropertyFieldIndex.CreatedBy), True), System.Int32)
            End Get
            Set
                SetValue(CInt(CustomBankPropertyFieldIndex.CreatedBy), value)
            End Set
        End Property
        Public Overridable Property [ModifiedDate]() As System.DateTime
            Get
                Return CType(GetValue(CInt(CustomBankPropertyFieldIndex.ModifiedDate), True), System.DateTime)
            End Get
            Set
                SetValue(CInt(CustomBankPropertyFieldIndex.ModifiedDate), value)
            End Set
        End Property
        Public Overridable Property [ModifiedBy]() As System.Int32
            Get
                Return CType(GetValue(CInt(CustomBankPropertyFieldIndex.ModifiedBy), True), System.Int32)
            End Get
            Set
                SetValue(CInt(CustomBankPropertyFieldIndex.ModifiedBy), value)
            End Set
        End Property
        Public Overridable Property [Code]() As System.Guid
            Get
                Return CType(GetValue(CInt(CustomBankPropertyFieldIndex.Code), True), System.Guid)
            End Get
            Set
                SetValue(CInt(CustomBankPropertyFieldIndex.Code), value)
            End Set
        End Property
        Public Overridable Property [StateId]() As Nullable(Of System.Int32)
            Get
                Return CType(GetValue(CInt(CustomBankPropertyFieldIndex.StateId), False), Nullable(Of System.Int32))
            End Get
            Set
                SetValue(CInt(CustomBankPropertyFieldIndex.StateId), value)
            End Set
        End Property
        Public Overridable Property [Version]() As System.String
            Get
                Return CType(GetValue(CInt(CustomBankPropertyFieldIndex.Version), True), System.String)
            End Get
            Set
                SetValue(CInt(CustomBankPropertyFieldIndex.Version), value)
            End Set
        End Property

        <TypeContainedAttribute(GetType(CustomBankPropertyValueEntity))> _
        Public Overridable ReadOnly Property [CustomBankPropertyValueCollection]() As EntityCollection(Of CustomBankPropertyValueEntity)
            Get
                If _customBankPropertyValueCollection Is Nothing Then
                    _customBankPropertyValueCollection = New EntityCollection(Of CustomBankPropertyValueEntity)(EntityFactoryCache2.GetEntityFactory(GetType(CustomBankPropertyValueEntityFactory)))
                    _customBankPropertyValueCollection.ActiveContext = Me.ActiveContext
                    _customBankPropertyValueCollection.SetContainingEntityInfo(Me, "CustomBankProperty")
                End If
                Return _customBankPropertyValueCollection
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
                    SetSingleRelatedEntityNavigator(value, "CustomBankPropertyCollection", "Bank", _bank, True)
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
                    SetSingleRelatedEntityNavigator(value, "CustomBankPropertyCollection", "State", _state, True)
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
                    SetSingleRelatedEntityNavigator(value, "CustomBankPropertyCollection_", "CreatedByUser", _createdByUser, True)
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
                    SetSingleRelatedEntityNavigator(value, "CustomBankPropertyCollection", "ModifiedByUser", _modifiedByUser, True)
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
                Return CInt(Questify.Builder.Model.ContentModel.EntityType.CustomBankPropertyEntity)
            End Get
        End Property






    End Class
End Namespace
