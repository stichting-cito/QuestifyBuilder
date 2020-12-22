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
    Public Class ConceptTypeEntity
        Inherits CommonEntityBase



        Private WithEvents _conceptStructurePartCustomBankPropertyCollection As EntityCollection(Of ConceptStructurePartCustomBankPropertyEntity)
        Private WithEvents _conceptStructureCustomBankPropertyCollectionViaConceptStructurePartCustomBankProperty As EntityCollection(Of ConceptStructureCustomBankPropertyEntity)



        Private Shared _customProperties As Dictionary(Of String, String)
        Private Shared _fieldsCustomProperties As Dictionary(Of String, Dictionary(Of String, String))

        Public NotInheritable Class MemberNames
            Private Sub New()
            End Sub
            Public Shared ReadOnly [ConceptStructurePartCustomBankPropertyCollection] As String = "ConceptStructurePartCustomBankPropertyCollection"
            Public Shared ReadOnly [ConceptStructureCustomBankPropertyCollectionViaConceptStructurePartCustomBankProperty] As String = "ConceptStructureCustomBankPropertyCollectionViaConceptStructurePartCustomBankProperty"
        End Class

        Shared Sub New()
            SetupCustomPropertyHashtables()
        End Sub

        Public Sub New()
            MyBase.New("ConceptTypeEntity")
            InitClassEmpty(Nothing, Nothing)
        End Sub

        Public Sub New(fields As IEntityFields2)
            MyBase.New("ConceptTypeEntity")
            InitClassEmpty(Nothing, fields)
        End Sub

        Public Sub New(validator As IValidator)
            MyBase.New("ConceptTypeEntity")
            InitClassEmpty(validator, Nothing)
        End Sub

        Public Sub New(conceptTypeId As System.Int32)
            MyBase.New("ConceptTypeEntity")
            InitClassEmpty(Nothing, Nothing)
            Me.ConceptTypeId = conceptTypeId
        End Sub

        Public Sub New(conceptTypeId As System.Int32, validator As IValidator)
            MyBase.New("ConceptTypeEntity")
            InitClassEmpty(validator, Nothing)
            Me.ConceptTypeId = conceptTypeId
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Sub New(info As SerializationInfo, context As StreamingContext)
            MyBase.New(info, context)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                _conceptStructurePartCustomBankPropertyCollection = CType(info.GetValue("_conceptStructurePartCustomBankPropertyCollection", GetType(EntityCollection(Of ConceptStructurePartCustomBankPropertyEntity))), EntityCollection(Of ConceptStructurePartCustomBankPropertyEntity))
                _conceptStructureCustomBankPropertyCollectionViaConceptStructurePartCustomBankProperty = CType(info.GetValue("_conceptStructureCustomBankPropertyCollectionViaConceptStructurePartCustomBankProperty", GetType(EntityCollection(Of ConceptStructureCustomBankPropertyEntity))), EntityCollection(Of ConceptStructureCustomBankPropertyEntity))
                Me.FixupDeserialization(FieldInfoProviderSingleton.GetInstance())
            End If

        End Sub



        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub SetRelatedEntityProperty(propertyName As String, entity As IEntityCore)
            Select Case propertyName
                Case "ConceptStructurePartCustomBankPropertyCollection"
                    Me.ConceptStructurePartCustomBankPropertyCollection.Add(CType(entity, ConceptStructurePartCustomBankPropertyEntity))
                Case "ConceptStructureCustomBankPropertyCollectionViaConceptStructurePartCustomBankProperty"
                    Me.ConceptStructureCustomBankPropertyCollectionViaConceptStructurePartCustomBankProperty.IsReadOnly = False
                    Me.ConceptStructureCustomBankPropertyCollectionViaConceptStructurePartCustomBankProperty.Add(CType(entity, ConceptStructureCustomBankPropertyEntity))
                    Me.ConceptStructureCustomBankPropertyCollectionViaConceptStructurePartCustomBankProperty.IsReadOnly = True

                Case Else
                    Me.OnSetRelatedEntityProperty(propertyName, entity)
            End Select
        End Sub

        Protected Overrides Function GetRelationsForFieldOfType(fieldName As String) As RelationCollection
            Return ConceptTypeEntity.GetRelationsForField(fieldName)
        End Function

        Friend Shared Function GetRelationsForField(fieldName As String) As RelationCollection
            Dim toReturn As New RelationCollection()
            Select Case fieldName
                Case "ConceptStructurePartCustomBankPropertyCollection"
                    toReturn.Add(ConceptTypeEntity.Relations.ConceptStructurePartCustomBankPropertyEntityUsingConceptTypeId)
                Case "ConceptStructureCustomBankPropertyCollectionViaConceptStructurePartCustomBankProperty"
                    toReturn.Add(ConceptTypeEntity.Relations.ConceptStructurePartCustomBankPropertyEntityUsingConceptTypeId, "ConceptTypeEntity__", "ConceptStructurePartCustomBankProperty_", JoinHint.None)
                    toReturn.Add(ConceptStructurePartCustomBankPropertyEntity.Relations.ConceptStructureCustomBankPropertyEntityUsingCustomBankPropertyId, "ConceptStructurePartCustomBankProperty_", String.Empty, JoinHint.None)
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
                Case "ConceptStructurePartCustomBankPropertyCollection"
                    Me.ConceptStructurePartCustomBankPropertyCollection.Add(CType(relatedEntity, ConceptStructurePartCustomBankPropertyEntity))

                Case Else
            End Select
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub UnsetRelatedEntity(relatedEntity As IEntityCore, fieldName As String, signalRelatedEntityManyToOne As Boolean)
            Select Case fieldName
                Case "ConceptStructurePartCustomBankPropertyCollection"
                    Me.PerformRelatedEntityRemoval(Me.ConceptStructurePartCustomBankPropertyCollection, relatedEntity, signalRelatedEntityManyToOne)
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
            toReturn.Add(Me.ConceptStructurePartCustomBankPropertyCollection)
            Return toReturn
        End Function


        Protected Overrides Sub GetObjectData(info As SerializationInfo, context As StreamingContext)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                Dim value As IEntityCollection2 = Nothing
                Dim entityValue As IEntity2 = Nothing
                value = Nothing
                If (Not (_conceptStructurePartCustomBankPropertyCollection Is Nothing)) AndAlso (_conceptStructurePartCustomBankPropertyCollection.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _conceptStructurePartCustomBankPropertyCollection
                End If
                info.AddValue("_conceptStructurePartCustomBankPropertyCollection", value)
                value = Nothing
                If (Not (_conceptStructureCustomBankPropertyCollectionViaConceptStructurePartCustomBankProperty Is Nothing)) AndAlso (_conceptStructureCustomBankPropertyCollectionViaConceptStructurePartCustomBankProperty.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _conceptStructureCustomBankPropertyCollectionViaConceptStructurePartCustomBankProperty
                End If
                info.AddValue("_conceptStructureCustomBankPropertyCollectionViaConceptStructurePartCustomBankProperty", value)
            End If

            MyBase.GetObjectData(info, context)
        End Sub


        Protected Overrides Overloads Function GetAllRelations() As List(Of IEntityRelation)
            Return New ConceptTypeRelations().GetAllRelations()
        End Function

        Public Overridable Function GetRelationInfoConceptStructurePartCustomBankPropertyCollection() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(ConceptStructurePartCustomBankPropertyFields.ConceptTypeId, Nothing, ComparisonOperator.Equal, Me.ConceptTypeId))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoConceptStructureCustomBankPropertyCollectionViaConceptStructurePartCustomBankProperty() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.Relations.AddRange(GetRelationsForFieldOfType("ConceptStructureCustomBankPropertyCollectionViaConceptStructurePartCustomBankProperty"))
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(ConceptTypeFields.ConceptTypeId, Nothing, ComparisonOperator.Equal, Me.ConceptTypeId, "ConceptTypeEntity__"))
            Return bucket
        End Function

        Protected Overrides Function CreateEntityFactory() As IEntityFactory2
            Return EntityFactoryCache2.GetEntityFactory(GetType(ConceptTypeEntityFactory))
        End Function
#If Not CF Then
        Protected Overrides Sub AddToMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2))
            MyBase.AddToMemberEntityCollectionsQueue(collectionsQueue)
            collectionsQueue.Enqueue(_conceptStructurePartCustomBankPropertyCollection)
            collectionsQueue.Enqueue(_conceptStructureCustomBankPropertyCollectionViaConceptStructurePartCustomBankProperty)
        End Sub

        Protected Overrides Sub GetFromMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2))
            MyBase.GetFromMemberEntityCollectionsQueue(collectionsQueue)
            _conceptStructurePartCustomBankPropertyCollection = CType(collectionsQueue.Dequeue(), EntityCollection(Of ConceptStructurePartCustomBankPropertyEntity))
            _conceptStructureCustomBankPropertyCollectionViaConceptStructurePartCustomBankProperty = CType(collectionsQueue.Dequeue(), EntityCollection(Of ConceptStructureCustomBankPropertyEntity))
        End Sub

        Protected Overrides Function HasPopulatedMemberEntityCollections() As Boolean
            If (Not _conceptStructurePartCustomBankPropertyCollection Is Nothing) Then
                Return True
            End If
            If (Not _conceptStructureCustomBankPropertyCollectionViaConceptStructurePartCustomBankProperty Is Nothing) Then
                Return True
            End If
            Return MyBase.HasPopulatedMemberEntityCollections()
        End Function

        Protected Overrides Overloads Sub CreateMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2), requiredQueue As Queue(Of Boolean))
            MyBase.CreateMemberEntityCollectionsQueue(collectionsQueue, requiredQueue)
            Dim toAdd As IEntityCollection2 = Nothing
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of ConceptStructurePartCustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ConceptStructurePartCustomBankPropertyEntityFactory)))
            Else
                toAdd = Nothing
            End If
            collectionsQueue.Enqueue(toAdd)
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of ConceptStructureCustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ConceptStructureCustomBankPropertyEntityFactory)))
            Else
                toAdd = Nothing
            End If
            collectionsQueue.Enqueue(toAdd)
        End Sub
#End If
        Protected Overrides Overloads Function GetRelatedData() As Dictionary(Of String, Object)
            Dim toReturn As New Dictionary(Of String, Object)()
            toReturn.Add("ConceptStructurePartCustomBankPropertyCollection", _conceptStructurePartCustomBankPropertyCollection)
            toReturn.Add("ConceptStructureCustomBankPropertyCollectionViaConceptStructurePartCustomBankProperty", _conceptStructureCustomBankPropertyCollectionViaConceptStructurePartCustomBankProperty)
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
            _fieldsCustomProperties.Add("ConceptTypeId", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("Name", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("ApplicableToMask", fieldHashtable)
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

        Public Shared ReadOnly Property Relations() As ConceptTypeRelations
            Get
                Return New ConceptTypeRelations()
            End Get
        End Property

        Public Shared ReadOnly Property CustomProperties() As Dictionary(Of String, String)
            Get
                Return _customProperties
            End Get
        End Property


        Public Shared ReadOnly Property PrefetchPathConceptStructurePartCustomBankPropertyCollection() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(Of ConceptStructurePartCustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ConceptStructurePartCustomBankPropertyEntityFactory))), _
                    CType(GetRelationsForField("ConceptStructurePartCustomBankPropertyCollection")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.ConceptTypeEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.ConceptStructurePartCustomBankPropertyEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "ConceptStructurePartCustomBankPropertyCollection", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathConceptStructureCustomBankPropertyCollectionViaConceptStructurePartCustomBankProperty() As IPrefetchPathElement2
            Get
                Dim intermediateRelation As IEntityRelation = ConceptTypeEntity.Relations.ConceptStructurePartCustomBankPropertyEntityUsingConceptTypeId
                intermediateRelation.SetAliases(String.Empty, "ConceptStructurePartCustomBankProperty_")
                Return New PrefetchPathElement2(New EntityCollection(Of ConceptStructureCustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ConceptStructureCustomBankPropertyEntityFactory))), _
                    intermediateRelation, CType(Questify.Builder.Model.ContentModel.EntityType.ConceptTypeEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.ConceptStructureCustomBankPropertyEntity, Integer), 0, Nothing, Nothing, GetRelationsForField("ConceptStructureCustomBankPropertyCollectionViaConceptStructurePartCustomBankProperty"), Nothing, "ConceptStructureCustomBankPropertyCollectionViaConceptStructurePartCustomBankProperty", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToMany)
            End Get
        End Property



        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property CustomPropertiesOfType() As Dictionary(Of String, String)
            Get
                Return ConceptTypeEntity.CustomProperties
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
                Return ConceptTypeEntity.FieldsCustomProperties
            End Get
        End Property

        Public Overridable Property [ConceptTypeId]() As System.Int32
            Get
                Return CType(GetValue(CInt(ConceptTypeFieldIndex.ConceptTypeId), True), System.Int32)
            End Get
            Set
                SetValue(CInt(ConceptTypeFieldIndex.ConceptTypeId), value)
            End Set
        End Property
        Public Overridable Property [Name]() As System.String
            Get
                Return CType(GetValue(CInt(ConceptTypeFieldIndex.Name), True), System.String)
            End Get
            Set
                SetValue(CInt(ConceptTypeFieldIndex.Name), value)
            End Set
        End Property
        Public Overridable Property [ApplicableToMask]() As System.Int32
            Get
                Return CType(GetValue(CInt(ConceptTypeFieldIndex.ApplicableToMask), True), System.Int32)
            End Get
            Set
                SetValue(CInt(ConceptTypeFieldIndex.ApplicableToMask), value)
            End Set
        End Property

        <TypeContainedAttribute(GetType(ConceptStructurePartCustomBankPropertyEntity))> _
        Public Overridable ReadOnly Property [ConceptStructurePartCustomBankPropertyCollection]() As EntityCollection(Of ConceptStructurePartCustomBankPropertyEntity)
            Get
                If _conceptStructurePartCustomBankPropertyCollection Is Nothing Then
                    _conceptStructurePartCustomBankPropertyCollection = New EntityCollection(Of ConceptStructurePartCustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ConceptStructurePartCustomBankPropertyEntityFactory)))
                    _conceptStructurePartCustomBankPropertyCollection.ActiveContext = Me.ActiveContext
                    _conceptStructurePartCustomBankPropertyCollection.SetContainingEntityInfo(Me, "ConceptType")
                End If
                Return _conceptStructurePartCustomBankPropertyCollection
            End Get
        End Property

        <TypeContainedAttribute(GetType(ConceptStructureCustomBankPropertyEntity))> _
        Public Overridable ReadOnly Property [ConceptStructureCustomBankPropertyCollectionViaConceptStructurePartCustomBankProperty]() As EntityCollection(Of ConceptStructureCustomBankPropertyEntity)
            Get
                If _conceptStructureCustomBankPropertyCollectionViaConceptStructurePartCustomBankProperty Is Nothing Then
                    _conceptStructureCustomBankPropertyCollectionViaConceptStructurePartCustomBankProperty = New EntityCollection(Of ConceptStructureCustomBankPropertyEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ConceptStructureCustomBankPropertyEntityFactory)))
                    _conceptStructureCustomBankPropertyCollectionViaConceptStructurePartCustomBankProperty.ActiveContext = Me.ActiveContext
                    _conceptStructureCustomBankPropertyCollectionViaConceptStructurePartCustomBankProperty.IsReadOnly = True
                    CType(_conceptStructureCustomBankPropertyCollectionViaConceptStructurePartCustomBankProperty, IEntityCollectionCore).IsForMN = True
                End If
                Return _conceptStructureCustomBankPropertyCollectionViaConceptStructurePartCustomBankProperty
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
                Return CInt(Questify.Builder.Model.ContentModel.EntityType.ConceptTypeEntity)
            End Get
        End Property






    End Class
End Namespace
