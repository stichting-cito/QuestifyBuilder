Public Class PropertyDialogBase


    Private _closingAfterOKButtonClicked As Boolean
    Private _applyButtonInvoked As Boolean = False



    Public ReadOnly Property ApplyButtonInvoked() As Boolean
        Get
            Return _applyButtonInvoked
        End Get
    End Property

    Public Property ApplyEnabled() As Boolean
        Get
            Return ApplyButton.Enabled
        End Get
        Set(ByVal value As Boolean)
            ApplyButton.Enabled = value
        End Set
    End Property

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

    Protected Overridable Sub OnApply()
    End Sub



    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click

        If OnOk() Then
            DialogResult = Windows.Forms.DialogResult.OK
            _closingAfterOKButtonClicked = True
            Me.Close()
        End If
    End Sub

    Private Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton1.Click
        Me.Close()
    End Sub

    Private Sub ApplyButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ApplyButton.Click
        _applyButtonInvoked = True
        OnApply()
    End Sub


    Private Sub PropertyDialogBase_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not _closingAfterOKButtonClicked Then
            If OnCancel() Then
                DialogResult = Windows.Forms.DialogResult.Cancel
            End If
        End If
    End Sub






End Class