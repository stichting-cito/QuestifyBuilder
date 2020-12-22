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
    Public Class TreeStructureCustomBankPropertyEntity
        Inherits CustomBankPropertyEntity



        Private WithEvents _treeStructureCustomBankPropertyValueCollection As EntityCollection(Of TreeStructureCustomBankPropertyValueEntity)
        Private WithEvents _treeStructurePartCustomBankPropertyCollection As EntityCollection(Of TreeStructurePartCustomBankPropertyEntity)



        Private Shared _customProperties As Dictionary(Of String, String)
        Private Shared _fieldsCustomProperties As Dictionary(Of String, Dictionary(Of String, String))

        Public Shadows NotInheritable Class MemberNames
            Private Sub New()
            End Sub
            Public Shared ReadOnly [Bank] As String = "Bank"
            Public Shared ReadOnly [State] As String = "State"
            Public Shared ReadOnly [CreatedByUser] As String = "CreatedByUser"
            Public Shared ReadOnly [ModifiedByUser] As String = "ModifiedByUser"
            Public Shared ReadOnly [CustomBankPropertyValueCollection] As String = "CustomBankPropertyValueCollection"
            Public Shared ReadOnly [TreeStructureCustomBankPropertyValueCollection] As String = "TreeStructureCustomBankPropertyValueCollection"
            Public Shared ReadOnly [TreeStructurePartCustomBankPropertyCollection] As String = "TreeStructurePartCustomBankPropertyCollection"
        End Class

        Shared Sub New()
            SetupCustomPropertyHashtables()
        End Sub

        Public Sub New()
            MyBase.New()
            InitClassEmpty()
            SetName("TreeStructureCustomBankPropertyEntity")
        End Sub

        Public Sub New(fields As IEntityFields2)
            MyBase.New(fields)
            InitClassEmpty()
            SetName("TreeStructureCustomBankPropertyEntity")
        End Sub

        Public Sub New(validator As IValidator)
            MyBase.New(validator)
            InitClassEmpty()
            SetName("TreeStructureCustomBankPropertyEntity")
        End Sub

        Public Sub New(customBankPropertyId As System.Guid)
            MyBase.New(customBankPropertyId)
            InitClassEmpty()

            SetName("TreeStructureCustomBankPropertyEntity")
        End Sub

        Public Sub New(customBankPropertyId As System.Guid, validator As IValidator)
            MyBase.New(customBankPropertyId, validator)
            InitClassEmpty()

            SetName("TreeStructureCustomBankPropertyEntity")
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Sub New(info As SerializationInfo, context As StreamingContext)
            MyBase.New(info, context)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                _treeStructureCustomBankPropertyValueCollection = CType(info.GetValue("_treeStructureCustomBankPropertyValueCollection", GetType(EntityCollection(Of TreeStructureCustomBankPropertyValueEntity))), EntityCollection(Of TreeStructureCustomBankPropertyValueEntity))
                _treeStructurePartCustomBankPropertyCollection = CType(info.GetValue("_treeStructurePartCustomBankPropertyCollection", GetType(EntityCollection(Of TreeStructurePartCustomBankPropertyEntity))), EntityCollection(Of TreeStructurePartCustomBankPropertyEntity))
                Me.FixupDeserialization(FieldInfoProviderSingleton.GetInstance())
            End If

        End Sub



        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub SetRelatedEntityProperty(propertyName As String, entity As IEntityCore)
            Select Case propertyName
                Case "TreeStructureCustomBankPropertyValueCollection"
                    Me.TreeStructureCustomBankPropertyValueCollection.Add(CType(entity, TreeStructureCustomBankPropertyValueEntity))
                Case "TreeStructurePartCustomBankPropertyCollection"
                    Me.TreeStructurePartCustomBankPropertyCollection.Add(CType(entity, TreeStructurePartCustomBankPropertyEntity))

                Case Else
                    MyBase.SetRelatedEntityProperty(propertyName, entity)
            End Select
        End Sub

        Protected Overrides Function GetRelationsForFieldOfType(fieldName As String) As RelationCollection
            Return TreeStructureCustomBankPropertyEntity.GetRelationsForField(fieldName)
        End Function

        Friend Shared Shadows Function GetRelationsForField(fieldName As String) As RelationCollection
            Dim toReturn As New RelationCollection()
            Select Case fieldName
                Case "TreeStructureCustomBankPropertyValueCollection"
                    toReturn.Add(TreeStructureCustomBankPropertyEntity.Relations.TreeStructureCustomBankPropertyValueEntityUsingCustomBankPropertyId)
                Case "TreeStructurePartCustomBankPropertyCollection"
                    toReturn.Add(TreeStructureCustomBankPropertyEntity.Relations.TreeStructurePartCustomBankPropertyEntityUsingCustomBankPropertyId)
                Case Else
                    toReturn = CustomBankPropertyEntity.GetRelationsForField(fieldName)
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
                Case "TreeStructureCustomBankPropertyValueCollection"
                    Me.TreeStructureCustomBankPropertyValueCollection.Add(CType(relatedEntity, TreeStructureCustomBankPropertyValueEntity))
                Case "TreeStructurePartCustomBankPropertyCollection"
                    Me.TreeStructurePartCustomBankPropertyCollection.Add(CType(relatedEntity, TreeStructurePartCustomBankPropertyEntity))

                Case Else
                    MyBase.SetRelatedEntity(relatedEntity, fieldName)
            End Select
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub UnsetRelatedEntity(relatedEntity As IEntityCore, fieldName As String, signalRelatedEntityManyToOne As Boolean)
            Select Case fieldName
                Case "TreeStructureCustomBankPropertyValueCollection"
                    Me.PerformRelatedEntityRemoval(Me.TreeStructureCustomBankPropertyValueCollection, relatedEntity, signalRelatedEntityManyToOne)
                Case "TreeStructurePartCustomBankPropertyCollection"
                    Me.PerformRelatedEntityRemoval(Me.TreeStructurePartCustomBankPropertyCollection, relatedEntity, signalRelatedEntityManyToOne)
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
            toReturn.AddRange(MyBase.GetDependentRelatedEntities())
            Return toReturn
        End Function

        Protected Overrides Function GetMemberEntityCollections() As List(Of IEntityCollection2)
            Dim toReturn As New List(Of IEntityCollection2)()
            toReturn.Add(Me.TreeStructureCustomBankPropertyValueCollection)
            toReturn.Add(Me.TreeStructurePartCustomBankPropertyCollection)
            toReturn.AddRange(MyBase.GetMemberEntityCollections())
            Return toReturn
        End Function

        Public Shadows Shared Function GetEntityTypeFilter() As IPredicateExpression
            Return InheritanceInfoProviderSingleton.GetInstance().GetEntityTypeFilter("TreeStructureCustomBankPropertyEntity", False)
        End Function

        Public Shadows Shared Function GetEntityTypeFilter(negate As Boolean) As IPredicateExpression
            Return InheritanceInfoProviderSingleton.GetInstance().GetEntityTypeFilter("TreeStructureCustomBankPropertyEntity", negate)
        End Function

        Protected Overrides Sub GetObjectData(info As SerializationInfo, context As StreamingContext)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                Dim value As IEntityCollection2 = Nothing
                Dim entityValue As IEntity2 = Nothing
                value = Nothing
                If (Not (_treeStructureCustomBankPropertyValueCollection Is Nothing)) AndAlso (_treeStructureCustomBankPropertyValueCollection.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _treeStructureCustomBankPropertyValueCollection
                End If
                info.AddValue("_treeStructureCustomBankPropertyValueCollection", value)
                value = Nothing
                If (Not (_treeStructurePartCustomBankPropertyCollection Is Nothing)) AndAlso (_treeStructurePartCustomBankPropertyCollection.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _treeStructurePartCustomBankPropertyCollection
                End If
                info.AddValue("_treeStructurePartCustomBankPropertyCollection", value)
            End If

            MyBase.GetObjectData(info, context)
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Function CheckIfIsSubTypeOf(typeOfEntity As Integer) As Boolean
            Return InheritanceInfoProviderSingleton.GetInstance().CheckIfIsSubTypeOf("TreeStructureCustomBankPropertyEntity", CType(typeOfEntity, Questify.Builder.Model.ContentModel.EntityType).ToString())
        End Function

        Protected Overrides Overloads Function GetAllRelations() As List(Of IEntityRelation)
            Return New TreeStructureCustomBankPropertyRelations().GetAllRelations()
        End Function

        Public Overridable Function GetRelationInfoTreeStructureCustomBankPropertyValueCollection() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(TreeStructureCustomBankPropertyValueFields.CustomBankPropertyId, Nothing, ComparisonOperator.Equal, Me.CustomBankPropertyId))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoTreeStructurePartCustomBankPropertyCollection() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(TreeStructurePartCustomBankPropertyFields.CustomBankPropertyId, Nothing, ComparisonOperator.Equal, Me.CustomBankPropertyId))
            Return bucket
        End Function

        Protected Overrides Function CreateEntityFactory() As IEntityFactory2
            Return EntityFactoryCache2.GetEntityFactory(GetType(TreeStructureCustomBankPropertyEntityFactory))
        End Function
#If Not CF Then
        Protected Overrides Sub AddToMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2))
            MyBase.AddToMemberEntityCollectionsQueue(collectionsQueue)
            collectionsQueue.Enqueue(_treeStructureCustomBankPropertyValueCollection)
            collectionsQueue.Enqueue(_treeStructurePartCustomBankPropertyCollection)
        End Sub

        Protected Overrides Sub GetFromMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2))
            MyBase.GetFromMemberEntityCollectionsQueue(collectionsQueue)
            _treeStructureCustomBankPropertyValueCollection = CType(collectionsQueue.Dequeue(), EntityCollection(Of TreeStructureCustomBankPropertyValueEntity))
            _treeStructurePartCustomBankPropertyCollection = CType(collectionsQueue.Dequeue(), EntityCollection(Of TreeStructurePartCustomBankPropertyEntity))
        End Sub

        Protected Overrides Function HasPopulatedMemberEntityCollections() As Boolean
            If (Not _treeStructureCustomBankPropertyValueCollection Is Nothing) Then
                Return True
            End If
            If (Not _treeStructurePartCustomBankPropertyCollection Is Nothing) Then
                Return True
            End If
            Return MyBase.HasPopulatedMemberEntityCollections()
        End Function

        Protected Overrides Overloads Sub CreateMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2), requiredQueue As Queue(Of Boolean))
            MyBase.CreateMemberEntityCollectionsQueue(collectionsQueue, requiredQueue)
            Dim toAdd As IEntityCollection2 = Nothing
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of TreeStructureCustomBankPropertyValueEntity)(EntityFactoryCache2.GetEntityFactory(GetType(TreeStructureCustomBankPropertyValueEntityFactory)))
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
            toReturn.Add("TreeStructureCustomBankPropertyValueCollection", _treeStructureCustomBankPropertyValueCollection)
            toReturn.Add("TreeStructurePartCustomBankPropertyCollection", _treeStructurePartCustomBankPropertyCollection)
            Return toReturn
        End Function

        Private Sub InitClassMembers()


        End Sub

        Private Shared Sub SetupCustomPropertyHashtables()
            _customProperties = New Dictionary(Of String, String)()
            _fieldsCustomProperties = New Dictionary(Of String, Dictionary(Of String, String))()
        End Sub





        Private Sub InitClassEmpty()
            InitClassMembers()




        End Sub

        Public Shadows Shared ReadOnly Property Relations() As TreeStructureCustomBankPropertyRelations
            Get
                Return New TreeStructureCustomBankPropertyRelations()
            End Get
        End Property

        Public Shadows Shared ReadOnly Property CustomProperties() As Dictionary(Of String, String)
            Get
                Return _customProperties
            End Get
        End Property


        Public Shared ReadOnly Property PrefetchPathTreeStructureCustomBankPropertyValueCollection() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(Of TreeStructureCustomBankPropertyValueEntity)(EntityFactoryCache2.GetEntityFactory(GetType(TreeStructureCustomBankPropertyValueEntityFactory))), _
                    CType(GetRelationsForField("TreeStructureCustomBankPropertyValueCollection")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.TreeStructureCustomBankPropertyEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.TreeStructureCustomBankPropertyValueEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "TreeStructureCustomBankPropertyValueCollection", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathTreeStructurePartCustomBankPropertyCollection() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(Of TreeStructurePartCustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(TreeStructurePartCustomBankPropertyEntityFactory))), _
                    CType(GetRelationsForField("TreeStructurePartCustomBankPropertyCollection")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.TreeStructureCustomBankPropertyEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.TreeStructurePartCustomBankPropertyEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "TreeStructurePartCustomBankPropertyCollection", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany)
            End Get
        End Property




        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property CustomPropertiesOfType() As Dictionary(Of String, String)
            Get
                Return TreeStructureCustomBankPropertyEntity.CustomProperties
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
                Return TreeStructureCustomBankPropertyEntity.FieldsCustomProperties
            End Get
        End Property


        <TypeContainedAttribute(GetType(TreeStructureCustomBankPropertyValueEntity))> _
        Public Overridable ReadOnly Property [TreeStructureCustomBankPropertyValueCollection]() As EntityCollection(Of TreeStructureCustomBankPropertyValueEntity)
            Get
                If _treeStructureCustomBankPropertyValueCollection Is Nothing Then
                    _treeStructureCustomBankPropertyValueCollection = New EntityCollection(Of TreeStructureCustomBankPropertyValueEntity)(EntityFactoryCache2.GetEntityFactory(GetType(TreeStructureCustomBankPropertyValueEntityFactory)))
                    _treeStructureCustomBankPropertyValueCollection.ActiveContext = Me.ActiveContext
                    _treeStructureCustomBankPropertyValueCollection.SetContainingEntityInfo(Me, "TreeStructureCustomBankProperty")
                End If
                Return _treeStructureCustomBankPropertyValueCollection
            End Get
        End Property

        <TypeContainedAttribute(GetType(TreeStructurePartCustomBankPropertyEntity))> _
        Public Overridable ReadOnly Property [TreeStructurePartCustomBankPropertyCollection]() As EntityCollection(Of TreeStructurePartCustomBankPropertyEntity)
            Get
                If _treeStructurePartCustomBankPropertyCollection Is Nothing Then
                    _treeStructurePartCustomBankPropertyCollection = New EntityCollection(Of TreeStructurePartCustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(TreeStructurePartCustomBankPropertyEntityFactory)))
                    _treeStructurePartCustomBankPropertyCollection.ActiveContext = Me.ActiveContext
                    _treeStructurePartCustomBankPropertyCollection.SetContainingEntityInfo(Me, "TreeStructureCustomBankProperty")
                End If
                Return _treeStructurePartCustomBankPropertyCollection
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
                Return True
            End Get
        End Property

        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property LLBLGenProEntityTypeValue As Integer
            Get
                Return CInt(Questify.Builder.Model.ContentModel.EntityType.TreeStructureCustomBankPropertyEntity)
            End Get
        End Property






    End Class
End Namespace
