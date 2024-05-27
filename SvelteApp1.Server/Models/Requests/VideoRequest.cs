using System.ComponentModel.DataAnnotations;

namespace SvelteApp1.Server.Models.Requests
{
    public class VideoRequest
    {
        [Required]
        public string? Link { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public int TopicId { get; set; }
        [Required]
        public int GradeId { get; set; }
    }
}
