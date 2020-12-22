Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Questify.Builder.Logic.ResourceManager
Imports Cito.Tester.ContentModel
Imports Cito.Tester.ContentModel.Datasources

Public Interface ICreateProposal
    Function CreateProposalsFromDataSourceList(ByVal testBase As AssessmentTestResourceEntity, sectionsWithItemDataSources As Dictionary(Of TestSection2, ItemDataSource), numberOfProposalsRequested As Integer, resourceManager As DataBaseResourceManager) As EntityCollection(Of AssessmentTestResourceEntity)
    Function CreateProposalsFromDataSourceList(ByVal testBase As AssessmentTestResourceEntity, numberOfProposalsRequested As Integer, resourceManager As DataBaseResourceManager) As EntityCollection(Of AssessmentTestResourceEntity)
    Function CreateAndSaveProposalsFromDataSourceList(ByVal testBase As AssessmentTestResourceEntity, numberOfProposalsRequested As Integer, resourceManager As DataBaseResourceManager) As IEnumerable(Of Guid)
    Function CanPerformProposalCreation(ByRef dataSourceSettingsList As List(Of DataSourceSettings), assessmentTest As AssessmentTest2, resourceManager As DataBaseResourceManager) As Boolean
End Interface
