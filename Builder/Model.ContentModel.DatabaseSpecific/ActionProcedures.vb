Imports System
Imports System.Data
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.DatabaseSpecific
    Public Class ActionProcedures
        Private Sub New()
        End Sub

        Delegate Function ChangeCreatorModifierCallBack(currentUserId As System.Int32, newUserId As System.Int32, dataAccessProvider As IDataAccessCore) As Integer
        Delegate Function ClearBankCallBack(bankId As System.Int32, dataAccessProvider As IDataAccessCore) As Integer
        Delegate Function SetMaintenanceWindowCallBack(plannedTimestamp As System.DateTime, dataAccessProvider As IDataAccessCore) As Integer
        Delegate Function UpdateCustomBankPropertyBankIdCallBack(customBankPropertyId As System.Guid, bankId As System.Int32, dataAccessProvider As IDataAccessCore) As Integer
        Delegate Function UpdateResourceBankIdCallBack(resourceId As System.Guid, bankId As System.Int32, dataAccessProvider As IDataAccessCore) As Integer


        Public Shared Function ChangeCreatorModifier(currentUserId As System.Int32, newUserId As System.Int32) As Integer
            Using dataAccessProvider As New DataAccessAdapter()
                Return ChangeCreatorModifier(currentUserId, newUserId, dataAccessProvider)
            End Using
        End Function

        Public Shared Function ChangeCreatorModifier(currentUserId As System.Int32, newUserId As System.Int32, dataAccessProvider As IDataAccessCore) As Integer
            Using spCall As StoredProcedureCall = CreateChangeCreatorModifierCall(dataAccessProvider, currentUserId, newUserId)
                Dim toReturn As Integer = spCall.Call()
                Return toReturn
            End Using
        End Function

        Public Shared Function ClearBank(bankId As System.Int32) As Integer
            Using dataAccessProvider As New DataAccessAdapter()
                Return ClearBank(bankId, dataAccessProvider)
            End Using
        End Function

        Public Shared Function ClearBank(bankId As System.Int32, dataAccessProvider As IDataAccessCore) As Integer
            Using spCall As StoredProcedureCall = CreateClearBankCall(dataAccessProvider, bankId)
                Dim toReturn As Integer = spCall.Call()
                Return toReturn
            End Using
        End Function

        Public Shared Function ClearAndDeleteBankHierarchical(bankId As System.Int32) As Integer
            Using dataAccessProvider As New DataAccessAdapter()
                Return ClearAndDeleteBankHierarchical(bankId, dataAccessProvider)
            End Using
        End Function

        Public Shared Function ClearAndDeleteBankHierarchical(bankId As System.Int32, dataAccessProvider As IDataAccessCore) As Integer
            Using spCall As StoredProcedureCall = CreateClearAndDeleteBankHierarchicalCall(dataAccessProvider, bankId)
                Dim toReturn As Integer = spCall.Call()
                Return toReturn
            End Using
        End Function

        Public Shared Function SetMaintenanceWindow(plannedTimestamp As System.DateTime) As Integer
            Using dataAccessProvider As New DataAccessAdapter()
                Return SetMaintenanceWindow(plannedTimestamp, dataAccessProvider)
            End Using
        End Function

        Public Shared Function SetMaintenanceWindow(plannedTimestamp As System.DateTime, dataAccessProvider As IDataAccessCore) As Integer
            Using spCall As StoredProcedureCall = CreateSetMaintenanceWindowCall(dataAccessProvider, plannedTimestamp)
                Dim toReturn As Integer = spCall.Call()
                Return toReturn
            End Using
        End Function

        Public Shared Function UpdateCustomBankPropertyBankId(customBankPropertyId As System.Guid, bankId As System.Int32) As Integer
            Using dataAccessProvider As New DataAccessAdapter()
                Return UpdateCustomBankPropertyBankId(customBankPropertyId, bankId, dataAccessProvider)
            End Using
        End Function

        Public Shared Function UpdateCustomBankPropertyBankId(customBankPropertyId As System.Guid, bankId As System.Int32, dataAccessProvider As IDataAccessCore) As Integer
            Using spCall As StoredProcedureCall = CreateUpdateCustomBankPropertyBankIdCall(dataAccessProvider, customBankPropertyId, bankId)
                Dim toReturn As Integer = spCall.Call()
                Return toReturn
            End Using
        End Function

        Public Shared Function UpdateResourceBankId(resourceId As System.Guid, bankId As System.Int32) As Integer
            Using dataAccessProvider As New DataAccessAdapter()
                Return UpdateResourceBankId(resourceId, bankId, dataAccessProvider)
            End Using
        End Function

        Public Shared Function UpdateResourceBankId(resourceId As System.Guid, bankId As System.Int32, dataAccessProvider As IDataAccessCore) As Integer
            Using spCall As StoredProcedureCall = CreateUpdateResourceBankIdCall(dataAccessProvider, resourceId, bankId)
                Dim toReturn As Integer = spCall.Call()
                Return toReturn
            End Using
        End Function

        Private Shared Function CreateChangeCreatorModifierCall(dataAccessProvider As IDataAccessCore, currentUserId As System.Int32, newUserId As System.Int32) As StoredProcedureCall
            Return New StoredProcedureCall(dataAccessProvider, "[QuestifyBuilder].[dbo].[ChangeCreatorModifier]", "ChangeCreatorModifier") _
                            .AddParameter("@currentUserId", "Int", 0, ParameterDirection.Input, True, 10, 0, currentUserId) _
                            .AddParameter("@newUserId", "Int", 0, ParameterDirection.Input, True, 10, 0, newUserId)
        End Function

        Private Shared Function CreateClearBankCall(dataAccessProvider As IDataAccessCore, bankId As System.Int32) As StoredProcedureCall
            Return New StoredProcedureCall(dataAccessProvider, "[QuestifyBuilder].[dbo].[ClearBank]", "ClearBank") _
                            .AddParameter("@bankId", "Int", 0, ParameterDirection.Input, True, 10, 0, bankId)
        End Function

        Private Shared Function CreateClearAndDeleteBankHierarchicalCall(dataAccessProvider As IDataAccessCore, bankId As System.Int32) As StoredProcedureCall
            Return New StoredProcedureCall(dataAccessProvider, "[QuestifyBuilder].[dbo].[ClearAndDeleteBankHierarchical]", "ClearAndDeleteBankHierarchical") _
                            .AddParameter("@bankId", "Int", 0, ParameterDirection.Input, True, 10, 0, bankId)
        End Function

        Private Shared Function CreateSetMaintenanceWindowCall(dataAccessProvider As IDataAccessCore, plannedTimestamp As System.DateTime) As StoredProcedureCall
            Return New StoredProcedureCall(dataAccessProvider, "[QuestifyBuilder].[dbo].[SetMaintenanceWindow]", "SetMaintenanceWindow") _
                            .AddParameter("@plannedTimestamp", "DateTime", 0, ParameterDirection.Input, True, 0, 0, plannedTimestamp)
        End Function

        Private Shared Function CreateUpdateCustomBankPropertyBankIdCall(dataAccessProvider As IDataAccessCore, customBankPropertyId As System.Guid, bankId As System.Int32) As StoredProcedureCall
            Return New StoredProcedureCall(dataAccessProvider, "[QuestifyBuilder].[dbo].[UpdateCustomBankPropertyBankId]", "UpdateCustomBankPropertyBankId") _
                            .AddParameter("@CustomBankPropertyId", "UniqueIdentifier", 0, ParameterDirection.Input, True, 0, 0, customBankPropertyId) _
                            .AddParameter("@BankId", "Int", 0, ParameterDirection.Input, True, 10, 0, bankId)
        End Function

        Private Shared Function CreateUpdateResourceBankIdCall(dataAccessProvider As IDataAccessCore, resourceId As System.Guid, bankId As System.Int32) As StoredProcedureCall
            Return New StoredProcedureCall(dataAccessProvider, "[QuestifyBuilder].[dbo].[UpdateResourceBankId]", "UpdateResourceBankId") _
                            .AddParameter("@ResourceId", "UniqueIdentifier", 0, ParameterDirection.Input, True, 0, 0, resourceId) _
                            .AddParameter("@BankId", "Int", 0, ParameterDirection.Input, True, 10, 0, bankId)
        End Function



    End Class
End Namespace
