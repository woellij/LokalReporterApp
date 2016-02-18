using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;
using LokalReporter.Responses;

namespace LokalReporter.Client.Dummy.Xml {

    public class XmlArticlesReader {
        private readonly IDictionary<string, Action<XElement, Article>> setterActions = new Dictionary<string, Action<XElement, Article>>();

        public XmlArticlesReader()
        {
            this.setterActions[":title"] = (element, article) => article.Title = element.Value;
            this.setterActions[":link"] = (element, article) => article.Link = element.Value;
            this.setterActions["http://purl.org/rss/1.0/modules/content/:encoded"] =
                (element, article) => new HtmlEncodedContentReader(element.Value).Apply(article);
            this.setterActions[":pubDate"] = (element, article) => article.Date = DateTime.Parse(element.Value);

            this.setterActions[":category"] = this.SetCategoryOrTags;
            this.setterActions["http://wordpress.org/export/1.2/:post_id"] = (element, article) => article.Id = element.Value;
        }

        private void SetCategoryOrTags(XElement element, Article article)
        {
            var domain = element.Attribute(XName.Get("domain"))?.Value;
            if (domain == null) {
                return;
            }
            //if (domain == "category") {
            //    article.Categories.Add(Entities.From<Category>(element.Attribute("nicename").Value, element.Value));
            //}
            //else 
            if (domain == "post_tag") {
                article.Tags.Add(Entities.From<Tag>(element.Attribute("nicename").Value, element.Value));
            }
        }

        public async Task<List<Article>> ReadArticlesAsync()
        {
            List<Article> articles = new List<Article>();

            await Task.Run(() => {
                var assembly = typeof (XmlArticlesService).GetTypeInfo().Assembly;
                using (Stream stream = assembly.GetManifestResourceStream("LokalReporter.Client.Dummy.lokalreporter.xml")) {
                    XElement channel = XDocument.Load(stream).Root?.Descendants().FirstOrDefault();

                    foreach (XElement item in channel.Descendants("item")) {
                        var article = new Article();
                        foreach (XElement propertyElement in item.Elements()) {
                            Action<XElement, Article> setter;
                            var name = $"{propertyElement.Name.Namespace}:{propertyElement.Name.LocalName}";

                            if (this.setterActions.TryGetValue(name, out setter)) {
                                setter(propertyElement, article);
                            }
                        }
                        articles.Add(article);
                    }
                }
            });

            return articles;
        }
    }

}