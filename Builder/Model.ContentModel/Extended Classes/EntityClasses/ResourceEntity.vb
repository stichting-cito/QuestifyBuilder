Imports System.Linq
Imports SD.LLBLGen.Pro.ORMSupportClasses
Imports Questify.Builder.Model.ContentModel.Interfaces

Namespace Questify.Builder.Model.ContentModel.EntityClasses

    Partial Public Class ResourceEntity
        Implements IPropertyEntity



        Private Property Id As Guid Implements IPropertyEntity.Id
            Get
                Return ResourceId
            End Get
            Set(value As Guid)
                ResourceId = value
            End Set
        End Property

        Private Property BankId2 As Integer Implements IPropertyEntity.BankId
            Get
                Return BankId
            End Get
            Set(value As Integer)
                BankId = value
            End Set
        End Property

        Private Property CreatedBy2 As Integer Implements IPropertyEntity.CreatedBy
            Get
                Return CreatedBy
            End Get
            Set(value As Integer)
                CreatedBy = value
            End Set
        End Property

        Private Property CreationDate2 As DateTime Implements IPropertyEntity.CreationDate
            Get
                Return CreationDate
            End Get
            Set(value As DateTime)
                CreationDate = value
            End Set
        End Property

        Private Property Description2 As String Implements IPropertyEntity.Description
            Get
                Return Description
            End Get
            Set(value As String)
                Description = value
            End Set
        End Property

        Private Property ModifiedBy2 As Integer Implements IPropertyEntity.ModifiedBy
            Get
                Return ModifiedBy
            End Get
            Set(value As Integer)
                ModifiedBy = value
            End Set
        End Property

        Private ReadOnly Property CreatedByFullName2 As System.String Implements IPropertyEntity.CreatedByFullName
            Get
                Return CreatedByFullName
            End Get
        End Property

        Private ReadOnly Property ModifiedByFullName2 As System.String Implements IPropertyEntity.ModifiedByFullName
            Get
                Return ModifiedByFullName
            End Get
        End Property

        Private Property ModifiedDate2 As Date Implements IPropertyEntity.ModifiedDate
            Get
                Return ModifiedDate
            End Get
            Set(value As Date)
                ModifiedDate = value
            End Set
        End Property

        Private Property Name2 As String Implements IPropertyEntity.Name
            Get
                Return Name
            End Get
            Set(value As String)
                Name = value
            End Set
        End Property

        Private Property OriginalName2 As String Implements IPropertyEntity.OriginalName
            Get
                Return OriginalName
            End Get
            Set(value As String)
                OriginalName = value
            End Set
        End Property

        Private Property OriginalVersion2 As String Implements IPropertyEntity.OriginalVersion
            Get
                Return OriginalVersion
            End Get
            Set(value As String)
                OriginalVersion = value
            End Set
        End Property

        Private Property State2 As StateEntity Implements IPropertyEntity.State
            Get
                Return State
            End Get
            Set(value As StateEntity)
                State = value
            End Set
        End Property

        Private Property StateId2 As Integer? Implements IPropertyEntity.StateId
            Get
                Return StateId
            End Get
            Set(value As Integer?)
                StateId = value
            End Set
        End Property

        Private ReadOnly Property StateName2 As String Implements IPropertyEntity.StateName
            Get
                Return StateName
            End Get
        End Property

        Private Property Title2 As String Implements IPropertyEntity.Title
            Get
                Return Title
            End Get
            Set(value As String)
                Title = value
            End Set
        End Property

        Private Property Version2 As String Implements IPropertyEntity.Version
            Get
                Return Version
            End Get
            Set(value As String)
                Version = value
            End Set
        End Property

        Private Property Bank2 As BankEntity Implements IPropertyEntity.Bank
            Get
                Return Bank
            End Get
            Set(value As BankEntity)
                Bank = value
            End Set
        End Property

        Private Property Fields2 As IEntityFields2 Implements IPropertyEntity.Fields
            Get
                Return Fields
            End Get
            Set(value As IEntityFields2)
                Fields = value
            End Set
        End Property

        Private Property IsDirty2() As Boolean Implements IPropertyEntity.IsDirty
            Get
                Return IsDirty
            End Get
            Set(value As Boolean)
                IsDirty = value
            End Set
        End Property

        Private Property IsNew2 As Boolean Implements IPropertyEntity.IsNew
            Get
                Return IsNew
            End Get
            Set(value As Boolean)
                IsNew = value
            End Set
        End Property

        Private Property ResourceData2() As ResourceDataEntity Implements IPropertyEntity.ResourceData
            Get
                Return ResourceData
            End Get
            Set(value As ResourceDataEntity)
                ResourceData = value
            End Set
        End Property

        Private ReadOnly Property DependentResourceCollection2 As HelperClasses.EntityCollection(Of DependentResourceEntity) Implements IPropertyEntity.DependentResourceCollection
            Get
                Return DependentResourceCollection
            End Get
        End Property

        Private ReadOnly Property ReferencedResourceCollection2 As HelperClasses.EntityCollection(Of DependentResourceEntity) Implements IPropertyEntity.ReferencedResourceCollection
            Get
                Return ReferencedResourceCollection
            End Get
        End Property

        Private Function CustomBankPropertyValueCollection2() As HelperClasses.EntityCollection(Of CustomBankPropertyValueEntity) Implements IPropertyEntity.CustomBankPropertyValueCollection
            Return CustomBankPropertyValueCollection
        End Function


        Private Function HasChangesInTopology2() As Boolean Implements IPropertyEntity.HasChangesInTopology
            Return HasChangesInTopology()
        End Function

        Private Function OnlyChangesInWorkflowMetaData2() As Boolean Implements IPropertyEntity.OnlyChangesInWorkflowMetaData
            Return OnlyChangesInWorkflowMetaData()
        End Function


        Private _removedDependentEntities As HelperClasses.EntityCollection


        <CustomXmlSerializationAttribute()> _
        Public ReadOnly Property RemovedDependentEntities() As HelperClasses.EntityCollection Implements IPropertyEntity.RemovedDependentEntities
            Get
                If _removedDependentEntities Is Nothing Then _removedDependentEntities = New HelperClasses.EntityCollection(New FactoryClasses.DependentResourceEntityFactory)
                Return _removedDependentEntities
            End Get
        End Property

        Public Overridable ReadOnly Property ResourceType() As String Implements IPropertyEntity.ResourceType
            Get
                Return "Resource"
            End Get
        End Property

        <ComponentModel.Bindable(True)> _
        Public ReadOnly Property ReferenceCount() As Integer
            Get
                Return Me.ReferencedResourceCollection.Count
            End Get
        End Property

        Public ReadOnly Property CopiedFromString() As String Implements IPropertyEntity.CopiedFromString
            Get
                If Not (String.IsNullOrEmpty(Me.OriginalName)) Then
                    Return $"{Me.OriginalName} ({My.Resources.Version.ToLowerInvariant()} {Me.OriginalVersion.ToString()})"
                End If
                Return String.Empty
            End Get
        End Property


        Public Sub ValidateEntity2() Implements IPropertyEntity.ValidateEntity
            MyBase.ValidateEntity()
        End Sub

        Public Function GetDependentResources() As HelperClasses.EntityCollection(Of ResourceEntity) Implements IPropertyEntity.GetDependentResources
            Dim resources As New HelperClasses.EntityCollection(Of ResourceEntity)
            For Each dependency As DependentResourceEntity In Me.DependentResourceCollection
                resources.Add(dependency.DependentResource)
            Next
            Return resources
        End Function

        Public Overridable Function ContainsDependentResource(ByVal resource As ResourceEntity) As Boolean Implements IPropertyEntity.ContainsDependentResource
            Return Me.DependentResourceCollection.Any(Function(dependency) resource.ResourceId = dependency.DependentResourceId)
        End Function


        Public Overridable Function ContainsDependentResource(ByVal dependendResourceId As Guid) As Boolean Implements IPropertyEntity.ContainsDependentResource
            Return Me.DependentResourceCollection.Any(Function(dependency) dependendResourceId = dependency.DependentResourceId)
        End Function

        Public Function GetDependentResourceByName(ByVal name As String) As EntityClasses.DependentResourceEntity Implements IPropertyEntity.GetDependentResourceByName
            Dim foundResource As EntityClasses.DependentResourceEntity = Nothing

            For Each dependency As DependentResourceEntity In Me.DependentResourceCollection
                If dependency.DependentResource IsNot Nothing AndAlso dependency.DependentResource.Name = name Then
                    foundResource = dependency
                    Exit For
                End If
            Next

            Return foundResource
        End Function



        Protected Overrides Function CreateValidator() As SD.LLBLGen.Pro.ORMSupportClasses.IValidator
            Return New ValidatorClasses.ResourceValidator
        End Function

        Protected Overrides Sub OnInitialized()
            MyBase.OnInitialized()

            AddHandler Me.DependentResourceCollection.EntityRemoved, AddressOf EntityRemovedFromDependentResourceCollection
            AddHandler Me.DependentResourceCollection.EntityAdding, AddressOf BeforeAddingEntityToDependentResourceCollection
        End Sub

        Protected Overrides Sub OnDeserialized(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.OnDeserialized(info, context)

            AddHandler Me.DependentResourceCollection.EntityRemoved, AddressOf EntityRemovedFromDependentResourceCollection
            AddHandler Me.DependentResourceCollection.EntityAdding, AddressOf BeforeAddingEntityToDependentResourceCollection
        End Sub

        Protected Overrides Sub PerformCustomXmlSerialization(ByVal descriptor As System.ComponentModel.PropertyDescriptor, ByVal propertyValue As Object, ByVal writer As System.Xml.XmlWriter, ByVal aspects As SD.LLBLGen.Pro.ORMSupportClasses.XmlFormatAspect)
            If descriptor.DisplayName = "RemovedDependentEntities" Then
                Me.RemovedDependentEntities.WriteXml(writer, aspects)
            End If
        End Sub

        Protected Overrides Sub PerformCustomXmlDeserialization(ByVal descriptor As System.ComponentModel.PropertyDescriptor, ByVal reader As System.Xml.XmlReader)
            If descriptor.DisplayName = "RemovedDependentEntities" Then
                reader.Read()
                Me.RemovedDependentEntities.ReadXml(reader)
                reader.Read()
            End If
        End Sub



        Private Sub EntityRemovedFromDependentResourceCollection(ByVal sender As Object, ByVal e As SD.LLBLGen.Pro.ORMSupportClasses.CollectionChangedEventArgs)
            If Not e.InvolvedEntity.IsNew Then
                RemovedDependentEntities.Add(DirectCast(e.InvolvedEntity, SD.LLBLGen.Pro.ORMSupportClasses.EntityBase2))
            End If
        End Sub

        Private Sub BeforeAddingEntityToDependentResourceCollection(ByVal sender As Object, ByVal e As SD.LLBLGen.Pro.ORMSupportClasses.CancelableCollectionChangedEventArgs)


        End Sub


    End Class

End Namespace