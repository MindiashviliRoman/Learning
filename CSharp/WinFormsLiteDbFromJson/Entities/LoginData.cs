
namespace WinFormsLiteDbFromJson.Entities
{
    public class LoginData
    {
        private int Id { get; set; }
        public string Uuid { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string salt { get; set; }
        public string md5 { get; set; }
        public string sha1 { get; set; }
        public string sha256 { get; set; }

        public override string ToString()
        {
            return String.Format("Id: {0}\r\n" +
                                 "Uuid: {1}\r\n" +
                                 "UserName: {2}\r\n" +
                                 "Password: {3}", 
                                 Id, Uuid, UserName, Password);
        }
    }
}
