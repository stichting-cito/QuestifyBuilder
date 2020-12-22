using System;
using System.Collections.Generic;
using System.ServiceModel;
using Questify.Builder.Services.TasksService.TaskClasses;

namespace Questify.Builder.Services.TasksService
{
    [ServiceContract]
    public interface IQuestifyBuilderTasksService
    {
        [OperationContract]
        BuilderTaskProgress PollProgress(BuilderTaskSessionTicket builderTaskSessionTicket);

        [OperationContract]
        void RequestCancellation(BuilderTaskSessionTicket builderTaskSessionTicket);

        [OperationContract]
        System.IO.Stream GetLogFileStream(BuilderTaskSessionTicket builderTaskSessionTicket);

        [OperationContract]
        BuilderTaskResult GetTaskResult(BuilderTaskSessionTicket builderTaskSessionTicket);

        [OperationContract]
        void Cleanup(BuilderTaskSessionTicket builderTaskSessionTicket);

        [OperationContract]
        BuilderTaskSessionTicket HarmonizeWithItemLayoutTemplates(List<Guid> templateGuids, bool logTheActions);

        [OperationContract]
        BuilderTaskSessionTicket HarmonizeItems(IEnumerable<Guid> itemResourceIds, bool logTheActions);

        [OperationContract]
        BuilderTaskSessionTicket HarmonizeAfterImport(int bankId, IEnumerable<Guid> templateGuids, IEnumerable<string> itemCodes);

    }
}
