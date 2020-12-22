Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.UI.Commanding

Public Class DataSourcePreview


    Private _dataSourceSettingsList As New List(Of Datasources.DataSourceSettings)
    Private _resourceManager As ResourceManagerBase
    Private _ExecutePreviewCommand As ICommand



    Public ReadOnly Property ExecutePreviewCommand() As ICommand
        Get
            Return _ExecutePreviewCommand
        End Get
    End Property

    Public ReadOnly Property ResourceManager() As ResourceManagerBase
        Get
            Return _resourceManager
        End Get
    End Property



    Public Overridable Sub Initialize(ByVal dataSourceSettingsList As IEnumerable(Of Datasources.DataSourceSettings), ByVal resourceManager As ResourceManagerBase)
        _dataSourceSettingsList.Clear()
        _dataSourceSettingsList.AddRange(dataSourceSettingsList)
        _resourceManager = resourceManager

        DataSourceSettingsEditorInstance.Initialize(IDataSourceSettingsDesignerFactory.DesignerMode.Selection, dataSourceSettingsList, resourceManager)

        _ExecutePreviewCommand = New DelegateCommand(Of Object)(Me.ExecuteButton.Text, AddressOf ExecuteCommand, AddressOf canExecute)
        CommandManager1.Bind(Me.ExecutePreviewCommand, ExecuteButton)
    End Sub

    Protected Overridable Sub PreviewDataSource(ByVal settings As Datasources.DataSourceSettings, ByVal datasource As Datasources.IDataSource)
    End Sub

    Private Sub DataSourcePreview_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        DataSourceSettingsEditorInstance.MaximumSize = New Size(Me.Width, CInt(Me.Height * 0.3))
    End Sub

    Private Sub ExecuteCommand(ByVal param As Object)
        If (OnCanDataSourcePreviewExecuted()) Then

            For Each settings As Datasources.DataSourceSettings In _dataSourceSettingsList
                Dim datasrc As Datasources.IDataSource = settings.CreateGetDataSource()
                Try
                    PreviewDataSource(settings, datasrc)
                Catch ex As ArgumentException
                    Dim message As String = String.Format(My.Resources.Message_CannotExecutePreview, ex.Message)
                    MessageBox.Show(message, My.Resources.Message_CannotExecutePreviewCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End Try
            Next

        End If
    End Sub


    Private Function canExecute(ByVal param As Object) As Boolean
        Return True
    End Function




    Public Event DataSourcePreviewExecuted(ByVal sender As Object, ByVal e As DataSourcePreviewExecutedEventArgs)

    Protected Sub OnDataSourcePreviewExecuted(ByVal e As DataSourcePreviewExecutedEventArgs)
        RaiseEvent DataSourcePreviewExecuted(Me, e)
    End Sub


    Public Event CanDataSourcePreviewExecuted(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)

    Protected Function OnCanDataSourcePreviewExecuted() As Boolean
        Dim args As New System.ComponentModel.CancelEventArgs(False)
        RaiseEvent CanDataSourcePreviewExecuted(Me, args)
        Return Not args.Cancel
    End Function

End Class