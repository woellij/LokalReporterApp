using System.Collections.Generic;

namespace LokalReporter.App.FormsApp.ViewModels {

    public class MenuSection : List<MenuItem> {
        public MenuSection(string title, IEnumerable<MenuItem> items) : base(items)
        {
            this.Title = title;
        }

        public MenuSection(string title, params MenuItem[] items) : this(title, (IEnumerable<MenuItem>) items) {}

        public string Title { get; set; }
    }

}