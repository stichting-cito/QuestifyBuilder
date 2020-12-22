Imports System.Linq
Imports Questify.Builder.Logic.CustomProperties.Interfaces
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.CustomProperties.Helpers
Imports Questify.Builder.Logic.Service.Model.Entities.Custom

Namespace CustomProperties.ValueConverters

    Public Class ConceptStructureCustomPropertyValueConverter
        Implements ICustomPropertyValueConverter

        Public Function GetSelectedValues(customPropertyValue As CustomBankPropertyValueEntity) As IList(Of Guid) Implements ICustomPropertyValueConverter.GetSelectedValues
            Dim conceptValue = DirectCast(customPropertyValue, ConceptStructureCustomBankPropertyValueEntity)
            If conceptValue Is Nothing Then Return Nothing
            If conceptValue.ConceptStructureCustomBankPropertySelectedPartCollection IsNot Nothing Then
                Return conceptValue.ConceptStructureCustomBankPropertySelectedPartCollection.Select(Function(cv) cv.ConceptStructurePartId).ToList()
            End If
            If conceptValue.ConceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart IsNot Nothing Then
                Return conceptValue.ConceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart.Select(Function(cv) cv.ConceptStructurePartCustomBankPropertyId).ToList()
            End If
            Return Nothing
        End Function

        Public Function GetValuesFromCustomBankProperty(customBankPropery As CustomBankPropertyEntity) As List(Of CustomPropertyValueDto) Implements ICustomPropertyValueConverter.GetValuesFromCustomBankProperty
            Dim concept = DirectCast(customBankPropery, ConceptStructureCustomBankPropertyEntity)
            If concept IsNot Nothing Then
                Return concept.ConceptStructurePartCustomBankPropertyCollection.Select(Function(conceptValue) New CustomPropertyValueDto With {.CustomPropertyValueId = conceptValue.ConceptStructurePartCustomBankPropertyId,
                                                                                                                                               .Name = conceptValue.Name,
                                                                                                                                               .Title = conceptValue.Title,
                                                                                                                                               .DisplayValue = conceptValue.ToString()}).ToList()
            End If
            Return Nothing
        End Function

        Public Function GetCustomPropertyDisplayValueForResource(customPropertyValue As CustomBankPropertyValueEntity, values As IList(Of CustomPropertyValueDto)) As String Implements ICustomPropertyValueConverter.GetCustomPropertyDisplayValueForResource
            Dim returnValue = String.Empty
            Dim conceptValue = DirectCast(customPropertyValue, ConceptStructureCustomBankPropertyValueEntity)
            If conceptValue Is Nothing Then Return returnValue

            If conceptValue.ConceptStructureCustomBankPropertySelectedPartCollection IsNot Nothing Then
                returnValue = CustomPropertyHelper.GetValueFromSelectedValueCollection(conceptValue.ConceptStructureCustomBankPropertySelectedPartCollection.Select(Function(cv) cv.ConceptStructurePartId), values)
            End If
            Return returnValue
        End Function
    End Class

End Namespace