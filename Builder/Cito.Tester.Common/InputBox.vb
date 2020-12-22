Imports System
Imports System.Drawing
Imports System.Windows.Forms


Public Class InputBoxResult
    Public ReturnCode As DialogResult
    Public Text As String
End Class


Public Class InputBox

    Public Delegate Function InputValidator(value As String) As String


    Private Shared _inputDialogForm As Form
    Private Shared _promptLabel As Label
    Private Shared _OKButton As Button
    Private Shared _cancelButton As Button
    Private Shared _inputTextBox As TextBox
    Private Shared _inputValidator As InputValidator

    Public Sub New()
    End Sub



    Private Shared _formCaption As String
    Private Shared _formPrompt As String
    Private Shared _outputResponse As New InputBoxResult()
    Private Shared _defaultValue As String
    Private Shared _xPos As Integer = -1
    Private Shared _yPos As Integer = -1
    Private Shared _masked As Boolean = False



    Private Shared Sub InitializeComponent()
        _inputDialogForm = New Form()
        _promptLabel = New Label()
        _OKButton = New Button()
        _cancelButton = New Button()
        _inputTextBox = New TextBox()
        _inputDialogForm.SuspendLayout()
        _promptLabel.Anchor = CType(((((AnchorStyles.Top Or AnchorStyles.Bottom) Or AnchorStyles.Left) Or AnchorStyles.Right)), AnchorStyles)
        _promptLabel.BackColor = SystemColors.Control
        _promptLabel.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte((0)))
        _promptLabel.Location = New Point(12, 9)
        _promptLabel.Name = "lblPrompt"
        _promptLabel.Size = New Size(302, 82)
        _promptLabel.TabIndex = 3
        _OKButton.FlatStyle = FlatStyle.Standard
        _OKButton.Location = New Point(326, 8)
        _OKButton.Name = "btnOK"
        _OKButton.Size = New Size(64, 24)
        _OKButton.TabIndex = 1
        _OKButton.Text = My.Resources.OK
        AddHandler _OKButton.Click, AddressOf btnOK_Click
        _cancelButton.FlatStyle = FlatStyle.Standard
        _cancelButton.Location = New Point(326, 40)
        _cancelButton.Name = "btnCancel"
        _cancelButton.Size = New Size(64, 24)
        _cancelButton.TabIndex = 2
        _cancelButton.Text = My.Resources.Cancel
        AddHandler _cancelButton.Click, AddressOf btnCancel_Click
        _inputTextBox.Location = New Point(8, 100)
        _inputTextBox.Name = "txtInput"
        _inputTextBox.Size = New Size(379, 20)
        _inputTextBox.TabIndex = 0
        _inputTextBox.Text = ""
        _inputDialogForm.AutoScaleBaseSize = New Size(5, 13)
        _inputDialogForm.ClientSize = New Size(398, 128)
        _inputDialogForm.Controls.Add(_inputTextBox)
        _inputDialogForm.Controls.Add(_cancelButton)
        _inputDialogForm.Controls.Add(_OKButton)
        _inputDialogForm.Controls.Add(_promptLabel)
        _inputDialogForm.FormBorderStyle = FormBorderStyle.FixedDialog
        _inputDialogForm.MaximizeBox = False
        _inputDialogForm.MinimizeBox = False
        _inputDialogForm.Name = "InputBoxDialog"
        _inputDialogForm.ResumeLayout(False)
        _inputDialogForm.AcceptButton = _OKButton
        _inputDialogForm.CancelButton = _cancelButton
    End Sub



    Private Shared Sub LoadForm()
        _outputResponse.ReturnCode = DialogResult.Ignore
        _outputResponse.Text = String.Empty

        _inputTextBox.Text = _defaultValue
        _promptLabel.Text = _formPrompt
        _inputDialogForm.Text = _formCaption

        If _masked Then
            _inputTextBox.UseSystemPasswordChar = _masked
        End If

        Dim workingRectangle As Rectangle = Screen.PrimaryScreen.WorkingArea

        If (_xPos >= 0 AndAlso _xPos < workingRectangle.Width - 100) AndAlso (_yPos >= 0 AndAlso _yPos < workingRectangle.Height - 100) Then
            _inputDialogForm.StartPosition = FormStartPosition.Manual
            _inputDialogForm.Location = New Point(_xPos, _yPos)
        Else
            _inputDialogForm.StartPosition = FormStartPosition.CenterScreen
        End If

        _inputDialogForm.TopMost = True

        Dim prompText As String = _promptLabel.Text

        Dim n As Integer = 0
        Dim index As Integer = 0
        While prompText.IndexOf("" & Chr(10) & "", index) > -1
            index = prompText.IndexOf("" & Chr(10) & "", index) + 1
            n += 1
        End While

        If n = 0 Then
            n = 1
        End If

        Dim txt As Point = _inputTextBox.Location
        txt.Y = txt.Y + (n * 4)
        _inputTextBox.Location = txt
        Dim form As Size = _inputDialogForm.Size
        form.Height = form.Height + (n * 4)
        _inputDialogForm.Size = form

        _inputTextBox.SelectionStart = 0
        _inputTextBox.SelectionLength = _inputTextBox.Text.Length
        _inputTextBox.Focus()
    End Sub



    Private Shared Sub btnOK_Click(sender As Object, e As EventArgs)
        Dim errorMsg As String = Nothing
        If _inputValidator IsNot Nothing Then
            errorMsg = _inputValidator(_inputTextBox.Text)
        End If

        If Not String.IsNullOrEmpty(errorMsg) Then
            MessageBox.Show(errorMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            _outputResponse.ReturnCode = DialogResult.OK
            _outputResponse.Text = _inputTextBox.Text
            _inputDialogForm.DialogResult = DialogResult.OK
            _inputDialogForm.Dispose()
        End If
    End Sub

    Private Shared Sub btnCancel_Click(sender As Object, e As EventArgs)
        _outputResponse.ReturnCode = DialogResult.Cancel
        _outputResponse.Text = String.Empty
        _inputDialogForm.DialogResult = DialogResult.Cancel
        _inputDialogForm.Dispose()
    End Sub




    Public Shared Function Show(prompt As String, masked As Boolean) As InputBoxResult
        Return Show(prompt, masked, Nothing, Nothing, Nothing, -1, -1)
    End Function


    Public Shared Function Show(prompt As String, masked As Boolean, title As String) As InputBoxResult
        Return Show(prompt, masked, title, Nothing, Nothing, -1, -1)
    End Function


    Public Shared Function Show(prompt As String, masked As Boolean, title As String, [default] As String) As InputBoxResult
        Return Show(prompt, masked, title, [default], Nothing, -1, -1)
    End Function


    Public Shared Function Show(prompt As String, masked As Boolean, title As String, [default] As String, validator As InputValidator) As InputBoxResult
        Return Show(prompt, masked, title, [default], validator, -1, -1)
    End Function


    Public Shared Function Show(prompt As String, masked As Boolean, title As String, [default] As String, validator As InputValidator, XPos As Integer, YPos As Integer) As InputBoxResult
        InitializeComponent()

        _formCaption = title
        _formPrompt = prompt
        _defaultValue = [default]
        _xPos = XPos
        _yPos = YPos
        _masked = masked
        _inputValidator = validator

        LoadForm()
        _inputDialogForm.ShowDialog()
        Return _outputResponse
    End Function


End Class
