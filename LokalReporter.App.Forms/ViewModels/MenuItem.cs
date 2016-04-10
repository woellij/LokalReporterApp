using System;
using System.Runtime.Serialization;

using PropertyChanged;

using Xamarin.Forms;

namespace LokalReporter.App.FormsApp.ViewModels {

    [DataContract]
    [ImplementPropertyChanged]
    public class MenuItem {
        public MenuItem(string title, object parameter, Type targetType)
        {
            this.Title = title;
            this.Parameter = parameter;
            this.TargetViewModelType = targetType;
        }

        public MenuItem(string title, Type targetViewModelType) : this(title, null, targetViewModelType) {}

        [DataMember(Name = "t")]
        public string Title { get; set; }

        public static Color DefaultColor = Color.Black;

        [IgnoreDataMember]
        public Color ForegroundColor { get; set; } = DefaultColor;

        [IgnoreDataMember]
        public Type TargetViewModelType { get; set; }

        [DataMember(Name = "p")]
        public object Parameter { get; set; }
    }

}