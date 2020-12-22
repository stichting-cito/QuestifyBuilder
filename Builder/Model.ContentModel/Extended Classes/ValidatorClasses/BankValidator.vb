Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.ValidatorClasses

    Partial Public Class BankValidator

        Public Overrides Function ValidateFieldValue(ByVal involvedEntity As IEntityCore, ByVal fieldIndex As Integer, ByVal value As Object) As Boolean
            Dim toReturn As Boolean = True
            Select Case CType(fieldIndex, BankFieldIndex)
                Case BankFieldIndex.Name
                    If Not CType(value, String).Length > 0 Then
                        toReturn = False
                        Dim errorString As String = My.Resources.NameIsRequired
                        involvedEntity.SetEntityFieldError("Name", errorString, False)
                        involvedEntity.SetEntityError(errorString)
                        toReturn = False
                    Else
                        involvedEntity.SetEntityError(String.Empty)
                    End If
                Case Else
                    toReturn = True
            End Select
            Return toReturn
        End Function

        Public Overrides Sub ValidateEntityBeforeSave(ByVal involvedEntity As IEntityCore)
            Dim toValidate As BankEntity = CType(involvedEntity, BankEntity)


            MyBase.ValidateEntityBeforeSave(involvedEntity)
        End Sub


        Public Overrides Sub ValidateEntity(involvedEntity As SD.LLBLGen.Pro.ORMSupportClasses.IEntityCore)

            Dim sb As New Text.StringBuilder

            For Each field As IEntityField2 In involvedEntity.Fields
                Dim possibleErr As String = DirectCast(involvedEntity, ComponentModel.IDataErrorInfo)(field.Name)
                If (Not String.IsNullOrEmpty(possibleErr)) Then
                    sb.Append(possibleErr + ";")
                End If
            Next

            involvedEntity.SetEntityError(sb.ToString())

            MyBase.ValidateEntity(involvedEntity)
        End Sub

    End Class

End Namespace
