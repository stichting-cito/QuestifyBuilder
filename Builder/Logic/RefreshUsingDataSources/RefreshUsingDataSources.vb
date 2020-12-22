Imports System.Linq
Imports Cito.Tester.Common
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Questify.Builder.Logic.ContentModel
Imports Cito.Tester.ContentModel
Imports Cito.Tester.ContentModel.Datasources
Imports Questify.Builder.Logic.TestConstruction.Helpers
Imports Questify.Builder.Logic.TestConstruction
Imports Questify.Builder.Logic.Service.Factories


Public NotInheritable Class RefreshUsingDataSources


    Public Shared Function GetItemDatasourceForSectionsInTest(ByVal test As AssessmentTest2, ByVal bankId As Integer) As Dictionary(Of TestSection2, ItemDataSource)
        return test.GetDataSourceSettingsForSectionsInTest(bankId).ToDictionary(Function(s) (s.Key), Function(s) TryCast(s.Value.CreateGetDataSource(), ItemDataSource))
    End Function

    Public Shared Function GetItemDatasourceManyOutputForSectionsInTest(ByVal test As AssessmentTest2, ByVal bankId As Integer) As Dictionary(Of TestSection2, ItemDataSource)
        Return GetItemDatasourceForSectionsInTest(test, bankId).Where(Function(s) TypeOf s.Value Is ItemDataSourceManyOutput).ToDictionary(Function(mo) (mo.Key), Function(mo) (mo.Value))
    End Function

    Public Shared Function RefreshSectionWithDataSource(ByVal dataSource As ItemDataSource, ByVal assessmentTest As AssessmentTest2, ByVal entity As AssessmentTestResourceEntity, ByVal section As TestSection2, ByVal resourceManager As ResourceManagerBase, ByVal facade As TestConstructionFacade, ByRef removedDependentResourceList As List(Of String)) As IList(Of ResourceRef)
        RemoveNodesInSectionExceptAnchorItems(assessmentTest, entity, section, facade, removedDependentResourceList)

        If TypeOf dataSource Is IDataSourceWithContext Then
            With DirectCast(dataSource, IDataSourceWithContext)
                .TestSectionContext = section
                .AssessmentTestContext = assessmentTest
            End With
        End If

        Dim resultFromDataSoure As IEnumerable(Of ResourceRef) = dataSource.Get(resourceManager)
        Dim returnValue As IList(Of ResourceRef) = Nothing
        If resultFromDataSoure IsNot Nothing Then
            returnValue = New List(Of ResourceRef)(resultFromDataSoure)
        End If
        Return returnValue
    End Function

    Friend Shared Sub RemoveNodesInSectionExceptAnchorItems(ByVal assessmentTest As AssessmentTest2, ByVal entity As AssessmentTestResourceEntity, ByVal section As TestSection2, ByVal facade As TestConstructionFacade, ByRef removedDependentResourceList As List(Of String))
        Dim nodesToDelete As New List(Of AssessmentTestNode)
        For Each node As AssessmentTestNode In section.Components
            If TypeOf node Is ItemReference2 Then
                Dim item As ItemReference2 = DirectCast(node, ItemReference2)
                If Not item.IsAnchorItem Then
                    nodesToDelete.Add(node)
                End If
            Else
                nodesToDelete.Add(node)
            End If
        Next

        If nodesToDelete.Count > 0 Then
            Dim removedResources As List(Of String) = TestConstructionOp.DeleteTestComponents(nodesToDelete, assessmentTest, entity, facade)
            If removedDependentResourceList IsNot Nothing Then
                removedDependentResourceList.AddRange(removedResources)
            End If
        End If
    End Sub

    Friend Shared Sub FacadeIgnoreResolution_ResolveValidationError(ByVal sender As Object, ByVal e As TestConstructionValidationEventArgs)
        If (e.ResolutionsAvailable And TestConstructionValidationEventArgs.resolutionEnum.RetryIgnore) = TestConstructionValidationEventArgs.resolutionEnum.RetryIgnore Then
            e.Resolution = TestConstructionValidationEventArgs.resolutionEnum.RetryIgnore
        Else
            e.Resolution = TestConstructionValidationEventArgs.resolutionEnum.Abort
        End If
    End Sub

    Public Shared Function RefreshSectionWithDataSource(ByVal assessmentTest As AssessmentTest2, ByVal entity As AssessmentTestResourceEntity, ByVal section As TestSection2, ByVal settings As DataSourceSettings, ByVal resourceManager As ResourceManagerBase, ByVal facade As TestConstructionFacade, ByRef removedDependentResourceList As List(Of String)) As IList(Of ResourceRef)
        Dim datasrc As ItemDataSource
        Dim returnValue As IList(Of ResourceRef) = Nothing

        datasrc = TryCast(settings.CreateGetDataSource(), ItemDataSource)

        If datasrc IsNot Nothing Then
            returnValue = RefreshSectionWithDataSource(datasrc, assessmentTest, entity, section, resourceManager, facade, removedDependentResourceList)
        End If

        Return returnValue
    End Function

    Public Shared Function ReturnItemListWithoutAnchorItems(ByVal input As IList(Of ResourceRef)) As List(Of ResourceRef)
        Dim itemsToAddByCode As New List(Of ResourceRef)

        For Each item As ResourceRef In input
            If Not (item.Properties.ContainsKey("IsAnchorItem") AndAlso item.Properties("IsAnchorItem") = Boolean.TrueString) Then
                itemsToAddByCode.Add(item)
            End If
        Next

        Return itemsToAddByCode
    End Function

    Public Shared Function ReturnItemListAsStringWithoutAnchorItems(ByVal input As IList(Of ResourceRef)) As List(Of String)
        Dim list As IList(Of ResourceRef) = ReturnItemListWithoutAnchorItems(input)
        Dim returnValue As New List(Of String)
        For Each item As ResourceRef In list
            returnValue.Add(item.Identifier)
        Next

        Return returnValue
    End Function

    Friend Function CreateNewAssessmentTest(name As String, bankId As Integer, depResourceCollection As EntityCollection(Of DependentResourceEntity)) As AssessmentTestResourceEntity
        Dim newTestEntity As New AssessmentTestResourceEntity With {
                .BankId = bankId,
                .Name = name,
                .Version = "0.1",
                .ResourceData = New ResourceDataEntity(),
                .IsTemplate = False}
        For Each depResource In depResourceCollection
            DependencyManagement.AddDependentResourceToResource(newTestEntity, depResource.DependentResource)
        Next
        Return newTestEntity
    End Function

    Friend Shared Function GetUniqueName(name As String, number As Integer, bankId As Integer) As String
        Dim exists = True
        Dim proposalIdentifier = number.ToString
        Dim attempt = 1
        If Not name.Contains("{0}") Then name = String.Concat(name, "_{0}")
        Dim resourceName As String = String.Format(name, proposalIdentifier)
        While exists
            exists = False
            If ResourceFactory.Instance.ResourceExists(bankId, resourceName, True) Then
                exists = True
                proposalIdentifier = $"{attempt}_{number.ToString}"
                resourceName = String.Format(name, proposalIdentifier)
                attempt += 1
            End If
        End While
        Return resourceName
    End Function


End Class