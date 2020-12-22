Imports System.Linq
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Questify.Builder.Model.ContentModel.EntityClasses

Public Class TreeStructureEditor

    Public Sub New()

        InitializeComponent()


    End Sub

    Public Sub New(ByVal treeStructureCustomBankPropertyEntity As TreeStructureCustomBankPropertyEntity, ByVal treeStructureCustomBankPropertyValueEntity As TreeStructureCustomBankPropertyValueEntity)
        Me.New()

        Me.Text = String.Format(My.Resources.SelectTreeStructureValues, treeStructureCustomBankPropertyEntity.Name)

        TreeStructureViewerUserControl1.previewLabel.Visible = False
        TreeStructureViewerUserControl1.TreeStructureTreeView.CheckBoxes = True
        TreeStructureViewerUserControl1.CreateTree(treeStructureCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyCollection)

        AddHandler TreeStructureViewerUserControl1.TreeStructureTreeView.AfterCheck, Sub(sender2 As Object, e2 As TreeViewEventArgs)
                                                                                         If e2.Node.Checked Then
                                                                                             If e2.Node.Parent IsNot Nothing Then
                                                                                                 e2.Node.Parent.Checked = True
                                                                                             End If
                                                                                         Else
                                                                                             For Each node As TreeNode In e2.Node.Nodes
                                                                                                 node.Checked = False
                                                                                             Next
                                                                                         End If
                                                                                     End Sub

        SetSelectedValues(treeStructureCustomBankPropertyEntity, treeStructureCustomBankPropertyValueEntity)
    End Sub

    Private Sub SetSelectedValues(ByVal treeStructureCustomBankPropertyEntity As TreeStructureCustomBankPropertyEntity, ByVal treeStructureCustomBankPropertyValueEntity As TreeStructureCustomBankPropertyValueEntity)
        If treeStructureCustomBankPropertyValueEntity IsNot Nothing Then
            For Each treeStructureCustomBankPropertySelectedPartEntity As TreeStructureCustomBankPropertySelectedPartEntity In treeStructureCustomBankPropertyValueEntity.TreeStructureCustomBankPropertySelectedPartCollection
                Dim selectedAsPart As TreeStructurePartCustomBankPropertyEntity = treeStructureCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyCollection.First(Function(i) i.TreeStructurePartCustomBankPropertyId = treeStructureCustomBankPropertySelectedPartEntity.TreeStructurePartId)

                Dim treeNodes As TreeNode() = TreeStructureViewerUserControl1.TreeStructureTreeView.Nodes.Find(selectedAsPart.Name, True)
                treeNodes(0).Checked = True
            Next
        End If
    End Sub

    Private Sub ButtonOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOK.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Friend Function GetEntitiesOfCheckedCheckBoxesOfTreeView(ByVal nodes As TreeNodeCollection) As EntityCollection(Of TreeStructurePartCustomBankPropertyEntity)
        Dim result As New EntityCollection(Of TreeStructurePartCustomBankPropertyEntity)()

        If nodes IsNot Nothing Then
            For Each node As TreeNode In nodes
                If node.Checked Then
                    result.Add(CType(node.Tag, TreeStructurePartCustomBankPropertyEntity))
                End If

                result.AddRange(GetEntitiesOfCheckedCheckBoxesOfTreeView(node.Nodes))
            Next
        End If

        Return result
    End Function


End Class