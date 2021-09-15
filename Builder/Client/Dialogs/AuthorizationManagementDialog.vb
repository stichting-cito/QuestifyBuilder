
Imports System.Diagnostics.CodeAnalysis
Imports System.Linq
Imports System.Threading
Imports Questify.Builder.Security
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Cito.Tester.Common
Imports Enums
Imports Janus.Windows.GridEX
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Security.ActiveDirectory.DsObjectPicker
Imports Questify.Builder.UI

Public Class AuthorizationManagementDialog

    Private _users As EntityCollection
    Private ReadOnly _removedUsers As New EntityCollection
    Private _removedUserApplicationRoleCollection As New EntityCollection
    Private _removedUserBankRoleCollection As New EntityCollection

    ''' <summary>
    ''' Initializes a new instance of the <see cref="AuthorizationManagementDialog" /> class.
    ''' </summary>
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ImportButton.Enabled = Questify.Builder.Security.ActiveDirectory.ActiveDirectoryHelper.IsComputerInDomain

        Dim currentUser = New UserEntity(CType(Thread.CurrentPrincipal.Identity, TestBuilderIdentity).UserId)

        DataBind()
    End Sub


    ''' <summary>
    ''' Builds the role permission data set based on the LLBLGen collections/entities.
    ''' This is done to make use of the native support that the Janus GridEx offers for datasets.
    ''' </summary>
    Private Function BuildRolePermissionDataSet() As DataSet
        Dim rolePermissionDS As New DataSet("RolePermissionDS")

        Dim roleTable As DataTable = rolePermissionDS.Tables.Add("Roles")

        roleTable.Columns.Add("Id", GetType(Integer))
        roleTable.Columns.Add("Name", GetType(String))

        Dim rolePermissions As DataTable = rolePermissionDS.Tables.Add("Permissions")

        Dim addedColumn As DataColumn

        rolePermissions.Columns.Add("RoleId", GetType(Integer))
        rolePermissions.Columns.Add("PermissionTargetId", GetType(Integer))
        addedColumn = rolePermissions.Columns.Add("PermissionTargetName", GetType(String))
        addedColumn.Caption = "Permissie onderwerp"

        For Each enumValue As TestBuilderPermissionAccess In [Enum].GetValues(GetType(TestBuilderPermissionAccess))
            'Skip the low-level Data Access Layer permissions and the AnyTask permission because those
            'permissions are never explicitly granted.
            Dim DalAll As TestBuilderPermissionAccess = (TestBuilderPermissionAccess.DALCreate Or TestBuilderPermissionAccess.DALRead Or TestBuilderPermissionAccess.DALUpdate Or TestBuilderPermissionAccess.DALDelete)
            If enumValue > DalAll AndAlso
                enumValue <> TestBuilderPermissionAccess.AnyTask Then
                addedColumn = rolePermissions.Columns.Add(enumValue.ToString(), GetType(Boolean))
            End If
        Next

        'Create primary key on rolePermissions to facilitate row finding on that table.
        rolePermissions.PrimaryKey = New DataColumn() {rolePermissions.Columns("RoleId"), rolePermissions.Columns("PermissionTargetId")}
        'Link roles and permissions via a relation to facilitate hierarchical display in the gridex.
        rolePermissionDS.Relations.Add("RolePermissions", roleTable.Columns("Id"), rolePermissions.Columns("RoleId"))

        ' Sample of resulting structure:
        ' Role 1 -
        '         |
        '         Permission target A | p1 | p2 | p3 | p4 | p5 | p6 etc...
        '         Permission target B | p1 | p2 | p3 | p4 | p5 | p6 etc...
        ' Role 2 -
        '         |
        '         Permission target A | p1 | p2 | p3 | p4 | p5 | p6 etc...
        '         Permission target C | p1 | p2 | p3 | p4 | p5 | p6 etc...
        Dim roles As EntityCollection = AuthorizationFactory.Instance.GetRolePermissionCollection()

        For Each role As RoleEntity In roles
            roleTable.Rows.Add(role.Id, role.Name)

            For Each rolePermission As RolePermissionEntity In role.RolePermissionCollection
                Dim keyValues As Object() = {rolePermission.RoleId, rolePermission.PermissionTargetId}
                Dim existingRow As DataRow = rolePermissions.Rows.Find(keyValues)

                If existingRow Is Nothing Then
                    Dim permissionTargetName As String = rolePermission.PermissionTarget.Name
                    If permissionTargetName = TestBuilderPermissionTarget.NamedTask.ToString() Then
                        permissionTargetName = rolePermission.PermissionTarget.TargettedNamedTask
                    End If

                    rolePermissions.Rows.Add(rolePermission.RoleId, rolePermission.PermissionTargetId, permissionTargetName)

                    existingRow = rolePermissions.Rows.Find(keyValues)
                    Debug.Assert(existingRow IsNot Nothing)
                End If

                'Mark the appropriate permission as granted.
                existingRow(rolePermission.Permission.Name) = True
            Next
        Next

        Return rolePermissionDS
    End Function

    Private Sub DataBind()
        ApplyEnabled = False
        _users = AuthorizationFactory.Instance.GetUsers()
        UserSelectionGrid.DataSource = _users

        RoleEntityBindingSource.DataSource = BuildRolePermissionDataSet()
        RoleEntityBindingSource.DataMember = "Roles"

        'Let the role GridEx retrieve the structure from the dataset created by BuildRolePermissionDataSet()
        RoleGridEX.RetrieveStructure(True)

        'Hide the id colunms.
        RoleGridEX.Tables("Roles").Columns("Id").Visible = False
        RoleGridEX.Tables("RolePermissions").Columns("RoleId").Visible = False
        RoleGridEX.Tables("RolePermissions").Columns("PermissionTargetId").Visible = False

        'Alter the caption of the PermissionTargetName column.
        RoleGridEX.Tables("RolePermissions").Columns("PermissionTargetName").Caption = My.Resources.PermissionTarget

        'Sort roles ascending on Name column.
        Dim column As GridEXColumn = RoleGridEX.RootTable.Columns("Name")
        Dim sortKey As GridEXSortKey = New GridEXSortKey(column, SortOrder.Ascending)
        RoleGridEX.RootTable.SortKeys.Clear()
        RoleGridEX.RootTable.SortKeys.Add(sortKey)

        'Sort permissions ascending on permission target name column.
        column = RoleGridEX.Tables("RolePermissions").Columns("PermissionTargetName")
        sortKey = New GridEXSortKey(column, SortOrder.Ascending)
        RoleGridEX.Tables("RolePermissions").SortKeys.Add(sortKey)

    End Sub

    Private Sub ShowUserProperties(ByVal user As UserEntity)
        'Use a copy of the user object
        Dim userClone = BinaryCloner.DeepClone(user)

        'Prepare and show dialog window
        Dim userProperty As New UserPropertyDialog(user, True, True)
        userProperty.RemovedUserApplicationRoleCollection.AddRange(_removedUserApplicationRoleCollection)
        userProperty.RemovedUserBankRoleCollection.AddRange(_removedUserBankRoleCollection)
        AddHandler userProperty.DataChanged, AddressOf UserProperty_DataChanged

        If userProperty.ShowDialog() = DialogResult.OK Then
            'Collections back because they may have been altered.
            _removedUserApplicationRoleCollection = userProperty.RemovedUserApplicationRoleCollection
            _removedUserBankRoleCollection = userProperty.RemovedUserBankRoleCollection

            'Make the changes visible. 
            UserSelectionGrid.Refresh()
        Else
            'Rollback
            Dim clonedUser As UserEntity = _users.OfType(Of UserEntity).SingleOrDefault(Function(u) u.Id = userClone.Id)
            If (clonedUser IsNot Nothing) Then
                _users(_users.IndexOf(clonedUser)) = userClone
            End If
        End If
    End Sub

    <SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")>
    Private Function SaveUsers() As Boolean
        Dim saved As Boolean = False
        Try
            If _removedUserApplicationRoleCollection.Count > 0 Then
                AuthorizationFactory.Instance.DeleteUserApplicationRoles(_removedUserApplicationRoleCollection)
                _removedUserApplicationRoleCollection.Clear()
            End If
            If _removedUserBankRoleCollection.Count > 0 Then
                AuthorizationFactory.Instance.DeleteUserBankRoles(_removedUserBankRoleCollection)
                _removedUserBankRoleCollection.Clear()
            End If

            Dim errorMsg As String = String.Empty

            Dim usersToRemoveFromRemovedUsers As New EntityCollection
            If _removedUsers.Count > 0 Then
                For Each userToDelete As UserEntity In _removedUsers
                    errorMsg = AuthorizationFactory.Instance.DeleteUser(userToDelete)
                    usersToRemoveFromRemovedUsers.Add(userToDelete)
                    If Not String.IsNullOrEmpty(errorMsg) Then
                        errorMsg = String.Empty
                        MessageBox.Show(My.Resources.DeleteUserError, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        _users.Add(userToDelete)
                        Exit For
                    End If
                Next

                For Each deletedUser As UserEntity In usersToRemoveFromRemovedUsers
                    _removedUsers.Remove(deletedUser)
                Next
            End If

            If String.IsNullOrEmpty(errorMsg) Then
                errorMsg = AuthorizationFactory.Instance.UpdateUsers(_users)
            End If

            If String.IsNullOrEmpty(errorMsg) Then
                saved = True
            Else
                MessageBox.Show(errorMsg)
            End If

        Catch ex As Exception
            MessageBox.Show(String.Format(My.Resources.Authorization_Error_Saving, Environment.NewLine, ex.Message))
        End Try

        Return saved
    End Function


    Protected Overrides Function OnOk() As Boolean
        If ApplyEnabled Then
            Return SaveUsers()
        End If

        Return True
    End Function

    Protected Overrides Sub OnApply()
        If SaveUsers() Then
            ApplyEnabled = False
        End If
    End Sub


    Private Sub AuthorizationTabControl_Click(ByVal sender As Object, ByVal e As EventArgs) Handles AuthorizationTabControl.Click
        'Autosize the gridex column, for the first time.
        'This only works if the columns are visible so that's the reason for the use of this event.
        RoleGridEX.ColumnAutoSizeMode = ColumnAutoSizeMode.AllCellsAndHeader
        RoleGridEX.AutoSizeColumns()
    End Sub

    ''' <param name="e">The <see cref="Janus.Windows.GridEX.RowActionEventArgs" /> instance containing the event data.</param>
    Private Sub RoleGridEX_RowExpanded(ByVal sender As Object, ByVal e As RowActionEventArgs) Handles RoleGridEX.RowExpanded
        'Autosize the gridex subcolumns, for the first time.
        'This only works if the columns are visible so that's the reason for the use of this event.
        For i As Integer = 0 To RoleGridEX.Tables("RolePermissions").Columns.Count - 1
            RoleGridEX.Tables("RolePermissions").Columns(i).AutoSizeMode = ColumnAutoSizeMode.AllCellsAndHeader
            RoleGridEX.Tables("RolePermissions").Columns(i).AutoSize()
        Next
    End Sub

    Private Sub UserSelectionGrid_RowDoubleClick(ByVal sender As Object, ByVal e As EventArgs(Of UserEntity)) Handles UserSelectionGrid.RowDoubleClick
        ShowUserProperties(e.Value)
    End Sub

    Private Sub AddUserButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles AddUserButton.Click
        Dim userDialog As New NewUserDialog()
        If userDialog.ShowDialog() = DialogResult.OK Then
            _users.Add(userDialog.User)
            UserSelectionGrid.Refresh()
            ApplyEnabled = True
        End If
    End Sub

    Private Sub EditUserButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles EditUserButton.Click
        ShowUserProperties(UserSelectionGrid.SelectedUser)
    End Sub

    ''' <remarks>
    ''' Active Directory users only for the moment.
    ''' </remarks>
    Private Sub ImportButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ImportButton.Click
        Dim picker As New DirectoryObjectPicker()
        If picker.ShowDialog(Handle) = DialogResult.OK Then
            For Each selection As DirectoryObjectSelection In picker.Selected
                Dim user As New UserEntity
                user.Active = True
                user.UserName = selection.UserName
                user.FullName = selection.FullName
                user.Password = selection.UserName
                user.AuthenticationType = AuthenticationType.ActiveDirectory.ToString()
                _users.Add(user)
                ApplyEnabled = True
            Next
        End If
    End Sub

    Private Sub DeleteUserButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles DeleteUserButton.Click
        If MessageBox.Show(Me, My.Resources.DeleteThisUser, Text, MessageBoxButtons.YesNo) = DialogResult.Yes Then
            _removedUsers.Add(UserSelectionGrid.SelectedUser)
            _users.Remove(UserSelectionGrid.SelectedUser)
            UserSelectionGrid.Refresh()
            ApplyEnabled = True
        End If
    End Sub

    Private Sub UserProperty_DataChanged(ByVal sender As Object, ByVal e As EventArgs)
        ApplyEnabled = True
    End Sub

    Private Sub TransferCreatorModifierButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TransferCreatorModifierButton.Click
        Try
            Dim changeCreatorDialog As New ChangeCreatorModifierDialog(UserSelectionGrid.SelectedUser)

            If changeCreatorDialog.ShowDialog(Me) = DialogResult.OK Then
                If ResourceFactory.Instance.ChangeCreatorModifier(UserSelectionGrid.SelectedUser.Id, changeCreatorDialog.SelectedUser.Id) Then
                    MsgBox("Gebruiker succesvol gewijzigd.")
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub UserSelectionGrid_UserSelectionChanged(sender As Object, e As EventArgs) Handles UserSelectionGrid.UserSelectionChanged
        Dim currentUser = New UserEntity(CType(Thread.CurrentPrincipal.Identity, TestBuilderIdentity).UserId)
        If UserSelectionGrid.SelectedUser.Id.Equals(currentUser.Id) Then
            DeleteUserButton.Enabled = False
        Else
            DeleteUserButton.Enabled = True
        End If
    End Sub

End Class