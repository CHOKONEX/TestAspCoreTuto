using System.ComponentModel.DataAnnotations;

namespace CslAppServicesConsumer.Tests.Test1
{
    public class AuthenticateModel
    {
        public AuthenticateModel(string username, string password)
        {
            Username = username;
            Password = password;
        }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
