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
    Public Class RolePermissionEntity
        Inherits CommonEntityBase



        Private WithEvents _permission As PermissionEntity
        Private WithEvents _permissionTarget As PermissionTargetEntity
        Private WithEvents _role As RoleEntity



        Private Shared _customProperties As Dictionary(Of String, String)
        Private Shared _fieldsCustomProperties As Dictionary(Of String, Dictionary(Of String, String))

        Public NotInheritable Class MemberNames
            Private Sub New()
            End Sub
            Public Shared ReadOnly [Permission] As String = "Permission"
            Public Shared ReadOnly [PermissionTarget] As String = "PermissionTarget"
            Public Shared ReadOnly [Role] As String = "Role"
        End Class

        Shared Sub New()
            SetupCustomPropertyHashtables()
        End Sub

        Public Sub New()
            MyBase.New("RolePermissionEntity")
            InitClassEmpty(Nothing, Nothing)
        End Sub

        Public Sub New(fields As IEntityFields2)
            MyBase.New("RolePermissionEntity")
            InitClassEmpty(Nothing, fields)
        End Sub

        Public Sub New(validator As IValidator)
            MyBase.New("RolePermissionEntity")
            InitClassEmpty(validator, Nothing)
        End Sub

        Public Sub New(roleId As System.Int32, permissionTargetId As System.Int32, permissionId As System.Int32)
            MyBase.New("RolePermissionEntity")
            InitClassEmpty(Nothing, Nothing)
            Me.RoleId = roleId
            Me.PermissionTargetId = permissionTargetId
            Me.PermissionId = permissionId
        End Sub

        Public Sub New(roleId As System.Int32, permissionTargetId As System.Int32, permissionId As System.Int32, validator As IValidator)
            MyBase.New("RolePermissionEntity")
            InitClassEmpty(validator, Nothing)
            Me.RoleId = roleId
            Me.PermissionTargetId = permissionTargetId
            Me.PermissionId = permissionId
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Sub New(info As SerializationInfo, context As StreamingContext)
            MyBase.New(info, context)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                _permission = CType(info.GetValue("_permission", GetType(PermissionEntity)), PermissionEntity)
                If Not _permission Is Nothing Then
                    AddHandler _permission.AfterSave, AddressOf OnEntityAfterSave
                End If
                _permissionTarget = CType(info.GetValue("_permissionTarget", GetType(PermissionTargetEntity)), PermissionTargetEntity)
                If Not _permissionTarget Is Nothing Then
                    AddHandler _permissionTarget.AfterSave, AddressOf OnEntityAfterSave
                End If
                _role = CType(info.GetValue("_role", GetType(RoleEntity)), RoleEntity)
                If Not _role Is Nothing Then
                    AddHandler _role.AfterSave, AddressOf OnEntityAfterSave
                End If
                Me.FixupDeserialization(FieldInfoProviderSingleton.GetInstance())
            End If

        End Sub


        Protected Overrides Sub PerformDesyncSetupFKFieldChange(fieldIndex As Integer)
            Select Case CType(fieldIndex, RolePermissionFieldIndex)
                Case RolePermissionFieldIndex.RoleId
                    DesetupSyncRole(True, False)
                Case RolePermissionFieldIndex.PermissionTargetId
                    DesetupSyncPermissionTarget(True, False)
                Case RolePermissionFieldIndex.PermissionId
                    DesetupSyncPermission(True, False)




                Case Else
                    MyBase.PerformDesyncSetupFKFieldChange(fieldIndex)
            End Select
        End Sub


        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub SetRelatedEntityProperty(propertyName As String, entity As IEntityCore)
            Select Case propertyName
                Case "Permission"
                    Me.Permission = CType(entity, PermissionEntity)
                Case "PermissionTarget"
                    Me.PermissionTarget = CType(entity, PermissionTargetEntity)
                Case "Role"
                    Me.Role = CType(entity, RoleEntity)

                Case Else
                    Me.OnSetRelatedEntityProperty(propertyName, entity)
            End Select
        End Sub

        Protected Overrides Function GetRelationsForFieldOfType(fieldName As String) As RelationCollection
            Return RolePermissionEntity.GetRelationsForField(fieldName)
        End Function

        Friend Shared Function GetRelationsForField(fieldName As String) As RelationCollection
            Dim toReturn As New RelationCollection()
            Select Case fieldName
                Case "Permission"
                    toReturn.Add(RolePermissionEntity.Relations.PermissionEntityUsingPermissionId)
                Case "PermissionTarget"
                    toReturn.Add(RolePermissionEntity.Relations.PermissionTargetEntityUsingPermissionTargetId)
                Case "Role"
                    toReturn.Add(RolePermissionEntity.Relations.RoleEntityUsingRoleId)
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
                Case "Permission"
                    SetupSyncPermission(relatedEntity)
                Case "PermissionTarget"
                    SetupSyncPermissionTarget(relatedEntity)
                Case "Role"
                    SetupSyncRole(relatedEntity)

                Case Else
            End Select
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub UnsetRelatedEntity(relatedEntity As IEntityCore, fieldName As String, signalRelatedEntityManyToOne As Boolean)
            Select Case fieldName
                Case "Permission"
                    DesetupSyncPermission(False, True)
                Case "PermissionTarget"
                    DesetupSyncPermissionTarget(False, True)
                Case "Role"
                    DesetupSyncRole(False, True)
                Case Else
            End Select
        End Sub

        Protected Overrides Function GetDependingRelatedEntities() As List(Of IEntity2)
            Dim toReturn As New List(Of IEntity2)()
            Return toReturn
        End Function

        Protected Overrides Function GetDependentRelatedEntities() As List(Of IEntity2)
            Dim toReturn As New List(Of IEntity2)()
            If Not _permission Is Nothing Then
                toReturn.Add(_permission)
            End If
            If Not _permissionTarget Is Nothing Then
                toReturn.Add(_permissionTarget)
            End If
            If Not _role Is Nothing Then
                toReturn.Add(_role)
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
                    entityValue = _permission
                End If
                info.AddValue("_permission", entityValue)
                entityValue = Nothing
                If Not Me.MarkedForDeletion Then
                    entityValue = _permissionTarget
                End If
                info.AddValue("_permissionTarget", entityValue)
                entityValue = Nothing
                If Not Me.MarkedForDeletion Then
                    entityValue = _role
                End If
                info.AddValue("_role", entityValue)
            End If

            MyBase.GetObjectData(info, context)
        End Sub


        Protected Overrides Overloads Function GetAllRelations() As List(Of IEntityRelation)
            Return New RolePermissionRelations().GetAllRelations()
        End Function

        Public Overridable Function GetRelationInfoPermission() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(PermissionFields.Id, Nothing, ComparisonOperator.Equal, Me.PermissionId))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoPermissionTarget() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(PermissionTargetFields.Id, Nothing, ComparisonOperator.Equal, Me.PermissionTargetId))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoRole() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(RoleFields.Id, Nothing, ComparisonOperator.Equal, Me.RoleId))
            Return bucket
        End Function

        Protected Overrides Function CreateEntityFactory() As IEntityFactory2
            Return EntityFactoryCache2.GetEntityFactory(GetType(RolePermissionEntityFactory))
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
            toReturn.Add("Permission", _permission)
            toReturn.Add("PermissionTarget", _permissionTarget)
            toReturn.Add("Role", _role)
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
            _fieldsCustomProperties.Add("RoleId", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("PermissionTargetId", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("PermissionId", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("CreationDate", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("CreatedBy", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("ModifiedDate", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("ModifiedBy", fieldHashtable)
        End Sub


        Private Sub DesetupSyncPermission(signalRelatedEntity As Boolean, resetFKFields As Boolean)
            Me.PerformDesetupSyncRelatedEntity(_permission, AddressOf OnPermissionPropertyChanged, "Permission", Questify.Builder.Model.ContentModel.RelationClasses.StaticRolePermissionRelations.PermissionEntityUsingPermissionIdStatic, True, signalRelatedEntity, "RolePermissionCollection", resetFKFields, New Integer() {CInt(RolePermissionFieldIndex.PermissionId)})
            _permission = Nothing
        End Sub

        Private Sub SetupSyncPermission(relatedEntity As IEntityCore)
            If Not _permission Is relatedEntity Then
                DesetupSyncPermission(True, True)
                _permission = CType(relatedEntity, PermissionEntity)
                Me.PerformSetupSyncRelatedEntity(_permission, AddressOf OnPermissionPropertyChanged, "Permission", Questify.Builder.Model.ContentModel.RelationClasses.StaticRolePermissionRelations.PermissionEntityUsingPermissionIdStatic, True, New String() {})
            End If
        End Sub

        Private Sub OnPermissionPropertyChanged(sender As Object, e As PropertyChangedEventArgs)
            Select Case e.PropertyName

                Case Else
            End Select
        End Sub

        Private Sub DesetupSyncPermissionTarget(signalRelatedEntity As Boolean, resetFKFields As Boolean)
            Me.PerformDesetupSyncRelatedEntity(_permissionTarget, AddressOf OnPermissionTargetPropertyChanged, "PermissionTarget", Questify.Builder.Model.ContentModel.RelationClasses.StaticRolePermissionRelations.PermissionTargetEntityUsingPermissionTargetIdStatic, True, signalRelatedEntity, "RolePermissionCollection", resetFKFields, New Integer() {CInt(RolePermissionFieldIndex.PermissionTargetId)})
            _permissionTarget = Nothing
        End Sub

        Private Sub SetupSyncPermissionTarget(relatedEntity As IEntityCore)
            If Not _permissionTarget Is relatedEntity Then
                DesetupSyncPermissionTarget(True, True)
                _permissionTarget = CType(relatedEntity, PermissionTargetEntity)
                Me.PerformSetupSyncRelatedEntity(_permissionTarget, AddressOf OnPermissionTargetPropertyChanged, "PermissionTarget", Questify.Builder.Model.ContentModel.RelationClasses.StaticRolePermissionRelations.PermissionTargetEntityUsingPermissionTargetIdStatic, True, New String() {})
            End If
        End Sub

        Private Sub OnPermissionTargetPropertyChanged(sender As Object, e As PropertyChangedEventArgs)
            Select Case e.PropertyName

                Case Else
            End Select
        End Sub

        Private Sub DesetupSyncRole(signalRelatedEntity As Boolean, resetFKFields As Boolean)
            Me.PerformDesetupSyncRelatedEntity(_role, AddressOf OnRolePropertyChanged, "Role", Questify.Builder.Model.ContentModel.RelationClasses.StaticRolePermissionRelations.RoleEntityUsingRoleIdStatic, True, signalRelatedEntity, "RolePermissionCollection", resetFKFields, New Integer() {CInt(RolePermissionFieldIndex.RoleId)})
            _role = Nothing
        End Sub

        Private Sub SetupSyncRole(relatedEntity As IEntityCore)
            If Not _role Is relatedEntity Then
                DesetupSyncRole(True, True)
                _role = CType(relatedEntity, RoleEntity)
                Me.PerformSetupSyncRelatedEntity(_role, AddressOf OnRolePropertyChanged, "Role", Questify.Builder.Model.ContentModel.RelationClasses.StaticRolePermissionRelations.RoleEntityUsingRoleIdStatic, True, New String() {})
            End If
        End Sub

        Private Sub OnRolePropertyChanged(sender As Object, e As PropertyChangedEventArgs)
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

        Public Shared ReadOnly Property Relations() As RolePermissionRelations
            Get
                Return New RolePermissionRelations()
            End Get
        End Property

        Public Shared ReadOnly Property CustomProperties() As Dictionary(Of String, String)
            Get
                Return _customProperties
            End Get
        End Property




        Public Shared ReadOnly Property PrefetchPathPermission() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(EntityFactoryCache2.GetEntityFactory(GetType(PermissionEntityFactory))), _
                    CType(GetRelationsForField("Permission")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.RolePermissionEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.PermissionEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "Permission", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathPermissionTarget() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(EntityFactoryCache2.GetEntityFactory(GetType(PermissionTargetEntityFactory))), _
                    CType(GetRelationsForField("PermissionTarget")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.RolePermissionEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.PermissionTargetEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "PermissionTarget", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathRole() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(EntityFactoryCache2.GetEntityFactory(GetType(RoleEntityFactory))), _
                    CType(GetRelationsForField("Role")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.RolePermissionEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.RoleEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "Role", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne)
            End Get
        End Property


        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property CustomPropertiesOfType() As Dictionary(Of String, String)
            Get
                Return RolePermissionEntity.CustomProperties
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
                Return RolePermissionEntity.FieldsCustomProperties
            End Get
        End Property

        Public Overridable Property [RoleId]() As System.Int32
            Get
                Return CType(GetValue(CInt(RolePermissionFieldIndex.RoleId), True), System.Int32)
            End Get
            Set
                SetValue(CInt(RolePermissionFieldIndex.RoleId), value)
            End Set
        End Property
        Public Overridable Property [PermissionTargetId]() As System.Int32
            Get
                Return CType(GetValue(CInt(RolePermissionFieldIndex.PermissionTargetId), True), System.Int32)
            End Get
            Set
                SetValue(CInt(RolePermissionFieldIndex.PermissionTargetId), value)
            End Set
        End Property
        Public Overridable Property [PermissionId]() As System.Int32
            Get
                Return CType(GetValue(CInt(RolePermissionFieldIndex.PermissionId), True), System.Int32)
            End Get
            Set
                SetValue(CInt(RolePermissionFieldIndex.PermissionId), value)
            End Set
        End Property
        Public Overridable Property [CreationDate]() As System.DateTime
            Get
                Return CType(GetValue(CInt(RolePermissionFieldIndex.CreationDate), True), System.DateTime)
            End Get
            Set
                SetValue(CInt(RolePermissionFieldIndex.CreationDate), value)
            End Set
        End Property
        Public Overridable Property [CreatedBy]() As System.Int32
            Get
                Return CType(GetValue(CInt(RolePermissionFieldIndex.CreatedBy), True), System.Int32)
            End Get
            Set
                SetValue(CInt(RolePermissionFieldIndex.CreatedBy), value)
            End Set
        End Property
        Public Overridable Property [ModifiedDate]() As System.DateTime
            Get
                Return CType(GetValue(CInt(RolePermissionFieldIndex.ModifiedDate), True), System.DateTime)
            End Get
            Set
                SetValue(CInt(RolePermissionFieldIndex.ModifiedDate), value)
            End Set
        End Property
        Public Overridable Property [ModifiedBy]() As System.Int32
            Get
                Return CType(GetValue(CInt(RolePermissionFieldIndex.ModifiedBy), True), System.Int32)
            End Get
            Set
                SetValue(CInt(RolePermissionFieldIndex.ModifiedBy), value)
            End Set
        End Property



        <Browsable(True)> _
        Public Overridable Property [Permission]() As PermissionEntity
            Get
                Return _permission
            End Get
            Set
                If MyBase.IsDeserializing Then
                    SetupSyncPermission(value)
                Else
                    SetSingleRelatedEntityNavigator(value, "RolePermissionCollection", "Permission", _permission, True)
                End If
            End Set
        End Property

        <Browsable(True)> _
        Public Overridable Property [PermissionTarget]() As PermissionTargetEntity
            Get
                Return _permissionTarget
            End Get
            Set
                If MyBase.IsDeserializing Then
                    SetupSyncPermissionTarget(value)
                Else
                    SetSingleRelatedEntityNavigator(value, "RolePermissionCollection", "PermissionTarget", _permissionTarget, True)
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
                    SetSingleRelatedEntityNavigator(value, "RolePermissionCollection", "Role", _role, True)
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
                Return CInt(Questify.Builder.Model.ContentModel.EntityType.RolePermissionEntity)
            End Get
        End Property






    End Class
End Namespace
