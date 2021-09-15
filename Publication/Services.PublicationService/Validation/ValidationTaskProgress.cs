
using System.Runtime.Serialization;
using Enums;

namespace Questify.Builder.Services.PublicationService.Validation
{
    /// <summary>
    /// Provides details on the progress of a validation task.
    /// </summary>
    [DataContract]
	public class ValidationTaskProgress : TaskProgress
	{
        /// <summary>
        /// Gets or sets the validation result.
        /// </summary>
        [DataMember]
        public ValidationResult ValidationResult { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a report is available.
        /// </summary>
        [DataMember]
        public bool IsReportAvailable { get; set; }

        /// <summary>
        /// Gets or sets the report string. (will be emptrystring if IsReportAvailable is false)
        /// </summary>
        [DataMember]
        public string Report { get; set; }

        /// <summary>
        /// Gets or sets the result text.
        /// </summary>
        [DataMember]
        public string ResultText { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationTaskProgress"/> class.
        /// </summary>
        /// <param name="taskId">The task unique identifier.</param>
        /// <param name="progress">The progress.</param>
        /// <param name="total">The total.</param>
        public ValidationTaskProgress(string taskId, int progress, int total) : base(taskId, progress, total)
        {
        }
	}
}