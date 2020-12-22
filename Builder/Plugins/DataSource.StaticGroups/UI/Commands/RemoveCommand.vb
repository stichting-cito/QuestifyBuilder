Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports Questify.Builder.Logic.ContentModel
Imports Janus.Windows.GridEX
Imports Questify.Builder.Plugins.DataSource.StaticGroups.Entities
Imports Questify.Builder.UI.Commanding
Imports Questify.Builder.Logic

Public Class RemoveCommand
    Inherits CommandBase


    Private ReadOnly _config As StaticGroupDataSourceConfig
    Private ReadOnly _itemInGroupGrid As GridEX
    Private ReadOnly _onDependentResourceRemovedAction As Action(Of ResourceNameEventArgs)




    Public Sub New(config As StaticGroupDataSourceConfig, itemInGroupGrid As GridEX, onDependentResourceRemovedAction As Action(Of ResourceNameEventArgs))
        If config Is Nothing Then
            Throw New ArgumentNullException("config")
        End If

        If itemInGroupGrid Is Nothing Then
            Throw New ArgumentNullException("itemInGroupGrid")
        End If
        If onDependentResourceRemovedAction Is Nothing Then
            Throw New ArgumentNullException("onDependentResourceRemovedAction")
        End If

        Me._config = config
        Me._onDependentResourceRemovedAction = onDependentResourceRemovedAction
        Me._itemInGroupGrid = itemInGroupGrid
    End Sub



    Public Overrides ReadOnly Property Image As Image
        Get
            Return My.Resources.DeleteItemToolStripButton_Image
        End Get
    End Property

    Public Overrides ReadOnly Property Name As String
        Get
            Return "RemoveCommand"
        End Get
    End Property

    Public Overrides ReadOnly Property NameLocalized As String
        Get
            Return My.Resources.RemoveCommand_Name
        End Get
    End Property



    Public Overrides Sub Execute(parameter As Object)
        If _itemInGroupGrid IsNot Nothing AndAlso _itemInGroupGrid.SelectedItems IsNot Nothing AndAlso Not _itemInGroupGrid.SelectedItems.Count = 0 Then
            Dim message = My.Resources.AreYouSure
            If _itemInGroupGrid.SelectedItems.Cast(Of GridEXSelectedItem).Any(Function(r) r IsNot Nothing AndAlso TypeOf r.GetRow.DataRow Is ItemGroup) Then
                message = My.Resources.AreYouSureGoup
            End If
            Dim shouldRenumber = False
            If MessageBox.Show(message, My.Resources.StaticGroup, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Dim itemsToRemove As New List(Of StaticGroupEntry)
                For Each selectedItem As GridEXSelectedItem In _itemInGroupGrid.SelectedItems
                    Dim itemToRemove = DirectCast(selectedItem.GetRow().DataRow, StaticGroupEntry)
                    itemsToRemove.Add(itemToRemove)
                    Dim staticGroupEntry = TryCast(itemToRemove, ItemGroup)
                    shouldRenumber = staticGroupEntry IsNot Nothing
                    If (staticGroupEntry IsNot Nothing AndAlso staticGroupEntry.Items IsNot Nothing) Then
                        For Each subItemToRemove In staticGroupEntry.Items
                            If Not itemsToRemove.Contains(subItemToRemove) Then
                                itemsToRemove.Add(subItemToRemove)
                            End If
                        Next
                    End If
                Next
                For Each itemToRemove In itemsToRemove
                    If _config.GroupDefinition.Any(Function(sg) sg.ResourceIdentifier = itemToRemove.ResourceIdentifier) Then
                        _config.GroupDefinition.Remove(itemToRemove)
                    End If
                    Dim removedSubItem = TryCast(itemToRemove, ItemReference)
                    If removedSubItem IsNot Nothing Then
                        For Each sq In From sq1 In _config.GroupDefinition.OfType(Of ItemGroup) Where sq1.Items.Any(Function(sg) sg.ResourceIdentifier = itemToRemove.ResourceIdentifier)
                            sq.Items.Remove(removedSubItem)
                        Next
                    End If
                    _onDependentResourceRemovedAction(New ResourceNameEventArgs(itemToRemove.ResourceIdentifier))
                Next
                If shouldRenumber Then
                    Dim index = 0
                    For Each createdGroup In _config.GroupDefinition.OfType(Of ItemGroup)
                        createdGroup.ResourceIdentifier = String.Concat(My.Resources.Group, String.Format("_{0}", (index + 1).ToString))
                        createdGroup.Title = String.Format("{0} {1}", My.Resources.Group, createdGroup.ResourceIdentifier.ExtractNumber)
                        index += 1
                    Next
                End If
                _itemInGroupGrid.Refetch()
            End If
        End If
    End Sub


    Protected Overrides Function GetCanExecuteState(parameter As Object) As Boolean
        Return _itemInGroupGrid.SelectedItems.Count > 0 AndAlso TypeOf _itemInGroupGrid.GetRow().DataRow Is StaticGroupEntry
    End Function


End Class