using System.Runtime.Serialization;

namespace LokalReporter.App.FormsApp.ViewModels {

    [DataContract]
    public class Identifier {
        public Identifier() {}

        public Identifier(string id)
        {
            this.Id = id;
        }

        [DataMember(Name = "id")]
        public string Id { get; set; }
    }

}