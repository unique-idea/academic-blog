namespace Academic_Blog.PayLoad.Response
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }    
        public string AccountStatus { get; set; }

    }
}
