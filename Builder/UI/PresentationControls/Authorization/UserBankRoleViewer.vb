Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports System.ComponentModel
Imports Questify.Builder.Logic.Service.Factories

Public Class UserBankRoleViewer


    <Description("This event will be raised when a row selection has changed"), Category("Bankroleviewer events")> _
    Public Event SelectionChanged As EventHandler(Of BankRoleViewerSelectionChangedEventArgs)



    Private _userBankRoleCollection As New EntityCollection(Of EntityClasses.UserBankRoleEntity)




    Public ReadOnly Property DataSource() As EntityCollection(Of EntityClasses.UserBankRoleEntity)
        Get
            Return _userBankRoleCollection
        End Get
    End Property

    Public ReadOnly Property SelectedRow() As BankRoleGridRowEntity
        Get
            If UserBankRoleGrid.GetRow() IsNot Nothing Then
                Return DirectCast(UserBankRoleGrid.GetRow().DataRow, BankRoleGridRowEntity)
            End If

            Return Nothing
        End Get
    End Property




    Public Sub DataBind()
        Dim banks As New List(Of Integer)
        For Each userRole In _userBankRoleCollection
            If Not banks.Contains(userRole.BankId) Then
                banks.Add(userRole.BankId)
            End If
        Next
        Dim bankCollection = BankFactory.Instance.GetBankHierarchyFilteredByBankIds(banks.ToArray())
        BankRoleGridRowEntityBindingSource.DataSource = CreateUserBankRoleGridRows(bankCollection)
        UserBankRoleGrid.ExpandRecords()
    End Sub



    Protected Overridable Sub OnSelectionChanged(ByVal e As BankRoleViewerSelectionChangedEventArgs)
        RaiseEvent SelectionChanged(Me, e)
    End Sub

    Public Overrides Sub Refresh()
        Me.DataBind()
        MyBase.Refresh()
    End Sub



    Private Function CreateUserBankRoleGridRows(ByVal bankCollection As EntityCollection) As List(Of BankRoleGridRowEntity)

        Dim rows As New List(Of BankRoleGridRowEntity)
        For Each bank As EntityClasses.BankEntity In bankCollection
            Dim row As New BankRoleGridRowEntity(bank.Id, bank.Name, Nothing, BankRoleGridRowEntityType.BankRow)

            AddChildUserRoleRowsToBankRow(row, bank.Id)

            CreateChildUserBankRoleGridRows(row, bank)
            rows.Add(row)
        Next

        Return rows
    End Function

    Private Sub CreateChildUserBankRoleGridRows(ByVal parentRow As BankRoleGridRowEntity, ByVal parentBank As EntityClasses.BankEntity)

        For Each bank As EntityClasses.BankEntity In parentBank.BankCollection
            Dim row As New BankRoleGridRowEntity(bank.Id, bank.Name, parentRow, BankRoleGridRowEntityType.BankRow)

            AddChildUserRoleRowsToBankRow(row, bank.Id)

            CreateChildUserBankRoleGridRows(row, bank)
            parentRow.BankRoleGridRowEntityCollection.Add(row)
        Next

    End Sub

    Private Sub AddChildUserRoleRowsToBankRow(ByVal bankRow As BankRoleGridRowEntity, ByVal bankId As Integer)

        Dim matches As List(Of Integer) = _userBankRoleCollection.FindMatches(UserBankRoleFields.BankId = bankId)
        For Each index As Integer In matches
            Dim userBankRole As EntityClasses.UserBankRoleEntity = _userBankRoleCollection.Item(index)
            bankRow.BankRoleGridRowEntityCollection.Add(New BankRoleGridRowEntity(userBankRole.BankRoleId, userBankRole.Role.Name, bankRow, BankRoleGridRowEntityType.UserRoleRow))
        Next

    End Sub



    Private Sub UserBankRoleGrid_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UserBankRoleGrid.SelectionChanged
        If UserBankRoleGrid.SelectedItems.Count > 0 Then
            Dim row As BankRoleGridRowEntity = DirectCast(UserBankRoleGrid.GetRow().DataRow, BankRoleGridRowEntity)
            OnSelectionChanged(New BankRoleViewerSelectionChangedEventArgs(row.Type))
        End If
    End Sub

    Private Sub UserBankRoleGrid_CollapsingRow(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles UserBankRoleGrid.CollapsingRow
        e.Cancel = True
    End Sub



End Class
