Imports System.ComponentModel

Public Class ProgressDialog

    Private ReadOnly _handleProgress As Action(Of ProgressHandler)
    Private ReadOnly _autoClose As Boolean = False
    Private _cancelled As Boolean = True

    Sub New(ByVal jobTitle As String, autoClose As Boolean, ByVal job As DoWorkEventHandler, handleProgress As Action(Of ProgressHandler))

        InitializeComponent()

        Me.Text = jobTitle
        AddHandler BackgroundWorker1.DoWork, job
        _handleProgress = handleProgress
        _autoClose = autoClose
    End Sub

    Public ReadOnly Property Cancelled As Boolean
        Get
            Return _cancelled
        End Get
    End Property


    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        _handleProgress(New ProgressHandler(Me, Nothing))
        BackgroundWorker1.RunWorkerAsync()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles OK_Button.Click
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Cancel_Button.Click
        Me.Text = $"{Me.Text}:---{My.Resources.Cancelled}"
        Me.txt.Text = My.Resources.Cancelled
        _cancelled = True
        Cancel_Button.Enabled = False
        BackgroundWorker1.CancelAsync()
    End Sub


    Private Sub ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
        _handleProgress(New ProgressHandler(Me, e))
    End Sub

    Private Sub Completed(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        If _autoClose Then
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Else
            OK_Button.Enabled = True
            Cancel_Button.Enabled = False
            PBar.Style = ProgressBarStyle.Blocks
            PBar.Value = PBar.Maximum
        End If
    End Sub



    Private Sub EvntClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = Not OK_Button.Enabled AndAlso Not _autoClose
    End Sub


    Public Class ProgressHandler

        Private ReadOnly _dlg As ProgressDialog
        Private ReadOnly _progress As ProgressChangedEventArgs
        Private _cancelled As Boolean = False
        Private _succesfullyCompleted As Boolean = True

        Friend Sub New(ByVal dlg As ProgressDialog, ByVal progress As ProgressChangedEventArgs)
            _dlg = dlg
            _progress = progress
        End Sub

        Public ReadOnly Property Progress As ProgressChangedEventArgs
            Get
                Return _progress
            End Get
        End Property

        Public Sub ProgressBar(ByVal percentage As Integer)
            _dlg.PBar.Value = percentage
        End Sub

        Public Sub ProgressBarDone()
            _dlg.PBar.Style = ProgressBarStyle.Blocks
            _dlg.PBar.Value = _dlg.PBar.Maximum
        End Sub

        Public Sub ProgressMarquee()
            _dlg.PBar.Style = ProgressBarStyle.Marquee
        End Sub

        Public Sub ProgressBlocks()
            _dlg.PBar.Style = ProgressBarStyle.Blocks
        End Sub


        Public Sub SetText(ByVal txt As String)
            _dlg.txt.Text = txt
        End Sub

    End Class
End Class
