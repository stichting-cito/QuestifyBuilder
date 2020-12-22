Imports System.Drawing
Imports System.Windows.Forms
Imports Janus.Windows.GridEX
Imports Cito.Tester.ContentModel.Datasources
Imports System.Linq
Imports Questify.Builder.Logic.ResourceManager
Imports Questify.Builder.Logic.TestConstruction.ChainHandlers.Processing
Imports Questify.Builder.Plugins.DataSource.StaticGroups.Entities
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.UI
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.Service.Model.Entities

Public Class AddItemsFromCodesCommand
    Inherits AddItemCommand



    Public Sub New(mode As DataSourceBehaviourEnum, config As StaticGroupDataSourceConfig, itemInGroupGrid As GridEX, resourceManager As DataBaseResourceManager, onDependentResourceAddedAction As Action(Of ResourceNameEventArgs))
        MyBase.New(mode, config, itemInGroupGrid, resourceManager, onDependentResourceAddedAction)
    End Sub



    Public Overrides ReadOnly Property Image As Image
        Get
            Return My.Resources.AddItemsFromCodesToolStripButton_Image
        End Get
    End Property

    Public Overrides ReadOnly Property Name As String
        Get
            Return "AddItemsFromCodesCommand"
        End Get
    End Property

    Public Overrides ReadOnly Property NameLocalized As String
        Get
            Return My.Resources.AddItemsFromCodesCommand_Name
        End Get
    End Property



    Public Overrides Sub Execute(parameter As Object)
        Using dialog As New AddItemsFromCodeDialog()
            If dialog.ShowDialog() = DialogResult.OK Then
                AddItemsToGroupInCodeListOrder(dialog.ListOfItemCodes)
            End If
        End Using
    End Sub

    Private Sub AddItemsToGroupInCodeListOrder(codeListOrder As List(Of String))
        If Not List(Of String).ReferenceEquals(codeListOrder, Nothing) AndAlso codeListOrder.Any() Then
            Dim selectedEntities = _itemInGroupGrid.SelectedItems.OfType(Of GridEXSelectedItem).Where(Function(si) si IsNot Nothing AndAlso si.GetRow IsNot Nothing).Select(Function(selectedItem) selectedItem.GetRow().DataRow)

            Dim selectedItemGroup = selectedEntities.OfType(Of ItemGroup).FirstOrDefault
            If selectedItemGroup Is Nothing Then
                Dim itemResource = selectedEntities.OfType(Of ItemReference).FirstOrDefault
                If itemResource IsNot Nothing Then
                    selectedItemGroup = _config.GroupDefinition.OfType(Of ItemGroup).FirstOrDefault(Function(ig) ig.Items.Any(Function(ir) ir.ResourceIdentifier = itemResource.ResourceIdentifier))
                End If
            End If

            _facade.ResetFacade()

            Dim codesToAdd As IEnumerable(Of String) = codeListOrder.Where(Function(c) Not _config.GroupDefinition.Where(Function(sge) sge.ResourceIdentifier = c).Any()).ToList()

            Dim itemResourcesToAdd As IEnumerable(Of ItemResourceDto) = DtoFactory.Item.GetResourcesForBank(_resourceManager.BankId).Where(Function(ird) codesToAdd.Contains(ird.name))

            _facade.EditContextHandlers.Add(New AddItemToGroupHandler(itemResourcesToAdd, _config, _onDependentResourceAddedAction, selectedItemGroup))

            _facade.AddItems(ProcessingHelpers.GetItemResourceRefList(itemResourcesToAdd))

            _itemInGroupGrid.Refetch()
            If selectedItemGroup IsNot Nothing Then
                _itemInGroupGrid.SelectedItems.Clear()
                Dim row = _itemInGroupGrid.GetRows.FirstOrDefault(Function(r) TypeOf r.DataRow Is ItemGroup AndAlso DirectCast(r.DataRow, ItemGroup).ResourceIdentifier = selectedItemGroup.ResourceIdentifier)
                If row IsNot Nothing Then
                    _itemInGroupGrid.SelectedItems.Add(row.RowIndex)
                End If
            End If
        End If
    End Sub


End Class