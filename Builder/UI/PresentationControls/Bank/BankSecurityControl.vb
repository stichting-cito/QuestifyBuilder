Imports System.ComponentModel
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.Security
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Questify.Builder.Logic.Service.Factories
Imports SD.LLBLGen.Pro.ORMSupportClasses

Public Class BankSecurityControl
    Private _userBankRoleCollection As EntityCollection
    Private _users As EntityCollection(Of UserEntity)
    Private _isDirty As Boolean
    Private _removedUserBankRoleEntities As EntityCollection


    <Description("This event will be raised when data is changed on this control"), Category("BankSecurityControl events")>
    Public Event DataChanged As EventHandler(Of EventArgs)

    Protected Sub OnDataChanged(e As EventArgs)
        RaiseEvent DataChanged(Me, e)
    End Sub



    Public Property BankContext As BankEntity

    Public ReadOnly Property UserBankRoleCollection As EntityCollection
        Get
            Return _userBankRoleCollection
        End Get
    End Property

    Public ReadOnly Property IsDirty As Boolean
        Get
            Return _isDirty OrElse RolesList.IsDirty
        End Get
    End Property

    Public Sub ResetIsDirty()
        _isDirty = False
        If _removedUserBankRoleEntities IsNot Nothing Then
            _removedUserBankRoleEntities.Dispose()
            _removedUserBankRoleEntities = Nothing
        End If

        RolesList.ResetIsDirty()
    End Sub

    Public ReadOnly Property RemovedUserBankRoleEntities As EntityCollection
        Get
            If _removedUserBankRoleEntities Is Nothing Then
                _removedUserBankRoleEntities = New EntityCollection(New UserBankRoleEntityFactory)
            End If

            Return _removedUserBankRoleEntities
        End Get
    End Property



    Public Sub DataBind()
        If BankContext IsNot Nothing Then
            _userBankRoleCollection = AuthorizationFactory.Instance.GetUsersWithPermissionsInBank(BankContext.Id)
            _users = CreateUserCollection()
            UsersBindingSource.DataSource = _users

            AddHandler _userBankRoleCollection.EntityRemoved, AddressOf UserBankRoleEntityRemovedFromCollection
            AddHandler _userBankRoleCollection.EntityAdded, AddressOf UserBankRoleEntityAddedToCollection

            RolesList.AllRoles.Clear()
            Dim roles As EntityCollection = AuthorizationFactory.Instance.GetBankRoleCollection()
            If roles IsNot Nothing Then
                RolesList.AllRoles.AddRange(roles)
            End If

            RolesList.BankId = BankContext.Id
        End If
    End Sub

    Public Sub ForceUpdate()
        RolesList.ForceBankRoleCollectionUpdate()
    End Sub



    Private Sub UsersGridView_SelectionChanged(sender As Object, e As EventArgs) Handles UsersGridView.SelectionChanged
        If UsersGridView.SelectedRows.Count = 1 Then
            Dim selectedUser = DirectCast(UsersGridView.SelectedRows(0).DataBoundItem, UserEntity)
            RolesList.SetGrid(selectedUser, _userBankRoleCollection)
        End If
    End Sub

    Private Sub AddUserButton_Click(sender As Object, e As EventArgs) Handles AddUserButton.Click
        Dim identity As TestBuilderIdentity = DirectCast(My.User.CurrentPrincipal.Identity(), TestBuilderIdentity)
        Dim dialog As New SelectUserDialog()
        Dim result As DialogResult = dialog.ShowDialog()
        If result = DialogResult.OK Then
            Dim selectedUser As UserEntity = dialog.SelectedUser
            If _users.Contains(selectedUser) Then
                MessageBox.Show(My.Resources.BankSecurityControl_AddUserButton_Click_UserAlreadyExists, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                _users.Add(selectedUser)
                UsersGridView.Refresh()

                SetSelectedUser(selectedUser)
                _isDirty = True

                OnDataChanged(New EventArgs)
            End If
        End If
    End Sub

    Private Sub DeleteUserButton_Click(sender As Object, e As EventArgs) Handles RemoveUserButton.Click
        If UsersGridView.SelectedRows.Count = 1 Then
            Dim selectedUser As UserEntity = DirectCast(UsersGridView.SelectedRows(0).DataBoundItem, UserEntity)
            Dim filter As New PredicateExpression()
            filter.Add(UserBankRoleFields.UserId = selectedUser.Id)
            Dim foundIndexes As List(Of Integer) = _userBankRoleCollection.FindMatches(filter)
            foundIndexes.Sort(AddressOf CompareIntegersDescending)
            For Each index As Integer In foundIndexes
                _userBankRoleCollection.RemoveAt(index)
            Next

            If _users.Contains(selectedUser) Then
                _isDirty = True
                OnDataChanged(New EventArgs)

                RolesList.ClearGrid()

                _users.Remove(selectedUser)
                UsersGridView.Refresh()

                If _users.Count > 0 Then
                    UsersGridView.Rows(0).Selected = True
                End If
            End If
        End If
    End Sub

    Private Sub RolesList_DataChanged(sender As Object, e As EventArgs) Handles RolesList.DataChanged
        OnDataChanged(New EventArgs)
    End Sub



    Private Function CreateUserCollection() As EntityCollection(Of UserEntity)
        Dim resultCollection As New EntityCollection(Of UserEntity)

        If _userBankRoleCollection IsNot Nothing Then
            For Each userBankRole As UserBankRoleEntity In _userBankRoleCollection
                If Not resultCollection.Contains(userBankRole.User) Then
                    resultCollection.Add(userBankRole.User)
                End If
            Next
        End If

        Return resultCollection
    End Function

    Private Sub UserBankRoleEntityRemovedFromCollection(sender As Object, e As CollectionChangedEventArgs)
        RemovedUserBankRoleEntities.Add(DirectCast(e.InvolvedEntity, UserBankRoleEntity))
        _isDirty = True
    End Sub

    Private Sub UserBankRoleEntityAddedToCollection(sender As Object, e As CollectionChangedEventArgs)
        Dim addedEntity = DirectCast(e.InvolvedEntity, UserBankRoleEntity)
        Dim foundEntity As UserBankRoleEntity = Nothing
        For Each entity As UserBankRoleEntity In Me.RemovedUserBankRoleEntities
            If entity.UserId.Equals(addedEntity.UserId) AndAlso entity.BankRoleId.Equals(addedEntity.BankRoleId) AndAlso entity.BankId.Equals(addedEntity.BankId) Then
                foundEntity = entity
                Exit For
            End If
        Next

        If foundEntity IsNot Nothing Then
            RemovedUserBankRoleEntities.Remove(foundEntity)
        End If

        _isDirty = True
    End Sub

    Private Function CompareIntegersDescending(x As Integer, y As Integer) As Integer
        Return y.CompareTo(x)
    End Function

    Public Sub SetSelectedUser(selectedUser As UserEntity)

        If selectedUser Is Nothing Then
            Throw New ArgumentNullException("selectedUser")
        End If

        For Each row As DataGridViewRow In UsersGridView.Rows
            Dim boundItem = DirectCast(row.DataBoundItem, UserEntity)

            If boundItem.Id.Equals(selectedUser.Id) Then
                row.Selected = True
                Exit Sub
            End If
        Next
    End Sub

End Class
