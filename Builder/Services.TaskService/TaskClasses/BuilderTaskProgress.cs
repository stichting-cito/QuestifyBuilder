using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Questify.Builder.Services.TasksService.TaskClasses
{
    [DataContract]
    public class BuilderTaskProgress
    {
        public enum ExecutionState
        {
            Finished = 0,
            Running = 1,
            Cancelled = 2
        };

        private List<BuilderTaskProgressItem> _progressItems = new List<BuilderTaskProgressItem>();

        [DataMember]
        public ExecutionState State { get; set; }

        [DataMember]
        public List<BuilderTaskProgressItem> ProgressItems
        {
            get { return _progressItems; }
            set { _progressItems = value; }
        }
    }
}