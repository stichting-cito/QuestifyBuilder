using System.Runtime.Serialization;
using System.Threading;

namespace Questify.Builder.Services.TasksService.TaskClasses
{
    [DataContract]
    public class BuilderTaskProgressItem
    {
        private int _processedCount;

        internal BuilderTaskProgressItem(string progressItemLabel, int totalCount)
        {
            ProgressItemLabel = progressItemLabel;
            TotalCount = totalCount;
        }

        public void IncrementProcessedCount()
        {
            Interlocked.Increment(ref _processedCount);
        }

        [DataMember]
        public int TotalCount { get; set; }

        [DataMember]
        public int ProcessedCount
        {
            get { return _processedCount; }
            set { _processedCount = value; }
        }

        [DataMember]
        public string ProgressItemLabel { get; set; }

        [DataMember]
        public string ProgressItemCode { get; set; }
    }
}