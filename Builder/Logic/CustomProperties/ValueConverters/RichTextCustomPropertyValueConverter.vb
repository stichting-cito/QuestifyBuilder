Imports Questify.Builder.Logic.CustomProperties.Interfaces
Imports Questify.Builder.Logic.Service.Model.Entities.Custom
Imports Questify.Builder.Model.ContentModel.EntityClasses

Namespace CustomProperties.ValueConverters

    Public Class RichTextCustomPropertyValueConverter
        Implements ICustomPropertyValueConverter

        Public Function GetSelectedValues(customPropertyValue As CustomBankPropertyValueEntity) As IList(Of Guid) Implements ICustomPropertyValueConverter.GetSelectedValues
            Return Nothing
        End Function

        Public Function GetValuesFromCustomBankProperty(customBankPropery As CustomBankPropertyEntity) As List(Of CustomPropertyValueDto) Implements ICustomPropertyValueConverter.GetValuesFromCustomBankProperty
            Return Nothing
        End Function

        Public Function GetCustomPropertyDisplayValueForResource(customPropertyValue As CustomBankPropertyValueEntity, values As IList(Of CustomPropertyValueDto)) As String Implements ICustomPropertyValueConverter.GetCustomPropertyDisplayValueForResource
            Dim freeValue = DirectCast(customPropertyValue, RichTextValueCustomBankPropertyValueEntity)
            Return If(String.IsNullOrWhiteSpace(freeValue.Value), String.Empty, My.Resources.IsEnteredMessage)
        End Function
    End Class

End Namespace