Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.Interfaces
Imports System.Collections.Generic
Imports System.Linq


Namespace Versioning.Retrieval
    Public Class TreeStructureRetriever
        Inherits MetaDataRetrieverBase(Of List(Of TreeStructureMetaData))

        Public Sub New(ByVal propertyEntity As IPropertyEntity)
            MyBase.New(propertyEntity)
        End Sub

        Public Overrides Function CreateMetaData() As List(Of TreeStructureMetaData)
            Dim treeStructureList = New List(Of TreeStructureMetaData)()

            If TypeOf PropertyEntity Is ItemResourceEntity OrElse TypeOf PropertyEntity Is AssessmentTestResourceEntity OrElse TypeOf PropertyEntity Is GenericResourceEntity Then
                For Each customBankPropertyValue As TreeStructureCustomBankPropertyValueEntity In PropertyEntity.CustomBankPropertyValueCollection.OfType(Of TreeStructureCustomBankPropertyValueEntity)
                    For Each customBankPropertyEntity In customBankPropertyValue.TreeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart
                        Dim metadata = treeStructureList.FirstOrDefault(Function(md) md.Id = customBankPropertyValue.CustomBankPropertyId)
                        If metadata Is Nothing Then
                            metadata = New TreeStructureMetaData(customBankPropertyValue.CustomBankPropertyId, customBankPropertyValue.CustomBankProperty.Name, New List(Of String), customBankPropertyValue.CustomBankProperty.Version)
                            treeStructureList.Add(metadata)
                        End If
                        metadata.Values.Add($"{customBankPropertyEntity.Code}-{customBankPropertyEntity.Name}")
                    Next
                Next
            End If
            Return treeStructureList
        End Function
    End Class
End Namespace