Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports System.Linq
Imports Questify.Builder.Logic.ResourceManager
Imports Questify.Builder.Logic.TestConstruction
Imports Questify.Builder.Logic.TestConstruction.Helpers
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Cito.Tester.ContentModel.Datasources
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.Logic.Service.Factories

Public Class ProposalCreator
    Implements ICreateProposal

    Private _errorMessage As String = String.Empty

    Public ReadOnly Property ErrorMessage As String
        Get
            Return _errorMessage
        End Get
    End Property


    Public Function CreateProposalsFromDataSourceList(testBase As AssessmentTestResourceEntity, sectionsWithItemDataSources As Dictionary(Of TestSection2, ItemDataSource), numberOfProposalsRequested As Integer, resourceManager As DataBaseResourceManager) As EntityCollection(Of AssessmentTestResourceEntity) Implements ICreateProposal.CreateProposalsFromDataSourceList
        Dim returnValue As New EntityCollection(Of AssessmentTestResourceEntity)
        Dim bankId = resourceManager.BankId
        Dim facadeWithRetryFixResolution As New TestConstructionFacade
        Dim facadeWithIgnoreResolution As New TestConstructionFacade

        AddHandler facadeWithRetryFixResolution.ResolveValidationError, AddressOf FacadeRetryFixResolution_ResolveValidationError
        AddHandler facadeWithIgnoreResolution.ResolveValidationError, AddressOf RefreshUsingDataSources.FacadeIgnoreResolution_ResolveValidationError

        Dim cachedMultiOutputDataSourceResult As New Dictionary(Of TestSection2, IList(Of IEnumerable(Of ResourceRef)))
        For proposalToCreateNr = 1 To numberOfProposalsRequested
            Dim name = RefreshUsingDataSources.GetUniqueName(My.Resources.RefreshUsingDataSources_CreateProposalsFromDataSourceList_ProposalPrefix, proposalToCreateNr, bankId)

            Dim newTestEntity = testBase.CopyToNew(name)
            newTestEntity.IsTemplate = False
            Dim assessmentTest = newTestEntity.GetAssessmentTest
            If sectionsWithItemDataSources Is Nothing Then sectionsWithItemDataSources = RefreshUsingDataSources.GetItemDatasourceForSectionsInTest(assessmentTest, newTestEntity.BankId)
            Dim removedDependentResourceList As New List(Of String)
            For Each sectionWithDataSourceInTemplate In sectionsWithItemDataSources
                Dim foundTestNode As AssessmentTestNode = assessmentTest.FindNodeByIdentifier(sectionWithDataSourceInTemplate.Key.Identifier)
                If foundTestNode IsNot Nothing Then
                    Dim dataSourceSectionInClonedTest As TestSection2 = DirectCast(foundTestNode, TestSection2)
                    Dim datasource = sectionWithDataSourceInTemplate.Value
                    Dim itemListToAddToSection As IList(Of ResourceRef)
                    If datasource IsNot Nothing Then
                        If sectionsWithItemDataSources.Any AndAlso TypeOf datasource Is ItemDataSourceManyOutput Then
                            If Not cachedMultiOutputDataSourceResult.ContainsKey(sectionWithDataSourceInTemplate.Key) Then
                                Dim dataSourceWithContext = TryCast(datasource, IDataSourceWithContext)
                                If (dataSourceWithContext IsNot Nothing) Then
                                    With dataSourceWithContext
                                        .TestSectionContext = dataSourceSectionInClonedTest
                                        .AssessmentTestContext = assessmentTest
                                    End With
                                End If
                                Try
                                    Dim dataSourceOutput As IList(Of IEnumerable(Of ResourceRef)) = DirectCast(datasource, ItemDataSourceManyOutput).GetMany(resourceManager, numberOfProposalsRequested)
                                    If dataSourceOutput IsNot Nothing Then
                                        If dataSourceOutput.Count <> numberOfProposalsRequested Then
                                            Throw New LogicFatalException(String.Format("Unexpected number of outputs from the '{0}' datasource: {0} expected, {1} actual.", datasource.GetType().FullName, proposalToCreateNr, dataSourceOutput.Count))
                                        End If
                                        cachedMultiOutputDataSourceResult.Add(sectionWithDataSourceInTemplate.Key, dataSourceOutput)
                                    Else
                                        Return Nothing
                                    End If
                                Catch ex As Exception
                                    Throw New Exception(String.Format(My.Resources.Message_CreatingProposal_Error, ex.Message), ex)
                                End Try
                            End If

                            If DirectCast(datasource, ItemDataSourceManyOutput).ClearSectionWhenProposing Then
                                RefreshUsingDataSources.RemoveNodesInSectionExceptAnchorItems(assessmentTest, newTestEntity, dataSourceSectionInClonedTest, facadeWithIgnoreResolution, removedDependentResourceList)
                            End If

                            Dim cachedOutput As IList(Of IEnumerable(Of ResourceRef)) = cachedMultiOutputDataSourceResult(sectionWithDataSourceInTemplate.Key)
                            itemListToAddToSection = New List(Of ResourceRef)(cachedOutput(proposalToCreateNr - 1))
                        Else
                            itemListToAddToSection = RefreshUsingDataSources.RefreshSectionWithDataSource(datasource, assessmentTest, newTestEntity, dataSourceSectionInClonedTest, resourceManager, facadeWithRetryFixResolution, removedDependentResourceList)
                        End If

                        Dim facadeToUse As TestConstructionFacade
                        If datasource.IsReturnSetValidated Then
                            facadeToUse = facadeWithIgnoreResolution
                        Else
                            facadeToUse = facadeWithRetryFixResolution
                        End If
                        facadeToUse.ResetFacade()
                        If resourceManager IsNot Nothing Then
                            If Not TestConstructionOp.AddItemsToTest(assessmentTest, newTestEntity, resourceManager, RefreshUsingDataSources.ReturnItemListWithoutAnchorItems(itemListToAddToSection), dataSourceSectionInClonedTest, dataSourceSectionInClonedTest.Components.Count, facadeToUse) Then
                                Throw New Exception(My.Resources.ProposalCreator_FailedToAddItems)
                            End If
                        End If
                    Else
                        Throw New LogicFatalException(
                            $"Cannot create data source for section '{sectionWithDataSourceInTemplate.Key.Identifier}'")
                    End If
                Else
                    Throw New LogicFatalException(
                        $"Could not find section '{sectionWithDataSourceInTemplate.Key.Identifier}' in cloned test")
                End If
            Next
            newTestEntity.Title = assessmentTest.Title
            If assessmentTest.GetAllItemReferencesInTest.GroupBy(Function(s) s.SourceName).Any(Function(c) c.Count() > 1) Then
                Throw New LogicFatalException(
                    $"Proposal contains items that are in the test more than once: { _
                                                 String.Join(", ",
                                                             assessmentTest.GetAllItemReferencesInTest.GroupBy(
                                                                 Function(s) s.SourceName).Where(
                                                                     Function(c) c.Count() > 1).Distinct.Select(
                                                                         Function(i) i.Key))}".ToArray)
            End If
            newTestEntity.SetAssessmentTest(assessmentTest)
            returnValue.Add(newTestEntity)
        Next
        RemoveHandler facadeWithRetryFixResolution.ResolveValidationError, AddressOf FacadeRetryFixResolution_ResolveValidationError
        RemoveHandler facadeWithIgnoreResolution.ResolveValidationError, AddressOf RefreshUsingDataSources.FacadeIgnoreResolution_ResolveValidationError
        Return returnValue
    End Function

    Public Function CreateProposalsFromDataSourceList(testBase As AssessmentTestResourceEntity, numberOfProposalsRequested As Integer, resourceManager As DataBaseResourceManager) As EntityCollection(Of AssessmentTestResourceEntity) Implements ICreateProposal.CreateProposalsFromDataSourceList
        Return CreateProposalsFromDataSourceList(testBase, Nothing, numberOfProposalsRequested, resourceManager)
    End Function

    Public Function CreateAndSaveProposalsFromDataSourceList(testBase As AssessmentTestResourceEntity, numberOfProposalsRequested As Integer, resourceManager As DataBaseResourceManager) As IEnumerable(Of Guid) Implements ICreateProposal.CreateAndSaveProposalsFromDataSourceList
        Dim tests = CreateProposalsFromDataSourceList(testBase, numberOfProposalsRequested, resourceManager)
        If tests IsNot Nothing Then
            For Each test In tests
                ResourceFactory.Instance.UpdateAssessmentTestResource(test)
            Next
            Return tests.Select(Function(t) t.ResourceId)
        End If
        Return Nothing
    End Function

    Public Function CanPerformProposalCreation(ByRef dataSourceSettingsList As List(Of DataSourceSettings), assessmentTest As AssessmentTest2, resourceManager As DataBaseResourceManager) As Boolean Implements ICreateProposal.CanPerformProposalCreation
        Dim itemsInAllDataSources As New List(Of String)
        Dim sectionsWithItemDataSources = RefreshUsingDataSources.GetItemDatasourceForSectionsInTest(assessmentTest, resourceManager.BankId)
        For Each node As AssessmentTestNode In sectionsWithItemDataSources.Keys
            If TypeOf node Is TestComponent2 AndAlso Not String.IsNullOrEmpty(DirectCast(node, TestSection2).ItemDataSource) Then
                Dim resourceName As String = DirectCast(node, TestSection2).ItemDataSource
                Dim request = new ResourceRequestDTO With {.WithDependencies = True, .WithCustomProperties = True}
                Dim resource As BinaryResource = resourceManager.GetTypedResource(resourceName, GetType(DataSourceSettings), request)
                If resource.ResourceObject IsNot Nothing AndAlso TypeOf resource.ResourceObject Is DataSourceSettings Then
                    Dim settings As DataSourceSettings
                    settings = CType(resource.ResourceObject, DataSourceSettings)
                    Dim dataSource As ItemDataSourceManyOutput = TryCast(settings.CreateGetDataSource(), ItemDataSourceManyOutput)
                    If Not dataSource.ClearSectionWhenProposing Then
                        Dim itemsInDataSource As List(Of String) = dataSource.GetAllItemcodes(resourceManager).ToList()
                        Dim dups = itemsInAllDataSources.Intersect(itemsInDataSource)
                        If dups.Count > 0 Then
                            _errorMessage = My.Resources.ProposalCreator_DuplicateItemsInMultipleDataSources
                            Return False
                        Else
                            itemsInAllDataSources = itemsInAllDataSources.Concat(itemsInDataSource).ToList
                        End If
                    End If

                    dataSourceSettingsList.Add(settings)
                End If
            End If
        Next

        Dim itemsInTest = assessmentTest.GetAllItemReferencesInTest().ToList.ConvertAll(Function(i) i.SourceName)
        Dim itemDups = itemsInTest.Intersect(itemsInAllDataSources)
        If itemDups.Count > 0 Then
            _errorMessage = String.Format(My.Resources.ProposalCreator_ItemsAlreadyInTest, String.Join(", ", itemDups.ToArray()))
            Return False
        End If

        Return (dataSourceSettingsList.Count > 0)
    End Function




    Private Shared Sub FacadeRetryFixResolution_ResolveValidationError(ByVal sender As Object, ByVal e As TestConstructionValidationEventArgs)
        If (e.ResolutionsAvailable And TestConstructionValidationEventArgs.resolutionEnum.RetryFix) = TestConstructionValidationEventArgs.resolutionEnum.RetryFix Then
            e.Resolution = TestConstructionValidationEventArgs.resolutionEnum.RetryFix
        Else
            e.Resolution = TestConstructionValidationEventArgs.resolutionEnum.Abort
        End If
    End Sub


End Class
