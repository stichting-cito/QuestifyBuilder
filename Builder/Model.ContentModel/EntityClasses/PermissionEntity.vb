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
    Public Class PermissionEntity
        Inherits CommonEntityBase



        Private WithEvents _rolePermissionCollection As EntityCollection(Of RolePermissionEntity)
        Private WithEvents _permissionTargetCollectionViaRolePermission As EntityCollection(Of PermissionTargetEntity)
        Private WithEvents _roleCollectionViaRolePermission As EntityCollection(Of RoleEntity)



        Private Shared _customProperties As Dictionary(Of String, String)
        Private Shared _fieldsCustomProperties As Dictionary(Of String, Dictionary(Of String, String))

        Public NotInheritable Class MemberNames
            Private Sub New()
            End Sub
            Public Shared ReadOnly [RolePermissionCollection] As String = "RolePermissionCollection"
            Public Shared ReadOnly [PermissionTargetCollectionViaRolePermission] As String = "PermissionTargetCollectionViaRolePermission"
            Public Shared ReadOnly [RoleCollectionViaRolePermission] As String = "RoleCollectionViaRolePermission"
        End Class

        Shared Sub New()
            SetupCustomPropertyHashtables()
        End Sub

        Public Sub New()
            MyBase.New("PermissionEntity")
            InitClassEmpty(Nothing, Nothing)
        End Sub

        Public Sub New(fields As IEntityFields2)
            MyBase.New("PermissionEntity")
            InitClassEmpty(Nothing, fields)
        End Sub

        Public Sub New(validator As IValidator)
            MyBase.New("PermissionEntity")
            InitClassEmpty(validator, Nothing)
        End Sub

        Public Sub New(id As System.Int32)
            MyBase.New("PermissionEntity")
            InitClassEmpty(Nothing, Nothing)
            Me.Id = id
        End Sub

        Public Sub New(id As System.Int32, validator As IValidator)
            MyBase.New("PermissionEntity")
            InitClassEmpty(validator, Nothing)
            Me.Id = id
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Sub New(info As SerializationInfo, context As StreamingContext)
            MyBase.New(info, context)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                _rolePermissionCollection = CType(info.GetValue("_rolePermissionCollection", GetType(EntityCollection(Of RolePermissionEntity))), EntityCollection(Of RolePermissionEntity))
                _permissionTargetCollectionViaRolePermission = CType(info.GetValue("_permissionTargetCollectionViaRolePermission", GetType(EntityCollection(Of PermissionTargetEntity))), EntityCollection(Of PermissionTargetEntity))
                _roleCollectionViaRolePermission = CType(info.GetValue("_roleCollectionViaRolePermission", GetType(EntityCollection(Of RoleEntity))), EntityCollection(Of RoleEntity))
                Me.FixupDeserialization(FieldInfoProviderSingleton.GetInstance())
            End If

        End Sub



        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub SetRelatedEntityProperty(propertyName As String, entity As IEntityCore)
            Select Case propertyName
                Case "RolePermissionCollection"
                    Me.RolePermissionCollection.Add(CType(entity, RolePermissionEntity))
                Case "PermissionTargetCollectionViaRolePermission"
                    Me.PermissionTargetCollectionViaRolePermission.IsReadOnly = False
                    Me.PermissionTargetCollectionViaRolePermission.Add(CType(entity, PermissionTargetEntity))
                    Me.PermissionTargetCollectionViaRolePermission.IsReadOnly = True
                Case "RoleCollectionViaRolePermission"
                    Me.RoleCollectionViaRolePermission.IsReadOnly = False
                    Me.RoleCollectionViaRolePermission.Add(CType(entity, RoleEntity))
                    Me.RoleCollectionViaRolePermission.IsReadOnly = True

                Case Else
                    Me.OnSetRelatedEntityProperty(propertyName, entity)
            End Select
        End Sub

        Protected Overrides Function GetRelationsForFieldOfType(fieldName As String) As RelationCollection
            Return PermissionEntity.GetRelationsForField(fieldName)
        End Function

        Friend Shared Function GetRelationsForField(fieldName As String) As RelationCollection
            Dim toReturn As New RelationCollection()
            Select Case fieldName
                Case "RolePermissionCollection"
                    toReturn.Add(PermissionEntity.Relations.RolePermissionEntityUsingPermissionId)
                Case "PermissionTargetCollectionViaRolePermission"
                    toReturn.Add(PermissionEntity.Relations.RolePermissionEntityUsingPermissionId, "PermissionEntity__", "RolePermission_", JoinHint.None)
                    toReturn.Add(RolePermissionEntity.Relations.PermissionTargetEntityUsingPermissionTargetId, "RolePermission_", String.Empty, JoinHint.None)
                Case "RoleCollectionViaRolePermission"
                    toReturn.Add(PermissionEntity.Relations.RolePermissionEntityUsingPermissionId, "PermissionEntity__", "RolePermission_", JoinHint.None)
                    toReturn.Add(RolePermissionEntity.Relations.RoleEntityUsingRoleId, "RolePermission_", String.Empty, JoinHint.None)
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
                Case "RolePermissionCollection"
                    Me.RolePermissionCollection.Add(CType(relatedEntity, RolePermissionEntity))

                Case Else
            End Select
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub UnsetRelatedEntity(relatedEntity As IEntityCore, fieldName As String, signalRelatedEntityManyToOne As Boolean)
            Select Case fieldName
                Case "RolePermissionCollection"
                    Me.PerformRelatedEntityRemoval(Me.RolePermissionCollection, relatedEntity, signalRelatedEntityManyToOne)
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
            toReturn.Add(Me.RolePermissionCollection)
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
                If (Not (_permissionTargetCollectionViaRolePermission Is Nothing)) AndAlso (_permissionTargetCollectionViaRolePermission.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _permissionTargetCollectionViaRolePermission
                End If
                info.AddValue("_permissionTargetCollectionViaRolePermission", value)
                value = Nothing
                If (Not (_roleCollectionViaRolePermission Is Nothing)) AndAlso (_roleCollectionViaRolePermission.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _roleCollectionViaRolePermission
                End If
                info.AddValue("_roleCollectionViaRolePermission", value)
            End If

            MyBase.GetObjectData(info, context)
        End Sub


        Protected Overrides Overloads Function GetAllRelations() As List(Of IEntityRelation)
            Return New PermissionRelations().GetAllRelations()
        End Function

        Public Overridable Function GetRelationInfoRolePermissionCollection() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(RolePermissionFields.PermissionId, Nothing, ComparisonOperator.Equal, Me.Id))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoPermissionTargetCollectionViaRolePermission() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.Relations.AddRange(GetRelationsForFieldOfType("PermissionTargetCollectionViaRolePermission"))
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(PermissionFields.Id, Nothing, ComparisonOperator.Equal, Me.Id, "PermissionEntity__"))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoRoleCollectionViaRolePermission() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.Relations.AddRange(GetRelationsForFieldOfType("RoleCollectionViaRolePermission"))
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(PermissionFields.Id, Nothing, ComparisonOperator.Equal, Me.Id, "PermissionEntity__"))
            Return bucket
        End Function

        Protected Overrides Function CreateEntityFactory() As IEntityFactory2
            Return EntityFactoryCache2.GetEntityFactory(GetType(PermissionEntityFactory))
        End Function
#If Not CF Then
        Protected Overrides Sub AddToMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2))
            MyBase.AddToMemberEntityCollectionsQueue(collectionsQueue)
            collectionsQueue.Enqueue(_rolePermissionCollection)
            collectionsQueue.Enqueue(_permissionTargetCollectionViaRolePermission)
            collectionsQueue.Enqueue(_roleCollectionViaRolePermission)
        End Sub

        Protected Overrides Sub GetFromMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2))
            MyBase.GetFromMemberEntityCollectionsQueue(collectionsQueue)
            _rolePermissionCollection = CType(collectionsQueue.Dequeue(), EntityCollection(Of RolePermissionEntity))
            _permissionTargetCollectionViaRolePermission = CType(collectionsQueue.Dequeue(), EntityCollection(Of PermissionTargetEntity))
            _roleCollectionViaRolePermission = CType(collectionsQueue.Dequeue(), EntityCollection(Of RoleEntity))
        End Sub

        Protected Overrides Function HasPopulatedMemberEntityCollections() As Boolean
            If (Not _rolePermissionCollection Is Nothing) Then
                Return True
            End If
            If (Not _permissionTargetCollectionViaRolePermission Is Nothing) Then
                Return True
            End If
            If (Not _roleCollectionViaRolePermission Is Nothing) Then
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
                toAdd = New EntityCollection(Of PermissionTargetEntity)(EntityFactoryCache2.GetEntityFactory(GetType(PermissionTargetEntityFactory)))
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
        End Sub
#End If
        Protected Overrides Overloads Function GetRelatedData() As Dictionary(Of String, Object)
            Dim toReturn As New Dictionary(Of String, Object)()
            toReturn.Add("RolePermissionCollection", _rolePermissionCollection)
            toReturn.Add("PermissionTargetCollectionViaRolePermission", _permissionTargetCollectionViaRolePermission)
            toReturn.Add("RoleCollectionViaRolePermission", _roleCollectionViaRolePermission)
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
            _fieldsCustomProperties.Add("WhenOwnerCondition", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("CreationDate", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("CreatedBy", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("ModifiedDate", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("ModifiedBy", fieldHashtable)
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

        Public Shared ReadOnly Property Relations() As PermissionRelations
            Get
                Return New PermissionRelations()
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
                    CType(GetRelationsForField("RolePermissionCollection")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.PermissionEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.RolePermissionEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "RolePermissionCollection", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathPermissionTargetCollectionViaRolePermission() As IPrefetchPathElement2
            Get
                Dim intermediateRelation As IEntityRelation = PermissionEntity.Relations.RolePermissionEntityUsingPermissionId
                intermediateRelation.SetAliases(String.Empty, "RolePermission_")
                Return New PrefetchPathElement2(New EntityCollection(Of PermissionTargetEntity)(EntityFactoryCache2.GetEntityFactory(GetType(PermissionTargetEntityFactory))), _
                    intermediateRelation, CType(Questify.Builder.Model.ContentModel.EntityType.PermissionEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.PermissionTargetEntity, Integer), 0, Nothing, Nothing, GetRelationsForField("PermissionTargetCollectionViaRolePermission"), Nothing, "PermissionTargetCollectionViaRolePermission", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathRoleCollectionViaRolePermission() As IPrefetchPathElement2
            Get
                Dim intermediateRelation As IEntityRelation = PermissionEntity.Relations.RolePermissionEntityUsingPermissionId
                intermediateRelation.SetAliases(String.Empty, "RolePermission_")
                Return New PrefetchPathElement2(New EntityCollection(Of RoleEntity)(EntityFactoryCache2.GetEntityFactory(GetType(RoleEntityFactory))), _
                    intermediateRelation, CType(Questify.Builder.Model.ContentModel.EntityType.PermissionEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.RoleEntity, Integer), 0, Nothing, Nothing, GetRelationsForField("RoleCollectionViaRolePermission"), Nothing, "RoleCollectionViaRolePermission", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToMany)
            End Get
        End Property



        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property CustomPropertiesOfType() As Dictionary(Of String, String)
            Get
                Return PermissionEntity.CustomProperties
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
                Return PermissionEntity.FieldsCustomProperties
            End Get
        End Property

        Public Overridable Property [Id]() As System.Int32
            Get
                Return CType(GetValue(CInt(PermissionFieldIndex.Id), True), System.Int32)
            End Get
            Set
                SetValue(CInt(PermissionFieldIndex.Id), value)
            End Set
        End Property
        Public Overridable Property [Name]() As System.String
            Get
                Return CType(GetValue(CInt(PermissionFieldIndex.Name), True), System.String)
            End Get
            Set
                SetValue(CInt(PermissionFieldIndex.Name), value)
            End Set
        End Property
        Public Overridable Property [Description]() As System.String
            Get
                Return CType(GetValue(CInt(PermissionFieldIndex.Description), True), System.String)
            End Get
            Set
                SetValue(CInt(PermissionFieldIndex.Description), value)
            End Set
        End Property
        Public Overridable Property [WhenOwnerCondition]() As System.Boolean
            Get
                Return CType(GetValue(CInt(PermissionFieldIndex.WhenOwnerCondition), True), System.Boolean)
            End Get
            Set
                SetValue(CInt(PermissionFieldIndex.WhenOwnerCondition), value)
            End Set
        End Property
        Public Overridable Property [CreationDate]() As System.DateTime
            Get
                Return CType(GetValue(CInt(PermissionFieldIndex.CreationDate), True), System.DateTime)
            End Get
            Set
                SetValue(CInt(PermissionFieldIndex.CreationDate), value)
            End Set
        End Property
        Public Overridable Property [CreatedBy]() As System.Int32
            Get
                Return CType(GetValue(CInt(PermissionFieldIndex.CreatedBy), True), System.Int32)
            End Get
            Set
                SetValue(CInt(PermissionFieldIndex.CreatedBy), value)
            End Set
        End Property
        Public Overridable Property [ModifiedDate]() As System.DateTime
            Get
                Return CType(GetValue(CInt(PermissionFieldIndex.ModifiedDate), True), System.DateTime)
            End Get
            Set
                SetValue(CInt(PermissionFieldIndex.ModifiedDate), value)
            End Set
        End Property
        Public Overridable Property [ModifiedBy]() As System.Int32
            Get
                Return CType(GetValue(CInt(PermissionFieldIndex.ModifiedBy), True), System.Int32)
            End Get
            Set
                SetValue(CInt(PermissionFieldIndex.ModifiedBy), value)
            End Set
        End Property

        <TypeContainedAttribute(GetType(RolePermissionEntity))> _
        Public Overridable ReadOnly Property [RolePermissionCollection]() As EntityCollection(Of RolePermissionEntity)
            Get
                If _rolePermissionCollection Is Nothing Then
                    _rolePermissionCollection = New EntityCollection(Of RolePermissionEntity)(EntityFactoryCache2.GetEntityFactory(GetType(RolePermissionEntityFactory)))
                    _rolePermissionCollection.ActiveContext = Me.ActiveContext
                    _rolePermissionCollection.SetContainingEntityInfo(Me, "Permission")
                End If
                Return _rolePermissionCollection
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

        <TypeContainedAttribute(GetType(RoleEntity))> _
        Public Overridable ReadOnly Property [RoleCollectionViaRolePermission]() As EntityCollection(Of RoleEntity)
            Get
                If _roleCollectionViaRolePermission Is Nothing Then
                    _roleCollectionViaRolePermission = New EntityCollection(Of RoleEntity)(EntityFactoryCache2.GetEntityFactory(GetType(RoleEntityFactory)))
                    _roleCollectionViaRolePermission.ActiveContext = Me.ActiveContext
                    _roleCollectionViaRolePermission.IsReadOnly = True
                    CType(_roleCollectionViaRolePermission, IEntityCollectionCore).IsForMN = True
                End If
                Return _roleCollectionViaRolePermission
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
                Return CInt(Questify.Builder.Model.ContentModel.EntityType.PermissionEntity)
            End Get
        End Property






    End Class
End Namespace
