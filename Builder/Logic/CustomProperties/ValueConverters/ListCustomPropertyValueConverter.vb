Imports System.Linq
Imports Questify.Builder.Logic.CustomProperties.Interfaces
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.Text
Imports Questify.Builder.Logic.CustomProperties.Helpers
Imports Questify.Builder.Logic.Service.Model.Entities.Custom

Namespace CustomProperties.ValueConverters

    Public Class ListCustomPropertyValueConverter
        Implements ICustomPropertyValueConverter

        Public Function GetSelectedValues(customPropertyValue As CustomBankPropertyValueEntity) As IList(Of Guid) Implements ICustomPropertyValueConverter.GetSelectedValues
            Dim listValue = DirectCast(customPropertyValue, ListCustomBankPropertyValueEntity)
            If listValue Is Nothing Then Return Nothing
            If listValue.ListCustomBankPropertySelectedValueCollection IsNot Nothing Then
                Return listValue.ListCustomBankPropertySelectedValueCollection.Select(Function(lv) lv.ListValueBankCustomPropertyId).ToList()
            End If
            If listValue.ListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue IsNot Nothing Then
                Return listValue.ListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue.Select(Function(lv) lv.ListValueBankCustomPropertyId).ToList()
            End If
            Return Nothing
        End Function

        Public Function GetValuesFromCustomBankProperty(customBankPropery As CustomBankPropertyEntity) As List(Of CustomPropertyValueDto) Implements ICustomPropertyValueConverter.GetValuesFromCustomBankProperty
            Dim listProp = DirectCast(customBankPropery, ListCustomBankPropertyEntity)
            If listProp IsNot Nothing Then
                Return listProp.ListValueCustomBankPropertyCollection.Select(Function(listValue) New CustomPropertyValueDto() With {.CustomPropertyValueId = listValue.ListValueBankCustomPropertyId, _
                                                                                                                                    .Name = listValue.Name, _
                                                                                                                                    .Title = listValue.Title, _
                                                                                                                                    .DisplayValue = listValue.ToString()}).ToList()
            End If
            Return Nothing
        End Function

        Private Function GetValueFromListValueCustomBankPropertyCollection(listValues As IEnumerable(Of ListValueCustomBankPropertyEntity)) As String
            Dim displayText = New StringBuilder(String.Empty)
            For Each listValue In listValues
                If displayText.Length > 0 Then displayText.Append(";")
                displayText.Append(listValue)
            Next
            Return displayText.ToString()
        End Function

        Public Function GetCustomPropertyDisplayValueForResource(customPropertyValue As CustomBankPropertyValueEntity, values As IList(Of CustomPropertyValueDto)) As String Implements ICustomPropertyValueConverter.GetCustomPropertyDisplayValueForResource
            Dim returnValue As String = String.Empty
            Dim listValue = DirectCast(customPropertyValue, ListCustomBankPropertyValueEntity)
            If listValue Is Nothing Then Return returnValue
            If listValue.ListCustomBankPropertySelectedValueCollection IsNot Nothing Then
                returnValue = CustomPropertyHelper.GetValueFromSelectedValueCollection(listValue.ListCustomBankPropertySelectedValueCollection.Select(Function(lv) lv.ListValueBankCustomPropertyId), values)
            End If
            If String.IsNullOrEmpty(returnValue) AndAlso listValue.ListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue IsNot Nothing Then
                returnValue = GetValueFromListValueCustomBankPropertyCollection(listValue.ListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue)
            End If
            Return returnValue
        End Function
    End Class

End Namespace