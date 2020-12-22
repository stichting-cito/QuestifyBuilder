Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.ComponentModel
Imports SD.LLBLGen.Pro.ORMSupportClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports System.Text
Imports Extended_Classes.HelperClasses

Namespace Questify.Builder.Model.ContentModel.ValidatorClasses

    Partial Public Class CustomBankPropertyValidator

        Public Overrides Function ValidateFieldValue(ByVal involvedEntity As IEntityCore, ByVal fieldIndex As Integer, ByVal value As Object) As Boolean
            Dim toReturn As Boolean = True
            Select Case CType(fieldIndex, CustomBankPropertyFieldIndex)
                Case CustomBankPropertyFieldIndex.Name
                    toReturn = ResourceValidator.ValidateNameField(involvedEntity, value)

                Case CustomBankPropertyFieldIndex.Title
                    If Not CType(value, String).Length > 0 Then
                        toReturn = False
                        Dim errorString As String = My.Resources.TitleIsRequired
                        involvedEntity.SetEntityFieldError("Title", errorString, False)
                        involvedEntity.SetEntityError(errorString)

                    End If
                Case Else
                    toReturn = True
            End Select

            Return toReturn
        End Function

        Public Overrides Sub ValidateEntityBeforeSave(ByVal involvedEntity As IEntityCore)
            Dim toValidate As CustomBankPropertyEntity = CType(involvedEntity, CustomBankPropertyEntity)

            If toValidate.IsNew OrElse involvedEntity.GetFieldByName(CustomBankPropertyFields.Name.Name).IsChanged Then

                Using adapter As New DatabaseSpecific.DataAccessAdapter()
                    Dim bankTreeIds() As Integer = BankstructureHelper.GetBankBrancheIds(adapter, toValidate.BankId)

                    Dim filter As IRelationPredicateBucket = New RelationPredicateBucket()
                    With filter
                        .PredicateExpression.Add(CustomBankPropertyFields.Name = toValidate.Name.ToLower())
                        .PredicateExpression.AddWithAnd(New FieldCompareRangePredicate(CustomBankPropertyFields.BankId, Nothing, bankTreeIds))
                    End With
                    If Not toValidate.IsNew Then
                        filter.PredicateExpression.AddWithAnd(HelperClasses.CustomBankPropertyFields.CustomBankPropertyId <> toValidate.CustomBankPropertyId)
                    End If

                    adapter.StartTransaction(IsolationLevel.ReadUncommitted, Guid.NewGuid.ToString().Substring(0, 32))

                    Dim results As New HelperClasses.EntityCollection(Of EntityClasses.CustomBankPropertyEntity)

                    Try
                        adapter.FetchEntityCollection(results, filter, 1)
                        adapter.Commit()
                    Catch ex As Exception
                        Throw ex
                    End Try

                    If results.Count > 0 Then
                        Throw New ORMEntityValidationException(String.Format(My.Resources.ACustomPropertyWithTheSameCodeAlreadyExists, toValidate.Name), toValidate)
                    End If

                End Using

            End If

            MyBase.ValidateEntityBeforeSave(involvedEntity)
        End Sub

        Public Overrides Sub ValidateEntity(involvedEntity As IEntityCore)

            Dim sb As New StringBuilder

            For Each field As IEntityField2 In involvedEntity.Fields
                Dim possibleErr As String = DirectCast(involvedEntity, IDataErrorInfo)(field.Name)
                If (Not String.IsNullOrEmpty(possibleErr)) Then
                    sb.Append(possibleErr + ";")
                End If
            Next

            involvedEntity.SetEntityError(sb.ToString())

            MyBase.ValidateEntity(involvedEntity)
        End Sub

    End Class

End Namespace

