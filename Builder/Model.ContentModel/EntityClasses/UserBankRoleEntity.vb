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
    Public Class UserBankRoleEntity
        Inherits CommonEntityBase



        Private WithEvents _bank As BankEntity
        Private WithEvents _role As RoleEntity
        Private WithEvents _user As UserEntity



        Private Shared _customProperties As Dictionary(Of String, String)
        Private Shared _fieldsCustomProperties As Dictionary(Of String, Dictionary(Of String, String))

        Public NotInheritable Class MemberNames
            Private Sub New()
            End Sub
            Public Shared ReadOnly [Bank] As String = "Bank"
            Public Shared ReadOnly [Role] As String = "Role"
            Public Shared ReadOnly [User] As String = "User"
        End Class

        Shared Sub New()
            SetupCustomPropertyHashtables()
        End Sub

        Public Sub New()
            MyBase.New("UserBankRoleEntity")
            InitClassEmpty(Nothing, Nothing)
        End Sub

        Public Sub New(fields As IEntityFields2)
            MyBase.New("UserBankRoleEntity")
            InitClassEmpty(Nothing, fields)
        End Sub

        Public Sub New(validator As IValidator)
            MyBase.New("UserBankRoleEntity")
            InitClassEmpty(validator, Nothing)
        End Sub

        Public Sub New(userId As System.Int32, bankId As System.Int32, bankRoleId As System.Int32)
            MyBase.New("UserBankRoleEntity")
            InitClassEmpty(Nothing, Nothing)
            Me.UserId = userId
            Me.BankId = bankId
            Me.BankRoleId = bankRoleId
        End Sub

        Public Sub New(userId As System.Int32, bankId As System.Int32, bankRoleId As System.Int32, validator As IValidator)
            MyBase.New("UserBankRoleEntity")
            InitClassEmpty(validator, Nothing)
            Me.UserId = userId
            Me.BankId = bankId
            Me.BankRoleId = bankRoleId
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Sub New(info As SerializationInfo, context As StreamingContext)
            MyBase.New(info, context)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                _bank = CType(info.GetValue("_bank", GetType(BankEntity)), BankEntity)
                If Not _bank Is Nothing Then
                    AddHandler _bank.AfterSave, AddressOf OnEntityAfterSave
                End If
                _role = CType(info.GetValue("_role", GetType(RoleEntity)), RoleEntity)
                If Not _role Is Nothing Then
                    AddHandler _role.AfterSave, AddressOf OnEntityAfterSave
                End If
                _user = CType(info.GetValue("_user", GetType(UserEntity)), UserEntity)
                If Not _user Is Nothing Then
                    AddHandler _user.AfterSave, AddressOf OnEntityAfterSave
                End If
                Me.FixupDeserialization(FieldInfoProviderSingleton.GetInstance())
            End If

        End Sub


        Protected Overrides Sub PerformDesyncSetupFKFieldChange(fieldIndex As Integer)
            Select Case CType(fieldIndex, UserBankRoleFieldIndex)
                Case UserBankRoleFieldIndex.UserId
                    DesetupSyncUser(True, False)
                Case UserBankRoleFieldIndex.BankId
                    DesetupSyncBank(True, False)
                Case UserBankRoleFieldIndex.BankRoleId
                    DesetupSyncRole(True, False)




                Case Else
                    MyBase.PerformDesyncSetupFKFieldChange(fieldIndex)
            End Select
        End Sub


        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub SetRelatedEntityProperty(propertyName As String, entity As IEntityCore)
            Select Case propertyName
                Case "Bank"
                    Me.Bank = CType(entity, BankEntity)
                Case "Role"
                    Me.Role = CType(entity, RoleEntity)
                Case "User"
                    Me.User = CType(entity, UserEntity)

                Case Else
                    Me.OnSetRelatedEntityProperty(propertyName, entity)
            End Select
        End Sub

        Protected Overrides Function GetRelationsForFieldOfType(fieldName As String) As RelationCollection
            Return UserBankRoleEntity.GetRelationsForField(fieldName)
        End Function

        Friend Shared Function GetRelationsForField(fieldName As String) As RelationCollection
            Dim toReturn As New RelationCollection()
            Select Case fieldName
                Case "Bank"
                    toReturn.Add(UserBankRoleEntity.Relations.BankEntityUsingBankId)
                Case "Role"
                    toReturn.Add(UserBankRoleEntity.Relations.RoleEntityUsingBankRoleId)
                Case "User"
                    toReturn.Add(UserBankRoleEntity.Relations.UserEntityUsingUserId)
                Case Else
            End Select
            Return toReturn
        End Function
#If Not CF Then
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Function CheckOneWayRelations(propertyName As String) As Boolean
            Dim numberOfOneWayRelations As Integer = 0 + 1
            Select Case propertyName
                Case Nothing
                    Return ((numberOfOneWayRelations > 0) Or MyBase.CheckOneWayRelations(Nothing))
                Case "Bank"
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
                Case "Role"
                    SetupSyncRole(relatedEntity)
                Case "User"
                    SetupSyncUser(relatedEntity)

                Case Else
            End Select
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub UnsetRelatedEntity(relatedEntity As IEntityCore, fieldName As String, signalRelatedEntityManyToOne As Boolean)
            Select Case fieldName
                Case "Bank"
                    DesetupSyncBank(False, True)
                Case "Role"
                    DesetupSyncRole(False, True)
                Case "User"
                    DesetupSyncUser(False, True)
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
            If Not _role Is Nothing Then
                toReturn.Add(_role)
            End If
            If Not _user Is Nothing Then
                toReturn.Add(_user)
            End If
            Return toReturn
        End Function

        Protected Overrides Function GetMemberEntityCollections() As List(Of IEntityCollection2)
            Dim toReturn As New List(Of IEntityCollection2)()
            Return toReturn
        End Function


        Protected Overrides Sub GetObjectData(info As SerializationInfo, context As StreamingContext)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                Dim value As IEntityCollection2 = Nothing
                Dim entityValue As IEntity2 = Nothing
                entityValue = Nothing
                If Not Me.MarkedForDeletion Then
                    entityValue = _bank
                End If
                info.AddValue("_bank", entityValue)
                entityValue = Nothing
                If Not Me.MarkedForDeletion Then
                    entityValue = _role
                End If
                info.AddValue("_role", entityValue)
                entityValue = Nothing
                If Not Me.MarkedForDeletion Then
                    entityValue = _user
                End If
                info.AddValue("_user", entityValue)
            End If

            MyBase.GetObjectData(info, context)
        End Sub


        Protected Overrides Overloads Function GetAllRelations() As List(Of IEntityRelation)
            Return New UserBankRoleRelations().GetAllRelations()
        End Function

        Public Overridable Function GetRelationInfoBank() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(BankFields.Id, Nothing, ComparisonOperator.Equal, Me.BankId))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoRole() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(RoleFields.Id, Nothing, ComparisonOperator.Equal, Me.BankRoleId))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoUser() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(UserFields.Id, Nothing, ComparisonOperator.Equal, Me.UserId))
            Return bucket
        End Function

        Protected Overrides Function CreateEntityFactory() As IEntityFactory2
            Return EntityFactoryCache2.GetEntityFactory(GetType(UserBankRoleEntityFactory))
        End Function
#If Not CF Then
        Protected Overrides Sub AddToMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2))
            MyBase.AddToMemberEntityCollectionsQueue(collectionsQueue)
        End Sub

        Protected Overrides Sub GetFromMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2))
            MyBase.GetFromMemberEntityCollectionsQueue(collectionsQueue)
        End Sub

        Protected Overrides Function HasPopulatedMemberEntityCollections() As Boolean
            Return MyBase.HasPopulatedMemberEntityCollections()
        End Function

        Protected Overrides Overloads Sub CreateMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2), requiredQueue As Queue(Of Boolean))
            MyBase.CreateMemberEntityCollectionsQueue(collectionsQueue, requiredQueue)
            Dim toAdd As IEntityCollection2 = Nothing
        End Sub
#End If
        Protected Overrides Overloads Function GetRelatedData() As Dictionary(Of String, Object)
            Dim toReturn As New Dictionary(Of String, Object)()
            toReturn.Add("Bank", _bank)
            toReturn.Add("Role", _role)
            toReturn.Add("User", _user)
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
            _fieldsCustomProperties.Add("UserId", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("BankId", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("BankRoleId", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("CreationDate", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("CreatedBy", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("ModifiedDate", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("ModifiedBy", fieldHashtable)
        End Sub


        Private Sub DesetupSyncBank(signalRelatedEntity As Boolean, resetFKFields As Boolean)
            Me.PerformDesetupSyncRelatedEntity(_bank, AddressOf OnBankPropertyChanged, "Bank", Questify.Builder.Model.ContentModel.RelationClasses.StaticUserBankRoleRelations.BankEntityUsingBankIdStatic, True, signalRelatedEntity, "", resetFKFields, New Integer() {CInt(UserBankRoleFieldIndex.BankId)})
            _bank = Nothing
        End Sub

        Private Sub SetupSyncBank(relatedEntity As IEntityCore)
            If Not _bank Is relatedEntity Then
                DesetupSyncBank(True, True)
                _bank = CType(relatedEntity, BankEntity)
                Me.PerformSetupSyncRelatedEntity(_bank, AddressOf OnBankPropertyChanged, "Bank", Questify.Builder.Model.ContentModel.RelationClasses.StaticUserBankRoleRelations.BankEntityUsingBankIdStatic, True, New String() {})
            End If
        End Sub

        Private Sub OnBankPropertyChanged(sender As Object, e As PropertyChangedEventArgs)
            Select Case e.PropertyName

                Case Else
            End Select
        End Sub

        Private Sub DesetupSyncRole(signalRelatedEntity As Boolean, resetFKFields As Boolean)
            Me.PerformDesetupSyncRelatedEntity(_role, AddressOf OnRolePropertyChanged, "Role", Questify.Builder.Model.ContentModel.RelationClasses.StaticUserBankRoleRelations.RoleEntityUsingBankRoleIdStatic, True, signalRelatedEntity, "UserBankRoleCollection", resetFKFields, New Integer() {CInt(UserBankRoleFieldIndex.BankRoleId)})
            _role = Nothing
        End Sub

        Private Sub SetupSyncRole(relatedEntity As IEntityCore)
            If Not _role Is relatedEntity Then
                DesetupSyncRole(True, True)
                _role = CType(relatedEntity, RoleEntity)
                Me.PerformSetupSyncRelatedEntity(_role, AddressOf OnRolePropertyChanged, "Role", Questify.Builder.Model.ContentModel.RelationClasses.StaticUserBankRoleRelations.RoleEntityUsingBankRoleIdStatic, True, New String() {})
            End If
        End Sub

        Private Sub OnRolePropertyChanged(sender As Object, e As PropertyChangedEventArgs)
            Select Case e.PropertyName

                Case Else
            End Select
        End Sub

        Private Sub DesetupSyncUser(signalRelatedEntity As Boolean, resetFKFields As Boolean)
            Me.PerformDesetupSyncRelatedEntity(_user, AddressOf OnUserPropertyChanged, "User", Questify.Builder.Model.ContentModel.RelationClasses.StaticUserBankRoleRelations.UserEntityUsingUserIdStatic, True, signalRelatedEntity, "UserBankRoleCollection", resetFKFields, New Integer() {CInt(UserBankRoleFieldIndex.UserId)})
            _user = Nothing
        End Sub

        Private Sub SetupSyncUser(relatedEntity As IEntityCore)
            If Not _user Is relatedEntity Then
                DesetupSyncUser(True, True)
                _user = CType(relatedEntity, UserEntity)
                Me.PerformSetupSyncRelatedEntity(_user, AddressOf OnUserPropertyChanged, "User", Questify.Builder.Model.ContentModel.RelationClasses.StaticUserBankRoleRelations.UserEntityUsingUserIdStatic, True, New String() {})
            End If
        End Sub

        Private Sub OnUserPropertyChanged(sender As Object, e As PropertyChangedEventArgs)
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

        Public Shared ReadOnly Property Relations() As UserBankRoleRelations
            Get
                Return New UserBankRoleRelations()
            End Get
        End Property

        Public Shared ReadOnly Property CustomProperties() As Dictionary(Of String, String)
            Get
                Return _customProperties
            End Get
        End Property




        Public Shared ReadOnly Property PrefetchPathBank() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(EntityFactoryCache2.GetEntityFactory(GetType(BankEntityFactory))), _
                    CType(GetRelationsForField("Bank")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.UserBankRoleEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.BankEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "Bank", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathRole() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(EntityFactoryCache2.GetEntityFactory(GetType(RoleEntityFactory))), _
                    CType(GetRelationsForField("Role")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.UserBankRoleEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.RoleEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "Role", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathUser() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(EntityFactoryCache2.GetEntityFactory(GetType(UserEntityFactory))), _
                    CType(GetRelationsForField("User")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.UserBankRoleEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.UserEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "User", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne)
            End Get
        End Property


        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property CustomPropertiesOfType() As Dictionary(Of String, String)
            Get
                Return UserBankRoleEntity.CustomProperties
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
                Return UserBankRoleEntity.FieldsCustomProperties
            End Get
        End Property

        Public Overridable Property [UserId]() As System.Int32
            Get
                Return CType(GetValue(CInt(UserBankRoleFieldIndex.UserId), True), System.Int32)
            End Get
            Set
                SetValue(CInt(UserBankRoleFieldIndex.UserId), value)
            End Set
        End Property
        Public Overridable Property [BankId]() As System.Int32
            Get
                Return CType(GetValue(CInt(UserBankRoleFieldIndex.BankId), True), System.Int32)
            End Get
            Set
                SetValue(CInt(UserBankRoleFieldIndex.BankId), value)
            End Set
        End Property
        Public Overridable Property [BankRoleId]() As System.Int32
            Get
                Return CType(GetValue(CInt(UserBankRoleFieldIndex.BankRoleId), True), System.Int32)
            End Get
            Set
                SetValue(CInt(UserBankRoleFieldIndex.BankRoleId), value)
            End Set
        End Property
        Public Overridable Property [CreationDate]() As System.DateTime
            Get
                Return CType(GetValue(CInt(UserBankRoleFieldIndex.CreationDate), True), System.DateTime)
            End Get
            Set
                SetValue(CInt(UserBankRoleFieldIndex.CreationDate), value)
            End Set
        End Property
        Public Overridable Property [CreatedBy]() As System.Int32
            Get
                Return CType(GetValue(CInt(UserBankRoleFieldIndex.CreatedBy), True), System.Int32)
            End Get
            Set
                SetValue(CInt(UserBankRoleFieldIndex.CreatedBy), value)
            End Set
        End Property
        Public Overridable Property [ModifiedDate]() As System.DateTime
            Get
                Return CType(GetValue(CInt(UserBankRoleFieldIndex.ModifiedDate), True), System.DateTime)
            End Get
            Set
                SetValue(CInt(UserBankRoleFieldIndex.ModifiedDate), value)
            End Set
        End Property
        Public Overridable Property [ModifiedBy]() As System.Int32
            Get
                Return CType(GetValue(CInt(UserBankRoleFieldIndex.ModifiedBy), True), System.Int32)
            End Get
            Set
                SetValue(CInt(UserBankRoleFieldIndex.ModifiedBy), value)
            End Set
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
        Public Overridable Property [Role]() As RoleEntity
            Get
                Return _role
            End Get
            Set
                If MyBase.IsDeserializing Then
                    SetupSyncRole(value)
                Else
                    SetSingleRelatedEntityNavigator(value, "UserBankRoleCollection", "Role", _role, True)
                End If
            End Set
        End Property

        <Browsable(True)> _
        Public Overridable Property [User]() As UserEntity
            Get
                Return _user
            End Get
            Set
                If MyBase.IsDeserializing Then
                    SetupSyncUser(value)
                Else
                    SetSingleRelatedEntityNavigator(value, "UserBankRoleCollection", "User", _user, True)
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
                Return CInt(Questify.Builder.Model.ContentModel.EntityType.UserBankRoleEntity)
            End Get
        End Property






    End Class
End Namespace
