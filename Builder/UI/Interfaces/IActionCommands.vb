
Imports Questify.Builder.Logic.Service.Model.Entities

''' <summary>
''' Interface for defining toolbar commands in the mainform
''' </summary>
Public Interface IActionCommands

    ''' <summary>
    ''' Event fired when the context in the main form is changed.
    ''' </summary>
    Event ContextChanged As EventHandler(Of EventArgs)

    ''' <summary>
    ''' Is adding new instances applicable within the current context.
    ''' </summary>
    ''' <value><c>true</c> if add new is applicable; otherwise, <c>false</c>.</value>
    ReadOnly Property AllowAddNew() As Boolean

    ''' <summary>
    ''' Is the current user allowed to "Add new" instances within the current context.
    ''' </summary>
    ''' <param name="bankId">The bank id.</param>
    Function AddNewIsPermitted(ByVal bankId As Integer) As Boolean

    ''' <summary>
    ''' Is deleting instances applicable within the current context.
    ''' </summary>
    ''' <value><c>true</c> if deleting is applicable; otherwise, <c>false</c>.</value>
    ReadOnly Property AllowDelete() As Boolean

    ''' <summary>
    ''' Is the current user allowed to delete instances within the current context.
    ''' </summary>
    Function DeleteIsPermitted(ByVal bankId As Integer) As Boolean

    ''' <summary>
    ''' Clears the is permitted.
    ''' </summary>
    ''' <param name="bankId">The bank id.</param>
    Function ClearIsPermitted(ByVal bankId As Integer) As Boolean

    ''' <summary>
    ''' Is editing instances applicable within the current context.
    ''' </summary>
    ''' <value><c>true</c> if edit is applicable; otherwise, <c>false</c>.</value>
    ReadOnly Property AllowEdit() As Boolean

    ''' <summary>
    ''' Is the current user allowed to edit instances within the current context.
    ''' </summary>
    ''' <returns><c>true</c> if allowed to edit; otherwise, <c>false</c>.</returns>
    Function EditIsPermitted(ByVal bankId As Integer) As Boolean

    ''' <summary>
    ''' Is the current user allowed to multi edit instances within the current context.
    ''' </summary>
    Function MultiEditIsPermitted(ByVal bankId As Integer) As Boolean

    ''' <summary>
    ''' Is executing instances applicable within the current context.
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [allow execute]; otherwise, <c>false</c>.
    ''' </value>
    ReadOnly Property AllowExecute() As Boolean

    ''' <summary>
    ''' Gets a value indicating whether [allow reports].
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [allow reports]; otherwise, <c>false</c>.
    ''' </value>
    ReadOnly Property AllowReports() As Boolean

    ''' <summary>
    ''' Allows the context menu cell.
    ''' </summary>
    ''' <param name="parentFormName">Name of the parent form.</param><returns></returns>
    Function AllowContextMenuCell(ByVal parentFormName As String) As Boolean

    ''' <summary>
    ''' Allows the context menu header.
    ''' </summary>
    ''' <param name="parentFormName">Name of the parent form.</param><returns></returns>
    Function AllowContextMenuHeader(ByVal parentFormName As String) As Boolean

    ''' <summary>
    ''' Executes the is permitted.
    ''' </summary>
    ''' <param name="bankId">The bank id.</param><returns></returns>
    Function ExecuteIsPermitted(ByVal bankId As Integer) As Boolean

    ''' <summary>
    ''' Is viewing properties applicable within the current context.
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if [show properties is applicable]; otherwise, <c>false</c>.
    ''' </value>
    ReadOnly Property AllowShowProperties() As Boolean

    ''' <summary>
    ''' Is the current user allowed to view instance properties within the current context.
    ''' </summary>
    ''' <returns><c>true</c> if [allow show property]; otherwise, <c>false</c>.</returns>
    Function ShowPropertiesIsPermitted(ByVal bankId As Integer) As Boolean

    ''' <summary>
    ''' Is synchronisation applicable within the current context.
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if synchronization is applicable; otherwise, <c>false</c>.
    ''' </value>
    ReadOnly Property AllowSynchronize() As Boolean


    ''' <summary>
    ''' Gets a value indicating whether [select all].
    ''' </summary>
    ''' <value><c>true</c> if [select all]; otherwise, <c>false</c>.</value>
    ReadOnly Property AllowSelectAll() As Boolean

    ''' <summary>
    ''' Is the current user allowed to synchronize within the current context.
    ''' </summary>
    Function SynchronizeIsPermitted(ByVal bankId As Integer) As Boolean

    ''' <summary>
    ''' Is publishing applicable within the current context.
    ''' </summary>
    ''' <value><c>true</c> if publishing is applicable; otherwise, <c>false</c>.</value>
    ReadOnly Property AllowPublish() As Boolean

    ''' <summary>
    ''' Is the current user allowed to publish within the current context.
    ''' </summary>
    Function PublishIsPermitted(ByVal bankId As Integer) As Boolean

    ''' <summary>
    ''' Gets a value indicating whether fast search is allowed.
    ''' </summary>
    ''' <value><c>true</c> if [allow fast search]; otherwise, <c>false</c>.</value>
    ReadOnly Property AllowFastSearch() As Boolean

    ''' <summary>
    ''' Synchronizes the is permitted.
    ''' </summary>
    ''' <param name="bankId">The bank id.</param>
    Function ExportToExcelIsPermitted(ByVal bankId As Integer) As Boolean

    ''' <summary>
    ''' Exports the is permitted.
    ''' </summary>
    ''' <param name="bankId">The bank id.</param>
    Function ExportIsPermitted(ByVal bankId As Integer) As Boolean

    Function ShowReportIsPermitted(ByVal bankId As Integer) As Boolean

    ''' <summary>
    ''' Is previewing applicable in the current context.
    ''' </summary>
    ''' <value><c>true</c> if [preview is applicable]; otherwise, <c>false</c>.</value>
    ReadOnly Property AllowPreview() As Boolean

    ''' <summary>
    ''' Is the current user allowed to preview within the current context.
    ''' </summary>
    ''' <returns><c>true</c> if [allow preview]; otherwise, <c>false</c>.</returns>
    Function PreviewIsPermitted(ByVal bankId As Integer) As Boolean

    ''' <summary>
    ''' Gets a value indicating whether synchronization of dependant items (items in the Cito meaning of the word) is allowed.
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if synchronizing dependant items and "resource on which the items depent" is allowed otherwise, <c>false</c>.
    ''' </value>
    ReadOnly Property AllowHarmonizeDependantItems() As Boolean

    ''' <summary>
    ''' Gets a value indicating whether the current user is permitted to synchronize dependant items (items in the Cito meaning of the word).
    ''' </summary>
    ''' <param name="bankid">The bankid.</param>
    Function HarmonizeDependantItemsIsPermitted(ByVal bankid As Integer) As Boolean

    ''' <summary>
    ''' Gets a value indicating whether moving of resources is allowed.
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property AllowMoveResources() As Boolean

    ''' <summary>
    ''' Gets a value indicating whether the current user is permitted to synchronize dependant items (items in the Cito meaning of the word).
    ''' </summary>
    ''' <param name="bankid">The bankid.</param>
    ''' <returns></returns>
	Function MoveResourceIsPermitted(ByVal bankid As Integer) As Boolean

	''' <summary>
	''' Gets a value indicating whether toggling resource visibility is allowed.
	''' </summary>
	ReadOnly Property AllowToggleResourceVisibility As Boolean

	''' <summary>
	''' Gets a value indicating whether the current user is permitted to toggle the visibility of the resource.
	''' </summary>
	''' <param name="bankId">The bank id.</param>
	Function ToggleResourceVisibilityIsPermitted(ByVal bankId As Integer) As Boolean

	''' <summary>
	''' Toggles the visibility of the selected resources for the given bank.
	''' </summary>
	''' <param name="bankId">The id of the bank to toggle the visibility of the resources for.</param>
	''' <returns>True if the visibility if the resources is succesfully toggled.</returns>
	Function ToggleResourceVisibility(ByVal bankId As Integer) As Boolean

    ''' <summary>
    ''' 'Add New'-button is pushed on the toolbar.
    ''' </summary>
    ''' <remarks></remarks>
    Sub AddNew()

    ''' <summary>
    ''' 'Delete'-button is pushed on the toolbar.
    ''' </summary>
    Sub Delete()

    ''' <summary>
    ''' Edits this instance.
    ''' </summary>
    Sub Edit()

    ''' <summary>
    ''' 'Synchonize'-button is pushed on the toolbar.
    ''' </summary>
    Sub Synchronize()

    ''' <summary>
    ''' Selects all.
    ''' </summary>
    Sub SelectAll()

    ''' <summary>
    ''' Exports this instance.
    ''' </summary>
    Sub Export()

    ''' <summary>
    ''' 'Publish'-button is pushed on the toolbar.
    ''' </summary>
    Sub Publish()

    ''' <summary>
    ''' Exports to excel.
    ''' </summary>
    Sub ExportToExcel()

	''' <summary>
	''' Function this is called when the context is changed and the toolbar is initialized.
	''' </summary>
    Sub InitializeActionCommands(ByVal bankId As Integer)

    ''' <summary>
    ''' Shows the property.
    ''' </summary>
    Sub ShowProperties()

    ''' <summary>
    ''' Previews this instance.
    ''' </summary>
    Sub Preview(ByVal entity As ResourceDto)

    ''' <summary>
    ''' Shows the report.
    ''' </summary>
    Sub ShowReport()
    ''' <summary>
    ''' Toggles the fast search bar.
    ''' </summary>
    Sub ToggleFastSearchBar()

    Sub HarmonizeDependantItems()

    Sub MoveResources()
End Interface
