using System.Runtime.Serialization;
using LokalReporter.Responses;

namespace LokalReporter.Requests {

    public class Filter {

        private Paging paging;

        public Paging Paging
        {
            get { return this.paging ?? (this.paging ?? (new Paging {Offset = 0, Limit = 10})); }
            set { this.paging = value; }
        }

        [DataMember(Name = "tag")]
        public Tag Tag { get; set; }

        [DataMember(Name = "category")]
        public Category Category { get; set; }

        [DataMember(Name = "district")]
        public District District { get; set; }
    }

}