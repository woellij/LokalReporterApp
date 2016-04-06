namespace LokalReporter.Client.Dummy.Locations
{
    public class Location
    {

        public int id { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public string name { get; set; }
        public int score { get; set; }
        public string county { get; set; }
        public string region { get; set; }
        public string featureClass { get; set; }

        protected bool Equals(Location other)
        {
            return this.id == other.id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return this.Equals((Location)obj);
        }

        public override int GetHashCode()
        {
            return this.id;
        }

        public Location Clone()
        {
            return (Location)this.MemberwiseClone();
        }

    }
}