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
    Public Class ActionEntity
        Inherits CommonEntityBase



        Private WithEvents _stateActionCollection As EntityCollection(Of StateActionEntity)
        Private WithEvents _stateCollectionViaStateAction As EntityCollection(Of StateEntity)



        Private Shared _customProperties As Dictionary(Of String, String)
        Private Shared _fieldsCustomProperties As Dictionary(Of String, Dictionary(Of String, String))

        Public NotInheritable Class MemberNames
            Private Sub New()
            End Sub
            Public Shared ReadOnly [StateActionCollection] As String = "StateActionCollection"
            Public Shared ReadOnly [StateCollectionViaStateAction] As String = "StateCollectionViaStateAction"
        End Class

        Shared Sub New()
            SetupCustomPropertyHashtables()
        End Sub

        Public Sub New()
            MyBase.New("ActionEntity")
            InitClassEmpty(Nothing, Nothing)
        End Sub

        Public Sub New(fields As IEntityFields2)
            MyBase.New("ActionEntity")
            InitClassEmpty(Nothing, fields)
        End Sub

        Public Sub New(validator As IValidator)
            MyBase.New("ActionEntity")
            InitClassEmpty(validator, Nothing)
        End Sub

        Public Sub New(actionId As System.Int32)
            MyBase.New("ActionEntity")
            InitClassEmpty(Nothing, Nothing)
            Me.ActionId = actionId
        End Sub

        Public Sub New(actionId As System.Int32, validator As IValidator)
            MyBase.New("ActionEntity")
            InitClassEmpty(validator, Nothing)
            Me.ActionId = actionId
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Sub New(info As SerializationInfo, context As StreamingContext)
            MyBase.New(info, context)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                _stateActionCollection = CType(info.GetValue("_stateActionCollection", GetType(EntityCollection(Of StateActionEntity))), EntityCollection(Of StateActionEntity))
                _stateCollectionViaStateAction = CType(info.GetValue("_stateCollectionViaStateAction", GetType(EntityCollection(Of StateEntity))), EntityCollection(Of StateEntity))
                Me.FixupDeserialization(FieldInfoProviderSingleton.GetInstance())
            End If

        End Sub



        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub SetRelatedEntityProperty(propertyName As String, entity As IEntityCore)
            Select Case propertyName
                Case "StateActionCollection"
                    Me.StateActionCollection.Add(CType(entity, StateActionEntity))
                Case "StateCollectionViaStateAction"
                    Me.StateCollectionViaStateAction.IsReadOnly = False
                    Me.StateCollectionViaStateAction.Add(CType(entity, StateEntity))
                    Me.StateCollectionViaStateAction.IsReadOnly = True

                Case Else
                    Me.OnSetRelatedEntityProperty(propertyName, entity)
            End Select
        End Sub

        Protected Overrides Function GetRelationsForFieldOfType(fieldName As String) As RelationCollection
            Return ActionEntity.GetRelationsForField(fieldName)
        End Function

        Friend Shared Function GetRelationsForField(fieldName As String) As RelationCollection
            Dim toReturn As New RelationCollection()
            Select Case fieldName
                Case "StateActionCollection"
                    toReturn.Add(ActionEntity.Relations.StateActionEntityUsingActionId)
                Case "StateCollectionViaStateAction"
                    toReturn.Add(ActionEntity.Relations.StateActionEntityUsingActionId, "ActionEntity__", "StateAction_", JoinHint.None)
                    toReturn.Add(StateActionEntity.Relations.StateEntityUsingStateId, "StateAction_", String.Empty, JoinHint.None)
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
                Case "StateActionCollection"
                    Me.StateActionCollection.Add(CType(relatedEntity, StateActionEntity))

                Case Else
            End Select
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub UnsetRelatedEntity(relatedEntity As IEntityCore, fieldName As String, signalRelatedEntityManyToOne As Boolean)
            Select Case fieldName
                Case "StateActionCollection"
                    Me.PerformRelatedEntityRemoval(Me.StateActionCollection, relatedEntity, signalRelatedEntityManyToOne)
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
            toReturn.Add(Me.StateActionCollection)
            Return toReturn
        End Function


        Protected Overrides Sub GetObjectData(info As SerializationInfo, context As StreamingContext)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                Dim value As IEntityCollection2 = Nothing
                Dim entityValue As IEntity2 = Nothing
                value = Nothing
                If (Not (_stateActionCollection Is Nothing)) AndAlso (_stateActionCollection.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _stateActionCollection
                End If
                info.AddValue("_stateActionCollection", value)
                value = Nothing
                If (Not (_stateCollectionViaStateAction Is Nothing)) AndAlso (_stateCollectionViaStateAction.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _stateCollectionViaStateAction
                End If
                info.AddValue("_stateCollectionViaStateAction", value)
            End If

            MyBase.GetObjectData(info, context)
        End Sub


        Protected Overrides Overloads Function GetAllRelations() As List(Of IEntityRelation)
            Return New ActionRelations().GetAllRelations()
        End Function

        Public Overridable Function GetRelationInfoStateActionCollection() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(StateActionFields.ActionId, Nothing, ComparisonOperator.Equal, Me.ActionId))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoStateCollectionViaStateAction() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.Relations.AddRange(GetRelationsForFieldOfType("StateCollectionViaStateAction"))
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(ActionFields.ActionId, Nothing, ComparisonOperator.Equal, Me.ActionId, "ActionEntity__"))
            Return bucket
        End Function

        Protected Overrides Function CreateEntityFactory() As IEntityFactory2
            Return EntityFactoryCache2.GetEntityFactory(GetType(ActionEntityFactory))
        End Function
#If Not CF Then
        Protected Overrides Sub AddToMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2))
            MyBase.AddToMemberEntityCollectionsQueue(collectionsQueue)
            collectionsQueue.Enqueue(_stateActionCollection)
            collectionsQueue.Enqueue(_stateCollectionViaStateAction)
        End Sub

        Protected Overrides Sub GetFromMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2))
            MyBase.GetFromMemberEntityCollectionsQueue(collectionsQueue)
            _stateActionCollection = CType(collectionsQueue.Dequeue(), EntityCollection(Of StateActionEntity))
            _stateCollectionViaStateAction = CType(collectionsQueue.Dequeue(), EntityCollection(Of StateEntity))
        End Sub

        Protected Overrides Function HasPopulatedMemberEntityCollections() As Boolean
            If (Not _stateActionCollection Is Nothing) Then
                Return True
            End If
            If (Not _stateCollectionViaStateAction Is Nothing) Then
                Return True
            End If
            Return MyBase.HasPopulatedMemberEntityCollections()
        End Function

        Protected Overrides Overloads Sub CreateMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2), requiredQueue As Queue(Of Boolean))
            MyBase.CreateMemberEntityCollectionsQueue(collectionsQueue, requiredQueue)
            Dim toAdd As IEntityCollection2 = Nothing
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of StateActionEntity)(EntityFactoryCache2.GetEntityFactory(GetType(StateActionEntityFactory)))
            Else
                toAdd = Nothing
            End If
            collectionsQueue.Enqueue(toAdd)
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of StateEntity)(EntityFactoryCache2.GetEntityFactory(GetType(StateEntityFactory)))
            Else
                toAdd = Nothing
            End If
            collectionsQueue.Enqueue(toAdd)
        End Sub
#End If
        Protected Overrides Overloads Function GetRelatedData() As Dictionary(Of String, Object)
            Dim toReturn As New Dictionary(Of String, Object)()
            toReturn.Add("StateActionCollection", _stateActionCollection)
            toReturn.Add("StateCollectionViaStateAction", _stateCollectionViaStateAction)
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
            _fieldsCustomProperties.Add("ActionId", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("Name", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("Title", fieldHashtable)
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

        Public Shared ReadOnly Property Relations() As ActionRelations
            Get
                Return New ActionRelations()
            End Get
        End Property

        Public Shared ReadOnly Property CustomProperties() As Dictionary(Of String, String)
            Get
                Return _customProperties
            End Get
        End Property


        Public Shared ReadOnly Property PrefetchPathStateActionCollection() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(Of StateActionEntity)(EntityFactoryCache2.GetEntityFactory(GetType(StateActionEntityFactory))), _
                    CType(GetRelationsForField("StateActionCollection")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.ActionEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.StateActionEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "StateActionCollection", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathStateCollectionViaStateAction() As IPrefetchPathElement2
            Get
                Dim intermediateRelation As IEntityRelation = ActionEntity.Relations.StateActionEntityUsingActionId
                intermediateRelation.SetAliases(String.Empty, "StateAction_")
                Return New PrefetchPathElement2(New EntityCollection(Of StateEntity)(EntityFactoryCache2.GetEntityFactory(GetType(StateEntityFactory))), _
                    intermediateRelation, CType(Questify.Builder.Model.ContentModel.EntityType.ActionEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.StateEntity, Integer), 0, Nothing, Nothing, GetRelationsForField("StateCollectionViaStateAction"), Nothing, "StateCollectionViaStateAction", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToMany)
            End Get
        End Property



        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property CustomPropertiesOfType() As Dictionary(Of String, String)
            Get
                Return ActionEntity.CustomProperties
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
                Return ActionEntity.FieldsCustomProperties
            End Get
        End Property

        Public Overridable Property [ActionId]() As System.Int32
            Get
                Return CType(GetValue(CInt(ActionFieldIndex.ActionId), True), System.Int32)
            End Get
            Set
                SetValue(CInt(ActionFieldIndex.ActionId), value)
            End Set
        End Property
        Public Overridable Property [Name]() As System.String
            Get
                Return CType(GetValue(CInt(ActionFieldIndex.Name), True), System.String)
            End Get
            Set
                SetValue(CInt(ActionFieldIndex.Name), value)
            End Set
        End Property
        Public Overridable Property [Title]() As System.String
            Get
                Return CType(GetValue(CInt(ActionFieldIndex.Title), True), System.String)
            End Get
            Set
                SetValue(CInt(ActionFieldIndex.Title), value)
            End Set
        End Property

        <TypeContainedAttribute(GetType(StateActionEntity))> _
        Public Overridable ReadOnly Property [StateActionCollection]() As EntityCollection(Of StateActionEntity)
            Get
                If _stateActionCollection Is Nothing Then
                    _stateActionCollection = New EntityCollection(Of StateActionEntity)(EntityFactoryCache2.GetEntityFactory(GetType(StateActionEntityFactory)))
                    _stateActionCollection.ActiveContext = Me.ActiveContext
                    _stateActionCollection.SetContainingEntityInfo(Me, "Action")
                End If
                Return _stateActionCollection
            End Get
        End Property

        <TypeContainedAttribute(GetType(StateEntity))> _
        Public Overridable ReadOnly Property [StateCollectionViaStateAction]() As EntityCollection(Of StateEntity)
            Get
                If _stateCollectionViaStateAction Is Nothing Then
                    _stateCollectionViaStateAction = New EntityCollection(Of StateEntity)(EntityFactoryCache2.GetEntityFactory(GetType(StateEntityFactory)))
                    _stateCollectionViaStateAction.ActiveContext = Me.ActiveContext
                    _stateCollectionViaStateAction.IsReadOnly = True
                    CType(_stateCollectionViaStateAction, IEntityCollectionCore).IsForMN = True
                End If
                Return _stateCollectionViaStateAction
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
                Return CInt(Questify.Builder.Model.ContentModel.EntityType.ActionEntity)
            End Get
        End Property






    End Class
End Namespace
