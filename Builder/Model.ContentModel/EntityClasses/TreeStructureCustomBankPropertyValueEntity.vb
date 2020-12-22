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
    Public Class TreeStructureCustomBankPropertyValueEntity
        Inherits CustomBankPropertyValueEntity



        Private WithEvents _treeStructureCustomBankPropertySelectedPartCollection As EntityCollection(Of TreeStructureCustomBankPropertySelectedPartEntity)
        Private WithEvents _treeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart As EntityCollection(Of TreeStructurePartCustomBankPropertyEntity)
        Private WithEvents _treeStructureCustomBankProperty As TreeStructureCustomBankPropertyEntity



        Private Shared _customProperties As Dictionary(Of String, String)
        Private Shared _fieldsCustomProperties As Dictionary(Of String, Dictionary(Of String, String))

        Public Shadows NotInheritable Class MemberNames
            Private Sub New()
            End Sub
            Public Shared ReadOnly [CustomBankProperty] As String = "CustomBankProperty"
            Public Shared ReadOnly [Resource] As String = "Resource"
            Public Shared ReadOnly [TreeStructureCustomBankProperty] As String = "TreeStructureCustomBankProperty"
            Public Shared ReadOnly [TreeStructureCustomBankPropertySelectedPartCollection] As String = "TreeStructureCustomBankPropertySelectedPartCollection"
            Public Shared ReadOnly [TreeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart] As String = "TreeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart"
        End Class

        Shared Sub New()
            SetupCustomPropertyHashtables()
        End Sub

        Public Sub New()
            MyBase.New()
            InitClassEmpty()
            SetName("TreeStructureCustomBankPropertyValueEntity")
        End Sub

        Public Sub New(fields As IEntityFields2)
            MyBase.New(fields)
            InitClassEmpty()
            SetName("TreeStructureCustomBankPropertyValueEntity")
        End Sub

        Public Sub New(validator As IValidator)
            MyBase.New(validator)
            InitClassEmpty()
            SetName("TreeStructureCustomBankPropertyValueEntity")
        End Sub

        Public Sub New(resourceId As System.Guid, customBankPropertyId As System.Guid)
            MyBase.New(resourceId, customBankPropertyId)
            InitClassEmpty()

            SetName("TreeStructureCustomBankPropertyValueEntity")
        End Sub

        Public Sub New(resourceId As System.Guid, customBankPropertyId As System.Guid, validator As IValidator)
            MyBase.New(resourceId, customBankPropertyId, validator)
            InitClassEmpty()

            SetName("TreeStructureCustomBankPropertyValueEntity")
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Sub New(info As SerializationInfo, context As StreamingContext)
            MyBase.New(info, context)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                _treeStructureCustomBankPropertySelectedPartCollection = CType(info.GetValue("_treeStructureCustomBankPropertySelectedPartCollection", GetType(EntityCollection(Of TreeStructureCustomBankPropertySelectedPartEntity))), EntityCollection(Of TreeStructureCustomBankPropertySelectedPartEntity))
                _treeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart = CType(info.GetValue("_treeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart", GetType(EntityCollection(Of TreeStructurePartCustomBankPropertyEntity))), EntityCollection(Of TreeStructurePartCustomBankPropertyEntity))
                _treeStructureCustomBankProperty = CType(info.GetValue("_treeStructureCustomBankProperty", GetType(TreeStructureCustomBankPropertyEntity)), TreeStructureCustomBankPropertyEntity)
                If Not _treeStructureCustomBankProperty Is Nothing Then
                    AddHandler _treeStructureCustomBankProperty.AfterSave, AddressOf OnEntityAfterSave
                End If
                Me.FixupDeserialization(FieldInfoProviderSingleton.GetInstance())
            End If

        End Sub



        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub SetRelatedEntityProperty(propertyName As String, entity As IEntityCore)
            Select Case propertyName
                Case "TreeStructureCustomBankProperty"
                    Me.TreeStructureCustomBankProperty = CType(entity, TreeStructureCustomBankPropertyEntity)
                Case "TreeStructureCustomBankPropertySelectedPartCollection"
                    Me.TreeStructureCustomBankPropertySelectedPartCollection.Add(CType(entity, TreeStructureCustomBankPropertySelectedPartEntity))
                Case "TreeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart"
                    Me.TreeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart.IsReadOnly = False
                    Me.TreeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart.Add(CType(entity, TreeStructurePartCustomBankPropertyEntity))
                    Me.TreeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart.IsReadOnly = True

                Case Else
                    MyBase.SetRelatedEntityProperty(propertyName, entity)
            End Select
        End Sub

        Protected Overrides Function GetRelationsForFieldOfType(fieldName As String) As RelationCollection
            Return TreeStructureCustomBankPropertyValueEntity.GetRelationsForField(fieldName)
        End Function

        Friend Shared Shadows Function GetRelationsForField(fieldName As String) As RelationCollection
            Dim toReturn As New RelationCollection()
            Select Case fieldName
                Case "TreeStructureCustomBankProperty"
                    toReturn.Add(TreeStructureCustomBankPropertyValueEntity.Relations.TreeStructureCustomBankPropertyEntityUsingCustomBankPropertyId)
                Case "TreeStructureCustomBankPropertySelectedPartCollection"
                    toReturn.Add(TreeStructureCustomBankPropertyValueEntity.Relations.TreeStructureCustomBankPropertySelectedPartEntityUsingCustomBankPropertyIdResourceId)
                Case "TreeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart"
                    toReturn.Add(TreeStructureCustomBankPropertyValueEntity.Relations.TreeStructureCustomBankPropertySelectedPartEntityUsingCustomBankPropertyIdResourceId, "TreeStructureCustomBankPropertyValueEntity__", "TreeStructureCustomBankPropertySelectedPart_", JoinHint.None)
                    toReturn.Add(TreeStructureCustomBankPropertySelectedPartEntity.Relations.TreeStructurePartCustomBankPropertyEntityUsingTreeStructurePartId, "TreeStructureCustomBankPropertySelectedPart_", String.Empty, JoinHint.None)
                Case Else
                    toReturn = CustomBankPropertyValueEntity.GetRelationsForField(fieldName)
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
                Case "TreeStructureCustomBankPropertySelectedPartCollection"
                    Me.TreeStructureCustomBankPropertySelectedPartCollection.Add(CType(relatedEntity, TreeStructureCustomBankPropertySelectedPartEntity))

                Case Else
                    MyBase.SetRelatedEntity(relatedEntity, fieldName)
            End Select
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub UnsetRelatedEntity(relatedEntity As IEntityCore, fieldName As String, signalRelatedEntityManyToOne As Boolean)
            Select Case fieldName
                Case "TreeStructureCustomBankProperty"
                    DesetupSyncTreeStructureCustomBankProperty(False, True)
                Case "TreeStructureCustomBankPropertySelectedPartCollection"
                    Me.PerformRelatedEntityRemoval(Me.TreeStructureCustomBankPropertySelectedPartCollection, relatedEntity, signalRelatedEntityManyToOne)
                Case Else
                    MyBase.UnsetRelatedEntity(relatedEntity, fieldName, signalRelatedEntityManyToOne)
            End Select
        End Sub

        Protected Overrides Function GetDependingRelatedEntities() As List(Of IEntity2)
            Dim toReturn As New List(Of IEntity2)()
            toReturn.AddRange(MyBase.GetDependingRelatedEntities())
            Return toReturn
        End Function

        Protected Overrides Function GetDependentRelatedEntities() As List(Of IEntity2)
            Dim toReturn As New List(Of IEntity2)()
            If Not _treeStructureCustomBankProperty Is Nothing Then
                toReturn.Add(_treeStructureCustomBankProperty)
            End If
            toReturn.AddRange(MyBase.GetDependentRelatedEntities())
            Return toReturn
        End Function

        Protected Overrides Function GetMemberEntityCollections() As List(Of IEntityCollection2)
            Dim toReturn As New List(Of IEntityCollection2)()
            toReturn.Add(Me.TreeStructureCustomBankPropertySelectedPartCollection)
            toReturn.AddRange(MyBase.GetMemberEntityCollections())
            Return toReturn
        End Function

        Public Shadows Shared Function GetEntityTypeFilter() As IPredicateExpression
            Return InheritanceInfoProviderSingleton.GetInstance().GetEntityTypeFilter("TreeStructureCustomBankPropertyValueEntity", False)
        End Function

        Public Shadows Shared Function GetEntityTypeFilter(negate As Boolean) As IPredicateExpression
            Return InheritanceInfoProviderSingleton.GetInstance().GetEntityTypeFilter("TreeStructureCustomBankPropertyValueEntity", negate)
        End Function

        Protected Overrides Sub GetObjectData(info As SerializationInfo, context As StreamingContext)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                Dim value As IEntityCollection2 = Nothing
                Dim entityValue As IEntity2 = Nothing
                value = Nothing
                If (Not (_treeStructureCustomBankPropertySelectedPartCollection Is Nothing)) AndAlso (_treeStructureCustomBankPropertySelectedPartCollection.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _treeStructureCustomBankPropertySelectedPartCollection
                End If
                info.AddValue("_treeStructureCustomBankPropertySelectedPartCollection", value)
                value = Nothing
                If (Not (_treeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart Is Nothing)) AndAlso (_treeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _treeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart
                End If
                info.AddValue("_treeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart", value)
                entityValue = Nothing
                If Not Me.MarkedForDeletion Then
                    entityValue = _treeStructureCustomBankProperty
                End If
                info.AddValue("_treeStructureCustomBankProperty", entityValue)
            End If

            MyBase.GetObjectData(info, context)
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Function CheckIfIsSubTypeOf(typeOfEntity As Integer) As Boolean
            Return InheritanceInfoProviderSingleton.GetInstance().CheckIfIsSubTypeOf("TreeStructureCustomBankPropertyValueEntity", CType(typeOfEntity, Questify.Builder.Model.ContentModel.EntityType).ToString())
        End Function

        Protected Overrides Overloads Function GetAllRelations() As List(Of IEntityRelation)
            Return New TreeStructureCustomBankPropertyValueRelations().GetAllRelations()
        End Function

        Public Overridable Function GetRelationInfoTreeStructureCustomBankPropertySelectedPartCollection() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(TreeStructureCustomBankPropertySelectedPartFields.CustomBankPropertyId, Nothing, ComparisonOperator.Equal, Me.CustomBankPropertyId))
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(TreeStructureCustomBankPropertySelectedPartFields.ResourceId, Nothing, ComparisonOperator.Equal, Me.ResourceId))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoTreeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.Relations.AddRange(GetRelationsForFieldOfType("TreeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart"))
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(TreeStructureCustomBankPropertyValueFields.ResourceId, Nothing, ComparisonOperator.Equal, Me.ResourceId, "TreeStructureCustomBankPropertyValueEntity__"))
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(TreeStructureCustomBankPropertyValueFields.CustomBankPropertyId, Nothing, ComparisonOperator.Equal, Me.CustomBankPropertyId, "TreeStructureCustomBankPropertyValueEntity__"))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoTreeStructureCustomBankProperty() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(TreeStructureCustomBankPropertyFields.CustomBankPropertyId, Nothing, ComparisonOperator.Equal, Me.CustomBankPropertyId))
            Return bucket
        End Function

        Protected Overrides Function CreateEntityFactory() As IEntityFactory2
            Return EntityFactoryCache2.GetEntityFactory(GetType(TreeStructureCustomBankPropertyValueEntityFactory))
        End Function
#If Not CF Then
        Protected Overrides Sub AddToMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2))
            MyBase.AddToMemberEntityCollectionsQueue(collectionsQueue)
            collectionsQueue.Enqueue(_treeStructureCustomBankPropertySelectedPartCollection)
            collectionsQueue.Enqueue(_treeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart)
        End Sub

        Protected Overrides Sub GetFromMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2))
            MyBase.GetFromMemberEntityCollectionsQueue(collectionsQueue)
            _treeStructureCustomBankPropertySelectedPartCollection = CType(collectionsQueue.Dequeue(), EntityCollection(Of TreeStructureCustomBankPropertySelectedPartEntity))
            _treeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart = CType(collectionsQueue.Dequeue(), EntityCollection(Of TreeStructurePartCustomBankPropertyEntity))
        End Sub

        Protected Overrides Function HasPopulatedMemberEntityCollections() As Boolean
            If (Not _treeStructureCustomBankPropertySelectedPartCollection Is Nothing) Then
                Return True
            End If
            If (Not _treeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart Is Nothing) Then
                Return True
            End If
            Return MyBase.HasPopulatedMemberEntityCollections()
        End Function

        Protected Overrides Overloads Sub CreateMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2), requiredQueue As Queue(Of Boolean))
            MyBase.CreateMemberEntityCollectionsQueue(collectionsQueue, requiredQueue)
            Dim toAdd As IEntityCollection2 = Nothing
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of TreeStructureCustomBankPropertySelectedPartEntity)(EntityFactoryCache2.GetEntityFactory(GetType(TreeStructureCustomBankPropertySelectedPartEntityFactory)))
            Else
                toAdd = Nothing
            End If
            collectionsQueue.Enqueue(toAdd)
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of TreeStructurePartCustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(TreeStructurePartCustomBankPropertyEntityFactory)))
            Else
                toAdd = Nothing
            End If
            collectionsQueue.Enqueue(toAdd)
        End Sub
#End If
        Protected Overrides Overloads Function GetRelatedData() As Dictionary(Of String, Object)
            Dim toReturn As Dictionary(Of String, Object) = MyBase.GetRelatedData()
            toReturn.Add("TreeStructureCustomBankProperty", _treeStructureCustomBankProperty)
            toReturn.Add("TreeStructureCustomBankPropertySelectedPartCollection", _treeStructureCustomBankPropertySelectedPartCollection)
            toReturn.Add("TreeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart", _treeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart)
            Return toReturn
        End Function

        Private Sub InitClassMembers()


        End Sub

        Private Shared Sub SetupCustomPropertyHashtables()
            _customProperties = New Dictionary(Of String, String)()
            _fieldsCustomProperties = New Dictionary(Of String, Dictionary(Of String, String))()
        End Sub


        Private Sub DesetupSyncTreeStructureCustomBankProperty(signalRelatedEntity As Boolean, resetFKFields As Boolean)
            Me.PerformDesetupSyncRelatedEntity(_treeStructureCustomBankProperty, AddressOf OnTreeStructureCustomBankPropertyPropertyChanged, "TreeStructureCustomBankProperty", Questify.Builder.Model.ContentModel.RelationClasses.StaticTreeStructureCustomBankPropertyValueRelations.TreeStructureCustomBankPropertyEntityUsingCustomBankPropertyIdStatic, True, signalRelatedEntity, "TreeStructureCustomBankPropertyValueCollection", resetFKFields, New Integer() {CInt(TreeStructureCustomBankPropertyValueFieldIndex.CustomBankPropertyId)})
            _treeStructureCustomBankProperty = Nothing
        End Sub

        Private Sub SetupSyncTreeStructureCustomBankProperty(relatedEntity As IEntityCore)
            If Not _treeStructureCustomBankProperty Is relatedEntity Then
                DesetupSyncTreeStructureCustomBankProperty(True, True)
                _treeStructureCustomBankProperty = CType(relatedEntity, TreeStructureCustomBankPropertyEntity)
                Me.PerformSetupSyncRelatedEntity(_treeStructureCustomBankProperty, AddressOf OnTreeStructureCustomBankPropertyPropertyChanged, "TreeStructureCustomBankProperty", Questify.Builder.Model.ContentModel.RelationClasses.StaticTreeStructureCustomBankPropertyValueRelations.TreeStructureCustomBankPropertyEntityUsingCustomBankPropertyIdStatic, True, New String() {})
            End If
        End Sub

        Private Sub OnTreeStructureCustomBankPropertyPropertyChanged(sender As Object, e As PropertyChangedEventArgs)
            Select Case e.PropertyName

                Case Else
            End Select
        End Sub



        Private Sub InitClassEmpty()
            InitClassMembers()




        End Sub

        Public Shadows Shared ReadOnly Property Relations() As TreeStructureCustomBankPropertyValueRelations
            Get
                Return New TreeStructureCustomBankPropertyValueRelations()
            End Get
        End Property

        Public Shadows Shared ReadOnly Property CustomProperties() As Dictionary(Of String, String)
            Get
                Return _customProperties
            End Get
        End Property


        Public Shared ReadOnly Property PrefetchPathTreeStructureCustomBankPropertySelectedPartCollection() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(Of TreeStructureCustomBankPropertySelectedPartEntity)(EntityFactoryCache2.GetEntityFactory(GetType(TreeStructureCustomBankPropertySelectedPartEntityFactory))), _
                    CType(GetRelationsForField("TreeStructureCustomBankPropertySelectedPartCollection")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.TreeStructureCustomBankPropertyValueEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.TreeStructureCustomBankPropertySelectedPartEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "TreeStructureCustomBankPropertySelectedPartCollection", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathTreeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart() As IPrefetchPathElement2
            Get
                Dim intermediateRelation As IEntityRelation = TreeStructureCustomBankPropertyValueEntity.Relations.TreeStructureCustomBankPropertySelectedPartEntityUsingCustomBankPropertyIdResourceId
                intermediateRelation.SetAliases(String.Empty, "TreeStructureCustomBankPropertySelectedPart_")
                Return New PrefetchPathElement2(New EntityCollection(Of TreeStructurePartCustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(TreeStructurePartCustomBankPropertyEntityFactory))), _
                    intermediateRelation, CType(Questify.Builder.Model.ContentModel.EntityType.TreeStructureCustomBankPropertyValueEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.TreeStructurePartCustomBankPropertyEntity, Integer), 0, Nothing, Nothing, GetRelationsForField("TreeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart"), Nothing, "TreeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathTreeStructureCustomBankProperty() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(EntityFactoryCache2.GetEntityFactory(GetType(TreeStructureCustomBankPropertyEntityFactory))), _
                    CType(GetRelationsForField("TreeStructureCustomBankProperty")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.TreeStructureCustomBankPropertyValueEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.TreeStructureCustomBankPropertyEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "TreeStructureCustomBankProperty", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne)
            End Get
        End Property


        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property CustomPropertiesOfType() As Dictionary(Of String, String)
            Get
                Return TreeStructureCustomBankPropertyValueEntity.CustomProperties
            End Get
        End Property

        Public Shadows Shared ReadOnly Property FieldsCustomProperties() As Dictionary(Of String, Dictionary(Of String, String))
            Get
                Return _fieldsCustomProperties
            End Get
        End Property

        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property FieldsCustomPropertiesOfType() As Dictionary(Of String, Dictionary(Of String, String))
            Get
                Return TreeStructureCustomBankPropertyValueEntity.FieldsCustomProperties
            End Get
        End Property


        <TypeContainedAttribute(GetType(TreeStructureCustomBankPropertySelectedPartEntity))> _
        Public Overridable ReadOnly Property [TreeStructureCustomBankPropertySelectedPartCollection]() As EntityCollection(Of TreeStructureCustomBankPropertySelectedPartEntity)
            Get
                If _treeStructureCustomBankPropertySelectedPartCollection Is Nothing Then
                    _treeStructureCustomBankPropertySelectedPartCollection = New EntityCollection(Of TreeStructureCustomBankPropertySelectedPartEntity)(EntityFactoryCache2.GetEntityFactory(GetType(TreeStructureCustomBankPropertySelectedPartEntityFactory)))
                    _treeStructureCustomBankPropertySelectedPartCollection.ActiveContext = Me.ActiveContext
                    _treeStructureCustomBankPropertySelectedPartCollection.SetContainingEntityInfo(Me, "TreeStructureCustomBankPropertyValue")
                End If
                Return _treeStructureCustomBankPropertySelectedPartCollection
            End Get
        End Property

        <TypeContainedAttribute(GetType(TreeStructurePartCustomBankPropertyEntity))> _
        Public Overridable ReadOnly Property [TreeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart]() As EntityCollection(Of TreeStructurePartCustomBankPropertyEntity)
            Get
                If _treeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart Is Nothing Then
                    _treeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart = New EntityCollection(Of TreeStructurePartCustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(TreeStructurePartCustomBankPropertyEntityFactory)))
                    _treeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart.ActiveContext = Me.ActiveContext
                    _treeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart.IsReadOnly = True
                    CType(_treeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart, IEntityCollectionCore).IsForMN = True
                End If
                Return _treeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart
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
                    SetSingleRelatedEntityNavigator(value, "TreeStructureCustomBankPropertyValueCollection", "TreeStructureCustomBankProperty", _treeStructureCustomBankProperty, True)
                End If
            End Set
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
                Return True
            End Get
        End Property

        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property LLBLGenProEntityTypeValue As Integer
            Get
                Return CInt(Questify.Builder.Model.ContentModel.EntityType.TreeStructureCustomBankPropertyValueEntity)
            End Get
        End Property






    End Class
End Namespace
