
using System;
using System.Runtime.Serialization;
using Questify.Builder.Logic.Service.Interfaces;

namespace Questify.Builder.Services.PublicationService.Validation
{
    [DataContract]
    public class ValidationHandlerIdentifier
    {
        /// <summary>
        /// Gets the user friendly name of the validation handler.
        /// </summary>
        [DataMember]
        public string UserFriendlyName { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of this handlers current validation task.
        /// </summary>
        [DataMember]
        public string TaskId { get; set; }

        /// <summary>
        /// An instance of the validation handler.
        /// </summary>
        public IReportValidationBase Handler { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationHandlerIdentifier" /> class.
        /// </summary>
        /// <param name="userFriendlyName">User friendly name of this handler.</param>
        /// <param name="taskId">The task unique identifier.</param>
        public ValidationHandlerIdentifier(string userFriendlyName, string taskId)
        {
            UserFriendlyName = userFriendlyName;
            TaskId = taskId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationHandlerIdentifier"/> class.
        /// </summary>
        /// <param name="handler">The handler.</param>
        public ValidationHandlerIdentifier(IReportValidationBase handler)
        {
            Handler = handler;
            UserFriendlyName = handler.Name;
            TaskId = Guid.NewGuid().ToString();
        }
    }
}