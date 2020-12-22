Imports System.ComponentModel
Imports System.Windows.Forms.Design

<DefaultProperty("PreviewSource")> _
Public Class PreviewPanel
    Inherits Panel


    Private _previewHandler As PreviewHandlerInstanceInfo
    Private _previewSource As String



    <Browsable(True)> _
    <Category("PreviewPanel")> _
    <Description("Gets and sets the file used to be previewed in preview pane.")> _
    <Editor(GetType(FileNameEditor), GetType(Drawing.Design.UITypeEditor))> _
    Public Property PreviewSource() As String
        Get
            Return _previewSource
        End Get
        Set(ByVal value As String)
            If String.IsNullOrEmpty(value) Then
                ClearPreviewPane()
            Else
                SetPreview(value, Me.Handle, Me.DisplayRectangle)
            End If
            _previewSource = value
        End Set
    End Property



    Public Sub ClearPreviewPane()
        If _previewHandler IsNot Nothing Then
            PreviewManager.DetachPreviewHandler(_previewHandler)
            _previewHandler = Nothing
            _previewSource = Nothing
        End If
    End Sub

    Private Sub InitializeComponent()
        Me.SuspendLayout()
        Me.ResumeLayout(False)

    End Sub

    Private Sub PreviewPanel_HandleDestroyed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.HandleDestroyed
        Me.ClearPreviewPane()
    End Sub

    Private Sub PreviewPanel_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If _previewHandler IsNot Nothing AndAlso Not Me.DesignMode Then
            PreviewManager.InvalidateAttachedPreview(_previewHandler, Me.DisplayRectangle)
        End If
    End Sub

    Private Sub SetPreview(ByVal fileName As String, ByVal hWnd As IntPtr, ByVal viewRect As Rectangle)
        If Me.DesignMode Then
            Exit Sub
        End If

        If String.IsNullOrEmpty(fileName) Then
            Throw New ArgumentNullException("fileName")
        End If

        If Not IO.File.Exists(fileName) Then
            Throw New IO.FileNotFoundException("Unable to preview file.", fileName)
        End If

        Me.Controls.Clear()

        Try
            ClearPreviewPane()
            _previewHandler = PreviewManager.AttachPreviewHandler(hWnd, fileName, viewRect)
        Catch ex As Exception
            Dim errorLabel As New Label
            With errorLabel
                .Name = "ErrorLabel"
                .Text = String.Format("an error occurred while creating preview: '{0}'", ex.Message)
                .Dock = DockStyle.Top
            End With
            Me.Controls.Add(errorLabel)
        End Try
    End Sub


End Class