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
    Public Class ListValueCustomBankPropertyEntity
        Inherits CommonEntityBase



        Private WithEvents _listCustomBankPropertySelectedValueCollection As EntityCollection(Of ListCustomBankPropertySelectedValueEntity)
        Private WithEvents _listCustomBankProperty As ListCustomBankPropertyEntity



        Private Shared _customProperties As Dictionary(Of String, String)
        Private Shared _fieldsCustomProperties As Dictionary(Of String, Dictionary(Of String, String))

        Public NotInheritable Class MemberNames
            Private Sub New()
            End Sub
            Public Shared ReadOnly [ListCustomBankProperty] As String = "ListCustomBankProperty"
            Public Shared ReadOnly [ListCustomBankPropertySelectedValueCollection] As String = "ListCustomBankPropertySelectedValueCollection"
        End Class

        Shared Sub New()
            SetupCustomPropertyHashtables()
        End Sub

        Public Sub New()
            MyBase.New("ListValueCustomBankPropertyEntity")
            InitClassEmpty(Nothing, Nothing)
        End Sub

        Public Sub New(fields As IEntityFields2)
            MyBase.New("ListValueCustomBankPropertyEntity")
            InitClassEmpty(Nothing, fields)
        End Sub

        Public Sub New(validator As IValidator)
            MyBase.New("ListValueCustomBankPropertyEntity")
            InitClassEmpty(validator, Nothing)
        End Sub

        Public Sub New(listValueBankCustomPropertyId As System.Guid)
            MyBase.New("ListValueCustomBankPropertyEntity")
            InitClassEmpty(Nothing, Nothing)
            Me.ListValueBankCustomPropertyId = listValueBankCustomPropertyId
        End Sub

        Public Sub New(listValueBankCustomPropertyId As System.Guid, validator As IValidator)
            MyBase.New("ListValueCustomBankPropertyEntity")
            InitClassEmpty(validator, Nothing)
            Me.ListValueBankCustomPropertyId = listValueBankCustomPropertyId
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Sub New(info As SerializationInfo, context As StreamingContext)
            MyBase.New(info, context)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                _listCustomBankPropertySelectedValueCollection = CType(info.GetValue("_listCustomBankPropertySelectedValueCollection", GetType(EntityCollection(Of ListCustomBankPropertySelectedValueEntity))), EntityCollection(Of ListCustomBankPropertySelectedValueEntity))
                _listCustomBankProperty = CType(info.GetValue("_listCustomBankProperty", GetType(ListCustomBankPropertyEntity)), ListCustomBankPropertyEntity)
                If Not _listCustomBankProperty Is Nothing Then
                    AddHandler _listCustomBankProperty.AfterSave, AddressOf OnEntityAfterSave
                End If
                Me.FixupDeserialization(FieldInfoProviderSingleton.GetInstance())
            End If

        End Sub


        Protected Overrides Sub PerformDesyncSetupFKFieldChange(fieldIndex As Integer)
            Select Case CType(fieldIndex, ListValueCustomBankPropertyFieldIndex)

                Case ListValueCustomBankPropertyFieldIndex.CustomBankPropertyId
                    DesetupSyncListCustomBankProperty(True, False)



                Case Else
                    MyBase.PerformDesyncSetupFKFieldChange(fieldIndex)
            End Select
        End Sub


        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub SetRelatedEntityProperty(propertyName As String, entity As IEntityCore)
            Select Case propertyName
                Case "ListCustomBankProperty"
                    Me.ListCustomBankProperty = CType(entity, ListCustomBankPropertyEntity)
                Case "ListCustomBankPropertySelectedValueCollection"
                    Me.ListCustomBankPropertySelectedValueCollection.Add(CType(entity, ListCustomBankPropertySelectedValueEntity))

                Case Else
                    Me.OnSetRelatedEntityProperty(propertyName, entity)
            End Select
        End Sub

        Protected Overrides Function GetRelationsForFieldOfType(fieldName As String) As RelationCollection
            Return ListValueCustomBankPropertyEntity.GetRelationsForField(fieldName)
        End Function

        Friend Shared Function GetRelationsForField(fieldName As String) As RelationCollection
            Dim toReturn As New RelationCollection()
            Select Case fieldName
                Case "ListCustomBankProperty"
                    toReturn.Add(ListValueCustomBankPropertyEntity.Relations.ListCustomBankPropertyEntityUsingCustomBankPropertyId)
                Case "ListCustomBankPropertySelectedValueCollection"
                    toReturn.Add(ListValueCustomBankPropertyEntity.Relations.ListCustomBankPropertySelectedValueEntityUsingListValueBankCustomPropertyId)
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
                Case "ListCustomBankProperty"
                    SetupSyncListCustomBankProperty(relatedEntity)
                Case "ListCustomBankPropertySelectedValueCollection"
                    Me.ListCustomBankPropertySelectedValueCollection.Add(CType(relatedEntity, ListCustomBankPropertySelectedValueEntity))

                Case Else
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
            End Select
        End Sub

        Protected Overrides Function GetDependingRelatedEntities() As List(Of IEntity2)
            Dim toReturn As New List(Of IEntity2)()
            Return toReturn
        End Function

        Protected Overrides Function GetDependentRelatedEntities() As List(Of IEntity2)
            Dim toReturn As New List(Of IEntity2)()
            If Not _listCustomBankProperty Is Nothing Then
                toReturn.Add(_listCustomBankProperty)
            End If
            Return toReturn
        End Function

        Protected Overrides Function GetMemberEntityCollections() As List(Of IEntityCollection2)
            Dim toReturn As New List(Of IEntityCollection2)()
            toReturn.Add(Me.ListCustomBankPropertySelectedValueCollection)
            Return toReturn
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
                entityValue = Nothing
                If Not Me.MarkedForDeletion Then
                    entityValue = _listCustomBankProperty
                End If
                info.AddValue("_listCustomBankProperty", entityValue)
            End If

            MyBase.GetObjectData(info, context)
        End Sub


        Protected Overrides Overloads Function GetAllRelations() As List(Of IEntityRelation)
            Return New ListValueCustomBankPropertyRelations().GetAllRelations()
        End Function

        Public Overridable Function GetRelationInfoListCustomBankPropertySelectedValueCollection() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(ListCustomBankPropertySelectedValueFields.ListValueBankCustomPropertyId, Nothing, ComparisonOperator.Equal, Me.ListValueBankCustomPropertyId))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoListCustomBankProperty() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(ListCustomBankPropertyFields.CustomBankPropertyId, Nothing, ComparisonOperator.Equal, Me.CustomBankPropertyId))
            Return bucket
        End Function

        Protected Overrides Function CreateEntityFactory() As IEntityFactory2
            Return EntityFactoryCache2.GetEntityFactory(GetType(ListValueCustomBankPropertyEntityFactory))
        End Function
#If Not CF Then
        Protected Overrides Sub AddToMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2))
            MyBase.AddToMemberEntityCollectionsQueue(collectionsQueue)
            collectionsQueue.Enqueue(_listCustomBankPropertySelectedValueCollection)
        End Sub

        Protected Overrides Sub GetFromMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2))
            MyBase.GetFromMemberEntityCollectionsQueue(collectionsQueue)
            _listCustomBankPropertySelectedValueCollection = CType(collectionsQueue.Dequeue(), EntityCollection(Of ListCustomBankPropertySelectedValueEntity))
        End Sub

        Protected Overrides Function HasPopulatedMemberEntityCollections() As Boolean
            If (Not _listCustomBankPropertySelectedValueCollection Is Nothing) Then
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
        End Sub
#End If
        Protected Overrides Overloads Function GetRelatedData() As Dictionary(Of String, Object)
            Dim toReturn As New Dictionary(Of String, Object)()
            toReturn.Add("ListCustomBankProperty", _listCustomBankProperty)
            toReturn.Add("ListCustomBankPropertySelectedValueCollection", _listCustomBankPropertySelectedValueCollection)
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
            _fieldsCustomProperties.Add("ListValueBankCustomPropertyId", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("CustomBankPropertyId", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("Name", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("Title", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("Code", fieldHashtable)
        End Sub


        Private Sub DesetupSyncListCustomBankProperty(signalRelatedEntity As Boolean, resetFKFields As Boolean)
            Me.PerformDesetupSyncRelatedEntity(_listCustomBankProperty, AddressOf OnListCustomBankPropertyPropertyChanged, "ListCustomBankProperty", Questify.Builder.Model.ContentModel.RelationClasses.StaticListValueCustomBankPropertyRelations.ListCustomBankPropertyEntityUsingCustomBankPropertyIdStatic, True, signalRelatedEntity, "ListValueCustomBankPropertyCollection", resetFKFields, New Integer() {CInt(ListValueCustomBankPropertyFieldIndex.CustomBankPropertyId)})
            _listCustomBankProperty = Nothing
        End Sub

        Private Sub SetupSyncListCustomBankProperty(relatedEntity As IEntityCore)
            If Not _listCustomBankProperty Is relatedEntity Then
                DesetupSyncListCustomBankProperty(True, True)
                _listCustomBankProperty = CType(relatedEntity, ListCustomBankPropertyEntity)
                Me.PerformSetupSyncRelatedEntity(_listCustomBankProperty, AddressOf OnListCustomBankPropertyPropertyChanged, "ListCustomBankProperty", Questify.Builder.Model.ContentModel.RelationClasses.StaticListValueCustomBankPropertyRelations.ListCustomBankPropertyEntityUsingCustomBankPropertyIdStatic, True, New String() {})
            End If
        End Sub

        Private Sub OnListCustomBankPropertyPropertyChanged(sender As Object, e As PropertyChangedEventArgs)
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

        Public Shared ReadOnly Property Relations() As ListValueCustomBankPropertyRelations
            Get
                Return New ListValueCustomBankPropertyRelations()
            End Get
        End Property

        Public Shared ReadOnly Property CustomProperties() As Dictionary(Of String, String)
            Get
                Return _customProperties
            End Get
        End Property


        Public Shared ReadOnly Property PrefetchPathListCustomBankPropertySelectedValueCollection() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(Of ListCustomBankPropertySelectedValueEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ListCustomBankPropertySelectedValueEntityFactory))), _
                    CType(GetRelationsForField("ListCustomBankPropertySelectedValueCollection")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.ListValueCustomBankPropertyEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.ListCustomBankPropertySelectedValueEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "ListCustomBankPropertySelectedValueCollection", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany)
            End Get
        End Property


        Public Shared ReadOnly Property PrefetchPathListCustomBankProperty() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(EntityFactoryCache2.GetEntityFactory(GetType(ListCustomBankPropertyEntityFactory))), _
                    CType(GetRelationsForField("ListCustomBankProperty")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.ListValueCustomBankPropertyEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.ListCustomBankPropertyEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "ListCustomBankProperty", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne)
            End Get
        End Property


        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property CustomPropertiesOfType() As Dictionary(Of String, String)
            Get
                Return ListValueCustomBankPropertyEntity.CustomProperties
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
                Return ListValueCustomBankPropertyEntity.FieldsCustomProperties
            End Get
        End Property

        Public Overridable Property [ListValueBankCustomPropertyId]() As System.Guid
            Get
                Return CType(GetValue(CInt(ListValueCustomBankPropertyFieldIndex.ListValueBankCustomPropertyId), True), System.Guid)
            End Get
            Set
                SetValue(CInt(ListValueCustomBankPropertyFieldIndex.ListValueBankCustomPropertyId), value)
            End Set
        End Property
        Public Overridable Property [CustomBankPropertyId]() As System.Guid
            Get
                Return CType(GetValue(CInt(ListValueCustomBankPropertyFieldIndex.CustomBankPropertyId), True), System.Guid)
            End Get
            Set
                SetValue(CInt(ListValueCustomBankPropertyFieldIndex.CustomBankPropertyId), value)
            End Set
        End Property
        Public Overridable Property [Name]() As System.String
            Get
                Return CType(GetValue(CInt(ListValueCustomBankPropertyFieldIndex.Name), True), System.String)
            End Get
            Set
                SetValue(CInt(ListValueCustomBankPropertyFieldIndex.Name), value)
            End Set
        End Property
        Public Overridable Property [Title]() As System.String
            Get
                Return CType(GetValue(CInt(ListValueCustomBankPropertyFieldIndex.Title), True), System.String)
            End Get
            Set
                SetValue(CInt(ListValueCustomBankPropertyFieldIndex.Title), value)
            End Set
        End Property
        Public Overridable Property [Code]() As System.Guid
            Get
                Return CType(GetValue(CInt(ListValueCustomBankPropertyFieldIndex.Code), True), System.Guid)
            End Get
            Set
                SetValue(CInt(ListValueCustomBankPropertyFieldIndex.Code), value)
            End Set
        End Property

        <TypeContainedAttribute(GetType(ListCustomBankPropertySelectedValueEntity))> _
        Public Overridable ReadOnly Property [ListCustomBankPropertySelectedValueCollection]() As EntityCollection(Of ListCustomBankPropertySelectedValueEntity)
            Get
                If _listCustomBankPropertySelectedValueCollection Is Nothing Then
                    _listCustomBankPropertySelectedValueCollection = New EntityCollection(Of ListCustomBankPropertySelectedValueEntity)(EntityFactoryCache2.GetEntityFactory(GetType(ListCustomBankPropertySelectedValueEntityFactory)))
                    _listCustomBankPropertySelectedValueCollection.ActiveContext = Me.ActiveContext
                    _listCustomBankPropertySelectedValueCollection.SetContainingEntityInfo(Me, "ListValueCustomBankProperty")
                End If
                Return _listCustomBankPropertySelectedValueCollection
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
                    SetSingleRelatedEntityNavigator(value, "ListValueCustomBankPropertyCollection", "ListCustomBankProperty", _listCustomBankProperty, True)
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
                Return CInt(Questify.Builder.Model.ContentModel.EntityType.ListValueCustomBankPropertyEntity)
            End Get
        End Property






    End Class
End Namespace
