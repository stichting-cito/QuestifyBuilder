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
    Public Class ListCustomBankPropertySelectedValueEntity
        Inherits CommonEntityBase



        Private WithEvents _listCustomBankPropertyValue As ListCustomBankPropertyValueEntity
        Private WithEvents _listValueCustomBankProperty As ListValueCustomBankPropertyEntity



        Private Shared _customProperties As Dictionary(Of String, String)
        Private Shared _fieldsCustomProperties As Dictionary(Of String, Dictionary(Of String, String))

        Public NotInheritable Class MemberNames
            Private Sub New()
            End Sub
            Public Shared ReadOnly [ListCustomBankPropertyValue] As String = "ListCustomBankPropertyValue"
            Public Shared ReadOnly [ListValueCustomBankProperty] As String = "ListValueCustomBankProperty"
        End Class

        Shared Sub New()
            SetupCustomPropertyHashtables()
        End Sub

        Public Sub New()
            MyBase.New("ListCustomBankPropertySelectedValueEntity")
            InitClassEmpty(Nothing, Nothing)
        End Sub

        Public Sub New(fields As IEntityFields2)
            MyBase.New("ListCustomBankPropertySelectedValueEntity")
            InitClassEmpty(Nothing, fields)
        End Sub

        Public Sub New(validator As IValidator)
            MyBase.New("ListCustomBankPropertySelectedValueEntity")
            InitClassEmpty(validator, Nothing)
        End Sub

        Public Sub New(resourceId As System.Guid, customBankPropertyId As System.Guid, listValueBankCustomPropertyId As System.Guid)
            MyBase.New("ListCustomBankPropertySelectedValueEntity")
            InitClassEmpty(Nothing, Nothing)
            Me.ResourceId = resourceId
            Me.CustomBankPropertyId = customBankPropertyId
            Me.ListValueBankCustomPropertyId = listValueBankCustomPropertyId
        End Sub

        Public Sub New(resourceId As System.Guid, customBankPropertyId As System.Guid, listValueBankCustomPropertyId As System.Guid, validator As IValidator)
            MyBase.New("ListCustomBankPropertySelectedValueEntity")
            InitClassEmpty(validator, Nothing)
            Me.ResourceId = resourceId
            Me.CustomBankPropertyId = customBankPropertyId
            Me.ListValueBankCustomPropertyId = listValueBankCustomPropertyId
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Sub New(info As SerializationInfo, context As StreamingContext)
            MyBase.New(info, context)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                _listCustomBankPropertyValue = CType(info.GetValue("_listCustomBankPropertyValue", GetType(ListCustomBankPropertyValueEntity)), ListCustomBankPropertyValueEntity)
                If Not _listCustomBankPropertyValue Is Nothing Then
                    AddHandler _listCustomBankPropertyValue.AfterSave, AddressOf OnEntityAfterSave
                End If
                _listValueCustomBankProperty = CType(info.GetValue("_listValueCustomBankProperty", GetType(ListValueCustomBankPropertyEntity)), ListValueCustomBankPropertyEntity)
                If Not _listValueCustomBankProperty Is Nothing Then
                    AddHandler _listValueCustomBankProperty.AfterSave, AddressOf OnEntityAfterSave
                End If
                Me.FixupDeserialization(FieldInfoProviderSingleton.GetInstance())
            End If

        End Sub


        Protected Overrides Sub PerformDesyncSetupFKFieldChange(fieldIndex As Integer)
            Select Case CType(fieldIndex, ListCustomBankPropertySelectedValueFieldIndex)
                Case ListCustomBankPropertySelectedValueFieldIndex.ResourceId
                    DesetupSyncListCustomBankPropertyValue(True, False)
                Case ListCustomBankPropertySelectedValueFieldIndex.CustomBankPropertyId
                    DesetupSyncListCustomBankPropertyValue(True, False)
                Case ListCustomBankPropertySelectedValueFieldIndex.ListValueBankCustomPropertyId
                    DesetupSyncListValueCustomBankProperty(True, False)
                Case Else
                    MyBase.PerformDesyncSetupFKFieldChange(fieldIndex)
            End Select
        End Sub


        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub SetRelatedEntityProperty(propertyName As String, entity As IEntityCore)
            Select Case propertyName
                Case "ListCustomBankPropertyValue"
                    Me.ListCustomBankPropertyValue = CType(entity, ListCustomBankPropertyValueEntity)
                Case "ListValueCustomBankProperty"
                    Me.ListValueCustomBankProperty = CType(entity, ListValueCustomBankPropertyEntity)

                Case Else
                    Me.OnSetRelatedEntityProperty(propertyName, entity)
            End Select
        End Sub

        Protected Overrides Function GetRelationsForFieldOfType(fieldName As String) As RelationCollection
            Return ListCustomBankPropertySelectedValueEntity.GetRelationsForField(fieldName)
        End Function

        Friend Shared Function GetRelationsForField(fieldName As String) As RelationCollection
            Dim toReturn As New RelationCollection()
            Select Case fieldName
                Case "ListCustomBankPropertyValue"
                    toReturn.Add(ListCustomBankPropertySelectedValueEntity.Relations.ListCustomBankPropertyValueEntityUsingCustomBankPropertyIdResourceId)
                Case "ListValueCustomBankProperty"
                    toReturn.Add(ListCustomBankPropertySelectedValueEntity.Relations.ListValueCustomBankPropertyEntityUsingListValueBankCustomPropertyId)
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
                Case "ListCustomBankPropertyValue"
                    SetupSyncListCustomBankPropertyValue(relatedEntity)
                Case "ListValueCustomBankProperty"
                    SetupSyncListValueCustomBankProperty(relatedEntity)

                Case Else
            End Select
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub UnsetRelatedEntity(relatedEntity As IEntityCore, fieldName As String, signalRelatedEntityManyToOne As Boolean)
            Select Case fieldName
                Case "ListCustomBankPropertyValue"
                    DesetupSyncListCustomBankPropertyValue(False, True)
                Case "ListValueCustomBankProperty"
                    DesetupSyncListValueCustomBankProperty(False, True)
                Case Else
            End Select
        End Sub

        Protected Overrides Function GetDependingRelatedEntities() As List(Of IEntity2)
            Dim toReturn As New List(Of IEntity2)()
            Return toReturn
        End Function

        Protected Overrides Function GetDependentRelatedEntities() As List(Of IEntity2)
            Dim toReturn As New List(Of IEntity2)()
            If Not _listCustomBankPropertyValue Is Nothing Then
                toReturn.Add(_listCustomBankPropertyValue)
            End If
            If Not _listValueCustomBankProperty Is Nothing Then
                toReturn.Add(_listValueCustomBankProperty)
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
                    entityValue = _listCustomBankPropertyValue
                End If
                info.AddValue("_listCustomBankPropertyValue", entityValue)
                entityValue = Nothing
                If Not Me.MarkedForDeletion Then
                    entityValue = _listValueCustomBankProperty
                End If
                info.AddValue("_listValueCustomBankProperty", entityValue)
            End If

            MyBase.GetObjectData(info, context)
        End Sub


        Protected Overrides Overloads Function GetAllRelations() As List(Of IEntityRelation)
            Return New ListCustomBankPropertySelectedValueRelations().GetAllRelations()
        End Function

        Public Overridable Function GetRelationInfoListCustomBankPropertyValue() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(ListCustomBankPropertyValueFields.CustomBankPropertyId, Nothing, ComparisonOperator.Equal, Me.CustomBankPropertyId))
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(ListCustomBankPropertyValueFields.ResourceId, Nothing, ComparisonOperator.Equal, Me.ResourceId))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoListValueCustomBankProperty() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(ListValueCustomBankPropertyFields.ListValueBankCustomPropertyId, Nothing, ComparisonOperator.Equal, Me.ListValueBankCustomPropertyId))
            Return bucket
        End Function

        Protected Overrides Function CreateEntityFactory() As IEntityFactory2
            Return EntityFactoryCache2.GetEntityFactory(GetType(ListCustomBankPropertySelectedValueEntityFactory))
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
            toReturn.Add("ListCustomBankPropertyValue", _listCustomBankPropertyValue)
            toReturn.Add("ListValueCustomBankProperty", _listValueCustomBankProperty)
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
            _fieldsCustomProperties.Add("ResourceId", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("CustomBankPropertyId", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("ListValueBankCustomPropertyId", fieldHashtable)
        End Sub


        Private Sub DesetupSyncListCustomBankPropertyValue(signalRelatedEntity As Boolean, resetFKFields As Boolean)
            Me.PerformDesetupSyncRelatedEntity(_listCustomBankPropertyValue, AddressOf OnListCustomBankPropertyValuePropertyChanged, "ListCustomBankPropertyValue", Questify.Builder.Model.ContentModel.RelationClasses.StaticListCustomBankPropertySelectedValueRelations.ListCustomBankPropertyValueEntityUsingCustomBankPropertyIdResourceIdStatic, True, signalRelatedEntity, "ListCustomBankPropertySelectedValueCollection", resetFKFields, New Integer() {CInt(ListCustomBankPropertySelectedValueFieldIndex.CustomBankPropertyId), CInt(ListCustomBankPropertySelectedValueFieldIndex.ResourceId)})
            _listCustomBankPropertyValue = Nothing
        End Sub

        Private Sub SetupSyncListCustomBankPropertyValue(relatedEntity As IEntityCore)
            If Not _listCustomBankPropertyValue Is relatedEntity Then
                DesetupSyncListCustomBankPropertyValue(True, True)
                _listCustomBankPropertyValue = CType(relatedEntity, ListCustomBankPropertyValueEntity)
                Me.PerformSetupSyncRelatedEntity(_listCustomBankPropertyValue, AddressOf OnListCustomBankPropertyValuePropertyChanged, "ListCustomBankPropertyValue", Questify.Builder.Model.ContentModel.RelationClasses.StaticListCustomBankPropertySelectedValueRelations.ListCustomBankPropertyValueEntityUsingCustomBankPropertyIdResourceIdStatic, True, New String() {})
            End If
        End Sub

        Private Sub OnListCustomBankPropertyValuePropertyChanged(sender As Object, e As PropertyChangedEventArgs)
            Select Case e.PropertyName

                Case Else
            End Select
        End Sub

        Private Sub DesetupSyncListValueCustomBankProperty(signalRelatedEntity As Boolean, resetFKFields As Boolean)
            Me.PerformDesetupSyncRelatedEntity(_listValueCustomBankProperty, AddressOf OnListValueCustomBankPropertyPropertyChanged, "ListValueCustomBankProperty", Questify.Builder.Model.ContentModel.RelationClasses.StaticListCustomBankPropertySelectedValueRelations.ListValueCustomBankPropertyEntityUsingListValueBankCustomPropertyIdStatic, True, signalRelatedEntity, "ListCustomBankPropertySelectedValueCollection", resetFKFields, New Integer() {CInt(ListCustomBankPropertySelectedValueFieldIndex.ListValueBankCustomPropertyId)})
            _listValueCustomBankProperty = Nothing
        End Sub

        Private Sub SetupSyncListValueCustomBankProperty(relatedEntity As IEntityCore)
            If Not _listValueCustomBankProperty Is relatedEntity Then
                DesetupSyncListValueCustomBankProperty(True, True)
                _listValueCustomBankProperty = CType(relatedEntity, ListValueCustomBankPropertyEntity)
                Me.PerformSetupSyncRelatedEntity(_listValueCustomBankProperty, AddressOf OnListValueCustomBankPropertyPropertyChanged, "ListValueCustomBankProperty", Questify.Builder.Model.ContentModel.RelationClasses.StaticListCustomBankPropertySelectedValueRelations.ListValueCustomBankPropertyEntityUsingListValueBankCustomPropertyIdStatic, True, New String() {})
            End If
        End Sub

        Private Sub OnListValueCustomBankPropertyPropertyChanged(sender As Object, e As PropertyChangedEventArgs)
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

        Public Shared ReadOnly Property Relations() As ListCustomBankPropertySelectedValueRelations
            Get
                Return New ListCustomBankPropertySelectedValueRelations()
            End Get
        End Property

        Public Shared ReadOnly Property CustomProperties() As Dictionary(Of String, String)
            Get
                Return _customProperties
            End Get
        End Property




        Public Shared ReadOnly Property PrefetchPathListCustomBankPropertyValue() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(EntityFactoryCache2.GetEntityFactory(GetType(ListCustomBankPropertyValueEntityFactory))), _
                    CType(GetRelationsForField("ListCustomBankPropertyValue")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.ListCustomBankPropertySelectedValueEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.ListCustomBankPropertyValueEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "ListCustomBankPropertyValue", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathListValueCustomBankProperty() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(EntityFactoryCache2.GetEntityFactory(GetType(ListValueCustomBankPropertyEntityFactory))), _
                    CType(GetRelationsForField("ListValueCustomBankProperty")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.ListCustomBankPropertySelectedValueEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.ListValueCustomBankPropertyEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "ListValueCustomBankProperty", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne)
            End Get
        End Property


        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property CustomPropertiesOfType() As Dictionary(Of String, String)
            Get
                Return ListCustomBankPropertySelectedValueEntity.CustomProperties
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
                Return ListCustomBankPropertySelectedValueEntity.FieldsCustomProperties
            End Get
        End Property

        Public Overridable Property [ResourceId]() As System.Guid
            Get
                Return CType(GetValue(CInt(ListCustomBankPropertySelectedValueFieldIndex.ResourceId), True), System.Guid)
            End Get
            Set
                SetValue(CInt(ListCustomBankPropertySelectedValueFieldIndex.ResourceId), value)
            End Set
        End Property
        Public Overridable Property [CustomBankPropertyId]() As System.Guid
            Get
                Return CType(GetValue(CInt(ListCustomBankPropertySelectedValueFieldIndex.CustomBankPropertyId), True), System.Guid)
            End Get
            Set
                SetValue(CInt(ListCustomBankPropertySelectedValueFieldIndex.CustomBankPropertyId), value)
            End Set
        End Property
        Public Overridable Property [ListValueBankCustomPropertyId]() As System.Guid
            Get
                Return CType(GetValue(CInt(ListCustomBankPropertySelectedValueFieldIndex.ListValueBankCustomPropertyId), True), System.Guid)
            End Get
            Set
                SetValue(CInt(ListCustomBankPropertySelectedValueFieldIndex.ListValueBankCustomPropertyId), value)
            End Set
        End Property



        <Browsable(True)> _
        Public Overridable Property [ListCustomBankPropertyValue]() As ListCustomBankPropertyValueEntity
            Get
                Return _listCustomBankPropertyValue
            End Get
            Set
                If MyBase.IsDeserializing Then
                    SetupSyncListCustomBankPropertyValue(value)
                Else
                    SetSingleRelatedEntityNavigator(value, "ListCustomBankPropertySelectedValueCollection", "ListCustomBankPropertyValue", _listCustomBankPropertyValue, True)
                End If
            End Set
        End Property

        <Browsable(True)> _
        Public Overridable Property [ListValueCustomBankProperty]() As ListValueCustomBankPropertyEntity
            Get
                Return _listValueCustomBankProperty
            End Get
            Set
                If MyBase.IsDeserializing Then
                    SetupSyncListValueCustomBankProperty(value)
                Else
                    SetSingleRelatedEntityNavigator(value, "ListCustomBankPropertySelectedValueCollection", "ListValueCustomBankProperty", _listValueCustomBankProperty, True)
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
                Return CInt(Questify.Builder.Model.ContentModel.EntityType.ListCustomBankPropertySelectedValueEntity)
            End Get
        End Property






    End Class
End Namespace
