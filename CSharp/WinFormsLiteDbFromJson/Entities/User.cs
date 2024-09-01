using System.Text.Json;
using System.Text.Json.Nodes;
using WinFormsLiteDbFromJson.Utils;

namespace WinFormsLiteDbFromJson.Entities
{
    public class User: Entity
    {
        private int Id { get; set; }
        public string Gender { get; set; }
        public SubjectName Name { get; set; }
        public string Nat { get; set; }
        public int Age { get; set; }
        public string FullName { get; set; }
        public Location Location { get; set; }
        public string Email { get; set; }
        public LoginData Login { get; set; }
        public DateTime FirstGettingTime { get; set; }
        public DateTime UpdatedTime { get; set; }

        public User() { }

        public static List<User> GetUsersFromJson(JsonNode jsonNode)
        {
            var results = jsonNode["results"];
            var users = new List<User>();
            if (results != null)
            {
                if (results is JsonArray)
                {
                    var arr = results.AsArray();
                    foreach (var item in arr) 
                    {
                        var user = UserFromJsonConverter.UserFromJson(item);
                        user.FirstGettingTime = DateTime.Now;
                        users.Add(user);
                    }
                }
                else
                {
                    var user = UserFromJsonConverter.UserFromJson(results);
                    user.FirstGettingTime = DateTime.Now;
                    users.Add(user);
                }
            }
            return users;
        }
    }
}
