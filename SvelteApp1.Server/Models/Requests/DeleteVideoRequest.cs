using System.ComponentModel.DataAnnotations;

namespace SvelteApp1.Server.Models.Requests
{
    public class DeleteVideoRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
