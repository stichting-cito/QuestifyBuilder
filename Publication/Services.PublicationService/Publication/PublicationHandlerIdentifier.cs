
using System.Runtime.Serialization;

namespace Questify.Builder.Services.PublicationService.Publication
{
    /// <summary>
    /// Provides the details of a publication handler.
    /// </summary>
    [DataContract]
    public class PublicationHandlerIdentifier
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
        public string Type { get; set; }

        /// <summary>
        /// Gets the file extension of the publication result.
        /// </summary>
        [DataMember]
        public string FileExtension { get; set; }

      
        [DataMember]
        public bool QualifiesForCurrentSelection {
            get { return string.IsNullOrEmpty(ReasonForNotQualifyingForCurrentSelection); }
            set { }
        }

        /// <summary>
        /// Describes the reason why this handler does not qualify for the current selection.
        /// </summary>
        [DataMember]
        public string ReasonForNotQualifyingForCurrentSelection { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PublicationHandlerIdentifier"/> class.
        /// </summary>
        /// <param name="userFriendlyName">User friendly name of this handler.</param>
        /// <param name="type">The exact publication handler type.</param>
        /// <param name="fileExtension">The file extension.</param>
        public PublicationHandlerIdentifier(string userFriendlyName, string type, string fileExtension) : this(userFriendlyName, null)
        {
            Type = type;
            FileExtension = fileExtension;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PublicationHandlerIdentifier"/> class.
        /// </summary>
        /// <param name="userFriendlyName">User friendly name of this handler.</param>
        /// <param name="reasonForNotQualifying">Brief text giving the reason why this publication handler cannot handle the current selection.</param>
        public PublicationHandlerIdentifier(string userFriendlyName, string reasonForNotQualifying)
        {
            UserFriendlyName = userFriendlyName;
            ReasonForNotQualifyingForCurrentSelection = reasonForNotQualifying;
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