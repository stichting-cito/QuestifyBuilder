
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Questify.Builder.Services.PublicationService.Publication
{
    /// <summary>
    /// Provides details on the progress of a publication task.
    /// </summary>
    [DataContract]
    public class PublicationTaskProgress : TaskProgress
    {
        /// <summary>
        /// Gets or sets a value indicating whether this publication task has succeeded. (Only available if <see cref="Finished"/> is true.)
        /// </summary>
        [DataMember]
        public bool Succeeded { get; set; }

        /// <summary>
        /// Gets a list of path where the package can be downloaded.)
        /// </summary>
        [DataMember]
        public IList<string> PublicationLocations { get; set; }


        /// <summary>
        /// Gets or sets the publication urls.
        /// </summary>
        /// <value>
        /// Gets a list of urls that can be used to start the published test
        /// </value>
        [DataMember]
        public IList<string> PublicationUrls { get; set; }

        /// <summary>
        /// Gets or sets the returned ids.
        /// </summary>
        /// <value>
        /// The returned ids.
        /// </value>
        [DataMember]
        public IList<string> ReturnedIds { get; set; }
        
        /// <summary>
        /// Gets the list of local files exported by the publication handler. (Used for cleanup after the task is finished)
        /// </summary>
        public IList<string> Files { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PublicationTaskProgress"/> class.
        /// </summary>
        /// <param name="taskId">The task's unique identifier.</param>
        /// <param name="progress">The current step/progress.</param>
        /// <param name="total">The total number of steps of this task.</param>
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