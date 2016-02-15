namespace LokalReporter.Responses {

    public class Image {
        public string Source { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Image(string source, int width, int height)
        {
            this.Source = source;
            this.Width = width;
            this.Height = height;
        }
    }

}