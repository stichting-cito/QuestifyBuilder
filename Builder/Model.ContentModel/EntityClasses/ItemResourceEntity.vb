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
    Public Class ItemResourceEntity
        Inherits ResourceEntity






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
            Public Shared ReadOnly [ReferencedResourceCollection] As String = "ReferencedResourceCollection"
            Public Shared ReadOnly [DependentResourceCollection] As String = "DependentResourceCollection"
            Public Shared ReadOnly [HiddenResourceCollection] As String = "HiddenResourceCollection"
            Public Shared ReadOnly [ResourceHistoryCollection] As String = "ResourceHistoryCollection"
            Public Shared ReadOnly [CustomBankPropertyCollectionViaCustomBankPropertyValue] As String = "CustomBankPropertyCollectionViaCustomBankPropertyValue"
            Public Shared ReadOnly [ResourceData] As String = "ResourceData"
        End Class

        Shared Sub New()
            SetupCustomPropertyHashtables()
        End Sub

        Public Sub New()
            MyBase.New()
            InitClassEmpty()
            SetName("ItemResourceEntity")
        End Sub

        Public Sub New(fields As IEntityFields2)
            MyBase.New(fields)
            InitClassEmpty()
            SetName("ItemResourceEntity")
        End Sub

        Public Sub New(validator As IValidator)
            MyBase.New(validator)
            InitClassEmpty()
            SetName("ItemResourceEntity")
        End Sub

        Public Sub New(resourceId As System.Guid)
            MyBase.New(resourceId)
            InitClassEmpty()

            SetName("ItemResourceEntity")
        End Sub

        Public Sub New(resourceId As System.Guid, validator As IValidator)
            MyBase.New(resourceId, validator)
            InitClassEmpty()

            SetName("ItemResourceEntity")
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Sub New(info As SerializationInfo, context As StreamingContext)
            MyBase.New(info, context)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                Me.FixupDeserialization(FieldInfoProviderSingleton.GetInstance())
            End If

        End Sub



        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub SetRelatedEntityProperty(propertyName As String, entity As IEntityCore)
            Select Case propertyName

                Case Else
                    MyBase.SetRelatedEntityProperty(propertyName, entity)
            End Select
        End Sub

        Protected Overrides Function GetRelationsForFieldOfType(fieldName As String) As RelationCollection
            Return ItemResourceEntity.GetRelationsForField(fieldName)
        End Function

        Friend Shared Shadows Function GetRelationsForField(fieldName As String) As RelationCollection
            Dim toReturn As New RelationCollection()
            Select Case fieldName
                Case Else
                    toReturn = ResourceEntity.GetRelationsForField(fieldName)
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

                Case Else
                    MyBase.SetRelatedEntity(relatedEntity, fieldName)
            End Select
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub UnsetRelatedEntity(relatedEntity As IEntityCore, fieldName As String, signalRelatedEntityManyToOne As Boolean)
            Select Case fieldName
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
            toReturn.AddRange(MyBase.GetMemberEntityCollections())
            Return toReturn
        End Function

        Public Shadows Shared Function GetEntityTypeFilter() As IPredicateExpression
            Return InheritanceInfoProviderSingleton.GetInstance().GetEntityTypeFilter("ItemResourceEntity", False)
        End Function

        Public Shadows Shared Function GetEntityTypeFilter(negate As Boolean) As IPredicateExpression
            Return InheritanceInfoProviderSingleton.GetInstance().GetEntityTypeFilter("ItemResourceEntity", negate)
        End Function

        Protected Overrides Sub GetObjectData(info As SerializationInfo, context As StreamingContext)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                Dim value As IEntityCollection2 = Nothing
                Dim entityValue As IEntity2 = Nothing
            End If

            MyBase.GetObjectData(info, context)
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Function CheckIfIsSubTypeOf(typeOfEntity As Integer) As Boolean
            Return InheritanceInfoProviderSingleton.GetInstance().CheckIfIsSubTypeOf("ItemResourceEntity", CType(typeOfEntity, Questify.Builder.Model.ContentModel.EntityType).ToString())
        End Function

        Protected Overrides Overloads Function GetAllRelations() As List(Of IEntityRelation)
            Return New ItemResourceRelations().GetAllRelations()
        End Function

        Protected Overrides Function CreateEntityFactory() As IEntityFactory2
            Return EntityFactoryCache2.GetEntityFactory(GetType(ItemResourceEntityFactory))
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
            Return toReturn
        End Function

        Private Sub InitClassMembers()


        End Sub

        Private Shared Sub SetupCustomPropertyHashtables()
            _customProperties = New Dictionary(Of String, String)()
            _fieldsCustomProperties = New Dictionary(Of String, Dictionary(Of String, String))()
            Dim fieldHashtable As Dictionary(Of String, String)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("IsSystemItem", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("AlternativesCount", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("KeyValues", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("ResponseCount", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("RawScore", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("TesterSchemaVersion", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("Iltname", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("Iltversion", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("MaxScore", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("ItemAutoId", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("ItemId", fieldHashtable)
        End Sub





        Private Sub InitClassEmpty()
            InitClassMembers()




        End Sub

        Public Shadows Shared ReadOnly Property Relations() As ItemResourceRelations
            Get
                Return New ItemResourceRelations()
            End Get
        End Property

        Public Shadows Shared ReadOnly Property CustomProperties() As Dictionary(Of String, String)
            Get
                Return _customProperties
            End Get
        End Property






        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property CustomPropertiesOfType() As Dictionary(Of String, String)
            Get
                Return ItemResourceEntity.CustomProperties
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
                Return ItemResourceEntity.FieldsCustomProperties
            End Get
        End Property

        Public Overridable Property [IsSystemItem]() As System.Boolean
            Get
                Return CType(GetValue(CInt(ItemResourceFieldIndex.IsSystemItem), True), System.Boolean)
            End Get
            Set
                SetValue(CInt(ItemResourceFieldIndex.IsSystemItem), value)
            End Set
        End Property
        Public Overridable Property [AlternativesCount]() As Nullable(Of System.Int32)
            Get
                Return CType(GetValue(CInt(ItemResourceFieldIndex.AlternativesCount), False), Nullable(Of System.Int32))
            End Get
            Set
                SetValue(CInt(ItemResourceFieldIndex.AlternativesCount), value)
            End Set
        End Property
        Public Overridable Property [KeyValues]() As System.String
            Get
                Return CType(GetValue(CInt(ItemResourceFieldIndex.KeyValues), True), System.String)
            End Get
            Set
                SetValue(CInt(ItemResourceFieldIndex.KeyValues), value)
            End Set
        End Property
        Public Overridable Property [ResponseCount]() As Nullable(Of System.Int32)
            Get
                Return CType(GetValue(CInt(ItemResourceFieldIndex.ResponseCount), False), Nullable(Of System.Int32))
            End Get
            Set
                SetValue(CInt(ItemResourceFieldIndex.ResponseCount), value)
            End Set
        End Property
        Public Overridable Property [RawScore]() As Nullable(Of System.Int32)
            Get
                Return CType(GetValue(CInt(ItemResourceFieldIndex.RawScore), False), Nullable(Of System.Int32))
            End Get
            Set
                SetValue(CInt(ItemResourceFieldIndex.RawScore), value)
            End Set
        End Property
        Public Overridable Property [TesterSchemaVersion]() As Nullable(Of System.Int32)
            Get
                Return CType(GetValue(CInt(ItemResourceFieldIndex.TesterSchemaVersion), False), Nullable(Of System.Int32))
            End Get
            Set
                SetValue(CInt(ItemResourceFieldIndex.TesterSchemaVersion), value)
            End Set
        End Property
        Public Overridable Property [Iltname]() As System.String
            Get
                Return CType(GetValue(CInt(ItemResourceFieldIndex.Iltname), True), System.String)
            End Get
            Set
                SetValue(CInt(ItemResourceFieldIndex.Iltname), value)
            End Set
        End Property
        Public Overridable Property [Iltversion]() As Nullable(Of System.Int32)
            Get
                Return CType(GetValue(CInt(ItemResourceFieldIndex.Iltversion), False), Nullable(Of System.Int32))
            End Get
            Set
                SetValue(CInt(ItemResourceFieldIndex.Iltversion), value)
            End Set
        End Property
        Public Overridable Property [MaxScore]() As Nullable(Of System.Decimal)
            Get
                Return CType(GetValue(CInt(ItemResourceFieldIndex.MaxScore), False), Nullable(Of System.Decimal))
            End Get
            Set
                SetValue(CInt(ItemResourceFieldIndex.MaxScore), value)
            End Set
        End Property
        Public Overridable ReadOnly Property [ItemAutoId]() As System.Int32
            Get
                Return CType(GetValue(CInt(ItemResourceFieldIndex.ItemAutoId), True), System.Int32)
            End Get

        End Property
        Public Overridable Property [ItemId]() As System.String
            Get
                Return CType(GetValue(CInt(ItemResourceFieldIndex.ItemId), True), System.String)
            End Get
            Set
                SetValue(CInt(ItemResourceFieldIndex.ItemId), value)
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
                Return CInt(Questify.Builder.Model.ContentModel.EntityType.ItemResourceEntity)
            End Get
        End Property






    End Class
End Namespace
