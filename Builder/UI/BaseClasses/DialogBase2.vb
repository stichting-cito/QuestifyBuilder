Public Class DialogBase2


    Private Sub CancelDialogButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelDialogButton.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub OkDialogButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OkDialogButton.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub


End Class