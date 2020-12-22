
Imports Questify.Builder.UI

Public Class ChangeTestCodeDialog

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
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub


    Private Sub ButtonOK_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonOK.Click
        If String.IsNullOrEmpty(NewNameTextBox.Text) Then
            MessageBox.Show(My.Resources.ChangeCodeDialog_EmptyCodeName, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            Dim args As New ValidateNewCodeNameEventArgs(NewNameTextBox.Text.Trim())
            OnValidateNewCodeName(args)

            If args.Valid Then
                _newCodeName = NewNameTextBox.Text.Trim()

                Me.DialogResult = DialogResult.OK
                Me.Close()
            End If
        End If
    End Sub


    Public Sub New(ByVal currentCodeName As String)

        InitializeComponent()

        CurrentNameTextBox.Text = currentCodeName
    End Sub
End Class