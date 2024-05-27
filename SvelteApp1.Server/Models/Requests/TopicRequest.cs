using System.ComponentModel.DataAnnotations;

namespace SvelteApp1.Server.Models.Requests
{
    public class TopicRequest
    {
        [Required]
        public string? Topic { get; set; }
    }
}
