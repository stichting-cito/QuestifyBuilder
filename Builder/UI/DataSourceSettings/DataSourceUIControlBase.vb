
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel.Datasources
Imports Questify.Builder.Logic
Imports Questify.Builder.UI.Commanding

Public Class DataSourceUIControlBase
    Inherits UserControl

    Private _mode As IDataSourceSettingsDesignerFactory.DesignerMode = IDataSourceSettingsDesignerFactory.DesignerMode.Config
    Private _commands As IList(Of ICommand) = New List(Of ICommand)
    Private _parameters As IDictionary(Of String, ParameterBinding) = New Dictionary(Of String, ParameterBinding)



    Public Property Mode() As IDataSourceSettingsDesignerFactory.DesignerMode
        Get
            Return _mode
        End Get
        Set(value As IDataSourceSettingsDesignerFactory.DesignerMode)
            _mode = value
        End Set
    End Property

    Public ReadOnly Property Commands() As IList(Of ICommand)
        Get
            Return _commands
        End Get
    End Property

    Public ReadOnly Property Parameters() As IDictionary(Of String, ParameterBinding)
        Get
            Return _parameters
        End Get
    End Property



    Protected Sub OnDependentResourceAdded(e As ResourceNameEventArgs)
        RaiseEvent DependentResourceAdded(Me, e)
    End Sub

    Protected Sub OnDependentResourceRemoved(e As ResourceNameEventArgs)
        RaiseEvent DependentResourceRemoved(Me, e)
    End Sub



    Public Overridable Sub Initialize(settings As DataSourceSettings, DataSourceConfig As DataSourceConfig, resourceManager As ResourceManagerBase)
    End Sub

    Public Overridable Function ValidateUI() As Boolean
        Return True
    End Function

    Public Overridable Function GetValidationMessage() As String
        Return String.Empty
    End Function



    Public Event DependentResourceAdded As EventHandler(Of ResourceNameEventArgs)

    Public Event DependentResourceRemoved As EventHandler(Of ResourceNameEventArgs)


End Class