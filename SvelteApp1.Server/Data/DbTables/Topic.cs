using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SvelteApp1.Server.Data
{
    public class Topic
    {
        [Key]
        public int Id { get; set; }
        [Required, Column(TypeName = "varchar(300)")]
        public string Name { get; set; }
    }
}