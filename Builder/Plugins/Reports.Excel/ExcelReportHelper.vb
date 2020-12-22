Imports System.Diagnostics.CodeAnalysis
Imports Cito.Tester.Common
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.Service.Factories
Imports SD.LLBLGen.Pro.ORMSupportClasses
Imports Questify.Builder.Logic

Public Class ExcelReportHelper

    Public Shared Function GetAllItemsInTestCollection(assessmenttestResource As AssessmentTestResourceEntity, ByRef itemCollection As EntityCollectionBase2(Of ItemResourceEntity), withDependencies As Boolean, withCustomProperties As Boolean) As List(Of ItemReference2)
        Dim allItemsInTestCollection As New List(Of ItemReference2)
        Dim fullTest As AssessmentTestResourceEntity = GetFullTestEntity(assessmenttestResource)
        Dim assessmentTest As AssessmentTest2 = GetTestFromResource(fullTest)
        For Each part As TestPart2 In assessmentTest.TestParts
            For Each section As TestSection2 In part.Sections
                ProcessSection(section, allItemsInTestCollection)
            Next
        Next
        Dim itemCodeList As New List(Of String)
        For Each item As ItemReference2 In allItemsInTestCollection
            itemCodeList.Add(item.SourceName)
        Next
        Dim request = New ItemResourceRequestDTO() With {.WithDependencies = withDependencies, .WithCustomProperties = withCustomProperties}
        itemCollection = ResourceFactory.Instance.GetItemsByCodes(itemCodeList, assessmenttestResource.BankId, request)
        Return allItemsInTestCollection
    End Function

    <SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")>
    Public Shared Function GetFullTestEntity(ByVal entity As AssessmentTestResourceEntity) As AssessmentTestResourceEntity
        Return ResourceFactory.Instance.GetAssessmentTest(entity)
    End Function

    Public Shared Function GetTestFromResource(ByVal testEntity As AssessmentTestResourceEntity) As AssessmentTest2
        Dim testDefinition As AssessmentTest2 = Nothing
        Dim data As ResourceDataEntity = ResourceFactory.Instance.GetResourceData(testEntity)
        Dim result As ReturnedAssessmentTestModelInfo
        result = AssessmentTestv2Factory.ReturnAssessmentTestv2ModelFromByteArray(data.BinData, True)
        testDefinition = result.AssessmentTestv2
        testEntity.ResourceData = data

        Return testDefinition
    End Function

    Public Shared Sub ProcessSection(ByVal testSection As TestSection2, ByRef itemCollection As List(Of ItemReference2))
        For Each testcomponent As TestComponent2 In testSection.Components
            If TypeOf testcomponent Is ItemReference2 Then
                Dim itemRefercence As ItemReference2 = DirectCast(testcomponent, ItemReference2)
                itemCollection.Add(itemRefercence)
            ElseIf TypeOf testcomponent Is TestSection2 Then
                ProcessSection(DirectCast(testcomponent, TestSection2), itemCollection)
            End If
        Next
    End Sub
End Class
