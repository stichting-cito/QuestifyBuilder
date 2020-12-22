Imports System.ComponentModel
Imports System.Diagnostics.CodeAnalysis
Imports Enums
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Questify.Builder.Security
Imports Questify.Builder.Logic.Service.ProgressIndicator
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.InvalidateCache.Helper
Imports SecurityException = System.Security.SecurityException

Public Class BankPropertyDialog

    Private ReadOnly _bank As BankEntity
    Private _editUserBankRolesPermitted As Boolean
    Private _editCustomPropertiesPermitted As Boolean

    Public Sub New(ByVal bank As BankEntity)
        InitializeComponent()

        If bank Is Nothing Then
            Throw New ArgumentNullException("bank")
        End If

        _bank = bank
    End Sub

    Private Sub BankPropertyDialog_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        _editUserBankRolesPermitted = PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Edit, TestBuilderPermissionTarget.UserBankRoleEntity, _bank.Id)
        If Not _editUserBankRolesPermitted Then
            BankPropertiesTabs.TabPages.RemoveByKey("SecurityTabPage")
        End If

        _editCustomPropertiesPermitted = PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Edit Or TestBuilderPermissionAccess.AddNew Or TestBuilderPermissionAccess.Delete, TestBuilderPermissionTarget.CustomBankPropertyEntity, _bank.Id)
        If Not _editCustomPropertiesPermitted Then
            BankPropertiesTabs.TabPages.RemoveByKey("CustomPropertiesPage")
        End If

        DataBind()

        _bank.SaveFields("BankProperties")
        For Each cbpe As CustomBankPropertyEntity In _bank.CustomBankPropertyCollection
            cbpe.SaveFields("BankProperties")
            If TypeOf cbpe Is ListCustomBankPropertyEntity Then
                For Each lv As ListValueCustomBankPropertyEntity In CType(cbpe, ListCustomBankPropertyEntity).ListValueCustomBankPropertyCollection
                    lv.SaveFields("BankProperties")
                Next
            End If
        Next

        ApplyEnabled = False
    End Sub

    Private Sub SubControl_DataChanged(ByVal sender As Object, ByVal e As EventArgs) Handles BankMetaData.DataChanged, BankSecurity.DataChanged
        ApplyEnabled = True
    End Sub



    Private Function FindDuplicatePropertynames(ByVal bank As BankEntity) As List(Of String)
        Dim propertiesWithUniqueName As List(Of String) = New List(Of String)
        Dim result As List(Of String) = New List(Of String)

        For Each customProperty As CustomBankPropertyEntity In bank.CustomBankPropertyCollection
            If (propertiesWithUniqueName.Contains(customProperty.Name)) Then
                result.Add(customProperty.Name)
            Else
                propertiesWithUniqueName.Add(customProperty.Name)
            End If
        Next

        Return result
    End Function

    Private Function ValidateNewProperties(ByVal bank As BankEntity) As List(Of String)
        Dim invalidCustomProperties As List(Of String) = New List(Of String)
        invalidCustomProperties.AddRange(FindDuplicatePropertynames(bank))

        If bank.ParentBank IsNot Nothing Then
            Dim customPropertiesOfParentBanks As EntityCollection = BankFactory.Instance.GetCustomBankPropertiesForBranch(bank.ParentBank, ResourceTypeEnum.AllResources)

            For Each customBankProperty As CustomBankPropertyEntity In bank.CustomBankPropertyCollection
                Dim results As List(Of Integer) = customPropertiesOfParentBanks.FindMatches(CustomBankPropertyFields.Name = customBankProperty.Name)

                If (results.Count > 0) Then
                    invalidCustomProperties.Add(customBankProperty.Name)
                End If
            Next
        End If

        Return invalidCustomProperties
    End Function

    <SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")>
    Private Function SaveBankProperties() As Boolean
        Dim result As Boolean = False
        Dim refreshHierarchy As Boolean = False

        If _bank.HasChangesInTopology Then
            Try
                Dim invalidProperties As List(Of String) = ValidateNewProperties(_bank)

                If (invalidProperties.Count = 0) Then
                    ProgressHandler.DoWorkWithModal(Sub(task)
                                                        task.Report(My.Resources.ProgressHandler_UpdatingBankAndServerCache)
                                                        BankFactory.Instance.UpdateBank(_bank)
                                                    End Sub, Me.Owner, 1000, 0, 0)
                    refreshHierarchy = True
                    result = True
                Else
                    MessageBox.Show(String.Format(My.Resources.CustomPropertyAlreadyExistInThisBankHierarchy, String.Join(", ", invalidProperties.ToArray())))
                    Return False
                End If
            Catch ex As SecurityException
                MessageBox.Show(ex.Message)
            Catch ex As Exception
                MessageBox.Show(String.Format(My.Resources.ErrorSavingBankInformation, Environment.NewLine, ex.Message))
            End Try
        Else
            result = True
        End If

        If _editUserBankRolesPermitted Then
            Try
                BankSecurity.ForceUpdate()
                If BankSecurity.IsDirty Then
                    If BankSecurity.RemovedUserBankRoleEntities.Count > 0 Then
                        AuthorizationFactory.Instance.DeleteUserBankRoles(BankSecurity.RemovedUserBankRoleEntities)
                    End If

                    AuthorizationFactory.Instance.UpdateUserBankRoles(BankSecurity.UserBankRoleCollection)

                    InvalidateCacheHelper.ClearCacheForBank(_bank.Id)
                    BankSecurity.ResetIsDirty()

                    refreshHierarchy = True
                End If

                result = True

            Catch ex As SecurityException
                MessageBox.Show(ex.Message)
            Catch ex As Exception
                MessageBox.Show(String.Format(My.Resources.ErrorSavingBankInformation, Environment.NewLine, ex.Message))
            End Try
        Else
            result = True
        End If

        If result AndAlso refreshHierarchy Then MainForm.MainBankBrowser.StartBanksRefresh()
        Return result
    End Function

    Private Sub DataBind()
        Me.Text = String.Format(My.Resources.BankPropertiesFor, _bank.Name)

        BankMetaData.Datasource = _bank
        If _editUserBankRolesPermitted Then
            BankSecurity.BankContext = _bank
            BankSecurity.DataBind()
        End If
    End Sub



    Protected Overrides Function OnOk() As Boolean
        Dim dataErrorInfo As IDataErrorInfo = DirectCast(_bank, IDataErrorInfo)
        If Not String.IsNullOrEmpty(dataErrorInfo.Error) Then
            Return False
        End If

        Return SaveBankProperties()
    End Function

    Protected Overrides Function OnCancel() As Boolean
        Dim propertyEntitiesToRemove As New List(Of CustomBankPropertyEntity)
        _bank.RollbackFields("BankProperties")
        For Each cbpe As CustomBankPropertyEntity In _bank.CustomBankPropertyCollection
            If cbpe.IsNew Then
                propertyEntitiesToRemove.Add(cbpe)
            Else

                If cbpe.IsDirty Then
                    cbpe.RollbackFields("BankProperties")
                End If

                If TypeOf cbpe Is ListCustomBankPropertyEntity Then
                    Dim valueEntitiesToRemove As New List(Of ListValueCustomBankPropertyEntity)
                    For Each lv As ListValueCustomBankPropertyEntity In CType(cbpe, ListCustomBankPropertyEntity).ListValueCustomBankPropertyCollection
                        If lv.IsNew Then
                            valueEntitiesToRemove.Add(lv)
                        ElseIf lv.IsDirty Then
                            lv.RollbackFields("BankProperties")
                        End If
                    Next

                    For Each entity As ListValueCustomBankPropertyEntity In valueEntitiesToRemove
                        CType(cbpe, ListCustomBankPropertyEntity).ListValueCustomBankPropertyCollection.Remove(entity)
                    Next

                End If

            End If
        Next

        For Each entity As CustomBankPropertyEntity In propertyEntitiesToRemove
            _bank.CustomBankPropertyCollection.Remove(entity)
        Next
    End Function

    Protected Overrides Sub OnApply()
        If SaveBankProperties() Then
            ApplyEnabled = False
        End If
    End Sub


End Class
