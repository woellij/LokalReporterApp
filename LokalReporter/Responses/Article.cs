using System;
using System.Collections.Generic;

namespace LokalReporter.Responses {

    public class Article {
        private List<Category> categories;
        private List<Image> images;
        private List<Tag> tags;

        public string Title { get; set; }
        public string Link { get; set; }

        public List<Category> Categories
        {
            get { return this.categories ?? (this.categories = new List<Category>()); }
            set { this.categories = value; }
        }

        public List<Tag> Tags
        {
            get { return this.tags ?? (this.tags = new List<Tag>()); }
            set { this.tags = value; }
        }

        public DateTime Date { get; set; }

        public HtmlContent HtmlContent { get; set; }

        public List<Image> Images
        {
            get { return this.images ?? (this.images = new List<Image>()); }
            set { this.images = value; }
        }

        public string Id { get; set; }

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