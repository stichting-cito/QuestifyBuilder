Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.ComponentModel
Imports Questify.Builder.Model.ContentModel

Public Class BankMetaData


    Private _bank As BankEntity
    Private _initialializeDataSource As Boolean
    Private _tooltip As New ToolTip()
    Private _maxBankNameLength As Integer



    <Description("This event will be raised when data is changed on this control"), Category("BankMetaData Control events")> _
    Public Event DataChanged As EventHandler(Of EventArgs)

    Protected Sub OnDataChanged(ByVal e As EventArgs)
        RaiseEvent DataChanged(Me, e)
    End Sub



    Public Property Datasource() As BankEntity
        Get
            Return _bank
        End Get
        Set(ByVal value As BankEntity)
            If value IsNot Nothing Then
                _bank = value
                _initialializeDataSource = True
                BankMetaDataBindingSource.DataSource = _bank
                _initialializeDataSource = False
            End If
        End Set
    End Property



    Public Sub New()

        InitializeComponent()

        _maxBankNameLength = CType(New BankEntity().Fields(BankFieldIndex.Name), SD.LLBLGen.Pro.ORMSupportClasses.EntityField2).MaxLength
        NameTextBox.MaxLength = _maxBankNameLength
    End Sub



    Private Sub NameTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NameTextBox.TextChanged
        If Not _initialializeDataSource Then OnDataChanged(New EventArgs)

        If NameTextBox.Text.Length = _maxBankNameLength Then
            _tooltip.Show(String.Format(My.Resources.BankNameMaxLenghtExceeded, NameTextBox.MaxLength), NameTextBox, 5000)
        End If
    End Sub


End Class
