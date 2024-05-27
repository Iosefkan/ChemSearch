using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SvelteApp1.Server.Data
{
    public class Video
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Link { get; set; }
        [Required]
        public int GradeId { get; set; }
        [Required, ForeignKey(nameof(GradeId))]
        public Grade Grade { get; set; }
        [Required]
        public int TopicId { get; set; }
        [Required, ForeignKey(nameof(TopicId))]
        public Topic Topic { get; set; }
    }
}