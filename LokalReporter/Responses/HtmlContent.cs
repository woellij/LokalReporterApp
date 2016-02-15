namespace LokalReporter.Responses {

    public class HtmlContent {
        public HtmlContent(string innerHtml)
        {
            this.InnerHtml = innerHtml;
        }

        public string InnerHtml { get; set; }
    }

}