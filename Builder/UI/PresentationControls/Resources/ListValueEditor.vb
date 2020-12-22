Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.HelperClasses

Public Class ListValueEditor


    Private _ListCustomBankProperty As EntityClasses.ListCustomBankPropertyEntity
    Private _ListCustomBankPropertyValue As EntityClasses.ListCustomBankPropertyValueEntity
    Private _selectionBeingSet As Boolean = False
    Private _selectedListValuesBeforeEditing As HashSet(Of String)




    Public Property ListCustomBankProperty() As EntityClasses.ListCustomBankPropertyEntity
        Get
            Return _ListCustomBankProperty
        End Get
        Set(ByVal value As EntityClasses.ListCustomBankPropertyEntity)
            _ListCustomBankProperty = value

            If _ListCustomBankProperty.MultipleSelect Then
                Me.Text = String.Format(My.Resources.SelectMultipleValuesFor0, _ListCustomBankProperty.Name)
            Else
                Me.Text = String.Format(My.Resources.SelectValueFor0, _ListCustomBankProperty.Name)
            End If

            AddPropertyValuesToList(_ListCustomBankProperty)

            If Me.ListCustomBankPropertyValue IsNot Nothing Then
                _selectionBeingSet = True
                SetSelected()
                _selectionBeingSet = False
            End If
        End Set
    End Property

    Public Property ListCustomBankPropertyValue() As EntityClasses.ListCustomBankPropertyValueEntity
        Get
            Return _ListCustomBankPropertyValue
        End Get
        Set(ByVal value As EntityClasses.ListCustomBankPropertyValueEntity)
            _ListCustomBankPropertyValue = value
            _selectionBeingSet = True
            SetSelected()
            _selectionBeingSet = False
        End Set
    End Property

    Public ReadOnly Property SelectedListValuesBeforeEditing() As HashSet(Of String)
        Get
            Return _selectedListValuesBeforeEditing
        End Get
    End Property



    Private Sub ValuesCheckedListBox_Format(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ListControlConvertEventArgs) Handles ValuesCheckedListBox.Format
        Dim listValue As EntityClasses.ListValueCustomBankPropertyEntity

        listValue = TryCast(e.ListItem, EntityClasses.ListValueCustomBankPropertyEntity)
        If listValue IsNot Nothing Then
            e.Value = listValue.Name
        Else
            e.Value = "??"
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

    Private Sub ValuesCheckedListBox_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles ValuesCheckedListBox.ItemCheck
        If Not Me.ListCustomBankProperty.MultipleSelect AndAlso e.NewValue = CheckState.Checked Then

            For Each index As Integer In ValuesCheckedListBox.CheckedIndices
                ValuesCheckedListBox.SetItemCheckState(index, CheckState.Unchecked)
            Next

        End If
        If Not _selectionBeingSet Then Me.ButtonOK.Enabled = True
    End Sub



    Private Sub AddPropertyValuesToList(ByVal ListCustomBankProperty As EntityClasses.ListCustomBankPropertyEntity)
        If ListCustomBankProperty IsNot Nothing Then
            For Each listValue As EntityClasses.ListValueCustomBankPropertyEntity In ListCustomBankProperty.ListValueCustomBankPropertyCollection
                ValuesCheckedListBox.Items.Add(listValue)
            Next
        End If
    End Sub

    Private Sub SetSelected()
        Dim index As Integer = 0
        Dim indexes As New ArrayList

        For Each listitem As EntityClasses.ListValueCustomBankPropertyEntity In ValuesCheckedListBox.Items
            Dim selectedValue As EntityClasses.ListCustomBankPropertySelectedValueEntity
            selectedValue = GetCustomPropertySelectedValue(listitem.ListValueBankCustomPropertyId)

            If selectedValue IsNot Nothing Then
                indexes.Add(index)
            End If
            index += 1
        Next

        For Each index In indexes
            ValuesCheckedListBox.SetItemCheckState(index, CheckState.Checked)
        Next

        SetSelectedListValuesBeforeEditing()
        Me.ButtonOK.Enabled = False
    End Sub

    Private Function GetCustomPropertySelectedValue(ByVal ListValueBankCustomPropertyId As Guid) As EntityClasses.ListCustomBankPropertySelectedValueEntity
        Dim filter As SD.LLBLGen.Pro.ORMSupportClasses.IPredicate
        Dim indexes As Generic.List(Of Integer)

        If ListCustomBankPropertyValue IsNot Nothing Then
            filter = (ListCustomBankPropertySelectedValueFields.ListValueBankCustomPropertyId = ListValueBankCustomPropertyId)
            indexes = ListCustomBankPropertyValue.ListCustomBankPropertySelectedValueCollection.FindMatches(filter)

            If indexes.Count = 1 Then
                Return ListCustomBankPropertyValue.ListCustomBankPropertySelectedValueCollection(indexes(0))
            Else
                Return Nothing
            End If
        Else
            Return Nothing
        End If
    End Function

    Public Sub SetSelectedListValuesBeforeEditing()
        _selectedListValuesBeforeEditing = New HashSet(Of String)
        For Each listValue As EntityClasses.ListValueCustomBankPropertyEntity In ValuesCheckedListBox.CheckedItems
            _selectedListValuesBeforeEditing.Add(listValue.ListValueBankCustomPropertyId.ToString())
        Next
    End Sub



    Public Function GetSelectedListValues() As EntityCollection(Of EntityClasses.ListValueCustomBankPropertyEntity)
        Dim collectionToReturn As New EntityCollection(Of EntityClasses.ListValueCustomBankPropertyEntity)

        For Each listValue As EntityClasses.ListValueCustomBankPropertyEntity In ValuesCheckedListBox.CheckedItems
            collectionToReturn.Add(listValue)
        Next

        Return collectionToReturn
    End Function



End Class