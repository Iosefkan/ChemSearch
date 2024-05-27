using System.ComponentModel.DataAnnotations;

namespace SvelteApp1.Server.Models.Requests
{
    public class FavouriteRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
