
Imports Questify.Builder.Logic.Service.Model.Entities

Public Interface IActionCommands

    Event ContextChanged As EventHandler(Of EventArgs)

    ReadOnly Property AllowAddNew() As Boolean

    Function AddNewIsPermitted(ByVal bankId As Integer) As Boolean

    ReadOnly Property AllowDelete() As Boolean

    Function DeleteIsPermitted(ByVal bankId As Integer) As Boolean

    Function ClearIsPermitted(ByVal bankId As Integer) As Boolean

    ReadOnly Property AllowEdit() As Boolean

    Function EditIsPermitted(ByVal bankId As Integer) As Boolean

    Function MultiEditIsPermitted(ByVal bankId As Integer) As Boolean

    ReadOnly Property AllowExecute() As Boolean

    ReadOnly Property AllowReports() As Boolean

    Function AllowContextMenuCell(ByVal parentFormName As String) As Boolean

    Function AllowContextMenuHeader(ByVal parentFormName As String) As Boolean

    Function ExecuteIsPermitted(ByVal bankId As Integer) As Boolean

    ReadOnly Property AllowShowProperties() As Boolean

    Function ShowPropertiesIsPermitted(ByVal bankId As Integer) As Boolean

    ReadOnly Property AllowSynchronize() As Boolean


    ReadOnly Property AllowSelectAll() As Boolean

    Function SynchronizeIsPermitted(ByVal bankId As Integer) As Boolean

    ReadOnly Property AllowPublish() As Boolean

    Function PublishIsPermitted(ByVal bankId As Integer) As Boolean

    ReadOnly Property AllowFastSearch() As Boolean

    Function ExportToExcelIsPermitted(ByVal bankId As Integer) As Boolean

    Function ExportIsPermitted(ByVal bankId As Integer) As Boolean

    Function ShowReportIsPermitted(ByVal bankId As Integer) As Boolean

    ReadOnly Property AllowPreview() As Boolean

    Function PreviewIsPermitted(ByVal bankId As Integer) As Boolean

    ReadOnly Property AllowHarmonizeDependantItems() As Boolean

    Function HarmonizeDependantItemsIsPermitted(ByVal bankid As Integer) As Boolean

    ReadOnly Property AllowMoveResources() As Boolean

    Function MoveResourceIsPermitted(ByVal bankid As Integer) As Boolean

    ReadOnly Property AllowToggleResourceVisibility As Boolean

    Function ToggleResourceVisibilityIsPermitted(ByVal bankId As Integer) As Boolean

    Function ToggleResourceVisibility(ByVal bankId As Integer) As Boolean

    Sub AddNew()

    Sub Delete()

    Sub Edit()

    Sub Synchronize()

    Sub SelectAll()

    Sub Export()

    Sub Publish()

    Sub ExportToExcel()

    Sub InitializeActionCommands(ByVal bankId As Integer)

    Sub ShowProperties()

    Sub Preview(ByVal entity As ResourceDto)

    Sub ShowReport()
    Sub ToggleFastSearchBar()

    Sub HarmonizeDependantItems()

    Sub MoveResources()
End Interface
