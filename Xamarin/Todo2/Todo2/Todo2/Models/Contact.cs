using SQLite;

namespace App1
{
    public class Contact
    {
		[PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int locationId { get; set; }
        public int userId { get; set; }
        public string contactType { get; set; }
        public string contactName { get; set; }
        public string phoneNumber { get; set; }
        public string emailAddress { get; set; }
        public string address2 { get; set; }
        public int priority { get; set; }
    }
}