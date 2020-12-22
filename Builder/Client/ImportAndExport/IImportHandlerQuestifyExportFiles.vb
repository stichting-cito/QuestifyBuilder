
Imports Enums
Imports Questify.Builder.Client.BuilderTasksService

Public Interface IImportHandlerQuestifyExportFiles
    Sub Harmonize(resourceType As ResourceTypeEnum, resourceGuids As IEnumerable(Of Guid))
    Sub CancelHarmonization()
    Event HarmonizeCompleted(result As BuilderTaskResult)
    Sub HarmonizeAfterImport(bankId As Integer, templatesToHarmonize As List(Of Guid), itemCodesToHarmonize As IEnumerable(Of String))
End Interface





