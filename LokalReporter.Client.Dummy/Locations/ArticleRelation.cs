using System.Runtime.Serialization;

namespace LokalReporter.Client.Dummy.Locations
{
    [DataContract]
    public class ArticleRelation
    {

        [DataMember(Name = "aId")]
        public int ArticleId { get; set; }

        [DataMember(Name = "lRefs")]
        public ReferencedLocation[] ReferencedLocations { get; set; }

    }
}