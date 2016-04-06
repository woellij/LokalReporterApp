using System.Runtime.Serialization;

namespace LokalReporter.Client.Dummy.Locations
{
    [DataContract]
    public class ReferencedLocation
    {

        [DataMember(Name = "lId")]
        public int LocationId { get; set; }

        [DataMember(Name = "sc")]
        public int Score { get; set; }

    }
}