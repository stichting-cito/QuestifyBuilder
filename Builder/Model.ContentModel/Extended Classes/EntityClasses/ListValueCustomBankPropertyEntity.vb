Imports SD.LLBLGen.Pro.ORMSupportClasses
Imports Cito.Tester.Common

Namespace Questify.Builder.Model.ContentModel.EntityClasses

    Partial Public Class ListValueCustomBankPropertyEntity

        Protected Overrides Function CreateValidator() As IValidator
            Return New ValidatorClasses.ListValueCustomBankPropertyValidator
        End Function


        Public Overrides Function ToString() As String
            Return $"{Me.Name} - {Me.Title}"
        End Function

        Protected Overrides Sub OnSetValueComplete(ByVal fieldIndex As Integer)

            If fieldIndex = ListValueCustomBankPropertyFieldIndex.Name Or fieldIndex = ListValueCustomBankPropertyFieldIndex.Title Then

                Dim originalValue As String = DirectCast(Me.Fields(fieldIndex).CurrentValue, String)
                Dim updatedValue As String = StringManipulationHelper.ReplaceTabsAndNewLinesBySpaces(originalValue)

                If updatedValue <> originalValue Then
                    Me.Fields(fieldIndex).CurrentValue = updatedValue
                End If
            End If

            MyBase.OnSetValueComplete(fieldIndex)
        End Sub

        Private Function ReplaceUnwantedCharacters(ByVal value As String) As String
            If value Is Nothing Then Return Nothing
            Return value.Replace(vbCrLf, " ").Replace(vbCr, String.Empty).Replace(vbLf, String.Empty).Replace(vbTab, " ").Trim()
        End Function
    End Class

End Namespace

