Public Class WizardResultTabControl
    Implements IDisposable

    Private _linkedLabel As List(Of LinkLabel)

    Public Sub New()

        InitializeComponent()


    End Sub

    Public Sub AddTextToResultTab(text As String)
        ResultTextBox.Text &= text
    End Sub

    Public Sub AddLink(ByVal introText As String, ByVal theUrl As String, ByVal verticalOffset As Integer)
        Dim newLabel As New LinkLabel
        With newLabel
            .AutoSize = True
            .Text = introText & " " & theUrl
            .Links.Add(introText.Length + 1, theUrl.Length, theUrl)
            .Top = verticalOffset
            AddHandler .LinkClicked, AddressOf LinkLabel_Click
        End With
        ResultTextBox.Controls.Add(newLabel)
    End Sub

    Private Sub LinkLabel_Click(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs)
        Try
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString)
        Catch NoExcelInstalledException As System.ComponentModel.Win32Exception
            MessageBox.Show(My.Resources.ThereIsNoApplicationAssociatedToThisFileExcelIsProbablyNotInstalled, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(My.Resources.ErrorThrown + ex.ToString())
        End Try
    End Sub



    Public Property ResultText() As String
        Get
            Return ResultTextBox.Text
        End Get
        Set(ByVal value As String)
            ResultTextBox.Text = value
        End Set
    End Property

End Class
