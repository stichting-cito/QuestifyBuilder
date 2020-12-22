
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Questify.Builder.Services.PublicationService.Publication
{
    [DataContract]
    public class PublicationTaskProgress : TaskProgress
    {
        [DataMember]
        public bool Succeeded { get; set; }

        [DataMember]
        public IList<string> PublicationLocations { get; set; }


        [DataMember]
        public IList<string> PublicationUrls { get; set; }

        [DataMember]
        public IList<string> ReturnedIds { get; set; }

        public IList<string> Files { get; set; }

        public PublicationTaskProgress(string taskId, int progress, int total) : base(taskId, progress, total)
        {
            PublicationLocations = new List<string>();
            PublicationUrls = new List<string>();
            ReturnedIds = new List<string>();
            Files = new List<string>();
            Finished = false;
            Succeeded = false;
        }
    }
}