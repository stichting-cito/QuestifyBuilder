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
    Public Class RichTextValueCustomBankPropertyValueEntity
        Inherits CustomBankPropertyValueEntity



        Private WithEvents _richTextValueCustomBankProperty As RichTextValueCustomBankPropertyEntity



        Private Shared _customProperties As Dictionary(Of String, String)
        Private Shared _fieldsCustomProperties As Dictionary(Of String, Dictionary(Of String, String))

        Public Shadows NotInheritable Class MemberNames
            Private Sub New()
            End Sub
            Public Shared ReadOnly [CustomBankProperty] As String = "CustomBankProperty"
            Public Shared ReadOnly [Resource] As String = "Resource"
            Public Shared ReadOnly [RichTextValueCustomBankProperty] As String = "RichTextValueCustomBankProperty"
        End Class

        Shared Sub New()
            SetupCustomPropertyHashtables()
        End Sub

        Public Sub New()
            MyBase.New()
            InitClassEmpty()
            SetName("RichTextValueCustomBankPropertyValueEntity")
        End Sub

        Public Sub New(fields As IEntityFields2)
            MyBase.New(fields)
            InitClassEmpty()
            SetName("RichTextValueCustomBankPropertyValueEntity")
        End Sub

        Public Sub New(validator As IValidator)
            MyBase.New(validator)
            InitClassEmpty()
            SetName("RichTextValueCustomBankPropertyValueEntity")
        End Sub

        Public Sub New(resourceId As System.Guid, customBankPropertyId As System.Guid)
            MyBase.New(resourceId, customBankPropertyId)
            InitClassEmpty()

            SetName("RichTextValueCustomBankPropertyValueEntity")
        End Sub

        Public Sub New(resourceId As System.Guid, customBankPropertyId As System.Guid, validator As IValidator)
            MyBase.New(resourceId, customBankPropertyId, validator)
            InitClassEmpty()

            SetName("RichTextValueCustomBankPropertyValueEntity")
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Sub New(info As SerializationInfo, context As StreamingContext)
            MyBase.New(info, context)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                _richTextValueCustomBankProperty = CType(info.GetValue("_richTextValueCustomBankProperty", GetType(RichTextValueCustomBankPropertyEntity)), RichTextValueCustomBankPropertyEntity)
                If Not _richTextValueCustomBankProperty Is Nothing Then
                    AddHandler _richTextValueCustomBankProperty.AfterSave, AddressOf OnEntityAfterSave
                End If
                Me.FixupDeserialization(FieldInfoProviderSingleton.GetInstance())
            End If

        End Sub



        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub SetRelatedEntityProperty(propertyName As String, entity As IEntityCore)
            Select Case propertyName
                Case "RichTextValueCustomBankProperty"
                    Me.RichTextValueCustomBankProperty = CType(entity, RichTextValueCustomBankPropertyEntity)

                Case Else
                    MyBase.SetRelatedEntityProperty(propertyName, entity)
            End Select
        End Sub

        Protected Overrides Function GetRelationsForFieldOfType(fieldName As String) As RelationCollection
            Return RichTextValueCustomBankPropertyValueEntity.GetRelationsForField(fieldName)
        End Function

        Friend Shared Shadows Function GetRelationsForField(fieldName As String) As RelationCollection
            Dim toReturn As New RelationCollection()
            Select Case fieldName
                Case "RichTextValueCustomBankProperty"
                    toReturn.Add(RichTextValueCustomBankPropertyValueEntity.Relations.RichTextValueCustomBankPropertyEntityUsingCustomBankPropertyId)
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
                Case "RichTextValueCustomBankProperty"
                    SetupSyncRichTextValueCustomBankProperty(relatedEntity)

                Case Else
                    MyBase.SetRelatedEntity(relatedEntity, fieldName)
            End Select
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub UnsetRelatedEntity(relatedEntity As IEntityCore, fieldName As String, signalRelatedEntityManyToOne As Boolean)
            Select Case fieldName
                Case "RichTextValueCustomBankProperty"
                    DesetupSyncRichTextValueCustomBankProperty(False, True)
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
            If Not _richTextValueCustomBankProperty Is Nothing Then
                toReturn.Add(_richTextValueCustomBankProperty)
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
            Return InheritanceInfoProviderSingleton.GetInstance().GetEntityTypeFilter("RichTextValueCustomBankPropertyValueEntity", False)
        End Function

        Public Shadows Shared Function GetEntityTypeFilter(negate As Boolean) As IPredicateExpression
            Return InheritanceInfoProviderSingleton.GetInstance().GetEntityTypeFilter("RichTextValueCustomBankPropertyValueEntity", negate)
        End Function

        Protected Overrides Sub GetObjectData(info As SerializationInfo, context As StreamingContext)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                Dim value As IEntityCollection2 = Nothing
                Dim entityValue As IEntity2 = Nothing
                entityValue = Nothing
                If Not Me.MarkedForDeletion Then
                    entityValue = _richTextValueCustomBankProperty
                End If
                info.AddValue("_richTextValueCustomBankProperty", entityValue)
            End If

            MyBase.GetObjectData(info, context)
        End Sub

        Public Function ConstructFilterForUCRichTextValueCustomBankPropertyValueId() As IPredicateExpression
            Dim Filter As IPredicateExpression = New PredicateExpression()
            Filter.Add(Questify.Builder.Model.ContentModel.HelperClasses.RichTextValueCustomBankPropertyValueFields.RichTextValueCustomBankPropertyValueId = Me.Fields.GetCurrentValue(CInt(RichTextValueCustomBankPropertyValueFieldIndex.RichTextValueCustomBankPropertyValueId)))
            Return Filter
        End Function

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Function CheckIfIsSubTypeOf(typeOfEntity As Integer) As Boolean
            Return InheritanceInfoProviderSingleton.GetInstance().CheckIfIsSubTypeOf("RichTextValueCustomBankPropertyValueEntity", CType(typeOfEntity, Questify.Builder.Model.ContentModel.EntityType).ToString())
        End Function

        Protected Overrides Overloads Function GetAllRelations() As List(Of IEntityRelation)
            Return New RichTextValueCustomBankPropertyValueRelations().GetAllRelations()
        End Function

        Public Overridable Function GetRelationInfoRichTextValueCustomBankProperty() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(RichTextValueCustomBankPropertyFields.CustomBankPropertyId, Nothing, ComparisonOperator.Equal, Me.CustomBankPropertyId))
            Return bucket
        End Function

        Protected Overrides Function CreateEntityFactory() As IEntityFactory2
            Return EntityFactoryCache2.GetEntityFactory(GetType(RichTextValueCustomBankPropertyValueEntityFactory))
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
            toReturn.Add("RichTextValueCustomBankProperty", _richTextValueCustomBankProperty)
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
            _fieldsCustomProperties.Add("RichTextValueCustomBankPropertyValueId", fieldHashtable)
        End Sub


        Private Sub DesetupSyncRichTextValueCustomBankProperty(signalRelatedEntity As Boolean, resetFKFields As Boolean)
            Me.PerformDesetupSyncRelatedEntity(_richTextValueCustomBankProperty, AddressOf OnRichTextValueCustomBankPropertyPropertyChanged, "RichTextValueCustomBankProperty", Questify.Builder.Model.ContentModel.RelationClasses.StaticRichTextValueCustomBankPropertyValueRelations.RichTextValueCustomBankPropertyEntityUsingCustomBankPropertyIdStatic, True, signalRelatedEntity, "RichTextValueCustomBankPropertyValueCollection", resetFKFields, New Integer() {CInt(RichTextValueCustomBankPropertyValueFieldIndex.CustomBankPropertyId)})
            _richTextValueCustomBankProperty = Nothing
        End Sub

        Private Sub SetupSyncRichTextValueCustomBankProperty(relatedEntity As IEntityCore)
            If Not _richTextValueCustomBankProperty Is relatedEntity Then
                DesetupSyncRichTextValueCustomBankProperty(True, True)
                _richTextValueCustomBankProperty = CType(relatedEntity, RichTextValueCustomBankPropertyEntity)
                Me.PerformSetupSyncRelatedEntity(_richTextValueCustomBankProperty, AddressOf OnRichTextValueCustomBankPropertyPropertyChanged, "RichTextValueCustomBankProperty", Questify.Builder.Model.ContentModel.RelationClasses.StaticRichTextValueCustomBankPropertyValueRelations.RichTextValueCustomBankPropertyEntityUsingCustomBankPropertyIdStatic, True, New String() {})
            End If
        End Sub

        Private Sub OnRichTextValueCustomBankPropertyPropertyChanged(sender As Object, e As PropertyChangedEventArgs)
            Select Case e.PropertyName

                Case Else
            End Select
        End Sub



        Private Sub InitClassEmpty()
            InitClassMembers()




        End Sub

        Public Shadows Shared ReadOnly Property Relations() As RichTextValueCustomBankPropertyValueRelations
            Get
                Return New RichTextValueCustomBankPropertyValueRelations()
            End Get
        End Property

        Public Shadows Shared ReadOnly Property CustomProperties() As Dictionary(Of String, String)
            Get
                Return _customProperties
            End Get
        End Property




        Public Shared ReadOnly Property PrefetchPathRichTextValueCustomBankProperty() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(EntityFactoryCache2.GetEntityFactory(GetType(RichTextValueCustomBankPropertyEntityFactory))), _
                    CType(GetRelationsForField("RichTextValueCustomBankProperty")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.RichTextValueCustomBankPropertyValueEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.RichTextValueCustomBankPropertyEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "RichTextValueCustomBankProperty", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne)
            End Get
        End Property


        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property CustomPropertiesOfType() As Dictionary(Of String, String)
            Get
                Return RichTextValueCustomBankPropertyValueEntity.CustomProperties
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
                Return RichTextValueCustomBankPropertyValueEntity.FieldsCustomProperties
            End Get
        End Property

        Public Overridable Property [Value]() As System.String
            Get
                Return CType(GetValue(CInt(RichTextValueCustomBankPropertyValueFieldIndex.Value), True), System.String)
            End Get
            Set
                SetValue(CInt(RichTextValueCustomBankPropertyValueFieldIndex.Value), value)
            End Set
        End Property
        Public Overridable ReadOnly Property [RichTextValueCustomBankPropertyValueId]() As System.Int32
            Get
                Return CType(GetValue(CInt(RichTextValueCustomBankPropertyValueFieldIndex.RichTextValueCustomBankPropertyValueId), True), System.Int32)
            End Get

        End Property



        <Browsable(True)> _
        Public Overridable Property [RichTextValueCustomBankProperty]() As RichTextValueCustomBankPropertyEntity
            Get
                Return _richTextValueCustomBankProperty
            End Get
            Set
                If MyBase.IsDeserializing Then
                    SetupSyncRichTextValueCustomBankProperty(value)
                Else
                    SetSingleRelatedEntityNavigator(value, "RichTextValueCustomBankPropertyValueCollection", "RichTextValueCustomBankProperty", _richTextValueCustomBankProperty, True)
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
                Return CInt(Questify.Builder.Model.ContentModel.EntityType.RichTextValueCustomBankPropertyValueEntity)
            End Get
        End Property






    End Class
End Namespace
