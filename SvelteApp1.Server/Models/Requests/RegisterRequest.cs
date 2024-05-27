using System.ComponentModel.DataAnnotations;

namespace SvelteApp1.Server.Models.Requests
{
    public class RegisterRequest
    {
        [Required]
        public string? Username { get; set; }
        [Required] 
        public string? Password { get; set; }
    }
}
