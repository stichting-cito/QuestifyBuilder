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
    Public Class ChildConceptStructurePartCustomBankPropertyEntity
        Inherits CommonEntityBase



        Private WithEvents _childConceptStructurePartCustomBankProperty As ConceptStructurePartCustomBankPropertyEntity



        Private Shared _customProperties As Dictionary(Of String, String)
        Private Shared _fieldsCustomProperties As Dictionary(Of String, Dictionary(Of String, String))

        Public NotInheritable Class MemberNames
            Private Sub New()
            End Sub
            Public Shared ReadOnly [ChildConceptStructurePartCustomBankProperty] As String = "ChildConceptStructurePartCustomBankProperty"
        End Class

        Shared Sub New()
            SetupCustomPropertyHashtables()
        End Sub

        Public Sub New()
            MyBase.New("ChildConceptStructurePartCustomBankPropertyEntity")
            InitClassEmpty(Nothing, Nothing)
        End Sub

        Public Sub New(fields As IEntityFields2)
            MyBase.New("ChildConceptStructurePartCustomBankPropertyEntity")
            InitClassEmpty(Nothing, fields)
        End Sub

        Public Sub New(validator As IValidator)
            MyBase.New("ChildConceptStructurePartCustomBankPropertyEntity")
            InitClassEmpty(validator, Nothing)
        End Sub

        Public Sub New(id As System.Guid, conceptStructurePartCustomBankPropertyId As System.Guid, childConceptStructurePartCustomBankPropertyId As System.Guid)
            MyBase.New("ChildConceptStructurePartCustomBankPropertyEntity")
            InitClassEmpty(Nothing, Nothing)
            Me.Id = id
            Me.ConceptStructurePartCustomBankPropertyId = conceptStructurePartCustomBankPropertyId
            Me.ChildConceptStructurePartCustomBankPropertyId = childConceptStructurePartCustomBankPropertyId
        End Sub

        Public Sub New(id As System.Guid, conceptStructurePartCustomBankPropertyId As System.Guid, childConceptStructurePartCustomBankPropertyId As System.Guid, validator As IValidator)
            MyBase.New("ChildConceptStructurePartCustomBankPropertyEntity")
            InitClassEmpty(validator, Nothing)
            Me.Id = id
            Me.ConceptStructurePartCustomBankPropertyId = conceptStructurePartCustomBankPropertyId
            Me.ChildConceptStructurePartCustomBankPropertyId = childConceptStructurePartCustomBankPropertyId
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Sub New(info As SerializationInfo, context As StreamingContext)
            MyBase.New(info, context)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                _childConceptStructurePartCustomBankProperty = CType(info.GetValue("_childConceptStructurePartCustomBankProperty", GetType(ConceptStructurePartCustomBankPropertyEntity)), ConceptStructurePartCustomBankPropertyEntity)
                If Not _childConceptStructurePartCustomBankProperty Is Nothing Then
                    AddHandler _childConceptStructurePartCustomBankProperty.AfterSave, AddressOf OnEntityAfterSave
                End If
                Me.FixupDeserialization(FieldInfoProviderSingleton.GetInstance())
            End If

        End Sub


        Protected Overrides Sub PerformDesyncSetupFKFieldChange(fieldIndex As Integer)
            Select Case CType(fieldIndex, ChildConceptStructurePartCustomBankPropertyFieldIndex)

                Case ChildConceptStructurePartCustomBankPropertyFieldIndex.ConceptStructurePartCustomBankPropertyId

                Case ChildConceptStructurePartCustomBankPropertyFieldIndex.ChildConceptStructurePartCustomBankPropertyId
                    DesetupSyncChildConceptStructurePartCustomBankProperty(True, False)

                Case Else
                    MyBase.PerformDesyncSetupFKFieldChange(fieldIndex)
            End Select
        End Sub


        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub SetRelatedEntityProperty(propertyName As String, entity As IEntityCore)
            Select Case propertyName
                Case "ChildConceptStructurePartCustomBankProperty"
                    Me.ChildConceptStructurePartCustomBankProperty = CType(entity, ConceptStructurePartCustomBankPropertyEntity)

                Case Else
                    Me.OnSetRelatedEntityProperty(propertyName, entity)
            End Select
        End Sub

        Protected Overrides Function GetRelationsForFieldOfType(fieldName As String) As RelationCollection
            Return ChildConceptStructurePartCustomBankPropertyEntity.GetRelationsForField(fieldName)
        End Function

        Friend Shared Function GetRelationsForField(fieldName As String) As RelationCollection
            Dim toReturn As New RelationCollection()
            Select Case fieldName
                Case "ChildConceptStructurePartCustomBankProperty"
                    toReturn.Add(ChildConceptStructurePartCustomBankPropertyEntity.Relations.ConceptStructurePartCustomBankPropertyEntityUsingChildConceptStructurePartCustomBankPropertyId)
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
                Case "ChildConceptStructurePartCustomBankProperty"
                    SetupSyncChildConceptStructurePartCustomBankProperty(relatedEntity)

                Case Else
            End Select
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub UnsetRelatedEntity(relatedEntity As IEntityCore, fieldName As String, signalRelatedEntityManyToOne As Boolean)
            Select Case fieldName
                Case "ChildConceptStructurePartCustomBankProperty"
                    DesetupSyncChildConceptStructurePartCustomBankProperty(False, True)
                Case Else
            End Select
        End Sub

        Protected Overrides Function GetDependingRelatedEntities() As List(Of IEntity2)
            Dim toReturn As New List(Of IEntity2)()
            Return toReturn
        End Function

        Protected Overrides Function GetDependentRelatedEntities() As List(Of IEntity2)
            Dim toReturn As New List(Of IEntity2)()
            If Not _childConceptStructurePartCustomBankProperty Is Nothing Then
                toReturn.Add(_childConceptStructurePartCustomBankProperty)
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
                    entityValue = _childConceptStructurePartCustomBankProperty
                End If
                info.AddValue("_childConceptStructurePartCustomBankProperty", entityValue)
            End If

            MyBase.GetObjectData(info, context)
        End Sub


        Protected Overrides Overloads Function GetAllRelations() As List(Of IEntityRelation)
            Return New ChildConceptStructurePartCustomBankPropertyRelations().GetAllRelations()
        End Function

        Public Overridable Function GetRelationInfoChildConceptStructurePartCustomBankProperty() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(ConceptStructurePartCustomBankPropertyFields.ConceptStructurePartCustomBankPropertyId, Nothing, ComparisonOperator.Equal, Me.ChildConceptStructurePartCustomBankPropertyId))
            Return bucket
        End Function

        Protected Overrides Function CreateEntityFactory() As IEntityFactory2
            Return EntityFactoryCache2.GetEntityFactory(GetType(ChildConceptStructurePartCustomBankPropertyEntityFactory))
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
            toReturn.Add("ChildConceptStructurePartCustomBankProperty", _childConceptStructurePartCustomBankProperty)
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
            _fieldsCustomProperties.Add("Id", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("ConceptStructurePartCustomBankPropertyId", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("ChildConceptStructurePartCustomBankPropertyId", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("VisualOrder", fieldHashtable)
        End Sub


        Private Sub DesetupSyncChildConceptStructurePartCustomBankProperty(signalRelatedEntity As Boolean, resetFKFields As Boolean)
            Me.PerformDesetupSyncRelatedEntity(_childConceptStructurePartCustomBankProperty, AddressOf OnChildConceptStructurePartCustomBankPropertyPropertyChanged, "ChildConceptStructurePartCustomBankProperty", Questify.Builder.Model.ContentModel.RelationClasses.StaticChildConceptStructurePartCustomBankPropertyRelations.ConceptStructurePartCustomBankPropertyEntityUsingChildConceptStructurePartCustomBankPropertyIdStatic, True, signalRelatedEntity, "ReferencedConceptStructurePartCustomBankPropertyCollection", resetFKFields, New Integer() {CInt(ChildConceptStructurePartCustomBankPropertyFieldIndex.ChildConceptStructurePartCustomBankPropertyId)})
            _childConceptStructurePartCustomBankProperty = Nothing
        End Sub

        Private Sub SetupSyncChildConceptStructurePartCustomBankProperty(relatedEntity As IEntityCore)
            If Not _childConceptStructurePartCustomBankProperty Is relatedEntity Then
                DesetupSyncChildConceptStructurePartCustomBankProperty(True, True)
                _childConceptStructurePartCustomBankProperty = CType(relatedEntity, ConceptStructurePartCustomBankPropertyEntity)
                Me.PerformSetupSyncRelatedEntity(_childConceptStructurePartCustomBankProperty, AddressOf OnChildConceptStructurePartCustomBankPropertyPropertyChanged, "ChildConceptStructurePartCustomBankProperty", Questify.Builder.Model.ContentModel.RelationClasses.StaticChildConceptStructurePartCustomBankPropertyRelations.ConceptStructurePartCustomBankPropertyEntityUsingChildConceptStructurePartCustomBankPropertyIdStatic, True, New String() {})
            End If
        End Sub

        Private Sub OnChildConceptStructurePartCustomBankPropertyPropertyChanged(sender As Object, e As PropertyChangedEventArgs)
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

        Public Shared ReadOnly Property Relations() As ChildConceptStructurePartCustomBankPropertyRelations
            Get
                Return New ChildConceptStructurePartCustomBankPropertyRelations()
            End Get
        End Property

        Public Shared ReadOnly Property CustomProperties() As Dictionary(Of String, String)
            Get
                Return _customProperties
            End Get
        End Property




        Public Shared ReadOnly Property PrefetchPathChildConceptStructurePartCustomBankProperty() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(EntityFactoryCache2.GetEntityFactory(GetType(ConceptStructurePartCustomBankPropertyEntityFactory))), _
                    CType(GetRelationsForField("ChildConceptStructurePartCustomBankProperty")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.ChildConceptStructurePartCustomBankPropertyEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.ConceptStructurePartCustomBankPropertyEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "ChildConceptStructurePartCustomBankProperty", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne)
            End Get
        End Property



        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property CustomPropertiesOfType() As Dictionary(Of String, String)
            Get
                Return ChildConceptStructurePartCustomBankPropertyEntity.CustomProperties
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
                Return ChildConceptStructurePartCustomBankPropertyEntity.FieldsCustomProperties
            End Get
        End Property

        Public Overridable Property [Id]() As System.Guid
            Get
                Return CType(GetValue(CInt(ChildConceptStructurePartCustomBankPropertyFieldIndex.Id), True), System.Guid)
            End Get
            Set
                SetValue(CInt(ChildConceptStructurePartCustomBankPropertyFieldIndex.Id), value)
            End Set
        End Property
        Public Overridable Property [ConceptStructurePartCustomBankPropertyId]() As System.Guid
            Get
                Return CType(GetValue(CInt(ChildConceptStructurePartCustomBankPropertyFieldIndex.ConceptStructurePartCustomBankPropertyId), True), System.Guid)
            End Get
            Set
                SetValue(CInt(ChildConceptStructurePartCustomBankPropertyFieldIndex.ConceptStructurePartCustomBankPropertyId), value)
            End Set
        End Property
        Public Overridable Property [ChildConceptStructurePartCustomBankPropertyId]() As System.Guid
            Get
                Return CType(GetValue(CInt(ChildConceptStructurePartCustomBankPropertyFieldIndex.ChildConceptStructurePartCustomBankPropertyId), True), System.Guid)
            End Get
            Set
                SetValue(CInt(ChildConceptStructurePartCustomBankPropertyFieldIndex.ChildConceptStructurePartCustomBankPropertyId), value)
            End Set
        End Property
        Public Overridable Property [VisualOrder]() As System.Int32
            Get
                Return CType(GetValue(CInt(ChildConceptStructurePartCustomBankPropertyFieldIndex.VisualOrder), True), System.Int32)
            End Get
            Set
                SetValue(CInt(ChildConceptStructurePartCustomBankPropertyFieldIndex.VisualOrder), value)
            End Set
        End Property



        <Browsable(True)> _
        Public Overridable Property [ChildConceptStructurePartCustomBankProperty]() As ConceptStructurePartCustomBankPropertyEntity
            Get
                Return _childConceptStructurePartCustomBankProperty
            End Get
            Set
                If MyBase.IsDeserializing Then
                    SetupSyncChildConceptStructurePartCustomBankProperty(value)
                Else
                    SetSingleRelatedEntityNavigator(value, "ReferencedConceptStructurePartCustomBankPropertyCollection", "ChildConceptStructurePartCustomBankProperty", _childConceptStructurePartCustomBankProperty, True)
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
                Return CInt(Questify.Builder.Model.ContentModel.EntityType.ChildConceptStructurePartCustomBankPropertyEntity)
            End Get
        End Property






    End Class
End Namespace
