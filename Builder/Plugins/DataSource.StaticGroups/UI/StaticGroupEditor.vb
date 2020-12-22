Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports Cito.Tester.Common
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.Logic.ResourceManager
Imports Questify.Builder.UI
Imports Questify.Builder.UI.Commanding
Imports Janus.Windows.GridEX
Imports Cito.Tester.ContentModel.Datasources
Imports Questify.Builder.Logic
Imports Questify.Builder.Plugins.DataSource.StaticGroups.Entities
Imports Questify.Builder.Logic.Service.Factories

Public Class StaticGroupEditor
    Inherits DataSourceUIControlBase

    Private _addGroupCommand As ICommand
    Private _createGroupCommand As ICommand
    Private _addItemCommand As ICommand
    Private _addItemsFromCodesCommand As ICommand
    Private _config As StaticGroupDataSourceConfig
    Private _moveDownCommand As ICommand
    Private _moveUpCommand As ICommand
    Private _removeCommand As ICommand
    Private _resourceManager As DataBaseResourceManager
    Private _settings As DataSourceSettings



    Public ReadOnly Property CreateGroupCommand As ICommand
        Get
            Return _createGroupCommand
        End Get
    End Property


    Public ReadOnly Property AddGroupCommand As ICommand
        Get
            Return _addGroupCommand
        End Get
    End Property

    Public ReadOnly Property AddItemCommand As ICommand
        Get
            Return _addItemCommand
        End Get
    End Property

    Public ReadOnly Property AddItemsCommand As ICommand
        Get
            Return _addItemsFromCodesCommand
        End Get
    End Property

    Public ReadOnly Property MoveDownCommand As ICommand
        Get
            Return _moveDownCommand
        End Get
    End Property

    Public ReadOnly Property MoveUpCommand As ICommand
        Get
            Return _moveUpCommand
        End Get
    End Property

    Public ReadOnly Property RemoveCommand As ICommand
        Get
            Return _removeCommand
        End Get
    End Property



    Public Overrides Function ValidateUI() As Boolean
        If _config.GroupEntryType = GroupEntryTypes.SeedingGroups AndAlso
             (Not _config.GroupDefinition.OfType(Of ItemGroup).Any OrElse
             Not _config.GroupDefinition.OfType(Of ItemGroup).Any(Function(i) i.Items.Any)) Then
            Return False
        End If
        Return True
    End Function

    Public Overrides Function GetValidationMessage() As String
        Dim message As String = String.Empty
        If _config.GroupEntryType = GroupEntryTypes.SeedingGroups AndAlso Not _config.GroupDefinition.OfType(Of ItemGroup).Any Then
            message = My.Resources.AtLeastOneGroup
        ElseIf _config.GroupEntryType = GroupEntryTypes.SeedingGroups AndAlso _config.GroupDefinition.OfType(Of ItemGroup).Any AndAlso Not _config.GroupDefinition.OfType(Of ItemGroup).Any(Function(i) i.Items.Any) Then
            message = My.Resources.SubGroup_Empty
        End If
        Return message
    End Function

    Public Overrides Sub Initialize(settings As DataSourceSettings, dataSourceConfig As DataSourceConfig, resourceManager As ResourceManagerBase)
        _settings = settings
        _config = DirectCast(dataSourceConfig, StaticGroupDataSourceConfig)

        Dim dataBaseResourceManager = TryCast(resourceManager, DataBaseResourceManager)
        If (dataBaseResourceManager IsNot Nothing) Then
            _resourceManager = dataBaseResourceManager
        End If

        EnrichEntriesWithMetadata()

        StaticGroupBindingSource.DataSource = _config.GroupDefinition

        Me.ItemInGroupGrid.ExpandRecords()
    End Sub

    Private Sub EnrichEntriesWithMetadata()
        If _resourceManager IsNot Nothing AndAlso _config.GroupDefinition.Count > 0 Then

            _resourceManager.IncludeMetaData = ResourceManager.MetaDataType.All

            Dim itemReferenceBatchCollection As New List(Of ItemReference)
            For Each entry As StaticGroupEntry In _config.GroupDefinition
                If TypeOf entry Is ItemReference Then
                    itemReferenceBatchCollection.Add(DirectCast(entry, ItemReference))

                ElseIf TypeOf entry Is GroupReference Then
                    Dim meta As MetaDataCollection = _resourceManager.GetResourceMetaData(entry.ResourceIdentifier)
                    entry.Title = meta.Item("Title", MetaData.enumMetaDataType.BankMetaData).Value
                ElseIf TypeOf entry Is ItemGroup Then
                    entry.Title = String.Format("{0} {1}", My.Resources.Group, entry.ResourceIdentifier.ExtractNumber)
                    itemReferenceBatchCollection.AddRange(DirectCast(entry, ItemGroup).Items.OfType(Of ItemReference))
                End If
            Next

            If itemReferenceBatchCollection.Count > 0 Then
                Dim identifiers As New List(Of String)
                For Each ref As ItemReference In itemReferenceBatchCollection
                    identifiers.Add(ref.ResourceIdentifier)
                Next

                Dim itemResourceEntities As EntityCollection(Of ItemResourceEntity) = ResourceFactory.Instance.GetItemsByCodes(identifiers, _resourceManager.BankId, New ItemResourceRequestDTO())

                For Each ref As ItemReference In itemReferenceBatchCollection
                    Dim foundItemEntity As ItemResourceEntity = itemResourceEntities.GetFirstMatch(ItemResourceFields.Name = ref.ResourceIdentifier)
                    If foundItemEntity IsNot Nothing Then
                        ref.Title = foundItemEntity.Title
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub ItemInGroupGrid_LoadingRow(sender As Object, e As RowLoadEventArgs) Handles ItemInGroupGrid.LoadingRow
        If TypeOf e.Row.DataRow Is ItemReference Then
            e.Row.Cells("EntryType").Value = My.Resources.ItemRef
        ElseIf TypeOf e.Row.DataRow Is GroupReference Then
            e.Row.Cells("EntryType").Value = My.Resources.GroupRef
        ElseIf TypeOf e.Row.DataRow Is ItemGroup Then
            e.Row.Cells("EntryType").Value = My.Resources.Group
        Else
            e.Row.Cells("EntryType").Value = My.Resources.Unknown
        End If
        If e.Row.RowType = RowType.Record Then
            With e.Row.Cells("RowIndex")
                .Value = e.Row.AbsolutePosition
                .Column.AutoSize()
            End With
        End If
        e.Row.Expanded = True
    End Sub

    Private Sub StaticGroupEditor_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not DesignMode AndAlso _config IsNot Nothing Then
            With CommandManager1
                Select Case _config.GroupEntryType
                    Case GroupEntryTypes.Items
                        BindAddingItem(CommandManager1)
                        BindAddingItemsFromCodes(CommandManager1)
                        BindAddingGroup(CommandManager1)
                        EnableContextMenuItems(GroupEntryTypes.Items.ToString())
                        EnableContextMenuItems("AddGroups")

                    Case GroupEntryTypes.InclusiveGroups
                        BindAddingGroup(CommandManager1)
                        EnableContextMenuItems("AddGroups")

                    Case GroupEntryTypes.SeedingGroups
                        BindAddingItem(CommandManager1, True)
                        BindAddingItemsFromCodes(CommandManager1, True)
                        BindCreatingGroup(CommandManager1)
                        EnableContextMenuItems(GroupEntryTypes.Items.ToString())
                        EnableContextMenuItems("CreateGroups")

                End Select
                _moveUpCommand = New MoveUpCommand(_config, Me.ItemInGroupGrid)
                .Bind(_moveUpCommand, MoveUpToolStripMenuItem)
                Me.Commands.Add(_moveUpCommand)

                _moveDownCommand = New MoveDownCommand(_config, Me.ItemInGroupGrid)
                .Bind(_moveDownCommand, MoveDownToolStripMenuItem)
                Me.Commands.Add(_moveDownCommand)

                _removeCommand = New RemoveCommand(_config, Me.ItemInGroupGrid, AddressOf OnDependentResourceRemoved)
                .Bind(_removeCommand, RemoveToolStripMenuItem)
                Me.Commands.Add(_removeCommand)
            End With
        End If
    End Sub

    Private Sub EnableContextMenuItems(tag As String)
        For Each itm As ToolStripItem In ContextMenuStrip1.Items
            If itm.Tag IsNot Nothing AndAlso itm.Tag.ToString().Equals(tag, StringComparison.InvariantCultureIgnoreCase) Then itm.Visible = True
        Next
    End Sub

    Private Sub BindAddingItem(cm As CommandManager)
        BindAddingItem(cm, False)
    End Sub

    Private Sub BindAddingItem(cm As CommandManager, canExecuteCheck As Boolean)
        _addItemCommand = New AddItemCommand(_settings.Behaviour,
                                                         _config, Me.ItemInGroupGrid,
                                                         _resourceManager, AddressOf OnDependentResourceAdded)

        If canExecuteCheck Then
            Dim canAddParameter = New DelegateParameterBinding(Function() CanAddItem())
            cm.Bind(_addItemCommand, canAddParameter, AddItemToolStripMenuItem)
            If Not Me.Parameters.ContainsKey(_addItemCommand.Name) Then
                Me.Parameters.Add(_addItemCommand.Name, canAddParameter)
            End If
        Else
            cm.Bind(_addItemCommand, AddItemToolStripMenuItem)
        End If

        Me.Commands.Add(_addItemCommand)
    End Sub

    Private Sub BindAddingItemsFromCodes(cm As CommandManager)
        BindAddingItemsFromCodes(cm, False)
    End Sub

    Private Sub BindAddingItemsFromCodes(cm As CommandManager, canExecuteCheck As Boolean)
        _addItemsFromCodesCommand = New AddItemsFromCodesCommand(_settings.Behaviour,
                                                         _config, Me.ItemInGroupGrid,
                                                         _resourceManager, AddressOf OnDependentResourceAdded)
        If canExecuteCheck Then
            Dim canAddParameter = New DelegateParameterBinding(Function() CanAddItem())
            cm.Bind(_addItemsFromCodesCommand, canAddParameter, AddItemsFromCodesToolStripMenuItem)
            If Not Me.Parameters.ContainsKey(_addItemsFromCodesCommand.Name) Then
                Me.Parameters.Add(_addItemsFromCodesCommand.Name, canAddParameter)
            End If
        Else
            cm.Bind(_addItemsFromCodesCommand, AddItemsFromCodesToolStripMenuItem)
        End If

        Me.Commands.Add(_addItemsFromCodesCommand)
    End Sub

    Private Sub BindAddingGroup(cm As CommandManager)
        _addGroupCommand = New AddgroupCommand(_settings.Identifier, _config, _resourceManager, AddressOf OnDependentResourceAdded)
        cm.Bind(_addGroupCommand, AddToolStripMenuItem)
        Me.Commands.Add(_addGroupCommand)
    End Sub

    Private Sub BindCreatingGroup(cm As CommandManager)
        _createGroupCommand = New CreateItemGroupCommand(_config)
        cm.Bind(_createGroupCommand, CreateGroupToolStripMenuItem)
        Me.Commands.Add(_createGroupCommand)
    End Sub

    Private Function CanAddItem() As Boolean
        Return Not _ItemInGroupGrid.SelectedItems.Count = 0
    End Function

    Private Sub ItemInGroupGrid_DragDrop(sender As Object, e As DragEventArgs) Handles ItemInGroupGrid.DragDrop
        If Not _config.GroupEntryType = GroupEntryTypes.SeedingGroups Then
            If e.Data.GetDataPresent(GetType(GridEXSelectedItemCollection)) Then
                Dim selectedItemColl = DirectCast(e.Data.GetData(GetType(GridEXSelectedItemCollection)), GridEXSelectedItemCollection)
                selectedItemColl.Sort()
                Dim index As Integer = GetPositionOfDroppedRow()
                For i = 0 To selectedItemColl.Count - 1
                    Dim entity = DirectCast(selectedItemColl.Item(i).GetRow.DataRow, ItemResourceEntity)
                    Dim identifierOfItemToAdd As String = entity.Name

                    If _config.GroupDefinition.GetEntryByIdentifier(identifierOfItemToAdd) Is Nothing Then
                        Dim newItemReference As New ItemReference(identifierOfItemToAdd)
                        newItemReference.Title = entity.Title
                        _config.GroupDefinition.Insert(index, newItemReference)

                        OnDependentResourceAdded(New ResourceNameEventArgs(newItemReference.ResourceIdentifier))
                    End If
                Next
                AddSelections(index, selectedItemColl.Count)
            End If
        End If
    End Sub

    Private Sub ItemInGroupGrid_DragOver(sender As Object, e As DragEventArgs) Handles ItemInGroupGrid.DragOver
        If Not _config.GroupEntryType = GroupEntryTypes.SeedingGroups Then
            If e.Data.GetDataPresent(GetType(GridEXSelectedItemCollection)) Then
                Dim rowDrop As Integer = ItemInGroupGrid.RowPositionFromPoint()
                If rowDrop >= 0 Then
                    Dim rowDragOver As GridEXRow = ItemInGroupGrid.GetRow(rowDrop)
                    If TypeOf rowDragOver.DataRow Is ItemReference Then
                        e.Effect = DragDropEffects.Move
                    Else
                        e.Effect = DragDropEffects.None
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub AddSelections(currentRow As Integer, selectedRowCount As Integer)
        ItemInGroupGrid.SelectedItems.Clear()
        ItemInGroupGrid.Row = currentRow
        For i = 0 To selectedRowCount - 1
            ItemInGroupGrid.SelectedItems.Add(currentRow + i)
        Next
    End Sub

    Private Function GetPositionOfDroppedRow() As Integer
        Dim mousePosition As Point = GridEX.MousePosition
        Dim translatedPoint As Point = ItemInGroupGrid.PointToClient(mousePosition)
        Dim indexOfDroppedRow As Integer = ItemInGroupGrid.RowPositionFromPoint(translatedPoint.X, translatedPoint.Y)

        Return indexOfDroppedRow
    End Function


    Public Sub New()

        InitializeComponent()


    End Sub
End Class