
Imports Questify.Builder.Security
Imports System.ComponentModel
Imports Questify.Builder.Logic.Service.Model.Entities

Public Class ActionCommandSupportBaseControl
    Implements IActionCommands

#Region " Implementation for IActionCommands "

    ''' <summary>
    ''' Initializes the actioncommands.
    ''' </summary>
    Public Sub InitializeActionCommands(ByVal bankId As Integer) Implements IActionCommands.InitializeActionCommands
        ActionCommand.Instance.SetNewContext(DirectCast(Me, IActionCommands), bankId)
    End Sub

    ''' <summary>
    ''' Gets a value indicating whether the current user is allowed to "Add new" instances within the current context.
    ''' </summary>
    ''' <returns><c>true</c> if [allow add new]; otherwise, <c>false</c>.</returns>
    Public Overridable Function AddNewIsPermitted(ByVal bankId As Integer) As Boolean Implements IActionCommands.AddNewIsPermitted
        Return False
    End Function

    ''' <summary>
    ''' Gets a value indicating whether "add new" is applicable.
    ''' </summary>
    ''' <value><c>true</c> if "add new" is applicable; otherwise, <c>false</c>.</value>
    Public Overridable ReadOnly Property AllowAddNew() As Boolean Implements IActionCommands.AllowAddNew
        Get
            Return False
        End Get
    End Property

    ''' <summary>
    ''' Gets a value indicating whether the current user is allowed to delete within the current context.
    ''' </summary>
    ''' <returns><c>true</c> if allowed to delete; otherwise, <c>false</c>.</returns>
    Public Overridable Function DeleteIsPermitted(ByVal bankId As Integer) As Boolean Implements IActionCommands.DeleteIsPermitted
        Return False
    End Function

    ''' <summary>
    ''' Clears the is permitted.
    ''' </summary>
    ''' <param name="bankId">The bank id.</param>
    Public Overridable Function ClearIsPermitted(ByVal bankId As Integer) As Boolean Implements IActionCommands.ClearIsPermitted
        Return False
    End Function

    ''' <summary>
    ''' Gets a value indicating whether "delete" is applicable.
    ''' </summary>
    ''' <value><c>true</c> if [delete is applicable]; otherwise, <c>false</c>.</value>
    Public Overridable ReadOnly Property AllowDelete() As Boolean Implements IActionCommands.AllowDelete
        Get
            Return False
        End Get
    End Property

    ''' <summary>
    ''' Gets a value indicating whether the current user is allowed to edit within the current context.
    ''' </summary>
    ''' <returns><c>true</c> if allowed to edit; otherwise, <c>false</c>.</returns>
    Public Overridable Function EditIsPermitted(ByVal bankId As Integer) As Boolean Implements IActionCommands.EditIsPermitted
        Return False
    End Function

    ''' <summary>
    ''' Gets a value indicating whether "edit" is applicable.
    ''' </summary>
    ''' <value><c>true</c> if [edit is applicable]; otherwise, <c>false</c>.</value>
    Public Overridable ReadOnly Property AllowEdit() As Boolean Implements IActionCommands.AllowEdit
        Get
            Return False
        End Get
    End Property

    ''' <summary>
    ''' Executes the is permitted.
    ''' </summary>
    ''' <param name="bankId">The bank id.</param><returns></returns>
    Public Overridable Function ExecuteIsPermitted(ByVal bankId As Integer) As Boolean Implements IActionCommands.ExecuteIsPermitted
        Return False
    End Function

    ''' <summary>
    ''' Allows the context menu cell.
    ''' </summary>
    ''' <param name="parentFormName">Name of the parent form.</param><returns></returns>
    Public Overridable Function AllowContextMenuCell(ByVal parentFormName As String) As Boolean Implements IActionCommands.AllowContextMenuCell
        Return False
    End Function

    ''' <summary>
    ''' Allows the context menu header.
    ''' </summary>
    ''' <param name="parentFormName">Name of the parent form.</param><returns></returns>
    Public Overridable Function AllowContextMenuHeader(ByVal parentFormName As String) As Boolean Implements IActionCommands.AllowContextMenuHeader
        Return False
    End Function

    ''' <summary>
    ''' Is executing instances applicable within the current context.
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [allow execute]; otherwise, <c>false</c>.
    ''' </value>
    Public Overridable ReadOnly Property AllowExecute() As Boolean Implements IActionCommands.AllowExecute
        Get
            Return False
        End Get
    End Property


    ''' <summary>
    ''' Gets a value indicating whether [allow reports].
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [allow reports]; otherwise, <c>false</c>.
    ''' </value>
    Public Overridable ReadOnly Property AllowReports() As Boolean Implements IActionCommands.AllowReports
        Get
            Return False
        End Get
    End Property

    ''' <summary>
    ''' Gets a value indicating whether the current user is allowed to synchronize within the current context.
    ''' </summary>
    ''' <returns><c>true</c> if allowed to synchronize; otherwise, <c>false</c>.</returns>
    Public Overridable Function SynchronizeIsPermitted(ByVal bankId As Integer) As Boolean Implements IActionCommands.SynchronizeIsPermitted
        Return False
    End Function

    ''' <summary>
    ''' Gets a value indicating whether the current user is allowed to multi-edit within the current context.
    ''' </summary>
    ''' <returns><c>true</c> if allowed to multi-edit; otherwise, <c>false</c>.</returns>
    Public Overridable Function MultiEditIsPermitted(ByVal bankId As Integer) As Boolean Implements IActionCommands.MultiEditIsPermitted
        Return False
    End Function

    Public Overridable Function ExportIsPermitted(ByVal bankId As Integer) As Boolean Implements IActionCommands.ExportIsPermitted
        Return False
    End Function

    Public Overridable Function ShowReportIsPermitted(ByVal bankId As Integer) As Boolean Implements IActionCommands.ShowReportIsPermitted
        Return False
    End Function

    ''' <summary>
    ''' Gets a value indicating whether "synchronization" is applicable.
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if [synchronize is applicable]; otherwise, <c>false</c>.
    ''' </value>
    Public Overridable ReadOnly Property AllowSynchronize() As Boolean Implements IActionCommands.AllowSynchronize
        Get
            Return False
        End Get
    End Property

    ''' <summary>
    ''' Gets a value indicating whether the current user is allowed to publish within the current context.
    ''' </summary>
    ''' <returns><c>true</c> if allowed to publish; otherwise, <c>false</c>.</returns>
    Public Overridable Function PublishIsPermitted(ByVal bankId As Integer) As Boolean Implements IActionCommands.PublishIsPermitted
        Return False
    End Function

    ''' <summary>
    ''' Gets a value indicating whether "publishing" is applicable.
    ''' </summary>
    ''' <value><c>true</c> if "publishing" is applicable; otherwise, <c>false</c>.</value>
    Public Overridable ReadOnly Property AllowPublish() As Boolean Implements IActionCommands.AllowPublish
        Get
            Return False
        End Get
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether showing the property is alowed.
    ''' </summary>
    ''' <returns><c>true</c> if [allow show property]; otherwise, <c>false</c>.</returns>
    Public Overridable Function ShowPropertiesIsPermitted(ByVal bankId As Integer) As Boolean Implements IActionCommands.ShowPropertiesIsPermitted
        Return False
    End Function


    ''' <summary>
    ''' Gets a value indicating whether "show properties" is applicable.
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if "show properties" is applicable; otherwise, <c>false</c>.
    ''' </value>
    Public Overridable ReadOnly Property AllowShowProperties() As Boolean Implements IActionCommands.AllowShowProperties
        Get
            Return False
        End Get
    End Property

    ''' <summary>
    ''' Gets a value indicating whether [select all].
    ''' </summary>
    ''' <value><c>true</c> if [select all]; otherwise, <c>false</c>.</value>
    Public Overridable ReadOnly Property AllowSelectAll() As Boolean Implements IActionCommands.AllowSelectAll
        Get
            Return False
        End Get
    End Property

    ''' <summary>
    ''' Synchronizes the is permitted.
    ''' </summary>
    ''' <param name="bankId">The bank id.</param>
    Public Overridable Function ExportToExcelIsPermitted(ByVal bankId As Integer) As Boolean Implements IActionCommands.ExportToExcelIsPermitted
        Return False
    End Function

    ''' <summary>
    ''' Gets a value indicating whether [allow preview].
    ''' </summary>
    ''' <returns><c>true</c> if [allow preview]; otherwise, <c>false</c>.</returns>
    Public Overridable Function PreviewIsPermitted(ByVal bankId As Integer) As Boolean Implements IActionCommands.PreviewIsPermitted
        Return False
    End Function

    ''' <summary>
    ''' Gets a value indicating whether "preview" is applicable.
    ''' </summary>
    ''' <value><c>true</c> if [preview is applicable]; otherwise, <c>false</c>.</value>
    Public Overridable ReadOnly Property AllowPreview() As Boolean Implements IActionCommands.AllowPreview
        Get
            Return False
        End Get
    End Property

    ''' <summary>
    ''' Gets a value indicating whether synchronization of dependant items (items in the Cito meaning of the word) is allowed.
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if synchronizing dependant items and "resource on which the items depent" is allowed otherwise, <c>false</c>.
    ''' </value>
    Public Overridable ReadOnly Property AllowHarmonizeDependantItems() As Boolean Implements IActionCommands.AllowHarmonizeDependantItems
        Get
            Return False
        End Get
    End Property

    ''' <summary>
    ''' Gets a value indicating whether the current user is permitted to synchronize dependant items (items in the Cito meaning of the word).
    ''' </summary>
    ''' <param name="bankid">The bankid.</param>
    Public Overridable Function HarmonizeDependantItemsIsPermitted(ByVal bankid As Integer) As Boolean Implements IActionCommands.HarmonizeDependantItemsIsPermitted
        Return AllowHarmonizeDependantItems()
    End Function

    Public Overridable ReadOnly Property AllowFastSearch() As Boolean Implements IActionCommands.AllowFastSearch
        Get
            Return False
        End Get
    End Property

    Public Overridable ReadOnly Property AllowMoveResources() As Boolean Implements IActionCommands.AllowMoveResources
        Get
            Return False
        End Get
    End Property

    ''' <summary>
    ''' Gets a value indicating whether moving of resources is allowed.
    ''' </summary>
    Public Overridable Function MoveResourcesIsPermitted(ByVal bankid As Integer) As Boolean Implements IActionCommands.MoveResourceIsPermitted
        ' The "named task" ClearBank is (ab)used to find out if the current user is permitted to move resources.
        'TODO: Add "named task" MoveResources so permissions can be assigned and checked.
        Return PermissionFactory.Instance.TryUserIsPermittedToNamedTask(TestBuilderPermissionAccess.Execute, TestBuilderPermissionTarget.NamedTask, TestBuilderPermissionNamedTask.MoveResourcesOrCustomBankProperties, bankid, 0)
    End Function

    ''' <summary>
    ''' Gets a value indicating whether toggling resource visibility is allowed.
    ''' </summary>
    Public Overridable ReadOnly Property AllowToggleResourceVisibility As Boolean Implements IActionCommands.AllowToggleResourceVisibility
        Get
            Return False
        End Get
    End Property

    ''' <summary>
    ''' Gets a value indicating whether the current user is permitted to toggle the visibility of the resource.
    ''' </summary>
    ''' <param name="bankId">The bank id.</param>
    Public Overridable Function ToggleResourceVisibilityIsPermitted(ByVal bankId As Integer) As Boolean Implements IActionCommands.ToggleResourceVisibilityIsPermitted
        Return False
    End Function

    ''' <summary>
    ''' Toggles the visibility of the selected resources for the given bank.
    ''' </summary>
    ''' <param name="bankId">The id of the bank to toggle the visibility of the resources for.</param>
    ''' <returns>True if the visibility if the resources is succesfully toggled.</returns>
    Public Overridable Function ToggleResourceVisibility(ByVal bankId As Integer) As Boolean Implements IActionCommands.ToggleResourceVisibility
        Return False
    End Function


    Public Overridable Sub Delete() Implements IActionCommands.Delete

    End Sub

    ''' <summary>
    ''' Toggles the fast search bar.
    ''' </summary>
    Public Overridable Sub ToggleFastSearchBar() Implements IActionCommands.ToggleFastSearchBar
        ' implementation of this function is only done is the inherited grids.
        Throw New NotSupportedException()
    End Sub

    ''' <summary>
    ''' Synchronizes this instance.
    ''' </summary>
    Public Overridable Sub Synchronize() Implements IActionCommands.Synchronize

    End Sub


    ''' <summary>
    ''' Synchronizes the items that are dependant on the currently selected resources. Only known use so far is synchronizing items and item layout templates.
    ''' </summary>
    Public Overridable Sub Harmonize() Implements IActionCommands.HarmonizeDependantItems

    End Sub

    Public Overridable Sub MoveTheResources() Implements IActionCommands.MoveResources

    End Sub

    Public Overridable Sub SelectAllRows() Implements IActionCommands.SelectAll

    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters")> _
    Public Overridable Sub AddNew() Implements IActionCommands.AddNew
        Throw New NotSupportedException(My.Resources.Not_Allowed)
    End Sub

    Public Overridable Sub Edit() Implements IActionCommands.Edit
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters")> _
    Public Overridable Sub ExportResources() Implements IActionCommands.Export
    End Sub

    Public Overridable Sub ShowReportWizard() Implements IActionCommands.ShowReport
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters")> _
    Public Overridable Sub Publish() Implements IActionCommands.Publish
        Throw New NotSupportedException(My.Resources.Not_Allowed)
    End Sub

    Public Overridable Sub ShowProperties() Implements IActionCommands.ShowProperties
    End Sub

    Public Overridable Sub Preview(ByVal entity As ResourceDto) Implements IActionCommands.Preview
        Throw New NotSupportedException(My.Resources.Not_Allowed)
    End Sub


    ''' <summary>
    ''' This event will be raised when the type of components has changed. e.a. the context is changed
    ''' </summary>
    ''' <remarks></remarks>
    <Description("This event will be raised when the type of components has changed. e.a. the context is changed"), Category("GridBase events")> _
    Public Event ContextChanged As EventHandler(Of EventArgs) Implements IActionCommands.ContextChanged

    Public Overridable Sub ExportToExcel() Implements IActionCommands.ExportToExcel

    End Sub

    Protected Sub RaiseContextChanged(sender As Object, e As EventArgs)
        RaiseEvent ContextChanged(sender, e)
    End Sub

#End Region


End Class
