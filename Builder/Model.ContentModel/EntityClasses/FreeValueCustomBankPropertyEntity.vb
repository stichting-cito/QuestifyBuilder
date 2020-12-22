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
    Public Class FreeValueCustomBankPropertyEntity
        Inherits CustomBankPropertyEntity



        Private WithEvents _freeValueCustomBankPropertyValueCollection As EntityCollection(Of FreeValueCustomBankPropertyValueEntity)



        Private Shared _customProperties As Dictionary(Of String, String)
        Private Shared _fieldsCustomProperties As Dictionary(Of String, Dictionary(Of String, String))

        Public Shadows NotInheritable Class MemberNames
            Private Sub New()
            End Sub
            Public Shared ReadOnly [Bank] As String = "Bank"
            Public Shared ReadOnly [State] As String = "State"
            Public Shared ReadOnly [CreatedByUser] As String = "CreatedByUser"
            Public Shared ReadOnly [ModifiedByUser] As String = "ModifiedByUser"
            Public Shared ReadOnly [CustomBankPropertyValueCollection] As String = "CustomBankPropertyValueCollection"
            Public Shared ReadOnly [FreeValueCustomBankPropertyValueCollection] As String = "FreeValueCustomBankPropertyValueCollection"
        End Class

        Shared Sub New()
            SetupCustomPropertyHashtables()
        End Sub

        Public Sub New()
            MyBase.New()
            InitClassEmpty()
            SetName("FreeValueCustomBankPropertyEntity")
        End Sub

        Public Sub New(fields As IEntityFields2)
            MyBase.New(fields)
            InitClassEmpty()
            SetName("FreeValueCustomBankPropertyEntity")
        End Sub

        Public Sub New(validator As IValidator)
            MyBase.New(validator)
            InitClassEmpty()
            SetName("FreeValueCustomBankPropertyEntity")
        End Sub

        Public Sub New(customBankPropertyId As System.Guid)
            MyBase.New(customBankPropertyId)
            InitClassEmpty()

            SetName("FreeValueCustomBankPropertyEntity")
        End Sub

        Public Sub New(customBankPropertyId As System.Guid, validator As IValidator)
            MyBase.New(customBankPropertyId, validator)
            InitClassEmpty()

            SetName("FreeValueCustomBankPropertyEntity")
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Sub New(info As SerializationInfo, context As StreamingContext)
            MyBase.New(info, context)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                _freeValueCustomBankPropertyValueCollection = CType(info.GetValue("_freeValueCustomBankPropertyValueCollection", GetType(EntityCollection(Of FreeValueCustomBankPropertyValueEntity))), EntityCollection(Of FreeValueCustomBankPropertyValueEntity))
                Me.FixupDeserialization(FieldInfoProviderSingleton.GetInstance())
            End If

        End Sub



        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub SetRelatedEntityProperty(propertyName As String, entity As IEntityCore)
            Select Case propertyName
                Case "FreeValueCustomBankPropertyValueCollection"
                    Me.FreeValueCustomBankPropertyValueCollection.Add(CType(entity, FreeValueCustomBankPropertyValueEntity))

                Case Else
                    MyBase.SetRelatedEntityProperty(propertyName, entity)
            End Select
        End Sub

        Protected Overrides Function GetRelationsForFieldOfType(fieldName As String) As RelationCollection
            Return FreeValueCustomBankPropertyEntity.GetRelationsForField(fieldName)
        End Function

        Friend Shared Shadows Function GetRelationsForField(fieldName As String) As RelationCollection
            Dim toReturn As New RelationCollection()
            Select Case fieldName
                Case "FreeValueCustomBankPropertyValueCollection"
                    toReturn.Add(FreeValueCustomBankPropertyEntity.Relations.FreeValueCustomBankPropertyValueEntityUsingCustomBankPropertyId)
                Case Else
                    toReturn = CustomBankPropertyEntity.GetRelationsForField(fieldName)
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
                Case "FreeValueCustomBankPropertyValueCollection"
                    Me.FreeValueCustomBankPropertyValueCollection.Add(CType(relatedEntity, FreeValueCustomBankPropertyValueEntity))

                Case Else
                    MyBase.SetRelatedEntity(relatedEntity, fieldName)
            End Select
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub UnsetRelatedEntity(relatedEntity As IEntityCore, fieldName As String, signalRelatedEntityManyToOne As Boolean)
            Select Case fieldName
                Case "FreeValueCustomBankPropertyValueCollection"
                    Me.PerformRelatedEntityRemoval(Me.FreeValueCustomBankPropertyValueCollection, relatedEntity, signalRelatedEntityManyToOne)
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
            toReturn.AddRange(MyBase.GetDependentRelatedEntities())
            Return toReturn
        End Function

        Protected Overrides Function GetMemberEntityCollections() As List(Of IEntityCollection2)
            Dim toReturn As New List(Of IEntityCollection2)()
            toReturn.Add(Me.FreeValueCustomBankPropertyValueCollection)
            toReturn.AddRange(MyBase.GetMemberEntityCollections())
            Return toReturn
        End Function

        Public Shadows Shared Function GetEntityTypeFilter() As IPredicateExpression
            Return InheritanceInfoProviderSingleton.GetInstance().GetEntityTypeFilter("FreeValueCustomBankPropertyEntity", False)
        End Function

        Public Shadows Shared Function GetEntityTypeFilter(negate As Boolean) As IPredicateExpression
            Return InheritanceInfoProviderSingleton.GetInstance().GetEntityTypeFilter("FreeValueCustomBankPropertyEntity", negate)
        End Function

        Protected Overrides Sub GetObjectData(info As SerializationInfo, context As StreamingContext)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                Dim value As IEntityCollection2 = Nothing
                Dim entityValue As IEntity2 = Nothing
                value = Nothing
                If (Not (_freeValueCustomBankPropertyValueCollection Is Nothing)) AndAlso (_freeValueCustomBankPropertyValueCollection.Count > 0) AndAlso Not Me.MarkedForDeletion Then
                    value = _freeValueCustomBankPropertyValueCollection
                End If
                info.AddValue("_freeValueCustomBankPropertyValueCollection", value)
            End If

            MyBase.GetObjectData(info, context)
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Function CheckIfIsSubTypeOf(typeOfEntity As Integer) As Boolean
            Return InheritanceInfoProviderSingleton.GetInstance().CheckIfIsSubTypeOf("FreeValueCustomBankPropertyEntity", CType(typeOfEntity, Questify.Builder.Model.ContentModel.EntityType).ToString())
        End Function

        Protected Overrides Overloads Function GetAllRelations() As List(Of IEntityRelation)
            Return New FreeValueCustomBankPropertyRelations().GetAllRelations()
        End Function

        Public Overridable Function GetRelationInfoFreeValueCustomBankPropertyValueCollection() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(FreeValueCustomBankPropertyValueFields.CustomBankPropertyId, Nothing, ComparisonOperator.Equal, Me.CustomBankPropertyId))
            Return bucket
        End Function

        Protected Overrides Function CreateEntityFactory() As IEntityFactory2
            Return EntityFactoryCache2.GetEntityFactory(GetType(FreeValueCustomBankPropertyEntityFactory))
        End Function
#If Not CF Then
        Protected Overrides Sub AddToMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2))
            MyBase.AddToMemberEntityCollectionsQueue(collectionsQueue)
            collectionsQueue.Enqueue(_freeValueCustomBankPropertyValueCollection)
        End Sub

        Protected Overrides Sub GetFromMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2))
            MyBase.GetFromMemberEntityCollectionsQueue(collectionsQueue)
            _freeValueCustomBankPropertyValueCollection = CType(collectionsQueue.Dequeue(), EntityCollection(Of FreeValueCustomBankPropertyValueEntity))
        End Sub

        Protected Overrides Function HasPopulatedMemberEntityCollections() As Boolean
            If (Not _freeValueCustomBankPropertyValueCollection Is Nothing) Then
                Return True
            End If
            Return MyBase.HasPopulatedMemberEntityCollections()
        End Function

        Protected Overrides Overloads Sub CreateMemberEntityCollectionsQueue(collectionsQueue As Queue(Of IEntityCollection2), requiredQueue As Queue(Of Boolean))
            MyBase.CreateMemberEntityCollectionsQueue(collectionsQueue, requiredQueue)
            Dim toAdd As IEntityCollection2 = Nothing
            If requiredQueue.Dequeue() Then
                toAdd = New EntityCollection(Of FreeValueCustomBankPropertyValueEntity)(EntityFactoryCache2.GetEntityFactory(GetType(FreeValueCustomBankPropertyValueEntityFactory)))
            Else
                toAdd = Nothing
            End If
            collectionsQueue.Enqueue(toAdd)
        End Sub
#End If
        Protected Overrides Overloads Function GetRelatedData() As Dictionary(Of String, Object)
            Dim toReturn As Dictionary(Of String, Object) = MyBase.GetRelatedData()
            toReturn.Add("FreeValueCustomBankPropertyValueCollection", _freeValueCustomBankPropertyValueCollection)
            Return toReturn
        End Function

        Private Sub InitClassMembers()


        End Sub

        Private Shared Sub SetupCustomPropertyHashtables()
            _customProperties = New Dictionary(Of String, String)()
            _fieldsCustomProperties = New Dictionary(Of String, Dictionary(Of String, String))()
        End Sub





        Private Sub InitClassEmpty()
            InitClassMembers()




        End Sub

        Public Shadows Shared ReadOnly Property Relations() As FreeValueCustomBankPropertyRelations
            Get
                Return New FreeValueCustomBankPropertyRelations()
            End Get
        End Property

        Public Shadows Shared ReadOnly Property CustomProperties() As Dictionary(Of String, String)
            Get
                Return _customProperties
            End Get
        End Property


        Public Shared ReadOnly Property PrefetchPathFreeValueCustomBankPropertyValueCollection() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(Of FreeValueCustomBankPropertyValueEntity)(EntityFactoryCache2.GetEntityFactory(GetType(FreeValueCustomBankPropertyValueEntityFactory))), _
                    CType(GetRelationsForField("FreeValueCustomBankPropertyValueCollection")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.FreeValueCustomBankPropertyEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.FreeValueCustomBankPropertyValueEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "FreeValueCustomBankPropertyValueCollection", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany)
            End Get
        End Property




        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property CustomPropertiesOfType() As Dictionary(Of String, String)
            Get
                Return FreeValueCustomBankPropertyEntity.CustomProperties
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
                Return FreeValueCustomBankPropertyEntity.FieldsCustomProperties
            End Get
        End Property


        <TypeContainedAttribute(GetType(FreeValueCustomBankPropertyValueEntity))> _
        Public Overridable ReadOnly Property [FreeValueCustomBankPropertyValueCollection]() As EntityCollection(Of FreeValueCustomBankPropertyValueEntity)
            Get
                If _freeValueCustomBankPropertyValueCollection Is Nothing Then
                    _freeValueCustomBankPropertyValueCollection = New EntityCollection(Of FreeValueCustomBankPropertyValueEntity)(EntityFactoryCache2.GetEntityFactory(GetType(FreeValueCustomBankPropertyValueEntityFactory)))
                    _freeValueCustomBankPropertyValueCollection.ActiveContext = Me.ActiveContext
                    _freeValueCustomBankPropertyValueCollection.SetContainingEntityInfo(Me, "FreeValueCustomBankProperty")
                End If
                Return _freeValueCustomBankPropertyValueCollection
            End Get
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
                Return CInt(Questify.Builder.Model.ContentModel.EntityType.FreeValueCustomBankPropertyEntity)
            End Get
        End Property






    End Class
End Namespace
