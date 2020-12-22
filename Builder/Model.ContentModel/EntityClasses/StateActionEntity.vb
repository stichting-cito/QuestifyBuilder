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
    Public Class StateActionEntity
        Inherits CommonEntityBase



        Private WithEvents _action As ActionEntity
        Private WithEvents _state As StateEntity



        Private Shared _customProperties As Dictionary(Of String, String)
        Private Shared _fieldsCustomProperties As Dictionary(Of String, Dictionary(Of String, String))

        Public NotInheritable Class MemberNames
            Private Sub New()
            End Sub
            Public Shared ReadOnly [Action] As String = "Action"
            Public Shared ReadOnly [State] As String = "State"
        End Class

        Shared Sub New()
            SetupCustomPropertyHashtables()
        End Sub

        Public Sub New()
            MyBase.New("StateActionEntity")
            InitClassEmpty(Nothing, Nothing)
        End Sub

        Public Sub New(fields As IEntityFields2)
            MyBase.New("StateActionEntity")
            InitClassEmpty(Nothing, fields)
        End Sub

        Public Sub New(validator As IValidator)
            MyBase.New("StateActionEntity")
            InitClassEmpty(validator, Nothing)
        End Sub

        Public Sub New(target As System.String, stateId As System.Int32, actionId As System.Int32)
            MyBase.New("StateActionEntity")
            InitClassEmpty(Nothing, Nothing)
            Me.Target = target
            Me.StateId = stateId
            Me.ActionId = actionId
        End Sub

        Public Sub New(target As System.String, stateId As System.Int32, actionId As System.Int32, validator As IValidator)
            MyBase.New("StateActionEntity")
            InitClassEmpty(validator, Nothing)
            Me.Target = target
            Me.StateId = stateId
            Me.ActionId = actionId
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Sub New(info As SerializationInfo, context As StreamingContext)
            MyBase.New(info, context)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                _action = CType(info.GetValue("_action", GetType(ActionEntity)), ActionEntity)
                If Not _action Is Nothing Then
                    AddHandler _action.AfterSave, AddressOf OnEntityAfterSave
                End If
                _state = CType(info.GetValue("_state", GetType(StateEntity)), StateEntity)
                If Not _state Is Nothing Then
                    AddHandler _state.AfterSave, AddressOf OnEntityAfterSave
                End If
                Me.FixupDeserialization(FieldInfoProviderSingleton.GetInstance())
            End If

        End Sub


        Protected Overrides Sub PerformDesyncSetupFKFieldChange(fieldIndex As Integer)
            Select Case CType(fieldIndex, StateActionFieldIndex)

                Case StateActionFieldIndex.StateId
                    DesetupSyncState(True, False)
                Case StateActionFieldIndex.ActionId
                    DesetupSyncAction(True, False)
                Case Else
                    MyBase.PerformDesyncSetupFKFieldChange(fieldIndex)
            End Select
        End Sub


        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub SetRelatedEntityProperty(propertyName As String, entity As IEntityCore)
            Select Case propertyName
                Case "Action"
                    Me.Action = CType(entity, ActionEntity)
                Case "State"
                    Me.State = CType(entity, StateEntity)

                Case Else
                    Me.OnSetRelatedEntityProperty(propertyName, entity)
            End Select
        End Sub

        Protected Overrides Function GetRelationsForFieldOfType(fieldName As String) As RelationCollection
            Return StateActionEntity.GetRelationsForField(fieldName)
        End Function

        Friend Shared Function GetRelationsForField(fieldName As String) As RelationCollection
            Dim toReturn As New RelationCollection()
            Select Case fieldName
                Case "Action"
                    toReturn.Add(StateActionEntity.Relations.ActionEntityUsingActionId)
                Case "State"
                    toReturn.Add(StateActionEntity.Relations.StateEntityUsingStateId)
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
                Case "Action"
                    SetupSyncAction(relatedEntity)
                Case "State"
                    SetupSyncState(relatedEntity)

                Case Else
            End Select
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub UnsetRelatedEntity(relatedEntity As IEntityCore, fieldName As String, signalRelatedEntityManyToOne As Boolean)
            Select Case fieldName
                Case "Action"
                    DesetupSyncAction(False, True)
                Case "State"
                    DesetupSyncState(False, True)
                Case Else
            End Select
        End Sub

        Protected Overrides Function GetDependingRelatedEntities() As List(Of IEntity2)
            Dim toReturn As New List(Of IEntity2)()
            Return toReturn
        End Function

        Protected Overrides Function GetDependentRelatedEntities() As List(Of IEntity2)
            Dim toReturn As New List(Of IEntity2)()
            If Not _action Is Nothing Then
                toReturn.Add(_action)
            End If
            If Not _state Is Nothing Then
                toReturn.Add(_state)
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
                    entityValue = _action
                End If
                info.AddValue("_action", entityValue)
                entityValue = Nothing
                If Not Me.MarkedForDeletion Then
                    entityValue = _state
                End If
                info.AddValue("_state", entityValue)
            End If

            MyBase.GetObjectData(info, context)
        End Sub


        Protected Overrides Overloads Function GetAllRelations() As List(Of IEntityRelation)
            Return New StateActionRelations().GetAllRelations()
        End Function

        Public Overridable Function GetRelationInfoAction() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(ActionFields.ActionId, Nothing, ComparisonOperator.Equal, Me.ActionId))
            Return bucket
        End Function

        Public Overridable Function GetRelationInfoState() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(StateFields.StateId, Nothing, ComparisonOperator.Equal, Me.StateId))
            Return bucket
        End Function

        Protected Overrides Function CreateEntityFactory() As IEntityFactory2
            Return EntityFactoryCache2.GetEntityFactory(GetType(StateActionEntityFactory))
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
            toReturn.Add("Action", _action)
            toReturn.Add("State", _state)
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
            _fieldsCustomProperties.Add("Target", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("StateId", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("ActionId", fieldHashtable)
        End Sub


        Private Sub DesetupSyncAction(signalRelatedEntity As Boolean, resetFKFields As Boolean)
            Me.PerformDesetupSyncRelatedEntity(_action, AddressOf OnActionPropertyChanged, "Action", Questify.Builder.Model.ContentModel.RelationClasses.StaticStateActionRelations.ActionEntityUsingActionIdStatic, True, signalRelatedEntity, "StateActionCollection", resetFKFields, New Integer() {CInt(StateActionFieldIndex.ActionId)})
            _action = Nothing
        End Sub

        Private Sub SetupSyncAction(relatedEntity As IEntityCore)
            If Not _action Is relatedEntity Then
                DesetupSyncAction(True, True)
                _action = CType(relatedEntity, ActionEntity)
                Me.PerformSetupSyncRelatedEntity(_action, AddressOf OnActionPropertyChanged, "Action", Questify.Builder.Model.ContentModel.RelationClasses.StaticStateActionRelations.ActionEntityUsingActionIdStatic, True, New String() {})
            End If
        End Sub

        Private Sub OnActionPropertyChanged(sender As Object, e As PropertyChangedEventArgs)
            Select Case e.PropertyName

                Case Else
            End Select
        End Sub

        Private Sub DesetupSyncState(signalRelatedEntity As Boolean, resetFKFields As Boolean)
            Me.PerformDesetupSyncRelatedEntity(_state, AddressOf OnStatePropertyChanged, "State", Questify.Builder.Model.ContentModel.RelationClasses.StaticStateActionRelations.StateEntityUsingStateIdStatic, True, signalRelatedEntity, "StateActionCollection", resetFKFields, New Integer() {CInt(StateActionFieldIndex.StateId)})
            _state = Nothing
        End Sub

        Private Sub SetupSyncState(relatedEntity As IEntityCore)
            If Not _state Is relatedEntity Then
                DesetupSyncState(True, True)
                _state = CType(relatedEntity, StateEntity)
                Me.PerformSetupSyncRelatedEntity(_state, AddressOf OnStatePropertyChanged, "State", Questify.Builder.Model.ContentModel.RelationClasses.StaticStateActionRelations.StateEntityUsingStateIdStatic, True, New String() {})
            End If
        End Sub

        Private Sub OnStatePropertyChanged(sender As Object, e As PropertyChangedEventArgs)
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

        Public Shared ReadOnly Property Relations() As StateActionRelations
            Get
                Return New StateActionRelations()
            End Get
        End Property

        Public Shared ReadOnly Property CustomProperties() As Dictionary(Of String, String)
            Get
                Return _customProperties
            End Get
        End Property




        Public Shared ReadOnly Property PrefetchPathAction() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(EntityFactoryCache2.GetEntityFactory(GetType(ActionEntityFactory))), _
                    CType(GetRelationsForField("Action")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.StateActionEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.ActionEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "Action", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne)
            End Get
        End Property

        Public Shared ReadOnly Property PrefetchPathState() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(EntityFactoryCache2.GetEntityFactory(GetType(StateEntityFactory))), _
                    CType(GetRelationsForField("State")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.StateActionEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.StateEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "State", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne)
            End Get
        End Property


        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property CustomPropertiesOfType() As Dictionary(Of String, String)
            Get
                Return StateActionEntity.CustomProperties
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
                Return StateActionEntity.FieldsCustomProperties
            End Get
        End Property

        Public Overridable Property [Target]() As System.String
            Get
                Return CType(GetValue(CInt(StateActionFieldIndex.Target), True), System.String)
            End Get
            Set
                SetValue(CInt(StateActionFieldIndex.Target), value)
            End Set
        End Property
        Public Overridable Property [StateId]() As System.Int32
            Get
                Return CType(GetValue(CInt(StateActionFieldIndex.StateId), True), System.Int32)
            End Get
            Set
                SetValue(CInt(StateActionFieldIndex.StateId), value)
            End Set
        End Property
        Public Overridable Property [ActionId]() As System.Int32
            Get
                Return CType(GetValue(CInt(StateActionFieldIndex.ActionId), True), System.Int32)
            End Get
            Set
                SetValue(CInt(StateActionFieldIndex.ActionId), value)
            End Set
        End Property



        <Browsable(True)> _
        Public Overridable Property [Action]() As ActionEntity
            Get
                Return _action
            End Get
            Set
                If MyBase.IsDeserializing Then
                    SetupSyncAction(value)
                Else
                    SetSingleRelatedEntityNavigator(value, "StateActionCollection", "Action", _action, True)
                End If
            End Set
        End Property

        <Browsable(True)> _
        Public Overridable Property [State]() As StateEntity
            Get
                Return _state
            End Get
            Set
                If MyBase.IsDeserializing Then
                    SetupSyncState(value)
                Else
                    SetSingleRelatedEntityNavigator(value, "StateActionCollection", "State", _state, True)
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
                Return CInt(Questify.Builder.Model.ContentModel.EntityType.StateActionEntity)
            End Get
        End Property






    End Class
End Namespace
