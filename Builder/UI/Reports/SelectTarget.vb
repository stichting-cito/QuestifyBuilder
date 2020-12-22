Imports System.Linq
Imports System.Windows.Forms
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.Service.Interfaces

Public Class SelectTarget
    Private _selectedHandler As IItemPreviewHandler
    Private _bankId As Integer

    Private Sub SelectTarget_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        InitialiseTargetCombobox()
    End Sub

    Public Sub SelectTarget_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        Dim datasource As OptionValidatorImageExport = DirectCast(Me.OptionValidatorImageExportBindingSource.Current, OptionValidatorImageExport)

        datasource.SelectedHandler = DirectCast(Me.SelectTargetComboBox.SelectedValue, IItemPreviewHandler)

        If Not String.IsNullOrEmpty(datasource.Item("SelectedTarget")) Then
            Me.ErrorProvider.SetError(Me.SelectTargetComboBox, datasource.Item("SelectedTarget"))

            e.Cancel = True
        End If

        e.Cancel = e.Cancel OrElse Not Me.ValidateChildren()

        If Not e.Cancel Then
            Me.OnValidated(e)
        End If
    End Sub

    Private Sub InitialiseTargetCombobox()
        Me.SelectTargetComboBox.ValueMember = "value"
        Me.SelectTargetComboBox.DisplayMember = "key"

        If Me.OptionValidatorImageExportBindingSource IsNot Nothing Then
            Dim handlers = ReportHelperClass.GetTargetDictionaryByHandlers(DirectCast(Me.OptionValidatorImageExportBindingSource.DataSource, OptionValidatorImageExport).Handlers)
            If handlers.Any() Then
                Me.SelectTargetComboBox.DataSource = New BindingSource(handlers, Nothing)
            Else
                Me.ErrorProvider.SetError(Me.SelectTargetComboBox, My.Resources.NoCommonTargetFound)
            End If
        End If
    End Sub

    Private Sub SelectTargetComboBox_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles SelectTargetComboBox.SelectedValueChanged
        If DirectCast(sender, Windows.Forms.ComboBox).SelectedValue IsNot Nothing Then
            Me._selectedHandler = DirectCast(Me.SelectTargetComboBox.SelectedValue, IItemPreviewHandler)
            Me.CbtExtraOptions.PopulateDimensionsComboBox(Me._selectedHandler.Dimensions)
        End If
    End Sub

    Public Sub New(ByVal datasource As OptionValidatorImageExport, bankId As Integer)
        InitializeComponent()

        Me._bankId = bankId
        Me.OptionValidatorImageExportBindingSource.DataSource = datasource
        Me.CbtExtraOptions.OptionValidatorWordExportBindingSource.DataSource = datasource
    End Sub


    Public Sub New()
        InitializeComponent()
    End Sub

    Public Property SelectedTarget As String
        Get
            Return Me.SelectTargetComboBox.SelectedText
        End Get
        Set(value As String)
            Me.SelectTargetComboBox.SelectedText = value
        End Set
    End Property

End Class
