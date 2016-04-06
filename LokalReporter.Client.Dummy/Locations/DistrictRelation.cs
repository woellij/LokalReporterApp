using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LokalReporter.Client.Dummy.Locations
{
    [DataContract]
    public class DistrictRelation
    {

        [DataMember(Name = "dId")]
        public string DistrictId { get; set; }

        [DataMember(Name = "aIds")]
        public List<int> ArticleIds { get; set; }

    }
}