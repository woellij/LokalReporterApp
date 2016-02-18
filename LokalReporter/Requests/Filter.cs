using System.Runtime.Serialization;
using LokalReporter.Responses;

namespace LokalReporter.Requests {

    [DataContract]
    public class Filter {
        private Paging paging;

        [DataMember(Name = "paging")]
        public Paging Paging
        {
            get { return this.paging ?? (this.paging ?? new Paging {Offset = 0, Limit = 10}); }
            set { this.paging = value; }
        }

        [DataMember(Name = "tag")]
        public Tag Tag { get; set; }

        [DataMember(Name = "category")]
        public Category Category { get; set; }

        [DataMember(Name = "district")]
        public District District { get; set; }

        [DataMember(Name = "topStory")]
        public bool IsTopStory { get; set; }
    }

}