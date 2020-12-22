
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel.Datasources
Imports Questify.Builder.Logic.ResourceManager
Imports Questify.Builder.UI

Public Class DynamicGroupEditor
    Inherits DataSourceUIControlBase


    Private _datasourceConfig As DynamicGroupDatasourceConfig
    Private _resourceManager As DataBaseResourceManager
    Private _settings As DataSourceSettings



    Public Overrides Sub Initialize(ByVal settings As DataSourceSettings, ByVal DataSourceConfig As DataSourceConfig, ByVal resourceManager As ResourceManagerBase)
        _datasourceConfig = DirectCast(DataSourceConfig, DynamicGroupDatasourceConfig)
        _settings = settings

        If TypeOf resourceManager Is DataBaseResourceManager Then
            _resourceManager = DirectCast(resourceManager, DataBaseResourceManager)
            Me.QueryEditorControl.ResourceManager = _resourceManager
        Else
            Throw New NotSupportedException("This editor only works in a TestBuilder environment.")
        End If

        Me.QueryEditorControl.Query = _datasourceConfig.Query
    End Sub

    Public Overrides Function ValidateUI() As Boolean
        Dim result As Boolean = False
        Try
            result = True
        Catch ex As Exception

        End Try

        Return result
    End Function


End Class