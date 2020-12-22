Imports System.Linq
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses

Public Class TreeStructureViewerUserControl

    Public Sub New()

        InitializeComponent()


    End Sub

    Friend Sub SetSelectedNode(ByVal name As String)
        If TreeStructureTreeView.Nodes.Count > 0 Then
            Dim foundNodes As TreeNode() = TreeStructureTreeView.Nodes.Find(name, True)

            If foundNodes.Count > 0 Then
                TreeStructureTreeView.SelectedNode = TreeStructureTreeView.Nodes.Find(name, True)(0)
            Else
                TreeStructureTreeView.SelectedNode = Nothing
            End If
        End If
    End Sub

    Friend Function IsTreeValid(ByVal treeStructurePartCustomBankPropertyEntities As EntityCollection(Of TreeStructurePartCustomBankPropertyEntity), ByVal rebuildBeforeValidating As Boolean) As Boolean
        If treeStructurePartCustomBankPropertyEntities.Count > 0 Then
            If rebuildBeforeValidating Then
                CreateTree(treeStructurePartCustomBankPropertyEntities)
            End If

            Dim unusedTreeStructurePartCustomBankPropertyEntityNames As New List(Of String)()

            For Each treeStructurePartCustomBankPropertyEntity As TreeStructurePartCustomBankPropertyEntity In treeStructurePartCustomBankPropertyEntities
                Dim treeNodes As List(Of TreeNode) = TreeStructureTreeView.Nodes.Find(treeStructurePartCustomBankPropertyEntity.Name, True).ToList()

                If treeNodes.Count = 0 Then
                    unusedTreeStructurePartCustomBankPropertyEntityNames.Add(treeStructurePartCustomBankPropertyEntity.Name)
                End If
            Next

            If unusedTreeStructurePartCustomBankPropertyEntityNames.Count > 0 Then
                MessageBox.Show(String.Format(My.Resources.TreeStructureViewer_UnusedStructures, String.Join(", ", unusedTreeStructurePartCustomBankPropertyEntityNames)), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                Return False
            End If
        End If

        Return True
    End Function

    Friend Sub CreateTree(ByVal treeStructurePartCustomBankPropertyEntities As EntityCollection(Of TreeStructurePartCustomBankPropertyEntity))
        TreeStructureTreeView.Nodes.Clear()

        Dim roots As List(Of TreeNode) = FindRoots(treeStructurePartCustomBankPropertyEntities)

        For Each root As TreeNode In roots
            TreeStructureTreeView.Nodes.Add(root)

            AddChildren(root, treeStructurePartCustomBankPropertyEntities)

            TreeStructureTreeView.ExpandAll()
        Next
    End Sub

    Private Sub AddChildren(ByVal parentTreeNode As TreeNode, treeStructurePartCustomBankPropertyEntities As EntityCollection(Of TreeStructurePartCustomBankPropertyEntity))
        Dim rootTreeStructurePartCustomBankPropertyEntity As TreeStructurePartCustomBankPropertyEntity = treeStructurePartCustomBankPropertyEntities.First(Function(i) i.Name = parentTreeNode.Name)

        For Each childTreeStructurePartCustomBankPropertyEntity As ChildTreeStructurePartCustomBankPropertyEntity In rootTreeStructurePartCustomBankPropertyEntity.ChildTreeStructurePartCustomBankPropertyCollection.OrderBy(Function(i) i.VisualOrder)
            Dim treeStructurePartCustomBankPropertyEntity As TreeStructurePartCustomBankPropertyEntity = ConvertChildTreeStructureToTreeStructurePart(childTreeStructurePartCustomBankPropertyEntity)
            Dim newNode As TreeNode = AddChild(parentTreeNode, treeStructurePartCustomBankPropertyEntity)
            newNode.Tag = treeStructurePartCustomBankPropertyEntity

            AddChildren(newNode, ConvertChildTreeStructurePartCustomBankPropertyEntitiesToTreeStructurePartCustomBankPropertyEntities(rootTreeStructurePartCustomBankPropertyEntity.ChildTreeStructurePartCustomBankPropertyCollection))
        Next
    End Sub

    Private Function AddChild(ByVal parentTreeNode As TreeNode, treeStructurePartCustomBankPropertyEntity As TreeStructurePartCustomBankPropertyEntity) As TreeNode
        Dim newNode As New TreeNode(String.Format("{0} - {1}", treeStructurePartCustomBankPropertyEntity.Name, treeStructurePartCustomBankPropertyEntity.Title))

        newNode.Name = treeStructurePartCustomBankPropertyEntity.Name
        parentTreeNode.Nodes.Add(newNode)

        Return newNode
    End Function

    Private Function ConvertChildTreeStructurePartCustomBankPropertyEntitiesToTreeStructurePartCustomBankPropertyEntities(ByVal ChildTreeStructurePartCustomBankPropertyEntities As EntityCollection(Of ChildTreeStructurePartCustomBankPropertyEntity)) As EntityCollection(Of TreeStructurePartCustomBankPropertyEntity)
        Dim result As New EntityCollection(Of TreeStructurePartCustomBankPropertyEntity)()

        For Each child As ChildTreeStructurePartCustomBankPropertyEntity In ChildTreeStructurePartCustomBankPropertyEntities
            Dim treeStructurePartCustomBankPropertyEntity As TreeStructurePartCustomBankPropertyEntity = ConvertChildTreeStructureToTreeStructurePart(child)

            If treeStructurePartCustomBankPropertyEntity IsNot Nothing Then
                result.Add(treeStructurePartCustomBankPropertyEntity)
            End If
        Next

        Return result
    End Function

    Private Function ConvertChildTreeStructureToTreeStructurePart(ByVal childTreeStructurePartCustomBankPropertyEntity As ChildTreeStructurePartCustomBankPropertyEntity) As TreeStructurePartCustomBankPropertyEntity
        Return childTreeStructurePartCustomBankPropertyEntity.TreeStructurePartCustomBankProperty.TreeStructureCustomBankProperty.TreeStructurePartCustomBankPropertyCollection.First(Function(i) i.TreeStructurePartCustomBankPropertyId = childTreeStructurePartCustomBankPropertyEntity.ChildTreeStructurePartCustomBankPropertyId)
    End Function

    Private Function FindRoots(ByVal treeStructurePartCustomBankPropertyEntities As EntityCollection(Of TreeStructurePartCustomBankPropertyEntity)) As List(Of TreeNode)
        Dim roots As New List(Of TreeNode)()
        Dim childTreeStructurePartCustomBankPropertyEntities As New List(Of TreeStructurePartCustomBankPropertyEntity)()

        For Each treeStructurePartCustomBankPropertyEntity As TreeStructurePartCustomBankPropertyEntity In treeStructurePartCustomBankPropertyEntities
            Dim childTreeStructurePartCustomBankPropertyEntityWithoutParent As ChildTreeStructurePartCustomBankPropertyEntity = Nothing

            For Each treeStructurePartCustomBankPropertyEntity2 As TreeStructurePartCustomBankPropertyEntity In treeStructurePartCustomBankPropertyEntities
                childTreeStructurePartCustomBankPropertyEntityWithoutParent = treeStructurePartCustomBankPropertyEntity2.ChildTreeStructurePartCustomBankPropertyCollection.FirstOrDefault(Function(i) i.ChildTreeStructurePartCustomBankPropertyId = treeStructurePartCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyId)

                If childTreeStructurePartCustomBankPropertyEntityWithoutParent IsNot Nothing Then
                    Exit For
                End If
            Next

            If childTreeStructurePartCustomBankPropertyEntityWithoutParent Is Nothing Then
                childTreeStructurePartCustomBankPropertyEntities.Add(treeStructurePartCustomBankPropertyEntity)
            End If
        Next

        For Each treeStructurePartCustomBankPropertyEntity As TreeStructurePartCustomBankPropertyEntity In childTreeStructurePartCustomBankPropertyEntities
            If treeStructurePartCustomBankPropertyEntity.ChildTreeStructurePartCustomBankPropertyCollection.Count > 0 Then
                Dim node As New TreeNode(String.Format("{0} - {1}", treeStructurePartCustomBankPropertyEntity.Name, treeStructurePartCustomBankPropertyEntity.Title))
                node.Name = treeStructurePartCustomBankPropertyEntity.Name
                node.Tag = treeStructurePartCustomBankPropertyEntity

                roots.Add(node)
            End If
        Next

        Return roots
    End Function

End Class
