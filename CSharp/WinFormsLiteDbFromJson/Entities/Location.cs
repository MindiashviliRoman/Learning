using LiteDB;

namespace WinFormsLiteDbFromJson.Entities
{
    public class Location: Entity
    {
        public int Id { get; private set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Postcode { get; set; }
        public Coordinates? Coordinates { get; set; }
        public TimeZone? TimeZone { get; set; }

        public Location()
        {

        }
        [BsonCtor]
        public Location(int id, Street street, string city, string country, string postcode, Coordinates coordinates, TimeZone timeZone)
        {
            Id = id;
            Street = street.ToString();
            City = city;
            Country = country;
            Postcode = postcode;
            Coordinates = coordinates;
            TimeZone = timeZone;
        }


        public override string ToString() 
        { 
            return String.Format(
                "Id: {0}\r\n" +
                "Street: {1}\r\n" +
                "City: {2}\r\n" +
                "Country: {3}\r\n" +
                "Postcode: {4}\r\n" +
                "Coordinates: {5}\r\n" +
                "TimeZone: {6}", 
                Id, Street, City, Country, Postcode, Coordinates, TimeZone);
        }
    }
}
