Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports System.Linq
Imports SD.LLBLGen.Pro.ORMSupportClasses
Imports System.ComponentModel
Imports Questify.Builder.Logic.Service.Factories

Public Class TreeStructureUserControl

    Private _deptControls As Dictionary(Of DeptTreeStructureUserControl, List(Of TreeStructurePartCustomBankPropertyEntity)) = New Dictionary(Of DeptTreeStructureUserControl, List(Of TreeStructurePartCustomBankPropertyEntity))

    Public Sub New()
        InitializeComponent()
    End Sub

    Private ReadOnly Property TreeStructureCustomBankPropertyEntity As TreeStructureCustomBankPropertyEntity
        Get
            Return CType(CustomBankProperty, TreeStructureCustomBankPropertyEntity)
        End Get
    End Property

    Public Overrides ReadOnly Property ErrorMessage As String
        Get
            Return StructureErrorProvider.GetError(StructureTableLayoutPanel)
        End Get
    End Property

    Public Overrides Sub Initialize(customBankProperty As CustomBankPropertyEntity, ByVal initAsReadOnly As Boolean)
        MyBase.Initialize(customBankProperty, initAsReadOnly)

        TreeStructureGrid.Initialize(TreeStructureCustomBankPropertyEntity)

        AddHandler Me.Enter, AddressOf TreeStructureUserControl_Enter

        AddHandler TreeStructureViewerUserControl.TreeStructureTreeView.AfterSelect, Sub(sender As Object, e As TreeViewEventArgs)
                                                                                         TreeStructureGrid.SelectRowWithMatchingName(e.Node.Name)
                                                                                     End Sub

        AddHandler TreeStructureGrid.ListViewData.ItemSelectionChanged, Sub()
                                                                            If TreeStructureGrid.SelectedRow Is Nothing Then
                                                                                Return
                                                                            End If

                                                                            CreateDependencyControls()

                                                                            TreeStructureViewerUserControl.SetSelectedNode(TreeStructureGrid.SelectedRow.Name)

                                                                            If StructureTableLayoutPanel.Enabled Then
                                                                                ButtonAdd.Enabled = True
                                                                            End If
                                                                        End Sub

        TreeStructureGrid.SelectFirstRow()
    End Sub

    Private Sub CreateTree()
        If TreeStructureCustomBankPropertyEntity IsNot Nothing Then
            SuspendLayout()
            TreeStructureViewerUserControl.CreateTree(TreeStructureCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyCollection)

            If TreeStructureGrid.SelectedRow IsNot Nothing Then
                TreeStructureViewerUserControl.SetSelectedNode(TreeStructureGrid.SelectedRow.Name)
            End If

            ResumeLayout()
        End If
    End Sub

    Private Sub TreeStructurePartCustomBankPropertyEntity_ChildTreeStructurePartCustomBankPropertyCollection_EntityRemoved(ByVal sender As Object, e As CollectionChangedEventArgs)
        Me.RemovedEntities.Add(DirectCast(e.InvolvedEntity, EntityBase2))
    End Sub

    Private Sub CreateDependencyControls()
        SuspendLayout()

        StructureTableLayoutPanel.Controls.Clear()
        StructureTableLayoutPanel.RowStyles.Clear()

        If TreeStructureGrid.SelectedRow IsNot Nothing Then

            Dim selectedTreeStructurePartCustomBankPropertyEntity As TreeStructurePartCustomBankPropertyEntity = TreeStructureGrid.SelectedRow
            Dim sortOrder As Integer = 0

            For Each childTreeStructurePartCustomBankPropertyEntity As ChildTreeStructurePartCustomBankPropertyEntity In selectedTreeStructurePartCustomBankPropertyEntity.ChildTreeStructurePartCustomBankPropertyCollection.OrderBy(Function(i) i.VisualOrder)
                Dim deptTreeStructureUserControl As DeptTreeStructureUserControl = New DeptTreeStructureUserControl(childTreeStructurePartCustomBankPropertyEntity, TreeStructureCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyCollection)

                childTreeStructurePartCustomBankPropertyEntity.VisualOrder = sortOrder
                deptTreeStructureUserControl.ComboBoxDep.SelectedItem = TreeStructureCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyCollection.FirstOrDefault(Function(i) i.TreeStructurePartCustomBankPropertyId = childTreeStructurePartCustomBankPropertyEntity.ChildTreeStructurePartCustomBankPropertyId AndAlso childTreeStructurePartCustomBankPropertyEntity.Id <> Guid.Empty)

                AddHandler deptTreeStructureUserControl.ComboBoxDep.SelectedIndexChanged, AddressOf DeptTreeStructureUserControl_ComboBoxDep_SelectedIndexChanged
                AddHandler deptTreeStructureUserControl.DeleteDependencyEvent, AddressOf DeptTreeStructureUserControl_Delete

                deptTreeStructureUserControl.Dock = DockStyle.Top
                deptTreeStructureUserControl.Height = 30

                StructureTableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Absolute, 34))
                Dim rowIndex As Integer = StructureTableLayoutPanel.RowStyles.Count - 1
                StructureTableLayoutPanel.Controls.Add(deptTreeStructureUserControl, 0, rowIndex)

                sortOrder += 1
            Next

            If Not TryGiveValueToNewDeptControl(selectedTreeStructurePartCustomBankPropertyEntity) Then
                MessageBox.Show(Me, My.Resources.NoMoreTreeStructuresToAdd, String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

        End If

        RemoveUnavailableValuesFromComboBoxes(GetValidTreeStructures())

        ResumeLayout()
    End Sub

    Private Function TryGiveValueToNewDeptControl(selectedTreeStructurePartCustomBankPropertyEntity As TreeStructurePartCustomBankPropertyEntity) As Boolean
        Dim availableStructures As List(Of TreeStructurePartCustomBankPropertyEntity) = GetValidTreeStructures()
        Dim result As Boolean = True

        _deptControls.Clear()

        For Each deptTreeStructureUserControl As DeptTreeStructureUserControl In StructureTableLayoutPanel.Controls
            Dim selectedItem As TreeStructurePartCustomBankPropertyEntity = CType(deptTreeStructureUserControl.ComboBoxDep.SelectedItem, TreeStructurePartCustomBankPropertyEntity)

            If selectedItem Is Nothing Then
                Dim newChild As ChildTreeStructurePartCustomBankPropertyEntity = Nothing

                For Each child As ChildTreeStructurePartCustomBankPropertyEntity In TreeStructureGrid.SelectedRow.ChildTreeStructurePartCustomBankPropertyCollection
                    If child.Id = Guid.Empty AndAlso child.ChildTreeStructurePartCustomBankPropertyId.ToString() = deptTreeStructureUserControl.Name Then
                        newChild = child
                        Exit For
                    End If
                Next

                If newChild IsNot Nothing Then
                    If availableStructures.Count > 0 Then
                        If newChild IsNot Nothing Then
                            newChild.ChildTreeStructurePartCustomBankPropertyId = availableStructures(0).TreeStructurePartCustomBankPropertyId
                            newChild.Id = Guid.NewGuid()
                            deptTreeStructureUserControl.ComboBoxDep.SelectedItem = availableStructures(0)
                            result = True
                            Exit For
                        End If
                    Else
                        selectedTreeStructurePartCustomBankPropertyEntity.ChildTreeStructurePartCustomBankPropertyCollection.Remove(newChild)
                        StructureTableLayoutPanel.Controls.RemoveAt(StructureTableLayoutPanel.Controls.Count - 1)
                        result = False
                        Exit For
                    End If
                End If
            End If

            AddToListOfComboBoxValues(selectedItem, deptTreeStructureUserControl)
        Next

        Return result
    End Function

    Private Sub DeptTreeStructureUserControl_ComboBoxDep_SelectedIndexChanged(ByVal sender As Object, e As EventArgs)
        Dim comboBox As ComboBox = CType(sender, ComboBox)

        If comboBox.Tag IsNot Nothing Then
            Dim comboBoxSelectedTreeStructurePartCustomBankPropertyEntity As TreeStructurePartCustomBankPropertyEntity = CType(comboBox.SelectedItem, TreeStructurePartCustomBankPropertyEntity)
            Dim gridSelectedTreeStructurePartCustomBankPropertyEntity As TreeStructurePartCustomBankPropertyEntity = TreeStructureGrid.SelectedRow
            Dim newValueSet As Boolean = False

            For Each gridChild As ChildTreeStructurePartCustomBankPropertyEntity In gridSelectedTreeStructurePartCustomBankPropertyEntity.ChildTreeStructurePartCustomBankPropertyCollection
                For Each deptTreeStructureUserControl As DeptTreeStructureUserControl In StructureTableLayoutPanel.Controls
                    If deptTreeStructureUserControl.ChildTreeStructurePartCustomBankPropertyEntity Is gridChild AndAlso deptTreeStructureUserControl.ComboBoxDep Is comboBox Then
                        gridChild.ChildTreeStructurePartCustomBankPropertyId = comboBoxSelectedTreeStructurePartCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyId

                        comboBox.Tag = comboBox.SelectedItem
                        newValueSet = True

                        CreateDependencyControls()
                        CreateTree()
                        Exit For
                    End If
                Next
            Next
        End If
    End Sub

    Private Sub AddToListOfComboBoxValues(ByVal selectedItem As TreeStructurePartCustomBankPropertyEntity, ByVal deptUserControl As DeptTreeStructureUserControl)
        Dim list = New List(Of TreeStructurePartCustomBankPropertyEntity)()

        For Each treeStructurePartCustomBankPropertyEntity As TreeStructurePartCustomBankPropertyEntity In TreeStructureCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyCollection
            list.Add(treeStructurePartCustomBankPropertyEntity)
        Next

        list.Remove(selectedItem)
        list.Insert(0, selectedItem)

        _deptControls.Add(deptUserControl, list)
    End Sub

    Private Sub RemoveUnavailableValuesFromComboBoxes(ByVal validTreeStructures As IEnumerable(Of TreeStructurePartCustomBankPropertyEntity))
        For Each kvp As KeyValuePair(Of DeptTreeStructureUserControl, List(Of TreeStructurePartCustomBankPropertyEntity)) In _deptControls
            For i As Integer = kvp.Value.Count - 1 To 0 Step -1
                Dim value As TreeStructurePartCustomBankPropertyEntity = kvp.Value(i)

                If Not validTreeStructures.Contains(value) AndAlso value IsNot kvp.Value.First() Then
                    kvp.Value.Remove(value)
                End If
            Next i

            kvp.Key.ComboBoxDep.DataSource = kvp.Value
        Next
    End Sub

    Private Sub AddDependentTreeStructureUserControl()
        If TreeStructureGrid.SelectedRow Is Nothing Then
            TreeStructureGrid.SelectFirstRow()
        End If

        Dim selectedTreeStructurePartCustomBankPropertyEntity As TreeStructurePartCustomBankPropertyEntity = TreeStructureGrid.SelectedRow
        Dim newChildTreeStructurePartCustomBankPropertyEntity As ChildTreeStructurePartCustomBankPropertyEntity = New ChildTreeStructurePartCustomBankPropertyEntity()

        newChildTreeStructurePartCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyId = selectedTreeStructurePartCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyId
        newChildTreeStructurePartCustomBankPropertyEntity.VisualOrder = 200
        newChildTreeStructurePartCustomBankPropertyEntity.ChildTreeStructurePartCustomBankPropertyId = TreeStructureCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyCollection.First().TreeStructurePartCustomBankPropertyId
        selectedTreeStructurePartCustomBankPropertyEntity.ChildTreeStructurePartCustomBankPropertyCollection.Add(newChildTreeStructurePartCustomBankPropertyEntity)

        CreateDependencyControls()
        CreateTree()
    End Sub

    Private Function GetValidTreeStructures() As List(Of TreeStructurePartCustomBankPropertyEntity)
        Dim result As New List(Of TreeStructurePartCustomBankPropertyEntity)()

        For Each treeStructurePartCustomBankPropertyEntity As TreeStructurePartCustomBankPropertyEntity In TreeStructureCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyCollection
            If treeStructurePartCustomBankPropertyEntity.ChildTreeStructurePartCustomBankPropertyCollection.FirstOrDefault(Function(i) i.ChildTreeStructurePartCustomBankPropertyId = treeStructurePartCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyId) Is Nothing Then
                If Not IsStructureUsedForEntity(treeStructurePartCustomBankPropertyEntity) Then
                    If Not HasParent(treeStructurePartCustomBankPropertyEntity) Then
                        If Not ExistsUpTheTree(treeStructurePartCustomBankPropertyEntity, treeStructurePartCustomBankPropertyEntity.ChildTreeStructurePartCustomBankPropertyCollection) Then
                            result.Add(treeStructurePartCustomBankPropertyEntity)
                        End If
                    End If
                End If
            End If
        Next

        Return result
    End Function

    Private Function IsStructureUsedForEntity(ByVal treeStructurePartCustomBankPropertyEntity As TreeStructurePartCustomBankPropertyEntity) As Boolean
        For Each control As UserControl In StructureTableLayoutPanel.Controls
            If TypeOf control Is DeptTreeStructureUserControl Then
                If CType(control, DeptTreeStructureUserControl).ComboBoxDep.SelectedItem IsNot Nothing AndAlso CType(CType(control, DeptTreeStructureUserControl).ComboBoxDep.SelectedItem, TreeStructurePartCustomBankPropertyEntity).TreeStructurePartCustomBankPropertyId = treeStructurePartCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyId Then
                    Return True
                End If
            End If
        Next

        Return False
    End Function

    Private Function ExistsUpTheTree(ByVal treeStructurePartCustomBankPropertyEntity As TreeStructurePartCustomBankPropertyEntity, childTreeStructurePartCustomBankPropertyEntities As EntityCollection(Of ChildTreeStructurePartCustomBankPropertyEntity)) As Boolean
        Dim found As Boolean = childTreeStructurePartCustomBankPropertyEntities.FirstOrDefault(Function(i) i.TreeStructurePartCustomBankPropertyId = treeStructurePartCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyId) IsNot Nothing

        If found Then
            Return True
        Else
            For Each childTreeStructurePartCustomBankPropertyEntity As ChildTreeStructurePartCustomBankPropertyEntity In childTreeStructurePartCustomBankPropertyEntities
                Return ExistsUpTheTree(childTreeStructurePartCustomBankPropertyEntity.TreeStructurePartCustomBankProperty, childTreeStructurePartCustomBankPropertyEntities)
            Next
        End If
    End Function

    Private Function HasParent(ByVal treeStructurePartCustomBankPropertyEntityToCheck As TreeStructurePartCustomBankPropertyEntity) As Boolean
        For Each treeStructurePartCustomBankPropertyEntity As TreeStructurePartCustomBankPropertyEntity In TreeStructureCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyCollection
            If treeStructurePartCustomBankPropertyEntityToCheck IsNot treeStructurePartCustomBankPropertyEntity Then
                Dim childTreeStructurePartCustomBankPropertyEntity As ChildTreeStructurePartCustomBankPropertyEntity = treeStructurePartCustomBankPropertyEntity.ChildTreeStructurePartCustomBankPropertyCollection.FirstOrDefault(Function(i) i.ChildTreeStructurePartCustomBankPropertyId = treeStructurePartCustomBankPropertyEntityToCheck.TreeStructurePartCustomBankPropertyId AndAlso i.Id <> Guid.Empty)

                If childTreeStructurePartCustomBankPropertyEntity IsNot Nothing Then
                    Return True
                End If
            End If
        Next

        Return False
    End Function

    Private Sub DeptTreeStructureUserControl_Delete(ByVal sender As Object, ByVal e As RowsDeletedEventArgs)
        If MessageBox.Show(My.Resources.ConfirmDeleteSubElement, String.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
            Dim dependentTreeStructureEntity As ChildTreeStructurePartCustomBankPropertyEntity = CType(e.DataSource, ChildTreeStructurePartCustomBankPropertyEntity)
            If dependentTreeStructureEntity IsNot Nothing Then
                Dim selectedTreeStructurePart As TreeStructurePartCustomBankPropertyEntity = BankFactory.Instance.GetTreeStructurePartCustomBankProperties(New List(Of Guid) From {dependentTreeStructureEntity.ChildTreeStructurePartCustomBankPropertyId}, False).OfType(Of TreeStructurePartCustomBankPropertyEntity).FirstOrDefault()
                Dim parentTreeStructurePart As TreeStructurePartCustomBankPropertyEntity = TreeStructureGrid.SelectedRow

                If selectedTreeStructurePart IsNot Nothing Then
                    If TreeStructureHelper.TreeStructureValueIsConnectedToResourcesAndUserCancelled(selectedTreeStructurePart, CustomBankProperty.BankId, BankFactory.Instance, ResourceFactory.Instance) Then
                        Return
                    End If
                End If
                If parentTreeStructurePart IsNot Nothing Then
                    If ConvertChildTreeStructureToTreeStructurePart(dependentTreeStructureEntity).ChildTreeStructurePartCustomBankPropertyCollection.Count > 0 Then
                        If MessageBox.Show(My.Resources.ConfirmDeleteSubElementWithChildren, String.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
                            RunDelete(parentTreeStructurePart, dependentTreeStructureEntity)
                        End If
                    Else
                        RunDelete(parentTreeStructurePart, dependentTreeStructureEntity)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub RunDelete(ByVal treeStructurePartToRemoveFrom As TreeStructurePartCustomBankPropertyEntity, ByVal childTreeStructurePartToRemove As ChildTreeStructurePartCustomBankPropertyEntity)
        RemoveStructureAndAllSubStructures(treeStructurePartToRemoveFrom, childTreeStructurePartToRemove)
        CreateDependencyControls()
        CreateTree()
    End Sub

    Private Sub RemoveStructureAndAllSubStructures(ByVal treeStructurePartToRemoveFrom As TreeStructurePartCustomBankPropertyEntity, ByVal childTreeStructurePartToRemove As ChildTreeStructurePartCustomBankPropertyEntity)
        Dim treeStructurePartCustomBankPropertyEntity As TreeStructurePartCustomBankPropertyEntity = ConvertChildTreeStructureToTreeStructurePart(childTreeStructurePartToRemove)

        If treeStructurePartCustomBankPropertyEntity.ChildTreeStructurePartCustomBankPropertyCollection.Count = 0 Then
            treeStructurePartToRemoveFrom.ChildTreeStructurePartCustomBankPropertyCollection.Remove(childTreeStructurePartToRemove)
        Else
            For i As Integer = treeStructurePartCustomBankPropertyEntity.ChildTreeStructurePartCustomBankPropertyCollection.Count - 1 To 0 Step -1
                RemoveStructureAndAllSubStructures(treeStructurePartCustomBankPropertyEntity, treeStructurePartCustomBankPropertyEntity.ChildTreeStructurePartCustomBankPropertyCollection(i))
            Next

            treeStructurePartToRemoveFrom.ChildTreeStructurePartCustomBankPropertyCollection.Remove(childTreeStructurePartToRemove)
        End If
    End Sub

    Private Function ConvertChildTreeStructureToTreeStructurePart(ByVal childTreeStructurePartCustomBankPropertyEntity As ChildTreeStructurePartCustomBankPropertyEntity) As TreeStructurePartCustomBankPropertyEntity
        Return childTreeStructurePartCustomBankPropertyEntity.TreeStructurePartCustomBankProperty.TreeStructureCustomBankProperty.TreeStructurePartCustomBankPropertyCollection.First(Function(i) i.TreeStructurePartCustomBankPropertyId = childTreeStructurePartCustomBankPropertyEntity.ChildTreeStructurePartCustomBankPropertyId)
    End Function

    Private Sub ButtonAdd_Click(sender As Object, e As EventArgs) Handles ButtonAdd.Click
        AddDependentTreeStructureUserControl()
    End Sub

    Private Sub TreeStructureUserControl_Leave(sender As Object, e As EventArgs) Handles MyBase.Leave
        For Each treeStructurePartCustomBankPropertyEntity As TreeStructurePartCustomBankPropertyEntity In TreeStructureCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyCollection
            RemoveHandler treeStructurePartCustomBankPropertyEntity.ChildTreeStructurePartCustomBankPropertyCollection.EntityRemoved, AddressOf TreeStructurePartCustomBankPropertyEntity_ChildTreeStructurePartCustomBankPropertyCollection_EntityRemoved
        Next

        VisualizeTreeToggleButton.Checked = False
    End Sub

    Private Sub TreeStructureUserControl_Enter(sender As Object, e As EventArgs)
        RemoveHandler Me.Enter, AddressOf TreeStructureUserControl_Enter

        If TreeStructureGrid.SelectedRow Is Nothing Then
            TreeStructureGrid.SelectFirstRow()
        End If

        For Each treeStructurePartCustomBankPropertyEntity As TreeStructurePartCustomBankPropertyEntity In TreeStructureCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyCollection
            AddHandler treeStructurePartCustomBankPropertyEntity.ChildTreeStructurePartCustomBankPropertyCollection.EntityRemoved, AddressOf TreeStructurePartCustomBankPropertyEntity_ChildTreeStructurePartCustomBankPropertyCollection_EntityRemoved
        Next

        If TreeStructureCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyCollection.Count = 0 Then
            VisualizeTreeToggleButton.Enabled = False
        Else
            VisualizeTreeToggleButton.Enabled = True
        End If

        CreateDependencyControls()
        CreateTree()

        TreeStructureGrid.Focus()

        AddHandler Me.Enter, AddressOf TreeStructureUserControl_Enter
    End Sub

    Private ReadOnly Property IsStructureValid As Boolean
        Get
            For Each treeStructurePartCustomBankPropertyEntity As TreeStructurePartCustomBankPropertyEntity In TreeStructureCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyCollection
                Dim usedIds As New List(Of Guid)

                For Each child As ChildTreeStructurePartCustomBankPropertyEntity In treeStructurePartCustomBankPropertyEntity.ChildTreeStructurePartCustomBankPropertyCollection
                    If usedIds.Contains(child.ChildTreeStructurePartCustomBankPropertyId) Then
                        Return False
                    Else
                        usedIds.Add(child.ChildTreeStructurePartCustomBankPropertyId)
                    End If
                Next
            Next

            Return True
        End Get
    End Property


    Private Sub ButtonAdd_Validating(sender As Object, e As CancelEventArgs) Handles ButtonAdd.Validating
        StructureErrorProvider.Clear()

        If Not IsStructureValid Then
            StructureErrorProvider.SetIconAlignment(ButtonAdd, ErrorIconAlignment.MiddleLeft)
            StructureErrorProvider.SetError(ButtonAdd, My.Resources.InvalidTreeStructure)
            e.Cancel = True
        End If

    End Sub

    Private Sub StructureValidating(ByVal sender As Object, e As CancelEventArgs) Handles TreeStructureGrid.Validating
        e.Cancel = Not TreeStructureViewerUserControl.IsTreeValid(TreeStructureCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyCollection, Not VisualizeTreeToggleButton.Checked)
    End Sub

End Class