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
    Public Class ListCustomBankPropertyEntity
        Inherits CustomBankPropertyEntity



        Private WithEvents _listCustomBankPropertyValueCollection As EntityCollection(Of ListCustomBankPropertyValueEntity)
        Private WithEvents _listValueCustomBankPropertyCollection As EntityCollection(Of ListValueCustomBankPropertyEntity)



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
            Public Shared ReadOnly [ListCustomBankPropertyValueCollection] As String = "ListCustomBankPropertyValueCollection"
            Public Shared ReadOnly [ListValueCustomBankPropertyCollection] As String = "ListValueCustomBankPropertyCollection"
        End Class

        Shared Sub New()
            SetupCustomPropertyHashtables()
        End Sub

        Public Sub New()
            MyBase.New()
            InitClassEmpty()
            SetName("ListCustomBankPropertyEntity")
        End Sub

        Public Sub New(fields As IEntityFields2)
            MyBase.New(fields)
            InitClassEmpty()
            SetName("ListCustomBankPropertyEntity")
        End Sub

        Public Sub New(validator As IValidator)
            MyBase.New(validator)
            InitClassEmpty()
            SetName("ListCustomBankPropertyEntity")
        End Sub

        Public Sub New(customBankPropertyId As System.Guid)
            MyBase.New(customBankPropertyId)
            InitClassEmpty()

            SetName("ListCustomBankPropertyEntity")
        End Sub

        Public Sub New(customBankPropertyId As System.Guid, validator As IValidator)
            MyBase.New(customBankPropertyId, validator)
            InitClassEmpty()

            SetName("ListCustomBankPropertyEntity")
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Sub New(info As SerializationInfo, context As StreamingContext)
            MyBase.New(info, context)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                _listCustomBankPropertyValueCollection = CType(info.GetValue("_listCustomBankPropertyValueCollection", GetType(EntityCollection(Of ListCustomBankPropertyValueEntity))), EntityCollection(Of ListCustomBankPropertyValueEntity))
                _listValueCustomBankPropertyCollection = CType(info.GetValue("_listValueCustomBankPropertyCollection", GetType(EntityCollection(Of ListValueCustomBankPropertyEntity))), EntityCollection(Of ListValueCustomBankPropertyEntity))
                Me.FixupDeserialization(FieldInfoProviderSingleton.GetInstance())
            End If

        End Sub



        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub SetRelatedEntityProperty(propertyName As String, entity As IEntityCore)
            Select Case propertyName
                Case "ListCustomBankPropertyValueCollection"
                    Me.ListCustomBankPropertyValueCollection.Add(CType(entity, ListCustomBankPropertyValueEntity))
                Case "ListValueCustomBankPropertyCollection"
                    Me.ListValueCustomBankPropertyCollection.Add(CType(entity, ListValueCustomBankPropertyEntity))

                Case Else
                    MyBase.SetRelatedEntityProperty(propertyName, entity)
            End Select
        End Sub

        Protected Overrides Function GetRelationsForFieldOfType(fieldName As String) As RelationCollection
            Return ListCustomBankPropertyEntity.GetRelationsForField(fieldName)
        End Function

        Friend Shared Shadows Function GetRelationsForField(fieldName As String) As RelationCollection
            Dim toReturn As New RelationCollection()
            Select Case fieldName
                Case "ListCustomBankPropertyValueCollection"
                    toReturn.Add(ListCustomBankPropertyEntity.Relations.ListCustomBankPropertyValueEntityUsingCustomBankPropertyId)
                Case "ListValueCustomBankPropertyCollection"
                    toReturn.Add(ListCustomBankPropertyEntity.Relations.ListValueCustomBankPropertyEntityUsingCustomBankPropertyId)
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
                Case "ListCustomBankPropertyValueCollection"
                    Me.ListCustomBankPropertyValueCollection.Add(CType(relatedEntity, ListCustomBankPropertyValueEntity))
                Case "ListValueCustomBankPropertyCollection"
                    Me.ListValueCustomBankPropertyCollection.Add(CType(relatedEntity, ListValueCustomBankPropertyEntity))

                Case Else
                    MyBase.SetRelatedEntity(relatedEntity, fieldName)
            End Select
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub UnsetRelatedEntity(relatedEntity As IEntityCore, fieldName As String, signalRelatedEntityManyToOne As Boolean)
            Select Case fieldName
                Case "ListCustomBankPropertyValueCollection"
                    Me.PerformRelatedEntityRemoval(Me.ListCustomBankPropertyValueCollection, relatedEntity, signalRelatedEntityManyToOne)
                Case "ListValueCustomBankPropertyCollection"
                    Me.PerformRelatedEntityRemoval(Me.ListValueCustomBankPropertyCollection, relatedEntity, signalRelatedEntityManyToOne)
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
            toReturn.Add(Me.ListCustomBankPropertyValueCollection)
            toReturn.Add(Me.ListValueCustomBankPropertyCollection)
            toReturn.AddRange(MyBase.GetMemberEntityCollections())
            Return toReturn
        End Function

        Public Shadows Shared Function GetEntityTypeFilter() As IPredicateExpression
            Return InheritanceInfoProviderSingleton.GetInstance().GetEntityTypeFilter("ListCustomBankPropertyEntity", False)
        End Function

        Public Shadows Shared Function GetEntityTypeFilter(negate As Boolean) As IPredicateExpression
            Return InheritanceInfoProviderSingleton.GetInstance().GetEntityTypeFilter("ListCustomBankPropertyEntity", negate)
        End Function

        Protected Overrides Sub GetObjectData(info As SerializationInfo, context As StreamingContext)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                Dim value As IEntityCollection2 = Nothing
                Dim entityValue As IEntity2 = Nothing
                value = Nothing
                If (Not (_listCustomBankPropertyValueCollection Is Nothing)) AndAlso (_listCustomBankPropertyValueCollection.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _listCustomBankPropertyValueCollection
                End If
                info.AddValue("_listCustomBankPropertyValueCollection", value)
                value = Nothing
                If (Not (_listValueCustomBankPropertyCollection Is Nothing)) AndAlso (_listValueCustomBankPropertyCollection.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _listValueCustomBankPropertyCollection
                End If
                info.AddValue("_listValueCustomBankPropertyCollection", value)
            End If

            MyBase.GetObjectData(info, context)
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Function CheckIfIsSubTypeOf(typeOfEntity As Integer) As Boolean
            Return InheritanceInfoProviderSingleton.GetInstance().CheckIfIsSubTypeOf("ListCustomBankPropertyEntity", CType(typeOfEntity, Questify.Builder.Model.ContentModel.EntityType).ToString())
        End Function

        Protected Overrides Overloads Function GetAllRelations() As List(Of IEntityRelation)
            Return New ListCustomBankPropertyRelations().GetAllRelations()
        End Function

        Public Overridable Function GetRelationInfoListCustomBankPropertyValueCollection() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(ListCustomBankPropertyValueFields.CustomBankPropertyId, Nothing, ComparisonOperator.Equal, Me.CustomBankPropertyId))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoListValueCustomBankPropertyCollection() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(ListValueCustomBankPropertyFields.CustomBankPropertyId, Nothing, ComparisonOperator.Equal, Me.CustomBankPropertyId))
            Return bucket
        End Function

        Protected Overrides Function CreateEntityFactory() As IEntityFactory2
            Return EntityFactoryCache2.GetEntityFactory(GetType(ListCustomBankPropertyEntityFactory))
        End Function
#If Not CF Then
        Protected Overrides Sub AddToMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2))
            MyBase.AddToMemberEntityCollectionsQueue(collectionsQueue)
            collectionsQueue.Enqueue(_listCustomBankPropertyValueCollection)
            collectionsQueue.Enqueue(_listValueCustomBankPropertyCollection)
        End Sub

        Protected Overrides Sub GetFromMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2))
            MyBase.GetFromMemberEntityCollectionsQueue(collectionsQueue)
            _listCustomBankPropertyValueCollection = CType(collectionsQueue.Dequeue(), EntityCollection(Of ListCustomBankPropertyValueEntity))
            _listValueCustomBankPropertyCollection = CType(collectionsQueue.Dequeue(), EntityCollection(Of ListValueCustomBankPropertyEntity))
        End Sub

        Protected Overrides Function HasPopulatedMemberEntityCollections() As Boolean
            If (Not _listCustomBankPropertyValueCollection Is Nothing) Then
                Return True
            End If
            If (Not _listValueCustomBankPropertyCollection Is Nothing) Then
                Return True
            End If
            Return MyBase.HasPopulatedMemberEntityCollections()
        End Function

        Protected Overrides Overloads Sub CreateMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2), requiredQueue As Queue(Of Boolean))
            MyBase.CreateMemberEntityCollectionsQueue(collectionsQueue, requiredQueue)
            Dim toAdd As IEntityCollection2 = Nothing
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of ListCustomBankPropertyValueEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ListCustomBankPropertyValueEntityFactory)))
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
            toReturn.Add("ListCustomBankPropertyValueCollection", _listCustomBankPropertyValueCollection)
            toReturn.Add("ListValueCustomBankPropertyCollection", _listValueCustomBankPropertyCollection)
            Return toReturn
        End Function

        Private Sub InitClassMembers()


        End Sub

        Private Shared Sub SetupCustomPropertyHashtables()
            _customProperties = New Dictionary(Of String, String)()
            _fieldsCustomProperties = New Dictionary(Of String, Dictionary(Of String, String))()
            Dim fieldHashtable As Dictionary(Of String, String)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("MultipleSelect", fieldHashtable)
        End Sub





        Private Sub InitClassEmpty()
            InitClassMembers()




        End Sub

        Public Shadows Shared ReadOnly Property Relations() As ListCustomBankPropertyRelations
            Get
                Return New ListCustomBankPropertyRelations()
            End Get
        End Property

        Public Shadows Shared ReadOnly Property CustomProperties() As Dictionary(Of String, String)
            Get
                Return _customProperties
            End Get
        End Property


        Public Shared ReadOnly Property PrefetchPathListCustomBankPropertyValueCollection() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(Of ListCustomBankPropertyValueEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ListCustomBankPropertyValueEntityFactory))), _
                    CType(GetRelationsForField("ListCustomBankPropertyValueCollection")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.ListCustomBankPropertyEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.ListCustomBankPropertyValueEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "ListCustomBankPropertyValueCollection", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathListValueCustomBankPropertyCollection() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(Of ListValueCustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ListValueCustomBankPropertyEntityFactory))), _
                    CType(GetRelationsForField("ListValueCustomBankPropertyCollection")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.ListCustomBankPropertyEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.ListValueCustomBankPropertyEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "ListValueCustomBankPropertyCollection", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany)
            End Get
        End Property




        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property CustomPropertiesOfType() As Dictionary(Of String, String)
            Get
                Return ListCustomBankPropertyEntity.CustomProperties
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
                Return ListCustomBankPropertyEntity.FieldsCustomProperties
            End Get
        End Property

        Public Overridable Property [MultipleSelect]() As System.Boolean
            Get
                Return CType(GetValue(CInt(ListCustomBankPropertyFieldIndex.MultipleSelect), True), System.Boolean)
            End Get
            Set
                SetValue(CInt(ListCustomBankPropertyFieldIndex.MultipleSelect), value)
            End Set
        End Property

        <TypeContainedAttribute(GetType(ListCustomBankPropertyValueEntity))> _
        Public Overridable ReadOnly Property [ListCustomBankPropertyValueCollection]() As EntityCollection(Of ListCustomBankPropertyValueEntity)
            Get
                If _listCustomBankPropertyValueCollection Is Nothing Then
                    _listCustomBankPropertyValueCollection = New EntityCollection(Of ListCustomBankPropertyValueEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ListCustomBankPropertyValueEntityFactory)))
                    _listCustomBankPropertyValueCollection.ActiveContext = Me.ActiveContext
                    _listCustomBankPropertyValueCollection.SetContainingEntityInfo(Me, "ListCustomBankProperty")
                End If
                Return _listCustomBankPropertyValueCollection
            End Get
        End Property

        <TypeContainedAttribute(GetType(ListValueCustomBankPropertyEntity))> _
        Public Overridable ReadOnly Property [ListValueCustomBankPropertyCollection]() As EntityCollection(Of ListValueCustomBankPropertyEntity)
            Get
                If _listValueCustomBankPropertyCollection Is Nothing Then
                    _listValueCustomBankPropertyCollection = New EntityCollection(Of ListValueCustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ListValueCustomBankPropertyEntityFactory)))
                    _listValueCustomBankPropertyCollection.ActiveContext = Me.ActiveContext
                    _listValueCustomBankPropertyCollection.SetContainingEntityInfo(Me, "ListCustomBankProperty")
                End If
                Return _listValueCustomBankPropertyCollection
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
                Return CInt(Questify.Builder.Model.ContentModel.EntityType.ListCustomBankPropertyEntity)
            End Get
        End Property






    End Class
End Namespace
