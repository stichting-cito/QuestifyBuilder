
using System.Runtime.Serialization;

namespace Questify.Builder.Services.PublicationService.Publication
{
    [DataContract]
    public class PublicationHandlerIdentifier
    {
        [DataMember]
        public string UserFriendlyName { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public string FileExtension { get; set; }


        [DataMember]
        public bool QualifiesForCurrentSelection
        {
            get { return string.IsNullOrEmpty(ReasonForNotQualifyingForCurrentSelection); }
            set { }
        }

        [DataMember]
        public string ReasonForNotQualifyingForCurrentSelection { get; set; }

        public PublicationHandlerIdentifier(string userFriendlyName, string type, string fileExtension) : this(userFriendlyName, null)
        {
            Type = type;
            FileExtension = fileExtension;
        }

        public PublicationHandlerIdentifier(string userFriendlyName, string reasonForNotQualifying)
        {
            UserFriendlyName = userFriendlyName;
            ReasonForNotQualifyingForCurrentSelection = reasonForNotQualifying;
        }

        public override string ToString()
        {
            return UserFriendlyName;
        }
    }
}