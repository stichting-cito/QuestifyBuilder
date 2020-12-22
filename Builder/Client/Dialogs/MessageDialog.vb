Public Class MessageDialog


    Public Sub New(ByVal caption As String, ByVal message As String)
        InitializeComponent()

        Me.Caption = caption
        Me.Message = message
    End Sub



    Public Property Caption() As String
        Get
            Return Me.Text
        End Get
        Set(ByVal value As String)
            Me.Text = value
        End Set
    End Property

    Public Property Message() As String
        Get
            Return Me.MessageTextBox.Text
        End Get
        Set(ByVal value As String)
            Me.MessageTextBox.Text = value
        End Set
    End Property



    Private Sub LoginDialog_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Shown
        Static setForegroundWindowDone As Boolean
        If Not setForegroundWindowDone Then
            setForegroundWindowDone = True
            Try
                SetForegroundWindow(Me.Handle)
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub OKButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles OKButton.Click
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Declare Function SetForegroundWindow Lib "user32" (ByVal hwnd As IntPtr) As Integer


End Class