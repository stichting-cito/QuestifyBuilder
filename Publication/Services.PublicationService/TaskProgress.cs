using System.Runtime.Serialization;

namespace Questify.Builder.Services.PublicationService
{
    [DataContract]
    public abstract class TaskProgress
    {
        [DataMember]
        public string TaskId { get; set; }

        [DataMember]
        public int Progress { get; set; }

        [DataMember]
        public int Total { get; set; }

        [DataMember]
        public string ProgressString { get; set; }

        [DataMember]
        public bool Finished { get; set; }

        [DataMember]
        public string Errors { get; set; }

        [DataMember]
        public string Warnings { get; set; }

        public TaskProgress(string taskId, int progress, int total)
        {
            TaskId = taskId;
            Progress = progress;
            Total = total;
        }
    }
}