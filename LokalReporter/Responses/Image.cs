using System.Runtime.Serialization;

namespace LokalReporter.Responses {

    [DataContract]
    public class Image {
        public Image(string source, int width, int height)
        {
            this.Source = source;
            this.Width = width;
            this.Height = height;
        }

        public Image() {}

        [DataMember(Name = "src")]
        public string Source { get; set; }

        [DataMember(Name = "width")]
        public int Width { get; set; }

        [DataMember(Name = "height")]
        public int Height { get; set; }
    }

}