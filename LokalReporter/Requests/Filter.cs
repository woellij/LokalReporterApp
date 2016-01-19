namespace LokalReporter {

    public class Filter {

        private Paging paging;

        public Paging Paging
        {
            get { return this.paging ?? (this.paging ?? (new Paging {Offset = 0, Limit = 10})); }
            set { this.paging = value; }
        }

    }

    public class Paging {

        public int Offset { get; set; }
        public int Limit { get; set; }

    }

}