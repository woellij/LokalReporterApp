using System;
using System.Runtime.Serialization;
using LokalReporter.Requests;

namespace LokalReporter.App.FormsApp.ViewModels {

    [DataContract]
    public class FilterPreset : IEquatable<FilterPreset> {
        public FilterPreset(string title, Filter filter)
        {
            this.Title = title;
            this.Filter = filter;
        }

        public FilterPreset() {}

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "filter")]
        public Filter Filter { get; set; }

        public bool Equals(FilterPreset other)
        {
            if (ReferenceEquals(null, other)) {
                return false;
            }
            if (ReferenceEquals(this, other)) {
                return true;
            }
            return string.Equals(this.Title, other.Title) && Equals(this.Filter, other.Filter);
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
            return Equals((FilterPreset) obj);
        }

        public override int GetHashCode()
        {
            unchecked {
                return ((this.Title != null ? this.Title.GetHashCode() : 0)*397) ^ (this.Filter != null ? this.Filter.GetHashCode() : 0);
            }
        }
    }

}