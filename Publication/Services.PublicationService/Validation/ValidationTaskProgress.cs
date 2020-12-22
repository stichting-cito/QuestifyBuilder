
using System.Runtime.Serialization;
using Enums;

namespace Questify.Builder.Services.PublicationService.Validation
{
    [DataContract]
    public class ValidationTaskProgress : TaskProgress
    {
        [DataMember]
        public ValidationResult ValidationResult { get; set; }

        [DataMember]
        public bool IsReportAvailable { get; set; }

        [DataMember]
        public string Report { get; set; }

        [DataMember]
        public string ResultText { get; set; }

        public ValidationTaskProgress(string taskId, int progress, int total) : base(taskId, progress, total)
        {
        }
    }
}