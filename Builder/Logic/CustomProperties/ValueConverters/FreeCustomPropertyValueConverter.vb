

Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.CustomProperties.Interfaces
Imports Questify.Builder.Logic.Service.Model.Entities.Custom

Namespace CustomProperties.ValueConverters

    Public Class FreeCustomPropertyValueConverter
        Implements ICustomPropertyValueConverter

        Public Function GetSelectedValues(customPropertyValue As CustomBankPropertyValueEntity) As IList(Of Guid) Implements ICustomPropertyValueConverter.GetSelectedValues
            Return Nothing
        End Function

        Public Function GetValuesFromCustomBankProperty(customBankPropery As CustomBankPropertyEntity) As List(Of CustomPropertyValueDto) Implements ICustomPropertyValueConverter.GetValuesFromCustomBankProperty
            Return Nothing
        End Function

        Public Function GetCustomPropertyDisplayValueForResource(customPropertyValue As CustomBankPropertyValueEntity, values As IList(Of CustomPropertyValueDto)) As String Implements ICustomPropertyValueConverter.GetCustomPropertyDisplayValueForResource
            Dim freeValue = DirectCast(customPropertyValue, FreeValueCustomBankPropertyValueEntity)
            Return freeValue.Value
        End Function
    End Class

End Namespace