
using System.Runtime.Serialization;

namespace Questify.Builder.Services.PublicationService.Publication
{
    [DataContract]
    public class TestPreviewHandlerIdentifier
    {
        [DataMember]
        public string UserFriendlyName { get; set; }

        [DataMember]
        public string PublicationHandlerType { get; set; }

        [DataMember]
        public string DefaultClient { get; set; }


        [DataMember]
        public bool IsClickOnce { get; set; }

        [DataMember]
        public string Url { get; set; }


        [DataMember]
        public string FileExtension { get; set; }
        public TestPreviewHandlerIdentifier(string userFriendlyName, string publicationHandlerType, string url, bool isClickOnce, string defaultClient, string fileExtension)
        {
            UserFriendlyName = userFriendlyName;
            PublicationHandlerType = publicationHandlerType;
            Url = url;
            IsClickOnce = isClickOnce;
            DefaultClient = defaultClient;
            FileExtension = fileExtension;
        }

        public override string ToString()
        {
            return UserFriendlyName;
        }
    }
}