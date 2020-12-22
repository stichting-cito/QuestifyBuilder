Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.ComponentModel
Imports System.Linq
Imports SD.LLBLGen.Pro.ORMSupportClasses
Imports System.Text
Imports Questify.Builder.Logic.Service.Factories

Public Class TreeValueDetailsUserControl

    Public Sub New()
        InitializeComponent()
    End Sub

    Private ReadOnly Property TreeStructureCustomBankPropertyEntity As TreeStructureCustomBankPropertyEntity
        Get
            Return CType(CustomBankProperty, TreeStructureCustomBankPropertyEntity)
        End Get
    End Property

    Public Overrides Sub Initialize(ByVal customBankProperty As CustomBankPropertyEntity, ByVal initAsReadOnly As Boolean)
        MyBase.Initialize(customBankProperty, initAsReadOnly)

        TreeStructureGrid.Initialize(TreeStructureCustomBankPropertyEntity)

        AddHandler TreeStructureGrid.ListViewData.ItemSelectionChanged, Sub()
                                                                            Dim entity As TreeStructurePartCustomBankPropertyEntity = TreeStructureGrid.SelectedRow
                                                                            entity = If(entity, New TreeStructurePartCustomBankPropertyEntity())

                                                                            TreeCustomBankPropertyBindingSource.DataSource = entity
                                                                            TextBoxName.Text = entity.Name
                                                                            TextBoxTitle.Text = entity.Title

                                                                            If ValidateChildren() Then
                                                                                NameErrorProvider.Clear()
                                                                                TitleErrorProvider.Clear()
                                                                            End If

                                                                            EnableDisableControls(True)
                                                                        End Sub

        TreeStructureGrid.SelectFirstRow()
    End Sub

    Public Overrides ReadOnly Property SelectedEntityId As Guid
        Get
            Return TreeStructureGrid.SelectedRow.TreeStructurePartCustomBankPropertyId
        End Get
    End Property

    Protected Overrides ReadOnly Property IsControlReadOnly As Boolean
        Get
            Return False
        End Get
    End Property

    Private Sub EnableDisableControls(ByVal state As Boolean)
        If Not IsControlReadOnly Then
            TextBoxName.Enabled = state
            TextBoxTitle.Enabled = state
        End If
    End Sub

    Public Overrides ReadOnly Property ErrorMessage As String
        Get
            Dim codeError As String = NameErrorProvider.GetError(TextBoxName)
            Dim titleError As String = TitleErrorProvider.GetError(TextBoxTitle)

            If Not String.IsNullOrEmpty(codeError) OrElse Not String.IsNullOrEmpty(titleError) Then
                Dim builder As New StringBuilder()
                builder.Append(My.Resources.ErrorsOccurred)
                builder.Append(vbNewLine)

                If Not String.IsNullOrEmpty(codeError) Then
                    builder.Append("   - ")
                    builder.Append(NameErrorProvider.GetError(TextBoxName))
                End If

                builder.Append(vbNewLine)

                If Not String.IsNullOrEmpty(titleError) Then
                    builder.Append("   - ")
                    builder.Append(TitleErrorProvider.GetError(TextBoxTitle))
                End If

                Return builder.ToString()
            End If

            Return String.Empty
        End Get
    End Property

    Private Sub TreeValueDetailsUserControl_Load(sender As Object, e As EventArgs) Handles Me.Load
        AddHandler TreeStructureCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyCollection.EntityRemoving, Sub(sender1 As Object, e1 As CollectionChangedEventArgs)
                                                                                                                           Dim treeStructurePartCustomBankPropertyEntity As TreeStructurePartCustomBankPropertyEntity = CType(e1.InvolvedEntity, TreeStructurePartCustomBankPropertyEntity)
                                                                                                                           Dim parent As TreeStructurePartCustomBankPropertyEntity = Nothing

                                                                                                                           RemoveChildsOfTreeStructurePartCustomBankPropertyEntity(treeStructurePartCustomBankPropertyEntity)

                                                                                                                           If TryGetParent(treeStructurePartCustomBankPropertyEntity, parent) Then
                                                                                                                               Dim childToRemove As ChildTreeStructurePartCustomBankPropertyEntity = parent.ChildTreeStructurePartCustomBankPropertyCollection.First(Function(i) i.ChildTreeStructurePartCustomBankPropertyId = treeStructurePartCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyId)

                                                                                                                               Me.RemovedEntities.Add(childToRemove)
                                                                                                                               parent.ChildTreeStructurePartCustomBankPropertyCollection.Remove(childToRemove)
                                                                                                                           End If
                                                                                                                       End Sub

        AddHandler TreeStructureCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyCollection.EntityRemoved, Sub(sender1 As Object, e1 As CollectionChangedEventArgs)
                                                                                                                          Dim treeStructurePartCustomBankPropertyEntity As TreeStructurePartCustomBankPropertyEntity = CType(e1.InvolvedEntity, TreeStructurePartCustomBankPropertyEntity)

                                                                                                                          Me.RemovedEntities.Add(treeStructurePartCustomBankPropertyEntity)
                                                                                                                      End Sub
    End Sub

    Public Overrides Sub Saved()
        TreeStructureGrid.UpdateData(TreeStructureCustomBankPropertyEntity)
    End Sub

    Public Overrides Sub AddItem()
        Dim newItem As TreeStructurePartCustomBankPropertyEntity = New TreeStructurePartCustomBankPropertyEntity(Guid.NewGuid())

        Using addCustomPropertyFormDialog As New AddCustomPropertyFormDialog(newItem, CustomBankProperty)
            If addCustomPropertyFormDialog.ShowDialog(Me) = DialogResult.OK Then
                TreeStructureCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyCollection.Add(newItem)
                TreeStructureGrid.UpdateData(TreeStructureCustomBankPropertyEntity)
            End If
        End Using
    End Sub

    Private Sub ClearInputFields()
        TextBoxName.Text = String.Empty
        TextBoxTitle.Text = String.Empty
    End Sub

    Public Overrides Sub RemoveItem()
        If TreeStructureGrid.SelectedRow Is Nothing Then
            Return
        End If

        Dim selectedEntity As TreeStructurePartCustomBankPropertyEntity = TreeStructureGrid.SelectedRow
        Dim resultAlsoRemoveChildren As DialogResult = DialogResult.Yes
        Dim resultRemoveWhenConnectedToResource As DialogResult = DialogResult.Yes
        Dim resultRemove As DialogResult = MessageBox.Show(My.Resources.RemovePropertyConfirmation, String.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)


        If resultRemove = DialogResult.Yes Then
            If selectedEntity.ChildTreeStructurePartCustomBankPropertyCollection.Count > 0 Then
                resultAlsoRemoveChildren = MessageBox.Show(My.Resources.RemoveChildrenConfirmation, String.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            End If

            If resultAlsoRemoveChildren <> DialogResult.Yes Then
                Return
            End If
            resultRemoveWhenConnectedToResource = TreeStructureHelper.CheckTreeStructureValueConnectedToResourcesAndRequestUserInput(selectedEntity, CustomBankProperty.BankId, BankFactory.Instance, ResourceFactory.Instance)
        End If

        If resultRemove = DialogResult.Yes AndAlso resultAlsoRemoveChildren = DialogResult.Yes AndAlso resultRemoveWhenConnectedToResource = DialogResult.Yes Then
            TreeStructureCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyCollection.Remove(selectedEntity)

            If TreeStructureCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyCollection.Count = 0 Then
                ClearInputFields()
            End If

            TreeStructureGrid.UpdateData(TreeStructureCustomBankPropertyEntity)
            TreeStructureGrid.SelectFirstRow()
        End If

        EnableDisableControls(TreeStructureGrid.ListViewData.Items.Count > 0)
    End Sub

    Private Function ConvertChildTreeStructureToParentTreeStructurePart(ByVal childTreeStructurePartCustomBankPropertyEntity As ChildTreeStructurePartCustomBankPropertyEntity) As TreeStructurePartCustomBankPropertyEntity
        Return TreeStructureCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyCollection.First(Function(i) i.TreeStructurePartCustomBankPropertyId = childTreeStructurePartCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyId)
    End Function

    Private Function TryGetParent(ByVal treeStructurePartCustomBankPropertyEntityToCheck As TreeStructurePartCustomBankPropertyEntity, ByRef treeStructurePartCustomBankPropertyEntityToReturn As TreeStructurePartCustomBankPropertyEntity) As Boolean
        For Each treeStructurePartCustomBankPropertyEntity As TreeStructurePartCustomBankPropertyEntity In TreeStructureCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyCollection
            If treeStructurePartCustomBankPropertyEntityToCheck IsNot treeStructurePartCustomBankPropertyEntity Then
                Dim childTreeStructurePartCustomBankPropertyEntity As ChildTreeStructurePartCustomBankPropertyEntity = treeStructurePartCustomBankPropertyEntity.ChildTreeStructurePartCustomBankPropertyCollection.FirstOrDefault(Function(i) i.ChildTreeStructurePartCustomBankPropertyId = treeStructurePartCustomBankPropertyEntityToCheck.TreeStructurePartCustomBankPropertyId)

                If childTreeStructurePartCustomBankPropertyEntity IsNot Nothing Then
                    treeStructurePartCustomBankPropertyEntityToReturn = ConvertChildTreeStructureToParentTreeStructurePart(childTreeStructurePartCustomBankPropertyEntity)

                    Return True
                End If
            End If
        Next

        Return False
    End Function

    Private Sub RemoveChildsOfTreeStructurePartCustomBankPropertyEntity(treeStructurePartCustomBankPropertyEntityToBeRemoved As TreeStructurePartCustomBankPropertyEntity)
        For i As Integer = treeStructurePartCustomBankPropertyEntityToBeRemoved.ChildTreeStructurePartCustomBankPropertyCollection.Count - 1 To 0 Step -1
            Dim listLocation = i
            Dim childOfChild As TreeStructurePartCustomBankPropertyEntity = treeStructurePartCustomBankPropertyEntityToBeRemoved.TreeStructureCustomBankProperty.TreeStructurePartCustomBankPropertyCollection.First(Function(j) j.TreeStructurePartCustomBankPropertyId = treeStructurePartCustomBankPropertyEntityToBeRemoved.ChildTreeStructurePartCustomBankPropertyCollection(listLocation).ChildTreeStructurePartCustomBankPropertyId)

            If childOfChild.ChildTreeStructurePartCustomBankPropertyCollection.Count > 0 Then
                RemoveChildsOfTreeStructurePartCustomBankPropertyEntity(childOfChild)
            End If

            treeStructurePartCustomBankPropertyEntityToBeRemoved.ChildTreeStructurePartCustomBankPropertyCollection.RemoveAt(listLocation)
        Next
    End Sub

    Private Sub NameValidating(ByVal sender As Object, e As CancelEventArgs) Handles TextBoxName.Validating
        NameErrorProvider.Clear()

        If TreeCustomBankPropertyBindingSource.Current IsNot Nothing Then
            If String.IsNullOrEmpty(TextBoxName.Text.Trim()) Then
                CType(TreeCustomBankPropertyBindingSource.Current, TreeStructurePartCustomBankPropertyEntity).Name = String.Empty

                NameErrorProvider.SetError(TextBoxName, My.Resources.CodeFieldCannotBeEmpty)
            End If

        End If

        e.Cancel = NameErrorProvider.GetError(TextBoxName) <> String.Empty
    End Sub

    Private Sub TitleValidating(ByVal sender As Object, e As CancelEventArgs) Handles TextBoxTitle.Validating
        TitleErrorProvider.Clear()

        If TreeCustomBankPropertyBindingSource.Current IsNot Nothing Then
            If String.IsNullOrEmpty(TextBoxTitle.Text.Trim()) Then
                CType(TreeCustomBankPropertyBindingSource.Current, TreeStructurePartCustomBankPropertyEntity).Title = String.Empty

                TitleErrorProvider.SetError(TextBoxTitle, My.Resources.TitleFieldCannotBeEmpty)
            End If
        End If

        e.Cancel = NameErrorProvider.GetError(TextBoxTitle) <> String.Empty
    End Sub

End Class

