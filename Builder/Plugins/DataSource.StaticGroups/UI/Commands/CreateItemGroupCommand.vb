Imports System.Drawing
Imports System.Linq
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.PlugIns.DataSource.StaticGroups.Entities
Imports Questify.Builder.UI.Commanding

Public Class CreateItemGroupCommand
    Inherits CommandBase




    Private _config As StaticGroupDataSourceConfig



    Public Sub New(config As StaticGroupDataSourceConfig)
        If config Is Nothing Then
            Throw New ArgumentNullException("config")
        End If
        Me._config = config
    End Sub



    Public Overrides ReadOnly Property Image As Image
        Get
            Return My.Resources.AddItemToolStripButton_Image
        End Get
    End Property

    Public Overrides ReadOnly Property Name As String
        Get
            Return "CreateGroupCommand"
        End Get
    End Property

    Public Overrides ReadOnly Property NameLocalized As String
        Get
            Return My.Resources.CreateGroupCommand_Name
        End Get
    End Property




    Public Overrides Sub Execute(parameter As Object)
        Dim newSubGroup As New ItemGroup() With {
            .ResourceIdentifier = String.Concat(My.Resources.Group, String.Format("_{0}", (_config.GroupDefinition.OfType(Of ItemGroup).Count + 1).ToString)),
            .Title = String.Format("{0} {1}", My.Resources.Group, .ResourceIdentifier.ExtractNumber)
        }
        _config.GroupDefinition.Add(newSubGroup)
    End Sub

    Protected Overrides Function GetCanExecuteState(parameter As Object) As Boolean
        Dim canExecute As Boolean = True
        If parameter IsNot Nothing Then Boolean.TryParse(parameter.ToString, canExecute)
        Return canExecute
    End Function


End Class