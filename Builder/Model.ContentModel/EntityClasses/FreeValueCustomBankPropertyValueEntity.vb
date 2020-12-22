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
    Public Class FreeValueCustomBankPropertyValueEntity
        Inherits CustomBankPropertyValueEntity



        Private WithEvents _freeValueCustomBankProperty As FreeValueCustomBankPropertyEntity



        Private Shared _customProperties As Dictionary(Of String, String)
        Private Shared _fieldsCustomProperties As Dictionary(Of String, Dictionary(Of String, String))

        Public Shadows NotInheritable Class MemberNames
            Private Sub New()
            End Sub
            Public Shared ReadOnly [CustomBankProperty] As String = "CustomBankProperty"
            Public Shared ReadOnly [FreeValueCustomBankProperty] As String = "FreeValueCustomBankProperty"
            Public Shared ReadOnly [Resource] As String = "Resource"
        End Class

        Shared Sub New()
            SetupCustomPropertyHashtables()
        End Sub

        Public Sub New()
            MyBase.New()
            InitClassEmpty()
            SetName("FreeValueCustomBankPropertyValueEntity")
        End Sub

        Public Sub New(fields As IEntityFields2)
            MyBase.New(fields)
            InitClassEmpty()
            SetName("FreeValueCustomBankPropertyValueEntity")
        End Sub

        Public Sub New(validator As IValidator)
            MyBase.New(validator)
            InitClassEmpty()
            SetName("FreeValueCustomBankPropertyValueEntity")
        End Sub

        Public Sub New(resourceId As System.Guid, customBankPropertyId As System.Guid)
            MyBase.New(resourceId, customBankPropertyId)
            InitClassEmpty()

            SetName("FreeValueCustomBankPropertyValueEntity")
        End Sub

        Public Sub New(resourceId As System.Guid, customBankPropertyId As System.Guid, validator As IValidator)
            MyBase.New(resourceId, customBankPropertyId, validator)
            InitClassEmpty()

            SetName("FreeValueCustomBankPropertyValueEntity")
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Sub New(info As SerializationInfo, context As StreamingContext)
            MyBase.New(info, context)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                _freeValueCustomBankProperty = CType(info.GetValue("_freeValueCustomBankProperty", GetType(FreeValueCustomBankPropertyEntity)), FreeValueCustomBankPropertyEntity)
                If Not _freeValueCustomBankProperty Is Nothing Then
                    AddHandler _freeValueCustomBankProperty.AfterSave, AddressOf OnEntityAfterSave
                End If
                Me.FixupDeserialization(FieldInfoProviderSingleton.GetInstance())
            End If

        End Sub



        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub SetRelatedEntityProperty(propertyName As String, entity As IEntityCore)
            Select Case propertyName
                Case "FreeValueCustomBankProperty"
                    Me.FreeValueCustomBankProperty = CType(entity, FreeValueCustomBankPropertyEntity)

                Case Else
                    MyBase.SetRelatedEntityProperty(propertyName, entity)
            End Select
        End Sub

        Protected Overrides Function GetRelationsForFieldOfType(fieldName As String) As RelationCollection
            Return FreeValueCustomBankPropertyValueEntity.GetRelationsForField(fieldName)
        End Function

        Friend Shared Shadows Function GetRelationsForField(fieldName As String) As RelationCollection
            Dim toReturn As New RelationCollection()
            Select Case fieldName
                Case "FreeValueCustomBankProperty"
                    toReturn.Add(FreeValueCustomBankPropertyValueEntity.Relations.FreeValueCustomBankPropertyEntityUsingCustomBankPropertyId)
                Case Else
                    toReturn = CustomBankPropertyValueEntity.GetRelationsForField(fieldName)
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
                Case "FreeValueCustomBankProperty"
                    SetupSyncFreeValueCustomBankProperty(relatedEntity)

                Case Else
                    MyBase.SetRelatedEntity(relatedEntity, fieldName)
            End Select
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub UnsetRelatedEntity(relatedEntity As IEntityCore, fieldName As String, signalRelatedEntityManyToOne As Boolean)
            Select Case fieldName
                Case "FreeValueCustomBankProperty"
                    DesetupSyncFreeValueCustomBankProperty(False, True)
                Case Else
                    MyBase.UnsetRelatedEntity(relatedEntity, fieldName, signalRelatedEntityManyToOne)
            End Select
        End Sub

        Protected Overrides Function GetDependingRelatedEntities() As List(Of IEntity2)
            Dim toReturn As New List(Of IEntity2)()
            toReturn.AddRange(MyBase.GetDependingRelatedEntities())
            Return toReturn
        End Function

        Protected Overrides Function GetDependentRelatedEntities() As List(Of IEntity2)
            Dim toReturn As New List(Of IEntity2)()
            If Not _freeValueCustomBankProperty Is Nothing Then
                toReturn.Add(_freeValueCustomBankProperty)
            End If
            toReturn.AddRange(MyBase.GetDependentRelatedEntities())
            Return toReturn
        End Function

        Protected Overrides Function GetMemberEntityCollections() As List(Of IEntityCollection2)
            Dim toReturn As New List(Of IEntityCollection2)()
            toReturn.AddRange(MyBase.GetMemberEntityCollections())
            Return toReturn
        End Function

        Public Shadows Shared Function GetEntityTypeFilter() As IPredicateExpression
            Return InheritanceInfoProviderSingleton.GetInstance().GetEntityTypeFilter("FreeValueCustomBankPropertyValueEntity", False)
        End Function

        Public Shadows Shared Function GetEntityTypeFilter(negate As Boolean) As IPredicateExpression
            Return InheritanceInfoProviderSingleton.GetInstance().GetEntityTypeFilter("FreeValueCustomBankPropertyValueEntity", negate)
        End Function

        Protected Overrides Sub GetObjectData(info As SerializationInfo, context As StreamingContext)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                Dim value As IEntityCollection2 = Nothing
                Dim entityValue As IEntity2 = Nothing
                entityValue = Nothing
                If Not Me.MarkedForDeletion Then
                    entityValue = _freeValueCustomBankProperty
                End If
                info.AddValue("_freeValueCustomBankProperty", entityValue)
            End If

            MyBase.GetObjectData(info, context)
        End Sub

        Public Function ConstructFilterForUCFreeValueCustomBankPropertyValueId() As IPredicateExpression
            Dim Filter As IPredicateExpression = New PredicateExpression()
            Filter.Add(Questify.Builder.Model.ContentModel.HelperClasses.FreeValueCustomBankPropertyValueFields.FreeValueCustomBankPropertyValueId = Me.Fields.GetCurrentValue(CInt(FreeValueCustomBankPropertyValueFieldIndex.FreeValueCustomBankPropertyValueId)))
            Return Filter
        End Function

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Function CheckIfIsSubTypeOf(typeOfEntity As Integer) As Boolean
            Return InheritanceInfoProviderSingleton.GetInstance().CheckIfIsSubTypeOf("FreeValueCustomBankPropertyValueEntity", CType(typeOfEntity, Questify.Builder.Model.ContentModel.EntityType).ToString())
        End Function

        Protected Overrides Overloads Function GetAllRelations() As List(Of IEntityRelation)
            Return New FreeValueCustomBankPropertyValueRelations().GetAllRelations()
        End Function

        Public Overridable Function GetRelationInfoFreeValueCustomBankProperty() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(FreeValueCustomBankPropertyFields.CustomBankPropertyId, Nothing, ComparisonOperator.Equal, Me.CustomBankPropertyId))
            Return bucket
        End Function

        Protected Overrides Function CreateEntityFactory() As IEntityFactory2
            Return EntityFactoryCache2.GetEntityFactory(GetType(FreeValueCustomBankPropertyValueEntityFactory))
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
            Dim toReturn As Dictionary(Of String, Object) = MyBase.GetRelatedData()
            toReturn.Add("FreeValueCustomBankProperty", _freeValueCustomBankProperty)
            Return toReturn
        End Function

        Private Sub InitClassMembers()


        End Sub

        Private Shared Sub SetupCustomPropertyHashtables()
            _customProperties = New Dictionary(Of String, String)()
            _fieldsCustomProperties = New Dictionary(Of String, Dictionary(Of String, String))()
            Dim fieldHashtable As Dictionary(Of String, String)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("Value", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("FreeValueCustomBankPropertyValueId", fieldHashtable)
        End Sub


        Private Sub DesetupSyncFreeValueCustomBankProperty(signalRelatedEntity As Boolean, resetFKFields As Boolean)
            Me.PerformDesetupSyncRelatedEntity(_freeValueCustomBankProperty, AddressOf OnFreeValueCustomBankPropertyPropertyChanged, "FreeValueCustomBankProperty", Questify.Builder.Model.ContentModel.RelationClasses.StaticFreeValueCustomBankPropertyValueRelations.FreeValueCustomBankPropertyEntityUsingCustomBankPropertyIdStatic, True, signalRelatedEntity, "FreeValueCustomBankPropertyValueCollection", resetFKFields, New Integer() {CInt(FreeValueCustomBankPropertyValueFieldIndex.CustomBankPropertyId)})
            _freeValueCustomBankProperty = Nothing
        End Sub

        Private Sub SetupSyncFreeValueCustomBankProperty(relatedEntity As IEntityCore)
            If Not _freeValueCustomBankProperty Is relatedEntity Then
                DesetupSyncFreeValueCustomBankProperty(True, True)
                _freeValueCustomBankProperty = CType(relatedEntity, FreeValueCustomBankPropertyEntity)
                Me.PerformSetupSyncRelatedEntity(_freeValueCustomBankProperty, AddressOf OnFreeValueCustomBankPropertyPropertyChanged, "FreeValueCustomBankProperty", Questify.Builder.Model.ContentModel.RelationClasses.StaticFreeValueCustomBankPropertyValueRelations.FreeValueCustomBankPropertyEntityUsingCustomBankPropertyIdStatic, True, New String() {})
            End If
        End Sub

        Private Sub OnFreeValueCustomBankPropertyPropertyChanged(sender As Object, e As PropertyChangedEventArgs)
            Select Case e.PropertyName

                Case Else
            End Select
        End Sub



        Private Sub InitClassEmpty()
            InitClassMembers()




        End Sub

        Public Shadows Shared ReadOnly Property Relations() As FreeValueCustomBankPropertyValueRelations
            Get
                Return New FreeValueCustomBankPropertyValueRelations()
            End Get
        End Property

        Public Shadows Shared ReadOnly Property CustomProperties() As Dictionary(Of String, String)
            Get
                Return _customProperties
            End Get
        End Property




        Public Shared ReadOnly Property PrefetchPathFreeValueCustomBankProperty() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(EntityFactoryCache2.GetEntityFactory(GetType(FreeValueCustomBankPropertyEntityFactory))), _
                    CType(GetRelationsForField("FreeValueCustomBankProperty")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.FreeValueCustomBankPropertyValueEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.FreeValueCustomBankPropertyEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "FreeValueCustomBankProperty", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne)
            End Get
        End Property


        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property CustomPropertiesOfType() As Dictionary(Of String, String)
            Get
                Return FreeValueCustomBankPropertyValueEntity.CustomProperties
            End Get
        End Property

        Public Shadows Shared ReadOnly Property FieldsCustomProperties() As Dictionary(Of String, Dictionary(Of String, String))
            Get
                Return _fieldsCustomProperties
            End Get
        End Property

        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property FieldsCustomPropertiesOfType() As Dictionary(Of String, Dictionary(Of String, String))
            Get
                Return FreeValueCustomBankPropertyValueEntity.FieldsCustomProperties
            End Get
        End Property

        Public Overridable Property [Value]() As System.String
            Get
                Return CType(GetValue(CInt(FreeValueCustomBankPropertyValueFieldIndex.Value), True), System.String)
            End Get
            Set
                SetValue(CInt(FreeValueCustomBankPropertyValueFieldIndex.Value), value)
            End Set
        End Property
        Public Overridable ReadOnly Property [FreeValueCustomBankPropertyValueId]() As System.Int32
            Get
                Return CType(GetValue(CInt(FreeValueCustomBankPropertyValueFieldIndex.FreeValueCustomBankPropertyValueId), True), System.Int32)
            End Get

        End Property



        <Browsable(True)> _
        Public Overridable Property [FreeValueCustomBankProperty]() As FreeValueCustomBankPropertyEntity
            Get
                Return _freeValueCustomBankProperty
            End Get
            Set
                If MyBase.IsDeserializing Then
                    SetupSyncFreeValueCustomBankProperty(value)
                Else
                    SetSingleRelatedEntityNavigator(value, "FreeValueCustomBankPropertyValueCollection", "FreeValueCustomBankProperty", _freeValueCustomBankProperty, True)
                End If
            End Set
        End Property



        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property LLBLGenProIsInHierarchyOfType() As InheritanceHierarchyType
            Get
                Return InheritanceHierarchyType.TargetPerEntity
            End Get
        End Property

        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property LLBLGenProIsSubType As Boolean
            Get
                Return True
            End Get
        End Property

        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property LLBLGenProEntityTypeValue As Integer
            Get
                Return CInt(Questify.Builder.Model.ContentModel.EntityType.FreeValueCustomBankPropertyValueEntity)
            End Get
        End Property






    End Class
End Namespace
