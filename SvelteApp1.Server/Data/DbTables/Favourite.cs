using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SvelteApp1.Server.Data
{
    public class Favourite
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int VideoId { get; set; }
        [Required, ForeignKey(nameof(VideoId))]
        public Video Video { get; set; }
        [Required]
        public Guid UserGuid { get; set; }
        [Required, ForeignKey(nameof(UserGuid))]
        public User User { get; set; }
    }
}