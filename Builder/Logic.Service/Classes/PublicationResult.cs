using System.Runtime.Serialization;

namespace Questify.Builder.Logic.Service.Classes
{

    [DataContract]
    public class PublicationResult
    {
        [DataMember]
        public bool Succeeded { get; set; }

        [DataMember]
        public string PublicationLocation { get; set; }

        [DataMember]
        public string DebugFileLocation { get; set; }

        [DataMember]
        public string ErrorMessage { get; set; }
    }
}

