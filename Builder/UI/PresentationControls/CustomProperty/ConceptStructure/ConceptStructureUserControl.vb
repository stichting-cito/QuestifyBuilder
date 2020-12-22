Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.Linq
Imports SD.LLBLGen.Pro.ORMSupportClasses
Imports System.ComponentModel

Public Class ConceptStructureUserControl

    Public Sub New()
        InitializeComponent()
    End Sub

    Private ReadOnly Property ConceptStructureCustomBankPropertyEntity As ConceptStructureCustomBankPropertyEntity
        Get
            Return CType(CustomBankProperty, ConceptStructureCustomBankPropertyEntity)
        End Get
    End Property

    Protected Overrides Sub DisableControls()
        ButtonAdd.Enabled = False
        StructureTableLayoutPanel.Enabled = False
    End Sub

    Public Overrides ReadOnly Property ErrorMessage As String
        Get
            Return StructureErrorProvider.GetError(StructureTableLayoutPanel)
        End Get
    End Property

    Public Overrides Sub Initialize(customBankProperty As CustomBankPropertyEntity, ByVal initAsReadOnly As Boolean)
        MyBase.Initialize(customBankProperty, initAsReadOnly)

        ConceptStructureGrid.Initialize(ConceptStructureCustomBankPropertyEntity, ConceptStructureHelper.CreateLocalizedConceptTypes())

        AddHandler ConceptStructureGrid.ListViewData.ItemSelectionChanged, AddressOf GridConceptStructure_AfterRowActivate
        ConceptStructureGrid.SelectFirstRow()
    End Sub

    Private Sub ConceptStructurePartCustomBankPropertyEntity_ChildConceptStructurePartCustomBankPropertyCollection_EntityRemoved(ByVal sender As Object, e As CollectionChangedEventArgs)
        RemovedEntities.Add(DirectCast(e.InvolvedEntity, EntityBase2))
    End Sub

    Private Sub GridConceptStructure_AfterRowActivate(ByVal sender As Object, ByVal e As EventArgs)
        CreateDependencyControls()

        If StructureTableLayoutPanel.Enabled Then
            ButtonAdd.Enabled = True
        End If
    End Sub

    Private Sub CreateDependencyControls()
        StructureTableLayoutPanel.Controls.Clear()
        StructureTableLayoutPanel.RowStyles.Clear()

        Dim selectedConceptStructurePartCustomBankPropertyEntity As ConceptStructurePartCustomBankPropertyEntity = CType(ConceptStructureGrid.SelectedRow, ConceptStructurePartCustomBankPropertyEntity)

        If selectedConceptStructurePartCustomBankPropertyEntity IsNot Nothing Then
            Dim allConceptStructuresExceptSelectedOne As IEnumerable(Of ConceptStructurePartCustomBankPropertyEntity) = GetConceptsExceptSelf(selectedConceptStructurePartCustomBankPropertyEntity)
            Dim sortOrder As Integer = 0

            For Each childConceptStructurePartCustomBankPropertyEntity As ChildConceptStructurePartCustomBankPropertyEntity In selectedConceptStructurePartCustomBankPropertyEntity.ChildConceptStructurePartCustomBankPropertyCollection _
    .OrderBy(Function(x) x.VisualOrder) _
    .ThenBy(Function(i) allConceptStructuresExceptSelectedOne.FirstOrDefault(Function(j) CType(j, ConceptStructurePartCustomBankPropertyEntity).ConceptStructurePartCustomBankPropertyId = i.ChildConceptStructurePartCustomBankPropertyId).Name)
                Dim deptConceptStructureUserControl As DeptConceptStructureUserControl = New DeptConceptStructureUserControl(childConceptStructurePartCustomBankPropertyEntity, allConceptStructuresExceptSelectedOne)
                childConceptStructurePartCustomBankPropertyEntity.VisualOrder = sortOrder

                AddHandler deptConceptStructureUserControl.ComboBoxDep.SelectedIndexChanged, AddressOf DeptConceptStructureUserControl_ComboBoxDep_SelectedIndexChanged
                AddHandler deptConceptStructureUserControl.DeleteDependencyEvent, AddressOf DeptConceptStructureUserControl_Delete

                deptConceptStructureUserControl.Dock = DockStyle.Top
                deptConceptStructureUserControl.Height = 30

                StructureTableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.AutoSize))
                StructureTableLayoutPanel.Controls.Add(deptConceptStructureUserControl, 0, StructureTableLayoutPanel.RowStyles.Count - 1)

                sortOrder += 1
            Next
        End If
    End Sub

    Private Sub DeptConceptStructureUserControl_ComboBoxDep_SelectedIndexChanged(ByVal sender As Object, e As EventArgs)
        Dim comboBox As ComboBox = CType(sender, ComboBox)

        If comboBox.Tag IsNot Nothing Then
            Dim comboBoxSelectedConceptStructurePartCustomBankPropertyEntity As ConceptStructurePartCustomBankPropertyEntity = CType(comboBox.SelectedItem, ConceptStructurePartCustomBankPropertyEntity)
            Dim gridSelectedConceptStructurePartCustomBankPropertyEntity As ConceptStructurePartCustomBankPropertyEntity = CType(ConceptStructureGrid.SelectedRow, ConceptStructurePartCustomBankPropertyEntity)
            Dim newValueSet As Boolean = False

            If gridSelectedConceptStructurePartCustomBankPropertyEntity IsNot Nothing Then
                For Each gridChild As ChildConceptStructurePartCustomBankPropertyEntity In gridSelectedConceptStructurePartCustomBankPropertyEntity.ChildConceptStructurePartCustomBankPropertyCollection
                    For Each deptConceptStructureUserControl As DeptConceptStructureUserControl In StructureTableLayoutPanel.Controls
                        If deptConceptStructureUserControl.ChildConceptStructurePartCustomBankPropertyEntity Is gridChild AndAlso deptConceptStructureUserControl.ComboBoxDep Is comboBox Then
                            gridChild.ChildConceptStructurePartCustomBankPropertyId = comboBoxSelectedConceptStructurePartCustomBankPropertyEntity.ConceptStructurePartCustomBankPropertyId
                            comboBox.Tag = comboBox.SelectedItem
                            newValueSet = True
                            Exit For
                        End If
                    Next

                    If newValueSet Then
                        Exit For
                    End If
                Next
            End If
        End If
    End Sub

    Public Sub AddDependentConceptStructureUserControl()
        Dim selectedConceptStructurePartCustomBankPropertyEntity As ConceptStructurePartCustomBankPropertyEntity = CType(ConceptStructureGrid.SelectedRow, ConceptStructurePartCustomBankPropertyEntity)

        If selectedConceptStructurePartCustomBankPropertyEntity IsNot Nothing Then
            If selectedConceptStructurePartCustomBankPropertyEntity.ChildConceptStructurePartCustomBankPropertyCollection.Count < ConceptStructureCustomBankPropertyEntity.ConceptStructurePartCustomBankPropertyCollection.Count - 1 Then
                Dim newChildConceptStructurePartCustomBankPropertyEntity As ChildConceptStructurePartCustomBankPropertyEntity = New ChildConceptStructurePartCustomBankPropertyEntity()

                newChildConceptStructurePartCustomBankPropertyEntity.Id = Guid.NewGuid()
                newChildConceptStructurePartCustomBankPropertyEntity.ConceptStructurePartCustomBankPropertyId = selectedConceptStructurePartCustomBankPropertyEntity.ConceptStructurePartCustomBankPropertyId
                newChildConceptStructurePartCustomBankPropertyEntity.VisualOrder = 100
                Dim allConceptStructuresExceptSelectedOne As IEnumerable(Of ConceptStructurePartCustomBankPropertyEntity) = GetConceptsExceptSelf(selectedConceptStructurePartCustomBankPropertyEntity)

                newChildConceptStructurePartCustomBankPropertyEntity.ChildConceptStructurePartCustomBankPropertyId = allConceptStructuresExceptSelectedOne(0).ConceptStructurePartCustomBankPropertyId
                selectedConceptStructurePartCustomBankPropertyEntity.ChildConceptStructurePartCustomBankPropertyCollection.Add(newChildConceptStructurePartCustomBankPropertyEntity)

                CreateDependencyControls()
            Else
                MessageBox.Show(Me, My.Resources.NoMoreConceptstructuresToAdd, String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        End If
    End Sub

    Private Function GetConceptsExceptSelf(ByVal selectedConceptStructurePartCustomBankPropertyEntity As ConceptStructurePartCustomBankPropertyEntity) As IEnumerable(Of ConceptStructurePartCustomBankPropertyEntity)
        Return ConceptStructureCustomBankPropertyEntity.ConceptStructurePartCustomBankPropertyCollection.Where(Function(c) c.ConceptStructurePartCustomBankPropertyId <> selectedConceptStructurePartCustomBankPropertyEntity.ConceptStructurePartCustomBankPropertyId).OrderBy(Function(i) i.Code)
    End Function

    Private Sub DeptConceptStructureUserControl_Delete(ByVal sender As Object, ByVal e As RowsDeletedEventArgs)
        If MessageBox.Show(My.Resources.ConfirmDeleteSubElement, String.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
            Dim selectedConceptStructurePartCustomBankPropertyEntity As ConceptStructurePartCustomBankPropertyEntity = CType(ConceptStructureGrid.SelectedRow, ConceptStructurePartCustomBankPropertyEntity)
            If selectedConceptStructurePartCustomBankPropertyEntity IsNot Nothing Then
                Dim dependentConceptStructureEntity As ChildConceptStructurePartCustomBankPropertyEntity = CType(e.DataSource, ChildConceptStructurePartCustomBankPropertyEntity)

                selectedConceptStructurePartCustomBankPropertyEntity.ChildConceptStructurePartCustomBankPropertyCollection.Remove(dependentConceptStructureEntity)

                CreateDependencyControls()
            End If
        End If
    End Sub

    Private Sub ButtonAdd_Click(sender As Object, e As EventArgs) Handles ButtonAdd.Click
        AddDependentConceptStructureUserControl()
    End Sub

    Private Sub ConceptStructureUserControl_Leave(sender As Object, e As EventArgs) Handles MyBase.Leave
        For Each conceptStructurePartCustomBankPropertyEntity As ConceptStructurePartCustomBankPropertyEntity In ConceptStructureCustomBankPropertyEntity.ConceptStructurePartCustomBankPropertyCollection
            RemoveHandler conceptStructurePartCustomBankPropertyEntity.ChildConceptStructurePartCustomBankPropertyCollection.EntityRemoved, AddressOf ConceptStructurePartCustomBankPropertyEntity_ChildConceptStructurePartCustomBankPropertyCollection_EntityRemoved
        Next
    End Sub

    Private Sub ConceptStructureUserControl_Enter(sender As Object, e As EventArgs) Handles MyBase.Enter
        If ConceptStructureGrid.SelectedRow Is Nothing Then
            ConceptStructureGrid.SelectFirstRow()
        End If

        For Each conceptStructurePartCustomBankPropertyEntity As ConceptStructurePartCustomBankPropertyEntity In ConceptStructureCustomBankPropertyEntity.ConceptStructurePartCustomBankPropertyCollection
            AddHandler conceptStructurePartCustomBankPropertyEntity.ChildConceptStructurePartCustomBankPropertyCollection.EntityRemoved, AddressOf ConceptStructurePartCustomBankPropertyEntity_ChildConceptStructurePartCustomBankPropertyCollection_EntityRemoved
        Next
    End Sub

    Private ReadOnly Property IsStructureValid As Boolean
        Get
            For Each conceptStructurePartCustomBankPropertyEntity As ConceptStructurePartCustomBankPropertyEntity In ConceptStructureCustomBankPropertyEntity.ConceptStructurePartCustomBankPropertyCollection
                Dim usedIds As New List(Of Guid)

                For Each child As ChildConceptStructurePartCustomBankPropertyEntity In conceptStructurePartCustomBankPropertyEntity.ChildConceptStructurePartCustomBankPropertyCollection
                    If usedIds.Contains(child.ChildConceptStructurePartCustomBankPropertyId) Then
                        Return False
                    Else
                        usedIds.Add(child.ChildConceptStructurePartCustomBankPropertyId)
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
            StructureErrorProvider.SetError(ButtonAdd, My.Resources.InvalidConceptStructure)
            e.Cancel = True
        End If
    End Sub

End Class