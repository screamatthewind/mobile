using SQLite;

namespace Rest1
{
    [Table("Location")]
    public class Location
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public string placeId { get; set; }
        public string locationType { get; set; }
        public string locationName { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
}