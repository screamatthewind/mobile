using System.Collections.Generic;

namespace Rest1
{
    internal class LocationContacts
    {
        public Location location { get; set; }
        public List<Contact> contacts { get; set; }
    }
}