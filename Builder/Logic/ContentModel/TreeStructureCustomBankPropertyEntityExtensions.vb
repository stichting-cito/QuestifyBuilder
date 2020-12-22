Imports System.Runtime.CompilerServices
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.Linq

Namespace ContentModel
    Public Module TreeStructureCustomBankPropertyEntityExtensions


        <Extension>
        Public Function GetTreeStructurePartById(entity As TreeStructureCustomBankPropertyEntity, id As Guid) As TreeStructurePartCustomBankPropertyEntity
            For Each treeStructurePartCustomBankPropertyEntity In entity.TreeStructurePartCustomBankPropertyCollection
                Dim treeStructurePart As TreeStructurePartCustomBankPropertyEntity = GetTreeStructurePartById(id, treeStructurePartCustomBankPropertyEntity)

                If Not treeStructurePart Is Nothing Then
                    Return treeStructurePart
                End If
            Next

            Return Nothing
        End Function



        Private Function GetTreeStructurePartById(id As Guid, treeStructurePartCustomBankPropertyEntity As TreeStructurePartCustomBankPropertyEntity) As TreeStructurePartCustomBankPropertyEntity
            If treeStructurePartCustomBankPropertyEntity.Code = id Then
                Return treeStructurePartCustomBankPropertyEntity
            End If

            For Each child As ChildTreeStructurePartCustomBankPropertyEntity In treeStructurePartCustomBankPropertyEntity.ChildTreeStructurePartCustomBankPropertyCollection
                If child.TreeStructurePartCustomBankPropertyId = treeStructurePartCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyId Then
                    Dim childPart As TreeStructurePartCustomBankPropertyEntity = ConvertChildTreeStructurePartCustomBankPropertyEntityToTreeStructurePartCustomBankPropertyEntity(child)

                    Return GetTreeStructurePartById(id, childPart)
                End If
            Next

            Return Nothing
        End Function



        Private Function ConvertChildTreeStructurePartCustomBankPropertyEntityToTreeStructurePartCustomBankPropertyEntity(childTreeStructurePartCustomBankPropertyEntity As ChildTreeStructurePartCustomBankPropertyEntity) As TreeStructurePartCustomBankPropertyEntity
            Return childTreeStructurePartCustomBankPropertyEntity.TreeStructurePartCustomBankProperty.TreeStructureCustomBankProperty.TreeStructurePartCustomBankPropertyCollection _
                .First(Function(x) x.TreeStructurePartCustomBankPropertyId = childTreeStructurePartCustomBankPropertyEntity.ChildTreeStructurePartCustomBankPropertyId)
        End Function



        <Extension>
        Public Function GetTreeStructurePartsForItem(treeStructure As TreeStructureCustomBankPropertyEntity, item As ItemResourceEntity) As List(Of TreeStructurePartCustomBankPropertyEntity)

            Dim propertyValue As TreeStructureCustomBankPropertyValueEntity = DirectCast(item.CustomBankPropertyValueCollection.First(Function(pv) TypeOf pv Is TreeStructureCustomBankPropertyValueEntity), TreeStructureCustomBankPropertyValueEntity)
            Dim selectedParts As List(Of TreeStructurePartCustomBankPropertyEntity) = propertyValue.TreeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart.ToList()
            Dim parts As List(Of TreeStructurePartCustomBankPropertyEntity) = ConvertSelectedPartListToPartList(treeStructure, selectedParts)

            Return parts
        End Function



        Private Function ConvertSelectedPartListToPartList(treeStructure As TreeStructureCustomBankPropertyEntity, selectedParts As List(Of TreeStructurePartCustomBankPropertyEntity)) As List(Of TreeStructurePartCustomBankPropertyEntity)
            Dim parts As New List(Of TreeStructurePartCustomBankPropertyEntity)()

            For Each part As TreeStructurePartCustomBankPropertyEntity In treeStructure.TreeStructurePartCustomBankPropertyCollection
                If selectedParts.Any(Function(sp) sp.TreeStructurePartCustomBankPropertyId = part.TreeStructurePartCustomBankPropertyId) Then
                    parts.Add(part)
                End If
            Next

            Return parts
        End Function



        <Extension>
        Public Function GetLeaves(parts As List(Of TreeStructurePartCustomBankPropertyEntity)) As List(Of TreeStructurePartCustomBankPropertyEntity)
            Dim leaves As New List(Of TreeStructurePartCustomBankPropertyEntity)()
            Dim partGuids = parts.Select(Function(p) p.TreeStructurePartCustomBankPropertyId).ToList()

            For Each part In parts
                If Not part.HasChildren() Then
                    leaves.Add(part)
                ElseIf Not part.ChildTreeStructurePartCustomBankPropertyCollection.Any(Function(c) partGuids.Contains(c.ChildTreeStructurePartCustomBankPropertyId)) Then
                    leaves.Add(part)
                End If
            Next

            Return leaves
        End Function



        <Extension>
        Public Function GetLeaves(part As TreeStructurePartCustomBankPropertyEntity) As List(Of TreeStructurePartCustomBankPropertyEntity)
            Dim leaves As New List(Of TreeStructurePartCustomBankPropertyEntity)()

            If Not part.HasChildren() Then
                leaves.Add(part)
            End If

            Return leaves
        End Function

        <Extension>
        Public Function GetParents(part As TreeStructurePartCustomBankPropertyEntity, treeStructure As TreeStructureCustomBankPropertyEntity) As List(Of TreeStructurePartCustomBankPropertyEntity)
            Dim returnValue As New List(Of TreeStructurePartCustomBankPropertyEntity)

            GetParentsRecursive(part, treeStructure, returnValue)

            Return returnValue
        End Function


        Private Sub GetParentsRecursive(part As TreeStructurePartCustomBankPropertyEntity, treeStructure As TreeStructureCustomBankPropertyEntity, ByRef list As List(Of TreeStructurePartCustomBankPropertyEntity))
            Dim parents As New List(Of TreeStructurePartCustomBankPropertyEntity)

            For Each potiantialParent In treeStructure.TreeStructurePartCustomBankPropertyCollection
                If potiantialParent.ChildTreeStructurePartCustomBankPropertyCollection.Where(Function(childPart) ConvertChildTreeStructurePartCustomBankPropertyEntityToTreeStructurePartCustomBankPropertyEntity(childPart).Code = part.Code).Count > 0 Then
                    If Not parents.Contains(potiantialParent) Then
                        parents.Add(potiantialParent)
                    End If
                End If
            Next

            list = list.Union(parents).ToList

            For Each parent In parents
                GetParentsRecursive(parent, treeStructure, list)
            Next
        End Sub


        <Extension>
        Public Function HasChildren(part As TreeStructurePartCustomBankPropertyEntity) As Boolean
            Return part.ChildTreeStructurePartCustomBankPropertyCollection.Count > 0
        End Function
    End Module
End Namespace
