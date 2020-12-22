Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.ContentModel
Imports System.Linq
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.TestPackageConstruction.Requests


Namespace TestPackageConstruction.ChainHandlers.Processing
    Public NotInheritable Class TestPackageProcessingHelpers


        Private Sub New()
        End Sub



        Public Shared Function GetTestResources(ByVal requestData As TestPackageConstructionRequest, ByVal bankId As Integer) As IDictionary(Of String, AssessmentTestResourceEntity)
            Dim testCodes As IList(Of String) = CType(Datasources.ResourceRef.ToIdentifierList(requestData.Tests), IList(Of String))
            Dim dict As New Dictionary(Of String, AssessmentTestResourceEntity)

            For Each testEntity As AssessmentTestResourceEntity In ResourceFactory.Instance.GetTestsByCodes(testCodes.ToList, bankId, False)
                dict.Add(testEntity.Name, testEntity)
            Next

            Return dict
        End Function

        Public Shared Function GetTestResourceRefList(ByVal testPackage As TestPackage) As List(Of Datasources.ResourceRef)
            Dim testContext As New List(Of Datasources.ResourceRef)
            For Each testref As TestReference In testPackage.GetAllTestReferencesInTestPackage()
                Dim ref As New Datasources.ResourceRef

                ref.Identifier = testref.SourceName
                testContext.Add(ref)
            Next

            Return testContext
        End Function


        Public Shared Function GetTestResourceRefList(ByVal testReferences As IEnumerable(Of TestReference)) As List(Of Datasources.ResourceRef)
            Dim testContext As New List(Of Datasources.ResourceRef)
            For Each testref As TestReference In testReferences
                Dim ref As New Datasources.ResourceRef
                ref.Identifier = testref.SourceName
                testContext.Add(ref)
            Next
            Return testContext
        End Function


        Public Shared Function GetTestResourceRefList(ByVal testReferences As IEnumerable(Of String)) As List(Of Datasources.ResourceRef)
            Dim testContext As New List(Of Datasources.ResourceRef)
            For Each testref In testReferences
                Dim ref As New Datasources.ResourceRef
                ref.Identifier = testref
                testContext.Add(ref)
            Next
            Return testContext
        End Function


    End Class
End Namespace