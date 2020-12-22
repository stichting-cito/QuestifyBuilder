Imports System.Collections.Generic
Imports System.Linq
Imports Questify.Builder.Model.ContentModel.DatabaseSpecific
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Extended_Classes.HelperClasses

    Public Class BankstructureHelper

        Public Shared Function GetBankBrancheIds(ByVal adapter As DataAccessAdapter, ByVal bankId As Integer) As Integer()
            Dim bankIdArray As New List(Of Integer)

            Dim allBanks As New EntityCollection(Of BankEntity)
            Dim filter As IRelationPredicateBucket = New RelationPredicateBucket()
            adapter.FetchEntityCollection(allBanks, filter)

            Dim bank = allBanks(allBanks.FindMatches(New PredicateExpression(BankFields.Id = bankId)).FirstOrDefault)
            Dim tempBank = bank
            bankIdArray.Add(tempBank.Id)

            Do While tempBank.ParentBankId.HasValue
                Dim results As List(Of Integer) = allBanks.FindMatches(New PredicateExpression(BankFields.Id = tempBank.ParentBankId))
                If results.Count = 1 Then
                    tempBank = allBanks(results(0))
                    bankIdArray.Add(tempBank.Id)
                Else
                    Throw New ApplicationException($"Unexpected number of banks when parentbank with id '{tempBank.ParentBankId}' was searched in collection of all banks. Count = {results.Count}")
                End If
            Loop

            WalkBankTreeToLeaf(bank, allBanks, bankIdArray)

            Return bankIdArray.ToArray()
        End Function


        Private Shared Sub WalkBankTreeToLeaf(ByVal rootBank As BankEntity, ByVal allBankCollection As EntityCollection(Of BankEntity), ByVal bankIdArray As List(Of Integer))
            Dim results As List(Of Integer) = allBankCollection.FindMatches(New PredicateExpression(BankFields.ParentBankId = rootBank.Id))
            For Each childBankId As Integer In results
                bankIdArray.Add(allBankCollection(childBankId).Id)

                WalkBankTreeToLeaf(allBankCollection(childBankId), allBankCollection, bankIdArray)
            Next
        End Sub
    End Class
End NameSpace