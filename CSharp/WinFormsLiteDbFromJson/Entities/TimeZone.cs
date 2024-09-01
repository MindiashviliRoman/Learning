using System.Text.Json.Serialization;

namespace WinFormsLiteDbFromJson.Entities
{
    public class TimeZone
    {
        private int Id { get; set; }
        public string Offset { get; set; }
        public string Description { get; set; }

        [JsonConstructor]
        public TimeZone(string offset, string description)
        {
            Offset = offset;
            Description = description;
        }

        public override string ToString()
        {
            return String.Format("Id: {0} \r\n" +
                                "Offset: {1}\r\n" +
                                "Description: {2}", 
                                Id, Offset, Description);
        }
    }
}
