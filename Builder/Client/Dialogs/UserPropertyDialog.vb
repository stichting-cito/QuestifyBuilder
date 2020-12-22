
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses
Imports System.ComponentModel
Imports System.Diagnostics.CodeAnalysis
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.UI

Public Class UserPropertyDialog


    <Description("This event will be raised when the data in the control has been changed."), Category("Userproperty events")> _
    Public Event DataChanged As EventHandler(Of EventArgs)



    Private _user As UserEntity
    Private ReadOnly _isEditable As Boolean
    Private ReadOnly _doFetchAndSave As Boolean
    Private _removedUserApplicationRoleCollection As EntityCollection
    Private _removedUserBankRoleCollection As EntityCollection
    Private _isDirty As Boolean



    Public ReadOnly Property RemovedUserApplicationRoleCollection() As EntityCollection
        Get
            If _removedUserApplicationRoleCollection Is Nothing Then
                _removedUserApplicationRoleCollection = New EntityCollection
            End If
            Return _removedUserApplicationRoleCollection
        End Get
    End Property

    Public ReadOnly Property RemovedUserBankRoleCollection() As EntityCollection
        Get
            If _removedUserBankRoleCollection Is Nothing Then
                _removedUserBankRoleCollection = New EntityCollection
            End If
            Return _removedUserBankRoleCollection
        End Get
    End Property




    Private Sub New()
        InitializeComponent()

    End Sub

    Public Sub New(ByVal user As UserEntity)
        Me.New(user, False, True)
    End Sub

    Public Sub New(ByVal user As UserEntity, ByVal editable As Boolean, ByVal doFetchAndSave As Boolean)
        Me.New()
        _user = user
        _isEditable = editable
        _doFetchAndSave = doFetchAndSave
        DataBind()
    End Sub

    Public Property IsDirty() As Boolean
        Get
            Return _isDirty
        End Get
        Private Set(ByVal value As Boolean)
            _isDirty = value
        End Set
    End Property



    Private Sub DataBind()
        ApplyEnabled = False

        If _doFetchAndSave Then
            _user = AuthorizationFactory.Instance.GetUserWithRoles(_user)
        End If

        AddHandler _user.PropertyChanged, AddressOf UserDataChanged

        MetaData.Datasource = _user
        MetaData.IsEditable = _isEditable

        Dim roles As EntityCollection = AuthorizationFactory.Instance.GetApplicationRoleCollection()
        If roles IsNot Nothing Then
            UserApplicationRoleGrid.AllRoles.AddRange(roles)
        End If

        UserApplicationRoleGrid.UserApplicationRoleCollection.AddRange(_user.UserApplicationRoleCollection)
        UserApplicationRoleGrid.SetGrid(_user)
        UserApplicationRoleGrid.Enabled = _isEditable
        AddHandler UserApplicationRoleGrid.UserApplicationRoleCollection.EntityAdded, AddressOf UserApplicationRoleEntityAddedToCollection
        AddHandler UserApplicationRoleGrid.UserApplicationRoleCollection.EntityRemoved, AddressOf UserApplicationRoleEntityRemovedFromCollection

        AddHandler _user.UserBankRoleCollection.EntityAdded, AddressOf UserBankRoleEntityAddedToCollection
        AddHandler _user.UserBankRoleCollection.EntityRemoved, AddressOf UserBankRoleEntityRemovedFromCollection
        BankRoleViewer.DataSource.AddRange(_user.UserBankRoleCollection)
        BankRoleViewer.DataBind()
        BankRoleViewer.Enabled = True

        AddBankRoleButton.Enabled = _isEditable
        DeleteBankRoleButton.Enabled = _isEditable AndAlso BankRoleViewer.SelectedRow IsNot Nothing AndAlso BankRoleViewer.SelectedRow.Type = BankRoleGridRowEntityType.UserRoleRow

        Dim titleBarBinding As New Binding("Text", _user, "FullName")
        AddHandler titleBarBinding.Format, AddressOf TitleToFormTitleFormatter
        DataBindings.Add(titleBarBinding)
    End Sub

    <SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> _
    Private Function SaveUser() As Boolean
        Dim saved As Boolean = False
        If IsDirty Then
            Try
                If RemovedUserApplicationRoleCollection.Count > 0 Then
                    AuthorizationFactory.Instance.DeleteUserApplicationRoles(Me.RemovedUserApplicationRoleCollection)
                    RemovedUserApplicationRoleCollection.Clear()
                End If
                If RemovedUserBankRoleCollection.Count > 0 Then
                    AuthorizationFactory.Instance.DeleteUserBankRoles(Me.RemovedUserBankRoleCollection)
                    RemovedUserBankRoleCollection.Clear()
                End If

                Dim errorMsg As String = AuthorizationFactory.Instance.UpdateUser(_user)

                If String.IsNullOrEmpty(errorMsg) Then
                    saved = True
                Else
                    MessageBox.Show(errorMsg)
                End If
            Catch ex As Exception
                MessageBox.Show(String.Format(My.Resources.Authorization_Error_Saving, Environment.NewLine, ex.Message))
            End Try
        End If
        Return saved
    End Function



    Protected Sub OnDataChanged(ByVal e As EventArgs)
        RaiseEvent DataChanged(Me, e)
    End Sub

    Protected Overrides Function OnOk() As Boolean
        Dim dataErrorInfo As IDataErrorInfo = DirectCast(_user, IDataErrorInfo)
        If String.IsNullOrEmpty(dataErrorInfo.Error) Then
            If _doFetchAndSave Then
                SaveUser()
            End If
            Return True
        End If
        Return False
    End Function

    Protected Overrides Sub OnApply()
        If SaveUser() Then
            Me.ApplyEnabled = False
        End If
    End Sub



    Private Sub UserApplicationRoleEntityAddedToCollection(ByVal sender As Object, ByVal e As CollectionChangedEventArgs)
        Dim addedEntity As UserApplicationRoleEntity = DirectCast(e.InvolvedEntity, UserApplicationRoleEntity)

        _user.UserApplicationRoleCollection.Add(addedEntity)

        Dim foundEntity As UserApplicationRoleEntity = Nothing
        For Each entity As UserApplicationRoleEntity In Me.RemovedUserApplicationRoleCollection
            If entity.ApplicationRoleId.Equals(addedEntity.ApplicationRoleId) AndAlso entity.UserId.Equals(addedEntity.UserId) Then
                foundEntity = entity
                Exit For
            End If
        Next

        If foundEntity IsNot Nothing Then
            RemovedUserApplicationRoleCollection.Remove(foundEntity)
        End If

        OnDataChanged(New EventArgs())
    End Sub

    Private Sub UserApplicationRoleEntityRemovedFromCollection(ByVal sender As Object, ByVal e As CollectionChangedEventArgs)
        Dim removedEntity As UserApplicationRoleEntity = DirectCast(e.InvolvedEntity, UserApplicationRoleEntity)

        RemovedUserApplicationRoleCollection.Add(DirectCast(e.InvolvedEntity, UserApplicationRoleEntity))

        _user.UserApplicationRoleCollection.Remove(removedEntity)

        OnDataChanged(New EventArgs())
    End Sub

    Private Sub UserBankRoleEntityAddedToCollection(ByVal sender As Object, ByVal e As CollectionChangedEventArgs)
        Dim addedEntity As UserBankRoleEntity = DirectCast(e.InvolvedEntity, UserBankRoleEntity)

        Dim foundEntity As UserBankRoleEntity = Nothing
        For Each entity As UserBankRoleEntity In Me.RemovedUserBankRoleCollection
            If entity.BankRoleId.Equals(addedEntity.BankRoleId) AndAlso entity.UserId.Equals(addedEntity.UserId) Then
                foundEntity = entity
                Exit For
            End If
        Next

        If foundEntity IsNot Nothing Then
            RemovedUserBankRoleCollection.Remove(foundEntity)
        End If

        OnDataChanged(New EventArgs())
    End Sub

    Private Sub UserBankRoleEntityRemovedFromCollection(ByVal sender As Object, ByVal e As CollectionChangedEventArgs)
        Me.RemovedUserBankRoleCollection.Add(DirectCast(e.InvolvedEntity, UserBankRoleEntity))
        OnDataChanged(New EventArgs())
    End Sub

    Private Sub UserApplicationRoleGrid_DataChanged(ByVal sender As Object, ByVal e As EventArgs) Handles UserApplicationRoleGrid.DataChanged
        Me.ApplyEnabled = _doFetchAndSave
        Me.IsDirty = True
        OnDataChanged(New EventArgs())
    End Sub

    Private Sub UserDataChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
        Me.ApplyEnabled = _doFetchAndSave
        Me.IsDirty = True
        OnDataChanged(New EventArgs())
    End Sub

    Private Sub BankRoleViewer_SelectionChanged(ByVal sender As Object, ByVal e As BankRoleViewerSelectionChangedEventArgs) Handles BankRoleViewer.SelectionChanged
        Me.DeleteBankRoleButton.Enabled = _isEditable AndAlso (e.SelectedRowType = BankRoleGridRowEntityType.UserRoleRow)
    End Sub

    Private Sub AddBankRoleButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles AddBankRoleButton.Click
        Dim newBankRoleDialog As New UserBankRoleDialog
        newBankRoleDialog.Filter = _user.UserBankRoleCollection
        If newBankRoleDialog.ShowDialog() = DialogResult.OK Then
            Dim bankId As Integer = newBankRoleDialog.SelectedBank.id
            Dim roles As List(Of RoleEntity) = newBankRoleDialog.SelectedRoles

            If roles.Count > 0 Then
                Me.ApplyEnabled = _doFetchAndSave
                Me.IsDirty = True

                For Each role As RoleEntity In roles
                    Dim userBankRole As UserBankRoleEntity = New UserBankRoleEntity(_user.Id, bankId, role.Id)
                    userBankRole.Role = role
                    _user.UserBankRoleCollection.Add(userBankRole)
                    BankRoleViewer.DataSource.Add(userBankRole)
                Next
                BankRoleViewer.Refresh()
            End If
        End If
    End Sub

    Private Sub DeleteBankRoleButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles DeleteBankRoleButton.Click

        DeleteBankRoleButton.Enabled = False

        Dim row As BankRoleGridRowEntity = BankRoleViewer.SelectedRow
        Dim selectedIndex As Integer = _user.UserBankRoleCollection.FindMatches(UserBankRoleFields.BankId = row.Parent.EntityId And UserBankRoleFields.BankRoleId = row.EntityId)(0)
        BankRoleViewer.DataSource.Remove(_user.UserBankRoleCollection(selectedIndex))
        _user.UserBankRoleCollection.RemoveAt(selectedIndex)
        BankRoleViewer.Refresh()
        Me.ApplyEnabled = _doFetchAndSave
        Me.IsDirty = True
    End Sub

    Private Sub TitleToFormTitleFormatter(ByVal sender As Object, ByVal e As ConvertEventArgs)
        e.Value = String.Format(My.Resources.UserPropertyTitle, e.Value)
    End Sub



End Class