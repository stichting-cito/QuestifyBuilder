using System.Runtime.Serialization;

namespace Questify.Builder.Services.PublicationService.Publication
{
    [DataContract]
    public class ConceptProcessingLabelEntry
    {

        internal ConceptProcessingLabelEntry(string conceptCode, string conceptResponseLabel, string factIdFirstFact)
        {
            ConceptCode = conceptCode;
            ConceptResponseLabel = conceptResponseLabel;
            FactIdFirstFact = factIdFirstFact;
        }

        [DataMember]
        public string ConceptCode { get; set; }

        [DataMember]
        public string ConceptResponseLabel { get; set; }

        [DataMember]
        public string FactIdFirstFact { get; set; }
    }
}