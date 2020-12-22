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
    Public Class TreeStructurePartCustomBankPropertyEntity
        Inherits CommonEntityBase



        Private WithEvents _childTreeStructurePartCustomBankPropertyCollection As EntityCollection(Of ChildTreeStructurePartCustomBankPropertyEntity)
        Private WithEvents _treeStructureCustomBankPropertySelectedPartCollection As EntityCollection(Of TreeStructureCustomBankPropertySelectedPartEntity)
        Private WithEvents _treeStructureCustomBankPropertyValueCollectionViaTreeStructureCustomBankPropertySelectedPart As EntityCollection(Of TreeStructureCustomBankPropertyValueEntity)
        Private WithEvents _treeStructureCustomBankProperty As TreeStructureCustomBankPropertyEntity



        Private Shared _customProperties As Dictionary(Of String, String)
        Private Shared _fieldsCustomProperties As Dictionary(Of String, Dictionary(Of String, String))

        Public NotInheritable Class MemberNames
            Private Sub New()
            End Sub
            Public Shared ReadOnly [TreeStructureCustomBankProperty] As String = "TreeStructureCustomBankProperty"
            Public Shared ReadOnly [ChildTreeStructurePartCustomBankPropertyCollection] As String = "ChildTreeStructurePartCustomBankPropertyCollection"
            Public Shared ReadOnly [TreeStructureCustomBankPropertySelectedPartCollection] As String = "TreeStructureCustomBankPropertySelectedPartCollection"
            Public Shared ReadOnly [TreeStructureCustomBankPropertyValueCollectionViaTreeStructureCustomBankPropertySelectedPart] As String = "TreeStructureCustomBankPropertyValueCollectionViaTreeStructureCustomBankPropertySelectedPart"
        End Class

        Shared Sub New()
            SetupCustomPropertyHashtables()
        End Sub

        Public Sub New()
            MyBase.New("TreeStructurePartCustomBankPropertyEntity")
            InitClassEmpty(Nothing, Nothing)
        End Sub

        Public Sub New(fields As IEntityFields2)
            MyBase.New("TreeStructurePartCustomBankPropertyEntity")
            InitClassEmpty(Nothing, fields)
        End Sub

        Public Sub New(validator As IValidator)
            MyBase.New("TreeStructurePartCustomBankPropertyEntity")
            InitClassEmpty(validator, Nothing)
        End Sub

        Public Sub New(treeStructurePartCustomBankPropertyId As System.Guid)
            MyBase.New("TreeStructurePartCustomBankPropertyEntity")
            InitClassEmpty(Nothing, Nothing)
            Me.TreeStructurePartCustomBankPropertyId = treeStructurePartCustomBankPropertyId
        End Sub

        Public Sub New(treeStructurePartCustomBankPropertyId As System.Guid, validator As IValidator)
            MyBase.New("TreeStructurePartCustomBankPropertyEntity")
            InitClassEmpty(validator, Nothing)
            Me.TreeStructurePartCustomBankPropertyId = treeStructurePartCustomBankPropertyId
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Sub New(info As SerializationInfo, context As StreamingContext)
            MyBase.New(info, context)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                _childTreeStructurePartCustomBankPropertyCollection = CType(info.GetValue("_childTreeStructurePartCustomBankPropertyCollection", GetType(EntityCollection(Of ChildTreeStructurePartCustomBankPropertyEntity))), EntityCollection(Of ChildTreeStructurePartCustomBankPropertyEntity))
                _treeStructureCustomBankPropertySelectedPartCollection = CType(info.GetValue("_treeStructureCustomBankPropertySelectedPartCollection", GetType(EntityCollection(Of TreeStructureCustomBankPropertySelectedPartEntity))), EntityCollection(Of TreeStructureCustomBankPropertySelectedPartEntity))
                _treeStructureCustomBankPropertyValueCollectionViaTreeStructureCustomBankPropertySelectedPart = CType(info.GetValue("_treeStructureCustomBankPropertyValueCollectionViaTreeStructureCustomBankPropertySelectedPart", GetType(EntityCollection(Of TreeStructureCustomBankPropertyValueEntity))), EntityCollection(Of TreeStructureCustomBankPropertyValueEntity))
                _treeStructureCustomBankProperty = CType(info.GetValue("_treeStructureCustomBankProperty", GetType(TreeStructureCustomBankPropertyEntity)), TreeStructureCustomBankPropertyEntity)
                If Not _treeStructureCustomBankProperty Is Nothing Then
                    AddHandler _treeStructureCustomBankProperty.AfterSave, AddressOf OnEntityAfterSave
                End If
                Me.FixupDeserialization(FieldInfoProviderSingleton.GetInstance())
            End If

        End Sub


        Protected Overrides Sub PerformDesyncSetupFKFieldChange(fieldIndex As Integer)
            Select Case CType(fieldIndex, TreeStructurePartCustomBankPropertyFieldIndex)

                Case TreeStructurePartCustomBankPropertyFieldIndex.CustomBankPropertyId
                    DesetupSyncTreeStructureCustomBankProperty(True, False)



                Case Else
                    MyBase.PerformDesyncSetupFKFieldChange(fieldIndex)
            End Select
        End Sub


        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub SetRelatedEntityProperty(propertyName As String, entity As IEntityCore)
            Select Case propertyName
                Case "TreeStructureCustomBankProperty"
                    Me.TreeStructureCustomBankProperty = CType(entity, TreeStructureCustomBankPropertyEntity)
                Case "ChildTreeStructurePartCustomBankPropertyCollection"
                    Me.ChildTreeStructurePartCustomBankPropertyCollection.Add(CType(entity, ChildTreeStructurePartCustomBankPropertyEntity))
                Case "TreeStructureCustomBankPropertySelectedPartCollection"
                    Me.TreeStructureCustomBankPropertySelectedPartCollection.Add(CType(entity, TreeStructureCustomBankPropertySelectedPartEntity))
                Case "TreeStructureCustomBankPropertyValueCollectionViaTreeStructureCustomBankPropertySelectedPart"
                    Me.TreeStructureCustomBankPropertyValueCollectionViaTreeStructureCustomBankPropertySelectedPart.IsReadOnly = False
                    Me.TreeStructureCustomBankPropertyValueCollectionViaTreeStructureCustomBankPropertySelectedPart.Add(CType(entity, TreeStructureCustomBankPropertyValueEntity))
                    Me.TreeStructureCustomBankPropertyValueCollectionViaTreeStructureCustomBankPropertySelectedPart.IsReadOnly = True

                Case Else
                    Me.OnSetRelatedEntityProperty(propertyName, entity)
            End Select
        End Sub

        Protected Overrides Function GetRelationsForFieldOfType(fieldName As String) As RelationCollection
            Return TreeStructurePartCustomBankPropertyEntity.GetRelationsForField(fieldName)
        End Function

        Friend Shared Function GetRelationsForField(fieldName As String) As RelationCollection
            Dim toReturn As New RelationCollection()
            Select Case fieldName
                Case "TreeStructureCustomBankProperty"
                    toReturn.Add(TreeStructurePartCustomBankPropertyEntity.Relations.TreeStructureCustomBankPropertyEntityUsingCustomBankPropertyId)
                Case "ChildTreeStructurePartCustomBankPropertyCollection"
                    toReturn.Add(TreeStructurePartCustomBankPropertyEntity.Relations.ChildTreeStructurePartCustomBankPropertyEntityUsingTreeStructurePartCustomBankPropertyId)
                Case "TreeStructureCustomBankPropertySelectedPartCollection"
                    toReturn.Add(TreeStructurePartCustomBankPropertyEntity.Relations.TreeStructureCustomBankPropertySelectedPartEntityUsingTreeStructurePartId)
                Case "TreeStructureCustomBankPropertyValueCollectionViaTreeStructureCustomBankPropertySelectedPart"
                    toReturn.Add(TreeStructurePartCustomBankPropertyEntity.Relations.TreeStructureCustomBankPropertySelectedPartEntityUsingTreeStructurePartId, "TreeStructurePartCustomBankPropertyEntity__", "TreeStructureCustomBankPropertySelectedPart_", JoinHint.None)
                    toReturn.Add(TreeStructureCustomBankPropertySelectedPartEntity.Relations.TreeStructureCustomBankPropertyValueEntityUsingCustomBankPropertyIdResourceId, "TreeStructureCustomBankPropertySelectedPart_", String.Empty, JoinHint.None)
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
                Case "TreeStructureCustomBankProperty"
                    SetupSyncTreeStructureCustomBankProperty(relatedEntity)
                Case "ChildTreeStructurePartCustomBankPropertyCollection"
                    Me.ChildTreeStructurePartCustomBankPropertyCollection.Add(CType(relatedEntity, ChildTreeStructurePartCustomBankPropertyEntity))
                Case "TreeStructureCustomBankPropertySelectedPartCollection"
                    Me.TreeStructureCustomBankPropertySelectedPartCollection.Add(CType(relatedEntity, TreeStructureCustomBankPropertySelectedPartEntity))

                Case Else
            End Select
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub UnsetRelatedEntity(relatedEntity As IEntityCore, fieldName As String, signalRelatedEntityManyToOne As Boolean)
            Select Case fieldName
                Case "TreeStructureCustomBankProperty"
                    DesetupSyncTreeStructureCustomBankProperty(False, True)
                Case "ChildTreeStructurePartCustomBankPropertyCollection"
                    Me.PerformRelatedEntityRemoval(Me.ChildTreeStructurePartCustomBankPropertyCollection, relatedEntity, signalRelatedEntityManyToOne)
                Case "TreeStructureCustomBankPropertySelectedPartCollection"
                    Me.PerformRelatedEntityRemoval(Me.TreeStructureCustomBankPropertySelectedPartCollection, relatedEntity, signalRelatedEntityManyToOne)
                Case Else
            End Select
        End Sub

        Protected Overrides Function GetDependingRelatedEntities() As List(Of IEntity2)
            Dim toReturn As New List(Of IEntity2)()
            Return toReturn
        End Function

        Protected Overrides Function GetDependentRelatedEntities() As List(Of IEntity2)
            Dim toReturn As New List(Of IEntity2)()
            If Not _treeStructureCustomBankProperty Is Nothing Then
                toReturn.Add(_treeStructureCustomBankProperty)
            End If
            Return toReturn
        End Function

        Protected Overrides Function GetMemberEntityCollections() As List(Of IEntityCollection2)
            Dim toReturn As New List(Of IEntityCollection2)()
            toReturn.Add(Me.ChildTreeStructurePartCustomBankPropertyCollection)
            toReturn.Add(Me.TreeStructureCustomBankPropertySelectedPartCollection)
            Return toReturn
        End Function


        Protected Overrides Sub GetObjectData(info As SerializationInfo, context As StreamingContext)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                Dim value As IEntityCollection2 = Nothing
                Dim entityValue As IEntity2 = Nothing
                value = Nothing
                If (Not (_childTreeStructurePartCustomBankPropertyCollection Is Nothing)) AndAlso (_childTreeStructurePartCustomBankPropertyCollection.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _childTreeStructurePartCustomBankPropertyCollection
                End If
                info.AddValue("_childTreeStructurePartCustomBankPropertyCollection", value)
                value = Nothing
                If (Not (_treeStructureCustomBankPropertySelectedPartCollection Is Nothing)) AndAlso (_treeStructureCustomBankPropertySelectedPartCollection.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _treeStructureCustomBankPropertySelectedPartCollection
                End If
                info.AddValue("_treeStructureCustomBankPropertySelectedPartCollection", value)
                value = Nothing
                If (Not (_treeStructureCustomBankPropertyValueCollectionViaTreeStructureCustomBankPropertySelectedPart Is Nothing)) AndAlso (_treeStructureCustomBankPropertyValueCollectionViaTreeStructureCustomBankPropertySelectedPart.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _treeStructureCustomBankPropertyValueCollectionViaTreeStructureCustomBankPropertySelectedPart
                End If
                info.AddValue("_treeStructureCustomBankPropertyValueCollectionViaTreeStructureCustomBankPropertySelectedPart", value)
                entityValue = Nothing
                If Not Me.MarkedForDeletion Then
                    entityValue = _treeStructureCustomBankProperty
                End If
                info.AddValue("_treeStructureCustomBankProperty", entityValue)
            End If

            MyBase.GetObjectData(info, context)
        End Sub


        Protected Overrides Overloads Function GetAllRelations() As List(Of IEntityRelation)
            Return New TreeStructurePartCustomBankPropertyRelations().GetAllRelations()
        End Function

        Public Overridable Function GetRelationInfoChildTreeStructurePartCustomBankPropertyCollection() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(ChildTreeStructurePartCustomBankPropertyFields.TreeStructurePartCustomBankPropertyId, Nothing, ComparisonOperator.Equal, Me.TreeStructurePartCustomBankPropertyId))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoTreeStructureCustomBankPropertySelectedPartCollection() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(TreeStructureCustomBankPropertySelectedPartFields.TreeStructurePartId, Nothing, ComparisonOperator.Equal, Me.TreeStructurePartCustomBankPropertyId))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoTreeStructureCustomBankPropertyValueCollectionViaTreeStructureCustomBankPropertySelectedPart() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.Relations.AddRange(GetRelationsForFieldOfType("TreeStructureCustomBankPropertyValueCollectionViaTreeStructureCustomBankPropertySelectedPart"))
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(TreeStructurePartCustomBankPropertyFields.TreeStructurePartCustomBankPropertyId, Nothing, ComparisonOperator.Equal, Me.TreeStructurePartCustomBankPropertyId, "TreeStructurePartCustomBankPropertyEntity__"))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoTreeStructureCustomBankProperty() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(TreeStructureCustomBankPropertyFields.CustomBankPropertyId, Nothing, ComparisonOperator.Equal, Me.CustomBankPropertyId))
            Return bucket
        End Function

        Protected Overrides Function CreateEntityFactory() As IEntityFactory2
            Return EntityFactoryCache2.GetEntityFactory(GetType(TreeStructurePartCustomBankPropertyEntityFactory))
        End Function
#If Not CF Then
        Protected Overrides Sub AddToMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2))
            MyBase.AddToMemberEntityCollectionsQueue(collectionsQueue)
            collectionsQueue.Enqueue(_childTreeStructurePartCustomBankPropertyCollection)
            collectionsQueue.Enqueue(_treeStructureCustomBankPropertySelectedPartCollection)
            collectionsQueue.Enqueue(_treeStructureCustomBankPropertyValueCollectionViaTreeStructureCustomBankPropertySelectedPart)
        End Sub

        Protected Overrides Sub GetFromMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2))
            MyBase.GetFromMemberEntityCollectionsQueue(collectionsQueue)
            _childTreeStructurePartCustomBankPropertyCollection = CType(collectionsQueue.Dequeue(), EntityCollection(Of ChildTreeStructurePartCustomBankPropertyEntity))
            _treeStructureCustomBankPropertySelectedPartCollection = CType(collectionsQueue.Dequeue(), EntityCollection(Of TreeStructureCustomBankPropertySelectedPartEntity))
            _treeStructureCustomBankPropertyValueCollectionViaTreeStructureCustomBankPropertySelectedPart = CType(collectionsQueue.Dequeue(), EntityCollection(Of TreeStructureCustomBankPropertyValueEntity))
        End Sub

        Protected Overrides Function HasPopulatedMemberEntityCollections() As Boolean
            If (Not _childTreeStructurePartCustomBankPropertyCollection Is Nothing) Then
                Return True
            End If
            If (Not _treeStructureCustomBankPropertySelectedPartCollection Is Nothing) Then
                Return True
            End If
            If (Not _treeStructureCustomBankPropertyValueCollectionViaTreeStructureCustomBankPropertySelectedPart Is Nothing) Then
                Return True
            End If
            Return MyBase.HasPopulatedMemberEntityCollections()
        End Function

        Protected Overrides Overloads Sub CreateMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2), requiredQueue As Queue(Of Boolean))
            MyBase.CreateMemberEntityCollectionsQueue(collectionsQueue, requiredQueue)
            Dim toAdd As IEntityCollection2 = Nothing
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of ChildTreeStructurePartCustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ChildTreeStructurePartCustomBankPropertyEntityFactory)))
            Else
                toAdd = Nothing
            End If
            collectionsQueue.Enqueue(toAdd)
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of TreeStructureCustomBankPropertySelectedPartEntity)(EntityFactoryCache2.GetEntityFactory(GetType(TreeStructureCustomBankPropertySelectedPartEntityFactory)))
            Else
                toAdd = Nothing
            End If
            collectionsQueue.Enqueue(toAdd)
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of TreeStructureCustomBankPropertyValueEntity)(EntityFactoryCache2.GetEntityFactory(GetType(TreeStructureCustomBankPropertyValueEntityFactory)))
            Else
                toAdd = Nothing
            End If
            collectionsQueue.Enqueue(toAdd)
        End Sub
#End If
        Protected Overrides Overloads Function GetRelatedData() As Dictionary(Of String, Object)
            Dim toReturn As New Dictionary(Of String, Object)()
            toReturn.Add("TreeStructureCustomBankProperty", _treeStructureCustomBankProperty)
            toReturn.Add("ChildTreeStructurePartCustomBankPropertyCollection", _childTreeStructurePartCustomBankPropertyCollection)
            toReturn.Add("TreeStructureCustomBankPropertySelectedPartCollection", _treeStructureCustomBankPropertySelectedPartCollection)
            toReturn.Add("TreeStructureCustomBankPropertyValueCollectionViaTreeStructureCustomBankPropertySelectedPart", _treeStructureCustomBankPropertyValueCollectionViaTreeStructureCustomBankPropertySelectedPart)
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
            _fieldsCustomProperties.Add("TreeStructurePartCustomBankPropertyId", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("CustomBankPropertyId", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("Name", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("Title", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("Code", fieldHashtable)
        End Sub


        Private Sub DesetupSyncTreeStructureCustomBankProperty(signalRelatedEntity As Boolean, resetFKFields As Boolean)
            Me.PerformDesetupSyncRelatedEntity(_treeStructureCustomBankProperty, AddressOf OnTreeStructureCustomBankPropertyPropertyChanged, "TreeStructureCustomBankProperty", Questify.Builder.Model.ContentModel.RelationClasses.StaticTreeStructurePartCustomBankPropertyRelations.TreeStructureCustomBankPropertyEntityUsingCustomBankPropertyIdStatic, True, signalRelatedEntity, "TreeStructurePartCustomBankPropertyCollection", resetFKFields, New Integer() {CInt(TreeStructurePartCustomBankPropertyFieldIndex.CustomBankPropertyId)})
            _treeStructureCustomBankProperty = Nothing
        End Sub

        Private Sub SetupSyncTreeStructureCustomBankProperty(relatedEntity As IEntityCore)
            If Not _treeStructureCustomBankProperty Is relatedEntity Then
                DesetupSyncTreeStructureCustomBankProperty(True, True)
                _treeStructureCustomBankProperty = CType(relatedEntity, TreeStructureCustomBankPropertyEntity)
                Me.PerformSetupSyncRelatedEntity(_treeStructureCustomBankProperty, AddressOf OnTreeStructureCustomBankPropertyPropertyChanged, "TreeStructureCustomBankProperty", Questify.Builder.Model.ContentModel.RelationClasses.StaticTreeStructurePartCustomBankPropertyRelations.TreeStructureCustomBankPropertyEntityUsingCustomBankPropertyIdStatic, True, New String() {})
            End If
        End Sub

        Private Sub OnTreeStructureCustomBankPropertyPropertyChanged(sender As Object, e As PropertyChangedEventArgs)
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

        Public Shared ReadOnly Property Relations() As TreeStructurePartCustomBankPropertyRelations
            Get
                Return New TreeStructurePartCustomBankPropertyRelations()
            End Get
        End Property

        Public Shared ReadOnly Property CustomProperties() As Dictionary(Of String, String)
            Get
                Return _customProperties
            End Get
        End Property


        Public Shared ReadOnly Property PrefetchPathChildTreeStructurePartCustomBankPropertyCollection() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(Of ChildTreeStructurePartCustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ChildTreeStructurePartCustomBankPropertyEntityFactory))), _
                    CType(GetRelationsForField("ChildTreeStructurePartCustomBankPropertyCollection")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.TreeStructurePartCustomBankPropertyEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.ChildTreeStructurePartCustomBankPropertyEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "ChildTreeStructurePartCustomBankPropertyCollection", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathTreeStructureCustomBankPropertySelectedPartCollection() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(Of TreeStructureCustomBankPropertySelectedPartEntity)(EntityFactoryCache2.GetEntityFactory(GetType(TreeStructureCustomBankPropertySelectedPartEntityFactory))), _
                    CType(GetRelationsForField("TreeStructureCustomBankPropertySelectedPartCollection")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.TreeStructurePartCustomBankPropertyEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.TreeStructureCustomBankPropertySelectedPartEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "TreeStructureCustomBankPropertySelectedPartCollection", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathTreeStructureCustomBankPropertyValueCollectionViaTreeStructureCustomBankPropertySelectedPart() As IPrefetchPathElement2
            Get
                Dim intermediateRelation As IEntityRelation = TreeStructurePartCustomBankPropertyEntity.Relations.TreeStructureCustomBankPropertySelectedPartEntityUsingTreeStructurePartId
                intermediateRelation.SetAliases(String.Empty, "TreeStructureCustomBankPropertySelectedPart_")
                Return New PrefetchPathElement2(New EntityCollection(Of TreeStructureCustomBankPropertyValueEntity)(EntityFactoryCache2.GetEntityFactory(GetType(TreeStructureCustomBankPropertyValueEntityFactory))), _
                    intermediateRelation, CType(Questify.Builder.Model.ContentModel.EntityType.TreeStructurePartCustomBankPropertyEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.TreeStructureCustomBankPropertyValueEntity, Integer), 0, Nothing, Nothing, GetRelationsForField("TreeStructureCustomBankPropertyValueCollectionViaTreeStructureCustomBankPropertySelectedPart"), Nothing, "TreeStructureCustomBankPropertyValueCollectionViaTreeStructureCustomBankPropertySelectedPart", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathTreeStructureCustomBankProperty() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(EntityFactoryCache2.GetEntityFactory(GetType(TreeStructureCustomBankPropertyEntityFactory))), _
                    CType(GetRelationsForField("TreeStructureCustomBankProperty")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.TreeStructurePartCustomBankPropertyEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.TreeStructureCustomBankPropertyEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "TreeStructureCustomBankProperty", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne)
            End Get
        End Property


        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property CustomPropertiesOfType() As Dictionary(Of String, String)
            Get
                Return TreeStructurePartCustomBankPropertyEntity.CustomProperties
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
                Return TreeStructurePartCustomBankPropertyEntity.FieldsCustomProperties
            End Get
        End Property

        Public Overridable Property [TreeStructurePartCustomBankPropertyId]() As System.Guid
            Get
                Return CType(GetValue(CInt(TreeStructurePartCustomBankPropertyFieldIndex.TreeStructurePartCustomBankPropertyId), True), System.Guid)
            End Get
            Set
                SetValue(CInt(TreeStructurePartCustomBankPropertyFieldIndex.TreeStructurePartCustomBankPropertyId), value)
            End Set
        End Property
        Public Overridable Property [CustomBankPropertyId]() As System.Guid
            Get
                Return CType(GetValue(CInt(TreeStructurePartCustomBankPropertyFieldIndex.CustomBankPropertyId), True), System.Guid)
            End Get
            Set
                SetValue(CInt(TreeStructurePartCustomBankPropertyFieldIndex.CustomBankPropertyId), value)
            End Set
        End Property
        Public Overridable Property [Name]() As System.String
            Get
                Return CType(GetValue(CInt(TreeStructurePartCustomBankPropertyFieldIndex.Name), True), System.String)
            End Get
            Set
                SetValue(CInt(TreeStructurePartCustomBankPropertyFieldIndex.Name), value)
            End Set
        End Property
        Public Overridable Property [Title]() As System.String
            Get
                Return CType(GetValue(CInt(TreeStructurePartCustomBankPropertyFieldIndex.Title), True), System.String)
            End Get
            Set
                SetValue(CInt(TreeStructurePartCustomBankPropertyFieldIndex.Title), value)
            End Set
        End Property
        Public Overridable Property [Code]() As System.Guid
            Get
                Return CType(GetValue(CInt(TreeStructurePartCustomBankPropertyFieldIndex.Code), True), System.Guid)
            End Get
            Set
                SetValue(CInt(TreeStructurePartCustomBankPropertyFieldIndex.Code), value)
            End Set
        End Property

        <TypeContainedAttribute(GetType(ChildTreeStructurePartCustomBankPropertyEntity))> _
        Public Overridable ReadOnly Property [ChildTreeStructurePartCustomBankPropertyCollection]() As EntityCollection(Of ChildTreeStructurePartCustomBankPropertyEntity)
            Get
                If _childTreeStructurePartCustomBankPropertyCollection Is Nothing Then
                    _childTreeStructurePartCustomBankPropertyCollection = New EntityCollection(Of ChildTreeStructurePartCustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ChildTreeStructurePartCustomBankPropertyEntityFactory)))
                    _childTreeStructurePartCustomBankPropertyCollection.ActiveContext = Me.ActiveContext
                    _childTreeStructurePartCustomBankPropertyCollection.SetContainingEntityInfo(Me, "TreeStructurePartCustomBankProperty")
                End If
                Return _childTreeStructurePartCustomBankPropertyCollection
            End Get
        End Property

        <TypeContainedAttribute(GetType(TreeStructureCustomBankPropertySelectedPartEntity))> _
        Public Overridable ReadOnly Property [TreeStructureCustomBankPropertySelectedPartCollection]() As EntityCollection(Of TreeStructureCustomBankPropertySelectedPartEntity)
            Get
                If _treeStructureCustomBankPropertySelectedPartCollection Is Nothing Then
                    _treeStructureCustomBankPropertySelectedPartCollection = New EntityCollection(Of TreeStructureCustomBankPropertySelectedPartEntity)(EntityFactoryCache2.GetEntityFactory(GetType(TreeStructureCustomBankPropertySelectedPartEntityFactory)))
                    _treeStructureCustomBankPropertySelectedPartCollection.ActiveContext = Me.ActiveContext
                    _treeStructureCustomBankPropertySelectedPartCollection.SetContainingEntityInfo(Me, "TreeStructurePartCustomBankProperty")
                End If
                Return _treeStructureCustomBankPropertySelectedPartCollection
            End Get
        End Property

        <TypeContainedAttribute(GetType(TreeStructureCustomBankPropertyValueEntity))> _
        Public Overridable ReadOnly Property [TreeStructureCustomBankPropertyValueCollectionViaTreeStructureCustomBankPropertySelectedPart]() As EntityCollection(Of TreeStructureCustomBankPropertyValueEntity)
            Get
                If _treeStructureCustomBankPropertyValueCollectionViaTreeStructureCustomBankPropertySelectedPart Is Nothing Then
                    _treeStructureCustomBankPropertyValueCollectionViaTreeStructureCustomBankPropertySelectedPart = New EntityCollection(Of TreeStructureCustomBankPropertyValueEntity)(EntityFactoryCache2.GetEntityFactory(GetType(TreeStructureCustomBankPropertyValueEntityFactory)))
                    _treeStructureCustomBankPropertyValueCollectionViaTreeStructureCustomBankPropertySelectedPart.ActiveContext = Me.ActiveContext
                    _treeStructureCustomBankPropertyValueCollectionViaTreeStructureCustomBankPropertySelectedPart.IsReadOnly = True
                    CType(_treeStructureCustomBankPropertyValueCollectionViaTreeStructureCustomBankPropertySelectedPart, IEntityCollectionCore).IsForMN = True
                End If
                Return _treeStructureCustomBankPropertyValueCollectionViaTreeStructureCustomBankPropertySelectedPart
            End Get
        End Property

        <Browsable(True)> _
        Public Overridable Property [TreeStructureCustomBankProperty]() As TreeStructureCustomBankPropertyEntity
            Get
                Return _treeStructureCustomBankProperty
            End Get
            Set
                If MyBase.IsDeserializing Then
                    SetupSyncTreeStructureCustomBankProperty(value)
                Else
                    SetSingleRelatedEntityNavigator(value, "TreeStructurePartCustomBankPropertyCollection", "TreeStructureCustomBankProperty", _treeStructureCustomBankProperty, True)
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
                Return CInt(Questify.Builder.Model.ContentModel.EntityType.TreeStructurePartCustomBankPropertyEntity)
            End Get
        End Property






    End Class
End Namespace
