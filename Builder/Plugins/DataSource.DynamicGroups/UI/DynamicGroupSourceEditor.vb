
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel.Datasources
Imports Questify.Builder.Logic.ResourceManager
Imports Questify.Builder.UI

Public Class DynamicGroupSourceEditor
    Inherits DataSourceUIControlBase


    Private _datasourceConfig As DynamicGroupDatasourceConfig
    Private _resourceManager As DataBaseResourceManager
    Private _settings As DataSourceSettings



    Public Overrides Sub Initialize(ByVal settings As DataSourceSettings, ByVal DataSourceConfig As DataSourceConfig, ByVal resourceManager As ResourceManagerBase)
        _datasourceConfig = DirectCast(DataSourceConfig, DynamicGroupDatasourceConfig)
        _settings = settings

        If TypeOf resourceManager Is DataBaseResourceManager Then
            _resourceManager = DirectCast(resourceManager, DataBaseResourceManager)
        Else
            Throw New NotSupportedException("This editor only works in a TestBuilder environment.")
        End If

        InitSourceEditor(_datasourceConfig)
    End Sub

    Public Overrides Function ValidateUI() As Boolean
        Dim result As Boolean = False

        Try
            PersistSourceEditor(_datasourceConfig)
            result = True
        Catch ex As Exception

        End Try

        Return result
    End Function

    Private Sub InitSourceEditor(_datasourceConfig As DynamicGroupDatasourceConfig)
        QuerySourceTextBox.Text = Cito.Tester.Common.SerializeHelper.XmlSerializeToString(_datasourceConfig.Query)
    End Sub

    Private Sub PersistSourceEditor(_datasourceConfig As DynamicGroupDatasourceConfig)
        Dim itemQuery As ItemQuery = Cito.Tester.Common.SerializeHelper.XmlDeserializeFromString(QuerySourceTextBox.Text, GetType(ItemQuery))
        _datasourceConfig.Query = itemQuery
    End Sub

    Private Sub QuerySourceTextBox_LostFocus(sender As Object, e As System.EventArgs) Handles QuerySourceTextBox.LostFocus
        Try
            PersistSourceEditor(_datasourceConfig)
        Catch ex As Exception
        End Try
    End Sub


End Class