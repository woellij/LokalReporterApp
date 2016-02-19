using System.Collections;
using System.Collections.Generic;
using PropertyChanged;

namespace LokalReporter.App.FormsApp.ViewModels {

    public class SelectViewModel {

        public IEnumerable Items { get; set; }
        public object SelectedItem { get; set; }
    }

    [ImplementPropertyChanged]
    public class SelectViewModel<TElement> : SelectViewModel
    {
        public new ICollection<TElement> Items
        {
            get { return (ICollection<TElement>) base.Items; }
            set { base.Items = value; }
        }

        public new TElement SelectedItem
        {
            get { return (TElement) base.SelectedItem; }
            set { base.SelectedItem = value; }
        }

        public string Title { get; set; }
        public string Placeholder { get; set; }
    }

}