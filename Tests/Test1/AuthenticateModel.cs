using System.ComponentModel.DataAnnotations;

namespace TestAspCoreTuto.Tests.Test1
{
    public class AuthenticateModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
