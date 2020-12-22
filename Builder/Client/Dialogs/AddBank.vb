
Imports System.ComponentModel
Imports System.Security
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Model.Entities
Imports SD.LLBLGen.Pro.ORMSupportClasses

Public Class AddBank


    Private ReadOnly _parentBank As BankDto
    Private _newBankId As Integer = 0
    Private ReadOnly _tooltip As New ToolTip()
    Private ReadOnly _maxBankNameLength As Integer


    Public ReadOnly Property NewBankId() As Integer
        Get
            Return _newBankId
        End Get
    End Property

    Private Sub DialogCancelButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles DialogCancelButton.Click
        Me.Close()
    End Sub

    Private Sub DialogOkButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles DialogOkButton.Click
        If Not String.IsNullOrEmpty(NameTextBox.Text) AndAlso Not String.IsNullOrEmpty(NameTextBox.Text.Trim()) Then
            Dim newBank As BankEntity = New BankEntity()
            If _parentBank IsNot Nothing Then
                newBank.ParentBankId = _parentBank.id
            End If
            newBank.Name = NameTextBox.Text
            Try
                BankFactory.Instance.UpdateBank(newBank)
                _newBankId = newBank.Id
            Catch securityException As SecurityException
                MessageBox.Show(securityException.Message)
            End Try

            Me.Close()
        Else
            MessageBox.Show(My.Resources.AddBank_EnterValidInfo, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub AddBank_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        If _parentBank IsNot Nothing Then
            Me.Text = String.Format(My.Resources.CreateNewBankAsChild, _parentBank.name)
        Else
            Me.Text = String.Format(My.Resources.CreateNewBankOnRootlevel)
        End If
    End Sub



    Public Sub New()

        InitializeComponent()

    End Sub
    Public Sub New(ByVal parentBankId As Integer?)
        InitializeComponent()

        If parentBankId.HasValue Then
            _parentBank = DtoFactory.Bank.Get(parentBankId.Value)
        End If
    End Sub

    Public Sub New(ByVal parentBank As BankDto)
        InitializeComponent()

        _parentBank = parentBank
        _maxBankNameLength = CType(New BankEntity().Fields(BankFieldIndex.Name), EntityField2).MaxLength
        NameTextBox.MaxLength = _maxBankNameLength
    End Sub


    Private Sub ValidateName(sender As Object, e As CancelEventArgs) Handles NameTextBox.Validating
        If Not String.IsNullOrEmpty(NameTextBox.Text) AndAlso Not String.IsNullOrEmpty(NameTextBox.Text.Trim()) Then
        Else
            ErrorProvider1.SetError(NameTextBox, My.Resources.AddBank_EnterValidInfo)
            e.Cancel = True
        End If
        DialogOkButton.Enabled = Not e.Cancel
    End Sub


    Private Sub NameTextBox_TextChanged(sender As Object, e As EventArgs) Handles NameTextBox.TextChanged
        ValidateChildren()

        If NameTextBox.Text.Length = _maxBankNameLength Then
            _tooltip.Show(String.Format(My.Resources.BankNameMaxLenghtExceeded, NameTextBox.MaxLength), NameTextBox, 5000)
        End If
    End Sub
End Class
