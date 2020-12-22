Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.Interfaces

Imports System.Collections.Generic
Imports System.Linq


Namespace Versioning.Retrieval
    Public Class ConceptStructureRetriever
        Inherits MetaDataRetrieverBase(Of List(Of ConceptStructureMetaData))

        Public Sub New(ByVal propertyEntity As IPropertyEntity)
            MyBase.New(propertyEntity)
        End Sub

        Public Overrides Function CreateMetaData() As List(Of ConceptStructureMetaData)
            Dim conceptStructures = New List(Of ConceptStructureMetaData)

            If TypeOf PropertyEntity Is ItemResourceEntity Then
                For Each customBankPropertyValue As ConceptStructureCustomBankPropertyValueEntity In PropertyEntity.CustomBankPropertyValueCollection.OfType(Of ConceptStructureCustomBankPropertyValueEntity)
                    For Each customBankPropertyEntity In customBankPropertyValue.ConceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart
                        Dim metadata = conceptStructures.FirstOrDefault(Function(md) md.Id = customBankPropertyValue.CustomBankPropertyId)
                        If metadata Is Nothing Then
                            metadata = New ConceptStructureMetaData(customBankPropertyValue.CustomBankPropertyId, customBankPropertyValue.CustomBankProperty.Name, New List(Of String), customBankPropertyValue.CustomBankProperty.Version)
                            conceptStructures.Add(metadata)
                        End If
                        metadata.Values.Add($"{customBankPropertyEntity.Code}-{customBankPropertyEntity.Name}")
                    Next
                Next
            End If
            Return conceptStructures
        End Function
    End Class
End Namespace