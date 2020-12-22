Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.ResourceManager
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Plugins.DataSource.StaticGroups.Entities
Imports Questify.Builder.UI
Imports Questify.Builder.UI.Commanding

Public Class AddgroupCommand
    Inherits CommandBase

    Const Inclusion As String = "inclusion"
    Const Normal As String = "normal"
    Const Seeding As String = "seeding"


    Private _config As StaticGroupDataSourceConfig
    Private _onDependentResourceAddedAction As Action(Of ResourceNameEventArgs)
    Private _resourceManager As DataBaseResourceManager
    Private ReadOnly _groupsUnableToSelect As New List(Of String)



    Public Sub New(currentDatasourceIdentifier As String, config As StaticGroupDataSourceConfig, resourceManager As DataBaseResourceManager, onDependentResourceAddedAction As Action(Of ResourceNameEventArgs))
        If config Is Nothing Then
            Throw New ArgumentNullException("config")
        End If

        If resourceManager Is Nothing Then
            Throw New ArgumentNullException("resourceManager")
        End If

        If onDependentResourceAddedAction Is Nothing Then
            Throw New ArgumentNullException("onDependentResourceAddedAction")
        End If

        Me._config = config
        Me._resourceManager = resourceManager
        Me._onDependentResourceAddedAction = onDependentResourceAddedAction

        If Not String.IsNullOrEmpty(currentDatasourceIdentifier) Then
            Dim groups = ResourceFactory.Instance.GetDataSourcesForBank(_resourceManager.BankId, False, Normal, Inclusion).OfType(Of DataSourceResourceEntity)()
            Dim currentDatasource = groups.FirstOrDefault(Function(d) d.Name.Equals(currentDatasourceIdentifier, StringComparison.InvariantCultureIgnoreCase))
            If currentDatasource IsNot Nothing Then
                _groupsUnableToSelect = New List(Of String) From {currentDatasourceIdentifier}

                CollectReferencedDatasources(currentDatasource)
            End If
        End If
    End Sub

    Private ReadOnly Property CurrentGroupReferences As IEnumerable(Of String)
        Get
            Dim refs = _config.GroupDefinition.OfType(Of GroupReference).Select(Function(g) g.ResourceIdentifier)
            Return refs
        End Get
    End Property

    Private Function CollectReferencedDatasources(datasource As DataSourceResourceEntity) As Boolean
        Dim references = ResourceFactory.Instance.GetReferencesForResource(datasource)
        For Each ref In references
            If ref IsNot Nothing AndAlso TypeOf ref Is DataSourceResourceEntity Then
                Dim res = DirectCast(ref, DataSourceResourceEntity)
                If Not _groupsUnableToSelect.Contains(res.Name) Then _groupsUnableToSelect.Add(res.Name)
                CollectReferencedDatasources(res)
            End If
        Next
    End Function



    Public Overrides ReadOnly Property Image As Image
        Get
            Return My.Resources.AddItemToolStripButton_Image
        End Get
    End Property

    Public Overrides ReadOnly Property Name As String
        Get
            Return "AddGroupCommand"
        End Get
    End Property

    Public Overrides ReadOnly Property NameLocalized As String
        Get
            Return My.Resources.AddGroupCommand_Name
        End Get
    End Property




    Public Overrides Sub Execute(parameter As Object)
        Dim dialog As New SelectDataSourceResourceDialog(_resourceManager.BankId, _groupsUnableToSelect.Union(CurrentGroupReferences).ToList(), False, Normal, Inclusion)
        dialog.AllowMultiSelect = True

        If dialog.ShowDialog() = DialogResult.OK Then
            For Each selectedDataSource In dialog.SelectedEntities
                Dim identifierOfGroupToAdd As String = selectedDataSource.Name

                If _config.GroupDefinition.GetEntryByIdentifier(identifierOfGroupToAdd) Is Nothing Then
                    Dim newGroupReference As New GroupReference(identifierOfGroupToAdd)
                    newGroupReference.Title = selectedDataSource.Title
                    _config.GroupDefinition.Add(newGroupReference)

                    _onDependentResourceAddedAction(New ResourceNameEventArgs(newGroupReference.ResourceIdentifier))
                End If
            Next
        End If
    End Sub

    Protected Overrides Function GetCanExecuteState(parameter As Object) As Boolean
        Dim canExecute As Boolean = True
        If parameter IsNot Nothing Then Boolean.TryParse(parameter.ToString, canExecute)
        Return canExecute
    End Function


End Class