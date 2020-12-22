Imports System.Linq
Imports Questify.Builder.Logic.CustomProperties.Interfaces
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.CustomProperties.Helpers
Imports Questify.Builder.Logic.Service.Model.Entities.Custom

Namespace CustomProperties.ValueConverters

    Public Class TreeStructureCustomPropertyValueConverter
        Implements ICustomPropertyValueConverter

        Public Function GetSelectedValues(customPropertyValue As CustomBankPropertyValueEntity) As IList(Of Guid) Implements ICustomPropertyValueConverter.GetSelectedValues
            Dim treeValue = DirectCast(customPropertyValue, TreeStructureCustomBankPropertyValueEntity)
            If treeValue Is Nothing Then Return Nothing
            If treeValue.TreeStructureCustomBankPropertySelectedPartCollection IsNot Nothing Then
                Return treeValue.TreeStructureCustomBankPropertySelectedPartCollection.Select(Function(cv) cv.TreeStructurePartId).ToList()
            End If
            If treeValue.TreeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart IsNot Nothing Then
                Return treeValue.TreeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart.Select(Function(cv) cv.TreeStructurePartCustomBankPropertyId).ToList()
            End If
            Return Nothing
        End Function

        Public Function GetValuesFromCustomBankProperty(customBankPropery As CustomBankPropertyEntity) As List(Of CustomPropertyValueDto) Implements ICustomPropertyValueConverter.GetValuesFromCustomBankProperty
            Dim tree = DirectCast(customBankPropery, TreeStructureCustomBankPropertyEntity)
            If tree IsNot Nothing Then
                Return tree.TreeStructurePartCustomBankPropertyCollection.Select(Function(treeValue) New CustomPropertyValueDto With {.CustomPropertyValueId = treeValue.TreeStructurePartCustomBankPropertyId, _
                                                                                                                                      .Name = treeValue.Name, _
                                                                                                                                      .Title = treeValue.Title, _
                                                                                                                                      .DisplayValue = treeValue.ToString()}).ToList()
            End If
            Return Nothing
        End Function

        Public Function GetCustomPropertyDisplayValueForResource(customPropertyValue As CustomBankPropertyValueEntity, values As IList(Of CustomPropertyValueDto)) As String Implements ICustomPropertyValueConverter.GetCustomPropertyDisplayValueForResource
            Dim returnValue As String = String.Empty
            Dim treeValue = DirectCast(customPropertyValue, TreeStructureCustomBankPropertyValueEntity)
            If treeValue Is Nothing Then Return returnValue
            If treeValue.TreeStructureCustomBankPropertySelectedPartCollection IsNot Nothing Then
                returnValue = CustomPropertyHelper.GetTreeValueFromCustomBankPropertySelectedParts(treeValue)
            End If
            Return returnValue
        End Function
    End Class

End Namespace