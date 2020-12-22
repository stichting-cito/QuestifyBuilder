Imports Questify.Builder.Logic.Service.Model.Entities.Custom
Imports Questify.Builder.Model.ContentModel.EntityClasses

Namespace CustomProperties.Interfaces

    Public Interface ICustomPropertyValueConverter
        Function GetValuesFromCustomBankProperty(customBankPropery As CustomBankPropertyEntity) As List(Of CustomPropertyValueDto)
        Function GetSelectedValues(customPropertyValue As CustomBankPropertyValueEntity) As IList(Of Guid)
        Function GetCustomPropertyDisplayValueForResource(customPropertyValue As CustomBankPropertyValueEntity, values As IList(Of CustomPropertyValueDto)) As String
    End Interface

End Namespace