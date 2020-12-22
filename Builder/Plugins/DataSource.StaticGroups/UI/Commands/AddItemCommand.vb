Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.DataSourceConstruction.Helpers
Imports System.Drawing
Imports System.Windows.Forms
Imports Janus.Windows.GridEX
Imports Questify.Builder.Logic
Imports Cito.Tester.ContentModel.Datasources
Imports System.Linq
Imports Cito.Tester.Common
Imports Questify.Builder.Logic.Common.Filtering
Imports Questify.Builder.Logic.ResourceManager
Imports Questify.Builder.Logic.TestConstruction.ChainHandlers.Processing
Imports Questify.Builder.Plugins.DataSource.StaticGroups.Entities
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.UI
Imports Questify.Builder.UI.Commanding
Imports Questify.Builder.Model.ContentModel.FactoryClasses

Public Class AddItemCommand
    Inherits CommandBase


    Friend ReadOnly _config As StaticGroupDataSourceConfig
    Private WithEvents _dialog As SelectItemResourceDialog
    Friend ReadOnly _itemInGroupGrid As GridEX
    Friend ReadOnly _onDependentResourceAddedAction As Action(Of ResourceNameEventArgs)
    Friend ReadOnly _resourceManager As DataBaseResourceManager
    Private ReadOnly _mode As DataSourceBehaviourEnum
    Friend ReadOnly _facade As DataSourceConstructionFacade
    Private ReadOnly _itemsAlreadyInGroup As List(Of String)
    Private Const CInclusion As String = "inclusion"



    Public Sub New(mode As DataSourceBehaviourEnum, config As StaticGroupDataSourceConfig, itemInGroupGrid As GridEX, resourceManager As DataBaseResourceManager, onDependentResourceAddedAction As Action(Of ResourceNameEventArgs))
        If config Is Nothing Then
            Throw New ArgumentNullException("config")
        End If

        If itemInGroupGrid Is Nothing Then
            Throw New ArgumentNullException("itemInGroupGrid")
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
        Me._itemInGroupGrid = itemInGroupGrid
        Me._mode = mode
        _itemsAlreadyInGroup = New List(Of String)
        If (_mode = DataSourceBehaviourEnum.Inclusion) Then
            Dim inclusionGroups = ResourceFactory.Instance.GetDataSourcesForBank(_resourceManager.BankId, False, CInclusion).OfType(Of DataSourceResourceEntity)()
            Dim dependentResourceIds = New List(Of Guid)
            For Each inclusionGroup In inclusionGroups
                dependentResourceIds = dependentResourceIds.Union(inclusionGroup.DependentResourceCollection.Select(Function(dr) dr.DependentResourceId)).ToList
            Next
            If dependentResourceIds.Count > 0 Then
                _itemsAlreadyInGroup = _itemsAlreadyInGroup.Union(ResourceFactory.Instance.GetResourcesByIdsWithOption(dependentResourceIds, New ItemResourceEntityFactory(), New ResourceRequestDTO()).OfType(Of ItemResourceEntity).Select(Function(r) r.Name.ToString)).Where(Function(id) Not Items.Contains(id)).ToList
            End If
        End If
        _facade = New DataSourceConstructionFacade()
    End Sub



    Public Overrides ReadOnly Property Image As Image
        Get
            Return My.Resources.AddItemToolStripButton_Image
        End Get
    End Property

    Public Overrides ReadOnly Property Name As String
        Get
            Return "AddItemCommand"
        End Get
    End Property

    Public Overrides ReadOnly Property NameLocalized As String
        Get
            Return My.Resources.AddItemCommand_Name
        End Get
    End Property

    Private ReadOnly Property Items As IEnumerable(Of String)
        Get
            Dim itms = _config.GroupDefinition.Select(Function(itm) itm.ResourceIdentifier).ToList
            For Each item In _config.GroupDefinition.OfType(Of ItemGroup).SelectMany(Function(itmgroup) itmgroup.Items).Select(Function(i) i.ResourceIdentifier)
                If Not itms.Contains(item) Then itms.Add(item)
            Next
            Return itms
        End Get
    End Property



    Public Overrides Sub Execute(parameter As Object)
        If _dialog Is Nothing Then
            _dialog = New SelectItemResourceDialog(_resourceManager.BankId)
        End If

        _dialog.ResourceAlreadyInContextFunction = AddressOf ResourceAlreadyInContextDelegate
        _dialog.Show()
    End Sub


    Private Function ResourceAlreadyInContextDelegate(itemIdentifier As String) As Boolean
        Return _itemsAlreadyInGroup.Union(Items).Contains(itemIdentifier)
    End Function

    Private Sub ThisEditorItemSelectorDialog_FormClosing(sender As Object, e As FormClosingEventArgs) Handles _dialog.FormClosing
        Me._dialog.Dispose()
        Me._dialog = Nothing
    End Sub

    Protected Overrides Function GetCanExecuteState(parameter As Object) As Boolean
        Dim canExecute As Boolean = True
        If parameter IsNot Nothing Then Boolean.TryParse(parameter.ToString, canExecute)
        Return canExecute
    End Function

    Private Sub _dialog_AddingResource(sender As Object, e As SelectedItemCollectionEventArgs) Handles _dialog.AddingResource
        Dim dialog = DirectCast(sender, SelectItemResourceDialog)
        Dim selectedItemGroup = _itemInGroupGrid.SelectedItems.OfType(Of GridEXSelectedItem).Where(Function(si) si IsNot Nothing AndAlso si.GetRow IsNot Nothing).Select(Function(selectedItem) selectedItem.GetRow().DataRow).OfType(Of ItemGroup).FirstOrDefault
        If selectedItemGroup Is Nothing Then
            Dim itemResource = _itemInGroupGrid.SelectedItems.OfType(Of GridEXSelectedItem).Where(Function(si) si IsNot Nothing AndAlso si.GetRow IsNot Nothing).Select(Function(selectedItem) selectedItem.GetRow().DataRow).OfType(Of ItemReference).FirstOrDefault
            If itemResource IsNot Nothing Then
                selectedItemGroup = _config.GroupDefinition.OfType(Of ItemGroup).FirstOrDefault(Function(ig) ig.Items.Any(Function(ir) ir.ResourceIdentifier = itemResource.ResourceIdentifier))
            End If
        End If
        _facade.ResetFacade()

        Dim excludedItems = _itemsAlreadyInGroup.Union(Items)
        _facade.PostFilteringHandlers.Add(New ModifyDataSourceRequestHandler(FilterRequestTypeEnum.Remove, excludedItems))

        _facade.EditContextHandlers.Add(New AddItemToGroupHandler(dialog.SelectedEntities, _config, _onDependentResourceAddedAction, selectedItemGroup))

        _facade.AddItems(ProcessingHelpers.GetItemResourceRefList(dialog.SelectedEntities))

        _itemInGroupGrid.Refetch()
        If selectedItemGroup IsNot Nothing Then
            _itemInGroupGrid.SelectedItems.Clear()
            Dim row = _itemInGroupGrid.GetRows.FirstOrDefault(Function(r) TypeOf r.DataRow Is ItemGroup AndAlso DirectCast(r.DataRow, ItemGroup).ResourceIdentifier = selectedItemGroup.ResourceIdentifier)
            If row IsNot Nothing Then
                _itemInGroupGrid.SelectedItems.Add(row.RowIndex)
            End If
        End If
        dialog.RefreshDatasource()

    End Sub


End Class