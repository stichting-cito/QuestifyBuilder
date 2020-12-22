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
    Public Class ConceptStructurePartCustomBankPropertyEntity
        Inherits CommonEntityBase



        Private WithEvents _referencedConceptStructurePartCustomBankPropertyCollection As EntityCollection(Of ChildConceptStructurePartCustomBankPropertyEntity)
        Private WithEvents _childConceptStructurePartCustomBankPropertyCollection As EntityCollection(Of ChildConceptStructurePartCustomBankPropertyEntity)
        Private WithEvents _conceptStructureCustomBankPropertySelectedPartCollection As EntityCollection(Of ConceptStructureCustomBankPropertySelectedPartEntity)
        Private WithEvents _conceptStructureCustomBankProperty As ConceptStructureCustomBankPropertyEntity
        Private WithEvents _conceptType As ConceptTypeEntity



        Private Shared _customProperties As Dictionary(Of String, String)
        Private Shared _fieldsCustomProperties As Dictionary(Of String, Dictionary(Of String, String))

        Public NotInheritable Class MemberNames
            Private Sub New()
            End Sub
            Public Shared ReadOnly [ConceptStructureCustomBankProperty] As String = "ConceptStructureCustomBankProperty"
            Public Shared ReadOnly [ConceptType] As String = "ConceptType"
            Public Shared ReadOnly [ReferencedConceptStructurePartCustomBankPropertyCollection] As String = "ReferencedConceptStructurePartCustomBankPropertyCollection"
            Public Shared ReadOnly [ChildConceptStructurePartCustomBankPropertyCollection] As String = "ChildConceptStructurePartCustomBankPropertyCollection"
            Public Shared ReadOnly [ConceptStructureCustomBankPropertySelectedPartCollection] As String = "ConceptStructureCustomBankPropertySelectedPartCollection"
        End Class

        Shared Sub New()
            SetupCustomPropertyHashtables()
        End Sub

        Public Sub New()
            MyBase.New("ConceptStructurePartCustomBankPropertyEntity")
            InitClassEmpty(Nothing, Nothing)
        End Sub

        Public Sub New(fields As IEntityFields2)
            MyBase.New("ConceptStructurePartCustomBankPropertyEntity")
            InitClassEmpty(Nothing, fields)
        End Sub

        Public Sub New(validator As IValidator)
            MyBase.New("ConceptStructurePartCustomBankPropertyEntity")
            InitClassEmpty(validator, Nothing)
        End Sub

        Public Sub New(conceptStructurePartCustomBankPropertyId As System.Guid)
            MyBase.New("ConceptStructurePartCustomBankPropertyEntity")
            InitClassEmpty(Nothing, Nothing)
            Me.ConceptStructurePartCustomBankPropertyId = conceptStructurePartCustomBankPropertyId
        End Sub

        Public Sub New(conceptStructurePartCustomBankPropertyId As System.Guid, validator As IValidator)
            MyBase.New("ConceptStructurePartCustomBankPropertyEntity")
            InitClassEmpty(validator, Nothing)
            Me.ConceptStructurePartCustomBankPropertyId = conceptStructurePartCustomBankPropertyId
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Sub New(info As SerializationInfo, context As StreamingContext)
            MyBase.New(info, context)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                _referencedConceptStructurePartCustomBankPropertyCollection = CType(info.GetValue("_referencedConceptStructurePartCustomBankPropertyCollection", GetType(EntityCollection(Of ChildConceptStructurePartCustomBankPropertyEntity))), EntityCollection(Of ChildConceptStructurePartCustomBankPropertyEntity))
                _childConceptStructurePartCustomBankPropertyCollection = CType(info.GetValue("_childConceptStructurePartCustomBankPropertyCollection", GetType(EntityCollection(Of ChildConceptStructurePartCustomBankPropertyEntity))), EntityCollection(Of ChildConceptStructurePartCustomBankPropertyEntity))
                _conceptStructureCustomBankPropertySelectedPartCollection = CType(info.GetValue("_conceptStructureCustomBankPropertySelectedPartCollection", GetType(EntityCollection(Of ConceptStructureCustomBankPropertySelectedPartEntity))), EntityCollection(Of ConceptStructureCustomBankPropertySelectedPartEntity))
                _conceptStructureCustomBankProperty = CType(info.GetValue("_conceptStructureCustomBankProperty", GetType(ConceptStructureCustomBankPropertyEntity)), ConceptStructureCustomBankPropertyEntity)
                If Not _conceptStructureCustomBankProperty Is Nothing Then
                    AddHandler _conceptStructureCustomBankProperty.AfterSave, AddressOf OnEntityAfterSave
                End If
                _conceptType = CType(info.GetValue("_conceptType", GetType(ConceptTypeEntity)), ConceptTypeEntity)
                If Not _conceptType Is Nothing Then
                    AddHandler _conceptType.AfterSave, AddressOf OnEntityAfterSave
                End If
                Me.FixupDeserialization(FieldInfoProviderSingleton.GetInstance())
            End If

        End Sub


        Protected Overrides Sub PerformDesyncSetupFKFieldChange(fieldIndex As Integer)
            Select Case CType(fieldIndex, ConceptStructurePartCustomBankPropertyFieldIndex)

                Case ConceptStructurePartCustomBankPropertyFieldIndex.CustomBankPropertyId
                    DesetupSyncConceptStructureCustomBankProperty(True, False)



                Case ConceptStructurePartCustomBankPropertyFieldIndex.ConceptTypeId
                    DesetupSyncConceptType(True, False)
                Case Else
                    MyBase.PerformDesyncSetupFKFieldChange(fieldIndex)
            End Select
        End Sub


        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub SetRelatedEntityProperty(propertyName As String, entity As IEntityCore)
            Select Case propertyName
                Case "ConceptStructureCustomBankProperty"
                    Me.ConceptStructureCustomBankProperty = CType(entity, ConceptStructureCustomBankPropertyEntity)
                Case "ConceptType"
                    Me.ConceptType = CType(entity, ConceptTypeEntity)
                Case "ReferencedConceptStructurePartCustomBankPropertyCollection"
                    Me.ReferencedConceptStructurePartCustomBankPropertyCollection.Add(CType(entity, ChildConceptStructurePartCustomBankPropertyEntity))
                Case "ChildConceptStructurePartCustomBankPropertyCollection"
                    Me.ChildConceptStructurePartCustomBankPropertyCollection.Add(CType(entity, ChildConceptStructurePartCustomBankPropertyEntity))
                Case "ConceptStructureCustomBankPropertySelectedPartCollection"
                    Me.ConceptStructureCustomBankPropertySelectedPartCollection.Add(CType(entity, ConceptStructureCustomBankPropertySelectedPartEntity))

                Case Else
                    Me.OnSetRelatedEntityProperty(propertyName, entity)
            End Select
        End Sub

        Protected Overrides Function GetRelationsForFieldOfType(fieldName As String) As RelationCollection
            Return ConceptStructurePartCustomBankPropertyEntity.GetRelationsForField(fieldName)
        End Function

        Friend Shared Function GetRelationsForField(fieldName As String) As RelationCollection
            Dim toReturn As New RelationCollection()
            Select Case fieldName
                Case "ConceptStructureCustomBankProperty"
                    toReturn.Add(ConceptStructurePartCustomBankPropertyEntity.Relations.ConceptStructureCustomBankPropertyEntityUsingCustomBankPropertyId)
                Case "ConceptType"
                    toReturn.Add(ConceptStructurePartCustomBankPropertyEntity.Relations.ConceptTypeEntityUsingConceptTypeId)
                Case "ReferencedConceptStructurePartCustomBankPropertyCollection"
                    toReturn.Add(ConceptStructurePartCustomBankPropertyEntity.Relations.ChildConceptStructurePartCustomBankPropertyEntityUsingChildConceptStructurePartCustomBankPropertyId)
                Case "ChildConceptStructurePartCustomBankPropertyCollection"
                    toReturn.Add(ConceptStructurePartCustomBankPropertyEntity.Relations.ChildConceptStructurePartCustomBankPropertyEntityUsingConceptStructurePartCustomBankPropertyId)
                Case "ConceptStructureCustomBankPropertySelectedPartCollection"
                    toReturn.Add(ConceptStructurePartCustomBankPropertyEntity.Relations.ConceptStructureCustomBankPropertySelectedPartEntityUsingConceptStructurePartId)
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
                Case "ConceptStructureCustomBankProperty"
                    SetupSyncConceptStructureCustomBankProperty(relatedEntity)
                Case "ConceptType"
                    SetupSyncConceptType(relatedEntity)
                Case "ReferencedConceptStructurePartCustomBankPropertyCollection"
                    Me.ReferencedConceptStructurePartCustomBankPropertyCollection.Add(CType(relatedEntity, ChildConceptStructurePartCustomBankPropertyEntity))
                Case "ChildConceptStructurePartCustomBankPropertyCollection"

                Case "ConceptStructureCustomBankPropertySelectedPartCollection"
                    Me.ConceptStructureCustomBankPropertySelectedPartCollection.Add(CType(relatedEntity, ConceptStructureCustomBankPropertySelectedPartEntity))

                Case Else
            End Select
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub UnsetRelatedEntity(relatedEntity As IEntityCore, fieldName As String, signalRelatedEntityManyToOne As Boolean)
            Select Case fieldName
                Case "ConceptStructureCustomBankProperty"
                    DesetupSyncConceptStructureCustomBankProperty(False, True)
                Case "ConceptType"
                    DesetupSyncConceptType(False, True)
                Case "ReferencedConceptStructurePartCustomBankPropertyCollection"
                    Me.PerformRelatedEntityRemoval(Me.ReferencedConceptStructurePartCustomBankPropertyCollection, relatedEntity, signalRelatedEntityManyToOne)
                Case "ChildConceptStructurePartCustomBankPropertyCollection"

                Case "ConceptStructureCustomBankPropertySelectedPartCollection"
                    Me.PerformRelatedEntityRemoval(Me.ConceptStructureCustomBankPropertySelectedPartCollection, relatedEntity, signalRelatedEntityManyToOne)
                Case Else
            End Select
        End Sub

        Protected Overrides Function GetDependingRelatedEntities() As List(Of IEntity2)
            Dim toReturn As New List(Of IEntity2)()
            Return toReturn
        End Function

        Protected Overrides Function GetDependentRelatedEntities() As List(Of IEntity2)
            Dim toReturn As New List(Of IEntity2)()
            If Not _conceptStructureCustomBankProperty Is Nothing Then
                toReturn.Add(_conceptStructureCustomBankProperty)
            End If
            If Not _conceptType Is Nothing Then
                toReturn.Add(_conceptType)
            End If
            Return toReturn
        End Function

        Protected Overrides Function GetMemberEntityCollections() As List(Of IEntityCollection2)
            Dim toReturn As New List(Of IEntityCollection2)()
            toReturn.Add(Me.ReferencedConceptStructurePartCustomBankPropertyCollection)
            toReturn.Add(Me.ChildConceptStructurePartCustomBankPropertyCollection)
            toReturn.Add(Me.ConceptStructureCustomBankPropertySelectedPartCollection)
            Return toReturn
        End Function


        Protected Overrides Sub GetObjectData(info As SerializationInfo, context As StreamingContext)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                Dim value As IEntityCollection2 = Nothing
                Dim entityValue As IEntity2 = Nothing
                value = Nothing
                If (Not (_referencedConceptStructurePartCustomBankPropertyCollection Is Nothing)) AndAlso (_referencedConceptStructurePartCustomBankPropertyCollection.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _referencedConceptStructurePartCustomBankPropertyCollection
                End If
                info.AddValue("_referencedConceptStructurePartCustomBankPropertyCollection", value)
                value = Nothing
                If (Not (_childConceptStructurePartCustomBankPropertyCollection Is Nothing)) AndAlso (_childConceptStructurePartCustomBankPropertyCollection.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _childConceptStructurePartCustomBankPropertyCollection
                End If
                info.AddValue("_childConceptStructurePartCustomBankPropertyCollection", value)
                value = Nothing
                If (Not (_conceptStructureCustomBankPropertySelectedPartCollection Is Nothing)) AndAlso (_conceptStructureCustomBankPropertySelectedPartCollection.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _conceptStructureCustomBankPropertySelectedPartCollection
                End If
                info.AddValue("_conceptStructureCustomBankPropertySelectedPartCollection", value)
                entityValue = Nothing
                If Not Me.MarkedForDeletion Then
                    entityValue = _conceptStructureCustomBankProperty
                End If
                info.AddValue("_conceptStructureCustomBankProperty", entityValue)
                entityValue = Nothing
                If Not Me.MarkedForDeletion Then
                    entityValue = _conceptType
                End If
                info.AddValue("_conceptType", entityValue)
            End If

            MyBase.GetObjectData(info, context)
        End Sub


        Protected Overrides Overloads Function GetAllRelations() As List(Of IEntityRelation)
            Return New ConceptStructurePartCustomBankPropertyRelations().GetAllRelations()
        End Function

        Public Overridable Function GetRelationInfoReferencedConceptStructurePartCustomBankPropertyCollection() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(ChildConceptStructurePartCustomBankPropertyFields.ChildConceptStructurePartCustomBankPropertyId, Nothing, ComparisonOperator.Equal, Me.ConceptStructurePartCustomBankPropertyId))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoChildConceptStructurePartCustomBankPropertyCollection() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(ChildConceptStructurePartCustomBankPropertyFields.ConceptStructurePartCustomBankPropertyId, Nothing, ComparisonOperator.Equal, Me.ConceptStructurePartCustomBankPropertyId))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoConceptStructureCustomBankPropertySelectedPartCollection() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(ConceptStructureCustomBankPropertySelectedPartFields.ConceptStructurePartId, Nothing, ComparisonOperator.Equal, Me.ConceptStructurePartCustomBankPropertyId))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoConceptStructureCustomBankProperty() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(ConceptStructureCustomBankPropertyFields.CustomBankPropertyId, Nothing, ComparisonOperator.Equal, Me.CustomBankPropertyId))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoConceptType() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(ConceptTypeFields.ConceptTypeId, Nothing, ComparisonOperator.Equal, Me.ConceptTypeId))
            Return bucket
        End Function

        Protected Overrides Function CreateEntityFactory() As IEntityFactory2
            Return EntityFactoryCache2.GetEntityFactory(GetType(ConceptStructurePartCustomBankPropertyEntityFactory))
        End Function
#If Not CF Then
        Protected Overrides Sub AddToMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2))
            MyBase.AddToMemberEntityCollectionsQueue(collectionsQueue)
            collectionsQueue.Enqueue(_referencedConceptStructurePartCustomBankPropertyCollection)
            collectionsQueue.Enqueue(_childConceptStructurePartCustomBankPropertyCollection)
            collectionsQueue.Enqueue(_conceptStructureCustomBankPropertySelectedPartCollection)
        End Sub

        Protected Overrides Sub GetFromMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2))
            MyBase.GetFromMemberEntityCollectionsQueue(collectionsQueue)
            _referencedConceptStructurePartCustomBankPropertyCollection = CType(collectionsQueue.Dequeue(), EntityCollection(Of ChildConceptStructurePartCustomBankPropertyEntity))
            _childConceptStructurePartCustomBankPropertyCollection = CType(collectionsQueue.Dequeue(), EntityCollection(Of ChildConceptStructurePartCustomBankPropertyEntity))
            _conceptStructureCustomBankPropertySelectedPartCollection = CType(collectionsQueue.Dequeue(), EntityCollection(Of ConceptStructureCustomBankPropertySelectedPartEntity))
        End Sub

        Protected Overrides Function HasPopulatedMemberEntityCollections() As Boolean
            If (Not _referencedConceptStructurePartCustomBankPropertyCollection Is Nothing) Then
                Return True
            End If
            If (Not _childConceptStructurePartCustomBankPropertyCollection Is Nothing) Then
                Return True
            End If
            If (Not _conceptStructureCustomBankPropertySelectedPartCollection Is Nothing) Then
                Return True
            End If
            Return MyBase.HasPopulatedMemberEntityCollections()
        End Function

        Protected Overrides Overloads Sub CreateMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2), requiredQueue As Queue(Of Boolean))
            MyBase.CreateMemberEntityCollectionsQueue(collectionsQueue, requiredQueue)
            Dim toAdd As IEntityCollection2 = Nothing
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of ChildConceptStructurePartCustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ChildConceptStructurePartCustomBankPropertyEntityFactory)))
            Else
                toAdd = Nothing
            End If
            collectionsQueue.Enqueue(toAdd)
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of ChildConceptStructurePartCustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ChildConceptStructurePartCustomBankPropertyEntityFactory)))
            Else
                toAdd = Nothing
            End If
            collectionsQueue.Enqueue(toAdd)
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of ConceptStructureCustomBankPropertySelectedPartEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ConceptStructureCustomBankPropertySelectedPartEntityFactory)))
            Else
                toAdd = Nothing
            End If
            collectionsQueue.Enqueue(toAdd)
        End Sub
#End If
        Protected Overrides Overloads Function GetRelatedData() As Dictionary(Of String, Object)
            Dim toReturn As New Dictionary(Of String, Object)()
            toReturn.Add("ConceptStructureCustomBankProperty", _conceptStructureCustomBankProperty)
            toReturn.Add("ConceptType", _conceptType)
            toReturn.Add("ReferencedConceptStructurePartCustomBankPropertyCollection", _referencedConceptStructurePartCustomBankPropertyCollection)
            toReturn.Add("ChildConceptStructurePartCustomBankPropertyCollection", _childConceptStructurePartCustomBankPropertyCollection)
            toReturn.Add("ConceptStructureCustomBankPropertySelectedPartCollection", _conceptStructureCustomBankPropertySelectedPartCollection)
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
            _fieldsCustomProperties.Add("ConceptStructurePartCustomBankPropertyId", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("CustomBankPropertyId", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("Name", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("Title", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("Code", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("ConceptTypeId", fieldHashtable)
        End Sub


        Private Sub DesetupSyncConceptStructureCustomBankProperty(signalRelatedEntity As Boolean, resetFKFields As Boolean)
            Me.PerformDesetupSyncRelatedEntity(_conceptStructureCustomBankProperty, AddressOf OnConceptStructureCustomBankPropertyPropertyChanged, "ConceptStructureCustomBankProperty", Questify.Builder.Model.ContentModel.RelationClasses.StaticConceptStructurePartCustomBankPropertyRelations.ConceptStructureCustomBankPropertyEntityUsingCustomBankPropertyIdStatic, True, signalRelatedEntity, "ConceptStructurePartCustomBankPropertyCollection", resetFKFields, New Integer() {CInt(ConceptStructurePartCustomBankPropertyFieldIndex.CustomBankPropertyId)})
            _conceptStructureCustomBankProperty = Nothing
        End Sub

        Private Sub SetupSyncConceptStructureCustomBankProperty(relatedEntity As IEntityCore)
            If Not _conceptStructureCustomBankProperty Is relatedEntity Then
                DesetupSyncConceptStructureCustomBankProperty(True, True)
                _conceptStructureCustomBankProperty = CType(relatedEntity, ConceptStructureCustomBankPropertyEntity)
                Me.PerformSetupSyncRelatedEntity(_conceptStructureCustomBankProperty, AddressOf OnConceptStructureCustomBankPropertyPropertyChanged, "ConceptStructureCustomBankProperty", Questify.Builder.Model.ContentModel.RelationClasses.StaticConceptStructurePartCustomBankPropertyRelations.ConceptStructureCustomBankPropertyEntityUsingCustomBankPropertyIdStatic, True, New String() {})
            End If
        End Sub

        Private Sub OnConceptStructureCustomBankPropertyPropertyChanged(sender As Object, e As PropertyChangedEventArgs)
            Select Case e.PropertyName

                Case Else
            End Select
        End Sub

        Private Sub DesetupSyncConceptType(signalRelatedEntity As Boolean, resetFKFields As Boolean)
            Me.PerformDesetupSyncRelatedEntity(_conceptType, AddressOf OnConceptTypePropertyChanged, "ConceptType", Questify.Builder.Model.ContentModel.RelationClasses.StaticConceptStructurePartCustomBankPropertyRelations.ConceptTypeEntityUsingConceptTypeIdStatic, True, signalRelatedEntity, "ConceptStructurePartCustomBankPropertyCollection", resetFKFields, New Integer() {CInt(ConceptStructurePartCustomBankPropertyFieldIndex.ConceptTypeId)})
            _conceptType = Nothing
        End Sub

        Private Sub SetupSyncConceptType(relatedEntity As IEntityCore)
            If Not _conceptType Is relatedEntity Then
                DesetupSyncConceptType(True, True)
                _conceptType = CType(relatedEntity, ConceptTypeEntity)
                Me.PerformSetupSyncRelatedEntity(_conceptType, AddressOf OnConceptTypePropertyChanged, "ConceptType", Questify.Builder.Model.ContentModel.RelationClasses.StaticConceptStructurePartCustomBankPropertyRelations.ConceptTypeEntityUsingConceptTypeIdStatic, True, New String() {})
            End If
        End Sub

        Private Sub OnConceptTypePropertyChanged(sender As Object, e As PropertyChangedEventArgs)
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

        Public Shared ReadOnly Property Relations() As ConceptStructurePartCustomBankPropertyRelations
            Get
                Return New ConceptStructurePartCustomBankPropertyRelations()
            End Get
        End Property

        Public Shared ReadOnly Property CustomProperties() As Dictionary(Of String, String)
            Get
                Return _customProperties
            End Get
        End Property


        Public Shared ReadOnly Property PrefetchPathReferencedConceptStructurePartCustomBankPropertyCollection() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(Of ChildConceptStructurePartCustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ChildConceptStructurePartCustomBankPropertyEntityFactory))), _
                    CType(GetRelationsForField("ReferencedConceptStructurePartCustomBankPropertyCollection")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.ConceptStructurePartCustomBankPropertyEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.ChildConceptStructurePartCustomBankPropertyEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "ReferencedConceptStructurePartCustomBankPropertyCollection", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathChildConceptStructurePartCustomBankPropertyCollection() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(Of ChildConceptStructurePartCustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ChildConceptStructurePartCustomBankPropertyEntityFactory))), _
                    CType(GetRelationsForField("ChildConceptStructurePartCustomBankPropertyCollection")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.ConceptStructurePartCustomBankPropertyEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.ChildConceptStructurePartCustomBankPropertyEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "ChildConceptStructurePartCustomBankPropertyCollection", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathConceptStructureCustomBankPropertySelectedPartCollection() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(Of ConceptStructureCustomBankPropertySelectedPartEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ConceptStructureCustomBankPropertySelectedPartEntityFactory))), _
                    CType(GetRelationsForField("ConceptStructureCustomBankPropertySelectedPartCollection")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.ConceptStructurePartCustomBankPropertyEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.ConceptStructureCustomBankPropertySelectedPartEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "ConceptStructureCustomBankPropertySelectedPartCollection", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany)
            End Get
        End Property


        Public Shared ReadOnly Property PrefetchPathConceptStructureCustomBankProperty() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(EntityFactoryCache2.GetEntityFactory(GetType(ConceptStructureCustomBankPropertyEntityFactory))), _
                    CType(GetRelationsForField("ConceptStructureCustomBankProperty")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.ConceptStructurePartCustomBankPropertyEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.ConceptStructureCustomBankPropertyEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "ConceptStructureCustomBankProperty", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathConceptType() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(EntityFactoryCache2.GetEntityFactory(GetType(ConceptTypeEntityFactory))), _
                    CType(GetRelationsForField("ConceptType")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.ConceptStructurePartCustomBankPropertyEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.ConceptTypeEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "ConceptType", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne)
            End Get
        End Property


        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property CustomPropertiesOfType() As Dictionary(Of String, String)
            Get
                Return ConceptStructurePartCustomBankPropertyEntity.CustomProperties
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
                Return ConceptStructurePartCustomBankPropertyEntity.FieldsCustomProperties
            End Get
        End Property

        Public Overridable Property [ConceptStructurePartCustomBankPropertyId]() As System.Guid
            Get
                Return CType(GetValue(CInt(ConceptStructurePartCustomBankPropertyFieldIndex.ConceptStructurePartCustomBankPropertyId), True), System.Guid)
            End Get
            Set
                SetValue(CInt(ConceptStructurePartCustomBankPropertyFieldIndex.ConceptStructurePartCustomBankPropertyId), value)
            End Set
        End Property
        Public Overridable Property [CustomBankPropertyId]() As System.Guid
            Get
                Return CType(GetValue(CInt(ConceptStructurePartCustomBankPropertyFieldIndex.CustomBankPropertyId), True), System.Guid)
            End Get
            Set
                SetValue(CInt(ConceptStructurePartCustomBankPropertyFieldIndex.CustomBankPropertyId), value)
            End Set
        End Property
        Public Overridable Property [Name]() As System.String
            Get
                Return CType(GetValue(CInt(ConceptStructurePartCustomBankPropertyFieldIndex.Name), True), System.String)
            End Get
            Set
                SetValue(CInt(ConceptStructurePartCustomBankPropertyFieldIndex.Name), value)
            End Set
        End Property
        Public Overridable Property [Title]() As System.String
            Get
                Return CType(GetValue(CInt(ConceptStructurePartCustomBankPropertyFieldIndex.Title), True), System.String)
            End Get
            Set
                SetValue(CInt(ConceptStructurePartCustomBankPropertyFieldIndex.Title), value)
            End Set
        End Property
        Public Overridable Property [Code]() As System.Guid
            Get
                Return CType(GetValue(CInt(ConceptStructurePartCustomBankPropertyFieldIndex.Code), True), System.Guid)
            End Get
            Set
                SetValue(CInt(ConceptStructurePartCustomBankPropertyFieldIndex.Code), value)
            End Set
        End Property
        Public Overridable Property [ConceptTypeId]() As System.Int32
            Get
                Return CType(GetValue(CInt(ConceptStructurePartCustomBankPropertyFieldIndex.ConceptTypeId), True), System.Int32)
            End Get
            Set
                SetValue(CInt(ConceptStructurePartCustomBankPropertyFieldIndex.ConceptTypeId), value)
            End Set
        End Property

        <TypeContainedAttribute(GetType(ChildConceptStructurePartCustomBankPropertyEntity))> _
        Public Overridable ReadOnly Property [ReferencedConceptStructurePartCustomBankPropertyCollection]() As EntityCollection(Of ChildConceptStructurePartCustomBankPropertyEntity)
            Get
                If _referencedConceptStructurePartCustomBankPropertyCollection Is Nothing Then
                    _referencedConceptStructurePartCustomBankPropertyCollection = New EntityCollection(Of ChildConceptStructurePartCustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ChildConceptStructurePartCustomBankPropertyEntityFactory)))
                    _referencedConceptStructurePartCustomBankPropertyCollection.ActiveContext = Me.ActiveContext
                    _referencedConceptStructurePartCustomBankPropertyCollection.SetContainingEntityInfo(Me, "ChildConceptStructurePartCustomBankProperty")
                End If
                Return _referencedConceptStructurePartCustomBankPropertyCollection
            End Get
        End Property

        <TypeContainedAttribute(GetType(ChildConceptStructurePartCustomBankPropertyEntity))> _
        Public Overridable ReadOnly Property [ChildConceptStructurePartCustomBankPropertyCollection]() As EntityCollection(Of ChildConceptStructurePartCustomBankPropertyEntity)
            Get
                If _childConceptStructurePartCustomBankPropertyCollection Is Nothing Then
                    _childConceptStructurePartCustomBankPropertyCollection = New EntityCollection(Of ChildConceptStructurePartCustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ChildConceptStructurePartCustomBankPropertyEntityFactory)))
                    _childConceptStructurePartCustomBankPropertyCollection.ActiveContext = Me.ActiveContext

                End If
                Return _childConceptStructurePartCustomBankPropertyCollection
            End Get
        End Property

        <TypeContainedAttribute(GetType(ConceptStructureCustomBankPropertySelectedPartEntity))> _
        Public Overridable ReadOnly Property [ConceptStructureCustomBankPropertySelectedPartCollection]() As EntityCollection(Of ConceptStructureCustomBankPropertySelectedPartEntity)
            Get
                If _conceptStructureCustomBankPropertySelectedPartCollection Is Nothing Then
                    _conceptStructureCustomBankPropertySelectedPartCollection = New EntityCollection(Of ConceptStructureCustomBankPropertySelectedPartEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ConceptStructureCustomBankPropertySelectedPartEntityFactory)))
                    _conceptStructureCustomBankPropertySelectedPartCollection.ActiveContext = Me.ActiveContext
                    _conceptStructureCustomBankPropertySelectedPartCollection.SetContainingEntityInfo(Me, "ConceptStructurePartCustomBankProperty")
                End If
                Return _conceptStructureCustomBankPropertySelectedPartCollection
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
                    SetSingleRelatedEntityNavigator(value, "ConceptStructurePartCustomBankPropertyCollection", "ConceptStructureCustomBankProperty", _conceptStructureCustomBankProperty, True)
                End If
            End Set
        End Property

        <Browsable(True)> _
        Public Overridable Property [ConceptType]() As ConceptTypeEntity
            Get
                Return _conceptType
            End Get
            Set
                If MyBase.IsDeserializing Then
                    SetupSyncConceptType(value)
                Else
                    SetSingleRelatedEntityNavigator(value, "ConceptStructurePartCustomBankPropertyCollection", "ConceptType", _conceptType, True)
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
                Return CInt(Questify.Builder.Model.ContentModel.EntityType.ConceptStructurePartCustomBankPropertyEntity)
            End Get
        End Property






    End Class
End Namespace
