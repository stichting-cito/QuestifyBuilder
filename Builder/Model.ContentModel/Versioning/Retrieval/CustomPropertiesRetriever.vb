Imports System.Linq
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.Interfaces
Imports System.Collections.Generic
Namespace Versioning.Retrieval
    Public Class CustomPropertiesRetriever
        Inherits MetaDataRetrieverBase(Of List(Of CustomBankPropertyMetaData))

        Public Sub New(ByVal propertyEntity As IPropertyEntity)
            MyBase.New(propertyEntity)
        End Sub

        Public Overrides Function CreateMetaData() As List(Of CustomBankPropertyMetaData)
            Dim customProperties As New List(Of CustomBankPropertyMetaData)()

            If TypeOf PropertyEntity Is ItemResourceEntity OrElse TypeOf PropertyEntity Is AssessmentTestResourceEntity OrElse TypeOf PropertyEntity Is GenericResourceEntity Then
                For Each customBankPropertyValue As CustomBankPropertyValueEntity In PropertyEntity.CustomBankPropertyValueCollection
                    If TypeOf customBankPropertyValue Is ListCustomBankPropertyValueEntity Then
                        Dim listCustomBankPropertyValueEntity As ListCustomBankPropertyValueEntity = CType(customBankPropertyValue, ListCustomBankPropertyValueEntity)

                        For Each listValueCustomBankPropertyEntity As ListValueCustomBankPropertyEntity In listCustomBankPropertyValueEntity.ListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue
                            If listValueCustomBankPropertyEntity.CustomBankPropertyId = customBankPropertyValue.CustomBankPropertyId Then
                                Dim customBankProperty As CustomBankPropertyMetaData = customProperties.FirstOrDefault(Function(i) i.Id = listValueCustomBankPropertyEntity.CustomBankPropertyId)

                                If customBankProperty Is Nothing Then
                                    customProperties.Add(New CustomBankPropertyMetaData(listValueCustomBankPropertyEntity.CustomBankPropertyId, listCustomBankPropertyValueEntity.CustomBankProperty.Name, New List(Of String) From {listValueCustomBankPropertyEntity.Name}, Nothing))
                                Else
                                    customBankProperty.Values.Add(listValueCustomBankPropertyEntity.Name)
                                End If
                            End If
                        Next
                    ElseIf TypeOf customBankPropertyValue Is FreeValueCustomBankPropertyValueEntity Then
                        Dim freeValueBankPropertyValue As FreeValueCustomBankPropertyValueEntity = CType(customBankPropertyValue, FreeValueCustomBankPropertyValueEntity)
                        Dim freeValueName As String = String.Empty

                        If freeValueBankPropertyValue.CustomBankProperty Is Nothing Then
                            freeValueName = FindFreeValueName(PropertyEntity.Bank, freeValueBankPropertyValue.CustomBankPropertyId)
                        Else
                            freeValueName = freeValueBankPropertyValue.CustomBankProperty.Name
                        End If

                        customProperties.Add(New CustomBankPropertyMetaData(freeValueBankPropertyValue.CustomBankPropertyId, freeValueName, freeValueBankPropertyValue.Value, Nothing))
                    End If
                Next
            End If

            Return customProperties
        End Function

        Private Function FindFreeValueName(ByVal bank As BankEntity, ByVal customBankPropertyId As Guid) As String
            If bank Is Nothing Then
                Return Nothing
            End If
            Dim customBankPropertyEntity As CustomBankPropertyEntity = bank.CustomBankPropertyCollection.FirstOrDefault(Function(i) i.CustomBankPropertyId = customBankPropertyId)

            If customBankPropertyEntity IsNot Nothing Then
                Return customBankPropertyEntity.Name
            Else
                If bank.ParentBank IsNot Nothing Then
                    Return FindFreeValueName(bank.ParentBank, customBankPropertyId)
                End If
            End If

            Return Nothing
        End Function

    End Class
End Namespace