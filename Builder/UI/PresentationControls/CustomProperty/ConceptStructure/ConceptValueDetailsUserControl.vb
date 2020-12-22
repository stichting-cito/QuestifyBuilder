Imports System.ComponentModel
Imports System.Text
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses
Imports System.Linq

Public Class ConceptValueDetailsUserControl

    Protected _allConceptTypes As New List(Of KeyValuePair(Of ConceptTypeEntity, String))()

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If

            RemoveHandler ConceptStructureGrid.ListViewData.ItemSelectionChanged, AddressOf ConceptStructure_AfterRowActivate
            RemoveHandler ConceptStructureCustomBankPropertyEntity.ConceptStructurePartCustomBankPropertyCollection.EntityRemoved, AddressOf EntityRemoved

            ConceptCustomBankPropertyBindingSource.DataSource = Nothing
            ConceptCustomBankPropertyBindingSource.Dispose()
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    Private ReadOnly Property ConceptStructureCustomBankPropertyEntity As ConceptStructureCustomBankPropertyEntity
        Get
            Return CType(CustomBankProperty, ConceptStructureCustomBankPropertyEntity)
        End Get
    End Property

    Public Overrides Sub Saved()
        ConceptStructureGrid.UpdateData(ConceptStructureCustomBankPropertyEntity)
    End Sub

    Public Overrides Sub Initialize(ByVal customBankProperty As CustomBankPropertyEntity, ByVal initAsReadOnly As Boolean)
        MyBase.Initialize(customBankProperty, initAsReadOnly)

        _allConceptTypes = ConceptStructureHelper.CreateLocalizedConceptTypes()
        ComboBoxTypes.DataSource = _allConceptTypes


        ConceptStructureGrid.Initialize(ConceptStructureCustomBankPropertyEntity, _allConceptTypes)

        AddHandler ConceptStructureGrid.ListViewData.ItemSelectionChanged, AddressOf ConceptStructure_AfterRowActivate

        ConceptStructureGrid.SelectFirstRow()
    End Sub

    Private Sub ConceptStructure_AfterRowActivate(sender As Object, e As EventArgs)
        Dim entity As ConceptStructurePartCustomBankPropertyEntity = ConceptStructureGrid.SelectedRow
        entity = If(entity, New ConceptStructurePartCustomBankPropertyEntity())
        ConceptCustomBankPropertyBindingSource.DataSource = entity
        ConceptCustomBankPropertyBindingSource.ResetBindings(False)

        TextBoxName.Text = entity.Name
        TextBoxTitle.Text = entity.Title

        If ValidateChildren() Then
            NameErrorProvider.Clear()
            TitleErrorProvider.Clear()
        End If

        EnableDisableControls(True)
    End Sub

    Public Overrides ReadOnly Property SelectedEntityId As Guid
        Get
            Return ConceptStructureGrid.SelectedRow.ConceptStructurePartCustomBankPropertyId
        End Get
    End Property

    Protected Overrides Sub DisableControls()
        TextBoxName.Enabled = False
        TextBoxTitle.Enabled = False
        ComboBoxTypes.Enabled = False
    End Sub

    Private Sub EnableDisableControls(ByVal state As Boolean)
        If Not IsControlReadOnly Then
            TextBoxName.Enabled = state
            TextBoxTitle.Enabled = state
            ComboBoxTypes.Enabled = state
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

    Private Sub ConceptValueDetailsUserControl_Load(sender As Object, e As EventArgs) Handles Me.Load
        ConceptStructureGrid.SelectFirstRow()

        AddHandler ConceptStructureCustomBankPropertyEntity.ConceptStructurePartCustomBankPropertyCollection.EntityRemoved, AddressOf EntityRemoved
    End Sub

    Private Sub EntityRemoved(sender As Object, e As CollectionChangedEventArgs)
        RemovedEntities.Add(DirectCast(e.InvolvedEntity, EntityBase2))
    End Sub

    Public Overrides Sub AddItem()
        Dim newItem As ConceptStructurePartCustomBankPropertyEntity = New ConceptStructurePartCustomBankPropertyEntity(Guid.NewGuid())

        Using addCustomPropertyFormDialog As New AddCustomPropertyFormDialog(newItem, CustomBankProperty, _allConceptTypes)
            If addCustomPropertyFormDialog.ShowDialog(Me) = DialogResult.OK Then
                If CheckName(newItem.Name) = 0 Then
                    ConceptStructureCustomBankPropertyEntity.ConceptStructurePartCustomBankPropertyCollection.Add(newItem)
                    ConceptStructureGrid.UpdateData(ConceptStructureCustomBankPropertyEntity)
                End If
            End If
        End Using
    End Sub

    Public Overrides Sub RemoveItem()
        If ConceptStructureGrid.SelectedRow Is Nothing Then
            Return
        End If

        Dim selectedEntity As ConceptStructurePartCustomBankPropertyEntity = CType(ConceptStructureGrid.SelectedRow, ConceptStructurePartCustomBankPropertyEntity)
        Dim resultRemove As DialogResult = DialogResult.No
        Dim resultAlsoRemoveChildren As DialogResult = DialogResult.No

        resultRemove = MessageBox.Show(My.Resources.RemovePropertyConfirmation, String.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

        If resultRemove = Windows.Forms.DialogResult.Yes Then
            If selectedEntity.ChildConceptStructurePartCustomBankPropertyCollection.Count > 0 Then
                resultAlsoRemoveChildren = MessageBox.Show(My.Resources.RemoveChildrenConfirmation, String.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            End If
        End If

        If (selectedEntity.ChildConceptStructurePartCustomBankPropertyCollection.Count = 0 AndAlso resultRemove = DialogResult.Yes) OrElse
            (selectedEntity.ChildConceptStructurePartCustomBankPropertyCollection.Count > 0 AndAlso resultRemove = DialogResult.Yes AndAlso resultAlsoRemoveChildren = DialogResult.Yes) Then
            If ConceptStructureGrid.SelectedRow IsNot Nothing Then
                For i As Integer = selectedEntity.ChildConceptStructurePartCustomBankPropertyCollection.Count - 1 To 0 Step -1
                    selectedEntity.ChildConceptStructurePartCustomBankPropertyCollection.RemoveAt(i)
                Next

                ConceptStructureCustomBankPropertyEntity.ConceptStructurePartCustomBankPropertyCollection.Remove(selectedEntity)
                ConceptStructureGrid.UpdateData(ConceptStructureCustomBankPropertyEntity)

                ConceptStructureGrid.SelectFirstRow()
            End If
        End If

        If ConceptStructureGrid.RowCount = 0 Then
            EnableDisableControls(False)
        End If
    End Sub

    Private Sub NameValidating(ByVal sender As Object, e As CancelEventArgs) Handles TextBoxName.Validating
        NameErrorProvider.Clear()

        If ConceptCustomBankPropertyBindingSource.Current IsNot Nothing Then

            Dim current As ConceptStructurePartCustomBankPropertyEntity = CType(ConceptCustomBankPropertyBindingSource.Current, ConceptStructurePartCustomBankPropertyEntity)

            If String.IsNullOrEmpty(TextBoxName.Text.Trim()) Then
                current.Name = String.Empty

                NameErrorProvider.SetError(TextBoxName, My.Resources.CodeFieldCannotBeEmpty)
            End If

            If CheckName(TextBoxName.Text.Trim()) > 1 Then
                NameErrorProvider.SetError(TextBoxName, My.Resources.CodeFieldNotUnique)
            End If
        End If

        e.Cancel = NameErrorProvider.GetError(TextBoxName) <> String.Empty
    End Sub

    Private Sub TitleValidating(ByVal sender As Object, e As CancelEventArgs) Handles TextBoxTitle.Validating
        TitleErrorProvider.Clear()

        If ConceptCustomBankPropertyBindingSource.Current IsNot Nothing Then
            If String.IsNullOrEmpty(TextBoxTitle.Text.Trim()) Then
                CType(ConceptCustomBankPropertyBindingSource.Current, ConceptStructurePartCustomBankPropertyEntity).Title = String.Empty

                TitleErrorProvider.SetError(TextBoxTitle, My.Resources.TitleFieldCannotBeEmpty)
            End If
        End If

        e.Cancel = NameErrorProvider.GetError(TextBoxTitle) <> String.Empty
    End Sub

    Private Function CheckName(name As String) As Integer
        Return ConceptStructureCustomBankPropertyEntity.ConceptStructurePartCustomBankPropertyCollection.Where(Function(c) c.Name = name).Count()
    End Function

End Class

