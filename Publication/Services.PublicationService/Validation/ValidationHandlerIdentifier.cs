
using System;
using System.Runtime.Serialization;
using Questify.Builder.Logic.Service.Interfaces;

namespace Questify.Builder.Services.PublicationService.Validation
{
    [DataContract]
    public class ValidationHandlerIdentifier
    {
        [DataMember]
        public string UserFriendlyName { get; set; }

        [DataMember]
        public string TaskId { get; set; }

        public IReportValidationBase Handler { get; set; }

        public ValidationHandlerIdentifier(string userFriendlyName, string taskId)
        {
            UserFriendlyName = userFriendlyName;
            TaskId = taskId;
        }

        public ValidationHandlerIdentifier(IReportValidationBase handler)
        {
            Handler = handler;
            UserFriendlyName = handler.Name;
            TaskId = Guid.NewGuid().ToString();
        }
    }
}