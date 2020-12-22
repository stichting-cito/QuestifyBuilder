Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses
Imports System.ComponentModel
Imports System.Text
Imports System.Linq
Imports Enums

Public Class ListValueDetailsUserControl

    Public Sub New()
        InitializeComponent()

    End Sub

    Private ReadOnly Property ListCustomBankPropertyEntity As ListCustomBankPropertyEntity
        Get
            Return CType(CustomBankProperty, ListCustomBankPropertyEntity)
        End Get
    End Property

    Protected Overrides Sub DisableControls()
        TextBoxName.Enabled = False
        TextBoxTitle.Enabled = False
    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If

            RemoveHandler ListValueDetailsGrid.ListViewData.ItemSelectionChanged, AddressOf ListValueDetailsGrid_AfterRowActivate
            RemoveHandler ListCustomBankPropertyEntity.ListValueCustomBankPropertyCollection.EntityRemoved, AddressOf EntityRemoved

            ListCustomBankPropertyBindingSource.DataSource = Nothing
            ListCustomBankPropertyBindingSource.Dispose()
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Public Overrides ReadOnly Property SelectedEntityId As Guid
        Get
            Return ListValueDetailsGrid.SelectedRow.ListValueBankCustomPropertyId
        End Get
    End Property

    Public Overrides Sub Initialize(customBankProperty As CustomBankPropertyEntity, ByVal initAsReadOnly As Boolean)
        MyBase.Initialize(customBankProperty, initAsReadOnly)

        ListValueDetailsGrid.Initialize(ListCustomBankPropertyEntity)

        AddHandler ListValueDetailsGrid.ListViewData.ItemSelectionChanged, AddressOf ListValueDetailsGrid_AfterRowActivate
        ListValueDetailsGrid.SelectFirstRow()
    End Sub

    Private Sub ListValueDetailsGrid_AfterRowActivate(ByVal sender As Object, ByVal e As EventArgs)
        Dim entity As ListValueCustomBankPropertyEntity = ListValueDetailsGrid.SelectedRow
        entity = If(entity, New ListValueCustomBankPropertyEntity())

        ListCustomBankPropertyBindingSource.DataSource = entity

        TextBoxName.Text = entity.Name
        TextBoxTitle.Text = entity.Title

        If ValidateChildren() Then
            NameErrorProvider.Clear()
            TitleErrorProvider.Clear()
        End If

        EnableDisableControls(True)
    End Sub

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

    Private Sub ListValueDetailsUserControl_Load(sender As Object, e As EventArgs) Handles Me.Load
        ListValueDetailsGrid.SelectFirstRow()

        AddHandler ListCustomBankPropertyEntity.ListValueCustomBankPropertyCollection.EntityRemoved, AddressOf EntityRemoved
    End Sub

    Private Sub EntityRemoved(sender As Object, e As CollectionChangedEventArgs)
        RemovedEntities.Add(DirectCast(e.InvolvedEntity, EntityBase2))
    End Sub

    Public Overrides Sub AddItem()
        Dim newItem As ListValueCustomBankPropertyEntity = New ListValueCustomBankPropertyEntity(Guid.NewGuid())

        Using addCustomPropertyFormDialog As New AddCustomPropertyFormDialog(newItem, CustomBankProperty)
            If addCustomPropertyFormDialog.ShowDialog(Me) = DialogResult.OK Then
                If CheckName(newItem.Name) = 0 Then
                    ListCustomBankPropertyEntity.ListValueCustomBankPropertyCollection.Add(newItem)
                    ListValueDetailsGrid.UpdateData(ListCustomBankPropertyEntity)
                End If
            End If
        End Using
    End Sub

    Private Sub ClearInputFields()
        TextBoxName.Text = String.Empty
        TextBoxTitle.Text = String.Empty
    End Sub

    Public Overrides Sub Saved()
        ListValueDetailsGrid.UpdateData(ListCustomBankPropertyEntity)
    End Sub

    Public Overrides Sub RemoveItem()
        If ListValueDetailsGrid.SelectedRow Is Nothing Then
            Return
        End If

        Dim selectedEntity As ListValueCustomBankPropertyEntity = ListValueDetailsGrid.SelectedRow
        Dim removeActionIsConfirmed As Boolean = AskConfirmationForReferencedValues(selectedEntity.ListValueBankCustomPropertyId, CustomBankPropertyType.ListSingleSelect)

        If removeActionIsConfirmed AndAlso Not RemoveConfirmed.ContainsKey(selectedEntity.ListValueBankCustomPropertyId) Then
            removeActionIsConfirmed = (MessageBox.Show(My.Resources.RemovePropertyConfirmation, String.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes)
        End If

        If removeActionIsConfirmed Then
            If selectedEntity IsNot Nothing Then
                ListCustomBankPropertyEntity.ListValueCustomBankPropertyCollection.Remove(selectedEntity)

                ListValueDetailsGrid.UpdateData(ListCustomBankPropertyEntity)
                ListValueDetailsGrid.SelectFirstRow()
            End If
        End If


        If ListValueDetailsGrid.RowCount = 0 Then
            EnableDisableControls(False)
        End If
    End Sub

    Private Function CheckName(name As String) As Integer
        Return ListCustomBankPropertyEntity.ListValueCustomBankPropertyCollection.Where(Function(c) c.Name = name).Count()
    End Function

    Private Sub NameValidating(ByVal sender As Object, e As CancelEventArgs) Handles TextBoxName.Validating
        NameErrorProvider.Clear()

        If ListCustomBankPropertyBindingSource.Current IsNot Nothing Then
            If String.IsNullOrEmpty(TextBoxName.Text.Trim()) Then
                CType(ListCustomBankPropertyBindingSource.Current, ListValueCustomBankPropertyEntity).Name = String.Empty

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

        If ListCustomBankPropertyBindingSource.Current IsNot Nothing Then
            If String.IsNullOrEmpty(TextBoxTitle.Text.Trim()) Then
                TitleErrorProvider.SetError(TextBoxTitle, My.Resources.TitleFieldCannotBeEmpty)
            End If
        End If

        e.Cancel = TitleErrorProvider.GetError(TextBoxTitle) <> String.Empty
    End Sub

    Public Overrides Sub UndoRemovedEntities()
        For Each entity As ListValueCustomBankPropertyEntity In RemovedEntities
            ListCustomBankPropertyEntity.ListValueCustomBankPropertyCollection.Add(entity)
        Next

        MyBase.UndoRemovedEntities()
    End Sub
End Class
