using System.Diagnostics;
using System.Linq;
using HtmlAgilityPack;
using LokalReporter.Responses;

namespace LokalReporter.Client.Dummy.Xml {

    internal class HtmlEncodedContentReader {
        private readonly string value;

        public HtmlEncodedContentReader(string value)
        {
            this.value = value;
        }

        public void Apply(Article article)
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(this.value);

            article.HtmlContent = htmlDocument.DocumentNode.InnerHtml;

            var htmlNodes = htmlDocument.DocumentNode.Descendants("img").ToList();

            var contains = article.HtmlContent.Contains("<img");
            if (contains && htmlNodes.Count == 0) {
                Debug.WriteLine($"ContainsImages BUT NO IMG TAGS FOUND");
            }

            foreach (HtmlNode node in htmlNodes) {
                Image image;
                if (this.TryParseImage(node, out image)) {
                    article.Images.Add(image);
                }
            }
        }

        private bool TryParseImage(HtmlNode node, out Image image)
        {
            image = null;

            var name = node.Name.ToLowerInvariant();
            if (name == "img") {
                var empty = "";
                var source = node.GetAttributeValue("src", empty);
                if (source != empty) {
                    int width;
                    int.TryParse(node.GetAttributeValue("width", empty), out width);

                    int height;
                    int.TryParse(node.GetAttributeValue("height", empty), out height);
                    image = new Image(source, width, height);
                    return true;
                }
            }

            return false;
        }
    }

}