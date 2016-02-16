using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using LokalReporter.Requests;
using LokalReporter.Responses;
using XLabs;

namespace LokalReporter.App.FormsApp.ViewModels {

    public class SecondViewModel : BaseViewModel {
        private readonly IArticlesService articlesService;

        public SecondViewModel(IArticlesService articlesService)
        {
            this.articlesService = articlesService;
            this.Articles = new ObservableCollection<Article>();
            this.ShowDetails = new RelayCommand<Article>(a => this.ShowViewModel<DetailsViewModel>(new Identifier(a.Id)));
        }

        public ICommand ShowDetails { get; }

        // Observablecollections geben dem layout bescheid, wenn etwas neues hinzugekommen ist
        public ObservableCollection<Article> Articles { get; set; }

        public override async void Start()
        {
            ArticlesResult articles = await this.articlesService.GetArticlesAsync(new Filter(), CancellationToken.None);
            this.Articles = new ObservableCollection<Article>(articles.Articles.Take(50));
        }
    }

}