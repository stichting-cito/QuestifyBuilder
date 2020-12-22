#If Not CF Then
#End If
Imports Questify.Builder.Model.ContentModel.HelperClasses

Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.EntityClasses

    Partial Public Class BankEntity

        Protected Overrides Function CreateValidator() As IValidator
            Return New ValidatorClasses.BankValidator()
        End Function


        Public Overridable Function ContainsBankInChildHierarchy(ByVal bankToFind As BankEntity) As Boolean
            Return ContainsBankInChildHierarchy(bankToFind, Me.BankCollection)
        End Function

        Private Function ContainsBankInChildHierarchy(ByVal bankToFind As BankEntity, ByVal banksToSearch As EntityCollection(Of BankEntity)) As Boolean
            For Each bank As BankEntity In banksToSearch
                If bank.Equals(bankToFind) Then Return True
                If ContainsBankInChildHierarchy(bankToFind, bank.BankCollection) Then Return True
            Next

            Return False
        End Function

    End Class

End Namespace
