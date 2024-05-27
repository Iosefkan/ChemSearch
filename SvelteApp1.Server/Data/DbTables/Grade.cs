using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SvelteApp1.Server.Data
{
    public class Grade
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public uint GradeNumber { get; set; }
    }
}