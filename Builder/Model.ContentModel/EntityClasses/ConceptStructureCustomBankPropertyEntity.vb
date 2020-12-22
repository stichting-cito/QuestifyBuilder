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
    Public Class ConceptStructureCustomBankPropertyEntity
        Inherits CustomBankPropertyEntity



        Private WithEvents _conceptStructureCustomBankPropertyValueCollection As EntityCollection(Of ConceptStructureCustomBankPropertyValueEntity)
        Private WithEvents _conceptStructurePartCustomBankPropertyCollection As EntityCollection(Of ConceptStructurePartCustomBankPropertyEntity)
        Private WithEvents _conceptTypeCollectionViaConceptStructurePartCustomBankProperty As EntityCollection(Of ConceptTypeEntity)



        Private Shared _customProperties As Dictionary(Of String, String)
        Private Shared _fieldsCustomProperties As Dictionary(Of String, Dictionary(Of String, String))

        Public Shadows NotInheritable Class MemberNames
            Private Sub New()
            End Sub
            Public Shared ReadOnly [Bank] As String = "Bank"
            Public Shared ReadOnly [State] As String = "State"
            Public Shared ReadOnly [CreatedByUser] As String = "CreatedByUser"
            Public Shared ReadOnly [ModifiedByUser] As String = "ModifiedByUser"
            Public Shared ReadOnly [ConceptStructureCustomBankPropertyValueCollection] As String = "ConceptStructureCustomBankPropertyValueCollection"
            Public Shared ReadOnly [ConceptStructurePartCustomBankPropertyCollection] As String = "ConceptStructurePartCustomBankPropertyCollection"
            Public Shared ReadOnly [CustomBankPropertyValueCollection] As String = "CustomBankPropertyValueCollection"
            Public Shared ReadOnly [ConceptTypeCollectionViaConceptStructurePartCustomBankProperty] As String = "ConceptTypeCollectionViaConceptStructurePartCustomBankProperty"
        End Class

        Shared Sub New()
            SetupCustomPropertyHashtables()
        End Sub

        Public Sub New()
            MyBase.New()
            InitClassEmpty()
            SetName("ConceptStructureCustomBankPropertyEntity")
        End Sub

        Public Sub New(fields As IEntityFields2)
            MyBase.New(fields)
            InitClassEmpty()
            SetName("ConceptStructureCustomBankPropertyEntity")
        End Sub

        Public Sub New(validator As IValidator)
            MyBase.New(validator)
            InitClassEmpty()
            SetName("ConceptStructureCustomBankPropertyEntity")
        End Sub

        Public Sub New(customBankPropertyId As System.Guid)
            MyBase.New(customBankPropertyId)
            InitClassEmpty()

            SetName("ConceptStructureCustomBankPropertyEntity")
        End Sub

        Public Sub New(customBankPropertyId As System.Guid, validator As IValidator)
            MyBase.New(customBankPropertyId, validator)
            InitClassEmpty()

            SetName("ConceptStructureCustomBankPropertyEntity")
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Sub New(info As SerializationInfo, context As StreamingContext)
            MyBase.New(info, context)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                _conceptStructureCustomBankPropertyValueCollection = CType(info.GetValue("_conceptStructureCustomBankPropertyValueCollection", GetType(EntityCollection(Of ConceptStructureCustomBankPropertyValueEntity))), EntityCollection(Of ConceptStructureCustomBankPropertyValueEntity))
                _conceptStructurePartCustomBankPropertyCollection = CType(info.GetValue("_conceptStructurePartCustomBankPropertyCollection", GetType(EntityCollection(Of ConceptStructurePartCustomBankPropertyEntity))), EntityCollection(Of ConceptStructurePartCustomBankPropertyEntity))
                _conceptTypeCollectionViaConceptStructurePartCustomBankProperty = CType(info.GetValue("_conceptTypeCollectionViaConceptStructurePartCustomBankProperty", GetType(EntityCollection(Of ConceptTypeEntity))), EntityCollection(Of ConceptTypeEntity))
                Me.FixupDeserialization(FieldInfoProviderSingleton.GetInstance())
            End If

        End Sub



        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub SetRelatedEntityProperty(propertyName As String, entity As IEntityCore)
            Select Case propertyName
                Case "ConceptStructureCustomBankPropertyValueCollection"
                    Me.ConceptStructureCustomBankPropertyValueCollection.Add(CType(entity, ConceptStructureCustomBankPropertyValueEntity))
                Case "ConceptStructurePartCustomBankPropertyCollection"
                    Me.ConceptStructurePartCustomBankPropertyCollection.Add(CType(entity, ConceptStructurePartCustomBankPropertyEntity))
                Case "ConceptTypeCollectionViaConceptStructurePartCustomBankProperty"
                    Me.ConceptTypeCollectionViaConceptStructurePartCustomBankProperty.IsReadOnly = False
                    Me.ConceptTypeCollectionViaConceptStructurePartCustomBankProperty.Add(CType(entity, ConceptTypeEntity))
                    Me.ConceptTypeCollectionViaConceptStructurePartCustomBankProperty.IsReadOnly = True

                Case Else
                    MyBase.SetRelatedEntityProperty(propertyName, entity)
            End Select
        End Sub

        Protected Overrides Function GetRelationsForFieldOfType(fieldName As String) As RelationCollection
            Return ConceptStructureCustomBankPropertyEntity.GetRelationsForField(fieldName)
        End Function

        Friend Shared Shadows Function GetRelationsForField(fieldName As String) As RelationCollection
            Dim toReturn As New RelationCollection()
            Select Case fieldName
                Case "ConceptStructureCustomBankPropertyValueCollection"
                    toReturn.Add(ConceptStructureCustomBankPropertyEntity.Relations.ConceptStructureCustomBankPropertyValueEntityUsingCustomBankPropertyId)
                Case "ConceptStructurePartCustomBankPropertyCollection"
                    toReturn.Add(ConceptStructureCustomBankPropertyEntity.Relations.ConceptStructurePartCustomBankPropertyEntityUsingCustomBankPropertyId)
                Case "ConceptTypeCollectionViaConceptStructurePartCustomBankProperty"
                    toReturn.Add(ConceptStructureCustomBankPropertyEntity.Relations.ConceptStructurePartCustomBankPropertyEntityUsingCustomBankPropertyId, "ConceptStructureCustomBankPropertyEntity__", "ConceptStructurePartCustomBankProperty_", JoinHint.None)
                    toReturn.Add(ConceptStructurePartCustomBankPropertyEntity.Relations.ConceptTypeEntityUsingConceptTypeId, "ConceptStructurePartCustomBankProperty_", String.Empty, JoinHint.None)
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
                Case "ConceptStructureCustomBankPropertyValueCollection"
                    Me.ConceptStructureCustomBankPropertyValueCollection.Add(CType(relatedEntity, ConceptStructureCustomBankPropertyValueEntity))
                Case "ConceptStructurePartCustomBankPropertyCollection"
                    Me.ConceptStructurePartCustomBankPropertyCollection.Add(CType(relatedEntity, ConceptStructurePartCustomBankPropertyEntity))

                Case Else
                    MyBase.SetRelatedEntity(relatedEntity, fieldName)
            End Select
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub UnsetRelatedEntity(relatedEntity As IEntityCore, fieldName As String, signalRelatedEntityManyToOne As Boolean)
            Select Case fieldName
                Case "ConceptStructureCustomBankPropertyValueCollection"
                    Me.PerformRelatedEntityRemoval(Me.ConceptStructureCustomBankPropertyValueCollection, relatedEntity, signalRelatedEntityManyToOne)
                Case "ConceptStructurePartCustomBankPropertyCollection"
                    Me.PerformRelatedEntityRemoval(Me.ConceptStructurePartCustomBankPropertyCollection, relatedEntity, signalRelatedEntityManyToOne)
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
            toReturn.Add(Me.ConceptStructureCustomBankPropertyValueCollection)
            toReturn.Add(Me.ConceptStructurePartCustomBankPropertyCollection)
            toReturn.AddRange(MyBase.GetMemberEntityCollections())
            Return toReturn
        End Function

        Public Shadows Shared Function GetEntityTypeFilter() As IPredicateExpression
            Return InheritanceInfoProviderSingleton.GetInstance().GetEntityTypeFilter("ConceptStructureCustomBankPropertyEntity", False)
        End Function

        Public Shadows Shared Function GetEntityTypeFilter(negate As Boolean) As IPredicateExpression
            Return InheritanceInfoProviderSingleton.GetInstance().GetEntityTypeFilter("ConceptStructureCustomBankPropertyEntity", negate)
        End Function

        Protected Overrides Sub GetObjectData(info As SerializationInfo, context As StreamingContext)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                Dim value As IEntityCollection2 = Nothing
                Dim entityValue As IEntity2 = Nothing
                value = Nothing
                If (Not (_conceptStructureCustomBankPropertyValueCollection Is Nothing)) AndAlso (_conceptStructureCustomBankPropertyValueCollection.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _conceptStructureCustomBankPropertyValueCollection
                End If
                info.AddValue("_conceptStructureCustomBankPropertyValueCollection", value)
                value = Nothing
                If (Not (_conceptStructurePartCustomBankPropertyCollection Is Nothing)) AndAlso (_conceptStructurePartCustomBankPropertyCollection.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _conceptStructurePartCustomBankPropertyCollection
                End If
                info.AddValue("_conceptStructurePartCustomBankPropertyCollection", value)
                value = Nothing
                If (Not (_conceptTypeCollectionViaConceptStructurePartCustomBankProperty Is Nothing)) AndAlso (_conceptTypeCollectionViaConceptStructurePartCustomBankProperty.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _conceptTypeCollectionViaConceptStructurePartCustomBankProperty
                End If
                info.AddValue("_conceptTypeCollectionViaConceptStructurePartCustomBankProperty", value)
            End If

            MyBase.GetObjectData(info, context)
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Function CheckIfIsSubTypeOf(typeOfEntity As Integer) As Boolean
            Return InheritanceInfoProviderSingleton.GetInstance().CheckIfIsSubTypeOf("ConceptStructureCustomBankPropertyEntity", CType(typeOfEntity, Questify.Builder.Model.ContentModel.EntityType).ToString())
        End Function

        Protected Overrides Overloads Function GetAllRelations() As List(Of IEntityRelation)
            Return New ConceptStructureCustomBankPropertyRelations().GetAllRelations()
        End Function

        Public Overridable Function GetRelationInfoConceptStructureCustomBankPropertyValueCollection() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(ConceptStructureCustomBankPropertyValueFields.CustomBankPropertyId, Nothing, ComparisonOperator.Equal, Me.CustomBankPropertyId))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoConceptStructurePartCustomBankPropertyCollection() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(ConceptStructurePartCustomBankPropertyFields.CustomBankPropertyId, Nothing, ComparisonOperator.Equal, Me.CustomBankPropertyId))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoConceptTypeCollectionViaConceptStructurePartCustomBankProperty() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.Relations.AddRange(GetRelationsForFieldOfType("ConceptTypeCollectionViaConceptStructurePartCustomBankProperty"))
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(ConceptStructureCustomBankPropertyFields.CustomBankPropertyId, Nothing, ComparisonOperator.Equal, Me.CustomBankPropertyId, "ConceptStructureCustomBankPropertyEntity__"))
            Return bucket
        End Function

        Protected Overrides Function CreateEntityFactory() As IEntityFactory2
            Return EntityFactoryCache2.GetEntityFactory(GetType(ConceptStructureCustomBankPropertyEntityFactory))
        End Function
#If Not CF Then
        Protected Overrides Sub AddToMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2))
            MyBase.AddToMemberEntityCollectionsQueue(collectionsQueue)
            collectionsQueue.Enqueue(_conceptStructureCustomBankPropertyValueCollection)
            collectionsQueue.Enqueue(_conceptStructurePartCustomBankPropertyCollection)
            collectionsQueue.Enqueue(_conceptTypeCollectionViaConceptStructurePartCustomBankProperty)
        End Sub

        Protected Overrides Sub GetFromMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2))
            MyBase.GetFromMemberEntityCollectionsQueue(collectionsQueue)
            _conceptStructureCustomBankPropertyValueCollection = CType(collectionsQueue.Dequeue(), EntityCollection(Of ConceptStructureCustomBankPropertyValueEntity))
            _conceptStructurePartCustomBankPropertyCollection = CType(collectionsQueue.Dequeue(), EntityCollection(Of ConceptStructurePartCustomBankPropertyEntity))
            _conceptTypeCollectionViaConceptStructurePartCustomBankProperty = CType(collectionsQueue.Dequeue(), EntityCollection(Of ConceptTypeEntity))
        End Sub

        Protected Overrides Function HasPopulatedMemberEntityCollections() As Boolean
            If (Not _conceptStructureCustomBankPropertyValueCollection Is Nothing) Then
                Return True
            End If
            If (Not _conceptStructurePartCustomBankPropertyCollection Is Nothing) Then
                Return True
            End If
            If (Not _conceptTypeCollectionViaConceptStructurePartCustomBankProperty Is Nothing) Then
                Return True
            End If
            Return MyBase.HasPopulatedMemberEntityCollections()
        End Function

        Protected Overrides Overloads Sub CreateMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2), requiredQueue As Queue(Of Boolean))
            MyBase.CreateMemberEntityCollectionsQueue(collectionsQueue, requiredQueue)
            Dim toAdd As IEntityCollection2 = Nothing
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of ConceptStructureCustomBankPropertyValueEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ConceptStructureCustomBankPropertyValueEntityFactory)))
            Else
                toAdd = Nothing
            End If
            collectionsQueue.Enqueue(toAdd)
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of ConceptStructurePartCustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ConceptStructurePartCustomBankPropertyEntityFactory)))
            Else
                toAdd = Nothing
            End If
            collectionsQueue.Enqueue(toAdd)
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of ConceptTypeEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ConceptTypeEntityFactory)))
            Else
                toAdd = Nothing
            End If
            collectionsQueue.Enqueue(toAdd)
        End Sub
#End If
        Protected Overrides Overloads Function GetRelatedData() As Dictionary(Of String, Object)
            Dim toReturn As Dictionary(Of String, Object) = MyBase.GetRelatedData()
            toReturn.Add("ConceptStructureCustomBankPropertyValueCollection", _conceptStructureCustomBankPropertyValueCollection)
            toReturn.Add("ConceptStructurePartCustomBankPropertyCollection", _conceptStructurePartCustomBankPropertyCollection)
            toReturn.Add("ConceptTypeCollectionViaConceptStructurePartCustomBankProperty", _conceptTypeCollectionViaConceptStructurePartCustomBankProperty)
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

        Public Shadows Shared ReadOnly Property Relations() As ConceptStructureCustomBankPropertyRelations
            Get
                Return New ConceptStructureCustomBankPropertyRelations()
            End Get
        End Property

        Public Shadows Shared ReadOnly Property CustomProperties() As Dictionary(Of String, String)
            Get
                Return _customProperties
            End Get
        End Property


        Public Shared ReadOnly Property PrefetchPathConceptStructureCustomBankPropertyValueCollection() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(Of ConceptStructureCustomBankPropertyValueEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ConceptStructureCustomBankPropertyValueEntityFactory))), _
                    CType(GetRelationsForField("ConceptStructureCustomBankPropertyValueCollection")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.ConceptStructureCustomBankPropertyEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.ConceptStructureCustomBankPropertyValueEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "ConceptStructureCustomBankPropertyValueCollection", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathConceptStructurePartCustomBankPropertyCollection() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(Of ConceptStructurePartCustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ConceptStructurePartCustomBankPropertyEntityFactory))), _
                    CType(GetRelationsForField("ConceptStructurePartCustomBankPropertyCollection")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.ConceptStructureCustomBankPropertyEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.ConceptStructurePartCustomBankPropertyEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "ConceptStructurePartCustomBankPropertyCollection", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathConceptTypeCollectionViaConceptStructurePartCustomBankProperty() As IPrefetchPathElement2
            Get
                Dim intermediateRelation As IEntityRelation = ConceptStructureCustomBankPropertyEntity.Relations.ConceptStructurePartCustomBankPropertyEntityUsingCustomBankPropertyId
                intermediateRelation.SetAliases(String.Empty, "ConceptStructurePartCustomBankProperty_")
                Return New PrefetchPathElement2(New EntityCollection(Of ConceptTypeEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ConceptTypeEntityFactory))), _
                    intermediateRelation, CType(Questify.Builder.Model.ContentModel.EntityType.ConceptStructureCustomBankPropertyEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.ConceptTypeEntity, Integer), 0, Nothing, Nothing, GetRelationsForField("ConceptTypeCollectionViaConceptStructurePartCustomBankProperty"), Nothing, "ConceptTypeCollectionViaConceptStructurePartCustomBankProperty", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToMany)
            End Get
        End Property



        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property CustomPropertiesOfType() As Dictionary(Of String, String)
            Get
                Return ConceptStructureCustomBankPropertyEntity.CustomProperties
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
                Return ConceptStructureCustomBankPropertyEntity.FieldsCustomProperties
            End Get
        End Property


        <TypeContainedAttribute(GetType(ConceptStructureCustomBankPropertyValueEntity))> _
        Public Overridable ReadOnly Property [ConceptStructureCustomBankPropertyValueCollection]() As EntityCollection(Of ConceptStructureCustomBankPropertyValueEntity)
            Get
                If _conceptStructureCustomBankPropertyValueCollection Is Nothing Then
                    _conceptStructureCustomBankPropertyValueCollection = New EntityCollection(Of ConceptStructureCustomBankPropertyValueEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ConceptStructureCustomBankPropertyValueEntityFactory)))
                    _conceptStructureCustomBankPropertyValueCollection.ActiveContext = Me.ActiveContext
                    _conceptStructureCustomBankPropertyValueCollection.SetContainingEntityInfo(Me, "ConceptStructureCustomBankProperty")
                End If
                Return _conceptStructureCustomBankPropertyValueCollection
            End Get
        End Property

        <TypeContainedAttribute(GetType(ConceptStructurePartCustomBankPropertyEntity))> _
        Public Overridable ReadOnly Property [ConceptStructurePartCustomBankPropertyCollection]() As EntityCollection(Of ConceptStructurePartCustomBankPropertyEntity)
            Get
                If _conceptStructurePartCustomBankPropertyCollection Is Nothing Then
                    _conceptStructurePartCustomBankPropertyCollection = New EntityCollection(Of ConceptStructurePartCustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ConceptStructurePartCustomBankPropertyEntityFactory)))
                    _conceptStructurePartCustomBankPropertyCollection.ActiveContext = Me.ActiveContext
                    _conceptStructurePartCustomBankPropertyCollection.SetContainingEntityInfo(Me, "ConceptStructureCustomBankProperty")
                End If
                Return _conceptStructurePartCustomBankPropertyCollection
            End Get
        End Property

        <TypeContainedAttribute(GetType(ConceptTypeEntity))> _
        Public Overridable ReadOnly Property [ConceptTypeCollectionViaConceptStructurePartCustomBankProperty]() As EntityCollection(Of ConceptTypeEntity)
            Get
                If _conceptTypeCollectionViaConceptStructurePartCustomBankProperty Is Nothing Then
                    _conceptTypeCollectionViaConceptStructurePartCustomBankProperty = New EntityCollection(Of ConceptTypeEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ConceptTypeEntityFactory)))
                    _conceptTypeCollectionViaConceptStructurePartCustomBankProperty.ActiveContext = Me.ActiveContext
                    _conceptTypeCollectionViaConceptStructurePartCustomBankProperty.IsReadOnly = True
                    CType(_conceptTypeCollectionViaConceptStructurePartCustomBankProperty, IEntityCollectionCore).IsForMN = True
                End If
                Return _conceptTypeCollectionViaConceptStructurePartCustomBankProperty
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
                Return CInt(Questify.Builder.Model.ContentModel.EntityType.ConceptStructureCustomBankPropertyEntity)
            End Get
        End Property






    End Class
End Namespace
