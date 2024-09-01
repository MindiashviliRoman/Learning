using System.Text.Json.Serialization;

namespace WinFormsLiteDbFromJson.Entities
{
    public class Coordinates
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }

        [JsonConstructor]
        public Coordinates(float latitude, float longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public override string ToString()
        {
            return String.Format("Latitude: {0}, Longitude: {1}", Latitude, Longitude);
        }
    }
}
