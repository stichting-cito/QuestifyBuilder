
Imports Questify.Builder.UI

Public Class ChangeDataSourceCodeDialog

    Private _newCodeName As String

    Public Event ValidateNewCodeName As EventHandler(Of ValidateNewCodeNameEventArgs)

    Protected Sub OnValidateNewCodeName(ByVal e As ValidateNewCodeNameEventArgs)
        RaiseEvent ValidateNewCodeName(Me, e)
    End Sub

    Public ReadOnly Property NewCodeName() As String
        Get
            Return _newCodeName
        End Get
    End Property

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonCancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub ButtonOK_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonOK.Click
        If String.IsNullOrEmpty(NewNameTextBox.Text) Then
            MessageBox.Show(My.Resources.ChangeCodeDialog_EmptyCodeName, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            Dim args As New ValidateNewCodeNameEventArgs(NewNameTextBox.Text.Trim())
            OnValidateNewCodeName(args)

            If args.Valid Then
                _newCodeName = NewNameTextBox.Text.Trim()

                DialogResult = DialogResult.OK
                Close()
            End If
        End If
    End Sub

    Public Sub New(ByVal currentCodeName As String, ByVal referenceCount As Integer)

        InitializeComponent()

        CurrentNameTextBox.Text = currentCodeName
        ReferencedTestsCountTextBox.Text = referenceCount.ToString()

        DisableControls(referenceCount)
    End Sub

    Public Sub DisableControls(ByVal referenceCount As Integer)
        ButtonOK.Enabled = referenceCount = 0
        NewNameTextBox.Enabled = referenceCount = 0
        headerLabel.Text = My.Resources.ChangeDataSourceDialogHeaderTooMuchReferences
    End Sub

End Class