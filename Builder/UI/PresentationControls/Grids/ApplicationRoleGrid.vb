Imports Questify.Builder.Model.ContentModel
Imports SD.LLBLGen.Pro.ORMSupportClasses

Imports System.ComponentModel
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Questify.Builder.Logic.Service.Exceptions

Public Class ApplicationRoleGrid


    <Description("This event will be raised when the data in the control changed"), Category("ApplicationRoleGrid events")> _
    Public Event DataChanged As EventHandler(Of EventArgs)



    Private _userApplicationRoleCollection As New EntityCollection(Of EntityClasses.UserApplicationRoleEntity)
    Private _allRoles As New EntityCollection
    Private _gridRows As List(Of RoleGridRowEntity)
    Private _selectedUser As EntityClasses.UserEntity
    Private _isDirty As Boolean




    Public ReadOnly Property AllRoles() As EntityCollection
        Get
            Return _allRoles
        End Get
    End Property


    Public ReadOnly Property UserApplicationRoleCollection() As EntityCollection(Of EntityClasses.UserApplicationRoleEntity)
        Get
            Return _userApplicationRoleCollection
        End Get
    End Property

    Public Property IsDirty() As Boolean
        Get
            Return _isDirty
        End Get
        Private Set(ByVal value As Boolean)
            _isDirty = value
        End Set
    End Property




    Public Sub SetGrid(ByVal selectedUser As EntityClasses.UserEntity)
        _selectedUser = selectedUser
        _gridRows = CreateRoleGridRowCollectionFromApplicationRoles()
        RoleGridView.DataSource = _gridRows
    End Sub

    Public Sub ClearGrid()
        _gridRows.Clear()
        RoleGridView.DataSource = Nothing
    End Sub



    Protected Overridable Sub OnDataChanged()
        Me.IsDirty = True
        RaiseEvent DataChanged(Me, New EventArgs())
    End Sub




    Private Function CreateRoleGridRowCollectionFromApplicationRoles() As List(Of RoleGridRowEntity)
        Dim resultCollection As New List(Of RoleGridRowEntity)

        If _userApplicationRoleCollection IsNot Nothing Then
            For Each role As EntityClasses.RoleEntity In _allRoles
                Dim filter As New PredicateExpression()
                Dim found As Boolean = False
                filter.Add(UserApplicationRoleFields.UserId = _selectedUser.Id)
                filter.AddWithAnd(UserApplicationRoleFields.ApplicationRoleId = role.Id)
                Dim foundIndexes As List(Of Integer) = _userApplicationRoleCollection.FindMatches(filter)
                If foundIndexes.Count = 1 Then
                    found = True
                ElseIf foundIndexes.Count > 1 Then
                    Throw New AppLogicException(String.Format("Found multiple records in userapplicationrole for user '{0}' with role '{1}'. This is not possible!", _
    _selectedUser.Id, role.Id))
                End If

                resultCollection.Add(New RoleGridRowEntity(role.Id, role.Name, found))
            Next
        End If

        Return resultCollection
    End Function



    Private Sub RoleGridView_CurrentCellDirtyStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RoleGridView.CurrentCellDirtyStateChanged
        Dim row As RoleGridRowEntity = DirectCast(RoleGridView.CurrentRow.DataBoundItem, RoleGridRowEntity)

        Dim filter As New PredicateExpression()
        filter.Add(UserApplicationRoleFields.UserId = _selectedUser.Id)
        filter.AddWithAnd(UserApplicationRoleFields.ApplicationRoleId = row.RoleId)
        Dim foundIndexes As List(Of Integer) = _userApplicationRoleCollection.FindMatches(filter)

        If foundIndexes.Count = 1 AndAlso row.IsInRoleNewValue = False Then
            _userApplicationRoleCollection.RemoveAt(foundIndexes(0))
        ElseIf foundIndexes.Count = 0 AndAlso row.IsInRoleNewValue = True Then
            _userApplicationRoleCollection.Add(New EntityClasses.UserApplicationRoleEntity(_selectedUser.Id, row.RoleId))
        End If

        OnDataChanged()
    End Sub



End Class
