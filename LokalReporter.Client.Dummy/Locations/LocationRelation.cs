using System.Runtime.Serialization;

namespace LokalReporter.Client.Dummy.Locations
{
    [DataContract]
    public class LocationRelation
    {

        [DataMember(Name = "lId")]
        public int LocationId { get; set; }

        [DataMember(Name = "aIds")]
        public int[] ArticleIds { get; set; }

    }
}