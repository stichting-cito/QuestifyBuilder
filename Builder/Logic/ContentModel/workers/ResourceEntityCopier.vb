Imports System.Linq
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.Cloning
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace ContentModel

    Friend Class ResourceEntityCopier

        Public Function GetCopy(Of T As ResourceEntity)(toCopy As T, newName As String) As T
            Return GetCopy(toCopy, newName, toCopy.name)
        End Function

        Public Function GetCopy(Of T As ResourceEntity)(toCopy As T, newName As String, oldName As String) As T

            Dim newResourceEntity = GetNewEntity(Of T)(toCopy, newName, oldName)

            CopyBankProperties(toCopy, newResourceEntity)

            If TypeOf newResourceEntity Is ItemResourceEntity Then
                Dim itemResource = CType(CObj(newResourceEntity), ItemResourceEntity)

                itemResource.UpdateDependencies()
            Else
                CopyDependentResources(toCopy, newResourceEntity)
            End If


            Return newResourceEntity

        End Function

        Private Function GetNewEntity(Of T As ResourceEntity)(toCopy As T, newName As String, oldName As String) As T
            Dim ret As T = Activator.CreateInstance(Of T)()

            For Each fld As EntityField2 In toCopy.Fields
                If ret.Fields.Item(fld.Name) Is Nothing Then
                    Debug.Assert(False, "Field not present, while it should be the same type.")
                ElseIf Not fld.IsReadOnly Then
                    ret.Fields(fld.Name).CurrentValue = fld.CurrentValue
                End If
            Next
            ret.Name = newName
            ret.ResourceId = Guid.NewGuid
            Dim specificCloner As IResourceCloner = BaseResourceCloner.GetResourceCloner(ret)
            specificCloner.DoSpecificClone(toCopy)

            ret.OriginalName = oldName
            ret.OriginalVersion = toCopy.Version
            ret.ResourceData = New ResourceDataEntity()
            If (toCopy.ResourceData IsNot Nothing) Then
                ret.ResourceData.BinData = toCopy.ResourceData.BinData
            End If
            ret.SetIdentifierToSharedModel()
            Return ret
        End Function

        Private Sub CopyBankProperties(toCopy As ResourceEntity, newResourceEntity As ResourceEntity)
            For Each customProperty As CustomBankPropertyValueEntity In toCopy.CustomBankPropertyValueCollection
                Select Case customProperty.GetType()
                    Case GetType(FreeValueCustomBankPropertyValueEntity)
                        Dim newProperty As New FreeValueCustomBankPropertyValueEntity(newResourceEntity.ResourceId, customProperty.CustomBankPropertyId)
                        newProperty.Value = DirectCast(customProperty, FreeValueCustomBankPropertyValueEntity).Value
                        newProperty.DisplayValue = customProperty.DisplayValue
                        newResourceEntity.CustomBankPropertyValueCollection.Add(newProperty)
                    Case GetType(RichTextValueCustomBankPropertyValueEntity)
                        Dim newProperty As New RichTextValueCustomBankPropertyValueEntity(newResourceEntity.ResourceId, customProperty.CustomBankPropertyId)
                        newProperty.Value = DirectCast(customProperty, RichTextValueCustomBankPropertyValueEntity).Value
                        newProperty.DisplayValue = customProperty.DisplayValue
                        newResourceEntity.CustomBankPropertyValueCollection.Add(newProperty)
                    Case GetType(ListCustomBankPropertyValueEntity)
                        Dim newProperty As New ListCustomBankPropertyValueEntity(newResourceEntity.ResourceId, customProperty.CustomBankPropertyId)
                        For Each selectedValue As ListCustomBankPropertySelectedValueEntity In DirectCast(customProperty, ListCustomBankPropertyValueEntity).ListCustomBankPropertySelectedValueCollection
                            Dim newSelectedValue As New ListCustomBankPropertySelectedValueEntity(newResourceEntity.ResourceId, customProperty.CustomBankPropertyId, selectedValue.ListValueBankCustomPropertyId)
                            newProperty.ListCustomBankPropertySelectedValueCollection.Add(newSelectedValue)
                        Next
                        newProperty.DisplayValue = customProperty.DisplayValue
                        newResourceEntity.CustomBankPropertyValueCollection.Add(newProperty)
                    Case GetType(TreeStructureCustomBankPropertyValueEntity)
                        Dim newProperty As New TreeStructureCustomBankPropertyValueEntity(newResourceEntity.ResourceId, customProperty.CustomBankPropertyId)
                        For Each selectedValue As TreeStructureCustomBankPropertySelectedPartEntity In DirectCast(customProperty, TreeStructureCustomBankPropertyValueEntity).TreeStructureCustomBankPropertySelectedPartCollection
                            Dim newSelectedValue As New TreeStructureCustomBankPropertySelectedPartEntity(selectedValue.TreeStructurePartId, newResourceEntity.ResourceId, customProperty.CustomBankPropertyId)
                            newProperty.TreeStructureCustomBankPropertySelectedPartCollection.Add(newSelectedValue)
                        Next
                        newProperty.DisplayValue = customProperty.DisplayValue
                        newResourceEntity.CustomBankPropertyValueCollection.Add(newProperty)
                    Case GetType(ConceptStructureCustomBankPropertyValueEntity)
                        Dim newProperty As New ConceptStructureCustomBankPropertyValueEntity(newResourceEntity.ResourceId, customProperty.CustomBankPropertyId)
                        For Each selectedPart As ConceptStructureCustomBankPropertySelectedPartEntity In DirectCast(customProperty, ConceptStructureCustomBankPropertyValueEntity).ConceptStructureCustomBankPropertySelectedPartCollection
                            Dim newSelectedPart As New ConceptStructureCustomBankPropertySelectedPartEntity(selectedPart.ConceptStructurePartId, newProperty.ResourceId, newProperty.CustomBankPropertyId)
                            newProperty.ConceptStructureCustomBankPropertySelectedPartCollection.Add(newSelectedPart)
                        Next
                        newProperty.DisplayValue = customProperty.DisplayValue
                        newResourceEntity.CustomBankPropertyValueCollection.Add(newProperty)
                End Select
            Next
        End Sub

        Private Sub CopyDependentResources(toCopy As ResourceEntity, newResourceEntity As ResourceEntity)
            Dim resourceIds = toCopy.DependentResourceCollection.Select(Function(s) s.DependentResourceId)
            For Each depResourceId In resourceIds
                Dim tempDependency As DependentResourceEntity = newResourceEntity.DependentResourceCollection.AddNew()
                With tempDependency
                    .ResourceId = newResourceEntity.ResourceId
                    .DependentResourceId = depResourceId
                End With
            Next
        End Sub


    End Class

End Namespace
