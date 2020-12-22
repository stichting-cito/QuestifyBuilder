Public Class DialogBase


    Public Property OkEnabled() As Boolean
        Get
            Return OKButton.Enabled
        End Get
        Set(ByVal value As Boolean)
            OKButton.Enabled = value
        End Set
    End Property




    Protected Overridable Function OnOk() As Boolean
        Return True
    End Function

    Protected Overridable Function OnCancel() As Boolean
        Return True
    End Function



    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
        If OnOk() Then
            DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End If
    End Sub

    Private Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton1.Click
        If OnCancel() Then
            DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        End If
    End Sub



End Class