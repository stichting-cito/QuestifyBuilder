Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Model.Entities
Imports SD.LLBLGen.Pro.ORMSupportClasses

Public Class UserBankRoleDialog


    Private _filter As EntityCollection(Of EntityClasses.UserBankRoleEntity)
    Private _allRoles As EntityCollection
    Private _filterdRoles As IEntityView2



    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")> _
    Public Property Filter() As EntityCollection(Of EntityClasses.UserBankRoleEntity)
        Get
            Return _filter
        End Get
        Set(ByVal value As EntityCollection(Of EntityClasses.UserBankRoleEntity))
            _filter = value
            If BankSelector.SelectedBank IsNot Nothing Then
                FilterRoles(BankSelector.SelectedBank.id)
            End If
        End Set
    End Property


    Public ReadOnly Property SelectedBank() As BankDto
        Get
            Return BankSelector.SelectedBank
        End Get
    End Property

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")> _
    Public ReadOnly Property SelectedRoles() As List(Of EntityClasses.RoleEntity)
        Get
            Dim roles As New List(Of EntityClasses.RoleEntity)
            For Each role As EntityClasses.RoleEntity In RoleListBox.SelectedItems
                roles.Add(role)
            Next
            Return roles
        End Get
    End Property




    Public Sub New()

        InitializeComponent()

        DataBind()

        AddHandler BankSelector.RefreshBanks, Function() (RefreshBanks())

    End Sub



    Private Sub DataBind()
        BankSelector.DataSource = DtoFactory.Bank.All
        _allRoles = AuthorizationFactory.Instance.GetBankRoleCollection
        _filterdRoles = _allRoles.DefaultView
        RoleEntityBindingSource.DataSource = _filterdRoles
    End Sub

    Private Function RefreshBanks() As Boolean
        BankSelector.DataSource = DtoFactory.Bank.All
        Return True
    End Function

    Private Sub FilterRoles(ByVal bankId As Integer)
        If Me.Filter IsNot Nothing Then
            Dim foundRoles As List(Of Integer) = Me.Filter.FindMatches(UserBankRoleFields.BankId = bankId)
            Dim excludeRoles As New List(Of Integer)
            For Each index As Integer In foundRoles
                excludeRoles.Add(Me.Filter.Item(index).BankRoleId)
            Next

            If excludeRoles.Count > 0 Then
                _filterdRoles.Filter = (RoleFields.Id <> excludeRoles)
            Else
                _filterdRoles.Filter = Nothing
            End If
            RoleListBox.Refresh()
        End If
    End Sub



    Private Sub UserBankRoleDialog_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        RoleListBox.SelectedIndex = -1
    End Sub


    Private Sub BankSelector_BankSelected(ByVal sender As System.Object, ByVal e As Questify.Builder.UI.BankSelectedEventArgs) Handles BankSelector.BankSelected
        FilterRoles(e.SelectedBank.Id)
        RoleListBox.SelectedIndex = -1
    End Sub

    Private Sub RoleListBox_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RoleListBox.SelectedValueChanged
        Me.OkEnabled = RoleListBox.SelectedItems.Count > 0
    End Sub



End Class
