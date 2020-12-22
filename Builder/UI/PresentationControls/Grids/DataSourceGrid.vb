Imports Questify.Builder.Security

Imports Janus.Windows.GridEX
Imports Cito.Tester.ContentModel.Datasources
Imports Cito.Tester.Common

Public Class DataSourceGrid

    Public Overrides ReadOnly Property AllowMoveResources As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides Function ShowReportIsPermitted(ByVal bankId As Integer) As Boolean
        Return True
    End Function


    Public Overrides Sub AddNew()
        OnAddNewItem(New EventArgs)
    End Sub

    Public Overrides Function AddNewIsPermitted(ByVal bankId As Integer) As Boolean
        Return AllowAddNew AndAlso PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.AddNew, TestBuilderPermissionTarget.DataSourceEntity, bankId)
    End Function

    Public Overrides Function DeleteIsPermitted(ByVal bankId As Integer) As Boolean
        Return AllowDelete AndAlso PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Delete, TestBuilderPermissionTarget.DataSourceEntity, bankId)
    End Function

    Public Overrides Function EditIsPermitted(ByVal bankId As Integer) As Boolean
        Return AllowAddNew AndAlso PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Edit, TestBuilderPermissionTarget.DataSourceEntity, bankId)
    End Function

    Public Overrides Function ExportIsPermitted(ByVal bankId As Integer) As Boolean
        Return PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Export, TestBuilderPermissionTarget.DataSourceEntity, bankId)
    End Function

    Public Overrides Function ShowPropertiesIsPermitted(ByVal bankId As Integer) As Boolean
        Return AllowShowProperties AndAlso PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.ViewProperties, TestBuilderPermissionTarget.DataSourceEntity, bankId)
    End Function

    Public Overrides Function SynchronizeIsPermitted(ByVal bankId As Integer) As Boolean
        Return AllowSynchronize
    End Function


    Public Sub New()

        InitializeComponent()


    End Sub

    Private Sub GridControl_FormattingRow(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowLoadEventArgs) Handles GridControl.FormattingRow
        If e.Row.DataRow IsNot Nothing Then
            If e.Row.RowType = RowType.Record Then
                Dim dataSourceBehaviour As DataSourceBehaviourEnum
                Dim cell As GridEXCell = e.Row.Cells("DataSourceType")
                If Not String.IsNullOrEmpty(cell.Text) AndAlso [Enum].TryParse(Of DataSourceBehaviourEnum)(cell.Text, True, dataSourceBehaviour) Then
                    cell.Text = ResourceEnumConverter.ConvertToString(dataSourceBehaviour)
                End If
            End If
        End If
    End Sub

End Class