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
    Public Class GenericResourceEntity
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
            SetName("GenericResourceEntity")
        End Sub

        Public Sub New(fields As IEntityFields2)
            MyBase.New(fields)
            InitClassEmpty()
            SetName("GenericResourceEntity")
        End Sub

        Public Sub New(validator As IValidator)
            MyBase.New(validator)
            InitClassEmpty()
            SetName("GenericResourceEntity")
        End Sub

        Public Sub New(resourceId As System.Guid)
            MyBase.New(resourceId)
            InitClassEmpty()

            SetName("GenericResourceEntity")
        End Sub

        Public Sub New(resourceId As System.Guid, validator As IValidator)
            MyBase.New(resourceId, validator)
            InitClassEmpty()

            SetName("GenericResourceEntity")
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
            Return GenericResourceEntity.GetRelationsForField(fieldName)
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
            Return InheritanceInfoProviderSingleton.GetInstance().GetEntityTypeFilter("GenericResourceEntity", False)
        End Function

        Public Shadows Shared Function GetEntityTypeFilter(negate As Boolean) As IPredicateExpression
            Return InheritanceInfoProviderSingleton.GetInstance().GetEntityTypeFilter("GenericResourceEntity", negate)
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
            Return InheritanceInfoProviderSingleton.GetInstance().CheckIfIsSubTypeOf("GenericResourceEntity", CType(typeOfEntity, Questify.Builder.Model.ContentModel.EntityType).ToString())
        End Function

        Protected Overrides Overloads Function GetAllRelations() As List(Of IEntityRelation)
            Return New GenericResourceRelations().GetAllRelations()
        End Function

        Protected Overrides Function CreateEntityFactory() As IEntityFactory2
            Return EntityFactoryCache2.GetEntityFactory(GetType(GenericResourceEntityFactory))
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
            _fieldsCustomProperties.Add("MediaType", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("Size", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("Dimensions", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("IsTemplate", fieldHashtable)
        End Sub





        Private Sub InitClassEmpty()
            InitClassMembers()




        End Sub

        Public Shadows Shared ReadOnly Property Relations() As GenericResourceRelations
            Get
                Return New GenericResourceRelations()
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
                Return GenericResourceEntity.CustomProperties
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
                Return GenericResourceEntity.FieldsCustomProperties
            End Get
        End Property

        Public Overridable Property [MediaType]() As System.String
            Get
                Return CType(GetValue(CInt(GenericResourceFieldIndex.MediaType), True), System.String)
            End Get
            Set
                SetValue(CInt(GenericResourceFieldIndex.MediaType), value)
            End Set
        End Property
        Public Overridable Property [Size]() As System.Int32
            Get
                Return CType(GetValue(CInt(GenericResourceFieldIndex.Size), True), System.Int32)
            End Get
            Set
                SetValue(CInt(GenericResourceFieldIndex.Size), value)
            End Set
        End Property
        Public Overridable Property [Dimensions]() As System.String
            Get
                Return CType(GetValue(CInt(GenericResourceFieldIndex.Dimensions), True), System.String)
            End Get
            Set
                SetValue(CInt(GenericResourceFieldIndex.Dimensions), value)
            End Set
        End Property
        Public Overridable Property [IsTemplate]() As System.Boolean
            Get
                Return CType(GetValue(CInt(GenericResourceFieldIndex.IsTemplate), True), System.Boolean)
            End Get
            Set
                SetValue(CInt(GenericResourceFieldIndex.IsTemplate), value)
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
                Return CInt(Questify.Builder.Model.ContentModel.EntityType.GenericResourceEntity)
            End Get
        End Property






    End Class
End Namespace
