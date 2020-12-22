Option Infer On

Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses
Imports Cito.Tester.Common
Imports System.Text
Imports System.Linq
Imports Enums
Imports Questify.Builder.Model.ContentModel.Interfaces
Imports Questify.Builder.Model.ContentModel.ValidatorClasses

Namespace Questify.Builder.Model.ContentModel.EntityClasses

    Partial Public Class CustomBankPropertyEntity
        Implements IPropertyEntity

        Private _removedDependentEntities As EntityCollection


        Private Property Id As Guid Implements IPropertyEntity.Id
            Get
                Return CustomBankPropertyId
            End Get
            Set(value As Guid)
                CustomBankPropertyId = value
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

        Private Property Title2 As String Implements IPropertyEntity.Title
            Get
                Return Title
            End Get
            Set(value As String)
                Title = value
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

        Private ReadOnly Property CreatedByFullName2 As String Implements IPropertyEntity.CreatedByFullName
            Get
                Return CreatedByFullName
            End Get
        End Property

        Private ReadOnly Property ModifiedByFullName2 As String Implements IPropertyEntity.ModifiedByFullName
            Get
                Return ModifiedByFullName
            End Get
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

        Private Property Version2 As String Implements IPropertyEntity.Version
            Get
                Return Version
            End Get
            Set(value As String)
                Version = value
            End Set
        End Property

        Private ReadOnly Property DependentResourceCollection2 As EntityCollection(Of DependentResourceEntity) Implements IPropertyEntity.DependentResourceCollection
            Get
                Return New EntityCollection(Of DependentResourceEntity)()
            End Get
        End Property

        Private ReadOnly Property ReferencedResourceCollection2 As EntityCollection(Of DependentResourceEntity) Implements IPropertyEntity.ReferencedResourceCollection
            Get
                Return New EntityCollection(Of DependentResourceEntity)()
            End Get
        End Property

        Private Function CustomBankPropertyValueCollection2() As EntityCollection(Of CustomBankPropertyValueEntity) Implements IPropertyEntity.CustomBankPropertyValueCollection
            Return CustomBankPropertyValueCollection
        End Function



        Public Property OriginalName As String Implements IPropertyEntity.OriginalName
            Get
                Return String.Empty
            End Get
            Set(value As String)

            End Set
        End Property

        Public Property OriginalVersion As String Implements IPropertyEntity.OriginalVersion
            Get
                Return Nothing
            End Get
            Set(value As String)

            End Set
        End Property

        Public Property ResourceData() As ResourceDataEntity Implements IPropertyEntity.ResourceData
            Get
                Return Nothing
            End Get
            Set(value As ResourceDataEntity)

            End Set
        End Property

        Public Overridable ReadOnly Property ResourceType() As String Implements IPropertyEntity.ResourceType
            Get
                Return "Custom BankProperty"
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



        Private Function HasChangesInTopology2() As Boolean Implements IPropertyEntity.HasChangesInTopology
            Return HasChangesInTopology()
        End Function

        Private Function OnlyChangesInWorkflowMetaData2() As Boolean Implements IPropertyEntity.OnlyChangesInWorkflowMetaData
            Return OnlyChangesInWorkflowMetaData()
        End Function



        Public Sub ValidateEntity2() Implements IPropertyEntity.ValidateEntity
            MyBase.ValidateEntity()
        End Sub

        Public Function GetDependentResources() As EntityCollection(Of ResourceEntity) Implements IPropertyEntity.GetDependentResources
            Return New EntityCollection(Of ResourceEntity)()
        End Function

        Public Function ContainsDependentResource(ByVal resource As ResourceEntity) As Boolean Implements IPropertyEntity.ContainsDependentResource
            Return False
        End Function

        Public Function ContainsDependentResource(ByVal resourceId As Guid) As Boolean Implements IPropertyEntity.ContainsDependentResource
            Return False
        End Function

        <CustomXmlSerializationAttribute()> _
        Public ReadOnly Property RemovedDependentEntities() As EntityCollection Implements IPropertyEntity.RemovedDependentEntities
            Get
                If _removedDependentEntities Is Nothing Then _removedDependentEntities = New EntityCollection(New DependentResourceEntityFactory)
                Return _removedDependentEntities
            End Get
        End Property

        Public Function GetDependentResourceByName(ByVal name As String) As DependentResourceEntity Implements IPropertyEntity.GetDependentResourceByName
            Return Nothing
        End Function


        Protected Overrides Function CreateValidator() As IValidator
            Return New CustomBankPropertyValidator
        End Function

        Protected Overrides Sub OnSetValueComplete(ByVal fieldIndex As Integer)

            If fieldIndex = CustomBankPropertyFieldIndex.Name Or fieldIndex = CustomBankPropertyFieldIndex.Title Then

                Dim originalValue As String = DirectCast(Me.Fields(fieldIndex).CurrentValue, String)
                Dim updatedValue As String = StringManipulationHelper.ReplaceTabsAndNewLinesBySpaces(originalValue)

                If updatedValue <> originalValue Then
                    Me.Fields(fieldIndex).CurrentValue = updatedValue
                End If
            End If

            MyBase.OnSetValueComplete(fieldIndex)
        End Sub

        Public Function ToResourceManagerSharedMetaData() As MetaData
            Dim metaDataCustomPropertyDefinition As MetaData

            If TypeOf Me Is ListCustomBankPropertyEntity Then
                Dim listCustomBankPropertyEntity As ListCustomBankPropertyEntity = DirectCast(Me, ListCustomBankPropertyEntity)
                Dim multiValueMetaData As MetaDataMultiValue = New MetaDataMultiValue

                multiValueMetaData.Code = Code

                If listCustomBankPropertyEntity.MultipleSelect Then
                    multiValueMetaData.MetaDataSubType = MetaDataMultiValue.enumMetaDataSubType.MultiSelect
                Else
                    multiValueMetaData.MetaDataSubType = MetaDataMultiValue.enumMetaDataSubType.SingleSelect
                End If

                For Each listValue As ListValueCustomBankPropertyEntity In listCustomBankPropertyEntity.ListValueCustomBankPropertyCollection
                    Dim listValueMetaData As New MetaDataCode()

                    listValueMetaData.MetaDatatype = MetaData.enumMetaDataType.BankCustomProperty
                    listValueMetaData.Name = listValue.Name
                    listValueMetaData.Title = listValue.Title
                    listValueMetaData.Code = listValue.Code

                    multiValueMetaData.ListValues.Add(listValueMetaData)
                Next

                metaDataCustomPropertyDefinition = multiValueMetaData
            ElseIf TypeOf Me Is ConceptStructureCustomBankPropertyEntity Then
                Dim conceptStructureCustomBankPropertyEntity As ConceptStructureCustomBankPropertyEntity = DirectCast(Me, ConceptStructureCustomBankPropertyEntity)
                Dim conceptStructureMetaData As MetaDataConceptStructure = New MetaDataConceptStructure

                conceptStructureMetaData.Code = Code

                For Each conceptStructurePart As ConceptStructurePartCustomBankPropertyEntity In conceptStructureCustomBankPropertyEntity.ConceptStructurePartCustomBankPropertyCollection
                    Dim conceptStructurePartMetaData As New MetaDataConceptStructurePart

                    conceptStructurePartMetaData.MetaDatatype = MetaData.enumMetaDataType.BankCustomProperty
                    conceptStructurePartMetaData.Name = conceptStructurePart.Name
                    conceptStructurePartMetaData.Title = conceptStructurePart.Title
                    conceptStructurePartMetaData.ConceptTypeId = conceptStructurePart.ConceptTypeId
                    conceptStructurePartMetaData.Code = conceptStructurePart.Code

                    conceptStructureMetaData.StructureParts.Add(conceptStructurePartMetaData)

                    For Each childConceptStructurePart As ChildConceptStructurePartCustomBankPropertyEntity In conceptStructurePart.ChildConceptStructurePartCustomBankPropertyCollection
                        Dim childConceptStructurePartMetaData As New MetaDataConceptStructurePart

                        childConceptStructurePartMetaData.Code = childConceptStructurePart.ChildConceptStructurePartCustomBankProperty.Code
                        childConceptStructurePartMetaData.Name = childConceptStructurePart.ChildConceptStructurePartCustomBankProperty.Name
                        childConceptStructurePartMetaData.Title = childConceptStructurePart.ChildConceptStructurePartCustomBankProperty.Title
                        childConceptStructurePartMetaData.VisualOrder = childConceptStructurePart.VisualOrder

                        conceptStructurePartMetaData.StructureParts.Add(childConceptStructurePartMetaData)
                    Next
                Next

                metaDataCustomPropertyDefinition = conceptStructureMetaData
            ElseIf TypeOf Me Is TreeStructureCustomBankPropertyEntity Then
                Dim treeStructureCustomBankPropertyEntity As TreeStructureCustomBankPropertyEntity = DirectCast(Me, TreeStructureCustomBankPropertyEntity)
                Dim treeStructureMetaData As MetaDataTreeStructure = New MetaDataTreeStructure()

                treeStructureMetaData.Code = Code

                For Each treeStructurePart As TreeStructurePartCustomBankPropertyEntity In TreeStructureCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyCollection
                    Dim treeStructurePartMetaData As New MetaDataTreeStructurePart()

                    treeStructurePartMetaData.MetaDatatype = MetaData.enumMetaDataType.BankCustomProperty
                    treeStructurePartMetaData.Name = treeStructurePart.Name
                    treeStructurePartMetaData.Title = treeStructurePart.Title
                    treeStructurePartMetaData.Code = treeStructurePart.Code

                    treeStructureMetaData.StructureParts.Add(treeStructurePartMetaData)

                    For Each childTreeStructurePart As ChildTreeStructurePartCustomBankPropertyEntity In treeStructurePart.ChildTreeStructurePartCustomBankPropertyCollection
                        Dim childTreeStructurePartMetaData As New MetaDataTreeStructurePart()
                        Dim childToPart As TreeStructurePartCustomBankPropertyEntity = treeStructureCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyCollection.First(Function(i) i.TreeStructurePartCustomBankPropertyId = childTreeStructurePart.ChildTreeStructurePartCustomBankPropertyId)

                        childTreeStructurePartMetaData.Code = childToPart.Code
                        childTreeStructurePartMetaData.Name = childToPart.Name
                        childTreeStructurePartMetaData.Title = childToPart.Title
                        childTreeStructurePartMetaData.VisualOrder = childTreeStructurePart.VisualOrder
                        treeStructurePartMetaData.StructureParts.Add(childTreeStructurePartMetaData)
                    Next
                Next

                metaDataCustomPropertyDefinition = treeStructureMetaData
            ElseIf TypeOf Me Is RichTextValueCustomBankPropertyEntity Then
                metaDataCustomPropertyDefinition = New MetaDataRichText()
            Else
                metaDataCustomPropertyDefinition = New MetaData()
            End If

            metaDataCustomPropertyDefinition.Name = Name
            metaDataCustomPropertyDefinition.Title = Title
            metaDataCustomPropertyDefinition.ApplicableTo = CInt(ApplicableToMask)
            metaDataCustomPropertyDefinition.MetaDatatype = MetaData.enumMetaDataType.BankCustomProperty
            metaDataCustomPropertyDefinition.Publishable = Publishable
            metaDataCustomPropertyDefinition.Scorable = Scorable

            Return metaDataCustomPropertyDefinition
        End Function


        Public ReadOnly Property ApplicableToString() As String
            Get
                Dim returnValue As New StringBuilder()

                If (ApplicableToMask And CType(ResourceTypeEnum.ItemResource, Integer)) = CType(ResourceTypeEnum.ItemResource, Integer) Then
                    returnValue.AppendFormat("{0}, ", My.Resources.ResourceTypeEnum_Items)
                End If
                If (ApplicableToMask And CType(ResourceTypeEnum.AssessmentTestResource, Integer)) = CType(ResourceTypeEnum.AssessmentTestResource, Integer) Then
                    returnValue.AppendFormat("{0}, ", My.Resources.ResourceTypeEnum_Tests)
                End If
                If (ApplicableToMask And CType(ResourceTypeEnum.GenericResource, Integer)) = CType(ResourceTypeEnum.GenericResource, Integer) Then
                    returnValue.AppendFormat("{0}, ", My.Resources.ResourceTypeEnum_Media)
                End If

                Return returnValue.ToString().TrimEnd(New Char() {" "c, ","c})
            End Get
        End Property

        Public ReadOnly Property CustomPropertyType() As String
            Get
                Dim returnValue As String = String.Empty
                Select Case Me.GetType().ToString()
                    Case GetType(ConceptStructureCustomBankPropertyEntity).ToString()
                        returnValue = My.Resources.ConceptStructure
                    Case GetType(TreeStructureCustomBankPropertyEntity).ToString()
                        returnValue = My.Resources.TreeStructure
                    Case GetType(ListCustomBankPropertyEntity).ToString()
                        Dim listCustomBankPropertyEntity As ListCustomBankPropertyEntity = DirectCast(Me, ListCustomBankPropertyEntity)
                        If listCustomBankPropertyEntity.MultipleSelect Then
                            returnValue = My.Resources.ListMultiple
                        Else
                            returnValue = My.Resources.ListSingle
                        End If
                    Case GetType(FreeValueCustomBankPropertyEntity).ToString()
                        returnValue = My.Resources.FreeValue
                End Select

                Return returnValue
            End Get
        End Property


    End Class

End Namespace
