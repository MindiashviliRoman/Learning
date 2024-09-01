using System.Text.Json.Serialization;

namespace WinFormsLiteDbFromJson.Entities
{
    public class Street
    {
        public int Number { get; set; }
        public string Name { get; set; }

        [JsonConstructor]
        public Street(int number, string name)
        {
            Number = number;
            Name = name;
        }

        public override string ToString()
        {
            return String.Format("{0} {1}", Name, Number);
        }
    }
}
