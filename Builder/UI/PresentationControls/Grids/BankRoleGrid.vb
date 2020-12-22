Imports System.ComponentModel
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Questify.Builder.Logic.Service.Exceptions
Imports SD.LLBLGen.Pro.ORMSupportClasses

Public Class BankRoleGrid

    Private _userBankRoleCollection As New EntityCollection
    Private _allRoles As New EntityCollection
    Private _gridRows As List(Of RoleGridRowEntity)
    Private _selectedUser As EntityClasses.UserEntity
    Private _bankId As Integer
    Private _isDirty As Boolean


    <Description("This event will be raised when data is changed on this control"), Category("BankRoleGrid Control events")> _
    Public Event DataChanged As EventHandler(Of EventArgs)

    Protected Sub OnDataChanged(ByVal e As EventArgs)
        RaiseEvent DataChanged(Me, e)
    End Sub



    Public ReadOnly Property AllRoles() As EntityCollection
        Get
            Return _allRoles
        End Get
    End Property

    Public ReadOnly Property IsDirty() As Boolean
        Get
            Return _isDirty
        End Get
    End Property

    Public Sub ResetIsDirty()
        _isDirty = False
    End Sub

    Public Property BankId() As Integer
        Get
            Return _bankId
        End Get
        Set(ByVal value As Integer)
            _bankId = value
        End Set
    End Property



    Public Sub SetGrid(ByVal selectedUser As EntityClasses.UserEntity, ByVal userBankRoleCollection As EntityCollection)
        _userBankRoleCollection = userBankRoleCollection

        If _userBankRoleCollection IsNot Nothing AndAlso _gridRows IsNot Nothing Then
            UpdateBankRoleCollection()
        End If

        _selectedUser = selectedUser
        _gridRows = CreateRoleGridRowCollectionFromBankRoles()
        RoleGridView.DataSource = _gridRows
    End Sub

    Public Sub ForceBankRoleCollectionUpdate()
        UpdateBankRoleCollection()
    End Sub

    Public Sub ClearGrid()
        _gridRows.Clear()
        RoleGridView.DataSource = Nothing
    End Sub



    Private Function CreateRoleGridRowCollectionFromBankRoles() As List(Of RoleGridRowEntity)
        Dim resultCollection As New List(Of RoleGridRowEntity)

        If _userBankRoleCollection IsNot Nothing Then
            For Each role As EntityClasses.RoleEntity In _allRoles
                Dim filter As New PredicateExpression()
                Dim found As Boolean = False
                filter.Add(UserBankRoleFields.UserId = _selectedUser.Id)
                filter.AddWithAnd(UserBankRoleFields.BankRoleId = role.Id)
                Dim foundIndexes As List(Of Integer) = _userBankRoleCollection.FindMatches(filter)
                If foundIndexes.Count = 1 Then
                    found = True
                ElseIf foundIndexes.Count > 1 Then
                    Throw New AppLogicException(String.Format("Found multiple records in userbankrole for user '{0}' with role '{1}'. This is not possible!", _
    _selectedUser.Id, role.Id))
                End If

                resultCollection.Add(New RoleGridRowEntity(role.Id, role.Name, found))
            Next
        End If

        Return resultCollection
    End Function

    Private Sub UpdateBankRoleCollection()
        If _gridRows IsNot Nothing Then
            For Each row As RoleGridRowEntity In _gridRows
                Dim filter As New PredicateExpression()
                filter.Add(UserBankRoleFields.UserId = _selectedUser.Id)
                filter.AddWithAnd(UserBankRoleFields.BankRoleId = row.RoleId)
                Dim foundIndexes As List(Of Integer) = _userBankRoleCollection.FindMatches(filter)

                If foundIndexes.Count = 1 AndAlso row.IsInRoleNewValue = False Then
                    _userBankRoleCollection.RemoveAt(foundIndexes(0))
                ElseIf foundIndexes.Count = 0 AndAlso row.IsInRoleNewValue = True Then
                    _userBankRoleCollection.Add(New EntityClasses.UserBankRoleEntity(_selectedUser.Id, _bankId, row.RoleId))
                End If
            Next
        End If
    End Sub

    Private Sub RoleGridView_CurrentCellDirtyStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RoleGridView.CurrentCellDirtyStateChanged
        OnDataChanged(New EventArgs)
    End Sub


End Class
