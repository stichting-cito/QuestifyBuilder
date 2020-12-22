

Imports Questify.Builder.UI

Public Class ItemInTestFilterEditor


    Public Overrides Property Filter() As FilterPredicate
        Get
            Return MyBase.Filter
        End Get
        Set(value As FilterPredicate)
            MyBase.Filter = value

            AssessmentTestNameTextBox.Text = CType(Me.Filter, ItemInTestFilterPredicate).AssessmentTestName
        End Set
    End Property



    Private Sub SelectTestButton_Click(sender As System.Object, e As System.EventArgs) Handles SelectTestButton.Click
        Dim dialog As New SelectTestResourceDialog(Me.ResourceManager.BankId)
        Dim propertyFilter As ItemInTestFilterPredicate = CType(Me.Filter, ItemInTestFilterPredicate)
        dialog.SingleSelect = True
        dialog.ShowDialog()

        If dialog.DialogResult = Windows.Forms.DialogResult.OK Then
            propertyFilter.AssessmentTest = dialog.SelectedEntity.ResourceId
            propertyFilter.AssessmentTestName = dialog.SelectedEntity.Title
            AssessmentTestNameTextBox.Text = CType(Me.Filter, ItemInTestFilterPredicate).AssessmentTestName
        End If
    End Sub


End Class