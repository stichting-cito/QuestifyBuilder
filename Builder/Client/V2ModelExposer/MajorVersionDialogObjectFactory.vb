Imports System.ComponentModel.Composition
Imports System.Linq
Imports Cito.Tester.Common

Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Questify.Builder.Model.ContentModel.Interfaces
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Interfaces

<Export(GetType(IMajorVersionDialogObjectFactory))>
Public Class MajorVersionDialogObjectFactory
    Implements IMajorVersionDialogObjectFactory

    Public Function GetRequiredObjectForPropertyEntity(id As Guid, type As Type) As IPropertyEntity Implements IMajorVersionDialogObjectFactory.GetRequiredObjectForPropertyEntity
        Select Case type
            Case GetType(TestPackageResourceEntity)
                Return ResourceFactory.Instance.GetTestPackage(New TestPackageResourceEntity(id))
            Case GetType(AssessmentTestResourceEntity)
                Return ResourceFactory.Instance.GetAssessmentTest(New AssessmentTestResourceEntity(id))
            Case GetType(ItemResourceEntity)
                Return ResourceFactory.Instance.GetItem(New ItemResourceEntity(id), New ResourceRequestDTO())
            Case GetType(GenericResourceEntity)
                Return ResourceFactory.Instance.GetGenericResource(New GenericResourceEntity(id))
            Case GetType(DataSourceResourceEntity)
                Return ResourceFactory.Instance.GetDataSource(New DataSourceResourceEntity(id))
            Case GetType(ControlTemplateResourceEntity)
                Return ResourceFactory.Instance.GetControlTemplate(New ControlTemplateResourceEntity(id))
            Case GetType(ItemLayoutTemplateResourceEntity)
                Return ResourceFactory.Instance.GetItemLayoutTemplate(New ItemLayoutTemplateResourceEntity(id))
            Case GetType(AspectResourceEntity)
                Return ResourceFactory.Instance.GetAspect(New AspectResourceEntity(id))
            Case GetType(FreeValueCustomBankPropertyEntity)
                Return BankFactory.Instance.GetFreeValueCustomBankProperty(New FreeValueCustomBankPropertyEntity(id))
            Case GetType(ListCustomBankPropertyEntity)
                Return BankFactory.Instance.GetListCustomBankProperty(New ListCustomBankPropertyEntity(id))
            Case GetType(ConceptStructureCustomBankPropertyEntity)
                Return BankFactory.Instance.GetConceptStructureCustomBankProperty(New ConceptStructureCustomBankPropertyEntity(id))
        End Select

        Throw New ArgumentException("Type not supported. Type: " & type.Name)
    End Function

    Public Function UpdateMajorVersion(propertyEntity As IPropertyEntity) As String Implements IMajorVersionDialogObjectFactory.UpdateMajorVersion
        Throw New NotImplementedException()
    End Function

    Public Function GetLastResourceHistoryEntityForResource(propertyEntity As IPropertyEntity) As ResourceHistoryEntity Implements IMajorVersionDialogObjectFactory.GetLastResourceHistoryEntityForResource
        Dim col As EntityCollection = ResourceFactory.Instance.GetResourceHistoryForResource(propertyEntity.Id)
        If (col IsNot Nothing AndAlso col.Any()) Then
            Return col.OfType(Of ResourceHistoryEntity)().OrderByDescending(Function(his) his.ModifiedDate).First()
        End If

        Throw New ArgumentException(
            $"No history found for Resource  of type {propertyEntity.GetType()} with id '{propertyEntity.Id}'")
    End Function
End Class
