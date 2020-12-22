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
    Public Class ConceptStructureCustomBankPropertyValueEntity
        Inherits CustomBankPropertyValueEntity



        Private WithEvents _conceptStructureCustomBankPropertySelectedPartCollection As EntityCollection(Of ConceptStructureCustomBankPropertySelectedPartEntity)
        Private WithEvents _conceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart As EntityCollection(Of ConceptStructurePartCustomBankPropertyEntity)
        Private WithEvents _conceptStructureCustomBankProperty As ConceptStructureCustomBankPropertyEntity



        Private Shared _customProperties As Dictionary(Of String, String)
        Private Shared _fieldsCustomProperties As Dictionary(Of String, Dictionary(Of String, String))

        Public Shadows NotInheritable Class MemberNames
            Private Sub New()
            End Sub
            Public Shared ReadOnly [ConceptStructureCustomBankProperty] As String = "ConceptStructureCustomBankProperty"
            Public Shared ReadOnly [CustomBankProperty] As String = "CustomBankProperty"
            Public Shared ReadOnly [Resource] As String = "Resource"
            Public Shared ReadOnly [ConceptStructureCustomBankPropertySelectedPartCollection] As String = "ConceptStructureCustomBankPropertySelectedPartCollection"
            Public Shared ReadOnly [ConceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart] As String = "ConceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart"
        End Class

        Shared Sub New()
            SetupCustomPropertyHashtables()
        End Sub

        Public Sub New()
            MyBase.New()
            InitClassEmpty()
            SetName("ConceptStructureCustomBankPropertyValueEntity")
        End Sub

        Public Sub New(fields As IEntityFields2)
            MyBase.New(fields)
            InitClassEmpty()
            SetName("ConceptStructureCustomBankPropertyValueEntity")
        End Sub

        Public Sub New(validator As IValidator)
            MyBase.New(validator)
            InitClassEmpty()
            SetName("ConceptStructureCustomBankPropertyValueEntity")
        End Sub

        Public Sub New(resourceId As System.Guid, customBankPropertyId As System.Guid)
            MyBase.New(resourceId, customBankPropertyId)
            InitClassEmpty()

            SetName("ConceptStructureCustomBankPropertyValueEntity")
        End Sub

        Public Sub New(resourceId As System.Guid, customBankPropertyId As System.Guid, validator As IValidator)
            MyBase.New(resourceId, customBankPropertyId, validator)
            InitClassEmpty()

            SetName("ConceptStructureCustomBankPropertyValueEntity")
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Sub New(info As SerializationInfo, context As StreamingContext)
            MyBase.New(info, context)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                _conceptStructureCustomBankPropertySelectedPartCollection = CType(info.GetValue("_conceptStructureCustomBankPropertySelectedPartCollection", GetType(EntityCollection(Of ConceptStructureCustomBankPropertySelectedPartEntity))), EntityCollection(Of ConceptStructureCustomBankPropertySelectedPartEntity))
                _conceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart = CType(info.GetValue("_conceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart", GetType(EntityCollection(Of ConceptStructurePartCustomBankPropertyEntity))), EntityCollection(Of ConceptStructurePartCustomBankPropertyEntity))
                _conceptStructureCustomBankProperty = CType(info.GetValue("_conceptStructureCustomBankProperty", GetType(ConceptStructureCustomBankPropertyEntity)), ConceptStructureCustomBankPropertyEntity)
                If Not _conceptStructureCustomBankProperty Is Nothing Then
                    AddHandler _conceptStructureCustomBankProperty.AfterSave, AddressOf OnEntityAfterSave
                End If
                Me.FixupDeserialization(FieldInfoProviderSingleton.GetInstance())
            End If

        End Sub



        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub SetRelatedEntityProperty(propertyName As String, entity As IEntityCore)
            Select Case propertyName
                Case "ConceptStructureCustomBankProperty"
                    Me.ConceptStructureCustomBankProperty = CType(entity, ConceptStructureCustomBankPropertyEntity)
                Case "ConceptStructureCustomBankPropertySelectedPartCollection"
                    Me.ConceptStructureCustomBankPropertySelectedPartCollection.Add(CType(entity, ConceptStructureCustomBankPropertySelectedPartEntity))
                Case "ConceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart"
                    Me.ConceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart.IsReadOnly = False
                    Me.ConceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart.Add(CType(entity, ConceptStructurePartCustomBankPropertyEntity))
                    Me.ConceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart.IsReadOnly = True

                Case Else
                    MyBase.SetRelatedEntityProperty(propertyName, entity)
            End Select
        End Sub

        Protected Overrides Function GetRelationsForFieldOfType(fieldName As String) As RelationCollection
            Return ConceptStructureCustomBankPropertyValueEntity.GetRelationsForField(fieldName)
        End Function

        Friend Shared Shadows Function GetRelationsForField(fieldName As String) As RelationCollection
            Dim toReturn As New RelationCollection()
            Select Case fieldName
                Case "ConceptStructureCustomBankProperty"
                    toReturn.Add(ConceptStructureCustomBankPropertyValueEntity.Relations.ConceptStructureCustomBankPropertyEntityUsingCustomBankPropertyId)
                Case "ConceptStructureCustomBankPropertySelectedPartCollection"
                    toReturn.Add(ConceptStructureCustomBankPropertyValueEntity.Relations.ConceptStructureCustomBankPropertySelectedPartEntityUsingCustomBankPropertyIdResourceId)
                Case "ConceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart"
                    toReturn.Add(ConceptStructureCustomBankPropertyValueEntity.Relations.ConceptStructureCustomBankPropertySelectedPartEntityUsingCustomBankPropertyIdResourceId, "ConceptStructureCustomBankPropertyValueEntity__", "ConceptStructureCustomBankPropertySelectedPart_", JoinHint.None)
                    toReturn.Add(ConceptStructureCustomBankPropertySelectedPartEntity.Relations.ConceptStructurePartCustomBankPropertyEntityUsingConceptStructurePartId, "ConceptStructureCustomBankPropertySelectedPart_", String.Empty, JoinHint.None)
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
                Case "ConceptStructureCustomBankProperty"
                    SetupSyncConceptStructureCustomBankProperty(relatedEntity)
                Case "ConceptStructureCustomBankPropertySelectedPartCollection"
                    Me.ConceptStructureCustomBankPropertySelectedPartCollection.Add(CType(relatedEntity, ConceptStructureCustomBankPropertySelectedPartEntity))

                Case Else
                    MyBase.SetRelatedEntity(relatedEntity, fieldName)
            End Select
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub UnsetRelatedEntity(relatedEntity As IEntityCore, fieldName As String, signalRelatedEntityManyToOne As Boolean)
            Select Case fieldName
                Case "ConceptStructureCustomBankProperty"
                    DesetupSyncConceptStructureCustomBankProperty(False, True)
                Case "ConceptStructureCustomBankPropertySelectedPartCollection"
                    Me.PerformRelatedEntityRemoval(Me.ConceptStructureCustomBankPropertySelectedPartCollection, relatedEntity, signalRelatedEntityManyToOne)
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
            If Not _conceptStructureCustomBankProperty Is Nothing Then
                toReturn.Add(_conceptStructureCustomBankProperty)
            End If
            toReturn.AddRange(MyBase.GetDependentRelatedEntities())
            Return toReturn
        End Function

        Protected Overrides Function GetMemberEntityCollections() As List(Of IEntityCollection2)
            Dim toReturn As New List(Of IEntityCollection2)()
            toReturn.Add(Me.ConceptStructureCustomBankPropertySelectedPartCollection)
            toReturn.AddRange(MyBase.GetMemberEntityCollections())
            Return toReturn
        End Function

        Public Shadows Shared Function GetEntityTypeFilter() As IPredicateExpression
            Return InheritanceInfoProviderSingleton.GetInstance().GetEntityTypeFilter("ConceptStructureCustomBankPropertyValueEntity", False)
        End Function

        Public Shadows Shared Function GetEntityTypeFilter(negate As Boolean) As IPredicateExpression
            Return InheritanceInfoProviderSingleton.GetInstance().GetEntityTypeFilter("ConceptStructureCustomBankPropertyValueEntity", negate)
        End Function

        Protected Overrides Sub GetObjectData(info As SerializationInfo, context As StreamingContext)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                Dim value As IEntityCollection2 = Nothing
                Dim entityValue As IEntity2 = Nothing
                value = Nothing
                If (Not (_conceptStructureCustomBankPropertySelectedPartCollection Is Nothing)) AndAlso (_conceptStructureCustomBankPropertySelectedPartCollection.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _conceptStructureCustomBankPropertySelectedPartCollection
                End If
                info.AddValue("_conceptStructureCustomBankPropertySelectedPartCollection", value)
                value = Nothing
                If (Not (_conceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart Is Nothing)) AndAlso (_conceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _conceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart
                End If
                info.AddValue("_conceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart", value)
                entityValue = Nothing
                If Not Me.MarkedForDeletion Then
                    entityValue = _conceptStructureCustomBankProperty
                End If
                info.AddValue("_conceptStructureCustomBankProperty", entityValue)
            End If

            MyBase.GetObjectData(info, context)
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Function CheckIfIsSubTypeOf(typeOfEntity As Integer) As Boolean
            Return InheritanceInfoProviderSingleton.GetInstance().CheckIfIsSubTypeOf("ConceptStructureCustomBankPropertyValueEntity", CType(typeOfEntity, Questify.Builder.Model.ContentModel.EntityType).ToString())
        End Function

        Protected Overrides Overloads Function GetAllRelations() As List(Of IEntityRelation)
            Return New ConceptStructureCustomBankPropertyValueRelations().GetAllRelations()
        End Function

        Public Overridable Function GetRelationInfoConceptStructureCustomBankPropertySelectedPartCollection() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(ConceptStructureCustomBankPropertySelectedPartFields.CustomBankPropertyId, Nothing, ComparisonOperator.Equal, Me.CustomBankPropertyId))
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(ConceptStructureCustomBankPropertySelectedPartFields.ResourceId, Nothing, ComparisonOperator.Equal, Me.ResourceId))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoConceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.Relations.AddRange(GetRelationsForFieldOfType("ConceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart"))
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(ConceptStructureCustomBankPropertyValueFields.ResourceId, Nothing, ComparisonOperator.Equal, Me.ResourceId, "ConceptStructureCustomBankPropertyValueEntity__"))
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(ConceptStructureCustomBankPropertyValueFields.CustomBankPropertyId, Nothing, ComparisonOperator.Equal, Me.CustomBankPropertyId, "ConceptStructureCustomBankPropertyValueEntity__"))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoConceptStructureCustomBankProperty() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(ConceptStructureCustomBankPropertyFields.CustomBankPropertyId, Nothing, ComparisonOperator.Equal, Me.CustomBankPropertyId))
            Return bucket
        End Function

        Protected Overrides Function CreateEntityFactory() As IEntityFactory2
            Return EntityFactoryCache2.GetEntityFactory(GetType(ConceptStructureCustomBankPropertyValueEntityFactory))
        End Function
#If Not CF Then
        Protected Overrides Sub AddToMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2))
            MyBase.AddToMemberEntityCollectionsQueue(collectionsQueue)
            collectionsQueue.Enqueue(_conceptStructureCustomBankPropertySelectedPartCollection)
            collectionsQueue.Enqueue(_conceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart)
        End Sub

        Protected Overrides Sub GetFromMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2))
            MyBase.GetFromMemberEntityCollectionsQueue(collectionsQueue)
            _conceptStructureCustomBankPropertySelectedPartCollection = CType(collectionsQueue.Dequeue(), EntityCollection(Of ConceptStructureCustomBankPropertySelectedPartEntity))
            _conceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart = CType(collectionsQueue.Dequeue(), EntityCollection(Of ConceptStructurePartCustomBankPropertyEntity))
        End Sub

        Protected Overrides Function HasPopulatedMemberEntityCollections() As Boolean
            If (Not _conceptStructureCustomBankPropertySelectedPartCollection Is Nothing) Then
                Return True
            End If
            If (Not _conceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart Is Nothing) Then
                Return True
            End If
            Return MyBase.HasPopulatedMemberEntityCollections()
        End Function

        Protected Overrides Overloads Sub CreateMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2), requiredQueue As Queue(Of Boolean))
            MyBase.CreateMemberEntityCollectionsQueue(collectionsQueue, requiredQueue)
            Dim toAdd As IEntityCollection2 = Nothing
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of ConceptStructureCustomBankPropertySelectedPartEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ConceptStructureCustomBankPropertySelectedPartEntityFactory)))
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
        End Sub
#End If
        Protected Overrides Overloads Function GetRelatedData() As Dictionary(Of String, Object)
            Dim toReturn As Dictionary(Of String, Object) = MyBase.GetRelatedData()
            toReturn.Add("ConceptStructureCustomBankProperty", _conceptStructureCustomBankProperty)
            toReturn.Add("ConceptStructureCustomBankPropertySelectedPartCollection", _conceptStructureCustomBankPropertySelectedPartCollection)
            toReturn.Add("ConceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart", _conceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart)
            Return toReturn
        End Function

        Private Sub InitClassMembers()


        End Sub

        Private Shared Sub SetupCustomPropertyHashtables()
            _customProperties = New Dictionary(Of String, String)()
            _fieldsCustomProperties = New Dictionary(Of String, Dictionary(Of String, String))()
        End Sub


        Private Sub DesetupSyncConceptStructureCustomBankProperty(signalRelatedEntity As Boolean, resetFKFields As Boolean)
            Me.PerformDesetupSyncRelatedEntity(_conceptStructureCustomBankProperty, AddressOf OnConceptStructureCustomBankPropertyPropertyChanged, "ConceptStructureCustomBankProperty", Questify.Builder.Model.ContentModel.RelationClasses.StaticConceptStructureCustomBankPropertyValueRelations.ConceptStructureCustomBankPropertyEntityUsingCustomBankPropertyIdStatic, True, signalRelatedEntity, "ConceptStructureCustomBankPropertyValueCollection", resetFKFields, New Integer() {CInt(ConceptStructureCustomBankPropertyValueFieldIndex.CustomBankPropertyId)})
            _conceptStructureCustomBankProperty = Nothing
        End Sub

        Private Sub SetupSyncConceptStructureCustomBankProperty(relatedEntity As IEntityCore)
            If Not _conceptStructureCustomBankProperty Is relatedEntity Then
                DesetupSyncConceptStructureCustomBankProperty(True, True)
                _conceptStructureCustomBankProperty = CType(relatedEntity, ConceptStructureCustomBankPropertyEntity)
                Me.PerformSetupSyncRelatedEntity(_conceptStructureCustomBankProperty, AddressOf OnConceptStructureCustomBankPropertyPropertyChanged, "ConceptStructureCustomBankProperty", Questify.Builder.Model.ContentModel.RelationClasses.StaticConceptStructureCustomBankPropertyValueRelations.ConceptStructureCustomBankPropertyEntityUsingCustomBankPropertyIdStatic, True, New String() {})
            End If
        End Sub

        Private Sub OnConceptStructureCustomBankPropertyPropertyChanged(sender As Object, e As PropertyChangedEventArgs)
            Select Case e.PropertyName

                Case Else
            End Select
        End Sub



        Private Sub InitClassEmpty()
            InitClassMembers()




        End Sub

        Public Shadows Shared ReadOnly Property Relations() As ConceptStructureCustomBankPropertyValueRelations
            Get
                Return New ConceptStructureCustomBankPropertyValueRelations()
            End Get
        End Property

        Public Shadows Shared ReadOnly Property CustomProperties() As Dictionary(Of String, String)
            Get
                Return _customProperties
            End Get
        End Property


        Public Shared ReadOnly Property PrefetchPathConceptStructureCustomBankPropertySelectedPartCollection() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(Of ConceptStructureCustomBankPropertySelectedPartEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ConceptStructureCustomBankPropertySelectedPartEntityFactory))), _
                    CType(GetRelationsForField("ConceptStructureCustomBankPropertySelectedPartCollection")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.ConceptStructureCustomBankPropertyValueEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.ConceptStructureCustomBankPropertySelectedPartEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "ConceptStructureCustomBankPropertySelectedPartCollection", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathConceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart() As IPrefetchPathElement2
            Get
                Dim intermediateRelation As IEntityRelation = ConceptStructureCustomBankPropertyValueEntity.Relations.ConceptStructureCustomBankPropertySelectedPartEntityUsingCustomBankPropertyIdResourceId
                intermediateRelation.SetAliases(String.Empty, "ConceptStructureCustomBankPropertySelectedPart_")
                Return New PrefetchPathElement2(New EntityCollection(Of ConceptStructurePartCustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ConceptStructurePartCustomBankPropertyEntityFactory))), _
                    intermediateRelation, CType(Questify.Builder.Model.ContentModel.EntityType.ConceptStructureCustomBankPropertyValueEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.ConceptStructurePartCustomBankPropertyEntity, Integer), 0, Nothing, Nothing, GetRelationsForField("ConceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart"), Nothing, "ConceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathConceptStructureCustomBankProperty() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(EntityFactoryCache2.GetEntityFactory(GetType(ConceptStructureCustomBankPropertyEntityFactory))), _
                    CType(GetRelationsForField("ConceptStructureCustomBankProperty")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.ConceptStructureCustomBankPropertyValueEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.ConceptStructureCustomBankPropertyEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "ConceptStructureCustomBankProperty", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne)
            End Get
        End Property


        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property CustomPropertiesOfType() As Dictionary(Of String, String)
            Get
                Return ConceptStructureCustomBankPropertyValueEntity.CustomProperties
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
                Return ConceptStructureCustomBankPropertyValueEntity.FieldsCustomProperties
            End Get
        End Property


        <TypeContainedAttribute(GetType(ConceptStructureCustomBankPropertySelectedPartEntity))> _
        Public Overridable ReadOnly Property [ConceptStructureCustomBankPropertySelectedPartCollection]() As EntityCollection(Of ConceptStructureCustomBankPropertySelectedPartEntity)
            Get
                If _conceptStructureCustomBankPropertySelectedPartCollection Is Nothing Then
                    _conceptStructureCustomBankPropertySelectedPartCollection = New EntityCollection(Of ConceptStructureCustomBankPropertySelectedPartEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ConceptStructureCustomBankPropertySelectedPartEntityFactory)))
                    _conceptStructureCustomBankPropertySelectedPartCollection.ActiveContext = Me.ActiveContext
                    _conceptStructureCustomBankPropertySelectedPartCollection.SetContainingEntityInfo(Me, "ConceptStructureCustomBankPropertyValue")
                End If
                Return _conceptStructureCustomBankPropertySelectedPartCollection
            End Get
        End Property

        <TypeContainedAttribute(GetType(ConceptStructurePartCustomBankPropertyEntity))> _
        Public Overridable ReadOnly Property [ConceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart]() As EntityCollection(Of ConceptStructurePartCustomBankPropertyEntity)
            Get
                If _conceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart Is Nothing Then
                    _conceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart = New EntityCollection(Of ConceptStructurePartCustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ConceptStructurePartCustomBankPropertyEntityFactory)))
                    _conceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart.ActiveContext = Me.ActiveContext
                    _conceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart.IsReadOnly = True
                    CType(_conceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart, IEntityCollectionCore).IsForMN = True
                End If
                Return _conceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart
            End Get
        End Property

        <Browsable(True)> _
        Public Overridable Property [ConceptStructureCustomBankProperty]() As ConceptStructureCustomBankPropertyEntity
            Get
                Return _conceptStructureCustomBankProperty
            End Get
            Set
                If MyBase.IsDeserializing Then
                    SetupSyncConceptStructureCustomBankProperty(value)
                Else
                    SetSingleRelatedEntityNavigator(value, "ConceptStructureCustomBankPropertyValueCollection", "ConceptStructureCustomBankProperty", _conceptStructureCustomBankProperty, True)
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
                Return CInt(Questify.Builder.Model.ContentModel.EntityType.ConceptStructureCustomBankPropertyValueEntity)
            End Get
        End Property






    End Class
End Namespace
