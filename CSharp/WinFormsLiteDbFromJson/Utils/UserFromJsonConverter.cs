using System.Text.Json;
using System.Text.Json.Nodes;
using WinFormsLiteDbFromJson.Entities;

namespace WinFormsLiteDbFromJson.Utils
{
    public class UserFromJsonConverter
    {
        private static JsonSerializerOptions _serializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        public static User UserFromJson(JsonNode jsonNode)
        {

            var user = new User();
            user.Gender = jsonNode["gender"].GetValue<string>();
            user.Name = JsonSerializer.Deserialize<SubjectName>(jsonNode["name"], _serializerOptions);
            user.Nat = jsonNode["nat"].GetValue<string>();

            var locationNode = jsonNode["location"];
            var street = JsonSerializer.Deserialize<Street>(locationNode["street"], _serializerOptions);
            var coordinates = JsonSerializer.Deserialize<Coordinates>(locationNode["coordinates"], _serializerOptions);
            var timeZone = JsonSerializer.Deserialize<Entities.TimeZone>(locationNode["timezone"], _serializerOptions);
            var city = locationNode["city"].GetValue<string>();
            var country = locationNode["country"].GetValue<string>();

            var postcodeNode = locationNode["postcode"];
            var pcJsonValue = postcodeNode.AsValue();
            var pcType = GetValueType(pcJsonValue);
            var postcode = pcType == typeof(string) ? 
                locationNode["postcode"].GetValue<string>() : 
                locationNode["postcode"].GetValue<int>().ToString();

            user.Location = new Location(0, street, city, country, postcode, coordinates, timeZone);

            user.Email = jsonNode["email"].GetValue<string>();
            user.Login = JsonSerializer.Deserialize<LoginData>(jsonNode["login"], _serializerOptions);
            var dobNode = jsonNode["dob"];
            user.Age = jsonNode["dob"]["age"].GetValue<int>();
            user.FullName = user.Name.ToString();
            return user;
        }

        public static Type GetValueType(JsonValue jsonValue)
        {
            var value = jsonValue.GetValue<object>();
            if (value is JsonElement element)
            {
                return element.ValueKind switch
                {
                    JsonValueKind.False => typeof(bool),
                    JsonValueKind.True => typeof(bool),
                    JsonValueKind.Number => typeof(double),
                    JsonValueKind.String => typeof(string),
                    var _ => typeof(JsonElement),
                };
            }
            return value.GetType();
        }
    }
}
