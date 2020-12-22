Imports System.Runtime.CompilerServices
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.Linq
Imports Cito.Tester.Common
Imports Questify.Builder.Logic.ResourceManager
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.TestConstruction.ChainHandlers.Validating

Namespace ContentModel

    Public Module AssessmentTestResourceExtension

        Public ResourceMan As DataBaseResourceManager

        <Extension>
        Public Function GetAssessmentTest(test As AssessmentTestResourceEntity) As AssessmentTest2
            If test.ResourceData Is Nothing OrElse test.ResourceData.BinData Is Nothing Then
                Dim data As ResourceDataEntity = ResourceFactory.Instance.GetResourceData(test)
                test.ResourceData = data
            End If
            Dim factoryResult As ReturnedAssessmentTestModelInfo = AssessmentTestv2Factory.ReturnAssessmentTestv2ModelFromByteArray(test.ResourceData.BinData, True)
            Return factoryResult.AssessmentTestv2
        End Function


        <Extension>
        Public Sub SetAssessmentTest(ByVal testResource As AssessmentTestResourceEntity, ByVal assessmentTest As AssessmentTest2)
            testResource.Name = assessmentTest.Identifier
            testResource.Title = assessmentTest.Title
            If testResource.ResourceData Is Nothing Then
                testResource.ResourceData = New ResourceDataEntity
            End If
            testResource.ResourceData.BinData = SerializeHelper.XmlSerializeToByteArray(assessmentTest)
            testResource.ResourceData.FileExtension = ".xml"
        End Sub


        <Extension>
        Public Function GetItemReferences(test As AssessmentTestResourceEntity) As IList(Of ItemResourceEntity)
            Dim assessmentTest = test.GetAssessmentTest()
            If assessmentTest IsNot Nothing Then
                Dim returnList As New List(Of ItemResourceEntity)
                Dim itemCodes = assessmentTest.GetAllItemReferencesInTest.Select(Function(i) i.SourceName)
                Dim itemCodesToFetch As New List(Of String)
                If test.DependentResourceCollection IsNot Nothing Then
                    For Each code In itemCodes
                        Dim depResource = test.DependentResourceCollection.FirstOrDefault(Function(d) d.DependentResource IsNot Nothing AndAlso d.DependentResource.Name = code)
                        If depResource IsNot Nothing AndAlso depResource.DependentResource IsNot Nothing AndAlso TypeOf depResource.DependentResource Is ItemResourceEntity Then
                            returnList.Add(DirectCast(depResource.DependentResource, ItemResourceEntity))
                        Else
                            itemCodesToFetch.Add(code)
                        End If
                    Next
                Else
                    itemCodesToFetch = itemCodes.ToList
                End If
                If Not itemCodesToFetch.Count = 0 Then
                    returnList.AddRange(ResourceFactory.Instance.GetItemsByCodes(itemCodes.ToList, test.BankId, New ItemResourceRequestDTO()))
                End If
                Return returnList
            End If
            Return Nothing
        End Function

        <Extension>
        Public Function CheckItemsSupportedViews(test As AssessmentTestResourceEntity, supportedViewsToCheck As List(Of String), resourceManager As DataBaseResourceManager) As Boolean
            Dim assessmentTest = test.GetAssessmentTest()
            If assessmentTest IsNot Nothing Then
                ResourceMan = resourceManager
                Dim validator As New ItemSupportedViewsValidator(AddressOf ResourceNeeded)
                Dim itemCodes = assessmentTest.GetAllItemReferencesInTest.Select(Function(i) i.SourceName).ToList()
                Dim request = New ItemResourceRequestDTO() With {.WithDependencies = True}
                Dim items = ResourceFactory.Instance.GetItemsByCodes(itemCodes, ResourceMan.BankId, request)

                For Each item As ItemResourceEntity In items
                    If Not validator.ContainsItemSupportedViews(item, supportedViewsToCheck) Then
                        Return False
                    End If
                Next
                Return True
            End If
            Return False
        End Function

        Private Sub ResourceNeeded(ByVal sender As System.Object, ByVal e As Cito.Tester.ContentModel.ResourceNeededEventArgs)
            Dim _resource As BinaryResource = Nothing
            Dim request = New ResourceRequestDTO()
            If e.TypedResourceType IsNot Nothing Then
                _resource = ResourceMan.GetTypedResource(e.ResourceName, e.TypedResourceType, request)
            Else
                _resource = ResourceMan.GetResource(e.ResourceName, e.StreamProcessingDelegate, request)
            End If
            e.BinaryResource = _resource
        End Sub

        <Extension>
        Public Function ContainsItemCode(assessmentTest As AssessmentTestResourceEntity, itemCode As String) As Boolean
            Dim wrk As New ItemManipulationInAssessment(assessmentTest)
            Return wrk.ContainsItemCode(itemCode)
        End Function


        <Extension>
        Public Function RenameItemCode(assessmentTest As AssessmentTestResourceEntity, currentItemCode As String, newItemCode As String) As Boolean
            Dim wrk As New ItemManipulationInAssessment(assessmentTest)
            Return wrk.Rename(currentItemCode, newItemCode)
        End Function

        <Extension>
        Public Function CanPropose(test As AssessmentTestResourceEntity, Optional forceCheck As Boolean = False) As Boolean
            If (test.DependentResourceCollection IsNot Nothing AndAlso test.DependentResourceCollection.OfType(Of DataSourceResourceEntity).Any) OrElse forceCheck Then
                Return test.GetAssessmentTest.CanPropose(test.BankId)
            End If
            Return False
        End Function
    End Module

End Namespace
