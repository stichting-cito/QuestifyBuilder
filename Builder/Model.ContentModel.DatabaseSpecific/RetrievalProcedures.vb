Imports System
Imports System.Data
Imports System.Collections.Generic
Imports SD.LLBLGen.Pro.ORMSupportClasses
Imports SD.LLBLGen.Pro.QuerySpec.Adapter

Namespace Questify.Builder.Model.ContentModel.DatabaseSpecific
    Public Class RetrievalProcedures
        Private Sub New()
        End Sub




        Public Shared Function GetBankStatistics(bankId As System.Int32, userName As System.String) As DataSet
            Using dataAccessProvider As New DataAccessAdapter()
                Return GetBankStatistics(bankId, userName, dataAccessProvider)
            End Using
        End Function

        Public Shared Function GetBankStatistics(bankId As System.Int32, userName As System.String, dataAccessProvider As IDataAccessCore) As DataSet
            Using spCall As StoredProcedureCall = CreateGetBankStatisticsCall(dataAccessProvider, bankId, userName)
                Dim toReturn As DataSet = spCall.FillDataSet()
                Return toReturn
            End Using
        End Function

        Public Shared Function GetGetBankStatisticsCallAsQuery(bankId As System.Int32, userName As System.String) As IRetrievalQuery
            Using dataAccessProvider As New DataAccessAdapter()
                Return GetGetBankStatisticsCallAsQuery(bankId, userName, dataAccessProvider)
            End Using
        End Function

        Public Shared Function GetGetBankStatisticsCallAsQuery(bankId As System.Int32, userName As System.String, dataAccessProvider As IDataAccessCore) As IRetrievalQuery
            Return CreateGetBankStatisticsCall(dataAccessProvider, bankId, userName).ToRetrievalQuery()
        End Function


        Public Shared Function GetMaintenanceWindow() As DataTable
            Using dataAccessProvider As New DataAccessAdapter()
                Return GetMaintenanceWindow(dataAccessProvider)
            End Using
        End Function

        Public Shared Function GetMaintenanceWindow(dataAccessProvider As IDataAccessCore) As DataTable
            Using spCall As StoredProcedureCall = CreateGetMaintenanceWindowCall(dataAccessProvider)
                Dim toReturn As DataTable = spCall.FillDataTable()
                Return toReturn
            End Using
        End Function

        Public Shared Function GetGetMaintenanceWindowCallAsQuery() As IRetrievalQuery
            Using dataAccessProvider As New DataAccessAdapter()
                Return GetGetMaintenanceWindowCallAsQuery(dataAccessProvider)
            End Using
        End Function

        Public Shared Function GetGetMaintenanceWindowCallAsQuery(dataAccessProvider As IDataAccessCore) As IRetrievalQuery
            Return CreateGetMaintenanceWindowCall(dataAccessProvider).ToRetrievalQuery()
        End Function


        Public Shared Function HasDependingResourcesInSubBanks(bankId As System.Int32) As DataTable
            Using dataAccessProvider As New DataAccessAdapter()
                Return HasDependingResourcesInSubBanks(bankId, dataAccessProvider)
            End Using
        End Function

        Public Shared Function HasDependingResourcesInSubBanks(bankId As System.Int32, dataAccessProvider As IDataAccessCore) As DataTable
            Using spCall As StoredProcedureCall = CreateHasDependingResourcesInSubBanksCall(dataAccessProvider, bankId)
                Dim toReturn As DataTable = spCall.FillDataTable()
                Return toReturn
            End Using
        End Function

        Public Shared Function GetHasDependingResourcesInSubBanksCallAsQuery(bankId As System.Int32) As IRetrievalQuery
            Using dataAccessProvider As New DataAccessAdapter()
                Return GetHasDependingResourcesInSubBanksCallAsQuery(bankId, dataAccessProvider)
            End Using
        End Function

        Public Shared Function GetHasDependingResourcesInSubBanksCallAsQuery(bankId As System.Int32, dataAccessProvider As IDataAccessCore) As IRetrievalQuery
            Return CreateHasDependingResourcesInSubBanksCall(dataAccessProvider, bankId).ToRetrievalQuery()
        End Function


        Private Shared Function CreateGetBankStatisticsCall(dataAccessProvider As IDataAccessCore, bankId As System.Int32, userName As System.String) As StoredProcedureCall
            Return New StoredProcedureCall(dataAccessProvider, "[QuestifyBuilder].[dbo].[GetBankStatistics]", "GetBankStatistics") _
                            .AddParameter("@bankId", "Int", 0, ParameterDirection.Input, True, 10, 0, bankId) _
                            .AddParameter("@userName", "VarChar", 50, ParameterDirection.Input, True, 0, 0, userName)
        End Function

        Private Shared Function CreateGetMaintenanceWindowCall(dataAccessProvider As IDataAccessCore) As StoredProcedureCall
            Return New StoredProcedureCall(dataAccessProvider, "[QuestifyBuilder].[dbo].[GetMaintenanceWindow]", "GetMaintenanceWindow")
        End Function

        Private Shared Function CreateHasDependingResourcesInSubBanksCall(dataAccessProvider As IDataAccessCore, bankId As System.Int32) As StoredProcedureCall
            Return New StoredProcedureCall(dataAccessProvider, "[QuestifyBuilder].[dbo].[HasDependingResourcesInSubBanks]", "HasDependingResourcesInSubBanks") _
                            .AddParameter("@bankId", "Int", 0, ParameterDirection.Input, True, 10, 0, bankId)
        End Function



    End Class
End Namespace
