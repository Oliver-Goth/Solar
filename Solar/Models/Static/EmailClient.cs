namespace Solar.Models.Static
{
    public class EmailClient
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public EmailClient(string email, string password) : this(email)
        {
            Password = password;
        }

        public EmailClient(string email)
        {
            Email = email;

        }
    }
}
