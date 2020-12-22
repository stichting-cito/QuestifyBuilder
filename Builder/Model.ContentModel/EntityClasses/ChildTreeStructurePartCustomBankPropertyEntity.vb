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
    Public Class ChildTreeStructurePartCustomBankPropertyEntity
        Inherits CommonEntityBase



        Private WithEvents _treeStructurePartCustomBankProperty As TreeStructurePartCustomBankPropertyEntity



        Private Shared _customProperties As Dictionary(Of String, String)
        Private Shared _fieldsCustomProperties As Dictionary(Of String, Dictionary(Of String, String))

        Public NotInheritable Class MemberNames
            Private Sub New()
            End Sub
            Public Shared ReadOnly [TreeStructurePartCustomBankProperty] As String = "TreeStructurePartCustomBankProperty"
        End Class

        Shared Sub New()
            SetupCustomPropertyHashtables()
        End Sub

        Public Sub New()
            MyBase.New("ChildTreeStructurePartCustomBankPropertyEntity")
            InitClassEmpty(Nothing, Nothing)
        End Sub

        Public Sub New(fields As IEntityFields2)
            MyBase.New("ChildTreeStructurePartCustomBankPropertyEntity")
            InitClassEmpty(Nothing, fields)
        End Sub

        Public Sub New(validator As IValidator)
            MyBase.New("ChildTreeStructurePartCustomBankPropertyEntity")
            InitClassEmpty(validator, Nothing)
        End Sub

        Public Sub New(id As System.Guid, childTreeStructurePartCustomBankPropertyId As System.Guid, treeStructurePartCustomBankPropertyId As System.Guid)
            MyBase.New("ChildTreeStructurePartCustomBankPropertyEntity")
            InitClassEmpty(Nothing, Nothing)
            Me.Id = id
            Me.ChildTreeStructurePartCustomBankPropertyId = childTreeStructurePartCustomBankPropertyId
            Me.TreeStructurePartCustomBankPropertyId = treeStructurePartCustomBankPropertyId
        End Sub

        Public Sub New(id As System.Guid, childTreeStructurePartCustomBankPropertyId As System.Guid, treeStructurePartCustomBankPropertyId As System.Guid, validator As IValidator)
            MyBase.New("ChildTreeStructurePartCustomBankPropertyEntity")
            InitClassEmpty(validator, Nothing)
            Me.Id = id
            Me.ChildTreeStructurePartCustomBankPropertyId = childTreeStructurePartCustomBankPropertyId
            Me.TreeStructurePartCustomBankPropertyId = treeStructurePartCustomBankPropertyId
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Sub New(info As SerializationInfo, context As StreamingContext)
            MyBase.New(info, context)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                _treeStructurePartCustomBankProperty = CType(info.GetValue("_treeStructurePartCustomBankProperty", GetType(TreeStructurePartCustomBankPropertyEntity)), TreeStructurePartCustomBankPropertyEntity)
                If Not _treeStructurePartCustomBankProperty Is Nothing Then
                    AddHandler _treeStructurePartCustomBankProperty.AfterSave, AddressOf OnEntityAfterSave
                End If
                Me.FixupDeserialization(FieldInfoProviderSingleton.GetInstance())
            End If

        End Sub


        Protected Overrides Sub PerformDesyncSetupFKFieldChange(fieldIndex As Integer)
            Select Case CType(fieldIndex, ChildTreeStructurePartCustomBankPropertyFieldIndex)


                Case ChildTreeStructurePartCustomBankPropertyFieldIndex.TreeStructurePartCustomBankPropertyId
                    DesetupSyncTreeStructurePartCustomBankProperty(True, False)

                Case Else
                    MyBase.PerformDesyncSetupFKFieldChange(fieldIndex)
            End Select
        End Sub


        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub SetRelatedEntityProperty(propertyName As String, entity As IEntityCore)
            Select Case propertyName
                Case "TreeStructurePartCustomBankProperty"
                    Me.TreeStructurePartCustomBankProperty = CType(entity, TreeStructurePartCustomBankPropertyEntity)

                Case Else
                    Me.OnSetRelatedEntityProperty(propertyName, entity)
            End Select
        End Sub

        Protected Overrides Function GetRelationsForFieldOfType(fieldName As String) As RelationCollection
            Return ChildTreeStructurePartCustomBankPropertyEntity.GetRelationsForField(fieldName)
        End Function

        Friend Shared Function GetRelationsForField(fieldName As String) As RelationCollection
            Dim toReturn As New RelationCollection()
            Select Case fieldName
                Case "TreeStructurePartCustomBankProperty"
                    toReturn.Add(ChildTreeStructurePartCustomBankPropertyEntity.Relations.TreeStructurePartCustomBankPropertyEntityUsingTreeStructurePartCustomBankPropertyId)
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
                Case "TreeStructurePartCustomBankProperty"
                    SetupSyncTreeStructurePartCustomBankProperty(relatedEntity)

                Case Else
            End Select
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub UnsetRelatedEntity(relatedEntity As IEntityCore, fieldName As String, signalRelatedEntityManyToOne As Boolean)
            Select Case fieldName
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
                    entityValue = _treeStructurePartCustomBankProperty
                End If
                info.AddValue("_treeStructurePartCustomBankProperty", entityValue)
            End If

            MyBase.GetObjectData(info, context)
        End Sub


        Protected Overrides Overloads Function GetAllRelations() As List(Of IEntityRelation)
            Return New ChildTreeStructurePartCustomBankPropertyRelations().GetAllRelations()
        End Function

        Public Overridable Function GetRelationInfoTreeStructurePartCustomBankProperty() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(TreeStructurePartCustomBankPropertyFields.TreeStructurePartCustomBankPropertyId, Nothing, ComparisonOperator.Equal, Me.TreeStructurePartCustomBankPropertyId))
            Return bucket
        End Function

        Protected Overrides Function CreateEntityFactory() As IEntityFactory2
            Return EntityFactoryCache2.GetEntityFactory(GetType(ChildTreeStructurePartCustomBankPropertyEntityFactory))
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
            _fieldsCustomProperties.Add("Id", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("ChildTreeStructurePartCustomBankPropertyId", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("TreeStructurePartCustomBankPropertyId", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("VisualOrder", fieldHashtable)
        End Sub


        Private Sub DesetupSyncTreeStructurePartCustomBankProperty(signalRelatedEntity As Boolean, resetFKFields As Boolean)
            Me.PerformDesetupSyncRelatedEntity(_treeStructurePartCustomBankProperty, AddressOf OnTreeStructurePartCustomBankPropertyPropertyChanged, "TreeStructurePartCustomBankProperty", Questify.Builder.Model.ContentModel.RelationClasses.StaticChildTreeStructurePartCustomBankPropertyRelations.TreeStructurePartCustomBankPropertyEntityUsingTreeStructurePartCustomBankPropertyIdStatic, True, signalRelatedEntity, "ChildTreeStructurePartCustomBankPropertyCollection", resetFKFields, New Integer() {CInt(ChildTreeStructurePartCustomBankPropertyFieldIndex.TreeStructurePartCustomBankPropertyId)})
            _treeStructurePartCustomBankProperty = Nothing
        End Sub

        Private Sub SetupSyncTreeStructurePartCustomBankProperty(relatedEntity As IEntityCore)
            If Not _treeStructurePartCustomBankProperty Is relatedEntity Then
                DesetupSyncTreeStructurePartCustomBankProperty(True, True)
                _treeStructurePartCustomBankProperty = CType(relatedEntity, TreeStructurePartCustomBankPropertyEntity)
                Me.PerformSetupSyncRelatedEntity(_treeStructurePartCustomBankProperty, AddressOf OnTreeStructurePartCustomBankPropertyPropertyChanged, "TreeStructurePartCustomBankProperty", Questify.Builder.Model.ContentModel.RelationClasses.StaticChildTreeStructurePartCustomBankPropertyRelations.TreeStructurePartCustomBankPropertyEntityUsingTreeStructurePartCustomBankPropertyIdStatic, True, New String() {})
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

        Public Shared ReadOnly Property Relations() As ChildTreeStructurePartCustomBankPropertyRelations
            Get
                Return New ChildTreeStructurePartCustomBankPropertyRelations()
            End Get
        End Property

        Public Shared ReadOnly Property CustomProperties() As Dictionary(Of String, String)
            Get
                Return _customProperties
            End Get
        End Property




        Public Shared ReadOnly Property PrefetchPathTreeStructurePartCustomBankProperty() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(EntityFactoryCache2.GetEntityFactory(GetType(TreeStructurePartCustomBankPropertyEntityFactory))), _
                    CType(GetRelationsForField("TreeStructurePartCustomBankProperty")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.ChildTreeStructurePartCustomBankPropertyEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.TreeStructurePartCustomBankPropertyEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "TreeStructurePartCustomBankProperty", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne)
            End Get
        End Property


        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property CustomPropertiesOfType() As Dictionary(Of String, String)
            Get
                Return ChildTreeStructurePartCustomBankPropertyEntity.CustomProperties
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
                Return ChildTreeStructurePartCustomBankPropertyEntity.FieldsCustomProperties
            End Get
        End Property

        Public Overridable Property [Id]() As System.Guid
            Get
                Return CType(GetValue(CInt(ChildTreeStructurePartCustomBankPropertyFieldIndex.Id), True), System.Guid)
            End Get
            Set
                SetValue(CInt(ChildTreeStructurePartCustomBankPropertyFieldIndex.Id), value)
            End Set
        End Property
        Public Overridable Property [ChildTreeStructurePartCustomBankPropertyId]() As System.Guid
            Get
                Return CType(GetValue(CInt(ChildTreeStructurePartCustomBankPropertyFieldIndex.ChildTreeStructurePartCustomBankPropertyId), True), System.Guid)
            End Get
            Set
                SetValue(CInt(ChildTreeStructurePartCustomBankPropertyFieldIndex.ChildTreeStructurePartCustomBankPropertyId), value)
            End Set
        End Property
        Public Overridable Property [TreeStructurePartCustomBankPropertyId]() As System.Guid
            Get
                Return CType(GetValue(CInt(ChildTreeStructurePartCustomBankPropertyFieldIndex.TreeStructurePartCustomBankPropertyId), True), System.Guid)
            End Get
            Set
                SetValue(CInt(ChildTreeStructurePartCustomBankPropertyFieldIndex.TreeStructurePartCustomBankPropertyId), value)
            End Set
        End Property
        Public Overridable Property [VisualOrder]() As System.Int32
            Get
                Return CType(GetValue(CInt(ChildTreeStructurePartCustomBankPropertyFieldIndex.VisualOrder), True), System.Int32)
            End Get
            Set
                SetValue(CInt(ChildTreeStructurePartCustomBankPropertyFieldIndex.VisualOrder), value)
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
                    SetSingleRelatedEntityNavigator(value, "ChildTreeStructurePartCustomBankPropertyCollection", "TreeStructurePartCustomBankProperty", _treeStructurePartCustomBankProperty, True)
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
                Return CInt(Questify.Builder.Model.ContentModel.EntityType.ChildTreeStructurePartCustomBankPropertyEntity)
            End Get
        End Property






    End Class
End Namespace
