
using System.Runtime.Serialization;

namespace Questify.Builder.Services.PublicationService.Publication
{
    [DataContract]
    public class TestPreviewHandlerIdentifier
    {
        /// <summary>
        /// Gets the user friendly name of the publication handler.
        /// </summary>
        [DataMember]
        public string UserFriendlyName { get; set; }

        /// <summary>
        /// Gets the exact publication handler type.
        /// </summary>
        [DataMember]
        public string PublicationHandlerType { get; set; }

        /// <summary>
        /// Gets or sets the default client.
        /// </summary>
        /// <value>
        /// The default client.
        /// </value>
        [DataMember]
        public string DefaultClient { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether [is click once].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is click once]; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool IsClickOnce { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [URL].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [URL]; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public string Url { get; set; }


        /// <summary>
        /// Gets or sets the file extension.
        /// </summary>
        /// <value>
        /// The file extension.
        /// </value>
         [DataMember]
        public string FileExtension { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="TestPreviewHandlerIdentifier"/> class.
        /// </summary>
        /// <param name="userFriendlyName">Name of the user friendly.</param>
        /// <param name="publicationHandlerType">Type of the publication handler.</param>
        /// <param name="url">The URL.</param>
        /// <param name="isClickOnce">if set to <c>true</c> [is click once].</param>
        /// <param name="defaultClient">The default client.</param>
        /// <param name="fileExtension">The file extension.</param>
        public TestPreviewHandlerIdentifier(string userFriendlyName, string publicationHandlerType, string url, bool isClickOnce, string defaultClient, string fileExtension)
        {
            UserFriendlyName = userFriendlyName;
            PublicationHandlerType = publicationHandlerType;
            Url = url;
            IsClickOnce = isClickOnce;
            DefaultClient = defaultClient;
            FileExtension = fileExtension;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return UserFriendlyName;
        }
    }
}