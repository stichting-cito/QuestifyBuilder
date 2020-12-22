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
    Public Class ConceptStructureCustomBankPropertySelectedPartEntity
        Inherits CommonEntityBase



        Private WithEvents _conceptStructureCustomBankPropertyValue As ConceptStructureCustomBankPropertyValueEntity
        Private WithEvents _conceptStructurePartCustomBankProperty As ConceptStructurePartCustomBankPropertyEntity



        Private Shared _customProperties As Dictionary(Of String, String)
        Private Shared _fieldsCustomProperties As Dictionary(Of String, Dictionary(Of String, String))

        Public NotInheritable Class MemberNames
            Private Sub New()
            End Sub
            Public Shared ReadOnly [ConceptStructureCustomBankPropertyValue] As String = "ConceptStructureCustomBankPropertyValue"
            Public Shared ReadOnly [ConceptStructurePartCustomBankProperty] As String = "ConceptStructurePartCustomBankProperty"
        End Class

        Shared Sub New()
            SetupCustomPropertyHashtables()
        End Sub

        Public Sub New()
            MyBase.New("ConceptStructureCustomBankPropertySelectedPartEntity")
            InitClassEmpty(Nothing, Nothing)
        End Sub

        Public Sub New(fields As IEntityFields2)
            MyBase.New("ConceptStructureCustomBankPropertySelectedPartEntity")
            InitClassEmpty(Nothing, fields)
        End Sub

        Public Sub New(validator As IValidator)
            MyBase.New("ConceptStructureCustomBankPropertySelectedPartEntity")
            InitClassEmpty(validator, Nothing)
        End Sub

        Public Sub New(conceptStructurePartId As System.Guid, resourceId As System.Guid, customBankPropertyId As System.Guid)
            MyBase.New("ConceptStructureCustomBankPropertySelectedPartEntity")
            InitClassEmpty(Nothing, Nothing)
            Me.ConceptStructurePartId = conceptStructurePartId
            Me.ResourceId = resourceId
            Me.CustomBankPropertyId = customBankPropertyId
        End Sub

        Public Sub New(conceptStructurePartId As System.Guid, resourceId As System.Guid, customBankPropertyId As System.Guid, validator As IValidator)
            MyBase.New("ConceptStructureCustomBankPropertySelectedPartEntity")
            InitClassEmpty(validator, Nothing)
            Me.ConceptStructurePartId = conceptStructurePartId
            Me.ResourceId = resourceId
            Me.CustomBankPropertyId = customBankPropertyId
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Sub New(info As SerializationInfo, context As StreamingContext)
            MyBase.New(info, context)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                _conceptStructureCustomBankPropertyValue = CType(info.GetValue("_conceptStructureCustomBankPropertyValue", GetType(ConceptStructureCustomBankPropertyValueEntity)), ConceptStructureCustomBankPropertyValueEntity)
                If Not _conceptStructureCustomBankPropertyValue Is Nothing Then
                    AddHandler _conceptStructureCustomBankPropertyValue.AfterSave, AddressOf OnEntityAfterSave
                End If
                _conceptStructurePartCustomBankProperty = CType(info.GetValue("_conceptStructurePartCustomBankProperty", GetType(ConceptStructurePartCustomBankPropertyEntity)), ConceptStructurePartCustomBankPropertyEntity)
                If Not _conceptStructurePartCustomBankProperty Is Nothing Then
                    AddHandler _conceptStructurePartCustomBankProperty.AfterSave, AddressOf OnEntityAfterSave
                End If
                Me.FixupDeserialization(FieldInfoProviderSingleton.GetInstance())
            End If

        End Sub


        Protected Overrides Sub PerformDesyncSetupFKFieldChange(fieldIndex As Integer)
            Select Case CType(fieldIndex, ConceptStructureCustomBankPropertySelectedPartFieldIndex)
                Case ConceptStructureCustomBankPropertySelectedPartFieldIndex.ConceptStructurePartId
                    DesetupSyncConceptStructurePartCustomBankProperty(True, False)
                Case ConceptStructureCustomBankPropertySelectedPartFieldIndex.ResourceId
                    DesetupSyncConceptStructureCustomBankPropertyValue(True, False)
                Case ConceptStructureCustomBankPropertySelectedPartFieldIndex.CustomBankPropertyId
                    DesetupSyncConceptStructureCustomBankPropertyValue(True, False)
                Case Else
                    MyBase.PerformDesyncSetupFKFieldChange(fieldIndex)
            End Select
        End Sub


        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub SetRelatedEntityProperty(propertyName As String, entity As IEntityCore)
            Select Case propertyName
                Case "ConceptStructureCustomBankPropertyValue"
                    Me.ConceptStructureCustomBankPropertyValue = CType(entity, ConceptStructureCustomBankPropertyValueEntity)
                Case "ConceptStructurePartCustomBankProperty"
                    Me.ConceptStructurePartCustomBankProperty = CType(entity, ConceptStructurePartCustomBankPropertyEntity)

                Case Else
                    Me.OnSetRelatedEntityProperty(propertyName, entity)
            End Select
        End Sub

        Protected Overrides Function GetRelationsForFieldOfType(fieldName As String) As RelationCollection
            Return ConceptStructureCustomBankPropertySelectedPartEntity.GetRelationsForField(fieldName)
        End Function

        Friend Shared Function GetRelationsForField(fieldName As String) As RelationCollection
            Dim toReturn As New RelationCollection()
            Select Case fieldName
                Case "ConceptStructureCustomBankPropertyValue"
                    toReturn.Add(ConceptStructureCustomBankPropertySelectedPartEntity.Relations.ConceptStructureCustomBankPropertyValueEntityUsingCustomBankPropertyIdResourceId)
                Case "ConceptStructurePartCustomBankProperty"
                    toReturn.Add(ConceptStructureCustomBankPropertySelectedPartEntity.Relations.ConceptStructurePartCustomBankPropertyEntityUsingConceptStructurePartId)
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
                Case "ConceptStructureCustomBankPropertyValue"
                    SetupSyncConceptStructureCustomBankPropertyValue(relatedEntity)
                Case "ConceptStructurePartCustomBankProperty"
                    SetupSyncConceptStructurePartCustomBankProperty(relatedEntity)

                Case Else
            End Select
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub UnsetRelatedEntity(relatedEntity As IEntityCore, fieldName As String, signalRelatedEntityManyToOne As Boolean)
            Select Case fieldName
                Case "ConceptStructureCustomBankPropertyValue"
                    DesetupSyncConceptStructureCustomBankPropertyValue(False, True)
                Case "ConceptStructurePartCustomBankProperty"
                    DesetupSyncConceptStructurePartCustomBankProperty(False, True)
                Case Else
            End Select
        End Sub

        Protected Overrides Function GetDependingRelatedEntities() As List(Of IEntity2)
            Dim toReturn As New List(Of IEntity2)()
            Return toReturn
        End Function

        Protected Overrides Function GetDependentRelatedEntities() As List(Of IEntity2)
            Dim toReturn As New List(Of IEntity2)()
            If Not _conceptStructureCustomBankPropertyValue Is Nothing Then
                toReturn.Add(_conceptStructureCustomBankPropertyValue)
            End If
            If Not _conceptStructurePartCustomBankProperty Is Nothing Then
                toReturn.Add(_conceptStructurePartCustomBankProperty)
            End If
            Return toReturn
        End Function

        Protected Overrides Function GetMemberEntityCollections() As List(Of IEntityCollection2)
            Dim toReturn As New List(Of IEntityCollection2)()
            Return toReturn
        End Function


        Protected Overrides Sub GetObjectData(info As SerializationInfo, context As StreamingContext)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                Dim value As IEntityCollection2 = Nothing
                Dim entityValue As IEntity2 = Nothing
                entityValue = Nothing
                If Not Me.MarkedForDeletion Then
                    entityValue = _conceptStructureCustomBankPropertyValue
                End If
                info.AddValue("_conceptStructureCustomBankPropertyValue", entityValue)
                entityValue = Nothing
                If Not Me.MarkedForDeletion Then
                    entityValue = _conceptStructurePartCustomBankProperty
                End If
                info.AddValue("_conceptStructurePartCustomBankProperty", entityValue)
            End If

            MyBase.GetObjectData(info, context)
        End Sub


        Protected Overrides Overloads Function GetAllRelations() As List(Of IEntityRelation)
            Return New ConceptStructureCustomBankPropertySelectedPartRelations().GetAllRelations()
        End Function

        Public Overridable Function GetRelationInfoConceptStructureCustomBankPropertyValue() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(ConceptStructureCustomBankPropertyValueFields.CustomBankPropertyId, Nothing, ComparisonOperator.Equal, Me.CustomBankPropertyId))
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(ConceptStructureCustomBankPropertyValueFields.ResourceId, Nothing, ComparisonOperator.Equal, Me.ResourceId))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoConceptStructurePartCustomBankProperty() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(ConceptStructurePartCustomBankPropertyFields.ConceptStructurePartCustomBankPropertyId, Nothing, ComparisonOperator.Equal, Me.ConceptStructurePartId))
            Return bucket
        End Function

        Protected Overrides Function CreateEntityFactory() As IEntityFactory2
            Return EntityFactoryCache2.GetEntityFactory(GetType(ConceptStructureCustomBankPropertySelectedPartEntityFactory))
        End Function
#If Not CF Then
        Protected Overrides Sub AddToMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2))
            MyBase.AddToMemberEntityCollectionsQueue(collectionsQueue)
        End Sub

        Protected Overrides Sub GetFromMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2))
            MyBase.GetFromMemberEntityCollectionsQueue(collectionsQueue)
        End Sub

        Protected Overrides Function HasPopulatedMemberEntityCollections() As Boolean
            Return MyBase.HasPopulatedMemberEntityCollections()
        End Function

        Protected Overrides Overloads Sub CreateMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2), requiredQueue As Queue(Of Boolean))
            MyBase.CreateMemberEntityCollectionsQueue(collectionsQueue, requiredQueue)
            Dim toAdd As IEntityCollection2 = Nothing
        End Sub
#End If
        Protected Overrides Overloads Function GetRelatedData() As Dictionary(Of String, Object)
            Dim toReturn As New Dictionary(Of String, Object)()
            toReturn.Add("ConceptStructureCustomBankPropertyValue", _conceptStructureCustomBankPropertyValue)
            toReturn.Add("ConceptStructurePartCustomBankProperty", _conceptStructurePartCustomBankProperty)
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
            _fieldsCustomProperties.Add("ConceptStructurePartId", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("ResourceId", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("CustomBankPropertyId", fieldHashtable)
        End Sub


        Private Sub DesetupSyncConceptStructureCustomBankPropertyValue(signalRelatedEntity As Boolean, resetFKFields As Boolean)
            Me.PerformDesetupSyncRelatedEntity(_conceptStructureCustomBankPropertyValue, AddressOf OnConceptStructureCustomBankPropertyValuePropertyChanged, "ConceptStructureCustomBankPropertyValue", Questify.Builder.Model.ContentModel.RelationClasses.StaticConceptStructureCustomBankPropertySelectedPartRelations.ConceptStructureCustomBankPropertyValueEntityUsingCustomBankPropertyIdResourceIdStatic, True, signalRelatedEntity, "ConceptStructureCustomBankPropertySelectedPartCollection", resetFKFields, New Integer() {CInt(ConceptStructureCustomBankPropertySelectedPartFieldIndex.CustomBankPropertyId), CInt(ConceptStructureCustomBankPropertySelectedPartFieldIndex.ResourceId)})
            _conceptStructureCustomBankPropertyValue = Nothing
        End Sub

        Private Sub SetupSyncConceptStructureCustomBankPropertyValue(relatedEntity As IEntityCore)
            If Not _conceptStructureCustomBankPropertyValue Is relatedEntity Then
                DesetupSyncConceptStructureCustomBankPropertyValue(True, True)
                _conceptStructureCustomBankPropertyValue = CType(relatedEntity, ConceptStructureCustomBankPropertyValueEntity)
                Me.PerformSetupSyncRelatedEntity(_conceptStructureCustomBankPropertyValue, AddressOf OnConceptStructureCustomBankPropertyValuePropertyChanged, "ConceptStructureCustomBankPropertyValue", Questify.Builder.Model.ContentModel.RelationClasses.StaticConceptStructureCustomBankPropertySelectedPartRelations.ConceptStructureCustomBankPropertyValueEntityUsingCustomBankPropertyIdResourceIdStatic, True, New String() {})
            End If
        End Sub

        Private Sub OnConceptStructureCustomBankPropertyValuePropertyChanged(sender As Object, e As PropertyChangedEventArgs)
            Select Case e.PropertyName

                Case Else
            End Select
        End Sub

        Private Sub DesetupSyncConceptStructurePartCustomBankProperty(signalRelatedEntity As Boolean, resetFKFields As Boolean)
            Me.PerformDesetupSyncRelatedEntity(_conceptStructurePartCustomBankProperty, AddressOf OnConceptStructurePartCustomBankPropertyPropertyChanged, "ConceptStructurePartCustomBankProperty", Questify.Builder.Model.ContentModel.RelationClasses.StaticConceptStructureCustomBankPropertySelectedPartRelations.ConceptStructurePartCustomBankPropertyEntityUsingConceptStructurePartIdStatic, True, signalRelatedEntity, "ConceptStructureCustomBankPropertySelectedPartCollection", resetFKFields, New Integer() {CInt(ConceptStructureCustomBankPropertySelectedPartFieldIndex.ConceptStructurePartId)})
            _conceptStructurePartCustomBankProperty = Nothing
        End Sub

        Private Sub SetupSyncConceptStructurePartCustomBankProperty(relatedEntity As IEntityCore)
            If Not _conceptStructurePartCustomBankProperty Is relatedEntity Then
                DesetupSyncConceptStructurePartCustomBankProperty(True, True)
                _conceptStructurePartCustomBankProperty = CType(relatedEntity, ConceptStructurePartCustomBankPropertyEntity)
                Me.PerformSetupSyncRelatedEntity(_conceptStructurePartCustomBankProperty, AddressOf OnConceptStructurePartCustomBankPropertyPropertyChanged, "ConceptStructurePartCustomBankProperty", Questify.Builder.Model.ContentModel.RelationClasses.StaticConceptStructureCustomBankPropertySelectedPartRelations.ConceptStructurePartCustomBankPropertyEntityUsingConceptStructurePartIdStatic, True, New String() {})
            End If
        End Sub

        Private Sub OnConceptStructurePartCustomBankPropertyPropertyChanged(sender As Object, e As PropertyChangedEventArgs)
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

        Public Shared ReadOnly Property Relations() As ConceptStructureCustomBankPropertySelectedPartRelations
            Get
                Return New ConceptStructureCustomBankPropertySelectedPartRelations()
            End Get
        End Property

        Public Shared ReadOnly Property CustomProperties() As Dictionary(Of String, String)
            Get
                Return _customProperties
            End Get
        End Property




        Public Shared ReadOnly Property PrefetchPathConceptStructureCustomBankPropertyValue() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(EntityFactoryCache2.GetEntityFactory(GetType(ConceptStructureCustomBankPropertyValueEntityFactory))), _
                    CType(GetRelationsForField("ConceptStructureCustomBankPropertyValue")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.ConceptStructureCustomBankPropertySelectedPartEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.ConceptStructureCustomBankPropertyValueEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "ConceptStructureCustomBankPropertyValue", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathConceptStructurePartCustomBankProperty() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(EntityFactoryCache2.GetEntityFactory(GetType(ConceptStructurePartCustomBankPropertyEntityFactory))), _
                    CType(GetRelationsForField("ConceptStructurePartCustomBankProperty")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.ConceptStructureCustomBankPropertySelectedPartEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.ConceptStructurePartCustomBankPropertyEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "ConceptStructurePartCustomBankProperty", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne)
            End Get
        End Property


        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property CustomPropertiesOfType() As Dictionary(Of String, String)
            Get
                Return ConceptStructureCustomBankPropertySelectedPartEntity.CustomProperties
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
                Return ConceptStructureCustomBankPropertySelectedPartEntity.FieldsCustomProperties
            End Get
        End Property

        Public Overridable Property [ConceptStructurePartId]() As System.Guid
            Get
                Return CType(GetValue(CInt(ConceptStructureCustomBankPropertySelectedPartFieldIndex.ConceptStructurePartId), True), System.Guid)
            End Get
            Set
                SetValue(CInt(ConceptStructureCustomBankPropertySelectedPartFieldIndex.ConceptStructurePartId), value)
            End Set
        End Property
        Public Overridable Property [ResourceId]() As System.Guid
            Get
                Return CType(GetValue(CInt(ConceptStructureCustomBankPropertySelectedPartFieldIndex.ResourceId), True), System.Guid)
            End Get
            Set
                SetValue(CInt(ConceptStructureCustomBankPropertySelectedPartFieldIndex.ResourceId), value)
            End Set
        End Property
        Public Overridable Property [CustomBankPropertyId]() As System.Guid
            Get
                Return CType(GetValue(CInt(ConceptStructureCustomBankPropertySelectedPartFieldIndex.CustomBankPropertyId), True), System.Guid)
            End Get
            Set
                SetValue(CInt(ConceptStructureCustomBankPropertySelectedPartFieldIndex.CustomBankPropertyId), value)
            End Set
        End Property



        <Browsable(True)> _
        Public Overridable Property [ConceptStructureCustomBankPropertyValue]() As ConceptStructureCustomBankPropertyValueEntity
            Get
                Return _conceptStructureCustomBankPropertyValue
            End Get
            Set
                If MyBase.IsDeserializing Then
                    SetupSyncConceptStructureCustomBankPropertyValue(value)
                Else
                    SetSingleRelatedEntityNavigator(value, "ConceptStructureCustomBankPropertySelectedPartCollection", "ConceptStructureCustomBankPropertyValue", _conceptStructureCustomBankPropertyValue, True)
                End If
            End Set
        End Property

        <Browsable(True)> _
        Public Overridable Property [ConceptStructurePartCustomBankProperty]() As ConceptStructurePartCustomBankPropertyEntity
            Get
                Return _conceptStructurePartCustomBankProperty
            End Get
            Set
                If MyBase.IsDeserializing Then
                    SetupSyncConceptStructurePartCustomBankProperty(value)
                Else
                    SetSingleRelatedEntityNavigator(value, "ConceptStructureCustomBankPropertySelectedPartCollection", "ConceptStructurePartCustomBankProperty", _conceptStructurePartCustomBankProperty, True)
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
                Return CInt(Questify.Builder.Model.ContentModel.EntityType.ConceptStructureCustomBankPropertySelectedPartEntity)
            End Get
        End Property






    End Class
End Namespace
