using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LokalReporter.Responses {

    [DataContract]
    public class Article {
        private List<Category> categories;
        private List<Image> images;
        private List<Tag> tags;

        [DataMember(Name = "isBookmarked")]
        public bool IsBookmarked { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "link")]
        public string Link { get; set; }

        [DataMember(Name = "categories")]
        public List<Category> Categories
        {
            get { return this.categories ?? (this.categories = new List<Category>()); }
            set { this.categories = value; }
        }

        [DataMember(Name = "tags")]
        public List<Tag> Tags
        {
            get { return this.tags ?? (this.tags = new List<Tag>()); }
            set { this.tags = value; }
        }

        [DataMember(Name = "date")]
        public DateTime Date { get; set; }

        [DataMember(Name = "htmlcontent")]
        public string HtmlContent { get; set; }

        [DataMember(Name = "images")]
        public List<Image> Images
        {
            get { return this.images ?? (this.images = new List<Image>()); }
            set { this.images = value; }
        }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "district")]
        public District District { get; set; }

        protected bool Equals(Article other)
        {
            return string.Equals(this.Id, other.Id);
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
            return Equals((Article) obj);
        }

        public override int GetHashCode()
        {
            return this.Id != null ? this.Id.GetHashCode() : 0;
        }
    }

}