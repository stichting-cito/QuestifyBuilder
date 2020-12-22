Imports Questify.Builder.Model.ContentModel.EntityClasses

Public Class DeptStructureUserControlBase

    Protected Property _structurePartCustomBankPropertyEntity As CommonEntityBase

    Public Event DeleteDependencyEvent As EventHandler(Of RowsDeletedEventArgs)

    Public Sub New()
        InitializeComponent()

    End Sub

    Private Sub ButtonRemove_Click(sender As Object, e As EventArgs) Handles ButtonRemove.Click
        RaiseEvent DeleteDependencyEvent(Me, New RowsDeletedEventArgs(_structurePartCustomBankPropertyEntity))
    End Sub

    Private Sub ComboBoxDep_Enter(sender As Object, e As EventArgs) Handles ComboBoxDep.Enter
        Dim comboBox As ComboBox = CType(sender, ComboBox)

        comboBox.Tag = comboBox.SelectedItem
    End Sub

    Private Sub ComboBoxDep_Leave(sender As Object, e As EventArgs) Handles ComboBoxDep.Leave
        Dim comboBox As ComboBox = CType(sender, ComboBox)

        comboBox.Tag = Nothing
    End Sub

End Class
