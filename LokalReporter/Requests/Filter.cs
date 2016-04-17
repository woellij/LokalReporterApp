using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using LokalReporter.Responses;

namespace LokalReporter.Requests {

    [DataContract]
    public class Filter : IEquatable<Filter> {
        private Paging paging;

        [DataMember(Name = "paging")]
        public Paging Paging
        {
            get { return this.paging ?? (this.paging = new Paging {Offset = 0, Limit = 10}); }
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

        [DataMember(Name = "ids")]
        public IEnumerable<int> Ids { get; set; }

        public bool Equals(Filter other)
        {
            if (ReferenceEquals(null, other)) {
                return false;
            }
            if (ReferenceEquals(this, other)) {
                return true;
            }
            return Equals(this.Tag, other.Tag) && Equals(this.Category, other.Category) && Equals(this.District, other.District);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            if (obj.GetType() != this.GetType()) {
                return false;
            }
            return Equals((Filter) obj);
        }

        public override int GetHashCode()
        {
            unchecked {
                var hashCode = this.Tag != null ? this.Tag.GetHashCode() : 0;
                hashCode = (hashCode*397) ^ (this.Category != null ? this.Category.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (this.District != null ? this.District.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ this.IsTopStory.GetHashCode();
                return hashCode;
            }
        }
    }

}