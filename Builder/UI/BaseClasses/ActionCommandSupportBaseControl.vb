
Imports Questify.Builder.Security
Imports System.ComponentModel
Imports Questify.Builder.Logic.Service.Model.Entities

Public Class ActionCommandSupportBaseControl
    Implements IActionCommands


    Public Sub InitializeActionCommands(ByVal bankId As Integer) Implements IActionCommands.InitializeActionCommands
        ActionCommand.Instance.SetNewContext(DirectCast(Me, IActionCommands), bankId)
    End Sub

    Public Overridable Function AddNewIsPermitted(ByVal bankId As Integer) As Boolean Implements IActionCommands.AddNewIsPermitted
        Return False
    End Function

    Public Overridable ReadOnly Property AllowAddNew() As Boolean Implements IActionCommands.AllowAddNew
        Get
            Return False
        End Get
    End Property

    Public Overridable Function DeleteIsPermitted(ByVal bankId As Integer) As Boolean Implements IActionCommands.DeleteIsPermitted
        Return False
    End Function

    Public Overridable Function ClearIsPermitted(ByVal bankId As Integer) As Boolean Implements IActionCommands.ClearIsPermitted
        Return False
    End Function

    Public Overridable ReadOnly Property AllowDelete() As Boolean Implements IActionCommands.AllowDelete
        Get
            Return False
        End Get
    End Property

    Public Overridable Function EditIsPermitted(ByVal bankId As Integer) As Boolean Implements IActionCommands.EditIsPermitted
        Return False
    End Function

    Public Overridable ReadOnly Property AllowEdit() As Boolean Implements IActionCommands.AllowEdit
        Get
            Return False
        End Get
    End Property

    Public Overridable Function ExecuteIsPermitted(ByVal bankId As Integer) As Boolean Implements IActionCommands.ExecuteIsPermitted
        Return False
    End Function

    Public Overridable Function AllowContextMenuCell(ByVal parentFormName As String) As Boolean Implements IActionCommands.AllowContextMenuCell
        Return False
    End Function

    Public Overridable Function AllowContextMenuHeader(ByVal parentFormName As String) As Boolean Implements IActionCommands.AllowContextMenuHeader
        Return False
    End Function

    Public Overridable ReadOnly Property AllowExecute() As Boolean Implements IActionCommands.AllowExecute
        Get
            Return False
        End Get
    End Property


    Public Overridable ReadOnly Property AllowReports() As Boolean Implements IActionCommands.AllowReports
        Get
            Return False
        End Get
    End Property

    Public Overridable Function SynchronizeIsPermitted(ByVal bankId As Integer) As Boolean Implements IActionCommands.SynchronizeIsPermitted
        Return False
    End Function

    Public Overridable Function MultiEditIsPermitted(ByVal bankId As Integer) As Boolean Implements IActionCommands.MultiEditIsPermitted
        Return False
    End Function

    Public Overridable Function ExportIsPermitted(ByVal bankId As Integer) As Boolean Implements IActionCommands.ExportIsPermitted
        Return False
    End Function

    Public Overridable Function ShowReportIsPermitted(ByVal bankId As Integer) As Boolean Implements IActionCommands.ShowReportIsPermitted
        Return False
    End Function

    Public Overridable ReadOnly Property AllowSynchronize() As Boolean Implements IActionCommands.AllowSynchronize
        Get
            Return False
        End Get
    End Property

    Public Overridable Function PublishIsPermitted(ByVal bankId As Integer) As Boolean Implements IActionCommands.PublishIsPermitted
        Return False
    End Function

    Public Overridable ReadOnly Property AllowPublish() As Boolean Implements IActionCommands.AllowPublish
        Get
            Return False
        End Get
    End Property

    Public Overridable Function ShowPropertiesIsPermitted(ByVal bankId As Integer) As Boolean Implements IActionCommands.ShowPropertiesIsPermitted
        Return False
    End Function


    Public Overridable ReadOnly Property AllowShowProperties() As Boolean Implements IActionCommands.AllowShowProperties
        Get
            Return False
        End Get
    End Property

    Public Overridable ReadOnly Property AllowSelectAll() As Boolean Implements IActionCommands.AllowSelectAll
        Get
            Return False
        End Get
    End Property

    Public Overridable Function ExportToExcelIsPermitted(ByVal bankId As Integer) As Boolean Implements IActionCommands.ExportToExcelIsPermitted
        Return False
    End Function

    Public Overridable Function PreviewIsPermitted(ByVal bankId As Integer) As Boolean Implements IActionCommands.PreviewIsPermitted
        Return False
    End Function

    Public Overridable ReadOnly Property AllowPreview() As Boolean Implements IActionCommands.AllowPreview
        Get
            Return False
        End Get
    End Property

    Public Overridable ReadOnly Property AllowHarmonizeDependantItems() As Boolean Implements IActionCommands.AllowHarmonizeDependantItems
        Get
            Return False
        End Get
    End Property

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

    Public Overridable Function MoveResourcesIsPermitted(ByVal bankid As Integer) As Boolean Implements IActionCommands.MoveResourceIsPermitted
        Return PermissionFactory.Instance.TryUserIsPermittedToNamedTask(TestBuilderPermissionAccess.Execute, TestBuilderPermissionTarget.NamedTask, TestBuilderPermissionNamedTask.MoveResourcesOrCustomBankProperties, bankid, 0)
    End Function

    Public Overridable ReadOnly Property AllowToggleResourceVisibility As Boolean Implements IActionCommands.AllowToggleResourceVisibility
        Get
            Return False
        End Get
    End Property

    Public Overridable Function ToggleResourceVisibilityIsPermitted(ByVal bankId As Integer) As Boolean Implements IActionCommands.ToggleResourceVisibilityIsPermitted
        Return False
    End Function

    Public Overridable Function ToggleResourceVisibility(ByVal bankId As Integer) As Boolean Implements IActionCommands.ToggleResourceVisibility
        Return False
    End Function


    Public Overridable Sub Delete() Implements IActionCommands.Delete

    End Sub

    Public Overridable Sub ToggleFastSearchBar() Implements IActionCommands.ToggleFastSearchBar
        Throw New NotSupportedException()
    End Sub

    Public Overridable Sub Synchronize() Implements IActionCommands.Synchronize

    End Sub


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


    <Description("This event will be raised when the type of components has changed. e.a. the context is changed"), Category("GridBase events")> _
    Public Event ContextChanged As EventHandler(Of EventArgs) Implements IActionCommands.ContextChanged

    Public Overridable Sub ExportToExcel() Implements IActionCommands.ExportToExcel

    End Sub

    Protected Sub RaiseContextChanged(sender As Object, e As EventArgs)
        RaiseEvent ContextChanged(sender, e)
    End Sub



End Class
