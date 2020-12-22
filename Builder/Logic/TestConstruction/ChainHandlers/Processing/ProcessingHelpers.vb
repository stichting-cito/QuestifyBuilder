Imports System.Linq
Imports Cito.Tester.Common

Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.TestConstruction.Requests
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Model.Entities

Namespace TestConstruction.ChainHandlers.Processing
    Public NotInheritable Class ProcessingHelpers


        Private Sub New()
        End Sub




        Public Shared Function GetItemResources(ByVal requestData As TestConstructionRequest, ByVal bankId As Integer) As IDictionary(Of String, ItemResourceEntity)
            Dim itemCodes As IList(Of String) = CType(Datasources.ResourceRef.ToIdentifierList(requestData.Items), IList(Of String))
            Dim dict As New Dictionary(Of String, ItemResourceEntity)
            Dim request = New ItemResourceRequestDTO() With {.WithDependencies = True}

            For Each itemEntity As ItemResourceEntity In ResourceFactory.Instance.GetItemsByCodes(itemCodes.ToList, bankId, request)
                dict.Add(itemEntity.Name, itemEntity)
            Next

            Return dict
        End Function


        Public Overloads Shared Function GetItemResourceRefList(ByVal test As AssessmentTest2) As List(Of Datasources.ResourceRef)
            Dim itemContext As New List(Of Datasources.ResourceRef)

            For Each itemref As ItemReference2 In test.GetAllItemReferencesInTest
                Dim ref As New Datasources.ResourceRef
                ref.Identifier = itemref.Title

                itemContext.Add(ref)
            Next

            Return itemContext
        End Function

        Public Overloads Shared Function GetItemResourceRefList(ByVal itemReferences As IEnumerable(Of ItemReference2)) As List(Of Datasources.ResourceRef)
            Dim itemContext As New List(Of Datasources.ResourceRef)

            For Each itemref As ItemReference2 In itemReferences
                Dim ref As New Datasources.ResourceRef
                ref.Identifier = itemref.SourceName

                itemContext.Add(ref)
            Next

            Return itemContext
        End Function


        Public Overloads Shared Function GetItemResourceRefList(ByVal itemResources As IEnumerable(Of String)) As List(Of Datasources.ResourceRef)
            Dim items As New List(Of Datasources.ResourceRef)

            For Each itemEntity In itemResources
                Dim ref As New Datasources.ResourceRef
                ref.Identifier = itemEntity

                items.Add(ref)
            Next

            Return items
        End Function


        Public Overloads Shared Function GetItemResourceRefList(ByVal itemResources As IEnumerable(Of ItemResourceDto)) As List(Of Datasources.ResourceRef)
            Dim items As New List(Of Datasources.ResourceRef)

            For Each itemEntity In itemResources
                Dim ref As New Datasources.ResourceRef
                ref.Identifier = itemEntity.name
                ref.Properties.Add("resourceId", itemEntity.resourceId.ToString())

                items.Add(ref)
            Next

            Return items
        End Function


    End Class
End Namespace