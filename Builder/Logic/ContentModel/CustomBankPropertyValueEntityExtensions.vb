Imports System.Runtime.CompilerServices
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.Linq
Imports Questify.Builder.Logic.CustomProperties.Helpers
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Model.Entities
Imports Questify.Builder.Logic.Service.Model.Entities.Custom

Namespace ContentModel
    Public Module CustomBankPropertyValueEntityExtensions

        <Extension>
        Public Function GetCustomPropertyDisplayValue(customPropertyValue As CustomBankPropertyValueEntity) As String
            Return DetermineCustomPropertyDisplayValue(customPropertyValue, Nothing, Nothing)
        End Function

        <Extension>
        Public Function GetCustomPropertyDisplayValue(customPropertyValue As CustomBankPropertyValueEntity, customProperty As CustomBankPropertyEntity) As String
            Return DetermineCustomPropertyDisplayValue(customPropertyValue, customProperty, Nothing)
        End Function

        <Extension>
        Public Function GetCustomPropertyDisplayValue(customPropertyValue As CustomBankPropertyValueEntity, customBankProperties As List(Of CustomBankPropertyDto)) As String
            Return customPropertyValue.GetCustomPropertyDisplayValue(Nothing, customBankProperties)
        End Function

        <Extension>
        Public Function GetCustomPropertyDisplayValue(customPropertyValue As CustomBankPropertyValueEntity, customProperty As CustomBankPropertyEntity, customBankProperties As List(Of CustomBankPropertyDto)) As String
            Dim customBankPropertyValues As IEnumerable(Of CustomPropertyValueDto) = New List(Of CustomPropertyValueDto)()
            customBankPropertyValues = customBankProperties.Where(Function(cv) cv.Values IsNot Nothing).Aggregate(customBankPropertyValues, Function(current, c) current.Union(c.Values))
            Return DetermineCustomPropertyDisplayValue(customPropertyValue, customProperty, customBankPropertyValues.ToList)
        End Function

        <Extension>
        Public Function GetCustomPropertyDisplayValue(customPropertyValue As CustomBankPropertyValueEntity, customProperty As CustomBankPropertyEntity, customBankPropertyValues As List(Of CustomPropertyValueDto)) As String
            Return DetermineCustomPropertyDisplayValue(customPropertyValue, customProperty, customBankPropertyValues)
        End Function

        <Extension>
        Public Sub SetCustomPropertyDisplayValue(customPropertyValue As CustomBankPropertyValueEntity)
            Dim displayValue As String = customPropertyValue.GetCustomPropertyDisplayValue()
            customPropertyValue.SetCustomPropertyDisplayValue(displayValue)
        End Sub

        <Extension>
        Public Sub SetCustomPropertyDisplayValue(customPropertyValue As CustomBankPropertyValueEntity, customProperty As CustomBankPropertyEntity)
            Dim displayValue As String = customPropertyValue.GetCustomPropertyDisplayValue(customProperty)
            customPropertyValue.SetCustomPropertyDisplayValue(displayValue)
        End Sub

        <Extension>
        Public Sub SetCustomPropertyDisplayValue(customPropertyValue As CustomBankPropertyValueEntity, customBankProperties As List(Of CustomBankPropertyDto))
            Dim displayValue As String = customPropertyValue.GetCustomPropertyDisplayValue(customBankProperties)
            customPropertyValue.SetCustomPropertyDisplayValue(displayValue)
        End Sub

        <Extension>
        Public Sub SetCustomPropertyDisplayValue(customPropertyValue As CustomBankPropertyValueEntity, customProperty As CustomBankPropertyEntity, customBankProperties As List(Of CustomBankPropertyDto))
            Dim displayValue As String = customPropertyValue.GetCustomPropertyDisplayValue(customProperty, customBankProperties)
            customPropertyValue.SetCustomPropertyDisplayValue(displayValue)
        End Sub

        <Extension>
        Public Sub SetCustomPropertyDisplayValue(customPropertyValue As CustomBankPropertyValueEntity, displayValue As String)
            customPropertyValue.DisplayValue = displayValue.TruncateWithEllipsis(255)
        End Sub

        Private Function DetermineCustomPropertyDisplayValue(customPropertyValue As CustomBankPropertyValueEntity, customProperty As CustomBankPropertyEntity, customBankPropertyValues As List(Of CustomPropertyValueDto)) As String
            Dim customPropertyHelper = New CustomPropertyHelper()
            If customProperty Is Nothing Then customProperty = BankFactory.Instance.GetCustomBankProperty(customPropertyValue.CustomBankPropertyId)
            Dim converter = customPropertyHelper.GetCustomBankConverter(customProperty)
            If customBankPropertyValues Is Nothing Then customBankPropertyValues = converter.GetValuesFromCustomBankProperty(customProperty)
            If converter IsNot Nothing Then Return converter.GetCustomPropertyDisplayValueForResource(customPropertyValue, customBankPropertyValues)
            Return String.Empty
        End Function

    End Module
End Namespace
