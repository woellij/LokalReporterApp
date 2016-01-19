using System.Collections.ObjectModel;
using System.Threading;

using LokalReporter.Responses;

namespace LokalReporter.App.FormsApp.ViewModels {

    public class SecondViewModel : BaseViewModel {

        private readonly IArticlesService articlesService;

        public SecondViewModel(IArticlesService articlesService)
        {
            this.articlesService = articlesService;
            this.Articles = new ObservableCollection<Article>();
        }

        // Observablecollections geben dem layout bescheid, wenn etwas neues hinzugekommen ist
        public ObservableCollection<Article> Articles { get; }

        public override async void Start()
        {
            var articles = await this.articlesService.GetArticlesAsync(new Filter(), CancellationToken.None);
            foreach (var article in articles.Articles)
            {
                this.Articles.Add(article);
            }
        }

    }

}