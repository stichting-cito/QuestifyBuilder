Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel.Datasources

Public Class RefreshDataSourcesDialog


    Private _dataSourceSettingsList As IEnumerable(Of DataSourceSettings)
    Private _resourceManagerBase As ResourceManagerBase
    Private _multipleRefreshMode As Boolean



    Public Sub New(ByVal dataSourceSettingsList As IEnumerable(Of DataSourceSettings), ByVal resourceManager As ResourceManagerBase, ByVal multipleRefreshMode As Boolean)
        InitializeComponent()

        _resourceManagerBase = resourceManager
        _dataSourceSettingsList = dataSourceSettingsList
        _multipleRefreshMode = multipleRefreshMode
    End Sub

    Private Sub New()
        InitializeComponent()
    End Sub


    Public ReadOnly Property NumberOfProposalsWanted() As Integer
        Get
            Return CInt(NumberOfProposalsUpDown.Value)
        End Get
    End Property

    Public Sub SetMaxNrOfProposals(maxNrOfProposals As Integer)
        If maxNrOfProposals > 0 Then
            Me.NumberOfProposalsUpDown.Maximum = maxNrOfProposals
            Me.MaxLabel.Text = String.Format(My.Resources.RefreshDataSourceMaxNrOfVariantsLabel, maxNrOfProposals)
            Me.MaxLabel.Visible = True
        Else
            Me.MaxLabel.Visible = False
        End If
    End Sub

    Private Sub RefreshDataSourcesDialog_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not _multipleRefreshMode Then
            MultiRefreshOptionsPanel.Visible = False
        Else
            InstructionsLabel.Text = String.Format(My.Resources.RefreshDataSourcesDialog_InstructionsLabelMultipleRefreshMode, Environment.NewLine)
        End If

        DataSourceSettingsEditorInstance.Initialize(IDataSourceSettingsDesignerFactory.DesignerMode.Selection, _dataSourceSettingsList, _resourceManagerBase)
    End Sub
End Class