using System.Runtime.Serialization;
using LokalReporter.Requests;

namespace LokalReporter.App.FormsApp.ViewModels {

    [DataContract]
    public class FilterPreset {
        [DataMember(Name="title")]
        public string Title { get; set; }
        [DataMember(Name = "filter")]
        public Filter Filter { get; set; }
    }

}