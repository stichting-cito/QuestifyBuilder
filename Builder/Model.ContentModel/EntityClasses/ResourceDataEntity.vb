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
    Public Class ResourceDataEntity
        Inherits CommonEntityBase



        Private WithEvents _resource As ResourceEntity



        Private Shared _customProperties As Dictionary(Of String, String)
        Private Shared _fieldsCustomProperties As Dictionary(Of String, Dictionary(Of String, String))

        Public NotInheritable Class MemberNames
            Private Sub New()
            End Sub
            Public Shared ReadOnly [Resource] As String = "Resource"
        End Class

        Shared Sub New()
            SetupCustomPropertyHashtables()
        End Sub

        Public Sub New()
            MyBase.New("ResourceDataEntity")
            InitClassEmpty(Nothing, Nothing)
        End Sub

        Public Sub New(fields As IEntityFields2)
            MyBase.New("ResourceDataEntity")
            InitClassEmpty(Nothing, fields)
        End Sub

        Public Sub New(validator As IValidator)
            MyBase.New("ResourceDataEntity")
            InitClassEmpty(validator, Nothing)
        End Sub

        Public Sub New(resourceId As System.Guid)
            MyBase.New("ResourceDataEntity")
            InitClassEmpty(Nothing, Nothing)
            Me.ResourceId = resourceId
        End Sub

        Public Sub New(resourceId As System.Guid, validator As IValidator)
            MyBase.New("ResourceDataEntity")
            InitClassEmpty(validator, Nothing)
            Me.ResourceId = resourceId
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Sub New(info As SerializationInfo, context As StreamingContext)
            MyBase.New(info, context)
            If SerializationHelper.Optimization <> SerializationOptimization.Fast Then
                _resource = CType(info.GetValue("_resource", GetType(ResourceEntity)), ResourceEntity)
                If Not _resource Is Nothing Then
                    AddHandler _resource.AfterSave, AddressOf OnEntityAfterSave
                End If
                Me.FixupDeserialization(FieldInfoProviderSingleton.GetInstance())
            End If

        End Sub


        Protected Overrides Sub PerformDesyncSetupFKFieldChange(fieldIndex As Integer)
            Select Case CType(fieldIndex, ResourceDataFieldIndex)
                Case ResourceDataFieldIndex.ResourceId
                    DesetupSyncResource(True, False)




                Case Else
                    MyBase.PerformDesyncSetupFKFieldChange(fieldIndex)
            End Select
        End Sub


        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub SetRelatedEntityProperty(propertyName As String, entity As IEntityCore)
            Select Case propertyName
                Case "Resource"
                    Me.Resource = CType(entity, ResourceEntity)

                Case Else
                    Me.OnSetRelatedEntityProperty(propertyName, entity)
            End Select
        End Sub

        Protected Overrides Function GetRelationsForFieldOfType(fieldName As String) As RelationCollection
            Return ResourceDataEntity.GetRelationsForField(fieldName)
        End Function

        Friend Shared Function GetRelationsForField(fieldName As String) As RelationCollection
            Dim toReturn As New RelationCollection()
            Select Case fieldName
                Case "Resource"
                    toReturn.Add(ResourceDataEntity.Relations.ResourceEntityUsingResourceId)
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
                Case "Resource"
                    SetupSyncResource(relatedEntity)

                Case Else
            End Select
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Protected Overrides Overloads Sub UnsetRelatedEntity(relatedEntity As IEntityCore, fieldName As String, signalRelatedEntityManyToOne As Boolean)
            Select Case fieldName
                Case "Resource"
                    DesetupSyncResource(False, True)
                Case Else
            End Select
        End Sub

        Protected Overrides Function GetDependingRelatedEntities() As List(Of IEntity2)
            Dim toReturn As New List(Of IEntity2)()


            Return toReturn
        End Function

        Protected Overrides Function GetDependentRelatedEntities() As List(Of IEntity2)
            Dim toReturn As New List(Of IEntity2)()

            If Not _resource Is Nothing Then
                toReturn.Add(_resource)
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
                    entityValue = _resource
                End If
                info.AddValue("_resource", entityValue)
            End If

            MyBase.GetObjectData(info, context)
        End Sub


        Protected Overrides Overloads Function GetAllRelations() As List(Of IEntityRelation)
            Return New ResourceDataRelations().GetAllRelations()
        End Function

        Public Overridable Function GetRelationInfoResource() As IRelationPredicateBucket
            Dim bucket As IRelationPredicateBucket = New RelationPredicateBucket()
            bucket.PredicateExpression.Add(New FieldCompareValuePredicate(ResourceFields.ResourceId, Nothing, ComparisonOperator.Equal, Me.ResourceId))
            Return bucket
        End Function

        Protected Overrides Function CreateEntityFactory() As IEntityFactory2
            Return EntityFactoryCache2.GetEntityFactory(GetType(ResourceDataEntityFactory))
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
            toReturn.Add("Resource", _resource)
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
            _fieldsCustomProperties.Add("BinData", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("Url", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("FileExtension", fieldHashtable)
            fieldHashtable = New Dictionary(Of String, String)()
            _fieldsCustomProperties.Add("Ident", fieldHashtable)
        End Sub



        Private Sub DesetupSyncResource(signalRelatedEntity As Boolean, resetFKFields As Boolean)
            Me.PerformDesetupSyncRelatedEntity(_resource, AddressOf OnResourcePropertyChanged, "Resource", Questify.Builder.Model.ContentModel.RelationClasses.StaticResourceDataRelations.ResourceEntityUsingResourceIdStatic, True, signalRelatedEntity, "ResourceData", False, New Integer() {CInt(ResourceDataFieldIndex.ResourceId)})
            _resource = Nothing
        End Sub

        Private Sub SetupSyncResource(relatedEntity As IEntityCore)
            If Not _resource Is relatedEntity Then
                DesetupSyncResource(True, True)
                _resource = CType(relatedEntity, ResourceEntity)
                Me.PerformSetupSyncRelatedEntity(_resource, AddressOf OnResourcePropertyChanged, "Resource", Questify.Builder.Model.ContentModel.RelationClasses.StaticResourceDataRelations.ResourceEntityUsingResourceIdStatic, True, New String() {})
            End If
        End Sub

        Private Sub OnResourcePropertyChanged(sender As Object, e As PropertyChangedEventArgs)
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

        Public Shared ReadOnly Property Relations() As ResourceDataRelations
            Get
                Return New ResourceDataRelations()
            End Get
        End Property

        Public Shared ReadOnly Property CustomProperties() As Dictionary(Of String, String)
            Get
                Return _customProperties
            End Get
        End Property





        Public Shared ReadOnly Property PrefetchPathResource() As IPrefetchPathElement2
            Get
                Return New PrefetchPathElement2(New EntityCollection(EntityFactoryCache2.GetEntityFactory(GetType(ResourceEntityFactory))), _
                    CType(GetRelationsForField("Resource")(0), IEntityRelation), CType(Questify.Builder.Model.ContentModel.EntityType.ResourceDataEntity, Integer), CType(Questify.Builder.Model.ContentModel.EntityType.ResourceEntity, Integer), 0, Nothing, Nothing, Nothing, Nothing, "Resource", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToOne)
            End Get
        End Property

        <Browsable(False), XmlIgnore> _
        Protected Overrides ReadOnly Property CustomPropertiesOfType() As Dictionary(Of String, String)
            Get
                Return ResourceDataEntity.CustomProperties
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
                Return ResourceDataEntity.FieldsCustomProperties
            End Get
        End Property

        Public Overridable Property [ResourceId]() As System.Guid
            Get
                Return CType(GetValue(CInt(ResourceDataFieldIndex.ResourceId), True), System.Guid)
            End Get
            Set
                SetValue(CInt(ResourceDataFieldIndex.ResourceId), value)
            End Set
        End Property
        Public Overridable Property [BinData]() As System.Byte()
            Get
                Return CType(GetValue(CInt(ResourceDataFieldIndex.BinData), True), System.Byte())
            End Get
            Set
                SetValue(CInt(ResourceDataFieldIndex.BinData), value)
            End Set
        End Property
        Public Overridable Property [Url]() As System.String
            Get
                Return CType(GetValue(CInt(ResourceDataFieldIndex.Url), True), System.String)
            End Get
            Set
                SetValue(CInt(ResourceDataFieldIndex.Url), value)
            End Set
        End Property
        Public Overridable Property [FileExtension]() As System.String
            Get
                Return CType(GetValue(CInt(ResourceDataFieldIndex.FileExtension), True), System.String)
            End Get
            Set
                SetValue(CInt(ResourceDataFieldIndex.FileExtension), value)
            End Set
        End Property
        Public Overridable ReadOnly Property [Ident]() As System.Int64
            Get
                Return CType(GetValue(CInt(ResourceDataFieldIndex.Ident), True), System.Int64)
            End Get

        End Property




        <Browsable(True)> _
        Public Overridable Property [Resource]() As ResourceEntity
            Get
                Return _resource
            End Get
            Set
                If MyBase.IsDeserializing Then
                    SetupSyncResource(value)
                    CallSetRelatedEntityDuringDeserialization(value, "ResourceData")
                Else
                    If value Is Nothing Then
                        Dim raisePropertyChanged As Boolean = Not (_resource Is Nothing)
                        DesetupSyncResource(True, True)
                        If raisePropertyChanged Then
                            OnPropertyChanged("Resource")
                        End If
                    Else
                        If Not _resource Is value Then
                            CType(value, IEntity2).SetRelatedEntity(Me, "ResourceData")
                            SetupSyncResource(value)
                        End If
                    End If
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
                Return CInt(Questify.Builder.Model.ContentModel.EntityType.ResourceDataEntity)
            End Get
        End Property






    End Class
End Namespace
