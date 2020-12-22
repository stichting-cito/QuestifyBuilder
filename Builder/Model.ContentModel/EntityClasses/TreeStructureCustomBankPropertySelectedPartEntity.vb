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
    Public Class TreeStructureCustomBankPropertySelectedPartEntity
        Inherits CommonEntityBase



        Private WithEvents _treeStructureCustomBankPropertyValue As TreeStructureCustomBankPropertyValueEntity
        Private WithEvents _treeStructurePartCustomBankProperty As TreeStructurePartCustomBankPropertyEntity



        Private Shared _customProperties As Dictionary(Of String, String)
        Private Shared _fieldsCustomProperties As Dictionary(Of String, Dictionary(Of String, String))

        Public NotInheritable Class MemberNames
            Private Sub New()
            End Sub
            Public Shared ReadOnly [TreeStructureCustomBankPropertyValue] As String = "TreeStructureCustomBankPropertyValue"
            Public Shared ReadOnly [TreeStructurePartCustomBankProperty] As String = "TreeStructurePartCustomBankProperty"
        End Class

        Shared Sub New()
            SetupCustomPropertyHashtables()
        End Sub

        Public Sub New()
            MyBase.New("TreeStructureCustomBankPropertySelectedPartEntity")
            InitClassEmpty(Nothing, Nothing)
        End Sub

        Public Sub New(fields As IEntityFields2)
            MyBase.New("TreeStructureCustomBankPropertySelectedPartEntity")
            InitClassEmpty(Nothing, fields)
        End Sub

        Public Sub New(validator As IValidator)
            MyBase.New("TreeStructureCustomBankPropertySelectedPartEntity")
            InitClassEmpty(validator, Nothing)
        End Sub

        Public Sub New(treeStructurePartId As System.Guid, resourceId As System.Guid, customBankPropertyId As System.Guid)
            MyBase.New("TreeStructureCustomBankPropertySelectedPartEntity")
            InitClassEmpty(Nothing, Nothing)
            Me.TreeStructurePartId = treeStructurePartId
            Me.ResourceId = resourceId
            Me.CustomBankPropertyId = customBankPropertyId
        End Sub

        Public Sub New(treeStructurePartId As System.Guid, resourceId As System.Guid, customBankPropertyId As System.Guid, validator As IValidator)
            MyBase.New("TreeStructureCustomBankPropertySelectedPartEntity")
            InitClassEmpty(validator, Nothing)
            Me.TreeStructurePartId = treeStructurePartId
            Me.ResourceId = resourceId
            Me.CustomBankPropertyId = customBankPropertyId
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Sub New(info As SerializationInfo, context As StreamingContext)
            MyBase.New(info, context)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                _treeStructureCustomBankPropertyValue = CType(info.GetValue("_treeStructureCustomBankPropertyValue", GetType(TreeStructureCustomBankPropertyValueEntity)), TreeStructureCustomBankPropertyValueEntity)
                If Not _treeStructureCustomBankPropertyValue Is Nothing Then
                    AddHandler _treeStructureCustomBankPropertyValue.AfterSave, AddressOf OnEntityAfterSave
                End If
                _treeStructurePartCustomBankProperty = CType(info.GetValue("_treeStructurePartCustomBankProperty", GetType(TreeStructurePartCustomBankPropertyEntity)), TreeStructurePartCustomBankPropertyEntity)
                If Not _treeStructurePartCustomBankProperty Is Nothing Then
                    AddHandler _treeStructurePartCustomBankProperty.AfterSave, AddressOf OnEntityAfterSave
                End If
                Me.FixupDeserialization(FieldInfoProviderSingleton.GetInstance())
            End If

        End Sub


        Protected Overrides Sub PerformDesyncSetupFKFieldChange(fieldIndex As Integer)
            Select Case CType(fieldIndex, TreeStructureCustomBankPropertySelectedPartFieldIndex)
                Case TreeStructureCustomBankPropertySelectedPartFieldIndex.TreeStructurePartId
                    DesetupSyncTreeStructurePartCustomBankProperty(True, False)
                Case TreeStructureCustomBankPropertySelectedPartFieldIndex.ResourceId
                    DesetupSyncTreeStructureCustomBankPropertyValue(True, False)
                Case TreeStructureCustomBankPropertySelectedPartFieldIndex.CustomBankPropertyId
                    DesetupSyncTreeStructureCustomBankPropertyValue(True, False)
                Case Else
                    MyBase.PerformDesyncSetupFKFieldChange(fieldIndex)
            End Select
        End Sub


        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub SetRelatedEntityProperty(propertyName As String, entity As IEntityCore)
            Select Case propertyName
                Case "TreeStructureCustomBankPropertyValue"
                    Me.TreeStructureCustomBankPropertyValue = CType(entity, TreeStructureCustomBankPropertyValueEntity)
                Case "TreeStructurePartCustomBankProperty"
                    Me.TreeStructurePartCustomBankProperty = CType(entity, TreeStructurePartCustomBankPropertyEntity)

                Case Else
                    Me.OnSetRelatedEntityProperty(propertyName, entity)
            End Select
        End Sub

        Protected Overrides Function GetRelationsForFieldOfType(fieldName As String) As RelationCollection
            Return TreeStructureCustomBankPropertySelectedPartEntity.GetRelationsForField(fieldName)
        End Function

        Friend Shared Function GetRelationsForField(fieldName As String) As RelationCollection
            Dim toReturn As New RelationCollection()
            Select Case fieldName
                Case "TreeStructureCustomBankPropertyValue"
                    toReturn.Add(TreeStructureCustomBankPropertySelectedPartEntity.Relations.TreeStructureCustomBankPropertyValueEntityUsingCustomBankPropertyIdResourceId)
                Case "TreeStructurePartCustomBankProperty"
                    toReturn.Add(TreeStructureCustomBankPropertySelectedPartEntity.Relations.TreeStructurePartCustomBankPropertyEntityUsingTreeStructurePartId)
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
                Case "TreeStructureCustomBankPropertyValue"
                    SetupSyncTreeStructureCustomBankPropertyValue(relatedEntity)
                Case "TreeStructurePartCustomBankProperty"
                    SetupSyncTreeStructurePartCustomBankProperty(relatedEntity)

                Case Else
            End Select
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub UnsetRelatedEntity(relatedEntity As IEntityCore, fieldName As String, signalRelatedEntityManyToOne As Boolean)
            Select Case fieldName
                Case "TreeStructureCustomBankPropertyValue"
                    DesetupSyncTreeStructureCustomBankPropertyValue(False, True)
                Case "TreeStructurePartCustomBankProperty"
                    DesetupSyncTreeStructurePartCustomBankProperty(False, True)
                Case Else
            End Select
        End Sub

        Protected Overrides Function GetDependingRelatedEntities() As List(Of IEntity2)
            Dim toReturn As New List(Of IEntity2)()
            Return toReturn
        End Function

        Protected Overrides Function GetDependentRelatedEntities() As List(Of IEntity2)
            Dim toReturn As New List(Of IEntity2)()
            If Not _treeStructureCustomBankPropertyValue Is Nothing Then
                toReturn.Add(_treeStructureCustomBankPropertyValue)
            End If
            If Not _treeStructurePartCustomBankProperty Is Nothing Then
                toReturn.Add(_treeStructurePartCustomBankProperty)
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
                    entityValue = _treeStructureCustomBankPropertyValue
                End If
                info.AddValue("_treeStructureCustomBankPropertyValue", entityValue)
                entityValue = Nothing
                If Not Me.MarkedForDeletion Then
                    entityValue = _treeStructurePartCustomBankProperty
                End If
                info.AddValue("_treeStructurePartCustomBankProperty", entityValue)
            End If

            MyBase.GetObjectData(info, context)
        End Sub


        Protected Overrides Overloads Function GetAllRelations() As List(Of IEntityRelation)
            Return New TreeStructureCustomBankPropertySelectedPartRelations().GetAllRelations()
        End Function

        Public Overridable Function GetRelationInfoTreeStructureCustomBankPropertyValue() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(TreeStructureCustomBankPropertyValueFields.CustomBankPropertyId, Nothing, ComparisonOperator.Equal, Me.CustomBankPropertyId))
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(TreeStructureCustomBankPropertyValueFields.ResourceId, Nothing, ComparisonOperator.Equal, Me.ResourceId))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoTreeStructurePartCustomBankProperty() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(TreeStructurePartCustomBankPropertyFields.TreeStructurePartCustomBankPropertyId, Nothing, ComparisonOperator.Equal, Me.TreeStructurePartId))
            Return bucket
        End Function

        Protected Overrides Function CreateEntityFactory() As IEntityFactory2
            Return EntityFactoryCache2.GetEntityFactory(GetType(TreeStructureCustomBankPropertySelectedPartEntityFactory))
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
            toReturn.Add("TreeStructureCustomBankPropertyValue", _treeStructureCustomBankPropertyValue)
            toReturn.Add("TreeStructurePartCustomBankProperty", _treeStructurePartCustomBankProperty)
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
            _fieldsCustomProperties.Add("TreeStructurePartId", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("ResourceId", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("CustomBankPropertyId", fieldHashtable)
        End Sub


        Private Sub DesetupSyncTreeStructureCustomBankPropertyValue(signalRelatedEntity As Boolean, resetFKFields As Boolean)
            Me.PerformDesetupSyncRelatedEntity(_treeStructureCustomBankPropertyValue, AddressOf OnTreeStructureCustomBankPropertyValuePropertyChanged, "TreeStructureCustomBankPropertyValue", Questify.Builder.Model.ContentModel.RelationClasses.StaticTreeStructureCustomBankPropertySelectedPartRelations.TreeStructureCustomBankPropertyValueEntityUsingCustomBankPropertyIdResourceIdStatic, True, signalRelatedEntity, "TreeStructureCustomBankPropertySelectedPartCollection", resetFKFields, New Integer() {CInt(TreeStructureCustomBankPropertySelectedPartFieldIndex.CustomBankPropertyId), CInt(TreeStructureCustomBankPropertySelectedPartFieldIndex.ResourceId)})
            _treeStructureCustomBankPropertyValue = Nothing
        End Sub

        Private Sub SetupSyncTreeStructureCustomBankPropertyValue(relatedEntity As IEntityCore)
            If Not _treeStructureCustomBankPropertyValue Is relatedEntity Then
                DesetupSyncTreeStructureCustomBankPropertyValue(True, True)
                _treeStructureCustomBankPropertyValue = CType(relatedEntity, TreeStructureCustomBankPropertyValueEntity)
                Me.PerformSetupSyncRelatedEntity(_treeStructureCustomBankPropertyValue, AddressOf OnTreeStructureCustomBankPropertyValuePropertyChanged, "TreeStructureCustomBankPropertyValue", Questify.Builder.Model.ContentModel.RelationClasses.StaticTreeStructureCustomBankPropertySelectedPartRelations.TreeStructureCustomBankPropertyValueEntityUsingCustomBankPropertyIdResourceIdStatic, True, New String() {})
            End If
        End Sub

        Private Sub OnTreeStructureCustomBankPropertyValuePropertyChanged(sender As Object, e As PropertyChangedEventArgs)
            Select Case e.PropertyName

                Case Else
            End Select
        End Sub

        Private Sub DesetupSyncTreeStructurePartCustomBankProperty(signalRelatedEntity As Boolean, resetFKFields As Boolean)
            Me.PerformDesetupSyncRelatedEntity(_treeStructurePartCustomBankProperty, AddressOf OnTreeStructurePartCustomBankPropertyPropertyChanged, "TreeStructurePartCustomBankProperty", Questify.Builder.Model.ContentModel.RelationClasses.StaticTreeStructureCustomBankPropertySelectedPartRelations.TreeStructurePartCustomBankPropertyEntityUsingTreeStructurePartIdStatic, True, signalRelatedEntity, "TreeStructureCustomBankPropertySelectedPartCollection", resetFKFields, New Integer() {CInt(TreeStructureCustomBankPropertySelectedPartFieldIndex.TreeStructurePartId)})
            _treeStructurePartCustomBankProperty = Nothing
        End Sub

        Private Sub SetupSyncTreeStructurePartCustomBankProperty(relatedEntity As IEntityCore)
            If Not _treeStructurePartCustomBankProperty Is relatedEntity Then
                DesetupSyncTreeStructurePartCustomBankProperty(True, True)
                _treeStructurePartCustomBankProperty = CType(relatedEntity, TreeStructurePartCustomBankPropertyEntity)
                Me.PerformSetupSyncRelatedEntity(_treeStructurePartCustomBankProperty, AddressOf OnTreeStructurePartCustomBankPropertyPropertyChanged, "TreeStructurePartCustomBankProperty", Questify.Builder.Model.ContentModel.RelationClasses.StaticTreeStructureCustomBankPropertySelectedPartRelations.TreeStructurePartCustomBankPropertyEntityUsingTreeStructurePartIdStatic, True, New String() {})
            End If
        End Sub

        Private Sub OnTreeStructurePartCustomBankPropertyPropertyChanged(sender As Object, e As PropertyChangedEventArgs)
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

        Public Shared ReadOnly Property Relations() As TreeStructureCustomBankPropertySelectedPartRelations
            Get
                Return New TreeStructureCustomBankPropertySelectedPartRelations()
            End Get
        End Property

        Public Shared ReadOnly Property CustomProperties() As Dictionary(Of String, String)
            Get
                Return _customProperties
            End Get
        End Property




        Public Shared ReadOnly Property PrefetchPathTreeStructureCustomBankPropertyValue() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(EntityFactoryCache2.GetEntityFactory(GetType(TreeStructureCustomBankPropertyValueEntityFactory))), _
                    CType(GetRelationsForField("TreeStructureCustomBankPropertyValue")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.TreeStructureCustomBankPropertySelectedPartEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.TreeStructureCustomBankPropertyValueEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "TreeStructureCustomBankPropertyValue", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathTreeStructurePartCustomBankProperty() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(EntityFactoryCache2.GetEntityFactory(GetType(TreeStructurePartCustomBankPropertyEntityFactory))), _
                    CType(GetRelationsForField("TreeStructurePartCustomBankProperty")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.TreeStructureCustomBankPropertySelectedPartEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.TreeStructurePartCustomBankPropertyEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "TreeStructurePartCustomBankProperty", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne)
            End Get
        End Property


        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property CustomPropertiesOfType() As Dictionary(Of String, String)
            Get
                Return TreeStructureCustomBankPropertySelectedPartEntity.CustomProperties
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
                Return TreeStructureCustomBankPropertySelectedPartEntity.FieldsCustomProperties
            End Get
        End Property

        Public Overridable Property [TreeStructurePartId]() As System.Guid
            Get
                Return CType(GetValue(CInt(TreeStructureCustomBankPropertySelectedPartFieldIndex.TreeStructurePartId), True), System.Guid)
            End Get
            Set
                SetValue(CInt(TreeStructureCustomBankPropertySelectedPartFieldIndex.TreeStructurePartId), value)
            End Set
        End Property
        Public Overridable Property [ResourceId]() As System.Guid
            Get
                Return CType(GetValue(CInt(TreeStructureCustomBankPropertySelectedPartFieldIndex.ResourceId), True), System.Guid)
            End Get
            Set
                SetValue(CInt(TreeStructureCustomBankPropertySelectedPartFieldIndex.ResourceId), value)
            End Set
        End Property
        Public Overridable Property [CustomBankPropertyId]() As System.Guid
            Get
                Return CType(GetValue(CInt(TreeStructureCustomBankPropertySelectedPartFieldIndex.CustomBankPropertyId), True), System.Guid)
            End Get
            Set
                SetValue(CInt(TreeStructureCustomBankPropertySelectedPartFieldIndex.CustomBankPropertyId), value)
            End Set
        End Property



        <Browsable(True)> _
        Public Overridable Property [TreeStructureCustomBankPropertyValue]() As TreeStructureCustomBankPropertyValueEntity
            Get
                Return _treeStructureCustomBankPropertyValue
            End Get
            Set
                If MyBase.IsDeserializing Then
                    SetupSyncTreeStructureCustomBankPropertyValue(value)
                Else
                    SetSingleRelatedEntityNavigator(value, "TreeStructureCustomBankPropertySelectedPartCollection", "TreeStructureCustomBankPropertyValue", _treeStructureCustomBankPropertyValue, True)
                End If
            End Set
        End Property

        <Browsable(True)> _
        Public Overridable Property [TreeStructurePartCustomBankProperty]() As TreeStructurePartCustomBankPropertyEntity
            Get
                Return _treeStructurePartCustomBankProperty
            End Get
            Set
                If MyBase.IsDeserializing Then
                    SetupSyncTreeStructurePartCustomBankProperty(value)
                Else
                    SetSingleRelatedEntityNavigator(value, "TreeStructureCustomBankPropertySelectedPartCollection", "TreeStructurePartCustomBankProperty", _treeStructurePartCustomBankProperty, True)
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
                Return CInt(Questify.Builder.Model.ContentModel.EntityType.TreeStructureCustomBankPropertySelectedPartEntity)
            End Get
        End Property






    End Class
End Namespace
