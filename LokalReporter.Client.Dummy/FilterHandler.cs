using System.Collections.Generic;
using System.Linq;
using LokalReporter.Requests;
using LokalReporter.Responses;

namespace LokalReporter.Client.Dummy {

    public class FilterHandler {
        private readonly IQueryable<Article> articles;

        public FilterHandler(IQueryable<Article> articles)
        {
            this.articles = articles;
            
        }

        public ArticlesResult FilterBy(Filter filter)
        {
            IQueryable<Article> result = this.articles;
            
            if (filter.District != null) {
                result = result.Where(a => a.District.Equals(filter.District));
            }
            if (filter.Category != null) {
                result = result.Where(a => a.District.Equals(filter.District));
            }
            if (filter.Tag != null) {
                result = result.Where(a => a.District.Equals(filter.District));
            }

            //var filtered = result.ToList();
            if (filter.Paging != null) {
                result = result.Skip(filter.Paging.Offset).Take(filter.Paging.Limit);
            }

            return new ArticlesResult {Articles = result.ToList()};
        }
    }

}