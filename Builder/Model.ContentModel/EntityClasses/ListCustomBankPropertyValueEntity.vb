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
    Public Class ListCustomBankPropertyValueEntity
        Inherits CustomBankPropertyValueEntity



        Private WithEvents _listCustomBankPropertySelectedValueCollection As EntityCollection(Of ListCustomBankPropertySelectedValueEntity)
        Private WithEvents _listValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue As EntityCollection(Of ListValueCustomBankPropertyEntity)
        Private WithEvents _listCustomBankProperty As ListCustomBankPropertyEntity



        Private Shared _customProperties As Dictionary(Of String, String)
        Private Shared _fieldsCustomProperties As Dictionary(Of String, Dictionary(Of String, String))

        Public Shadows NotInheritable Class MemberNames
            Private Sub New()
            End Sub
            Public Shared ReadOnly [CustomBankProperty] As String = "CustomBankProperty"
            Public Shared ReadOnly [ListCustomBankProperty] As String = "ListCustomBankProperty"
            Public Shared ReadOnly [Resource] As String = "Resource"
            Public Shared ReadOnly [ListCustomBankPropertySelectedValueCollection] As String = "ListCustomBankPropertySelectedValueCollection"
            Public Shared ReadOnly [ListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue] As String = "ListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue"
        End Class

        Shared Sub New()
            SetupCustomPropertyHashtables()
        End Sub

        Public Sub New()
            MyBase.New()
            InitClassEmpty()
            SetName("ListCustomBankPropertyValueEntity")
        End Sub

        Public Sub New(fields As IEntityFields2)
            MyBase.New(fields)
            InitClassEmpty()
            SetName("ListCustomBankPropertyValueEntity")
        End Sub

        Public Sub New(validator As IValidator)
            MyBase.New(validator)
            InitClassEmpty()
            SetName("ListCustomBankPropertyValueEntity")
        End Sub

        Public Sub New(resourceId As System.Guid, customBankPropertyId As System.Guid)
            MyBase.New(resourceId, customBankPropertyId)
            InitClassEmpty()

            SetName("ListCustomBankPropertyValueEntity")
        End Sub

        Public Sub New(resourceId As System.Guid, customBankPropertyId As System.Guid, validator As IValidator)
            MyBase.New(resourceId, customBankPropertyId, validator)
            InitClassEmpty()

            SetName("ListCustomBankPropertyValueEntity")
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Sub New(info As SerializationInfo, context As StreamingContext)
            MyBase.New(info, context)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                _listCustomBankPropertySelectedValueCollection = CType(info.GetValue("_listCustomBankPropertySelectedValueCollection", GetType(EntityCollection(Of ListCustomBankPropertySelectedValueEntity))), EntityCollection(Of ListCustomBankPropertySelectedValueEntity))
                _listValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue = CType(info.GetValue("_listValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue", GetType(EntityCollection(Of ListValueCustomBankPropertyEntity))), EntityCollection(Of ListValueCustomBankPropertyEntity))
                _listCustomBankProperty = CType(info.GetValue("_listCustomBankProperty", GetType(ListCustomBankPropertyEntity)), ListCustomBankPropertyEntity)
                If Not _listCustomBankProperty Is Nothing Then
                    AddHandler _listCustomBankProperty.AfterSave, AddressOf OnEntityAfterSave
                End If
                Me.FixupDeserialization(FieldInfoProviderSingleton.GetInstance())
            End If

        End Sub



        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub SetRelatedEntityProperty(propertyName As String, entity As IEntityCore)
            Select Case propertyName
                Case "ListCustomBankProperty"
                    Me.ListCustomBankProperty = CType(entity, ListCustomBankPropertyEntity)
                Case "ListCustomBankPropertySelectedValueCollection"
                    Me.ListCustomBankPropertySelectedValueCollection.Add(CType(entity, ListCustomBankPropertySelectedValueEntity))
                Case "ListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue"
                    Me.ListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue.IsReadOnly = False
                    Me.ListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue.Add(CType(entity, ListValueCustomBankPropertyEntity))
                    Me.ListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue.IsReadOnly = True

                Case Else
                    MyBase.SetRelatedEntityProperty(propertyName, entity)
            End Select
        End Sub

        Protected Overrides Function GetRelationsForFieldOfType(fieldName As String) As RelationCollection
            Return ListCustomBankPropertyValueEntity.GetRelationsForField(fieldName)
        End Function

        Friend Shared Shadows Function GetRelationsForField(fieldName As String) As RelationCollection
            Dim toReturn As New RelationCollection()
            Select Case fieldName
                Case "ListCustomBankProperty"
                    toReturn.Add(ListCustomBankPropertyValueEntity.Relations.ListCustomBankPropertyEntityUsingCustomBankPropertyId)
                Case "ListCustomBankPropertySelectedValueCollection"
                    toReturn.Add(ListCustomBankPropertyValueEntity.Relations.ListCustomBankPropertySelectedValueEntityUsingCustomBankPropertyIdResourceId)
                Case "ListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue"
                    toReturn.Add(ListCustomBankPropertyValueEntity.Relations.ListCustomBankPropertySelectedValueEntityUsingCustomBankPropertyIdResourceId, "ListCustomBankPropertyValueEntity__", "ListCustomBankPropertySelectedValue_", JoinHint.None)
                    toReturn.Add(ListCustomBankPropertySelectedValueEntity.Relations.ListValueCustomBankPropertyEntityUsingListValueBankCustomPropertyId, "ListCustomBankPropertySelectedValue_", String.Empty, JoinHint.None)
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
                Case "ListCustomBankProperty"
                    SetupSyncListCustomBankProperty(relatedEntity)
                Case "ListCustomBankPropertySelectedValueCollection"
                    Me.ListCustomBankPropertySelectedValueCollection.Add(CType(relatedEntity, ListCustomBankPropertySelectedValueEntity))

                Case Else
                    MyBase.SetRelatedEntity(relatedEntity, fieldName)
            End Select
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub UnsetRelatedEntity(relatedEntity As IEntityCore, fieldName As String, signalRelatedEntityManyToOne As Boolean)
            Select Case fieldName
                Case "ListCustomBankProperty"
                    DesetupSyncListCustomBankProperty(False, True)
                Case "ListCustomBankPropertySelectedValueCollection"
                    Me.PerformRelatedEntityRemoval(Me.ListCustomBankPropertySelectedValueCollection, relatedEntity, signalRelatedEntityManyToOne)
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
            If Not _listCustomBankProperty Is Nothing Then
                toReturn.Add(_listCustomBankProperty)
            End If
            toReturn.AddRange(MyBase.GetDependentRelatedEntities())
            Return toReturn
        End Function

        Protected Overrides Function GetMemberEntityCollections() As List(Of IEntityCollection2)
            Dim toReturn As New List(Of IEntityCollection2)()
            toReturn.Add(Me.ListCustomBankPropertySelectedValueCollection)
            toReturn.AddRange(MyBase.GetMemberEntityCollections())
            Return toReturn
        End Function

        Public Shadows Shared Function GetEntityTypeFilter() As IPredicateExpression
            Return InheritanceInfoProviderSingleton.GetInstance().GetEntityTypeFilter("ListCustomBankPropertyValueEntity", False)
        End Function

        Public Shadows Shared Function GetEntityTypeFilter(negate As Boolean) As IPredicateExpression
            Return InheritanceInfoProviderSingleton.GetInstance().GetEntityTypeFilter("ListCustomBankPropertyValueEntity", negate)
        End Function

        Protected Overrides Sub GetObjectData(info As SerializationInfo, context As StreamingContext)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                Dim value As IEntityCollection2 = Nothing
                Dim entityValue As IEntity2 = Nothing
                value = Nothing
                If (Not (_listCustomBankPropertySelectedValueCollection Is Nothing)) AndAlso (_listCustomBankPropertySelectedValueCollection.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _listCustomBankPropertySelectedValueCollection
                End If
                info.AddValue("_listCustomBankPropertySelectedValueCollection", value)
                value = Nothing
                If (Not (_listValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue Is Nothing)) AndAlso (_listValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _listValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue
                End If
                info.AddValue("_listValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue", value)
                entityValue = Nothing
                If Not Me.MarkedForDeletion Then
                    entityValue = _listCustomBankProperty
                End If
                info.AddValue("_listCustomBankProperty", entityValue)
            End If

            MyBase.GetObjectData(info, context)
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Function CheckIfIsSubTypeOf(typeOfEntity As Integer) As Boolean
            Return InheritanceInfoProviderSingleton.GetInstance().CheckIfIsSubTypeOf("ListCustomBankPropertyValueEntity", CType(typeOfEntity, Questify.Builder.Model.ContentModel.EntityType).ToString())
        End Function

        Protected Overrides Overloads Function GetAllRelations() As List(Of IEntityRelation)
            Return New ListCustomBankPropertyValueRelations().GetAllRelations()
        End Function

        Public Overridable Function GetRelationInfoListCustomBankPropertySelectedValueCollection() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(ListCustomBankPropertySelectedValueFields.CustomBankPropertyId, Nothing, ComparisonOperator.Equal, Me.CustomBankPropertyId))
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(ListCustomBankPropertySelectedValueFields.ResourceId, Nothing, ComparisonOperator.Equal, Me.ResourceId))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.Relations.AddRange(GetRelationsForFieldOfType("ListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue"))
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(ListCustomBankPropertyValueFields.ResourceId, Nothing, ComparisonOperator.Equal, Me.ResourceId, "ListCustomBankPropertyValueEntity__"))
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(ListCustomBankPropertyValueFields.CustomBankPropertyId, Nothing, ComparisonOperator.Equal, Me.CustomBankPropertyId, "ListCustomBankPropertyValueEntity__"))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoListCustomBankProperty() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(ListCustomBankPropertyFields.CustomBankPropertyId, Nothing, ComparisonOperator.Equal, Me.CustomBankPropertyId))
            Return bucket
        End Function

        Protected Overrides Function CreateEntityFactory() As IEntityFactory2
            Return EntityFactoryCache2.GetEntityFactory(GetType(ListCustomBankPropertyValueEntityFactory))
        End Function
#If Not CF Then
        Protected Overrides Sub AddToMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2))
            MyBase.AddToMemberEntityCollectionsQueue(collectionsQueue)
            collectionsQueue.Enqueue(_listCustomBankPropertySelectedValueCollection)
            collectionsQueue.Enqueue(_listValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue)
        End Sub

        Protected Overrides Sub GetFromMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2))
            MyBase.GetFromMemberEntityCollectionsQueue(collectionsQueue)
            _listCustomBankPropertySelectedValueCollection = CType(collectionsQueue.Dequeue(), EntityCollection(Of ListCustomBankPropertySelectedValueEntity))
            _listValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue = CType(collectionsQueue.Dequeue(), EntityCollection(Of ListValueCustomBankPropertyEntity))
        End Sub

        Protected Overrides Function HasPopulatedMemberEntityCollections() As Boolean
            If (Not _listCustomBankPropertySelectedValueCollection Is Nothing) Then
                Return True
            End If
            If (Not _listValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue Is Nothing) Then
                Return True
            End If
            Return MyBase.HasPopulatedMemberEntityCollections()
        End Function

        Protected Overrides Overloads Sub CreateMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2), requiredQueue As Queue(Of Boolean))
            MyBase.CreateMemberEntityCollectionsQueue(collectionsQueue, requiredQueue)
            Dim toAdd As IEntityCollection2 = Nothing
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of ListCustomBankPropertySelectedValueEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ListCustomBankPropertySelectedValueEntityFactory)))
            Else
                toAdd = Nothing
            End If
            collectionsQueue.Enqueue(toAdd)
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of ListValueCustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ListValueCustomBankPropertyEntityFactory)))
            Else
                toAdd = Nothing
            End If
            collectionsQueue.Enqueue(toAdd)
        End Sub
#End If
        Protected Overrides Overloads Function GetRelatedData() As Dictionary(Of String, Object)
            Dim toReturn As Dictionary(Of String, Object) = MyBase.GetRelatedData()
            toReturn.Add("ListCustomBankProperty", _listCustomBankProperty)
            toReturn.Add("ListCustomBankPropertySelectedValueCollection", _listCustomBankPropertySelectedValueCollection)
            toReturn.Add("ListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue", _listValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue)
            Return toReturn
        End Function

        Private Sub InitClassMembers()


        End Sub

        Private Shared Sub SetupCustomPropertyHashtables()
            _customProperties = New Dictionary(Of String, String)()
            _fieldsCustomProperties = New Dictionary(Of String, Dictionary(Of String, String))()
        End Sub


        Private Sub DesetupSyncListCustomBankProperty(signalRelatedEntity As Boolean, resetFKFields As Boolean)
            Me.PerformDesetupSyncRelatedEntity(_listCustomBankProperty, AddressOf OnListCustomBankPropertyPropertyChanged, "ListCustomBankProperty", Questify.Builder.Model.ContentModel.RelationClasses.StaticListCustomBankPropertyValueRelations.ListCustomBankPropertyEntityUsingCustomBankPropertyIdStatic, True, signalRelatedEntity, "ListCustomBankPropertyValueCollection", resetFKFields, New Integer() {CInt(ListCustomBankPropertyValueFieldIndex.CustomBankPropertyId)})
            _listCustomBankProperty = Nothing
        End Sub

        Private Sub SetupSyncListCustomBankProperty(relatedEntity As IEntityCore)
            If Not _listCustomBankProperty Is relatedEntity Then
                DesetupSyncListCustomBankProperty(True, True)
                _listCustomBankProperty = CType(relatedEntity, ListCustomBankPropertyEntity)
                Me.PerformSetupSyncRelatedEntity(_listCustomBankProperty, AddressOf OnListCustomBankPropertyPropertyChanged, "ListCustomBankProperty", Questify.Builder.Model.ContentModel.RelationClasses.StaticListCustomBankPropertyValueRelations.ListCustomBankPropertyEntityUsingCustomBankPropertyIdStatic, True, New String() {})
            End If
        End Sub

        Private Sub OnListCustomBankPropertyPropertyChanged(sender As Object, e As PropertyChangedEventArgs)
            Select Case e.PropertyName

                Case Else
            End Select
        End Sub



        Private Sub InitClassEmpty()
            InitClassMembers()




        End Sub

        Public Shadows Shared ReadOnly Property Relations() As ListCustomBankPropertyValueRelations
            Get
                Return New ListCustomBankPropertyValueRelations()
            End Get
        End Property

        Public Shadows Shared ReadOnly Property CustomProperties() As Dictionary(Of String, String)
            Get
                Return _customProperties
            End Get
        End Property


        Public Shared ReadOnly Property PrefetchPathListCustomBankPropertySelectedValueCollection() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(Of ListCustomBankPropertySelectedValueEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ListCustomBankPropertySelectedValueEntityFactory))), _
                    CType(GetRelationsForField("ListCustomBankPropertySelectedValueCollection")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.ListCustomBankPropertyValueEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.ListCustomBankPropertySelectedValueEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "ListCustomBankPropertySelectedValueCollection", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue() As IPrefetchPathElement2
            Get
                Dim intermediateRelation As IEntityRelation = ListCustomBankPropertyValueEntity.Relations.ListCustomBankPropertySelectedValueEntityUsingCustomBankPropertyIdResourceId
                intermediateRelation.SetAliases(String.Empty, "ListCustomBankPropertySelectedValue_")
                Return New PrefetchPathElement2(New EntityCollection(Of ListValueCustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ListValueCustomBankPropertyEntityFactory))), _
                    intermediateRelation, CType(Questify.Builder.Model.ContentModel.EntityType.ListCustomBankPropertyValueEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.ListValueCustomBankPropertyEntity, Integer), 0, Nothing, Nothing, GetRelationsForField("ListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue"), Nothing, "ListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathListCustomBankProperty() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(EntityFactoryCache2.GetEntityFactory(GetType(ListCustomBankPropertyEntityFactory))), _
                    CType(GetRelationsForField("ListCustomBankProperty")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.ListCustomBankPropertyValueEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.ListCustomBankPropertyEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "ListCustomBankProperty", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne)
            End Get
        End Property


        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property CustomPropertiesOfType() As Dictionary(Of String, String)
            Get
                Return ListCustomBankPropertyValueEntity.CustomProperties
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
                Return ListCustomBankPropertyValueEntity.FieldsCustomProperties
            End Get
        End Property


        <TypeContainedAttribute(GetType(ListCustomBankPropertySelectedValueEntity))> _
        Public Overridable ReadOnly Property [ListCustomBankPropertySelectedValueCollection]() As EntityCollection(Of ListCustomBankPropertySelectedValueEntity)
            Get
                If _listCustomBankPropertySelectedValueCollection Is Nothing Then
                    _listCustomBankPropertySelectedValueCollection = New EntityCollection(Of ListCustomBankPropertySelectedValueEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ListCustomBankPropertySelectedValueEntityFactory)))
                    _listCustomBankPropertySelectedValueCollection.ActiveContext = Me.ActiveContext
                    _listCustomBankPropertySelectedValueCollection.SetContainingEntityInfo(Me, "ListCustomBankPropertyValue")
                End If
                Return _listCustomBankPropertySelectedValueCollection
            End Get
        End Property

        <TypeContainedAttribute(GetType(ListValueCustomBankPropertyEntity))> _
        Public Overridable ReadOnly Property [ListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue]() As EntityCollection(Of ListValueCustomBankPropertyEntity)
            Get
                If _listValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue Is Nothing Then
                    _listValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue = New EntityCollection(Of ListValueCustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ListValueCustomBankPropertyEntityFactory)))
                    _listValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue.ActiveContext = Me.ActiveContext
                    _listValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue.IsReadOnly = True
                    CType(_listValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue, IEntityCollectionCore).IsForMN = True
                End If
                Return _listValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue
            End Get
        End Property

        <Browsable(True)> _
        Public Overridable Property [ListCustomBankProperty]() As ListCustomBankPropertyEntity
            Get
                Return _listCustomBankProperty
            End Get
            Set
                If MyBase.IsDeserializing Then
                    SetupSyncListCustomBankProperty(value)
                Else
                    SetSingleRelatedEntityNavigator(value, "ListCustomBankPropertyValueCollection", "ListCustomBankProperty", _listCustomBankProperty, True)
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
                Return CInt(Questify.Builder.Model.ContentModel.EntityType.ListCustomBankPropertyValueEntity)
            End Get
        End Property






    End Class
End Namespace
