using System.ComponentModel.DataAnnotations;

namespace SvelteApp1.Server.Models.Requests
{
    public class DeleteTopicRequest
    {
        [Required]
        public int TopicId { get; set; }
    }
}
